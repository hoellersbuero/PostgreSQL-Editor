using Npgsql;
using PostgreSQL_Editor.DBUtils;
using PostgreSQL_Editor.Global;
using PostgreSQL_Editor.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgreSQL_Editor.EditRules
{
    public partial class EditFrameSleeveRule : Form
    {
        private NpgsqlConnection _npgsql;
        private DgvChangeDetector _dgvChangeDetector;
        private BindingList<frame_sleeve_rule> frameSleeveRules = new BindingList<frame_sleeve_rule>();
        private List<frame_sleeve_rule> newFrameSleeveRules = new List<frame_sleeve_rule>();
        private List<frame_sleeve_rule> changedFrameSleeveRules = new List<frame_sleeve_rule>();
        private BindingSource _bindingSource = new BindingSource();

        public EditFrameSleeveRule()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public static void Execute(Form parent, NpgsqlConnection npgsql)
        {
            using (var form = new EditFrameSleeveRule())
            {
                form.ShowDialog(parent);
                form._npgsql = npgsql;
            }
        }

        private void EditFrameSleeveRule_Load(object sender, EventArgs e)
        {
            CreateDgvChangeDetector();
            cbFrame.DataSource = standardLists.frames.Where(f => f.name.Contains("RR")).OrderBy(f => f.name.Split(new char[] { '-' })[1]).ToList();
            cbFrame.DisplayMember = "name";
            cbFrame.ValueMember = "id";
            cbFrame.SelectedIndex = 0;
            cbSleeve.DataSource = standardLists.sleeveTypes;
            cbSleeve.DisplayMember = "name";
            cbSleeve.ValueMember = "id";
            cbSleeve.SelectedIndex = 0;
            var sleeveMaterialIds = standardLists.sleeveTypes.Select(s => s.material_type_id).Distinct().ToList();
            var sleeveMaterials = standardLists.materialTypes.Where(m => sleeveMaterialIds.Contains(m.id)).ToList();
            cbSleeveMaterial.DataSource = sleeveMaterials;
            cbSleeveMaterial.DisplayMember = "name";
            cbSleeveMaterial.ValueMember = "id";
            cbSleeveMaterial.SelectedIndex = 0;
            var initial = standardLists.frameSleeveRules.Select(x => new frame_sleeve_rule()
            {
                check = x.check,
                id = x.id,
                frame_id = x.frame_id,
                frame_name = x.frame_name,
                sleeve_id = x.sleeve_id,
                sleeve_name = x.sleeve_name,
                rule_version = x.rule_version,
                is_active = x.is_active,
            }).ToList();
            frameSleeveRules = new BindingList<frame_sleeve_rule>(initial);
            _bindingSource.DataSource = frameSleeveRules    ;
            dgv.DataSource = _bindingSource;
            _dgvChangeDetector.TakeSnapshot();
            updateLbInfo();
        }

        private void CreateDgvChangeDetector()
        {
            _dgvChangeDetector?.Dispose();
            _dgvChangeDetector = new Utilities.DgvChangeDetector(dgv);
            _dgvChangeDetector.RowChanged += (s, changedRow) =>
            {
                // Schutz: Nur wenn Row gültig und gebunden
                if (changedRow == null) return;
                if (changedRow.Index < 0) return;
                if (changedRow.DataBoundItem == null) return;

                changedRow.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                frame_sleeve_rule changedRule = changedRow.DataBoundItem as frame_sleeve_rule;
                if (changedRule == null) return;
                string jsonNew = JsonSerializer.Serialize(changedRule);
                var x = from rule in changedFrameSleeveRules where JsonSerializer.Serialize(rule) == jsonNew select rule;
                if (!x.Any())
                {
                    changedFrameSleeveRules.Add(changedRule);
                    updateLbInfo();
                }
                Action updateButton = () =>
                {
                    btnCreateSQL.Enabled = (newFrameSleeveRules?.Count ?? 0) > 0 || changedFrameSleeveRules.Count > 0;
                };

                if (btnCreateSQL.InvokeRequired)
                    btnCreateSQL.BeginInvoke(updateButton);
                else
                    updateButton();
            };
        }

        private void updateLbInfo()
        {
            lbInfo.Text = "Rules: " + frameSleeveRules.Count.ToString() + " | New: " + newFrameSleeveRules.Count.ToString() + " | Changed: " + changedFrameSleeveRules.Where(x => !x.check).Count().ToString() + " | Deleted: " + changedFrameSleeveRules.Where(x => x.check).Count().ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool exists = frameSleeveRules.Any(x => x.frame_id == ((frame)cbFrame.SelectedItem).id &&
                            x.sleeve_id == ((sleeve_type)cbSleeve.SelectedItem).id &&   
                          x.is_active == cbActive.Checked &&
                          x.rule_version == (int)nudVersion.Value);
            if (exists)
            {
                MessageBox.Show(this, "This rule is already implemented", "Already implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frame_sleeve_rule frameSleeveRule = new frame_sleeve_rule();
            frameSleeveRule.id = Guid.NewGuid();
            frameSleeveRule.frame_id = ((frame)cbFrame.SelectedItem).id;
            frameSleeveRule.frame_name = ((frame)cbFrame.SelectedItem).name;
            frameSleeveRule.sleeve_id = ((sleeve_type)cbSleeve.SelectedItem).id;
            frameSleeveRule.sleeve_name = ((sleeve_type)cbSleeve.SelectedItem).name;
            frameSleeveRule.is_active = cbActive.Checked;
            frameSleeveRule.rule_version = (int)nudVersion.Value;
            frameSleeveRules.Add(frameSleeveRule);
            newFrameSleeveRules.Add(frameSleeveRule);
            updateLbInfo();
            _dgvChangeDetector.TakeSnapshot();
            btnCreateSQL.Enabled = newFrameSleeveRules.Count > 0 || changedFrameSleeveRules.Count > 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditFrameSleeveRule_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changedFrameSleeveRules.Count > 0 || newFrameSleeveRules.Count > 0)
            {
                var result = MessageBox.Show(this, "Do you want to save the changes?", "Save changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    CreateSQLStatements();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true; // Schließen abbrechen
                }
            }
        }

        private void CreateSQLStatements()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save changes to SQL-file";
            sfd.Filter = "SQL-file (*.sql)|*.sql";
            sfd.DefaultExt = "sql";
            List<string> sqllist = new List<string>();
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                // Speichern der Änderungen in der Datenbank
                string sqlinsert = "INSERT INTO frame_sleeve_rule (id,frame_id,sleeve_type_id,rule_version,is_active,created_ts,created_by,modified_ts,modified_by) VALUES ";
                string sqlupdate = "UPDATE frame_sleeve_rule SET ";
                string sqldelete = "DELETE FROM frame_sleeve_rule WHERE id = '";
                foreach (var rule in newFrameSleeveRules)
                {
                    // INSERT-Logik für neue Regeln
                    string s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    string sql = "('" + rule.id.ToString() + "'::uuid,'" +
                                 rule.frame_id.ToString() + "'::uuid,'" +
                                 rule.sleeve_id.ToString() + "'::uuid,'" +
                                 rule.rule_version.ToString() + "','" +
                                 rule.is_active.ToString().ToLower() + "','" +
                                 s + "','pg_editor','" + s + "','pg_editor')";
                    sqllist.Add(sqlinsert + sql + ";");
                }
                foreach (var rule in changedFrameSleeveRules)
                {
                    string sql = String.Empty;
                    if (rule.check) // Delete row
                    {
                        // Delete-Logik für gelöschte Regeln
                        sql = sqldelete + rule.id.ToString() + "'";
                        sqllist.Add(sql + ";");
                    }
                    else
                    {
                        // UPDATE-Logik für geänderte Regeln
                        sql = sqlupdate + "is_active = " + rule.is_active.ToString().ToLower() + ", rule_version = " + rule.rule_version
                                        + ", modified_ts = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)
                                        + "', modified_by = 'pg_editor' " + " WHERE id = '" + rule.id.ToString() + "'::uuid";
                        sqllist.Add(sql + ";");
                    }
                }
                File.WriteAllLines(sfd.FileName, sqllist);
                newFrameSleeveRules.Clear();
                changedFrameSleeveRules.Clear();
                CreateDgvChangeDetector();
                btnAdd.Enabled = newFrameSleeveRules.Count > 0 || changedFrameSleeveRules.Count > 0;
            }
        }

        private void btnCreateSQL_Click(object sender, EventArgs e)
        {
            CreateSQLStatements();
        }

        private void cbSleeveMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sleeveList = standardLists.sleeveTypes.Where(s => s.material_type_id.ToString() == (cbSleeveMaterial.SelectedItem as material_type).id.ToString()).OrderBy(s => int.Parse(Regex.Match(s.name, @"\d+").Value)).ThenBy(s => s.name).ToList();
            cbSleeve.DataSource = sleeveList;
        }
    }
}
