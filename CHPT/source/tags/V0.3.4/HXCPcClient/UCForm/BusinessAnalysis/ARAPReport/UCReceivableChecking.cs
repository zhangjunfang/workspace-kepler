using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYSModel;
using HXCPcClient.Chooser;

namespace HXCPcClient.UCForm.BusinessAnalysis.ARAPReport
{
    public partial class UCReceivableChecking : UCReport
    {
        public UCReceivableChecking()
            : base("v_receivable_checking", "应收款对账单")
        {
            InitializeComponent();
        }

        private void UCReceivableChecking_Load(object sender, EventArgs e)
        {
            //客户类别
            CommonFuncCall.BindComBoxDataSource(cboCust_type, "sys_customer_category", "全部");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            //是否会员
            DataSources.BindComBoxDataEnum(cboIsMember, typeof(DataSources.EnumYesNo), true);

            listNegative = new List<string>();
            listNegative.Add("应收金额");
            listNegative.Add("期末余额");

            BindData();
        }

        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCompany.SelectedValue == null)
            {
                return;
            }
            CommonFuncCall.BindDepartment(cboorg_id, cboCompany.SelectedValue.ToString(), "全部");
        }

        private void txtcCust_code_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo frmCust = new frmCustomerInfo();
            if (frmCust.ShowDialog() == DialogResult.OK)
            {
                txtcCust_code.Text = frmCust.strCustomerNo;
                txtCust_name.Caption = frmCust.strCustomerName;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {

        }
    }
}
