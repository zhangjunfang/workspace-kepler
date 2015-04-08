using System;
using System.Windows.Forms;
using HXCPcClient.UCForm;
using HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseOrder;
using HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.YuTongPurchaseOrder;
using HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseBilling;
using HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleBilling;
using HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchasePlan;
using HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleOrder;
using HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SalePlan;
using HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.AllocationBill;
using HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.RequisitionBill;
using HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.ReportedLossBill;
using HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.InventoryBill;
using HXCPcClient.UCForm.DataManage.CustomerFiles;
using HXCPcClient.UCForm.DataManage.WareHouse;
using HXCPcClient.UCForm.DataManage.SupplierFile;
using HXCPcClient.UCForm.SysManage.Settlement;
using HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.StockQuery;
using HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.OtherReceiveGoods;
using HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.OtherSendGoods;

namespace HXCPcClient.HomeManage
{
    public partial class UCPurchaseMap : UCView
    {
        #region --成员变量
        private bool loadFlag = true;
        private int width = 0;
        #endregion        

        #region --构造函数
        public UCPurchaseMap()
        {
            InitializeComponent();

            SetStyle(                
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |              
                ControlStyles.DoubleBuffer, true);
            miYtcgd.Text = "宇通采购订单";
            miCgkd.Text = "采购开单";
            miXskd.Text = "销售开单";
            miXsdd.Text = "销售订单";
            miCgjh.Text = "采购计划";
            miCgdd.Text = "采购订单";
            miCrkd.Text = "出入库单";
            miXsjh.Text = "销售计划";
            miDbd.Text = "调拨单";
            miBsd.Text = "报损单";
            miPdd.Text = "盘点单";
            miQtfhd.Text = "其它发货单";
            miQtshd.Text = "其它收货单";
            miKhzl.Text = "客户资料";
            miGyszl.Text = "供应商资料";
            miCkzl.Text = "仓库资料";
            miJsfs.Text = "结算方式";
            miKccx.Text = "库存查询";
        }
        #endregion

        #region --单击事件
        private void UCPurchaseMap_Load(object sender, EventArgs e)
        {
            if (this.loadFlag)
            {
                this.width = this.Size.Width;
                this.loadFlag = false;
            }
        } 
        #endregion           

        #region --宇通采购订单
        private void picYtcgdd_Click(object sender, EventArgs e)
        {
            UCYTManager uc = new UCYTManager();
            string tag = "CL_AccessoriesBusiness|CL_PurchaseManagement_Function|CL_YTPurchaseOrder_Function";
            UCBase.AddUserControl(uc, "宇通采购订单", "CL_YTPurchaseOrder_Function", tag, "");
        }
        private void picYtcgdd_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.ytcgdd_on;
        }

        private void picYtcgdd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.ytcgdd;
        }
        #endregion

        #region --采购开单
        private void piccgkd_Click(object sender, EventArgs e)
        {
            UCPurchaseBillManang uc = new UCPurchaseBillManang();
            string tag = "CL_AccessoriesBusiness|CL_PurchaseManagement_Function|CL_UCPurchaseBill_Function";
            UCBase.AddUserControl(uc, "采购开单", "CL_UCPurchaseBill_Function", tag, "");
        }
        private void piccgkd_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.cgkd_on;
        }

        private void piccgkd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.cgkd;
        }
        #endregion

        #region --销售开单
        private void picXskd_Click(object sender, EventArgs e)
        {
            UCSaleBillManang uc = new UCSaleBillManang();
            string tag = "CL_AccessoriesBusiness|CL_SaleManagement_Function|CL_SaleBill_Function";
            UCBase.AddUserControl(uc, "销售开单", "CL_SaleBill_Function", tag, "");
        }
        private void picXskd_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.xskd_on;
        }

        private void picXskd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.xskd;
        }
        #endregion

        #region --采购计划
        private void picCgjh_Click(object sender, EventArgs e)
        {
            UCPurchasePlanOrderManager uc = new UCPurchasePlanOrderManager();
            string tag = "CL_AccessoriesBusiness|CL_PurchaseManagement_Function|CL_PurchasePlan_Function";
            UCBase.AddUserControl(uc, "采购计划", "CL_PurchasePlan_Function", tag, "");
        }
        private void picCgjh_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.cgjh_on;
        }
        private void picCgjh_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.cgjh;
        }
        #endregion      
        
        #region --采购订单
        private void picJxdd_Click(object sender, EventArgs e)
        {
            UCPurchaseOrderManager uc = new UCPurchaseOrderManager();
            string tag = "CL_AccessoriesBusiness|CL_PurchaseManagement_Function|CL_PurchaseOrder_Function";
            UCBase.AddUserControl(uc, "采购订单", "CL_PurchaseOrder_Function", tag, "");
        }
        private void picCgdd_MouseEnter(object sender, EventArgs e)
        {

        }

        private void picCgdd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.cgdd;
        }
        #endregion

        #region --出入库单
        private void picCrkd_Click(object sender, EventArgs e)
        {
            UCAllocationBillManager uc = new UCAllocationBillManager();
            string tag = "CL_AccessoriesBusiness|CL_StockManagement_Function|CL_AllocationBill_Function";
            UCBase.AddUserControl(uc, "出入库单", "CL_AllocationBill_Function", tag, "");
        }
        private void picCrkd_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.crkd_on;
        }

        private void picCrkd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.crkd;
        }
        #endregion

        #region --销售订单
        private void picXsdd_Click(object sender, EventArgs e)
        {
            UCSaleOrderManager uc = new UCSaleOrderManager();
            string tag = "CL_AccessoriesBusiness|CL_SaleManagement_Function|CL_SaleOrder_Function";
            UCBase.AddUserControl(uc, "销售订单", "CL_SaleOrder_Function", tag, "");
        }
        private void picXsdd_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.xsdd_on;
        }

        private void picXsdd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.xsdd;
        }        
        #endregion

        #region --销售计划
        private void picXsjh_Click(object sender, EventArgs e)
        {
            UCSalePlanManager uc = new UCSalePlanManager();
            string tag = "CL_AccessoriesBusiness|CL_SaleManagement_Function|CL_SalePlan_Function";
            UCBase.AddUserControl(uc, "销售计划", "CL_SalePlan_Function", tag, "");
        }
        private void picXsjh_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.xsjh_on;
        }

        private void picXsjh_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.xsjh;
        }        
        #endregion

        #region --调拨单
        private void picDpd_Click(object sender, EventArgs e)
        {
            UCRequisitionManager uc = new UCRequisitionManager();
            string tag = "CL_AccessoriesBusiness|CL_StockManagement_Function|CL_RequisitionBill_Function";
            UCBase.AddUserControl(uc, "调拨单", "CL_RequisitionBill_Function", tag, "");
        }
        private void picDpd_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.dpd_on;
        }

        private void picDpd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.dpd;
        }        
        #endregion

        #region --报损单
        private void picBsd_Click(object sender, EventArgs e)
        {
            UCReportedLossBillManager uc = new UCReportedLossBillManager();
            string tag = "CL_AccessoriesBusiness|CL_StockManagement_Function|CL_ReportedLossBill_Function";
            UCBase.AddUserControl(uc, "报损单", "CL_ReportedLossBill_Function", tag, "");
        }
        private void picBsd_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.bsd_on;
        }

        private void picBsd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.bsd;
        }
        #endregion

        #region --盘点单
        private void picPdd_Click(object sender, EventArgs e)
        {
            UCStockCheckManager uc = new UCStockCheckManager();
            string tag = "CL_AccessoriesBusiness|CL_StockManagement_Function|CL_StockCheck_Function";
            UCBase.AddUserControl(uc, "盘点单", "CL_StockCheck_Function", tag, "");
        }
        private void picPdd_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.pdd_on;
        }

        private void picPdd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.pdd;
        }
        #endregion

        #region --其它收货单
        private void picQtshd_Click(object sender, EventArgs e)
        {
            UCStockReceiptManager uc = new UCStockReceiptManager();
            string tag = "CL_AccessoriesBusiness|CL_StockManagement_Function|CL_StockReceipt_Function";
            UCBase.AddUserControl(uc, "其它收货单", "CL_StockReceipt_Function", tag, "");
        }
        private void picQtshd_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.qtshd_on;
        }

        private void picQtshd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.qtshd;
        }
        #endregion

        #region --其它发货单
        private void picQtfhd_Click(object sender, EventArgs e)
        {
            UCStockShippingManager uc = new UCStockShippingManager();
            string tag = "CL_AccessoriesBusiness|CL_StockManagement_Function|CL_StockShipping_Function";
            UCBase.AddUserControl(uc, "其它发货单", "CL_StockShipping_Function", tag, "");
        }
        private void picQtfhd_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.qtfhd_on;
        }

        private void picQtfhd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.qtfhd;
        }
        #endregion

        #region --客户资料
        private void picKhzl_Click(object sender, EventArgs e)
        {
            UCCustomerManager uc = new UCCustomerManager();
            string tag = "CL_DataManagement|CL_DataManagement_BasicData|CL_DataManagement_BasicData_Customer";
            UCBase.AddUserControl(uc, "客户资料", "CL_DataManagement_BasicData_Customer", tag, "");
        }
        private void picKhzl_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.khzl_on;
        }

        private void picKhzl_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.khzl;
        }
        #endregion

        #region --供应商资料
        private void picGyszl_Click(object sender, EventArgs e)
        {
            UCSupplierManager uc = new UCSupplierManager();
            string tag = "CL_DataManagement|CL_DataManagement_BasicData|CL_DataManagement_BasicData_SupplierFile";
            UCBase.AddUserControl(uc, "供应商资料", "CL_DataManagement_BasicData_SupplierFile", tag, "");
        }
        private void picGyszl_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.gyszl_on;
        }

        private void picGyszl_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.gyszl;
        }
        #endregion

        #region --仓库资料
        private void picCkzl_Click(object sender, EventArgs e)
        {
            UCWareHouseManager uc = new UCWareHouseManager();
            string tag = "CL_DataManagement|CL_DataManagement_BasicData|CL_DataManagement_BasicData_Warehouse";
            UCBase.AddUserControl(uc, "仓库资料", "CL_DataManagement_BasicData_Warehouse", tag, "");
        }
        private void picCkzl_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.ckzl_on;
        }

        private void picCkzl_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.ckzl;
        }
        #endregion

        #region --结算方式
        private void picJsfs_Click(object sender, EventArgs e)
        {
            UCSettlementManage uc = new UCSettlementManage();
            string tag = "CL_SystemManagement|CL_SystemManagement_Business|CL_SystemManagement_Business_Settlement";
            UCBase.AddUserControl(uc, "结算方式", "CL_SystemManagement_Business_Settlement", tag, "");
        }
        private void picJsfs_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.jsfs_on;
        }

        private void picJsfs_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.jsfs;
        }
        #endregion

        #region --库存查询
        private void picKccx_Click(object sender, EventArgs e)
        {
            UCStockQuery uc = new UCStockQuery();
            string tag = "CL_AccessoriesBusiness|CL_StockManagement_Function|CL_StockQuery_Function";
            UCBase.AddUserControl(uc, "库存查询", "CL_StockQuery_Function", tag, "");
        }
        private void picKccx_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.kccx_on;
        }

        private void picKccx_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.kccx;
        }
        #endregion        

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
