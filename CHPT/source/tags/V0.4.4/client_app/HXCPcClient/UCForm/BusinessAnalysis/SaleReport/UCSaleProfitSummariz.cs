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

namespace HXCPcClient.UCForm.BusinessAnalysis.SaleReport
{
    public partial class UCSaleProfitSummariz : UCReport
    {
        public UCSaleProfitSummariz()
            : base("v_sale_profit_summariz_report", "销售毛利汇总表")
        {
            InitializeComponent();

            colIncome.DefaultCellStyle = styleMoney;
            colCost.DefaultCellStyle = styleMoney;
            colGrossMargin.DefaultCellStyle = styleMoney;
            colRate.DefaultCellStyle = styleMoney;
        }
        //窗体加载
        private void UCSaleProfitSummariz_Load(object sender, EventArgs e)
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
            //查询字段
            string fileds = @" 客户编码, 客户名称,sum(销售收入) 销售收入,sum(销售成本) 销售成本,
                        case when sum(销售收入)<>0 then sum(销售成本)/sum(销售收入) else 0 end 销售毛利率";
            //分组
            string groupBy = "group by 客户编码, 客户名称";
            dt = DBHelper.GetTable("", "v_sale_profit_summariz_report", fileds, where, "", groupBy);
            dt.DataTableSum(null);//添加合计
            dgvReport.DataSource = dt;
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        /// <summary>
        /// 打开销售毛利明细表
        /// </summary>
        void OpenDetail()
        {
            if (dgvReport.CurrentRow == null)
            {
                return;
            }
            string custName = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[Column2.Name].Value);
            if (custName.Length == 0)
            {
                return;
            }
            UCSaleProfitDetail detail = new UCSaleProfitDetail();
            detail.custCode = txtcCust_code.Text;
            detail.custName = txtCust_name.Caption;
            detail.custType = CommonCtrl.IsNullToString(cboCust_type.SelectedValue);
            detail.isMember = CommonCtrl.IsNullToString(cboIsMember.SelectedValue);
            detail.orderType = CommonCtrl.IsNullToString(cboorder_type.SelectedValue);
            detail.whCode = CommonCtrl.IsNullToString(cbowh_code.SelectedValue);
            detail.partsType = txtcPartsType.Text;
            detail.vehicleModels = txtcVehicleModels.Text;
            detail.custCode = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[Column1.Name].Value);
            detail.custName = custName;
            detail.drawingNum = txtdrawing_num.Caption;
            detail.partsBrand = txtparts_brand.Caption;
            detail.stratDate = dicreate_time.StartDate;
            detail.endDate = dicreate_time.EndDate;
            detail.commpany = CommonCtrl.IsNullToString(cboCompany.SelectedValue);
            detail.orgID = CommonCtrl.IsNullToString(cboorg_id.SelectedValue);
            detail.isGift = chbGift.Checked;
            base.addUserControl(detail, "销售毛利明细表", "UCSaleProfitDetail", this.Tag.ToString(), this.Name);
        }

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDetail();
        }
    }
}
