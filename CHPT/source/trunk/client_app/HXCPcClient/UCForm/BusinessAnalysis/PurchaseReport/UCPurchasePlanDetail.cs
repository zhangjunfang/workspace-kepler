using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using Utility.Common;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchasePlan;

namespace HXCPcClient.UCForm.BusinessAnalysis.PurchaseReport
{
    public partial class UCPurchasePlanDetail : UCReport
    {
        #region 属性，用来设置查询条件的默认值
        public string partsName = string.Empty;
        public string partsCode = string.Empty;
        public string company = string.Empty;
        public string orgId = string.Empty;
        public string startDate = DateTime.Now.ToString("yyyy-MM-01");
        public string endDate = DateTime.Now.ToString("yyyy-MM-dd");
        public string drawNum = string.Empty;
        public string partsBland = string.Empty;
        #endregion
        public UCPurchasePlanDetail()
            : base("v_parts_purchase_plan_detail_report","采购计划明细表")
        {
            InitializeComponent();

            colPrice.DefaultCellStyle = styleMoney;
            colPlanNum.DefaultCellStyle = styleNum;
            colPlanMoney.DefaultCellStyle = styleMoney;
            colOrderMoney.DefaultCellStyle = styleMoney;
            colCompleteNum.DefaultCellStyle = styleNum;
            colCompleteMoney.DefaultCellStyle = styleMoney;
            colUnfinishedNum.DefaultCellStyle = styleNum;
            colUnfinishedMoney.DefaultCellStyle = styleMoney;
            colCompleteRate.DefaultCellStyle = styleMoney;

            Quick qParts = new Quick();
            qParts.BindParts(txtcPartsCode);
            txtcPartsCode.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtcPartsCode_DataBacked);
        }

        void txtcPartsCode_DataBacked(DataRow dr)
        {
            txtcPartsCode.Text = CommonCtrl.IsNullToString(dr["ser_parts_code"]);
            txtPartsName.Caption = CommonCtrl.IsNullToString(dr["parts_name"]);
        }
        //窗体加载
        private void UCPurchasePlanDetail_Load(object sender, EventArgs e)
        {
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            #region 初始化查询
            txtcPartsCode.Text = partsCode;
            txtPartsBrand.Caption = partsBland;
            txtPartsName.Caption = partsName;
            txtPartsNum.Caption = drawNum;
            cboCompany.SelectedValue = company;
            cboOrg.SelectedValue = orgId;
            diCreateTime.StartDate = startDate;
            diCreateTime.EndDate = endDate;
            #endregion
            BindData();
            //双击查看明细,要手动调用权限
            if (this.Name != "CL_BusinessAnalysis_Purchase_PlanDet")
            {
                base.RoleButtonStstus("CL_BusinessAnalysis_Purchase_PlanDet");
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
            dt = DBHelper.GetTable("", "v_parts_purchase_plan_detail_report", "*", GetWhere(), "", "order by parts_name");
            dt.DataTableToDate("单据日期");
            //按配件分组
            dt.DataTableGroup("parts_name", "公司", "配件名称：", "parts_code", "单据编码", "配件编码：", null);
            dgvReport.DataSource = dt;
        }

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDocument();
        }
        /// <summary>
        /// 打开采购计划单
        /// </summary>
        void OpenDocument()
        {
            if (dgvReport.CurrentRow == null)
            {
                return;
            }
            string plan_Id = Convert.ToString(dgvReport.CurrentRow.Cells[colID.Name].Value);
            if (plan_Id.Length == 0)
            {
                return;
            }
            UCPurchasePlanOrderView UCPurchasePlanOrderView = new UCPurchasePlanOrderView(plan_Id, null);
            base.addUserControl(UCPurchasePlanOrderView, "采购计划单-查看", "UCPurchasePlanOrderView" + plan_Id + "", this.Tag.ToString(), this.Name);
        }
    }
}
