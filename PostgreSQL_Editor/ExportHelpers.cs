using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

public static class ExportHelpers
{
    public sealed class DataGridViewExportSnapshot
    {
        public List<string> Headers { get; } = new List<string>();
        public List<object[]> Rows { get; } = new List<object[]>();
    }

    // Muss auf UI-Thread aufgerufen werden: erstellt eine threadsichere Kopie der sichtbaren Spalten/Zeilen.
    public static DataGridViewExportSnapshot CreateSnapshot(DataGridView dgv)
    {
        if (dgv == null) throw new ArgumentNullException(nameof(dgv));

        var snapshot = new DataGridViewExportSnapshot();

        var visibleColumns = dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).ToList();
        foreach (var col in visibleColumns)
        {
            snapshot.Headers.Add(col.HeaderText ?? col.Name);
        }

        foreach (DataGridViewRow row in dgv.Rows)
        {
            if (row.IsNewRow) continue;
            var values = new object[visibleColumns.Count];
            for (int c = 0; c < visibleColumns.Count; c++)
            {
                var cell = row.Cells[visibleColumns[c].Index];
                var val = cell?.Value;
                if (val == null || val == DBNull.Value) values[c] = string.Empty;
                else values[c] = val;
            }
            snapshot.Rows.Add(values);
        }

        return snapshot;
    }

    // Exportiert den zuvor erzeugten Snapshot. Wird im BackgroundWorker.DoWork aufgerufen.
    // Unterstuetzt Cancellation via worker.CancellationPending und meldet Fortschritt.
    public static void ExportSnapshotToExcel(DataGridViewExportSnapshot snapshot, string filePath, BackgroundWorker worker = null)
    {
        if (snapshot == null) throw new ArgumentNullException(nameof(snapshot));
        if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));

        Excel.Application xlApp = null;
        Excel.Workbook workbook = null;
        Excel.Worksheet worksheet = null;

        try
        {
            xlApp = new Excel.Application { Visible = false, DisplayAlerts = false };
            workbook = xlApp.Workbooks.Add(Type.Missing);
            worksheet = workbook.Sheets[1] as Excel.Worksheet;
            worksheet.Name = "Export";

            // Header
            for (int c = 0; c < snapshot.Headers.Count; c++)
            {
                var cell = worksheet.Cells[1, c + 1] as Excel.Range;
                cell.Value2 = snapshot.Headers[c];
                cell.Font.Bold = true;
                Marshal.ReleaseComObject(cell);
            }

            int totalRows = snapshot.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                if (worker != null && worker.CancellationPending)
                {
                    throw new OperationCanceledException("Export vom Benutzer abgebrochen.");
                }

                var rowValues = snapshot.Rows[r];
                for (int c = 0; c < rowValues.Length; c++)
                {
                    var excelCell = worksheet.Cells[r + 2, c + 1] as Excel.Range;
                    var val = rowValues[c];

                    if (val is DateTime dt)
                    {
                        excelCell.Value2 = dt;
                        excelCell.NumberFormat = "yyyy-mm-dd hh:mm:ss";
                    }
                    else if (val is IFormattable && (val is byte || val is sbyte || val is short || val is ushort
                             || val is int || val is uint || val is long || val is ulong || val is float || val is double || val is decimal))
                    {
                        excelCell.Value2 = val;
                    }
                    else
                    {
                        excelCell.Value2 = val?.ToString() ?? string.Empty;
                    }

                    Marshal.ReleaseComObject(excelCell);
                }

                // Fortschritt melden (optional)
                if (worker != null && worker.WorkerReportsProgress)
                {
                    int percent = (int)((r + 1) * 100L / Math.Max(1, totalRows));
                    worker.ReportProgress(percent, r + 1);
                }
            }

            worksheet.Columns.AutoFit();
            workbook.SaveAs(filePath, Excel.XlFileFormat.xlOpenXMLWorkbook);
        }
        finally
        {
            if (worksheet != null) Marshal.ReleaseComObject(worksheet);
            if (workbook != null)
            {
                try { workbook.Close(false); } catch { }
                Marshal.ReleaseComObject(workbook);
            }
            if (xlApp != null)
            {
                try { xlApp.Quit(); } catch { }
                Marshal.ReleaseComObject(xlApp);
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}