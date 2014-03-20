using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ErpBudgetBudgetEditor
{
    public partial class frmCopyBudget : DevExpress.XtraEditors.XtraForm
    {
        #region Переменные, свойства, константы
        /// <summary>
        /// Бюджет (источник)
        /// </summary>
        private ERP_Budget.Common.CBudget m_objBudgetSrc;
        /// <summary>
        /// Бюджет (приемник)
        /// </summary>
        private ERP_Budget.Common.CBudget m_objBudgetDst;
        /// <summary>
        /// Бюджет (приемник)
        /// </summary>
        public ERP_Budget.Common.CBudget BudgetDst
        {
            get { return m_objBudgetDst; }
        }
        /// <summary>
        /// профайл
        /// </summary>
        private UniXP.Common.CProfile m_objProfile;
        private const System.String m_strCaption = "Копирование бюджета";
        #endregion

        public frmCopyBudget( )
        {
            m_objBudgetDst = null;
            m_objBudgetSrc = null;
            m_objProfile = null;

            InitializeComponent();
        }
        /// <summary>
        /// Копирование бюджета
        /// </summary>
        /// <param name="objBudgetSrc">бюджет (источник)</param>
        /// <param name="objBudgetList">список бюджетов</param>
        /// <param name="objProfile">профайл</param>
        public void CopyBudget(ERP_Budget.Common.CBudget objBudgetSrc, List<ERP_Budget.Common.CBudget> objBudgetList,
           UniXP.Common.CProfile objProfile)
        {
            if ((objBudgetSrc == null) || (objProfile == null) || (objBudgetList == null)) { return; }
            try
            {
                m_objBudgetSrc = objBudgetSrc;
                m_objProfile = objProfile;

                treeListBudget.Nodes.Clear();
                DevExpress.XtraTreeList.Nodes.TreeListNode objNode = null;
                // нас интересуют не утвержденные бюджеты, дата которых больше даты бюджета источника и службы совпадают со службой бюджета источника
                foreach (ERP_Budget.Common.CBudget objBudget in objBudgetList)
                {
                    if (objBudget.uuidID.CompareTo(m_objBudgetSrc.uuidID) == 0) { continue; }
                    if (objBudget.IsAccept == true) { continue; }
                    if (objBudget.Date <= m_objBudgetSrc.Date) { continue; }
                    if (objBudget.BudgetDep.uuidID.CompareTo(m_objBudgetSrc.BudgetDep.uuidID) != 0) { continue; }

                    objNode = treeListBudget.AppendNode(new object[] { objBudget.Name, 
                                objBudget.Date.Year, objBudget.BudgetDep.Name }, null);

                    objNode.Tag = objBudget;
                }
                if (treeListBudget.Nodes.Count > 0)
                {
                    treeListBudget.FocusedNode = treeListBudget.Nodes[0];
                }
                btnSave.Enabled = (treeListBudget.FocusedNode != null);
                btnCancel.Enabled = btnSave.Enabled;
                this.Text = m_strCaption;
                this.ShowDialog();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                   "Ошибка копирования бюджета.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;            
        }
        /// <summary>
        /// Копирует бюджет в базу данных
        /// </summary>
        private void CopyBudgetToDB()
        {
            if ((m_objBudgetSrc == null) || (m_objProfile == null) || (treeListBudget.FocusedNode == null)) { return; }
            try
            {
                m_objBudgetDst = (ERP_Budget.Common.CBudget)treeListBudget.FocusedNode.Tag;
                if (m_objBudgetDst == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                       "Программе не удалось определить бюджет (приемник)", "Внимание!",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                if (ERP_Budget.Common.CBudget.CopyBudgetFromSrc(m_objBudgetSrc, m_objBudgetDst, m_objProfile) == true)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                   "Ошибка копирования бюджета в базу данных.\n\nТекст ошибки: " + f.Message, "Ошибка",
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
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                   "btnCancel_Click\n\nТекст ошибки: " + f.Message, "Ошибка",
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
                CopyBudgetToDB();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                   "btnSave_Click\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;
        }

    }
}
