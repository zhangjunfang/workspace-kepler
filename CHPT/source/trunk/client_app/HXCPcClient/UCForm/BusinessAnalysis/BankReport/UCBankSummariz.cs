using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility.Common;
using HXCPcClient.CommonClass;

namespace HXCPcClient.UCForm.BusinessAnalysis.BankReport
{
    public partial class UCBankSummariz : UCReport
    {
        public UCBankSummariz()
            : base("v_bank_summariz", "现金银行汇总表")
        {
            InitializeComponent();
        }

        private void UCBankSummariz_Load(object sender, EventArgs e)
        {
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            //绑定结算账户
            CommonFuncCall.BindAccount(cboAccount, "全部");
            diDate.StartDate = DateTime.Now.ToString("yyyy-MM-01");
            diDate.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
            colQiChu.DefaultCellStyle = styleMoney;
            colShouRu.DefaultCellStyle = styleMoney;
            colZhiChu.DefaultCellStyle = styleMoney;
            colQiMo.DefaultCellStyle = styleMoney;
            BindData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {
            string strWhere = GetWhere();
            string filed = @"a.账户名称,a.银行名称,b.期初余额,SUM(isnull(a.本期收入,0)) 本期收入金额,SUM(ISNULL(a.本期支出,0)) 本期支出金额,
ISNULL(b.期初余额,0)+SUM(ISNULL(a.本期收入,0))-SUM(ISNULL(a.本期支出,0)) 期末余额,a.payment_account";
            string table = string.Format(@"v_bank_summariz a left join (
select payment_account,SUM(ISNULL(本期收入,0)-ISNULL(本期支出,0)) 期初余额 from v_bank_summariz where 单据日期<0
group by payment_account
)b on a.payment_account=b.payment_account", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(diDate.StartDate).Date));
            string groupBy = "group by a.账户名称,a.银行名称,b.期初余额,a.payment_account order by 账户名称";
            dt = DBHelper.GetTable("", table, filed, strWhere, "", groupBy);
            dt.DataTableSum(null);
            dgvReport.DataSource = dt;
        }

        private void dtpReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDocument();
        }

        void OpenDocument()
        {
            if (dgvReport.CurrentRow == null)
            {
                return;
            }
            string account = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colAccountID.Name].Value);
            if (account.Length == 0)
            {
                return;
            }
            UCBankDetail detail = new UCBankDetail();
            detail.account = account;
            detail.startDate = diDate.StartDate;
            detail.endDate = diDate.EndDate;
            detail.company = CommonCtrl.IsNullToString(cboCompany.SelectedValue);
            base.addUserControl(detail, "现金银行明细表", "UCBankDetail", this.Tag.ToString(), this.Name);
        }
    }
}
