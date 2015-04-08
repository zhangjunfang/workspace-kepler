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

namespace HXCPcClient.UCForm.BusinessAnalysis.ARAPReport
{
    public partial class UCPayableDanSummariz : UCReport
    {
        public UCPayableDanSummariz()
            : base("v_payable_dan_summariz", "应付款单汇总表")
        {
            InitializeComponent();
        }

        private void UCPayableDanSummariz_Load(object sender, EventArgs e)
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

        private void txtcSup_code_ChooserClick(object sender, EventArgs e)
        {
            frmSupplier frmSup = new frmSupplier();
            if (frmSup.ShowDialog() == DialogResult.OK)
            {
                txtcSup_code.Text = frmSup.supperCode;
                txtSup_name.Caption = frmSup.supperName;
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
            strWhere += string.Format(" and order_type='{0}'", (int)DataSources.EnumOrderType.PAYMENT);
            string files = "cust_code,cust_name,SUM(金额) 金额";
            string group = " group by cust_code,cust_name order by cust_name";
            dt = DBHelper.GetTable("", "v_bill_payment_detail", files, strWhere, "", group);
            dgvReport.DataSource = dt;
        }

        void OpenDetail()
        {
            if (dgvReport.CurrentRow == null)
            {
                return;
            }
            string supName = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colSupName.Name].Value);
            if (supName.Length == 0)
            {
                return;
            }
            UCPayableDanDetail detail = new UCPayableDanDetail();
            detail.supCode = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colSupCode.Name].Value);
            detail.supName = supName;
            detail.supType = CommonCtrl.IsNullToString(cboSup_type.SelectedValue);
            detail.payableType = CommonCtrl.IsNullToString(cboPaymentType.SelectedValue);
            detail.balanceWay = CommonCtrl.IsNullToString(cboBalanceWay.SelectedValue);
            detail.account = CommonCtrl.IsNullToString(cboPaymentAccount.SelectedValue);
            detail.startDate = dicreate_time.StartDate;
            detail.endDate = dicreate_time.EndDate;
            detail.company = CommonCtrl.IsNullToString(cboCompany.SelectedValue);
            detail.orgID = CommonCtrl.IsNullToString(cboorg_id.SelectedValue);
            base.addUserControl(detail, "应付款单明细表", "UCPayableDanDetail", this.Tag.ToString(), this.Name);
        }

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDetail();
        }
    }
}
