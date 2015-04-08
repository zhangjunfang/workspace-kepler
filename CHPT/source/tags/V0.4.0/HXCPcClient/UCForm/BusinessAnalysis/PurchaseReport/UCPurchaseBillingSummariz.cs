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

namespace HXCPcClient.UCForm.BusinessAnalysis.PurchaseReport
{
    public partial class UCPurchaseBillingSummariz : UCReport
    {
        public UCPurchaseBillingSummariz()
            : base("v_parts_purchase_billing_summariz_report","采购开单汇总表")
        {
            InitializeComponent();
        }
        //窗体加载
        private void UCPurchaseBillingSummariz_Load(object sender, EventArgs e)
        {
            //单据类型
            CommonFuncCall.BindPurchaseOrderType(cboorder_type, true, "全部");
            //发票类型
            CommonFuncCall.BindComBoxDataSource(cboreceipt_type, "sys_receipt_type", "全部");
            //结算单位
            CommonFuncCall.BindBalanceWayByItem(cbobalance_way, "全部");
            //供应商类别
            CommonFuncCall.BindComBoxDataSource(cbosup_type, "sys_supplier_category", "全部");
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
        //供应商选择
        private void txtcsup_code_ChooserClick(object sender, EventArgs e)
        {
            frmSupplier frmSup = new frmSupplier();
            if (frmSup.ShowDialog() == DialogResult.OK)
            {
                txtcsup_code.Text = frmSup.supperCode;
                txtsup_name.Caption = frmSup.supperName;
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
        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindData()
        {
            //查询字段
            string fileds = "供应商编码,供应商名称,sum(采购货款) 采购货款,sum(税额) 税额,sum(金额) 金额,sup_id";
            //分组
            string groupBy = "group by 供应商编码,供应商名称,sup_id";
            dt = DBHelper.GetTable("", "v_parts_purchase_billing_summariz_report",fileds, GetWhere(), "", groupBy);
            dt.DataTableSum(null);//添加合计
            dgvReport.DataSource = dt;
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        /// <summary>
        /// 采购综合汇总表
        /// </summary>
        void OpenDocument()
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
            UCPurchaseBillingZongHe zonghe = new UCPurchaseBillingZongHe();
            zonghe.orderType = CommonCtrl.IsNullToString(cboorder_type.SelectedValue);
            zonghe.receiptType = CommonCtrl.IsNullToString(cboreceipt_type.SelectedValue);
            zonghe.balanceWay = CommonCtrl.IsNullToString(cbobalance_way.SelectedValue);
            zonghe.balanceAccount = CommonCtrl.IsNullToString(cbobalance_account.SelectedValue);
            zonghe.supCode = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colSupCode.Name].Value);
            zonghe.supName = supName;
            zonghe.supType = CommonCtrl.IsNullToString(cbosup_type.SelectedValue);
            zonghe.whCode = CommonCtrl.IsNullToString(cbowh_code.SelectedValue);
            zonghe.partsCode = txtcsup_code.Text;
            zonghe.partsName = txtPartsName.Caption;
            zonghe.drawingNum = txtdrawing_num.Caption;
            zonghe.partsBrand = txtparts_brand.Caption;
            zonghe.stratDate = dicreate_time.StartDate;
            zonghe.endDate = dicreate_time.EndDate;
            zonghe.commpany = CommonCtrl.IsNullToString(cboCompany.SelectedValue);
            zonghe.orgID = CommonCtrl.IsNullToString(cboorg_id.SelectedValue);
            base.addUserControl(zonghe, "采购综合汇总表", "UCPurchaseBillingZongHe", this.Tag.ToString(), this.Name);
        }

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDocument();
        }
    }
}
