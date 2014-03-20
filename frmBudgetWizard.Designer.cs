namespace ErpBudgetBudgetEditor
{
    partial class frmBudgetWizard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barBtnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.tableLayoutBugrnd = new System.Windows.Forms.TableLayoutPanel();
            this.treeListBudget = new DevExpress.XtraTreeList.TreeList();
            this.colBudgetDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colBudgetName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colBudgetDep = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCurrency = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colBudgetDateAccept = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tableLayoutBugrndDescrpn = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutDescrpn = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.cboxBudgetDep = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBudgetName = new DevExpress.XtraEditors.TextEdit();
            this.dtBudgetDate = new DevExpress.XtraEditors.DateEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.cboxBudgetProject = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.cboxBudgetType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.txtBudgetManagerList = new DevExpress.XtraEditors.MemoEdit();
            this.btnLoadBudgetManagerList = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grCurremncyRate = new DevExpress.XtraEditors.GroupControl();
            this.tableLayCurrencyRate = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.cboxBudgetCurrency = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.checkOffExp = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.tableLayoutBugrnd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListBudget)).BeginInit();
            this.tableLayoutBugrndDescrpn.SuspendLayout();
            this.tableLayoutDescrpn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxBudgetDep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBudgetName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBudgetDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBudgetDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxBudgetProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxBudgetType.Properties)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBudgetManagerList.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grCurremncyRate)).BeginInit();
            this.grCurremncyRate.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxBudgetCurrency.Properties)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkOffExp.Properties)).BeginInit();
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
            this.barBtnRefresh,
            this.barBtnAdd,
            this.barBtnDelete,
            this.barBtnPrint,
            this.barBtnCopy});
            this.barManager.MaxItemId = 5;
            this.barManager.ToolTipController = this.toolTipController;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 1";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnCopy)});
            this.bar1.Text = "Custom 1";
            // 
            // barBtnRefresh
            // 
            this.barBtnRefresh.Caption = "barBtnRefresh";
            this.barBtnRefresh.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.refresh;
            this.barBtnRefresh.Hint = "Обновить список";
            this.barBtnRefresh.Id = 0;
            this.barBtnRefresh.Name = "barBtnRefresh";
            this.barBtnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnRefresh_ItemClick);
            // 
            // barBtnAdd
            // 
            this.barBtnAdd.Caption = "barBtnAdd";
            this.barBtnAdd.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.add2;
            this.barBtnAdd.Id = 1;
            this.barBtnAdd.Name = "barBtnAdd";
            this.barBtnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAdd_ItemClick);
            // 
            // barBtnDelete
            // 
            this.barBtnDelete.Caption = "barBtnDelete";
            this.barBtnDelete.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.delete2;
            this.barBtnDelete.Hint = "Удалить запись";
            this.barBtnDelete.Id = 2;
            this.barBtnDelete.Name = "barBtnDelete";
            this.barBtnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDelete_ItemClick);
            // 
            // barBtnPrint
            // 
            this.barBtnPrint.Caption = "barBtnPrint";
            this.barBtnPrint.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.printer2;
            this.barBtnPrint.Hint = "Печать";
            this.barBtnPrint.Id = 3;
            this.barBtnPrint.Name = "barBtnPrint";
            // 
            // barBtnCopy
            // 
            this.barBtnCopy.Caption = "Копировать";
            this.barBtnCopy.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.document_into;
            this.barBtnCopy.Hint = "Копировать бюджет";
            this.barBtnCopy.Id = 4;
            this.barBtnCopy.Name = "barBtnCopy";
            this.barBtnCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnCopy_ItemClick);
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
            // tableLayoutBugrnd
            // 
            this.tableLayoutBugrnd.ColumnCount = 1;
            this.tableLayoutBugrnd.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutBugrnd.Controls.Add(this.treeListBudget, 0, 0);
            this.tableLayoutBugrnd.Controls.Add(this.tableLayoutBugrndDescrpn, 0, 1);
            this.tableLayoutBugrnd.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutBugrnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutBugrnd.Location = new System.Drawing.Point(0, 26);
            this.tableLayoutBugrnd.Name = "tableLayoutBugrnd";
            this.tableLayoutBugrnd.RowCount = 3;
            this.tableLayoutBugrnd.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutBugrnd.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.tableLayoutBugrnd.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutBugrnd.Size = new System.Drawing.Size(634, 433);
            this.toolTipController.SetSuperTip(this.tableLayoutBugrnd, null);
            this.tableLayoutBugrnd.TabIndex = 5;
            // 
            // treeListBudget
            // 
            this.treeListBudget.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListBudget.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colBudgetDate,
            this.colBudgetName,
            this.colBudgetDep,
            this.colCurrency,
            this.colBudgetDateAccept});
            this.treeListBudget.Location = new System.Drawing.Point(3, 3);
            this.treeListBudget.Name = "treeListBudget";
            this.treeListBudget.OptionsBehavior.Editable = false;
            this.treeListBudget.ParentFieldName = "";
            this.treeListBudget.Size = new System.Drawing.Size(628, 234);
            this.treeListBudget.TabIndex = 0;
            this.treeListBudget.BeforeFocusNode += new DevExpress.XtraTreeList.BeforeFocusNodeEventHandler(this.treeListBudget_BeforeFocusNode);
            this.treeListBudget.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListBudget_FocusedNodeChanged);
            // 
            // colBudgetDate
            // 
            this.colBudgetDate.Caption = "Период";
            this.colBudgetDate.FieldName = "Тип документа";
            this.colBudgetDate.Name = "colBudgetDate";
            this.colBudgetDate.OptionsColumn.AllowEdit = false;
            this.colBudgetDate.OptionsColumn.AllowFocus = false;
            this.colBudgetDate.OptionsColumn.ReadOnly = true;
            this.colBudgetDate.Visible = true;
            this.colBudgetDate.VisibleIndex = 1;
            this.colBudgetDate.Width = 119;
            // 
            // colBudgetName
            // 
            this.colBudgetName.Caption = "Имя";
            this.colBudgetName.FieldName = "Имя";
            this.colBudgetName.Name = "colBudgetName";
            this.colBudgetName.OptionsColumn.AllowEdit = false;
            this.colBudgetName.OptionsColumn.AllowFocus = false;
            this.colBudgetName.OptionsColumn.ReadOnly = true;
            this.colBudgetName.Visible = true;
            this.colBudgetName.VisibleIndex = 0;
            this.colBudgetName.Width = 124;
            // 
            // colBudgetDep
            // 
            this.colBudgetDep.Caption = "Служба";
            this.colBudgetDep.FieldName = "Действие";
            this.colBudgetDep.Name = "colBudgetDep";
            this.colBudgetDep.OptionsColumn.AllowEdit = false;
            this.colBudgetDep.OptionsColumn.AllowFocus = false;
            this.colBudgetDep.OptionsColumn.ReadOnly = true;
            this.colBudgetDep.Visible = true;
            this.colBudgetDep.VisibleIndex = 2;
            this.colBudgetDep.Width = 119;
            // 
            // colCurrency
            // 
            this.colCurrency.Caption = "Валюта";
            this.colCurrency.FieldName = "Роль";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.OptionsColumn.AllowEdit = false;
            this.colCurrency.OptionsColumn.AllowFocus = false;
            this.colCurrency.OptionsColumn.ReadOnly = true;
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 3;
            this.colCurrency.Width = 119;
            // 
            // colBudgetDateAccept
            // 
            this.colBudgetDateAccept.Caption = "Дата утверждения";
            this.colBudgetDateAccept.FieldName = "Дата утверждения";
            this.colBudgetDateAccept.Name = "colBudgetDateAccept";
            this.colBudgetDateAccept.OptionsColumn.AllowEdit = false;
            this.colBudgetDateAccept.OptionsColumn.AllowFocus = false;
            this.colBudgetDateAccept.OptionsColumn.ReadOnly = true;
            this.colBudgetDateAccept.Visible = true;
            this.colBudgetDateAccept.VisibleIndex = 4;
            this.colBudgetDateAccept.Width = 119;
            // 
            // tableLayoutBugrndDescrpn
            // 
            this.tableLayoutBugrndDescrpn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutBugrndDescrpn.ColumnCount = 2;
            this.tableLayoutBugrndDescrpn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutBugrndDescrpn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 269F));
            this.tableLayoutBugrndDescrpn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutBugrndDescrpn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutBugrndDescrpn.Controls.Add(this.tableLayoutDescrpn, 0, 0);
            this.tableLayoutBugrndDescrpn.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tableLayoutBugrndDescrpn.Location = new System.Drawing.Point(0, 240);
            this.tableLayoutBugrndDescrpn.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutBugrndDescrpn.Name = "tableLayoutBugrndDescrpn";
            this.tableLayoutBugrndDescrpn.RowCount = 1;
            this.tableLayoutBugrndDescrpn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutBugrndDescrpn.Size = new System.Drawing.Size(634, 162);
            this.toolTipController.SetSuperTip(this.tableLayoutBugrndDescrpn, null);
            this.tableLayoutBugrndDescrpn.TabIndex = 1;
            // 
            // tableLayoutDescrpn
            // 
            this.tableLayoutDescrpn.ColumnCount = 2;
            this.tableLayoutDescrpn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutDescrpn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutDescrpn.Controls.Add(this.label7, 0, 5);
            this.tableLayoutDescrpn.Controls.Add(this.cboxBudgetDep, 1, 2);
            this.tableLayoutDescrpn.Controls.Add(this.label1, 0, 0);
            this.tableLayoutDescrpn.Controls.Add(this.label2, 0, 1);
            this.tableLayoutDescrpn.Controls.Add(this.label3, 0, 2);
            this.tableLayoutDescrpn.Controls.Add(this.txtBudgetName, 1, 0);
            this.tableLayoutDescrpn.Controls.Add(this.dtBudgetDate, 1, 1);
            this.tableLayoutDescrpn.Controls.Add(this.label5, 0, 3);
            this.tableLayoutDescrpn.Controls.Add(this.cboxBudgetProject, 1, 3);
            this.tableLayoutDescrpn.Controls.Add(this.label6, 0, 4);
            this.tableLayoutDescrpn.Controls.Add(this.cboxBudgetType, 1, 4);
            this.tableLayoutDescrpn.Controls.Add(this.tableLayoutPanel4, 1, 5);
            this.tableLayoutDescrpn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutDescrpn.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutDescrpn.Name = "tableLayoutDescrpn";
            this.tableLayoutDescrpn.RowCount = 6;
            this.tableLayoutDescrpn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutDescrpn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutDescrpn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutDescrpn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutDescrpn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutDescrpn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutDescrpn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutDescrpn.Size = new System.Drawing.Size(359, 156);
            this.toolTipController.SetSuperTip(this.tableLayoutDescrpn, null);
            this.tableLayoutDescrpn.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 26);
            this.toolTipController.SetSuperTip(this.label7, null);
            this.label7.TabIndex = 8;
            this.label7.Text = "Ограничения доступа";
            // 
            // cboxBudgetDep
            // 
            this.cboxBudgetDep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxBudgetDep.Location = new System.Drawing.Point(89, 53);
            this.cboxBudgetDep.Name = "cboxBudgetDep";
            this.cboxBudgetDep.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxBudgetDep.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboxBudgetDep.Size = new System.Drawing.Size(267, 20);
            this.cboxBudgetDep.TabIndex = 2;
            this.cboxBudgetDep.ToolTip = "Бюджетное подразделение";
            this.cboxBudgetDep.ToolTipController = this.toolTipController;
            this.cboxBudgetDep.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.cboxBudgetDep.EditValueChanged += new System.EventHandler(this.BudgetEditValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.toolTipController.SetSuperTip(this.label1, null);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.toolTipController.SetSuperTip(this.label2, null);
            this.label2.TabIndex = 1;
            this.label2.Text = "Период:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.toolTipController.SetSuperTip(this.label3, null);
            this.label3.TabIndex = 2;
            this.label3.Text = "Служба:";
            // 
            // txtBudgetName
            // 
            this.txtBudgetName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBudgetName.Location = new System.Drawing.Point(89, 3);
            this.txtBudgetName.Name = "txtBudgetName";
            this.txtBudgetName.Size = new System.Drawing.Size(267, 20);
            this.txtBudgetName.TabIndex = 0;
            this.txtBudgetName.ToolTip = "Наименование";
            this.txtBudgetName.ToolTipController = this.toolTipController;
            this.txtBudgetName.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.txtBudgetName.EditValueChanged += new System.EventHandler(this.BudgetEditValueChanged);
            // 
            // dtBudgetDate
            // 
            this.dtBudgetDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtBudgetDate.EditValue = null;
            this.dtBudgetDate.Location = new System.Drawing.Point(89, 28);
            this.dtBudgetDate.Name = "dtBudgetDate";
            this.dtBudgetDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtBudgetDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtBudgetDate.Size = new System.Drawing.Size(267, 20);
            this.dtBudgetDate.TabIndex = 1;
            this.dtBudgetDate.ToolTip = "Дата бюджета";
            this.dtBudgetDate.ToolTipController = this.toolTipController;
            this.dtBudgetDate.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.dtBudgetDate.EditValueChanged += new System.EventHandler(this.BudgetEditValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.toolTipController.SetSuperTip(this.label5, null);
            this.label5.TabIndex = 6;
            this.label5.Text = "Проект:";
            // 
            // cboxBudgetProject
            // 
            this.cboxBudgetProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxBudgetProject.Location = new System.Drawing.Point(89, 78);
            this.cboxBudgetProject.Name = "cboxBudgetProject";
            this.cboxBudgetProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxBudgetProject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboxBudgetProject.Size = new System.Drawing.Size(267, 20);
            this.cboxBudgetProject.TabIndex = 3;
            this.cboxBudgetProject.ToolTip = "Проект";
            this.cboxBudgetProject.ToolTipController = this.toolTipController;
            this.cboxBudgetProject.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.cboxBudgetProject.EditValueChanged += new System.EventHandler(this.BudgetEditValueChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.toolTipController.SetSuperTip(this.label6, null);
            this.label6.TabIndex = 7;
            this.label6.Text = "Тип бюджета:";
            // 
            // cboxBudgetType
            // 
            this.cboxBudgetType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxBudgetType.Location = new System.Drawing.Point(89, 103);
            this.cboxBudgetType.Name = "cboxBudgetType";
            this.cboxBudgetType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxBudgetType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboxBudgetType.Size = new System.Drawing.Size(267, 20);
            this.cboxBudgetType.TabIndex = 5;
            this.cboxBudgetType.ToolTip = "Тип бюджета";
            this.cboxBudgetType.ToolTipController = this.toolTipController;
            this.cboxBudgetType.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.cboxBudgetType.EditValueChanged += new System.EventHandler(this.BudgetEditValueChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel4.Controls.Add(this.txtBudgetManagerList, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnLoadBudgetManagerList, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(86, 125);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(273, 31);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel4, null);
            this.tableLayoutPanel4.TabIndex = 9;
            // 
            // txtBudgetManagerList
            // 
            this.txtBudgetManagerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBudgetManagerList.Location = new System.Drawing.Point(3, 3);
            this.txtBudgetManagerList.Name = "txtBudgetManagerList";
            this.txtBudgetManagerList.Properties.ReadOnly = true;
            this.txtBudgetManagerList.Size = new System.Drawing.Size(234, 25);
            this.txtBudgetManagerList.TabIndex = 0;
            // 
            // btnLoadBudgetManagerList
            // 
            this.btnLoadBudgetManagerList.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnLoadBudgetManagerList.Location = new System.Drawing.Point(241, 4);
            this.btnLoadBudgetManagerList.Margin = new System.Windows.Forms.Padding(1);
            this.btnLoadBudgetManagerList.Name = "btnLoadBudgetManagerList";
            this.btnLoadBudgetManagerList.Size = new System.Drawing.Size(25, 23);
            this.btnLoadBudgetManagerList.TabIndex = 1;
            this.btnLoadBudgetManagerList.Text = "...";
            this.btnLoadBudgetManagerList.Click += new System.EventHandler(this.btnLoadBudgetManagerList_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grCurremncyRate, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(365, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(269, 162);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel1, null);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // grCurremncyRate
            // 
            this.grCurremncyRate.Controls.Add(this.tableLayCurrencyRate);
            this.grCurremncyRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grCurremncyRate.Location = new System.Drawing.Point(3, 28);
            this.grCurremncyRate.Name = "grCurremncyRate";
            this.grCurremncyRate.Size = new System.Drawing.Size(263, 131);
            this.toolTipController.SetSuperTip(this.grCurremncyRate, null);
            this.grCurremncyRate.TabIndex = 2;
            this.grCurremncyRate.Text = "Курсы валют";
            // 
            // tableLayCurrencyRate
            // 
            this.tableLayCurrencyRate.ColumnCount = 2;
            this.tableLayCurrencyRate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayCurrencyRate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayCurrencyRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayCurrencyRate.Location = new System.Drawing.Point(2, 20);
            this.tableLayCurrencyRate.Name = "tableLayCurrencyRate";
            this.tableLayCurrencyRate.RowCount = 1;
            this.tableLayCurrencyRate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayCurrencyRate.Size = new System.Drawing.Size(259, 109);
            this.toolTipController.SetSuperTip(this.tableLayCurrencyRate, null);
            this.tableLayCurrencyRate.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.cboxBudgetCurrency, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(269, 25);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel3, null);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // cboxBudgetCurrency
            // 
            this.cboxBudgetCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxBudgetCurrency.Location = new System.Drawing.Point(113, 3);
            this.cboxBudgetCurrency.Name = "cboxBudgetCurrency";
            this.cboxBudgetCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxBudgetCurrency.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboxBudgetCurrency.Size = new System.Drawing.Size(153, 20);
            this.cboxBudgetCurrency.TabIndex = 4;
            this.cboxBudgetCurrency.ToolTip = "Валюта бюджета";
            this.cboxBudgetCurrency.ToolTipController = this.toolTipController;
            this.cboxBudgetCurrency.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.cboxBudgetCurrency.EditValueChanged += new System.EventHandler(this.BudgetEditValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.toolTipController.SetSuperTip(this.label4, null);
            this.label4.TabIndex = 3;
            this.label4.Text = "Валюта бюджета:";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 179F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.checkOffExp, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 402);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(634, 31);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel2, null);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::ErpBudgetBudgetEditor.Properties.Resources.disk_blue_ok;
            this.btnSave.Location = new System.Drawing.Point(445, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Сохранить";
            this.btnSave.ToolTip = "Сохранить изменения";
            this.btnSave.ToolTipController = this.toolTipController;
            this.btnSave.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::ErpBudgetBudgetEditor.Properties.Resources.delete2;
            this.btnCancel.Location = new System.Drawing.Point(541, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отменить";
            this.btnCancel.ToolTip = "Отменить изменения";
            this.btnCancel.ToolTipController = this.toolTipController;
            this.btnCancel.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // checkOffExp
            // 
            this.checkOffExp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkOffExp.Location = new System.Drawing.Point(3, 6);
            this.checkOffExp.Name = "checkOffExp";
            this.checkOffExp.Properties.Caption = "Внебюджетные расходы";
            this.checkOffExp.Size = new System.Drawing.Size(245, 19);
            this.checkOffExp.TabIndex = 5;
            this.checkOffExp.ToolTip = "Признак \"Внебюджетные расходы\"";
            this.checkOffExp.Visible = false;
            this.checkOffExp.EditValueChanged += new System.EventHandler(this.BudgetEditValueChanged);
            // 
            // frmBudgetWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 459);
            this.Controls.Add(this.tableLayoutBugrnd);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmBudgetWizard";
            this.toolTipController.SetSuperTip(this, null);
            this.Text = "Список бюджетов";
            this.Load += new System.EventHandler(this.frmBudgetWizard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.tableLayoutBugrnd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListBudget)).EndInit();
            this.tableLayoutBugrndDescrpn.ResumeLayout(false);
            this.tableLayoutDescrpn.ResumeLayout(false);
            this.tableLayoutDescrpn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxBudgetDep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBudgetName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBudgetDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBudgetDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxBudgetProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxBudgetType.Properties)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtBudgetManagerList.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grCurremncyRate)).EndInit();
            this.grCurremncyRate.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxBudgetCurrency.Properties)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkOffExp.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barBtnAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnDelete;
        private DevExpress.XtraBars.BarButtonItem barBtnPrint;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barBtnRefresh;
        private DevExpress.Utils.ToolTipController toolTipController;
        private System.Windows.Forms.TableLayoutPanel tableLayoutBugrnd;
        private DevExpress.XtraTreeList.TreeList treeListBudget;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colBudgetName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colBudgetDep;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCurrency;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colBudgetDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutBugrndDescrpn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutDescrpn;
        private DevExpress.XtraEditors.ComboBoxEdit cboxBudgetCurrency;
        private DevExpress.XtraEditors.ComboBoxEdit cboxBudgetDep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtBudgetName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.DateEdit dtBudgetDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colBudgetDateAccept;
        private DevExpress.XtraEditors.CheckEdit checkOffExp;
        private DevExpress.XtraBars.BarButtonItem barBtnCopy;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.ComboBoxEdit cboxBudgetProject;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.ComboBoxEdit cboxBudgetType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.GroupControl grCurremncyRate;
        private System.Windows.Forms.TableLayoutPanel tableLayCurrencyRate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private DevExpress.XtraEditors.MemoEdit txtBudgetManagerList;
        private DevExpress.XtraEditors.SimpleButton btnLoadBudgetManagerList;
    }
}