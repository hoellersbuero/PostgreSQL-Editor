using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PostgreSQL_Editor
{
    partial class SplashForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();

            // Controls werden in der anderen Partial-Klasse deklariert (oder hierher verschieben).
            // Falls die Felder `lblTitle`, `lblStatus`, `pb` noch in SplasForm.cs stehen,
            // dürfen sie hier NICHT nochmals deklariert werden. Entferne dann die Deklarationen aus SplasForm.cs.
            this.SuspendLayout();

            // 
            // SplashForm (Form-Eigenschaften)
            // 
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(460, 140);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.Name = "SplashForm";
            this.Text = "SplashForm";

            // 
            // lblTitle
            // 
            if (this.lblTitle == null) this.lblTitle = new Label();
            this.lblTitle.Text = "PostgreSQL Editor";
            this.lblTitle.AutoSize = false;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitle.Dock = DockStyle.Top;
            this.lblTitle.Height = 70;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);

            // 
            // lblStatus
            // 
            if (this.lblStatus == null) this.lblStatus = new Label();
            this.lblStatus.Text = "Starting...";
            this.lblStatus.AutoSize = false;
            this.lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            this.lblStatus.Dock = DockStyle.Top;
            this.lblStatus.Height = 24;
            this.lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            // 
            // pb
            // 
            if (this.pb == null) this.pb = new ProgressBar();
            this.pb.Style = ProgressBarStyle.Marquee;
            this.pb.MarqueeAnimationSpeed = 30;
            this.pb.Dock = DockStyle.Bottom;
            this.pb.Height = 18;

            // 
            // Controls hinzufügen (Reihenfolge wie im Original)
            // 
            this.Controls.Add(this.pb);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblTitle);

            this.ResumeLayout(false);
        }

        #endregion
    }
}