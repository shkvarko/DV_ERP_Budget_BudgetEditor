using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UniXP.Common;
using ERP_Budget.Common;
using OfficeOpenXml;
using System.Threading;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace DebitArticle
{
    public partial class frmImportDataInDebitArticleList : DevExpress.XtraEditors.XtraForm
    {
        #region Свойства
        private CProfile m_objProfile;
        private List<CBudgetDep> m_objBudgetDepList;
        private List<CBudgetExpenseType> m_objBudgetExpenseTypeList;
        private List<CBudgetProject> m_objBudgetProjectList;
        private List<CAccountPlan> m_objAccountPlanList;

        private List<CDebitArticle> m_objDebitArticleList;
        private System.Data.DataTable m_objdtDebitArticleItems;

        /// <summary>
        /// Список настроек
        /// </summary>
        private CSettingForImportData m_objSettingForImportData;
        private System.String m_strFileFullName;
        /// <summary>
        /// Имя файла
        /// </summary>
        public System.String FileFullName
        {
            get { return m_strFileFullName; }
        }
        public System.Int32 SelectedSheetId { get; set; }
        public List<System.String> SheetList;

        private const System.String strNodeSettingname = "ColumnItem";
        private const string strCaptionTextForImportInPlan = "Импорт данных в справочник статей расходов";

        private static readonly System.String strFieldNameSTARTROW = "STARTROW";
        private static readonly System.String strFieldNameACCOUNTPLAN_1C_CODE = "ACCOUNTPLAN_1C_CODE";
        private static readonly System.String strFieldNameBUDGETITEM_NUM = "BUDGETITEM_NUM";
        private static readonly System.String strFieldNameBUDGETITEM_NAME = "BUDGETITEM_NAME";
        private static readonly System.String strFieldNameBUDGETDEP_NAME = "BUDGETDEP_NAME";
        private static readonly System.String strFieldNameBUDGETEXPENSETYPE_NAME = "BUDGETEXPENSETYPE_NAME";
        private static readonly System.String strFieldNameBUDGETPROJECT_NAME = "BUDGETPROJECT_NAME";

        #endregion

        #region Конструктор
        public frmImportDataInDebitArticleList( UniXP.Common.CProfile objProfile )
        {
            InitializeComponent();

            m_objProfile = objProfile;
            
            m_objBudgetDepList = null;
            m_objBudgetExpenseTypeList = null;
            m_objBudgetProjectList = null;
            m_objAccountPlanList = null;

            m_objDebitArticleList = null;
            m_objdtDebitArticleItems = null;

            m_strFileFullName = "";
            m_objSettingForImportData = null;
            btnLoadDataFromFile.Enabled = false;
            SelectedSheetId = 0;
            SheetList = null;
        }
        #endregion

        #region Загрузка списков для проверки импортируемых значений
        public void LoadComboBox(List<CBudgetDep> objBudgetDepList, List<CBudgetExpenseType> objBudgetExpenseTypeList,
           List<CBudgetProject> objBudgetProjectList, List<CAccountPlan> objAccountPlanList )
        {
            try
            {
                m_objBudgetDepList = objBudgetDepList;
                m_objBudgetExpenseTypeList = objBudgetExpenseTypeList;
                m_objBudgetProjectList = objBudgetProjectList;
                m_objAccountPlanList = objAccountPlanList;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("LoadComboBox.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }
        #endregion

        #region Открыть форму с настройками для импорта статей расходов
        /// <summary>
        /// Открывает форму в режиме импорта данных
        /// </summary>
        /// <param name="objPlanItemList">приложение к плану продаж</param>
        /// <param name="strFileName">путь к файлу MS Excel</param>
        /// <param name="iSelectedSheetId">номер листа в файле</param>
        /// <param name="SheetList">список листов в файле</param>
        public void OpenForImportDataInDebitArticle(
            List<CDebitArticle> objDebitArticleList, System.String strFileName,
            System.Int32 iSelectedSheetId, List<System.String> SheetList
            )
        {
            try
            {
                m_objDebitArticleList = objDebitArticleList;

                SetInitialParams();

                txtID_Ib.Text = strFileName;
                cboxSheet.Properties.Items.Clear();
                if (SheetList != null)
                {
                    cboxSheet.Properties.Items.AddRange(SheetList);
                    cboxSheet.SelectedIndex = iSelectedSheetId;
                }

                dateEditBegin.DateTime = new DateTime(System.DateTime.Today.AddYears(1).Year, 1, 1);
                dateEditEnd.DateTime = new DateTime(System.DateTime.Today.AddYears(1).Year, 12, 31);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "OpenForImportPartsInSuppl.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                ShowDialog();
            }

            return;

        }
        #endregion

        #region Первоначальные установки
        private void SetInitialParams()
        {
            try
            {
                txtID_Ib.Text = "";
                cboxSheet.Properties.Items.Clear();
                treeListSettings.Nodes.Clear();

                m_objSettingForImportData = CSettingForImportData.GetSettingForImportDataInDebitArticle(m_objProfile, null);
                tabControlTreeList.SelectedTabPage = tabPageImportOrder;
                Text = strCaptionTextForImportInPlan;

                dateEditBegin.DateTime = new DateTime(System.DateTime.Today.AddYears(1).Year, 1, 1);
                dateEditEnd.DateTime = new DateTime(System.DateTime.Today.AddYears(1).Year, 12, 31);

                if (m_objSettingForImportData != null)
                {
                    foreach (CSettingItemForImportData objSetting in m_objSettingForImportData.SettingsList)
                    {
                        treeListSettings.AppendNode(new object[] { true, objSetting.TOOLS_USERNAME, System.String.Format("{0:### ### ##0}", objSetting.TOOLS_VALUE), objSetting.TOOLS_DESCRIPTION }, null).Tag = objSetting;
                    }
                }
            }//try
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "SetInitialParams.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;
        }

        #endregion

        #region Выбор файла
        private void btnFileOpenDialog_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.Refresh();
                    if ((openFileDialog.FileName != "") && (System.IO.File.Exists(openFileDialog.FileName) == true))
                    {
                        txtID_Ib.Text = openFileDialog.FileName;
                        ReadSheetListFromXLSFile(txtID_Ib.Text);
                    }
                }
            }//try
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "btnFileOpenDialog_Click.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;
        }
        /// <summary>
        /// Считывает коллекцию листов в файле MS Excel
        /// </summary>
        /// <param name="strFileName">имя файла MS Excel</param>
        private void ReadSheetListFromXLSFile(System.String strFileName)
        {
            if (strFileName == "") { return; }
            if (System.IO.File.Exists(strFileName) == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                     "файл \"" + strFileName + "\" не найден.", "Ошибка",
                     System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            Excel.Application oXL = null;
            Excel._Workbook oWB;

            object m = Type.Missing;
            try
            {
                cboxSheet.Properties.Items.Clear();
                this.Cursor = Cursors.WaitCursor;
                oXL = new Excel.Application();
                oWB = (Excel._Workbook)(oXL.Workbooks.Open(strFileName, 0, true, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));

                foreach (Excel._Worksheet objSheet in oWB.Worksheets)
                {
                    cboxSheet.Properties.Items.Add(objSheet.Name);
                }

                oWB.Close(Missing.Value, Missing.Value, Missing.Value);
                oXL.Quit();

            }
            catch (System.Exception f)
            {
                oXL = null;
                oWB = null;
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка экспорта в MS Excel.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oWB = null;
                oXL = null;
                cboxSheet.SelectedItem = ((cboxSheet.Properties.Items.Count > 0) ? cboxSheet.Properties.Items[0] : null);
                btnLoadDataFromFile.Enabled = (cboxSheet.SelectedItem != null);

                this.Cursor = System.Windows.Forms.Cursors.Default;
            }

            return;
        }
        /// <summary>
        /// Возвращает значение параметра настройки по его наименованию
        /// </summary>
        /// <param name="strSettingName">имя параметра</param>
        /// <param name="bIsCheck">признак того, включена ли настройка</param>
        /// <returns>значение параметра настройки</returns>
        private System.Int32 GetSettingValueByName(System.String strSettingName, ref System.Boolean bIsCheck)
        {
            System.Int32 iRet = 0;
            try
            {
                CSettingItemForImportData objSetting = null;
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeListSettings.Nodes)
                {
                    if (objNode.Tag == null) { continue; }
                    objSetting = (CSettingItemForImportData)objNode.Tag;
                    foreach (CSettingItemForImportData objItem in m_objSettingForImportData.SettingsList)
                    {
                        if (objSetting.TOOLS_NAME == strSettingName)
                        {
                            iRet = System.Convert.ToInt32(objNode.GetValue(colSettingsColumnNum));
                            bIsCheck = System.Convert.ToBoolean(objNode.GetValue(colCheck));
                            break;
                        }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "GetSettingValueByName.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return iRet;
        }

        /// <summary>
        /// Считывает информацию из фала MS Excel
        /// </summary>
        /// <param name="strFileName">имя файла MS Excel</param>
        private void ReadDataFromXLSFileForImportInPlan(System.String strFileName)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                System.IO.FileInfo newFile = new System.IO.FileInfo(strFileName);
                if (newFile.Exists == false)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Ошибка экспорта в MS Excel.\n\nНе найден файл: " + strFileName, "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

                treeListImportPlan.CellValueChanged -= new DevExpress.XtraTreeList.CellValueChangedEventHandler(treeListImportPlan_CellValueChanged);
                treeListImportPlan.Nodes.Clear();
                listEditLog.Items.Clear();

                object objToolsItemValue = null;
                System.Int32 iStartRowForImport = 0;
                System.Int32 iColumnACCOUNTPLAN_1C_CODE = 0;
                System.Int32 iColumnBUDGETITEM_NUM = 0;
                System.Int32 iColumnBUDGETITEM_NAME = 0;
                System.Int32 iColumnBUDGETDEP_NAME = 0;
                System.Int32 iColumnBUDGETEXPENSETYPE_NAME = 0;
                System.Int32 iColumnBUDGETPROJECT_NAME = 0;

                objToolsItemValue = m_objSettingForImportData.SettingsList.SingleOrDefault<CSettingItemForImportData>(x => x.TOOLS_NAME == strFieldNameACCOUNTPLAN_1C_CODE).TOOLS_VALUE;
                if (objToolsItemValue != null)
                {
                    iColumnACCOUNTPLAN_1C_CODE = System.Convert.ToInt32(objToolsItemValue);
                }
                objToolsItemValue = m_objSettingForImportData.SettingsList.SingleOrDefault<CSettingItemForImportData>(x => x.TOOLS_NAME == strFieldNameBUDGETITEM_NUM).TOOLS_VALUE;
                if (objToolsItemValue != null)
                {
                    iColumnBUDGETITEM_NUM = System.Convert.ToInt32(objToolsItemValue);
                }
                objToolsItemValue = m_objSettingForImportData.SettingsList.SingleOrDefault<CSettingItemForImportData>(x => x.TOOLS_NAME == strFieldNameBUDGETITEM_NAME).TOOLS_VALUE;
                if (objToolsItemValue != null)
                {
                    iColumnBUDGETITEM_NAME = System.Convert.ToInt32(objToolsItemValue);
                }
                objToolsItemValue = m_objSettingForImportData.SettingsList.SingleOrDefault<CSettingItemForImportData>(x => x.TOOLS_NAME == strFieldNameBUDGETDEP_NAME).TOOLS_VALUE;
                if (objToolsItemValue != null)
                {
                    iColumnBUDGETDEP_NAME = System.Convert.ToInt32(objToolsItemValue);
                }
                objToolsItemValue = m_objSettingForImportData.SettingsList.SingleOrDefault<CSettingItemForImportData>(x => x.TOOLS_NAME == strFieldNameBUDGETEXPENSETYPE_NAME).TOOLS_VALUE;
                if (objToolsItemValue != null)
                {
                    iColumnBUDGETEXPENSETYPE_NAME = System.Convert.ToInt32(objToolsItemValue);
                }
                objToolsItemValue = m_objSettingForImportData.SettingsList.SingleOrDefault<CSettingItemForImportData>(x => x.TOOLS_NAME == strFieldNameBUDGETPROJECT_NAME).TOOLS_VALUE;
                if (objToolsItemValue != null)
                {
                    iColumnBUDGETPROJECT_NAME = System.Convert.ToInt32(objToolsItemValue);
                }
                objToolsItemValue = m_objSettingForImportData.SettingsList.SingleOrDefault<CSettingItemForImportData>(x => x.TOOLS_NAME == strFieldNameSTARTROW).TOOLS_VALUE;
                if (objToolsItemValue != null)
                {
                    iStartRowForImport = System.Convert.ToInt32(objToolsItemValue);
                }

                System.String strACCOUNTPLAN_1C_CODE = System.String.Empty;
                System.String strBUDGETITEM_NUM = System.String.Empty;
                System.String strBUDGETITEM_NAME = System.String.Empty;
                System.String strBUDGETDEP_NAME = System.String.Empty;
                System.String strBUDGETEXPENSETYPE_NAME = System.String.Empty;
                System.String strBUDGETPROJECT_NAME = System.String.Empty;


                System.Int32 iCurrentRow = iStartRowForImport;
                System.Int32 i = 1;

                CAccountPlan objAccountPlan = null;
                CBudgetDep objBudgetDep = null;
                CBudgetExpenseType objBudgetExpenseType = null;
                CBudgetProject objBudgetProject = null;
                CDebitArticle objDebitArticle = null;

                using (ExcelPackage package = new ExcelPackage(newFile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[cboxSheet.Text];
                    if (worksheet != null)
                    {

                        System.Boolean bStopRead = false;
                        System.Boolean bErrExists = false;
                        System.String strFrstColumn = "";

                        while (bStopRead == false)
                        {
                            bErrExists = false;

                            // пробежим по строкам и считаем информацию
                            strFrstColumn = System.Convert.ToString(worksheet.Cells[iCurrentRow, 1].Value);
                            if (strFrstColumn == "")
                            {
                                bStopRead = true;
                            }
                            else
                            {
                                strACCOUNTPLAN_1C_CODE = System.Convert.ToString(worksheet.Cells[iCurrentRow, iColumnACCOUNTPLAN_1C_CODE].Value).Trim();
                                strBUDGETITEM_NUM = System.Convert.ToString(worksheet.Cells[iCurrentRow, iColumnBUDGETITEM_NUM].Value).Trim();
                                strBUDGETITEM_NAME = System.Convert.ToString(worksheet.Cells[iCurrentRow, iColumnBUDGETITEM_NAME].Value).Trim();
                                strBUDGETDEP_NAME = System.Convert.ToString(worksheet.Cells[iCurrentRow, iColumnBUDGETDEP_NAME].Value).Trim();
                                strBUDGETEXPENSETYPE_NAME = System.Convert.ToString(worksheet.Cells[iCurrentRow, iColumnBUDGETEXPENSETYPE_NAME].Value).Trim();
                                strBUDGETPROJECT_NAME = System.Convert.ToString(worksheet.Cells[iCurrentRow, iColumnBUDGETPROJECT_NAME].Value).Trim();

                                objAccountPlan = null;
                                objBudgetDep = null;
                                objBudgetExpenseType = null;
                                objBudgetProject = null;
                                objDebitArticle = null;

                                if (strACCOUNTPLAN_1C_CODE.Length == 0)
                                {
                                    bErrExists = true;
                                    listEditLog.Items.Add(String.Format("{0} ошибка: пустое значение плана счетов.", i));
                                    listEditLog.Refresh();
                                }
                                else
                                {
                                    objAccountPlan = m_objAccountPlanList.SingleOrDefault<CAccountPlan>(x => x.CodeIn1C.Equals(strACCOUNTPLAN_1C_CODE) == true);
                                    if (objAccountPlan == null)
                                    {
                                        bErrExists = true;
                                        listEditLog.Items.Add(String.Format("{0} ошибка: не найден план счетов.", i));
                                        listEditLog.Refresh();
                                    }
                                }

                                if (strBUDGETITEM_NUM.Length == 0)
                                {
                                    bErrExists = true;
                                    listEditLog.Items.Add(String.Format("{0} ошибка: пустое значение номера статьи расходов.", i));
                                    listEditLog.Refresh();
                                }

                                if (strBUDGETITEM_NAME.Length == 0)
                                {
                                    bErrExists = true;
                                    listEditLog.Items.Add(String.Format("{0} ошибка: пустое значение наименования статьи расходов.", i));
                                    listEditLog.Refresh();
                                }

                                if (strBUDGETDEP_NAME.Length == 0)
                                {
                                    bErrExists = true;
                                    listEditLog.Items.Add(String.Format("{0} ошибка: пустое значение наименования службы.", i));
                                    listEditLog.Refresh();
                                }
                                else
                                {
                                    objBudgetDep = m_objBudgetDepList.SingleOrDefault<CBudgetDep>(x => x.Name.Equals(strBUDGETDEP_NAME) == true);
                                    if (objBudgetDep == null)
                                    {
                                        bErrExists = true;
                                        listEditLog.Items.Add(String.Format("{0} ошибка: не найдена служба.", i));
                                        listEditLog.Refresh();
                                    }
                                }

                                if (strBUDGETEXPENSETYPE_NAME.Length == 0)
                                {
                                    bErrExists = true;
                                    listEditLog.Items.Add(String.Format("{0} ошибка: пустое значение наименования типа бюджетных расходов.", i));
                                    listEditLog.Refresh();
                                }
                                else
                                {
                                    objBudgetExpenseType = m_objBudgetExpenseTypeList.SingleOrDefault<CBudgetExpenseType>(x => x.Name.Equals(strBUDGETEXPENSETYPE_NAME) == true);
                                    if (objBudgetExpenseType == null)
                                    {
                                        bErrExists = true;
                                        listEditLog.Items.Add(String.Format("{0} ошибка: не найден тип бюджетных расходов.", i));
                                        listEditLog.Refresh();
                                    }
                                }

                                if (strBUDGETPROJECT_NAME.Length == 0)
                                {
                                    bErrExists = true;
                                    listEditLog.Items.Add(String.Format("{0} ошибка: пустое значение наименования проекта.", i));
                                    listEditLog.Refresh();
                                }
                                else
                                {
                                    objBudgetProject = m_objBudgetProjectList.SingleOrDefault<CBudgetProject>(x => x.Name.Equals(strBUDGETPROJECT_NAME) == true);
                                    if (objBudgetProject == null)
                                    {
                                        bErrExists = true;
                                        listEditLog.Items.Add(String.Format("{0} ошибка: не найден проект.", i));
                                        listEditLog.Refresh();
                                    }
                                }

                                if( (bErrExists == false) && (bStopRead == false) && (objAccountPlan != null) &&
                                    (objBudgetDep != null) && (objBudgetExpenseType != null) && (objBudgetProject != null) )
                                {
                                    treeListImportPlan.AppendNode(new object[] { true, objAccountPlan.CodeIn1C, 
                                        strBUDGETITEM_NUM, strBUDGETITEM_NAME,  true, objBudgetDep.Name, 
                                        true, objBudgetExpenseType.Name, true, objBudgetProject.Name, false, null}, null).Tag = new CDebitArticleImportRow()
                                        {
                                            ACCOUNTPLAN_1C_CODE = objAccountPlan.CodeIn1C,
                                            ACCOUNTPLAN_GUID = objAccountPlan.uuidID,
                                            BUDGETDEP_NAME = objBudgetDep.Name,
                                            BUDGETDEP_GUID = objBudgetDep.uuidID,
                                            BUDGETEXPENSETYPE_NAME = objBudgetExpenseType.Name,
                                            BUDGETEXPENSETYPE_GUID = objBudgetExpenseType.uuidID,
                                            BUDGETITEM_NUM = strBUDGETITEM_NUM,
                                            BUDGETITEM_NAME = strBUDGETITEM_NAME,
                                            BUDGETPROJECT_NAME = objBudgetProject.Name,
                                            BUDGETPROJECT_GUID = objBudgetProject.uuidID,
                                            DEBITARTICLE_ID = iCurrentRow, 
                                            IMPORTISOK = false, 
                                            IMPORTRESULT_DESCRIPTION = System.String.Empty
                                        };
                                    treeListImportPlan.Refresh();

                                    listEditLog.Items.Add(String.Format("{0} OK ", i));
                                    listEditLog.Refresh();
                                }
                                else if ((bErrExists == true) && (bStopRead == false))
                                {
                                    treeListImportPlan.AppendNode(new object[] {  ( objAccountPlan != null ), strACCOUNTPLAN_1C_CODE, 
                                        strBUDGETITEM_NUM, strBUDGETITEM_NAME,  
                                        ( objBudgetDep != null ), strBUDGETDEP_NAME, 
                                        ( objBudgetExpenseType != null ), strBUDGETEXPENSETYPE_NAME, 
                                        ( objBudgetProject != null ), strBUDGETPROJECT_NAME, false, null }, null).Tag = new CDebitArticleImportRow()
                                        {
                                            ACCOUNTPLAN_1C_CODE = ( (objAccountPlan == null) ? System.String.Empty : objAccountPlan.CodeIn1C ),
                                            ACCOUNTPLAN_GUID = ((objAccountPlan == null) ? System.Guid.Empty : objAccountPlan.uuidID),
                                            BUDGETDEP_NAME = ((objBudgetDep == null) ? System.String.Empty : objBudgetDep.Name),
                                            BUDGETDEP_GUID = ( (objBudgetDep == null) ? System.Guid.Empty : objBudgetDep.uuidID),
                                            BUDGETEXPENSETYPE_NAME = ((objBudgetExpenseType == null) ? System.String.Empty : objBudgetExpenseType.Name),
                                            BUDGETEXPENSETYPE_GUID = ((objBudgetExpenseType == null) ? System.Guid.Empty : objBudgetExpenseType.uuidID),
                                            BUDGETITEM_NUM = strBUDGETITEM_NUM,
                                            BUDGETITEM_NAME = strBUDGETITEM_NAME,
                                            BUDGETPROJECT_NAME = ((objBudgetProject == null) ? System.String.Empty : objBudgetProject.Name),
                                            BUDGETPROJECT_GUID = ((objBudgetProject == null) ? System.Guid.Empty : objBudgetProject.uuidID),
                                            DEBITARTICLE_ID = iCurrentRow,
                                            IMPORTISOK = false,
                                            IMPORTRESULT_DESCRIPTION = System.String.Empty
                                        };

                                    treeListImportPlan.Refresh();
                                }

                                iCurrentRow++;
                                i++;
                                strFrstColumn = System.Convert.ToString(worksheet.Cells[iCurrentRow, 1].Value);
                                listEditLog.Refresh();

                                this.Text = String.Format("обрабатывается запись №{0}", i);
                                this.Refresh();
                            }


                        } //while (bStopRead == false)
                    }
                    worksheet = null;
                }


            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка импорта данных из файла MS Excel.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                treeListImportPlan.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(treeListImportPlan_CellValueChanged);
                treeListImportPlan.BestFitColumns();

                this.Cursor = System.Windows.Forms.Cursors.Default;
                this.Text = strCaptionTextForImportInPlan;
            }
        }

        private void treeListImportPlan_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            try
            {
                if ((e.Value != null) && (treeListImportPlan.FocusedNode != null) && (treeListImportPlan.FocusedNode.Tag != null))
                {
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "treeListImportPlan_CellValueChanged.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }

        private void btnLoadDataFromFile_Click(object sender, EventArgs e)
        {
            try
            {
                ReadDataFromXLSFileForImportInPlan(txtID_Ib.Text);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "btnLoadDataFromFile_Click.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }

        private void cboxSheet_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                btnLoadDataFromFile.Enabled = (cboxSheet.SelectedItem != null);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "cboxSheet_SelectedValueChanged.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }

        #endregion

        #region Подтвердить выбор
        private void treeListSettings_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            try
            {
                if ((e.Node == null) || (e.Node.Tag == null)) { return; }

                if (e.Column == colCheck)
                {
                    System.String strSettingName = ((CSettingItemForImportData)e.Node.Tag).TOOLS_NAME;
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "treeListSettings_CellValueChanged.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;
        }


        #endregion

        #region Отмена
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();

            }//try
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "btnCancel_Click.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;
        }
        #endregion

        #region Сохранить настройки в БД

        private void SaveSettings()
        {
            try
            {
                if ((m_objSettingForImportData != null) && (m_objSettingForImportData.SettingsList != null))
                {
                    CSettingItemForImportData objSetting = null;
                    foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeListSettings.Nodes)
                    {
                        if (objNode.Tag == null) { continue; }
                        objSetting = (CSettingItemForImportData)objNode.Tag;
                        foreach( CSettingItemForImportData objItem in m_objSettingForImportData.SettingsList )
                        {
                            if (objSetting.TOOLS_ID == objItem.TOOLS_ID)
                            {
                                objItem.TOOLS_VALUE = objSetting.TOOLS_VALUE;
                                objItem.TOOLS_DESCRIPTION = objSetting.TOOLS_DESCRIPTION;
                                break;
                            }
                        }
                    }
                }

                System.String strErr = "";
                if (SaveXMLSettings(ref strErr) == false)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Ошибка сохранения настроек в базе данных.\nТекст ошибки: " + strErr, "Ошибка",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                }

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "SaveSettings.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;
        }

        private System.Boolean SaveXMLSettings(ref System.String strErr)
        {
            System.Boolean bRet = false;
            try
            {
                System.Xml.XmlNodeList nodeList = m_objSettingForImportData.XMLSettings.GetElementsByTagName(strNodeSettingname);
                if (nodeList != null)
                {
                    CSettingItemForImportData objSetting = null;
                    foreach (System.Xml.XmlNode xmlNode in nodeList)
                    {
                        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeListSettings.Nodes)
                        {
                            if (objNode.Tag == null) { continue; }
                            objSetting = (CSettingItemForImportData)objNode.Tag;

                            if (objSetting.TOOLS_ID.ToString() == xmlNode.Attributes[0].Value)
                            {
                                xmlNode.Attributes[3].InnerText = System.Convert.ToString(objNode.GetValue(colSettingsDescription));
                                xmlNode.Attributes[4].InnerText = System.Convert.ToString(objNode.GetValue(colSettingsColumnNum));
                            }
                        }
                    }
                    // теперь и в Базе данных
                    bRet = m_objSettingForImportData.SaveExportSetting(m_objProfile, null, ref strErr);
                }
            }//try
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "SaveXMLSettingsForSheet.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return bRet;

        }

        private void btnSaveSetings_Click(object sender, EventArgs e)
        {
            try
            {
                SaveSettings();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка сохранения настроек.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;

        }
        #endregion

        #region Закрытие формы
        private void frmImportDataInDebitArticleList_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                m_strFileFullName = txtID_Ib.Text;
                SelectedSheetId = cboxSheet.SelectedIndex;
                SheetList = new List<string>();
                foreach (object objItem in cboxSheet.Properties.Items)
                {
                    SheetList.Add(System.Convert.ToString(objItem));
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "frmImportDataInDebitArticleList_FormClosed.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }
        #endregion

        #region Отрисовка ячеек дерева
        private void treeListImportPlan_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            try
            {
                if( ( (e.Column == colAccountPlan) && ( System.Convert.ToBoolean( e.Node.GetValue( colAccountPlanCheck ) ) == false ) ) ||
                    ((e.Column == colBudgetDep) && (System.Convert.ToBoolean(e.Node.GetValue(colBudgetDepCheck)) == false))  ||
                    ((e.Column == colBudgetExpenseType) && (System.Convert.ToBoolean(e.Node.GetValue(colBudgetExpenseTypeCheck)) == false)) ||
                    ((e.Column == colBudgetProject) && (System.Convert.ToBoolean(e.Node.GetValue(colBudgetProjectCheck)) == false)) 
                    )
                {
                    e.Appearance.DrawString(e.Cache, e.CellText,
                                new Rectangle(e.Bounds.Location.X, e.Bounds.Location.Y,
                                e.Bounds.Size.Width - 3, e.Bounds.Size.Height), new System.Drawing.SolidBrush(Color.Red));
                    e.Handled = true;
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "treeListImportPlan_CustomDrawNodeCell.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }

        private void treeListImportPlan_CustomDrawNodeImages(object sender, DevExpress.XtraTreeList.CustomDrawNodeImagesEventArgs e)
        {
            try
            {
                if (treeListImportPlan.Nodes.Count == 0) { return; }
                if (e.Node == null) { return; }

                System.Boolean bAccountPlanCheck = System.Convert.ToBoolean(e.Node.GetValue(colAccountPlanCheck));
                System.Boolean bBudgetExpenseTypeCheck = System.Convert.ToBoolean(e.Node.GetValue(colBudgetExpenseTypeCheck));
                System.Boolean bBudgetProjectCheck = System.Convert.ToBoolean(e.Node.GetValue(colBudgetProjectCheck));
                System.Boolean bBudgetDepCheck = System.Convert.ToBoolean(e.Node.GetValue(colBudgetDepCheck));

                int Y = e.SelectRect.Top + (e.SelectRect.Height - imglNodes.Images[0].Height) / 2;

                if ((bAccountPlanCheck == false) || (bBudgetExpenseTypeCheck == false) ||
                    (bBudgetProjectCheck == false) || (bBudgetDepCheck == false))
                {
                    try
                    {
                        //ControlPaint.DrawImageDisabled(e.Graphics, imglNodes.Images[1], e.SelectRect.X, Y, Color.Black);
                        e.Graphics.DrawImage(imglNodes.Images[1], new Point(e.SelectRect.X, Y));
                        e.Handled = true;
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        e.Graphics.DrawImage(imglNodes.Images[0], new Point(e.SelectRect.X, Y));
                        e.Handled = true;
                    }
                    catch { }
                }
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(null, "Ошибка отрисовки картинок в узлах\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;

        }

        #endregion

        #region Импорт данных в справочник статей расходов
        /// <summary>
        /// Импорт данных в справочник статей расходов
        /// </summary>
        /// <param name="strErr">сообщение об ошибке</param>
        /// <returns>true - все записи импортированы; false - не все записи импортированы</returns>
        private System.Boolean ImportDebitArticleListToDB(ref System.String strErr)
        {
            System.Boolean bRet = false;
            System.Boolean bImportResult = false;
            System.Int32 iRes = 0;
            try
            {
                // необхоимо предварительно пробехать и выяснить, все ли записи идентифицированы в справочниках
                System.Boolean bAccountPlanCheck = false;
                System.Boolean bBudgetExpenseTypeCheck = false;
                System.Boolean bBudgetProjectCheck = false;
                System.Boolean bBudgetDepCheck = false;
                System.Boolean bAllRecordsAreValid = true;

                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeListImportPlan.Nodes)
                {
                    bAccountPlanCheck = System.Convert.ToBoolean(objNode.GetValue(colAccountPlanCheck));
                    bBudgetExpenseTypeCheck = System.Convert.ToBoolean(objNode.GetValue(colBudgetExpenseTypeCheck));
                    bBudgetProjectCheck = System.Convert.ToBoolean(objNode.GetValue(colBudgetProjectCheck));
                    bBudgetDepCheck = System.Convert.ToBoolean(objNode.GetValue(colBudgetDepCheck));

                    if ((bAccountPlanCheck == false) || (bBudgetExpenseTypeCheck == false) ||
                    (bBudgetProjectCheck == false) || (bBudgetDepCheck == false))
                    {
                        bAllRecordsAreValid = false;
                        treeListImportPlan.FocusedNode = objNode;

                        break;
                    }
                }

                if (bAllRecordsAreValid == false)
                {
                    strErr += ( "Процедура сохранения данных отменена,\nт.к. в списке имеются записи, не удовлетворяющие условия импорта." );
                    return bRet;
                }

                // заполняется список записей для импорта в БД
                List<CDebitArticleImportRow> DebitArticleImportRowList = new List<CDebitArticleImportRow>();
                CDebitArticleImportRow objDebitArticleImportRow = null;
                CDebitArticleImportRow objDebitArticleImportRowResult = null;

                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeListImportPlan.Nodes)
                {
                    if (objNode.Tag == null)
                    {
                        objDebitArticleImportRow = new CDebitArticleImportRow();
                    }
                    else
                    {
                        objDebitArticleImportRow = (CDebitArticleImportRow)objNode.Tag;
                    }

                    DebitArticleImportRowList.Add(objDebitArticleImportRow);
                }

                List<CDebitArticleImportRow> DebitArticleImportResultRowList = new List<CDebitArticleImportRow>();
                System.DateTime BEGIN_DATE = dateEditBegin.DateTime;
                System.DateTime END_DATE = dateEditEnd.DateTime;
                System.Boolean ClearDebitArticleperiod = (rGroupClearFinansialPeriod.SelectedIndex > 0);

                // импорт данных в БД
                bImportResult = CDebitArticleDatabaseModel.ImportDebitArticleListToDB(m_objProfile, ClearDebitArticleperiod, BEGIN_DATE, END_DATE,
                    DebitArticleImportRowList, ref DebitArticleImportResultRowList,
                    ref strErr, ref iRes);

                // для каждой записи указывается результат импорта
                if ((DebitArticleImportResultRowList != null) && (DebitArticleImportResultRowList.Count > 0) && (bImportResult == false))
                {
                    this.tableLayoutPanel1.SuspendLayout();

                    foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeListImportPlan.Nodes)
                    {
                        if (objNode.Tag == null) { continue; }

                        objDebitArticleImportRow = null;
                        objDebitArticleImportRowResult = null;

                        objDebitArticleImportRow = (CDebitArticleImportRow)objNode.Tag;

                        if (objDebitArticleImportRow != null)
                        {
                            objDebitArticleImportRowResult = DebitArticleImportResultRowList.SingleOrDefault<CDebitArticleImportRow>(x => x.DEBITARTICLE_ID == objDebitArticleImportRow.DEBITARTICLE_ID);
                            if (objDebitArticleImportRowResult != null)
                            {
                                objDebitArticleImportRow.IMPORTISOK = objDebitArticleImportRowResult.IMPORTISOK;
                                objDebitArticleImportRow.IMPORTRESULT_DESCRIPTION = objDebitArticleImportRowResult.IMPORTRESULT_DESCRIPTION;

                                objNode.SetValue(colImportIsOK, objDebitArticleImportRow.IMPORTISOK);
                                objNode.SetValue(colImportResultDescrpn, objDebitArticleImportRow.IMPORTRESULT_DESCRIPTION);
                            }
                        }
                    }

                    this.tableLayoutPanel1.ResumeLayout(false);
                }

                if (bImportResult == true)
                {
                    strErr += ("Все записи успешно импортированы.\nПрограмма загрузит обновлённый справочник статей расходов.\nПроцесс может занять некоторое время.");
                }
                else
                {
                    strErr += ("Не все записи удалось импортировать.\nОзнакомьтесь с результатами импорта и повторите попытку.");
                }

                bRet = bImportResult;

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "ImportDebitArticleListToDB.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return bRet;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (treeListImportPlan.Nodes.Count == 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Список для импорта пуст.\nОперация отменена.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                    return;
                }

                System.String strErr = System.String.Empty;

                if (ImportDebitArticleListToDB(ref strErr) == true)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        strErr, "Сообщение",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }

            }//try
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "btnOk_Click.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            return;
        }

        #endregion 


    }
}
