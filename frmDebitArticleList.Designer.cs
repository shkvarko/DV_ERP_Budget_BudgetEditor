namespace ErpBudgetBudgetEditor
{
    partial class frmDebitArticleList
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
            this.repItemCheckEdit_ReadOnly = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.colGuid_ID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colParentGuid_ID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDebitArticleNum = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colReadOnly = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colAccountPlan = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.repItemCheckEdit_ReadOnly)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            this.SuspendLayout();
            // 
            // repItemCheckEdit_ReadOnly
            // 
            this.repItemCheckEdit_ReadOnly.AutoHeight = false;
            this.repItemCheckEdit_ReadOnly.Name = "repItemCheckEdit_ReadOnly";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.treeList, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(703, 381);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 208F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 345);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(697, 33);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::ErpBudgetBudgetEditor.Properties.Resources.disk_blue_ok;
            this.btnSave.Location = new System.Drawing.Point(517, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 25);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Выбрать";
            this.btnSave.ToolTip = "Сохранить изменения";
            this.btnSave.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(609, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 25);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.ToolTip = "Отменить изменения";
            this.btnCancel.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // treeList
            // 
            this.treeList.AllowDrop = true;
            this.treeList.Appearance.EvenRow.BackColor = System.Drawing.SystemColors.Info;
            this.treeList.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colGuid_ID,
            this.colParentGuid_ID,
            this.colDebitArticleNum,
            this.colAccountPlan,
            this.colReadOnly});
            this.treeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList.Location = new System.Drawing.Point(3, 3);
            this.treeList.Name = "treeList";
            this.treeList.OptionsBehavior.AutoChangeParent = false;
            this.treeList.OptionsBehavior.AutoNodeHeight = false;
            this.treeList.OptionsBehavior.DragNodes = true;
            this.treeList.OptionsBehavior.ImmediateEditor = false;
            this.treeList.OptionsBehavior.KeepSelectedOnClick = false;
            this.treeList.OptionsBehavior.SmartMouseHover = false;
            this.treeList.OptionsPrint.PrintPreview = true;
            this.treeList.OptionsView.EnableAppearanceEvenRow = true;
            this.treeList.OptionsView.ShowPreview = true;
            this.treeList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repItemCheckEdit_ReadOnly});
            this.treeList.Size = new System.Drawing.Size(697, 336);
            this.treeList.TabIndex = 1;
            this.treeList.AfterFocusNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList_AfterFocusNode);
            // 
            // colGuid_ID
            // 
            this.colGuid_ID.Caption = "DEBITARTICLE_GUID_ID";
            this.colGuid_ID.FieldName = "DEBITARTICLE_GUID_ID";
            this.colGuid_ID.Name = "colGuid_ID";
            this.colGuid_ID.Width = 48;
            // 
            // colParentGuid_ID
            // 
            this.colParentGuid_ID.Caption = "DEBITARTICLE_PARENT_GUID_ID";
            this.colParentGuid_ID.FieldName = "DEBITARTICLE_PARENT_GUID_ID";
            this.colParentGuid_ID.Name = "colParentGuid_ID";
            this.colParentGuid_ID.Width = 47;
            // 
            // colDebitArticleNum
            // 
            this.colDebitArticleNum.Caption = "Номер";
            this.colDebitArticleNum.FieldName = "Номер";
            this.colDebitArticleNum.MinWidth = 27;
            this.colDebitArticleNum.Name = "colDebitArticleNum";
            this.colDebitArticleNum.OptionsColumn.AllowEdit = false;
            this.colDebitArticleNum.OptionsColumn.AllowFocus = false;
            this.colDebitArticleNum.OptionsColumn.AllowSort = false;
            this.colDebitArticleNum.OptionsColumn.ReadOnly = true;
            this.colDebitArticleNum.Visible = true;
            this.colDebitArticleNum.VisibleIndex = 0;
            this.colDebitArticleNum.Width = 291;
            // 
            // colReadOnly
            // 
            this.colReadOnly.Caption = "READONLY";
            this.colReadOnly.ColumnEdit = this.repItemCheckEdit_ReadOnly;
            this.colReadOnly.FieldName = "READONLY";
            this.colReadOnly.Name = "colReadOnly";
            // 
            // colAccountPlan
            // 
            this.colAccountPlan.Caption = "План счетов";
            this.colAccountPlan.FieldName = "План счетов";
            this.colAccountPlan.Name = "colAccountPlan";
            this.colAccountPlan.OptionsColumn.AllowEdit = false;
            this.colAccountPlan.OptionsColumn.AllowFocus = false;
            this.colAccountPlan.OptionsColumn.ReadOnly = true;
            this.colAccountPlan.Visible = true;
            this.colAccountPlan.VisibleIndex = 1;
            this.colAccountPlan.Width = 385;
            // 
            // frmDebitArticleList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 381);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "frmDebitArticleList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Статьи расходов";
            ((System.ComponentModel.ISupportInitialize)(this.repItemCheckEdit_ReadOnly)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colGuid_ID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colParentGuid_ID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDebitArticleNum;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colReadOnly;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repItemCheckEdit_ReadOnly;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colAccountPlan;
    }
}