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
    public partial class UCSalePlanSummariz : UCReport
    {
        public UCSalePlanSummariz()
            : base("v_sale_plan_summariz_report", "销售计划汇总表")
        {
            InitializeComponent();

            colPlanNum.DefaultCellStyle = styleNum;
            colPlanMoney.DefaultCellStyle = styleMoney;
            colOrderMoney.DefaultCellStyle = styleMoney;
            colCompleteNum.DefaultCellStyle = styleNum;
            colCompleteMoney.DefaultCellStyle = styleMoney;
            colUnfinishedNum.DefaultCellStyle = styleNum;
            colUnfinishedMoney.DefaultCellStyle = styleMoney;
            colCompleteRate.DefaultCellStyle = styleMoney;
        }
        //窗体加载
        private void UCSalePlanSummariz_Load(object sender, EventArgs e)
        {
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            BindData();
        }
        //配件选择
        private void txtcPartsCode_ChooserClick(object sender, EventArgs e)
        {
            frmParts parts = new frmParts();
            if (parts.ShowDialog() == DialogResult.OK)
            {
                txtcPartsCode.Text = parts.PartsCode;
                txtPartsName.Caption = parts.PartsName;
            }
        }
        //选择公司，绑定公司所有部门
        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCompany.SelectedValue == null)
            {
                return;
            }
            CommonFuncCall.BindDepartment(cboOrg, cboCompany.SelectedValue.ToString(), "全部");
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindData()
        {
            //查询字段
            string fileds = @" 配件编码, 配件名称, 配件图号, 品牌,  厂商编码, 单位,
sum(business_count) 计划数量,sum(business_count*business_price) 计划金额,sum(isnull(已订数量,0)*business_price) 已订金额,sum(finish_count) 完成数量,
sum(finish_count*business_price) 完成金额,sum(business_count-finish_count) 未完成数量,0 已订金额,
sum((business_count-finish_count)*business_price) 未完成金额,case when sum(finish_count)=0 then 0 else sum(business_count)/sum(finish_count) end 完成比率";
            //分组
            string groupBy = "group by 配件编码,配件名称,配件图号,品牌,厂商编码,单位";
            dt = DBHelper.GetTable("", "v_sale_plan_summariz_report", fileds, GetWhere(), "", groupBy);
            dt.DataTableSum(null);//添加合计
            dgvSummariz.DataSource = dt;
        }

        private void dgvSummariz_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDocumnet();
        }
        /// <summary>
        /// 打开销售计划明细表
        /// </summary>
        void OpenDocumnet()
        {
            if (dgvSummariz.CurrentRow == null)
            {
                return;
            }
            string partsName = CommonCtrl.IsNullToString(dgvSummariz.CurrentRow.Cells[colPartsNme.Name].Value);
            if (partsName.Length == 0)
            {
                return;
            }
            string partsCode = CommonCtrl.IsNullToString(dgvSummariz.CurrentRow.Cells[colPartsCode.Name].Value);
            UCSalePlanDetail detail = new UCSalePlanDetail();
            detail.partsCode = partsCode;
            detail.partsName = partsName;
            detail.partsBland = CommonCtrl.IsNullToString(dgvSummariz.CurrentRow.Cells[colParts_brand.Name].Value);
            detail.drawNum = CommonCtrl.IsNullToString(dgvSummariz.CurrentRow.Cells[colDrawing_num.Name].Value);
            detail.startDate = dateInterval1.StartDate;
            detail.endDate = dateInterval1.EndDate;
            detail.company = CommonCtrl.IsNullToString(cboCompany.SelectedValue);
            detail.orgId = CommonCtrl.IsNullToString(cboOrg.SelectedValue);
            base.addUserControl(detail, "销售计划明细表", "UCSalePlanDetail" + partsCode, this.Tag.ToString(), this.Name);
        }
    }
}
