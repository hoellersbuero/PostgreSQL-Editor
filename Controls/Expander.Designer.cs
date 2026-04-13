// Auto-generated Designer-Datei für Expander (kopiere in dein Projekt)
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Controls
{
    partial class Expander
    {
        /// <summary>Erforderliche Designervariable.</summary>
        private IContainer components = null;

        private Panel headerPanel;
        private Label headerLabel;
        private PictureBox chevron;
        private Panel contentPanel;
        private Timer animTimer;

        /// <summary>Initialisiert die GUI-Komponenten</summary>
        private void InitializeComponent()
        {
            this.components = new Container();
            this.headerPanel = new Panel();
            this.headerLabel = new Label();
            this.chevron = new PictureBox();
            this.contentPanel = new Panel();
            this.animTimer = new Timer(this.components);
            ((ISupportInitialize)(this.chevron)).BeginInit();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 28;
            this.headerPanel.BackColor = SystemColors.Control;
            this.headerPanel.Padding = new Padding(0);
            this.headerPanel.Controls.Add(this.headerLabel);
            this.headerPanel.Controls.Add(this.chevron);
            this.headerPanel.Click += new EventHandler(this.HeaderPanel_Click);
            // 
            // headerLabel
            // 
            this.headerLabel.Dock = DockStyle.Fill;
            this.headerLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.headerLabel.Padding = new Padding(6, 0, 0, 0);
            this.headerLabel.Text = "Expander";
            this.headerLabel.Click += new EventHandler(this.HeaderLabel_Click);
            // 
            // chevron
            // 
            this.chevron.Dock = DockStyle.Right;
            this.chevron.Width = 20;
            this.chevron.SizeMode = PictureBoxSizeMode.Center;
            this.chevron.Click += new EventHandler(this.Chevron_Click);
            // 
            // contentPanel
            // 
            this.contentPanel.Dock = DockStyle.Fill;
            this.contentPanel.BackColor = SystemColors.ControlLightLight;
            // 
            // animTimer
            // 
            this.animTimer.Interval = 15;
            this.animTimer.Tick += new EventHandler(this.AnimTimer_Tick);
            // 
            // Expander (this)
            // 
            this.AutoScaleMode = AutoScaleMode.Inherit;
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.Name = "Expander";
            this.Size = new Size(200, 100);
            ((ISupportInitialize)(this.chevron)).EndInit();
            this.ResumeLayout(false);
        }

        /// <summary>Verwendete Ressourcen bereinigen.</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}