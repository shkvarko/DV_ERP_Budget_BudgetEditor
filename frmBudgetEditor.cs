using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;
using ERP_Budget.Common;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace ErpBudgetBudgetEditor
{
    public partial class frmBudgetEditor : DevExpress.XtraEditors.XtraForm
    {
        #region ����������, ��������, ���������
        private UniXP.Common.CProfile m_objProfile;
        /// <summary>
        /// ���������� ������������� �������
        /// </summary>
        private System.Guid m_uuidBudget;
        /// <summary>
        /// ������ ������ "������", ��������� � ������ ��������
        /// </summary>
        private ERP_Budget.Common.CBudget m_objBudget;
        /// <summary>
        /// ������ �����
        /// </summary>
        private List<ERP_Budget.Common.CCurrency> m_objCurrencyList;
        ///// <summary>
        ///// ������, ������� ������� ��������� ��� �������� ������ � �����������
        ///// </summary>
        //private System.String m_strCurrency;
        /// <summary>
        /// ������ �������� ���� ��� ������������ ������
        /// </summary>
        private List<TotalRow> m_objTotalRowList;
        /// <summary>
        /// �������� ������ � �������
        /// </summary>
        private TotalRow m_objGrandTotalRow;
        private DevExpress.XtraTreeList.Nodes.TreeListNode draggedNodeParent;
        /// <summary>
        /// �������� ������
        /// </summary>
        private frmCellProperties m_objfrmCellProperties;
        /// <summary>
        /// ��������� ��������� �������� ������
        /// </summary>
        private ERP_Budget.Common.CCurrency m_objLastSelectedCurrency;
        /// <summary>
        /// ������� ����, ��� ���������� �������� ������
        /// </summary>
        private System.Boolean m_bChangeCellValue;
        // ������
        private System.Threading.Thread thrAddress;
        public System.Threading.Thread ThreadAddress
        {
            get { return thrAddress; }
        }
        private System.Threading.ManualResetEvent m_EventStopThread;
        public System.Threading.ManualResetEvent EventStopThread
        {
            get { return m_EventStopThread; }
        }
        private System.Threading.ManualResetEvent m_EventThreadStopped;
        public System.Threading.ManualResetEvent EventThreadStopped
        {
            get { return m_EventThreadStopped; }
        }
        public delegate void LoadAddressDelegate();
        public LoadAddressDelegate m_LoadAddressDelegate;

        public delegate void SendMessageToLogDelegate( System.String strMessage );
        public SendMessageToLogDelegate m_SendMessageToLogDelegate;


        private const System.Int32 iThreadSleepTime = 1000;
        private System.Boolean m_bThreadFinishJob;
        private UniXP.Common.MENUITEM m_objMenuItem;
        #endregion

        #region �����������

        public frmBudgetEditor(UniXP.Common.CProfile objProfile, System.Guid uuidBudget, UniXP.Common.MENUITEM objMenuItem)
        {
            InitializeComponent();

            this.m_objProfile = objProfile;
            this.m_uuidBudget = uuidBudget;
            this.m_objMenuItem = objMenuItem;
            this.draggedNodeParent = null;
            m_objLastSelectedCurrency = null;
            m_objfrmCellProperties = null;
            m_bChangeCellValue = false;

            // ������������� ������� ������ "������"
            this.m_objBudget = new ERP_Budget.Common.CBudget();
            this.m_objBudget.uuidID = m_uuidBudget;
            this.m_objBudget.Init( this.m_objProfile, uuidBudget );
            lblInfo.Text = "������: " + m_objBudget.BudgetDep.Name + "\t������: " + m_objBudget.Name + "\t���: " + m_objBudget.Date.Year.ToString() +
                "\t������: " + m_objBudget.Currency.CurrencyCode;
            lblCurrencyRate.Text = "";

            // ������ �����
            this.m_objCurrencyList = null;
            // ����������� ������ �����
            this.m_objCurrencyList = ERP_Budget.Common.CCurrency.GetCurrencyList(this.m_objProfile);

            // ����� �����
            this.m_objBudget.BudgetCurrencyRate = new ERP_Budget.Common.CBudgetCurrencyRate( m_objBudget.uuidID );
            this.m_objBudget.BudgetCurrencyRate.LoadCurrencyRateList(m_objProfile);


            // ������ �������� �����
            this.m_objTotalRowList = new List<TotalRow>();

            // �������� ������
            this.m_objGrandTotalRow = new TotalRow();

            // �������� ������������ ����
            SetAccessForDynamicRights();

            // �������� ��������� �������
            CheckBudgetState();
        }

        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if( components != null )
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }
        #endregion

        #region ������������ �����
        private System.Boolean m_bSuperVisor;
        private System.Boolean m_bManager;
        private void SetAccessForDynamicRights()
        {
            try
            {
                m_bSuperVisor = m_objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRCoordinator ) || m_objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRInspector );
                m_bManager = m_objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRManager );
                // � ����������� �� ���� �������, ��������/��������� ������ ����������� �������
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ �������� ������������ ����\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        /// <summary>
        /// �������� ��������� �������
        /// </summary>
        private void CheckBudgetState()
        {
            try
            {
                // ���� ������ ���������, �� ��������� ��������
                barBtnAddNode.Visibility = ( m_objBudget.IsAccept ) ? DevExpress.XtraBars.BarItemVisibility.Never : DevExpress.XtraBars.BarItemVisibility.Always;
                barBtnDeleteNode.Visibility = ( m_objBudget.IsAccept ) ? DevExpress.XtraBars.BarItemVisibility.Never : DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnAccept.Visibility = ( m_bSuperVisor ) ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                barBtnAddBudgetItem.Visibility = (m_bSuperVisor) ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                barBtnAccept.Glyph = ( m_objBudget.IsAccept ) ? ErpBudgetBudgetEditor.Properties.Resources.document_edit : ErpBudgetBudgetEditor.Properties.Resources.document_lock;
                barBtnAccept.Hint = ( m_objBudget.IsAccept ) ? "����� ������� '������ ���������'" : "��������� ������";

                foreach( DevExpress.XtraTreeList.Columns.TreeListColumn column in treeList.Columns )
                {
                    column.ImageIndex = ( ( column.VisibleIndex >= 0 ) && ( m_objBudget.IsAccept ) ) ? 0 : -1;
                }

                treeList.SelectImageList = ( m_objBudget.IsAccept ) ? null : imglNodes;
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ ��������� ��������� ����������\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        #endregion

        #region ��������� ��������� � ������
        /// <summary>
        /// ��������� "������ ��������"
        /// </summary>
        private System.Boolean m_bIsTreeModified;
        /// <summary>
        /// �������� �������� ���������� "������ ��������"
        /// </summary>
        /// <param name="bModified"></param>
        private void SetModified( System.Boolean bModified )
        {
            try
            {
                if( m_objBudget.IsAccept ) { return; }
                m_bIsTreeModified = bModified;
                btnSave.Enabled = m_bIsTreeModified;
                btnCancel.Enabled = m_bIsTreeModified;
                barBtnAddBudgetItem.Enabled = (m_bIsTreeModified == false);
                barBtnRefresh.Enabled = (m_bIsTreeModified == false);
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ ������ SetModified()\nbModified = " + 
                    bModified.ToString() + "\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        #endregion

        #region ���������� ������ ��������� ������

        /// <summary>
        /// ��������� ������ ��������� ������
        /// </summary>
        /// <returns>true - �������; false - ������</returns>
        private System.Boolean bLoadBudgeItems()
        {
            System.Boolean bRes = false;
            try
            {
                // ���� ������ ���������, �� ������ "���������", "��������" �������
                btnSave.Visible = ( this.m_objBudget.IsAccept != true );
                btnCancel.Visible = ( this.m_objBudget.IsAccept != true );
                btnDown.Visible = ( this.m_objBudget.IsAccept != true );
                btnDrop.Visible = ( this.m_objBudget.IsAccept != true );
                btnLeft.Visible = ( this.m_objBudget.IsAccept != true );
                btnRight.Visible = ( this.m_objBudget.IsAccept != true );
                btnUp.Visible = ( this.m_objBudget.IsAccept != true );
                // ���������� �������� ������ ��������� ������
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                treeList.Enabled = false;

                m_objBudget.BudgetItemList.Clear();
                // ��������� ����������� ������� ������
                treeList.BeforeFocusNode -= new DevExpress.XtraTreeList.BeforeFocusNodeEventHandler( this.treeList_BeforeFocusNode );
                treeList.AfterFocusNode -= new DevExpress.XtraTreeList.NodeEventHandler( this.treeList_AfterFocusNode );

                this.tableLayoutPanel1.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();

                bRes = ERP_Budget.Common.CBudgetItem.LoadBudgetItemList(this.m_objProfile, treeList, this.m_objBudget);

                // ��������� �������� ����� ��� ��������� ������� ������
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeList.Nodes)
                {
                    if (objNode.HasChildren == false) { continue; }
                    if (objNode.Nodes.Count <= 1) { continue; }

                    foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNodeChild in objNode.Nodes)
                    {
                        foreach (DevExpress.XtraTreeList.Columns.TreeListColumn objColumn in treeList.Columns)
                        {
                            if (isMonthColumn(objColumn) == false) { continue; }
                            if (objColumn == colSummary) { continue; }

                            CalcSubBudgetRowTotalSum(objNodeChild, objColumn);
                        }
                    }
                }

                this.tableLayoutPanel1.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();

                // ������� ������ �������� �����
                this.m_objTotalRowList.Clear();
                this.m_objGrandTotalRow.ClearTotal();
                if( treeList.Nodes.Count > 0 )
                {
                    for( System.Int32 i = 0; i < treeList.Nodes.Count; i++ )
                    {
                        this.m_objTotalRowList.Add( new TotalRow() );
                    }
                }

                if( bRes == true )
                {
                    // ����� ����� �������
                    if (m_objBudget.BudgetCurrencyRate.BudgetCurrencyRateItemList != null)
                    {
                        foreach (ERP_Budget.Common.CBudgetCurrencyRateItem objItem in m_objBudget.BudgetCurrencyRate.BudgetCurrencyRateItemList)
                        {
                            lblCurrencyRate.Text += (objItem.CurrencyIn.CurrencyCode + " - " + objItem.CurrencyOut.CurrencyCode + " " + System.String.Format("{0:### ### ##0.00}", objItem.Value) + "\t");
                        }
                    }

                    // ������� �������� �����
                    m_objfrmCellProperties = null;
                    m_objfrmCellProperties = new frmCellProperties(m_objBudget.Currency);
                    m_objfrmCellProperties.LoadCurrencyList(m_objCurrencyList);

                }
                bRes = true;
            }//try
            catch( System.Exception e )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ ������ ������ ��������� ������\n" + e.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
                SetModified( false );
                treeList.Enabled = true;
                treeList.AfterFocusNode += new DevExpress.XtraTreeList.NodeEventHandler( this.treeList_AfterFocusNode );
                treeList.BeforeFocusNode += new DevExpress.XtraTreeList.BeforeFocusNodeEventHandler( this.treeList_BeforeFocusNode );
                OnSelectItem();
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }

            return bRes;
        }
        /// <summary>
        /// ��������� ������ ��������� ������
        /// </summary>
        private void RefreshBudgetItemsList()
        {
            try
            {
                if( bLoadBudgeItems() )
                {
                    // ������������ �����
                    CalcGrandTotalRow();
                }

            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ ���������� ������ ������ �������.\n\n����� ������: " + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        private void barBtnRefresh_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            try
            {
                RefreshBudgetItemsList();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ ���������� ������ ������\n" + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        private void mitemRefresh_Click( object sender, EventArgs e )
        {
            try
            {
                RefreshBudgetItemsList();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ ���������� ������ ������\n" + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        #endregion

        #region ��������, ������� ����
        /// <summary>
        /// ������� ����� ������ �������
        /// </summary>
        private void AddBudgetItem()
        {
            try
            {
                frmDebitArticleEditor objfrmDebitArticleEditor = new frmDebitArticleEditor(m_objProfile, m_objBudget);
                objfrmDebitArticleEditor.NewBudgetItem( m_objBudget );
                System.Boolean bNeedRefresh = (objfrmDebitArticleEditor.DialogResult == DialogResult.OK);
                objfrmDebitArticleEditor.Dispose();
                if (bNeedRefresh == true)
                {
                    RefreshBudgetItemsList();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "������ ���������� ������ ��������.\n\n����� ������: " + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        private void barBtnAddBudgetItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                AddBudgetItem();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "������ ���������� ������ �������\n" + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        private void mitemAddBudgetItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddBudgetItem();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "������ ���������� ������ �������\n" + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        /// <summary>
        /// ��������� ���� � ������
        /// </summary>
        /// <param name="objParentNode">������������ ����</param>
        private void AddNode( DevExpress.XtraTreeList.Nodes.TreeListNode objParentNode )
        {
            try
            {
                // ��������� ����� ������ ���������
                if( ( treeList.Nodes.Count == 0 ) || ( objParentNode == null ) || ( objParentNode.Tag == null ) ) { return; }

                // ������ ��������� ������������ ������ ��������
                ERP_Budget.Common.CBudgetItem objBudgetItemParent = null;
                if( objParentNode.Tag == null )
                {
                    System.Windows.Forms.MessageBox.Show( this,
                        "�� ������� ���������� ������������ ������ ��������!", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return;
                }
                else
                {
                    objBudgetItemParent = ( ERP_Budget.Common.CBudgetItem )objParentNode.Tag;
                }

                frmDebitArticleEditor objfrmDebitArticleEditor = new frmDebitArticleEditor( this.m_objProfile, this.m_objBudget );
                System.Int32 iArticleChildNum = 0;
                if (objParentNode.ParentNode == null)
                {
                    iArticleChildNum = objParentNode.Nodes.Count;
                }
                else
                {
                    iArticleChildNum = 1;
                    foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in objParentNode.Nodes)
                    {
                        if ( System.Convert.ToString( objNode.GetValue( colDEBITARTICLE_NUM ) ).IndexOf( ".0" ) > 0 ) {continue;}
                        iArticleChildNum++;
                    }
                }
                objfrmDebitArticleEditor.AddDebitArticleChild( objBudgetItemParent, iArticleChildNum, this.m_objBudget.uuidID);
                if( objfrmDebitArticleEditor.DialogResult == DialogResult.OK )
                {
                    // ������� ����� �����������
                    ERP_Budget.Common.CBudgetItem objBudgetItem = objfrmDebitArticleEditor.BudgetItem;

                    // � ����� ��������� �������� �������� ���������� �� ������������ ������
                    // � ������������ ������ �������� �������� ����������
                    // ���� ������������ ������ �� �������� ������� �������� ������, �� �� ����������� ���������
                    objBudgetItem.CopyDecodeListPlan( objBudgetItemParent );
                    objBudgetItemParent.ClearPlanInDecodeList();

                    // ��������� ����
                    DevExpress.XtraTreeList.Nodes.TreeListNode objNode = 
                    treeList.AppendNode( new object[] { objBudgetItem.uuidID, 
                        objBudgetItem.ParentID, objBudgetItem.BudgetItemFullName, 
                        false, false, objBudgetItem.GetBudgetItemDecodeInfo( ERP_Budget.Common.enumMonth.Jan ), 
                        null, null, null, null, null, null, null, null, null, null, null, null }, objParentNode );
                    objNode.Tag = objBudgetItem;

                    SetModified( true );

                }

                objfrmDebitArticleEditor.Dispose();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ ���������� ��������� ��������.\n\n����� ������: " + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        private void barBtnAddChildNode_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            try
            {
                if( treeList.FocusedNode == null ) { return; }
                AddNode( treeList.FocusedNode );
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ ���������� ��������� �������\n" + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        private void mitemAddChild_Click( object sender, EventArgs e )
        {
            try
            {
                if( treeList.FocusedNode == null ) { return; }
                AddNode( treeList.FocusedNode );
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ ���������� ��������� �������\n" + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        /// <summary>
        /// ������� ���� � ������
        /// </summary>
        /// <param name="objNode">��������� ����</param>
        private void DeleteNode( DevExpress.XtraTreeList.Nodes.TreeListNode objNode )
        {
            try
            {
                if( ( objNode == null ) || ( objNode.ParentNode == null ) || ( objNode.Tag == null ) ) { return; }

                // ���������, ����� �� ������� ����
                ERP_Budget.Common.CBudgetItem objBudgetItem = ( ERP_Budget.Common.CBudgetItem )objNode.Tag;
                if( objBudgetItem.IsPossibleChangeBudgetItem( this.m_objProfile, true ) == true )
                {
                    // ���� ������� �����
                    if( objBudgetItem.Remove( this.m_objProfile ) == true )
                    {
                        // �������
                        objNode.ParentNode.Nodes.Remove( objNode );

                        // �������� �������� ����� � ������������ ������
                        CalcRowTotalSum( treeList.FocusedNode );

                        // ������������ ����� ����� ������� �������� �����
                        FillGrandTotalRow();
                    }
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ �������� ����.\n\n����� ������: " + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        /// <summary>
        /// �������� ���� ������
        /// </summary>
        private void DeleteNodeClick()
        {
            try
            {
                if( ( treeList.FocusedNode == null ) || ( treeList.FocusedNode.ParentNode == null ) || 
                    ( treeList.FocusedNode.Tag == null ) ) { return; }

                // ��������� �������������� �� ������ �������� ����
                System.String strQuestion = "������� ��������� " + 
                    ( System.String )treeList.FocusedNode.GetValue( colDEBITARTICLE_NUM ) + 
                    "?\n������ �������� ����� ����������!";
                if( System.Windows.Forms.MessageBox.Show( this, strQuestion, "�������������",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question ) == System.Windows.Forms.DialogResult.No )
                { return; }

                // ������� ����
                treeList.CustomDrawNodeCell -= new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler( treeList_CustomDrawNodeCell );
                DeleteNode( treeList.FocusedNode );
                treeList.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler( treeList_CustomDrawNodeCell );
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ �������� ������\n" + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        private void barBtnDeleteNode_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            try
            {
                DeleteNodeClick();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ �������� ��������� �������\n" + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        private void mitemDelete_Click( object sender, EventArgs e )
        {
            try
            {
                DeleteNodeClick();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ �������� ��������� �������\n" + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        #endregion

        #region �������������� �����
        /// <summary>
        /// ������������� ������� ��� ������� "��-���������"
        /// </summary>
        private void SetDefaultCursor()
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ ��������� �������\n" + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        private void SetDragCursor( DragDropEffects e )
        {
            try
            {
                switch( e )
                {
                    case DragDropEffects.Move:
                    {
                        this.Cursor = Cursors.Default;
                        break;
                    }
                    case DragDropEffects.Copy:
                    {
                        this.Cursor = Cursors.Default;
                        break;
                    }
                    case DragDropEffects.None:
                    {
                        this.Cursor = Cursors.No;
                        break;
                    }
                    default:
                    {
                        this.Cursor = Cursors.Default;
                        break;
                    }
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ 'SetDragCursor'\n" + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        /// <summary>
        /// ���������� ��������������� ����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DevExpress.XtraTreeList.Nodes.TreeListNode GetDragNode( System.Windows.Forms.IDataObject data )
        {
            return data.GetData( typeof( DevExpress.XtraTreeList.Nodes.TreeListNode ) ) as DevExpress.XtraTreeList.Nodes.TreeListNode;
        }
        /// <summary>
        /// ���������� �������������� (��������� �����)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_DragDrop( object sender, DragEventArgs e )
        {
            try
            {
                DevExpress.XtraTreeList.TreeListHitInfo hi = treeList.CalcHitInfo( treeList.PointToClient( new Point( e.X, e.Y ) ) );
                // ����, ������� �������������
                DevExpress.XtraTreeList.Nodes.TreeListNode draggedNode = 
                    ( DevExpress.XtraTreeList.Nodes.TreeListNode )e.Data.GetData( typeof( DevExpress.XtraTreeList.Nodes.TreeListNode ) );
                if( ( draggedNode != null ) && ( draggedNode.ParentNode != null ) && ( ( e.KeyState & 4 ) != 4 ) )
                {
                    //����, ��� ������� ��������� �����
                    DevExpress.XtraTreeList.Nodes.TreeListNode node = hi.Node;
                    if (node != draggedNode)
                    {
                        // ���� �� �������� �������� ��������������� ���� 
                        this.draggedNodeParent = draggedNode.ParentNode;
                        if ((node != null) && (node.ParentNode != null))
                        {
                            // �������� ��������� �� �������� � ������������� (����������� ����-����) ����
                            System.Guid uuidBudgetItemID = ((ERP_Budget.Common.CBudgetItem)node.Tag).uuidID;
                            ((ERP_Budget.Common.CBudgetItem)draggedNode.Tag).ParentID = uuidBudgetItemID;
                            ((ERP_Budget.Common.CBudgetItem)draggedNode.Tag).ReadOnly = true;
                            draggedNode.SetValue(colDEBITARTICLE_PARENT_ID, uuidBudgetItemID);

                            // ������ �������, ��� ��������� ���������
                            SetModified(true);
                        }
                    }
                }
                SetDefaultCursor();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ treeList_DragDrop\n" + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        /// <summary>
        /// ������ ��������������� ��� treeList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_DragOver( object sender, DragEventArgs e )
        {
            try
            {
                DevExpress.XtraTreeList.TreeListHitInfo hi = treeList.CalcHitInfo( treeList.PointToClient( new Point( e.X, e.Y ) ) );
                // ���� ��� ��������
                DevExpress.XtraTreeList.Nodes.TreeListNode node = hi.Node;
                // ��������������� ����
                DevExpress.XtraTreeList.Nodes.TreeListNode draggedNode = GetDragNode( e.Data );

                if( ( node == null ) || ( node.ParentNode == null ) || ( draggedNode == null ) ||
                    (draggedNode.ParentNode == null) || (draggedNode.ParentNode.Tag == null) || (draggedNode.Tag == null) || (node == draggedNode))
                {
                    e.Effect= DragDropEffects.None;
                }
                else
                {
                    // �������� ������������� ��������� ��-��������� - � ��� �������� ��������� � ��������� ������ � � ������ ��������� ".0"
                    ERP_Budget.Common.CBudgetItem objdraggedNodeTag = (ERP_Budget.Common.CBudgetItem)draggedNode.Tag;
                    ERP_Budget.Common.CBudgetItem objdraggedNodeParentTag = (ERP_Budget.Common.CBudgetItem)draggedNode.ParentNode.Tag;
                    if ((objdraggedNodeTag.Name == objdraggedNodeParentTag.Name) &&
                        ((objdraggedNodeParentTag.BudgetItemNum + ".0") == objdraggedNodeTag.BudgetItemNum))
                    {
                        e.Effect = DragDropEffects.None;
                    }
                    else
                    {
                        // �� ����� ��������� � ��� �������� ���� ���������
                        if ((node != null) && (node.Tag != null) && (node.ParentNode != null))
                        {
                            // � �������� ������������� ���� �� ����� ������������ ������ � ������
                            ERP_Budget.Common.CBudgetItem objBudgetItemnode = GetParentBudgetItem(node);
                            ERP_Budget.Common.CBudgetItem objBudgetItemdraggedNode = GetParentBudgetItem(draggedNode);
                            if (objBudgetItemnode.uuidID.CompareTo(objBudgetItemdraggedNode.uuidID) == 0)
                            {
                                e.Effect = DragDropEffects.Move;
                            }
                            else
                            {
                                e.Effect = DragDropEffects.None;
                            }
                        }
                    }

                }

                SetDragCursor( e.Effect );
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ 'treeList_DragOver'\n" + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        /// <summary>
        /// ��������������� ������ ����� �� ������� treeList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_DragLeave( object sender, EventArgs e )
        {
            try
            {
                SetDefaultCursor();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ 'treeList_DragLeave'\n" + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        /// <summary>
        /// ���� ���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_AfterDragNode( object sender, DevExpress.XtraTreeList.NodeEventArgs e )
        {
            try
            {
                if( e.Node == null ) { return; }
                // �������� ������� ������ � ������������� ����
                RecalcDebitArticleNumber( e.Node.ParentNode );
                // � ����������� ������������� ���� ������� �������� ����������
                ( ( ERP_Budget.Common.CBudgetItem )e.Node.ParentNode.Tag ).ClearPlanInDecodeList();

                if( this.draggedNodeParent == null ) { return; }
                // �������� ������� ������ � �����, ������ ������������� ����
                RecalcDebitArticleNumber( this.draggedNodeParent );
                this.draggedNodeParent = null;
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ \"treeList_AfterDragNode\"\n\n����� ������: " + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        private void treeList_GiveFeedback( object sender, GiveFeedbackEventArgs e )
        {
            try
            {
                e.UseDefaultCursors = false;
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ 'treeList_GiveFeedback'\n" + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        #endregion

        #region ������ ���������� �������

        /// <summary>
        /// ����������� ���� ������ �����
        /// </summary>
        private void OnLeft()
        {
            try
            {
                // ������� ����
                DevExpress.XtraTreeList.Nodes.TreeListNode objFocusedNode = treeList.FocusedNode;
                if( ( objFocusedNode == null ) || ( objFocusedNode.Tag == null ) ) { return; }
                // ������������ ����
                if( ( objFocusedNode.ParentNode == null ) || ( objFocusedNode.ParentNode.ParentNode == null ) || 
                    ( ( objFocusedNode.ParentNode.ParentNode.Tag == null ) ) ) { return; }
                System.Guid uuidBudgetItemID = ( ( ERP_Budget.Common.CBudgetItem )objFocusedNode.ParentNode.ParentNode.Tag ).uuidID;

                // ���������� ����
                treeList.MoveNode( objFocusedNode, objFocusedNode.ParentNode.ParentNode );
                ( ( ERP_Budget.Common.CBudgetItem )objFocusedNode.Tag ).BudgetItemID = treeList.GetNodeIndex( objFocusedNode );
                ( ( ERP_Budget.Common.CBudgetItem )objFocusedNode.Tag ).ReadOnly = true;
                objFocusedNode.SetValue( colREADONLY, true );

                // �������� ��������� �� �������� � ���������������� ����
                ( ( ERP_Budget.Common.CBudgetItem )objFocusedNode.Tag ).ParentID = uuidBudgetItemID;
                objFocusedNode.SetValue( colDEBITARTICLE_PARENT_ID, uuidBudgetItemID );

                // ��������� ����������� ������ ���������
                RecalcDebitArticleNumber( objFocusedNode.ParentNode );

                // ������ ������� �� ���������� � ������
                SetModified( true );

            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "OnLeft\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        private void btnLeft_Click( object sender, EventArgs e )
        {
            try
            {
                OnLeft();
                OnSelectItem();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ ��� ����������� ���� �����\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        /// <summary>
        /// ����������� ���� ������ ���� ������
        /// </summary>
        private void OnRight()
        {
            try
            {
                // ������� ����
                DevExpress.XtraTreeList.Nodes.TreeListNode objFocusedNode = treeList.FocusedNode;
                if( ( objFocusedNode == null ) || ( objFocusedNode.Tag == null ) ) { return; }
                // ���������� ����
                DevExpress.XtraTreeList.Nodes.TreeListNode objPrevFocusedNode = objFocusedNode.PrevNode;
                // ���������� ���� ������ ����� ��������  � �� ������ ���
                if( ( objPrevFocusedNode == null ) || ( objFocusedNode.ParentNode == null ) || ( objPrevFocusedNode.Tag == null ) ) { return; }
                System.Guid uuidPrevNodeBudgetItemID = ( ( ERP_Budget.Common.CBudgetItem )objPrevFocusedNode.Tag ).uuidID;

                // ���������� ����
                treeList.MoveNode( objFocusedNode, objPrevFocusedNode );
                ( ( ERP_Budget.Common.CBudgetItem )objFocusedNode.Tag ).BudgetItemID = treeList.GetNodeIndex( objFocusedNode );
                ( ( ERP_Budget.Common.CBudgetItem )objFocusedNode.Tag ).ReadOnly = true;
                objFocusedNode.SetValue( colREADONLY, true );

                // �������� ��������� �� �������� � ���������������� ����
                ( ( ERP_Budget.Common.CBudgetItem )objFocusedNode.Tag ).ParentID = uuidPrevNodeBudgetItemID;
                objFocusedNode.SetValue( colDEBITARTICLE_PARENT_ID, uuidPrevNodeBudgetItemID );

                // ��������� ����������� ������ ���������
                RecalcDebitArticleNumber( objFocusedNode.ParentNode );

                // ������ ������� �� ���������� � ������
                SetModified( true );
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "OnRight\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        private void btnRight_Click( object sender, EventArgs e )
        {
            try
            {
                OnRight();
                OnSelectItem();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ ��� ����������� ���� ������\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        /// <summary>
        /// ����������� ���� ������ ���� ����
        /// </summary>
        private void OnDown()
        {
            try
            {
                // ���������� ����
                DevExpress.XtraTreeList.Nodes.TreeListNode objFocusedNode = treeList.FocusedNode;
                if( ( objFocusedNode == null ) || ( objFocusedNode.Tag == null ) || ( objFocusedNode.ParentNode == null ) ) { return; }
                // ��������� ����
                DevExpress.XtraTreeList.Nodes.TreeListNode objNexFocusedNode = objFocusedNode.NextNode;
                if( ( objNexFocusedNode == null ) || ( objNexFocusedNode.Tag == null ) ) { return; }
                // ���������� ����
                System.Int32 iFocusedNodeIndx = treeList.GetNodeIndex( objFocusedNode );
                System.Int32 iNexFocusedNodeIndx = treeList.GetNodeIndex( objNexFocusedNode );
                treeList.SetNodeIndex( objFocusedNode, iNexFocusedNodeIndx );

                ( ( ERP_Budget.Common.CBudgetItem )objFocusedNode.Tag ).BudgetItemID = iNexFocusedNodeIndx;
                treeList.SetNodeIndex( objNexFocusedNode, iFocusedNodeIndx );
                ( ( ERP_Budget.Common.CBudgetItem )objNexFocusedNode.Tag ).BudgetItemID = iFocusedNodeIndx;

                objFocusedNode.SetValue( colREADONLY, true );
                objNexFocusedNode.SetValue( colREADONLY, true );
                ( ( ERP_Budget.Common.CBudgetItem )objFocusedNode.Tag ).ReadOnly = true;
                ( ( ERP_Budget.Common.CBudgetItem )objNexFocusedNode.Tag ).ReadOnly = true;

                // ��������� ����������� ������ ���������
                RecalcDebitArticleNumber( objNexFocusedNode );
                RecalcDebitArticleNumber( objFocusedNode );

                // ������ ������� �� ���������� � ������
                SetModified( true );
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "OnDown\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        private void btnDown_Click( object sender, EventArgs e )
        {
            try
            {
                OnDown();
                OnSelectItem();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ ��� ����������� ���� ����\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        /// <summary>
        /// ����������� ���� ������ ���� �����
        /// </summary>
        private void OnUp()
        {
            try
            {
                // ���������� ����
                DevExpress.XtraTreeList.Nodes.TreeListNode objFocusedNode = treeList.FocusedNode;
                if( ( objFocusedNode == null ) || ( objFocusedNode.ParentNode == null ) || ( objFocusedNode.Tag == null ) ) { return; }
                // ���������� ����
                DevExpress.XtraTreeList.Nodes.TreeListNode objPrevFocusedNode = objFocusedNode.PrevNode;
                if( ( objPrevFocusedNode == null ) || ( objPrevFocusedNode.Tag == null ) ) { return; }
                // ���������� ����
                System.Int32 iFocusedNodeIndx = treeList.GetNodeIndex( objFocusedNode );
                System.Int32 iPrevFocusedNodeIndx = treeList.GetNodeIndex( objPrevFocusedNode );

                treeList.SetNodeIndex( objFocusedNode, iPrevFocusedNodeIndx );
                ( ( ERP_Budget.Common.CBudgetItem )objFocusedNode.Tag ).BudgetItemID = iPrevFocusedNodeIndx;

                treeList.SetNodeIndex( objPrevFocusedNode, iFocusedNodeIndx );
                ( ( ERP_Budget.Common.CBudgetItem )objPrevFocusedNode.Tag ).BudgetItemID = iFocusedNodeIndx;

                objFocusedNode.SetValue( colREADONLY, true );
                objPrevFocusedNode.SetValue( colREADONLY, true );
                ( ( ERP_Budget.Common.CBudgetItem )objFocusedNode.Tag ).ReadOnly = true;
                ( ( ERP_Budget.Common.CBudgetItem )objPrevFocusedNode.Tag ).ReadOnly = true;

                // ��������� ����������� ������ ���������
                RecalcDebitArticleNumber( objFocusedNode );
                RecalcDebitArticleNumber( objPrevFocusedNode );

                // ������ ������� �� ���������� � ������
                SetModified( true );
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "OnUp\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        private void btnUp_Click( object sender, EventArgs e )
        {
            try
            {
                OnUp();
                OnSelectItem();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ ��� ����������� ���� �����\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }
        /// <summary>
        /// ��������� ���� ������ ����
        /// </summary>
        private void OnSelectItem()
        {
            try
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode objFocusedNode = treeList.FocusedNode;
                if( ( objFocusedNode == null ) || ( objFocusedNode.ParentNode == null ) || ( m_objBudget.IsAccept ) )
                {
                    // ������ �� �������� ��� ������������ ������ ��� ������ ���������
                    btnLeft.Enabled = false;
                    btnRight.Enabled = false;
                    btnUp.Enabled = false;
                    btnDown.Enabled = false;
                    btnDrop.Enabled = false;
                    // ToolBar
                    barBtnAddChildNode.Enabled = ( ( objFocusedNode != null ) && ( objFocusedNode.ParentNode == null ) );
                    barBtnDeleteNode.Enabled = false;
                }
                else
                {
                    ERP_Budget.Common.CBudgetItem objBudgetItem = ( ERP_Budget.Common.CBudgetItem )objFocusedNode.Tag;
                    if( objBudgetItem.DebitArticleID.CompareTo( System.Guid.Empty ) == 0 )
                    {
                        if ((objFocusedNode.PrevNode != null) &&
                            (((ERP_Budget.Common.CBudgetItem)objFocusedNode.PrevNode.Tag)).DebitArticleID.CompareTo(System.Guid.Empty) == 0)
                        {
                            // ���� �� ������� ����, ��������� ����� �� ���������� ��-���������, �� ��� ������ �� ��������
                            btnLeft.Enabled = false;
                            btnRight.Enabled = false;
                            btnUp.Enabled = false;
                            btnDown.Enabled = false;
                            btnDrop.Enabled = false;

                            btnDrop.Enabled = (this.m_bIsTreeModified == false);
                            barBtnDeleteNode.Enabled = btnDrop.Enabled;
                            barBtnAddChildNode.Enabled = (this.m_bIsTreeModified == false);

                        }
                        else
                        {
                            if (objFocusedNode.ParentNode != null)
                            {
                                ERP_Budget.Common.CBudgetItem objBudgetItemParent = (ERP_Budget.Common.CBudgetItem)objFocusedNode.ParentNode.Tag;
                                btnLeft.Enabled = (objBudgetItemParent.DebitArticleID.CompareTo(System.Guid.Empty) == 0);
                                btnRight.Enabled = (objFocusedNode.PrevNode != null);

                                btnUp.Enabled = (objFocusedNode.ParentNode != null) && (objFocusedNode.PrevNode != null);
                                btnDown.Enabled = (objFocusedNode.ParentNode != null) && (objFocusedNode.NextNode != null);

                                btnDrop.Enabled = (this.m_bIsTreeModified == false);
                                barBtnDeleteNode.Enabled = btnDrop.Enabled;
                                barBtnAddChildNode.Enabled = (this.m_bIsTreeModified == false);
                            }
                        }
                    }
                    else
                    {
                        // ��� ������, ��������� �� �����������, � ��� ������ ��������� �����������
                        btnLeft.Enabled = false;
                        btnRight.Enabled = false;
                        btnUp.Enabled = false;
                        btnDown.Enabled = false;
                        btnDrop.Enabled = false;
                        // ToolBar
                        barBtnAddChildNode.Enabled = true;
                        barBtnDeleteNode.Enabled = false;

                    }
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "OnSelectItem\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        private void treeList_KeyDown( object sender, KeyEventArgs e )
        {
            try
            {
                if( treeList.Nodes.Count == 0 ) { return; }
                if( treeList.FocusedNode == null ) { return; }
                if( e.Control )
                {
                    switch( e.KeyCode )
                    {
                        // Ctrl - �����
                        case System.Windows.Forms.Keys.Left:
                        if( btnLeft.Enabled )
                        {
                            OnLeft();
                            OnSelectItem();
                        }
                        break;
                        // Ctrl - ������
                        case System.Windows.Forms.Keys.Right:
                        if( btnRight.Enabled )
                        {
                            OnRight();
                            OnSelectItem();
                        }
                        break;
                        // Ctrl - �����
                        case System.Windows.Forms.Keys.Up:
                        if( btnUp.Enabled )
                        {
                            OnUp();
                            OnSelectItem();
                        }
                        break;
                        // Ctrl - ����
                        case System.Windows.Forms.Keys.Down:
                        if( btnDown.Enabled )
                        {
                            OnDown();
                            OnSelectItem();
                        }
                        break;
                        // Ctrl - Delete
                        case System.Windows.Forms.Keys.Delete:
                        if( btnDrop.Enabled )
                        {
                            // ��������� �������������� �� ������ �������� ����
                            if( System.Windows.Forms.MessageBox.Show( this, "������� ���������?", "�������������",
                                System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question ) == System.Windows.Forms.DialogResult.No )
                            { break; }
                            // ������� ����
                            DeleteNode( treeList.FocusedNode );
                            OnSelectItem();
                        }
                        break;

                        default:
                        break;
                    }
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ ��������� ������� �������\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        #endregion

        #region ����������, ������ ��������� � ��
        /// <summary>
        /// ������ ��������� ���������
        /// </summary>
        private void CancelChanges()
        {
            try
            {
                if( this.m_bIsTreeModified )
                {
                    RefreshBudgetItemsList();
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ ������ ��������� ���������.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        private void btnCancel_Click( object sender, EventArgs e )
        {
            try
            {
                if( System.Windows.Forms.MessageBox.Show( this,
                   "�������� ��� ��������� ���������?", "�������������",
                   System.Windows.Forms.MessageBoxButtons.YesNoCancel,
                   System.Windows.Forms.MessageBoxIcon.Question ) == System.Windows.Forms.DialogResult.Yes )
                {
                    CancelChanges();
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                   "������ ������ ��������� ���������.\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        /// <summary>
        /// ��������� ��������� � ������ �������� ������������� � ��
        /// </summary>
        /// <returns></returns>
        private System.Boolean SaveChangesToDB()
        {
            System.Boolean bRes = false;
            try
            {
                if( treeList.Nodes.Count == 0 )
                {
                    System.Windows.Forms.MessageBox.Show( this, "������ ������ ����!", "��������",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRes;
                }

                bRes = this.m_objBudget.SaveBudgetItemList( treeList, this.m_objProfile );

                if( bRes == true )
                {
                    // ������ ������� �� ���������� � ������
                    SetModified( false );
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ ���������� ��������� � ������ ������ �������.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return bRes;
        }

        private void btnSave_Click( object sender, EventArgs e )
        {
            try
            {
                if( SaveChangesToDB() == false )
                {
                    System.Windows.Forms.MessageBox.Show( this, "�� ������� ��������� ��������� � ���� ������.", "������",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ ���������� ���������.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        #endregion

        #region �������� ������

        private void SetCellValueFromCalcEdit( decimal CalcEditValue )
        {
            try
            {
                if (CalcEditValue < 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("����� ����� �� ������ ��� ������ ����.", "Ghtleght;ltybt",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }
                // ����������� ����������� �� ������ �����
                ERP_Budget.Common.CBudgetItemDecode objBudgetItemDecode =
                    ((ERP_Budget.Common.CBudgetItem)(treeList.FocusedNode.Tag)).GetBudgetItemDecode(GetMonthEnum(treeList.FocusedColumn));

                if (objBudgetItemDecode == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("�� ������� �������� �������� � ������.\n�� ������� ����� ����������� �� " +
                        treeList.FocusedColumn.Caption + " �����.", "������",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                else
                {
                    objBudgetItemDecode.MoneyPlan = Convert.ToDouble( CalcEditValue );
                    if (objBudgetItemDecode.Currency == null)
                    {
                        if (m_objLastSelectedCurrency == null)
                        {
                            objBudgetItemDecode.Currency = new ERP_Budget.Common.CCurrency(m_objBudget.Currency.uuidID,
                                m_objBudget.Currency.CurrencyCode, m_objBudget.Currency.Name); 
                        }
                        else
                        {
                            objBudgetItemDecode.Currency = new ERP_Budget.Common.CCurrency( m_objLastSelectedCurrency.uuidID, 
                                m_objLastSelectedCurrency.CurrencyCode, m_objLastSelectedCurrency.Name );
                        }
                    }
                    treeList.FocusedNode.SetValue(treeList.FocusedColumn, objBudgetItemDecode.MoneyPlan);
                    // ������ ������� �� ����������
                    SetModified(true);
                    m_bChangeCellValue = true;

                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show ("�� ������� �������� �������� � ������.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void repItemCalcEdit_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                SetCellValueFromCalcEdit(((DevExpress.XtraEditors.CalcEdit)sender).Value);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("�� ������� �������� �������� � ������.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

        }
        private void repItemCalcEdit_KeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.XtraEditors.BaseEdit editor = sender as DevExpress.XtraEditors.BaseEdit;
            try
            {
                if (treeList.Nodes.Count == 0) { return; }
                if (treeList.FocusedNode == null) { return; }
                if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.F3))
                {
                    ERP_Budget.Common.CBudgetItemDecode objItemDecode =
                        ((ERP_Budget.Common.CBudgetItem)(treeList.FocusedNode.Tag)).GetBudgetItemDecode(GetMonthEnum(treeList.FocusedColumn));
                    if (e.KeyCode == Keys.Delete)
                    {
                        objItemDecode.MoneyPlan = 0;
                        objItemDecode.Description = "";
                        objItemDecode.Currency = null;
                        ((DevExpress.XtraEditors.CalcEdit)sender).Value = 0;
                        ((DevExpress.XtraEditors.CalcEdit)sender).Text = "";
                    }
                    else
                    {
                        if (e.KeyCode == Keys.F3)
                        {
                            System.Int32 iMonth = System.Convert.ToInt32(GetMonthEnum(treeList.FocusedColumn));
                            if ((m_objBudget.IsAccept == false) && (objItemDecode != null) && (objItemDecode.MoneyPlan > 0) && (iMonth < 12))
                            {
                                AutoFill();
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    m_bChangeCellValue = true;
                    SetModified(true);
                }
            }
            catch (System.InvalidCastException ex)
            {
                editor.EditValue = "";
                System.Windows.Forms.MessageBox.Show(this, "������ ��������� ������� �������.\n\n����� ������: " + ex.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;

        }
        private void treeList_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            try
            {
                if ((m_objBudget.IsAccept == false) && (m_bChangeCellValue == true))
                {
                    DevExpress.XtraTreeList.Columns.TreeListColumn objColumn = e.Column;
                    // �������� �������� ����� � ������������ ������
                    CalcRowTotalSum(treeList.FocusedNode);

                    // ������������ ����� ����� ������� �������� �����
                    FillGrandTotalRow();

                    m_bChangeCellValue = false;
                    e.Node.TreeList.FocusedColumn = objColumn;
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("�� ������� �������� �������� � ������.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void repItemCalcEdit_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_objfrmCellProperties == null) 
                { 
                    m_objfrmCellProperties = new frmCellProperties(m_objBudget.Currency);
                    m_objfrmCellProperties.LoadCurrencyList(m_objCurrencyList);
                }
                // ����������� ����������� �� ������ �����
                ERP_Budget.Common.CBudgetItemDecode objBudgetItemDecode =
                    ((ERP_Budget.Common.CBudgetItem)(treeList.FocusedNode.Tag)).GetBudgetItemDecode(GetMonthEnum(treeList.FocusedColumn));

                m_objfrmCellProperties.ShowCellProperties((System.String)treeList.FocusedNode.GetValue(colDEBITARTICLE_NUM),
                    objBudgetItemDecode, m_objLastSelectedCurrency);
                if (m_objfrmCellProperties.DialogResult == DialogResult.OK)
                {
                    // �������� � ������ ��������
                    treeList.FocusedNode.SetValue(treeList.FocusedColumn, objBudgetItemDecode.MoneyPlan);
                    m_objLastSelectedCurrency = m_objfrmCellProperties.LastSelectedCurrency;
                    SetModified(true);
                    // �������� �������� ����� � ������������ ������
                    CalcRowTotalSum(treeList.FocusedNode);

                    // ������������ ����� ����� ������� �������� �����
                    FillGrandTotalRow();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������ ��������� �������� ������� ������� ����.\nrepItemCalcEdit_EditValueChanging\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// �������������� ����� ������ �� ������ �������� ������
        /// </summary>
        private void AutoFill()
        {
            try
            {
                if ((treeList.Nodes.Count == 0) || (treeList.FocusedNode == null)) { return; }

                // ���������� ������� 
                DevExpress.XtraTreeList.Columns.TreeListColumn column = treeList.FocusedColumn;
                if ((column == null) || (isMonthColumn(column) == false) || (column == colDECEMBER)) { return; }

                // ������� ������� � �������
                ERP_Budget.Common.CBudgetItemDecode objBudgetItemDecode =
                    ((ERP_Budget.Common.CBudgetItem)(treeList.FocusedNode.Tag)).GetBudgetItemDecode(GetMonthEnum(column));

                if (objBudgetItemDecode == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("�� ������� ����� ����������� �� " +
                        column.Caption + " �����.", "������",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }

                // � ����������� ������ ���� ������� ������ � �����
                if ((objBudgetItemDecode.Currency == null) || (objBudgetItemDecode.MoneyPlan == 0)) { return; }

                // �������� ������ �� ������ �� ��������� � � ������ ������� ����������� ��������
                ERP_Budget.Common.CBudgetItemDecode objItemDecode = null;
                foreach (DevExpress.XtraTreeList.Columns.TreeListColumn clmn in treeList.Columns)
                {
                    if (clmn.VisibleIndex <= column.VisibleIndex) { continue; }
                    if (isMonthColumn(clmn) == false) { continue; }

                    // ����������� �������
                    objItemDecode = ((ERP_Budget.Common.CBudgetItem)(treeList.FocusedNode.Tag)).GetBudgetItemDecode(GetMonthEnum(clmn));
                    if (objItemDecode.MoneyPlan == 0)
                    {
                        objItemDecode.MoneyPlan = objBudgetItemDecode.MoneyPlan;
                        objItemDecode.Description = objBudgetItemDecode.Description;
                        objItemDecode.Currency = new ERP_Budget.Common.CCurrency(objBudgetItemDecode.Currency.uuidID,
                            objBudgetItemDecode.Currency.CurrencyCode, objBudgetItemDecode.Currency.Name);
                    }
                    else
                    {
                        break;
                    }
                }
                // �������� �������� ����� � ������������ ������
                CalcRowTotalSum(treeList.FocusedNode);

                // ������������ ����� ����� ������� �������� �����
                FillGrandTotalRow();

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������ �������������� �����.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                treeList.Refresh();
            }
            return;
        }
        private void mitemAutoFill_Click(object sender, EventArgs e)
        {
            try
            {
                AutoFill();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������ ��������� �������� ������ �����.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        #endregion

        #region TreeList & TreeNode
        /// <summary>
        /// ���������� �������, ������������ ����� ������ ������ �� ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_MouseClick( object sender, MouseEventArgs e )
        {
            try
            {
                if( e.Button == MouseButtons.Right )
                {
                    // ��������� ����������, ��� �� � ��� ��� ������
                    DevExpress.XtraTreeList.TreeListHitInfo hi = treeList.CalcHitInfo( new Point( e.X, e.Y ) );
                    if( ( hi == null ) || ( hi.Node == null ) ) { return; }

                    // �������� ����
                    hi.Node.TreeList.FocusedNode = hi.Node;

                    // ��������� ����������� ����
                    mitemAddChild.Enabled = barBtnAddChildNode.Enabled;
                    mitemDelete.Enabled = btnDrop.Enabled;
                    mitemAddBudgetItem.Enabled = barBtnAddBudgetItem.Enabled;

                    contextMenuStrip.Show( treeList, new Point( e.X, e.Y ) );
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ 'treeList_MouseClick'\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }
        /// <summary>
        /// ���������� �������, ������������ ����� ���������� ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_BeforeFocusNode( object sender, DevExpress.XtraTreeList.BeforeFocusNodeEventArgs e )
        {
            try
            {
                // ����� ��� ��� �������� ����� ���� ��������, ��� �� ��������� � ������� ���������� ����
                if( treeList.Nodes.Count == 0 ) { return; }
                if( e.OldNode == null ) { return; }

                //e.CanFocus = bIsNodeValuesValidation( e.OldNode, true );
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ treeList_AfterFocusNode\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }
        /// <summary>
        /// ���������� �������, ������������ ����� ��������� ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_AfterFocusNode( object sender, DevExpress.XtraTreeList.NodeEventArgs e )
        {
            try
            {
                if( treeList.Nodes.Count == 0 ) { return; }
                //if( treeList.FocusedNode.GetValue( colDEBITARTICLE_MULTIBUDGETDEP ) == null ) { return; }
                OnSelectItem();
                // ���������, ��� �� ���� ������ � ��������/��������� ����������� ������������� � ��������
                //DevExpress.XtraTreeList.Nodes.TreeListNode objNode = treeList.FocusedNode;
                EnableDisableColumn( e.Node );
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ treeList_AfterFocusNode\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }
        /// <summary>
        /// ��������/��������� ����������� �������������� � ��������
        /// </summary>
        /// <param name="objNode">����</param>
        private void EnableDisableColumn( DevExpress.XtraTreeList.Nodes.TreeListNode objNode )
        {
            try
            {
                // ����� ����� ������� ������ � ����� ������� ������
                System.Boolean bHasChildren = objNode.HasChildren;
                System.Boolean bMultiBudgetDep = ( System.Boolean )objNode.GetValue( colDEBITARTICLE_MULTIBUDGETDEP );
                // ���� ������� ������/���������, ����������� ���������� ��������� ��������������, �� ��������� ������������� ����� � ������������ ������/���������
                //colDEBITARTICLE_NUM.OptionsColumn.AllowEdit = !( bMultiBudgetDep || m_objBudget.IsAccept );
                //colDEBITARTICLE_NUM.OptionsColumn.ReadOnly = bMultiBudgetDep || m_objBudget.IsAccept;

                colJANUARY.OptionsColumn.AllowEdit = !( bHasChildren || m_objBudget.IsAccept );
                colJANUARY.OptionsColumn.ReadOnly = ( bHasChildren || m_objBudget.IsAccept );

                colFEBRUARY.OptionsColumn.AllowEdit = !( bHasChildren || m_objBudget.IsAccept );
                colFEBRUARY.OptionsColumn.ReadOnly = ( bHasChildren || m_objBudget.IsAccept );

                colMARCH.OptionsColumn.AllowEdit = !( bHasChildren || m_objBudget.IsAccept );
                colMARCH.OptionsColumn.ReadOnly = ( bHasChildren || m_objBudget.IsAccept );

                colAPRIL.OptionsColumn.AllowEdit = !( bHasChildren || m_objBudget.IsAccept );
                colAPRIL.OptionsColumn.ReadOnly = ( bHasChildren || m_objBudget.IsAccept );

                colMAY.OptionsColumn.AllowEdit = !( bHasChildren || m_objBudget.IsAccept );
                colMAY.OptionsColumn.ReadOnly = ( bHasChildren || m_objBudget.IsAccept );

                colJUNE.OptionsColumn.AllowEdit = !( bHasChildren || m_objBudget.IsAccept );
                colJUNE.OptionsColumn.ReadOnly = ( bHasChildren || m_objBudget.IsAccept );

                colJULY.OptionsColumn.AllowEdit = !( bHasChildren || m_objBudget.IsAccept );
                colJULY.OptionsColumn.ReadOnly = ( bHasChildren || m_objBudget.IsAccept );

                colAUGUST.OptionsColumn.AllowEdit = !( bHasChildren || m_objBudget.IsAccept );
                colAUGUST.OptionsColumn.ReadOnly = ( bHasChildren || m_objBudget.IsAccept );

                colSEPTEMBER.OptionsColumn.AllowEdit = !( bHasChildren || m_objBudget.IsAccept );
                colSEPTEMBER.OptionsColumn.ReadOnly = ( bHasChildren || m_objBudget.IsAccept );

                colOCTEMBER.OptionsColumn.AllowEdit = !( bHasChildren || m_objBudget.IsAccept );
                colOCTEMBER.OptionsColumn.ReadOnly = ( bHasChildren || m_objBudget.IsAccept );

                colNOVEMBER.OptionsColumn.AllowEdit = !( bHasChildren || m_objBudget.IsAccept );
                colNOVEMBER.OptionsColumn.ReadOnly = ( bHasChildren || m_objBudget.IsAccept );

                colDECEMBER.OptionsColumn.AllowEdit = !( bHasChildren || m_objBudget.IsAccept );
                colDECEMBER.OptionsColumn.ReadOnly = ( bHasChildren || m_objBudget.IsAccept );

                this.repItemPopupContainerEdit.Buttons[ 0 ].Visible = !( bHasChildren || m_objBudget.IsAccept );
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ EnableDisableMonthColumn\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }
        /// <summary>
        /// ������������ ���������� �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_CustomDrawNodeCell( object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e )
        {
            try
            {
                if ((e.Node == null) || (e.Column == null) || (e.Node.Tag == null) ) { return; }
                //if ((e.Node == null) || (e.Column == null) || (e.Node.Tag == null) || ((e.Node.ParentNode != null) && (e.Node.ParentNode.ParentNode == null) && (e.Node.HasChildren == true))) { return; }
                //if( e.Node.GetValue( colDEBITARTICLE_MULTIBUDGETDEP ) == null ) { return; }
                //if( e.Node.GetValue( colDEBITARTICLE_MULTIBUDGETDEP ) == System.DBNull.Value ) { return; }
                ERP_Budget.Common.CBudgetItem objBudgetItem = ( ERP_Budget.Common.CBudgetItem )e.Node.Tag;
                ERP_Budget.Common.enumMonth objMonth = GetMonthEnum( e.Column );

                System.Drawing.Brush brushBckgrnd = null; // ���� ����
                System.Drawing.Brush brushFont = null; // ���� ������ � ������
                System.String strText = ""; // ����� � ������
                System.Drawing.Color litegrayColor = Color.FromArgb( 255, 250, 250, 250 );
                System.Drawing.Color yelowColor = Color.FromArgb( 255, 255, 250 );
                System.String strCurrencyBudget = this.m_objBudget.Currency.CurrencyCode;

                System.Boolean bNodeFocused = ( e.Node == treeList.FocusedNode );
                System.Boolean bColumnFocused = ( e.Column == treeList.FocusedColumn );
                System.Boolean bHasChildren = e.Node.HasChildren;
                System.Boolean bMultiBudgetDep = ( System.Boolean )e.Node.GetValue( colDEBITARTICLE_MULTIBUDGETDEP );
                System.Boolean bIsMoneyColumn = ( isMonthColumn( e.Column ) || ( e.Column == colSummary ) );
                System.Boolean bSetDscrpn = false;

                // ������������� �������� ����� ��� ������
                System.Boolean bNeedPaintItogBorder = ( isMonthColumn( e.Column ) && ( bNodeFocused == false ) && 
                    ( e.Node.HasChildren == true ) && ( e.Node.ParentNode == null ) );
                // ���� ����� ��� ������
                System.Drawing.Color ColorItogBorder = ( bNodeFocused ) ? System.Drawing.Color.White : System.Drawing.Color.Silver;
                System.Drawing.Brush brushItogBorder = new System.Drawing.SolidBrush( ColorItogBorder );
                // ���� ����
                if( bIsMoneyColumn )
                {
                    if( bNodeFocused && ( bColumnFocused == false ) )
                    {
                        // ���� � ������, � ������� ���
                        brushBckgrnd = e.Appearance.GetBackBrush( e.Cache );
                    }
                    if( bNodeFocused && bColumnFocused )
                    {
                        // ���� � ������� � ������
                        if( m_objBudget.IsAccept )
                        {
                            brushBckgrnd = e.Appearance.GetBorderBrush( e.Cache );
                        }
                        else
                        {
                            if( bMultiBudgetDep )
                            {
                                brushBckgrnd = ( bHasChildren ) ? e.Appearance.GetBorderBrush( e.Cache ) : System.Drawing.Brushes.White;
                            }
                            else
                            {
                                brushBckgrnd = ( bHasChildren ) ? e.Appearance.GetBorderBrush( e.Cache ) : System.Drawing.Brushes.White;
                            }
                        }
                    }
                    if( ( bNodeFocused == false ) && ( bColumnFocused == false ) )
                    {
                        // ���� � ������� �� � ������
                        brushBckgrnd = ( bHasChildren ) ? new System.Drawing.SolidBrush( litegrayColor ) : new System.Drawing.SolidBrush( yelowColor );
                    }
                    if( ( bNodeFocused == false ) && ( bColumnFocused ) )
                    {
                        // ���� �� � ������, � ������� � ������
                        brushBckgrnd = ( bHasChildren ) ? new System.Drawing.SolidBrush( litegrayColor ) : new System.Drawing.SolidBrush( yelowColor );
                    }
                }
                else
                {
                    if( bNodeFocused )
                    {
                        if( bColumnFocused )
                        {
                            // ���� � ������� � ������
                            if( m_objBudget.IsAccept )
                            {
                                brushBckgrnd = e.Appearance.GetBorderBrush( e.Cache );
                            }
                            else
                            {
                                brushBckgrnd = ( bMultiBudgetDep ) ?  e.Appearance.GetBorderBrush( e.Cache ) : System.Drawing.Brushes.White;
                            }
                        }
                        else
                        {
                            brushBckgrnd = e.Appearance.GetBackBrush( e.Cache );
                        }
                    }
                    else
                    {
                        brushBckgrnd = ( bMultiBudgetDep ) ? new System.Drawing.SolidBrush( litegrayColor ) : new System.Drawing.SolidBrush( yelowColor );
                    }
                }
                // ���� ������
                if( bIsMoneyColumn )
                {
                    if( bNodeFocused )
                    {
                        if( bColumnFocused )
                        {
                            if( m_objBudget.IsAccept )
                            {
                                brushFont = System.Drawing.Brushes.White;
                            }
                            else
                            {
                                brushFont = ( bHasChildren ) ? System.Drawing.Brushes.White : System.Drawing.Brushes.Black;
                            }
                        }
                        else
                        {
                            brushFont = System.Drawing.Brushes.White;
                        }
                    }
                    else
                    {
                        if( e.Column == colSummary )
                        {
                            if (e.Node.ParentNode == null)
                            {
                                brushFont = System.Drawing.Brushes.Blue;
                            }
                            else
                            {
                                if ((e.Node.ParentNode != null) && (e.Node.ParentNode.ParentNode == null))
                                {
                                    brushFont = System.Drawing.Brushes.Black;
                                }
                            }
                        }
                        else
                        {
                            if( bNeedPaintItogBorder )
                            {
                                brushFont = new System.Drawing.SolidBrush( Color.FromArgb( 255, 96, 96, 96 ) );
                            }
                            else
                            {
                                brushFont = System.Drawing.Brushes.Black;
                            }
                        }
                    }
                }
                else
                {
                    if( bNodeFocused )
                    {
                        if( bColumnFocused )
                        {
                            if( m_objBudget.IsAccept )
                            {
                                brushFont = System.Drawing.Brushes.White;
                            }
                            else
                            {
                                if( bMultiBudgetDep )
                                {
                                    brushFont = System.Drawing.Brushes.White;
                                }
                                else
                                {
                                    brushFont = System.Drawing.Brushes.Black;
                                }
                            }
                        }
                        else
                        {
                            brushFont = System.Drawing.Brushes.White;
                        }
                    }
                    else
                    {
                        brushFont = System.Drawing.Brushes.Black;
                    }
                }
                // ����� � ������
                if( bIsMoneyColumn )
                {
                    if( e.Column == colSummary )
                    {
                        // ������� "�����"
                        if ((e.Node.ParentNode == null) || ((e.Node.ParentNode != null) && (e.Node.ParentNode.ParentNode == null) && (e.Node.HasChildren == true))) // ��� ������������ ������ ��� ��������� ������� ������
                        {
                            // ����� ����� �� ������, ��� � ������ ������� � ���� �������� � ������ 
                            // ��� ��������� ������ ������������ ������
                            if (e.Node.ParentNode == null)
                            {
                                try
                                {
                                    if ((e.Node.GetValue(e.Column) != null) && (e.Node.GetValue(e.Column) != null) && (System.Convert.ToString(e.Node.GetValue(e.Column)) != ""))
                                    {
                                            strText = System.Convert.ToString(e.Node.GetValue(e.Column));
                                    }
                                }
                                catch (System.Exception f10)
                                {
                                    System.Windows.Forms.MessageBox.Show(null, f10.Message, "������10",
                                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                try
                                {
                                    if ((e.Node.ParentNode != null) && (e.Node.ParentNode.ParentNode == null) && (e.Node.HasChildren == true)) // ��� ��������� ������� �����
                                    {
                                        if ((e.Node.GetValue(e.Column) != null) && (e.Node.GetValue(e.Column) != null) && (System.Convert.ToString(e.Node.GetValue(e.Column)) != ""))
                                        {
                                            strText = System.String.Format("{0:### ### ##0.00}", System.Convert.ToDouble(e.Node.GetValue(e.Column))) + " " + m_objBudget.Currency.CurrencyCode + " ";
                                        }
                                    }
                                }
                                catch (System.Exception f11)
                                {
                                 System.Windows.Forms.MessageBox.Show(null, f11.Message, "������11",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            try
                            {

                                if ((e.Node.GetValue(e.Column) != null) && (e.Node.GetValue(e.Column) != null) && (System.Convert.ToString(e.Node.GetValue(e.Column)) != ""))
                                {
                                    strText = System.Convert.ToString(e.Node.GetValue(e.Column));
                                }
                            }
                            catch (System.Exception f12)
                            {
                             System.Windows.Forms.MessageBox.Show(null, f12.Message, "������12",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        // ������� � ��������
                        if( e.Node.ParentNode == null ) // ��� ������������ ������
                        {
                            if( bHasChildren == true )
                            {
                                // � ������ ���� ���������
                                try
                                {
                                    if (treeList.GetNodeIndex(e.Node) < this.m_objTotalRowList.Count)
                                    {
                                        if (this.m_objTotalRowList[treeList.GetNodeIndex(e.Node)].GetColumnValue(e.Column) != null)
                                        {
                                            try
                                            {
                                                strText = System.String.Format("{0:### ### ##0.00}",
                                                    this.m_objTotalRowList[treeList.GetNodeIndex(e.Node)].GetColumnValue(e.Column));
                                                strText += " " + strCurrencyBudget + " ";
                                            }
                                            catch (System.Exception f13)
                                            {
                                                System.Windows.Forms.MessageBox.Show(null, f13.Message, "������13",
                                                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                            }
                                        }
                                    }
                                }
                                catch (System.Exception f1)
                                {
                                    System.Windows.Forms.MessageBox.Show(null, f1.Message, "������1",
                                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                ERP_Budget.Common.CBudgetItemDecode objBudgetItemDecode = objBudgetItem.GetBudgetItemDecode( objMonth );
                                if( ( objBudgetItemDecode != null) && (objBudgetItemDecode.Currency != null))
                                {
                                    try
                                    {
                                        System.Double MoneyPlan = objBudgetItemDecode.MoneyPlan;
                                        bSetDscrpn = (objBudgetItemDecode.Description != "");
                                        if (MoneyPlan != 0)
                                        {
                                            try
                                            {
                                                strText = System.String.Format("{0:### ### ##0.00}", MoneyPlan);
                                                strText += " " + objBudgetItemDecode.Currency.CurrencyCode + " ";
                                            }
                                            catch (System.Exception f14)
                                            {
                                                System.Windows.Forms.MessageBox.Show(null, f14.Message, "������14",
                                                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                            }
                                        }
                                    }
                                    catch (System.Exception f2)
                                    {
                                        System.Windows.Forms.MessageBox.Show(null, f2.Message, "������2",
                                           System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    }

                                }
                            }
                        }
                        else
                        {
                            //if( bHasChildren == false ) // ������ ������� ������
                            //{
                            try
                            {
                                if ((e.Node.ParentNode.ParentNode == null) && (e.Node.HasChildren == true) 
                                    && (e.Node.GetValue(e.Column) != null) )
                                {
                                    if (e.Node.GetValue(e.Column).GetType().FullName.Contains("String") == true)
                                    {
                                        strText += System.Convert.ToString(e.Node.GetValue(e.Column));
                                    }
                                    else
                                    {
                                        System.Double MoneyPlan = System.Convert.ToDouble(e.Node.GetValue(e.Column));
                                        strText = System.String.Format("{0:### ### ##0.00}", MoneyPlan);
                                        strText += " " + m_objBudget.Currency.CurrencyCode + " ";
                                    }
                                }
                                else
                                {
                                    ERP_Budget.Common.CBudgetItemDecode objBudgetItemDecode = objBudgetItem.GetBudgetItemDecode(objMonth);
                                    if ((objBudgetItemDecode != null) && (objBudgetItemDecode.Currency != null))
                                    {
                                        try
                                        {
                                            System.Double MoneyPlan = objBudgetItemDecode.MoneyPlan;
                                            bSetDscrpn = (objBudgetItemDecode.Description != "");
                                            if (MoneyPlan != 0)
                                            {
                                                strText = System.String.Format("{0:### ### ##0.00}", MoneyPlan);
                                                strText += " " + objBudgetItemDecode.Currency.CurrencyCode + " ";
                                            }
                                        }
                                        catch (System.Exception f3)
                                        {
                                            System.Windows.Forms.MessageBox.Show(null, f3.Message, "������2",
                                               System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                        }
                                    }
                                }
                            }
                            catch (System.Exception f15)
                            {
                                System.Windows.Forms.MessageBox.Show(null, f15.Message, "������15",
                                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            }
                            //}
                        }
                    }
                }
                else
                {
                    try
                    {
                        if( (e.Column == colDEBITARTICLE_NUM) && (e.Node.GetValue(e.Column) != null) )
                        {
                            strText = objBudgetItem.BudgetItemFullName; // System.Convert.ToString(e.Node.GetValue(colDEBITARTICLE_NUM));
                        }
                    }
                    catch (System.Exception f17)
                    {
                        System.Windows.Forms.MessageBox.Show(null, f17.Message, "������17",
                           System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                    //strText = e.CellText;
                }

                // ���������� ���������
                // ���
                if( brushBckgrnd != null )
                {
                    e.Graphics.FillRectangle( brushBckgrnd, e.Bounds );
                }
                if( strText != "" )
                {
                    // ����� ��� ������
                    if( bNeedPaintItogBorder )
                    {
                        System.Drawing.Rectangle BorderRect = new Rectangle( e.Bounds.Location, e.Bounds.Size );
                        BorderRect.Y += 1;
                        BorderRect.X += ( 1 + 16 );
                        BorderRect.Width -= ( 2 + 16 );
                        BorderRect.Height -= 2;

                        System.Drawing.Rectangle ImageRect = new Rectangle( e.Bounds.Location, e.Bounds.Size );
                        ImageRect.Y += 2;
                        //BorderRect.X += 1;
                        ImageRect.Width = 16;
                        ImageRect.Height = 16;


                        // ������ �����
                        ControlPaint.DrawBorder( e.Cache.Graphics, BorderRect, ColorItogBorder, ButtonBorderStyle.Solid );
                        // ������ ����� � �����
                        System.Drawing.RectangleF StringRect = new RectangleF( BorderRect.Location, BorderRect.Size );
                        StringRect.Width -= 2;
                        StringRect.Y += 1;
                        StringRect.Height -= 1;
                        e.Graphics.DrawString( strText, e.Appearance.Font, brushFont, StringRect, e.Appearance.GetStringFormat() );

                        e.Graphics.DrawImage( ErpBudgetBudgetEditor.Properties.Resources.sum_bmp, ImageRect );

                    }
                    else
                    {
                        if( bIsMoneyColumn )
                        {
                            e.Appearance.DrawString( e.Cache, strText,
                                new Rectangle( e.Bounds.Location.X, e.Bounds.Location.Y,
                                e.Bounds.Size.Width - 3, e.Bounds.Size.Height ), brushFont );
                            if (bSetDscrpn == true)
                            {
                                Point[] curvePoints = {new Point(e.Bounds.Location.X, e.Bounds.Location.Y), 
                                                          new Point(e.Bounds.Location.X + 4, e.Bounds.Location.Y), 
                                                          new Point(e.Bounds.Location.X, e.Bounds.Location.Y + 4)};

                                e.Graphics.FillPolygon(System.Drawing.Brushes.Green, curvePoints);
                                //e.Graphics.FillRectangle(System.Drawing.Brushes.Green, e.Bounds.Location.X, e.Bounds.Location.Y, 3, 3);
                            }
                            e.Handled = true;
                        }
                        else
                        {
                            //System.Drawing.Rectangle StringRect = new Rectangle( e.Bounds.Location.X + 2, e.Bounds.Location.Y, e.Bounds.Size.Width - 2, e.Bounds.Size.Height );
                            e.Appearance.DrawString( e.Cache, strText,
                                new Rectangle( e.Bounds.Location.X + 2, e.Bounds.Location.Y,
                                e.Bounds.Size.Width - 2, e.Bounds.Size.Height ), brushFont );
                        }
                    }
                }
                // ��������� �����, ������������� ������� ����, ��� �� ������ ���� � True
                e.Handled = true;
            }
            catch( System.Exception f )
            {
                e.Handled = false;

                System.Windows.Forms.MessageBox.Show( null, "������ treeList_CustomDrawNodeCell\n������: " + 
                    ( System.String )e.Node.GetValue( colDEBITARTICLE_NUM ) + "\n�������: "  + 
                    e.Column.Caption + "\n\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        /// <summary>
        /// ������������ �������� � ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_CustomDrawNodeImages( object sender, DevExpress.XtraTreeList.CustomDrawNodeImagesEventArgs e )
        {
            try
            {
                if( treeList.Nodes.Count == 0 ) { return; }
                if( e.Node == null ) { return; }
                //if( e.Node.GetValue( colDEBITARTICLE_MULTIBUDGETDEP ) == null ) { return; }
                //if( e.Node.GetValue( colDEBITARTICLE_MULTIBUDGETDEP ) == System.DBNull.Value ) { return; }
                int Y = e.SelectRect.Top + ( e.SelectRect.Height - imglNodes.Images[ 0 ].Height ) / 2;
                if( e.Node.Tag != null )
                {
                    ERP_Budget.Common.CBudgetItem objBudgetItem = ( ERP_Budget.Common.CBudgetItem )e.Node.Tag;
                    if( objBudgetItem.DebitArticleID.CompareTo( System.Guid.Empty ) == 0 )
                    {
                        try
                        {
                            ControlPaint.DrawImageDisabled( e.Graphics, imglNodes.Images[ 1 ], e.SelectRect.X, Y, Color.Black );
                            e.Handled = true;
                        }
                        catch { }
                    }
                    else
                    {
                        try
                        {
                            e.Graphics.DrawImage( imglNodes.Images[ 0 ], new Point( e.SelectRect.X, Y ) );
                            e.Handled = true;
                        }
                        catch { }
                    }
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( null, "������ ��������� �������� � �����\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        private void treeList_MouseDoubleClick( object sender, MouseEventArgs e )
        {
            if( ( treeList.Nodes.Count == 0 ) || ( treeList.FocusedNode == null ) || ( treeList.FocusedNode.Tag == null ) ) { return; }
            if( ( treeList.FocusedColumn == null ) || ( treeList.FocusedColumn != colDEBITARTICLE_NUM ) ) { return; }

            try
            {
                frmDebitArticleEditor objfrmDebitArticleEditor = new frmDebitArticleEditor(this.m_objProfile, this.m_objBudget);

                // ��������� �������� ������ �������
                ERP_Budget.Common.CBudgetItem objBudgetItem = (ERP_Budget.Common.CBudgetItem)treeList.FocusedNode.Tag;
                if (this.m_objBudget.IsAccept)
                {
                    objfrmDebitArticleEditor.ViewDebitArticle(objBudgetItem);
                }
                else
                {
                    objfrmDebitArticleEditor.EditDebitArticle(objBudgetItem);
                }

                if (objfrmDebitArticleEditor.DialogResult == DialogResult.OK)
                {
                    objBudgetItem.DontChange = objfrmDebitArticleEditor.BudgetItem.DontChange;
                    objBudgetItem.Name = objfrmDebitArticleEditor.BudgetItem.Name;
                    objBudgetItem.BudgetItemDescription = objfrmDebitArticleEditor.BudgetItem.BudgetItemDescription;
                    objBudgetItem.TransprtRest = objfrmDebitArticleEditor.BudgetItem.TransprtRest;
                    objBudgetItem.BudgetExpenseType = objfrmDebitArticleEditor.BudgetItem.BudgetExpenseType;

                    treeList.FocusedNode.SetValue(colDEBITARTICLE_NUM, objfrmDebitArticleEditor.BudgetItem.BudgetItemFullName);
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ ��������� �������� ������� ������� ����.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        #endregion

        #region �������� � �������� �����
        /// <summary>
        /// ������������� ������ �������
        /// </summary>
        private void SetColumnWidth()
        {
            try
            {
                if( treeList.Nodes.Count == 0 ) { return; }

                System.Int32 iMinWidth = 110;
                foreach( DevExpress.XtraTreeList.Columns.TreeListColumn column in treeList.Columns )
                {
                    if( column == colDEBITARTICLE_NUM ) { continue; }
                    column.Width = iMinWidth;
                    //column.Width = isMonthColumn( column ) ? iMinWidth : column.Width;
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "�� ����� ��������� ������ �������� ��������� ������.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        private void frmBudget_Load( object sender, EventArgs e )
        {
            try
            {
                treeList.Appearance.GroupFooter.BackColor = Color.White;

                // ��������� ������ ������ �������
                if( bLoadBudgeItems() == true )
                {
                    if( treeList.Nodes.Count > 0 )
                    {
                        // ������������ �����
                        CalcGrandTotalRow();
                        // �������� ������ ��������
                        SetColumnWidth();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show( this,
                        "������ ������ ������� ����.\n����� �������� ������ ������ �������� �� �����������.", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information );

                        if( ERP_Budget.Common.CBudgetItem.ImportDebitArticleListToBudget( this.m_objProfile, this.m_objBudget.uuidID ) )
                        {
                            if( bLoadBudgeItems() == true )
                            {
                                // ������������ �����
                                CalcGrandTotalRow();
                                // �������� ������ ��������
                                SetColumnWidth();
                            }
                        }

                    }
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ �������� �����\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        private void frmBudget_FormClosing( object sender, FormClosingEventArgs e )
        {
            try
            {
                if( m_bIsTreeModified )
                {
                    if( System.Windows.Forms.MessageBox.Show( this,
                        "������ ��� �������.\n����� �� ����� ��� ���������� ���������?", "��������",
                        System.Windows.Forms.MessageBoxButtons.YesNo,
                        System.Windows.Forms.MessageBoxIcon.Question ) == System.Windows.Forms.DialogResult.No )
                    // ��������� ������� ����������
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                if (bIsThreadsActive() == true)
                {
                    // ������������ ������� ������
                    DevExpress.XtraEditors.XtraMessageBox.Show("� ������ ������ ���������� ������� ���������� � MS Excel.\n���������, ����������, ���������� ��������.", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Cancel = true;
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "frmBudget_FormClosing\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        #endregion

        #region ToolTip
        /// <summary>
        /// ���������, �������� �� ������� �������� ����������� �� �������
        /// </summary>
        /// <param name="column">����������� �������</param>
        /// <returns></returns>
        private System.Boolean isMonthColumn( DevExpress.XtraTreeList.Columns.TreeListColumn column )
        {
            System.Boolean bRes = false;
            try
            {
                bRes = ( ( column == colJANUARY ) || ( column == colFEBRUARY ) || ( column == colMARCH ) || 
                        ( column == colAPRIL ) || ( column == colMAY ) || ( column == colJUNE ) || 
                        ( column == colJULY ) || ( column == colAUGUST ) || ( column == colSEPTEMBER ) || 
                        ( column == colOCTEMBER ) || ( column == colNOVEMBER ) || ( column == colDECEMBER ) );
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ isMonthColumn\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return bRes;
        }
        /// <summary>
        /// �������������� ����� ��� ����������� ���������
        /// </summary>
        /// <param name="hi"></param>
        /// <returns></returns>
        private System.String ShowHitInfo( DevExpress.XtraTreeList.TreeListHitInfo hi )
        {
            System.String strRes = "";
            try
            {
                if( ( hi.Column == null ) || ( hi.Node == null ) || ( hi.Node.Tag == null ) ) { strRes = ""; }
                else
                {
                    if( isMonthColumn( hi.Column ) )
                    {
                        // ������� "������" ... "�������"
                        // ����������� �����������
                        ERP_Budget.Common.CBudgetItemDecode objBudgetItemDecode = 
                            ( ( ERP_Budget.Common.CBudgetItem )( hi.Node.Tag ) ).GetBudgetItemDecode( GetMonthEnum( hi.Column ) );

                        if( objBudgetItemDecode.Currency != null )
                        {
                            // ��������� ������, ����� � ���������� �� �����������
                            strRes = hi.Column.Caption + "\n" + 
                                System.String.Format( "{0:### ### ###.00}", objBudgetItemDecode.MoneyPlan ) + "\n" + 
                                objBudgetItemDecode.Currency.CurrencyCode + "\n" + 
                                objBudgetItemDecode.Description;

                        }
                    }
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( null, "������ ������������ ����������� ��������.\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return strRes;
        }

        private void treeList_MouseMove( object sender, MouseEventArgs e )
        {
            try
            {
                System.String strInfo = ShowHitInfo( treeList.CalcHitInfo( new Point( e.X, e.Y ) ) );
                if( strInfo != "" ) { toolTipController.ShowHint( strInfo ); }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( null, "������ ������������ ����������� ��������.\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        /// <summary>
        /// ���������� �������, ������������, ���� ��� �������� ������������ ��������� �������� � ������ �������� ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_InvalidNodeException( object sender, DevExpress.XtraTreeList.InvalidNodeExceptionEventArgs e )
        {
            try
            {
                e.ErrorText = "�������� �������� � ������� ������.\n���������, ����������, ��������� ��������.\n\n����� ������: " + e.Exception.Message;
                e.WindowCaption = "������";
                e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "treeList_InvalidNodeException" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        #endregion

        #region �������� �����
        /// <summary>
        /// ���������� ��������� �������� � �������� ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_CustomDrawFooterCell( object sender, DevExpress.XtraTreeList.CustomDrawFooterCellEventArgs e )
        {
            try
            {
                System.String strItogo = "";
                if( isMonthColumn( e.Column ) )
                {
                    strItogo = System.String.Format( "{0:### ### ##0.00}", this.m_objGrandTotalRow.GetColumnValue( e.Column ) );
                    strItogo += " " + this.m_objBudget.Currency.CurrencyCode;
                    DrawFooterCell( e.Cache, e.Bounds, strItogo, e.Appearance, e.Appearance.Font );
                    e.Handled = true;
                }
                // �������� �����
                if( e.Column == colSummary )
                {
                    strItogo = System.String.Format( "{0:### ### ##0.00}", this.m_objGrandTotalRow.GetSumItogo() );
                    strItogo += " " + ERP_Budget.Global.Consts.strCurrencyBudget;
                    DrawFooterCell( e.Cache, e.Bounds, strItogo, e.Appearance, new Font( e.Appearance.Font, FontStyle.Bold ) );
                    e.Handled = true;
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "treeList_CustomDrawFooterCell" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }
        /// <summary>
        /// ������������ �������� � �������� ������
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="r"></param>
        /// <param name="s"></param>
        /// <param name="appObject"></param>
        private void DrawFooterCell( DevExpress.Utils.Drawing.GraphicsCache cache, Rectangle r, string s,
            DevExpress.Utils.AppearanceObject appObject, System.Drawing.Font font )
        {
            try
            {
                if( s == "" ) { return; }

                Brush brush = ( Brush )appObject.GetBackBrush( cache );
                ControlPaint.DrawBorder( cache.Graphics, r, appObject.BackColor, ButtonBorderStyle.Solid );

                r.Inflate( -1, -1 );
                cache.Graphics.FillRectangle( ( Brush )appObject.GetBorderBrush( cache ), r );

                r.Inflate( -2, 0 );
                appObject.DrawString( cache, s, r, font, appObject.GetStringFormat() );
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "DrawFooterCell" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }
        /// <summary>
        /// ������������ ������ � �������������� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_CustomDrawRowFooter( object sender, DevExpress.XtraTreeList.CustomDrawRowFooterEventArgs e )
        {
            try
            {
                // ������ ��� ��� ������������� ������
                System.Drawing.Rectangle RowFooterRect = new Rectangle( e.Bounds.Location, e.Bounds.Size );
                RowFooterRect.Height -= 1;// .Inflate( -1, -1 );

                e.Graphics.DrawLine( new Pen( e.Appearance.ForeColor, 1 ), e.Bounds.X, e.Bounds.Y + e.Bounds.Height - 1, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height - 1 );
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( null, "������ ��������� ���� ������ � �������������� �������\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        /// <summary>
        /// ���������� ����� ����� � ��������� � ������ �������
        /// </summary>
        /// <param name="objNode">���� ������</param>
        /// <param name="objColumn">�������</param>
        /// <returns>����� ����� � ��������� � ������ �������</returns>
        private System.Double GetDecodeMoneyPlanValue( DevExpress.XtraTreeList.Nodes.TreeListNode objNode,
            DevExpress.XtraTreeList.Columns.TreeListColumn objColumn )
        {
            System.Double doubleRet = 0;

            if( ( objNode == null ) || ( objColumn == null ) || ( objNode.Tag == null ) || 
                    ( isMonthColumn( objColumn ) == false ) ) { return doubleRet; }

            try
            {
                // �����������
                ERP_Budget.Common.CBudgetItemDecode objBudgetItemDecode = ( ( ERP_Budget.Common.CBudgetItem )objNode.Tag ).GetBudgetItemDecode( GetMonthEnum( objColumn ) );
                // ������ ����� 
                ERP_Budget.Common.CCurrency objCurrency = objBudgetItemDecode.Currency;
                if( objCurrency != null )
                {
                    // ����� ����� �� �����������
                    System.Double MoneyPlan = objBudgetItemDecode.MoneyPlan;
                    // ���� ������ ����������� � ������ ����� 
                    System.Double CurrencyRate = this.m_objBudget.BudgetCurrencyRate.GetCurrencyRate( objCurrency.uuidID, this.m_objBudget.Currency.uuidID );
                    // �������� ����� � ������ �������
                    doubleRet = MoneyPlan * CurrencyRate;
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ ��������� �������� ����� � ������� "  + 
                    objColumn.Caption + " � " + this.m_objBudget.Currency.CurrencyCode + ".\n\n����� ������: " + 
                    f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return doubleRet;
        }
        /// <summary>
        /// ���������� ������ ������ � ������� �������� ����
        /// </summary>
        /// <param name="objColumn">������� ������</param>
        /// <returns>������ ������</returns>
        private System.Int32 GetMonthIndex( DevExpress.XtraTreeList.Columns.TreeListColumn objColumn )
        {
            System.Int32 iRet = -1;
            try
            {
                if( objColumn == colJANUARY ) { iRet = 0; }
                if( objColumn == colFEBRUARY ) { iRet = 1; }
                if( objColumn == colMARCH ) { iRet = 2; }
                if( objColumn == colAPRIL ) { iRet = 3; }
                if( objColumn == colMAY ) { iRet = 4; }
                if( objColumn == colJUNE ) { iRet = 5; }
                if( objColumn == colJULY ) { iRet = 6; }
                if( objColumn == colAUGUST ) { iRet = 7; }
                if( objColumn == colSEPTEMBER ) { iRet = 8; }
                if( objColumn == colOCTEMBER ) { iRet = 9; }
                if( objColumn == colNOVEMBER ) { iRet = 10; }
                if( objColumn == colDECEMBER ) { iRet = 11; }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ ������ ������� ������ "  + objColumn.Caption + 
                    ".\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return iRet;
        }
        /// <summary>
        /// ���������� ���������� ������
        /// </summary>
        /// <param name="objColumn">������� ������</param>
        /// <returns>���������� ������</returns>
        private ERP_Budget.Common.enumMonth GetMonthEnum( DevExpress.XtraTreeList.Columns.TreeListColumn objColumn )
        {
            ERP_Budget.Common.enumMonth enRet = ERP_Budget.Common.enumMonth.Unkown;
            try
            {
                if( objColumn == colJANUARY ) { enRet = ERP_Budget.Common.enumMonth.Jan; }
                if( objColumn == colFEBRUARY ) { enRet = ERP_Budget.Common.enumMonth.Feb; }
                if( objColumn == colMARCH ) { enRet = ERP_Budget.Common.enumMonth.Mar; }
                if( objColumn == colAPRIL ) { enRet = ERP_Budget.Common.enumMonth.Apr; }
                if( objColumn == colMAY ) { enRet = ERP_Budget.Common.enumMonth.May; }
                if( objColumn == colJUNE ) { enRet = ERP_Budget.Common.enumMonth.Jun; }
                if( objColumn == colJULY ) { enRet = ERP_Budget.Common.enumMonth.Jul; }
                if( objColumn == colAUGUST ) { enRet = ERP_Budget.Common.enumMonth.Aug; }
                if( objColumn == colSEPTEMBER ) { enRet = ERP_Budget.Common.enumMonth.Sep; }
                if( objColumn == colOCTEMBER ) { enRet = ERP_Budget.Common.enumMonth.Oct; }
                if( objColumn == colNOVEMBER ) { enRet = ERP_Budget.Common.enumMonth.Nov; }
                if( objColumn == colDECEMBER ) { enRet = ERP_Budget.Common.enumMonth.Dec; }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ ������ enum ������ "  + objColumn.Caption + 
                    ".\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return enRet;
        }

        #region ����� "�������� ������"
        /// <summary>
        /// ����� "�������� ������"
        /// </summary>
        class TotalRow
        {
            public System.Double SumJan;
            public System.Double SumFeb;
            public System.Double SumMar;
            public System.Double SumApr;
            public System.Double SumMay;
            public System.Double SumJun;
            public System.Double SumJul;
            public System.Double SumAug;
            public System.Double SumSep;
            public System.Double SumOct;
            public System.Double SumNov;
            public System.Double SumDec;

            public TotalRow()
            {
                // ���������� ����� � ����
                ClearTotal();
            }
            /// <summary>
            /// ���������� �������� ����� � ����
            /// </summary>
            public void ClearTotal()
            {
                try
                {
                    this.SumJan = 0;
                    this.SumFeb = 0;
                    this.SumMar = 0;
                    this.SumApr = 0;
                    this.SumMay = 0;
                    this.SumJun = 0;
                    this.SumJul = 0;
                    this.SumAug = 0;
                    this.SumSep = 0;
                    this.SumOct = 0;
                    this.SumNov = 0;
                    this.SumDec = 0;
                }
                catch( System.Exception f )
                {
                    System.Windows.Forms.MessageBox.Show( null, "������ ������� �������� ����.\n\n����� ������: " + f.Message, "������",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }

                return;
            }
            /// <summary>
            /// ������� �������� ����� �� ������
            /// </summary>
            /// <returns>�������� ����� �� ������</returns>
            public System.Double GetSumItogo()
            {
                System.Double SumItogo = 0;
                try
                {
                    SumItogo += this.SumJan;
                    SumItogo += this.SumFeb;
                    SumItogo += this.SumMar;
                    SumItogo += this.SumApr;
                    SumItogo += this.SumMay;
                    SumItogo += this.SumJun;
                    SumItogo += this.SumJul;
                    SumItogo += this.SumAug;
                    SumItogo += this.SumSep;
                    SumItogo += this.SumOct;
                    SumItogo += this.SumNov;
                    SumItogo += this.SumDec;
                }
                catch( System.Exception f )
                {
                    System.Windows.Forms.MessageBox.Show( null, "������ �������� ����� �� 12 �������.\n\n����� ������: " + f.Message, "������",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }

                return SumItogo;
            }
            /// <summary>
            /// ���������� ����� ��� ��������� ������
            /// </summary>
            /// <param name="enMonth">�����</param>
            /// <returns>����� ��� ��������� ������</returns>
            public System.Double GetMonthValue( ERP_Budget.Common.enumMonth enMonth )
            {
                System.Double SumMonth = 0;
                try
                {
                    switch( enMonth )
                    {
                        case ERP_Budget.Common.enumMonth.Jan:
                        {
                            SumMonth = this.SumJan;
                            break;
                        }
                        case ERP_Budget.Common.enumMonth.Feb:
                        {
                            SumMonth = this.SumFeb;
                            break;
                        }
                        case ERP_Budget.Common.enumMonth.Mar:
                        {
                            SumMonth = this.SumMar;
                            break;
                        }
                        case ERP_Budget.Common.enumMonth.Apr:
                        {
                            SumMonth = this.SumApr;
                            break;
                        }
                        case ERP_Budget.Common.enumMonth.May:
                        {
                            SumMonth = this.SumMay;
                            break;
                        }
                        case ERP_Budget.Common.enumMonth.Jun:
                        {
                            SumMonth = this.SumJun;
                            break;
                        }
                        case ERP_Budget.Common.enumMonth.Jul:
                        {
                            SumMonth = this.SumJul;
                            break;
                        }
                        case ERP_Budget.Common.enumMonth.Aug:
                        {
                            SumMonth = this.SumAug;
                            break;
                        }
                        case ERP_Budget.Common.enumMonth.Sep:
                        {
                            SumMonth = this.SumSep;
                            break;
                        }
                        case ERP_Budget.Common.enumMonth.Oct:
                        {
                            SumMonth = this.SumOct;
                            break;
                        }
                        case ERP_Budget.Common.enumMonth.Nov:
                        {
                            SumMonth = this.SumNov;
                            break;
                        }
                        case ERP_Budget.Common.enumMonth.Dec:
                        {
                            SumMonth = this.SumDec;
                            break;
                        }
                        default:
                        break;

                    }
                }
                catch( System.Exception f )
                {
                    System.Windows.Forms.MessageBox.Show( null, "������ ������ ����� ��� ������ " + enMonth.ToString() +  ".\n\n����� ������: " + f.Message, "������",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }

                return SumMonth;
            }
            /// <summary>
            /// ���������� ����� ��� ��������� �������
            /// </summary>
            /// <param name="objColumn">�������</param>
            /// <returns>����� ��� ��������� �������</returns>
            public System.Double GetColumnValue( DevExpress.XtraTreeList.Columns.TreeListColumn objColumn )
            {
                System.Double SumMonth = 0;
                try
                {
                    if( objColumn.Name == "colJANUARY" ) { SumMonth = this.SumJan; }
                    if( objColumn.Name == "colFEBRUARY" ) { SumMonth = this.SumFeb; }
                    if( objColumn.Name == "colMARCH" ) { SumMonth = this.SumMar; }
                    if( objColumn.Name == "colAPRIL" ) { SumMonth = this.SumApr; }
                    if( objColumn.Name == "colMAY" ) { SumMonth = this.SumMay; }
                    if( objColumn.Name == "colJUNE" ) { SumMonth = this.SumJun; }
                    if( objColumn.Name == "colJULY" ) { SumMonth = this.SumJul; }
                    if( objColumn.Name == "colAUGUST" ) { SumMonth = this.SumAug; }
                    if( objColumn.Name == "colSEPTEMBER" ) { SumMonth = this.SumSep; }
                    if( objColumn.Name == "colOCTEMBER" ) { SumMonth = this.SumOct; }
                    if( objColumn.Name == "colNOVEMBER" ) { SumMonth = this.SumNov; }
                    if( objColumn.Name == "colDECEMBER" ) { SumMonth = this.SumDec; }
                }
                catch( System.Exception f )
                {
                    System.Windows.Forms.MessageBox.Show( null, "������ ������ ����� ��� ������ " + objColumn.Caption +  ".\n\n����� ������: " + f.Message, "������",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }

                return SumMonth;
            }
            /// <summary>
            /// ������������� �������� � �������� �����, ��������������� ��������� �������
            /// </summary>
            /// <param name="objColumn">�������</param>
            public void SetColumnValue( DevExpress.XtraTreeList.Columns.TreeListColumn objColumn,
                System.Double SumMonth )
            {
                try
                {
                    if( objColumn.Name == "colJANUARY" ) { this.SumJan = SumMonth; }
                    if( objColumn.Name == "colFEBRUARY" ) { this.SumFeb = SumMonth; }
                    if( objColumn.Name == "colMARCH" ) { this.SumMar = SumMonth; }
                    if( objColumn.Name == "colAPRIL" ) { this.SumApr = SumMonth; }
                    if( objColumn.Name == "colMAY" ) { this.SumMay = SumMonth; }
                    if( objColumn.Name == "colJUNE" ) { this.SumJun = SumMonth; }
                    if( objColumn.Name == "colJULY" ) { this.SumJul = SumMonth; }
                    if( objColumn.Name == "colAUGUST" ) { this.SumAug = SumMonth; }
                    if( objColumn.Name == "colSEPTEMBER" ) { this.SumSep = SumMonth; }
                    if( objColumn.Name == "colOCTEMBER" ) { this.SumOct = SumMonth; }
                    if( objColumn.Name == "colNOVEMBER" ) { this.SumNov = SumMonth; }
                    if( objColumn.Name == "colDECEMBER" ) { this.SumDec = SumMonth; }
                }
                catch( System.Exception f )
                {
                    System.Windows.Forms.MessageBox.Show( null, "������ ��������� ����� � �������� ������ ��� ������ " + objColumn.Caption +  ".\n\n����� ������: " + f.Message, "������",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }

                return;
            }
            /// <summary>
            /// ������������� �������� � �������� �����, ��������������� ��������� �������
            /// </summary>
            /// <param name="objColumn">�������</param>
            public void AppendColumnValue( DevExpress.XtraTreeList.Columns.TreeListColumn objColumn,
                System.Double SumMonth )
            {
                try
                {
                    if( objColumn.Name == "colJANUARY" ) { this.SumJan += SumMonth; }
                    if( objColumn.Name == "colFEBRUARY" ) { this.SumFeb += SumMonth; }
                    if( objColumn.Name == "colMARCH" ) { this.SumMar += SumMonth; }
                    if( objColumn.Name == "colAPRIL" ) { this.SumApr += SumMonth; }
                    if( objColumn.Name == "colMAY" ) { this.SumMay += SumMonth; }
                    if( objColumn.Name == "colJUNE" ) { this.SumJun += SumMonth; }
                    if( objColumn.Name == "colJULY" ) { this.SumJul += SumMonth; }
                    if( objColumn.Name == "colAUGUST" ) { this.SumAug += SumMonth; }
                    if( objColumn.Name == "colSEPTEMBER" ) { this.SumSep += SumMonth; }
                    if( objColumn.Name == "colOCTEMBER" ) { this.SumOct += SumMonth; }
                    if( objColumn.Name == "colNOVEMBER" ) { this.SumNov += SumMonth; }
                    if( objColumn.Name == "colDECEMBER" ) { this.SumDec += SumMonth; }
                }
                catch( System.Exception f )
                {
                    System.Windows.Forms.MessageBox.Show( null, "������ ���������� ����� � �������� ������ ��� ������ " + objColumn.Caption +  ".\n\n����� ������: " + f.Message, "������",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }

                return;
            }
        }
        #endregion

        /// <summary>
        /// �������� ����� �������� �����
        /// </summary>
        private void CalcGrandTotalRow()
        {
            try
            {
                // ������ ������� �������� � �������� ������
                this.m_objGrandTotalRow.ClearTotal();

                // ������ ���������� �� ������ �������� ����� � ������� ��
                foreach( TotalRow objTotalRow in this.m_objTotalRowList )
                {
                    objTotalRow.ClearTotal();
                }

                // ���������� �� ����� � ������������� �������� � �������� �������� �����
                foreach( DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeList.Nodes )
                {
                    CalcRowTotalSum( objNode );
                }

                // ����������� ����� ��� �������� ������ GrandTotalRow
                FillGrandTotalRow();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( null, "������ ��������� ���� ����� �������� ������.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        /// <summary>
        /// ����������� ����� ��� �������� ������ GrandTotalRow
        /// </summary>
        private void FillGrandTotalRow()
        {
            try
            {
                // ������ ������� �������� � �������� ������
                this.m_objGrandTotalRow.ClearTotal();

                // ������ ���������� �� ������ �������� �����
                foreach( TotalRow objTotalRow in this.m_objTotalRowList )
                {
                    this.m_objGrandTotalRow.SumJan += objTotalRow.SumJan;
                    this.m_objGrandTotalRow.SumFeb += objTotalRow.SumFeb;
                    this.m_objGrandTotalRow.SumMar += objTotalRow.SumMar;
                    this.m_objGrandTotalRow.SumApr += objTotalRow.SumApr;
                    this.m_objGrandTotalRow.SumMay += objTotalRow.SumMay;
                    this.m_objGrandTotalRow.SumJun += objTotalRow.SumJun;
                    this.m_objGrandTotalRow.SumJul += objTotalRow.SumJul;
                    this.m_objGrandTotalRow.SumAug += objTotalRow.SumAug;
                    this.m_objGrandTotalRow.SumSep += objTotalRow.SumSep;
                    this.m_objGrandTotalRow.SumOct += objTotalRow.SumOct;
                    this.m_objGrandTotalRow.SumNov += objTotalRow.SumNov;
                    this.m_objGrandTotalRow.SumDec += objTotalRow.SumDec;
                }

            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( null, "������ ������� ���� ��� ����� �������� ������.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        /// <summary>
        /// ��������� ����� ������������ ������ �������� ��� ����
        /// </summary>
        /// <param name="objNode"></param>
        /// <returns></returns>
        private ERP_Budget.Common.CBudgetItem GetParentBudgetItem( DevExpress.XtraTreeList.Nodes.TreeListNode objNode )
        {
            ERP_Budget.Common.CBudgetItem objBudgetItem = null;
            try
            {
                // ������ ��� ���� ������ ����� ������� ������������ ����
                DevExpress.XtraTreeList.Nodes.TreeListNode objParentNode = null;
                if( objNode.ParentNode == null ) { objParentNode = objNode; }
                else
                {
                    objParentNode = objNode.ParentNode;
                    while( objParentNode.ParentNode != null )
                    {
                        objParentNode = objParentNode.ParentNode;
                    }
                }
                if( objParentNode.ParentNode == null )
                {
                    objBudgetItem = ( ERP_Budget.Common.CBudgetItem )objParentNode.Tag;
                }

            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "�� ������� ���������� ������������ ������ �������� ��� ����.\n���������: " + 
                    ( System.String )objNode.GetValue( colDEBITARTICLE_NUM ) + 
                    ".\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
            }
            return objBudgetItem;
        }

        #region �������� ������ ��� ��������� ������� ������
        /// <summary>
        /// �������� �������� ����� ��� ������������ ������ 
        /// </summary>
        private void CalcSubBudgetRowTotalSum( DevExpress.XtraTreeList.Nodes.TreeListNode objNode,
            DevExpress.XtraTreeList.Columns.TreeListColumn objColumn )
        {
            if (treeList.Nodes.Count == 0) { return; }
            if (objNode.ParentNode == null) { return; }
            DevExpress.XtraTreeList.Nodes.TreeListNode objParentNode = null;
            // ���� ���� ��������� ���� ��������� ������� ������, �� ����� ����� ���-�� �������������
            try
            {

                if (objNode.ParentNode.ParentNode == null) { objParentNode = objNode; }
                else
                {
                    objParentNode = objNode;
                    while (objParentNode.ParentNode.ParentNode != null)
                    {
                        objParentNode = objParentNode.ParentNode;
                    }
                }

                if (objParentNode.ParentNode.ParentNode != null)
                {
                    System.Windows.Forms.MessageBox.Show( this, "��� ��������� " + 
                        ( System.String )objNode.GetValue( colDEBITARTICLE_NUM ) + " �� ������� ����� ��������� �������� ������.", "������",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return;
                }

                // ����������� �������� ������ ��� ��������� �������� (��������)
                // ����������� ����������� �� ������ �����

                System.Double TotalSumColumn = 0;
                CalcSubBudgetRowTotalSum(objParentNode, objColumn, ref TotalSumColumn);
                objParentNode.SetValue(objColumn, TotalSumColumn);

                System.Double TotalSumRow = 0;
                foreach (DevExpress.XtraTreeList.Columns.TreeListColumn objCol in objParentNode.TreeList.Columns)
                {
                    if (isMonthColumn(objCol) == false) { continue; }
                    if (objCol == colSummary) { continue; }

                    TotalSumRow += System.Convert.ToDouble(objParentNode.GetValue(objCol));
                }
                objParentNode.SetValue(colSummary, TotalSumRow);
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                    "������ ��������� �������� ����� � ���������.\n���������: " + 
                    ( System.String )objNode.GetValue( colDEBITARTICLE_NUM ) + 
                    ".\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
            }
            return;
        }
        private void CalcSubBudgetRowTotalSum(DevExpress.XtraTreeList.Nodes.TreeListNode objNode, 
            DevExpress.XtraTreeList.Columns.TreeListColumn objColumn, ref System.Double TotalSum )
        {

            try
            {
                if ((objNode == null) || (objNode.Tag == null)) { return; }

                if (isMonthColumn(objColumn) == true) 
                {
                    TotalSum += GetDecodeMoneyPlanValue(objNode, objColumn);
                }

                // �������� �������� ����
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objChildNode in objNode.Nodes)
                {
                    CalcSubBudgetRowTotalSum(objChildNode, objColumn, ref TotalSum);
                }
            }
            catch (System.Exception f)
            {
                TotalSum = 0;
                System.Windows.Forms.MessageBox.Show(this,
                    "������ ������������ ������ ��������� �������� ����� ��� ���������.\n���������: " +
                    (System.String)objNode.GetValue(colDEBITARTICLE_NUM) +
                    ".\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }
        #endregion

        /// <summary>
        /// �������� �������� ����� ��� ������������ ������ 
        /// </summary>
        /// <param name="objNode">����, ������� ������� � ������������ ������</param>
        private void CalcRowTotalSum(DevExpress.XtraTreeList.Nodes.TreeListNode objNode)
        {
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            if (treeList.Nodes.Count == 0) { return; }

            try
            {
                // ������ ��� ���� ������ ����� ������� ������������ ����
                DevExpress.XtraTreeList.Nodes.TreeListNode objParentNode = null;
                if (objNode.ParentNode == null) { objParentNode = objNode; }
                else
                {
                    objParentNode = objNode.ParentNode;
                    while (objParentNode.ParentNode != null)
                    {
                        objParentNode = objParentNode.ParentNode;
                    }
                }

                if (objParentNode.ParentNode != null)
                {
                    System.Windows.Forms.MessageBox.Show(this, "��� ������ " +
                        (System.String)objNode.GetValue(colDEBITARTICLE_NUM) + " �� ������� ����� ������������ ������.", "������",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }

                // ����������� �������� ������ �� ������
                TotalRow objTotalRow = this.m_objTotalRowList[treeList.GetNodeIndex(objParentNode)];
                if (objTotalRow == null)
                {
                    System.Windows.Forms.MessageBox.Show(this, "��� ������ " +
                        (System.String)objParentNode.GetValue(colDEBITARTICLE_NUM) + " �� ������� ����� ������ � �������� ������.", "������",
                      System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }

                // ����������� �������� ������ ��� ������ �������� (��������)
                objTotalRow.ClearTotal();
                CalcRowTotalSum(objParentNode, objTotalRow);

                // � ������� "�����" �������� ����� �� ������
                System.String strText = System.String.Format("{0:### ### ##0.00}", objTotalRow.GetSumItogo());
                strText += " " + this.m_objBudget.Currency.CurrencyCode + " ";

                objParentNode.SetValue(colSummary, strText);

                // ������ ���������� ����� ��������� ������, � ������ ������ ���������
                if (objNode.ParentNode != null)
                {
                    CalcSubBudgetRowTotalSum(objNode, objNode.TreeList.FocusedColumn);
                }
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                    "������ ��������� �������� �����.\n������: " +
                    (System.String)objNode.GetValue(colDEBITARTICLE_NUM) +
                    ".\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {

                this.tableLayoutPanel1.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            }
            return;
        }
        /// <summary>
        /// ����������� ����� ��� �������� �������� ����� �� ������
        /// </summary>
        /// <param name="objNode">����</param>
        /// <param name="objTotalRow">�������� ������</param>
        private void CalcRowTotalSum( DevExpress.XtraTreeList.Nodes.TreeListNode objNode,
            TotalRow objTotalRow )
        {
            try
            {
                if( ( objNode == null ) || ( objNode.Tag == null ) ) { return; }

                // ��������� �� �������� � ���������
                System.Double MoneyPlan = 0;
                System.Double SumItogo = 0;
                foreach( DevExpress.XtraTreeList.Columns.TreeListColumn objColumn in objNode.TreeList.Columns )
                {
                    if( isMonthColumn( objColumn ) == false ) { continue; }
                    // ����� � ������ ������� 
                    MoneyPlan = GetDecodeMoneyPlanValue( objNode, objColumn );
                    SumItogo += MoneyPlan;
                    // ��������� ��� ����� � �������� ������
                    objTotalRow.AppendColumnValue( objColumn, MoneyPlan );
                }

                if( SumItogo == 0 )
                {
                    objNode.SetValue(colSummary, null);
                }
                else
                {
                    System.String strText = System.String.Format( "{0:### ### ##0.00}", SumItogo );
                    strText += " " + this.m_objBudget.Currency.CurrencyCode + " ";
                    objNode.SetValue( colSummary, strText );
                }

                // �������� �������� ����
                foreach( DevExpress.XtraTreeList.Nodes.TreeListNode objChildNode in objNode.Nodes )
                {
                    CalcRowTotalSum( objChildNode, objTotalRow );
                }
            }
            catch( System.Exception f )
            {
                objTotalRow.ClearTotal();
                System.Windows.Forms.MessageBox.Show( this,
                    "������ ������������ ������ ��������� �������� �����.\n������: " + 
                    ( System.String )objNode.GetValue( colDEBITARTICLE_NUM ) + 
                    ".\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
            }
            return;
        }
        #endregion

        #region �������� ������� ���������
        /// <summary>
        /// ������������� ����� ������ �������� � �������� ��������� 
        /// </summary>
        /// <param name="objNode">���� ������</param>
        private void RecalcDebitArticleNumber( DevExpress.XtraTreeList.Nodes.TreeListNode objNode )
        {
            try
            {
                if( ( objNode == null ) || ( objNode.Tag == null ) ) { return; }

                // ������ ����� ���������� ����� �����
                System.String strDebitArticleNum = "";
                if( ( ( ERP_Budget.Common.CBudgetItem )objNode.Tag ).DebitArticleID.CompareTo( System.Guid.Empty ) != 0 )
                {
                    // ������ ��������
                    strDebitArticleNum = ( ( ERP_Budget.Common.CBudgetItem )objNode.Tag ).BudgetItemNum;
                }
                else
                {
                    DevExpress.XtraTreeList.Nodes.TreeListNode objParentNode = objNode.ParentNode;
                    // ������������ ����� �������� ���������
                    strDebitArticleNum = ( ( ERP_Budget.Common.CBudgetItem )objParentNode.Tag ).BudgetItemNum + 
                        "." + ( treeList.GetNodeIndex( objNode ) + 1 ).ToString();
                }
                // ����������� � ���� � ������ �������� ����� �����
                if( ( ( ERP_Budget.Common.CBudgetItem )objNode.Tag ).BudgetItemNum != strDebitArticleNum )
                {
                    ( ( ERP_Budget.Common.CBudgetItem )objNode.Tag ).BudgetItemNum = strDebitArticleNum;
                    ( ( ERP_Budget.Common.CBudgetItem )objNode.Tag ).BudgetItemID = treeList.GetNodeIndex( objNode );
                    ( ( ERP_Budget.Common.CBudgetItem )objNode.Tag ).ReadOnly = true;
                    objNode.SetValue( colDEBITARTICLE_NUM, ( ( ERP_Budget.Common.CBudgetItem )objNode.Tag ).BudgetItemFullName );
                    objNode.SetValue( colREADONLY, true );
                }

                if( objNode.HasChildren )
                {
                    // �������� �� �������� ����� � ����������� �� ������
                    foreach( DevExpress.XtraTreeList.Nodes.TreeListNode objNodeChild in objNode.Nodes )
                    {
                        RecalcDebitArticleNumber( objNodeChild );
                    }
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( null, "������ ��������� ������� ������ ��������.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        /// <summary>
        /// �������� ���� ������� ������
        /// </summary>
        private void RecalcAllDebitArticleNumber()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                for( System.Int32 i = 0; i < treeList.Nodes.Count; i++ )
                {
                    RecalcDebitArticleNumber( treeList.Nodes[ i ] );
                }
                SetModified( true );
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( null, "������ ��������� ������� ������ ��������.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return;
        }


        #endregion

        #region ������
        private void barBtnPrint_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //ExportToExcel();
                SendMessageToLog( "���� ������� ������ � MS Excel..." );
                StartThreadWithLoadData();
                Cursor.Current = Cursors.Default;

            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ ������\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        private System.Int32 m_LastIndxRowForPrint;
        /// <summary>
        /// ������� ������� � MS Excel
        /// </summary>
        private void ExportToExcel()
        {
            Excel.Application oXL = null;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            //Excel.Range oRng;

            try
            {
                barBtnPrint.Enabled = false;
                //Start Excel and get Application object.
                oXL = new Excel.Application();
                //oXL.Visible = true;

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.Worksheets[1];

                //Add table headers going cell by cell.
                oSheet.Name = "������";
                oSheet.Cells[1, 1] = lblInfo.Text.Replace( '\t', ' ' );
                oSheet.Cells[2, 1] = lblCurrencyRate.Text.Replace('\t', ' '); ;
                // ����� ��������
                oSheet.Cells[3, 1] = "������ ��������";
                oSheet.Cells[3, 2] = "������";
                oSheet.Cells[3, 3] = "�������";
                oSheet.Cells[3, 4] = "����";
                oSheet.Cells[3, 5] = "������";
                oSheet.Cells[3, 6] = "���";
                oSheet.Cells[3, 7] = "����";
                oSheet.Cells[3, 8] = "����";
                oSheet.Cells[3, 9] = "������";
                oSheet.Cells[3, 10] = "��������";
                oSheet.Cells[3, 11] = "�������";
                oSheet.Cells[3, 12] = "������";
                oSheet.Cells[3, 13] = "�������";

                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A3", "M3").Font.Size = 12;
                oSheet.get_Range("A3", "M3").Font.Bold = true;
                oSheet.get_Range("A3", "M3").VerticalAlignment =
                    Excel.XlVAlign.xlVAlignCenter;

                // ��������� �� ������ � ��������� �����������
                System.Boolean bRet = false;
                m_LastIndxRowForPrint = 3;
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeList.Nodes)
                {
                    // ����������� ����� ������ ������ �����������
                    oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint+1, 1], oSheet.Cells[m_LastIndxRowForPrint+1, 1]).Font.Bold = true;
                    bRet = ExportBudgetItemList(objNode, oSheet);
                    if (bRet == false) { break; }
                }
                oSheet.get_Range("A3", "M3").EntireColumn.AutoFit();


                oSheet = (Excel._Worksheet)oWB.Worksheets[2];

                //Add table headers going cell by cell.
                oSheet.Name = "������ (�������� � EUR)";
                oSheet.Cells[1, 1] = lblInfo.Text.Replace('\t', ' ');
                //oSheet.Cells[2, 1] = lblCurrencyRate.Text.Replace('\t', ' '); ;
                // ����� ��������
                oSheet.Cells[3, 1] = "������ ��������";
                oSheet.Cells[2, 2] = "������";
                oSheet.Cells[2, 6] = "�������";
                oSheet.Cells[2, 10] = "����";
                oSheet.Cells[2, 14] = "������";
                oSheet.Cells[2, 18] = "���";
                oSheet.Cells[2, 22] = "����";
                oSheet.Cells[2, 26] = "����";
                oSheet.Cells[2, 30] = "������";
                oSheet.Cells[2, 34] = "��������";
                oSheet.Cells[2, 38] = "�������";
                oSheet.Cells[2, 42] = "������";
                oSheet.Cells[2, 46] = "�������";
                oSheet.Cells[2, 50] = "�����";

                System.Int32 k = 1;
                System.Int32 iCurColumnIndx = 2;
                for (System.Int32 j = 1; j <= 13; j++)
                {
                    k = 1;
                    while (k < 5)
                    {
                        switch (k)
                        {
                            case 1:
                                oSheet.Cells[3, iCurColumnIndx] = "����";
                                break;
                            case 2:
                                oSheet.Cells[3, iCurColumnIndx] = "������";
                                break;
                            case 3:
                                oSheet.Cells[3, iCurColumnIndx] = "������";
                                break;
                            case 4:
                                oSheet.Cells[3, iCurColumnIndx] = "�������";
                                break;
                            default:
                                oSheet.Cells[3, iCurColumnIndx] = "";
                                break;
                        }

                        k++;
                        iCurColumnIndx++;
                    }
                }


                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A2", "AZ4").Font.Size = 12;
                oSheet.get_Range("A2", "AZ4").Font.Bold = true;
                oSheet.get_Range("A2", "AZ4").VerticalAlignment =  Excel.XlVAlign.xlVAlignCenter;

                // ��������� �� ������ � �������� �����������
                bRet = false;
                m_LastIndxRowForPrint = 3;
                List<System.Int32> objParentSubItemsIndxList = new List<int>();
                List<System.Int32> objParentItemsIndxList = new List<int>();
                List<System.Int32> objInxForParentNodeSumList = new List<int>();
                System.Int32 iStartIndx = m_LastIndxRowForPrint;
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeList.Nodes)
                {
                    objInxForParentNodeSumList.Clear();
                    iStartIndx = m_LastIndxRowForPrint + 1;

                    if (objNode.HasChildren == true)
                    {
                        objParentItemsIndxList.Add(m_LastIndxRowForPrint);
                    }
                    // ����������� ����� ������ ������ �����������
                    oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint + 1, 1], oSheet.Cells[m_LastIndxRowForPrint + 1, 1]).Font.Bold = true;
                    bRet = ExportBudgetItemList2(objNode, oSheet, objParentSubItemsIndxList, objInxForParentNodeSumList);
                    if (bRet == false) { break; }
                    else
                    {
                        if ((objNode.ParentNode == null) && (objNode.HasChildren == true) && (objInxForParentNodeSumList != null) && (objInxForParentNodeSumList.Count > 0))
                        {
                            System.String strFormula = "=";
                            for (System.Int32 i = 0; i < objInxForParentNodeSumList.Count; i++)
                            {
                                strFormula += "R[" + (objInxForParentNodeSumList[i] - iStartIndx + 1).ToString() + "]C";
                                if (i < (objInxForParentNodeSumList.Count - 1))
                                {
                                    strFormula += " + ";
                                }
                            }

                            oSheet.get_Range(oSheet.Cells[iStartIndx, 1], oSheet.Cells[iStartIndx, 1]).Borders.LineStyle = Excel.XlBordersIndex.xlEdgeBottom;
                            foreach (System.Int32 iSubItem in objInxForParentNodeSumList)
                            {
                                for (System.Int32 iCol = 2; iCol < 54; iCol++)
                                {
                                    oSheet.get_Range(oSheet.Cells[iStartIndx, iCol], oSheet.Cells[iStartIndx, iCol]).Formula = strFormula;
                                    oSheet.get_Range(oSheet.Cells[iStartIndx, iCol], oSheet.Cells[iStartIndx, iCol]).NumberFormat = "# ##0,00";
                                    oSheet.get_Range(oSheet.Cells[iStartIndx, iCol], oSheet.Cells[iStartIndx, iCol]).Font.Size = 12;
                                    oSheet.get_Range(oSheet.Cells[iStartIndx, iCol], oSheet.Cells[iStartIndx, iCol]).Font.Bold = true;
                                    oSheet.get_Range(oSheet.Cells[iStartIndx, iCol], oSheet.Cells[iStartIndx, iCol]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                                    oSheet.get_Range(oSheet.Cells[iStartIndx, iCol], oSheet.Cells[iStartIndx, iCol]).Borders.LineStyle = Excel.XlBordersIndex.xlEdgeBottom;
                                }

                            }
                        }
                        else
                        {
                            if ((objNode.ParentNode == null) && (objNode.HasChildren == false))
                            {
                                oSheet.get_Range(oSheet.Cells[iStartIndx, 1], oSheet.Cells[iStartIndx, 1]).Borders.LineStyle = Excel.XlBordersIndex.xlEdgeBottom;
                                for (System.Int32 iCol = 2; iCol < 54; iCol++)
                                {
                                    oSheet.get_Range(oSheet.Cells[iStartIndx, iCol], oSheet.Cells[iStartIndx, iCol]).NumberFormat = "# ##0,00";
                                    oSheet.get_Range(oSheet.Cells[iStartIndx, iCol], oSheet.Cells[iStartIndx, iCol]).Font.Size = 12;
                                    oSheet.get_Range(oSheet.Cells[iStartIndx, iCol], oSheet.Cells[iStartIndx, iCol]).Font.Bold = true;
                                    oSheet.get_Range(oSheet.Cells[iStartIndx, iCol], oSheet.Cells[iStartIndx, iCol]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                                    oSheet.get_Range(oSheet.Cells[iStartIndx, iCol], oSheet.Cells[iStartIndx, iCol]).Borders.LineStyle = Excel.XlBordersIndex.xlEdgeBottom;
                                }

                                oSheet.get_Range(oSheet.Cells[iStartIndx, 50], oSheet.Cells[iStartIndx, 54]).Font.Size = 12;
                                oSheet.get_Range(oSheet.Cells[iStartIndx, 50], oSheet.Cells[iStartIndx, 54]).Font.Bold = true;

                                oSheet.get_Range(oSheet.Cells[iStartIndx, 50], oSheet.Cells[iStartIndx, 50]).Formula = "=����(RC[-48];RC[-44];RC[-40];RC[-36];RC[-32];RC[-28];RC[-24];RC[-20];RC[-16];RC[-12];RC[-8];RC[-4])";
                                oSheet.get_Range(oSheet.Cells[iStartIndx, 51], oSheet.Cells[iStartIndx, 51]).Formula = "=����(RC[-48];RC[-44];RC[-40];RC[-36];RC[-32];RC[-28];RC[-24];RC[-20];RC[-16];RC[-12];RC[-8];RC[-4])";
                                oSheet.get_Range(oSheet.Cells[iStartIndx, 52], oSheet.Cells[iStartIndx, 52]).Formula = "=����(RC[-48];RC[-44];RC[-40];RC[-36];RC[-32];RC[-28];RC[-24];RC[-20];RC[-16];RC[-12];RC[-8];RC[-4])";
                                oSheet.get_Range(oSheet.Cells[iStartIndx, 53], oSheet.Cells[iStartIndx, 53]).Formula = "=����(RC[-48];RC[-44];RC[-40];RC[-36];RC[-32];RC[-28];RC[-24];RC[-20];RC[-16];RC[-12];RC[-8];RC[-4])";

                                oSheet.get_Range(oSheet.Cells[iStartIndx, 50], oSheet.Cells[iStartIndx, 54]).NumberFormat = "# ##0,00";
                                oSheet.get_Range(oSheet.Cells[iStartIndx, 50], oSheet.Cells[iStartIndx, 54]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                                oSheet.get_Range(oSheet.Cells[iStartIndx, 50], oSheet.Cells[iStartIndx, 54]).Borders.LineStyle = Excel.XlBordersIndex.xlEdgeBottom;
                            }
                        }
                    }
                }
                // ����� �������� �����
                oSheet.Cells[m_LastIndxRowForPrint + 1, 1] = "�����: ";
                oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint + 1, 2], oSheet.Cells[m_LastIndxRowForPrint + 1, 54]).NumberFormat = "# ##0,00";
                oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint + 1, 1], oSheet.Cells[m_LastIndxRowForPrint + 1, 54]).Font.Bold = true;
                oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint + 1, 1], oSheet.Cells[m_LastIndxRowForPrint + 1, 54]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                for (System.Int32 i = 2; i < 54; i++)
                {
                    System.String strFormula = "=����(R[-" + ( m_LastIndxRowForPrint - 3 ).ToString() + "]C:R[-1]C)";
                    if ((objParentSubItemsIndxList != null) && (objParentSubItemsIndxList.Count > 0))
                    {
                        foreach (System.Int32 iParentSubItemsIndx in objParentSubItemsIndxList)
                        {
                            strFormula += ("-R[-" + ( ( m_LastIndxRowForPrint - iParentSubItemsIndx ) + 1).ToString() + "]C");
                        }
                        if ((objParentItemsIndxList != null) && (objParentItemsIndxList.Count > 0))
                        {
                            foreach (System.Int32 iParentItemsIndx in objParentItemsIndxList)
                            {
                                strFormula += ("-R[-" + (m_LastIndxRowForPrint - iParentItemsIndx).ToString() + "]C");
                            }
                        }
                    }
                    oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint + 1, i], oSheet.Cells[m_LastIndxRowForPrint + 1, i]).FormulaR1C1 = strFormula;
                }

                oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint + 1, 50], oSheet.Cells[m_LastIndxRowForPrint + 1, 50]).Formula = "=����(RC[-48];RC[-44];RC[-40];RC[-36];RC[-32];RC[-28];RC[-24];RC[-20];RC[-16];RC[-12];RC[-8];RC[-4])";
                oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint + 1, 51], oSheet.Cells[m_LastIndxRowForPrint + 1, 51]).Formula = "=����(RC[-48];RC[-44];RC[-40];RC[-36];RC[-32];RC[-28];RC[-24];RC[-20];RC[-16];RC[-12];RC[-8];RC[-4])";
                oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint + 1, 52], oSheet.Cells[m_LastIndxRowForPrint + 1, 52]).Formula = "=����(RC[-48];RC[-44];RC[-40];RC[-36];RC[-32];RC[-28];RC[-24];RC[-20];RC[-16];RC[-12];RC[-8];RC[-4])";
                oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint + 1, 53], oSheet.Cells[m_LastIndxRowForPrint + 1, 53]).Formula = "=����(RC[-48];RC[-44];RC[-40];RC[-36];RC[-32];RC[-28];RC[-24];RC[-20];RC[-16];RC[-12];RC[-8];RC[-4])";
                oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint + 1, 50], oSheet.Cells[m_LastIndxRowForPrint + 1, 53]).NumberFormat = "# ##0,00";
                oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint + 1, 50], oSheet.Cells[m_LastIndxRowForPrint + 1, 54]).Font.Bold = true;
                oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint + 1, 50], oSheet.Cells[m_LastIndxRowForPrint + 1, 54]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                iCurColumnIndx = 2;
                for (System.Int32 l = 1; l <= 14; l++ )
                {
                    oSheet.get_Range(oSheet.Cells[2, iCurColumnIndx], oSheet.Cells[m_LastIndxRowForPrint + 1, iCurColumnIndx + 3]).Borders.Item[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                    oSheet.get_Range(oSheet.Cells[2, iCurColumnIndx], oSheet.Cells[m_LastIndxRowForPrint + 1, iCurColumnIndx + 3]).Borders.Item[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlMedium;

                    oSheet.get_Range(oSheet.Cells[2, iCurColumnIndx], oSheet.Cells[m_LastIndxRowForPrint + 1, iCurColumnIndx + 3]).Borders.Item[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                    oSheet.get_Range(oSheet.Cells[2, iCurColumnIndx], oSheet.Cells[m_LastIndxRowForPrint + 1, iCurColumnIndx + 3]).Borders.Item[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlMedium;

                    oSheet.get_Range(oSheet.Cells[2, iCurColumnIndx], oSheet.Cells[m_LastIndxRowForPrint + 1, iCurColumnIndx + 3]).Borders.Item[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                    oSheet.get_Range(oSheet.Cells[2, iCurColumnIndx], oSheet.Cells[m_LastIndxRowForPrint + 1, iCurColumnIndx + 3]).Borders.Item[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlMedium;

                    oSheet.get_Range(oSheet.Cells[2, iCurColumnIndx], oSheet.Cells[m_LastIndxRowForPrint + 1, iCurColumnIndx + 3]).Borders.Item[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    oSheet.get_Range(oSheet.Cells[2, iCurColumnIndx], oSheet.Cells[m_LastIndxRowForPrint + 1, iCurColumnIndx + 3]).Borders.Item[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlMedium;
                    iCurColumnIndx = 2 + (l - 1) * 4;
                }

                oSheet.get_Range("A3", "AZ4").EntireColumn.AutoFit();

                oSheet.Outline.AutomaticStyles = false;
                oSheet.get_Range( "A1", "A1" ).AutoOutline();
                oSheet.Outline.ShowLevels( 1, Missing.Value);

                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch( System.Exception f )
            { 
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "������ �������� � MS Excel.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
                barBtnPrint.Enabled = true;
                this.Invoke(m_SendMessageToLogDelegate, new Object[] { "������� ������ ��������" });
                EventStopThread.Set();
            }
        }
        private System.Boolean ExportBudgetItemList(DevExpress.XtraTreeList.Nodes.TreeListNode objNode, Excel._Worksheet oSheet)
        {
            System.Boolean bRet = false;

            // ������ ������ �������� �� ������ ���� ������
            if ((objNode == null) || (objNode.Tag == null))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "���� ������ �� �������� ���������� � ������ �������.", "��������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                // ����������� ������ �������    
                ERP_Budget.Common.CBudgetItem objBudgetItem = (ERP_Budget.Common.CBudgetItem)objNode.Tag;
                m_LastIndxRowForPrint++;


                // �������� ����������� 
                bRet = true;
                oSheet.Cells[m_LastIndxRowForPrint, 1] = objBudgetItem.BudgetItemFullName;
                System.Int32 iColumnIndx = 0;
                System.String strCurrency = "";
                System.String strDscrpn = "";
                foreach( ERP_Budget.Common.CBudgetItemDecode BudgetItemDecode in objBudgetItem.BudgetItemDecodeList )
                {
                    iColumnIndx = System.Convert.ToInt32( BudgetItemDecode.Month ) + 1;
                    strCurrency = (BudgetItemDecode.Currency == null) ? "" : BudgetItemDecode.Currency.CurrencyCode;
                    strDscrpn = (BudgetItemDecode.Description == "") ? "" : BudgetItemDecode.Description;
                    if (BudgetItemDecode.MoneyPlan > 0)
                    {
                        oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx] = System.String.Format("{0:### ### ##0.00}", BudgetItemDecode.MoneyPlan) + " " + strCurrency;
                        oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx], oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    }
                    if (strDscrpn != "")
                    {
                        oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx], oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx]).AddComment(strDscrpn);
                        oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx], oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx]).Comment.Visible = false;
                    }
                    oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx], oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx]).NumberFormat = "@";
                }

                if (bRet == true)
                {
                    // �������� �������� ����
                    foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objChildNode in objNode.Nodes)
                    {
                        bRet = ExportBudgetItemList(objChildNode, oSheet);
                        if (bRet == false) { break; }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ���������� ������������ ������.\n�� ������� ��������� ����������� ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }

            return bRet;
        }
        private System.Boolean ExportBudgetItemList2(DevExpress.XtraTreeList.Nodes.TreeListNode objNode,
            Excel._Worksheet oSheet, List<System.Int32> ParentSubItemsIndxList,
            List<System.Int32> InxForParentNodeSumList)
        {
            System.Boolean bRet = false;

            // ������ ������ �������� �� ������ ���� ������
            if ((objNode == null) || (objNode.Tag == null))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "���� ������ �� �������� ���������� � ������ �������.", "��������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                // ����������� ������ �������    
                ERP_Budget.Common.CBudgetItem objBudgetItem = (ERP_Budget.Common.CBudgetItem)objNode.Tag;
                m_LastIndxRowForPrint++;


                // �������� ����������� 
                bRet = true;
                oSheet.Cells[m_LastIndxRowForPrint, 1] = objBudgetItem.BudgetItemFullName;
                System.Int32 iColumnIndx = 0;
                System.String strCurrency = "";
                System.String strDscrpn = "";
                System.Double MoneyPlan = 0;
                System.Double SumMoneyPlan = 0;
                System.Double CurrencyRate = 0;
                foreach (ERP_Budget.Common.CBudgetItemDecode BudgetItemDecode in objBudgetItem.BudgetItemDecodeList)
                {
                    iColumnIndx = 2 + (( System.Convert.ToInt32(BudgetItemDecode.Month) - 1 ) * 4);
                    strCurrency = (BudgetItemDecode.Currency == null) ? "" : BudgetItemDecode.Currency.CurrencyCode;
                    strDscrpn = (BudgetItemDecode.Description == "") ? "" : BudgetItemDecode.Description;
                    if (BudgetItemDecode.MoneyPlan > 0)
                    {
                        if (m_objBudget.IsAccept == true)
                        {
                            oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx] = BudgetItemDecode.MoneyPlanAccept;
                            oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx + 1] = BudgetItemDecode.MoneyFact;
                            oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx + 2] = BudgetItemDecode.MoneyReserve;
                            oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx + 3] = ( BudgetItemDecode.MoneyPermit - BudgetItemDecode.MoneyFact - BudgetItemDecode.MoneyReserve + BudgetItemDecode.MoneyCredit );
                        }
                        else
                        {
                            // ����� ����� �� �����������
                            MoneyPlan = BudgetItemDecode.MoneyPlan;
                            // ���� ������ ����������� � ������ ����� 
                            CurrencyRate = this.m_objBudget.BudgetCurrencyRate.GetCurrencyRate(BudgetItemDecode.Currency.uuidID, m_objBudget.Currency.uuidID);
                            // �������� ����� � ������ �������
                            oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx] = MoneyPlan * CurrencyRate;
                            oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx + 1] = BudgetItemDecode.MoneyFact;
                            oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx + 2] = BudgetItemDecode.MoneyReserve;
                            oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx + 3] = 0;

                            SumMoneyPlan += (MoneyPlan * CurrencyRate);
                        }
                        oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx], oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx + 3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    }
                    if (strDscrpn != "")
                    {
                        oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx], oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx]).AddComment(strDscrpn);
                        oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx], oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx]).Comment.Visible = false;
                    }
                    oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx], oSheet.Cells[m_LastIndxRowForPrint, iColumnIndx+3]).NumberFormat = "# ##0,00";
                }
                if (SumMoneyPlan > 0)
                {
                    // �������� ����� �� ������
                    oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint, 50], oSheet.Cells[m_LastIndxRowForPrint, 50]).Formula = "=����(RC[-48];RC[-44];RC[-40];RC[-36];RC[-32];RC[-28];RC[-24];RC[-20];RC[-16];RC[-12];RC[-8];RC[-4])";
                    oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint, 51], oSheet.Cells[m_LastIndxRowForPrint, 51]).Formula = "=����(RC[-48];RC[-44];RC[-40];RC[-36];RC[-32];RC[-28];RC[-24];RC[-20];RC[-16];RC[-12];RC[-8];RC[-4])";
                    oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint, 52], oSheet.Cells[m_LastIndxRowForPrint, 52]).Formula = "=����(RC[-48];RC[-44];RC[-40];RC[-36];RC[-32];RC[-28];RC[-24];RC[-20];RC[-16];RC[-12];RC[-8];RC[-4])";
                    oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint, 53], oSheet.Cells[m_LastIndxRowForPrint, 53]).Formula = "=����(RC[-48];RC[-44];RC[-40];RC[-36];RC[-32];RC[-28];RC[-24];RC[-20];RC[-16];RC[-12];RC[-8];RC[-4])";
                    oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint, 50], oSheet.Cells[m_LastIndxRowForPrint, 53]).NumberFormat = "# ##0,00";
                    oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint, 50], oSheet.Cells[m_LastIndxRowForPrint, 53]).Font.Bold = true;
                    oSheet.get_Range(oSheet.Cells[m_LastIndxRowForPrint, 50], oSheet.Cells[m_LastIndxRowForPrint, 53]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                }
                
                if (bRet == true)
                {
                    System.Boolean bIsParentSubItem = false;
                    System.Int32 iStartIndx = 0;
                    if ((objNode.ParentNode != null) && (objNode.ParentNode.ParentNode == null))
                    {
                        //� ��� ��������� �������� ������
                        bIsParentSubItem = true;
                        iStartIndx = m_LastIndxRowForPrint;
                    }
                    // �������� �������� ����
                    foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objChildNode in objNode.Nodes)
                    {
                        if (objNode.ParentNode == null)
                        {
                            InxForParentNodeSumList.Add(m_LastIndxRowForPrint);
                        }
                        bRet = ExportBudgetItemList2(objChildNode, oSheet, ParentSubItemsIndxList, InxForParentNodeSumList);
                        if (bRet == false) { break; }
                    }
                    if (bRet == true)
                    {
                        if (bIsParentSubItem == true)
                        {
                            if (m_LastIndxRowForPrint != iStartIndx)
                            {
                                System.Int32 iDiffIndx = m_LastIndxRowForPrint - iStartIndx;
                                for (System.Int32 iCol = 2; iCol < 54; iCol++)
                                {
                                    oSheet.get_Range(oSheet.Cells[iStartIndx, iCol], oSheet.Cells[iStartIndx, iCol]).Formula = "=����(R[1]C:R[" + iDiffIndx.ToString() + "]C)";
                                    oSheet.get_Range(oSheet.Cells[iStartIndx, iCol], oSheet.Cells[iStartIndx, iCol]).NumberFormat = "# ##0,00";
                                    oSheet.get_Range(oSheet.Cells[iStartIndx, iCol], oSheet.Cells[iStartIndx, iCol]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                                }
                                ParentSubItemsIndxList.Add(iStartIndx);
                            }

                            oSheet.get_Range(oSheet.Cells[iStartIndx, 50], oSheet.Cells[iStartIndx, 50]).Formula = "=����(RC[-48];RC[-44];RC[-40];RC[-36];RC[-32];RC[-28];RC[-24];RC[-20];RC[-16];RC[-12];RC[-8];RC[-4])";
                            oSheet.get_Range(oSheet.Cells[iStartIndx, 51], oSheet.Cells[iStartIndx, 51]).Formula = "=����(RC[-48];RC[-44];RC[-40];RC[-36];RC[-32];RC[-28];RC[-24];RC[-20];RC[-16];RC[-12];RC[-8];RC[-4])";
                            oSheet.get_Range(oSheet.Cells[iStartIndx, 52], oSheet.Cells[iStartIndx, 52]).Formula = "=����(RC[-48];RC[-44];RC[-40];RC[-36];RC[-32];RC[-28];RC[-24];RC[-20];RC[-16];RC[-12];RC[-8];RC[-4])";
                            oSheet.get_Range(oSheet.Cells[iStartIndx, 53], oSheet.Cells[iStartIndx, 53]).Formula = "=����(RC[-48];RC[-44];RC[-40];RC[-36];RC[-32];RC[-28];RC[-24];RC[-20];RC[-16];RC[-12];RC[-8];RC[-4])";
                            oSheet.get_Range(oSheet.Cells[iStartIndx, 50], oSheet.Cells[iStartIndx, 53]).NumberFormat = "# ##0,00";

                            oSheet.get_Range(oSheet.Cells[iStartIndx, 1], oSheet.Cells[iStartIndx, 54]).Font.Size = 11;
                            oSheet.get_Range(oSheet.Cells[iStartIndx, 1], oSheet.Cells[iStartIndx, 54]).Font.Bold = true;
                            oSheet.get_Range(oSheet.Cells[iStartIndx, 1], oSheet.Cells[iStartIndx, 54]).Font.Italic = true;
                            oSheet.get_Range(oSheet.Cells[iStartIndx, 50], oSheet.Cells[iStartIndx, 54]).Font.Bold = true;
                            oSheet.get_Range(oSheet.Cells[iStartIndx, 50], oSheet.Cells[iStartIndx, 54]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                        }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ���������� ������������ ������.\n�� ������� ��������� ����������� ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }

            return bRet;
        }
        #endregion

        #region ��������� ������
        /// <summary>
        /// ������������� ������� "���������"
        /// </summary>
        /// <param name="bAccept">true - ���������; false - �������� ������� "���������"</param>
        /// <returns></returns>
        System.Boolean bSetAcceptBudget( System.Boolean bAccept )
        {
            System.Boolean bRet = false;
            try
            {
                // ������ ������������ ������, � ������� ���� �������� ���������
                List<ERP_Budget.Common.CBudgetItem> objParentBudgetItemList = new List<ERP_Budget.Common.CBudgetItem>();
                foreach( DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeList.Nodes )
                {
                    if( objNode.HasChildren == false ) { continue; }

                    // � ����� ���� ���� �������� ����, 
                    // ������� ������ "������ �������" � ��������� ��� ����������� ���������� �� ������ �������� ���� �� �����
                    ERP_Budget.Common.CBudgetItem objBudgetItemParent = ( ERP_Budget.Common.CBudgetItem )objNode.Tag;
                    ERP_Budget.Common.CBudgetItem objBudgetItem = ERP_Budget.Common.CBudgetItem.Copy( objBudgetItemParent );

                    TotalRow objTotalRow = this.m_objTotalRowList[ treeList.GetNodeIndex( objNode ) ];

                    foreach( DevExpress.XtraTreeList.Columns.TreeListColumn objColumn in treeList.Columns )
                    {
                        if( isMonthColumn( objColumn ) )
                        {
                            // �����
                            double TotalSum = objTotalRow.GetColumnValue( objColumn );

                            if( TotalSum > 0 )
                            {
                                objBudgetItem.GetBudgetItemDecode( GetMonthEnum( objColumn ) ).Currency = new ERP_Budget.Common.CCurrency(
                                    this.m_objBudget.Currency.uuidID, this.m_objBudget.Currency.CurrencyCode, this.m_objBudget.Currency.Name );
                                objBudgetItem.GetBudgetItemDecode( GetMonthEnum( objColumn ) ).MoneyPlan = TotalSum;

                            }
                        }
                    }

                    objParentBudgetItemList.Add( objBudgetItem );
                }

                bRet = m_objBudget.Accept( m_objProfile, bAccept, objParentBudgetItemList );

                CheckBudgetState();
            }
            catch( System.Exception f )
            {
                bRet = false;
                System.Windows.Forms.MessageBox.Show( this, "������ ��������� �������� \"���������\"\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }


        private void barBtnAccept_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            try
            {
                System.Boolean bNewAccept = !( m_objBudget.IsAccept );
                if( bNewAccept )
                {
                    if( System.Windows.Forms.MessageBox.Show( this, "��������� ������?", "�������������",
                       System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question ) == DialogResult.Yes )
                    {
                        bSetAcceptBudget( bNewAccept );
                    }
                }
                else
                {
                    if( System.Windows.Forms.MessageBox.Show( this, "�������� ��������� ������� '���������' ?", "�������������",
                       System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question ) == DialogResult.Yes )
                    {
                        bSetAcceptBudget( bNewAccept );
                    }
                }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ ��������� �������� '���������'\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }

        #endregion

        #region ������ ��������
        private void mitemTrnList_Click( object sender, EventArgs e )
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                frmTrnList objfrmTrnList = new frmTrnList( this.m_objProfile, ( ERP_Budget.Common.CBudgetItem )treeList.FocusedNode.Tag );
                objfrmTrnList.ShowDialog();
                objfrmTrnList.Dispose();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ �������� ������� ��������.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }

            return;
        }
        #endregion

        #region ������ ������
        private void mitemDocList_Click( object sender, EventArgs e )
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                frmBudgetDocList objfrmBudgetDocList = new frmBudgetDocList( this.m_objProfile, ( ERP_Budget.Common.CBudgetItem )treeList.FocusedNode.Tag );
                objfrmBudgetDocList.ShowDialog();
                objfrmBudgetDocList.Dispose();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ �������� ������� ������.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }

            return;
        }
        #endregion

        #region ���������
        private void mitemDetail_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                frmBudgetItemDetail objfrmBudgetItemDetail = new frmBudgetItemDetail(m_objProfile, (ERP_Budget.Common.CBudgetItem)treeList.FocusedNode.Tag);
                objfrmBudgetItemDetail.ShowDialog();
                objfrmBudgetItemDetail.Dispose();
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this, "������ ����������� ������ �������.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
        #endregion

        #region ������

        public void StartThreadWithLoadData()
        {
            try
            {
                // �������������� �������
                this.m_EventStopThread = new System.Threading.ManualResetEvent(false);
                this.m_EventThreadStopped = new System.Threading.ManualResetEvent(false);

                // �������������� ��������
                m_LoadAddressDelegate = new LoadAddressDelegate(ExportToExcel);
                m_SendMessageToLogDelegate = new SendMessageToLogDelegate(SendMessageToLog);

                // ������ ������
                StartThread();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("StartThreadWithLoadData().\n\n����� ������: " + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void StartThread()
        {
            try
            {
                // ������ �������� reset
                this.m_EventStopThread.Reset();
                this.m_EventThreadStopped.Reset();

                this.thrAddress = new System.Threading.Thread(WorkerThreadFunction);
                this.thrAddress.Start();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("StartThread().\n\n����� ������: " + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        public void WorkerThreadFunction()
        {
            try
            {
                Run();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("WorkerThreadFunction\n" + e.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            return;
        }

        public void Run()
        {
            try
            {
                //LoadAllLists2();
                ExportToExcel();

                // ���� ����������� ������ ������� ����� ���������, �� ���� �� ������� ���������� ��� ���
                while (this.m_bThreadFinishJob == false)
                {
                    // ��������, � �� ��������� �� ��� ���������
                    if (this.m_EventStopThread.WaitOne(iThreadSleepTime, true))
                    {
                        this.m_EventThreadStopped.Set();
                        break;
                    }
                }

            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Run\n" + e.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            return;
        }
        /// <summary>
        /// ������ ������� � ������������� ���������� �����
        /// </summary>
        public void TreadIsFree()
        {
            try
            {
                this.m_bThreadFinishJob = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("StopPleaseTread() " + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            return;
        }

        private void LoadAllLists2()
        {
            try
            {
                this.Invoke(m_LoadAddressDelegate);
                return;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("LoadAllLists2 " + f.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            return;
        }

        private System.Boolean bIsThreadsActive()
        {
            System.Boolean bRet = false;
            try
            {
                bRet = (
                    ((ThreadAddress != null) && (ThreadAddress.IsAlive == true))
                    );
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("bIsThreadsActive.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return bRet;
        }

        private void CloseThreadInAddressEditor()
        {
            try
            {
                if (bIsThreadsActive() == true)
                {
                    if ((ThreadAddress != null) && (ThreadAddress.IsAlive == true))
                    {
                        EventStopThread.Set();
                    }
                }
                while (bIsThreadsActive() == true)
                {
                    if (System.Threading.WaitHandle.WaitAll( (new System.Threading.ManualResetEvent[] { EventThreadStopped }),  100, true))
                    {
                        break;
                    }
                    Application.DoEvents();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("bIsThreadsActive.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        #endregion

        #region ������ ���������
        private void SendMessageToLog(System.String strMessage)
        {
            try
            {
                m_objMenuItem.SimulateNewMessage(strMessage);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "SendMessageToLog.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region ������ �������������

        /// <summary>
        /// ��������� ������ �������� � ��������
        /// </summary>
        /// <param name="objBudget">������</param>
        /// <param name="objParentNode">���� � �����</param>
        /// <param name="objCheckNode">������� ����</param>
        /// <param name="objBudgetDepList">������ ��������� �������������</param>
        /// <param name="objMenuItem"></param>
        public static void CreateBudgetNode(
            CBudget objBudget,
            System.Windows.Forms.TreeNode objParentNodeWithYear,
            List<CBudgetDep> objBudgetDepList,
            UniXP.Common.MENUITEM objMenuItem)
        {

            if (objParentNodeWithYear == null) { return; }

            try
            {
                if (objBudget != null)
                {
                    // �� ����� ������
                    // ������� ���� � ��������
                    System.Windows.Forms.TreeNode objBudgetNode = new System.Windows.Forms.TreeNode();
                    objBudgetNode.Text = objBudget.Name;
                    objBudgetNode.ImageIndex = objMenuItem.nImage;
                    objBudgetNode.SelectedImageIndex = objMenuItem.nImage;
                    objBudgetNode.Tag = new UniXP.Common.MENUITEM()
                    {
                        enMenuType = objMenuItem.enMenuType,
                        strName = objBudget.Name,
                        lClassID = 1,
                        uuidFarClient = objMenuItem.uuidFarClient,
                        strDescription = objBudget.Name,
                        nImage = objMenuItem.nImage,
                        strDLLName = objMenuItem.strDLLName,
                        enCmdType = objMenuItem.enCmdType,
                        enDLLType = objMenuItem.enDLLType,
                        objProfile = objMenuItem.objProfile,
                        objAdvancedParametr = objBudget
                    };

                    System.Windows.Forms.TreeNode objBudgetDepNode = FindBudgetDepNode(objParentNodeWithYear, objBudget.BudgetDep.Name);

                    if (objBudgetDepNode == null)
                    {
                        objBudgetDepNode = CreateBudgetDepNode(objParentNodeWithYear, objBudget.BudgetDep.Name, objBudgetDepList, objMenuItem);
                    }
                    if (objBudgetDepNode != null)
                    {
                        objBudgetDepNode.Nodes.Add(objBudgetNode);
                    }
                }

            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "CreateBudgetDepNode.\n\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }
            return;
        }

        public static System.Windows.Forms.TreeNode CreateBudgetDepNode(System.Windows.Forms.TreeNode objParentNodeWithYear,
            System.String strBudgetDepName, List<CBudgetDep> objBudgetDepList, UniXP.Common.MENUITEM objMenuItem)
        {
            System.Windows.Forms.TreeNode objRetBudgetDepNode = null;

            try
            {
                if (objParentNodeWithYear == null) { return objRetBudgetDepNode; }

                // ������������� � ���� objBudgetDepNode
                CBudgetDep objCurrentBudgetDep = objBudgetDepList.SingleOrDefault<CBudgetDep>(x => x.Name == strBudgetDepName);

                if (objCurrentBudgetDep != null)
                {
                    objRetBudgetDepNode = FindBudgetDepNode(objParentNodeWithYear, objCurrentBudgetDep.Name);

                    if (objRetBudgetDepNode != null) { return objRetBudgetDepNode; }
                    else
                    {
                        // ���� � �������������� �� ������
                        // ��������� �����
                        objRetBudgetDepNode = new System.Windows.Forms.TreeNode();
                        objRetBudgetDepNode.Text = objCurrentBudgetDep.Name;
                        objRetBudgetDepNode.ImageIndex = objMenuItem.nImage;
                        objRetBudgetDepNode.SelectedImageIndex = objMenuItem.nImage;
                        objRetBudgetDepNode.Tag = new UniXP.Common.MENUITEM()
                        {
                            enMenuType = objMenuItem.enMenuType,
                            strName = objCurrentBudgetDep.Name,
                            lClassID = 1,
                            uuidFarClient = objMenuItem.uuidFarClient,
                            strDescription = objCurrentBudgetDep.Name,
                            nImage = objMenuItem.nImage,
                            strDLLName = objMenuItem.strDLLName,
                            enCmdType = objMenuItem.enCmdType,
                            enDLLType = objMenuItem.enDLLType,
                            objProfile = objMenuItem.objProfile,
                            objAdvancedParametr = null
                        };

                        if (objCurrentBudgetDep.ParentID.CompareTo(System.Guid.Empty) == 0)
                        {
                            // � ������������� ��� ��������
                            // ���� ����������� �������� � ���� � �����
                            objParentNodeWithYear.Nodes.Add(objRetBudgetDepNode);
                            return objRetBudgetDepNode;
                        }
                        else
                        {
                            System.Windows.Forms.TreeNode objParentBudgetDepNode = null;

                            while ((objCurrentBudgetDep != null) && (objCurrentBudgetDep.ParentID.CompareTo(System.Guid.Empty) != 0))
                            {
                                CBudgetDep objParentBudgetDep = objBudgetDepList.SingleOrDefault<CBudgetDep>(x => x.uuidID.CompareTo(objCurrentBudgetDep.ParentID) == 0);
                                if (objParentBudgetDep != null)
                                {
                                    objParentBudgetDepNode = FindBudgetDepNode(objParentNodeWithYear, objParentBudgetDep.Name);
                                    if (objParentBudgetDepNode == null)
                                    {
                                        objParentBudgetDepNode = CreateBudgetDepNode(objParentNodeWithYear, objParentBudgetDep.Name, objBudgetDepList, objMenuItem);
                                    }
                                    else
                                    {
                                        objParentBudgetDepNode.Nodes.Add(objRetBudgetDepNode);
                                    }

                                    objCurrentBudgetDep = objParentBudgetDep;
                                }
                                else
                                {
                                    objCurrentBudgetDep = null;
                                }
                            }
                        }
                    }

                }

            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                String.Format("CreateBudgetDepNode \"{0}\".\n\n����� ������: {1}", strBudgetDepName, e.Message), "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }
            return objRetBudgetDepNode;
        }

        public static System.Windows.Forms.TreeNode FindBudgetDepNode(System.Windows.Forms.TreeNode objParentNode,
            System.String strBudgetDepName)
        {
            System.Windows.Forms.TreeNode objRetNode = null;
            try
            {
                if ((objParentNode == null) || (strBudgetDepName.Length == 0)) { return objRetNode; }

                foreach (System.Windows.Forms.TreeNode objChildNode in objParentNode.Nodes)
                {
                    if (objChildNode.Tag == null) { continue; }
                    if (((UniXP.Common.MENUITEM)objChildNode.Tag).objAdvancedParametr != null) { continue; }

                    if (objChildNode.Text == strBudgetDepName)
                    {
                        objRetNode = objChildNode;
                        break;
                    }
                    else
                    {
                        objRetNode = FindBudgetDepNode(objChildNode, strBudgetDepName);
                        if ((objRetNode != null) && (objRetNode.Text == strBudgetDepName)) { break; }
                    }
                }
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "FindBudgetDepNode.\n\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }
            return objRetNode;
        }


        #endregion


    }

    public class ViewBudgetEditor : PlugIn.IClassTypeView
    {
        public override void Run( UniXP.Common.MENUITEM objMenuItem, System.String strCaption )
        {
            if( objMenuItem.objAdvancedParametr == null ) { return; }
            frmBudgetEditor obj = new frmBudgetEditor(objMenuItem.objProfile, 
                ((ERP_Budget.Common.CBudget)objMenuItem.objAdvancedParametr ).uuidID, objMenuItem);
            obj.Text = strCaption;
            obj.MdiParent = objMenuItem.objProfile.m_objMDIManager.MdiParent;
            obj.Visible = true;
        }
        
        public override System.Windows.Forms.TreeNode InitModule( UniXP.Common.MENUITEM objMenuItem )
        {
            System.Windows.Forms.TreeNode objTreeNode = new System.Windows.Forms.TreeNode();
            try
            {
                System.String strErr = System.String.Empty;
                List<ERP_Budget.Common.CBudget> objBudgetListForProfile = ERP_Budget.Common.CBudget.GetBudgetListForProfile(objMenuItem.objProfile,
                    objMenuItem.objProfile.m_nSQLUserID, ref strErr);

                if ((objBudgetListForProfile == null) || (objBudgetListForProfile.Count == 0)) { return objTreeNode; }

                List<ERP_Budget.Common.CBudgetDep> objBudgetDepList = ERP_Budget.Common.CBudgetDep.GetBudgetDepsList(objMenuItem.objProfile, false);
                
                UniXP.Common.MENUITEM objMenuNode = null;

                // ������ ������ ������ ��� - ������ - ������
                    System.Windows.Forms.TreeNode objNodeYear = null;

                    foreach (ERP_Budget.Common.CBudget objBudget in objBudgetListForProfile)
                    {
                        objNodeYear = null;

                        // �������� ������� ����
                        foreach (System.Windows.Forms.TreeNode objNode in objTreeNode.Nodes)
                        {
                            if (objNode.Text == objBudget.Date.Year.ToString())
                            {
                                objNodeYear = objNode;
                                break;
                            }
                        }
                        if (objNodeYear == null) 
                        {
                            objMenuNode = new UniXP.Common.MENUITEM();
                            objMenuNode.enMenuType = objMenuItem.enMenuType;
                            objMenuNode.strName = objBudget.Date.Year.ToString();
                            objMenuNode.lClassID = 1;
                            objMenuNode.uuidFarClient = objMenuItem.uuidFarClient;
                            objMenuNode.strDescription = objBudget.Date.Year.ToString();
                            objMenuNode.nImage = objMenuItem.nImage;
                            objMenuNode.strDLLName = objMenuItem.strDLLName;
                            objMenuNode.enCmdType = objMenuItem.enCmdType;
                            objMenuNode.enDLLType = objMenuItem.enDLLType;
                            objMenuNode.objProfile = objMenuItem.objProfile;
                            objMenuNode.objAdvancedParametr = null;

                            objNodeYear = new System.Windows.Forms.TreeNode();
                            objNodeYear.Text = objMenuNode.strName;
                            objNodeYear.ImageIndex = objMenuItem.nImage;
                            objNodeYear.SelectedImageIndex = objMenuItem.nImage;
                            objNodeYear.Tag = objMenuNode;
                            objTreeNode.Nodes.Add(objNodeYear);
                        }

                        if (objNodeYear != null)
                        {
                            frmBudgetEditor.CreateBudgetNode(objBudget, objNodeYear, objBudgetDepList, objMenuItem);
                        }

                    }
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( null, "������ ���������� ������ Init\n" + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return objTreeNode;
        }

    }

}