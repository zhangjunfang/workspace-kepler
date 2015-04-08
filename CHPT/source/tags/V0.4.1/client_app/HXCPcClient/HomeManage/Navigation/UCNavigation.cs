using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HXCPcClient.HomeManage
{
    public partial class UCNavigation : UserControl
    {
        #region --成员变量        
        private bool loadFlag = true;
        UCMap ucMap = null;
        #endregion

        #region --构造函数
        public UCNavigation()
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |             
                ControlStyles.DoubleBuffer, true);
        }
        #endregion

        #region --初始化窗体
        private void UCNavigation_Load(object sender, EventArgs e)
        {
            if (this.loadFlag)
            {
                //SYSModel.clsSysConfig.STR_CURR_MAINMEMU = "CL_RepairBusiness";
                string title = "维修业务";

                this.ucMap = new UCMap(title);
                this.panelMap.Controls.Add(ucMap);
                ucMap.Font = new System.Drawing.Font(ucMap.Font.FontFamily, 9);
                ucMap.Width = this.panelMap.Width;
                ucMap.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                ucMap.Location = new Point(0, 0);

                ucMap.SetMap(new UCMaintain());
                this.loadFlag = false;
            }
        }
        #endregion 
       
        public void LoadGram(UCView view)
        {
            if (this.ucMap != null)
            {
                this.ucMap.SetMap(view);
            }
        }
        public void NeedResize()
        {
            if (this.ucMap != null)
            {
                this.ucMap.NeedResize();
            }
        }
    }
}