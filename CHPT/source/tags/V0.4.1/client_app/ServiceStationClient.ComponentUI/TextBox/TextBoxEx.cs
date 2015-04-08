/*----------------------------------------------------------------
 * Copyright (C) 2012 北京北大千方科技信息有限公司 版权所有。
 * 保留所有权利

 * 文件名称: ZJXLtextbox.cs 
 * 编程语言: C# 
 * 文件说明: 自定义文本框TextBox

 * 功能: 
 * 自定义文本框TextBox

 * 当前版本: 1.1.0.0
 * 替换版本：1.0.0.0

 * 创建人: 项载杰 
 * EMail: xiangzaijie@ctfo.com
 * 创建日期: 2012-03-29 
 * 最后修改日期: 2014-12-23
 * -------------------------------
 * 历史修改记录: 
 * 修改人：郭保强
 * EMail: baoqiang.guo@outlook.com
 * 修改时间: 2014-12-23
 * 修改内容: 实现数据校验接口
 * -------------------------------
 * 修改人：郭保强
 * EMail: baoqiang.guo@outlook.com
 * 修改时间: 2015-01-10
 * 修改内容: 重写ToString方法
----------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ServiceStationClient.ComponentUI
{
    public partial class TextBoxEx : UserControl,IContentControl
    {
        #region - 方法 -

        public static System.Drawing.Drawing2D.GraphicsPath DrawRoundRect(Rectangle rect, int radius)
        {
            int x = rect.X;
            int y = rect.Y;
            int width = rect.Width;
            int height = rect.Height;
            return DrawRoundRect(x, y, width - 2, height - 1, radius);
        }

        public static System.Drawing.Drawing2D.GraphicsPath DrawRoundRect(Rectangle r, int r1, int r2, int r3, int r4)
        {
            float x = r.X;
            float y = r.Y;
            float width = r.Width;
            float height = r.Height;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddBezier(x, y + r1, x, y, x + r1, y, x + r1, y);
            path.AddLine(x + r1, y, (x + width) - r2, y);
            path.AddBezier((x + width) - r2, y, x + width, y, x + width, y + r2, x + width, y + r2);
            path.AddLine((float)(x + width), (float)(y + r2), (float)(x + width), (float)((y + height) - r3));
            path.AddBezier((float)(x + width), (float)((y + height) - r3), (float)(x + width), (float)(y + height), (float)((x + width) - r3), (float)(y + height), (float)((x + width) - r3), (float)(y + height));
            path.AddLine((float)((x + width) - r3), (float)(y + height), (float)(x + r4), (float)(y + height));
            path.AddBezier(x + r4, y + height, x, y + height, x, (y + height) - r4, x, (y + height) - r4);
            path.AddLine(x, (y + height) - r4, x, y + r1);
            return path;
        }

        public static System.Drawing.Drawing2D.GraphicsPath DrawRoundRect(int x, int y, int width, int height, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(x, y, radius, radius, 180f, 90f);
            path.AddArc(width - radius, y, radius, radius, 270f, 90f);
            path.AddArc(width - radius, height - radius, radius, radius, 0f, 90f);
            path.AddArc(x, height - radius, radius, radius, 90f, 90f);
            path.CloseAllFigures();
            return path;
        }

        public static Color GetIntermediateColor(Color c, Color c2, int value)
        {
            float num = (value * 1f) / 100f;
            int a = c.A;
            int r = c.R;
            int g = c.G;
            int b = c.B;
            int num6 = c2.A;
            int num7 = c2.R;
            int num8 = c2.G;
            int num9 = c2.B;
            int alpha = (int)Math.Abs((float)(a + ((a - num6) * num)));
            int red = (int)Math.Abs((float)(r - ((r - num7) * num)));
            int green = (int)Math.Abs((float)(g - ((g - num8) * num)));
            int blue = (int)Math.Abs((float)(b - ((b - num9) * num)));
            if (alpha > 0xff)
            {
                alpha = 0xff;
            }
            if (red > 0xff)
            {
                red = 0xff;
            }
            if (green > 0xff)
            {
                green = 0xff;
            }
            if (blue > 0xff)
            {
                blue = 0xff;
            }
            return Color.FromArgb(alpha, red, green, blue);
        }

        public static StringFormat StringFormatAlignment(ContentAlignment textalign)
        {
            StringFormat format = new StringFormat();
            switch (textalign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    format.LineAlignment = StringAlignment.Near;
                    break;

                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    format.LineAlignment = StringAlignment.Center;
                    break;

                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                case ContentAlignment.BottomLeft:
                    format.LineAlignment = StringAlignment.Far;
                    break;
            }
            ContentAlignment alignment = textalign;
            if (alignment <= ContentAlignment.MiddleCenter)
            {
                switch (alignment)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.MiddleLeft:
                        goto Label_00CD;

                    case ContentAlignment.TopCenter:
                    case ContentAlignment.MiddleCenter:
                        goto Label_00D7;

                    case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                        return format;

                    case ContentAlignment.TopRight:
                        goto Label_00E1;
                }
                return format;
            }
            if (alignment <= ContentAlignment.BottomLeft)
            {
                switch (alignment)
                {
                    case ContentAlignment.MiddleRight:
                        goto Label_00E1;

                    case ContentAlignment.BottomLeft:
                        goto Label_00CD;
                }
                return format;
            }
            switch (alignment)
            {
                case ContentAlignment.BottomCenter:
                    goto Label_00D7;

                case ContentAlignment.BottomRight:
                    goto Label_00E1;

                default:
                    return format;
            }
        Label_00CD:
            format.Alignment = StringAlignment.Near;
            return format;
        Label_00D7:
            format.Alignment = StringAlignment.Center;
            return format;
        Label_00E1:
            format.Alignment = StringAlignment.Far;
            return format;
        }

        /// <summary>
        /// TextChanged 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void TextBoxChangedHandle(object sender, EventArgs e);
        public event TextBoxChangedHandle UserControlValueChanged;
        protected virtual void OnTextChanged()
        {
            if (this.UserControlValueChanged != null)
                this.UserControlValueChanged(this, EventArgs.Empty);
        }

        #endregion

        #region - 变量 -

        private Color _borderColor = Color.FromArgb(166, 208, 226);
        private Color _shadowColor = Color.FromArgb(175, 212, 228);
        private Image _foreImage = null;
        private bool _isFouse = false;
        private Color _backColor = Color.Transparent;
        private string _waterMark = null;
        private Color _waterMarkColor = Color.Silver;
        private Color _foreColor = Color.Black;
        private int _radius = 3;
        private String _oldValue = null;
        private Boolean _isSetWaterMark = false;
        private Color _defaultBorderColor = Color.FromArgb(166, 208, 226);
        private ToolTip _toolTip;
        private Boolean _hasError;
        private Boolean _changBorderColor = true;
        #endregion

        #region - 属性 -

        [Category("自定义"), Description("内部输入框")]
        public System.Windows.Forms.TextBox InnerTextBox
        {
            get { return this.ZjxlText; }            
        }

        [Category("自定义"), Description("边框颜色,BorderStyle为FixedSingle有效")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                if (_changBorderColor)
                {
                    _defaultBorderColor = _borderColor;
                }
                Invalidate();
            }
        }

        [Category("自定义"), Description("边框阴影颜色,BorderStyle为FixedSingle有效")]
        public Color ShadowColor
        {
            get { return _shadowColor; }
            set
            {
                _shadowColor = value;
                //this.Invalidate();
            }
        }

        [Category("自定义"), Description("显示的前端的图片")]
        public Image ForeImage
        {
            get { return pic.Image; }
            set
            {
                _foreImage = value;
                pic.Image = _foreImage;
                //Invalidate();
            }
        }

        [Category("自定义"), Description("文字"), DefaultValue("")]
        public string Caption
        {
            get
            {
                return ZjxlText.Text;
            }
            set
            {
                ZjxlText.Text = value;
                SetWaterMark();
                Invalidate();
            }
        }

        [Category("自定义行为"), Description("是否多行显示")]
        public bool Multiline
        {
            get { return ZjxlText.Multiline; }
            set
            {
                ZjxlText.Multiline = value;
            }
        }

        [Category("自定义行为"), Description("是否以密码形式显示字符")]
        public bool UseSystemPasswordChar
        {
            get { return ZjxlText.UseSystemPasswordChar; }
            set
            {
                ZjxlText.UseSystemPasswordChar = value;

            }
        }

        [Category("自定义行为"), Description("是否只读")]
        public bool ReadOnly
        {
            get { return ZjxlText.ReadOnly; }
            set
            {
                ZjxlText.ReadOnly = value;

            }
        }

        [Category("自定义"), Description("水印文字")]
        public string WaterMark
        {
            get { return _waterMark; }
            set
            {
                _waterMark = value;
                //Invalidate();
            }
        }

        [Category("自定义"), Description("水印颜色")]
        public Color WaterMarkColor
        {
            get { return _waterMarkColor; }
            set
            {
                _waterMarkColor = value;
                //Invalidate();
            }
        }

        #region 需要被隐藏的属性

        [Browsable(false)]
        public new BorderStyle BorderStyle
        {
            get { return BorderStyle.None; }
        }

        [Browsable(false)]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
            }
        }

        [Browsable(false)]
        public new Image BackgroundImage
        {
            get { return null; }
        }

        [Browsable(false)]
        public new ImageLayout BackgroundImageLayout
        {
            get { return base.BackgroundImageLayout; }
            set { base.BackgroundImageLayout = value; }
        }

        #endregion

        [Category("自定义"), Description("边角弯曲的角度(1-10),数值越大越弯曲")]
        public int Radius
        {
            get { return _radius; }
            set
            {
                if (value > 10)
                {
                    value = 10;
                }
                if (value < 0)
                {
                    value = 0;
                }
                _radius = value;
                //this.Invalidate();
            }
        }

        [Browsable(true)]
        [Category("自定义外观"), Description("文本颜色")]
        public new Color ForeColor
        {
            get { return _foreColor; }
            set { _foreColor = value; }
        }

        [Browsable(true)]
        [Category("自定义外观"), Description("鼠标形状")]
        public new Cursor Cursor
        {
            get { return ZjxlText.Cursor; }
            set { ZjxlText.Cursor = value; }
        }

        [Category("自定义行为"), Description("自动提示方式")]
        public AutoCompleteMode AutoCompleteMode
        {
            get { return ZjxlText.AutoCompleteMode; }
            set { ZjxlText.AutoCompleteMode = value; }
        }

        [Category("自定义行为"), Description("自动提示类型")]
        public AutoCompleteSource AutoCompleteSource
        {
            get { return ZjxlText.AutoCompleteSource; }
            set { ZjxlText.AutoCompleteSource = value; }
        }

        [Category("自定义行为"), Description("自动提示集合")]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get { return ZjxlText.AutoCompleteCustomSource; }
            set { ZjxlText.AutoCompleteCustomSource = value; }
        }

        public int MaxLengh
        {
            get
            {
                return this.ZjxlText.MaxLength;
            }
            set
            {
                this.ZjxlText.MaxLength = value;
            }
        }
        #endregion

        #region - 构造函数 -

        public TextBoxEx()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            InitializeComponent();

            BackColor = Color.Transparent;

            //下面的图片和文本框的大小位置必须设置，否则首次启动时
            //会出现莫名其妙的断痕
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.BorderStyle = BorderStyle.None;
            pic.Height = pic.Width = 16;//图片大小固定为16
            pic.Left = -40;  //隐藏图片
            ZjxlText.Width = Width - 9;
            ZjxlText.Location = new Point(3, 6);
            _oldValue = Caption;
            _defaultBorderColor = BorderColor;

            ZjxlText.MouseEnter += new EventHandler(TextBoxEx2_MouseEnter);
            ZjxlText.Enter += new EventHandler(ZjxlText_Enter);
            ZjxlText.MouseLeave += new EventHandler(TextBoxEx2_MouseLeave);
            ZjxlText.KeyPress += new KeyPressEventHandler(TextBoxEx2_KeyPress);
            pic.MouseEnter += new EventHandler(TextBoxEx2_MouseEnter);
            pic.MouseLeave += new EventHandler(TextBoxEx2_MouseLeave);
            ZjxlText.LostFocus += new EventHandler(Txt_LostFocus);
            ZjxlText.GotFocus += new EventHandler(Txt_GotFocus);
            ZjxlText.KeyDown += new KeyEventHandler(ZjxlText_KeyDown);
            ZjxlText.TextChanged += new EventHandler(ZjxlText_TextChanged);
            pic.BackColor = Color.White;  //不设置成白色则边框会一同加阴影

        }

        void TextBoxEx2_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }

        void ZjxlText_Enter(object sender, EventArgs e)
        {
            ((System.Windows.Forms.TextBox)sender).Focus();
            ((System.Windows.Forms.TextBox)sender).SelectAll();
        }

        #endregion

        #region 辅助函数

        private void MakeTransparent(Image image)
        {
            Bitmap bitmap = image as Bitmap;
            bitmap.MakeTransparent(Color.FromArgb(255, 0, 255));
        }
        private void ShowToolTip()
        {
            if (!ShowError || !_hasError) return;
            if (_toolTip == null) _toolTip = new ToolTip();
            //_toolTip.SetToolTip(this, );
            _toolTip.Show(VerifyManager.GetVerifyMessage(this), this, Size.Width, 0, 3000);
        }
        private void HideToolTip()
        {
            if (ShowError && _toolTip != null)
            {
                _toolTip.Hide(this);
            }
        }
        #endregion

        #region - 事件 -

        private void ZJXLtextbox_Load(object sender, EventArgs e)
        {
            SetWaterMark();
        }

        private void TextBoxEx2_MouseEnter(object sender, EventArgs e)
        {
            _isFouse = true;
            ShowToolTip();
            //((TextBox)sender).Focus();
            //((TextBox)sender).SelectAll();
            //this.Invalidate();
        }

        private void TextBoxEx2_MouseLeave(object sender, EventArgs e)
        {
            _isFouse = false;
            HideToolTip();
            //this.Invalidate();
        }

        private void Txt_GotFocus(object sender, EventArgs e)
        {
            if (_waterMark != null && (ZjxlText.Text.Trim().Length == 0 || ZjxlText.Text.Replace(" ", "") == WaterMark))
            //if (ZjxlText.Text == @" " + WaterMark)
            {
                //获得焦点，切换正常文字等待填写
                ZjxlText.ForeColor = ForeColor;
                ZjxlText.Text = "";
            }
        }

        private void Txt_LostFocus(object sender, EventArgs e)
        {
            SetWaterMark();
            if (_isSetWaterMark) return;
            var vt = VerifyManager.GetVerifyType(this);
            if (vt == null) return;
            var result = vt.Verify(this);
            
            if (result)
            {
                _hasError = false;
                HideToolTip();
                _changBorderColor = false;
                BorderColor = _defaultBorderColor;
                _changBorderColor = true;
            }
            else
            {
                _changBorderColor = false;
                BorderColor = Color.Red;
                _changBorderColor = true;
                _hasError = true;
                ShowToolTip();
            }
            Invalidate();
        }

        private void ZjxlText_KeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        private void ZjxlText_TextChanged(object sender, EventArgs e)
        {            
            //base.OnTextChanged(e);
            _changBorderColor = false;
            BorderColor = _defaultBorderColor;
            _changBorderColor = true;
            if (!_isSetWaterMark)
            {
                var vt = VerifyManager.GetVerifyType(this);
                if (vt != null)
                {
                    var result = vt.InputtingVerify(this);
                    if (!result)
                    {
                        Caption = _oldValue;
                        ZjxlText.SelectionStart = ZjxlText.Text.Length;
                    }
                }
            }
            _oldValue = Caption;
            Invalidate();
            OnTextChanged();
        }
        /// <summary>
        /// 属性改变 重绘控件
        /// 孙明生
        /// </summary>
        private void ZjxlText_ReadOnlyChanged(object sender, EventArgs e)
        {
            //this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {           
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

           // CalculateSizeAndPosition();
            Draw(e.ClipRectangle, e.Graphics);

            base.OnPaint(e);
        }

        #endregion

        #region - 帮助方法 -

        private void SetWaterMark()
        {
            //if (_waterMark != null && (ZjxlText.Text == " " || ZjxlText.Text == @" " + WaterMark))   //用户没有输入
            if (_waterMark != null && (ZjxlText.Text.Trim().Length == 0 || ZjxlText.Text.Replace(" ", "") == WaterMark))
            {
                ZjxlText.ForeColor = _waterMarkColor;
                ZjxlText.Text = WaterMark;
                _isSetWaterMark = true;
            }
            else
            {
                ZjxlText.ForeColor = ForeColor;
                _isSetWaterMark = false;
            }
        }

        private void CalculateSizeAndPosition()
        {
            if (ForeImage != null)
            {
                pic.Top = pic.Left = 3;
                ZjxlText.Width = Width - pic.Width - 12;
                ZjxlText.Location = new Point(16 + 3 + 3, 6);
            }
            else
            {
                pic.Left = -40;  //隐藏图片
                ZjxlText.Width = Width - 9;
                ZjxlText.Location = new Point(3, 6);
            }

            //单行
            if (!ZjxlText.Multiline)
            {
                Height = ZjxlText.Height + 9;
            }
            else
            {
                ZjxlText.Height = Height - 9;
            }
        }

        private void Draw(Rectangle rectangle, Graphics g)
        {

            #region 画背景
            if (this.ReadOnly)
            {
                using (SolidBrush backgroundBrush = new SolidBrush(System.Drawing.SystemColors.Control))
                {
                    g.FillRectangle(backgroundBrush, 2, 2, this.Width - 5, this.Height - 4);
                }
            }
            else
            {
                #region 更改Enable=False时上边出现白边 add by kord
                if (!Enabled)
                {
                    using (SolidBrush backgroundBrush = new SolidBrush(System.Drawing.SystemColors.Control))
                    {
                        g.FillRectangle(backgroundBrush, 2, 2, this.Width - 5, this.Height - 4);
                    }
                }
                #endregion
                else
                {
                    using (SolidBrush backgroundBrush = new SolidBrush(Color.White))
                    {
                        g.FillRectangle(backgroundBrush, 2, 2, this.Width - 5, this.Height - 4);
                    }
                }
            }
            #endregion

            #region 画阴影(外边框)

            Color drawShadowColor = _shadowColor;
            if (!_isFouse)    //判断是否获得焦点
            {
                drawShadowColor = Color.Transparent;
            }
            using (Pen shadowPen = new Pen(drawShadowColor))
            {
                if (_radius == 0)
                {
                    g.DrawRectangle(shadowPen, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1));
                }
                else
                {
                    g.DrawPath(shadowPen, DrawRoundRect(rectangle.X, rectangle.Y, rectangle.Width - 2, rectangle.Height - 1, _radius));
                }
            }
            #endregion

            #region 画边框
            using (Pen borderPen = new Pen(_borderColor))
            {
                if (_radius == 0)
                {
                    g.DrawRectangle(borderPen, new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 3, rectangle.Height - 3));
                }
                else
                {
                    g.DrawPath(borderPen, DrawRoundRect(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 3, rectangle.Height - 2, _radius));
                }
            }
            #endregion
        }

        #endregion

        #region 数据控件值及值验证 add by kord
        /// <summary>
        /// 控件值
        /// </summary>
        public object Value
        {
            get { return ZjxlText.Text; }
            set { Caption = value == null ? String.Empty : value.ToString(); }
        }
        /// <summary>
        /// 是否包含错误信息
        /// </summary>
        public bool Verify(bool showError = false)
        {
            var result = VerifyManager.Verify(this);
            if (result)
            {
                _hasError = false;
                HideToolTip();
                _changBorderColor = false;
                BorderColor = _defaultBorderColor;
                _changBorderColor = true;
            }
            else
            {
                _changBorderColor = false;
                BorderColor = Color.Red;
                _changBorderColor = true;
                _hasError = true;
                ShowToolTip();
            }
            Invalidate();
            return result;
        }
        /// <summary>
        /// 是否包含错误信息
        /// </summary>
        public bool InputtingVerify(bool showError = false)
        {
            return VerifyManager.Verify(this);
        }
        /// <summary>
        /// 是否显示错误信息
        /// </summary>
        public bool ShowError { get; set; }
        /// <summary>
        /// 验证类型名称
        /// </summary>
        public string VerifyTypeName { get; set; }
        /// <summary>
        /// 验证类型名称
        /// </summary>
        public Type VerifyType { get; set; }
        /// <summary>
        /// 验证条件值
        /// </summary>
        public string VerifyCondition { get; set; }
        /// <summary>
        /// 实时验证条件
        /// </summary>
        public string InputtingVerifyCondition { get; set; }
        public event EventHandler ValueChanged;
        #endregion

        #region Method -- 方法 add by kord
        public override string ToString()
        {
            return Value == null ? String.Empty : Value.ToString();
        }
        #endregion
    }
}
