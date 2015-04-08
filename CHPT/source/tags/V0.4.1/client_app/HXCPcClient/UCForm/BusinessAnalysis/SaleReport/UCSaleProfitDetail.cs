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
using HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleBilling;

namespace HXCPcClient.UCForm.BusinessAnalysis.SaleReport
{
    public partial class UCSaleProfitDetail : UCReport
    {
        #region 属性，用来设置查询条件的默认值
        public string orderType = string.Empty;
        public string custCode = string.Empty;
        public string custName = string.Empty;
        public string custType = string.Empty;
        public string isMember = string.Empty;
        public string whCode = string.Empty;
        public string partsType = string.Empty;
        public string vehicleModels = string.Empty;
        public string partsCode = string.Empty;
        public string partsName = string.Empty;
        public string drawingNum = string.Empty;
        public string partsBrand = string.Empty;
        public string stratDate = DateTime.Now.ToString("yyyy-MM-01");
        public string endDate = DateTime.Now.ToString("yyyy-MM-dd");
        public string commpany = string.Empty;
        public string orgID = string.Empty;
        public bool isGift = false;
        #endregion
        public UCSaleProfitDetail()
            : base("v_sale_profit_detail_report", "销售毛利明细表")
        {
            InitializeComponent();

            colNum.DefaultCellStyle = styleNum;
            colSalePrice.DefaultCellStyle = styleMoney;
            colCostPrice.DefaultCellStyle = styleMoney;
            colIncome.DefaultCellStyle = styleMoney;
            colCost.DefaultCellStyle = styleMoney;
            colGrossMargin.DefaultCellStyle = styleMoney;
            colRate.DefaultCellStyle = styleMoney;
        }
        //窗体加载
        private void UCSaleProfitDetail_Load(object sender, EventArgs e)
        {
            //单据类型
            CommonFuncCall.BindSaleOrderType(cboorder_type, true, "全部");
            //客户类别
            CommonFuncCall.BindComBoxDataSource(cboCust_type, "sys_customer_category", "全部");
            //仓库
            CommonFuncCall.BindWarehouse(cbowh_code, "全部");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            //是否会员
            DataSources.BindComBoxDataEnum(cboIsMember, typeof(DataSources.EnumYesNo), true);

            #region 初始化查询
            txtcCust_code.Text = custCode;
            txtCust_name.Caption = custName;
            cboCust_type.SelectedValue = custType;
            cboIsMember.SelectedValue = isMember;
            cboorder_type.SelectedValue = orderType;
            cbowh_code.SelectedValue = whCode;
            txtcPartsType.Text = partsType;
            txtcVehicleModels.Text = vehicleModels;
            txtcparts_code.Text = partsCode;
            txtPartsName.Caption = partsName;
            txtdrawing_num.Caption = drawingNum;
            txtparts_brand.Caption = partsBrand;
            dicreate_time.StartDate = stratDate;
            dicreate_time.EndDate = endDate;
            cboCompany.SelectedValue = commpany;
            cboorg_id.SelectedValue = orgID;
            chbGift.Checked = isGift;
            #endregion
            BindData();
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
        //配件类型选择
        private void txtcPartsType_ChooserClick(object sender, EventArgs e)
        {
            frmPartsType frmType = new frmPartsType();
            if (frmType.ShowDialog() == DialogResult.OK)
            {
                txtcPartsType.Text = frmType.TypeName;
            }
        }
        //车型选择
        private void txtcVehicleModels_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleModels frmModels = new frmVehicleModels();
            if (frmModels.ShowDialog() == DialogResult.OK)
            {
                txtcVehicleModels.Text = frmModels.VMName;
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindData()
        {
            string where = GetWhere();
            //判断是否包含赠品
            if (!chbGift.Checked)
            {
                where += " and is_gift='0'";
            }
            dt = DBHelper.GetTable("", "v_sale_profit_detail_report", "*", where, "", "order by cust_code");
            dt.DataTableToDate("单据日期");
            //单价不合计
            List<string> listNot = new List<string>();
            listNot.Add("销售价");
            listNot.Add("成本价");
            //按客户分组
            dt.DataTableGroup("cust_name", "公司", "客户名称：", "cust_code", "单据编号", "客户编码：", listNot);
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
        /// 打开销售开单
        /// </summary>
        void OpenDocument()
        {
            if (dgvReport.CurrentRow == null)
            {
                return;
            }
            string sale_billing_id = Convert.ToString(this.dgvReport.CurrentRow.Cells[colID.Name].Value);
            if (sale_billing_id.Length == 0)
            {
                return;
            }
            UCSaleBillView view = new UCSaleBillView(sale_billing_id, null);
            base.addUserControl(view, "销售开单-查看", "UCSaleBillView" + sale_billing_id + "", this.Tag.ToString(), this.Name);
        }
    }
}
