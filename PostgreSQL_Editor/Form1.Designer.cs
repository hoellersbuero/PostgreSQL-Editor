namespace PostgreSQL_Editor
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSuccess = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.tbQuery = new System.Windows.Forms.TextBox();
            this.BtnExecute = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslLabel1,
            this.tsslInfo,
            this.toolStripStatusLabel1,
            this.tsslSuccess});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslLabel1
            // 
            this.tsslLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tsslLabel1.Name = "tsslLabel1";
            this.tsslLabel1.Size = new System.Drawing.Size(27, 17);
            this.tsslLabel1.Text = "DB:";
            // 
            // tsslInfo
            // 
            this.tsslInfo.ForeColor = System.Drawing.Color.Navy;
            this.tsslInfo.Name = "tsslInfo";
            this.tsslInfo.Size = new System.Drawing.Size(54, 17);
            this.tsslInfo.Text = "DBStatus";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(49, 17);
            this.toolStripStatusLabel1.Text = "QUERY:";
            // 
            // tsslSuccess
            // 
            this.tsslSuccess.ForeColor = System.Drawing.Color.Navy;
            this.tsslSuccess.Name = "tsslSuccess";
            this.tsslSuccess.Size = new System.Drawing.Size(47, 17);
            this.tsslSuccess.Text = "success";
            // 
            // dgv
            // 
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(12, 39);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(776, 377);
            this.dgv.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "SQL:";
            // 
            // tbQuery
            // 
            this.tbQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbQuery.Location = new System.Drawing.Point(46, 6);
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Size = new System.Drawing.Size(661, 20);
            this.tbQuery.TabIndex = 3;
            // 
            // BtnExecute
            // 
            this.BtnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExecute.Location = new System.Drawing.Point(713, 4);
            this.BtnExecute.Name = "BtnExecute";
            this.BtnExecute.Size = new System.Drawing.Size(75, 23);
            this.BtnExecute.TabIndex = 4;
            this.BtnExecute.Text = "Execute";
            this.BtnExecute.UseVisualStyleBackColor = true;
            this.BtnExecute.Click += new System.EventHandler(this.BtnExecute_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnExecute);
            this.Controls.Add(this.tbQuery);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslInfo;
        private System.Windows.Forms.ToolStripStatusLabel tsslSuccess;
        private System.Windows.Forms.ToolStripStatusLabel tsslLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbQuery;
        private System.Windows.Forms.Button BtnExecute;
    }
}

