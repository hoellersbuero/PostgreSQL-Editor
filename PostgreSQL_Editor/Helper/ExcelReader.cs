using ClosedXML.Excel;
using System;
using System.Data;
using System.Linq;

namespace PostgreSQL_Editor.Helper
{
    public static class ExcelReader
    {
        // Liest das erste Worksheet und verwendet die erste Zeile als Spaltenüberschriften (wenn vorhanden).
        public static DataTable ReadExcelFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("filePath is null or empty", nameof(filePath));

            var dt = new DataTable();

            using (var wb = new XLWorkbook(filePath))
            {
                var ws = wb.Worksheets.Count > 0 ? wb.Worksheet(1) : null;
                if (ws == null) return dt;

                var range = ws.RangeUsed();
                if (range == null) return dt;

                var usedRows = range.RowsUsed().ToList();
                if (usedRows.Count < 2)
                {
                    // Weniger als zwei benutzte Zeilen: keine Header-Zeile in Zeile 2 vorhanden
                    return dt;
                }

                // Header in der zweiten benutzten Zeile
                var headerRow = usedRows[1];

                // Erzeuge Spalten anhand der Header-Zeile
                foreach (var cell in headerRow.Cells())
                {
                    var colName = cell.GetString();
                    if (string.IsNullOrWhiteSpace(colName))
                        colName = "Column" + (dt.Columns.Count + 1);
                    // Vermeide doppelte Spaltennamen
                    var safeName = colName;
                    int dup = 1;
                    while (dt.Columns.Contains(safeName))
                        safeName = colName + "_" + (dup++);
                    dt.Columns.Add(safeName, typeof(string));
                }

                // Fülle Daten ab der dritten benutzten Zeile (nach headerRow)
                var dataRows = usedRows.Skip(2);
                foreach (var row in dataRows)
                {
                    var dr = dt.NewRow();
                    int i = 0;
                    // Nur so viele Zellen lesen, wie Spalten definiert sind
                    foreach (var cell in row.Cells(1, dt.Columns.Count))
                    {
                        object val = cell.Value;
                        dr[i++] = val ?? DBNull.Value;
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }
    }
}