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

namespace HXCPcClient.UCForm.BusinessAnalysis.PurchaseReport
{
    public partial class UCPurchaseOrderSummariz : UCReport
    {
        public UCPurchaseOrderSummariz()
            : base("v_parts_purchase_order_summariz_report","采购订单汇总表")
        {
            InitializeComponent();
        }
        //窗体加载
        private void UCPurchaseOrderSummariz_Load(object sender, EventArgs e)
        {
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            //供应商类别
            CommonFuncCall.BindComBoxDataSource(cboSupType, "sys_supplier_category", "全部");
            BindData();
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
        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindData()
        {
            string files = "供应商编码,供应商名称,SUM(订货金额) 订货金额,SUM(已收货金额) 已收货金额,SUM(中止金额) 中止金额,SUM(未收货金额) 未收货金额,sup_id";
            string groupBy = " group by sup_id,供应商编码,供应商名称";
            dt = DBHelper.GetTable("", "v_parts_purchase_order_summariz_report", files, GetWhere(), "", groupBy);
            dt.DataTableSum(null);//添加合计
            dgvReport.DataSource = dt;
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        //供应商选择
        private void txtcSupCode_ChooserClick(object sender, EventArgs e)
        {
            frmSupplier frmSup = new frmSupplier();
            if (frmSup.ShowDialog() == DialogResult.OK)
            {
                txtcSupCode.Text = frmSup.supperCode;
                txtSupName.Caption = frmSup.supperName;
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

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDetail();
        }
        /// <summary>
        /// 打开采购订单明细表
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
            UCPurchaseOrderDetail detail = new UCPurchaseOrderDetail();
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
            base.addUserControl(detail, "采购订单明细表", "UCPurchaseOrderDetail" + supCode, this.Tag.ToString(), this.Name);
        }
    }
}
