namespace ErpBudgetBudgetEditor
{
    partial class frmCopyBudget
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.treeListBudget = new DevExpress.XtraTreeList.TreeList();
            this.colBudgetName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colBudgetDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colBudgetDep = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListBudget)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.treeListBudget, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(473, 328);
            this.tableLayoutPanel1.TabIndex = 0;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 298);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(473, 30);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::ErpBudgetBudgetEditor.Properties.Resources.disk_blue_ok;
            this.btnSave.Location = new System.Drawing.Point(284, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Копировать";
            this.btnSave.ToolTip = "Сохранить изменения";
            this.btnSave.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::ErpBudgetBudgetEditor.Properties.Resources.undo;
            this.btnCancel.Location = new System.Drawing.Point(380, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отменить";
            this.btnCancel.ToolTip = "Отменить изменения";
            this.btnCancel.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // treeListBudget
            // 
            this.treeListBudget.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListBudget.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colBudgetName,
            this.colBudgetDate,
            this.colBudgetDep});
            this.treeListBudget.Location = new System.Drawing.Point(3, 3);
            this.treeListBudget.Name = "treeListBudget";
            this.treeListBudget.OptionsBehavior.Editable = false;
            this.treeListBudget.ParentFieldName = "";
            this.treeListBudget.Size = new System.Drawing.Size(467, 292);
            this.treeListBudget.TabIndex = 1;
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
            this.colBudgetName.Width = 152;
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
            this.colBudgetDate.Width = 128;
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
            this.colBudgetDep.Width = 166;
            // 
            // frmCopyBudget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 328);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "frmCopyBudget";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmCopyBudget";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListBudget)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraTreeList.TreeList treeListBudget;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colBudgetDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colBudgetName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colBudgetDep;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}