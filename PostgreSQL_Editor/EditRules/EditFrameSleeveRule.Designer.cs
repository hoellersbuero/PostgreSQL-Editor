namespace PostgreSQL_Editor.EditRules
{
    partial class EditFrameSleeveRule
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
            this.cbFrame = new System.Windows.Forms.ComboBox();
            this.cbSleeve = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudVersion = new System.Windows.Forms.NumericUpDown();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.lbInfo = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCreateSQL = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cbSleeveMaterial = new System.Windows.Forms.ComboBox();
            this.cCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cFrame = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSleeve = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.nudVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Frame:";
            // 
            // cbFrame
            // 
            this.cbFrame.FormattingEnabled = true;
            this.cbFrame.Location = new System.Drawing.Point(15, 31);
            this.cbFrame.Name = "cbFrame";
            this.cbFrame.Size = new System.Drawing.Size(161, 21);
            this.cbFrame.TabIndex = 1;
            // 
            // cbSleeve
            // 
            this.cbSleeve.FormattingEnabled = true;
            this.cbSleeve.Location = new System.Drawing.Point(182, 31);
            this.cbSleeve.Name = "cbSleeve";
            this.cbSleeve.Size = new System.Drawing.Size(161, 21);
            this.cbSleeve.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sleeve:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(428, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 45;
            this.label5.Text = "Rule version:";
            // 
            // nudVersion
            // 
            this.nudVersion.Location = new System.Drawing.Point(432, 32);
            this.nudVersion.Name = "nudVersion";
            this.nudVersion.Size = new System.Drawing.Size(63, 20);
            this.nudVersion.TabIndex = 44;
            this.nudVersion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbActive
            // 
            this.cbActive.AutoSize = true;
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Location = new System.Drawing.Point(354, 33);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(56, 17);
            this.cbActive.TabIndex = 43;
            this.cbActive.Text = "Active";
            this.cbActive.UseVisualStyleBackColor = true;
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
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cCheck,
            this.cFrame,
            this.cSleeve,
            this.cActive,
            this.cVersion,
            this.cFill});
            this.dgv.Location = new System.Drawing.Point(15, 67);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 51;
            this.dgv.Size = new System.Drawing.Size(558, 319);
            this.dgv.TabIndex = 46;
            // 
            // lbInfo
            // 
            this.lbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInfo.Location = new System.Drawing.Point(12, 389);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(561, 20);
            this.lbInfo.TabIndex = 50;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(498, 412);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 49;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCreateSQL
            // 
            this.btnCreateSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateSQL.Enabled = false;
            this.btnCreateSQL.Location = new System.Drawing.Point(12, 412);
            this.btnCreateSQL.Name = "btnCreateSQL";
            this.btnCreateSQL.Size = new System.Drawing.Size(75, 23);
            this.btnCreateSQL.TabIndex = 48;
            this.btnCreateSQL.Text = "Create SQL";
            this.btnCreateSQL.UseVisualStyleBackColor = true;
            this.btnCreateSQL.Click += new System.EventHandler(this.btnCreateSQL_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(501, 29);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 51;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cbSleeveMaterial
            // 
            this.cbSleeveMaterial.FormattingEnabled = true;
            this.cbSleeveMaterial.Location = new System.Drawing.Point(232, 7);
            this.cbSleeveMaterial.Name = "cbSleeveMaterial";
            this.cbSleeveMaterial.Size = new System.Drawing.Size(111, 21);
            this.cbSleeveMaterial.TabIndex = 52;
            this.cbSleeveMaterial.SelectedIndexChanged += new System.EventHandler(this.cbSleeveMaterial_SelectedIndexChanged);
            // 
            // cCheck
            // 
            this.cCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cCheck.HeaderText = "";
            this.cCheck.MinimumWidth = 6;
            this.cCheck.Name = "cCheck";
            this.cCheck.Width = 6;
            // 
            // cFrame
            // 
            this.cFrame.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cFrame.DataPropertyName = "frame_name";
            this.cFrame.HeaderText = "Frame";
            this.cFrame.MinimumWidth = 6;
            this.cFrame.Name = "cFrame";
            this.cFrame.Width = 61;
            // 
            // cSleeve
            // 
            this.cSleeve.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cSleeve.DataPropertyName = "sleeve_name";
            this.cSleeve.HeaderText = "Sleeve";
            this.cSleeve.MinimumWidth = 6;
            this.cSleeve.Name = "cSleeve";
            this.cSleeve.Width = 65;
            // 
            // cActive
            // 
            this.cActive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cActive.DataPropertyName = "is_active";
            this.cActive.HeaderText = "Active";
            this.cActive.MinimumWidth = 6;
            this.cActive.Name = "cActive";
            this.cActive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cActive.Width = 62;
            // 
            // cVersion
            // 
            this.cVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cVersion.DataPropertyName = "rule_version";
            this.cVersion.HeaderText = "Version";
            this.cVersion.MinimumWidth = 6;
            this.cVersion.Name = "cVersion";
            this.cVersion.Width = 67;
            // 
            // cFill
            // 
            this.cFill.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cFill.HeaderText = "";
            this.cFill.MinimumWidth = 6;
            this.cFill.Name = "cFill";
            // 
            // EditFrameSleeveRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 450);
            this.Controls.Add(this.cbSleeveMaterial);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreateSQL);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudVersion);
            this.Controls.Add(this.cbActive);
            this.Controls.Add(this.cbSleeve);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbFrame);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditFrameSleeveRule";
            this.Text = "Edit Frame Sleeve Rule";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditFrameSleeveRule_FormClosing);
            this.Load += new System.EventHandler(this.EditFrameSleeveRule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFrame;
        private System.Windows.Forms.ComboBox cbSleeve;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudVersion;
        private System.Windows.Forms.CheckBox cbActive;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCreateSQL;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cbSleeveMaterial;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFrame;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSleeve;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn cVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFill;
    }
}