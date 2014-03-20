using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ErpBudgetBudgetEditor
{
    public partial class frmDebitArticleList : DevExpress.XtraEditors.XtraForm
    {
        private ERP_Budget.Common.CDebitArticle m_objDebitArticle;
        public ERP_Budget.Common.CDebitArticle DebitArticle
        {
            get { return m_objDebitArticle; }
        }
        public frmDebitArticleList()
        {
            m_objDebitArticle = null;

            InitializeComponent();
        }

        /// <summary>
        /// Загружает дерево статей расходов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        public void LoadDebitArticles( UniXP.Common.CProfile objProfile, ERP_Budget.Common.CBudget objBudget )
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                // отключаем дерево
                treeList.Enabled = false;
                btnSave.Enabled = false;

                if (ERP_Budget.Common.CDebitArticle.LoadDebitArticleListForBudget( objProfile, treeList, objBudget, colDebitArticleNum) == true)
                {
                    treeList.Enabled = true;
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    this.ShowDialog();
                }
            }//try
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка чтения списка статей расходов.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }

            return ;
        }
        /// <summary>
        /// Выбор статьи расходов
        /// </summary>
        private void SelectBudgetItem()
        {
            m_objDebitArticle = null;
            try
            {
                if ((treeList.Nodes.Count > 0) && (treeList.FocusedNode != null) && (treeList.FocusedNode.Tag != null))
                {
                    m_objDebitArticle = (ERP_Budget.Common.CDebitArticle)treeList.FocusedNode.Tag;

                }
                this.DialogResult = (m_objDebitArticle == null) ? DialogResult.No : DialogResult.OK;
                this.Close();
            }//try
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка выбора статьи расходов.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }

            return;
        }

        private void treeList_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            try
            {
                btnSave.Enabled = ((treeList.Nodes.Count > 0) && (treeList.FocusedNode != null) && (treeList.FocusedNode.Tag != null));
            }//try
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "treeList_AfterFocusNode.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SelectBudgetItem();
            }//try
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "btnSave_Click.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                m_objDebitArticle = null;
                this.Close();
            }//try
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "btnCancel_Click.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;
        }

    }
}
