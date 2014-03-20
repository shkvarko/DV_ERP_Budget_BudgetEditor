using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ErpBudgetBudgetEditor
{
    public partial class frmCellProperties : DevExpress.XtraEditors.XtraForm
    {
        #region Переменные, свойсва, константы
        ERP_Budget.Common.CBudgetItemDecode m_objBudgetItemDecode;
        ERP_Budget.Common.CCurrency m_objBudgetCurrency;
        ERP_Budget.Common.CCurrency m_objLastSelectedCurrency;
        public ERP_Budget.Common.CCurrency LastSelectedCurrency
        {
            get { return m_objLastSelectedCurrency; }
        }
        List<ERP_Budget.Common.CCurrency> m_objCurrencyList;
        System.Boolean m_bIsModified;
        #endregion

        public frmCellProperties(ERP_Budget.Common.CCurrency objBudgetCurrency)
        {
            m_objBudgetItemDecode = null;
            m_objLastSelectedCurrency = null;
            m_objCurrencyList = new List<ERP_Budget.Common.CCurrency>();
            m_objBudgetCurrency = objBudgetCurrency;
            m_bIsModified = false;
            InitializeComponent();
        }
        /// <summary>
        /// Обновление выпадающего списка валют
        /// </summary>
        /// <param name="objCurrencyList">список валют</param>
        public void LoadCurrencyList(List<ERP_Budget.Common.CCurrency> objCurrencyList)
        {
            if (objCurrencyList == null) { return; }
            try
            {
                // очищаем выпадающий список валют
                m_objCurrencyList.Clear();
                foreach (ERP_Budget.Common.CCurrency objCurrency in objCurrencyList)
                {
                    m_objCurrencyList.Add(objCurrency);
                }
                cboxCurrency.Properties.Items.Clear();
                // пробегаем по списку и заполняем выпадающий список
                foreach (ERP_Budget.Common.CCurrency objCurrency in m_objCurrencyList)
                {
                    cboxCurrency.Properties.Items.Add(objCurrency.CurrencyCode);
                }

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка обновления списка валют.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// Отображает содержимое ячейки для редактирования
        /// </summary>
        /// <param name="strBudgetItem">Статья бюджета</param>
        /// <param name="objBudgetItemDecode">Расшифровка</param>
        /// <param name="objLastSelectedCurrency">Последняя выбранная валюта</param>
        public void ShowCellProperties( System.String strBudgetItem, ERP_Budget.Common.CBudgetItemDecode objBudgetItemDecode, 
            ERP_Budget.Common.CCurrency objLastSelectedCurrency)
        {
            try
            {
                txtMoneyPlan.EditValueChanged -= new EventHandler(txtMoneyPlan_EditValueChanged);
                cboxCurrency.EditValueChanged -= new EventHandler(cboxCurrency_EditValueChanged );
                txtDescription.EditValueChanging -= new DevExpress.XtraEditors.Controls.ChangingEventHandler(txtDescription_EditValueChanging);

                txtMoneyPlan.Value = 0;
                cboxCurrency.Text = "";
                txtDescription.Text = "";
                m_objLastSelectedCurrency = objLastSelectedCurrency;
                SetModified(false);

                if (objBudgetItemDecode == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Не удалось определить содержимое ячейки.", "Ошибка",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
                m_objBudgetItemDecode = objBudgetItemDecode;
                txtMoneyPlan.Value = (decimal)m_objBudgetItemDecode.MoneyPlan;
                if (m_objBudgetItemDecode.Currency == null)
                {
                    cboxCurrency.Text = (m_objLastSelectedCurrency == null) ? m_objBudgetCurrency.CurrencyCode : m_objLastSelectedCurrency.CurrencyCode;
                }
                else
                {
                    cboxCurrency.Text = m_objBudgetItemDecode.Currency.CurrencyCode;
                }
                txtDescription.Text = m_objBudgetItemDecode.Description;

                this.Text = strBudgetItem;
                lblInfo.Text = "Месяц: " + m_objBudgetItemDecode.MonthTranslateRu;

                txtMoneyPlan.EditValueChanged += new EventHandler(txtMoneyPlan_EditValueChanged);
                cboxCurrency.EditValueChanged += new EventHandler(cboxCurrency_EditValueChanged);
                txtDescription.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(txtDescription_EditValueChanging);

                ShowDialog();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "ShowCellProperties.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (this.DialogResult == DialogResult.OK)
                {
                    // проверяем все ли заполнили
                    if ((txtMoneyPlan.Value > 0) && (cboxCurrency.Text != ""))
                    {
                        // выбранная валюта
                        foreach (ERP_Budget.Common.CCurrency objCurr in m_objCurrencyList)
                        {
                            if (objCurr.CurrencyCode == cboxCurrency.Text)
                            {
                                if (m_objBudgetItemDecode.Currency == null)
                                {
                                    m_objBudgetItemDecode.Currency = new ERP_Budget.Common.CCurrency(objCurr.uuidID,
                                    objCurr.CurrencyCode, objCurr.Name);
                                }
                                else
                                {
                                    m_objBudgetItemDecode.Currency.uuidID = objCurr.uuidID;
                                    m_objBudgetItemDecode.Currency.CurrencyCode = objCurr.CurrencyCode;
                                    m_objBudgetItemDecode.Currency.Name = objCurr.Name;
                                }
                                m_objLastSelectedCurrency = objCurr;
                                break;
                            }
                        }
                        m_objBudgetItemDecode.MoneyPlan = System.Convert.ToDouble(txtMoneyPlan.Value);
                        m_objBudgetItemDecode.Description = txtDescription.Text;
                    }
                }
            }

            return;
        }
        private void SetModified(System.Boolean bModified)
        {
            try
            {
                if (m_bIsModified != bModified) { m_bIsModified = bModified; }
                if (btnSave.Enabled != m_bIsModified) { btnSave.Enabled = m_bIsModified; }
                if (btnCancel.Enabled != m_bIsModified) { btnCancel.Enabled = m_bIsModified; }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "SetModified.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
            
        private void txtMoneyPlan_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SetModified(true);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "txtMoneyPlan_EditValueChanged.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void cboxCurrency_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SetModified(true);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "cboxCurrency_EditValueChanged.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void txtDescription_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            try
            {
                SetModified(true);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "txtDescription_EditValueChanging.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtMoneyPlan.Value > 0) && (cboxCurrency.Text != ""))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Необходимо указать сумму и валюту.", "Внимание!",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "btnSave_Click.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "btnCancel_Click.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void txtMoneyPlan_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if ((((Control)sender) == txtMoneyPlan) || ((Control)sender) == cboxCurrency)
                    {
                        if ((txtMoneyPlan.Value > 0) && (cboxCurrency.Text != ""))
                        {
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка txtMoneyPlan_KeyDown\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
    }
}
