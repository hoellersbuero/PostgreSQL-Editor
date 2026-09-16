using PostgreSQL_Editor.DBUtils;
using PostgreSQL_Editor.Global;
using PostgreSQL_Editor.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace PostgreSQL_Editor.FunctionViews
{
    public partial class addNewFrames : Form
    {
        private string excelFile = null;
        DataTable dataTable = null;
        List<string> usedTables = new List<string>() { "product", "frame", "frame_window", "entity_metadata" };
        private Dictionary<string, List<string>> TableColumnsByTable = new Dictionary<string, List<string>>();
        private List<string> SQLCommands = new List<string>();
        private List<string> RemoveCommands = new List<string>();
        private PleaseWaitForm waitForm = null;

        public addNewFrames()
        {
            InitializeComponent();
        }

        public static void Execute(Form owner)
        {
            addNewFrames form = new addNewFrames();
            form.Owner = owner;
            form.ShowDialog();
        }

        private void ImportExcel()
        {
            if (string.IsNullOrEmpty(excelFile))
            {
                MessageBox.Show("Please select an Excel file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                dataTable = ExcelReader.ReadExcelFile(excelFile);
                dgv.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading Excel file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hilfsfunktionen
        static int? ParseNullableInt(object value)
        {
            if (value == null || value == DBNull.Value) return null;
            var s = value.ToString();
            return string.IsNullOrWhiteSpace(s) ? null : (int.TryParse(s, out var v) ? (int?)v : null);
        }

        static Guid ParseGuidOrDefault(object value, Guid defaultValue = default)
        {
            if (value == null || value == DBNull.Value) return defaultValue;
            var s = value.ToString();
            return Guid.TryParse(s, out var g) ? g : defaultValue;
        }

        static decimal ParseDecimalOrDefault(object value, decimal defaultValue = 0)
        {
            if (value == null || value == DBNull.Value) return defaultValue;

            var s = value.ToString().Trim();
            if (string.IsNullOrWhiteSpace(s)) return defaultValue;

            s = s.Replace("\u00A0", "").Replace(" ", "");

            bool hasDot = s.IndexOf('.') >= 0;
            bool hasComma = s.IndexOf(',') >= 0;

            // Fall mit beiden Trennzeichen (z.B. "1.234,56" oder "1,234.56")
            if (hasDot && hasComma)
            {
                if (s.LastIndexOf(',') > s.LastIndexOf('.'))
                {
                    var normalized = s.Replace(".", "").Replace(',', '.'); // "1.234,56" -> "1234.56"
                    if (decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out var d)) return d;
                }
                else
                {
                    var normalized = s.Replace(",", ""); // "1,234.56" -> "1234.56"
                    if (decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out var d)) return d;
                }
            }
            else
            {
                // Erst aktuelle Kultur (z.B. de-DE -> Komma ist Dezimaltrennzeichen)
                if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.CurrentCulture, out var d)) return d;

                // Dann invariant (z.B. falls Punkt verwendet wird)
                if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out d)) return d;

                // Sonderfall: nur Komma vorhanden, versuche als Punkt
                if (hasComma)
                {
                    var alt = s.Replace(',', '.');
                    if (decimal.TryParse(alt, NumberStyles.Number, CultureInfo.InvariantCulture, out d)) return d;
                }
            }

            return defaultValue;
        }

        static bool ParseBoolOrDefault(object value, bool defaultValue = false)
        {
            if (value == null || value == DBNull.Value) return defaultValue;
            var s = value.ToString().Trim();
            if (string.IsNullOrWhiteSpace(s)) return defaultValue;

            // Direkter bool-Parse
            if (bool.TryParse(s, out var b)) return b;

            // Numerische Formen 1/0
            if (int.TryParse(s, out var i)) return i != 0;
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var d)) return d != 0m;

            // Häufige textuelle Varianten (inkl. Deutsch/Englisch)
            switch (s.ToLowerInvariant())
            {
                case "1":
                case "true":
                case "yes":
                case "y":
                case "ja":
                case "j":
                    return true;
                case "0":
                case "false":
                case "no":
                case "n":
                case "nein":
                    return false;
                default:
                    return defaultValue;
            }
        }

        private void UpdateButtons(bool enable)
        {
            btnLoad.Enabled = enable;
            tbUser.Enabled = enable;
            btnSave.Enabled = enable && SQLCommands.Count > 0;
            btnClose.Enabled = enable;
            Application.DoEvents();
        }

        private void CreateSQLCommands()
        {
            UpdateButtons(false);
            try
            {
                waitForm = new PleaseWaitForm();
                waitForm.Owner = this;
                waitForm.StartPosition= FormStartPosition.CenterParent;
                waitForm.Show();
                Application.DoEvents();
                ImportExcel();
                if (dataTable == null)
                {
                    MessageBox.Show("No data loaded. Please load an Excel file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                productClass productData = new productClass();
                productData.sqlinsert = "INSERT INTO product (id, name, article_number, product_type_id, is_available, weight_kg, created_by, created_ts, modified_by, modified_ts) VALUES ('@id'::uuid, '@name', '@article_number', '@product_type_id'::uuid, @is_available, @weight_kg, '@created_by', '@created_ts', '@modified_by', '@modified_ts');";
                productData.remove = "DELETE FROM product WHERE id = '@id'::uuid;";
                geometryClass geometryData = new geometryClass();
                geometryData.sqlinsert = "INSERT INTO frame_geometry (id, h1, b1, h2, b2, opening_addition_min, opening_addition_max, flange_type, created_by, created_ts, modified_by, modified_ts) VALUES ('@id', @h1, @b1, @h2, @b2, @opening_addition_min, @opening_addition_max, '@flange_type', '@created_by', '@created_ts', '@modified_by', '@modified_ts');";
                geometryData.remove = "DELETE FROM frame_geometry WHERE id = '@id';";
                frameClass frameData = new frameClass();
                frameData.sqlinsert = "INSERT INTO frame (id, frame_type_id, material_type_id, geometry_id, wedge_quantity, holes_horizontal, holes_vertical, offset_horizontal, offset_vertical, drill_diameter, has_drilled_holes, drilling_schema_id, hole_schema_id, rows, columns, created_by, created_ts, modified_by, modified_ts) VALUES ('@id'::uuid, '@frame_type_id'::uuid, '@material_type_id'::uuid, '@geometry_id'::uuid, @wedge_quantity, @holes_horizontal, @holes_vertical, @offset_horizontal, @offset_vertical, @drill_diameter, @has_drilled_holes, @drilling_schema_id, @hole_schema_id, @rows, @columns, '@created_by', '@created_ts', '@modified_by', '@modified_ts');";
                frameData.remove = "DELETE FROM frame WHERE id = '@id'::uuid;";
                frameWindowClass frameWindowData = new frameWindowClass();
                frameWindowData.sqlinsert = "INSERT INTO frame_window (id, frame_id, frame_window_height, frame_window_width, frame_window_height_natural, frame_window_width_natural, created_by, created_ts, modified_by, modified_ts) VALUES ('@id'::uuid, '@frame_id'::uuid, @frame_window_height, @frame_window_width, @frame_window_height_natural, @frame_window_width_natural, '@created_by', '@created_ts', '@modified_by', '@modified_ts');";
                frameWindowData.remove = "DELETE FROM frame_window WHERE id = '@id'::uuid;";
                metadataClass metadataData = new metadataClass();
                metadataData.sqlinsert = "INSERT INTO entity_metadata (id, entity_id, bool_value, entity_type, metadata_key, created_by, created_ts, modified_by, modified_ts) VALUES ('@id'::uuid, '@entity_id'::uuid, @bool_value, '@entity_type', '@metadata_key', '@created_by', '@created_ts', '@modified_by', '@modified_ts');";
                metadataData.remove = "DELETE FROM entity_metadata WHERE id = '@id'::uuid;";
                foreach (DataRow row in dataTable.Rows)
                {
                    Guid frameId = Guid.NewGuid();
                    // Guid geometryId = Guid.NewGuid();
                    product productType = new product
                    {
                        id = frameId,
                        name = row["name"].ToString(),
                        article_number = row["article_number"].ToString(),
                        is_available = ParseBoolOrDefault(row["is_available"], false),
                        weight_kg = ParseDecimalOrDefault(row["weight_kg"], 0),
                        product_type_id = (Guid)standardLists.productTypes.FirstOrDefault(t => t.name.Equals("frame"))?.id,
                        created_by = tbUser.Text,
                        created_ts = DateTime.Now,
                        modified_by = tbUser.Text,
                        modified_ts = DateTime.Now
                    };
                    productData.productTypes.Add(productType);

                    frame_geometry geometryItem = new frame_geometry
                    {
                        id = frameId,
                        h1 = ParseDecimalOrDefault(row["h1"], 0),
                        b1 = ParseDecimalOrDefault(row["b1"], 0),
                        h2 = ParseDecimalOrDefault(row["h2"], 0),
                        b2 = ParseDecimalOrDefault(row["b2"], 0),
                        opening_addition_min = ParseDecimalOrDefault(row["opening_addition_min"], 0),
                        opening_addition_max = ParseDecimalOrDefault(row["opening_addition_max"], 0),
                        flange_type = string.Empty,
                        created_by = tbUser.Text,
                        created_ts = DateTime.Now,
                        modified_by = tbUser.Text,
                        modified_ts = DateTime.Now
                    };
                    geometryData.geometries.Add(geometryItem);

                    frame_alone frameItem = new frame_alone
                    {
                        id = frameId,
                        frame_type_id = (Guid)standardLists.frameTypes.FirstOrDefault(t => t.name.Equals(row["frame_type"]))?.id,
                        material_type_id = (Guid)standardLists.materialTypes.FirstOrDefault(m => m.name.Equals(row["material_type"]))?.id,
                        geometry_id = frameId,
                        wedge_quantity = ParseNullableInt(row["wedge_quantity"]) ?? 0,
                        holes_horizontal = ParseNullableInt(row["holes_horizontal"]) ?? 0,
                        holes_vertical = ParseNullableInt(row["holes_vertical"]) ?? 0,
                        offset_horizontal = ParseDecimalOrDefault(row["offset_horizontal"], 0),
                        offset_vertical = ParseDecimalOrDefault(row["offset_vertical"], 0),
                        drill_diameter = ParseDecimalOrDefault(row["drill_diameter"], 0),
                        has_drilled_holes = ParseBoolOrDefault(row["has_drilled_holes"], false),
                        drilling_schema_id = ParseNullableInt(row["drilling_schema_id"]),
                        hole_schema_id = ParseNullableInt(row["hole_schema_id"]),
                        rows = ParseNullableInt(row["rows"]) ?? 0,
                        columns = ParseNullableInt(row["columns"]) ?? 0,
                        created_by = tbUser.Text,
                        created_ts = DateTime.Now,
                        modified_by = tbUser.Text,
                        modified_ts = DateTime.Now
                    };
                    frameData.frames.Add(frameItem);

                    frame_window frameWindowItem = new frame_window
                    {
                        id = Guid.NewGuid(),
                        frame_id = frameId,
                        frame_window_height_natural = ParseDecimalOrDefault(row["frame_window_height_natural"], 0),
                        frame_window_width_natural = ParseDecimalOrDefault(row["frame_window_width_natural"], 0),
                        frame_window_height = ParseDecimalOrDefault(row["frame_window_height"], 0),
                        frame_window_width = ParseDecimalOrDefault(row["frame_window_width"], 0),
                        created_by = tbUser.Text,
                        created_ts = DateTime.Now,
                        modified_by = tbUser.Text,
                        modified_ts = DateTime.Now
                    };
                    frameWindowData.frameWindows.Add(frameWindowItem);

                    entity_metadata metadataItem = new entity_metadata
                    {
                        id = Guid.NewGuid(),
                        entity_id = frameId,
                        number_value = null,
                        bool_value = ParseBoolOrDefault(row["is_selectable"], false),
                        json_value = null,
                        entity_type = "frame",
                        metadata_key = "isSelectable",
                        string_value = null,
                        created_by = tbUser.Text,
                        created_ts = DateTime.Now,
                        modified_by = tbUser.Text,
                        modified_ts = DateTime.Now
                    };
                    metadataData.entityMetadatas.Add(metadataItem);
                }
                if (productData.productTypes.Count == 0 && geometryData.geometries.Count == 0 && frameData.frames.Count == 0 && frameWindowData.frameWindows.Count == 0 && metadataData.entityMetadatas.Count == 0)
                {
                    MessageBox.Show("No valid data found in the Excel file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseWaitForm();
                    return;
                }
                int cnt = productData.productTypes.Count;
                if (geometryData.geometries.Count == cnt && frameData.frames.Count == cnt && frameWindowData.frameWindows.Count == cnt && metadataData.entityMetadatas.Count == cnt)
                {
                    SQLCommands.Clear();
                    tbSQL.Text = string.Empty;
                    for (int i = 0; i < cnt; i++)
                    {
                        string sql = productData.sqlinsert + string.Empty;
                        product pro = productData.productTypes[i];
                        sql =sql.Replace("@id", pro.id.ToString())
                           .Replace("@name", pro.name)
                           .Replace("@article_number", pro.article_number)
                           .Replace("@product_type_id", pro.product_type_id.ToString())
                           .Replace("@is_available", pro.is_available.ToString())
                           .Replace("@weight_kg", pro.weight_kg.ToString(CultureInfo.InvariantCulture))
                           .Replace("@created_by", pro.created_by)
                           .Replace("@created_ts", pro.created_ts.ToString("yyyy-MM-dd HH:mm:ss"))
                           .Replace("@modified_by", pro.modified_by)
                           .Replace("@modified_ts", pro.modified_ts.ToString("yyyy-MM-dd HH:mm:ss"));
                        SQLCommands.Add(sql);
                        tbSQL.AppendText(sql + Environment.NewLine);
                        sql = productData.remove + string.Empty;
                        sql = sql.Replace("@id", pro.id.ToString());
                        RemoveCommands.Add(sql);
                        tbRemove.AppendText(sql + Environment.NewLine);

                        sql = geometryData.sqlinsert + string.Empty;
                        frame_geometry geo = geometryData.geometries[i];
                        sql = sql.Replace("@id", geo.id.ToString())
                           .Replace("@h1", geo.h1.ToString(CultureInfo.InvariantCulture))
                           .Replace("@b1", geo.b1.ToString(CultureInfo.InvariantCulture))
                           .Replace("@h2", geo.h2.ToString(CultureInfo.InvariantCulture))
                           .Replace("@b2", geo.b2.ToString(CultureInfo.InvariantCulture))
                           .Replace("@opening_addition_min", geo.opening_addition_min.ToString(CultureInfo.InvariantCulture))
                           .Replace("@opening_addition_max", geo.opening_addition_max.ToString(CultureInfo.InvariantCulture))
                           .Replace("@flange_type", geo.flange_type)
                           .Replace("@created_by", geo.created_by)
                           .Replace("@created_ts", geo.created_ts.ToString("yyyy-MM-dd HH:mm:ss"))
                           .Replace("@modified_by", geo.modified_by)
                           .Replace("@modified_ts", geo.modified_ts.ToString("yyyy-MM-dd HH:mm:ss"));
                        SQLCommands.Add(sql);
                        tbSQL.AppendText(sql + Environment.NewLine);
                        sql = geometryData.remove + string.Empty;
                        sql = sql.Replace("@id", geo.id.ToString());
                        RemoveCommands.Add(sql);
                        tbRemove.AppendText(sql + Environment.NewLine);

                        sql = frameData.sqlinsert + string.Empty;
                        frame_alone frame = frameData.frames[i];
                        sql = sql.Replace("@id", frame.id.ToString())
                           .Replace("@frame_type_id", frame.frame_type_id.ToString())
                           .Replace("@material_type_id", frame.material_type_id.ToString())
                           .Replace("@geometry_id", frame.geometry_id.ToString())
                           .Replace("@wedge_quantity", frame.wedge_quantity.ToString(CultureInfo.InvariantCulture))
                           .Replace("@holes_horizontal", frame.holes_horizontal.ToString(CultureInfo.InvariantCulture))
                           .Replace("@holes_vertical", frame.holes_vertical.ToString(CultureInfo.InvariantCulture))
                           .Replace("@offset_horizontal", frame.offset_horizontal.ToString(CultureInfo.InvariantCulture))
                           .Replace("@offset_vertical", frame.offset_vertical.ToString(CultureInfo.InvariantCulture))
                           .Replace("@drill_diameter", frame.drill_diameter.ToString(CultureInfo.InvariantCulture))
                           .Replace("@has_drilled_holes", frame.has_drilled_holes.ToString())
                           .Replace("@drilling_schema_id", frame.drilling_schema_id?.ToString() ?? "NULL")
                           .Replace("@hole_schema_id", frame.hole_schema_id?.ToString() ?? "NULL")
                           .Replace("@rows", frame.rows.ToString(CultureInfo.InvariantCulture))
                           .Replace("@columns", frame.columns.ToString(CultureInfo.InvariantCulture))
                           .Replace("@created_by", frame.created_by)
                           .Replace("@created_ts", frame.created_ts.ToString("yyyy-MM-dd HH:mm:ss"))
                           .Replace("@modified_by", frame.modified_by)
                           .Replace("@modified_ts", frame.modified_ts.ToString("yyyy-MM-dd HH:mm:ss"));
                        SQLCommands.Add(sql);
                        tbSQL.AppendText(sql + Environment.NewLine);
                        sql = frameData.remove + string.Empty;
                        sql = sql.Replace("@id", frame.id.ToString());
                        RemoveCommands.Add(sql);
                        tbRemove.AppendText(sql + Environment.NewLine);

                        sql = frameWindowData.sqlinsert + string.Empty;
                        frame_window window = frameWindowData.frameWindows[i];
                        sql = sql.Replace("@id", window.id.ToString())
                           .Replace("@frame_id", window.frame_id.ToString())
                           .Replace("@frame_window_height_natural", window.frame_window_height_natural.ToString(CultureInfo.InvariantCulture))
                           .Replace("@frame_window_width_natural", window.frame_window_width_natural.ToString(CultureInfo.InvariantCulture))
                           .Replace("@frame_window_height", window.frame_window_height.ToString(CultureInfo.InvariantCulture))
                           .Replace("@frame_window_width", window.frame_window_width.ToString(CultureInfo.InvariantCulture))
                           .Replace("@created_by", window.created_by)
                           .Replace("@created_ts", window.created_ts.ToString("yyyy-MM-dd HH:mm:ss"))
                           .Replace("@modified_by", window.modified_by)
                           .Replace("@modified_ts", window.modified_ts.ToString("yyyy-MM-dd HH:mm:ss"));
                        SQLCommands.Add(sql);
                        tbSQL.AppendText(sql + Environment.NewLine);
                        sql = frameWindowData.remove + string.Empty;
                        sql = sql.Replace("@id", window.id.ToString());
                        RemoveCommands.Add(sql);
                        tbRemove.AppendText(sql + Environment.NewLine);

                        sql = metadataData.sqlinsert + string.Empty;
                        entity_metadata metadata = metadataData.entityMetadatas[i];
                        string jsonSql = metadata.json_value != null ? "'" + metadata.json_value.RootElement.GetRawText().Replace("'", "''") + "'" : "NULL";
                        sql = sql.Replace("@id", metadata.id.ToString())
                           .Replace("@entity_id", metadata.entity_id.ToString())
                           .Replace("@bool_value", metadata.bool_value?.ToString() ?? "NULL")
                           .Replace("@entity_type", metadata.entity_type)
                           .Replace("@metadata_key", metadata.metadata_key)
                           .Replace("@created_by", metadata.created_by)
                           .Replace("@created_ts", metadata.created_ts.ToString("yyyy-MM-dd HH:mm:ss"))
                           .Replace("@modified_by", metadata.modified_by)
                           .Replace("@modified_ts", metadata.modified_ts.ToString("yyyy-MM-dd HH:mm:ss"));
                        SQLCommands.Add(sql);
                        tbSQL.AppendText(sql + Environment.NewLine);
                        sql = metadataData.remove + string.Empty;
                        sql = sql.Replace("@id", metadata.id.ToString());
                        RemoveCommands.Add(sql);
                        tbRemove.AppendText(sql + Environment.NewLine);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CloseWaitForm();
                return;
            }
            tbSQL.HighlightSqlWords();
            tbRemove.HighlightSqlWords();
            UpdateButtons(true);
            CloseWaitForm();
        }

        private void CloseWaitForm()
        {
            if (waitForm != null && !waitForm.IsDisposed)
            {
                waitForm.Close();
                waitForm.Dispose();
                waitForm = null;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select an Excel file";
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                excelFile = openFileDialog.FileName;
                tbExcelFile.Text = excelFile;
                CreateSQLCommands();
            }
        }

        private void tbUser_TextChanged(object sender, EventArgs e)
        {
            btnLoad.Enabled = !string.IsNullOrWhiteSpace(tbUser.Text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save SQL commands";
            saveFileDialog.Filter = "SQL files (*.sql)|*.sql|All files (*.*)|*.*";
            saveFileDialog.FileName = "create_frames_commands-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".sql";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string saveFileName = saveFileDialog.FileName;
                    string removeFileName = saveFileName.Replace("create_", "remove_");
                    System.IO.File.WriteAllLines(saveFileDialog.FileName, SQLCommands);
                    System.IO.File.WriteAllLines(removeFileName, RemoveCommands);
                    MessageBox.Show($"SQL commands saved to {saveFileDialog.FileName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SQLCommands.Clear();
                    RemoveCommands.Clear();
                    tbSQL.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving SQL commands: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void addNewFrames_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SQLCommands.Count > 0)
            {
                var result = MessageBox.Show("You have unsaved SQL commands. Do you want to save them before closing?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    btnSave.PerformClick();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true; // Cancel the closing event
                }
            }
        }
    }

    internal class tableColumns
    {
        public string tableColumn { get; set; }
        public string excelColumn { get; set; }
    }

    internal class productClass
    {
        public string sqlinsert { get; set; }
        public string remove { get; set; }

        public List<product> productTypes { get; set; } = new List<product>();
    }

    internal class  geometryClass
    {
        public string sqlinsert { get; set; }
        public string remove { get; set; }
        public List<frame_geometry> geometries { get; set; } = new List<frame_geometry>();
    }

    internal class frameClass
    {
        public string sqlinsert { get; set; }
        public string remove { get; set; }
        public List<frame_alone> frames { get; set; } = new List<frame_alone>();
    }

    internal class frameWindowClass
    {
        public string sqlinsert { get; set; }
        public string remove { get; set; }
        public List<frame_window> frameWindows { get; set; } = new List<frame_window>();
    }

    internal class  metadataClass
    {
        public string sqlinsert { get; set; }
        public string remove { get; set; }
        public List<entity_metadata> entityMetadatas { get; set; } = new List<entity_metadata>();
    }
}
