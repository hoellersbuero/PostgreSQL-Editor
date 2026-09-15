namespace PostgreSQL_Editor.EditRules
{
    partial class editWedgeOptionRule
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
            this.lbInfo = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCreateSQL = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.nudVersion = new System.Windows.Forms.NumericUpDown();
            this.cbSpecial = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSystemType = new System.Windows.Forms.ComboBox();
            this.cbWedge = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbMaterialType = new System.Windows.Forms.ComboBox();
            this.cbWedgeOption = new System.Windows.Forms.CheckBox();
            this.cbMaterialOption = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cBaseMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaterialType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cWedgeOption = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cMaterialOption = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cDefaultWedge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cArticleNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSpecial = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cRuleVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVersion)).BeginInit();
            this.SuspendLayout();
            // 
            // lbInfo
            // 
            this.lbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInfo.Location = new System.Drawing.Point(13, 408);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(939, 18);
            this.lbInfo.TabIndex = 78;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(877, 429);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 77;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCreateSQL
            // 
            this.btnCreateSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateSQL.Enabled = false;
            this.btnCreateSQL.Location = new System.Drawing.Point(13, 429);
            this.btnCreateSQL.Name = "btnCreateSQL";
            this.btnCreateSQL.Size = new System.Drawing.Size(75, 23);
            this.btnCreateSQL.TabIndex = 76;
            this.btnCreateSQL.Text = "Create SQL";
            this.btnCreateSQL.UseVisualStyleBackColor = true;
            this.btnCreateSQL.Click += new System.EventHandler(this.btnCreateSQL_Click);
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
            this.cMaterialType,
            this.cWedgeOption,
            this.cMaterialOption,
            this.cDefaultWedge,
            this.cArticleNumber,
            this.cSpecial,
            this.cActive,
            this.cRuleVersion,
            this.cFill});
            this.dgv.Location = new System.Drawing.Point(13, 80);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 51;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(939, 325);
            this.dgv.TabIndex = 75;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(396, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 74;
            this.label5.Text = "Rule version:";
            // 
            // nudVersion
            // 
            this.nudVersion.Location = new System.Drawing.Point(471, 51);
            this.nudVersion.Name = "nudVersion";
            this.nudVersion.Size = new System.Drawing.Size(63, 20);
            this.nudVersion.TabIndex = 73;
            this.nudVersion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbSpecial
            // 
            this.cbSpecial.AutoSize = true;
            this.cbSpecial.Checked = true;
            this.cbSpecial.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSpecial.Location = new System.Drawing.Point(267, 52);
            this.cbSpecial.Name = "cbSpecial";
            this.cbSpecial.Size = new System.Drawing.Size(61, 17);
            this.cbSpecial.TabIndex = 72;
            this.cbSpecial.Text = "Special";
            this.cbSpecial.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(549, 25);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 46);
            this.btnAdd.TabIndex = 71;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 70;
            this.label2.Text = "Systemtype:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(264, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 69;
            this.label1.Text = "Default wedge:";
            // 
            // cbSystemType
            // 
            this.cbSystemType.FormattingEnabled = true;
            this.cbSystemType.Location = new System.Drawing.Point(13, 25);
            this.cbSystemType.Name = "cbSystemType";
            this.cbSystemType.Size = new System.Drawing.Size(121, 21);
            this.cbSystemType.TabIndex = 68;
            // 
            // cbWedge
            // 
            this.cbWedge.FormattingEnabled = true;
            this.cbWedge.Items.AddRange(new object[] {
            "AAAAA\\rAAAA",
            "BBBBB\\rBBBB"});
            this.cbWedge.Location = new System.Drawing.Point(267, 25);
            this.cbWedge.Name = "cbWedge";
            this.cbWedge.Size = new System.Drawing.Size(267, 21);
            this.cbWedge.TabIndex = 67;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(137, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 80;
            this.label4.Text = "Materialtype:";
            // 
            // cbMaterialType
            // 
            this.cbMaterialType.FormattingEnabled = true;
            this.cbMaterialType.Location = new System.Drawing.Point(140, 25);
            this.cbMaterialType.Name = "cbMaterialType";
            this.cbMaterialType.Size = new System.Drawing.Size(121, 21);
            this.cbMaterialType.TabIndex = 79;
            // 
            // cbWedgeOption
            // 
            this.cbWedgeOption.AutoSize = true;
            this.cbWedgeOption.Checked = true;
            this.cbWedgeOption.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWedgeOption.Location = new System.Drawing.Point(12, 52);
            this.cbWedgeOption.Name = "cbWedgeOption";
            this.cbWedgeOption.Size = new System.Drawing.Size(146, 17);
            this.cbWedgeOption.TabIndex = 81;
            this.cbWedgeOption.Text = "Wedge packaging option";
            this.cbWedgeOption.UseVisualStyleBackColor = true;
            // 
            // cbMaterialOption
            // 
            this.cbMaterialOption.AutoSize = true;
            this.cbMaterialOption.Checked = true;
            this.cbMaterialOption.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMaterialOption.Location = new System.Drawing.Point(164, 52);
            this.cbMaterialOption.Name = "cbMaterialOption";
            this.cbMaterialOption.Size = new System.Drawing.Size(95, 17);
            this.cbMaterialOption.TabIndex = 82;
            this.cbMaterialOption.Text = "Material option";
            this.cbMaterialOption.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(334, 52);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(56, 17);
            this.checkBox1.TabIndex = 83;
            this.checkBox1.Text = "Active";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // cCheck
            // 
            this.cCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cCheck.DataPropertyName = "check";
            this.cCheck.HeaderText = "";
            this.cCheck.MinimumWidth = 25;
            this.cCheck.Name = "cCheck";
            this.cCheck.Width = 25;
            // 
            // cBaseMaterial
            // 
            this.cBaseMaterial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cBaseMaterial.DataPropertyName = "system_type";
            this.cBaseMaterial.HeaderText = "Systemtype";
            this.cBaseMaterial.MinimumWidth = 130;
            this.cBaseMaterial.Name = "cBaseMaterial";
            this.cBaseMaterial.ReadOnly = true;
            this.cBaseMaterial.Width = 130;
            // 
            // cMaterialType
            // 
            this.cMaterialType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cMaterialType.DataPropertyName = "material_type";
            this.cMaterialType.HeaderText = "Material";
            this.cMaterialType.MinimumWidth = 130;
            this.cMaterialType.Name = "cMaterialType";
            this.cMaterialType.Width = 130;
            // 
            // cWedgeOption
            // 
            this.cWedgeOption.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cWedgeOption.DataPropertyName = "has_wedge_option";
            this.cWedgeOption.HeaderText = "Wedge option";
            this.cWedgeOption.Name = "cWedgeOption";
            this.cWedgeOption.ReadOnly = true;
            this.cWedgeOption.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cWedgeOption.Width = 99;
            // 
            // cMaterialOption
            // 
            this.cMaterialOption.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cMaterialOption.DataPropertyName = "has_material_option";
            this.cMaterialOption.HeaderText = "Material option";
            this.cMaterialOption.Name = "cMaterialOption";
            this.cMaterialOption.ReadOnly = true;
            this.cMaterialOption.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cMaterialOption.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cMaterialOption.Width = 101;
            // 
            // cDefaultWedge
            // 
            this.cDefaultWedge.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cDefaultWedge.DataPropertyName = "wedge_type";
            this.cDefaultWedge.HeaderText = "Default wedge";
            this.cDefaultWedge.Name = "cDefaultWedge";
            this.cDefaultWedge.Width = 101;
            // 
            // cArticleNumber
            // 
            this.cArticleNumber.DataPropertyName = "article_number";
            this.cArticleNumber.HeaderText = "Article #";
            this.cArticleNumber.Name = "cArticleNumber";
            this.cArticleNumber.ReadOnly = true;
            // 
            // cSpecial
            // 
            this.cSpecial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cSpecial.DataPropertyName = "is_special";
            this.cSpecial.HeaderText = "Special";
            this.cSpecial.Name = "cSpecial";
            this.cSpecial.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cSpecial.Width = 67;
            // 
            // cActive
            // 
            this.cActive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cActive.DataPropertyName = "is_active";
            this.cActive.HeaderText = "Active";
            this.cActive.MinimumWidth = 6;
            this.cActive.Name = "cActive";
            this.cActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cActive.Width = 62;
            // 
            // cRuleVersion
            // 
            this.cRuleVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cRuleVersion.DataPropertyName = "rule_version";
            this.cRuleVersion.HeaderText = "Rule version";
            this.cRuleVersion.MinimumWidth = 6;
            this.cRuleVersion.Name = "cRuleVersion";
            this.cRuleVersion.Width = 91;
            // 
            // cFill
            // 
            this.cFill.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cFill.HeaderText = "";
            this.cFill.MinimumWidth = 6;
            this.cFill.Name = "cFill";
            // 
            // editWedgeOptionRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 461);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cbMaterialOption);
            this.Controls.Add(this.cbWedgeOption);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbMaterialType);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreateSQL);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudVersion);
            this.Controls.Add(this.cbSpecial);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSystemType);
            this.Controls.Add(this.cbWedge);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(980, 9999);
            this.MinimumSize = new System.Drawing.Size(980, 500);
            this.Name = "editWedgeOptionRule";
            this.Text = "Edit Wedge Option Rule";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.editWedgeOptionRule_FormClosing);
            this.Load += new System.EventHandler(this.editWedgeOptionRule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVersion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCreateSQL;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudVersion;
        private System.Windows.Forms.CheckBox cbSpecial;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSystemType;
        private System.Windows.Forms.ComboBox cbWedge;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbMaterialType;
        private System.Windows.Forms.CheckBox cbWedgeOption;
        private System.Windows.Forms.CheckBox cbMaterialOption;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBaseMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaterialType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cWedgeOption;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cMaterialOption;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDefaultWedge;
        private System.Windows.Forms.DataGridViewTextBoxColumn cArticleNumber;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cSpecial;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRuleVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFill;
    }
}