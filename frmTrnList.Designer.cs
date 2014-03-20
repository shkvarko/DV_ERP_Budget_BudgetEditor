namespace ErpBudgetBudgetEditor
{
    partial class frmTrnList
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
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barBtnPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.colDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colOperation = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTrnMoney = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.coltrnCurrency = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colUser = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDocDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDocMoney = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDocCurrency = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDocObjective = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDebitArticle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.barBtnPrint,
            this.barBtnClose});
            this.barManager.MaxItemId = 2;
            this.barManager.ToolTipController = this.toolTipController;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 1";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnClose)});
            this.bar1.Text = "Custom 1";
            // 
            // barBtnPrint
            // 
            this.barBtnPrint.Caption = "Печать";
            this.barBtnPrint.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.printer2;
            this.barBtnPrint.Hint = "Печать";
            this.barBtnPrint.Id = 0;
            this.barBtnPrint.Name = "barBtnPrint";
            this.barBtnPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnPrint_ItemClick);
            // 
            // barBtnClose
            // 
            this.barBtnClose.Caption = "Выход";
            this.barBtnClose.Glyph = global::ErpBudgetBudgetEditor.Properties.Resources.delete2;
            this.barBtnClose.Hint = "Выход";
            this.barBtnClose.Id = 1;
            this.barBtnClose.Name = "barBtnClose";
            this.barBtnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnClose_ItemClick);
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
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.treeList, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 26);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(549, 329);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel2, null);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // treeList
            // 
            this.treeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colDate,
            this.colOperation,
            this.colTrnMoney,
            this.coltrnCurrency,
            this.colUser,
            this.colDocDate,
            this.colDocMoney,
            this.colDocCurrency,
            this.colDocObjective});
            this.treeList.Location = new System.Drawing.Point(3, 23);
            this.treeList.Name = "treeList";
            this.treeList.OptionsView.ShowSummaryFooter = true;
            this.treeList.Size = new System.Drawing.Size(543, 303);
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
            this.colDate.Visible = true;
            this.colDate.VisibleIndex = 0;
            this.colDate.Width = 76;
            // 
            // colOperation
            // 
            this.colOperation.Caption = "Операция";
            this.colOperation.FieldName = "Состояние";
            this.colOperation.Name = "colOperation";
            this.colOperation.OptionsColumn.AllowEdit = false;
            this.colOperation.OptionsColumn.AllowFocus = false;
            this.colOperation.OptionsColumn.ReadOnly = true;
            this.colOperation.Visible = true;
            this.colOperation.VisibleIndex = 1;
            this.colOperation.Width = 89;
            // 
            // colTrnMoney
            // 
            this.colTrnMoney.AppearanceCell.Options.UseTextOptions = true;
            this.colTrnMoney.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTrnMoney.Caption = "Сумма";
            this.colTrnMoney.FieldName = "Сумма";
            this.colTrnMoney.Format.FormatString = "### ### ##0.00";
            this.colTrnMoney.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTrnMoney.Name = "colTrnMoney";
            this.colTrnMoney.OptionsColumn.AllowEdit = false;
            this.colTrnMoney.OptionsColumn.AllowFocus = false;
            this.colTrnMoney.OptionsColumn.ReadOnly = true;
            this.colTrnMoney.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colTrnMoney.SummaryFooterStrFormat = "{0:### ### ##0.00}";
            this.colTrnMoney.Visible = true;
            this.colTrnMoney.VisibleIndex = 2;
            this.colTrnMoney.Width = 58;
            // 
            // coltrnCurrency
            // 
            this.coltrnCurrency.Caption = "Валюта";
            this.coltrnCurrency.FieldName = "Валюта";
            this.coltrnCurrency.Name = "coltrnCurrency";
            this.coltrnCurrency.OptionsColumn.AllowEdit = false;
            this.coltrnCurrency.OptionsColumn.AllowFocus = false;
            this.coltrnCurrency.OptionsColumn.ReadOnly = true;
            this.coltrnCurrency.Visible = true;
            this.coltrnCurrency.VisibleIndex = 3;
            this.coltrnCurrency.Width = 53;
            // 
            // colUser
            // 
            this.colUser.Caption = "Пользователь";
            this.colUser.FieldName = "Пользователь";
            this.colUser.Name = "colUser";
            this.colUser.OptionsColumn.AllowEdit = false;
            this.colUser.OptionsColumn.AllowFocus = false;
            this.colUser.OptionsColumn.ReadOnly = true;
            this.colUser.Visible = true;
            this.colUser.VisibleIndex = 4;
            this.colUser.Width = 136;
            // 
            // colDocDate
            // 
            this.colDocDate.Caption = "Дата завки";
            this.colDocDate.FieldName = "Дата завки";
            this.colDocDate.Name = "colDocDate";
            this.colDocDate.OptionsColumn.AllowEdit = false;
            this.colDocDate.OptionsColumn.AllowFocus = false;
            this.colDocDate.OptionsColumn.ReadOnly = true;
            this.colDocDate.Visible = true;
            this.colDocDate.VisibleIndex = 5;
            this.colDocDate.Width = 76;
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
            this.colDocMoney.Visible = true;
            this.colDocMoney.VisibleIndex = 6;
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
            this.colDocCurrency.Visible = true;
            this.colDocCurrency.VisibleIndex = 7;
            this.colDocCurrency.Width = 50;
            // 
            // colDocObjective
            // 
            this.colDocObjective.Caption = "Цель";
            this.colDocObjective.FieldName = "Цель";
            this.colDocObjective.Name = "colDocObjective";
            this.colDocObjective.OptionsColumn.AllowEdit = false;
            this.colDocObjective.OptionsColumn.AllowFocus = false;
            this.colDocObjective.OptionsColumn.ReadOnly = true;
            this.colDocObjective.Visible = true;
            this.colDocObjective.VisibleIndex = 8;
            this.colDocObjective.Width = 100;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblDebitArticle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(549, 20);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel1, null);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // lblDebitArticle
            // 
            this.lblDebitArticle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDebitArticle.AutoSize = true;
            this.lblDebitArticle.Location = new System.Drawing.Point(61, 3);
            this.lblDebitArticle.Name = "lblDebitArticle";
            this.lblDebitArticle.Size = new System.Drawing.Size(72, 13);
            this.toolTipController.SetSuperTip(this.lblDebitArticle, null);
            this.lblDebitArticle.TabIndex = 1;
            this.lblDebitArticle.Text = "lblDebitArticle";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.toolTipController.SetSuperTip(this.label2, null);
            this.label2.TabIndex = 1;
            this.label2.Text = "Статья:";
            // 
            // frmTrnList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 355);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "frmTrnList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.toolTipController.SetSuperTip(this, null);
            this.Text = "Журнал проводок";
            this.Load += new System.EventHandler(this.frmTrnList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

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
        private DevExpress.XtraTreeList.Columns.TreeListColumn colOperation;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colUser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblDebitArticle;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTrnMoney;
        private DevExpress.XtraTreeList.Columns.TreeListColumn coltrnCurrency;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDocDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDocMoney;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDocCurrency;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDocObjective;
    }
}