namespace PostgreSQL_Editor.EditRules
{
    partial class editSleeve_rule
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
            this.label3 = new System.Windows.Forms.Label();
            this.cbFrameType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSystemType = new System.Windows.Forms.ComboBox();
            this.cbBaseMaterial = new System.Windows.Forms.ComboBox();
            this.cbMandatory = new System.Windows.Forms.CheckBox();
            this.cCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cBaseMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSystemType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFrametype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMandatory = new System.Windows.Forms.DataGridViewCheckBoxColumn();
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
            this.lbInfo.Location = new System.Drawing.Point(12, 406);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(744, 20);
            this.lbInfo.TabIndex = 47;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(681, 429);
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
            this.cSystemType,
            this.cFrametype,
            this.cMandatory,
            this.cActive,
            this.cRuleVersion,
            this.cFill});
            this.dgv.Location = new System.Drawing.Point(12, 52);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 51;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(744, 351);
            this.dgv.TabIndex = 43;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(608, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 15);
            this.label5.TabIndex = 42;
            this.label5.Text = "Rule version:";
            // 
            // nudVersion
            // 
            this.nudVersion.Location = new System.Drawing.Point(611, 27);
            this.nudVersion.Name = "nudVersion";
            this.nudVersion.Size = new System.Drawing.Size(63, 20);
            this.nudVersion.TabIndex = 41;
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
            this.cbActive.Location = new System.Drawing.Point(549, 28);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(60, 19);
            this.cbActive.TabIndex = 40;
            this.cbActive.Text = "Active";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(683, 24);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 39;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(201, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 15);
            this.label3.TabIndex = 36;
            this.label3.Text = "Frametype:";
            // 
            // cbFrameType
            // 
            this.cbFrameType.FormattingEnabled = true;
            this.cbFrameType.Location = new System.Drawing.Point(204, 25);
            this.cbFrameType.Name = "cbFrameType";
            this.cbFrameType.Size = new System.Drawing.Size(183, 21);
            this.cbFrameType.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 34;
            this.label2.Text = "Systemtype:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 15);
            this.label1.TabIndex = 33;
            this.label1.Text = "Basematerial:";
            // 
            // cbSystemType
            // 
            this.cbSystemType.FormattingEnabled = true;
            this.cbSystemType.Location = new System.Drawing.Point(108, 25);
            this.cbSystemType.Name = "cbSystemType";
            this.cbSystemType.Size = new System.Drawing.Size(90, 21);
            this.cbSystemType.TabIndex = 32;
            // 
            // cbBaseMaterial
            // 
            this.cbBaseMaterial.FormattingEnabled = true;
            this.cbBaseMaterial.Location = new System.Drawing.Point(12, 25);
            this.cbBaseMaterial.Name = "cbBaseMaterial";
            this.cbBaseMaterial.Size = new System.Drawing.Size(90, 21);
            this.cbBaseMaterial.TabIndex = 31;
            this.cbBaseMaterial.SelectedIndexChanged += new System.EventHandler(this.cbBaseMaterial_SelectedIndexChanged);
            // 
            // cbMandatory
            // 
            this.cbMandatory.AutoSize = true;
            this.cbMandatory.Checked = true;
            this.cbMandatory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMandatory.Location = new System.Drawing.Point(411, 28);
            this.cbMandatory.Name = "cbMandatory";
            this.cbMandatory.Size = new System.Drawing.Size(87, 19);
            this.cbMandatory.TabIndex = 48;
            this.cbMandatory.Text = "Mandatory";
            this.cbMandatory.UseVisualStyleBackColor = true;
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
            this.cBaseMaterial.DataPropertyName = "base_material";
            this.cBaseMaterial.HeaderText = "Basematerial";
            this.cBaseMaterial.MinimumWidth = 110;
            this.cBaseMaterial.Name = "cBaseMaterial";
            this.cBaseMaterial.ReadOnly = true;
            this.cBaseMaterial.Width = 110;
            // 
            // cSystemType
            // 
            this.cSystemType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cSystemType.DataPropertyName = "system_type";
            this.cSystemType.HeaderText = "Systemtype";
            this.cSystemType.MinimumWidth = 110;
            this.cSystemType.Name = "cSystemType";
            this.cSystemType.ReadOnly = true;
            this.cSystemType.Width = 110;
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
            // cMandatory
            // 
            this.cMandatory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cMandatory.DataPropertyName = "is_mandatory";
            this.cMandatory.HeaderText = "Mandatory";
            this.cMandatory.MinimumWidth = 6;
            this.cMandatory.Name = "cMandatory";
            this.cMandatory.Width = 71;
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
            // editFRSL_rule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 461);
            this.Controls.Add(this.cbMandatory);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreateSQL);
            this.Controls.Add(this.dgv);
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
            this.MaximumSize = new System.Drawing.Size(785, 9999);
            this.MinimumSize = new System.Drawing.Size(785, 500);
            this.Name = "editFRSL_rule";
            this.Text = "Edit Frame-Sleeve Rules";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditSystemTypeFrameType_FormClosing);
            this.Load += new System.EventHandler(this.EditSleeveRule_Load);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFrameType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSystemType;
        private System.Windows.Forms.ComboBox cbBaseMaterial;
        private System.Windows.Forms.CheckBox cbMandatory;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBaseMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSystemType;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFrametype;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cMandatory;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRuleVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFill;
    }
}