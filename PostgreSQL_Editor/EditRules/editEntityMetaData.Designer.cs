namespace PostgreSQL_Editor.EditRules
{
    partial class editEntityMetaData
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
            this.btnClose = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.cCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cBaseMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSystemType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFrametype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaterialType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cRuleVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cJSON = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(713, 415);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 83;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
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
            this.cRuleVersion,
            this.cJSON,
            this.cFill});
            this.dgv.Location = new System.Drawing.Point(12, 38);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(776, 353);
            this.dgv.TabIndex = 82;
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
            this.cBaseMaterial.DataPropertyName = "entity_name";
            this.cBaseMaterial.HeaderText = "Entityname:";
            this.cBaseMaterial.MinimumWidth = 130;
            this.cBaseMaterial.Name = "cBaseMaterial";
            this.cBaseMaterial.ReadOnly = true;
            this.cBaseMaterial.Width = 130;
            // 
            // cSystemType
            // 
            this.cSystemType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cSystemType.DataPropertyName = "entity_type";
            this.cSystemType.HeaderText = "Entity Type";
            this.cSystemType.MinimumWidth = 130;
            this.cSystemType.Name = "cSystemType";
            this.cSystemType.ReadOnly = true;
            this.cSystemType.Width = 130;
            // 
            // cFrametype
            // 
            this.cFrametype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cFrametype.DataPropertyName = "metadata_key";
            this.cFrametype.HeaderText = "Key";
            this.cFrametype.MinimumWidth = 130;
            this.cFrametype.Name = "cFrametype";
            this.cFrametype.ReadOnly = true;
            this.cFrametype.Width = 130;
            // 
            // cMaterialType
            // 
            this.cMaterialType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cMaterialType.DataPropertyName = "number_value";
            this.cMaterialType.HeaderText = "Number";
            this.cMaterialType.MinimumWidth = 130;
            this.cMaterialType.Name = "cMaterialType";
            this.cMaterialType.Width = 130;
            // 
            // cActive
            // 
            this.cActive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cActive.DataPropertyName = "bool_value";
            this.cActive.HeaderText = "Boolean";
            this.cActive.Name = "cActive";
            this.cActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cActive.Width = 71;
            // 
            // cRuleVersion
            // 
            this.cRuleVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cRuleVersion.DataPropertyName = "string_value";
            this.cRuleVersion.HeaderText = "String";
            this.cRuleVersion.Name = "cRuleVersion";
            this.cRuleVersion.Width = 59;
            // 
            // cJSON
            // 
            this.cJSON.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cJSON.DataPropertyName = "json_value";
            this.cJSON.HeaderText = "JSON";
            this.cJSON.Name = "cJSON";
            this.cJSON.Width = 60;
            // 
            // cFill
            // 
            this.cFill.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cFill.HeaderText = "";
            this.cFill.Name = "cFill";
            // 
            // editEntityMetaData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "editEntityMetaData";
            this.Text = "Edit Entity-Metadata";
            this.Load += new System.EventHandler(this.editEntityMetaData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBaseMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSystemType;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFrametype;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaterialType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRuleVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn cJSON;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFill;
    }
}