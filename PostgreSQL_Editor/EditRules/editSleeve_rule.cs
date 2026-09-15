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
    public partial class editSleeve_rule : Form
    {
        private NpgsqlConnection npgsql;
        private SortableBindingList<sleeve_rule> sleeveRules = new SortableBindingList<sleeve_rule>();
        private List<sleeve_rule> newSleeveRules = new List<sleeve_rule>();
        private List<sleeve_rule> changedSleeveRules = new List<sleeve_rule>();
        private List<system_type> filteredSystemTypes = new List<system_type>();
        private List<frame_type> filteredFrameTypes = new List<frame_type>();
        private DgvChangeDetector _dgvChangeDetector;
        private BindingSource _bindingSource = new BindingSource();

        public editSleeve_rule()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public static void Execute(Form parent, NpgsqlConnection npgsql)
        {
            using (var form = new editSleeve_rule())
            {
                form.npgsql = npgsql;
                form.ShowDialog(parent);
            }
        }

        private void EditSleeveRule_Load(object sender, EventArgs e)
        {
            CreateDgvChangeDetector();
            cbBaseMaterial.DataSource = standardLists.baseMaterials;
            cbBaseMaterial.DisplayMember = "visiblename";
            cbBaseMaterial.SelectedIndex = 0;
            filteredSystemTypes = (filteredSystemTypes.Count == 0) ? standardLists.systemTypes : filteredSystemTypes;
            cbSystemType.DataSource = filteredSystemTypes;
            cbSystemType.DisplayMember = "name";
            cbSystemType.SelectedIndex = 0;
            cbFrameType.DataSource = standardLists.frameTypes.Where(ft => ft.name.Contains("RR")).ToList();
            cbFrameType.DisplayMember = "name";
            cbFrameType.SelectedIndex = 0;
            var initial = standardLists.sleeveRules.Select(x => new sleeve_rule()
            {
                check = x.check,
                id = x.id,
                base_material_id = x.base_material_id,
                base_material = x.base_material,
                system_type_id = x.system_type_id,
                system_type = x.system_type,
                frame_type_id = x.frame_type_id,
                frame_type = x.frame_type,
                rule_version = x.rule_version,
                is_active = x.is_active,
                is_mandatory = x.is_mandatory
            }).ToList();
            sleeveRules = new SortableBindingList<sleeve_rule>();
            foreach (var rule in initial)
            {
                sleeveRules.Add(rule);
            }
            _bindingSource.DataSource = sleeveRules;
            dgv.DataSource = _bindingSource; 
            _dgvChangeDetector.TakeSnapshot();
            updateLbInfo();
        }

        private void updateLbInfo()
        {
            lbInfo.Text = "Rules: " + sleeveRules.Count.ToString() + " | New: " + newSleeveRules.Count.ToString() + " | Changed: " + changedSleeveRules.Where(x => !x.check).Count().ToString() + " | Deleted: " + changedSleeveRules.Where(x => x.check).Count().ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool exists = sleeveRules.Any(x => x.base_material_id == ((base_material)cbBaseMaterial.SelectedItem).id &&
                          x.system_type_id == ((system_type)cbSystemType.SelectedItem).id &&
                          x.frame_type_id == ((frame_type)cbFrameType.SelectedItem).id &&
                          x.is_mandatory == cbMandatory.Checked &&
                          x.is_active == cbActive.Checked &&
                          x.rule_version == (int)nudVersion.Value);
            if (exists)
            {
                MessageBox.Show(this, "This rule is already implemented", "Already implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sleeve_rule sleeveRule = new sleeve_rule();
            sleeveRule.id = Guid.NewGuid();
            sleeveRule.base_material_id = ((base_material)cbBaseMaterial.SelectedItem).id;
            sleeveRule.base_material = ((base_material)cbBaseMaterial.SelectedItem).name;
            sleeveRule.system_type_id = ((system_type)cbSystemType.SelectedItem).id;
            sleeveRule.system_type = ((system_type)cbSystemType.SelectedItem).name;
            sleeveRule.frame_type_id = ((frame_type)cbFrameType.SelectedItem).id;
            sleeveRule.frame_type = ((frame_type)cbFrameType.SelectedItem).name;
            sleeveRule.is_mandatory = cbMandatory.Checked;
            sleeveRule.is_active = cbActive.Checked;
            sleeveRule.rule_version = (int)nudVersion.Value;
            sleeveRules.Add(sleeveRule);
            newSleeveRules.Add(sleeveRule);
            updateLbInfo();
            _dgvChangeDetector.TakeSnapshot();
            btnCreateSQL.Enabled = newSleeveRules.Count > 0 || changedSleeveRules.Count > 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditSystemTypeFrameType_FormClosing(object sender, FormClosingEventArgs e)
        {
            //var changedRows = _dgvChangeDetector.GetChangedRows().ToList();
            if (changedSleeveRules.Count > 0 || newSleeveRules.Count > 0)
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
                string sqlinsert = "INSERT INTO sleeve_rule (id,base_material_id,system_type_id,frame_type_id,rule_version,is_active,is_mandatory,created_ts,created_by,modified_ts,modified_by) VALUES ";
                string sqlupdate = "UPDATE sleeve_rule SET ";
                string sqldelete = "DELETE FROM sleeve_rule WHERE id = '";
                foreach (var rule in newSleeveRules)
                {
                    // INSERT-Logik für neue Regeln
                    string s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    string sql = "('" + rule.id.ToString() + "'::uuid,'" + 
                                 rule.base_material_id.ToString() + "'::uuid,'" + 
                                 rule.system_type_id.ToString() + "'::uuid,'" + 
                                 rule.frame_type_id.ToString() + "'::uuid,'" + 
                                 rule.rule_version.ToString() + "','" + 
                                 rule.is_active.ToString().ToLower() + "','" + 
                                 rule.is_mandatory.ToString().ToLower() + "','" +
                                 s + "','pg_editor','" + s + "','pg_editor')";
                    sqllist.Add(sqlinsert + sql + ";");
                }
                foreach (var rule in changedSleeveRules)
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
                                        + ", is_mandatory = " + rule.is_mandatory.ToString().ToLower()
                                        + ", modified_ts = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) 
                                        + "', modified_by = 'pg_editor' " + " WHERE id = '" + rule.id.ToString() + "'::uuid";
                        sqllist.Add(sql + ";");
                    }
                }
                File.WriteAllLines(sfd.FileName, sqllist);
                newSleeveRules.Clear();
                changedSleeveRules.Clear();
                CreateDgvChangeDetector();
                btnAdd.Enabled = newSleeveRules.Count > 0 || changedSleeveRules.Count > 0;
            }
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
                sleeve_rule changedRule = changedRow.DataBoundItem as sleeve_rule;
                if (changedRule == null) return;
                string jsonNew = JsonSerializer.Serialize(changedRule);
                var x = from rule in changedSleeveRules where JsonSerializer.Serialize(rule) == jsonNew select rule;
                if (!x.Any())
                {
                    changedSleeveRules.Add(changedRule);
                    updateLbInfo();
                }
                Action updateButton = () =>
                {
                    btnCreateSQL.Enabled = (newSleeveRules?.Count ?? 0) > 0 || changedSleeveRules.Count > 0;
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
            filteredSystemTypes = standardLists.baseMaterialSystemTypeRules.Where(x => x.base_material_id == ((base_material)cbBaseMaterial.SelectedItem).id && x.is_active)
                                  .Select(x => new system_type() { id = x.system_type_id, name = x.system_type }).Distinct().OrderBy(x => x.name).ToList();
            cbSystemType.DataSource = filteredSystemTypes;
        }
    }
}
