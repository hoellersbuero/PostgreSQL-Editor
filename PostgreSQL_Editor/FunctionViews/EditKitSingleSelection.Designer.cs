namespace PostgreSQL_Editor.FunctionViews
{
    partial class EditKitSingleSelection
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.lbInfo = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCreateSQL = new System.Windows.Forms.Button();
            this.cBasematerial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSystemtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFrametype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFramematerial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAvailable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cKitSingle = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cNameEdit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cSpecialCoating = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cBasematerial,
            this.cSystemtype,
            this.cFrametype,
            this.cFramematerial,
            this.cAvailable,
            this.cKitSingle,
            this.cNameEdit,
            this.cSpecialCoating,
            this.Column1});
            this.dgv.Location = new System.Drawing.Point(12, 12);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(697, 370);
            this.dgv.TabIndex = 0;
            // 
            // lbInfo
            // 
            this.lbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInfo.Location = new System.Drawing.Point(12, 385);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(697, 21);
            this.lbInfo.TabIndex = 12;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(627, 415);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCreateSQL
            // 
            this.btnCreateSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateSQL.Enabled = false;
            this.btnCreateSQL.Location = new System.Drawing.Point(12, 415);
            this.btnCreateSQL.Name = "btnCreateSQL";
            this.btnCreateSQL.Size = new System.Drawing.Size(75, 23);
            this.btnCreateSQL.TabIndex = 13;
            this.btnCreateSQL.Text = "Create SQL";
            this.btnCreateSQL.UseVisualStyleBackColor = true;
            this.btnCreateSQL.Click += new System.EventHandler(this.btnCreateSQL_Click);
            // 
            // cBasematerial
            // 
            this.cBasematerial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cBasematerial.DataPropertyName = "base_material";
            this.cBasematerial.HeaderText = "Base material";
            this.cBasematerial.Name = "cBasematerial";
            this.cBasematerial.ReadOnly = true;
            this.cBasematerial.Width = 95;
            // 
            // cSystemtype
            // 
            this.cSystemtype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cSystemtype.DataPropertyName = "system_type";
            this.cSystemtype.HeaderText = "System type";
            this.cSystemtype.Name = "cSystemtype";
            this.cSystemtype.ReadOnly = true;
            this.cSystemtype.Width = 89;
            // 
            // cFrametype
            // 
            this.cFrametype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cFrametype.DataPropertyName = "frame_type";
            this.cFrametype.HeaderText = "Frame type";
            this.cFrametype.Name = "cFrametype";
            this.cFrametype.ReadOnly = true;
            this.cFrametype.Width = 84;
            // 
            // cFramematerial
            // 
            this.cFramematerial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cFramematerial.DataPropertyName = "frame_material_code";
            this.cFramematerial.HeaderText = "Frame material";
            this.cFramematerial.Name = "cFramematerial";
            this.cFramematerial.ReadOnly = true;
            // 
            // cAvailable
            // 
            this.cAvailable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cAvailable.DataPropertyName = "is_available";
            this.cAvailable.HeaderText = "Available";
            this.cAvailable.Name = "cAvailable";
            this.cAvailable.Width = 56;
            // 
            // cKitSingle
            // 
            this.cKitSingle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cKitSingle.DataPropertyName = "kit_single_selectable";
            this.cKitSingle.HeaderText = "Kit/Single";
            this.cKitSingle.Name = "cKitSingle";
            this.cKitSingle.Width = 59;
            // 
            // cNameEdit
            // 
            this.cNameEdit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cNameEdit.DataPropertyName = "name_editable";
            this.cNameEdit.HeaderText = "Name editable";
            this.cNameEdit.Name = "cNameEdit";
            this.cNameEdit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cNameEdit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // cSpecialCoating
            // 
            this.cSpecialCoating.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cSpecialCoating.DataPropertyName = "special_coating_editable";
            this.cSpecialCoating.HeaderText = "Special coating";
            this.cSpecialCoating.Name = "cSpecialCoating";
            this.cSpecialCoating.Width = 78;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EditKitSingleSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 450);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreateSQL);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.dgv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "EditKitSingleSelection";
            this.Text = "EditKitSingleSelection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditKitSingleSelection_FormClosing);
            this.Load += new System.EventHandler(this.EditKitSingleSelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCreateSQL;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBasematerial;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSystemtype;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFrametype;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFramematerial;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cAvailable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cKitSingle;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cNameEdit;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cSpecialCoating;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}