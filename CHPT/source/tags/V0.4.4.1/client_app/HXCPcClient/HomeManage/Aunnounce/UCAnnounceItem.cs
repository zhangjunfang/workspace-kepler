using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Utility.Common;
using HXCPcClient.UCForm.SysManage.BulletinManage;
using HXCPcClient.UCForm;
using HXCPcClient.CommonClass;

namespace HXCPcClient.HomeManage
{
    public partial class UCAnnounceItem : UserControl
    {
        #region --成员变量
        private Color defaultColor = Color.FromArgb(115, 115, 115);
        private DataRow dr;
        private bool loadFlag = true;
        #endregion

        #region --构造函数
        public UCAnnounceItem(DataRow _dr)
        {
            InitializeComponent();           

            this.dr = _dr;
            this.Init();
        }
        #endregion

        #region --初始化数据
        private void Init()
        {
            if (this.dr == null)
            {
                this.Visible = false;
                return;
            }
            this.labelContent.Text = "[系统公告] " + dr["announcement_title"];            
            try
            {
                this.labelTime.Text = DateTime.Parse(Common.UtcLongToLocalDateTime(dr["date_up"])).ToString("yyyy-MM-dd");
            }
            catch
            {

            }
        }
        #endregion

        #region --窗体初始化
        private void UCAnnounceItem_Load(object sender, EventArgs e)
        {
            if (this.loadFlag)
            {
                if (this.labelContent.Width > this.labelTime.Location.X + 20)
                {
                    this.labelContent.Text = this.labelContent.Text.Substring(0, 15) + "…";
                }
                this.loadFlag = false;
                foreach (Control trl in this.Controls)
                {
                    trl.Click += new EventHandler(this.UCAnnounceItem_Click);
                    trl.MouseEnter += new EventHandler(this.UCAnnounceItem_MouseEnter);
                    trl.MouseLeave += new EventHandler(this.UCAnnounceItem_MouseLeave);
                }
            }
        }
        #endregion       

        #region --绘制下划线
        private void UCAnnounceItem_Paint(object sender, PaintEventArgs e)
        {
            this.loadFlag = false;
            using (Graphics g = e.Graphics)
            {
                Pen pen = new Pen(new SolidBrush(Color.FromArgb(194, 216, 244)), 1);
                pen.DashPattern = new float[] { 5, 4 };
                g.DrawLine(pen, new Point(6, this.Height - 1), new Point(this.Width - 6, this.Height - 1));
            }
        }
        #endregion

        #region --事件
        //单击事件
        private void UCAnnounceItem_Click(object sender, EventArgs e)
        {
            if (this.dr != null)
            {
                if (LocalCache.HasFunction("CL_SystemManagement_BulletinManagement"))
                {
                    //进入公告页，显示公告               
                    UCBulletinView ucAnnounce = new UCBulletinView(dr, SYSModel.clsSysConfig.STR_CS_MEMU_HOMEMANAGE);
                    string tag = "BulletinView|BulletinView_001|BulletinView_002";
                    UCBase.AddUserControl(ucAnnounce, "公告管理-浏览", "BulletinView" + dr["announcement_id"].ToString(), tag, "");
                }
            }
        }
        //鼠标进入
        private void UCAnnounceItem_MouseEnter(object sender, EventArgs e)
        {
            this.labelContent.ForeColor = Color.FromArgb(254, 126, 0);
            this.labelContent.Font = new Font(this.labelContent.Font.FontFamily, this.labelContent.Font.Size, FontStyle.Bold);
            this.labelTime.ForeColor = Color.FromArgb(254, 126, 0);
        }
        //鼠标离开
        private void UCAnnounceItem_MouseLeave(object sender, EventArgs e)
        {            
            this.labelContent.Font = new Font(this.labelContent.Font.FontFamily, this.labelContent.Font.Size, FontStyle.Regular);
            this.labelContent.ForeColor = this.defaultColor;
            this.labelTime.ForeColor = this.defaultColor;
        }
        #endregion        
    }
}
