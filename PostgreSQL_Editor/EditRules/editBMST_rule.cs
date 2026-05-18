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
    public partial class editBMST_rule : Form
    {
        private NpgsqlConnection npgsql;
        private BindingList<material_type_rule> systemTypeRules = new BindingList<material_type_rule>();
        private List<material_type_rule> newSystemTypeRules = new List<material_type_rule>();
        private List<material_type_rule> changedSystemTypeRules = new List<material_type_rule>();
        private List<system_type> filteredSystemTypes = new List<system_type>();
        private DgvChangeDetector _dgvChangeDetector;

        public editBMST_rule()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public static void Execute(Form parent, NpgsqlConnection npgsql)
        {
            using (var form = new editBMST_rule())
            {
                form.npgsql = npgsql;
                form.ShowDialog(parent);
            }
        }

        private void EditBaseMaterialSystemType_Load(object sender, EventArgs e)
        {
            CreateDgvChangeDetector();
            cbBaseMaterial.DataSource = standardLists.baseMaterials;
            cbBaseMaterial.DisplayMember = "visiblename";
            cbBaseMaterial.SelectedIndex = 0;
            cbSystemType.DataSource = standardLists.systemTypes;
            cbSystemType.DisplayMember = "name";
            cbSystemType.SelectedIndex = 0;
            systemTypeRules = new BindingList<material_type_rule>(standardLists.baseMaterialSystemTypeRules);
            dgv.DataSource = systemTypeRules;
            _dgvChangeDetector.TakeSnapshot();
            updateLbInfo();
        }

        private void updateLbInfo()
        {
            lbInfo.Text = "Rules: " + systemTypeRules.Count.ToString() + " | New: " + newSystemTypeRules.Count.ToString() + " | Changed: " + changedSystemTypeRules.Where(x => !x.check).Count().ToString() + " | Deleted: " + changedSystemTypeRules.Where(x => x.check).Count().ToString();
        }   

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool exists = systemTypeRules.Any(x => x.base_material_id == ((base_material)cbBaseMaterial.SelectedItem).id && 
                          x.system_type_id == ((system_type)cbSystemType.SelectedItem).id && 
                          x.rule_version == (int)nudVersion.Value && x.is_active == cbActive.Checked);
            if (exists)
            {
                MessageBox.Show(this, "This rule is already implemented", "Already implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            material_type_rule systemTypeRule = new material_type_rule();
            systemTypeRule.id = Guid.NewGuid();
            systemTypeRule.base_material_id = ((base_material)cbBaseMaterial.SelectedItem).id;
            systemTypeRule.base_material = ((base_material)cbBaseMaterial.SelectedItem).visiblename;
            systemTypeRule.system_type_id = ((system_type)cbSystemType.SelectedItem).id;
            systemTypeRule.system_type = ((system_type)cbSystemType.SelectedItem).name;
            systemTypeRule.is_active = cbActive.Checked;
            systemTypeRule.rule_version = (int)nudVersion.Value;
            systemTypeRules.Add(systemTypeRule);
            newSystemTypeRules.Add(systemTypeRule);
            updateLbInfo();
            _dgvChangeDetector.TakeSnapshot();
            btnCreateSQL.Enabled=newSystemTypeRules.Count > 0 || changedSystemTypeRules.Count > 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditBaseMaterialSystemType_FormClosing(object sender, FormClosingEventArgs e)
        {
            //var changedRows = _dgvChangeDetector.GetChangedRows().ToList();
            if (changedSystemTypeRules.Count > 0 || newSystemTypeRules.Count > 0)
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
                string sqlinsert = "INSERT INTO material_type_rule (id,base_material_id,system_type_id,rule_version,is_active,created_ts,created_by,modified_ts,modified_by) VALUES ";
                string sqlupdate = "UPDATE material_type_rule SET ";
                string sqldelete = "DELETE FROM material_type_rule WHERE id = ";
                string s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                foreach (var rule in newSystemTypeRules)
                {
                    // INSERT-Logik für neue Regeln
                    string sql = "('" + rule.id.ToString() + "'::uuid,'" + rule.base_material_id.ToString() + "'::uuid,'" + rule.system_type_id.ToString() +
                        "'::uuid, " + rule.rule_version.ToString() + "," + rule.is_active.ToString().ToLower() + ",'" + s + "','pg_editor','" + s + "','pg_editor')";
                    sqllist.Add(sqlinsert + sql + ";");
                }
                foreach (var rule in changedSystemTypeRules)
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
                        sql = sqlupdate + "is_active = " + rule.is_active.ToString().ToLower() + ", rule_version = " 
                                        + rule.rule_version + ", modified_ts = '" + s + "', modified_by = 'pg_editor' "
                                        + " WHERE id = '" + rule.id.ToString() + "'::uuid";
                    }
                    sqllist.Add(sql + ";");
                }
                File.WriteAllLines(sfd.FileName, sqllist);
                newSystemTypeRules.Clear();
                changedSystemTypeRules.Clear();
                CreateDgvChangeDetector();
                btnAdd.Enabled = newSystemTypeRules.Count > 0 || changedSystemTypeRules.Count > 0;
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
                material_type_rule changedRule = changedRow.DataBoundItem as material_type_rule;
                string jsonNew = JsonSerializer.Serialize(changedRule);
                var x = from rule in changedSystemTypeRules where JsonSerializer.Serialize(rule) == jsonNew select rule;
                if (x.Count() == 0)
                {
                    changedSystemTypeRules.Add(changedRow.DataBoundItem as material_type_rule);
                    updateLbInfo();
                }
                Action updateButton = () =>
                {
                    btnCreateSQL.Enabled = (newSystemTypeRules?.Count ?? 0) > 0 || changedSystemTypeRules.Count > 0;
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

        private void cbBaseMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
