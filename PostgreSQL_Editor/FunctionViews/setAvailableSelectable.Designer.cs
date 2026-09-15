namespace PostgreSQL_Editor.FunctionViews
{
    partial class setAvailableSelectable
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.cArticleNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAvailable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cSelectable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cFill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSetAvail = new System.Windows.Forms.Button();
            this.btnClearAvail = new System.Windows.Forms.Button();
            this.btnClearSelect = new System.Windows.Forms.Button();
            this.btnSetSelect = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopyToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangeArticleNumber = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.lbRows = new System.Windows.Forms.Label();
            this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.dgvContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drop list from clipboard";
            // 
            // dgv
            // 
            this.dgv.AllowDrop = true;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cArticleNumber,
            this.cName,
            this.cAvailable,
            this.cSelectable,
            this.cFill});
            this.dgv.ContextMenuStrip = this.dgvContextMenu;
            this.dgv.Location = new System.Drawing.Point(12, 25);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(456, 451);
            this.dgv.TabIndex = 1;
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
            this.dgv.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgv_DragDrop);
            this.dgv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgv_KeyPress);
            this.dgv.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dgv_PreviewKeyDown);
            // 
            // cArticleNumber
            // 
            this.cArticleNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cArticleNumber.DataPropertyName = "article_number";
            this.cArticleNumber.HeaderText = "Article #";
            this.cArticleNumber.Name = "cArticleNumber";
            this.cArticleNumber.ReadOnly = true;
            this.cArticleNumber.Width = 71;
            // 
            // cName
            // 
            this.cName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cName.DataPropertyName = "name";
            this.cName.HeaderText = "Name";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            this.cName.Width = 60;
            // 
            // cAvailable
            // 
            this.cAvailable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cAvailable.DataPropertyName = "is_available";
            this.cAvailable.HeaderText = "Available";
            this.cAvailable.Name = "cAvailable";
            this.cAvailable.ReadOnly = true;
            this.cAvailable.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cAvailable.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cAvailable.Width = 75;
            // 
            // cSelectable
            // 
            this.cSelectable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cSelectable.DataPropertyName = "is_selectable";
            this.cSelectable.HeaderText = "Selectable";
            this.cSelectable.Name = "cSelectable";
            this.cSelectable.ReadOnly = true;
            this.cSelectable.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cSelectable.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cSelectable.Width = 82;
            // 
            // cFill
            // 
            this.cFill.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cFill.HeaderText = "";
            this.cFill.Name = "cFill";
            this.cFill.ReadOnly = true;
            // 
            // btnSetAvail
            // 
            this.btnSetAvail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetAvail.Enabled = false;
            this.btnSetAvail.Location = new System.Drawing.Point(12, 482);
            this.btnSetAvail.Name = "btnSetAvail";
            this.btnSetAvail.Size = new System.Drawing.Size(93, 23);
            this.btnSetAvail.TabIndex = 2;
            this.btnSetAvail.Text = "Set Available";
            this.btnSetAvail.UseVisualStyleBackColor = true;
            this.btnSetAvail.Click += new System.EventHandler(this.btnSetAvail_Click);
            // 
            // btnClearAvail
            // 
            this.btnClearAvail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearAvail.Enabled = false;
            this.btnClearAvail.Location = new System.Drawing.Point(111, 482);
            this.btnClearAvail.Name = "btnClearAvail";
            this.btnClearAvail.Size = new System.Drawing.Size(93, 23);
            this.btnClearAvail.TabIndex = 3;
            this.btnClearAvail.Text = "Clear Available";
            this.btnClearAvail.UseVisualStyleBackColor = true;
            this.btnClearAvail.Click += new System.EventHandler(this.btnClearAvail_Click);
            // 
            // btnClearSelect
            // 
            this.btnClearSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearSelect.Enabled = false;
            this.btnClearSelect.Location = new System.Drawing.Point(375, 482);
            this.btnClearSelect.Name = "btnClearSelect";
            this.btnClearSelect.Size = new System.Drawing.Size(93, 23);
            this.btnClearSelect.TabIndex = 5;
            this.btnClearSelect.Text = "Clear Selectable";
            this.btnClearSelect.UseVisualStyleBackColor = true;
            this.btnClearSelect.Click += new System.EventHandler(this.btnClearSelect_Click);
            // 
            // btnSetSelect
            // 
            this.btnSetSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetSelect.Enabled = false;
            this.btnSetSelect.Location = new System.Drawing.Point(276, 482);
            this.btnSetSelect.Name = "btnSetSelect";
            this.btnSetSelect.Size = new System.Drawing.Size(93, 23);
            this.btnSetSelect.TabIndex = 4;
            this.btnSetSelect.Text = "Set Selectable";
            this.btnSetSelect.UseVisualStyleBackColor = true;
            this.btnSetSelect.Click += new System.EventHandler(this.btnSetSelect_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(12, 511);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save SQL";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(393, 511);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvContextMenu
            // 
            this.dgvContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyToClipboard,
            this.tsmiChangeArticleNumber,
            this.toolStripMenuItem2,
            this.tsmiPaste});
            this.dgvContextMenu.Name = "dgvContextMenu";
            this.dgvContextMenu.Size = new System.Drawing.Size(200, 98);
            this.dgvContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.dgvContextMenu_Opening);
            // 
            // tsmiCopyToClipboard
            // 
            this.tsmiCopyToClipboard.Name = "tsmiCopyToClipboard";
            this.tsmiCopyToClipboard.Size = new System.Drawing.Size(199, 22);
            this.tsmiCopyToClipboard.Text = "Copy to Clipboard";
            this.tsmiCopyToClipboard.Click += new System.EventHandler(this.tsmiCopyToClipboard_Click);
            // 
            // tsmiChangeArticleNumber
            // 
            this.tsmiChangeArticleNumber.Name = "tsmiChangeArticleNumber";
            this.tsmiChangeArticleNumber.Size = new System.Drawing.Size(199, 22);
            this.tsmiChangeArticleNumber.Text = "Change Article Number";
            this.tsmiChangeArticleNumber.Click += new System.EventHandler(this.tsmiChangeArticleNumber_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(345, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Selected rows:";
            // 
            // lbRows
            // 
            this.lbRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRows.ForeColor = System.Drawing.Color.Navy;
            this.lbRows.Location = new System.Drawing.Point(428, 9);
            this.lbRows.Name = "lbRows";
            this.lbRows.Size = new System.Drawing.Size(41, 13);
            this.lbRows.TabIndex = 10;
            this.lbRows.Text = "0";
            this.lbRows.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tsmiPaste
            // 
            this.tsmiPaste.Name = "tsmiPaste";
            this.tsmiPaste.Size = new System.Drawing.Size(199, 22);
            this.tsmiPaste.Text = "Paste";
            this.tsmiPaste.Click += new System.EventHandler(this.tsmiPaste_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(196, 6);
            // 
            // setAvailableSelectable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 545);
            this.Controls.Add(this.lbRows);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClearSelect);
            this.Controls.Add(this.btnSetSelect);
            this.Controls.Add(this.btnClearAvail);
            this.Controls.Add(this.btnSetAvail);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "setAvailableSelectable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set Available and Selectable";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.setAvailableSelectable_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.dgvContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn cArticleNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cAvailable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cSelectable;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFill;
        private System.Windows.Forms.Button btnSetAvail;
        private System.Windows.Forms.Button btnClearAvail;
        private System.Windows.Forms.Button btnClearSelect;
        private System.Windows.Forms.Button btnSetSelect;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ContextMenuStrip dgvContextMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyToClipboard;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangeArticleNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbRows;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmiPaste;
    }
}