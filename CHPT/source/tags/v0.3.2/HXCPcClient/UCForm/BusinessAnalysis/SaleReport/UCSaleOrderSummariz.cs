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
    public partial class UCSaleOrderSummariz : UCReport
    {
        public UCSaleOrderSummariz()
            : base("v_sale_order_summariz", "销售订单汇总表")
        {
            InitializeComponent();
        }
        //窗体加载
        private void UCSaleOrderSummariz_Load(object sender, EventArgs e)
        {
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            //客户类别
            CommonFuncCall.BindComBoxDataSource(cboSupType, "sys_customer_category", "全部");
            BindData();
        }
        //客户选择
        private void txtcSupCode_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo frmCust = new frmCustomerInfo();
            if (frmCust.ShowDialog() == DialogResult.OK)
            {
                txtcCustCode.Text = frmCust.strCustomerNo;
                txtCustName.Caption = frmCust.strCustomerName;
            }
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
        //配件类型选择
        private void txtcPartsType_ChooserClick(object sender, EventArgs e)
        {
            frmPartsType frmType = new frmPartsType();
            if (frmType.ShowDialog() == DialogResult.OK)
            {
                txtcPartsType.Text = frmType.TypeName;
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
            string files = "客户编码,客户名称,SUM(订货金额) 订货金额,SUM(已收货金额) 已收货金额,SUM(中止金额) 中止金额,SUM(未收货金额) 未收货金额";
            //分组
            string groupBy = " group by 客户编码,客户名称";
            dt = DBHelper.GetTable("", "v_sale_order_summariz_report", files, GetWhere(), "", groupBy);
            dt.DataTableSum(null);//添加合计
            dgvReport.DataSource = dt;
        }
        /// <summary>
        /// 打开销售订单明细表
        /// </summary>
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
            string supCode = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colSupCode.Name].Value);
            UCSaleOrderDetail detail = new UCSaleOrderDetail();
            detail.supCode = supCode;
            detail.supName = supName;
            detail.startDate = diCreateTime.StartDate;
            detail.endDate = diCreateTime.EndDate;
            detail.company = CommonCtrl.IsNullToString(cboCompany.SelectedValue);
            detail.orgId = CommonCtrl.IsNullToString(cboOrg.SelectedValue);
            detail.partsBrand = txtPartsBrand.Caption.Trim();
            detail.partsCode = txtcPartsCode.Text;
            detail.partsName = txtPartsName.Caption;
            detail.partsType = txtcPartsType.Text;
            detail.drawNum = txtDrawingNum.Caption;
            base.addUserControl(detail, "销售订单明细表", "UCSaleOrderDetail" + supCode, this.Tag.ToString(), this.Name);
        }

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDetail();
        }
    }
}