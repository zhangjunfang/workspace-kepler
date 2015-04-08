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
using HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseBilling;

namespace HXCPcClient.UCForm.BusinessAnalysis.PurchaseReport
{
    public partial class UCPurchaseDiscountDetail : UCReport
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
        public UCPurchaseDiscountDetail()
            : base("v_purchase_discount_detail_report", "采购商品折扣明细表")
        {
            InitializeComponent();

            colNum.DefaultCellStyle = styleNum;
            colPrice.DefaultCellStyle = styleMoney;
            colMoney.DefaultCellStyle = styleMoney;
            colDiscountRate.DefaultCellStyle = styleMoney;
            colDiscount.DefaultCellStyle = styleMoney;
            colDiscountPrice.DefaultCellStyle = styleMoney;
            colDiscountMoney.DefaultCellStyle = styleMoney;
            Quick qSupp = new Quick();
            qSupp.BindSupplier(txtcsup_code);
            txtcsup_code.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtcSup_code_DataBacked);

            qSupp.BindParts(txtcparts_code);
            txtcparts_code.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtcparts_code_DataBacked);
        }

        void txtcparts_code_DataBacked(DataRow dr)
        {
            txtcparts_code.Text = CommonCtrl.IsNullToString(dr["ser_parts_code"]);
            txtPartsName.Caption = CommonCtrl.IsNullToString(dr["parts_name"]);
        }

        void txtcSup_code_DataBacked(DataRow dr)
        {
            txtsup_name.Caption = CommonCtrl.IsNullToString(dr["sup_full_name"]);
            txtcsup_code.Text = CommonCtrl.IsNullToString(dr["sup_code"]);
        }
        //窗体加载
        private void UCPurchaseDiscountDetail_Load(object sender, EventArgs e)
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
            #region 初始化查询
            cboorder_type.SelectedValue = orderType;
            cboreceipt_type.SelectedValue = receiptType;
            cbobalance_way.SelectedValue = balanceWay;
            cbobalance_account.SelectedValue = balanceAccount;
            txtcsup_code.Text = supCode;
            txtsup_name.Caption = supName;
            cbosup_type.SelectedValue = supType;
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

            //双击查看明细,要手动调用权限
            if (this.Name != "CL_BusinessAnalysis_Purchase_DiscountDet")
            {
                base.RoleButtonStstus("CL_BusinessAnalysis_Purchase_DiscountDet");
            }
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
            dt = DBHelper.GetTable("", "v_purchase_discount_detail_report", "*", GetWhere(), "", "order by 配件名称");
            dt.DataTableToDate("单据日期");
            //单价不合计
            List<string> listNot = new List<string>();
            listNot.Add("create_time");
            listNot.Add("单价");
            listNot.Add("折扣率");
            listNot.Add("折后单价");
            //按配件分组
            dt.DataTableGroup("配件名称", "公司", "配件名称：", "配件编码", "单据日期", "配件编码：", listNot);
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
        /// 打开采购开单
        /// </summary>
        void OpenDocument()
        {
            if (dgvReport.CurrentRow == null)
            {
                return;
            }
            string purchase_billing_id = Convert.ToString(this.dgvReport.CurrentRow.Cells[colID.Name].Value);
            if (purchase_billing_id.Length == 0)
            {
                return;
            }
            UCPurchaseBillView UCPurchaseBillView = new UCPurchaseBillView(purchase_billing_id, null);
            base.addUserControl(UCPurchaseBillView, "采购开单-查看", "UCPurchaseBillView" + purchase_billing_id + "", this.Tag.ToString(), this.Name);
        }
    }
}
