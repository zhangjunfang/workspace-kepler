using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HXCPcClient.HomeManage.Navigation
{
    public partial class UCMapItem : UserControl
    {
        private Image leaveImage = null;
        private Image enterImage = null;
        /// <summary>
        /// 地图单击事件
        /// </summary>
        [Browsable(true), Description("地图单击事件")]
        public event EventHandler MapClick;
        public UCMapItem()
        {
            InitializeComponent();
        }
        [Browsable(true), Description("显示文字")]
        public override string Text
        {
            get
            {
                return lblText.Text;
            }
            set
            {
                lblText.Text = value;
            }
        }
        [Browsable(true), Description("地图图片")]
        public Image MapImage
        {
            get
            {
                return picImage.BackgroundImage;
            }
            set
            {
                leaveImage = value;
                picImage.BackgroundImage = value;
            }
        }

        [Browsable(true), Description("鼠标移入图片")]
        public Image EnterImage
        {
            get
            {
                return enterImage;
            }
            set { enterImage = value; }
        }

        private void picImage_MouseEnter(object sender, EventArgs e)
        {
            picImage.BackgroundImage = enterImage;
        }

        private void picImage_MouseLeave(object sender, EventArgs e)
        {
            picImage.BackgroundImage = leaveImage;
        }

        private void picImage_Click(object sender, EventArgs e)
        {
            if (MapClick != null)
            {
                MapClick(sender, e);
            }
        }
    }
}
