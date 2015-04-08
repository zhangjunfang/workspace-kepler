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
using HXCPcClient.UCForm.FinancialManagement.Receivable;
using SYSModel;

namespace HXCPcClient.UCForm.BusinessAnalysis.BankReport
{
    public partial class UCBankDetail : UCReport
    {
        public string company = string.Empty;
        public string account = string.Empty;
        public string startDate = DateTime.Now.ToString("yyyy-MM-01");
        public string endDate = DateTime.Now.ToString("yyyy-MM-dd");
        public UCBankDetail()
            : base("v_bank_detail", "现金银行明细表")
        {
            InitializeComponent();
        }

        private void UCBankDetail_Load(object sender, EventArgs e)
        {
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            //绑定结算账户
            CommonFuncCall.BindAccount(cboAccount, "全部");
            cboCompany.SelectedValue = company;
            cboAccount.SelectedValue = account;
            diDate.StartDate = startDate;
            diDate.EndDate = endDate;
            colJieSuan.DefaultCellStyle = styleMoney;
            colShouRu.DefaultCellStyle = styleMoney;
            colZhiChu.DefaultCellStyle = styleMoney;
            BindData();
            //双击查看明细,要手动调用权限
            if (this.Name != "CL_BusinessAnalysis_Bank_BankDet")
            {
                base.RoleButtonStstus("CL_BusinessAnalysis_Bank_BankDet");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {
            string strWhere = GetWhere();
            string filed = "a.单据名称,a.单据号,a.单据日期,a.结算方式,a.账户名称,a.本期收入,a.本期支出,b.期初余额,0 结算,a.备注,a.ID,a.order_type";
            string table = string.Format(@"v_bank_detail a left join (
select payment_account,SUM(ISNULL(本期收入,0)-ISNULL(本期支出,0)) 期初余额 from v_bank_detail where 单据日期<0
group by payment_account
) b on a.payment_account=b.payment_account", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(diDate.StartDate).Date));
            dt = DBHelper.GetTable("", table, filed, strWhere, "", "order by 账户名称,单据日期");
            dt.DataTableToDate("单据日期");
            List<string> listNot = new List<string>();
            listNot.Add("结算");
            //按账户分组
            dt.DataTableGroup("账户名称", "单据名称", "账户名称：", listNot, "期初余额", "期初余额", "结算", "期末", "本期收入", "本期支出");
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
            //单据ID
            string orderId = Convert.ToString(this.dgvReport.CurrentRow.Cells[colID.Name].Value);
            if (orderId.Length == 0)
            {
                return;
            }
            //单据类型
            string orderType = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colOrderType.Name].Value);
            if (orderType.Length == 0)
            {
                return;
            }
            string viewTitle = "应收账款-查看";//明细标题
            DataSources.EnumOrderType enumOrderType = (DataSources.EnumOrderType)Convert.ToInt32(orderType);
            if (enumOrderType == DataSources.EnumOrderType.PAYMENT)
            {
                viewTitle = "应付账款-查看";
            }
            UCReceivableAdd ucView = new UCReceivableAdd(WindowStatus.View, orderId, null, enumOrderType);
            base.addUserControl(ucView, viewTitle, "UCReceivableAdd" + orderId, this.Tag.ToString(), this.Name);
        }
    }
}
