namespace PostgreSQL_Editor
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSuccess = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbExecute = new System.Windows.Forms.ToolStripButton();
            this.tsbClear = new System.Windows.Forms.ToolStripButton();
            this.tsbLoadSQL = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveSQL = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExportExcel = new System.Windows.Forms.ToolStripButton();
            this.tsbShowDataTree = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbForeignKeys = new System.Windows.Forms.ToolStripButton();
            this.tsbCreateFKeys = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tbQuery = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbError = new System.Windows.Forms.Label();
            this.lbSQL = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFindIdx = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeselectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportSqlDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.lbContent = new System.Windows.Forms.Label();
            this.tpSchema = new System.Windows.Forms.TabPage();
            this.splitContainerSchema1 = new System.Windows.Forms.SplitContainer();
            this.dgvs = new System.Windows.Forms.DataGridView();
            this.splitContainerSchema2 = new System.Windows.Forms.SplitContainer();
            this.dgvsc = new System.Windows.Forms.DataGridView();
            this.dgvidx = new System.Windows.Forms.DataGridView();
            this.lInfoSchema = new System.Windows.Forms.Label();
            this.lbTableName = new System.Windows.Forms.Label();
            this.tpCreate = new System.Windows.Forms.TabPage();
            this.tbCreate = new System.Windows.Forms.RichTextBox();
            this.tpClass = new System.Windows.Forms.TabPage();
            this.TakeIt = new System.Windows.Forms.PictureBox();
            this.tbClassField = new System.Windows.Forms.RichTextBox();
            this.tpGuidSearch = new System.Windows.Forms.TabPage();
            this.dgvGuidSearch = new System.Windows.Forms.DataGridView();
            this.lbGuidSearch = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadSQLQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveSQLQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditBMST = new System.Windows.Forms.ToolStripMenuItem();
            this.editBMSTFT = new System.Windows.Forms.ToolStripMenuItem();
            this.editBMSTFTMT = new System.Windows.Forms.ToolStripMenuItem();
            this.editSTFTMTModule = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditSTFT = new System.Windows.Forms.ToolStripMenuItem();
            this.editSTFTMTWE = new System.Windows.Forms.ToolStripMenuItem();
            this.editFRSL = new System.Windows.Forms.ToolStripMenuItem();
            this.editFrameSleeveRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editFRST = new System.Windows.Forms.ToolStripMenuItem();
            this.editSLST = new System.Windows.Forms.ToolStripMenuItem();
            this.editEntityMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.functionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowTableColumnTree = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGetModuleFromID = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGetFrameFromID = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.cms.SuspendLayout();
            this.tpSchema.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSchema1)).BeginInit();
            this.splitContainerSchema1.Panel1.SuspendLayout();
            this.splitContainerSchema1.Panel2.SuspendLayout();
            this.splitContainerSchema1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSchema2)).BeginInit();
            this.splitContainerSchema2.Panel1.SuspendLayout();
            this.splitContainerSchema2.Panel2.SuspendLayout();
            this.splitContainerSchema2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvsc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvidx)).BeginInit();
            this.tpCreate.SuspendLayout();
            this.tpClass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TakeIt)).BeginInit();
            this.tpGuidSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGuidSearch)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslLabel1,
            this.tsslInfo,
            this.toolStripStatusLabel1,
            this.tsslSuccess,
            this.tslInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 481);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslLabel1
            // 
            this.tsslLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tsslLabel1.Name = "tsslLabel1";
            this.tsslLabel1.Size = new System.Drawing.Size(27, 17);
            this.tsslLabel1.Text = "DB:";
            // 
            // tsslInfo
            // 
            this.tsslInfo.ForeColor = System.Drawing.Color.Navy;
            this.tsslInfo.Name = "tsslInfo";
            this.tsslInfo.Size = new System.Drawing.Size(54, 17);
            this.tsslInfo.Text = "DBStatus";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(49, 17);
            this.toolStripStatusLabel1.Text = "QUERY:";
            // 
            // tsslSuccess
            // 
            this.tsslSuccess.ForeColor = System.Drawing.Color.Navy;
            this.tsslSuccess.Name = "tsslSuccess";
            this.tsslSuccess.Size = new System.Drawing.Size(47, 17);
            this.tsslSuccess.Text = "success";
            // 
            // tslInfo
            // 
            this.tslInfo.Name = "tslInfo";
            this.tslInfo.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.toolStripSeparator2,
            this.tsbSQL,
            this.tsbExecute,
            this.tsbClear,
            this.tsbLoadSQL,
            this.tsbSaveSQL,
            this.toolStripSeparator1,
            this.tsbExportExcel,
            this.tsbShowDataTree,
            this.toolStripSeparator3,
            this.tsbForeignKeys,
            this.tsbCreateFKeys});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 31);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Image = global::PostgreSQL_Editor.Properties.Resources.Refresh;
            this.tsbRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(28, 28);
            this.tsbRefresh.Text = "Refresh database";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbSQL
            // 
            this.tsbSQL.CheckOnClick = true;
            this.tsbSQL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSQL.Image = global::PostgreSQL_Editor.Properties.Resources.SQL;
            this.tsbSQL.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSQL.Name = "tsbSQL";
            this.tsbSQL.Size = new System.Drawing.Size(28, 28);
            this.tsbSQL.Text = "Open Query Input";
            this.tsbSQL.CheckedChanged += new System.EventHandler(this.tsbSQL_CheckedChanged);
            // 
            // tsbExecute
            // 
            this.tsbExecute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExecute.Image = global::PostgreSQL_Editor.Properties.Resources.Execute;
            this.tsbExecute.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExecute.Margin = new System.Windows.Forms.Padding(4, 1, 0, 2);
            this.tsbExecute.Name = "tsbExecute";
            this.tsbExecute.Size = new System.Drawing.Size(28, 28);
            this.tsbExecute.Text = "Execute SQL";
            this.tsbExecute.Click += new System.EventHandler(this.BtnExecute_Click);
            // 
            // tsbClear
            // 
            this.tsbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClear.Image = global::PostgreSQL_Editor.Properties.Resources.Delete;
            this.tsbClear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClear.Margin = new System.Windows.Forms.Padding(4, 1, 0, 2);
            this.tsbClear.Name = "tsbClear";
            this.tsbClear.Size = new System.Drawing.Size(30, 28);
            this.tsbClear.Text = "Clear SQL";
            this.tsbClear.Click += new System.EventHandler(this.tsbClear_Click);
            // 
            // tsbLoadSQL
            // 
            this.tsbLoadSQL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLoadSQL.Image = global::PostgreSQL_Editor.Properties.Resources.Load;
            this.tsbLoadSQL.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbLoadSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadSQL.Margin = new System.Windows.Forms.Padding(4, 1, 0, 2);
            this.tsbLoadSQL.Name = "tsbLoadSQL";
            this.tsbLoadSQL.Size = new System.Drawing.Size(29, 28);
            this.tsbLoadSQL.Text = "Load SQL Query";
            this.tsbLoadSQL.Click += new System.EventHandler(this.tsmiLoadSQLQuery_Click);
            // 
            // tsbSaveSQL
            // 
            this.tsbSaveSQL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveSQL.Image = global::PostgreSQL_Editor.Properties.Resources.Save;
            this.tsbSaveSQL.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSaveSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveSQL.Margin = new System.Windows.Forms.Padding(4, 1, 0, 2);
            this.tsbSaveSQL.Name = "tsbSaveSQL";
            this.tsbSaveSQL.Size = new System.Drawing.Size(28, 28);
            this.tsbSaveSQL.Text = "Save SQL Query";
            this.tsbSaveSQL.Click += new System.EventHandler(this.tsmiSaveSQLQuery_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbExportExcel
            // 
            this.tsbExportExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExportExcel.Image = global::PostgreSQL_Editor.Properties.Resources.ExportToExcel;
            this.tsbExportExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportExcel.Name = "tsbExportExcel";
            this.tsbExportExcel.Size = new System.Drawing.Size(44, 28);
            this.tsbExportExcel.Text = "Export content to excel";
            this.tsbExportExcel.Click += new System.EventHandler(this.tsbExportExcel_Click);
            // 
            // tsbShowDataTree
            // 
            this.tsbShowDataTree.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbShowDataTree.Image = global::PostgreSQL_Editor.Properties.Resources.TreeView;
            this.tsbShowDataTree.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbShowDataTree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowDataTree.Name = "tsbShowDataTree";
            this.tsbShowDataTree.Size = new System.Drawing.Size(28, 28);
            this.tsbShowDataTree.Text = "Show Tables and Columns";
            this.tsbShowDataTree.Click += new System.EventHandler(this.tsbShowDataTree_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbForeignKeys
            // 
            this.tsbForeignKeys.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbForeignKeys.Image = global::PostgreSQL_Editor.Properties.Resources.ForeignKey;
            this.tsbForeignKeys.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbForeignKeys.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbForeignKeys.Name = "tsbForeignKeys";
            this.tsbForeignKeys.Size = new System.Drawing.Size(28, 28);
            this.tsbForeignKeys.Text = "Get all foreign keys";
            this.tsbForeignKeys.Click += new System.EventHandler(this.tsbForeignKeys_Click);
            // 
            // tsbCreateFKeys
            // 
            this.tsbCreateFKeys.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCreateFKeys.Image = global::PostgreSQL_Editor.Properties.Resources.createConstraints;
            this.tsbCreateFKeys.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCreateFKeys.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCreateFKeys.Margin = new System.Windows.Forms.Padding(4, 1, 0, 2);
            this.tsbCreateFKeys.Name = "tsbCreateFKeys";
            this.tsbCreateFKeys.Size = new System.Drawing.Size(52, 28);
            this.tsbCreateFKeys.Text = "Create foreign keys with on delete cascade";
            this.tsbCreateFKeys.Click += new System.EventHandler(this.tsbCreateFKeys_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 55);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tbQuery);
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(800, 426);
            this.splitContainer2.SplitterDistance = 110;
            this.splitContainer2.TabIndex = 8;
            // 
            // tbQuery
            // 
            this.tbQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbQuery.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbQuery.Location = new System.Drawing.Point(0, 0);
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Size = new System.Drawing.Size(800, 90);
            this.tbQuery.TabIndex = 12;
            this.tbQuery.Text = "";
            this.tbQuery.TextChanged += new System.EventHandler(this.tbQuery_TextChanged);
            this.tbQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbQuery_KeyDown);
            this.tbQuery.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbQuery_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbError);
            this.panel1.Controls.Add(this.lbSQL);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 90);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 20);
            this.panel1.TabIndex = 10;
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbError.Location = new System.Drawing.Point(689, 0);
            this.lbError.Name = "lbError";
            this.lbError.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lbError.Size = new System.Drawing.Size(109, 15);
            this.lbError.TabIndex = 2;
            this.lbError.Text = "kdlkdlfgfgdfsdfsdsfsdf";
            this.lbError.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbSQL
            // 
            this.lbSQL.AutoSize = true;
            this.lbSQL.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbSQL.Location = new System.Drawing.Point(0, 0);
            this.lbSQL.Name = "lbSQL";
            this.lbSQL.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lbSQL.Size = new System.Drawing.Size(59, 15);
            this.lbSQL.TabIndex = 1;
            this.lbSQL.Text = "sddsfssfsdf";
            this.lbSQL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 312);
            this.splitContainer1.SplitterDistance = 213;
            this.splitContainer1.TabIndex = 10;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.ShowPlusMinus = false;
            this.treeView.Size = new System.Drawing.Size(213, 312);
            this.treeView.TabIndex = 7;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.DoubleClick += new System.EventHandler(this.treeView_DoubleClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tpSchema);
            this.tabControl1.Controls.Add(this.tpCreate);
            this.tabControl1.Controls.Add(this.tpClass);
            this.tabControl1.Controls.Add(this.tpGuidSearch);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(583, 312);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv);
            this.tabPage1.Controls.Add(this.lbContent);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(575, 284);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Content";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.ContextMenuStrip = this.cms;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 26);
            this.dgv.Margin = new System.Windows.Forms.Padding(0);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidth = 25;
            this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv.Size = new System.Drawing.Size(569, 255);
            this.dgv.TabIndex = 3;
            this.dgv.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_DataBindingComplete);
            this.dgv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dgv_KeyDown);
            // 
            // cms
            // 
            this.cms.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopy,
            this.tsmiFindIdx,
            this.tsmiSelectAll,
            this.tsmiDeselectAll,
            this.tsmiExportSqlDelete});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(186, 114);
            this.cms.Opening += new System.ComponentModel.CancelEventHandler(this.cms_Opening);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(185, 22);
            this.tsmiCopy.Text = "Copy";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // tsmiFindIdx
            // 
            this.tsmiFindIdx.Name = "tsmiFindIdx";
            this.tsmiFindIdx.Size = new System.Drawing.Size(185, 22);
            this.tsmiFindIdx.Text = "Find Guid";
            this.tsmiFindIdx.Click += new System.EventHandler(this.tsmiFindIdx_Click);
            // 
            // tsmiSelectAll
            // 
            this.tsmiSelectAll.Name = "tsmiSelectAll";
            this.tsmiSelectAll.Size = new System.Drawing.Size(185, 22);
            this.tsmiSelectAll.Text = "Select all";
            this.tsmiSelectAll.Click += new System.EventHandler(this.tsmiSelectAll_Click);
            // 
            // tsmiDeselectAll
            // 
            this.tsmiDeselectAll.Name = "tsmiDeselectAll";
            this.tsmiDeselectAll.Size = new System.Drawing.Size(185, 22);
            this.tsmiDeselectAll.Text = "Deselect all";
            this.tsmiDeselectAll.Click += new System.EventHandler(this.tsmiDeselectAll_Click);
            // 
            // tsmiExportSqlDelete
            // 
            this.tsmiExportSqlDelete.Name = "tsmiExportSqlDelete";
            this.tsmiExportSqlDelete.Size = new System.Drawing.Size(185, 22);
            this.tsmiExportSqlDelete.Text = "Export SQL for delete";
            this.tsmiExportSqlDelete.Click += new System.EventHandler(this.tsmiExportSqlDelete_Click);
            // 
            // lbContent
            // 
            this.lbContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbContent.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbContent.Location = new System.Drawing.Point(3, 3);
            this.lbContent.Name = "lbContent";
            this.lbContent.Size = new System.Drawing.Size(569, 23);
            this.lbContent.TabIndex = 4;
            this.lbContent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tpSchema
            // 
            this.tpSchema.Controls.Add(this.splitContainerSchema1);
            this.tpSchema.Controls.Add(this.lInfoSchema);
            this.tpSchema.Controls.Add(this.lbTableName);
            this.tpSchema.Location = new System.Drawing.Point(4, 24);
            this.tpSchema.Name = "tpSchema";
            this.tpSchema.Padding = new System.Windows.Forms.Padding(3);
            this.tpSchema.Size = new System.Drawing.Size(575, 284);
            this.tpSchema.TabIndex = 1;
            this.tpSchema.Text = "Schema";
            this.tpSchema.UseVisualStyleBackColor = true;
            // 
            // splitContainerSchema1
            // 
            this.splitContainerSchema1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSchema1.Location = new System.Drawing.Point(3, 26);
            this.splitContainerSchema1.Name = "splitContainerSchema1";
            this.splitContainerSchema1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerSchema1.Panel1
            // 
            this.splitContainerSchema1.Panel1.Controls.Add(this.dgvs);
            // 
            // splitContainerSchema1.Panel2
            // 
            this.splitContainerSchema1.Panel2.Controls.Add(this.splitContainerSchema2);
            this.splitContainerSchema1.Size = new System.Drawing.Size(569, 232);
            this.splitContainerSchema1.SplitterDistance = 60;
            this.splitContainerSchema1.TabIndex = 4;
            // 
            // dgvs
            // 
            this.dgvs.AllowUserToAddRows = false;
            this.dgvs.AllowUserToDeleteRows = false;
            this.dgvs.AllowUserToResizeRows = false;
            this.dgvs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvs.BackgroundColor = System.Drawing.Color.White;
            this.dgvs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvs.ContextMenuStrip = this.cms;
            this.dgvs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvs.EnableHeadersVisualStyles = false;
            this.dgvs.Location = new System.Drawing.Point(0, 0);
            this.dgvs.Name = "dgvs";
            this.dgvs.RowHeadersVisible = false;
            this.dgvs.RowHeadersWidth = 51;
            this.dgvs.Size = new System.Drawing.Size(569, 60);
            this.dgvs.TabIndex = 0;
            this.dgvs.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvs_CellContentDoubleClick);
            // 
            // splitContainerSchema2
            // 
            this.splitContainerSchema2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSchema2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerSchema2.Name = "splitContainerSchema2";
            this.splitContainerSchema2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerSchema2.Panel1
            // 
            this.splitContainerSchema2.Panel1.Controls.Add(this.dgvsc);
            // 
            // splitContainerSchema2.Panel2
            // 
            this.splitContainerSchema2.Panel2.Controls.Add(this.dgvidx);
            this.splitContainerSchema2.Size = new System.Drawing.Size(569, 168);
            this.splitContainerSchema2.SplitterDistance = 67;
            this.splitContainerSchema2.TabIndex = 4;
            // 
            // dgvsc
            // 
            this.dgvsc.AllowUserToAddRows = false;
            this.dgvsc.AllowUserToDeleteRows = false;
            this.dgvsc.AllowUserToResizeRows = false;
            this.dgvsc.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvsc.BackgroundColor = System.Drawing.Color.White;
            this.dgvsc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvsc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvsc.Location = new System.Drawing.Point(0, 0);
            this.dgvsc.Name = "dgvsc";
            this.dgvsc.RowHeadersVisible = false;
            this.dgvsc.RowHeadersWidth = 51;
            this.dgvsc.Size = new System.Drawing.Size(569, 67);
            this.dgvsc.TabIndex = 3;
            // 
            // dgvidx
            // 
            this.dgvidx.AllowUserToAddRows = false;
            this.dgvidx.AllowUserToDeleteRows = false;
            this.dgvidx.AllowUserToResizeRows = false;
            this.dgvidx.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvidx.BackgroundColor = System.Drawing.Color.White;
            this.dgvidx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvidx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvidx.Location = new System.Drawing.Point(0, 0);
            this.dgvidx.Name = "dgvidx";
            this.dgvidx.RowHeadersVisible = false;
            this.dgvidx.RowHeadersWidth = 51;
            this.dgvidx.Size = new System.Drawing.Size(569, 97);
            this.dgvidx.TabIndex = 4;
            // 
            // lInfoSchema
            // 
            this.lInfoSchema.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lInfoSchema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lInfoSchema.ForeColor = System.Drawing.Color.Green;
            this.lInfoSchema.Location = new System.Drawing.Point(3, 258);
            this.lInfoSchema.Name = "lInfoSchema";
            this.lInfoSchema.Size = new System.Drawing.Size(569, 23);
            this.lInfoSchema.TabIndex = 2;
            this.lInfoSchema.Text = "Double click to copy table name or column to SQL field";
            this.lInfoSchema.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lInfoSchema.Visible = false;
            // 
            // lbTableName
            // 
            this.lbTableName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTableName.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTableName.Location = new System.Drawing.Point(3, 3);
            this.lbTableName.Name = "lbTableName";
            this.lbTableName.Size = new System.Drawing.Size(569, 23);
            this.lbTableName.TabIndex = 1;
            this.lbTableName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbTableName.DoubleClick += new System.EventHandler(this.lbTableName_DoubleClick);
            // 
            // tpCreate
            // 
            this.tpCreate.Controls.Add(this.tbCreate);
            this.tpCreate.Location = new System.Drawing.Point(4, 24);
            this.tpCreate.Name = "tpCreate";
            this.tpCreate.Padding = new System.Windows.Forms.Padding(3);
            this.tpCreate.Size = new System.Drawing.Size(575, 284);
            this.tpCreate.TabIndex = 2;
            this.tpCreate.Text = "Create statement";
            this.tpCreate.UseVisualStyleBackColor = true;
            // 
            // tbCreate
            // 
            this.tbCreate.BackColor = System.Drawing.Color.White;
            this.tbCreate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCreate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCreate.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCreate.Location = new System.Drawing.Point(3, 3);
            this.tbCreate.Name = "tbCreate";
            this.tbCreate.ReadOnly = true;
            this.tbCreate.Size = new System.Drawing.Size(569, 278);
            this.tbCreate.TabIndex = 0;
            this.tbCreate.Text = "";
            // 
            // tpClass
            // 
            this.tpClass.Controls.Add(this.TakeIt);
            this.tpClass.Controls.Add(this.tbClassField);
            this.tpClass.Location = new System.Drawing.Point(4, 24);
            this.tpClass.Name = "tpClass";
            this.tpClass.Padding = new System.Windows.Forms.Padding(3);
            this.tpClass.Size = new System.Drawing.Size(575, 284);
            this.tpClass.TabIndex = 3;
            this.tpClass.Text = "Class";
            this.tpClass.UseVisualStyleBackColor = true;
            // 
            // TakeIt
            // 
            this.TakeIt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TakeIt.Image = global::PostgreSQL_Editor.Properties.Resources.TakeItTransparent;
            this.TakeIt.Location = new System.Drawing.Point(531, 6);
            this.TakeIt.Name = "TakeIt";
            this.TakeIt.Size = new System.Drawing.Size(36, 24);
            this.TakeIt.TabIndex = 1;
            this.TakeIt.TabStop = false;
            this.toolTip1.SetToolTip(this.TakeIt, "Copy codeblock");
            this.TakeIt.Click += new System.EventHandler(this.TakeIt_Click);
            this.TakeIt.MouseEnter += new System.EventHandler(this.TakeIt_MouseEnter);
            this.TakeIt.MouseLeave += new System.EventHandler(this.TakeIt_MouseLeave);
            this.TakeIt.MouseHover += new System.EventHandler(this.TakeIt_MouseHover);
            // 
            // tbClassField
            // 
            this.tbClassField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbClassField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbClassField.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbClassField.Location = new System.Drawing.Point(3, 3);
            this.tbClassField.Name = "tbClassField";
            this.tbClassField.ReadOnly = true;
            this.tbClassField.Size = new System.Drawing.Size(569, 278);
            this.tbClassField.TabIndex = 0;
            this.tbClassField.Text = "";
            // 
            // tpGuidSearch
            // 
            this.tpGuidSearch.Controls.Add(this.dgvGuidSearch);
            this.tpGuidSearch.Controls.Add(this.lbGuidSearch);
            this.tpGuidSearch.Location = new System.Drawing.Point(4, 24);
            this.tpGuidSearch.Name = "tpGuidSearch";
            this.tpGuidSearch.Size = new System.Drawing.Size(575, 284);
            this.tpGuidSearch.TabIndex = 4;
            this.tpGuidSearch.Text = "Guid search";
            this.tpGuidSearch.UseVisualStyleBackColor = true;
            // 
            // dgvGuidSearch
            // 
            this.dgvGuidSearch.BackgroundColor = System.Drawing.Color.White;
            this.dgvGuidSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGuidSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGuidSearch.Location = new System.Drawing.Point(0, 25);
            this.dgvGuidSearch.Name = "dgvGuidSearch";
            this.dgvGuidSearch.RowHeadersWidth = 25;
            this.dgvGuidSearch.Size = new System.Drawing.Size(575, 259);
            this.dgvGuidSearch.TabIndex = 1;
            // 
            // lbGuidSearch
            // 
            this.lbGuidSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbGuidSearch.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGuidSearch.Location = new System.Drawing.Point(0, 0);
            this.lbGuidSearch.Name = "lbGuidSearch";
            this.lbGuidSearch.Size = new System.Drawing.Size(575, 25);
            this.lbGuidSearch.TabIndex = 0;
            this.lbGuidSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiTools,
            this.toolStripMenuItem1,
            this.functionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoadSQLQuery,
            this.tsmiSaveSQLQuery});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(37, 20);
            this.tsmiFile.Text = "File";
            // 
            // tsmiLoadSQLQuery
            // 
            this.tsmiLoadSQLQuery.Enabled = false;
            this.tsmiLoadSQLQuery.Image = global::PostgreSQL_Editor.Properties.Resources.Load;
            this.tsmiLoadSQLQuery.Name = "tsmiLoadSQLQuery";
            this.tsmiLoadSQLQuery.Size = new System.Drawing.Size(157, 22);
            this.tsmiLoadSQLQuery.Text = "Load SQL query";
            this.tsmiLoadSQLQuery.Click += new System.EventHandler(this.tsmiLoadSQLQuery_Click);
            // 
            // tsmiSaveSQLQuery
            // 
            this.tsmiSaveSQLQuery.Enabled = false;
            this.tsmiSaveSQLQuery.Image = global::PostgreSQL_Editor.Properties.Resources.Save;
            this.tsmiSaveSQLQuery.Name = "tsmiSaveSQLQuery";
            this.tsmiSaveSQLQuery.Size = new System.Drawing.Size(157, 22);
            this.tsmiSaveSQLQuery.Text = "Save SQL query";
            this.tsmiSaveSQLQuery.Click += new System.EventHandler(this.tsmiSaveSQLQuery_Click);
            // 
            // tsmiTools
            // 
            this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditBMST,
            this.editBMSTFT,
            this.editBMSTFTMT,
            this.editSTFTMTModule,
            this.tsmiEditSTFT,
            this.editSTFTMTWE,
            this.editFRSL,
            this.editFrameSleeveRulesToolStripMenuItem,
            this.editFRST,
            this.editSLST,
            this.editEntityMetadata});
            this.tsmiTools.Name = "tsmiTools";
            this.tsmiTools.Size = new System.Drawing.Size(46, 20);
            this.tsmiTools.Text = "Tools";
            // 
            // tsmiEditBMST
            // 
            this.tsmiEditBMST.Name = "tsmiEditBMST";
            this.tsmiEditBMST.Size = new System.Drawing.Size(411, 22);
            this.tsmiEditBMST.Text = "Edit Basematerial - Systemtype Rules";
            this.tsmiEditBMST.Click += new System.EventHandler(this.tsmiEditBMST_Click);
            // 
            // editBMSTFT
            // 
            this.editBMSTFT.Name = "editBMSTFT";
            this.editBMSTFT.Size = new System.Drawing.Size(411, 22);
            this.editBMSTFT.Text = "Edit Basematerial - Systemtype - Frametype Rules";
            this.editBMSTFT.Click += new System.EventHandler(this.editBMSTFT_Click);
            // 
            // editBMSTFTMT
            // 
            this.editBMSTFTMT.Name = "editBMSTFTMT";
            this.editBMSTFTMT.Size = new System.Drawing.Size(411, 22);
            this.editBMSTFTMT.Text = "Edit Basematerial - Systemtype - Frametype - Materialtype Rules";
            this.editBMSTFTMT.Click += new System.EventHandler(this.editBMSTFTMT_Click);
            // 
            // editSTFTMTModule
            // 
            this.editSTFTMTModule.Name = "editSTFTMTModule";
            this.editSTFTMTModule.Size = new System.Drawing.Size(411, 22);
            this.editSTFTMTModule.Text = "Edit Systemtype - Frametype - Module-class Rules";
            this.editSTFTMTModule.Click += new System.EventHandler(this.editSTFTMTModule_Click);
            // 
            // tsmiEditSTFT
            // 
            this.tsmiEditSTFT.Name = "tsmiEditSTFT";
            this.tsmiEditSTFT.Size = new System.Drawing.Size(411, 22);
            this.tsmiEditSTFT.Text = "Edit Systemtype - Frametype Rules";
            this.tsmiEditSTFT.Click += new System.EventHandler(this.tsmiEditSTFT_Click);
            // 
            // editSTFTMTWE
            // 
            this.editSTFTMTWE.Name = "editSTFTMTWE";
            this.editSTFTMTWE.Size = new System.Drawing.Size(411, 22);
            this.editSTFTMTWE.Text = "Edit Systemtype - Wedge Rules";
            this.editSTFTMTWE.Click += new System.EventHandler(this.editSTFTMTWE_Click);
            // 
            // editFRSL
            // 
            this.editFRSL.Name = "editFRSL";
            this.editFRSL.Size = new System.Drawing.Size(411, 22);
            this.editFRSL.Text = "Edit Frametype - Sleeve Rules";
            this.editFRSL.Click += new System.EventHandler(this.editFRSL_Click);
            // 
            // editFrameSleeveRulesToolStripMenuItem
            // 
            this.editFrameSleeveRulesToolStripMenuItem.Name = "editFrameSleeveRulesToolStripMenuItem";
            this.editFrameSleeveRulesToolStripMenuItem.Size = new System.Drawing.Size(411, 22);
            this.editFrameSleeveRulesToolStripMenuItem.Text = "Edit Frame - Sleeve Rules";
            this.editFrameSleeveRulesToolStripMenuItem.Click += new System.EventHandler(this.editFrameSleeveRulesToolStripMenuItem_Click);
            // 
            // editFRST
            // 
            this.editFRST.Name = "editFRST";
            this.editFRST.Size = new System.Drawing.Size(411, 22);
            this.editFRST.Text = "Edit Frame - Sticker Rules";
            this.editFRST.Click += new System.EventHandler(this.editFRST_Click);
            // 
            // editSLST
            // 
            this.editSLST.Name = "editSLST";
            this.editSLST.Size = new System.Drawing.Size(411, 22);
            this.editSLST.Text = "Edit Sleeve - Sticker Rules";
            this.editSLST.Click += new System.EventHandler(this.editSLST_Click);
            // 
            // editEntityMetadata
            // 
            this.editEntityMetadata.Name = "editEntityMetadata";
            this.editEntityMetadata.Size = new System.Drawing.Size(411, 22);
            this.editEntityMetadata.Text = "Edit Entity - Metadata";
            this.editEntityMetadata.Click += new System.EventHandler(this.editEntityMetadata_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // functionsToolStripMenuItem
            // 
            this.functionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowTableColumnTree,
            this.tsmiGetModuleFromID,
            this.tsmiGetFrameFromID});
            this.functionsToolStripMenuItem.Name = "functionsToolStripMenuItem";
            this.functionsToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.functionsToolStripMenuItem.Text = "Functions";
            // 
            // tsmiShowTableColumnTree
            // 
            this.tsmiShowTableColumnTree.Name = "tsmiShowTableColumnTree";
            this.tsmiShowTableColumnTree.Size = new System.Drawing.Size(203, 22);
            this.tsmiShowTableColumnTree.Text = "Show Table Column Tree";
            this.tsmiShowTableColumnTree.Click += new System.EventHandler(this.tsmiShowTableColumnTree_Click);
            // 
            // tsmiGetModuleFromID
            // 
            this.tsmiGetModuleFromID.Name = "tsmiGetModuleFromID";
            this.tsmiGetModuleFromID.Size = new System.Drawing.Size(203, 22);
            this.tsmiGetModuleFromID.Text = "Get Module from ID";
            this.tsmiGetModuleFromID.Click += new System.EventHandler(this.getModuleFromID_Click);
            // 
            // tsmiGetFrameFromID
            // 
            this.tsmiGetFrameFromID.Name = "tsmiGetFrameFromID";
            this.tsmiGetFrameFromID.Size = new System.Drawing.Size(203, 22);
            this.tsmiGetFrameFromID.Text = "Get Frame from ID";
            this.tsmiGetFrameFromID.Click += new System.EventHandler(this.tsmiGetFrameFromID_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 503);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "PCT-Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.cms.ResumeLayout(false);
            this.tpSchema.ResumeLayout(false);
            this.splitContainerSchema1.Panel1.ResumeLayout(false);
            this.splitContainerSchema1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSchema1)).EndInit();
            this.splitContainerSchema1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvs)).EndInit();
            this.splitContainerSchema2.Panel1.ResumeLayout(false);
            this.splitContainerSchema2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSchema2)).EndInit();
            this.splitContainerSchema2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvsc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvidx)).EndInit();
            this.tpCreate.ResumeLayout(false);
            this.tpClass.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TakeIt)).EndInit();
            this.tpGuidSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGuidSearch)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslInfo;
        private System.Windows.Forms.ToolStripStatusLabel tsslSuccess;
        private System.Windows.Forms.ToolStripStatusLabel tsslLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TabPage tpSchema;
        private System.Windows.Forms.DataGridView dgvs;
        private System.Windows.Forms.ToolStripButton tsbSQL;
        private System.Windows.Forms.ToolStripButton tsbExecute;
        private System.Windows.Forms.ToolStripButton tsbClear;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.TabPage tpCreate;
        private System.Windows.Forms.RichTextBox tbCreate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbSQL;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadSQLQuery;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveSQLQuery;
        private System.Windows.Forms.ToolStripButton tsbLoadSQL;
        private System.Windows.Forms.ToolStripButton tsbSaveSQL;
        private System.Windows.Forms.ToolStripMenuItem tsmiTools;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditBMST;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.TabPage tpClass;
        private System.Windows.Forms.RichTextBox tbClassField;
        private System.Windows.Forms.PictureBox TakeIt;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lbTableName;
        private System.Windows.Forms.Label lInfoSchema;
        private System.Windows.Forms.Label lbContent;
        private System.Windows.Forms.RichTextBox tbQuery;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditSTFT;
        private System.Windows.Forms.ToolStripMenuItem editBMSTFTMT;
        private System.Windows.Forms.ToolStripMenuItem editBMSTFT;
        private System.Windows.Forms.ToolStripMenuItem editSTFTMTModule;
        private System.Windows.Forms.DataGridView dgvsc;
        private System.Windows.Forms.SplitContainer splitContainerSchema1;
        private System.Windows.Forms.SplitContainer splitContainerSchema2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvidx;
        private System.Windows.Forms.ToolStripMenuItem tsmiFindIdx;
        private System.Windows.Forms.TabPage tpGuidSearch;
        private System.Windows.Forms.DataGridView dgvGuidSearch;
        private System.Windows.Forms.Label lbGuidSearch;
        private System.Windows.Forms.ToolStripMenuItem editFRSL;
        private System.Windows.Forms.ToolStripMenuItem editSTFTMTWE;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbExportExcel;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeselectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportSqlDelete;
        private System.Windows.Forms.ToolStripButton tsbForeignKeys;
        private System.Windows.Forms.ToolStripButton tsbCreateFKeys;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem editSLST;
        private System.Windows.Forms.ToolStripMenuItem editFRST;
        private System.Windows.Forms.ToolStripMenuItem editEntityMetadata;
        private System.Windows.Forms.ToolStripStatusLabel tslInfo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem functionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiGetModuleFromID;
        private System.Windows.Forms.ToolStripMenuItem tsmiGetFrameFromID;
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowTableColumnTree;
        private System.Windows.Forms.ToolStripButton tsbShowDataTree;
        private System.Windows.Forms.ToolStripMenuItem editFrameSleeveRulesToolStripMenuItem;
    }
}

