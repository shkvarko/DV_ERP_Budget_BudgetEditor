using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ErpBudgetBudgetEditor
{
    public partial class frmTrnList : DevExpress.XtraEditors.XtraForm
    {
        #region ����������, ��������, ���������

        private UniXP.Common.CProfile m_objProfile;
        private ERP_Budget.Common.CBudgetItem m_objBudgetItem;

        #endregion

        #region �����������
        public frmTrnList( UniXP.Common.CProfile objProfile, ERP_Budget.Common.CBudgetItem objBudgetItem )
        {
            InitializeComponent();

            this.m_objProfile = objProfile;
            this.m_objBudgetItem = objBudgetItem;
        }
        #endregion

        #region ���������� ������� ��������
        /// <summary>
        /// ��������� ������ ���������
        /// </summary>
        /// <returns>true - �������� ����������; false - ������</returns>
        System.Boolean bRefreshtrnList()
        {
            System.Boolean bRet = false;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                treeList.Nodes.Clear();
                //System.String strInfoText = String.Format( "\t\t������ {0} \t\t {1} �� {2} \t\t��������� {3}",
                //   this.m_objBudgetDoc.BudgetDep.Name, this.m_objBudgetDoc.DocType.Name,
                //   this.m_objBudgetDoc.Date.ToShortDateString(), ( this.m_objBudgetDoc.OwnerUser.UserLastName + " " + this.m_objBudgetDoc.OwnerUser.UserFirstName ) );
                //lblInfo.Text = strInfoText;

                //lblMoney.Text = "\t\t" +  System.String.Format( "{0:### ### ##0.00}", this.m_objBudgetDoc.Money ) + " " + 
                //    this.m_objBudgetDoc.Currency.CurrencyCode;
                lblDebitArticle.Text = "\t\t" +  this.m_objBudgetItem.BudgetItemFullName;

                //������ ����������
                List<ERP_Budget.Common.structBudgetItemAccTrn> objList = this.m_objBudgetItem.GetAccountTransnList( this.m_objProfile );
                if( ( objList != null ) && ( objList.Count > 0 ) )
                {
                    foreach( ERP_Budget.Common.structBudgetItemAccTrn objAccTrn in objList )
                    {
                        // ��������� ���� � ������
                        treeList.AppendNode( new object[] { objAccTrn.TrnDate, objAccTrn.Operation, 
                            objAccTrn.MoneyTrn, objAccTrn.CurrencyTrn, objAccTrn.UserTrn, 
                            objAccTrn.DocDate, objAccTrn.MoneyDoc, objAccTrn.CurrencyDoc, 
                            objAccTrn.ObjectiveDoc }, null );
                    }
                }
                bRet = true;

            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                "������ ���������� ������� ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }

            return bRet;
        }
        #endregion

        #region �������� �����
        private void frmTrnList_Load( object sender, EventArgs e )
        {
            try
            {
                // ��������� ������ ��������
                bRefreshtrnList();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                "������ �������� ������� ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        #endregion

        #region �������� �����
        private void barBtnClose_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            try
            {
                this.Close();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                "������ �������� ������� ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
                if( DevExpress.XtraPrinting.PrintHelper.IsPrintingAvailable )
                    DevExpress.XtraPrinting.PrintHelper.ShowPreview( treeList );
                else
                    MessageBox.Show( "XtraPrinting Library is not found...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information );
                Cursor.Current = Cursors.Default;
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ ������.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        #endregion
    }
}