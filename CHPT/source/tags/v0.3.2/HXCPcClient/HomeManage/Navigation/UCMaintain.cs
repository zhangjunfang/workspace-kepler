using System;
using System.Windows.Forms;
using HXCPcClient.UCForm.RepairBusiness.Reserve;
using HXCPcClient.UCForm;
using HXCPcClient.UCForm.RepairBusiness.RepairCallback;
using HXCPcClient.UCForm.RepairBusiness.Receive;
using HXCPcClient.UCForm.RepairBusiness.RepairDispatch;
using HXCPcClient.UCForm.RepairBusiness.RepairBalance;
using HXCPcClient.UCForm.RepairBusiness.RepairRescue;
using HXCPcClient.UCForm.RepairBusiness.FetchMaterialReturn;
using HXCPcClient.UCForm.RepairBusiness.FetchMaterial;
using HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuaranty;
using HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuarantySettlement;
using HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsPalautus;
using HXCPcClient.UCForm.FinancialManagement.Receivable;
using HXCPcClient.UCForm.BusinessAnalysis.ARAPReport;

namespace HXCPcClient.HomeManage
{
    public partial class UCMaintain : UserControl
    {
        public UCMaintain()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
        }

        #region --维修预约
        private void picWxyy_Click(object sender, EventArgs e)
        {
            ReserveOrder uc = new ReserveOrder();           
            //dr["fun_id"].ToString() + "|" + dr["firstid"].ToString() + "|" + dr["parent_id"].ToString();//三级 |一级| 二级 菜单的id
            string tag = "CL_RepairBusiness_Reserve_ReserveOrder|CL_SystemManagement|CL_SystemManagement_BulletinManagement";
            UCBase.AddUserControl(uc, "预约单", "CL_RepairBusiness_Reserve_ReserveOrder", tag, "");
        }

        private void picWxyy_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.wxyy_on;
        }

        private void picWxyy_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.wxyy;
        }
        #endregion

        #region --返修
        private void picFx_Click(object sender, EventArgs e)
        {
            UCRepairCallbackManager uc = new UCRepairCallbackManager();           
            string tag = "CL_RepairBusiness_RepairCallback|CL_RepairBusiness|CL_RepairBusiness_Reserve";
            UCBase.AddUserControl(uc, "维修返修单", "CL_RepairBusiness_RepairCallback", tag, "");
        }

        private void picFx_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.fx_on;
        }

        private void picFx_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.fx;
        }
        #endregion

        #region --维修接待
        private void picWxjd_Click(object sender, EventArgs e)
        {
            UCReceiveManager uc = new UCReceiveManager();
            string tag = "CL_RepairBusiness_Receive|CL_RepairBusiness|CL_RepairBusiness_Reserve";
            UCBase.AddUserControl(uc, "维修接待", "CL_RepairBusiness_Receive", tag, "");
        }

        private void picWxjd_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.wxjd_on;
        }

        private void picWxjd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.wxjd;
        }
        #endregion

        #region --维修调度
        private void picWxdd_Click(object sender, EventArgs e)
        {           
            UCDispatchManager uc = new UCDispatchManager();
            string tag = "CL_RepairBusiness_RepairDispatch|CL_RepairBusiness|CL_RepairBusiness_Reserve";
            UCBase.AddUserControl(uc, "维修调度", "CL_RepairBusiness_RepairDispatch", tag, "");
        }

        private void picWxdd_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.wxdd_on;
        }

        private void picWxdd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.wxdd;
        }
        #endregion

        #region -维修结算
        private void picWxjs_Click(object sender, EventArgs e)
        {
            UCRepairBalanceManager uc = new UCRepairBalanceManager();
            string tag = "CL_RepairBusiness_RepairBalance|CL_RepairBusiness|CL_RepairBusiness_Reserve";
            UCBase.AddUserControl(uc, "维修结算", "CL_RepairBusiness_RepairBalance", tag, "");
        }

        private void picWxjs_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.wxjs_on;
        }

        private void picWxjs_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.wxjs;
        }
        #endregion

        #region --救援
        private void picJy_Click(object sender, EventArgs e)
        {
            UCRepairRescueManager uc = new UCRepairRescueManager();
            string tag = "CL_RepairBusiness_RepairRescue|CL_RepairBusiness|CL_RepairBusiness_Reserve";
            UCBase.AddUserControl(uc, "救援单", "CL_RepairBusiness_RepairRescue", tag, "");
        }

        private void picJy_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.jy_on;
        }

        private void picJy_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.jy;
        }
        #endregion

        #region --财务收款
        private void picCwsk_Click(object sender, EventArgs e)
        {           
            UCReceivableChecking uc = new UCReceivableChecking();
            string tag = "CL_BusinessAnalysis_ARAP_RecCheck|CL_BusinessAnalysis_ARAPReport|CL_BusinessAnalysis_ARAP_RecCheck";
            UCBase.AddUserControl(uc, "应收款对账单", "CL_BusinessAnalysis_ARAP_RecCheck", tag, "");
        }

        private void picCwsk_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.cwsk_on;
        }

        private void picCwsk_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.cwsk;
        }
        #endregion

        #region --领料退货
        private void picLlth_Click(object sender, EventArgs e)
        {
            UCFMaterialReturnManager uc = new UCFMaterialReturnManager();
            string tag = "CL_RepairBusiness_FetchMaterialReturn|CL_RepairBusiness|CL_RepairBusiness_Reserve";
            UCBase.AddUserControl(uc, "领料退货单", "CL_RepairBusiness_FetchMaterialReturn", tag, "");
        }

        private void picLlth_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.llth_on;
        }

        private void picLlth_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.llth;
        }
        #endregion

        #region --维修领料
        private void picWxll_Click(object sender, EventArgs e)
        {
            UCFetchMaterialManager uc = new UCFetchMaterialManager();
            string tag = "CL_RepairBusiness_FetchMaterial|CL_RepairBusiness|CL_RepairBusiness_Reserve";
            UCBase.AddUserControl(uc, "领料单", "CL_RepairBusiness_FetchMaterial", tag, "");
        }

        private void picWxll_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.wxll_on;
        }

        private void picWxll_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.wxll;
        }
        #endregion

        #region --三包服务单
        private void picSbfwd_Click(object sender, EventArgs e)
        {
            UCMaintainThreeGuarantyManager uc = new UCMaintainThreeGuarantyManager();
            string tag = "CL_RepairBusiness__MaintainThreeGuaranty|CL_RepairBusiness|CL_RepairBusiness_Reserve";
            UCBase.AddUserControl(uc, "三包服务单", "CL_RepairBusiness__MaintainThreeGuaranty", tag, "");
        }

        private void picSbfwd_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.sbfwd_on;
        }

        private void picSbfwd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.sbfwd;
        }
        #endregion

        #region --三包结算单
        private void picSbjsd_Click(object sender, EventArgs e)
        {
            UCThreeGuarantySettlementManager uc = new UCThreeGuarantySettlementManager();
            string tag = "CL_RepairBusiness__GuarantySettlement|CL_RepairBusiness|CL_RepairBusiness_Reserve";
            UCBase.AddUserControl(uc, "三包结算单", "CL_RepairBusiness__GuarantySettlement", tag, "");
        }

        private void picSbjsd_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.sbjsd_on;
        }

        private void picSbjsd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.sbjsd;
        }
        #endregion        

        #region --旧件返厂单
        private void picJjfcd_Click(object sender, EventArgs e)
        {
            UCOldPartsPalautusManager uc = new UCOldPartsPalautusManager();
            string tag = "CL_RepairBusiness_OldPartsManagement_YTOldPartsPalautus|CL_RepairBusiness|CL_RepairBusiness_Reserve";
            UCBase.AddUserControl(uc, "旧件返厂单", "CL_RepairBusiness_OldPartsManagement_YTOldPartsPalautus", tag, "");
        }

        private void picJjfcd_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.jjfc_on;
        }

        private void picJjfcd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.jjfc;
        }
        #endregion
    }
}
