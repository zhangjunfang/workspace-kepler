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
    public partial class UCSaleBillingZongHe : UCReport
    {
        #region 属性，用来设置查询条件的默认值
        public string orderType = string.Empty;
        public string receiptType = string.Empty;
        public string balanceWay = string.Empty;
        public string balanceAccount = string.Empty;
        public string supCode = string.Empty;
        public string supName = string.Empty;
        public string supType = string.Empty;
        public string whCode = string.Empty;
        public string partsCode = string.Empty;
        public string partsName = string.Empty;
        public string drawingNum = string.Empty;
        public string partsBrand = string.Empty;
        public string stratDate = DateTime.Now.ToString("yyyy-MM-01");
        public string endDate = DateTime.Now.ToString("yyyy-MM-dd");
        public string commpany = string.Empty;
        public string orgID = string.Empty;
        #endregion
        public UCSaleBillingZongHe()
            : base("v_sale_billing_zonghe_report", "销售开单综合汇总表")
        {
            InitializeComponent();

            colNum.DefaultCellStyle = styleNum;
            colGifNum.DefaultCellStyle = styleNum;
            colPayment.DefaultCellStyle = styleMoney;
            colTax.DefaultCellStyle = styleMoney;
            colMoney.DefaultCellStyle = styleMoney;
        }
        //窗体加载
        private void UCSaleBillingZongHe_Load(object sender, EventArgs e)
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
            #region 初始化查询
            cboorder_type.SelectedValue = orderType;
            cboreceipt_type.SelectedValue = receiptType;
            cbobalance_way.SelectedValue = balanceWay;
            cbobalance_account.SelectedValue = balanceAccount;
            txtcCust_code.Text = supCode;
            txtCust_name.Caption = supName;
            cboCust_type.SelectedValue = supType;
            cbowh_code.SelectedValue = whCode;
            txtcparts_code.Text = partsCode;
            txtPartsName.Caption = partsName;
            txtdrawing_num.Caption = drawingNum;
            txtparts_brand.Caption = partsBrand;
            dicreate_time.StartDate = stratDate;
            dicreate_time.EndDate = endDate;
            cboCompany.SelectedValue = commpany;
            cboorg_id.SelectedValue = orgID;
            #endregion
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
        private void txtcCust_code_ChooserClick(object sender, EventArgs e)
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
        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindData()
        {
            //查询字段
            string fileds = @"公司,cust_code,cust_name,配件编码,配件名称,图号,配件品牌,配件类别,厂商编码,业务单位,sum(数量) 数量,sum(销售货款) 销售货款,sum(税额) 税额,sum(金额) 金额,
                            sum(case when 赠品数量='1' then 1 else 0 end) 赠品数量 ";
            //分组
            string groupBy = "group by 公司,cust_code,cust_name,配件编码,配件名称,图号,配件品牌,配件类别,厂商编码,业务单位";
            dt = DBHelper.GetTable("", "v_sale_billing_zonghe_report", fileds, GetWhere(), "", groupBy);
            //按客户分组
            dt.DataTableGroup("cust_name", "公司", "客户名称：", "cust_code", "配件编码", "客户编码：", null);
            dgvReport.DataSource = dt;
        }
        /// <summary>
        /// 销售开单明细表
        /// </summary>
        void OpenDocument()
        {
            if (dgvReport.CurrentRow == null)
            {
                return;
            }
            partsName = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colPartsName.Name].Value);
            if (partsName.Length == 0)
            {
                return;
            }
            UCSaleBillingDetail detail = new UCSaleBillingDetail();
            detail.orderType = CommonCtrl.IsNullToString(cboorder_type.SelectedValue);
            detail.receiptType = CommonCtrl.IsNullToString(cboreceipt_type.SelectedValue);
            detail.balanceWay = CommonCtrl.IsNullToString(cbobalance_way.SelectedValue);
            detail.balanceAccount = CommonCtrl.IsNullToString(cbobalance_account.SelectedValue);
            detail.supCode = txtcCust_code.Text;
            detail.supName = txtCust_name.Caption;
            detail.supType = CommonCtrl.IsNullToString(cboCust_type.SelectedValue);
            detail.whCode = CommonCtrl.IsNullToString(cbowh_code.SelectedValue);
            detail.partsCode = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colPartsCode.Name].Value);
            detail.partsName = partsName;
            detail.drawingNum = txtdrawing_num.Caption;
            detail.partsBrand = txtparts_brand.Caption;
            detail.stratDate = dicreate_time.StartDate;
            detail.endDate = dicreate_time.EndDate;
            detail.commpany = CommonCtrl.IsNullToString(cboCompany.SelectedValue);
            detail.orgID = CommonCtrl.IsNullToString(cboorg_id.SelectedValue);
            base.addUserControl(detail, "销售开单明细表", "UCSaleBillingDetail", this.Tag.ToString(), this.Name);
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
    }
}
