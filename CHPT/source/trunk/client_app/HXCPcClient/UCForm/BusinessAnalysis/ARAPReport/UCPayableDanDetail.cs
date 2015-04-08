using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using SYSModel;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm.FinancialManagement.Receivable;

namespace HXCPcClient.UCForm.BusinessAnalysis.ARAPReport
{
    public partial class UCPayableDanDetail : UCReport
    {
        public string supCode = string.Empty;
        public string supName = string.Empty;
        public string supType = string.Empty;
        public string payableType = string.Empty;
        public string balanceWay = string.Empty;
        public string account = string.Empty;
        public string startDate = DateTime.Now.ToString("yyyy-MM-01");
        public string endDate = DateTime.Now.ToString("yyyy-MM-dd");
        public string company = string.Empty;
        public string orgID = string.Empty;
        public UCPayableDanDetail()
            : base("v_payable_dan_detail", "应付款单明细表")
        {
            InitializeComponent();
            Quick qSupp = new Quick();
            qSupp.BindSupplier(txtcSup_code);
            txtcSup_code.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtcSup_code_DataBacked);
        }

        void txtcSup_code_DataBacked(DataRow dr)
        {
            txtSup_name.Caption = CommonCtrl.IsNullToString(dr["sup_full_name"]);
            txtcSup_code.Text = CommonCtrl.IsNullToString(dr["sup_code"]);
        }

        private void UCPayableDanDetail_Load(object sender, EventArgs e)
        {
            //绑定结算方式
            CommonFuncCall.BindBalanceWayByItem(cboBalanceWay, "全部");
            //绑定结算账户
            CommonFuncCall.BindAccount(cboPaymentAccount, "全部");
            //供应商类别
            CommonFuncCall.BindComBoxDataSource(cboSup_type, "sys_supplier_category", "全部");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");

            //绑定收款类型
            DataSources.BindComBoxDataEnum(cboPaymentType, typeof(DataSources.EnumPaymentType), true);
            listNegative = new List<string>();
            listNegative.Add("金额");

            txtcSup_code.Text = supCode;
            txtSup_name.Caption = supName;
            cboSup_type.SelectedValue = supType;
            cboBalanceWay.SelectedValue = balanceWay;
            cboPaymentType.SelectedValue = payableType;
            cboPaymentAccount.SelectedValue = account;
            dicreate_time.StartDate = startDate;
            dicreate_time.EndDate = endDate;
            cboCompany.SelectedValue = company;
            cboorg_id.SelectedValue = orgID;
            BindData();

            //双击查看明细,要手动调用权限
            if (this.Name != "CL_BusinessAnalysis_ARAP_PayDanDet")
            {
                base.RoleButtonStstus("CL_BusinessAnalysis_ARAP_PayDanDet");
            }
        }

        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCompany.SelectedValue == null)
            {
                return;
            }
            CommonFuncCall.BindDepartment(cboorg_id, cboCompany.SelectedValue.ToString(), "全部");
        }

        private void txtcSup_code_ChooserClick(object sender, EventArgs e)
        {
            frmSupplier frmSup = new frmSupplier();
            if (frmSup.ShowDialog() == DialogResult.OK)
            {
                txtcSup_code.Text = frmSup.supperCode;
                txtSup_name.Caption = frmSup.supperName;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void cboBalanceWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBalanceWay.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)cboBalanceWay.SelectedItem;
                cboPaymentAccount.SelectedValue = drv["default_account"];
            }
        }

        void BindData()
        {
            string strWhere = GetWhere();
            strWhere += string.Format(" and order_type='{0}'", (int)DataSources.EnumOrderType.PAYMENT);
            dt = DBHelper.GetTable("", "v_bill_payment_detail", "*", strWhere, "", "order by cust_name");
            dt.DataTableToDate("开单日期");
            //List<string> listNot = new List<string>();
            //listNot.Add("create_time");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["payment_type"] != null && dr["payment_type"] != DBNull.Value)
                {
                    dr["payment_type"] = DataSources.GetDescription(typeof(DataSources.EnumPaymentType), dr["payment_type"]);
                }
            }
            //按客户分组
            dt.DataTableGroup("cust_name", "单据编号", "供应商名称：", "cust_code", "开单日期", "供应商编码：", null);
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
            UCReceivableAdd UCPurchaseBillView = new UCReceivableAdd(WindowStatus.View, orderId, null, DataSources.EnumOrderType.PAYMENT);
            base.addUserControl(UCPurchaseBillView, "应付账款-查看", "UCReceivableAdd" + orderId + "", this.Tag.ToString(), this.Name);
        }
    }
}
