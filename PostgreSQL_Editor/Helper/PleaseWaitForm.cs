// PleaseWaitForm.cs
using System.Windows.Forms;

public class PleaseWaitForm : Form
{
    public PleaseWaitForm()
    {
        this.StartPosition = FormStartPosition.CenterParent;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.ControlBox = false;
        this.Width = 320;
        this.Height = 110;

        var lbl = new Label
        {
            Text = "Creating SQL Statements\r\nPlease standby...",
            AutoSize = false,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill
        };
        var pb = new ProgressBar
        {
            Style = ProgressBarStyle.Marquee,
            Dock = DockStyle.Bottom,
            Height = 16
        };
        this.Controls.Add(lbl);
        this.Controls.Add(pb);
    }
}