using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseOrder;
using HXCPcClient.UCForm;

namespace HXCPcClient.HomeManage
{
    public partial class UCPurchaseMap : UserControl
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
                ControlStyles.ResizeRedraw |
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

        private void picJxdd_Click(object sender, EventArgs e)
        {
            UCPurchaseOrderManager ucItem = new UCPurchaseOrderManager();
            string tag = "UCPurchaseOrderManager|UCPurchaseOrderManager_001|UCPurchaseOrderManager_002";
            UCBase.AddUserControl(ucItem, "进货订单", "UCPurchaseOrderManager", tag, "UCPurchaseOrderManager");
        }

        private void picCgjh_Click(object sender, EventArgs e)
        {

        }

        private void picYwfk_Click(object sender, EventArgs e)
        {

        }

        private void picKcpj_Click(object sender, EventArgs e)
        {

        }

        private void picCgth_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region --MouseEnter和MouserLeave
        private void picJxdd_MouseEnter(object sender, EventArgs e)
        {
            this.picJxdd.BackgroundImage = Properties.Resources.jhdd_on;
        }

        private void picJxdd_MouseLeave(object sender, EventArgs e)
        {
            this.picJxdd.BackgroundImage = Properties.Resources.jhdd;
        }

        private void picCgjh_MouseEnter(object sender, EventArgs e)
        {
            this.picCgjh.BackgroundImage = Properties.Resources.cgjh_on;
        }

        private void picCgjh_MouseLeave(object sender, EventArgs e)
        {
            this.picCgjh.BackgroundImage = Properties.Resources.cgjh;
        }
        private void picYwfk_MouseEnter(object sender, EventArgs e)
        {
            this.picYwfk.BackgroundImage = Properties.Resources.ywfk_on;
        }

        private void picYwfk_MouseLeave(object sender, EventArgs e)
        {
            this.picYwfk.BackgroundImage = Properties.Resources.ywfk;
        }

        private void picKcpj_MouseEnter(object sender, EventArgs e)
        {
            this.picKcpj.BackgroundImage = Properties.Resources.kcpj_on;
        }

        private void picKcpj_MouseLeave(object sender, EventArgs e)
        {
            this.picKcpj.BackgroundImage = Properties.Resources.kcpj;
        }

        private void picCgth_MouseEnter(object sender, EventArgs e)
        {
            this.picCgth.BackgroundImage = Properties.Resources.cgth_on;
        }

        private void picCgth_MouseLeave(object sender, EventArgs e)
        {
            this.picCgth.BackgroundImage = Properties.Resources.cgth;
        }
        #endregion                
    }
}
