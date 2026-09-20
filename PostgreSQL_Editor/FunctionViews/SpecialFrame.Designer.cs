namespace PostgreSQL_Editor.FunctionViews
{
    partial class SpecialFrame
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbBasematerial = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSystemtype = new System.Windows.Forms.ComboBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvComplete = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbJSON = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComplete)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Base material:";
            // 
            // cbBasematerial
            // 
            this.cbBasematerial.FormattingEnabled = true;
            this.cbBasematerial.Location = new System.Drawing.Point(91, 8);
            this.cbBasematerial.Name = "cbBasematerial";
            this.cbBasematerial.Size = new System.Drawing.Size(146, 21);
            this.cbBasematerial.TabIndex = 1;
            this.cbBasematerial.SelectedIndexChanged += new System.EventHandler(this.cbBasematerial_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "System type:";
            // 
            // cbSystemtype
            // 
            this.cbSystemtype.FormattingEnabled = true;
            this.cbSystemtype.Location = new System.Drawing.Point(316, 8);
            this.cbSystemtype.Name = "cbSystemtype";
            this.cbSystemtype.Size = new System.Drawing.Size(121, 21);
            this.cbSystemtype.TabIndex = 3;
            this.cbSystemtype.SelectedIndexChanged += new System.EventHandler(this.cbSystemtype_SelectedIndexChanged);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(3, 35);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(759, 333);
            this.dgv.TabIndex = 4;
            this.dgv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);
            this.dgv.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.Dgv_DataBindingComplete);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(713, 415);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(15, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(773, 397);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvComplete);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(765, 371);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Complete";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvComplete
            // 
            this.dgvComplete.BackgroundColor = System.Drawing.Color.White;
            this.dgvComplete.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComplete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvComplete.Location = new System.Drawing.Point(3, 3);
            this.dgvComplete.Name = "dgvComplete";
            this.dgvComplete.Size = new System.Drawing.Size(759, 365);
            this.dgvComplete.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgv);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.cbSystemtype);
            this.tabPage2.Controls.Add(this.cbBasematerial);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(765, 371);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Selectable";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tbJSON);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(765, 371);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "JSON Result";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbJSON
            // 
            this.tbJSON.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbJSON.Location = new System.Drawing.Point(3, 3);
            this.tbJSON.Multiline = true;
            this.tbJSON.Name = "tbJSON";
            this.tbJSON.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbJSON.Size = new System.Drawing.Size(759, 365);
            this.tbJSON.TabIndex = 0;
            // 
            // SpecialFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SpecialFrame";
            this.Text = "SpecialFrame";
            this.Load += new System.EventHandler(this.SpecialFrame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvComplete)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbBasematerial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSystemtype;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvComplete;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tbJSON;
    }
}