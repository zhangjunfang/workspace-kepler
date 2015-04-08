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
using HXCPcClient.CommonClass;
using Utility.Common;

namespace HXCPcClient.UCForm.BusinessAnalysis.ARAPReport
{
    public partial class UCReceivableAbove : UCReport
    {
        public UCReceivableAbove()
            : base("v_receivable_above", "应收款超信用额度报表")
        {
            InitializeComponent();
            colYingShou.DefaultCellStyle = styleMoney;
            colEDu.DefaultCellStyle = styleMoney;
            colChao.DefaultCellStyle = styleMoney;
            dtEndDate.Value = DateTime.Now;
        }

        private void UCReceivableAbove_Load(object sender, EventArgs e)
        {
            //客户类别
            CommonFuncCall.BindComBoxDataSource(cboCust_type, "sys_customer_category", "全部");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            //是否会员
            DataSources.BindComBoxDataEnum(cboIsMember, typeof(DataSources.EnumYesNo), true);
            listNegative = new List<string>();
            listNegative.Add("本期发生");//客户应收款
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
            string strWhere = GetWhere();
            strWhere += " and isnull(b.credit_line,0)<a.客户应收款";
            string filed = "b.cust_code,b.cust_name,a.客户应收款,b.credit_line,a.客户应收款-ISNULL(b.credit_line,0) 超信用额度";
            string table = string.Format(@"(
select cust_id,SUM(isnull(本期发生,0)-isnull(本期收款,0)) 客户应收款 from v_receivable_document where 单据日期<{0}
group by cust_id) a
inner join tb_customer b on a.cust_id=b.cust_id", Common.LocalDateTimeToUtcLong(dtEndDate.Value.Date));
            dt = DBHelper.GetTable("", table, filed, strWhere, "", "order by cust_name");
            List<string> listNot = new List<string>();
            listNot.Add("credit_line");
            listNot.Add("超信用额度");
            dt.DataTableSum(listNot);
            dgvReport.DataSource = dt;
        }
    }
}
