using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace HuiXiuCheSys.PCClient.ControlLibrary.CrystalButtonEXHyh
{
   [System.Drawing.ToolboxBitmapAttribute(typeof(System.Windows.Forms.Button))]
    public partial class CrystalButtonEXHyh : Button
    {
        private MouseAction MAction;
        private enum MouseAction
         {
             Leave,
             Over,
             Click
         }
        public GradualMethod GradualM
        {
            get;
            set;       
        }
         /// <summary>
         /// 颜色渐变方式
         /// </summary>
        public enum GradualMethod
         {
             UpToDown,
             LeftToRight,
             LeftUpToRightDown,
             RightUpToLeftDown
         }
       private Color FirstColor;
        /// <summary>
        /// 第一渐变颜色
        /// </summary>
       public Color FirstGradualColor
        {
            get
            {
                return FirstColor;
            }
            set
            {
                FirstColor = value;
            }
        }
        private Color SecondColor;
        /// <summary>
        /// 第二渐变颜色
        /// </summary>
        public Color SecondGradualColor
        {
            get
            {
                return SecondColor;
            }
            set
            {
                SecondColor = value;
            }
        }
        public CrystalButtonEXHyh()
            : base()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            base.OnPaintBackground(e);
            GradualM = GradualMethod.UpToDown;
            int r = 10;
            int BtnOffSet = 0;
            Color FColor = Color.FromArgb(245, 245, 245);        
            Color SColor = Color.FromArgb(180, 175, 190);
            Color TempFColor = this.FirstColor;
            Color TempSColor = this.SecondColor;           
            int offsetwidth = this.Width / 50;
            switch (MAction)
            {
                case MouseAction.Click:
                    BtnOffSet = 2;
                    break;
                case MouseAction.Leave:
                    BtnOffSet = 0;
                    TempFColor = FirstColor;
                    TempSColor = SecondColor;
                    break;
                case MouseAction.Over:
                    TempFColor = FColor;
                    TempSColor = SColor;
                    break;
            }            
            Rectangle rc = new Rectangle(BtnOffSet, BtnOffSet, this.ClientSize.Width-1 , this.ClientSize.Height-1 );           
            int x = rc.X, y = rc.Y, w = rc.Width, h = rc.Height;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(x, y, r, r, 180, 90);
            path.AddArc(x + w - r, y, r, r, 270, 90);
            path.AddArc(x + w - r, y + h - r, r, r, 0, 90);    
            path.AddArc(x, y + h - r, r, r, 90, 90);    
            path.CloseFigure();
            this.Region = new Region(path);
            LinearGradientBrush b = null;
            switch (GradualM)
            {
                case GradualMethod.UpToDown:
                    b = new LinearGradientBrush(rc, TempFColor, TempSColor, LinearGradientMode.Vertical);
                    break;
                case GradualMethod.RightUpToLeftDown:
                    b = new LinearGradientBrush(rc, TempFColor, TempSColor, LinearGradientMode.BackwardDiagonal);
                    break;
                case GradualMethod.LeftUpToRightDown:
                    b = new LinearGradientBrush(rc, TempFColor, TempSColor, LinearGradientMode.ForwardDiagonal);
                    break;
                case GradualMethod.LeftToRight:
                    b = new LinearGradientBrush(rc, TempFColor, TempSColor, LinearGradientMode.Horizontal);
                    break;
            }            
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillPath(b, path);          
            e.Graphics.DrawPath(new Pen(Color.Gray, 3), path);
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString(this.Text, this.Font, new LinearGradientBrush(this.ClientRectangle, Color.Black, Color.Black, LinearGradientMode.Vertical), rc, drawFormat);
            b.Dispose();
        } 
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
        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {          
            pevent.Graphics.Clear(Color.Wheat);
        }
    }
}
