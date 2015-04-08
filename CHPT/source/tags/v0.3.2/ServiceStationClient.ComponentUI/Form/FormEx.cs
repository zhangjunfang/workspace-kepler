using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ServiceStationClient.ComponentUI
{
    public enum WindowStyle : uint
    {
        WS_OVERLAPPED = 0x00000000,
        WS_POPUP = 0x80000000,
        WS_CHILD = 0x40000000,
        WS_MINIMIZE = 0x20000000,
        WS_VISIBLE = 0x10000000,
        WS_DISABLED = 0x08000000,
        WS_CLIPSIBLINGS = 0x04000000,
        WS_CLIPCHILDREN = 0x02000000,
        WS_MAXIMIZE = 0x01000000,
        WS_CAPTION = 0x00C00000,
        WS_BORDER = 0x00800000,
        WS_DLGFRAME = 0x00400000,
        WS_VSCROLL = 0x00200000,
        WS_HSCROLL = 0x00100000,
        WS_SYSMENU = 0x00080000,
        WS_THICKFRAME = 0x00040000,
        WS_GROUP = 0x00020000,
        WS_TABSTOP = 0x00010000,
        WS_MINIMIZEBOX = 0x00020000,
        WS_MAXIMIZEBOX = 0x00010000,
        WS_TILED = WS_OVERLAPPED,
        WS_ICONIC = WS_MINIMIZE,
        WS_SIZEBOX = WS_THICKFRAME,
        WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,
        WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU |
                                WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX),
        WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU),
        WS_CHILDWINDOW = (WS_CHILD)
    }

    public enum WindowStyleEx
    {
        WS_EX_DLGMODALFRAME = 0x00000001,
        WS_EX_NOPARENTNOTIFY = 0x00000004,
        WS_EX_TOPMOST = 0x00000008,
        WS_EX_ACCEPTFILES = 0x00000010,
        WS_EX_TRANSPARENT = 0x00000020,
        WS_EX_MDICHILD = 0x00000040,
        WS_EX_TOOLWINDOW = 0x00000080,
        WS_EX_WINDOWEDGE = 0x00000100,
        WS_EX_CLIENTEDGE = 0x00000200,
        WS_EX_CONTEXTHELP = 0x00000400,
        WS_EX_RIGHT = 0x00001000,
        WS_EX_LEFT = 0x00000000,
        WS_EX_RTLREADING = 0x00002000,
        WS_EX_LTRREADING = 0x00000000,
        WS_EX_LEFTSCROLLBAR = 0x00004000,
        WS_EX_RIGHTSCROLLBAR = 0x00000000,
        WS_EX_CONTROLPARENT = 0x00010000,
        WS_EX_STATICEDGE = 0x00020000,
        WS_EX_APPWINDOW = 0x00040000,
        WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE),
        WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST),
        WS_EX_LAYERED = 0x00080000,
        WS_EX_NOINHERITLAYOUT = 0x00100000, // Disable inheritence of mirroring by children
        WS_EX_LAYOUTRTL = 0x00400000, // Right to left mirroring
        WS_EX_COMPOSITED = 0x02000000,
        WS_EX_NOACTIVATE = 0x08000000,
    }

    public enum ClassStyle
    {
        CS_DropSHADOW = 0x20000  //实现窗体阴影
    }

    public enum SystemButtonState
    {
        Normal,
        HighLight,
        Down,
        DownLeave
    }
    public enum MouseOperate
    {
        Move,
        Down,
        Up,
        Leave
    }

    public partial class FormEx : Form
    {
        #region Field

        //窗体圆角的半径
        private int _radius = 5;

        //是否允许窗体改变大小
        private bool _canResize = true;

        //绘制窗体标题的字体、标题的颜色
        private Font _textFont = new Font("微软雅黑", 10.0f, FontStyle.Bold);
        private Color _textForeColor = Color.FromArgb(250, Color.White);

        //是否绘制带有阴影的窗体标题
        private bool _isTextWithShadow = false;
        private Color _textShadowColor = Color.FromArgb(2, Color.Black); //标题的阴影颜色
        private int _textShadowWidth = 4;  //标题阴影的宽度

        private Image _formFringe = ServiceStationClient.ComponentUI.Properties.Resources.fringe_bkg;
        private Image _formBkg;

        //系统按钮管理器
        private SystemButtonManager _systemButtonManager;
        public ToolTip ToolTip;
        protected System.Windows.Forms.Panel pnlContainer;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        #endregion

        #region Constructor

        public FormEx()
        {
            InitializeComponent();
            FormExIni();
            _systemButtonManager = new SystemButtonManager(this);
        }

        #endregion

        #region Properties

        [Description("窗体圆角的半径")]
        public int Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                if (_radius != value)
                {
                    _radius = value;
                    this.Invalidate();
                }
            }
        }

        [Description("是否允许窗体改变大小")]
        public bool CanResize
        {
            get
            {
                return _canResize;
            }
            set
            {
                if (_canResize != value)
                {
                    _canResize = value;
                }
            }
        }

        public override Image BackgroundImage
        {
            get
            {
                return _formBkg;
            }
            set
            {
                if (_formBkg != value)
                {
                    _formBkg = value;
                    Invalidate();
                }
            }
        }

        [Description("用于绘制窗体标题的字体")]
        public Font TextFont
        {
            get { return _textFont; }
            set
            {
                if (_textFont != value)
                {
                    _textFont = value;
                }
            }

        }

        [Description("用于绘制窗体标题的颜色")]
        public Color TextForeColor
        {
            get { return _textForeColor; }
            set
            {
                if (_textForeColor != value)
                { _textForeColor = value; }
            }
        }

        [Description("是否绘制带有阴影的窗体标题")]
        public bool TextWithShadow
        {
            get { return _isTextWithShadow; }
            set
            {
                if (_isTextWithShadow != value)
                {
                    _isTextWithShadow = value;
                }
            }
        }

        [Description("如果TextWithShadow属性为True,则使用该属性绘制阴影")]
        public Color TextShadowColor
        {
            get { return _textShadowColor; }
            set
            {
                if (_textShadowColor != value)
                {
                    _textShadowColor = value;
                }
            }
        }

        [Description("如果TextWithShadow属性为True,则使用该属性获取或色泽阴影的宽度")]
        public int TextShadowWidth
        {
            get { return _textShadowWidth; }
            set
            {
                if (_textShadowWidth != value)
                {
                    _textShadowWidth = value;
                }
            }
        }

        [Browsable(false)]
        [Description("返回窗体关闭系统按钮所在的坐标矩形")]
        public Rectangle CloseBoxRect
        {
            get { return SystemButtonManager.SystemButtonArray[0].LocationRect; }
        }

        [Browsable(false)]
        [Description("返回窗体最大化或者还原系统按钮所在的坐标矩形")]
        public Rectangle MaximiziBoxRect
        {
            get { return SystemButtonManager.SystemButtonArray[1].LocationRect; }
        }

        [Browsable(false)]
        [Description("返回窗体最小化系统按钮所在的坐标矩形")]
        public Rectangle MinimiziBoxRect
        {
            get { return SystemButtonManager.SystemButtonArray[2].LocationRect; }
        }


        private  bool showIconInTitle=false ;
        [Description("标题中显示图标")]
        public bool ShowIconInTitle
        {
            get
            {
                return showIconInTitle;
            }
            set
            {
                if (showIconInTitle != value)
                {
                    showIconInTitle = value;
                    Invalidate();
                }
            }
        }

        private bool showTitle = true ;
        [Description("标题中显示文字")]
        public bool ShowTitle
        {
            get
            {
                return showTitle;
            }
            set
            {
                if (showTitle != value)
                {
                    showTitle = value;
                    Invalidate();
                }
                
            }
           
        }

        internal Rectangle IconRect
        {
            get
            {
                if (ShowIconInTitle && base.Icon != null)
                {
                    return new Rectangle(8, 6, SystemInformation.SmallIconSize.Width, SystemInformation.SmallIconSize.Width);
                }
                return Rectangle.Empty;
            }
        }

        public Rectangle TextRect
        {
            get
            {
                if (base.Text.Length != 0)
                {
                    return new Rectangle(IconRect.Right + 2, 4, Width - (8 + IconRect.Width + 2), TextFont.Height);
                }
                return Rectangle.Empty;
            }
        }

        internal SystemButtonManager SystemButtonManager
        {
            get
            {
                if (_systemButtonManager == null)
                {
                    _systemButtonManager = new SystemButtonManager(this);
                }
                return _systemButtonManager;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            set { base.FormBorderStyle = value; }
        }

        private bool showCloseButton = true;
        [Description("显示关闭按钮")]
        public bool ShowCloseButton 
        {
            get
            {
                return showCloseButton;
            }
            set
            {
                if (showCloseButton != value)
                {
                    showCloseButton = value;
                  
                }
            }
        }
        #endregion

        #region Overrides

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!DesignMode)
                {
                    if (MaximizeBox) { cp.Style |= (int)WindowStyle.WS_MAXIMIZEBOX; }
                    if (MinimizeBox) { cp.Style |= (int)WindowStyle.WS_MINIMIZEBOX; }
                    cp.ExStyle |= (int)WindowStyle.WS_CLIPCHILDREN;  //防止因窗体控件太多出现闪烁
                    cp.ClassStyle |= (int)ClassStyle.CS_DropSHADOW;  //实现窗体边框阴影效果
                }
                return cp;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            CommonMethod.SetFormRoundRectRgn(this, Radius);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateSystemButtonRect();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
           
            CommonMethod.SetFormRoundRectRgn(this, Radius);
            UpdateSystemButtonRect();
            UpdateMaxButton();
            base.OnSizeChanged(e);

        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32API.WM_ERASEBKGND:
                    m.Result = IntPtr.Zero;
                    break;
                case Win32API.WM_NCHITTEST:
                    WmNcHitTest(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            SystemButtonManager.ProcessMouseOperate(e.Location, MouseOperate.Move);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                SystemButtonManager.ProcessMouseOperate(e.Location, MouseOperate.Down);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                SystemButtonManager.ProcessMouseOperate(e.Location, MouseOperate.Up);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            SystemButtonManager.ProcessMouseOperate(Point.Empty, MouseOperate.Leave);
        }

        private Graphics g = null;
        private Bitmap bmpFormExBack = null;
        private ImageAttributes imageAttr = null;
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g = e.Graphics;

            //draw BackgroundImage
            if (BackgroundImage != null)
            {
                switch (BackgroundImageLayout)
                {
                    case ImageLayout.Stretch:
                    case ImageLayout.Zoom:
                        e.Graphics.DrawImage(
                            _formBkg,
                            ClientRectangle,
                            new Rectangle(0, 0, _formBkg.Width, _formBkg.Height),
                            GraphicsUnit.Pixel);
                        break;
                    case ImageLayout.Center:
                    case ImageLayout.None:
                    case ImageLayout.Tile:
                        e.Graphics.DrawImage(
                            _formBkg,
                            ClientRectangle,
                            ClientRectangle,
                            GraphicsUnit.Pixel);
                        break;
                }
            }
            else 
            {
                bmpFormExBack = (Bitmap)ServiceStationClient.ComponentUI.Properties.Resources.window;
                imageAttr = new ImageAttributes();
                imageAttr.SetColorKey(bmpFormExBack.GetPixel(1, 1), bmpFormExBack.GetPixel(1, 1));
                g.DrawImage(bmpFormExBack, new Rectangle(0, 0, 5, 31), 0, 0, 5, 31, GraphicsUnit.Pixel);
                g.DrawImage(bmpFormExBack, new Rectangle(5, 0, this.Width - 10, 31), 5, 0, bmpFormExBack.Width - 10, 31, GraphicsUnit.Pixel);
                g.DrawImage(bmpFormExBack, new Rectangle(this.Width - 5, 0, 5, 31), bmpFormExBack.Width - 5, 0, 5, 31, GraphicsUnit.Pixel);
                g.DrawImage(bmpFormExBack, new Rectangle(0, 31, 5, this.Height - 36), 0, 31, 5, bmpFormExBack.Height - 36, GraphicsUnit.Pixel);
                g.DrawImage(bmpFormExBack, new Rectangle(5, 31, this.Width - 8, this.Height - 36), 5, 31, 5, bmpFormExBack.Height - 36, GraphicsUnit.Pixel);
                g.DrawImage(bmpFormExBack, new Rectangle(this.Width - 5, 31, 5, this.Height - 36), bmpFormExBack.Width - 5, 31, 5, bmpFormExBack.Height - 36, GraphicsUnit.Pixel);
                g.DrawImage(bmpFormExBack, new Rectangle(0, this.Height - 5, 5, 5), 0, bmpFormExBack.Height - 5, 5, 5, GraphicsUnit.Pixel);
                g.DrawImage(bmpFormExBack, new Rectangle(5, this.Height - 5, this.Width - 10, 5), 5, bmpFormExBack.Height - 5, bmpFormExBack.Width - 10, 5, GraphicsUnit.Pixel);
                g.DrawImage(bmpFormExBack, new Rectangle(this.Width - 5, this.Height - 5, 5, 5), bmpFormExBack.Width - 5, bmpFormExBack.Height - 5, 5, 5, GraphicsUnit.Pixel);
            }

            //draw system buttons
            SystemButtonManager.DrawSystemButtons(e.Graphics);

            //draw fringe
            CommonMethod.DrawFormFringe(this, e.Graphics, _formFringe, Radius);

            //draw icon
            if (Icon != null && ShowIconInTitle)
            {
                e.Graphics.DrawIcon(Icon, IconRect);
            }

            //draw text
            if (Text.Length != 0 && ShowTitle)
            {
                if (TextWithShadow)
                {
                    //using (Image textImg = CommonMethod.GetStringImgWithShadowEffect(Text, TextFont, TextForeColor, TextShadowColor, TextShadowWidth))
                    //{
                    //    e.Graphics.DrawImage(textImg, TextRect.Location);
                    //}
                }
                else
                {
                    TextRenderer.DrawText(
                    e.Graphics,
                    Text, TextFont,
                    TextRect,
                    TextForeColor,
                    TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
                }
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_systemButtonManager != null)
                {
                    _systemButtonManager.Dispose();
                    _systemButtonManager = null;

                    _formFringe.Dispose();
                    _formFringe = null;

                    _textFont.Dispose();
                    _textFont = null;

                    if (_formBkg != null)
                    {
                        _formBkg.Dispose();
                        _formBkg = null;
                    }
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Private Methods


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEx));
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlContainer.Location = new System.Drawing.Point(1, 30);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(682, 371);
            this.pnlContainer.TabIndex = 0;
            // 
            // FormEx
            // 
            this.ClientSize = new System.Drawing.Size(684, 402);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEx";
            this.ResumeLayout(false);

        }

        private void FormExIni()
        {
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;

            SetStyles();

        }

        private void SetStyles()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
        }

        private void WmNcHitTest(ref Message m)  //调整窗体大小
        {
            int wparam = m.LParam.ToInt32();
            Point mouseLocation = new Point(CommonMethod.LOWORD(wparam), CommonMethod.HIWORD(wparam));
            mouseLocation = PointToClient(mouseLocation);

            if (WindowState != FormWindowState.Maximized)
            {
                if (CanResize == true)
                {
                    if (mouseLocation.X < 5 && mouseLocation.Y < 5)
                    {
                        m.Result = new IntPtr(Win32API.HTTOPLEFT);
                        return;
                    }

                    if (mouseLocation.X > Width - 5 && mouseLocation.Y < 5)
                    {
                        m.Result = new IntPtr(Win32API.HTTOPRIGHT);
                        return;
                    }

                    if (mouseLocation.X < 5 && mouseLocation.Y > Height - 5)
                    {
                        m.Result = new IntPtr(Win32API.HTBOTTOMLEFT);
                        return;
                    }

                    if (mouseLocation.X > Width - 5 && mouseLocation.Y > Height - 5)
                    {
                        m.Result = new IntPtr(Win32API.HTBOTTOMRIGHT);
                        return;
                    }

                    if (mouseLocation.Y < 3)
                    {
                        m.Result = new IntPtr(Win32API.HTTOP);
                        return;
                    }

                    if (mouseLocation.Y > Height - 3)
                    {
                        m.Result = new IntPtr(Win32API.HTBOTTOM);
                        return;
                    }

                    if (mouseLocation.X < 3)
                    {
                        m.Result = new IntPtr(Win32API.HTLEFT);
                        return;
                    }

                    if (mouseLocation.X > Width - 3)
                    {
                        m.Result = new IntPtr(Win32API.HTRIGHT);
                        return;
                    }
                }
            }
            m.Result = new IntPtr(Win32API.HTCLIENT);
        }

        private void UpdateMaxButton()
        {
            bool isMax = WindowState == FormWindowState.Maximized;
            if (isMax)
            {
                SystemButtonManager.SystemButtonArray[1].NormalImg = ServiceStationClient.ComponentUI.Properties.Resources.restore_normal;//CommonMethod.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.restore_normal.png");
                SystemButtonManager.SystemButtonArray[1].HighLightImg = ServiceStationClient.ComponentUI.Properties.Resources.restore_highlight;//CommonMethod.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.restore_highlight.png");
                SystemButtonManager.SystemButtonArray[1].DownImg = ServiceStationClient.ComponentUI.Properties.Resources.restore_down;//CommonMethod.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.restore_down.png");
                SystemButtonManager.SystemButtonArray[1].ToolTip = "还原";
            }
            else
            {
                SystemButtonManager.SystemButtonArray[1].NormalImg = ServiceStationClient.ComponentUI.Properties.Resources.max_normal;//CommonMethod.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.max_normal.png");
                SystemButtonManager.SystemButtonArray[1].HighLightImg = ServiceStationClient.ComponentUI.Properties.Resources.max_highlight;//CommonMethod.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.max_highlight.png");
                SystemButtonManager.SystemButtonArray[1].DownImg = ServiceStationClient.ComponentUI.Properties.Resources.max_down;//CommonMethod.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.max_down.png");
                SystemButtonManager.SystemButtonArray[1].ToolTip = "最大化";
            }
        }

        protected void UpdateSystemButtonRect()
        {
            bool isShowMaxButton = MaximizeBox;
            bool isShowMinButton = MinimizeBox;
            Rectangle closeRect = new Rectangle(Width,-1,1,1);
            //Max
            if (showCloseButton)
            {
                closeRect = new Rectangle(
                        Width - SystemButtonManager.SystemButtonArray[0].NormalImg.Width,
                        -1,
                        SystemButtonManager.SystemButtonArray[0].NormalImg.Width,
                        SystemButtonManager.SystemButtonArray[0].NormalImg.Height);

                //update close button location rect.
                SystemButtonManager.SystemButtonArray[0].LocationRect = closeRect;
            }
            else
            {
                SystemButtonManager.SystemButtonArray[0].LocationRect = Rectangle.Empty;
            }



            //Max
            if (isShowMaxButton)
            {
                SystemButtonManager.SystemButtonArray[1].LocationRect = new Rectangle(
                    closeRect.X - SystemButtonManager.SystemButtonArray[1].NormalImg.Width,
                    -1,
                    SystemButtonManager.SystemButtonArray[1].NormalImg.Width,
                    SystemButtonManager.SystemButtonArray[1].NormalImg.Height);
            }
            else
            {
                SystemButtonManager.SystemButtonArray[1].LocationRect = Rectangle.Empty;
            }

            //Min
            if (!isShowMinButton)
            {
                SystemButtonManager.SystemButtonArray[2].LocationRect = Rectangle.Empty;
                return;
            }
            if (isShowMaxButton)
            {
                SystemButtonManager.SystemButtonArray[2].LocationRect = new Rectangle(
                    SystemButtonManager.SystemButtonArray[1].LocationRect.X - SystemButtonManager.SystemButtonArray[2].NormalImg.Width,
                    -1,
                    SystemButtonManager.SystemButtonArray[2].NormalImg.Width,
                    SystemButtonManager.SystemButtonArray[2].NormalImg.Height);
            }
            else
            {
                SystemButtonManager.SystemButtonArray[2].LocationRect = new Rectangle(
                   closeRect.X - SystemButtonManager.SystemButtonArray[2].NormalImg.Width,
                   -1,
                   SystemButtonManager.SystemButtonArray[2].NormalImg.Width,
                   SystemButtonManager.SystemButtonArray[2].NormalImg.Height);
            }
        }

        #endregion


    }



    //按钮事件委托
    public delegate void MouseDownEventHandler();

    internal class SystemButton
    {
        public SystemButtonState State { get; set; }
        public Rectangle LocationRect { get; set; }
        public Image NormalImg { get; set; }
        public Image HighLightImg { get; set; }
        public Image DownImg { get; set; }
        public string ToolTip { get; set; }

        public event MouseDownEventHandler OnMouseDownEvent;
        public void OnMouseDown()
        {
            if (OnMouseDownEvent != null)
            {
                OnMouseDownEvent();
            }
        }
    }

    /// <summary>
    /// 系统按钮控制器，对其用参数所构建的窗体系统按钮的控制
    /// </summary>
    internal class SystemButtonManager : IDisposable
    {
        #region Field

        private FormEx _owner;
        private SystemButton[] _systemButtons = new SystemButton[3];   // 0.Close 1.Max 2 Min

        private bool _mouseDown;

        private bool isShowCloseButton;
        #endregion

       
        #region Constructor

        public SystemButtonManager(FormEx owner)
        {
            this._owner = owner;
            isShowCloseButton = owner.ShowCloseButton;
            IniSystemButtons();
        }

        #endregion

        #region Properties

        public SystemButton[] SystemButtonArray
        {
            get
            {
                return _systemButtons;
            }
        }

        //按钮状态索引器
        public SystemButtonState this[int buttonID]
        {
            get
            {
                return SystemButtonArray[buttonID].State;
            }
            set
            {
                if (SystemButtonArray[buttonID].State != value)
                {
                    SystemButtonArray[buttonID].State = value;
                    if (_owner != null)
                    {
                        Invalidate(SystemButtonArray[buttonID].LocationRect);
                    }
                }
            }
        }

        #endregion

        #region Public

        public void ProcessMouseOperate(Point mousePoint, MouseOperate operate)
        {
            switch (operate)
            {
                case MouseOperate.Move:
                    ProcessMouseMove(mousePoint);
                    break;
                case MouseOperate.Down:
                    ProcessMouseDown(mousePoint);
                    break;
                case MouseOperate.Up:
                    ProcessMouseUP(mousePoint);
                    break;
                case MouseOperate.Leave:
                    ProcessMouseLeave();
                    break;
                default:
                    break;
            }
        }

        public void DrawSystemButtons(Graphics g)
        {
            for (int index = 0; index < SystemButtonArray.Length; index++)
            {
                //当窗体没有此系统按钮时，不进行绘制
                if (SystemButtonArray[index].LocationRect == Rectangle.Empty)
                {
                    continue;
                }

                switch (this[index])
                {
                    case SystemButtonState.DownLeave:
                    case SystemButtonState.Normal:
                        g.DrawImage(
                            SystemButtonArray[index].NormalImg,
                            SystemButtonArray[index].LocationRect,
                            new Rectangle(0, 0, SystemButtonArray[index].NormalImg.Width, SystemButtonArray[index].NormalImg.Height),
                            GraphicsUnit.Pixel);
                        break;
                    case SystemButtonState.HighLight:
                        g.DrawImage(
                            SystemButtonArray[index].HighLightImg,
                            SystemButtonArray[index].LocationRect,
                            new Rectangle(0, 0, SystemButtonArray[index].HighLightImg.Width, SystemButtonArray[index].HighLightImg.Height),
                            GraphicsUnit.Pixel);
                        break;
                    case SystemButtonState.Down:
                        g.DrawImage(
                            SystemButtonArray[index].DownImg,
                            SystemButtonArray[index].LocationRect,
                            new Rectangle(0, 0, SystemButtonArray[index].DownImg.Width, SystemButtonArray[index].DownImg.Height),
                            GraphicsUnit.Pixel);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Private

        private void ProcessMouseMove(Point mousePoint)
        {
            string toolTip = string.Empty;
            bool hide = true;

            int index = SearchPointInRects(mousePoint);
            if (index != -1)
            {
                hide = false;  //显示提示文本
                if (!_mouseDown)
                {
                    if (this[index] != SystemButtonState.HighLight)
                    {
                        toolTip = SystemButtonArray[index].ToolTip;
                    }
                    this[index] = SystemButtonState.HighLight;
                }
                else
                {
                    if (this[index] == SystemButtonState.DownLeave)
                    {
                        this[index] = SystemButtonState.Down;
                    }
                }

                //其他按钮的状态为 Normal
                for (int i = 0; i < SystemButtonArray.Length; i++)
                {
                    if (i != index)
                    {
                        this[i] = SystemButtonState.Normal;
                    }
                }
            }
            else
            {
                for (int i = 0; i < SystemButtonArray.Length; i++)
                {
                    if (!_mouseDown)
                    {
                        this[i] = SystemButtonState.Normal;
                    }
                    else
                    {
                        if (this[i] == SystemButtonState.Down)
                        {
                            this[i] = SystemButtonState.DownLeave;
                        }
                    }
                }
            }

            if (toolTip != string.Empty)
            {
                HideToolTip();
                ShowTooTip(toolTip);
            }

            if (hide)
            {
                HideToolTip();
            }

        }

        private void ProcessMouseDown(Point mousePoint)
        {

            int index = SearchPointInRects(mousePoint);
            if (index != -1)
            {
                _mouseDown = true;
                this[index] = SystemButtonState.Down;
            }
            else
            {
                CommonMethod.MoveWindow(_owner);
            }
        }

        private void ProcessMouseUP(Point mousePoint)
        {
            _mouseDown = false;
            int index = SearchPointInRects(mousePoint);
            if (index != -1)
            {
                if (this[index] == SystemButtonState.Down)
                {
                    this[index] = SystemButtonState.Normal;

                    //handle event at there
                    SystemButtonArray[index].OnMouseDown();
                }
            }
            else
            {
                ProcessMouseLeave();
            }
        }

        private void ProcessMouseLeave()
        {
            for (int i = 0; i < SystemButtonArray.Length; i++)
            {
                if (this[i] == SystemButtonState.Down)
                {
                    this[i] = SystemButtonState.DownLeave;
                }
                else
                { //所有按钮的状态为 Normal
                    this[i] = SystemButtonState.Normal;
                }
            }
        }

        public void Invalidate(Rectangle rect)
        {
            _owner.Invalidate(rect);
        }

        private void ShowTooTip(string toolTipText)
        {
            if (_owner != null)
            {
                _owner.ToolTip.Active = true;
                _owner.ToolTip.SetToolTip(_owner, toolTipText);
            }
        }

        private void HideToolTip()
        {
            if (_owner != null)
            {
                _owner.ToolTip.Active = false;
            }
        }

        /// <summary>
        /// 判断鼠标点是否在系统按钮矩形内
        /// </summary>
        /// <param name="mousePoint">鼠标坐标点</param>
        /// <returns>如果存在，返回系统按钮索引，否则返回 -1</returns>
        private int SearchPointInRects(Point mousePoint)
        {
            bool isFind = false;
            int index = 0;
            foreach (SystemButton button in SystemButtonArray)
            {
                if (button.LocationRect != Rectangle.Empty && button.LocationRect.Contains(mousePoint))
                {
                    isFind = true;
                    break;
                }
                index++;
            }
            if (isFind)
            {
                return index;
            }
            else
            {
                return -1;
            }
        }

        #endregion

        #region SystemButton Initialization

        private void IniSystemButtons()
        {
            bool isShowMaxButton = _owner.MaximizeBox;
            bool isShowMinButton = _owner.MinimizeBox;

            //Colse
            SystemButton closeBtn = new SystemButton();
            SystemButtonArray[0] = closeBtn;
            closeBtn.ToolTip = "关闭";

            if (isShowCloseButton)
            {
                closeBtn.NormalImg = ServiceStationClient.ComponentUI.Properties.Resources.close_normal;// CommonMethod.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.close_normal.png");
                closeBtn.HighLightImg = ServiceStationClient.ComponentUI.Properties.Resources.close_highlight;//CommonMethod.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.close_highlight.png");
                closeBtn.DownImg = ServiceStationClient.ComponentUI.Properties.Resources.close_down;//CommonMethod.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.close_down.png");
                closeBtn.LocationRect = new Rectangle(
                    _owner.Width - closeBtn.NormalImg.Width,
                    -1,
                    closeBtn.NormalImg.Width,
                    closeBtn.NormalImg.Height);
                //注册事件
                closeBtn.OnMouseDownEvent += new MouseDownEventHandler(this.CloseButtonEvent);
            }

            //Max
            SystemButton MaxBtn = new SystemButton();
            SystemButtonArray[1] = MaxBtn;
            MaxBtn.ToolTip = "最大化";
            if (isShowMaxButton)
            {
                MaxBtn.NormalImg = ServiceStationClient.ComponentUI.Properties.Resources.max_normal;// CommonMethod.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.max_normal.png");
                MaxBtn.HighLightImg = ServiceStationClient.ComponentUI.Properties.Resources.max_highlight;// CommonMethod.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.max_highlight.png");
                MaxBtn.DownImg = ServiceStationClient.ComponentUI.Properties.Resources.max_down;// CommonMethod.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.max_down.png");
                MaxBtn.OnMouseDownEvent += new MouseDownEventHandler(this.MaxButtonEvent);
                MaxBtn.LocationRect = new Rectangle(
                    closeBtn.LocationRect.X - MaxBtn.NormalImg.Width,
                    -1,
                    MaxBtn.NormalImg.Width,
                    MaxBtn.NormalImg.Height);
            }
            else
            {
                MaxBtn.LocationRect = Rectangle.Empty;
            }

            //Min
            SystemButton minBtn = new SystemButton();
            SystemButtonArray[2] = minBtn;
            minBtn.ToolTip = "最小化";
            if (!isShowMinButton)
            {
                minBtn.LocationRect = Rectangle.Empty;
                return;
            }

            minBtn.NormalImg = ServiceStationClient.ComponentUI.Properties.Resources.min_normal;//CommonMethod.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.min_normal.png");
            minBtn.HighLightImg = ServiceStationClient.ComponentUI.Properties.Resources.min_highlight;//CommonMethod.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.min_highlight.png");
            minBtn.DownImg = ServiceStationClient.ComponentUI.Properties.Resources.min_down;//CommonMethod.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.min_down.png");
            minBtn.OnMouseDownEvent += new MouseDownEventHandler(this.MinButtonEvent);
            if (isShowMaxButton)
            {
                minBtn.LocationRect = new Rectangle(
                    MaxBtn.LocationRect.X - minBtn.NormalImg.Width,
                    -1,
                    minBtn.NormalImg.Width,
                    minBtn.NormalImg.Height);
            }
            else
            {
                minBtn.LocationRect = new Rectangle(
                   closeBtn.LocationRect.X - minBtn.NormalImg.Width,
                   -1,
                   minBtn.NormalImg.Width,
                   minBtn.NormalImg.Height);
            }
        }

        private void CloseButtonEvent()
        {
            _owner.Close();
        }

        private void MaxButtonEvent()
        {
            bool isMax = _owner.WindowState == FormWindowState.Maximized;
            if (isMax)
            {
                _owner.WindowState = FormWindowState.Normal;
            }
            else
            {
                _owner.WindowState = FormWindowState.Maximized;
            }
        }

        private void MinButtonEvent()
        {
            _owner.WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _owner = null;
        }

        #endregion
    }

}
