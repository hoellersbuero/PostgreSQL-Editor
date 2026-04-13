using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PostgreSQL_Editor
{
    partial class SplashForm2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashForm2));
            this.lblTitle = new Label();
            this.lblStatus = new Label();
            this.pb = new ProgressBar();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "SplashForm2";

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
            this.lblTitle.Text = "PostgreSQL Editor";
            this.lblTitle.AutoSize = false;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitle.Dock = DockStyle.Top;
            this.lblTitle.Height = 70;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);

            // 
            // lblStatus
            // 
            this.lblStatus.Text = "Starting...";
            this.lblStatus.AutoSize = false;
            this.lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            this.lblStatus.Dock = DockStyle.Top;
            this.lblStatus.Height = 24;
            this.lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            // 
            // pb
            // 
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

        private Label lblTitle;
        private Label lblStatus;
        private ProgressBar pb;
    }



}
