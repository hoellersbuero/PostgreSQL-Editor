namespace PostgreSQL_Editor.EditRules
{
    partial class editWedge_rule
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
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSystemType = new System.Windows.Forms.ComboBox();
            this.cbWedge = new System.Windows.Forms.ComboBox();
            this.cCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cBaseMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaterialType = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.lbInfo.Size = new System.Drawing.Size(716, 18);
            this.lbInfo.TabIndex = 66;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(654, 429);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 63;
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
            this.btnCreateSQL.TabIndex = 62;
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
            this.cActive,
            this.cRuleVersion,
            this.cFill});
            this.dgv.Location = new System.Drawing.Point(13, 52);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 51;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(716, 353);
            this.dgv.TabIndex = 61;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(581, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 15);
            this.label5.TabIndex = 60;
            this.label5.Text = "Rule version:";
            // 
            // nudVersion
            // 
            this.nudVersion.Location = new System.Drawing.Point(584, 26);
            this.nudVersion.Name = "nudVersion";
            this.nudVersion.Size = new System.Drawing.Size(63, 20);
            this.nudVersion.TabIndex = 59;
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
            this.cbActive.Location = new System.Drawing.Point(522, 27);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(60, 19);
            this.cbActive.TabIndex = 58;
            this.cbActive.Text = "Active";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(656, 23);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 57;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 52;
            this.label2.Text = "Systemtype:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(137, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 51;
            this.label1.Text = "Wedge:";
            // 
            // cbSystemType
            // 
            this.cbSystemType.FormattingEnabled = true;
            this.cbSystemType.Location = new System.Drawing.Point(13, 25);
            this.cbSystemType.Name = "cbSystemType";
            this.cbSystemType.Size = new System.Drawing.Size(121, 21);
            this.cbSystemType.TabIndex = 50;
            this.cbSystemType.SelectedIndexChanged += new System.EventHandler(this.cbSystemType_SelectedIndexChanged);
            // 
            // cbWedge
            // 
            this.cbWedge.FormattingEnabled = true;
            this.cbWedge.Location = new System.Drawing.Point(140, 25);
            this.cbWedge.Name = "cbWedge";
            this.cbWedge.Size = new System.Drawing.Size(158, 21);
            this.cbWedge.TabIndex = 49;
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
            this.cMaterialType.DataPropertyName = "wedge_type";
            this.cMaterialType.HeaderText = "Wedge";
            this.cMaterialType.MinimumWidth = 130;
            this.cMaterialType.Name = "cMaterialType";
            this.cMaterialType.Width = 130;
            // 
            // cActive
            // 
            this.cActive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cActive.DataPropertyName = "is_active";
            this.cActive.HeaderText = "Active";
            this.cActive.MinimumWidth = 6;
            this.cActive.Name = "cActive";
            this.cActive.Width = 44;
            // 
            // cRuleVersion
            // 
            this.cRuleVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cRuleVersion.DataPropertyName = "rule_version";
            this.cRuleVersion.HeaderText = "Rule version";
            this.cRuleVersion.MinimumWidth = 6;
            this.cRuleVersion.Name = "cRuleVersion";
            this.cRuleVersion.Width = 104;
            // 
            // cFill
            // 
            this.cFill.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cFill.HeaderText = "";
            this.cFill.MinimumWidth = 6;
            this.cFill.Name = "cFill";
            // 
            // editWedge_rule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 461);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreateSQL);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudVersion);
            this.Controls.Add(this.cbActive);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSystemType);
            this.Controls.Add(this.cbWedge);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(757, 9999);
            this.MinimumSize = new System.Drawing.Size(757, 500);
            this.Name = "editWedge_rule";
            this.Text = "Edit Wedge Rules";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.editBMSTFTMT_rule_FormClosing);
            this.Load += new System.EventHandler(this.editWedge_rule_Load);
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
        private System.Windows.Forms.CheckBox cbActive;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSystemType;
        private System.Windows.Forms.ComboBox cbWedge;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBaseMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaterialType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRuleVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFill;
    }
}