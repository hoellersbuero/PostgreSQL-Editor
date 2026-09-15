using Npgsql;
using PostgreSQL_Editor.DBUtils;
using PostgreSQL_Editor.EditRules;
using PostgreSQL_Editor.FunctionViews;
using PostgreSQL_Editor.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PostgreSQL_Editor
{
    public partial class Main : Form
    {
        private NpgsqlConnection npgsql = null;
        private ImageList imageList = new ImageList();
        private readonly string _windowStateFile;
        private TreeNode _selectedNode = null;
        private int _ctxRow = -1;
        private int _ctxCol = -1;
        private Dictionary<string, string> TableColumns = new Dictionary<string, string>();
        private Dictionary<string, List<string>> TableColumnsByTable = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        private List<string> tableNames = new List<string>();
        private List<string> columnNames = new List<string>();
        private List<GuidSearchResult> guidSearchResults = new List<GuidSearchResult>();
        private BackgroundWorker _exportWorker;
        private ContextMenuStrip contextMenuStrip1;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0x000B;


        public Main()
        {
            InitializeComponent();
            InitializeExportWorker();
            // Pfad für Fensterzustandsdatei
            _windowStateFile = Path.Combine(Application.UserAppDataPath, "windowstate.xml");
            // Wiederherstellen der letzten Fenstergröße/-position vor weiteren Initialisierungen
            RestoreWindowState();

            string connectionstring = "Host=localhost:5434;Username=admin;Password=test123;Database=postgres";
            npgsql = new NpgsqlConnection(connectionstring);
            try
            {
                npgsql.Open();
            }
            catch (Exception ex)
            {
                tsslInfo.Text = "Connection failed: " + ex.Message;
            }
            if (npgsql.State == ConnectionState.Open)
            {
                tsslInfo.Text = "CONNECTED";
                using (var cmd = npgsql.CreateCommand())
                {
                    cmd.CommandText = "SET schema 'public'";
                    cmd.ExecuteNonQuery();
                }
            }
            contextMenuStrip1 = new ContextMenuStrip();
            contextMenuStrip1.Items.Add("Update", null, updateRow_Click);
            dgv.RowHeaderMouseClick += Dgv_RowHeaderMouseClick;
        }

        private void updateRow_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dgv.Rows[_ctxRow];
            if (selectedRow != null)
            {
                string tableName = _selectedNode.Text;
                string primaryKeyColumn = "id"; // Assuming 'id' is the primary key column
                object primaryKeyValue = selectedRow.Cells[primaryKeyColumn].Value;
                UpdateDgvRow.Execute(this, tableName, selectedRow);
                //List<string> setClauses = new List<string>();
                //foreach (DataGridViewCell cell in selectedRow.Cells)
                //{
                //    if (cell.OwningColumn.Name != primaryKeyColumn)
                //    {
                //        string columnName = cell.OwningColumn.Name;
                //        object value = cell.Value;
                //        string formattedValue = value is string ? $"'{value}'" : value.ToString();
                //        setClauses.Add($"{columnName} = {formattedValue}");
                //    }
                //}
                //string updateQuery = $"UPDATE {Global.Global.schema}.{tableName} SET {string.Join(", ", setClauses)} WHERE {primaryKeyColumn} = '{primaryKeyValue}'";
                //ExecuteSQL(0, updateQuery);
            }
        }

        private void Dgv_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _ctxRow = e.RowIndex;
                _ctxCol = e.ColumnIndex;
                dgv.ClearSelection();
                dgv.Rows[e.RowIndex].Selected = true;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save window state before closing
            SaveWindowState();

            if (npgsql.State == ConnectionState.Open)
                npgsql.Close();
            tsslInfo.Text = "Connection closed";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbSQL.Text = "";
            lbError.Text = "";
            tbClassField.BackColor = Color.White;
            tbClassField.ForeColor = Color.Black;
            tpSchema.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tpCreate.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tpClass.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dgv.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            SetDgvHeaderBold(dgv);
            SetDgvHeaderBold(dgvs);
            // Alternating row coloring: jede zweite Zeile hellgrau
            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            long V = 0xFFF4F4F4;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb((int)V);
            imageList.Images.Add(Properties.Resources.database);
            imageList.Images.Add(Properties.Resources.table);
            tsbSQL.Checked = false;
            tsbExecute.Enabled = false;
            tsbClear.Enabled = false;
            tsbLoadSQL.Enabled = false;
            tsbSaveSQL.Enabled = false;
            tsbExportExcel.Enabled = false;
            tsbShowDataTree.Checked = false;
            splitContainer2.Panel1Collapsed = true;
            treeView.ImageList = imageList;
            LoadDB();

            // Splash sicher schließen (wenn vorhanden). BeginInvoke marshalt auf den Splash-Thread.
            try
            {
                if (Program.Splash != null && Program.Splash.IsHandleCreated)
                {
                    Program.Splash.BeginInvoke((Action)(() =>
                    {
                        try { Program.Splash.Close(); } catch { }
                    }));
                }
            }
            catch
            {
                // Ignoriere Fehler beim Schließen des Splash (keine App-Abstürze)
            }
            this.BringToFront();
        }

        private void LoadDB()
        {
            TreeNode rootNode = null;
            if (npgsql.State == ConnectionState.Open)
            {
                using (var cmd = npgsql.CreateCommand())
                {
                    cmd.CommandText = "SELECT tablename FROM pg_catalog.pg_tables WHERE schemaname = '" + Global.Global.schema + "' ORDER BY tablename;";
                    using (NpgsqlDataReader rd = cmd.ExecuteReader())
                    {
                        rootNode = new TreeNode(Global.Global.schema);
                        rootNode.ImageIndex = 0;
                        rootNode.SelectedImageIndex = 0;
                        while (rd.Read())
                        {
                            string tableName = rd["tablename"] as string;
                            if (!string.IsNullOrEmpty(tableName))
                            {
                                TreeNode tableNode = new TreeNode(tableName);
                                tableNode.ImageIndex = 1;
                                tableNode.SelectedImageIndex = 1;
                                rootNode.Nodes.Add(tableNode);
                            }
                        }
                        rootNode.StateImageIndex = -1;
                        treeView.Nodes.Add(rootNode);
                        treeView.ExpandAll();
                    }
                }
                tsslSuccess.Text = "SUCCESS (" + rootNode.Nodes.Count.ToString() + ")";
                foreach (TreeNode node in rootNode.Nodes)
                {
                    string sql = "SELECT column_name, data_type FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '" + node.Text + "';";
                    ExecuteSQL(4, sql);
                }
                standardLists.loadAll(npgsql);
            }
        }

        private void SetDgvHeaderBold(DataGridView grid)
        {
            if (grid == null) return;

            // Damit eigene Header-Styles angewendet werden (nicht die visuellen Systemstyles)
            grid.EnableHeadersVisualStyles = false;

            // Column header
            var colHeaderBase = grid.ColumnHeadersDefaultCellStyle.Font ?? grid.Font;
            grid.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(
                colHeaderBase.FontFamily,
                colHeaderBase.Size,
                System.Drawing.FontStyle.Bold);

            // Row header (optional)
            var rowHeaderBase = grid.RowHeadersDefaultCellStyle.Font ?? grid.Font;
            grid.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font(
                rowHeaderBase.FontFamily,
                rowHeaderBase.Size,
                System.Drawing.FontStyle.Bold);
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                tsbRefresh.Enabled = false;
                tsbSQL.Enabled = false;
                tsbExecute.Enabled = false;
                tsbClear.Enabled = false;
                tsbLoadSQL.Enabled = false;
                tsbSaveSQL.Enabled = false;
                tsbExportExcel.Enabled = false;
                tsbShowDataTree.Enabled = false;
                tsbForeignKeys.Enabled = false;
                tsbCreateFKeys.Enabled = false;
                tsmiFile.Enabled = false;
                treeView.Enabled = false;
                tbQuery.Enabled = false;
                ExecuteSQL(1, tbQuery.Text);
                tabControl1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                tsbRefresh.Enabled = true;
                tsbSQL.Enabled = true;
                tsbExecute.Enabled = tsbSQL.Checked;
                tsbClear.Enabled = tsbSQL.Checked;
                tsbLoadSQL.Enabled = tsbSQL.Checked;
                tsbSaveSQL.Enabled = tsbSQL.Checked;
                tsbExportExcel.Enabled = true;
                tsbShowDataTree.Enabled = true;
                tsbForeignKeys.Enabled = true;
                tsbCreateFKeys.Enabled = true;
                tsmiFile.Enabled = true;
                treeView.Enabled = true;
                tbQuery.Enabled = true;
            }

        }

        private void ExecuteSQL(int dgrid, string query, string query2 = null, string query3 = null)
        {
            try
            {
                lbError.Text = "";
                lbSQL.Text = "";
                DateTime dt = DateTime.Now;
                using (var cmd = npgsql.CreateCommand())
                {
                    cmd.CommandText = "set schema '" + Global.Global.schema + "'; " + query;
                    if (cmd.CommandText.ToUpper().Contains("SELECT"))
                    {
                        DateTime dtq = DateTime.Now;
                        using (NpgsqlDataReader rd = cmd.ExecuteReader())
                        {
                            TimeSpan tsq = DateTime.Now - dtq;
                            lbSQL.Text = "Query executed in: " + Math.Round(tsq.TotalMilliseconds, 0).ToString() + " ms";
                            Application.DoEvents();
                            var dataTable = new DataTable();
                            dataTable.Load(rd);
                            dataTable.Columns.Add("#", typeof(int)).SetOrdinal(0); // Neue Spalte "row_number" an erster Position
                            for (int i = 0; i < dataTable.Rows.Count; i++)
                            {
                                dataTable.Rows[i]["#"] = i + 1;
                            }
                            if (dgrid <= 1)
                            {
                                lbContent.Text = dgrid == 0 ? _selectedNode.Text : "Query Result";
                                if (query.ToLower().Contains("from product") || query.ToLower().Contains("from entity_metadata") || query.ToLower().Contains("from pct_technical_data.product"))
                                {
                                    dataTable.Columns.Add("DEL", typeof(bool)).SetOrdinal(0); // Neue Spalte "delete" an erster Position
                                    for (int i = 0; i < dataTable.Rows.Count; i++)
                                    {
                                        dataTable.Rows[i]["DEL"] = false;
                                    }
                                    dgv.DataSource = dataTable;
                                    dgv.ReadOnly = false; // erlaubt Editieren
                                                          // nur DEL editierbar lassen
                                    foreach (DataGridViewColumn c in dgv.Columns)
                                        c.ReadOnly = c.Name != "DEL";
                                    if (!dgv.Columns.Contains("DEL"))
                                    {
                                        dgv.Columns.Insert(1, new DataGridViewCheckBoxColumn()
                                        {
                                            Name = "DEL",
                                            HeaderText = "DEL",
                                            DataPropertyName = "DEL",
                                            TrueValue = true,
                                            FalseValue = false
                                        });
                                    }
                                    if (dgv.Columns["DEL"].Index != 1)
                                        dgv.Columns["DEL"].DisplayIndex = 1;
                                    tsmiSelectAll.Visible = true;
                                    tsmiDeselectAll.Visible = true;
                                    tsmiExportSqlDelete.Visible = true;
                                }
                                else
                                {
                                    if (dgv.Columns.Contains("DEL"))
                                        dgv.Columns.Remove("DEL");
                                    tsmiSelectAll.Visible = false;
                                    tsmiDeselectAll.Visible = false;
                                    tsmiExportSqlDelete.Visible = false;
                                    if (dataTable.Columns["constraint_type"] != null)
                                    {
                                        for (int i = dataTable.Rows.Count - 1; i >= 0; i--)
                                        {
                                            char ct = dataTable.Rows[i].Field<char>("constraint_type");
                                            if (ct != 'f')
                                                dataTable.Rows.RemoveAt(i);
                                        }
                                    }
                                    dgv.DataSource = dataTable;
                                    dgv.ReadOnly = true;
                                }
                                dgv.Columns["#"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dgv.Columns["#"].Frozen = true;
                                tsslSuccess.Text = "SUCCESS (" + dataTable.Rows.Count.ToString() + ")";
                                createClass(dataTable, query);
                            }
                            else if (dgrid == 2)
                            {
                                lbTableName.Text = _selectedNode.Text;
                                dgvs.DataSource = dataTable;
                                dgvs.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                createClass(dataTable);
                            }
                            else if (dgrid == 3)
                            {
                                tbCreate.Text = "";
                                string txt = dataTable.Rows[0][1].ToString();
                                FillTbCreate(txt);
                            }
                            else if (dgrid == 4)
                            {
                                // query erwartet Format mit Tabellenname in einfachen Anführungszeichen: ... WHERE table_name = 'MyTable';
                                string tableName = query.Replace(";", "").Split('\'')[1];
                                if (!tableNames.Contains(tableName))
                                    tableNames.Add(tableName);

                                // Sicherstellen, dass für die Tabelle eine Liste existiert
                                if (!TableColumnsByTable.ContainsKey(tableName))
                                    TableColumnsByTable[tableName] = new List<string>();

                                foreach (DataRow r in dataTable.Rows)
                                {
                                    string columnName = r["column_name"].ToString();
                                    if (!columnNames.Contains(columnName))
                                        columnNames.Add(columnName);

                                    string dataType = r["data_type"].ToString();
                                    dataType = translateDataType(dataType);

                                    string fullColumnKey = tableName + "." + columnName;
                                    if (!TableColumns.ContainsKey(fullColumnKey))
                                        TableColumns.Add(fullColumnKey, dataType);

                                    // Spalte zur tabellenspezifischen Liste hinzufügen (einmalig)
                                    if (!TableColumnsByTable[tableName].Contains(columnName))
                                        TableColumnsByTable[tableName].Add(columnName);
                                }
                            }
                            else if (dgrid == 5)
                            {
                                string txt = dataTable.Rows[0][1].ToString();
                                // FillTbCreate(txt);
                            }
                        }
                    }
                    else
                    {
                        cmd.ExecuteNonQuery();
                        tsslSuccess.Text = "SUCCESS";
                    }
                    if (!string.IsNullOrEmpty(query2))
                    {
                        cmd.CommandText = "set schema '" + Global.Global.schema + "'; " + query2;
                        using (NpgsqlDataReader rd = cmd.ExecuteReader())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(rd);
                            dgvsc.DataSource = dataTable;
                            dgvsc.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgvsc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            if (dgvs.Rows.Count > 0)
                            {
                                int dgvsHeight = dgvs.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + dgvs.ColumnHeadersHeight + 2
                                     + dgvs.Rows.GetRowsHeight(DataGridViewElementStates.Visible) / dgvs.Rows.Count;
                                int dgvscHeight = dgvsc.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + dgvs.ColumnHeadersHeight + 2
                                     + dgvsc.Rows.GetRowsHeight(DataGridViewElementStates.Visible) / dgvsc.Rows.Count;
                                splitContainerSchema1.SplitterDistance = dgvsHeight;
                                splitContainerSchema2.SplitterDistance = dgvscHeight;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(query3))
                    {
                        cmd.CommandText = "set schema '" + Global.Global.schema + "'; " + query3;
                        using (NpgsqlDataReader rd = cmd.ExecuteReader())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(rd);
                            dgvidx.DataSource = dataTable;
                            dgvidx.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgvidx.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            if (dgvs.Rows.Count > 0)
                            {
                                int dgvsHeight = dgvs.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + dgvs.ColumnHeadersHeight + 2
                                     + dgvs.Rows.GetRowsHeight(DataGridViewElementStates.Visible) / dgvs.Rows.Count;
                                int dgvscHeight = dgvsc.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + dgvs.ColumnHeadersHeight + 2
                                     + dgvsc.Rows.GetRowsHeight(DataGridViewElementStates.Visible) / dgvsc.Rows.Count;
                                splitContainerSchema1.SplitterDistance = dgvsHeight;
                                splitContainerSchema2.SplitterDistance = dgvscHeight;
                            }
                        }
                    }
                }
                TimeSpan ts = DateTime.Now - dt;
                lbSQL.Text += " Response: " + Math.Round(ts.TotalMilliseconds, 0).ToString() + " ms";
            }
            catch (Exception ex)
            {
                lbError.Text = ex.Message;
            }
        }

        private void createClass(DataTable dt, string query = "")
        {
            try
            {
                if (String.IsNullOrEmpty(query))
                {
                    tbClassField.Clear();
                    tbClassField.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                    string className = _selectedNode.Text;
                    tbClassField.AppendTextColor("public class ", Color.Blue);
                    tbClassField.AppendTextColor(className, Color.Gray);
                    tbClassField.AppendTextColor("\n{", Color.Black);
                    string classDef = $"public class {className}\n{{";
                    foreach (DataRow row in dt.Rows)
                    {
                        string columnName = row["column_name"].ToString();
                        string dataType = row["data_type"].ToString();
                        dataType = translateDataType(dataType);
                        Color col = getTypeColor(dataType);
                        tbClassField.AppendTextColor("\n    public ", Color.Blue);
                        tbClassField.AppendTextColor(dataType, col);
                        tbClassField.AppendTextColor(" " + columnName, Color.Black);
                        tbClassField.AppendTextColor(" { ", Color.Black);
                        tbClassField.AppendTextColor("get", Color.Blue);
                        tbClassField.AppendTextColor("; ", Color.Black);
                        tbClassField.AppendTextColor("set", Color.Blue);
                        tbClassField.AppendTextColor("; }", Color.Black);
                    }
                    tbClassField.AppendTextColor("\n}", Color.Black);
                    tbClassField.SelectionStart = 0;
                    tbClassField.SelectionLength = 0;
                }
                else
                {
                    if (!query.Trim().StartsWith("select", StringComparison.OrdinalIgnoreCase))
                    {
                        tbClassField.Clear();
                        return;
                    }
                    tbClassField.Clear();
                    tbClassField.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                    // Ersetze die fehlerhafte Zeile (und die ähnliche für columns) durch Tokenisierung:
                    List<string> tokenscol = query.extractColumns();
                    var tokenstab = query.RegexSplit(new[] { "select", "from" }).LastOrDefault();
                    var tabtokens = tokenstab.Split(new[] { ' ', '\r', '\n', '\t', ',', ';', '(', ')', '.', '=' }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> tables = tabtokens
                        .Select(tok => tok.ToString())
                        .Where(tokStr => tableNames.Any(tn => string.Equals(tn, tokStr, StringComparison.OrdinalIgnoreCase)))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList();
                    List<string> columnsStar = new List<string>();
                    if (tokenscol.Contains("*"))
                    {
                        tokenscol.Remove("*");
                        foreach (string table in tables)
                        {
                            var tn = TableColumnsByTable.Where(k => k.Key.Equals(table)).FirstOrDefault();
                            foreach (string s in tn.Value)
                            {
                                columnsStar.Add(table + "." + s);
                            }
                            tokenscol.Remove(table + ".*");
                        }
                    }
                    foreach (string table in tables)
                    {
                        if (query.Contains(table + ".*"))
                        {
                            var tn = TableColumnsByTable.Where(k => k.Key.Equals(table)).FirstOrDefault();
                            foreach (string s in tn.Value)
                            {
                                columnsStar.Add(table + "." + s);
                            }
                            tokenscol.Remove(table + ".*");
                        }
                    }
                    tokenscol.AddRange(columnsStar);
                    List<string> noTable = tokenscol.Where(x => !x.Contains('.')).ToList();
                    tokenscol = tokenscol.Except(noTable).ToList();
                    foreach (string table in tables)
                    {
                        foreach (string col in noTable)
                        {
                            if (TableColumns.ContainsKey(table + "." + col))
                                tokenscol.Add(table + "." + col);
                        }
                    }
                    tokenscol = tokenscol.Distinct().ToList();
                    tokenscol.Sort();
                    tbClassField.AppendTextColor("public class ", Color.Blue);
                    tbClassField.AppendTextColor("newClass", Color.Gray);
                    tbClassField.AppendTextColor("\n{", Color.Black);
                    foreach (string column in tokenscol)
                    {
                        //string columnName = column.Split('.').Last();
                        //string tableName = column.Split('.').First();
                        string dataType = TableColumns.ContainsKey(column) ? TableColumns[column] : null;
                        if (!String.IsNullOrEmpty(dataType))
                        {
                            Color col = getTypeColor(dataType);
                            tbClassField.AppendTextColor("\n    public ", Color.Blue);
                            tbClassField.AppendTextColor(dataType, col);
                            tbClassField.AppendTextColor(" " + column.Replace('.', '-'), Color.Black);
                            tbClassField.AppendTextColor(" { ", Color.Black);
                            tbClassField.AppendTextColor("get", Color.Blue);
                            tbClassField.AppendTextColor("; ", Color.Black);
                            tbClassField.AppendTextColor("set", Color.Blue);
                            tbClassField.AppendTextColor("; }", Color.Black);
                        }
                    }
                    tbClassField.AppendTextColor("\n}", Color.Black);
                    tbClassField.SelectionStart = 0;
                    tbClassField.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            {
                lbError.Text = "Error in create class: " + ex.Message.Replace("\r\n", " ");
            }
        }

        private string translateDataType(string tt)
        {
            switch (tt.Split(' ').FirstOrDefault())
            {
                case "text":
                    return "string";
                case "integer":
                    return "int";
                case "uuid":
                    return "Guid";
                case "numeric":
                    return "decimal";
                case "boolean":
                    return "bool";
                case "timestamp":
                    return "DateTime";
                default:
                    return tt;
            }
        }

        private Color getTypeColor(string tt)
        {
            switch (tt)
            {
                case "string": return Color.Blue;
                case "int": return Color.Blue;
                case "Guid": return Color.Gray;
                case "decimal": return Color.Blue;
                case "bool": return Color.Blue;
                case "DateTime": return Color.Gray;
                default: return Color.Black;
            }
        }
        private void FillTbCreate(string sql)
        {
            tbCreate.Clear();
            Font boldFont = new Font(tbCreate.Font, FontStyle.Bold);
            List<string> lines = sql.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (string line in lines)
            {
                if (line.Contains("CREATE") || line.Contains('(') || line.Contains(')'))
                {
                    tbCreate.SelectionFont = boldFont;
                    tbCreate.AppendTextColor(line + "\n", Color.Black);
                    tbCreate.SelectionFont = tbCreate.Font;
                }
                else
                {
                    List<string> sub = line.Trim().Split(' ').ToList();
                    tbCreate.AppendText("    ");
                    tbCreate.SelectionFont = boldFont;
                    tbCreate.AppendTextColor(sub[0] + " ", Color.Black);
                    tbCreate.SelectionFont = tbCreate.Font;
                    sub.RemoveAt(0);
                    tbCreate.AppendTextColor(String.Join(" ", sub) + "\n", Color.Red);
                }
            }
            // 2) Schlüsselwörter blau färben (überschreibt ggf. Grau)
            foreach (string key in new[] { "CREATE TABLE", "NOT", "NULL" })
            {
                int idx = 0;
                while ((idx = tbCreate.Text.IndexOf(key, idx, StringComparison.OrdinalIgnoreCase)) >= 0)
                {
                    tbCreate.SelectionFont = tbCreate.Font;
                    tbCreate.Select(idx, key.Length);
                    tbCreate.SelectionColor = Color.Blue;
                    idx += key.Length;
                }
            }

            // Cursor zurücksetzen
            tbCreate.SelectionStart = 0;
            tbCreate.SelectionLength = 0;
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            tbQuery.Clear();
        }

        private void tsbSQL_CheckedChanged(object sender, EventArgs e)
        {
            if (tsbSQL.Checked)
            {
                splitContainer2.Panel1Collapsed = false;
                tsbExecute.Enabled = true;
                tsbClear.Enabled = true;
                tsbLoadSQL.Enabled = true;
                tsbSaveSQL.Enabled = true;
                tsmiLoadSQLQuery.Enabled = true;
                tsmiSaveSQLQuery.Enabled = true;
                lInfoSchema.Visible = true;
            }
            else
            {
                splitContainer2.Panel1Collapsed = true;
                tsbExecute.Enabled = false;
                tsbClear.Enabled = false;
                tsbLoadSQL.Enabled = false;
                tsbSaveSQL.Enabled = false;
                tsmiLoadSQLQuery.Enabled = false;
                tsmiSaveSQLQuery.Enabled = false;
                lInfoSchema.Visible = false;
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0) return;
            _selectedNode = e.Node;
            string sql = "SELECT * FROM " + Global.Global.schema + "." + e.Node.Text;
            ExecuteSQL(0, sql);
            sql = " select column_name, data_type, character_maximum_length from INFORMATION_SCHEMA.COLUMNS where table_name ='" + e.Node.Text + "';";
            string sqlsc = Global.Global.getConstraints.Replace("{{table}}", e.Node.Text);
            string sqlidx = Global.Global.getIndexes.Replace("{{table}}", e.Node.Text);
            ExecuteSQL(2, sql, sqlsc, sqlidx);
            sql = "SELECT\r\n'CREATE TABLE ' || relname || E'\\n(\\n' ||\r\n  array_to_string(\r\n    array_agg(\r\n      '    ' || column_name || ' ' ||" +
                  "  type || ' '|| not_null\r\n    )\r\n    , E',\\n'\r\n  ) || E'\\n);\\n'\r\nfrom\r\n(\r\n  SELECT \r\n    c.relname, a.attname AS column_name,\r\n" +
                  "    pg_catalog.format_type(a.atttypid, a.atttypmod) as type,\r\n    case \r\n      when a.attnotnull\r\n    then 'NOT NULL' \r\n    else 'NULL' \r\n" +
                  "    END as not_null \r\n  FROM pg_class c,\r\n   pg_attribute a,\r\n   pg_type t\r\n   WHERE c.relname = '" + e.Node.Text + "'\r\n" +
                  "   AND a.attnum > 0\r\n   AND a.attrelid = c.oid\r\n   AND a.atttypid = t.oid\r\n ORDER BY a.attnum\r\n) as tabledefinition\r\ngroup by relname;";
            ExecuteSQL(3, sql, sqlsc);
            createInsert(e);
        }

        private void createInsert(TreeViewEventArgs e)
        {
            string insertTemplateSql = @"
SELECT
  'INSERT INTO ' || quote_ident(n.nspname) || '.' || quote_ident(c.relname) || ' (' ||
  string_agg(quote_ident(a.attname), ', ' ORDER BY a.attnum) || ') VALUES (' ||
  string_agg('<' || a.attname || '_value>', ', ' ORDER BY a.attnum) || ');' AS insert_template
FROM pg_class c
JOIN pg_namespace n ON n.oid = c.relnamespace
JOIN pg_attribute a ON a.attrelid = c.oid
WHERE n.nspname = @schema
  AND c.relname = @table
  AND a.attnum > 0
  AND NOT a.attisdropped
GROUP BY n.nspname, c.relname;
";

            using (var cmd = new NpgsqlCommand(insertTemplateSql, npgsql))
            {
                cmd.Parameters.AddWithValue("schema", Global.Global.schema);
                cmd.Parameters.AddWithValue("table", e.Node.Text);
                using (var reader = cmd.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    // Zeige Ergebnis z.B. in tbInsert oder setze dgrid==5-Handling so, dass es angezeigt wird
                    if (dt.Rows.Count > 0)
                        tbInsert.Text = dt.Rows[0]["insert_template"].ToString();
                    else
                        tbInsert.Text = $"No insert template for {Global.Global.schema}.{e.Node.Text}";
                }
            }
            string updateTemplateSql = @"
SELECT
  'UPDATE ' || quote_ident(n.nspname) || '.' || quote_ident(c.relname) || ' SET ' ||
  string_agg(quote_ident(a.attname) || ' = <value_for_'|| quote_ident(a.attname) || '>', ', ' ORDER BY a.attnum) ||
  ' WHERE ' || COALESCE(quote_ident(pk.attname), '<no_pk>') || ' = <pk_value>;' AS update_template
FROM pg_class c
JOIN pg_namespace n ON n.oid = c.relnamespace
JOIN pg_attribute a ON a.attrelid = c.oid
LEFT JOIN (
  SELECT conrelid, conkey[1] AS pk_attnum
  FROM pg_constraint
  WHERE contype = 'p'
) pc ON pc.conrelid = c.oid
LEFT JOIN pg_attribute pk ON pk.attrelid = pc.conrelid AND pk.attnum = pc.pk_attnum
WHERE n.nspname = @schema
  AND c.relname = @table
  AND a.attnum > 0
  AND NOT a.attisdropped
GROUP BY n.nspname, c.relname, pk.attname;
";

            using (var cmd = new NpgsqlCommand(updateTemplateSql, npgsql))
            {
                cmd.Parameters.AddWithValue("schema", Global.Global.schema ?? "public");
                cmd.Parameters.AddWithValue("table", e.Node.Text);
                using (var reader = cmd.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    if (dt.Rows.Count > 0)
                        tbInsert.Text += "\n\n\n" + dt.Rows[0]["update_template"].ToString();
                    else
                        tbInsert.Text += $"\n\n\nNo update template for {Global.Global.schema}.{e.Node.Text}";
                }
            }
        }

        private void RestoreWindowState()
        {
            try
            {
                if (!File.Exists(_windowStateFile)) return;
                WindowStateInfo info;
                var serializer = new XmlSerializer(typeof(WindowStateInfo));
                using (var fs = File.OpenRead(_windowStateFile))
                {
                    info = (WindowStateInfo)serializer.Deserialize(fs);
                }

                if (info == null) return;

                var restoredBounds = new Rectangle(info.X, info.Y, Math.Max(100, info.Width), Math.Max(100, info.Height));

                // Prüfen, ob die gespeicherten Bounds auf irgendeinem Bildschirm sichtbar sind
                bool visibleOnAny = Screen.AllScreens.Any(s => s.WorkingArea.IntersectsWith(restoredBounds));
                if (!visibleOnAny)
                {
                    // setze auf Zentriert auf Primärbildschirm
                    var primary = Screen.PrimaryScreen.WorkingArea;
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                else
                {
                    this.StartPosition = FormStartPosition.Manual;
                    this.Bounds = restoredBounds;
                }

                // WindowState nachsetzen (maximized/minimized/normal). Setze nach Bounds.
                this.WindowState = info.WindowState;
                this.BringToFront();
                Application.DoEvents();
            }
            catch
            {
                // bei Fehlern: nicht blockieren, einfach Standardwerte verwenden
            }
        }

        private void SaveWindowState()
        {
            try
            {
                // Bestimme die tatsächlich sichtbaren Bounds (wenn maximiert -> RestoreBounds verwenden)
                Rectangle bounds;
                if (this.WindowState == FormWindowState.Normal)
                    bounds = this.Bounds;
                else
                    bounds = this.RestoreBounds; // zuletzt normale Größe

                var info = new WindowStateInfo
                {
                    X = bounds.X,
                    Y = bounds.Y,
                    Width = bounds.Width,
                    Height = bounds.Height,
                    WindowState = this.WindowState
                };

                var dir = Path.GetDirectoryName(_windowStateFile);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                var serializer = new XmlSerializer(typeof(WindowStateInfo));
                using (var fs = File.Create(_windowStateFile))
                {
                    serializer.Serialize(fs, info);
                }
            }
            catch
            {
                // stillschweigend ignorieren, keine Anwendung stoppen
            }
        }

        private void tsmiLoadSQLQuery_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Load SQL Query";
            ofd.Filter = "SQL Files (*.sql)|*.sql|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string sql = File.ReadAllText(ofd.FileName);
                    tbQuery.Text = sql;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tsmiSaveSQLQuery_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save SQL Query";
            sfd.Filter = "SQL Files (*.sql)|*.sql|All Files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(sfd.FileName, tbQuery.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                try
                {
                    if (dgv.SelectedCells.Count == 0) return;

                    // Verwende die eingebaute Clipboard-Unterstützung des DataGridView
                    var data = dgv.GetClipboardContent();
                    if (data != null)
                    {
                        Clipboard.SetDataObject(data);
                        tsslSuccess.Text = "COPIED";
                    }
                    else
                    {
                        // Fallback: eigenes Tab-separated-Format erzeugen
                        CopySelectedCellsManualToClipboard();
                        tsslSuccess.Text = "COPIED (manual)";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Copy error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                e.Handled = true;
            }
        }

        private void CopySelectedCellsManualToClipboard()
        {
            // Gruppen nach Zeile sortieren und Tab-getrennt kopieren
            var cells = dgv.SelectedCells.Cast<DataGridViewCell>().OrderBy(c => c.RowIndex).ThenBy(c => c.ColumnIndex).ToList();
            if (!cells.Any()) return;

            int currentRow = cells.First().RowIndex;
            var sb = new System.Text.StringBuilder();

            // Optional: Header
            var cols = dgv.SelectedCells.Cast<DataGridViewCell>().Select(c => c.OwningColumn.Index).Distinct().OrderBy(i => i).ToList();
            sb.AppendLine(string.Join("\t", cols.Select(i => dgv.Columns[i].HeaderText)));

            foreach (var g in cells.GroupBy(c => c.RowIndex).OrderBy(g => g.Key))
            {
                var rowValues = new List<string>();
                foreach (int colIndex in cols)
                {
                    var cell = g.FirstOrDefault(c => c.ColumnIndex == colIndex);
                    rowValues.Add(cell?.FormattedValue?.ToString() ?? string.Empty);
                }
                sb.AppendLine(string.Join("\t", rowValues));
            }

            Clipboard.SetText(sb.ToString());
        }

        private void tsmiEditBMST_Click(object sender, EventArgs e)
        {
            editBMST_rule.Execute(this, npgsql);
        }

        //private bool getAllSelectedCells()
        //{
        //    var cells = dgv.SelectedCells.Cast<DataGridViewCell>()
        //        .OrderBy(c => c.RowIndex)
        //        .ThenBy(c => c.ColumnIndex)
        //        .ToList();
        //    if (cells.Count == 0) return false;

        //    var rows = cells.GroupBy(c => c.RowIndex).OrderBy(g => g.Key);

        //    var sb = new System.Text.StringBuilder();
        //    foreach (var grp in rows)
        //    {
        //        var values = grp
        //            .OrderBy(c => c.ColumnIndex)
        //            .Select(c => (c.Value?.ToString() ?? string.Empty)
        //                .Replace("\r", " ").Replace("\n", " ").Replace("\t", " "));
        //        sb.AppendLine(string.Join("\t", values));
        //    }

        //    try
        //    {
        //        Clipboard.SetText(sb.ToString());
        //    }
        //    catch (System.Runtime.InteropServices.ExternalException)
        //    {
        //        // Clipboard in use - optional: show message or retry
        //    }

        //    return true;
        //}

        private bool getAllSelectedCells()
        {
            // Verwende das aktuell sichtbare DataGridView (dgv oder dgvs)
            DataGridView dgv1 = tabControl1.SelectedIndex == 0 ? dgv : dgvs;

            var cells = dgv1.SelectedCells.Cast<DataGridViewCell>()
                .OrderBy(c => c.RowIndex)
                .ThenBy(c => c.ColumnIndex)
                .ToList();
            if (cells.Count == 0) return false;

            // Spaltenreihenfolge der Auswahl (distinct, sortiert)
            var cols = cells.Select(c => c.OwningColumn.Index).Distinct().OrderBy(i => i).ToList();

            var sb = new System.Text.StringBuilder();

            // Header (optional). Wenn nicht gewünscht: diese Zeile entfernen.
            sb.Append(string.Join("\t", cols.Select(i => dgv1.Columns[i].HeaderText)));
            sb.Append("\r\n");

            // Zeilen gruppiert nach RowIndex
            foreach (var grp in cells.GroupBy(c => c.RowIndex).OrderBy(g => g.Key))
            {
                var rowValues = cols.Select(colIndex =>
                {
                    var cell = grp.FirstOrDefault(c => c.ColumnIndex == colIndex);
                    // Zeilenumbrüche/Tabs in Zellen ersetzen, Nulls zu leerer Zeichenkette
                    return (cell?.Value?.ToString() ?? string.Empty)
                        .Replace("\r", " ")
                        .Replace("\n", " ")
                        .Replace("\t", " ");
                });
                sb.Append(string.Join("\t", rowValues));
                sb.Append("\t"); // expliziter TAB als Separator
            }

            try
            {
                Clipboard.SetText(sb.ToString());
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                // Clipboard in use - optional: Fehlerbehandlung/Retry implementieren
                return false;
            }

            return true;
        }

        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            if (!getAllSelectedCells()) return;
        }

        private void cms_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Position der Maus in client-Koordinaten des dgv
            DataGridView dgv1 = tabControl1.SelectedIndex == 0 ? dgv : dgvs;
            var clientPos = dgv1.PointToClient(Cursor.Position);
            var hit = dgv1.HitTest(clientPos.X, clientPos.Y);

            if (hit.Type == DataGridViewHitTestType.Cell)
            {
                _ctxRow = hit.RowIndex;
                _ctxCol = hit.ColumnIndex;

                // Beispiel: Zugriff auf die Zelle
                var cell = dgv[_ctxCol, _ctxRow];
                // z.B. Menüitems aktivieren/deaktivieren
                // toolStripMenuItemEdit.Enabled = (_ctxRow >= 0);
            }
            else
            {
                // kein Zell-Klick ? Kontextmenü nicht öffnen
                e.Cancel = true;
                _ctxRow = _ctxCol = -1;
            }
        }

        private void dgvs_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!tsbSQL.Checked) return;
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                var cell = dgvs[e.ColumnIndex, e.RowIndex];
                if (cell != null && cell.Value != null)
                {
                    string value = cell.Value.ToString();
                    tbQuery.Text += value;
                    tbQuery.Focus();
                    tbQuery.SelectionStart = tbQuery.Text.Length;
                }
            }
        }

        private void treeView_DoubleClick(object sender, EventArgs e)
        {
            if (!tsbSQL.Checked) return;
            treeView.SelectedNode = treeView.GetNodeAt(treeView.PointToClient(Cursor.Position));
            if (treeView.SelectedNode != null && treeView.SelectedNode.Level == 1)
            {
                tbQuery.Text += treeView.SelectedNode.Text;
                tbQuery.Focus();
                tbQuery.SelectionStart = tbQuery.Text.Length;
            }
        }

        private void TakeIt_MouseHover(object sender, EventArgs e)
        {
        }

        private void TakeIt_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbClassField.Text);
        }

        private void TakeIt_MouseEnter(object sender, EventArgs e)
        {
            TakeIt.BackColor = Color.LightGray;
        }

        private void TakeIt_MouseLeave(object sender, EventArgs e)
        {
            TakeIt.BackColor = Color.Transparent;
        }

        private void lbTableName_DoubleClick(object sender, EventArgs e)
        {
            if (tsbSQL.Checked)
            {
                tbQuery.Text += lbTableName.Text;
                tbQuery.Focus();
                tbQuery.SelectionStart = tbQuery.Text.Length;
            }
        }

        private void tbQuery_KeyDown(object sender, KeyEventArgs e)
        {
            string txt = tbQuery.Text;
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                BtnExecute_Click(sender, e);
                e.Handled = true;
            }
            int charpos = tbQuery.SelectionStart;
            tslInfo.Text = $"Cursor at: {charpos}";
            Application.DoEvents();
        }

        private void tbQuery_TextChanged(object sender, EventArgs e)
        {
            HighlightSqlWordsInTbQuery();
        }

        private void HighlightSqlWordsInTbQuery()
        {
            var keywords = standardLists.SQLStatements;
            if (keywords == null || !keywords.Any() || string.IsNullOrEmpty(tbQuery.Text)) return;

            // Auswahl sichern
            int selStart = tbQuery.SelectionStart;
            int selLength = tbQuery.SelectionLength;

            // Text komplett auf Default zurücksetzen (zuerst Redraw aus)
            SendMessage(tbQuery.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
            try
            {
                tbQuery.SelectAll();
                tbQuery.SelectionColor = Color.Black;

                // Einfache Normalisierung: eindeutige Keywords, leere Einträge überspringen
                var distinctKeys = keywords
                    .Where(k => !string.IsNullOrWhiteSpace(k))
                    .Select(k => k.Trim())
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                string text = tbQuery.Text;

                foreach (string key in distinctKeys)
                {
                    // whole-word Match; Escape für Sonderzeichen
                    string pattern = $@"\b{Regex.Escape(key)}\b";
                    foreach (Match m in Regex.Matches(text, pattern, RegexOptions.IgnoreCase))
                    {
                        tbQuery.Select(m.Index, m.Length);
                        tbQuery.SelectionColor = Color.Blue;
                    }
                }
            }
            finally
            {
                // Auswahl wiederherstellen und Redraw aktivieren
                tbQuery.Select(selStart, selLength);
                SendMessage(tbQuery.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
                tbQuery.Invalidate();
            }
        }

        private void tsmiEditSTFT_Click(object sender, EventArgs e)
        {
            editSTFT_rule.Execute(this, npgsql);
        }

        private void editBMSTFTMT_Click(object sender, EventArgs e)
        {
            editBMSTFTMT_rule.Execute(this, npgsql);
        }

        private void editBMSTFT_Click(object sender, EventArgs e)
        {
            editBMSTFT_rule.Execute(this, npgsql);
        }

        private void editSTFTMTModule_Click(object sender, EventArgs e)
        {
            editModule_rule.Execute(this, npgsql);
        }

        private void tsmiFindIdx_Click(object sender, EventArgs e)
        {
            if (!getAllSelectedCells()) return;

            guidSearchResults.Clear();
            string gui = null;
            string col = null;
            if (_ctxRow >= 0 && _ctxCol >= 0)
            {
                DataGridView dgv1 = tabControl1.SelectedIndex == 0 ? dgv : dgvs;
                var cell = dgv1[_ctxCol, _ctxRow];
                if (cell != null && cell.Value != null)
                {
                    gui = cell.Value.ToString();
                    col = cell.OwningColumn.Name;
                }
            }

            List<string> found = new List<string>();
            foreach (string table in tableNames)
            {
                if (!_selectedNode.Text.Equals(table))
                {
                    string query = "SELECT * FROM " + table + ";";
                    using (NpgsqlCommand command = new NpgsqlCommand("set schema '" + Global.Global.schema + "'; " + query, npgsql))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            List<int> idIndices = DbUtils.GetIdColumnIndices(reader);
                            int line = 1;
                            while (reader.Read())
                            {
                                foreach (int idx in idIndices)
                                {
                                    string columnName = reader.GetName(idx);
                                    object value = reader.IsDBNull(idx) ? null : reader.GetValue(idx);
                                    if (value != null)
                                    {
                                        if (found.Count == 0)
                                            lbGuidSearch.Text = "Searching in table " + _selectedNode.Text + "." + col + ": " + gui;
                                        if (gui.Equals(value.ToString()))
                                        {
                                            guidSearchResults.Add(new GuidSearchResult
                                            {
                                                Line = line,
                                                TableName = table,
                                                ColumnName = columnName,
                                                Value = value
                                            });
                                        }
                                    }
                                }
                                line++;
                            }
                        }
                    }
                }
            }
            if (guidSearchResults.Count > 0)
            {
                dgvGuidSearch.RowHeadersVisible = false;
                dgvGuidSearch.DataSource = null;
                dgvGuidSearch.DataSource = guidSearchResults;
                dgvGuidSearch.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvGuidSearch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                //dgvGuidSearch.Width = (from DataGridViewColumn column in dgvGuidSearch.Columns select column.Width).Sum() + 23;
                //dgvGuidSearch.Height = (dgvGuidSearch.Rows.Count + 1) * dgvGuidSearch.Rows[0].Height;
                //dgvGuidSearch.ScrollBars = ScrollBars.Vertical;
            }
            else
                dgvGuidSearch.DataSource = null;
        }

        private void editFRSL_Click(object sender, EventArgs e)
        {
            editSleeve_rule.Execute(this, npgsql);
        }

        private void editSTFTMTWE_Click(object sender, EventArgs e)
        {
            editWedge_rule.Execute(this, npgsql);
        }

        private void InitializeExportWorker()
        {
            _exportWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _exportWorker.DoWork += ExportWorker_DoWork;
            _exportWorker.ProgressChanged += ExportWorker_ProgressChanged;
            _exportWorker.RunWorkerCompleted += ExportWorker_RunWorkerCompleted;
        }


        private void tsbExportExcel_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog { Title = "Export to Excel", Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*" })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;

                // Snapshot auf UI-Thread erstellen
                var snapshot = ExportHelpers.CreateSnapshot(dgv);

                // Starten (übergibt Snapshot als Argument)
                _exportWorker.RunWorkerAsync(new Tuple<ExportHelpers.DataGridViewExportSnapshot, string>(snapshot, sfd.FileName));

                // Optional: UI sperren, ProgressBar sichtbar etc.
            }
        }

        private void ExportWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var tuple = (Tuple<ExportHelpers.DataGridViewExportSnapshot, string>)e.Argument;
            var snapshot = tuple.Item1;
            var filePath = tuple.Item2;
            var worker = (BackgroundWorker)sender;

            try
            {
                ExportHelpers.ExportSnapshotToExcel(snapshot, filePath, worker);
            }
            catch (OperationCanceledException)
            {
                e.Cancel = true;
            }
        }

        private void ExportWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // z.B. progressBar.Value = e.ProgressPercentage;
            // labelStatus.Text = $"Exportierte Zeilen: {e.UserState}";
        }

        private void ExportWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Export abgebrochen.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Fehler beim Export: " + e.Error.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Export abgeschlossen.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // UI wieder freigeben
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            // defensiv: keine Annahmen über _selectedNode oder Text
            string actNode = _selectedNode?.Text;
            treeView.BeginUpdate();
            try
            {
                treeView.Nodes.Clear();
                LoadDB();
                if (!string.IsNullOrEmpty(actNode))
                {
                    // sichere, statische Equals-Verwendung (vermeidet instance-delegate-Probleme)
                    var node = treeView.Nodes
                        .Cast<TreeNode>()
                        .SelectMany(n => n.Nodes.Cast<TreeNode>().Prepend(n)) // falls verschachtelt
                        .FirstOrDefault(n => string.Equals(n.Text, actNode, StringComparison.Ordinal));
                    if (node != null) treeView.SelectedNode = node;
                }
            }
            finally
            {
                treeView.EndUpdate();
            }
        }

        private void tsmiSelectAll_Click(object sender, EventArgs e)
        {
            // DataTable (ggf. über BindingSource) bevorzugt ändern
            string lb = lbContent.Text;
            var bs = dgv.DataSource as BindingSource;
            var dt = (bs != null ? bs.DataSource as DataTable : dgv.DataSource as DataTable);
            if (dt != null)
            {
                bool hadRaiseEvents = false;
                if (bs != null)
                {
                    hadRaiseEvents = bs.RaiseListChangedEvents;
                    bs.RaiseListChangedEvents = false;
                }

                try
                {
                    // Bulk-Update ohne Events
                    foreach (DataRow row in dt.Rows)
                    {
                        row["DEL"] = true;
                        lbContent.Text = lb + ": " + $"Marking rows... {dt.Rows.IndexOf(row) + 1}/{dt.Rows.Count}";
                        Application.DoEvents();
                    }
                }
                finally
                {
                    if (bs != null)
                    {
                        bs.RaiseListChangedEvents = hadRaiseEvents;
                        bs.ResetBindings(false);
                    }
                }
                return;
            }

            // Fallback für ungebundene grid: Redraw unterdrücken
            SendMessage(dgv.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
            dgv.SuspendLayout();
            try
            {
                foreach (DataGridViewRow r in dgv.Rows)
                    r.Cells["DEL"].Value = true;
            }
            finally
            {
                dgv.ResumeLayout();
                SendMessage(dgv.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
                dgv.Invalidate();
            }
        }

        private void tsmiDeselectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dgv.Rows)
            {
                r.Cells["DEL"].Value = false;
            }
        }

        private void tsmiExportSqlDelete_Click(object sender, EventArgs e)
        {
            List<string> deleteStatements = new List<string>();
            foreach (DataGridViewRow r in dgv.Rows)
            {
                if (r.Cells["DEL"].Value is bool del && del)
                {
                    string id = r.Cells["ID"].Value.ToString();
                    string sql = $"DELETE FROM product WHERE id = '{id}';";
                    deleteStatements.Add(sql);
                    // Optional: SQL in TextBox einfügen oder direkt ausführen
                }
            }
            if (deleteStatements.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Title = "Export SQL Delete Statements",
                    Filter = "SQL Files (*.sql)|*.sql|All Files (*.*)|*.*"
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllLines(sfd.FileName, deleteStatements);
                }
            }
        }

        private void tsbForeignKeys_Click(object sender, EventArgs e)
        {
            List<string> tl = new List<string>();
            foreach (string t in tableNames)
                tl.Add("rel.relname = '" + t + "'");
            string tn = String.Join(" OR ", tl);
            string sql = "set schema '" + Global.Global.schema + "'; " + Global.Global.allForeignKeys2.Replace("{{alltables}}", tn);
            ExecuteSQL(1, sql);
        }

        private void tsbCreateFKeys_Click(object sender, EventArgs e)
        {
            List<string> tl = new List<string>();
            foreach (string t in tableNames)
                tl.Add("rel.relname = '" + t + "'");
            string tn = String.Join(" OR ", tl);
            string sql = "set schema '" + Global.Global.schema + "'; " + Global.Global.allForeignKeys2.Replace("{{alltables}}", tn);
            ExecuteSQL(1, sql);
            List<string> ls = new List<string>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                string table = row.Cells["table_name"].Value as string;
                string fkey = row.Cells["constraint_name"].Value as string;
                string reftable = row.Cells["referenced_table"].Value as string;
                string column = row.Cells["columns"].Value as string;
                string refcol = row.Cells["referenced_columns"].Value as string;
                string csql = Global.Global.alterConstraints.Replace("{{table}}", table);
                csql = csql.Replace("{{fkey}}", fkey);
                csql = csql.Replace("{{column}}", column);
                csql = csql.Replace("{{referenced_table}}", reftable);
                csql = csql.Replace("{{referenced_column}}", refcol);
                ls.Add(csql);
            }
            if (ls.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Title = "Export ALTER TABLE Statements",
                    Filter = "SQL Files (*.sql)|*.sql|All Files (*.*)|*.*"
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllLines(sfd.FileName, ls);
                }
            }
        }

        private void editSLST_Click(object sender, EventArgs e)
        {
            editSleeveSticker_rule.Execute(this, npgsql);
        }

        private void editFRST_Click(object sender, EventArgs e)
        {
            editFrameSticker_rule.Execute(this, npgsql);
        }

        private void editEntityMetadata_Click(object sender, EventArgs e)
        {
            editEntityMetaData.Execute(this, npgsql);
        }

        private void tbQuery_MouseDown(object sender, MouseEventArgs e)
        {
            int charpos = tbQuery.GetCharIndexFromPosition(e.Location);
            tslInfo.Text = $"Cursor at: {charpos}";
            Application.DoEvents();
        }

        private void getModuleFromID_Click(object sender, EventArgs e)
        {
            getModuleFromId.Execute(this);
        }

        private void tsmiGetFrameFromID_Click(object sender, EventArgs e)
        {
            getFrameFromId.Execute(this);
        }

        private void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            tsbExportExcel.Enabled = dgv.Rows.Count > 0;
        }

        private void tsmiShowTableColumnTree_Click(object sender, EventArgs e)
        {
            showTabelColumnTree.Execute(this, TableColumnsByTable);
        }

        private void tsbShowDataTree_Click(object sender, EventArgs e)
        {
            showTabelColumnTree.Execute(this, TableColumnsByTable);
        }

        private void editFrameSleeveRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFrameSleeveRule.Execute(this, npgsql);
        }

        private void editWedgeOptionRules_Click(object sender, EventArgs e)
        {
            editWedgeOptionRule.Execute(this, npgsql);
        }

        private void dgvGuidSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow r = dgvGuidSearch.Rows[e.RowIndex];
                GuidSearchResult result = r.DataBoundItem as GuidSearchResult;
                if (result != null)
                {
                    string sql = $"SELECT * FROM {result.TableName} WHERE {result.ColumnName} = '{result.Value}';";
                    DataTable dt = DbUtils.GetResultFromTable(npgsql, sql);
                    if (dt != null)
                    {
                        MessageBox.Show($"Result from {result.TableName}: {dt.Rows.Count} rows returned.", $"Result from {result.TableName}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void tsmiSetAvailableSelectable_Click(object sender, EventArgs e)
        {
            setAvailableSelectable.Execute(this, npgsql);
        }
    }


    [Serializable]
    public class WindowStateInfo
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public FormWindowState WindowState;
    }

    public class GuidSearchResult
    {
        public int Line { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public object Value { get; set; }
    }
}