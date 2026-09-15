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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgreSQL_Editor.EditRules
{
    public partial class editWedgeOptionRule : Form
    {
        private NpgsqlConnection npgsql;
        private List<material_type_rule> filteredMaterialRules = new List<material_type_rule>();
        private SortableBindingList<wedge_option_rule> wedgeOptionRules = new SortableBindingList<wedge_option_rule>();
        private List<wedge_option_rule> newWedgeOptionRules = new List<wedge_option_rule>();
        private List<wedge_option_rule> changedWedgeOptionRules = new List<wedge_option_rule>();
        private List<wedgeListItem> wedgeListItems = new List<wedgeListItem>();
        private DgvChangeDetector _dgvChangeDetector;
        public editWedgeOptionRule()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }


        public static void Execute(Form owner, NpgsqlConnection npgsql)
        {
            using (var form = new editWedgeOptionRule())
            {
                form.npgsql = npgsql;
                form.ShowDialog(owner);
            }
        }

        private void editWedgeOptionRule_Load(object sender, EventArgs e)
        {
            CreateDgvChangeDetector();
            cbSystemType.DataSource = standardLists.systemTypesAllowed;
            cbSystemType.DisplayMember = "name";
            cbSystemType.SelectedIndex = 0;
            cbMaterialType.DataSource = standardLists.materialTypes;
            cbMaterialType.DisplayMember = "name";
            cbMaterialType.SelectedIndex = 0;
            foreach (var wedge in standardLists.wedgeTypes)
            {
                wedgeListItems.Add(new wedgeListItem(wedge));
            }
            cbWedge.DataSource = wedgeListItems;
            cbWedge.DisplayMember = "visual_name2";
            cbWedge.SelectedIndex = 0;
            wedgeOptionRules = new SortableBindingList<wedge_option_rule>();
            foreach (var rule in standardLists.wedgeOptionRules)
            {
                wedgeOptionRules.Add(rule);
            }
            dgv.DataSource = wedgeOptionRules;
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
                wedge_option_rule changedRule = changedRow.DataBoundItem as wedge_option_rule;
                string jsonNew = JsonSerializer.Serialize(changedRule);
                var x = from rule in changedWedgeOptionRules where JsonSerializer.Serialize(rule) == jsonNew select rule;
                if (x.Count() == 0)
                {
                    changedWedgeOptionRules.Add(changedRow.DataBoundItem as wedge_option_rule);
                    updateLbInfo();
                }
                Action updateButton = () =>
                {
                    btnCreateSQL.Enabled = (newWedgeOptionRules?.Count ?? 0) > 0 || changedWedgeOptionRules.Count > 0;
                };

                if (btnCreateSQL.InvokeRequired)
                    btnCreateSQL.BeginInvoke(updateButton);
                else
                    updateButton();
            };
        }

        private void updateLbInfo()
        {
            lbInfo.Text = "Rules: " + wedgeOptionRules.Count.ToString() + " | New: " + newWedgeOptionRules.Count.ToString() + " | Changed: " + changedWedgeOptionRules.Where(x => !x.check).Count().ToString() + " | Deleted: " + changedWedgeOptionRules.Where(x => x.check).Count().ToString();
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
                string sqlinsert = "INSERT INTO wedge_option_rule (id,system_type_id, material_type_id,wedge_type_id,has_wedge_option,has_material_option,is_special,rule_version,is_active,created_ts,created_by,modified_ts,modified_by) VALUES ";
                string sqlupdate = "UPDATE wedge_option_rule SET ";
                string sqldelete = "DELETE FROM wedge_option_rule WHERE id = ";
                foreach (var rule in newWedgeOptionRules)
                {
                    // INSERT-Logik für neue Regeln
                    string s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    string sql = "('" + rule.id.ToString() + "'::uuid,'" + rule.system_type_id.ToString() + "'::uuid,'" +
                                        rule.material_type_id.ToString() + "'::uuid,'" + rule.wedge_type_id.ToString() + "'::uuid," +
                                        rule.has_wedge_option.ToString().ToLower() + "," + rule.has_material_option.ToString().ToLower() + "," +
                                        rule.is_special.ToString().ToLower() + "," + rule.rule_version.ToString() + "," +
                                        rule.is_active.ToString().ToLower() + ",'" + s + "','pg_editor','" + s + "','pg_editor')";
                    sqllist.Add(sqlinsert + sql + ";");
                }
                foreach (var rule in changedWedgeOptionRules)
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
                        sql = sqlupdate + "has_wedge_option = " + rule.has_wedge_option.ToString().ToLower() + ", has_material_option = " + rule.has_material_option.ToString().ToLower()
                                        + ", is_special = " + rule.is_special.ToString().ToLower() + ", is_active = " + rule.is_active.ToString().ToLower() + ", rule_version = " + rule.rule_version
                                        + ", modified_ts = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + "', modified_by = 'pg_editor' "
                                        + " WHERE id = '" + rule.id.ToString() + "'";
                    }
                    sqllist.Add(sql + ";");
                }
                File.WriteAllLines(sfd.FileName, sqllist);
                newWedgeOptionRules.Clear();
                changedWedgeOptionRules.Clear();
                CreateDgvChangeDetector();
                btnAdd.Enabled = newWedgeOptionRules.Count > 0 || changedWedgeOptionRules.Count > 0;
            }
        }

        private void editWedgeOptionRule_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changedWedgeOptionRules.Count > 0 || newWedgeOptionRules.Count > 0)
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
            bool exists = wedgeOptionRules.Any(x => x.system_type_id == ((system_type)cbSystemType.SelectedItem).id &&
                          x.material_type_id == ((material_type)cbMaterialType.SelectedItem).id &&
                          x.wedge_type_id == ((wedge_type)cbWedge.SelectedItem).id &&
                          x.has_wedge_option == cbWedgeOption.Checked &&
                          x.has_material_option == cbMaterialOption.Checked &&
                          x.is_special == cbSpecial.Checked &&
                          x.is_active == cbSpecial.Checked && x.rule_version == (int)nudVersion.Value);
            if (exists)
            {
                MessageBox.Show(this, "This rule is already implemented", "Already implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            wedge_option_rule wedgeOptionRule = new wedge_option_rule();
            wedgeOptionRule.id = Guid.NewGuid();
            wedgeOptionRule.system_type_id = ((system_type)cbSystemType.SelectedItem).id;
            wedgeOptionRule.system_type = ((system_type)cbSystemType.SelectedItem).name;
            wedgeOptionRule.material_type_id = ((material_type)cbMaterialType.SelectedItem).id;
            wedgeOptionRule.material_type = ((material_type)cbMaterialType.SelectedItem).name;
            wedgeOptionRule.wedge_type_id = ((wedge_type)cbWedge.SelectedItem).id;
            wedgeOptionRule.wedge_type = ((wedge_type)cbWedge.SelectedItem).name;
            wedgeOptionRule.has_wedge_option = cbWedgeOption.Checked;
            wedgeOptionRule.has_material_option = cbMaterialOption.Checked;
            wedgeOptionRule.is_special = cbSpecial.Checked;
            wedgeOptionRule.is_active = cbSpecial.Checked;
            wedgeOptionRule.rule_version = (int)nudVersion.Value;
            wedgeOptionRules.Add(wedgeOptionRule);
            newWedgeOptionRules.Add(wedgeOptionRule);
            updateLbInfo();
            _dgvChangeDetector.TakeSnapshot();
            btnCreateSQL.Enabled = newWedgeOptionRules.Count > 0 || changedWedgeOptionRules.Count > 0;
        }

        private void btnCreateSQL_Click(object sender, EventArgs e)
        {
            CreateSQLStatements();
        }
    }
}
