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
using System.Threading;
using HXCPcClient.CommonClass;

namespace HXCPcClient.HomeManage
{
    public partial class UCMaintain : UCView
    {
        #region --成员变量
        private int oldWidth = 0;
        private int oldHeight = 0;
        #endregion

        #region --构造函数
        public UCMaintain()
        {
            InitializeComponent();

            this.oldWidth = this.Width;
            this.oldHeight = this.Height;

            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.DoubleBuffer, true);
            miFX.Text = "返 修";
            miJY.Text = "救 援";
            miWXYY.Text = "维修预约";
            miWXJD.Text = "维修接待";
            miWXDD.Text = "维修调度";
            miXWJS.Text = "维修结算";
            miCWSK.Text = "财务收款";
            miJJFCD.Text = "旧件返厂单";
            miLLTH.Text = "领料退货";
            miSBFWD.Text = "三包服务单";
            miSBJSD.Text = "三包结算单";
            miWXLL.Text = "维修领料";
            //this.picWxyy.Enabled = false;
            //this.picFx.Enabled = false;
            //this.picWxjd.Enabled = false;
            //this.picWxdd.Enabled = false;
            //this.picWxjs.Enabled = false;
            //this.picJy.Enabled = false;
            //this.picCwsk.Enabled = false;
            //this.picLlth.Enabled = false;
            //this.picWxll.Enabled = false;
            //this.picSbfwd.Enabled = false;
            //this.picSbjsd.Enabled = false;
            //this.picJjfcd.Enabled = false;
        }
        #endregion

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

        #region --调整窗体
        public override void ResizeControl(int width, int height)
        {
            if (oldWidth > width)
            {
                this.Width = width;
            }
            else
            {
                this.Width = oldWidth;
            }
            int moveW = this.Width / 9;
            foreach (Control ctl in this.Controls)
            {
                if (ctl.Tag != null)
                {
                    string[] arrray = ctl.Tag.ToString().Split(',');
                    if (arrray.Length == 2)
                    {
                        int col = int.Parse(arrray[0]);
                        ctl.Location = new System.Drawing.Point(col * moveW + moveW / 2 - ctl.Width / 2,
                                ctl.Location.Y);
                    }
                }
            }
        }
        #endregion

        #region --窗体加载
        private void UCMaintain_Load(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.LoadFunEnable));
        }
        private delegate void SetEnable(PictureBox pb, string fun_id);
        private void SetFunEnable(PictureBox pb, string fun_id)
        {
            if (pb.InvokeRequired)
            {
                SetEnable se = new SetEnable(this.SetFunEnable);
                this.Invoke(se, pb, fun_id);
            }
            else
            {
                pb.Enabled = this.HasFunction(fun_id);
            }
        }
        private void LoadFunEnable(object obj)
        {
            //预约单
            if (HXCPcClient.GlobalStaticObj.gLoginDataSet != null
               && HXCPcClient.GlobalStaticObj.gLoginDataSet.Tables[2].Rows.Count > 0)
            {
                //维修预约
                //this.SetFunEnable(this.picWxyy, "CL_RepairBusiness_Reserve_ReserveOrder");
                ////返修
                //this.SetFunEnable(this.picFx, "CL_RepairBusiness_RepairCallback");               
                ////维修接待               
                //this.SetFunEnable(this.picWxjd, "CL_RepairBusiness_Receive"); 
                ////维修调度             
                //this.SetFunEnable(this.picWxdd, "CL_RepairBusiness_RepairDispatch"); 
                ////维修结算              
                //this.SetFunEnable(this.picWxjs, "CL_RepairBusiness_RepairBalance"); 
                ////救援              
                //this.SetFunEnable(this.picJy, "CL_RepairBusiness_RepairRescue"); 
                ////财务收款               
                //this.SetFunEnable(this.picCwsk, "CL_BusinessAnalysis_ARAP_RecCheck"); 
                ////领料退货              
                //this.SetFunEnable(this.picLlth, "CL_RepairBusiness_FetchMaterialReturn"); 
                ////维修领料             
                //this.SetFunEnable(this.picWxll, "CL_RepairBusiness_FetchMaterial"); 
                ////三包服务单               
                //this.SetFunEnable(this.picSbfwd, "CL_RepairBusiness__MaintainThreeGuaranty"); 
                ////三包结算单               
                //this.SetFunEnable(this.picSbjsd, "CL_RepairBusiness__GuarantySettlement"); 
                ////旧件返厂单              
                //this.SetFunEnable(this.picJjfcd, "CL_RepairBusiness_OldPartsManagement_YTOldPartsPalautus"); 
            }
        }
        #endregion

        #region --私有方法
        private bool HasFunction(string fun_id)
        {
            return LocalCache.HasFunction(fun_id);
        }
        #endregion
    }
}
