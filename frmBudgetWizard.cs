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
    public partial class frmBudgetWizard : DevExpress.XtraEditors.XtraForm
    {
        #region Переменные, Свойства, Константы
        private UniXP.Common.CProfile m_objProfile;
        private List<ERP_Budget.Common.CCurrency> m_CurrencyList;
        private List<ERP_Budget.Common.CBudgetDep> m_BudgetDepList;
        private ERP_Budget.Common.CCurrency m_objCurrencyMain;
        private System.Boolean m_bNewBudget;
        #endregion

        #region Конструктор
        public frmBudgetWizard( UniXP.Common.CProfile objProfile )
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-RU");
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            ci.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            this.m_objProfile = objProfile;
            this.m_CurrencyList = null;
            this.m_BudgetDepList = null;
            this.m_objCurrencyMain = null;
            this.m_bNewBudget = false;
        }
        #endregion

        #region Список объектов
        /// <summary>
        /// Обновляет содержимое выпадающих списков
        /// </summary>
        private void LoadComboBox()
        {
            try
            {
                m_bCancelEvents = true;

                cboxBudgetCurrency.Properties.Items.Clear();
                cboxBudgetDep.Properties.Items.Clear();
                cboxBudgetType.Properties.Items.Clear();
                cboxBudgetProject.Properties.Items.Clear();

                System.String strErr = System.String.Empty;

                // типы бюджета
                List<ERP_Budget.Common.CBudgetType> objBudgetTypeList = ERP_Budget.Common.CBudgetTypeDataBaseModel.GetBudgetTypeList(m_objProfile, null, ref strErr);
                if ((objBudgetTypeList != null) || (objBudgetTypeList.Count > 0))
                {
                    cboxBudgetType.Properties.Items.AddRange(objBudgetTypeList);
                }
                objBudgetTypeList = null;

                // проекты
                List<ERP_Budget.Common.CBudgetProject> objBudgetProjectList = ERP_Budget.Common.CBudgetProjectDataBaseModel.GetBudgetProjectList(m_objProfile, null, ref strErr);
                if ((objBudgetProjectList != null) && (objBudgetProjectList.Count > 0))
                {
                    cboxBudgetProject.Properties.Items.AddRange(objBudgetProjectList);
                }

                // список подразделений
                m_BudgetDepList = ERP_Budget.Common.CBudgetDep.GetBudgetDepartmentListWhitoutManager(m_objProfile, ref strErr);
                if ((this.m_BudgetDepList != null) || (this.m_BudgetDepList.Count > 0))
                {
                    cboxBudgetDep.Properties.Items.AddRange(m_BudgetDepList);
                }

                //список валют
                this.m_CurrencyList = ERP_Budget.Common.CCurrency.GetCurrencyList(this.m_objProfile);
                if ((this.m_CurrencyList != null) || (this.m_CurrencyList.Count > 0))
                {

                    // строим список переменных
                    tableLayCurrencyRate.SuspendLayout();
                    // удаляем содержимое контрола, в который будут вклеены выпадающие списки
                    foreach (Control objControl in tableLayCurrencyRate.Controls) { objControl.Dispose(); }
                    tableLayCurrencyRate.Controls.Clear();
                    // удаляем все строки
                    tableLayCurrencyRate.RowStyles.Clear();
                    // добавляем строки
                    tableLayCurrencyRate.RowCount = this.m_CurrencyList.Count - 1;
                    System.Int32 i = 0;

                    this.m_objCurrencyMain = null;
                    foreach (ERP_Budget.Common.CCurrency objCurrency in this.m_CurrencyList)
                    {
                        if (objCurrency.IsMain == true)
                        {
                            this.m_objCurrencyMain = new ERP_Budget.Common.CCurrency(objCurrency.uuidID, objCurrency.CurrencyCode, objCurrency.Name);
                            continue;
                        }
                        cboxBudgetCurrency.Properties.Items.Add(objCurrency);

                        tableLayCurrencyRate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
                        // в строку добавляем label и txtEdit
                        // label
                        System.Windows.Forms.Label lblCurrencyInfo = new System.Windows.Forms.Label();
                        lblCurrencyInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
                        lblCurrencyInfo.AutoSize = true;
                        lblCurrencyInfo.Name = "lbl_" + i.ToString();
                        lblCurrencyInfo.Text = objCurrency.CurrencyCode;
                        tableLayCurrencyRate.Controls.Add(lblCurrencyInfo, 0, i);
                        // txtEdit
                        DevExpress.XtraEditors.CalcEdit txtCurrencyRate = new DevExpress.XtraEditors.CalcEdit();
                        txtCurrencyRate.Name = "txt_" + i.ToString();

                        tableLayCurrencyRate.Controls.Add(txtCurrencyRate, 1, i);
                        txtCurrencyRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
                        txtCurrencyRate.Tag = objCurrency.uuidID;
                        txtCurrencyRate.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.CurrencyRateEditValueChanging);
                        txtCurrencyRate.EditValueChanged += new EventHandler(this.CurrencyRateEditValueChanged);
                        i++;
                    }

                    tableLayCurrencyRate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
                    tableLayCurrencyRate.Controls.Add(new System.Windows.Forms.Label(), 0, i);

                    tableLayCurrencyRate.ResumeLayout(false);
                    tableLayCurrencyRate.PerformLayout();
                }

            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this, "LoadComboBox\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                m_bCancelEvents = false;
            }
            return;
        }
        /// <summary>
        /// Загружает список бюджетов
        /// </summary>
        private void LoadBudgetList()
        {
            System.Int32 iSelectedIndex = ((treeListBudget.FocusedNode == null) ? 0 : treeListBudget.GetNodeIndex(treeListBudget.FocusedNode));
            this.Cursor = Cursors.WaitCursor;
            try
            {
                m_bCancelEvents = true;
                this.tableLayoutBugrnd.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.treeListBudget)).BeginInit();

                treeListBudget.Nodes.Clear();

                // собственно список бюджетов
                List<ERP_Budget.Common.CBudget> objBudgetList = ERP_Budget.Common.CBudget.GetBudgetList( m_objProfile, false );
                if (objBudgetList != null)
                {
                    foreach (ERP_Budget.Common.CBudget objBudget in objBudgetList )
                    {
                        // добавляем узел
                        if (objBudget.IsAccept == true)
                        {
                            treeListBudget.AppendNode(new object[] { objBudget.Date, objBudget.Name, objBudget.BudgetDep.Name, objBudget.Currency.CurrencyCode,  
                                objBudget.AcceptDate}, null).Tag = objBudget;
                        }
                        else
                        {
                            treeListBudget.AppendNode(new object[] { objBudget.Date, objBudget.Name, objBudget.BudgetDep.Name, objBudget.Currency.CurrencyCode,  
                                null}, null).Tag = objBudget;
                        }
                    }
                }
                
                EnableControls((treeListBudget.Nodes.Count > 0));
                if (treeListBudget.Nodes.Count > 0)
                {
                    if (treeListBudget.Nodes.Count > iSelectedIndex)
                    {
                        treeListBudget.FocusedNode = treeListBudget.Nodes[iSelectedIndex];
                    }
                    else
                    {
                        treeListBudget.FocusedNode = treeListBudget.Nodes[0];
                    }
                    LoadBudgetProperties((ERP_Budget.Common.CBudget)treeListBudget.FocusedNode.Tag);
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "Ошибка обновления списка.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
                this.tableLayoutBugrnd.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.treeListBudget)).EndInit();

                SetModified(false);
                this.m_bNewBudget = false;
                m_bCancelEvents = false;
                this.Cursor = Cursors.Default;
            }

            return ;
        }
        /// <summary>
        /// Включает отключает элементы управления
        /// </summary>
        /// <param name="bEnable">включить/выключить</param>
        private void EnableControls( System.Boolean bEnable )
        {
            try
            {
                txtBudgetName.Enabled = bEnable;
                cboxBudgetDep.Enabled = bEnable;
                cboxBudgetCurrency.Enabled = bEnable;
                dtBudgetDate.Enabled = bEnable;
                tableLayCurrencyRate.Enabled = bEnable;
                checkOffExp.Enabled = bEnable;
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "Ошибка отключения/включения элементов управления.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        private void ClearEditorControls()
        {
            try
            {
                txtBudgetName.Text = System.String.Empty;
                txtBudgetManagerList.Text = System.String.Empty;
                cboxBudgetDep.SelectedItem = null;
                cboxBudgetCurrency.SelectedItem = null;
                cboxBudgetProject.SelectedItem = null;
                cboxBudgetType.SelectedItem = null;
                dtBudgetDate.EditValue = null;
                checkOffExp.Checked = false;

                foreach (Control objControl in tableLayCurrencyRate.Controls)
                {
                    if (objControl.Tag == null) { continue; }
                    ( ( DevExpress.XtraEditors.CalcEdit )objControl ).Value = 0;
                }
            
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this, "ClearEditorControls.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        /// <summary>
        /// Загружает свойства объекта в элементы управления
        /// </summary>
        /// <param name="objBudget">объект</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        private void LoadBudgetProperties( ERP_Budget.Common.CBudget objBudget )
        {
            try
            {
                this.m_bCancelEvents = true;
                ClearEditorControls();
                if( (objBudget == null) || (this.m_bNewBudget == true) )
                {
                    return;
                }

                txtBudgetName.Text = objBudget.Name;
                txtBudgetManagerList.Text = objBudget.IsAccessLimitedName;

                cboxBudgetDep.SelectedItem = ((objBudget.BudgetDep != null) ? cboxBudgetDep.Properties.Items.Cast<ERP_Budget.Common.CBudgetDep>().SingleOrDefault<ERP_Budget.Common.CBudgetDep>(x => x.uuidID.Equals(objBudget.BudgetDep.uuidID)) : null);
                cboxBudgetCurrency.SelectedItem = ((objBudget.Currency != null) ? cboxBudgetCurrency.Properties.Items.Cast<ERP_Budget.Common.CCurrency>().SingleOrDefault<ERP_Budget.Common.CCurrency>(x => x.uuidID.Equals( objBudget.Currency.uuidID)) : null);
                cboxBudgetProject.SelectedItem = ((objBudget.BudgetProject != null) ? cboxBudgetProject.Properties.Items.Cast<ERP_Budget.Common.CBudgetProject>().SingleOrDefault<ERP_Budget.Common.CBudgetProject>(x => x.uuidID.Equals(objBudget.BudgetProject.uuidID)) : null);
                cboxBudgetType.SelectedItem = ((objBudget.BudgetType != null) ? cboxBudgetType.Properties.Items.Cast<ERP_Budget.Common.CBudgetType>().SingleOrDefault<ERP_Budget.Common.CBudgetType>(x => x.uuidID.Equals(objBudget.BudgetType.uuidID)) : null);
                dtBudgetDate.DateTime = objBudget.Date;
                checkOffExp.Checked = objBudget.OffExpenditures;

                //// загружаем допустимые значения проектов для указанного подразделениея и финансового периода
                //RefreshBudgetProjectListForBudgetDep(((cboxBudgetDep.SelectedItem == null) ? null : (ERP_Budget.Common.CBudgetDep)cboxBudgetDep.SelectedItem), 
                //    objBudget.BudgetProject, dtBudgetDate.DateTime, ref strErr); 


                // прописываем значения курсов валют 
                if ((objBudget.BudgetCurrencyRate != null) && (objBudget.BudgetCurrencyRate.BudgetCurrencyRateItemList.Count == 0))
                {
                    objBudget.BudgetCurrencyRate.LoadCurrencyRateList(m_objProfile);
                }
                foreach (Control objControl in tableLayCurrencyRate.Controls)
                {
                    if (objControl.Tag == null) { continue; }
                    System.Guid uuidCurrency = (System.Guid)objControl.Tag;
                    foreach (ERP_Budget.Common.CBudgetCurrencyRateItem objRateItem in objBudget.BudgetCurrencyRate.BudgetCurrencyRateItemList)
                    {
                        if (objRateItem.CurrencyIn.uuidID.CompareTo(uuidCurrency) == 0)
                        {
                            ((DevExpress.XtraEditors.CalcEdit)objControl).Value = System.Convert.ToDecimal(objRateItem.Value);
                            break;
                        }
                    }
                }

            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this, "Ошибка озагрузки свойств объекта: " + objBudget.Name + "\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                this.m_bCancelEvents = false;
            }
            return;
        }

        private void RefreshBudgetProjectListForBudgetDep(ERP_Budget.Common.CBudgetDep objBudgetDep, 
            ERP_Budget.Common.CBudgetProject objCurrentBudgetProject,
            System.DateTime dtBudgetDate, ref System.String strErr)
        {
            try
            {
                cboxBudgetProject.Properties.Items.Clear();
                if (objBudgetDep == null) { return; }

                //List<ERP_Budget.Common.CBudgetProject> objBudgetProjectList = ERP_Budget.Common.CBudgetProjectDataBaseModel.GetBudgetProjectListForBudgetDep(m_objProfile, null, objBudgetDep.uuidID, dtBudgetDate, ref strErr);

                List<ERP_Budget.Common.CBudgetProject> objBudgetProjectList = ERP_Budget.Common.CBudgetProjectDataBaseModel.GetBudgetProjectList(m_objProfile, null, ref strErr);

                if( (objBudgetProjectList != null) && ( objCurrentBudgetProject != null ))
                {
                    if (objBudgetProjectList.SingleOrDefault<ERP_Budget.Common.CBudgetProject>(x => x.uuidID.Equals(objCurrentBudgetProject.uuidID)) == null)
                    {
                        cboxBudgetProject.Properties.Items.Add(objCurrentBudgetProject);
                    }
                }

                if ((objBudgetProjectList != null) && (objBudgetProjectList.Count > 0))
                {
                    cboxBudgetProject.Properties.Items.AddRange(objBudgetProjectList);
                }

            }
            catch (System.Exception f)
            {
                strErr += ("Ошибка загрузки списка проектов для выбранного бюджетного подразделения.\nТекст ошибки:\n" + f.Message);
            }
            finally
            {
            }

            return;
        }

        private void treeListBudget_BeforeFocusNode(object sender, DevExpress.XtraTreeList.BeforeFocusNodeEventArgs e)
        {
            try
            {
                System.String strErr = System.String.Empty;
                if (m_bIsModified == true)
                {
                    DialogResult dlgRes = System.Windows.Forms.MessageBox.Show(this, "Данные в бюджете были изменены.\nПодтвердить изменения?", "Подтверждение",
                       System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Question);

                    if( dlgRes == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (bSaveBudgetChange(ref strErr) == true)
                        {
                            e.CanFocus = true;
                            SetModified(false);
                        }
                        else
                        {
                            e.CanFocus = false;
                            System.Windows.Forms.MessageBox.Show(this, strErr, "Внимание!",
                             System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                        }
                    }
                    else if( dlgRes == System.Windows.Forms.DialogResult.No )
                    {
                        e.CanFocus = true;
                    }
                    else if( dlgRes == System.Windows.Forms.DialogResult.Cancel )
                    {
                        e.CanFocus = false;
                    }
                }
                else
                {
                    e.CanFocus = true;
                }

            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this, "treeListBudget_FocusedNodeChanged.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;
        }

        private void treeListBudget_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                this.m_bCancelEvents = true;

                ERP_Budget.Common.CBudget objBudget  = ( ((treeListBudget.Nodes.Count > 0) && 
                    (treeListBudget.FocusedNode != null) && 
                    (treeListBudget.FocusedNode.Tag != null)) ? (ERP_Budget.Common.CBudget)treeListBudget.FocusedNode.Tag : null );

                LoadBudgetProperties( objBudget );
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show(this, "treeListBudget_FocusedNodeChanged.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
                this.m_bCancelEvents = false;
                SetModified( false );
            }

            return;
        }
        /// <summary>
        /// Обработчик изменения значения в элементах управления, которые связаны со свойствами объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BudgetEditValueChanged( object sender, EventArgs e )
        {
            try
            {
                if( ( this.m_bCancelEvents == false ) && ( treeListBudget.FocusedNode != null ) )
                {
                    SetModified( true );
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                "Ошибка функции BudgetEditValueChanged.\nОбъект : " + ( ( Control )sender ).Name + "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        private void CurrencyRateEditValueChanging( object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e )
        {
            try
            {
                e.Cancel = false;
                if( this.m_bCancelEvents == true ) { return; }

                if( ( e.NewValue.ToString() ) != "" )
                {
                    if( ( System.Convert.ToDouble( e.NewValue ) ) <= 0 )
                    {
                        e.Cancel = true;
                        System.Windows.Forms.MessageBox.Show( this,
                       "Курс должен быть больше нуля!", "Внимание",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information );
                    }
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "Ошибка изменения курса.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK,
                   System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        private void CurrencyRateEditValueChanged( object sender, EventArgs e )
        {
            try
            {
                if ((this.m_bCancelEvents == true) || (treeListBudget.FocusedNode == null) ||
                    ( treeListBudget.FocusedNode.Tag == null ) || ( ( ( Control )sender ) ).Tag == null ) { return; }

                // нам нужно найти объект "курс валюты" и прописать туда значение из элемента управления
                ERP_Budget.Common.CBudget objBudgetDep = (ERP_Budget.Common.CBudget)treeListBudget.FocusedNode.Tag;
                if (objBudgetDep.BudgetCurrencyRate != null)
                {
                    foreach (ERP_Budget.Common.CBudgetCurrencyRateItem objCurrencyRateItem in objBudgetDep.BudgetCurrencyRate.BudgetCurrencyRateItemList)
                    {
                        if (objCurrencyRateItem.CurrencyIn.uuidID.CompareTo((System.Guid)(((Control)sender)).Tag) == 0)
                        {
                            objCurrencyRateItem.Value = System.Convert.ToDouble(((DevExpress.XtraEditors.CalcEdit)sender).Value);
                            break;
                        }
                    }
                    SetModified(true);
                }

            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "Ошибка изменения курса.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK,
                   System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }
        private void barBtnRefresh_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            try
            {
                // обновляем список бюджетов
                LoadBudgetList();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "Ошибка обновления списка.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK,
                   System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        #endregion

        #region Индикация изменений в наборе данных

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
                //treeListBudget.Enabled = !bModified;
                barBtnRefresh.Enabled = !bModified;
                barBtnAdd.Enabled = !bModified;
                btnLoadBudgetManagerList.Enabled = !bModified;

                if( treeListBudget.Nodes.Count == 0 )
                {
                    barBtnDelete.Enabled = false;
                    barBtnPrint.Enabled = false;
                }
                else
                {
                    barBtnDelete.Enabled = !bModified;
                    barBtnPrint.Enabled = !bModified;
                }
                if( bModified ) { EnableControls( true ); }
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
            return m_bIsModified;
        }
        #endregion

        #region Открытие формы
        private void frmBudgetWizard_Load( object sender, EventArgs e )
        {
            try
            {
                LoadComboBox();

                LoadBudgetList();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "Ошибка открытия формы.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        #endregion

        #region Новый объект
        private void barBtnAdd_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            try
            {
                NewBudget();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "Ошибка создания нового объекта.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
            }
            return;
        }

        /// <summary>
        /// Создание нового объекта
        /// </summary>
        private void NewBudget()
        {
            try
            {
                ERP_Budget.Common.CBudget objBudget = new ERP_Budget.Common.CBudget();
                objBudget.BudgetCurrencyRate = new ERP_Budget.Common.CBudgetCurrencyRate( objBudget.uuidID );
                
                treeListBudget.FocusedNode = treeListBudget.AppendNode(null, null);
                treeListBudget.FocusedNode.Tag = objBudget;

                this.m_bNewBudget = true;
                SetModified(true);
            }
            catch( System.Exception f )
            {
                this.m_bNewBudget = false;
                System.Windows.Forms.MessageBox.Show( this,
                    "Ошибка создания нового объекта.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
            }
            return;
        }

        #endregion

        #region Сохранение изменений

        private void btnSave_Click( object sender, EventArgs e )
        {
            System.String strErr = System.String.Empty;
            try
            {
                if (bSaveBudgetChange(ref strErr) == true)
                {
                    SetModified(false);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(this,
                       strErr, "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK,
                       System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                   "Ошибка сохранения изменений.\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
            return;
        }

        /// <summary>
        /// Сохраняет изменения в БД
        /// </summary>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        private System.Boolean bSaveBudgetChange( ref System.String strErr )
        {
            System.Boolean bRet = false;
            try
            {
                if( ( treeListBudget.FocusedNode == null ) || ( treeListBudget.FocusedNode.Tag == null ) )
                {
                    strErr +=("\nНе определен индекс объекта!");
                    return bRet;
                }

                if (cboxBudgetCurrency.SelectedItem == null)
                {
                    strErr += ("\nУкажите, пожалуйста, валюту учета бюджета!");
                    return bRet;
                }

                if ( cboxBudgetDep.SelectedItem == null)
                {
                    strErr += ("\nУкажите, пожалуйста, бюжетное подразделение!");
                    return bRet;
                }

                if ( cboxBudgetProject.SelectedItem == null)
                {
                    strErr += ("\nУкажите, пожалуйста, проект!");
                    return bRet;
                }

                if ( cboxBudgetType.SelectedItem == null)
                {
                    strErr += ("\nУкажите, пожалуйста, тип бюджета!");
                    return bRet;
                }

                System.Boolean bIsValidCurRateList = true;
                foreach (Control objControl in tableLayCurrencyRate.Controls)
                {
                    if (objControl.Tag == null) { continue; }

                    // проверим, что за курс проставили
                    System.Decimal CurrencyRate = ((DevExpress.XtraEditors.CalcEdit)objControl).Value;
                    if (CurrencyRate <= 0)
                    {
                        bIsValidCurRateList = false;
                        strErr += (String.Format("\nКурс должен быть больше нуля!\n\n{0}", CurrencyRate));
                        break;
                    }

                }
                if (bIsValidCurRateList == false) { return bRet; }

                ERP_Budget.Common.CBudget objBudgetForSave = new ERP_Budget.Common.CBudget();
                objBudgetForSave.uuidID = ((ERP_Budget.Common.CBudget)treeListBudget.FocusedNode.Tag).uuidID;
                objBudgetForSave.Name = txtBudgetName.Text;
                objBudgetForSave.Date = dtBudgetDate.DateTime;
                objBudgetForSave.Currency = (ERP_Budget.Common.CCurrency)cboxBudgetCurrency.SelectedItem;
                objBudgetForSave.BudgetDep = (ERP_Budget.Common.CBudgetDep)cboxBudgetDep.SelectedItem;
                objBudgetForSave.BudgetProject = (ERP_Budget.Common.CBudgetProject)cboxBudgetProject.SelectedItem;
                objBudgetForSave.BudgetType = (ERP_Budget.Common.CBudgetType)cboxBudgetType.SelectedItem;
                objBudgetForSave.BudgetCurrencyRate = new ERP_Budget.Common.CBudgetCurrencyRate(objBudgetForSave.uuidID);

                foreach (Control objControl in tableLayCurrencyRate.Controls)
                {
                    if (objControl.Tag == null) { continue; }

                    // проверим, что за курс проставили
                    System.Decimal CurrencyRate = ((DevExpress.XtraEditors.CalcEdit)objControl).Value;

                    // нужно найти валюту в списке
                    System.Guid uuidCurrency = (System.Guid)objControl.Tag;
                    ERP_Budget.Common.CCurrency objCurrencyIn = m_CurrencyList.SingleOrDefault<ERP_Budget.Common.CCurrency>(x => x.uuidID.CompareTo(uuidCurrency) == 0);
                    // добавляем курс в список
                    objBudgetForSave.BudgetCurrencyRate.BudgetCurrencyRateItemList.Add(
                        new ERP_Budget.Common.CBudgetCurrencyRateItem(objCurrencyIn,
                        new ERP_Budget.Common.CCurrency(this.m_objCurrencyMain.uuidID,
                        this.m_objCurrencyMain.CurrencyCode, this.m_objCurrencyMain.Name),
                        System.Convert.ToDouble(CurrencyRate)));
                }

                // курсы проставлены верно, проверим другие значения
                if (objBudgetForSave.IsValidProperties(ref strErr) == true)
                {
                    // все свойства определены - можно сохранять
                    if (this.m_bNewBudget == true)
                    {
                        // новый 
                        bRet = objBudgetForSave.Add(this.m_objProfile, ref strErr);
                    }
                    else
                    {
                        // ранее сохраненный
                        bRet = objBudgetForSave.Update(this.m_objProfile, ref strErr);
                    }
                }

                if (bRet == true) 
                {
                    ERP_Budget.Common.CBudget objBudget = (ERP_Budget.Common.CBudget)treeListBudget.FocusedNode.Tag;

                    objBudget.uuidID = objBudgetForSave.uuidID;
                    objBudget.Name = objBudgetForSave.Name;
                    objBudget.Date = objBudgetForSave.Date;
                    objBudget.Currency = objBudgetForSave.Currency;
                    objBudget.BudgetDep = objBudgetForSave.BudgetDep;
                    objBudget.BudgetProject = objBudgetForSave.BudgetProject;
                    objBudget.BudgetType = objBudgetForSave.BudgetType;
                    objBudget.BudgetCurrencyRate = objBudgetForSave.BudgetCurrencyRate;

                    treeListBudget.FocusedNode.SetValue(colBudgetName, objBudget.Name);
                    treeListBudget.FocusedNode.SetValue(colBudgetDate, objBudget.Date);
                    treeListBudget.FocusedNode.SetValue(colCurrency, objBudget.Currency.CurrencyCode);
                    treeListBudget.FocusedNode.SetValue(colBudgetDep, objBudget.BudgetDep.Name);

                    m_bNewBudget = false;
                }

                objBudgetForSave = null;
            }
            catch( System.Exception f )
            {
                strErr += ("\nНе удалось сохранить изменения в БД.\n\nТекст ошибки: " + f.Message);
            }
            finally
            {
            }
            return bRet;
        }

        #endregion

        #region Отмена изменений
        private void btnCancel_Click( object sender, EventArgs e )
        {
            try
            {
                CancelBudgetChange();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                   "Ошибка отмены внесенных изменений.\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }
        /// <summary>
        /// Отменяет сделанные в списке изменения
        /// </summary>
        /// <returns></returns>
        private void CancelBudgetChange()
        {
            try
            {
                if( IsModified() )
                {
                    if (m_bNewBudget == true)
                    {
                        treeListBudget.Nodes.Remove(treeListBudget.FocusedNode);
                    }
                    else
                    {
                        if ((treeListBudget.FocusedNode != null) && (treeListBudget.FocusedNode.Tag != null))
                        {
                            LoadBudgetProperties((ERP_Budget.Common.CBudget)treeListBudget.FocusedNode.Tag);
                        }
                        else
                        {
                            LoadBudgetList();
                        }
                    }

                    SetModified(false);
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "Ошибка отмены внесенных изменений.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        #endregion

        #region Удаление объекта

        /// <summary>
        /// Удаляет из БД запись
        /// </summary>
        /// <param name="iRowHndl">указатель на измененнную запись</param>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
        private System.Boolean bDeleteBudget( DevExpress.XtraTreeList.Nodes.TreeListNode objNode, 
            System.Int32 iRowHndl, ref System.String strErr )
        {
            System.Boolean bRet = false;
            try
            {
                if ((objNode == null) || (objNode.Tag == null))
                {
                    strErr +=( "\nНе удалось определить бюджет для удаления." );
                    return bRet;
                }

                ERP_Budget.Common.CBudget objBudgetForDelete = (ERP_Budget.Common.CBudget)objNode.Tag;
                // удаление
                if (objBudgetForDelete.uuidID.CompareTo(System.Guid.Empty) == 0)
                {
                    bRet = true;
                }
                else
                {
                    bRet = objBudgetForDelete.Remove(this.m_objProfile);
                }
                if( bRet == true)
                {
                    treeListBudget.FocusedNodeChanged -= new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler( this.treeListBudget_FocusedNodeChanged );
                    treeListBudget.Nodes.Remove(objNode);
                    if( treeListBudget.Nodes.Count > 0 )
                    {
                        System.Int32 iSelectedIndex = ( iRowHndl > 0 ) ? ( iRowHndl - 1 ) : 0;
                        treeListBudget.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler( this.treeListBudget_FocusedNodeChanged );
                        treeListBudget.FocusedNode = treeListBudget.Nodes[ iSelectedIndex ];
                    }

                }
            }
            catch( System.Exception f )
            {
                strErr += ("\nОшибка удаления бюджета.Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return bRet;
        }

        private void barBtnDelete_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            try
            {
                if( treeListBudget.Nodes.Count == 0 ) { return; }
                if( treeListBudget.FocusedNode == null )
                {
                    System.Windows.Forms.MessageBox.Show( this, "Необходимо выбрать запись для удаления.", "Внимание",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return;
                }

                System.Int32 iRowHndl = treeListBudget.GetNodeIndex( treeListBudget.FocusedNode );
                if( ( System.Windows.Forms.MessageBox.Show( this,
                    "Вы действительно хотите удалить  " + 
                    System.Convert.ToString( treeListBudget.FocusedNode.GetValue(colBudgetName) ) + " ?", "Подтверждение",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question ) == DialogResult.Yes ) )
                {
                    System.String strErr = System.String.Empty;
                    if (bDeleteBudget(treeListBudget.FocusedNode, iRowHndl, ref strErr) == true)
                    {
                        SetModified(false);
                        if (treeListBudget.Nodes.Count > 0)
                        {
                            System.Int32 iFocusRowHndl = (iRowHndl == 0) ? 0 : (iRowHndl - 1);
                            treeListBudget.FocusedNode = treeListBudget.Nodes[iFocusRowHndl];
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show(this,
                            strErr, "Ошибка",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "Ошибка удаления записи.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
            }

            return;
        }

        #endregion

        #region Копирование бюджетов
        /// <summary>
        /// Копирование бюджетов
        /// </summary>
        private void CopyBudget()
        {
            try
            {
                //this.tableLayoutBugrnd.SuspendLayout();
                //((System.ComponentModel.ISupportInitialize)(this.treeListBudget)).BeginInit();

                if( (treeListBudget.FocusedNode == null) || ( treeListBudget.FocusedNode.Tag == null ) )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                       "Необходимо выбрать бюджет (источник)", "Внимание!",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }
                ERP_Budget.Common.CBudget objBudgetSrc = (ERP_Budget.Common.CBudget)treeListBudget.FocusedNode.Tag;
                if (objBudgetSrc == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                       "Не удалось определить бюджет (источник)", "Внимание!",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }

                System.String strBudgetDstName = System.String.Empty;
                System.DateTime dtBudgetDstDate = System.DateTime.Today;
                System.Boolean bCopySeccess = false;
                
                using (frmCopyBudget objfrmCopyBudget = new frmCopyBudget())
                {
                    List<ERP_Budget.Common.CBudget> BudgetList = new List<ERP_Budget.Common.CBudget>();
                    foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeListBudget.Nodes)
                    {
                        if (objNode.Tag == null) { continue; }

                        BudgetList.Add((ERP_Budget.Common.CBudget)objNode.Tag);
                    }

                    objfrmCopyBudget.CopyBudget(objBudgetSrc, BudgetList, m_objProfile);

                    if( objfrmCopyBudget.DialogResult == DialogResult.OK )
                    {
                        bCopySeccess = true;
                        strBudgetDstName = objfrmCopyBudget.BudgetDst.Name;
                        dtBudgetDstDate = objfrmCopyBudget.BudgetDst.Date;
                    }

                    BudgetList = null;
                }

                if (bCopySeccess == true)
                {
                    // обновляем информацию
                    LoadBudgetList();
                    // найдем скопированный бюджет
                    foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeListBudget.Nodes)
                    {
                        if (((System.String)objNode.GetValue(colBudgetName) == strBudgetDstName) &&
                            ((System.DateTime)objNode.GetValue(colBudgetDate)).Year == dtBudgetDstDate.Year)
                        {
                            treeListBudget.FocusedNode = objNode;
                            break;
                        }
                    }
                }



            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                   "Ошибка копирования бюджета.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                //this.tableLayoutBugrnd.ResumeLayout(false);
                //((System.ComponentModel.ISupportInitialize)(this.treeListBudget)).EndInit();
            }

            return;
        }
        private void barBtnCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (treeListBudget.FocusedNode == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                       "Необходимо выбрать бюджет (источник)", "Внимание!",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }
                CopyBudget();
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
        #endregion

        #region Список распорядителей и согласователей
        private void ShowManagerList()
        {
            if (treeListBudget.Nodes.Count == 0) { return; }
            if (treeListBudget.FocusedNode == null) { return; }
            try
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode objFocusedNode = treeListBudget.FocusedNode;
                if( ( objFocusedNode != null ) && (objFocusedNode.Tag != null ) )
                {
                    ERP_Budget.Common.CBudget objBudget = (ERP_Budget.Common.CBudget)objFocusedNode.Tag;
                    if (objBudget != null)
                    {
                        using (ERP_Budget.Common.frmBudgetDepManagerList objfrmBudgetDepManagerList = new ERP_Budget.Common.frmBudgetDepManagerList(m_objProfile))
                        {
                            if (objfrmBudgetDepManagerList != null)
                            {
                                objfrmBudgetDepManagerList.LoadBudgetManagerList(objBudget.uuidID, objBudget.Name);
                                //objfrmBudgetDepManagerList.ShowDialog();
                            }
                        }
                    }
                }
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this, "ShowManagerList\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        private void btnLoadBudgetManagerList_Click(object sender, EventArgs e)
        {
            ShowManagerList();
        }
        #endregion



    }

    public class ViewBudgetWizard : PlugIn.IClassTypeView
    {
        public override void Run( UniXP.Common.MENUITEM objMenuItem, System.String strCaption )
        {
            frmBudgetWizard obj = new frmBudgetWizard( objMenuItem.objProfile );
            obj.Text = strCaption;
            obj.MdiParent = objMenuItem.objProfile.m_objMDIManager.MdiParent;
            obj.Visible = true;
        }
    }

}