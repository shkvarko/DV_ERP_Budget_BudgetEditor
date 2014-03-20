namespace ErpBudgetBudgetEditor
{
    partial class frmBudgetEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBudgetEditor));
            this.barManager = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barBtnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnAddNode = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDeleteNode = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnAccept = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnAddBudgetItem = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barBtnAddChildNode = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barTextAccept = new DevExpress.XtraBars.BarStaticItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDown = new DevExpress.XtraEditors.SimpleButton();
            this.btnLeft = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnDrop = new DevExpress.XtraEditors.SimpleButton();
            this.btnUp = new DevExpress.XtraEditors.SimpleButton();
            this.btnRight = new DevExpress.XtraEditors.SimpleButton();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.colDEBITARTICLE_GUID_ID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDEBITARTICLE_PARENT_ID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDEBITARTICLE_NUM = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colREADONLY = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDEBITARTICLE_MULTIBUDGETDEP = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colJANUARY = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repItemCalcEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.colFEBRUARY = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colMARCH = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colAPRIL = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colMAY = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colJUNE = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colJULY = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colAUGUST = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colSEPTEMBER = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colOCTEMBER = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNOVEMBER = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDECEMBER = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colSummary = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imglNodes = new System.Windows.Forms.ImageList();
            this.repItemPopupContainerEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            this.toolTipController = new DevExpress.Utils.ToolTipController();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.mitemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemAddBudgetItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemAddChild = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mitemTrnList = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemDocList = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.lblCurrencyRate = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemCalcEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemPopupContainerEdit)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnAddNode,
            this.barBtnRefresh,
            this.barBtnDeleteNode,
            this.barBtnPrint,
            this.barBtnAddChildNode,
            this.barBtnAccept,
            this.barButtonItem1,
            this.barTextAccept,
            this.barEditItem1,
            this.barBtnAddBudgetItem});
            this.barManager.MaxItemId = 17;
            this.barManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1});
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 1";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAddNode),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnDeleteNode),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAccept),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAddBudgetItem)});
            this.bar1.Offset = 2;
            this.bar1.Text = "Custom 1";
            // 
            // barBtnRefresh
            // 
            this.barBtnRefresh.Caption = "Обновить";
            this.barBtnRefresh.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.refresh;
            this.barBtnRefresh.Hint = "Обновить";
            this.barBtnRefresh.Id = 1;
            this.barBtnRefresh.Name = "barBtnRefresh";
            this.barBtnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnRefresh_ItemClick);
            // 
            // barBtnAddNode
            // 
            this.barBtnAddNode.Caption = "Добавить";
            this.barBtnAddNode.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.treenode_add16_h_child2;
            this.barBtnAddNode.Hint = "Добавить подстатью";
            this.barBtnAddNode.Id = 0;
            this.barBtnAddNode.Name = "barBtnAddNode";
            this.barBtnAddNode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAddChildNode_ItemClick);
            // 
            // barBtnDeleteNode
            // 
            this.barBtnDeleteNode.Caption = "Удалить";
            this.barBtnDeleteNode.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.treenode_delete16_h2;
            this.barBtnDeleteNode.Hint = "Удалить подстатью";
            this.barBtnDeleteNode.Id = 2;
            this.barBtnDeleteNode.Name = "barBtnDeleteNode";
            this.barBtnDeleteNode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDeleteNode_ItemClick);
            // 
            // barBtnPrint
            // 
            this.barBtnPrint.Caption = "Печать";
            this.barBtnPrint.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.printer2;
            this.barBtnPrint.Hint = "Печать";
            this.barBtnPrint.Id = 3;
            this.barBtnPrint.Name = "barBtnPrint";
            this.barBtnPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnPrint_ItemClick);
            // 
            // barBtnAccept
            // 
            this.barBtnAccept.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.barBtnAccept.Caption = "Утвердить";
            this.barBtnAccept.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.document_lock;
            this.barBtnAccept.Id = 7;
            this.barBtnAccept.Name = "barBtnAccept";
            this.barBtnAccept.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAccept_ItemClick);
            // 
            // barBtnAddBudgetItem
            // 
            this.barBtnAddBudgetItem.Caption = "Новая статья бюджета";
            this.barBtnAddBudgetItem.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.treenode_add16_h_root2;
            this.barBtnAddBudgetItem.Hint = "Добавить в бюджет статью";
            this.barBtnAddBudgetItem.Id = 16;
            this.barBtnAddBudgetItem.Name = "barBtnAddBudgetItem";
            this.barBtnAddBudgetItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAddBudgetItem_ItemClick);
            // 
            // barDockControlTop
            // 
            this.toolTipController.SetSuperTip(this.barDockControlTop, null);
            // 
            // barDockControlBottom
            // 
            this.toolTipController.SetSuperTip(this.barDockControlBottom, null);
            // 
            // barDockControlLeft
            // 
            this.toolTipController.SetSuperTip(this.barDockControlLeft, null);
            // 
            // barDockControlRight
            // 
            this.toolTipController.SetSuperTip(this.barDockControlRight, null);
            // 
            // barBtnAddChildNode
            // 
            this.barBtnAddChildNode.Caption = "Добавить подстатью";
            this.barBtnAddChildNode.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.treenode_add16_h_child;
            this.barBtnAddChildNode.Hint = "Добавить дочернее подразделение";
            this.barBtnAddChildNode.Id = 5;
            this.barBtnAddChildNode.Name = "barBtnAddChildNode";
            this.barBtnAddChildNode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAddChildNode_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barBtnBudgetState";
            this.barButtonItem1.Id = 9;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barTextAccept
            // 
            this.barTextAccept.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.barTextAccept.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.printer2;
            this.barTextAccept.Id = 10;
            this.barTextAccept.Name = "barTextAccept";
            this.barTextAccept.TextAlignment = System.Drawing.StringAlignment.Far;
            this.barTextAccept.Width = 16;
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "barImageAccept";
            this.barEditItem1.Edit = this.repositoryItemPictureEdit1;
            this.barEditItem1.Id = 11;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.treeList, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblInfo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCurrencyRate, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(644, 398);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel1, null);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.Controls.Add(this.btnDown, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnLeft, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnDrop, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnUp, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnRight, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 364);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(638, 31);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel2, null);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnDown
            // 
            this.btnDown.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(35, 3);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(25, 24);
            this.btnDown.TabIndex = 2;
            this.btnDown.ToolTip = "Перемещение узла вниз";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnLeft.Image = ((System.Drawing.Image)(resources.GetObject("btnLeft.Image")));
            this.btnLeft.Location = new System.Drawing.Point(3, 3);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(25, 24);
            this.btnLeft.TabIndex = 1;
            this.btnLeft.ToolTip = "Перемещение узла влево";
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::ErpBudgetBudgetEditor.Properties.Resources.disk_blue_ok;
            this.btnSave.Location = new System.Drawing.Point(344, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 24);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Сохранить изменения";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::ErpBudgetBudgetEditor.Properties.Resources.undo;
            this.btnCancel.Location = new System.Drawing.Point(494, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(141, 24);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Отменить изменения";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDrop
            // 
            this.btnDrop.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDrop.Image = ((System.Drawing.Image)(resources.GetObject("btnDrop.Image")));
            this.btnDrop.Location = new System.Drawing.Point(67, 3);
            this.btnDrop.Name = "btnDrop";
            this.btnDrop.Size = new System.Drawing.Size(25, 24);
            this.btnDrop.TabIndex = 3;
            this.btnDrop.ToolTipTitle = "Удаление узла";
            this.btnDrop.Click += new System.EventHandler(this.mitemDelete_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(99, 3);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(25, 24);
            this.btnUp.TabIndex = 4;
            this.btnUp.ToolTipTitle = "Перемещение узла вверх";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnRight
            // 
            this.btnRight.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRight.Image = ((System.Drawing.Image)(resources.GetObject("btnRight.Image")));
            this.btnRight.Location = new System.Drawing.Point(131, 3);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(25, 24);
            this.btnRight.TabIndex = 5;
            this.btnRight.ToolTipTitle = "Перемещение узла вправо";
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // treeList
            // 
            this.treeList.AllowDrop = true;
            this.treeList.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.treeList.Appearance.Empty.Options.UseBackColor = true;
            this.treeList.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(250)))));
            this.treeList.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.treeList.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeList.Appearance.EvenRow.Options.UseForeColor = true;
            this.treeList.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(250)))));
            this.treeList.Appearance.OddRow.Options.UseBackColor = true;
            this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colDEBITARTICLE_GUID_ID,
            this.colDEBITARTICLE_PARENT_ID,
            this.colDEBITARTICLE_NUM,
            this.colREADONLY,
            this.colDEBITARTICLE_MULTIBUDGETDEP,
            this.colJANUARY,
            this.colFEBRUARY,
            this.colMARCH,
            this.colAPRIL,
            this.colMAY,
            this.colJUNE,
            this.colJULY,
            this.colAUGUST,
            this.colSEPTEMBER,
            this.colOCTEMBER,
            this.colNOVEMBER,
            this.colDECEMBER,
            this.colSummary});
            this.treeList.ColumnsImageList = this.imglNodes;
            this.treeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList.ImageIndexFieldName = "";
            this.treeList.KeyFieldName = "";
            this.treeList.Location = new System.Drawing.Point(3, 47);
            this.treeList.Name = "treeList";
            this.treeList.OptionsBehavior.AutoChangeParent = false;
            this.treeList.OptionsBehavior.AutoNodeHeight = false;
            this.treeList.OptionsBehavior.DragNodes = true;
            this.treeList.OptionsBehavior.KeepSelectedOnClick = false;
            this.treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            this.treeList.OptionsBehavior.SmartMouseHover = false;
            this.treeList.OptionsBehavior.UseTabKey = true;
            this.treeList.OptionsPrint.PrintImages = false;
            this.treeList.OptionsPrint.PrintPreview = true;
            this.treeList.OptionsPrint.PrintRowFooterSummary = true;
            this.treeList.OptionsView.AutoWidth = false;
            this.treeList.OptionsView.EnableAppearanceEvenRow = true;
            this.treeList.OptionsView.EnableAppearanceOddRow = true;
            this.treeList.OptionsView.ShowSummaryFooter = true;
            this.treeList.ParentFieldName = "";
            this.treeList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repItemPopupContainerEdit,
            this.repItemCalcEdit});
            this.treeList.SelectImageList = this.imglNodes;
            this.treeList.Size = new System.Drawing.Size(638, 311);
            this.treeList.TabIndex = 2;
            this.treeList.ToolTipController = this.toolTipController;
            this.treeList.BeforeFocusNode += new DevExpress.XtraTreeList.BeforeFocusNodeEventHandler(this.treeList_BeforeFocusNode);
            this.treeList.AfterDragNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList_AfterDragNode);
            this.treeList.AfterFocusNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList_AfterFocusNode);
            this.treeList.InvalidNodeException += new DevExpress.XtraTreeList.InvalidNodeExceptionEventHandler(this.treeList_InvalidNodeException);
            this.treeList.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.treeList_CustomDrawNodeCell);
            this.treeList.CustomDrawRowFooter += new DevExpress.XtraTreeList.CustomDrawRowFooterEventHandler(this.treeList_CustomDrawRowFooter);
            this.treeList.CustomDrawFooterCell += new DevExpress.XtraTreeList.CustomDrawFooterCellEventHandler(this.treeList_CustomDrawFooterCell);
            this.treeList.CustomDrawNodeImages += new DevExpress.XtraTreeList.CustomDrawNodeImagesEventHandler(this.treeList_CustomDrawNodeImages);
            this.treeList.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treeList_CellValueChanged);
            this.treeList.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeList_DragDrop);
            this.treeList.DragOver += new System.Windows.Forms.DragEventHandler(this.treeList_DragOver);
            this.treeList.DragLeave += new System.EventHandler(this.treeList_DragLeave);
            this.treeList.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.treeList_GiveFeedback);
            this.treeList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeList_KeyDown);
            this.treeList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeList_MouseClick);
            this.treeList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeList_MouseDoubleClick);
            this.treeList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeList_MouseMove);
            // 
            // colDEBITARTICLE_GUID_ID
            // 
            this.colDEBITARTICLE_GUID_ID.Caption = "УИ статьи расходов";
            this.colDEBITARTICLE_GUID_ID.FieldName = "УИ статьи расходов";
            this.colDEBITARTICLE_GUID_ID.Name = "colDEBITARTICLE_GUID_ID";
            this.colDEBITARTICLE_GUID_ID.OptionsColumn.AllowFocus = false;
            this.colDEBITARTICLE_GUID_ID.OptionsColumn.AllowMove = false;
            this.colDEBITARTICLE_GUID_ID.OptionsColumn.AllowSort = false;
            // 
            // colDEBITARTICLE_PARENT_ID
            // 
            this.colDEBITARTICLE_PARENT_ID.Caption = "УИ Родителя";
            this.colDEBITARTICLE_PARENT_ID.FieldName = "УИ Родителя";
            this.colDEBITARTICLE_PARENT_ID.Name = "colDEBITARTICLE_PARENT_ID";
            this.colDEBITARTICLE_PARENT_ID.OptionsColumn.AllowEdit = false;
            this.colDEBITARTICLE_PARENT_ID.OptionsColumn.ReadOnly = true;
            // 
            // colDEBITARTICLE_NUM
            // 
            this.colDEBITARTICLE_NUM.Caption = "№";
            this.colDEBITARTICLE_NUM.FieldName = "№";
            this.colDEBITARTICLE_NUM.MinWidth = 100;
            this.colDEBITARTICLE_NUM.Name = "colDEBITARTICLE_NUM";
            this.colDEBITARTICLE_NUM.OptionsColumn.AllowEdit = false;
            this.colDEBITARTICLE_NUM.OptionsColumn.AllowMove = false;
            this.colDEBITARTICLE_NUM.OptionsColumn.AllowSort = false;
            this.colDEBITARTICLE_NUM.OptionsColumn.ReadOnly = true;
            this.colDEBITARTICLE_NUM.Visible = true;
            this.colDEBITARTICLE_NUM.VisibleIndex = 0;
            this.colDEBITARTICLE_NUM.Width = 150;
            // 
            // colREADONLY
            // 
            this.colREADONLY.Caption = "READONLY";
            this.colREADONLY.FieldName = "READONLY";
            this.colREADONLY.Name = "colREADONLY";
            this.colREADONLY.OptionsColumn.AllowMove = false;
            this.colREADONLY.OptionsColumn.AllowSort = false;
            this.colREADONLY.Width = 39;
            // 
            // colDEBITARTICLE_MULTIBUDGETDEP
            // 
            this.colDEBITARTICLE_MULTIBUDGETDEP.Caption = "Мульти";
            this.colDEBITARTICLE_MULTIBUDGETDEP.FieldName = "Мульти";
            this.colDEBITARTICLE_MULTIBUDGETDEP.Name = "colDEBITARTICLE_MULTIBUDGETDEP";
            this.colDEBITARTICLE_MULTIBUDGETDEP.OptionsColumn.AllowFocus = false;
            this.colDEBITARTICLE_MULTIBUDGETDEP.OptionsColumn.AllowMove = false;
            this.colDEBITARTICLE_MULTIBUDGETDEP.OptionsColumn.AllowSort = false;
            // 
            // colJANUARY
            // 
            this.colJANUARY.AppearanceCell.Options.UseBackColor = true;
            this.colJANUARY.AppearanceCell.Options.UseForeColor = true;
            this.colJANUARY.AppearanceCell.Options.UseTextOptions = true;
            this.colJANUARY.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colJANUARY.Caption = "Январь";
            this.colJANUARY.ColumnEdit = this.repItemCalcEdit;
            this.colJANUARY.FieldName = "Январь";
            this.colJANUARY.MinWidth = 70;
            this.colJANUARY.Name = "colJANUARY";
            this.colJANUARY.OptionsColumn.AllowMove = false;
            this.colJANUARY.OptionsColumn.AllowSort = false;
            this.colJANUARY.RowFooterSummary = DevExpress.XtraTreeList.SummaryItemType.Custom;
            this.colJANUARY.RowFooterSummaryStrFormat = "";
            this.colJANUARY.SummaryFooterStrFormat = "";
            this.colJANUARY.Visible = true;
            this.colJANUARY.VisibleIndex = 1;
            this.colJANUARY.Width = 70;
            // 
            // repItemCalcEdit
            // 
            this.repItemCalcEdit.AutoHeight = false;
            this.repItemCalcEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repItemCalcEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repItemCalcEdit.Name = "repItemCalcEdit";
            this.repItemCalcEdit.ValueChanged += new System.EventHandler(this.repItemCalcEdit_ValueChanged);
            this.repItemCalcEdit.DoubleClick += new System.EventHandler(this.repItemCalcEdit_DoubleClick);
            this.repItemCalcEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.repItemCalcEdit_KeyDown);
            // 
            // colFEBRUARY
            // 
            this.colFEBRUARY.AppearanceCell.Options.UseTextOptions = true;
            this.colFEBRUARY.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colFEBRUARY.Caption = "Февраль";
            this.colFEBRUARY.ColumnEdit = this.repItemCalcEdit;
            this.colFEBRUARY.FieldName = "Февраль";
            this.colFEBRUARY.MinWidth = 70;
            this.colFEBRUARY.Name = "colFEBRUARY";
            this.colFEBRUARY.OptionsColumn.AllowMove = false;
            this.colFEBRUARY.OptionsColumn.AllowSort = false;
            this.colFEBRUARY.Visible = true;
            this.colFEBRUARY.VisibleIndex = 2;
            this.colFEBRUARY.Width = 70;
            // 
            // colMARCH
            // 
            this.colMARCH.AppearanceCell.Options.UseTextOptions = true;
            this.colMARCH.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colMARCH.Caption = "Март";
            this.colMARCH.ColumnEdit = this.repItemCalcEdit;
            this.colMARCH.FieldName = "Март";
            this.colMARCH.MinWidth = 70;
            this.colMARCH.Name = "colMARCH";
            this.colMARCH.OptionsColumn.AllowMove = false;
            this.colMARCH.OptionsColumn.AllowSort = false;
            this.colMARCH.Visible = true;
            this.colMARCH.VisibleIndex = 3;
            this.colMARCH.Width = 70;
            // 
            // colAPRIL
            // 
            this.colAPRIL.AppearanceCell.Options.UseTextOptions = true;
            this.colAPRIL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colAPRIL.Caption = "Апрель";
            this.colAPRIL.ColumnEdit = this.repItemCalcEdit;
            this.colAPRIL.FieldName = "Апрель";
            this.colAPRIL.MinWidth = 70;
            this.colAPRIL.Name = "colAPRIL";
            this.colAPRIL.OptionsColumn.AllowMove = false;
            this.colAPRIL.OptionsColumn.AllowSort = false;
            this.colAPRIL.Visible = true;
            this.colAPRIL.VisibleIndex = 4;
            this.colAPRIL.Width = 70;
            // 
            // colMAY
            // 
            this.colMAY.AppearanceCell.Options.UseTextOptions = true;
            this.colMAY.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colMAY.Caption = "Май";
            this.colMAY.ColumnEdit = this.repItemCalcEdit;
            this.colMAY.FieldName = "Май";
            this.colMAY.MinWidth = 70;
            this.colMAY.Name = "colMAY";
            this.colMAY.OptionsColumn.AllowMove = false;
            this.colMAY.OptionsColumn.AllowSort = false;
            this.colMAY.Visible = true;
            this.colMAY.VisibleIndex = 5;
            this.colMAY.Width = 70;
            // 
            // colJUNE
            // 
            this.colJUNE.AppearanceCell.Options.UseTextOptions = true;
            this.colJUNE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colJUNE.Caption = "Июнь";
            this.colJUNE.ColumnEdit = this.repItemCalcEdit;
            this.colJUNE.FieldName = "Июнь";
            this.colJUNE.MinWidth = 70;
            this.colJUNE.Name = "colJUNE";
            this.colJUNE.OptionsColumn.AllowMove = false;
            this.colJUNE.OptionsColumn.AllowSort = false;
            this.colJUNE.Visible = true;
            this.colJUNE.VisibleIndex = 6;
            this.colJUNE.Width = 70;
            // 
            // colJULY
            // 
            this.colJULY.AppearanceCell.Options.UseTextOptions = true;
            this.colJULY.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colJULY.Caption = "Июль";
            this.colJULY.ColumnEdit = this.repItemCalcEdit;
            this.colJULY.FieldName = "Июль";
            this.colJULY.MinWidth = 70;
            this.colJULY.Name = "colJULY";
            this.colJULY.OptionsColumn.AllowMove = false;
            this.colJULY.OptionsColumn.AllowSort = false;
            this.colJULY.Visible = true;
            this.colJULY.VisibleIndex = 7;
            this.colJULY.Width = 70;
            // 
            // colAUGUST
            // 
            this.colAUGUST.AppearanceCell.Options.UseTextOptions = true;
            this.colAUGUST.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colAUGUST.Caption = "Август";
            this.colAUGUST.ColumnEdit = this.repItemCalcEdit;
            this.colAUGUST.FieldName = "Август";
            this.colAUGUST.MinWidth = 70;
            this.colAUGUST.Name = "colAUGUST";
            this.colAUGUST.OptionsColumn.AllowMove = false;
            this.colAUGUST.OptionsColumn.AllowSort = false;
            this.colAUGUST.Visible = true;
            this.colAUGUST.VisibleIndex = 8;
            this.colAUGUST.Width = 70;
            // 
            // colSEPTEMBER
            // 
            this.colSEPTEMBER.AppearanceCell.Options.UseTextOptions = true;
            this.colSEPTEMBER.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSEPTEMBER.Caption = "Сентябрь";
            this.colSEPTEMBER.ColumnEdit = this.repItemCalcEdit;
            this.colSEPTEMBER.FieldName = "Сентябрь";
            this.colSEPTEMBER.MinWidth = 70;
            this.colSEPTEMBER.Name = "colSEPTEMBER";
            this.colSEPTEMBER.OptionsColumn.AllowMove = false;
            this.colSEPTEMBER.OptionsColumn.AllowSort = false;
            this.colSEPTEMBER.Visible = true;
            this.colSEPTEMBER.VisibleIndex = 9;
            this.colSEPTEMBER.Width = 70;
            // 
            // colOCTEMBER
            // 
            this.colOCTEMBER.AppearanceCell.Options.UseTextOptions = true;
            this.colOCTEMBER.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colOCTEMBER.Caption = "Октябрь";
            this.colOCTEMBER.ColumnEdit = this.repItemCalcEdit;
            this.colOCTEMBER.FieldName = "Октябрь";
            this.colOCTEMBER.MinWidth = 70;
            this.colOCTEMBER.Name = "colOCTEMBER";
            this.colOCTEMBER.OptionsColumn.AllowMove = false;
            this.colOCTEMBER.OptionsColumn.AllowSort = false;
            this.colOCTEMBER.Visible = true;
            this.colOCTEMBER.VisibleIndex = 10;
            this.colOCTEMBER.Width = 70;
            // 
            // colNOVEMBER
            // 
            this.colNOVEMBER.AppearanceCell.Options.UseTextOptions = true;
            this.colNOVEMBER.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colNOVEMBER.Caption = "Ноябрь";
            this.colNOVEMBER.ColumnEdit = this.repItemCalcEdit;
            this.colNOVEMBER.FieldName = "Ноябрь";
            this.colNOVEMBER.MinWidth = 70;
            this.colNOVEMBER.Name = "colNOVEMBER";
            this.colNOVEMBER.OptionsColumn.AllowMove = false;
            this.colNOVEMBER.OptionsColumn.AllowSort = false;
            this.colNOVEMBER.Visible = true;
            this.colNOVEMBER.VisibleIndex = 11;
            // 
            // colDECEMBER
            // 
            this.colDECEMBER.AppearanceCell.Options.UseTextOptions = true;
            this.colDECEMBER.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colDECEMBER.Caption = "Декабрь";
            this.colDECEMBER.ColumnEdit = this.repItemCalcEdit;
            this.colDECEMBER.FieldName = "Декабрь";
            this.colDECEMBER.MinWidth = 70;
            this.colDECEMBER.Name = "colDECEMBER";
            this.colDECEMBER.OptionsColumn.AllowMove = false;
            this.colDECEMBER.OptionsColumn.AllowSort = false;
            this.colDECEMBER.Visible = true;
            this.colDECEMBER.VisibleIndex = 12;
            this.colDECEMBER.Width = 70;
            // 
            // colSummary
            // 
            this.colSummary.AppearanceCell.Options.UseTextOptions = true;
            this.colSummary.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSummary.Caption = "Итого";
            this.colSummary.FieldName = "Итого";
            this.colSummary.MinWidth = 70;
            this.colSummary.Name = "colSummary";
            this.colSummary.OptionsColumn.AllowEdit = false;
            this.colSummary.OptionsColumn.AllowMove = false;
            this.colSummary.OptionsColumn.AllowSort = false;
            this.colSummary.OptionsColumn.ReadOnly = true;
            this.colSummary.Visible = true;
            this.colSummary.VisibleIndex = 13;
            // 
            // imglNodes
            // 
            this.imglNodes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglNodes.ImageStream")));
            this.imglNodes.TransparentColor = System.Drawing.Color.Magenta;
            this.imglNodes.Images.SetKeyName(0, "lock16_h.bmp");
            this.imglNodes.Images.SetKeyName(1, "unlock16_h.bmp");
            // 
            // repItemPopupContainerEdit
            // 
            this.repItemPopupContainerEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repItemPopupContainerEdit.CloseOnLostFocus = false;
            this.repItemPopupContainerEdit.CloseOnOuterMouseClick = false;
            this.repItemPopupContainerEdit.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.NumPad0);
            this.repItemPopupContainerEdit.Name = "repItemPopupContainerEdit";
            this.repItemPopupContainerEdit.PopupSizeable = false;
            this.repItemPopupContainerEdit.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.DoubleClick;
            // 
            // toolTipController
            // 
            this.toolTipController.AutoPopDelay = 1000;
            this.toolTipController.ToolTipLocation = DevExpress.Utils.ToolTipLocation.BottomLeft;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitemRefresh,
            this.mitemAddBudgetItem,
            this.mitemAddChild,
            this.mitemDelete,
            this.toolStripMenuItem1,
            this.mitemTrnList,
            this.mitemDocList,
            this.mitemDetail});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(188, 164);
            this.toolTipController.SetSuperTip(this.contextMenuStrip, null);
            // 
            // mitemRefresh
            // 
            this.mitemRefresh.Image = global::ErpBudgetBudgetEditor.Properties.Resources.refresh;
            this.mitemRefresh.Name = "mitemRefresh";
            this.mitemRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mitemRefresh.Size = new System.Drawing.Size(187, 22);
            this.mitemRefresh.Text = "Обновить";
            this.mitemRefresh.Click += new System.EventHandler(this.mitemRefresh_Click);
            // 
            // mitemAddBudgetItem
            // 
            this.mitemAddBudgetItem.Image = global::ErpBudgetBudgetEditor.Properties.Resources.treenode_add16_h_root2;
            this.mitemAddBudgetItem.Name = "mitemAddBudgetItem";
            this.mitemAddBudgetItem.Size = new System.Drawing.Size(187, 22);
            this.mitemAddBudgetItem.Text = "Добавить статью";
            this.mitemAddBudgetItem.ToolTipText = "Добавить статью";
            this.mitemAddBudgetItem.Click += new System.EventHandler(this.mitemAddBudgetItem_Click);
            // 
            // mitemAddChild
            // 
            this.mitemAddChild.AutoToolTip = true;
            this.mitemAddChild.Image = global::ErpBudgetBudgetEditor.Properties.Resources.treenode_add16_h_child;
            this.mitemAddChild.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.mitemAddChild.Name = "mitemAddChild";
            this.mitemAddChild.Size = new System.Drawing.Size(187, 22);
            this.mitemAddChild.Text = "Добавить подстатью";
            this.mitemAddChild.ToolTipText = "Добавить подстатью";
            this.mitemAddChild.Click += new System.EventHandler(this.mitemAddChild_Click);
            // 
            // mitemDelete
            // 
            this.mitemDelete.AutoToolTip = true;
            this.mitemDelete.Image = global::ErpBudgetBudgetEditor.Properties.Resources.treenode_delete16_h;
            this.mitemDelete.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.mitemDelete.Name = "mitemDelete";
            this.mitemDelete.Size = new System.Drawing.Size(187, 22);
            this.mitemDelete.Text = "Удалить подстатью";
            this.mitemDelete.Click += new System.EventHandler(this.mitemDelete_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(184, 6);
            // 
            // mitemTrnList
            // 
            this.mitemTrnList.Name = "mitemTrnList";
            this.mitemTrnList.Size = new System.Drawing.Size(187, 22);
            this.mitemTrnList.Text = "Журнал проводок";
            this.mitemTrnList.Click += new System.EventHandler(this.mitemTrnList_Click);
            // 
            // mitemDocList
            // 
            this.mitemDocList.Name = "mitemDocList";
            this.mitemDocList.Size = new System.Drawing.Size(187, 22);
            this.mitemDocList.Text = "Журнал заявок";
            this.mitemDocList.Click += new System.EventHandler(this.mitemDocList_Click);
            // 
            // mitemDetail
            // 
            this.mitemDetail.Name = "mitemDetail";
            this.mitemDetail.Size = new System.Drawing.Size(187, 22);
            this.mitemDetail.Text = "Подробнее...";
            this.mitemDetail.Click += new System.EventHandler(this.mitemDetail_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblInfo.Location = new System.Drawing.Point(3, 4);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(63, 13);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "labelControl1";
            // 
            // lblCurrencyRate
            // 
            this.lblCurrencyRate.Location = new System.Drawing.Point(3, 25);
            this.lblCurrencyRate.Name = "lblCurrencyRate";
            this.lblCurrencyRate.Size = new System.Drawing.Size(63, 13);
            this.lblCurrencyRate.TabIndex = 4;
            this.lblCurrencyRate.Text = "labelControl1";
            // 
            // frmBudgetEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 426);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBudgetEditor";
            this.toolTipController.SetSuperTip(this, null);
            this.Text = "Бюджет";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBudget_FormClosing);
            this.Load += new System.EventHandler(this.frmBudget_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemCalcEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemPopupContainerEdit)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barBtnAddNode;
        private DevExpress.XtraBars.BarButtonItem barBtnRefresh;
        private DevExpress.XtraBars.BarButtonItem barBtnDeleteNode;
        private DevExpress.XtraBars.BarButtonItem barBtnPrint;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraEditors.SimpleButton btnRight;
        private DevExpress.XtraEditors.SimpleButton btnDrop;
        private DevExpress.XtraEditors.SimpleButton btnDown;
        private DevExpress.XtraEditors.SimpleButton btnLeft;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnUp;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraBars.BarButtonItem barBtnAddChildNode;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mitemRefresh;
        private System.Windows.Forms.ToolStripMenuItem mitemAddChild;
        private System.Windows.Forms.ToolStripMenuItem mitemDelete;
        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDEBITARTICLE_NUM;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colREADONLY;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colJANUARY;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFEBRUARY;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMARCH;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colAPRIL;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMAY;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colJUNE;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colJULY;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colAUGUST;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSEPTEMBER;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colOCTEMBER;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDECEMBER;
        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit repItemPopupContainerEdit;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDEBITARTICLE_GUID_ID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNOVEMBER;
        private DevExpress.Utils.ToolTipController toolTipController;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSummary;
        private System.Windows.Forms.ImageList imglNodes;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDEBITARTICLE_MULTIBUDGETDEP;
        private DevExpress.XtraBars.BarButtonItem barBtnAccept;
        private DevExpress.XtraBars.BarStaticItem barTextAccept;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDEBITARTICLE_PARENT_ID;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mitemTrnList;
        private System.Windows.Forms.ToolStripMenuItem mitemDocList;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repItemCalcEdit;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private DevExpress.XtraEditors.LabelControl lblCurrencyRate;
        private DevExpress.XtraBars.BarButtonItem barBtnAddBudgetItem;
        private System.Windows.Forms.ToolStripMenuItem mitemAddBudgetItem;
        private System.Windows.Forms.ToolStripMenuItem mitemDetail;
    }
}