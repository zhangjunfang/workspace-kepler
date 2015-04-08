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
using Utility.Common;

namespace HXCPcClient.UCForm.BusinessAnalysis
{
    public partial class UCDayToDayAccountReport : UCReport
    {
        public UCDayToDayAccountReport()
            : base("v_day_to_day_account_report", "流水账报表")
        {
            InitializeComponent();
            cboOrderType.Text = "全部";
        }

        private void UCDayToDayAccountReport_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");
            BindData();
        }
        //选择公司，绑定部门
        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comID = CommonCtrl.IsNullToString(cboCompany.SelectedValue);
            if (comID.Length == 0)
            {
                return;
            }
            CommonFuncCall.BindDepartment(cboOrg, comID, "全部");
        }
        //往来单位
        private void txtcDepartment_ChooserClick(object sender, EventArgs e)
        {
            frmBtype frmbtype = new frmBtype();
            if (frmbtype.ShowDialog() == DialogResult.OK)
            {
                txtcDepartment.Text = frmbtype.BtypeName;
            }
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        //绑定数据
        void BindData()
        {
            string strWhere = GetWhere();
            if (cboOrderType.Text != "全部")
            {
                strWhere += " and 单据类型='" + cboOrderType.Text + "'";
            }
            string filed = @"id,公司,单据日期,单据号,单据类型,往来单位,金额,部门,经办人,操作人";
            dt = DBHelper.GetTable("流水账报表", "v_day_to_day_account_report", filed, strWhere, "", "");
            dt.DataTableToDate("单据日期");
            //dt.DataTableSum(null);
            dgvReport.DataSource = dt;
        }

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDocument();
        }
        /// <summary>
        /// 打开单据
        /// </summary>
        void OpenDocument()
        {
            if (dgvReport.CurrentRow == null)
            {
                return;
            }
            string id = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colID.Name].Value);//单据ID
            if (id.Length == 0)
            {
                return;
            }
            string orderType = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colOrderType.Name].Value);//单据类型
            switch (orderType)
            {
                case "采购计划单":
                    HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchasePlan.UCPurchasePlanOrderView ucPurchasePlan = new AccessoriesBusiness.PurchaseManagement.PurchasePlan.UCPurchasePlanOrderView(id, null);
                    base.addUserControl(ucPurchasePlan, "采购计划单-查看", "UCPurchasePlanOrderView", this.Tag.ToString(), this.Name);
                    break;
                case "采购订单":
                    HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseOrder.UCPurchaseOrderView ucPurchaseOrder = new AccessoriesBusiness.PurchaseManagement.PurchaseOrder.UCPurchaseOrderView(id, "1", null);
                    base.addUserControl(ucPurchaseOrder, "采购订单-查看", "UCPurchaseOrderView", this.Tag.ToString(), this.Name);
                    break;
                case "宇通采购订单":
                    HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.YuTongPurchaseOrder.UCYTView ucYtView = new AccessoriesBusiness.PurchaseManagement.YuTongPurchaseOrder.UCYTView(id, null);
                    base.addUserControl(ucYtView, "宇通采购订单-查看", "UCYTView", this.Tag.ToString(), this.Name);
                    break;
                case "采购收货单":
                case "采购退货单":
                case "采购换货单":
                    HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseBilling.UCPurchaseBillView ucPurchaseBill = new AccessoriesBusiness.PurchaseManagement.PurchaseBilling.UCPurchaseBillView(id, null);
                    base.addUserControl(ucPurchaseBill, "采购开单-查看", "UCPurchaseBillView", this.Tag.ToString(), this.Name);
                    break;
                case "销售计划单":
                    HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SalePlan.UCSalePlanView ucSalePlan = new AccessoriesBusiness.SaleManagement.SalePlan.UCSalePlanView(id, null);
                    base.addUserControl(ucSalePlan, "销售计划单-查看", "UCSalePlanView", this.Tag.ToString(), this.Name);
                    break;
                case "销售订单":
                    HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleOrder.UCSaleOrderView ucSaleOrder = new AccessoriesBusiness.SaleManagement.SaleOrder.UCSaleOrderView(id, "1", null);
                    base.addUserControl(ucSaleOrder, "销售订单-查看", "UCSaleOrderView", this.Tag.ToString(), this.Name);
                    break;
                case "销售开单":
                case "销售退货单":
                case "销售换货单":
                    HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleBilling.UCSaleBillView ucSaleBill = new AccessoriesBusiness.SaleManagement.SaleBilling.UCSaleBillView(id, null);
                    base.addUserControl(ucSaleBill, "销售开单-查看", "UCSaleBillView", this.Tag.ToString(), this.Name);
                    break;
                case "入库单":
                case "出库单":
                    HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.AllocationBill.UCAllocationBillDetails ucAllocation=new AccessoriesBusiness.WarehouseManagement.AllocationBill.UCAllocationBillDetails (id);
                    base.addUserControl(ucAllocation, "出入库单-查看", "UCAllocationBillDetails", this.Tag.ToString(), this.Name);
                    break;
                case "调拨单":
                    HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.RequisitionBill.UCRequisitionBillDetail ucRequisition=new AccessoriesBusiness.WarehouseManagement.RequisitionBill.UCRequisitionBillDetail (id);
                    base.addUserControl(ucRequisition, "调拨单-查看", "UCRequisitionBillDetail", this.Tag.ToString(), this.Name);
                    break;
                case "报损单":
                    HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.ReportedLossBill.UCReportedLossBillDetail ucReportedLoss=new AccessoriesBusiness.WarehouseManagement.ReportedLossBill.UCReportedLossBillDetail (id,null);
                    base.addUserControl(ucReportedLoss, "报损单-查看", "UCReportedLossBillDetail", this.Tag.ToString(), this.Name);
                    break;
                case "盘点单":
                    HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.InventoryBill.UCStockCheckDetail ucStockCheck = new AccessoriesBusiness.WarehouseManagement.InventoryBill.UCStockCheckDetail(id, null);
                    base.addUserControl(ucStockCheck, "盘点单-查看", "UCStockCheckDetail", this.Tag.ToString(), this.Name);
                    break;
                case "调价单":
                    HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.ModifyPriceBill.UCModifyPriceDetail ucModifyPrice = new AccessoriesBusiness.WarehouseManagement.ModifyPriceBill.UCModifyPriceDetail(id, "");
                    base.addUserControl(ucModifyPrice, "调价单-查看", "UCModifyPriceDetail", this.Tag.ToString(), this.Name);
                    break;
                case "其他发货单":
                    HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.OtherSendGoods.UCStockShippingDetail ucStockSend = new AccessoriesBusiness.WarehouseManagement.OtherSendGoods.UCStockShippingDetail(id, null);
                    base.addUserControl(ucStockSend, "其他发货单-查看", "UCStockShippingDetail", this.Tag.ToString(), this.Name);
                    break;
                case "其他收货单":
                    HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.OtherReceiveGoods.UCStockReceiptDetail ucStockReceipt = new AccessoriesBusiness.WarehouseManagement.OtherReceiveGoods.UCStockReceiptDetail(id, null);
                    base.addUserControl(ucStockReceipt, "其他收货单-查看", "UCStockReceiptDetail", this.Tag.ToString(), this.Name);
                    break;
                case "预收款单":
                case "收款单":
                    HXCPcClient.UCForm.FinancialManagement.Receivable.UCReceivableAdd ucReceivable = new FinancialManagement.Receivable.UCReceivableAdd(WindowStatus.View, id, null, SYSModel.DataSources.EnumOrderType.RECEIVABLE);
                    base.addUserControl(ucReceivable, "应收账款-查看", "UCReceivableView", this.Tag.ToString(), this.Name);
                    break;
                case "预付款单":
                case "付款单":
                    HXCPcClient.UCForm.FinancialManagement.Receivable.UCReceivableAdd ucPayment = new FinancialManagement.Receivable.UCReceivableAdd(WindowStatus.View, id, null, SYSModel.DataSources.EnumOrderType.PAYMENT);
                    base.addUserControl(ucPayment, "应付账款-查看", "UCPaymentView", this.Tag.ToString(), this.Name);
                    break;
                case "往来核销单":
                    HXCPcClient.UCForm.FinancialManagement.AccountVerification.UCAccountVerificationAdd ucAccountVerification = new FinancialManagement.AccountVerification.UCAccountVerificationAdd(WindowStatus.View, id, null);
                    base.addUserControl(ucAccountVerification, "往来核销单-查看", "UCAccountVerificationView", this.Tag.ToString(), this.Name);
                    break;
            }
        }
    }
}
