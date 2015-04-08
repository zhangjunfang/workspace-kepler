using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYSModel;
using HXCPcClient.CommonClass;

namespace HXCPcClient.UCForm.BusinessAnalysis.ARAPReport
{
    public partial class UCReceivableDanSummariz : UCReport
    {
        public UCReceivableDanSummariz()
            : base("v_receivable_dan_summariz", "应收款单汇总表")
        {
            InitializeComponent();
        }

        private void UCReceivableDanSummariz_Load(object sender, EventArgs e)
        {
            //绑定结算方式
            CommonFuncCall.BindBalanceWayByItem(cboBalanceWay, "全部");
            //绑定结算账户
            CommonFuncCall.BindAccount(cboPaymentAccount, "全部");
            //客户类别
            CommonFuncCall.BindComBoxDataSource(cboCust_type, "sys_customer_category", "全部");
            //绑定是否会员
            DataSources.BindComBoxDataEnum(cboIsMember, typeof(DataSources.EnumYesNo), true);
            //绑定收款类型
            DataSources.BindComBoxDataEnum(cboPaymentType, typeof(DataSources.EnumReceivableType), true);
            //绑定公司
            CommonFuncCall.BindCompany(cboCompany, "全部");

            listNegative = new List<string>();
            listNegative.Add("金额");

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

        private void cboBalanceWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBalanceWay.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)cboBalanceWay.SelectedItem;
                cboPaymentAccount.SelectedValue = drv["default_account"];
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {
            string strWhere = GetWhere();
            strWhere += string.Format(" and order_type='{0}'", (int)DataSources.EnumOrderType.RECEIVABLE);
            string files = "cust_code,cust_name,SUM(金额) 金额";
            string group = " group by cust_code,cust_name order by cust_name";
            dt = DBHelper.GetTable("", "v_bill_receivable_detail", files, strWhere, "", group);
            dgvReport.DataSource = dt;
        }

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDetail();
        }

        void OpenDetail()
        {
            if (dgvReport.CurrentRow == null)
            {
                return;
            }
            string custName = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colCustName.Name].Value);
            if (custName.Length == 0)
            {
                return;
            }
            UCReceivableDanDetail detail = new UCReceivableDanDetail();
            detail.custCode = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colCustCode.Name].Value);
            detail.custName = custName;
            detail.custType = CommonCtrl.IsNullToString(cboCust_type.SelectedValue);
            detail.is_member = CommonCtrl.IsNullToString(cboIsMember.SelectedValue);
            detail.receivableType = CommonCtrl.IsNullToString(cboPaymentType.SelectedValue);
            detail.balanceWay = CommonCtrl.IsNullToString(cboBalanceWay.SelectedValue);
            detail.account = CommonCtrl.IsNullToString(cboPaymentAccount.SelectedValue);
            detail.startDate = dicreate_time.StartDate;
            detail.endDate = dicreate_time.EndDate;
            detail.company = CommonCtrl.IsNullToString(cboCompany.SelectedValue);
            detail.orgID = CommonCtrl.IsNullToString(cboorg_id.SelectedValue);
            base.addUserControl(detail, "应收款单明细表", "UCReceivableDanDetail", this.Tag.ToString(), this.Name);
        }
    }
}
