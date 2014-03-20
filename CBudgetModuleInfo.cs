using System;
using System.Collections.Generic;
using System.Text;

namespace ErpBudgetBudgetEditor
{
    public class CBudgetModuleClassInfo : UniXP.Common.CModuleClassInfo
    {
        public CBudgetModuleClassInfo()
        {
            UniXP.Common.CLASSINFO objClassInfo;
            // ������ �������� ��������
            objClassInfo = new UniXP.Common.CLASSINFO();
            objClassInfo.enClassType = UniXP.Common.EnumClassType.mcView;
            objClassInfo.strClassName = "ErpBudgetBudgetEditor.ViewBudgetWizard";
            objClassInfo.strName = "������ ��������";
            objClassInfo.strDescription = "������ ��� �������� ��������";
            objClassInfo.lID = 0;
            objClassInfo.nImage = 1;
            objClassInfo.strResourceName = "IMAGES_BUDGETSMALL";
            m_arClassInfo.Add( objClassInfo );
            // �������� �������
            objClassInfo = new UniXP.Common.CLASSINFO();
            objClassInfo.enClassType = UniXP.Common.EnumClassType.mcView;
            objClassInfo.strClassName = "ErpBudgetBudgetEditor.ViewBudgetEditor";
            objClassInfo.strName = "������ v.2";
            objClassInfo.strDescription = "������ ��� �������������� �������";
            objClassInfo.lID = 1;
            objClassInfo.nImage = 1;
            objClassInfo.strResourceName = "IMAGES_BUDGETSMALL";
            m_arClassInfo.Add(objClassInfo);
        }
    }

    public class CBudgetModuleInfo : UniXP.Common.CClientModuleInfo
    {
        public CBudgetModuleInfo()
            : base( System.Reflection.Assembly.GetExecutingAssembly(),
            UniXP.Common.EnumDLLType.typeItem,
            new System.Guid( "{C94D936B-FE93-4231-A695-5175C5C0E29B}" ),
            new System.Guid( "{a8e694df-15a3-4713-80ac-304b3fe911e8}" ),
            ErpBudgetBudgetEditor.Properties.Resources.IMAGES_BUDGET,
            ErpBudgetBudgetEditor.Properties.Resources.IMAGES_BUDGETSMALL )
        {
        }

        /// <summary>
        /// ��������� �������� �� �������� ������������ ��������� ������ � �������.
        /// </summary>
        /// <param name="objProfile">������� ������������.</param>
        public override System.Boolean Check( UniXP.Common.CProfile objProfile )
        {
            return true;
        }
        /// <summary>
        /// ��������� �������� �� ��������� ������ � �������.
        /// </summary>
        /// <param name="objProfile">������� ������������.</param>
        public override System.Boolean Install( UniXP.Common.CProfile objProfile )
        {
            return true;
        }
        /// <summary>
        /// ��������� �������� �� �������� ������ �� �������.
        /// </summary>
        /// <param name="objProfile">������� ������������.</param>
        public override System.Boolean UnInstall( UniXP.Common.CProfile objProfile )
        {
            return true;
        }
        /// <summary>
        /// ���������� �������� �� ���������� ��� ��������� ����� ������ ������������� ������.
        /// </summary>
        /// <param name="objProfile">������� ������������.</param>
        public override System.Boolean Update( UniXP.Common.CProfile objProfile )
        {
            return true;
        }
        /// <summary>
        /// ���������� ������ ��������� ������� � ������ ������.
        /// </summary>
        public override UniXP.Common.CModuleClassInfo GetClassInfo()
        {
            return new CBudgetModuleClassInfo();
        }
    }

    public class ModuleInfo : PlugIn.IModuleInfo
    {
        public UniXP.Common.CClientModuleInfo GetModuleInfo()
        {
            return new CBudgetModuleInfo();
        }
    }

}
