using System;
using System.Drawing;
using System.Windows.Forms;

namespace HXCPcClient.HomeManage
{
    public partial class UCMap : UserControl
    {
        #region --成员变量
        private UCView uc = null;
        private bool drawFlag = false;
        #endregion

        #region --构造函数
        public UCMap(string title)
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.DoubleBuffer, true);

            this.btnTitle.Caption = title;
            using (Graphics g = this.CreateGraphics())
            {
                this.btnTitle.Width = (int)g.MeasureString(title, this.btnTitle.Font).Width + 20;
            }
        }
        #endregion

        #region --设置具体业务流程图
        public void SetMap(UCView _uc)
        {
            bool flag = false;
            if (SYSModel.clsSysConfig.STR_CURR_MAINMEMU == "CL_RepairBusiness")
                this.btnTitle.Caption = "维修业务";
            else if (SYSModel.clsSysConfig.STR_CURR_MAINMEMU == "CL_AccessoriesBusiness")
                this.btnTitle.Caption = "配件业务";

            if (this.uc != null)
            {
                this.panelMap.Controls.Remove(this.uc);
                flag = true;
            }
            _uc.Dock = DockStyle.Fill;
            this.uc = _uc;
            this.panelMap.Controls.Add(_uc);
            if (flag)
            {
                panelMap_SizeChanged(null, null);
            }
        }
        #endregion

        public void NeedResize()
        {
            this.drawFlag = true;
        }

        /// <summary>
        /// 设置导航的标题
        /// </summary>
        /// <param name="title">导航标题</param>
        public void SetBtnTitle(string title)
        {
            this.btnTitle.Caption = title;
        }

        #region --改变大小
        private void panelMap_SizeChanged(object sender, EventArgs e)
        {
            if (this.uc != null)
            {
                int x = 0;
                int y = 0;

                if (this.drawFlag)
                {
                    uc.ResizeControl(this.panelMap.Width, this.panelMap.Height);
                }

                x = (this.panelMap.Width - this.uc.Width) / 2;
                if (x < 0)
                {
                    x = 0;
                }
                y = (this.panelMap.Height - this.uc.Height) / 2;
                if (y < 0)
                {
                    y = 0;
                    this.panelMap.SizeChanged -= new System.EventHandler(this.panelMap_SizeChanged);
                    this.Height = this.uc.Height + 45;
                    this.panelMap.SizeChanged += new System.EventHandler(this.panelMap_SizeChanged);
                }
                this.uc.Location = new Point(x, y);
            }
        }
        #endregion
    }
}
