using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

public static class ExportHelpers
{
    // Exportiert sichtbare Spalten und alle Zeilen in eine .xlsx-Datei
    public static void ExportDataGridViewToExcel(DataGridView dgv, string filePath)
    {
        if (dgv == null) throw new ArgumentNullException(nameof(dgv));
        if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Export");
            // Header
            var visibleColumns = dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).ToList();
            for (int c = 0; c < visibleColumns.Count; c++)
            {
                worksheet.Cell(1, c + 1).Value = visibleColumns[c].HeaderText ?? visibleColumns[c].Name;
                worksheet.Cell(1, c + 1).Style.Font.SetBold();
            }

            // Datenzeilen
            int rowIndex = 2;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;
                for (int c = 0; c < visibleColumns.Count; c++)
                {
                    var cell = row.Cells[visibleColumns[c].Index];
                    object val = cell.Value;
                    // ClosedXML erkennt Typen; übergebe null als leeren String
                    worksheet.Cell(rowIndex, c + 1).Value = val ?? string.Empty;
                }
                rowIndex++;
            }

            // Formatiere Spalten
            worksheet.Columns().AdjustToContents();
            // File speichern (überschreibt)
            workbook.SaveAs(filePath);
        }
    }

    // Einfacher CSV-Export (Trennzeichen standardmäßig ';')
    public static void ExportDataGridViewToCsv(DataGridView dgv, string filePath, char separator = ';')
    {
        if (dgv == null) throw new ArgumentNullException(nameof(dgv));
        if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));

        var visibleColumns = dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).ToList();
        using (var sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
        {
            // Header
            sw.WriteLine(string.Join(separator.ToString(), visibleColumns.Select(h => QuoteCsv(h.HeaderText ?? h.Name, separator))));
            // Rows
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;
                var parts = visibleColumns.Select(col =>
                {
                    var val = row.Cells[col.Index].Value;
                    var s = val?.ToString() ?? string.Empty;
                    return QuoteCsv(s, separator);
                });
                sw.WriteLine(string.Join(separator.ToString(), parts));
            }
        }
    }

    private static string QuoteCsv(string input, char separator)
    {
        if (input == null) return string.Empty;
        bool mustQuote = input.Contains(separator) || input.Contains('"') || input.Contains('\n') || input.Contains('\r');
        if (!mustQuote) return input;
        // Doppelte Anführungszeichen im Inhalt verdoppeln
        string escaped = input.Replace("\"", "\"\"");
        return "\"" + escaped + "\"";
    }
}