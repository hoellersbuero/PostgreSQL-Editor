using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PostgreSQL_Editor.Global;

namespace PostgreSQL_Editor.FunctionViews
{
    public partial class UpdateDgvRow : Form
    {
        private string tableName;
        private DataGridViewRow selectedRow;
        private List<dgvRow> dgvRows = new List<dgvRow>();

        public UpdateDgvRow()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
            // Event-Handler für Validierung und DataErrors anmelden
            dgv.CellValidating += dgv_CellValidating;
            dgv.DataError += dgv_DataError;
            dgv.CellParsing += dgv_CellParsing;
            // Neu: zuverlässigere Validierung nach Ende der Bearbeitung
            dgv.CellEndEdit += dgv_CellEndEdit;
            // Für bestimmte Zelltypen (Checkboxen) Commit erzwingen
            dgv.CurrentCellDirtyStateChanged += dgv_CurrentCellDirtyStateChanged;
            btnClear.Enabled = Global.Global.SQLCommands.Count > 0;
        }

        public static UpdateDgvRow Execute(Form owner, string tableName, DataGridViewRow selectedRow)
        {
            UpdateDgvRow form = new UpdateDgvRow();
            form.Owner = owner;
            form.tableName = tableName;
            form.selectedRow = selectedRow;
            form.ShowDialog(owner);
            return form;
        }

        private void UpdateDgvRow_Load(object sender, EventArgs e)
        {
            lTable.Text = tableName;
            lId.Text = selectedRow.Cells["id"].Value.ToString();
            dgvRows.Clear();
            foreach (DataGridViewCell cell in selectedRow.Cells)
            {
                if (cell.OwningColumn.Name != "id" && cell.OwningColumn.Name != "#")
                {
                    dgvRow row = new dgvRow
                    {
                        column = cell.OwningColumn.Name,
                        type = cell.ValueType.Name.ToString(),
                        oldValue = cell.Value?.ToString() ?? string.Empty,
                        newValue = string.Empty
                    };
                    dgvRows.Add(row);
                    if (row.type.Equals("boolean"))
                        cell.ValueType = Type.GetType("boolean");
                }
            }
            SortableBindingList<dgvRow> bindingList = new SortableBindingList<dgvRow>();
            foreach (var row in dgvRows)
            {
                bindingList.Add(row);
            }
            dgv.DataSource = bindingList;
        }

        private void btnSql_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE " + tableName + " SET ";
            List<string> vals = new List<string>();
            foreach (dgvRow r in dgvRows)
            {
                if (!String.IsNullOrEmpty(r.newValue) && r.newValue != r.oldValue)
                {
                    switch (r.type)
                    {
                        case "String": vals.Add(r.column + " = '" + r.newValue + "'"); break;
                        case "Int32": vals.Add(r.column + " = " + r.newValue); break;
                        case "Long": vals.Add(r.column + " = " + r.newValue); break;
                        case "Decimal": vals.Add(r.column + " = " + r.newValue.Replace(',', '.')); break;
                        case "Double": vals.Add(r.column + " = " + r.newValue.Replace(',', '.')); break;
                        case "Boolean": vals.Add(r.column + " = " + r.newValue); break;
                        case "Guid": vals.Add(r.column + " = '" + r.newValue + "'"); break;
                    }
                }
            }
            if (vals.Count > 0)
            {
                sql += String.Join(",", vals) + " where " + "id='" + lId.Text + "';";
                Global.Global.SQLCommands.Add(sql);
            }
            btnSave.Enabled = Global.Global.SQLCommands.Count > 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.OwningColumn.Name == "cNewValue" || cell.OwningColumn.Name == "newValue")
            {
                dgvRow row = dgv.Rows[e.RowIndex].DataBoundItem as dgvRow;
                if (row != null)
                {
                    row.newValue = cell.Value?.ToString() ?? string.Empty;
                }
            }
            btnSql.Enabled = dgvRows.Any(r => !string.IsNullOrEmpty(r.newValue) && r.newValue != r.oldValue);
        }

        // Optional: wird derzeit nicht verwendet, bleibt aber angemeldet
        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Leere Implementierung: Validierung erfolgt nun in CellEndEdit
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show($"Datenfehler: {e.Exception?.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.ThrowException = false;
        }

        // Kann weiterhin vorhanden bleiben; die praktische Validierung erfolgt in CellEndEdit
        private void dgv_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            // Keine Logik hier; validiere in CellEndEdit
        }

        // Commit für dirty states (z.B. CheckBox) damit CellEndEdit/CellValueChanged zuverlässig ausgeführt werden
        private void dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv.IsCurrentCellDirty)
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        // Hauptvalidierung: wird nach Beenden der Zellbearbeitung ausgeführt
        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var col = dgv.Columns[e.ColumnIndex];
            // Wir interessieren uns nur für die "new value"-Spalte
            if (col.Name != "cNewValue" && col.Name != "newValue") return;

            DataGridViewRow r = dgv.Rows[e.RowIndex];
            DataGridViewCell cell = r.Cells[e.ColumnIndex];

            // Versuche, den eingegebenen Text zu bekommen (EditedFormattedValue oder Value)
            string raw = cell.EditedFormattedValue?.ToString()
                         ?? cell.FormattedValue?.ToString()
                         ?? cell.Value?.ToString()
                         ?? string.Empty;

            // Leerer Wert zulassen
            if (string.IsNullOrWhiteSpace(raw))
            {
                cell.ErrorText = string.Empty;
                UpdateBoundNewValue(r, string.Empty);
                return;
            }

            string stype = (r.Cells["cType"].FormattedValue ?? string.Empty).ToString().ToLower();

            // Normalisiere Dezimaltrenner: akzeptiere sowohl '.' als auch ',' als Dezimalpunkt
            string normalized = NormalizeDecimal(raw);
            string decSep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            int decCount = normalized.Count(c => c.ToString() == decSep);
            if (decCount > 1)
            {
                string msg = $"Ungültiges Dezimalformat: '{raw}'";
                cell.ErrorText = msg;
                MessageBox.Show(msg, "Validierungsfehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Revert: setze bound.newValue zurück (oder leer)
                RevertBoundNewValue(r);
                return;
            }

            bool valid = true;
            object parsedValue = null;

            switch (stype)
            {
                case "int32":
                case "int":
                    valid = int.TryParse(normalized, NumberStyles.Integer, CultureInfo.CurrentCulture, out var ires);
                    parsedValue = ires;
                    break;
                case "int64":
                case "long":
                    valid = long.TryParse(normalized, NumberStyles.Integer, CultureInfo.CurrentCulture, out var lres);
                    parsedValue = lres;
                    break;
                case "double":
                    valid = double.TryParse(normalized, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out var dres);
                    parsedValue = dres;
                    break;
                case "decimal":
                    valid = decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.CurrentCulture, out var decimalRes);
                    parsedValue = decimalRes;
                    break;
                case "boolean":
                case "bool":
                    valid = bool.TryParse(normalized, out var bres);
                    parsedValue = bres;
                    break;
                case "datetime":
                    valid = DateTime.TryParse(normalized, CultureInfo.CurrentCulture, DateTimeStyles.None, out var dtres);
                    parsedValue = dtres;
                    break;
                case "guid":
                    valid = Guid.TryParse(normalized, out var gres);
                    parsedValue = gres;
                    break;
                case "string":
                    valid = true;
                    parsedValue = normalized;
                    break;
                default:
                    valid = true;
                    parsedValue = normalized;
                    break;
            }

            if (!valid)
            {
                string msg = $"Ungültiger Wert für Typ '{stype}'";
                cell.ErrorText = msg;
                MessageBox.Show(msg, "Validierungsfehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RevertBoundNewValue(r);
            }
            else
            {
                // Erfolg: setze bound.newValue (als Rohstring) und lösche Fehlertext
                cell.ErrorText = string.Empty;
                UpdateBoundNewValue(r, raw);
            }

            // Aktualisiere den Button-Status
            btnSql.Enabled = dgvRows.Any(x => !string.IsNullOrEmpty(x.newValue) && x.newValue != x.oldValue);
            // Frische Anzeige
            dgv.Refresh();
        }

        private void UpdateBoundNewValue(DataGridViewRow row, string newValue)
        {
            var bound = row.DataBoundItem as dgvRow;
            if (bound != null)
            {
                bound.newValue = newValue ?? string.Empty;
            }
        }

        private void RevertBoundNewValue(DataGridViewRow row)
        {
            var bound = row.DataBoundItem as dgvRow;
            if (bound != null)
            {
                // Setze zurück auf alten Wert (oder leer). Hier wähle ich leer, damit Benutzer neu eingeben kann.
                bound.newValue = string.Empty;
            }
        }

        // Hilfsmethode: Konvertiert ',' oder '.' zum aktuellen Kultur-Dezimaltrenner.
        private string NormalizeDecimal(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input.Trim();

            string dec = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string alt = dec == "," ? "." : ",";

            string s = input.Trim();

            if (s.Contains(dec) && s.Contains(alt))
            {
                // beide vorkommen -> das alternative als Tausender-Trenner behandeln und entfernen
                s = s.Replace(alt, string.Empty);
            }
            else if (s.Contains(alt) && !s.Contains(dec))
            {
                s = s.Replace(alt, dec);
            }

            return s;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "SQL-Dateien (*.sql)|*.sql|Alle Dateien (*.*)|*.*",
                Title = "SQL-Datei speichern"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.File.WriteAllLines(sfd.FileName, Global.Global.SQLCommands);
                    MessageBox.Show("SQL-Befehle erfolgreich gespeichert.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Speichern der Datei: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Global.Global.SQLCommands.Clear();
            btnSave.Enabled = false;
            btnSql.Enabled = false; 
            btnClear.Enabled = false;
        }
    }

    internal class dgvRow
    {
        public string column { get; set; }
        public string type { get; set; }
        public string oldValue { get; set; }
        public string newValue { get; set; }
    }
}