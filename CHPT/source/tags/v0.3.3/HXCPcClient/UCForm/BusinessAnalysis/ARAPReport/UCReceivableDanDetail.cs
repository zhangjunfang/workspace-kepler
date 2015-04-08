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
using HXCPcClient.UCForm.FinancialManagement.Receivable;

namespace HXCPcClient.UCForm.BusinessAnalysis.ARAPReport
{
    public partial class UCReceivableDanDetail : UCReport
    {
        public string custCode = string.Empty;
        public string custName = string.Empty;
        public string custType = string.Empty;
        public string is_member = string.Empty;
        public string receivableType = string.Empty;
        public string balanceWay = string.Empty;
        public string account = string.Empty;
        public string startDate = DateTime.Now.ToString("yyyy-MM-01");
        public string endDate = DateTime.Now.ToString("yyyy-MM-dd");
        public string company = string.Empty;
        public string orgID = string.Empty;
        public UCReceivableDanDetail()
            : base("v_receivable_dan_detail", "应收款单明细表")
        {
            InitializeComponent();
        }

        private void UCReceivableDanDetail_Load(object sender, EventArgs e)
        {
            //绑定结算方式
            CommonFuncCall.BindBalanceWayByItem(cboBalanceWay, "全部");
            //绑定结算账户
            CommonFuncCall.BindAccount(cboPaymentAccount, "全部");
            //客户类别
            CommonFuncCall.BindComBoxDataSource(cboCust_type, "sys_customer_category", "全部");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            //是否会员
            DataSources.BindComBoxDataEnum(cboIsMember, typeof(DataSources.EnumYesNo), true);
            //绑定收款类型
            DataSources.BindComBoxDataEnum(cboPaymentType, typeof(DataSources.EnumReceivableType), true);
            listNegative = new List<string>();
            listNegative.Add("金额");

            txtcCust_code.Text = custCode;
            txtCust_name.Caption = custName;
            cboCust_type.SelectedValue = custType;
            cboIsMember.SelectedValue = is_member;
            cboBalanceWay.SelectedValue = balanceWay;
            cboPaymentType.SelectedValue = receivableType;
            cboPaymentAccount.SelectedValue = account;
            dicreate_time.StartDate = startDate;
            dicreate_time.EndDate = endDate;
            cboCompany.SelectedValue = company;
            cboorg_id.SelectedValue = orgID;

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
            dt = DBHelper.GetTable("", "v_bill_receivable_detail", "*", strWhere, "", "order by cust_name");
            dt.DataTableToDate("开单日期");
            //List<string> listNot = new List<string>();
            //listNot.Add("create_time");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["payment_type"] != null && dr["payment_type"] != DBNull.Value)
                {
                    dr["payment_type"] = DataSources.GetDescription(typeof(DataSources.EnumReceivableType), dr["payment_type"]);
                }
            }
            //按客户分组
            dt.DataTableGroup("cust_name", "单据编号", "客户名称：", "cust_code", "开单日期", "客户编码：", null);
            dgvReport.DataSource = dt;
        }

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDocument();
        }

        void OpenDocument()
        {
            if (dgvReport.CurrentRow == null)
            {
                return;
            }
            string orderId = Convert.ToString(this.dgvReport.CurrentRow.Cells[colOrderID.Name].Value);
            if (orderId.Length == 0)
            {
                return;
            }
            UCReceivableAdd UCPurchaseBillView = new UCReceivableAdd(WindowStatus.View, orderId, null, DataSources.EnumOrderType.RECEIVABLE);
            base.addUserControl(UCPurchaseBillView, "应收账款-查看", "UCReceivableAdd" + orderId + "", this.Tag.ToString(), this.Name);
        }

    }
}
