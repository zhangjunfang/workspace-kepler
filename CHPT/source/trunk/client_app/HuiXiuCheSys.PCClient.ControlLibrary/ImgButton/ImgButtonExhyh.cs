using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HuiXiuCheSys.PCClient.ControlLibrary.ImgButton
{
   [System.Drawing.ToolboxBitmapAttribute(typeof(System.Windows.Forms.Button))]
    public partial class ImgButtonExhyh : System.Windows.Forms.Button
    {
       [DefaultValue(typeof(Image), "global::ServiceStationClient.Skin.Properties.Resources.btnDown01")]
        public  Image NormalImg { get; set; }
       [DefaultValue(typeof(Image), "global::ServiceStationClient.Skin.Properties.Resources.btnDown02")]
        public  Image HoverImg { get; set; }
       [DefaultValue(typeof(Image), "global::ServiceStationClient.Skin.Properties.Resources._btnMaximize01")]
        public  Image DownImg { get; set; }
        //public string Text { get; set; }
        private MouseAction MAction;
        private enum MouseAction
        {
            Leave,
            Over,
            Click
        }
        public ImgButtonExhyh()
            : base()
        {
            InitializeComponent();
        }

        #region 重载OnPaint方法
        /// <summary>
        /// 重载OnPaint方法
        /// </summary>
        /// <param name="pevent"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            Image img = null;
            if (this.DesignMode == true)
            {
               SolidBrush myBrush = new SolidBrush(Color.FromArgb(100,Color.Black));
               Pen myPen = new Pen(myBrush);
               g.DrawRectangle(myPen, this.ClientRectangle);
               g.DrawString(Text, Font, new SolidBrush(Color.Yellow), e.ClipRectangle, sf);
    
            }
            else
            {
              
                switch (MAction)
                {
                    case MouseAction.Click:
                        img = DownImg;
                        break;
                    case MouseAction.Leave:
                        img = NormalImg;
                        break;
                    case MouseAction.Over:
                        img = HoverImg;
                        break;
                    default:
                        img = NormalImg;
                        break;
                }
                //g.DrawImage(img, e.ClipRectangle, 0, 0, this.Width, this.Height, GraphicsUnit.Pixel);
                if (img != null)
                {
                    g.DrawImage(img, this.ClientRectangle);
                    g.DrawString(Text, Font, new SolidBrush(Color.Yellow), e.ClipRectangle, sf);
                   
                }
            }
       
            
        }
        #endregion

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs mevent)
        {
            MAction = MouseAction.Click;
            this.Invalidate(false);
            base.OnMouseDown(mevent);
        }
        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs mevent)
        {
            MAction = MouseAction.Over;
            this.Invalidate(false);
            base.OnMouseUp(mevent);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            MAction = MouseAction.Over;
            this.Invalidate(false);
            base.OnMouseEnter(e);
        }
        protected override void OnNotifyMessage(System.Windows.Forms.Message m)
        {
            base.OnNotifyMessage(m);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            MAction = MouseAction.Leave;
            this.Invalidate(false);
            base.OnMouseLeave(e);
        }       
    }
}
