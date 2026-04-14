using Npgsql;
using PostgreSQL_Editor.Utilities;
using PostgreSQL_Editor.Global;
using PostgreSQL_Editor.DBUtils;
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
    public partial class editWedge_rule : Form
    {
        private NpgsqlConnection npgsql;
        private List<material_type_rule> filteredMaterialRules = new List<material_type_rule>();
        private BindingList<wedge_rule> wedgeRules = new BindingList<wedge_rule>();
        private List<wedge_rule> newWedgeRules = new List<wedge_rule>();
        private List<wedge_rule> changedWedgeRules = new List<wedge_rule>();
        private List<frame_type> filteredFrameTypes = new List<frame_type>();
        private List<material_type> filteredMaterialTypes = new List<material_type>();
        private DgvChangeDetector _dgvChangeDetector;

        public editWedge_rule()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public static void Execute(Form owner, NpgsqlConnection npgsql)
        {
            using (var form = new editWedge_rule())
            {
                form.npgsql = npgsql;
                form.ShowDialog(owner);
            }
        }

        private void editWedge_rule_Load(object sender, EventArgs e)
        {
            CreateDgvChangeDetector();
            cbSystemType.DataSource = standardLists.systemTypesAllowed;
            cbSystemType.DisplayMember = "name";
            cbSystemType.SelectedIndex = 0;
            filteredFrameTypes = (filteredFrameTypes.Count == 0) ? standardLists.frameTypes : filteredFrameTypes;
            cbFrameType.DataSource = standardLists.frameTypes;
            cbFrameType.DisplayMember = "name";
            cbFrameType.SelectedIndex = 0;
            cbMaterialType.DataSource = standardLists.materialTypes;
            cbMaterialType.DisplayMember = "name";
            cbMaterialType.SelectedIndex = 0;
            cbWedge.DataSource = standardLists.wedgeTypes;
            cbWedge.DisplayMember = "name";
            cbWedge.SelectedIndex = 0;
            wedgeRules = new BindingList<wedge_rule>(standardLists.wedgeRules);
            dgv.DataSource = wedgeRules;
            _dgvChangeDetector.TakeSnapshot();
            updateLbInfo();
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
                wedge_rule changedRule = changedRow.DataBoundItem as wedge_rule;
                string jsonNew = JsonSerializer.Serialize(changedRule);
                var x = from rule in changedWedgeRules where JsonSerializer.Serialize(rule) == jsonNew select rule;
                if (x.Count() == 0)
                {
                    changedWedgeRules.Add(changedRow.DataBoundItem as wedge_rule);
                    updateLbInfo();
                }
                Action updateButton = () =>
                {
                    btnCreateSQL.Enabled = (newWedgeRules?.Count ?? 0) > 0 || changedWedgeRules.Count > 0;
                };

                if (btnCreateSQL.InvokeRequired)
                    btnCreateSQL.BeginInvoke(updateButton);
                else
                    updateButton();
            };
        }

        private void updateLbInfo()
        {
            lbInfo.Text = "Rules: " + wedgeRules.Count.ToString() + " | New: " + newWedgeRules.Count.ToString() + " | Changed: " + changedWedgeRules.Where(x => !x.check).Count().ToString() + " | Deleted: " + changedWedgeRules.Where(x => x.check).Count().ToString();
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
                string sqlinsert = "INSERT INTO wedge_rule (id,system_type_id, frame_type_id,material_type_id,wedge_type_id,rule_version,is_active,created_ts,created_by,modified_ts,modified_by) VALUES ";
                string sqlupdate = "UPDATE wedge_rule SET ";
                string sqldelete = "DELETE FROM wedge_rule WHERE id = ";
                foreach (var rule in newWedgeRules)
                {
                    // INSERT-Logik für neue Regeln
                    string s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    string sql = "('" + rule.id.ToString() + "'::uuid,'" + rule.system_type_id.ToString() + "'::uuid,'" + 
                                        rule.frame_type_id.ToString() + "'::uuid,'" + rule.material_type_id.ToString() + "'::uuid,'" +
                                        rule.wedge_type_id.ToString() + "'::uuid," + rule.rule_version.ToString() + "," + 
                                        rule.is_active.ToString().ToLower() + "," + ",'" + s + "','pg_editor','" + s + "','pg_editor')";
                    sqllist.Add(sqlinsert + sql + ";");
                }
                foreach (var rule in changedWedgeRules)
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
                        sql = sqlupdate + "is_active = " + rule.is_active.ToString().ToLower() + ", rule_version = " + rule.rule_version + " WHERE id = '" + rule.id.ToString() + "'";
                    }
                    sqllist.Add(sql + ";");
                }
                File.WriteAllLines(sfd.FileName, sqllist);
                newWedgeRules.Clear();
                changedWedgeRules.Clear();
                CreateDgvChangeDetector();
                btnAdd.Enabled = newWedgeRules.Count > 0 || changedWedgeRules.Count > 0;
            }
        }

        private void btnCreateSQL_Click(object sender, EventArgs e)
        {
            CreateSQLStatements();
        }

        private void editBMSTFTMT_rule_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changedWedgeRules.Count > 0 || newWedgeRules.Count > 0)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool exists = wedgeRules.Any(x => x.system_type_id == ((system_type)cbSystemType.SelectedItem).id &&
                          x.material_type_id == ((material_type)cbMaterialType.SelectedItem).id &&
                          x.frame_type_id == ((frame_type)cbFrameType.SelectedItem).id &&
                          x.wedge_type_id == ((wedge_type)cbWedge.SelectedItem).id &&
                          x.is_active == cbActive.Checked && x.rule_version == (int)nudVersion.Value);
            if (exists)
            {
                MessageBox.Show(this, "This rule is already implemented", "Already implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            wedge_rule wedgeRule = new wedge_rule();
            wedgeRule.id = Guid.NewGuid();
            wedgeRule.system_type_id = ((system_type)cbSystemType.SelectedItem).id;
            wedgeRule.system_type = ((system_type)cbSystemType.SelectedItem).name;
            wedgeRule.frame_type_id = ((frame_type)cbFrameType.SelectedItem).id;
            wedgeRule.frame_type = ((frame_type)cbFrameType.SelectedItem).name;
            wedgeRule.material_type_id = ((material_type)cbMaterialType.SelectedItem).id;
            wedgeRule.material_type = ((material_type)cbMaterialType.SelectedItem).name;
            wedgeRule.wedge_type_id = ((wedge_type)cbWedge.SelectedItem).id;
            wedgeRule.wedge_type = ((wedge_type)cbWedge.SelectedItem).name;
            wedgeRule.is_active = cbActive.Checked;
            wedgeRule.rule_version = (int)nudVersion.Value;
            wedgeRules.Add(wedgeRule);
            newWedgeRules.Add(wedgeRule);
            updateLbInfo();
            _dgvChangeDetector.TakeSnapshot();
            btnCreateSQL.Enabled = newWedgeRules.Count > 0 || changedWedgeRules.Count > 0;
        }
        private void cbSystemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            filteredFrameTypes = standardLists.systemTypeFrameTypeRules.Where(x => x.system_type_id == ((system_type)cbSystemType.SelectedItem).id && x.is_active)
                                 .Select(x => new frame_type() { id = x.frame_type_id, name = x.frame_type }).Distinct().OrderBy(x => x.name).ToList();
            cbFrameType.DataSource = filteredFrameTypes;
        }
    }
}
