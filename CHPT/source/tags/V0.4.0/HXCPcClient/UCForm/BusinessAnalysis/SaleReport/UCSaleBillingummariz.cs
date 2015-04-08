using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;

namespace HXCPcClient.UCForm.BusinessAnalysis.SaleReport
{
    public partial class UCSaleBillingummariz : UCReport
    {
        public UCSaleBillingummariz()
            : base("v_sale_billing_summariz_report", "销售开单汇总表")
        {
            InitializeComponent();
        }
        //窗体加载
        private void UCSaleBillingummariz_Load(object sender, EventArgs e)
        {
            //单据类型
            CommonFuncCall.BindSaleOrderType(cboorder_type, true, "全部");
            //发票类型
            CommonFuncCall.BindComBoxDataSource(cboreceipt_type, "sys_receipt_type", "全部");
            //结算单位
            CommonFuncCall.BindBalanceWayByItem(cbobalance_way, "全部");
            //客户类别
            CommonFuncCall.BindComBoxDataSource(cboCust_type, "sys_customer_category", "全部");
            //仓库
            CommonFuncCall.BindWarehouse(cbowh_code, "全部");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");

            BindData();
        }
        //选择结算单位，绑定默认结算账户
        private void cbobalance_way_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbobalance_way.SelectedItem == null)
            {
                return;
            }
            DataRowView drv = (DataRowView)cbobalance_way.SelectedItem;
            string defalutAccount = drv["default_account"].ToString();
            if (defalutAccount.Length == 0)
            {
                return;
            }
            CommonFuncCall.BindAccount(cbobalance_account, defalutAccount, "全部");
        }
        //选择公司，绑定公司所有部门
        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCompany.SelectedValue == null)
            {
                return;
            }
            CommonFuncCall.BindDepartment(cboorg_id, cboCompany.SelectedValue.ToString(), "全部");
        }
        //客户选择
        private void txtcsup_code_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo frmCust = new frmCustomerInfo();
            if (frmCust.ShowDialog() == DialogResult.OK)
            {
                txtcCust_code.Text = frmCust.strCustomerNo;
                txtCust_name.Caption = frmCust.strCustomerName;
            }
        }
        //配件选择
        private void txtcparts_code_ChooserClick(object sender, EventArgs e)
        {
            frmParts parts = new frmParts();
            if (parts.ShowDialog() == DialogResult.OK)
            {
                txtcparts_code.Text = parts.PartsCode;
                txtPartsName.Caption = parts.PartsName;
            }
        }
        //绑定报表数据
        void BindData()
        {
            //查询字段
            string fileds = "客户编码,客户名称,sum(销售货款) 销售货款,sum(税额) 税额,sum(金额) 金额";
            //分组
            string groupBy = "group by 客户编码,客户名称";
            dt = DBHelper.GetTable("", "v_sale_billing_summariz_report", fileds, GetWhere(), "", groupBy);
            //添加合计
            dt.DataTableSum(null);
            dgvReport.DataSource = dt;
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        /// <summary>
        /// 打开销售综合表
        /// </summary>
        void OpenDocument()
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
            UCSaleBillingZongHe zonghe = new UCSaleBillingZongHe();
            zonghe.orderType = CommonCtrl.IsNullToString(cboorder_type.SelectedValue);
            zonghe.receiptType = CommonCtrl.IsNullToString(cboreceipt_type.SelectedValue);
            zonghe.balanceWay = CommonCtrl.IsNullToString(cbobalance_way.SelectedValue);
            zonghe.balanceAccount = CommonCtrl.IsNullToString(cbobalance_account.SelectedValue);
            zonghe.supCode = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colCustCode.Name].Value);
            zonghe.supName = custName;
            zonghe.supType = CommonCtrl.IsNullToString(cboCust_type.SelectedValue);
            zonghe.whCode = CommonCtrl.IsNullToString(cbowh_code.SelectedValue);
            zonghe.partsCode = txtcCust_code.Text;
            zonghe.partsName = txtPartsName.Caption;
            zonghe.drawingNum = txtdrawing_num.Caption;
            zonghe.partsBrand = txtparts_brand.Caption;
            zonghe.stratDate = dicreate_time.StartDate;
            zonghe.endDate = dicreate_time.EndDate;
            zonghe.commpany = CommonCtrl.IsNullToString(cboCompany.SelectedValue);
            zonghe.orgID = CommonCtrl.IsNullToString(cboorg_id.SelectedValue);
            base.addUserControl(zonghe, "销售综合汇总表", "UCSaleBillingZongHe", this.Tag.ToString(), this.Name);
        }

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDocument();
        }
    }
}
