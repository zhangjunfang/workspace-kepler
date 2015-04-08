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
    public partial class UCMap : UserControl
    {
        #region --成员变量
        private bool loadFlag = true;
        #endregion
        private UserControl uc = null;

        #region --构造函数
        public UCMap(string title)
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);

            this.btnTitle.Caption = title;
            using (Graphics g = this.CreateGraphics())
            {
                this.btnTitle.Width = (int)g.MeasureString(title, this.btnTitle.Font).Width + 20;
            }
        }
        #endregion       

        #region --设置具体业务流程图
        public void SetMap(UserControl _uc)
        {
            if (this.uc == null)
            {
                this.uc = _uc;
                this.panelMap.Controls.Add(_uc);                
            }
        }
        #endregion

        #region --改变大小
        private void panelMap_SizeChanged(object sender, EventArgs e)
        {
            if (this.uc != null)
            {
                int x = 0;
                int y = 0;
                x = (this.panelMap.Width - this.uc.Width) / 2;
                if (x < 0)
                {
                    x = 0;
                }
                y = (this.panelMap.Height - this.uc.Height) / 2;
                if (y < 0)
                {
                    y = 0;
                    this.Height = this.uc.Height + 45;
                }                
                this.uc.Location = new Point(x, y);
            }
        }
        #endregion      
    }
}
