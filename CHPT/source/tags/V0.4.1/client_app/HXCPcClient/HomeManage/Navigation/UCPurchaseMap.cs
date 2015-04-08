using System;
using System.Windows.Forms;
using HXCPcClient.UCForm;
using HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseOrder;

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
            //UCReceiveManager uc = new UCReceiveManager();
            //string tag = "CL_RepairBusiness_Receive|CL_RepairBusiness|CL_RepairBusiness_Reserve";
            //UCBase.AddUserControl(uc, "维修接待", "CL_RepairBusiness_Receive", tag, "");
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
            
        }
        private void picCgdd_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackgroundImage = Properties.Resources.cgdd_on;
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

        #region --其他收货单
        private void picQtshd_Click(object sender, EventArgs e)
        {

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

        #region --其他发货单
        private void picQtfhd_Click(object sender, EventArgs e)
        {

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
    }
}
