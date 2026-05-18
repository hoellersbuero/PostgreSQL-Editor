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
    public partial class editFRSL_rule : Form
    {
        private NpgsqlConnection npgsql;
        private BindingList<frame_sleeve_rule> frameSleeveRules = new BindingList<frame_sleeve_rule>();
        private List<frame_sleeve_rule> newFrameSleeveRules = new List<frame_sleeve_rule>();
        private List<frame_sleeve_rule> changedFrameSleeveRules = new List<frame_sleeve_rule>();
        private List<system_type> filteredSystemTypes = new List<system_type>();
        private List<frame_type> filteredFrameTypes = new List<frame_type>();
        private DgvChangeDetector _dgvChangeDetector;
        private BindingSource _bindingSource = new BindingSource();

        public editFRSL_rule()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public static void Execute(Form parent, NpgsqlConnection npgsql)
        {
            using (var form = new editFRSL_rule())
            {
                form.npgsql = npgsql;
                form.ShowDialog(parent);
            }
        }

        private void EditFrameSleeveRule_Load(object sender, EventArgs e)
        {
            CreateDgvChangeDetector();
            cbBaseMaterial.DataSource = standardLists.baseMaterials;
            cbBaseMaterial.DisplayMember = "visiblename";
            cbBaseMaterial.SelectedIndex = 0;
            filteredSystemTypes = (filteredSystemTypes.Count == 0) ? standardLists.systemTypes : filteredSystemTypes;
            cbSystemType.DataSource = filteredSystemTypes;
            cbSystemType.DisplayMember = "name";
            cbSystemType.SelectedIndex = 0;
            cbFrame.DataSource = standardLists.getAllRoundFrames();
            cbFrame.DisplayMember = "name";
            cbFrame.SelectedIndex = 0;
            cbSleeve.DataSource = standardLists.sleeveTypes;
            cbSleeve.DisplayMember = "name";
            cbSleeve.SelectedIndex = 0;
            var initial = standardLists.frameSleeveRules.Select(x => new frame_sleeve_rule()
            {
                check = x.check,
                id = x.id,
                base_material_id = x.base_material_id,
                base_material = x.base_material,
                system_type_id = x.system_type_id,
                system_type = x.system_type,
                frame_id = x.frame_id,
                frame = x.frame,
                sleeve_type_id = x.sleeve_type_id,
                sleeve_type = x.sleeve_type,
                rule_version = x.rule_version,
                is_active = x.is_active,
                is_mandatory = standardLists.entityMetaData
                    .FirstOrDefault(m => m.entity_id == x.id && m.entity_type == "frame" && m.metadata_key == "sleeve_is_mandatory")?.bool_value == true
            }).ToList();
            frameSleeveRules = new BindingList<frame_sleeve_rule>(initial);
            _bindingSource.DataSource = frameSleeveRules;
            dgv.DataSource = _bindingSource; 
            _dgvChangeDetector.TakeSnapshot();
            updateLbInfo();
        }

        private void updateLbInfo()
        {
            lbInfo.Text = "Rules: " + frameSleeveRules.Count.ToString() + " | New: " + newFrameSleeveRules.Count.ToString() + " | Changed: " + changedFrameSleeveRules.Where(x => !x.check).Count().ToString() + " | Deleted: " + changedFrameSleeveRules.Where(x => x.check).Count().ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool exists = frameSleeveRules.Any(x => x.base_material_id == ((base_material)cbBaseMaterial.SelectedItem).id &&
                          x.system_type_id == ((system_type)cbSystemType.SelectedItem).id &&
                          x.frame_id == ((frame)cbFrame.SelectedItem).id &&
                          x.sleeve_type_id == ((sleeve_type)cbSleeve.SelectedItem).id &&
                          x.rule_version == (int)nudVersion.Value && x.is_active == cbActive.Checked);
            if (exists)
            {
                MessageBox.Show(this, "This rule is already implemented", "Already implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frame_sleeve_rule frameSleeveRule = new frame_sleeve_rule();
            frameSleeveRule.id = Guid.NewGuid();
            frameSleeveRule.base_material_id = ((base_material)cbBaseMaterial.SelectedItem).id;
            frameSleeveRule.base_material = ((base_material)cbBaseMaterial.SelectedItem).name;
            frameSleeveRule.system_type_id = ((system_type)cbSystemType.SelectedItem).id;
            frameSleeveRule.system_type = ((system_type)cbSystemType.SelectedItem).name;
            frameSleeveRule.frame_id = ((frame)cbFrame.SelectedItem).id;
            frameSleeveRule.frame = ((frame)cbFrame.SelectedItem).name;
            frameSleeveRule.sleeve_type_id = ((sleeve_type)cbSleeve.SelectedItem).id;
            frameSleeveRule.sleeve_type = ((sleeve_type)cbSleeve.SelectedItem).name;
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

        private void EditSystemTypeFrameType_FormClosing(object sender, FormClosingEventArgs e)
        {
            //var changedRows = _dgvChangeDetector.GetChangedRows().ToList();
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
            //var changedRows = _dgvChangeDetector.GetChangedRows().ToList();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save changes to SQL-file";
            sfd.Filter = "SQL-file (*.sql)|*.sql";
            sfd.DefaultExt = "sql";
            List<string> sqllist = new List<string>();
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                // Speichern der Änderungen in der Datenbank
                string sqlinsert = "INSERT INTO frame_sleeve_rule (id,base_material_id,system_type_id,frame_id,sleeve_type_id," +
                                   "rule_version,is_active,created_ts,created_by,modified_ts,modified_by) VALUES ";
                string sqlinsertmeta = "INSERT INTO entity_metadata (id,entity_id,entity_type,metadata_key,bool_value,created_ts,created_by,modified_ts,modified_by) VALUES ";
                string sqlupdate = "UPDATE frame_sleeve_rule SET ";
                string sqlupdatemeta = "UPDATE entity_metadata SET ";
                string sqldelete = "DELETE FROM frame_sleeve_rule WHERE id = '";
                string sqldeletemeta = "DELETE FROM entity_metadata WHERE entity_id = '";
                foreach (var rule in newFrameSleeveRules)
                {
                    // INSERT-Logik für neue Regeln
                    string s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    string sql = "('" + rule.id.ToString() + "'::uuid,'" + rule.base_material_id.ToString() + "'::uuid,'" + rule.system_type_id.ToString() +
                                 "'::uuid,'" + rule.frame_id.ToString() + "'::uuid,'" + rule.sleeve_type_id.ToString() +
                                 "'::uuid, " + rule.rule_version.ToString() + "," + rule.is_active.ToString().ToLower() + ",'" + s + "','pg_editor','" + s + "','pg_editor')";
                    sqllist.Add(sqlinsert + sql + ";");
                    sql = ("('" + Guid.NewGuid().ToString() + "'::uuid,'" + rule.id.ToString() + "'::uuid,'frame','sleeve_is_mandatory'," + rule.is_mandatory.ToString().ToLower() + ",'" + s + "','pg_editor','" + s + "','pg_editor')");
                    sqllist.Add(sqlinsertmeta + sql + ";");
                }
                foreach (var rule in changedFrameSleeveRules)
                {
                    string sql = String.Empty;
                    if (rule.check) // Delete row
                    {
                        // Delete-Logik für gelöschte Regeln
                        sql = sqldelete + rule.id.ToString() + "'";
                        sqllist.Add(sql + ";");
                        sql = sqldeletemeta + rule.id.ToString() + "' AND entity_type = 'frame' AND metadata_key = 'sleeve_is_mandatory'";
                        sqllist.Add(sql + ";");
                    }
                    else
                    {
                        // UPDATE-Logik für geänderte Regeln
                        sql = sqlupdate + "is_active = " + rule.is_active.ToString().ToLower() + ", rule_version = " + rule.rule_version 
                                        + ", modified_ts = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) 
                                        + "', modified_by = 'pg_editor' " + " WHERE id = '" + rule.id.ToString() + "'::uuid";
                        sqllist.Add(sql + ";");
                        sql = sqlupdatemeta + "bool_value = " + rule.is_mandatory.ToString().ToLower() 
                                            + ", modified_ts = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) 
                                            + "', modified_by = 'pg_editor' " 
                                            + "WHERE entity_id = '" + rule.id.ToString() + "'::uuid AND entity_type = 'frame' AND metadata_key = 'sleeve_is_mandatory'";
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

        private void cbFrame_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame fr = cbFrame.SelectedItem as frame;
            if (fr != null)
            {
                frame_geometry fg = standardLists.frameGeometries.Where(x => x.id.Equals(fr.geometry_id)).FirstOrDefault();
                if (fg != null)
                {
                    List<sleeve_type> sls = standardLists.sleeveTypes.Where(x => x.name.Contains(" " + fg.b1.ToString())).ToList();
                    if (sls != null && sls.Count() > 0)
                    {
                        cbSleeve.DataSource = sls;
                        cbSleeve.DisplayMember = "name";
                    }
                    else
                        cbSleeve.DataSource = standardLists.sleeveTypes;
                }
            }
        }
    }
}
