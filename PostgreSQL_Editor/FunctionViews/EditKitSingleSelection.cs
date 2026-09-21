using Npgsql;
using PostgreSQL_Editor.DBUtils;
using PostgreSQL_Editor.Global;
using PostgreSQL_Editor.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace PostgreSQL_Editor.FunctionViews
{
    public partial class EditKitSingleSelection : Form
    {
        private NpgsqlConnection _npgsql;
        private DgvChangeDetector _dgvChangeDetector;
        private SortableBindingList<KitSingleSelect> kitSingleSelections = new SortableBindingList<KitSingleSelect>();
        private List<KitSingleSelect> changedKitSingleSelections = new List<KitSingleSelect>();
        private string _sqlTemplate = "UPDATE special_frame_variant sfv SET is_available = @IS_AVAILABLE, kit_single_selectable = @KIT_SINGLE_SELECTABLE, name_editable = @NAME_EDITABLE, special_coating_editable = @SPECIAL_COATING_EDITABLE FROM special_frame_configuration sfc WHERE sfv.configuration_id = sfc.id AND sfc.base_material_id = '@BASEMATERIAL_ID' AND sfc.system_type_id = '@SYSTEMTYPE_ID' AND sfc.frame_type_id = '@FRAMETYPE_ID' AND sfv.frame_material_code = '@FRAME_MATERIAL_CODE';";

        public EditKitSingleSelection()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public static void Execute(Form owner, NpgsqlConnection npgsql)
        {
            EditKitSingleSelection form = new EditKitSingleSelection();
            form._npgsql = npgsql;
            form.ShowDialog(owner);
        }

        private void EditKitSingleSelection_Load(object sender, System.EventArgs e)
        {
            CreateDgvChangeDetector();
            List<KitSingleSelect> kitSingleSelectionsList = standardLists.GetKitSingleSelects(_npgsql);
            if (kitSingleSelectionsList != null)
            {
                kitSingleSelections.Clear();
                foreach (var item in kitSingleSelectionsList)
                {
                    kitSingleSelections.Add(item);
                }
                dgv.DataSource = kitSingleSelections;
                _dgvChangeDetector.TakeSnapshot();
                updateLbInfo();
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
                KitSingleSelect changedItem = changedRow.DataBoundItem as KitSingleSelect;
                string jsonNew = JsonSerializer.Serialize(changedItem);
                var x = from item in changedKitSingleSelections where JsonSerializer.Serialize(item) == jsonNew select item;
                if (x.Count() == 0)
                {
                    changedKitSingleSelections.Add(changedRow.DataBoundItem as KitSingleSelect);
                    updateLbInfo();
                }
                Action updateButton = () =>
                {
                    btnCreateSQL.Enabled = changedKitSingleSelections.Count > 0;
                };

                if (btnCreateSQL.InvokeRequired)
                    btnCreateSQL.BeginInvoke(updateButton);
                else
                    updateButton();
            };
        }

        private void updateLbInfo()
        {
            lbInfo.Text = "Entries: " + kitSingleSelections.Count.ToString() + " | Changed: " + changedKitSingleSelections.Count().ToString();
        }

        private void btnCreateSQL_Click(object sender, EventArgs e)
        {
            createSQL();
        }

        private void createSQL()
        {
            List<string> sqlStatements = new List<string>();
            foreach (var changedItem in changedKitSingleSelections)
            {
                string sql = _sqlTemplate.Replace("@IS_AVAILABLE", changedItem.is_available.ToString()).Replace("@KIT_SINGLE_SELECTABLE", changedItem.kit_single_selectable.ToString()).Replace("@NAME_EDITABLE", changedItem.name_editable.ToString()).Replace("@SPECIAL_COATING_EDITABLE", changedItem.special_coating_editable.ToString()).Replace("@BASEMATERIAL_ID", changedItem.base_material_id.ToString()).Replace("@SYSTEMTYPE_ID", changedItem.system_type_id.ToString()).Replace("@FRAMETYPE_ID", changedItem.frame_type_id.ToString()).Replace("@FRAME_MATERIAL_CODE", changedItem.frame_material_code.ToString());
                if (!string.IsNullOrEmpty(sql))
                {
                    if (!sqlStatements.Contains(sql))
                        sqlStatements.Add(sql);
                }
            }
            if (sqlStatements.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "SQL files (*.sql)|*.sql|All files (*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllLines(sfd.FileName, sqlStatements);
                    MessageBox.Show("SQL statements saved to file: " + sfd.FileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    changedKitSingleSelections.Clear();
                    CreateDgvChangeDetector();
                    btnCreateSQL.Enabled = changedKitSingleSelections.Count > 0;

                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditKitSingleSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changedKitSingleSelections.Count > 0)
            {
                var result = MessageBox.Show(this, "Do you want to save the changes?", "Save changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    createSQL();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true; // Schließen abbrechen
                }
            }
        }
    }
}
