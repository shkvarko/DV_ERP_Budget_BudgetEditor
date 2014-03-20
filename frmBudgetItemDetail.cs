using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ErpBudgetBudgetEditor
{
    public partial class frmBudgetItemDetail : DevExpress.XtraEditors.XtraForm
    {
        #region Переменные, Свойства, Константы

        private UniXP.Common.CProfile m_objProfile;
        private ERP_Budget.Common.CBudgetItem m_objBudgetItem;

        #endregion

        #region Конструктор
        public frmBudgetItemDetail(UniXP.Common.CProfile objProfile, ERP_Budget.Common.CBudgetItem objBudgetItem)
        {
            this.m_objProfile = objProfile;
            this.m_objBudgetItem = objBudgetItem;

            InitializeComponent();
        }
        #endregion

        #region Построение журнала
        /// <summary>
        /// Обновляет журнал состояний
        /// </summary>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        System.Boolean bRefreshList()
        {
            System.Boolean bRet = false;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                cboxBudgetItem.SelectedIndexChanged -= new EventHandler(this.cboxBudgetItem_SelectedIndexChanged);

                treeList.Nodes.Clear();
                cboxBudgetItem.Properties.Items.Clear();
                List<ERP_Budget.Common.CBudgetItem> objList = ERP_Budget.Common.CBudgetItem.GetBudgetItemDetailList(m_objBudgetItem.uuidID, m_objProfile);
                if ((objList != null) && (objList.Count > 0))
                {
                    foreach (ERP_Budget.Common.CBudgetItem objBudgetItem in objList)
                    {
                        cboxBudgetItem.Properties.Items.Add(objBudgetItem);
                    }
                    cboxBudgetItem.SelectedItem = cboxBudgetItem.Properties.Items[0];
                    LoadBudgetItemDetail((ERP_Budget.Common.CBudgetItem)cboxBudgetItem.SelectedItem);
                }
                bRet = true;

            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                "Ошибка построения журнала.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                cboxBudgetItem.SelectedIndexChanged += new EventHandler(this.cboxBudgetItem_SelectedIndexChanged);
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }

            return bRet;
        }
        /// <summary>
        /// Отрисовывает детализацию статьи бюджета
        /// </summary>
        /// <param name="objBudgetItem">статья бюджета</param>
        private void LoadBudgetItemDetail(ERP_Budget.Common.CBudgetItem objBudgetItem)
        {
            treeList.Nodes.Clear();
            if( (objBudgetItem == null) || (objBudgetItem.BudgetItemDecodeList == null) ) { return; }
            try
            {
                if (objBudgetItem.BudgetItemDecodeList.Count > 0)
                {
                    ERP_Budget.Common.CBudgetItemDecode objBudgetItemDecode = null;
                    double Rest = 0;
                    for (System.Int32 i = 1; i <= 12; i++)
                    {
                        objBudgetItemDecode = objBudgetItem.GetBudgetItemDecode( (ERP_Budget.Common.enumMonth)i );
                        if (objBudgetItemDecode == null) { continue; }
                        // добавляем узел в дерево
                        Rest = 0;
                        Rest = objBudgetItemDecode.MoneyPermit - objBudgetItemDecode.MoneyReserve - objBudgetItemDecode.MoneyFact + objBudgetItemDecode.MoneyCredit;
                        treeList.AppendNode(new object[] { objBudgetItemDecode.MonthTranslateRu, 
                            objBudgetItemDecode.MoneyPlanAccept,
                            objBudgetItemDecode.MoneyPermit,
                            objBudgetItemDecode.MoneyReserve,
                            objBudgetItemDecode.MoneyFact,
                            objBudgetItemDecode.MoneyCredit,
                            Rest }, null);

                    }
                }

            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                "Ошибка детализации.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                treeList.Refresh();
            }

            return ;
        }
        private void cboxBudgetItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxBudgetItem.SelectedItem == null) { return; }
            try
            {
                LoadBudgetItemDetail((ERP_Budget.Common.CBudgetItem)cboxBudgetItem.SelectedItem);
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                "Ошибка смены записи в выпадающем списке.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        #endregion

        #region Открытие формы
        private void frmBudgetItemDetail_Load(object sender, EventArgs e)
        {
            try
            {
                // обновляем журнал проводок
                bRefreshList();
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                "Ошибка открытия журнала.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region Закрытие формы
        private void barBtnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                "Ошибка закрытия журнала.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region Печать
        private void barBtnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (DevExpress.XtraPrinting.PrintHelper.IsPrintingAvailable)
                    DevExpress.XtraPrinting.PrintHelper.ShowPreview(treeList);
                else
                    MessageBox.Show("XtraPrinting Library is not found...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cursor.Current = Cursors.Default;
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this, "Ошибка печати.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

    }
}
