using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace ErpBudgetBudgetEditor
{
    
    public partial class frmDebitArticleEditor : DevExpress.XtraEditors.XtraForm
    {
        #region Переменные, свойства, константы
        private UniXP.Common.CProfile m_objProfile;
        private ERP_Budget.Common.CBudgetItem m_objBudgetItem;
        public ERP_Budget.Common.CBudgetItem BudgetItem
        {
            get { return m_objBudgetItem; }
        }
        private ERP_Budget.Common.CBudget m_objBudget;
        private System.Guid m_uuidBudgetID;
        private System.Boolean m_bNewDebitArticle;
        private System.Boolean m_bIsCanModifWarningParam;
        private const System.Int32 iWarningPanelHeight = 62;
        private const System.Int32 irGroupPanelHeight = 27;
        #endregion

        #region Конструктор
        public frmDebitArticleEditor(UniXP.Common.CProfile objProfile, ERP_Budget.Common.CBudget objBudget)
        {
            InitializeComponent();

            this.m_objProfile = objProfile;
            this.m_objBudgetItem = null;
            this.m_objBudget = objBudget;
            this.m_bCancelEvents = false;
            this.m_bNewDebitArticle = false;
            this.m_bIsCanModifWarningParam = false;
            this.m_uuidBudgetID = System.Guid.Empty;

        }
        #endregion

        #region Редактирование статьи расходов

        private void RefreshComboBox()
        {
            System.String strErr = "";
            try
            {
                cboxBudgetExpenseType.Properties.Items.Clear();
                cboxAccountPlan.Properties.Items.Clear();

                List<ERP_Budget.Common.CBudgetExpenseType> objBudgetExpenseTypeList = ERP_Budget.Common.CBudgetExpenseType.GetBudgetExpenseTypeList(m_objProfile);
                List<ERP_Budget.Common.CAccountPlan> objAccountPlanList = ERP_Budget.Common.CAccountPlanDataBaseModel.GetAccountPlanList(m_objProfile, null, ref strErr);

                if (objBudgetExpenseTypeList != null)
                {
                    cboxBudgetExpenseType.Properties.Items.AddRange(objBudgetExpenseTypeList);
                }

                if (objAccountPlanList != null)
                {
                    cboxAccountPlan.Properties.Items.AddRange(objAccountPlanList);
                }
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(null, "RefreshComboBox.\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;
        }

        /// <summary>
        /// Редактирование статьи расходов
        /// </summary>
        /// <param name="objDebitArticle">статья расходов</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        public void EditDebitArticle( ERP_Budget.Common.CBudgetItem objBudgetItem )
        {
            try
            {
                RefreshComboBox();

                tableLayoutPanel1.RowStyles[0].Height = 0;
                this.m_objBudgetItem = ERP_Budget.Common.CBudgetItem.Copy( objBudgetItem );

                m_bCancelEvents = true;
                cboxBudgetExpenseType.SelectedItem = null;
                // номер, наименование, описание
                txtDebitArticleName.Text = this.m_objBudgetItem.Name;
                txtDebitArticleNum.Text = this.m_objBudgetItem.BudgetItemNum;
                txtDebitArticleDscrpn.Text = ( this.m_objBudgetItem.BudgetItemDescription == "" ) ? ( "" ) : this.m_objBudgetItem.BudgetItemDescription;
                checkTranspRest.Checked = this.m_objBudgetItem.TransprtRest;
                checkDontChange.Checked = this.m_objBudgetItem.DontChange;

                cboxBudgetExpenseType.SelectedItem = (m_objBudgetItem.BudgetExpenseType == null) ? null : (cboxBudgetExpenseType.Properties.Items.Cast<ERP_Budget.Common.CBudgetExpenseType>().SingleOrDefault<ERP_Budget.Common.CBudgetExpenseType>(x => x.uuidID.Equals(m_objBudgetItem.BudgetExpenseType.uuidID)));
                cboxAccountPlan.SelectedItem = (m_objBudgetItem.AccountPlan == null) ? null : (cboxAccountPlan.Properties.Items.Cast<ERP_Budget.Common.CAccountPlan>().SingleOrDefault<ERP_Budget.Common.CAccountPlan>(x => x.uuidID.Equals(m_objBudgetItem.AccountPlan.uuidID)));

                // для родительской статьи блокируем все элементы управления
                txtDebitArticleName.Properties.ReadOnly = ( this.m_objBudgetItem.DebitArticleID.CompareTo( System.Guid.Empty ) != 0 );
                txtDebitArticleNum.Properties.ReadOnly = ( this.m_objBudgetItem.DebitArticleID.CompareTo( System.Guid.Empty ) != 0 );
                txtDebitArticleDscrpn.Properties.ReadOnly = ( this.m_objBudgetItem.DebitArticleID.CompareTo( System.Guid.Empty ) != 0 );
                checkTranspRest.Properties.ReadOnly = false;
                checkDontChange.Properties.ReadOnly = ( this.m_objBudgetItem.DebitArticleID.CompareTo( System.Guid.Empty ) != 0 );
                cboxBudgetExpenseType.Properties.ReadOnly = (this.m_objBudgetItem.DebitArticleID.CompareTo(System.Guid.Empty) != 0);
                cboxAccountPlan.Properties.ReadOnly = (this.m_objBudgetItem.DebitArticleID.CompareTo(System.Guid.Empty) != 0);

                // заголовок формы
                this.Text = ( this.m_objBudgetItem.ParentID.CompareTo( System.Guid.Empty ) == 0 ) ? "Статья бюджета" : "Подстатья бюджета";

                m_bCancelEvents = false;
                SetModified( false );
                this.ShowDialog();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( null, "Ошибка редактирования статьи расходов.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
            }
            return;
        }
        /// <summary>
        /// Просмотр статьи расходов
        /// </summary>
        /// <param name="objDebitArticle">статья расходов</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        public void ViewDebitArticle( ERP_Budget.Common.CBudgetItem objBudgetItem )
        {
            try
            {
                RefreshComboBox();

                this.m_objBudgetItem = ERP_Budget.Common.CBudgetItem.Copy(objBudgetItem);

                m_bCancelEvents = true;

                // номер, наименование, описание
                txtDebitArticleName.Text = this.m_objBudgetItem.Name;
                txtDebitArticleNum.Text = this.m_objBudgetItem.BudgetItemNum;
                txtDebitArticleDscrpn.Text = ( this.m_objBudgetItem.BudgetItemDescription == "" ) ? ( "" ) : this.m_objBudgetItem.BudgetItemDescription;
                checkTranspRest.Checked = this.m_objBudgetItem.TransprtRest;
                checkDontChange.Checked = this.m_objBudgetItem.DontChange;
                cboxBudgetExpenseType.SelectedItem = (m_objBudgetItem.BudgetExpenseType == null) ? null : (cboxBudgetExpenseType.Properties.Items.Cast<ERP_Budget.Common.CBudgetExpenseType>().SingleOrDefault<ERP_Budget.Common.CBudgetExpenseType>(x => x.uuidID.Equals(m_objBudgetItem.BudgetExpenseType.uuidID)));
                cboxAccountPlan.SelectedItem = (m_objBudgetItem.AccountPlan == null) ? null : (cboxAccountPlan.Properties.Items.Cast<ERP_Budget.Common.CAccountPlan>().SingleOrDefault<ERP_Budget.Common.CAccountPlan>(x => x.uuidID.Equals(m_objBudgetItem.AccountPlan.uuidID)));

                // блокируем все элементы управления
                txtDebitArticleName.Properties.ReadOnly = true;
                txtDebitArticleNum.Properties.ReadOnly = true;
                txtDebitArticleDscrpn.Properties.ReadOnly = true;
                checkTranspRest.Properties.ReadOnly = true;
                checkDontChange.Properties.ReadOnly = true;
                cboxBudgetExpenseType.Properties.ReadOnly = true;
                cboxAccountPlan.Properties.ReadOnly = true;

                // заголовок формы
                this.Text = ( this.m_objBudgetItem.ParentID.CompareTo( System.Guid.Empty ) == 0 ) ? "Статья бюджета" : "Подстатья бюджета";

                m_bCancelEvents = false;
                SetModified( false );
                this.ShowDialog();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( null, "Ошибка редактирования статьи расходов.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
            }
            return;
        }
        #endregion

        #region Новая подстатья
        /// <summary>
        /// Создание новой подстатьи расходов
        /// </summary>
        public void AddDebitArticleChild( ERP_Budget.Common.CBudgetItem objBudgetItem,
            System.Int32 iArticleChildNum, System.Guid uuidBudgetID )
        {
            try
            {
                RefreshComboBox();

                tableLayoutPanel1.RowStyles[0].Height = 0;
                this.m_bNewDebitArticle = true;
                this.m_uuidBudgetID = uuidBudgetID;
                this.m_objBudgetItem = new ERP_Budget.Common.CBudgetItem();
                this.m_objBudgetItem.BudgetGUID = this.m_uuidBudgetID;
                this.m_objBudgetItem.uuidID = System.Guid.NewGuid();
                this.m_objBudgetItem.ParentID = objBudgetItem.uuidID;
                this.m_objBudgetItem.BudgetItemNum = objBudgetItem.BudgetItemNum + "." + iArticleChildNum.ToString();
                this.m_objBudgetItem.DontChange = false;
                this.m_objBudgetItem.TransprtRest = objBudgetItem.TransprtRest;
                this.m_objBudgetItem.BudgetItemID = iArticleChildNum;
                m_objBudgetItem.BudgetExpenseType = objBudgetItem.BudgetExpenseType;
                m_objBudgetItem.AccountPlan = objBudgetItem.AccountPlan;

                m_bCancelEvents = true;
                // номер, наименование, описание
                checkTranspRest.Checked = this.m_objBudgetItem.TransprtRest;
                checkTranspRest.Properties.ReadOnly = true;
                checkDontChange.Checked = this.m_objBudgetItem.DontChange;
                // заголовок формы
                this.Text = "Новая подстатья бюджета";
                txtDebitArticleNum.Text = this.m_objBudgetItem.BudgetItemNum;
                if (iArticleChildNum == 0)
                {
                    txtDebitArticleName.Text = objBudgetItem.Name;
                    txtDebitArticleName.Enabled = false;
                    txtDebitArticleNum.Enabled = false;
                }
                cboxBudgetExpenseType.SelectedItem = (m_objBudgetItem.BudgetExpenseType == null) ? null : (cboxBudgetExpenseType.Properties.Items.Cast<ERP_Budget.Common.CBudgetExpenseType>().SingleOrDefault<ERP_Budget.Common.CBudgetExpenseType>(x => x.uuidID.Equals(m_objBudgetItem.BudgetExpenseType.uuidID)));
                cboxAccountPlan.SelectedItem = (m_objBudgetItem.AccountPlan == null) ? null : (cboxAccountPlan.Properties.Items.Cast<ERP_Budget.Common.CAccountPlan>().SingleOrDefault<ERP_Budget.Common.CAccountPlan>(x => x.uuidID.Equals(m_objBudgetItem.AccountPlan.uuidID)));
                cboxBudgetExpenseType.Properties.ReadOnly = true;
                cboxAccountPlan.Properties.ReadOnly = true;
                m_bCancelEvents = false;
                SetModified(iArticleChildNum == 0);
                
                this.ShowDialog();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( null, "Ошибка создания подстатьи бюджета.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
            }
            return;
        }

        #endregion

        #region Новая статья бюджета
        public void NewBudgetItem(ERP_Budget.Common.CBudget objBudget)
        {
            try
            {
                RefreshComboBox();

                tableLayoutPanel1.RowStyles[0].Height = irGroupPanelHeight;
                rGroup.SelectedIndex = rGroup.Properties.Items.Count - 1;
                this.m_bNewDebitArticle = true;
                this.m_uuidBudgetID = objBudget.uuidID;
                this.m_objBudgetItem = new ERP_Budget.Common.CBudgetItem();
                this.m_objBudgetItem.BudgetGUID = objBudget.uuidID;
                this.m_objBudgetItem.uuidID = System.Guid.NewGuid();

                m_bCancelEvents = true;

                checkTranspRest.Checked = true;
                checkTranspRest.Properties.ReadOnly = false;
                checkDontChange.Checked = true;
                cboxBudgetExpenseType.Properties.ReadOnly = false;
                cboxAccountPlan.Properties.ReadOnly = false;
                // заголовок формы
                this.Text = "Новая статья бюджета";

                m_bCancelEvents = false;
                SetModified(false);
                this.ShowDialog();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка создания статьи бюджета.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }
        private void rGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSelectArticle.Enabled = (rGroup.SelectedIndex > 0);
                txtDebitArticleNum.Text = "";
                txtDebitArticleName.Text = "";
                txtDebitArticleDscrpn.Text = "";
                cboxBudgetExpenseType.SelectedItem = null;

                checkTranspRest.Properties.ReadOnly = (rGroup.SelectedIndex > 0);
                checkDontChange.Properties.ReadOnly = (rGroup.SelectedIndex > 0);
                txtDebitArticleNum.Enabled = (rGroup.SelectedIndex == 0);
                txtDebitArticleName.Enabled = (rGroup.SelectedIndex == 0);

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка выбора.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }
        /// <summary>
        /// Выбор статьи расходов из списка
        /// </summary>
        private void SelectDebitArticle()
        {
            try
            {
                frmDebitArticleList objfrmDebitArticleList = new frmDebitArticleList();
                if (objfrmDebitArticleList != null)
                {
                    objfrmDebitArticleList.LoadDebitArticles(m_objProfile, m_objBudget);
                    if (objfrmDebitArticleList.DialogResult == DialogResult.OK)
                    {
                        m_objBudgetItem.BudgetItemNum = objfrmDebitArticleList.DebitArticle.ArticleNum;
                        m_objBudgetItem.Name = objfrmDebitArticleList.DebitArticle.Name;
                        m_objBudgetItem.DontChange = objfrmDebitArticleList.DebitArticle.DontChange;
                        m_objBudgetItem.TransprtRest = objfrmDebitArticleList.DebitArticle.TransprtRest;
                        m_objBudgetItem.BudgetItemID = objfrmDebitArticleList.DebitArticle.ArticleID;
                        m_objBudgetItem.DebitArticleID = objfrmDebitArticleList.DebitArticle.uuidID;
                        if (objfrmDebitArticleList.DebitArticle.BudgetDepList != null)
                        {
                            ERP_Budget.Common.CBudgetDep objBudgetDep = objfrmDebitArticleList.DebitArticle.BudgetDepList.SingleOrDefault<ERP_Budget.Common.CBudgetDep>( x=>x.uuidID.Equals(m_objBudget.BudgetDep.uuidID) );
                            if( objBudgetDep != null )
                            {
                                m_objBudgetItem.BudgetExpenseType = objBudgetDep.BudgetExpenseType;
                            }

                        }
                        m_objBudgetItem.AccountPlan = objfrmDebitArticleList.DebitArticle.AccountPlan; 

                        checkTranspRest.Checked = m_objBudgetItem.TransprtRest;
                        checkDontChange.Checked = m_objBudgetItem.DontChange;
                        txtDebitArticleNum.Text = m_objBudgetItem.BudgetItemNum;
                        txtDebitArticleName.Text = m_objBudgetItem.Name;

                        cboxBudgetExpenseType.SelectedItem = (m_objBudgetItem.BudgetExpenseType == null) ? null : (cboxBudgetExpenseType.Properties.Items.Cast<ERP_Budget.Common.CBudgetExpenseType>().SingleOrDefault<ERP_Budget.Common.CBudgetExpenseType>(x => x.uuidID.Equals(m_objBudgetItem.BudgetExpenseType.uuidID)));
                        cboxAccountPlan.SelectedItem = (m_objBudgetItem.AccountPlan == null) ? null : (cboxAccountPlan.Properties.Items.Cast<ERP_Budget.Common.CAccountPlan>().SingleOrDefault<ERP_Budget.Common.CAccountPlan>(x => x.uuidID.Equals(m_objBudgetItem.AccountPlan.uuidID)));

                    }
                    objfrmDebitArticleList.Dispose();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка выбора статьи расходов.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }
        private void btnSelectArticle_Click(object sender, EventArgs e)
        {
            try
            {
                SelectDebitArticle();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка выбора статьи расходов.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }

        #endregion

        #region Индикация изменений

        private System.Boolean m_bIsModified;
        private System.Boolean m_bCancelEvents;
        /// <summary>
        /// Устанавливает индикатор "изменена запись"
        /// </summary>
        private void SetModified( System.Boolean bModified )
        {
            try
            {
                m_bIsModified = bModified;
                btnSave.Enabled = bModified;
                btnCancel.Enabled = bModified;
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "Ошибка метода SetModified().\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }
        /// <summary>
        /// Возвращает признак того, изменялась ли запись
        /// </summary>
        /// <returns>true - изменялась; false - не изменялась</returns>
        private System.Boolean IsModified()
        {
            System.Boolean bRes = false;
            try
            {
                return m_bIsModified;
            }
            catch( System.Exception e )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    e.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
            }
            return bRes;
        }

        private void EditValueChanged( object sender, EventArgs e )
        {
            try
            {
                if( this.m_bCancelEvents == false )
                {
                    SetModified( true );
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                "Ошибка функции EditValueChanged.\nОбъект : " + ( ( Control )sender ).Name + "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        private void chklstBudgetDep_ItemCheck( object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e )
        {
            try
            {
                if( this.m_bCancelEvents == false )
                {
                    SetModified( true );
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                "Ошибка функции chklstBudgetDep_ItemCheck.\nОбъект : " + ( ( Control )sender ).Name + "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        #endregion

        #region Сохранение свойств статьи расходов
        /// <summary>
        /// Присваивает свойствам объекта значения из элементов управления
        /// </summary>
        /// <returns>true - операция прошла успешно;false - ошибка</returns>
        private System.Boolean bInitDebitArticleParams()
        {
            System.Boolean bRet = false;
            try
            {
                this.m_objBudgetItem.Name = txtDebitArticleName.Text;
                this.m_objBudgetItem.BudgetItemNum = txtDebitArticleNum.Text;
                this.m_objBudgetItem.BudgetItemDescription = txtDebitArticleDscrpn.Text;
                this.m_objBudgetItem.TransprtRest = checkTranspRest.Checked;
                this.m_objBudgetItem.DontChange = checkDontChange.Checked;
                m_objBudgetItem.BudgetExpenseType = ((cboxBudgetExpenseType.SelectedItem == null) ? null : (ERP_Budget.Common.CBudgetExpenseType)cboxBudgetExpenseType.SelectedItem);
                m_objBudgetItem.AccountPlan = ((cboxAccountPlan.SelectedItem == null) ? null : (ERP_Budget.Common.CAccountPlan)cboxAccountPlan.SelectedItem);
                if (m_objBudgetItem.ParentID.CompareTo(System.Guid.Empty) == 0)
                {
                    // необходимо определить номер статьи по порядку
                    try
                    {
                        m_objBudgetItem.BudgetItemID = System.Convert.ToInt32(txtDebitArticleNum.Text);
                    }
                    catch (OverflowException)
                    {
                        m_objBudgetItem.BudgetItemID = 0;
                    }
                    catch (FormatException)
                    {
                        m_objBudgetItem.BudgetItemID = 0;
                    }   
                }
                bRet = true;
            }
            catch( System.Exception f )
            {
                bRet = false;
                System.Windows.Forms.MessageBox.Show( this, "Ошибка инициализации свойств объекта \"Статья бюджета\".\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }

        /// <summary>
        /// Сохраняет статью бюджета
        /// </summary>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        private System.Boolean bSaveDebitArticle()
        {
            System.Boolean bRet = false;
            try
            {
                if( this.m_objBudgetItem == null )
                {
                    System.Windows.Forms.MessageBox.Show( this, "Не удалось создать объект \"Статья бюджета\".", "Внимание",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return bRet;
                }
                // теперь присвоим свойствам объекта "Статья расходов" значения из элементов управления
                bRet = bInitDebitArticleParams();
                if( bRet == true )
                {
                    // теперь проверим обязательные параметры
                    bRet = this.m_objBudgetItem.IsValidateProperties();
                }
                if( bRet == true )
                {
                    // все свойства определены, можно сохраняться
                    if( this.m_bNewDebitArticle )
                    {
                        // новая статья
                        bRet = this.m_objBudgetItem.Add( this.m_objProfile );
                    }
                    else
                    {
                        bRet = this.m_objBudgetItem.Update( this.m_objProfile, this.m_bIsCanModifWarningParam );
                    }
                }

            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "Ошибка сохранения свойств объекта \"Статья расходов\".\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }

        private void btnSave_Click( object sender, EventArgs e )
        {
            try
            {
                if( IsModified() == false ) { return; }
                if( bSaveDebitArticle() )
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "Ошибка сохранения свойств статьи расходов.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        #endregion

        #region Отмена
        private void btnCancel_Click( object sender, EventArgs e )
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( null, "Ошибка отмены изменений.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        #endregion

        private void frmDebitArticleEditor_Shown( object sender, EventArgs e )
        {
            try
            {
                txtDebitArticleName.Focus();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "Ошибка отображения формы.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }




    }
}