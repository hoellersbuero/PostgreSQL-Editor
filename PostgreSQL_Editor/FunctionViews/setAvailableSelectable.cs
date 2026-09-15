using Npgsql;
using PostgreSQL_Editor.DBUtils;
using PostgreSQL_Editor.Global;
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

        public setAvailableSelectable()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public static void Execute(Form owner, NpgsqlConnection npgsql)
        {
            setAvailableSelectable form = new setAvailableSelectable();
            form.Owner = owner;
            form.npgsql = npgsql; 
            form.ShowDialog();
        }

        private void dgv_DragDrop(object sender, DragEventArgs e)
        {
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

        private void ReadValues()
        {
            try
            {
                listitems.Clear();
                if (!string.IsNullOrEmpty(clipboardText))
                {
                    string[] items = clipboardText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in items)
                    {
                        try
                        {
                            product pro = standardLists.products.FirstOrDefault(p => p.article_number == item) ?? null;
                            if (pro != null)
                            {
                                asItem listitem = new asItem
                                {
                                    article_number = item,
                                    name = pro?.name ?? "Unknown",
                                    is_available = pro?.is_available ?? false,
                                    is_selectable = standardLists.entityMetaData.FirstOrDefault(p => p.entity_id == pro?.id && p.entity_type.Equals("frame") && p.metadata_key.Equals("isSelectable"))?.bool_value ?? false
                                };
                                listitems.Add(listitem);
                            }
                            else
                            {
                                asItem listitem = new asItem
                                {
                                    article_number = item,
                                    name = "Unknown",
                                    is_available = false,
                                    is_selectable = false
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
        }

        private void updateButtons(bool enable)
        {
            btnSetAvail.Enabled = enable;
            btnClearAvail.Enabled = enable;
            btnSetSelect.Enabled = enable;
            btnClearSelect.Enabled = enable;
        }

        private void UpdateDgv()
        {
            standardLists.getProducts(npgsql);
            standardLists.getEntityMetaData(npgsql);
            ReadValues();
        }

        private void ExecuteBulkUpdate(IEnumerable<asItem> items, Func<asItem, string> sqlFactory, string successMessage)
        {
            if (items == null) return;
            updateButtons(false);

            var sb = new System.Text.StringBuilder();
            foreach (var item in items)
            {
                // Falls ein Item nicht in standardLists.products existiert -> überspringen
                var product = standardLists.products.FirstOrDefault(p => p.article_number == item.article_number);
                if (product != null)
                {
                    string sql = sqlFactory(item);
                    if (!string.IsNullOrWhiteSpace(sql))
                    {
                        if (sb.Length > 0) sb.Append(";\n");
                        sb.Append(sql);
                    }
                }
            }

            if (sb.Length == 0)
            {
                updateButtons(true);
                return;
            }

            try
            {
                using (var command = new NpgsqlCommand(sb.ToString(), npgsql))
                {
                    command.ExecuteNonQuery();
                }
                UpdateDgv();
                Application.DoEvents();
                MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing SQL command: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                updateButtons(true);
            }
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
    }

    public class asItem
    {
        public string article_number { get; set; }
        public string name { get; set; }
        public bool is_available { get; set; }
        public bool is_selectable { get; set; }
    }
}
