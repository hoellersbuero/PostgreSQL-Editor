using Npgsql;
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
using PostgreSQL_Editor.Global;
using PostgreSQL_Editor.DBUtils;

namespace PostgreSQL_Editor.EditRules
{
    public partial class editBMSTFT_rule : Form
    {
        private NpgsqlConnection npgsql;
        private BindingList<frame_type_rule> frameTypeRules = new BindingList<frame_type_rule>();
        private List<frame_type_rule> newFrameTypeRules = new List<frame_type_rule>();
        private List<frame_type_rule> changedFrameTypeRules = new List<frame_type_rule>();
        private List<system_type> filteredSystemTypes = new List<system_type>();
        private List<frame_type> filteredFrameTypes = new List<frame_type>();
        private DgvChangeDetector _dgvChangeDetector;


        public editBMSTFT_rule()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public static void Execute(Form parent, NpgsqlConnection npgsql)
        {
            using (var form = new editBMSTFT_rule())
            {
                form.npgsql = npgsql;
                form.ShowDialog(parent);
            }
        }

        private void editBMSTFT_rule_Load(object sender, EventArgs e)
        {
            CreateDgvChangeDetector();
            cbBaseMaterial.DataSource = standardLists.baseMaterials;
            cbBaseMaterial.DisplayMember = "visiblename";
            cbBaseMaterial.SelectedIndex = 0;
            filteredSystemTypes = (filteredSystemTypes.Count == 0) ? standardLists.systemTypes : filteredSystemTypes;
            cbSystemType.DataSource = filteredSystemTypes;
            cbSystemType.DisplayMember = "name";
            cbSystemType.SelectedIndex = 0;
            filteredFrameTypes = (filteredFrameTypes.Count == 0) ? standardLists.frameTypes : filteredFrameTypes;
            cbFrameType.DataSource = standardLists.frameTypes;
            cbFrameType.DisplayMember = "name";
            cbFrameType.SelectedIndex = 0;
            frameTypeRules = new BindingList<frame_type_rule>(standardLists.frameTypeRules);
            dgv.DataSource = frameTypeRules;
            _dgvChangeDetector.TakeSnapshot();
            updateLbInfo();
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
                string sqlinsert = "INSERT INTO frame_type_rule (id,base_material_id,system_type_id, frame_type_id,rule_version,is_active,is_special,created_ts,created_by,modified_ts,modified_by) VALUES ";
                string sqlupdate = "UPDATE frame_type_rule SET ";
                string sqldelete = "DELETE FROM frame_type_rule WHERE id = ";
                string s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                foreach (var rule in newFrameTypeRules)
                {
                    // INSERT-Logik für neue Regeln
                    string sql = "('" + rule.id.ToString() + "'::uuid,'" + 
                                        rule.base_material_id.ToString() + "'::uuid,'" +
                                        rule.system_type_id.ToString() + "'::uuid,'" + 
                                        rule.frame_type_id.ToString() + "'::uuid," + 
                                        rule.rule_version.ToString() + "," + 
                                        rule.is_active.ToString().ToLower() + "," +
                                        rule.is_special.ToString().ToLower() + ",'" + s + "','pg_editor','" + s + "','pg_editor')";
                    sqllist.Add(sqlinsert + sql + ";");
                }
                foreach (var rule in changedFrameTypeRules)
                {
                    string sql = String.Empty;
                    if (rule.check)
                    {
                        // DELETE-Logik für deaktivierte Regeln
                        sql = sqldelete + "'" + rule.id.ToString() + "'::uuid";
                    }
                    else
                    {
                        // UPDATE-Logik für geänderte Regeln
                        sql = sqlupdate + "is_active = " + rule.is_active.ToString().ToLower() + ", is_special = " + rule.is_special.ToString().ToLower() + 
                                        ", rule_version = " + rule.rule_version + ", modified_ts = '" + s + "', modified_by = 'pg_editor' "
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
                frame_type_rule changedRule = changedRow.DataBoundItem as frame_type_rule;
                string jsonNew = JsonSerializer.Serialize(changedRule);
                var x = from rule in changedFrameTypeRules where JsonSerializer.Serialize(rule) == jsonNew select rule;
                if (x.Count() == 0)
                {
                    changedFrameTypeRules.Add(changedRow.DataBoundItem as frame_type_rule);
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

        private void editBMSTFTMT_rule_FormClosing(object sender, FormClosingEventArgs e)
        {
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void updateLbInfo()
        {
            lbInfo.Text = "Rules: " + frameTypeRules.Count.ToString() + " | New: " + newFrameTypeRules.Count.ToString() + " | Changed: " + changedFrameTypeRules.Where(x => !x.check).Count().ToString() + " | Deleted: " + changedFrameTypeRules.Where(x => x.check).Count().ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool exists = frameTypeRules.Any(x => x.base_material_id == ((base_material)cbBaseMaterial.SelectedItem).id &&
                          x.system_type_id == ((system_type)cbSystemType.SelectedItem).id &&
                          x.frame_type_id == ((frame_type)cbFrameType.SelectedItem).id &&
                          x.is_active == cbActive.Checked && x.is_special == cbSpecial.Checked &&
                          x.rule_version == (int)nudVersion.Value);
            if (exists)
            {
                MessageBox.Show(this, "This rule is already implemented", "Already implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frame_type_rule frameTypeRule = new frame_type_rule();
            frameTypeRule.id = Guid.NewGuid();
            frameTypeRule.base_material_id = ((base_material)cbBaseMaterial.SelectedItem).id;
            frameTypeRule.base_material = ((base_material)cbBaseMaterial.SelectedItem).visiblename;
            frameTypeRule.system_type_id = ((system_type)cbSystemType.SelectedItem).id;
            frameTypeRule.system_type = ((system_type)cbSystemType.SelectedItem).name;
            frameTypeRule.frame_type_id = ((frame_type)cbFrameType.SelectedItem).id;
            frameTypeRule.frame_type = ((frame_type)cbFrameType.SelectedItem).name;
            frameTypeRule.is_active = cbActive.Checked;
            frameTypeRule.rule_version = (int)nudVersion.Value;
            frameTypeRules.Add(frameTypeRule);
            updateLbInfo();
            newFrameTypeRules.Add(frameTypeRule);
            _dgvChangeDetector.TakeSnapshot();
            btnCreateSQL.Enabled = newFrameTypeRules.Count > 0 || changedFrameTypeRules.Count > 0;
        }

        private void cbBaseMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            filteredSystemTypes = standardLists.baseMaterialSystemTypeRules.Where(x => x.base_material_id == ((base_material)cbBaseMaterial.SelectedItem).id && x.is_active)
                                  .Select(x => new system_type() { id = x.system_type_id, name = x.system_type }).Distinct().OrderBy(x => x.name).ToList();
            cbSystemType.DataSource = filteredSystemTypes;
        }
    }
}
