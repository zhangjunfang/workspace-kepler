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
    public partial class UCPurchaseDiscountSummariz : UCReport
    {
        public UCPurchaseDiscountSummariz()
            : base("v_purchase_discount_summariz_report","采购商品折扣汇总表")
        {
            InitializeComponent();

            colNum.DefaultCellStyle = styleNum;
            colPrice.DefaultCellStyle = styleMoney;
            colMoney.DefaultCellStyle = styleMoney;
            colDiscount.DefaultCellStyle = styleMoney;
            colDiscountPrice.DefaultCellStyle = styleMoney;
            colDiscountMoney.DefaultCellStyle = styleMoney;
        }
        //窗体加载
        private void UCPurchaseDiscountSummariz_Load(object sender, EventArgs e)
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
            string files = @"配件编码,配件名称,图号,配件品牌,配件类别,厂商编码,单位,sum(数量) 数量,case when SUM(数量)=0 then 0 else sum(金额)/sum(数量) end 价格,
        sum(金额) 金额,case when SUM(数量)=0 then 0 else sum(折扣金额)/sum(数量) end 折后单价,sum(折扣金额) 折后金额,sum(金额)-sum(折扣金额) 折扣金额";
            string groupBy = "group by 配件编码,配件名称,图号,配件品牌,配件类别,厂商编码,单位";
            dt = DBHelper.GetTable("", "v_purchase_discount_summariz_report", files, GetWhere(), "", groupBy);
            //单价不合计
            List<string> listNot = new List<string>();
            listNot.Add("价格");
            listNot.Add("折后单价");
            dt.DataTableSum(listNot);
            dgvReport.DataSource = dt;
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDocument();
        }
        /// <summary>
        /// 打开采购商品折扣明细表
        /// </summary>
        void OpenDocument()
        {
            if (dgvReport.CurrentRow == null)
            {
                return;
            }
            string partsName = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colPartsName.Name].Value);
            if (partsName.Length == 0)
            {
                return;
            }
            UCPurchaseDiscountDetail detail = new UCPurchaseDiscountDetail();
            detail.orderType = CommonCtrl.IsNullToString(cboorder_type.SelectedValue);
            detail.receiptType = CommonCtrl.IsNullToString(cboreceipt_type.SelectedValue);
            detail.balanceWay = CommonCtrl.IsNullToString(cbobalance_way.SelectedValue);
            detail.balanceAccount = CommonCtrl.IsNullToString(cbobalance_account.SelectedValue);
            detail.supCode = txtcsup_code.Text;
            detail.supName = txtsup_name.Caption;
            detail.supType = CommonCtrl.IsNullToString(cbosup_type.SelectedValue);
            detail.whCode = CommonCtrl.IsNullToString(cbowh_code.SelectedValue);
            detail.partsCode = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colPartsCode.Name].Value);
            detail.partsName = partsName;
            detail.drawingNum = txtdrawing_num.Caption;
            detail.partsBrand = txtparts_brand.Caption;
            detail.stratDate = dicreate_time.StartDate;
            detail.endDate = dicreate_time.EndDate;
            detail.commpany = CommonCtrl.IsNullToString(cboCompany.SelectedValue);
            detail.orgID = CommonCtrl.IsNullToString(cboorg_id.SelectedValue);
            base.addUserControl(detail, "采购商品折扣明细表", "UCPurchaseDiscountDetail", this.Tag.ToString(), this.Name);
        }
    }
}
