using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using System.Threading;
using HXCPcClient.UCForm;
using HXCPcClient.UCForm.SysManage.BulletinManage;

namespace HXCPcClient.HomeManage
{
    public partial class UCAnnounce : UserControl
    {
        #region --UI交互
        public delegate void UiHandler(DataTable dt);
        public UiHandler uiHandler;
        #endregion

        #region --成员变量
        private int curY = 0;
        private bool loadFlag = true;
        #endregion

        #region --构造函数
        public UCAnnounce()
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);

            LocalCache.AnnounceComplated -= new LocalCache.AnnounceComplate(this._ShowItem);
            LocalCache.AnnounceComplated += new LocalCache.AnnounceComplate(this._ShowItem);
            this.uiHandler -= new UiHandler(this.ShowItem);
            this.uiHandler += new UiHandler(this.ShowItem);
        }
        #endregion

        #region --窗体初始化
        private void UCAnnounce_Load(object sender, EventArgs e)
        {
            if (this.loadFlag)
            {              
                //加载数据
                if (LocalCache.DtAnnounce != null)
                {
                    this.Invoke(this.uiHandler, LocalCache.DtAnnounce);
                }          
                this.loadFlag = false;
            }
        }
        #endregion       

        #region --公共方法
        public void _ShowItem(DataTable dt)
        {
            this.loadFlag = false;
            UiHandler _uiHandler = new UiHandler(this.ShowItem);
            this.Invoke(_uiHandler,dt);
        }
        public void ShowItem(DataTable dt)
        {
            this.panelContent.Controls.Clear();
            this.curY = 0;
            foreach (DataRow dr in dt.Rows)
            {               
                UCAnnounceItem ucItem = new UCAnnounceItem(dr);
                this.panelContent.Controls.Add(ucItem);
                ucItem.Width = this.panelContent.Width;
                ucItem.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                ucItem.Location = new Point(0, this.curY);
                this.curY += ucItem.Height;
            }                       
        }
        #endregion

        #region --更多
        private void labelMore_Click(object sender, EventArgs e)
        {
            UCBulletinManage moreUc = new UCBulletinManage();
            string tag = "CL_SystemManagement_BulletinManagement|CL_SystemManagement|CL_SystemManagement_BulletinManagement";
            UCBase.AddUserControl(moreUc, "公告管理", "CL_SystemManagement_BulletinManagement", tag, "");
        }
        #endregion
    }
}
