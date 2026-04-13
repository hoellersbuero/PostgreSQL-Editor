using System;
using System.Drawing;
using System.Windows.Forms;

public partial class YourForm : Form
{
    public YourForm()
    {
        InitializeComponent();

        // Annahme: DataGridView heißt dataGridView1
        dataGridView1.RowPostPaint += DataGridView1_RowPostPaint;
        dataGridView1.RowsAdded += DataGridView1_RowsChanged;
        dataGridView1.RowsRemoved += DataGridView1_RowsChanged;
        dataGridView1.Sorted += (s, e) => dataGridView1.Invalidate(); // nach Sortierung neu zeichnen
    }

    private void DataGridView1_RowsChanged(object sender, EventArgs e)
    {
        AdjustRowHeaderWidth();
    }

    private void AdjustRowHeaderWidth()
    {
        int rowCount = Math.Max(1, dataGridView1.Rows.Count); // mind. 1 für Header-Breite
        int digits = rowCount.ToString().Length;
        var sample = new string('9', digits);
        int needed = TextRenderer.MeasureText(sample, dataGridView1.Font).Width + 20; // Padding
        if (dataGridView1.RowHeadersWidth < needed)
            dataGridView1.RowHeadersWidth = needed;
    }

    private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
    {
        // Nummerierung beginnt bei 1
        string rowNumber = (e.RowIndex + 1).ToString();

        // Hintergrund/Standardinhalt zeichnen (falls nötig)
        e.PaintBackground(e.RowBounds, true);

        // Text rechtsbündig im RowHeader zeichnen, vertikal zentriert
        var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dataGridView1.RowHeadersWidth, e.RowBounds.Height);
        TextRenderer.DrawText(
            e.Graphics,
            rowNumber,
            dataGridView1.Font,
            headerBounds,
            dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);

        // Optional: Linie/Border erneut zeichnen
        e.Paint(e.RowBounds, DataGridViewPaintParts.Border);
    }
}