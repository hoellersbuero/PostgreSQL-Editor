using PostgreSQL_Editor.DBUtils;
using PostgreSQL_Editor.Global;
using PostgreSQL_Editor.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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

        private void matchColumns()
        {
            if (dataTable == null)
            {
                MessageBox.Show("No data loaded. Please load an Excel file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            productClass productData = new productClass();
            productData.sqlinsert = "INSERT INTO product (id, name, article_number, product_type_id, is_available, weight_kg, created_by, created_ts, modified_by, modified_ts) VALUES (@id, @name, @article_number, @product_type_id, @is_available, @weight_kg, @created_by, @created_ts, @modified_by, @modified_ts);";
            geometryClass geometryData = new geometryClass();
            geometryData.sqlinsert = "INSERT INTO frame_geometry (id, h1, b1, h2, b2, opening_addition_min, opening_addition_max, flange_type, created_by, created_ts, modified_by, modified_ts) VALUES (@id, @h1, @b1, @h2, @b2, @opening_addition_min, @opening_addition_max, @flange_type, @created_by, @created_ts, @modified_by, @modified_ts);";
            frameClass frameData = new frameClass();
            frameData.sqlinsert = "INSERT INTO frame (id, frame_type_id, material_type_id, geometry_id, wedge_quantity, holes_horizontal, holes_vertical, offset_horizontal, offset_vertical, drill_diameter, has_drilled_holes, drilling_schema_id, hole_schema_id, rows, columns, created_by, created_ts, modified_by, modified_ts) VALUES (@id, @frame_type_id, @material_type_id, @geometry_id, @wedge_quantity, @holes_horizontal, @holes_vertical, @offset_horizontal, @offset_vertical, @drill_diameter, @has_drilled_holes, @drilling_schema_id, @hole_schema_id, @rows, @columns, @created_by, @created_ts, @modified_by, @modified_ts);";
            frameWindowClass frameWindowData = new frameWindowClass();
            frameWindowData.sqlinsert = "INSERT INTO frame_window (id, frame_id, frame_window_height, frame_window_width, frame_window_height_natural, frame_window_width_natural, created_by, created_ts, modified_by, modified_ts) VALUES (@id, @frame_id, @frame_window_height, @frame_window_width, @frame_window_height_natural, @frame_window_width_natural, @created_by, @created_ts, @modified_by, @modified_ts);";
            metadataClass metadataData = new metadataClass();
            metadataData.sqlinsert = "INSERT INTO entity_metadata (id, entity_id, number_value, bool_value, json_value, entity_type, metadata_key, string_value, created_by, created_ts, modified_by, modified_ts) VALUES (@id, @entity_id, @number_value, @bool_value, @json_value, @entity_type, @metadata_key, @string_value, @created_by, @created_ts, @modified_by, @modified_ts);";
            foreach (DataRow row in dataTable.Rows)
            {
                Guid frameId = Guid.NewGuid();
                Guid geometryId = Guid.NewGuid();
                product productType = new product
                {
                    id = frameId,
                    name = row["name"].ToString(),
                    is_available = ParseBoolOrDefault(row["is_available"], false),
                    weight_kg = ParseDecimalOrDefault(row["weight_kg"], 0),
                    product_type_id = ParseGuidOrDefault(row["product_type_id"], Guid.Empty),
                    created_by = tbUser.Text,
                    created_ts = DateTime.Now,
                    modified_by = tbUser.Text,
                    modified_ts = DateTime.Now
                };
                productData.productTypes.Add(productType);

                frame_geometry geometryItem = new frame_geometry
                {
                    id = geometryId,
                    h1 = ParseDecimalOrDefault(row["h1"], 0),
                    b1 = ParseDecimalOrDefault(row["b1"], 0),
                    h2 = ParseDecimalOrDefault(row["h2"], 0),
                    b2 = ParseDecimalOrDefault(row["b2"], 0),
                    opening_addition_min = ParseDecimalOrDefault(row["opening_addition_min"], 0),
                    opening_addition_max = ParseDecimalOrDefault(row["opening_addition_max"], 0),
                    flange_type = row["flange_type"] != DBNull.Value ? row["flange_type"].ToString() : string.Empty,
                    created_by = tbUser.Text,
                    created_ts = DateTime.Now,
                    modified_by = tbUser.Text,
                    modified_ts = DateTime.Now
                };
                geometryData.geometries.Add(geometryItem);

                frame_alone frameItem = new frame_alone
                {
                    id = frameId,
                    frame_type_id = ParseGuidOrDefault(row["frame_type_id"], Guid.Empty),
                    material_type_id = ParseGuidOrDefault(row["material_type_id"], Guid.Empty),
                    geometry_id = geometryId,
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
                    frame_window_height = ParseDecimalOrDefault(row["frame_window_height"], 0),
                    frame_window_width = ParseDecimalOrDefault(row["frame_window_width"], 0),
                    frame_window_height_natural = ParseDecimalOrDefault(row["frame_window_height_natural"], 0),
                    frame_window_width_natural = ParseDecimalOrDefault(row["frame_window_width_natural"], 0),
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
                    number_value = ParseDecimalOrDefault(row["number_value"]),
                    bool_value = ParseBoolOrDefault(row["bool_value"], false),
                    json_value = (row["json_value"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["json_value"].ToString())) ? JsonDocument.Parse(row["json_value"].ToString()) : null,
                    entity_type = row["entity_type"] != DBNull.Value ? row["entity_type"].ToString() : string.Empty,
                    metadata_key = row["metadata_key"] != DBNull.Value ? row["metadata_key"].ToString() : string.Empty,
                    string_value = row["string_value"] != DBNull.Value ? row["string_value"].ToString() : string.Empty,
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
                return;
            }
            int cnt = productData.productTypes.Count;
            if (geometryData.geometries.Count == cnt && frameData.frames.Count == cnt && frameWindowData.frameWindows.Count == cnt && metadataData.entityMetadatas.Count == cnt)
            {
                
            }
        }


        private List<string> GetDatabaseColumns(string tableName)
        {
            Global.Global.TableColumnsByTable.TryGetValue(tableName, out List<string> dbColumns);
            return dbColumns ?? new List<string>();
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
                ImportExcel();
                matchColumns();
            }
        }

        private void addNewFrames_Load(object sender, EventArgs e)
        {
            while (tabControl1.TabPages.Count > 1)
            {
                tabControl1.TabPages.RemoveAt(tabControl1.TabPages.Count - 1);
            }
        }

        private void tbUser_TextChanged(object sender, EventArgs e)
        {
            btnLoad.Enabled = !string.IsNullOrWhiteSpace(tbUser.Text);
        }

        private void tsmiCopyToClipboard_Click(object sender, EventArgs e)
        {

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

        public List<product> productTypes { get; set; } = new List<product>();
    }

    internal class  geometryClass
    {
        public string sqlinsert { get; set; }
        public List<frame_geometry> geometries { get; set; } = new List<frame_geometry>();
    }

    internal class frameClass
    {
        public string sqlinsert { get; set; }
        public List<frame_alone> frames { get; set; } = new List<frame_alone>();
    }

    internal class frameWindowClass
    {
        public string sqlinsert { get; set; }
        public List<frame_window> frameWindows { get; set; } = new List<frame_window>();
    }

    internal class  metadataClass
    {
        public string sqlinsert { get; set; }
        public List<entity_metadata> entityMetadatas { get; set; } = new List<entity_metadata>();
    }
}
