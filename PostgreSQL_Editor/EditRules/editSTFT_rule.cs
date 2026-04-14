using Npgsql;
using PostgreSQL_Editor.DBUtils;
using PostgreSQL_Editor.Global;
using PostgreSQL_Editor.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace PostgreSQL_Editor.EditRules
{
    public partial class editSTFT_rule : Form
    {
        private NpgsqlConnection npgsql;
        private BindingList<system_type_frame_type_rule> frameTypeRules = new BindingList<system_type_frame_type_rule>();
        private List<system_type_frame_type_rule> newFrameTypeRules = new List<system_type_frame_type_rule>();
        private List<system_type_frame_type_rule> changedFrameTypeRules = new List<system_type_frame_type_rule>();
        private DgvChangeDetector _dgvChangeDetector;

        public editSTFT_rule()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public static void Execute(Form parent, NpgsqlConnection npgsql)
        {
            using (var form = new editSTFT_rule())
            {
                form.npgsql = npgsql;
                form.ShowDialog(parent);
            }
        }

        private void EditSystemTypeFrameType_Load(object sender, EventArgs e)
        {
            CreateDgvChangeDetector();
            cbSystemType.DataSource = standardLists.systemTypesAllowed;
            cbSystemType.DisplayMember = "name";
            cbSystemType.SelectedIndex = 0;
            cbFrameType.DataSource = standardLists.frameTypes;
            cbFrameType.DisplayMember = "name";
            cbFrameType.SelectedIndex = 0;
            frameTypeRules = new BindingList<system_type_frame_type_rule>(standardLists.systemTypeFrameTypeRules);
            dgv.DataSource = frameTypeRules;
            _dgvChangeDetector.TakeSnapshot();
            updateLbInfo();
        }

        private void updateLbInfo()
        {
            lbInfo.Text = "Rules: " + frameTypeRules.Count.ToString() + " | New: " + newFrameTypeRules.Count.ToString() + " | Changed: " + changedFrameTypeRules.Where(x => !x.check).Count().ToString() + " | Deleted: " + changedFrameTypeRules.Where(x => x.check).Count().ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool exists = frameTypeRules.Any(x => x.system_type_id == ((system_type)cbSystemType.SelectedItem).id &&
                          x.frame_type_id == ((frame_type)cbFrameType.SelectedItem).id && 
                          x.rule_version == (int)nudVersion.Value && x.is_active == cbActive.Checked);
            if (exists)
            {
                MessageBox.Show(this, "This rule is already implemented", "Already implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            system_type_frame_type_rule systemTypeRule = new system_type_frame_type_rule();
            systemTypeRule.id = Guid.NewGuid();
            systemTypeRule.system_type_id = ((system_type)cbSystemType.SelectedItem).id;
            systemTypeRule.system_type = ((system_type)cbSystemType.SelectedItem).name;
            systemTypeRule.frame_type_id = ((frame_type)cbFrameType.SelectedItem).id;
            systemTypeRule.frame_type = ((frame_type)cbFrameType.SelectedItem).name;
            systemTypeRule.is_active = cbActive.Checked;
            systemTypeRule.rule_version = (int)nudVersion.Value;
            frameTypeRules.Add(systemTypeRule);
            newFrameTypeRules.Add(systemTypeRule);
            updateLbInfo();
            _dgvChangeDetector.TakeSnapshot();
            btnCreateSQL.Enabled = newFrameTypeRules.Count > 0 || changedFrameTypeRules.Count > 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditSystemTypeFrameType_FormClosing(object sender, FormClosingEventArgs e)
        {
            //var changedRows = _dgvChangeDetector.GetChangedRows().ToList();
            if (changedFrameTypeRules.Count > 0 || newFrameTypeRules.Count > 0)
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
            //var changedRows = _dgvChangeDetector.GetChangedRows().ToList();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save changes to SQL-file";
            sfd.Filter = "SQL-file (*.sql)|*.sql";
            sfd.DefaultExt = "sql";
            List<string> sqllist = new List<string>();
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                // Speichern der Änderungen in der Datenbank
                string sqlinsert = "INSERT INTO system_type_frame_type_rule (id,system_type_id,frame_type_id,rule_version,is_active,created_ts,created_by,modified_ts,modified_by) VALUES ";
                string sqlupdate = "UPDATE system_type_frame_type_rule SET ";
                string sqldelete = "DELETE FROM system_type_frame_type_rule WHERE id = '";
                foreach (var rule in newFrameTypeRules)
                {
                    // INSERT-Logik für neue Regeln
                    string s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    string sql = "('" + rule.id.ToString() + "'::uuid,'" + rule.system_type_id.ToString() + "'::uuid,'" + rule.frame_type_id.ToString() +
                        "'::uuid, " + rule.rule_version.ToString() + "," + rule.is_active.ToString().ToLower() + ",'" + s + "','pg_editor','" + s + "','pg_editor')";
                    sqllist.Add(sqlinsert + sql + ";");
                }
                foreach (var rule in changedFrameTypeRules)
                {
                    string sql = String.Empty;
                    if (rule.check) // Delete row
                    {
                        // Delete-Logik für gelöschte Regeln
                        sql = sqldelete + rule.id.ToString() + "'";
                    }
                    else
                    {
                        // UPDATE-Logik für geänderte Regeln
                        sql = sqlupdate + "is_active = " + rule.is_active.ToString().ToLower() + ", rule_version = " + rule.rule_version
                                        + " WHERE id = '" + rule.id.ToString() + "'::uuid";
                    }
                    sqllist.Add(sql + ";");
                }
                File.WriteAllLines(sfd.FileName, sqllist);
                newFrameTypeRules.Clear();
                changedFrameTypeRules.Clear();
                CreateDgvChangeDetector();
                btnAdd.Enabled = newFrameTypeRules.Count > 0 || changedFrameTypeRules.Count > 0;
            }
        }

        private void CreateDgvChangeDetector()
        {
            _dgvChangeDetector?.Dispose();
            _dgvChangeDetector = new Utilities.DgvChangeDetector(dgv);
            _dgvChangeDetector.RowChanged += (s, changedRow) =>
            {
                // direkt bei Änderung: changedRow enthält die veränderte DataGridViewRow
                // Beispiel: markiere die Zeile visuell
                changedRow.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                system_type_rule changedRule = changedRow.DataBoundItem as system_type_rule;
                string jsonNew = JsonSerializer.Serialize(changedRule);
                var x = from rule in changedFrameTypeRules where JsonSerializer.Serialize(rule) == jsonNew select rule;
                if (x.Count() == 0)
                {
                    changedFrameTypeRules.Add(changedRow.DataBoundItem as system_type_frame_type_rule);
                    updateLbInfo();
                }
                Action updateButton = () =>
                {
                    btnCreateSQL.Enabled = (newFrameTypeRules?.Count ?? 0) > 0 || changedFrameTypeRules.Count > 0;
                };

                if (btnCreateSQL.InvokeRequired)
                    btnCreateSQL.BeginInvoke(updateButton);
                else
                    updateButton();
            };
        }

        private void btnCreateSQL_Click(object sender, EventArgs e)
        {
            CreateSQLStatements();
        }
    }
}
