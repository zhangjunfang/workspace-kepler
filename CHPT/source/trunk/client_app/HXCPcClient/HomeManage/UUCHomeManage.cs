using System;
using System.Drawing;
using System.Windows.Forms;
using HXCPcClient.HomeManage;

namespace HXCPcClient
{
    public partial class UUCHomeManage : UserControl
    {
        #region --成员变量
        public string ID;
        private bool loadFlag = true;
        private UCNavigation ucv;
        #endregion

        #region --构造函数
        public UUCHomeManage()
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);

            this.Tag = "UUCHomeManage|UUCHomeManage_001|UUCHomeManage_002";            
        }
        #endregion

        #region --窗体初始化
        private void UUCHomeManage_Load(object sender, EventArgs e)
        {
            if (this.loadFlag)
            {
                //导航图
                ucv = new UCNavigation();                
                this.panelLeft.Controls.Add(ucv);
                //ucv.LoadGram(new UCMaintain());
                ucv.Location = new Point(0, 0);
                ucv.Size = this.panelLeft.Size;                
                ucv.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                ucv.NeedResize();

                //公告信息
                UCAnnounce uca = new UCAnnounce();
                this.panelRightTop.Controls.Add(uca);
                uca.Location = new Point(0, 0);
                uca.Size = this.panelRightTop.Size;
                uca.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

                //提醒信息
                UCReminder ucr = new UCReminder();
                this.panelRightButtom.Controls.Add(ucr);
                ucr.Location = new Point(0, 0);
                ucr.Size = this.panelRightButtom.Size;
                ucr.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

                this.loadFlag = false;
            }   
        }
        #endregion

        /// <summary>
        /// 一级菜单
        /// </summary>
        /// <param name="menu"></param>
        public void LoadMap(string menu)
        {
            if (menu == "CL_RepairBusiness")
            {
                ucv.LoadGram(new UCMaintain());
            }
            else if (menu == "CL_AccessoriesBusiness")
            {
                ucv.LoadGram(new UCPurchaseMap());
            }           
        }
    }
}
