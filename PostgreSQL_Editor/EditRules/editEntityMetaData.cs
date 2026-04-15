using DocumentFormat.OpenXml.Wordprocessing;
using Npgsql;
using PostgreSQL_Editor.DBUtils;
using PostgreSQL_Editor.Global;
using PostgreSQL_Editor.Utilities;
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
    public partial class editEntityMetaData : Form
    {
        NpgsqlConnection npgsql;
        private BindingList<entity_metadata> entityData = new BindingList<entity_metadata>();
        private List<entity_metadata> changedEntityData = new List<entity_metadata>();
        private List<entity_metadata> newEntityData = new List<entity_metadata>();
        private Dictionary<string, Guid> filteredEntityNames = new Dictionary<string, Guid>();
        private DgvChangeDetector _dgvChangeDetector;

        public editEntityMetaData()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public static void Execute(Form owner, NpgsqlConnection npgsql)
        {
            using (var form = new editEntityMetaData())
            {
                form.npgsql = npgsql;
                form.ShowDialog(owner);
            }
        }

        private void editEntityMetaData_Load(object sender, EventArgs e)
        {
            CreateDgvChangeDetector();
            List<product_type> productTypes = standardLists.productTypes;
            productTypes.Add(new product_type { id = Guid.Empty, name = "Module variation" });
            productTypes.Add(new product_type { id = Guid.Empty, name = "Frame sleeve rule" });
            cbEntityType.DataSource = productTypes;
            cbEntityType.DisplayMember = "name";
            cbEntityType.ValueMember = "id";
            cbEntityType.SelectedIndex = 0;
            cbEntity.DataSource = filteredEntityNames.ToList();
            cbEntity.DisplayMember = "Key";
            cbEntity.ValueMember = "Value";
            entityData = new BindingList<entity_metadata>(standardLists.entityMetaData);
            dgv.DataSource = null;
            dgv.DataSource = entityData;
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
                entity_metadata changedRule = changedRow.DataBoundItem as entity_metadata;
                if (changedRule == null) return;
                string jsonNew = JsonSerializer.Serialize(changedRule);
                var x = from rule in changedEntityData where JsonSerializer.Serialize(rule) == jsonNew select rule;
                if (!x.Any())
                {
                    changedEntityData.Add(changedRule);
                    updateLbInfo();
                }
                Action updateButton = () =>
                {
                    btnCreateSQL.Enabled = (newEntityData?.Count ?? 0) > 0 || changedEntityData.Count > 0;
                };

                if (btnCreateSQL.InvokeRequired)
                    btnCreateSQL.BeginInvoke(updateButton);
                else
                    updateButton();
            };
        }

        private void updateLbInfo()
        {
            lbInfo.Text = "Rules: " + entityData.Count.ToString() + " | New: " + newEntityData.Count.ToString() + " | Changed: " + changedEntityData.Where(x => !x.check).Count().ToString() + " | Deleted: " + changedEntityData.Where(x => x.check).Count().ToString();
        }

        private void cbEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbEntityType.SelectedItem as product_type)
            {
                case product_type pt when pt.name == "Module variation":
                    filteredEntityNames = (from mv in standardLists.moduleVariations
                                           join m in standardLists.modules on mv.module_type_id equals m.id
                                           select new { m.name, mv.id }).GroupBy(x => x.name).ToDictionary(g => g.Key, g => g.First().id);
                    var sortedModuleVariations = filteredEntityNames.ToList();
                    sortedModuleVariations.Sort((a, b) => string.Compare(a.Key, b.Key, StringComparison.OrdinalIgnoreCase));
                    cbEntity.DataSource = sortedModuleVariations;
                    break;
                case product_type pt when pt.name == "Frame sleeve rule":
                    filteredEntityNames = (from frame_sleeve_rule fsr in standardLists.frameSleeveRules
                                           join f in standardLists.frames on fsr.frame_id equals f.id
                                           select new { f.name, fsr.id }).ToDictionary(x => x.name, x => x.id);
                    var sortedFrameSleeveRules = filteredEntityNames.ToList();
                    sortedFrameSleeveRules.Sort((a, b) => string.Compare(a.Key, b.Key, StringComparison.OrdinalIgnoreCase));
                    cbEntity.DataSource = sortedFrameSleeveRules;
                    break;
                default:
                    filteredEntityNames = standardLists.products.Where(x => x.product_type_id == (cbEntityType.SelectedValue as Guid?)).ToDictionary(x => x.name, x => x.id);
                    cbEntity.DataSource = filteredEntityNames.ToList();
                    break;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool exists = entityData.Any(x => x.entity_id == ((KeyValuePair<string, Guid>)cbEntity.SelectedItem).Value &&
                          x.entity_type == (cbEntityType.SelectedItem as product_type).name &&
                          x.metadata_key == tbKey.Text && x.string_value.Equals(tbString.Text) && x.bool_value.Equals(cbBool.Checked) && 
                          (!String.IsNullOrEmpty(tbNumber.Text) ? x.number_value == Decimal.Parse(tbNumber.Text) : true) &&
                          x.json_value.Equals(tbJSON.Text));
            if (exists)
            {
                MessageBox.Show(this, "This rule is already implemented", "Already implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                {
                entity_metadata newRule = new entity_metadata
                {
                    id = Guid.NewGuid(),
                    entity_id = ((KeyValuePair<string, Guid>)cbEntity.SelectedItem).Value,
                    entity_type = (cbEntityType.SelectedItem as product_type).name,
                    metadata_key = tbKey.Text,
                    string_value = tbString.Text,
                    bool_value = cbBool.Checked,
                    number_value = !String.IsNullOrEmpty(tbNumber.Text) ? Decimal.Parse(tbNumber.Text) : (decimal?)null,
                    json_value = JsonDocument.Parse(tbJSON.Text),
                    check = false
                };
                entityData.Add(newRule);
                newEntityData.Add(newRule);
                updateLbInfo();
                _dgvChangeDetector.TakeSnapshot();
                btnCreateSQL.Enabled = newEntityData.Count > 0 || changedEntityData.Count > 0;
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
                string s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                string sqlinsert = "INSERT INTO entity_metadata (id, entity_id, entity_type, metadata_key, string_value, bool_value, number_value, json_value, created_by, created_ts, modified_by, modified_ts) VALUES ";
                string sqlupdate = "UPDATE entity_metadata SET entity_id = @entity_id, entity_type = @entity_type, metadata_key = @metadata_key, string_value = @string_value, bool_value = @bool_value, number_value = @number_value, json_value = @json_value WHERE id = @id;";
                string sqldelete = "DELETE FROM entity_metadata WHERE id = @id;";
                foreach (var newRule in newEntityData)
                {
                    string sqlValues = $"('{newRule.id}', '{newRule.entity_id}', '{newRule.entity_type}', '{newRule.metadata_key}', " +
                        $"'{newRule.string_value}', {newRule.bool_value}, {(newRule.number_value.HasValue ? newRule.number_value.Value.ToString() : "NULL")}, " +
                        $"'{newRule.json_value.RootElement.ToString()}', 'pg_editor', '{s}', 'pg_editor', '{s}')";
                    sqllist.Add(sqlinsert + sqlValues + ";");
                }
                foreach (var changedRule in changedEntityData.Where(x => !x.check))
                {
                    string sqlSet = $"entity_id = '{changedRule.entity_id}', entity_type = '{changedRule.entity_type}', metadata_key = '{changedRule.metadata_key}', string_value = '{changedRule.string_value}', bool_value = {changedRule.bool_value}, number_value = {(changedRule.number_value.HasValue ? changedRule.number_value.Value.ToString() : "NULL")}, json_value = '{changedRule.json_value.RootElement.ToString()}'";
                    sqllist.Add(sqlupdate.Replace("@id", $"'{changedRule.id}'").Replace("@entity_id", $"'{changedRule.entity_id}'").Replace("@entity_type", $"'{changedRule.entity_type}'").Replace("@metadata_key", $"'{changedRule.metadata_key}'").Replace("@string_value", $"'{changedRule.string_value}'").Replace("@bool_value", changedRule.bool_value.ToString()).Replace("@number_value", changedRule.number_value.HasValue ? changedRule.number_value.Value.ToString() : "NULL").Replace("@json_value", $"'{changedRule.json_value.RootElement.ToString()}'"));
                }
                foreach (var deletedRule in changedEntityData.Where(x => x.check))
                {
                    sqllist.Add(sqldelete.Replace("@id", $"'{deletedRule.id}'"));
                }
                File.WriteAllLines(sfd.FileName, sqllist);
                newEntityData.Clear();
                changedEntityData.Clear();
                CreateDgvChangeDetector();
                btnAdd.Enabled = newEntityData.Count > 0 || changedEntityData.Count > 0;
            }
        }

        private void btnCreateSQL_Click(object sender, EventArgs e)
        {
            CreateSQLStatements();
        }

        private void editEntityMetaData_FormClosing(object sender, FormClosingEventArgs e)
        {
            //var changedRows = _dgvChangeDetector.GetChangedRows().ToList();
            if (changedEntityData.Count > 0 || newEntityData.Count > 0)
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
    }
}
