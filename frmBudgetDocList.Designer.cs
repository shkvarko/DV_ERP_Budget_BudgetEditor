namespace ErpBudgetBudgetEditor
{
    partial class frmBudgetDocList
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
            this.toolTipController = new DevExpress.Utils.ToolTipController( this.components );
            this.barManager = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barBtnPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnClose = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnExport = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.colDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDocState = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colBudgetItem = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDocMoney = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDocCurrency = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colMoney = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colUser = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDocObjective = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.lblDebitArticle = new System.Windows.Forms.Label();
            this.lblMoney = new System.Windows.Forms.Label();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.dataSet = new System.Data.DataSet();
            this.dtReport = new System.Data.DataTable();
            this.DOC_DATE = new System.Data.DataColumn();
            this.DOC_STATE = new System.Data.DataColumn();
            this.BUDGETITEM_NAME = new System.Data.DataColumn();
            this.DOC_SUM = new System.Data.DataColumn();
            this.DOC_CURRENCY = new System.Data.DataColumn();
            this.SUM = new System.Data.DataColumn();
            this.USER = new System.Data.DataColumn();
            this.OBJECTIVE = new System.Data.DataColumn();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDOC_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDOC_STATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBUDGETITEM_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDOC_SUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDOC_CURRENCY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUSER1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOBJECTIVE = new DevExpress.XtraGrid.Columns.GridColumn();
            ( ( System.ComponentModel.ISupportInitialize )( this.barManager ) ).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this.treeList ) ).BeginInit();
            ( ( System.ComponentModel.ISupportInitialize )( this.gridControl ) ).BeginInit();
            ( ( System.ComponentModel.ISupportInitialize )( this.dataSet ) ).BeginInit();
            ( ( System.ComponentModel.ISupportInitialize )( this.dtReport ) ).BeginInit();
            ( ( System.ComponentModel.ISupportInitialize )( this.gridView ) ).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange( new DevExpress.XtraBars.Bar[] {
            this.bar1} );
            this.barManager.DockControls.Add( this.barDockControlTop );
            this.barManager.DockControls.Add( this.barDockControlBottom );
            this.barManager.DockControls.Add( this.barDockControlLeft );
            this.barManager.DockControls.Add( this.barDockControlRight );
            this.barManager.Form = this;
            this.barManager.Items.AddRange( new DevExpress.XtraBars.BarItem[] {
            this.barBtnPrint,
            this.barBtnClose,
            this.barBtnExport} );
            this.barManager.MaxItemId = 4;
            this.barManager.ToolTipController = this.toolTipController;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 1";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange( new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnClose),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnExport)} );
            this.bar1.Text = "Custom 1";
            // 
            // barBtnPrint
            // 
            this.barBtnPrint.Caption = "Печать";
            this.barBtnPrint.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.printer2;
            this.barBtnPrint.Hint = "Печать";
            this.barBtnPrint.Id = 0;
            this.barBtnPrint.Name = "barBtnPrint";
            this.barBtnPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler( this.barBtnPrint_ItemClick );
            // 
            // barBtnClose
            // 
            this.barBtnClose.Caption = "Выход";
            this.barBtnClose.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.delete2;
            this.barBtnClose.Hint = "Выход";
            this.barBtnClose.Id = 1;
            this.barBtnClose.Name = "barBtnClose";
            this.barBtnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler( this.barBtnClose_ItemClick );
            // 
            // barBtnExport
            // 
            this.barBtnExport.Caption = "Экспорт";
            this.barBtnExport.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.export1;
            this.barBtnExport.Hint = "Экспорт данных в MS Excel";
            this.barBtnExport.Id = 3;
            this.barBtnExport.Name = "barBtnExport";
            this.barBtnExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler( this.barBtnExport_ItemClick );
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel2.Controls.Add( this.treeList, 0, 2 );
            this.tableLayoutPanel2.Controls.Add( this.lblDebitArticle, 0, 0 );
            this.tableLayoutPanel2.Controls.Add( this.lblMoney, 0, 1 );
            this.tableLayoutPanel2.Controls.Add( this.gridControl, 0, 3 );
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point( 0, 26 );
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding( 0 );
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 20F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 20F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 100F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel2.Size = new System.Drawing.Size( 549, 329 );
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // treeList
            // 
            this.treeList.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom ) 
            | System.Windows.Forms.AnchorStyles.Left ) 
            | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.treeList.Columns.AddRange( new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colDate,
            this.colDocState,
            this.colBudgetItem,
            this.colDocMoney,
            this.colDocCurrency,
            this.colMoney,
            this.colUser,
            this.colDocObjective} );
            this.treeList.Location = new System.Drawing.Point( 3, 43 );
            this.treeList.Name = "treeList";
            this.treeList.Size = new System.Drawing.Size( 543, 94 );
            this.treeList.TabIndex = 4;
            // 
            // colDate
            // 
            this.colDate.Caption = "Дата";
            this.colDate.FieldName = "Дата";
            this.colDate.Name = "colDate";
            this.colDate.OptionsColumn.AllowEdit = false;
            this.colDate.OptionsColumn.AllowFocus = false;
            this.colDate.OptionsColumn.ReadOnly = true;
            this.colDate.VisibleIndex = 0;
            this.colDate.Width = 76;
            // 
            // colDocState
            // 
            this.colDocState.Caption = "Состояние";
            this.colDocState.FieldName = "Состояние";
            this.colDocState.Name = "colDocState";
            this.colDocState.VisibleIndex = 1;
            // 
            // colBudgetItem
            // 
            this.colBudgetItem.Caption = "Статья";
            this.colBudgetItem.FieldName = "Состояние";
            this.colBudgetItem.Name = "colBudgetItem";
            this.colBudgetItem.OptionsColumn.AllowEdit = false;
            this.colBudgetItem.OptionsColumn.AllowFocus = false;
            this.colBudgetItem.OptionsColumn.ReadOnly = true;
            this.colBudgetItem.VisibleIndex = 2;
            this.colBudgetItem.Width = 89;
            // 
            // colDocMoney
            // 
            this.colDocMoney.AppearanceCell.Options.UseTextOptions = true;
            this.colDocMoney.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colDocMoney.Caption = "Сумма заявки";
            this.colDocMoney.FieldName = "Сумма заявки";
            this.colDocMoney.Format.FormatString = "### ### ##0.00";
            this.colDocMoney.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDocMoney.Name = "colDocMoney";
            this.colDocMoney.OptionsColumn.AllowEdit = false;
            this.colDocMoney.OptionsColumn.AllowFocus = false;
            this.colDocMoney.OptionsColumn.ReadOnly = true;
            this.colDocMoney.VisibleIndex = 3;
            this.colDocMoney.Width = 60;
            // 
            // colDocCurrency
            // 
            this.colDocCurrency.Caption = "Валюта";
            this.colDocCurrency.FieldName = "Валюта";
            this.colDocCurrency.Name = "colDocCurrency";
            this.colDocCurrency.OptionsColumn.AllowEdit = false;
            this.colDocCurrency.OptionsColumn.AllowFocus = false;
            this.colDocCurrency.OptionsColumn.ReadOnly = true;
            this.colDocCurrency.VisibleIndex = 4;
            this.colDocCurrency.Width = 50;
            // 
            // colMoney
            // 
            this.colMoney.AppearanceCell.Options.UseTextOptions = true;
            this.colMoney.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colMoney.Caption = "Сумма";
            this.colMoney.FieldName = "Сумма";
            this.colMoney.Format.FormatString = "### ### ##0.00";
            this.colMoney.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMoney.Name = "colMoney";
            this.colMoney.OptionsColumn.AllowEdit = false;
            this.colMoney.OptionsColumn.AllowFocus = false;
            this.colMoney.OptionsColumn.ReadOnly = true;
            this.colMoney.VisibleIndex = 5;
            this.colMoney.Width = 58;
            // 
            // colUser
            // 
            this.colUser.Caption = "Инициатор";
            this.colUser.FieldName = "Пользователь";
            this.colUser.Name = "colUser";
            this.colUser.OptionsColumn.AllowEdit = false;
            this.colUser.OptionsColumn.AllowFocus = false;
            this.colUser.OptionsColumn.ReadOnly = true;
            this.colUser.VisibleIndex = 6;
            this.colUser.Width = 136;
            // 
            // colDocObjective
            // 
            this.colDocObjective.Caption = "Цель";
            this.colDocObjective.FieldName = "Цель";
            this.colDocObjective.Name = "colDocObjective";
            this.colDocObjective.OptionsColumn.AllowEdit = false;
            this.colDocObjective.OptionsColumn.AllowFocus = false;
            this.colDocObjective.OptionsColumn.ReadOnly = true;
            this.colDocObjective.VisibleIndex = 7;
            this.colDocObjective.Width = 100;
            // 
            // lblDebitArticle
            // 
            this.lblDebitArticle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDebitArticle.AutoSize = true;
            this.lblDebitArticle.Font = new System.Drawing.Font( "Tahoma", 8.25F, System.Drawing.FontStyle.Bold );
            this.lblDebitArticle.Location = new System.Drawing.Point( 3, 3 );
            this.lblDebitArticle.Name = "lblDebitArticle";
            this.lblDebitArticle.Size = new System.Drawing.Size( 49, 13 );
            this.lblDebitArticle.TabIndex = 5;
            this.lblDebitArticle.Text = "Статья";
            // 
            // lblMoney
            // 
            this.lblMoney.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMoney.AutoSize = true;
            this.lblMoney.Font = new System.Drawing.Font( "Tahoma", 8.25F, System.Drawing.FontStyle.Bold );
            this.lblMoney.Location = new System.Drawing.Point( 3, 23 );
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size( 44, 13 );
            this.lblMoney.TabIndex = 6;
            this.lblMoney.Text = "Сумма";
            // 
            // gridControl
            // 
            this.gridControl.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom ) 
            | System.Windows.Forms.AnchorStyles.Left ) 
            | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.gridControl.DataMember = "dtReport";
            this.gridControl.DataSource = this.dataSet;
            // 
            // 
            // 
            this.gridControl.EmbeddedNavigator.Name = "";
            this.gridControl.Location = new System.Drawing.Point( 3, 143 );
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size( 543, 183 );
            this.gridControl.TabIndex = 7;
            this.gridControl.ViewCollection.AddRange( new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView} );
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "dataSet";
            this.dataSet.Tables.AddRange( new System.Data.DataTable[] {
            this.dtReport} );
            // 
            // dtReport
            // 
            this.dtReport.Columns.AddRange( new System.Data.DataColumn[] {
            this.DOC_DATE,
            this.DOC_STATE,
            this.BUDGETITEM_NAME,
            this.DOC_SUM,
            this.DOC_CURRENCY,
            this.SUM,
            this.USER,
            this.OBJECTIVE} );
            this.dtReport.TableName = "dtReport";
            // 
            // DOC_DATE
            // 
            this.DOC_DATE.ColumnName = "DOC_DATE";
            // 
            // DOC_STATE
            // 
            this.DOC_STATE.ColumnName = "DOC_STATE";
            // 
            // BUDGETITEM_NAME
            // 
            this.BUDGETITEM_NAME.ColumnName = "BUDGETITEM_NAME";
            // 
            // DOC_SUM
            // 
            this.DOC_SUM.ColumnName = "DOC_SUM";
            this.DOC_SUM.DataType = typeof( double );
            // 
            // DOC_CURRENCY
            // 
            this.DOC_CURRENCY.ColumnName = "DOC_CURRENCY";
            // 
            // SUM
            // 
            this.SUM.ColumnName = "SUM";
            this.SUM.DataType = typeof( double );
            // 
            // USER
            // 
            this.USER.ColumnName = "USER";
            // 
            // OBJECTIVE
            // 
            this.OBJECTIVE.ColumnName = "OBJECTIVE";
            // 
            // gridView
            // 
            this.gridView.Columns.AddRange( new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDOC_DATE,
            this.colDOC_STATE,
            this.colBUDGETITEM_NAME,
            this.colDOC_SUM,
            this.colDOC_CURRENCY,
            this.colSUM,
            this.colUSER1,
            this.colOBJECTIVE} );
            this.gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsView.ShowGroupPanel = false;
            // 
            // colDOC_DATE
            // 
            this.colDOC_DATE.Caption = "Дата документа";
            this.colDOC_DATE.FieldName = "DOC_DATE";
            this.colDOC_DATE.Name = "colDOC_DATE";
            this.colDOC_DATE.OptionsColumn.AllowEdit = false;
            this.colDOC_DATE.OptionsColumn.AllowFocus = false;
            this.colDOC_DATE.OptionsColumn.ReadOnly = true;
            this.colDOC_DATE.Visible = true;
            this.colDOC_DATE.VisibleIndex = 0;
            // 
            // colDOC_STATE
            // 
            this.colDOC_STATE.Caption = "Текущее состояние";
            this.colDOC_STATE.FieldName = "DOC_STATE";
            this.colDOC_STATE.Name = "colDOC_STATE";
            this.colDOC_STATE.OptionsColumn.AllowEdit = false;
            this.colDOC_STATE.OptionsColumn.AllowFocus = false;
            this.colDOC_STATE.OptionsColumn.ReadOnly = true;
            this.colDOC_STATE.Visible = true;
            this.colDOC_STATE.VisibleIndex = 1;
            // 
            // colBUDGETITEM_NAME
            // 
            this.colBUDGETITEM_NAME.Caption = "Статья";
            this.colBUDGETITEM_NAME.FieldName = "BUDGETITEM_NAME";
            this.colBUDGETITEM_NAME.Name = "colBUDGETITEM_NAME";
            this.colBUDGETITEM_NAME.OptionsColumn.AllowEdit = false;
            this.colBUDGETITEM_NAME.OptionsColumn.AllowFocus = false;
            this.colBUDGETITEM_NAME.OptionsColumn.ReadOnly = true;
            this.colBUDGETITEM_NAME.Visible = true;
            this.colBUDGETITEM_NAME.VisibleIndex = 2;
            // 
            // colDOC_SUM
            // 
            this.colDOC_SUM.Caption = "Сумма документа";
            this.colDOC_SUM.DisplayFormat.FormatString = "### ### ##0.00";
            this.colDOC_SUM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDOC_SUM.FieldName = "DOC_SUM";
            this.colDOC_SUM.Name = "colDOC_SUM";
            this.colDOC_SUM.OptionsColumn.AllowEdit = false;
            this.colDOC_SUM.OptionsColumn.AllowFocus = false;
            this.colDOC_SUM.OptionsColumn.ReadOnly = true;
            this.colDOC_SUM.Visible = true;
            this.colDOC_SUM.VisibleIndex = 3;
            // 
            // colDOC_CURRENCY
            // 
            this.colDOC_CURRENCY.Caption = "Валюта";
            this.colDOC_CURRENCY.FieldName = "DOC_CURRENCY";
            this.colDOC_CURRENCY.Name = "colDOC_CURRENCY";
            this.colDOC_CURRENCY.OptionsColumn.AllowEdit = false;
            this.colDOC_CURRENCY.OptionsColumn.AllowFocus = false;
            this.colDOC_CURRENCY.OptionsColumn.ReadOnly = true;
            this.colDOC_CURRENCY.Visible = true;
            this.colDOC_CURRENCY.VisibleIndex = 4;
            // 
            // colSUM
            // 
            this.colSUM.Caption = "Сумма";
            this.colSUM.DisplayFormat.FormatString = "### ### ##0.00";
            this.colSUM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSUM.FieldName = "SUM";
            this.colSUM.Name = "colSUM";
            this.colSUM.OptionsColumn.AllowEdit = false;
            this.colSUM.OptionsColumn.AllowFocus = false;
            this.colSUM.OptionsColumn.ReadOnly = true;
            this.colSUM.Visible = true;
            this.colSUM.VisibleIndex = 5;
            // 
            // colUSER1
            // 
            this.colUSER1.Caption = "Инициатор";
            this.colUSER1.FieldName = "USER";
            this.colUSER1.Name = "colUSER1";
            this.colUSER1.OptionsColumn.AllowEdit = false;
            this.colUSER1.OptionsColumn.AllowFocus = false;
            this.colUSER1.OptionsColumn.ReadOnly = true;
            this.colUSER1.Visible = true;
            this.colUSER1.VisibleIndex = 6;
            // 
            // colOBJECTIVE
            // 
            this.colOBJECTIVE.Caption = "Цель";
            this.colOBJECTIVE.FieldName = "OBJECTIVE";
            this.colOBJECTIVE.Name = "colOBJECTIVE";
            this.colOBJECTIVE.OptionsColumn.AllowEdit = false;
            this.colOBJECTIVE.OptionsColumn.AllowFocus = false;
            this.colOBJECTIVE.OptionsColumn.ReadOnly = true;
            this.colOBJECTIVE.Visible = true;
            this.colOBJECTIVE.VisibleIndex = 7;
            // 
            // frmBudgetDocList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 549, 355 );
            this.Controls.Add( this.tableLayoutPanel2 );
            this.Controls.Add( this.barDockControlLeft );
            this.Controls.Add( this.barDockControlRight );
            this.Controls.Add( this.barDockControlBottom );
            this.Controls.Add( this.barDockControlTop );
            this.MinimumSize = new System.Drawing.Size( 500, 300 );
            this.Name = "frmBudgetDocList";
            this.Text = "Журнал заявок";
            this.Load += new System.EventHandler( this.frmBudgetDocList_Load );
            ( ( System.ComponentModel.ISupportInitialize )( this.barManager ) ).EndInit();
            this.tableLayoutPanel2.ResumeLayout( false );
            this.tableLayoutPanel2.PerformLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this.treeList ) ).EndInit();
            ( ( System.ComponentModel.ISupportInitialize )( this.gridControl ) ).EndInit();
            ( ( System.ComponentModel.ISupportInitialize )( this.dataSet ) ).EndInit();
            ( ( System.ComponentModel.ISupportInitialize )( this.dtReport ) ).EndInit();
            ( ( System.ComponentModel.ISupportInitialize )( this.gridView ) ).EndInit();
            this.ResumeLayout( false );

        }

        #endregion

        private DevExpress.Utils.ToolTipController toolTipController;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barBtnPrint;
        private DevExpress.XtraBars.BarButtonItem barBtnClose;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colBudgetItem;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDocMoney;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDocCurrency;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMoney;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colUser;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDocObjective;
        private System.Windows.Forms.Label lblDebitArticle;
        private System.Windows.Forms.Label lblMoney;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDocState;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private System.Data.DataSet dataSet;
        private System.Data.DataTable dtReport;
        private System.Data.DataColumn DOC_DATE;
        private System.Data.DataColumn DOC_STATE;
        private System.Data.DataColumn BUDGETITEM_NAME;
        private System.Data.DataColumn DOC_SUM;
        private System.Data.DataColumn DOC_CURRENCY;
        private System.Data.DataColumn SUM;
        private System.Data.DataColumn USER;
        private System.Data.DataColumn OBJECTIVE;
        private DevExpress.XtraGrid.Columns.GridColumn colDOC_DATE;
        private DevExpress.XtraGrid.Columns.GridColumn colDOC_STATE;
        private DevExpress.XtraGrid.Columns.GridColumn colBUDGETITEM_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn colDOC_SUM;
        private DevExpress.XtraGrid.Columns.GridColumn colDOC_CURRENCY;
        private DevExpress.XtraGrid.Columns.GridColumn colSUM;
        private DevExpress.XtraGrid.Columns.GridColumn colUSER1;
        private DevExpress.XtraGrid.Columns.GridColumn colOBJECTIVE;
        private DevExpress.XtraBars.BarButtonItem barBtnExport;
    }
}