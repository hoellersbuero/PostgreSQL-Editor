using Npgsql;
using PostgreSQL_Editor.DBUtils;
using PostgreSQL_Editor.Global;
using PostgreSQL_Editor.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgreSQL_Editor.FunctionViews
{
    public partial class setAvailableSelectable : Form
    {
        private NpgsqlConnection npgsql;
        private List<asItem> listitems = new List<asItem>();
        private string clipboardText = null;
        private List<string> sqlCommands = new List<string>();

        public setAvailableSelectable()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
            dgv.AllowDrop = true;
            dgv.DragEnter += dgv_DragEnter;
            dgv.DragOver += dgv_DragOver;
            dgv.DragDrop += dgv_DragDrop;
        }

        public static void Execute(Form owner, NpgsqlConnection npgsql)
        {
            setAvailableSelectable form = new setAvailableSelectable();
            form.Owner = owner;
            form.npgsql = npgsql; 
            form.ShowDialog();
        }

        private void dgv_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) ||
                e.Data.GetDataPresent(DataFormats.UnicodeText) ||
                e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void dgv_DragOver(object sender, DragEventArgs e)
        {
            // gleiche Logik wie DragEnter, sorgt für korrektes Cursor-Verhalten beim Überfahren
            if (e.Data.GetDataPresent(DataFormats.FileDrop) ||
                e.Data.GetDataPresent(DataFormats.UnicodeText) ||
                e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void dgv_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                // Dateien gedroppt?
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (files != null && files.Length > 0)
                    {
                        var path = files[0];
                        var ext = System.IO.Path.GetExtension(path).ToLowerInvariant();
                        if (ext == ".txt" || ext == ".csv")
                        {
                            clipboardText = System.IO.File.ReadAllText(path);
                            ReadValues();
                        }
                        else if (ext == ".xlsx" || ext == ".xls")
                        {
                            // Excel direkt einlesen (ExcelReader im Projekt vorhanden)
                            try
                            {
                                DataTable dataTable = ExcelReader.ReadExcelFile(path);
                                dgv.DataSource = dataTable;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error reading Excel file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            // Versuch, als Text zu lesen
                            try
                            {
                                clipboardText = System.IO.File.ReadAllText(path);
                                ReadValues();
                            }
                            catch
                            {
                                MessageBox.Show("Unsupported file type for drop: " + ext, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else if (e.Data.GetDataPresent(DataFormats.UnicodeText) || e.Data.GetDataPresent(DataFormats.Text))
                {
                    clipboardText = e.Data.GetData(DataFormats.UnicodeText) as string ?? e.Data.GetData(DataFormats.Text) as string;
                    ReadValues();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DragDrop error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void dgv_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.V && Control.ModifierKeys == Keys.Control)
            {
                clipboardText = Clipboard.GetText();
                ReadValues();
            }
        }

        private string getName(string name, product pro, string matname = null)
        {
            if (String.IsNullOrEmpty(name) && pro != null)
                return pro.name + (matname != null ? " " + matname : "");
            else if (!String.IsNullOrEmpty(name) && pro == null)
                return "---" + name + (matname != null ? " " + matname : "");
            else if (!String.IsNullOrEmpty(name) && pro != null && name.Equals(pro.name))
                return pro.name + (matname != null ? " " + matname : "");
            else if (!String.IsNullOrEmpty(name) && pro != null && !name.Equals(pro.name))
                return name + (matname != null ? " " + matname : "") + " | " + pro.name;
            return "unknown";
        }

        private void ReadValues()
        {
            try
            {
                updateButtons(false);
                listitems.Clear();
                if (!string.IsNullOrEmpty(clipboardText))
                {
                    string[] items = clipboardText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string item in items)
                    {
                        try
                        {
                            string artNr = item;
                            string fname = null;
                            string matname = null;
                            product pro = null;
                            if (item.CountCharFast('\t') == 2)
                            {
                                artNr = item.Split(new char[] { '\t' })[0];
                                matname = item.Split(new char[] { '\t' })[1];
                                if (matname == "S/S")
                                    matname = "SS";
                                fname = item.Split(new char[] { '\t' })[2];
                                material_type mat = standardLists.materialTypes.FirstOrDefault(m => m.name.Equals(matname));
                                if (mat != null)
                                {
                                    if (fname.Contains("Sleeve"))
                                    {
                                        string fname2 = fname.Replace("Sleeve", "");
                                        pro = (from product p in standardLists.products from sleeve_type f in standardLists.sleeveTypes where p.name.Contains(fname2) && p.id == f.id && f.material_type_id.Equals(mat.id) select p).FirstOrDefault();
                                        if (pro == null)
                                            pro = standardLists.products.FirstOrDefault(p => p.article_number == artNr);
                                    }
                                    else
                                        pro = (from product p in standardLists.products from frame f in standardLists.frames where p.name.Equals(fname) && p.id == f.id && f.material_type_id.Equals(mat.id) select p).FirstOrDefault();
                                }
                                //if (pro == null)
                                //    pro = standardLists.products.FirstOrDefault(p => p.article_number == artNr) ?? null;
                                //if (pro == null)
                                //    pro = standardLists.products.FirstOrDefault(p => p.name == fname) ?? null;
                            }
                            else if (item.CountCharFast('\t') == 1)
                            {
                                artNr = item.Split(new char[] { '\t' })[0];
                                fname = item.Split(new char[] { '\t' })[1];
                                pro = standardLists.products.FirstOrDefault(p => p.article_number == artNr && p.name.Equals(fname)) ?? null;
                                //if (pro == null)
                                //    pro = standardLists.products.FirstOrDefault(p => p.name == fname) ?? null;
                            }
                            else
                                pro = standardLists.products.FirstOrDefault(p => p.article_number == artNr) ?? null;
                            if (pro != null)
                            {
                                asItem listitem = new asItem
                                {
                                    article_number = artNr.Equals(pro.article_number) ? artNr : artNr + " (" + pro.article_number + ")",
                                    name = getName(fname, pro, matname),
                                    is_available = pro?.is_available ?? false,
                                    is_selectable = standardLists.entityMetaData.FirstOrDefault(p => p.entity_id == pro?.id && p.entity_type.Equals("frame") && p.metadata_key.Equals("isSelectable"))?.bool_value ?? false,
                                    prod = pro
                                };
                                listitems.Add(listitem);
                            }
                            else
                            {
                                asItem listitem = new asItem
                                {
                                    article_number = artNr,
                                    name = getName(fname, pro, matname),
                                    is_available = false,
                                    is_selectable = false,
                                    prod = null
                                }; 
                                listitems.Add(listitem);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    dgv.DataSource = null;
                    SortableBindingList<asItem> sortableList = new SortableBindingList<asItem>();
                    foreach (var litem in listitems)
                    {
                        sortableList.Add(litem);
                    }
                    dgv.DataSource = sortableList;
                    btnSetAvail.Enabled = listitems.Count > 0;
                    btnClearAvail.Enabled = listitems.Count > 0;
                    btnSetSelect.Enabled = listitems.Count > 0;
                    btnClearSelect.Enabled = listitems.Count > 0;
                }
            }
            catch (Exception ex)
            {
                //listitems.Clear();
                //dgv.DataSource = null;
                MessageBox.Show($"Error reading values: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            updateButtons(true);
        }

        private void updateButtons(bool enable)
        {
            btnSetAvail.Enabled = enable;
            btnClearAvail.Enabled = enable;
            btnSetSelect.Enabled = enable;
            btnClearSelect.Enabled = enable;
            btnSave.Enabled = sqlCommands.Count > 0 && enable;
            btnClose.Enabled = enable;
            Application.DoEvents();
        }

        private void ExecuteBulkUpdate(IEnumerable<asItem> items, Func<asItem, string> sqlFactory, string successMessage)
        {
            if (items == null) return;
            updateButtons(false);

            foreach (var item in items)
            {
                // Falls ein Item nicht in standardLists.products existiert -> überspringen
                var product = standardLists.products.FirstOrDefault(p => p.article_number == item.article_number);
                if (product != null)
                {
                    string sql = sqlFactory(item);
                    if (!string.IsNullOrWhiteSpace(sql))
                    {
                        sqlCommands.Add(sql);
                    }
                }
            }
            updateButtons(true);

        }

        // Event-Handler jetzt kurz und lesbar:
        private void btnClearAvail_Click(object sender, EventArgs e)
        {
            ExecuteBulkUpdate(listitems,
                item => $"UPDATE product SET is_available = false WHERE article_number = '{item.article_number}'",
                "is_available set to false for the selected items.");
        }

        private void btnSetAvail_Click(object sender, EventArgs e)
        {
            ExecuteBulkUpdate(listitems,
                item => $"UPDATE product SET is_available = true WHERE article_number = '{item.article_number}'",
                "is_available set to true for the selected items.");
        }

        private void btnSetSelect_Click(object sender, EventArgs e)
        {
            ExecuteBulkUpdate(listitems,
                item =>
                {
                    var product = standardLists.products.FirstOrDefault(p => p.article_number == item.article_number);
                    return product == null ? null :
                        $"UPDATE entity_metadata SET bool_value = true WHERE entity_id = '{product.id}' and entity_type = 'frame' and metadata_key = 'isSelectable'";
                },
                "is_selectable set to true for the selected items.");
        }

        private void btnClearSelect_Click(object sender, EventArgs e)
        {
            ExecuteBulkUpdate(listitems,
                item =>
                {
                    var product = standardLists.products.FirstOrDefault(p => p.article_number == item.article_number);
                    return product == null ? null :
                        $"UPDATE entity_metadata SET bool_value = false WHERE entity_id = '{product.id}' and entity_type = 'frame' and metadata_key = 'isSelectable'";
                },
                "is_selectable set to false for the selected items.");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void setAvailableSelectable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlCommands.Count > 0)
            {
                DialogResult result = MessageBox.Show("There are unsaved changes. Do you want to save them before closing?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    btnSave_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true; // Cancel the closing event
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "SQL files (*.sql)|*.sql|All files (*.*)|*.*",
                Title = "Save SQL Script"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialog.FileName, string.Join(";\n", sqlCommands));
                sqlCommands.Clear();
                btnSave.Enabled = false;
            }
        }

        private void tsmiCopyToClipboard_Click(object sender, EventArgs e)
        {
            try
            {
                // Empfehlung: vorher einmal setzen, z.B. beim Initialisieren des dgv:
                dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;

                var content = dgv.GetClipboardContent();
                if (content != null)
                {
                    Clipboard.SetDataObject(content);
                    // optional: Statusanzeige
                }
                else
                {
                    // Fallback: manuelles Kopieren (Tab-getrennt)
                    var cells = dgv.SelectedCells.Cast<DataGridViewCell>()
                                 .OrderBy(c => c.RowIndex).ThenBy(c => c.ColumnIndex).ToList();
                    if (!cells.Any()) return;

                    var cols = cells.Select(c => c.OwningColumn.Index).Distinct().OrderBy(i => i).ToList();
                    var sb = new System.Text.StringBuilder();
                    sb.AppendLine(string.Join("\t", cols.Select(i => dgv.Columns[i].HeaderText)));

                    foreach (var g in cells.GroupBy(c => c.RowIndex).OrderBy(g => g.Key))
                    {
                        var rowValues = cols.Select(colIndex =>
                        {
                            var cell = g.FirstOrDefault(c => c.ColumnIndex == colIndex);
                            return (cell?.FormattedValue?.ToString() ?? string.Empty)
                                   .Replace("\r", " ").Replace("\n", " ").Replace("\t", " ");
                        });
                        sb.AppendLine(string.Join("\t", rowValues));
                    }
                    Clipboard.SetText(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Copy error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvContextMenu_Opening(object sender, CancelEventArgs e)
        {
            tsmiCopyToClipboard.Enabled = dgv.SelectedCells.Count > 0;
            tsmiChangeArticleNumber.Enabled = dgv.SelectedCells.Count > 0;
        }

        private void tsmiChangeArticleNumber_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dgv.SelectedCells)
            {
                if (cell.OwningColumn.DataPropertyName == "article_number")
                {
                    string oldValue = cell.Value?.ToString().Split(new char[] { '(', ')' })[1] ?? string.Empty;
                    string newValue = cell.Value?.ToString().Split(new char[] { '(', ')' })[0] ?? string.Empty;
                    if (!string.IsNullOrWhiteSpace(newValue) && !string.IsNullOrWhiteSpace(oldValue) && newValue != oldValue)
                    {
                        string sql = $"UPDATE product SET article_number = '{newValue}' WHERE article_number = '{oldValue}'";
                        if (!string.IsNullOrWhiteSpace(sql))
                        {
                            sqlCommands.Add(sql);
                        }
                    }
                }
            }
            updateButtons(true);
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            int cnt = (from DataGridViewCell cell in dgv.SelectedCells 
                       where cell.OwningColumn.DataPropertyName == "article_number"
                       select cell).Count();
            lbRows.Text = cnt.ToString();
        }

        private void tsmiPaste_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                clipboardText = Clipboard.GetText();
                ReadValues();
            }
        }
    }

    public class asItem
    {
        public string article_number { get; set; }
        public string name { get; set; }
        public bool is_available { get; set; }
        public bool is_selectable { get; set; }
        public product prod { get; set; }
    }
}
