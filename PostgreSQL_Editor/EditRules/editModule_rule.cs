using Npgsql;
using PostgreSQL_Editor.Utilities;
using PostgreSQL_Editor.Global;
using PostgreSQL_Editor.DBUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace PostgreSQL_Editor.EditRules
{
    public partial class editModule_rule : Form
    {
        private NpgsqlConnection npgsql;
        private BindingList<material_type_rule> filteredMaterialRules = new BindingList<material_type_rule>();
        private BindingList<module_rule> moduleRules = new BindingList<module_rule>();
        private List<module_rule> newModuleRules = new List<module_rule>();
        private List<module_rule> changedModuleRules = new List<module_rule>();
        private List<frame_type> filteredFrameTypes = new List<frame_type>();
        private List<material_type> filteredMaterialTypes = new List<material_type>();
        private DgvChangeDetector _dgvChangeDetector;

        public editModule_rule()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public static void Execute(Form parent, NpgsqlConnection npgsql)
        {
            using (var form = new editModule_rule())
            {
                form.npgsql = npgsql;
                form.ShowDialog(parent);
            }
        }

        private void editModule_rule_Load(object sender, EventArgs e)
        {
            CreateDgvChangeDetector();
            cbSystemType.DataSource = standardLists.systemTypesAllowed;
            cbSystemType.DisplayMember = "name";
            cbSystemType.SelectedIndex = 0;
            cbFrameType.DataSource = (filteredFrameTypes.Count == 0) ? standardLists.frameTypes : filteredFrameTypes;
            cbFrameType.DisplayMember = "name";
            cbFrameType.SelectedIndex = 0;
            cbModuleClass.DataSource = standardLists.moduleClasses;
            cbModuleClass.DisplayMember = "name";
            cbModuleClass.SelectedIndex = 0;
            // List<module> filteredModules = (from module m in standardLists.modules from system_type s in standardLists.systemTypesAllowed where m.name.StartsWith(s.name) select m).ToList();
            // Filter modules (case-insensitive, trimmed) und eindeutige Einträge nach id
            // filteredModules = standardLists.modules
            //    .Where(m => standardLists.systemTypesAllowed
            //        .Any(s => !string.IsNullOrEmpty(m.name) && !string.IsNullOrEmpty(s.name)
            //                  && m.name.Trim().StartsWith(s.name.Trim(), StringComparison.OrdinalIgnoreCase)))
            //    .GroupBy(m => m.id)
            //    .Select(g => g.First()).Distinct()
            //    .ToList();
            // Falls kein Filterergebnis, fallback auf komplette Liste
            // cbModule.DataSource = (filteredModules.Count > 0) ? filteredModules : standardLists.modules;
            moduleRules = new BindingList<module_rule>(standardLists.moduleRules);
            dgv.DataSource =  moduleRules;
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
                string sqlinsert = "INSERT INTO material_type_rule (id,base_material_id,system_type_id, frame_type_id,material_type_id,rule_version,is_active,is_special,created_ts,created_by,modified_ts,modified_by) VALUES ";
                string sqlupdate = "UPDATE material_type_rule SET ";
                string sqldelete = "DELETE FROM material_type_rule WHERE id = ";
                foreach (var rule in newModuleRules)
                {
                    // INSERT-Logik für neue Regeln
                    string s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    string sql = "('" + rule.id.ToString() + "'::uuid,'" + rule.system_type_id.ToString() + "'::uuid,'" +
                                        rule.frame_type_id.ToString() + "'::uuid,'" + rule.module_class_id.ToString() + "'::uuid,'" +
                                        rule.rule_version.ToString() + "," +
                                        rule.is_active.ToString().ToLower() + ",'" + s + "','pg_editor','" + s + "','pg_editor')";
                    sqllist.Add(sqlinsert + sql + ";");
                }
                foreach (var rule in changedModuleRules)
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
                        sql = sqlupdate + "is_active = " + rule.is_active.ToString().ToLower() + ", rule_version = " + rule.rule_version 
                                        + ", modified_ts = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + "', modified_by = 'pg_editor'"
                                        + " WHERE id = '" + rule.id.ToString() + "'";
                    }
                    sqllist.Add(sql + ";");
                }

                File.WriteAllLines(sfd.FileName, sqllist);
                newModuleRules.Clear();
                changedModuleRules.Clear();
                CreateDgvChangeDetector();
                btnAdd.Enabled = newModuleRules.Count > 0 || changedModuleRules.Count > 0;
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
                module_rule changedRule = changedRow.DataBoundItem as module_rule;
                string jsonNew = JsonSerializer.Serialize(changedRule);
                var x = from rule in changedModuleRules where JsonSerializer.Serialize(rule) == jsonNew select rule;
                if (x.Count() == 0)
                {
                    changedModuleRules.Add(changedRow.DataBoundItem as module_rule);
                    updateLbInfo();
                }
                Action updateButton = () =>
                {
                    btnCreateSQL.Enabled = (newModuleRules?.Count ?? 0) > 0 || changedModuleRules.Count > 0;
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

        private void editModule_rule_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changedModuleRules.Count > 0 || newModuleRules.Count > 0)
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
            lbInfo.Text = "Rules: " + moduleRules.Count.ToString() + " | New: " + newModuleRules.Count.ToString() + " | Changed: " + changedModuleRules.Where(x => !x.check).Count().ToString() + " | Deleted: " + changedModuleRules.Where(x => x.check).Count().ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool exists = moduleRules.Any(x => x.system_type_id == ((system_type)cbSystemType.SelectedItem).id &&
                          x.frame_type_id == ((frame_type)cbFrameType.SelectedItem).id &&
                          x.module_class_id == ((material_type)cbModuleClass.SelectedItem).id &&
                          x.is_active == cbActive.Checked && x.priority == (int)nudPriority.Value &&
                          x.rule_version == (int)nudVersion.Value);
            if (exists)
            {
                MessageBox.Show(this, "This rule is already implemented", "Already implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            module_rule moduleRule = new module_rule();
            moduleRule.id = Guid.NewGuid();
            moduleRule.system_type_id = ((system_type)cbSystemType.SelectedItem).id;
            moduleRule.system_type = ((system_type)cbSystemType.SelectedItem).name;
            moduleRule.frame_type_id = ((frame_type)cbFrameType.SelectedItem).id;
            moduleRule.frame_type = ((frame_type)cbFrameType.SelectedItem).name;
            moduleRule.module_class_id = ((material_type)cbModuleClass.SelectedItem).id;
            moduleRule.module_class = ((material_type)cbModuleClass.SelectedItem).name;
            moduleRule.module_class_id = ((material_type)cbModuleClass.SelectedItem).id;
            moduleRule.module_class = ((material_type)cbModuleClass.SelectedItem).name;
            moduleRule.is_active = cbActive.Checked;
            moduleRule.priority = (int)nudPriority.Value;
            moduleRule.rule_version = (int)nudVersion.Value;
            moduleRules.Add(moduleRule);
            newModuleRules.Add(moduleRule);
            updateLbInfo();
            _dgvChangeDetector.TakeSnapshot();
            btnCreateSQL.Enabled = newModuleRules.Count > 0 || changedModuleRules.Count > 0;
        }

        private void cbSystemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<frame_type> framelist = standardLists.systemTypeFrameTypeRules.Where(x => x.system_type_id == ((system_type)cbSystemType.SelectedItem).id && x.is_active)
                                 .Select(x => new frame_type() { id = x.frame_type_id, name = x.frame_type }).Distinct().OrderBy(x => x.name).ToList();
            filteredFrameTypes = framelist.Count > 0 ? framelist : standardLists.frameTypes;
            cbFrameType.DataSource = filteredFrameTypes;
        }
    }
}
