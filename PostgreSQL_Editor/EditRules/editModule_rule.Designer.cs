namespace PostgreSQL_Editor.EditRules
{
    partial class editModule_rule
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
            this.label5 = new System.Windows.Forms.Label();
            this.nudVersion = new System.Windows.Forms.NumericUpDown();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbMaterialType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFrameType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSystemType = new System.Windows.Forms.ComboBox();
            this.cbModule = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCreateSQL = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.nudPriority = new System.Windows.Forms.NumericUpDown();
            this.lbInfo = new System.Windows.Forms.Label();
            this.cCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cBaseMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSystemType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFrametype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaterialType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cSpecial = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cRuleVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.nudVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPriority)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(647, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "Rule version:";
            // 
            // nudVersion
            // 
            this.nudVersion.Location = new System.Drawing.Point(650, 26);
            this.nudVersion.Name = "nudVersion";
            this.nudVersion.Size = new System.Drawing.Size(63, 20);
            this.nudVersion.TabIndex = 40;
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
            this.cbActive.Location = new System.Drawing.Point(521, 27);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(56, 17);
            this.cbActive.TabIndex = 39;
            this.cbActive.Text = "Active";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(722, 23);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 38;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(263, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Materialtype:";
            // 
            // cbMaterialType
            // 
            this.cbMaterialType.FormattingEnabled = true;
            this.cbMaterialType.Location = new System.Drawing.Point(266, 25);
            this.cbMaterialType.Name = "cbMaterialType";
            this.cbMaterialType.Size = new System.Drawing.Size(84, 21);
            this.cbMaterialType.TabIndex = 36;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Frametype:";
            // 
            // cbFrameType
            // 
            this.cbFrameType.FormattingEnabled = true;
            this.cbFrameType.Location = new System.Drawing.Point(139, 25);
            this.cbFrameType.Name = "cbFrameType";
            this.cbFrameType.Size = new System.Drawing.Size(121, 21);
            this.cbFrameType.TabIndex = 34;
            this.cbFrameType.SelectedIndexChanged += new System.EventHandler(this.cbFrameType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Systemtype:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(353, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Module:";
            // 
            // cbSystemType
            // 
            this.cbSystemType.FormattingEnabled = true;
            this.cbSystemType.Location = new System.Drawing.Point(12, 25);
            this.cbSystemType.Name = "cbSystemType";
            this.cbSystemType.Size = new System.Drawing.Size(121, 21);
            this.cbSystemType.TabIndex = 31;
            this.cbSystemType.SelectedIndexChanged += new System.EventHandler(this.cbSystemType_SelectedIndexChanged);
            // 
            // cbModule
            // 
            this.cbModule.FormattingEnabled = true;
            this.cbModule.Location = new System.Drawing.Point(356, 25);
            this.cbModule.Name = "cbModule";
            this.cbModule.Size = new System.Drawing.Size(158, 21);
            this.cbModule.TabIndex = 30;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(721, 429);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 45;
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
            this.btnCreateSQL.TabIndex = 44;
            this.btnCreateSQL.Text = "Create SQL";
            this.btnCreateSQL.UseVisualStyleBackColor = true;
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
            this.cMaterialType,
            this.cActive,
            this.cSpecial,
            this.cRuleVersion,
            this.cFill});
            this.dgv.Location = new System.Drawing.Point(12, 52);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(784, 353);
            this.dgv.TabIndex = 43;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(578, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 47;
            this.label6.Text = "Priority:";
            // 
            // nudPriority
            // 
            this.nudPriority.Location = new System.Drawing.Point(581, 26);
            this.nudPriority.Name = "nudPriority";
            this.nudPriority.Size = new System.Drawing.Size(63, 20);
            this.nudPriority.TabIndex = 46;
            this.nudPriority.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lbInfo
            // 
            this.lbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInfo.Location = new System.Drawing.Point(12, 408);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(784, 18);
            this.lbInfo.TabIndex = 48;
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
            // cSystemType
            // 
            this.cSystemType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cSystemType.DataPropertyName = "frame_type";
            this.cSystemType.HeaderText = "Frametype";
            this.cSystemType.MinimumWidth = 130;
            this.cSystemType.Name = "cSystemType";
            this.cSystemType.ReadOnly = true;
            this.cSystemType.Width = 130;
            // 
            // cFrametype
            // 
            this.cFrametype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cFrametype.DataPropertyName = "Matrial_type";
            this.cFrametype.HeaderText = "Materialtype";
            this.cFrametype.MinimumWidth = 130;
            this.cFrametype.Name = "cFrametype";
            this.cFrametype.Width = 130;
            // 
            // cMaterialType
            // 
            this.cMaterialType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cMaterialType.DataPropertyName = "module";
            this.cMaterialType.HeaderText = "Module";
            this.cMaterialType.MinimumWidth = 130;
            this.cMaterialType.Name = "cMaterialType";
            this.cMaterialType.Width = 130;
            // 
            // cActive
            // 
            this.cActive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cActive.DataPropertyName = "is_active";
            this.cActive.HeaderText = "Active";
            this.cActive.Name = "cActive";
            this.cActive.Width = 43;
            // 
            // cSpecial
            // 
            this.cSpecial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cSpecial.DataPropertyName = "priority";
            this.cSpecial.HeaderText = "Priority";
            this.cSpecial.Name = "cSpecial";
            this.cSpecial.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cSpecial.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cSpecial.Width = 63;
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
            // editModule_rule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 461);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudPriority);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreateSQL);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudVersion);
            this.Controls.Add(this.cbActive);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbMaterialType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbFrameType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSystemType);
            this.Controls.Add(this.cbModule);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(825, 9999);
            this.MinimumSize = new System.Drawing.Size(825, 500);
            this.Name = "editModule_rule";
            this.Text = "Edit Systemtype-Frametype-Materialtype-Module Rules";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.editModule_rule_FormClosing);
            this.Load += new System.EventHandler(this.editModule_rule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPriority)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudVersion;
        private System.Windows.Forms.CheckBox cbActive;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbMaterialType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFrameType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSystemType;
        private System.Windows.Forms.ComboBox cbModule;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCreateSQL;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudPriority;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBaseMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSystemType;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFrametype;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaterialType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cActive;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cSpecial;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRuleVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFill;
    }
}