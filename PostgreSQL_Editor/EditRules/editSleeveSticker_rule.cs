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
    public partial class editSleeveSticker_rule : Form
    {
        private NpgsqlConnection npgsql;
        private BindingList<sleeve_sticker_rule> sleeveStickerRules = new BindingList<sleeve_sticker_rule>();
        private List<sleeve_sticker_rule> newSleeveStickerRules = new List<sleeve_sticker_rule>();
        private List<sleeve_sticker_rule> changedSleeveStickerRules = new List<sleeve_sticker_rule>();
        private DgvChangeDetector _dgvChangeDetector;

        public editSleeveSticker_rule()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public static void Execute(Form owner, NpgsqlConnection npgsql)
        {
            using (var form = new editSleeveSticker_rule())
            {
                form.npgsql = npgsql;
                form.ShowDialog(owner);
            }
        }

        private void editSleeveSticker_rule_Load(object sender, EventArgs e)
        {
            CreateDgvChangeDetector();
            cbSystemType.DataSource = standardLists.systemTypesAllowed;
            cbSystemType.DisplayMember = "name";
            cbSystemType.SelectedIndex = 0;
            cbSleeve.DataSource = standardLists.sleeveTypes;
            cbSleeve.DisplayMember = "name";
            cbSleeve.SelectedIndex = 0;
            cbSticker.DataSource = standardLists.stickerTypes;
            cbSticker.DisplayMember = "name";
            cbSticker.SelectedIndex = 0;
            dgv.DataSource = sleeveStickerRules;
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
                string sqlinsert = "INSERT INTO material_type_rule (id,system_type_id, sleeve_type_id,sticker_type_id,rule_version,is_active,is_special,created_ts,created_by,modified_ts,modified_by) VALUES ";
                string sqlupdate = "UPDATE material_type_rule SET ";
                string sqldelete = "DELETE FROM material_type_rule WHERE id = ";
                foreach (var rule in sleeveStickerRules)
                {
                    // INSERT-Logik für neue Regeln
                    string s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    string sql = "('" + rule.id.ToString() + "'::uuid,'" + rule.system_type_id.ToString() +
                        "'::uuid,'" + rule.sleeve_type_id.ToString() + "'::uuid,'" + rule.sticker_type_id.ToString() +
                        "'::uuid, " + rule.rule_version.ToString() + "," + rule.is_active.ToString().ToLower() + "," + s + "','pg_editor','" + s + "','pg_editor')";
                    sqllist.Add(sqlinsert + sql + ";");
                }
                foreach (var rule in changedSleeveStickerRules)
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
                            + ", system_type_id = '" + rule.system_type_id.ToString() + "'::uuid, sleeve_type_id = '" 
                            + rule.sleeve_type_id.ToString() + "'::uuid, sticker_type_id = '" + rule.sticker_type_id.ToString() + "'::uuid, modified_ts = '" 
                            + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + "', modified_by = 'pg_editor'"
                            + " WHERE id = '" + rule.id.ToString() + "'";
                    }
                    sqllist.Add(sql + ";");
                }
                File.WriteAllLines(sfd.FileName, sqllist);
                newSleeveStickerRules.Clear();
                changedSleeveStickerRules.Clear();
                CreateDgvChangeDetector();
                btnAdd.Enabled = newSleeveStickerRules.Count > 0 || changedSleeveStickerRules.Count > 0;
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
                sleeve_sticker_rule changedRule = changedRow.DataBoundItem as sleeve_sticker_rule;
                string jsonNew = JsonSerializer.Serialize(changedRule);
                var x = from rule in changedSleeveStickerRules where JsonSerializer.Serialize(rule) == jsonNew select rule;
                if (x.Count() == 0)
                {
                    changedSleeveStickerRules.Add(changedRow.DataBoundItem as sleeve_sticker_rule   );
                    updateLbInfo();
                }
                Action updateButton = () =>
                {
                    btnCreateSQL.Enabled = (newSleeveStickerRules?.Count ?? 0) > 0 || changedSleeveStickerRules.Count > 0;
                };

                if (btnCreateSQL.InvokeRequired)
                    btnCreateSQL.BeginInvoke(updateButton);
                else
                    updateButton();
            };
        }

        private void updateLbInfo()
        {
            lbInfo.Text = "Rules: " + sleeveStickerRules.Count.ToString() + " | New: " + newSleeveStickerRules.Count.ToString() + " | Changed: " + changedSleeveStickerRules.Where(x => !x.check).Count().ToString() + " | Deleted: " + changedSleeveStickerRules.Where(x => x.check).Count().ToString();
        }

        private void editSleeveSticker_rule_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changedSleeveStickerRules.Count > 0 || newSleeveStickerRules.Count > 0)
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

        private void btnCreateSQL_Click(object sender, EventArgs e)
        {
            CreateSQLStatements();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool exists = sleeveStickerRules.Any(x => x.system_type_id == ((system_type)cbSystemType.SelectedItem).id &&
                          x.sleeve_type_id == ((sleeve_type)cbSleeve.SelectedItem).id &&
                          x.sticker_type_id == ((sticker_type)cbSticker.SelectedItem).id &&
                          x.is_active == cbActive.Checked && x.rule_version == (int)nudVersion.Value);
            if (exists)
            {
                MessageBox.Show(this, "This rule is already implemented", "Already implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sleeve_sticker_rule materialTypeRule = new sleeve_sticker_rule();
            materialTypeRule.id = Guid.NewGuid();
            materialTypeRule.system_type_id = ((system_type)cbSystemType.SelectedItem).id;
            materialTypeRule.system_type = ((system_type)cbSystemType.SelectedItem).name;
            materialTypeRule.sleeve_type_id = ((sleeve_type)cbSleeve.SelectedItem).id;
            materialTypeRule.sleeve_type = ((sleeve_type)cbSleeve.SelectedItem).name;
            materialTypeRule.sticker_type_id = ((sticker_type)cbSticker.SelectedItem).id;
            materialTypeRule.sticker_type = ((sticker_type)cbSticker.SelectedItem).name;
            materialTypeRule.is_active = cbActive.Checked;
            materialTypeRule.rule_version = (int)nudVersion.Value;
            sleeveStickerRules.Add(materialTypeRule);
            newSleeveStickerRules.Add(materialTypeRule);
            updateLbInfo();
            _dgvChangeDetector.TakeSnapshot();
            btnCreateSQL.Enabled = newSleeveStickerRules.Count > 0 || changedSleeveStickerRules.Count > 0;
        }
    }
}
