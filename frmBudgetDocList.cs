using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ErpBudgetBudgetEditor
{
    public partial class frmBudgetDocList : DevExpress.XtraEditors.XtraForm
    {
        #region ����������, ��������, ���������

        private UniXP.Common.CProfile m_objProfile;
        private ERP_Budget.Common.CBudgetItem m_objBudgetItem;

        #endregion

        #region �����������
        public frmBudgetDocList( UniXP.Common.CProfile objProfile, ERP_Budget.Common.CBudgetItem objBudgetItem )
        {
            InitializeComponent();

            this.m_objProfile = objProfile;
            this.m_objBudgetItem = objBudgetItem;
        }
        #endregion

        #region ���������� ������� ����������
        /// <summary>
        /// ��������� ������ ����������
        /// </summary>
        /// <returns>true - �������� ����������; false - ������</returns>
        System.Boolean bRefreshBudgetDocList()
        {
            System.Boolean bRet = false;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                treeList.Nodes.Clear();
                //������ ����������
                System.Double SumMoney = 0;
                List<ERP_Budget.Common.structBudgetItemDoc> objList = this.m_objBudgetItem.GetBudgetDocList( this.m_objProfile );
                if( ( objList != null ) && ( objList.Count > 0 ) )
                {
                    foreach( ERP_Budget.Common.structBudgetItemDoc objItemDoc in objList )
                    {
                        // ��������� ���� � ������
                        treeList.AppendNode( new object[] { objItemDoc.DocDate, objItemDoc.DocState, 
                            objItemDoc.BudgetItemFullName, objItemDoc.DocMoney, objItemDoc.Currency, 
                            objItemDoc.Money, objItemDoc.User, objItemDoc.ObjectiveDoc }, null );
                        SumMoney += objItemDoc.Money;

                        System.Data.DataRow row = dtReport.NewRow();
                        row[ "DOC_DATE" ] = objItemDoc.DocDate;
                        row[ "DOC_STATE" ] = objItemDoc.DocState;
                        row[ "BUDGETITEM_NAME" ] = objItemDoc.BudgetItemFullName;
                        row[ "DOC_SUM" ] = objItemDoc.DocMoney;
                        row[ "DOC_CURRENCY" ] = objItemDoc.Currency;
                        row[ "SUM" ] = objItemDoc.Money;
                        row[ "USER" ] = objItemDoc.User;
                        row[ "OBJECTIVE" ] = objItemDoc.ObjectiveDoc;
                        dtReport.Rows.Add( row );
                    }
                    dataSet.AcceptChanges();
                }
                lblDebitArticle.Text = this.m_objBudgetItem.BudgetItemFullName;
                lblMoney.Text = "�����: " + System.String.Format( "{0:### ### ##0.00}", SumMoney );

                bRet = true;
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                "������ ���������� ������� ����������.\n\n����� ������: " + f.Message, "��������",
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
        private void frmBudgetDocList_Load( object sender, EventArgs e )
        {
            try
            {
                tableLayoutPanel2.RowStyles[ 2 ].Height = 0;
                // ��������� ������ ��������
                bRefreshBudgetDocList();
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this,
                "������ �������� ������� ����������.\n\n����� ������: " + f.Message, "��������",
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
                "������ �������� ������� ����������.\n\n����� ������: " + f.Message, "��������",
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
                    DevExpress.XtraPrinting.PrintHelper.ShowPreview( gridControl );
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

        private void barBtnExport_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            try
            {

                Cursor.Current = Cursors.WaitCursor;

                using( SaveFileDialog fd = new SaveFileDialog() )
                {
                    fd.Title = "������� ������ � MS Excel";
                    fd.RestoreDirectory = true;
                    fd.Filter = "Excel|*.xls";
                    fd.FilterIndex = 1;
                    if( fd.ShowDialog() == DialogResult.OK )
                    {
                        gridControl.ExportToXls( fd.FileName );
                    }
                }

                Cursor.Current = Cursors.Default;
            }
            catch( System.Exception f )
            {
                System.Windows.Forms.MessageBox.Show( this, "������ �������� ������.\n\n����� ������: " + f.Message, "������",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return;
        }
        #endregion

    }
}