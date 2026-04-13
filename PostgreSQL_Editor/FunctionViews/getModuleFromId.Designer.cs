namespace PostgreSQL_Editor.FunctionViews
{
    partial class getModuleFromId
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbModule = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbId = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.lbHoleTo = new System.Windows.Forms.Label();
            this.lbHoleFrom = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lbHeight = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbWidth = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbVar = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.cNbr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbFiller = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbCables = new System.Windows.Forms.Label();
            this.lbOversized = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbArticleNumber = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbWeight = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbPackaging = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // cbModule
            // 
            this.cbModule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbModule.FormattingEnabled = true;
            this.cbModule.Location = new System.Drawing.Point(12, 25);
            this.cbModule.Name = "cbModule";
            this.cbModule.Size = new System.Drawing.Size(353, 21);
            this.cbModule.TabIndex = 0;
            this.cbModule.SelectedIndexChanged += new System.EventHandler(this.cbModule_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Module";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 61);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Name:";
            // 
            // lbId
            // 
            this.lbId.AutoSize = true;
            this.lbId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbId.ForeColor = System.Drawing.Color.Navy;
            this.lbId.Location = new System.Drawing.Point(113, 61);
            this.lbId.Name = "lbId";
            this.lbId.Size = new System.Drawing.Size(20, 13);
            this.lbId.TabIndex = 4;
            this.lbId.Text = "ID";
            this.lbId.DoubleClick += new System.EventHandler(this.lbId_DoubleClick);
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.ForeColor = System.Drawing.Color.Navy;
            this.lbName.Location = new System.Drawing.Point(113, 86);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(42, 13);
            this.lbName.TabIndex = 5;
            this.lbName.Text = "NAME";
            // 
            // lbHoleTo
            // 
            this.lbHoleTo.AutoSize = true;
            this.lbHoleTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHoleTo.ForeColor = System.Drawing.Color.Navy;
            this.lbHoleTo.Location = new System.Drawing.Point(113, 236);
            this.lbHoleTo.Name = "lbHoleTo";
            this.lbHoleTo.Size = new System.Drawing.Size(71, 13);
            this.lbHoleTo.TabIndex = 9;
            this.lbHoleTo.Text = "DIAMETER";
            // 
            // lbHoleFrom
            // 
            this.lbHoleFrom.AutoSize = true;
            this.lbHoleFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHoleFrom.ForeColor = System.Drawing.Color.Navy;
            this.lbHoleFrom.Location = new System.Drawing.Point(113, 211);
            this.lbHoleFrom.Name = "lbHoleFrom";
            this.lbHoleFrom.Size = new System.Drawing.Size(71, 13);
            this.lbHoleFrom.TabIndex = 8;
            this.lbHoleFrom.Text = "DIAMETER";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 236);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Hole dimater to:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 211);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Hole diameter from:";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(290, 506);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbHeight
            // 
            this.lbHeight.AutoSize = true;
            this.lbHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeight.ForeColor = System.Drawing.Color.Navy;
            this.lbHeight.Location = new System.Drawing.Point(113, 136);
            this.lbHeight.Name = "lbHeight";
            this.lbHeight.Size = new System.Drawing.Size(54, 13);
            this.lbHeight.TabIndex = 12;
            this.lbHeight.Text = "HEIGHT";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 136);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Height:";
            // 
            // lbWidth
            // 
            this.lbWidth.AutoSize = true;
            this.lbWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWidth.ForeColor = System.Drawing.Color.Navy;
            this.lbWidth.Location = new System.Drawing.Point(113, 161);
            this.lbWidth.Name = "lbWidth";
            this.lbWidth.Size = new System.Drawing.Size(49, 13);
            this.lbWidth.TabIndex = 14;
            this.lbWidth.Text = "WIDTH";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 161);
            this.label12.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Width:";
            // 
            // lbVar
            // 
            this.lbVar.AutoSize = true;
            this.lbVar.Location = new System.Drawing.Point(9, 361);
            this.lbVar.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.lbVar.Name = "lbVar";
            this.lbVar.Size = new System.Drawing.Size(56, 13);
            this.lbVar.TabIndex = 16;
            this.lbVar.Text = "Variations:";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cNbr,
            this.cFrom,
            this.cMax,
            this.cColor,
            this.cc});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgv.Location = new System.Drawing.Point(116, 356);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(246, 136);
            this.dgv.TabIndex = 17;
            this.dgv.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_CellPainting);
            // 
            // cNbr
            // 
            this.cNbr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cNbr.DataPropertyName = "lfnr";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cNbr.DefaultCellStyle = dataGridViewCellStyle8;
            this.cNbr.HeaderText = "#";
            this.cNbr.MinimumWidth = 25;
            this.cNbr.Name = "cNbr";
            this.cNbr.ReadOnly = true;
            this.cNbr.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cNbr.Width = 25;
            // 
            // cFrom
            // 
            this.cFrom.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cFrom.DataPropertyName = "holeDiameterFrom";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cFrom.DefaultCellStyle = dataGridViewCellStyle9;
            this.cFrom.HeaderText = "min";
            this.cFrom.Name = "cFrom";
            this.cFrom.ReadOnly = true;
            this.cFrom.Width = 53;
            // 
            // cMax
            // 
            this.cMax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cMax.DataPropertyName = "holeDiameterTo";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cMax.DefaultCellStyle = dataGridViewCellStyle10;
            this.cMax.HeaderText = "max";
            this.cMax.Name = "cMax";
            this.cMax.ReadOnly = true;
            this.cMax.Width = 53;
            // 
            // cColor
            // 
            this.cColor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cColor.DataPropertyName = "colorhex";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cColor.DefaultCellStyle = dataGridViewCellStyle11;
            this.cColor.HeaderText = "Color";
            this.cColor.Name = "cColor";
            this.cColor.ReadOnly = true;
            this.cColor.Width = 67;
            // 
            // cc
            // 
            this.cc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cc.HeaderText = "";
            this.cc.MinimumWidth = 25;
            this.cc.Name = "cc";
            this.cc.ReadOnly = true;
            this.cc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cc.Width = 25;
            // 
            // lbFiller
            // 
            this.lbFiller.AutoSize = true;
            this.lbFiller.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFiller.ForeColor = System.Drawing.Color.Navy;
            this.lbFiller.Location = new System.Drawing.Point(113, 261);
            this.lbFiller.Name = "lbFiller";
            this.lbFiller.Size = new System.Drawing.Size(25, 13);
            this.lbFiller.TabIndex = 23;
            this.lbFiller.Text = "NO";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 261);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Is filler:";
            // 
            // lbCables
            // 
            this.lbCables.AutoSize = true;
            this.lbCables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCables.ForeColor = System.Drawing.Color.Navy;
            this.lbCables.Location = new System.Drawing.Point(113, 311);
            this.lbCables.Name = "lbCables";
            this.lbCables.Size = new System.Drawing.Size(14, 13);
            this.lbCables.TabIndex = 21;
            this.lbCables.Text = "0";
            // 
            // lbOversized
            // 
            this.lbOversized.AutoSize = true;
            this.lbOversized.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOversized.ForeColor = System.Drawing.Color.Navy;
            this.lbOversized.Location = new System.Drawing.Point(113, 286);
            this.lbOversized.Name = "lbOversized";
            this.lbOversized.Size = new System.Drawing.Size(25, 13);
            this.lbOversized.TabIndex = 20;
            this.lbOversized.Text = "NO";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 311);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Max. cables:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 286);
            this.label13.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 13);
            this.label13.TabIndex = 18;
            this.label13.Text = "Is oversized:";
            // 
            // lbArticleNumber
            // 
            this.lbArticleNumber.AutoSize = true;
            this.lbArticleNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbArticleNumber.ForeColor = System.Drawing.Color.Navy;
            this.lbArticleNumber.Location = new System.Drawing.Point(113, 111);
            this.lbArticleNumber.Name = "lbArticleNumber";
            this.lbArticleNumber.Size = new System.Drawing.Size(119, 13);
            this.lbArticleNumber.TabIndex = 25;
            this.lbArticleNumber.Text = "ARTICLE_NUMBER";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 111);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Article number:";
            // 
            // lbWeight
            // 
            this.lbWeight.AutoSize = true;
            this.lbWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWeight.ForeColor = System.Drawing.Color.Navy;
            this.lbWeight.Location = new System.Drawing.Point(113, 186);
            this.lbWeight.Name = "lbWeight";
            this.lbWeight.Size = new System.Drawing.Size(57, 13);
            this.lbWeight.TabIndex = 27;
            this.lbWeight.Text = "WEIGHT";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 186);
            this.label14.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 13);
            this.label14.TabIndex = 26;
            this.label14.Text = "Weight:";
            // 
            // lbPackaging
            // 
            this.lbPackaging.AutoSize = true;
            this.lbPackaging.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPackaging.ForeColor = System.Drawing.Color.Navy;
            this.lbPackaging.Location = new System.Drawing.Point(113, 336);
            this.lbPackaging.Name = "lbPackaging";
            this.lbPackaging.Size = new System.Drawing.Size(78, 13);
            this.lbPackaging.TabIndex = 29;
            this.lbPackaging.Text = "PACKAGING";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 336);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "Packaging:";
            // 
            // getModuleFromId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 541);
            this.Controls.Add(this.lbPackaging);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lbWeight);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lbArticleNumber);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbFiller);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbCables);
            this.Controls.Add(this.lbOversized);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.lbVar);
            this.Controls.Add(this.lbWidth);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lbHeight);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbHoleTo);
            this.Controls.Add(this.lbHoleFrom);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.lbId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbModule);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "getModuleFromId";
            this.Text = "Get Module from id";
            this.Load += new System.EventHandler(this.getModuleFromId_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbModule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbId;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbHoleTo;
        private System.Windows.Forms.Label lbHoleFrom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lbHeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbWidth;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbVar;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label lbFiller;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbCables;
        private System.Windows.Forms.Label lbOversized;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNbr;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn cColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn cc;
        private System.Windows.Forms.Label lbArticleNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbWeight;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbPackaging;
        private System.Windows.Forms.Label label9;
    }
}