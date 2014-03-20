namespace ErpBudgetBudgetEditor
{
    partial class frmBudgetItemDetail
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBudgetItemDetail));
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.colMonth = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colAcceptPlan = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPermit = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colReserve = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colFact = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCredit = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRest = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cboxBudgetItem = new DevExpress.XtraEditors.ComboBoxEdit();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barBtnPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnClose = new DevExpress.XtraBars.BarButtonItem();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxBudgetItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.SuspendLayout();
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
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(501, 318);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel2, null);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // treeList
            // 
            this.treeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colMonth,
            this.colAcceptPlan,
            this.colPermit,
            this.colReserve,
            this.colFact,
            this.colCredit,
            this.colRest});
            this.treeList.Location = new System.Drawing.Point(3, 30);
            this.treeList.Name = "treeList";
            this.treeList.OptionsView.ShowSummaryFooter = true;
            this.treeList.Size = new System.Drawing.Size(495, 285);
            this.treeList.TabIndex = 4;
            // 
            // colMonth
            // 
            this.colMonth.Caption = "Месяц";
            this.colMonth.FieldName = "Месяц";
            this.colMonth.Name = "colMonth";
            this.colMonth.OptionsColumn.AllowEdit = false;
            this.colMonth.OptionsColumn.AllowFocus = false;
            this.colMonth.OptionsColumn.ReadOnly = true;
            this.colMonth.Visible = true;
            this.colMonth.VisibleIndex = 0;
            this.colMonth.Width = 89;
            // 
            // colAcceptPlan
            // 
            this.colAcceptPlan.AppearanceCell.Options.UseTextOptions = true;
            this.colAcceptPlan.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colAcceptPlan.Caption = "План (утв.)";
            this.colAcceptPlan.FieldName = "План (утвержденный)";
            this.colAcceptPlan.Format.FormatString = "### ### ##0.00";
            this.colAcceptPlan.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAcceptPlan.Name = "colAcceptPlan";
            this.colAcceptPlan.OptionsColumn.AllowEdit = false;
            this.colAcceptPlan.OptionsColumn.AllowFocus = false;
            this.colAcceptPlan.OptionsColumn.ReadOnly = true;
            this.colAcceptPlan.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colAcceptPlan.SummaryFooterStrFormat = "{0:### ### ##0.00}";
            this.colAcceptPlan.Visible = true;
            this.colAcceptPlan.VisibleIndex = 1;
            this.colAcceptPlan.Width = 70;
            // 
            // colPermit
            // 
            this.colPermit.Caption = "Разрешено";
            this.colPermit.FieldName = "Разрешено";
            this.colPermit.Format.FormatString = "### ### ##0.00";
            this.colPermit.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPermit.MinWidth = 50;
            this.colPermit.Name = "colPermit";
            this.colPermit.OptionsColumn.AllowEdit = false;
            this.colPermit.OptionsColumn.AllowFocus = false;
            this.colPermit.OptionsColumn.ReadOnly = true;
            this.colPermit.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colPermit.SummaryFooterStrFormat = "{0:### ### ##0.00}";
            this.colPermit.Visible = true;
            this.colPermit.VisibleIndex = 2;
            // 
            // colReserve
            // 
            this.colReserve.Caption = "Зарезервировано";
            this.colReserve.FieldName = "Зарезервировано";
            this.colReserve.Format.FormatString = "### ### ##0.00";
            this.colReserve.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colReserve.MinWidth = 50;
            this.colReserve.Name = "colReserve";
            this.colReserve.OptionsColumn.AllowEdit = false;
            this.colReserve.OptionsColumn.AllowFocus = false;
            this.colReserve.OptionsColumn.ReadOnly = true;
            this.colReserve.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colReserve.SummaryFooterStrFormat = "{0:### ### ##0.00}";
            this.colReserve.Visible = true;
            this.colReserve.VisibleIndex = 3;
            // 
            // colFact
            // 
            this.colFact.Caption = "Расход + Курсовая разница";
            this.colFact.FieldName = "Расход";
            this.colFact.Format.FormatString = "### ### ##0.00";
            this.colFact.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFact.Name = "colFact";
            this.colFact.OptionsColumn.AllowEdit = false;
            this.colFact.OptionsColumn.AllowFocus = false;
            this.colFact.OptionsColumn.ReadOnly = true;
            this.colFact.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colFact.SummaryFooterStrFormat = "{0:### ### ##0.00}";
            this.colFact.Visible = true;
            this.colFact.VisibleIndex = 4;
            // 
            // colCredit
            // 
            this.colCredit.Caption = "Поступления";
            this.colCredit.FieldName = "Поступления";
            this.colCredit.Format.FormatString = "### ### ##0.00";
            this.colCredit.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCredit.MinWidth = 50;
            this.colCredit.Name = "colCredit";
            this.colCredit.OptionsColumn.AllowEdit = false;
            this.colCredit.OptionsColumn.AllowFocus = false;
            this.colCredit.OptionsColumn.ReadOnly = true;
            this.colCredit.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colCredit.SummaryFooterStrFormat = "{0:### ### ##0.00}";
            this.colCredit.Visible = true;
            this.colCredit.VisibleIndex = 5;
            // 
            // colRest
            // 
            this.colRest.Caption = "Остаток";
            this.colRest.FieldName = "Остаток";
            this.colRest.Format.FormatString = "### ### ##0.00";
            this.colRest.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRest.Name = "colRest";
            this.colRest.OptionsColumn.AllowEdit = false;
            this.colRest.OptionsColumn.AllowFocus = false;
            this.colRest.OptionsColumn.ReadOnly = true;
            this.colRest.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colRest.SummaryFooterStrFormat = "{0:### ### ##0.00}";
            this.colRest.Visible = true;
            this.colRest.VisibleIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboxBudgetItem, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(501, 27);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel1, null);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.toolTipController.SetSuperTip(this.label2, null);
            this.label2.TabIndex = 1;
            this.label2.Text = "Статья:";
            // 
            // cboxBudgetItem
            // 
            this.cboxBudgetItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxBudgetItem.Location = new System.Drawing.Point(61, 3);
            this.cboxBudgetItem.Name = "cboxBudgetItem";
            this.cboxBudgetItem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxBudgetItem.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboxBudgetItem.Size = new System.Drawing.Size(437, 20);
            this.cboxBudgetItem.TabIndex = 2;
            this.cboxBudgetItem.SelectedIndexChanged += new System.EventHandler(this.cboxBudgetItem_SelectedIndexChanged);
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
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnClose)});
            this.bar1.Text = "Tools";
            // 
            // barBtnPrint
            // 
            this.barBtnPrint.Caption = "Печать";
            this.barBtnPrint.Description = "Печать";
            this.barBtnPrint.Glyph = ((System.Drawing.Image)(resources.GetObject("barBtnPrint.Glyph")));
            this.barBtnPrint.Hint = "Печать";
            this.barBtnPrint.Id = 0;
            this.barBtnPrint.Name = "barBtnPrint";
            this.barBtnPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnPrint_ItemClick);
            // 
            // barBtnClose
            // 
            this.barBtnClose.Caption = "Закрыть";
            this.barBtnClose.Glyph = ((System.Drawing.Image)(resources.GetObject("barBtnClose.Glyph")));
            this.barBtnClose.Hint = "Закрыть";
            this.barBtnClose.Id = 1;
            this.barBtnClose.Name = "barBtnClose";
            this.barBtnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnClose_ItemClick);
            // 
            // frmBudgetItemDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 344);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmBudgetItemDetail";
            this.toolTipController.SetSuperTip(this, null);
            this.Text = "Детализация статьи расходов";
            this.Load += new System.EventHandler(this.frmBudgetItemDetail_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxBudgetItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ToolTipController toolTipController;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barBtnPrint;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMonth;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colAcceptPlan;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraBars.BarButtonItem barBtnClose;
        private DevExpress.XtraEditors.ComboBoxEdit cboxBudgetItem;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPermit;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colReserve;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCredit;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRest;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFact;
    }
}