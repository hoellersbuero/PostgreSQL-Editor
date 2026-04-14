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
    public partial class editFrameSticker_rule : Form
    {
        private NpgsqlConnection npgsql;
        private BindingList<frame_sticker_rule> frameStickerRules = new BindingList<frame_sticker_rule>();
        private List<frame_sticker_rule> newFrameStickerRules = new List<frame_sticker_rule>();
        private List<frame_sticker_rule> changedFrameStickerRules = new List<frame_sticker_rule>();
        private DgvChangeDetector _dgvChangeDetector;

        public editFrameSticker_rule()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public static void Execute(Form owner, NpgsqlConnection npgsql)
        {
            using (var form = new editFrameSticker_rule())
            {
                form.npgsql = npgsql;
                form.ShowDialog(owner);
            }
        }

        private void editFrameSticker_rule_Load(object sender, EventArgs e)
        {
            CreateDgvChangeDetector();
            cbSystemType.DataSource = standardLists.systemTypesAllowed;
            cbSystemType.DisplayMember = "name";
            cbSystemType.SelectedIndex = 0;
            cbFrameType.DataSource = standardLists.frameTypes;
            cbFrameType.DisplayMember = "name";
            cbFrameType.SelectedIndex = 0;
            cbFWHeight.DataSource = standardLists.fwHeights;
            cbFWHeight.DisplayMember = "name";
            cbFWHeight.SelectedIndex = 0;
            cbSticker.DataSource = standardLists.stickerTypes;
            cbSticker.DisplayMember = "name";
            cbSticker.SelectedIndex = 0;
            dgv.DataSource = frameStickerRules;
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
                string sqlinsert = "INSERT INTO system_type_rule (id,system_type_id, frame_type_id, fw_height, sticker_type_id,rule_version,is_active,is_special,created_ts,created_by,modified_ts,modified_by) VALUES ";
                string sqlupdate = "UPDATE system_type_rule SET ";
                string sqldelete = "DELETE FROM system_type_rule WHERE id = ";
                foreach (var rule in frameStickerRules)
                {
                    // INSERT-Logik für neue Regeln
                    string s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    string sql = "('" + rule.id.ToString() + "'::uuid,'" + rule.system_type_id.ToString() + "'::uuid,'" + 
                                        rule.frame_type_id.ToString() + "'::uuid,'" + rule.fw_height.ToString() + "'::uuid,'" +
                                        rule.sticker_type_id.ToString() + "'::uuid," + rule.rule_version.ToString() + "," + 
                                        rule.is_active.ToString().ToLower() + ", '" + s + "','pg_editor','" + s + "','pg_editor')";
                    sqllist.Add(sqlinsert + sql + ";");
                }
                foreach (var rule in changedFrameStickerRules)
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
                newFrameStickerRules.Clear();
                changedFrameStickerRules.Clear();
                CreateDgvChangeDetector();
                btnAdd.Enabled = newFrameStickerRules.Count > 0 || changedFrameStickerRules.Count > 0;
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
                var x = from rule in changedFrameStickerRules where JsonSerializer.Serialize(rule) == jsonNew select rule;
                if (x.Count() == 0)
                {
                    changedFrameStickerRules.Add(changedRow.DataBoundItem as frame_sticker_rule);
                    updateLbInfo();
                }
                Action updateButton = () =>
                {
                    btnCreateSQL.Enabled = (newFrameStickerRules?.Count ?? 0) > 0 || changedFrameStickerRules.Count > 0;
                };

                if (btnCreateSQL.InvokeRequired)
                    btnCreateSQL.BeginInvoke(updateButton);
                else
                    updateButton();
            };
        }

        private void updateLbInfo()
        {
            lbInfo.Text = "Rules: " + frameStickerRules.Count.ToString() + " | New: " + newFrameStickerRules.Count.ToString() + " | Changed: " + changedFrameStickerRules.Where(x => !x.check).Count().ToString() + " | Deleted: " + changedFrameStickerRules.Where(x => x.check).Count().ToString();
        }

        private void editFrameSticker_rule_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changedFrameStickerRules.Count > 0 || newFrameStickerRules.Count > 0)
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
            bool exists = frameStickerRules.Any(x => x.system_type_id == ((system_type)cbSystemType.SelectedItem).id &&
                          x.frame_type_id == ((frame_type)cbFrameType.SelectedItem).id &&
                          x.fw_height == ((int)cbFWHeight.SelectedItem) &&
                          x.sticker_type_id == ((sticker_type)cbSticker.SelectedItem).id &&
                          x.is_active == cbActive.Checked && x.rule_version == (int)nudVersion.Value);
            if (exists)
            {
                MessageBox.Show(this, "This rule is already implemented", "Already implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frame_sticker_rule frameStickerRule = new frame_sticker_rule();
            frameStickerRule.id = Guid.NewGuid();
            frameStickerRule.system_type_id = ((system_type)cbSystemType.SelectedItem).id;
            frameStickerRule.system_type = ((system_type)cbSystemType.SelectedItem).name;
            frameStickerRule.frame_type_id = ((frame_type)cbFrameType.SelectedItem).id;
            frameStickerRule.frame_type = ((frame_type)cbFrameType.SelectedItem).name;
            frameStickerRule.fw_height = (decimal)cbFWHeight.SelectedItem;
            frameStickerRule.sticker_type_id = ((sticker_type)cbSticker.SelectedItem).id;
            frameStickerRule.sticker_type = ((sticker_type)cbSticker.SelectedItem).name;
            frameStickerRule.is_active = cbActive.Checked;
            frameStickerRule.rule_version = (int)nudVersion.Value;
            frameStickerRules.Add(frameStickerRule);
            newFrameStickerRules.Add(frameStickerRule);
            updateLbInfo();
            _dgvChangeDetector.TakeSnapshot();
            btnCreateSQL.Enabled = newFrameStickerRules.Count > 0 || changedFrameStickerRules.Count > 0;
        }
    }
}
