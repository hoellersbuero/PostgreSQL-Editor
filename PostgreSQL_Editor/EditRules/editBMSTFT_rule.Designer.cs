namespace PostgreSQL_Editor.EditRules
{
    partial class editBMSTFT_rule
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
            this.label3 = new System.Windows.Forms.Label();
            this.cbFrameType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSystemType = new System.Windows.Forms.ComboBox();
            this.cbBaseMaterial = new System.Windows.Forms.ComboBox();
            this.cbSpecial = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nudVersion = new System.Windows.Forms.NumericUpDown();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCreateSQL = new System.Windows.Forms.Button();
            this.lbInfo = new System.Windows.Forms.Label();
            this.cCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cBaseMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSystemType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFrametype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cSpecial = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cRuleVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.nudVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Frametype:";
            // 
            // cbFrameType
            // 
            this.cbFrameType.FormattingEnabled = true;
            this.cbFrameType.Location = new System.Drawing.Point(266, 25);
            this.cbFrameType.Name = "cbFrameType";
            this.cbFrameType.Size = new System.Drawing.Size(121, 21);
            this.cbFrameType.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Systemtype:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Basematerial:";
            // 
            // cbSystemType
            // 
            this.cbSystemType.FormattingEnabled = true;
            this.cbSystemType.Location = new System.Drawing.Point(139, 25);
            this.cbSystemType.Name = "cbSystemType";
            this.cbSystemType.Size = new System.Drawing.Size(121, 21);
            this.cbSystemType.TabIndex = 20;
            // 
            // cbBaseMaterial
            // 
            this.cbBaseMaterial.FormattingEnabled = true;
            this.cbBaseMaterial.Location = new System.Drawing.Point(12, 25);
            this.cbBaseMaterial.Name = "cbBaseMaterial";
            this.cbBaseMaterial.Size = new System.Drawing.Size(121, 21);
            this.cbBaseMaterial.TabIndex = 19;
            this.cbBaseMaterial.SelectedIndexChanged += new System.EventHandler(this.cbBaseMaterial_SelectedIndexChanged);
            // 
            // cbSpecial
            // 
            this.cbSpecial.AutoSize = true;
            this.cbSpecial.Location = new System.Drawing.Point(455, 27);
            this.cbSpecial.Name = "cbSpecial";
            this.cbSpecial.Size = new System.Drawing.Size(61, 17);
            this.cbSpecial.TabIndex = 34;
            this.cbSpecial.Text = "Special";
            this.cbSpecial.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(519, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Rule version:";
            // 
            // nudVersion
            // 
            this.nudVersion.Location = new System.Drawing.Point(522, 26);
            this.nudVersion.Name = "nudVersion";
            this.nudVersion.Size = new System.Drawing.Size(63, 20);
            this.nudVersion.TabIndex = 32;
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
            this.cbActive.Location = new System.Drawing.Point(393, 27);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(56, 17);
            this.cbActive.TabIndex = 31;
            this.cbActive.Text = "Active";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(594, 23);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 30;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.cBaseMaterial,
            this.cSystemType,
            this.cFrametype,
            this.cActive,
            this.cSpecial,
            this.cRuleVersion,
            this.cFill});
            this.dgv.Location = new System.Drawing.Point(12, 52);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(657, 351);
            this.dgv.TabIndex = 35;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(594, 429);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 37;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCreateSQL
            // 
            this.btnCreateSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateSQL.Enabled = false;
            this.btnCreateSQL.Location = new System.Drawing.Point(12, 429);
            this.btnCreateSQL.Name = "btnCreateSQL";
            this.btnCreateSQL.Size = new System.Drawing.Size(75, 23);
            this.btnCreateSQL.TabIndex = 36;
            this.btnCreateSQL.Text = "Create SQL";
            this.btnCreateSQL.UseVisualStyleBackColor = true;
            this.btnCreateSQL.Click += new System.EventHandler(this.btnCreateSQL_Click);
            // 
            // lbInfo
            // 
            this.lbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInfo.Location = new System.Drawing.Point(12, 406);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(657, 20);
            this.lbInfo.TabIndex = 38;
            // 
            // cCheck
            // 
            this.cCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cCheck.HeaderText = "";
            this.cCheck.MinimumWidth = 25;
            this.cCheck.Name = "cCheck";
            this.cCheck.Width = 25;
            // 
            // cBaseMaterial
            // 
            this.cBaseMaterial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cBaseMaterial.DataPropertyName = "base_material";
            this.cBaseMaterial.HeaderText = "Basematerial";
            this.cBaseMaterial.MinimumWidth = 130;
            this.cBaseMaterial.Name = "cBaseMaterial";
            this.cBaseMaterial.ReadOnly = true;
            this.cBaseMaterial.Width = 130;
            // 
            // cSystemType
            // 
            this.cSystemType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cSystemType.DataPropertyName = "system_type";
            this.cSystemType.HeaderText = "Systemtype";
            this.cSystemType.MinimumWidth = 130;
            this.cSystemType.Name = "cSystemType";
            this.cSystemType.ReadOnly = true;
            this.cSystemType.Width = 130;
            // 
            // cFrametype
            // 
            this.cFrametype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cFrametype.DataPropertyName = "frame_type";
            this.cFrametype.HeaderText = "Frametype";
            this.cFrametype.MinimumWidth = 130;
            this.cFrametype.Name = "cFrametype";
            this.cFrametype.Width = 130;
            // 
            // cActive
            // 
            this.cActive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cActive.DataPropertyName = "is_active";
            this.cActive.HeaderText = "Active";
            this.cActive.Name = "cActive";
            this.cActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cActive.Width = 62;
            // 
            // cSpecial
            // 
            this.cSpecial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cSpecial.DataPropertyName = "is_special";
            this.cSpecial.HeaderText = "Special";
            this.cSpecial.Name = "cSpecial";
            this.cSpecial.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cSpecial.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cSpecial.Width = 67;
            // 
            // cRuleVersion
            // 
            this.cRuleVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cRuleVersion.DataPropertyName = "rule_version";
            this.cRuleVersion.HeaderText = "Rule version";
            this.cRuleVersion.Name = "cRuleVersion";
            this.cRuleVersion.Width = 91;
            // 
            // cFill
            // 
            this.cFill.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cFill.HeaderText = "";
            this.cFill.Name = "cFill";
            // 
            // editBMSTFT_rule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 461);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreateSQL);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.cbSpecial);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudVersion);
            this.Controls.Add(this.cbActive);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbFrameType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSystemType);
            this.Controls.Add(this.cbBaseMaterial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(697, 9999);
            this.MinimumSize = new System.Drawing.Size(697, 500);
            this.Name = "editBMSTFT_rule";
            this.Text = "Edit Basematerial-Systemtype-Frametype Rules";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.editBMSTFTMT_rule_FormClosing);
            this.Load += new System.EventHandler(this.editBMSTFT_rule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFrameType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSystemType;
        private System.Windows.Forms.ComboBox cbBaseMaterial;
        private System.Windows.Forms.CheckBox cbSpecial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudVersion;
        private System.Windows.Forms.CheckBox cbActive;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCreateSQL;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBaseMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSystemType;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFrametype;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cActive;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cSpecial;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRuleVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFill;
    }
}