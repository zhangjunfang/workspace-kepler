using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HXC.UI.Library.Verify;

namespace HXC.UI.Library.Controls
{
    /// <summary>
    /// ExtButton
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/16 9:21:19</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public partial class ExtButton : UserControl, IBorderStyle, INotifyPropertyChanged, IContentControl
    {
        #region Constructor -- 构造函数
        public ExtButton()
        {
            InitializeComponent();

            InitEvent();
            
            InitProperty();
        }
        #endregion

        #region Field -- 字段
        internal Object _oldValue;
        internal Color _defaultBorderColor = DefaultStyle.DefaultBorderColor;
        internal ToolTip _toolTip;
        internal Boolean _hasError;
        internal Boolean _changBorderColor = true;
        #endregion

        #region Property -- 属性
        public new event EventHandler Click;

        [Browsable(false)]
        [Obsolete("已过时的属性,请使用BorderWidth", true)]
        public new BorderStyle BorderStyle { get; set; }

        private Image _normalBackGroundImage;

        /// <summary>
        /// 获取或设置在控件中显示的默认背景图像
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("获取或设置在控件中显示的默认背景图像")]
        public Image NormalBackgroudImage
        {
            get { return _normalBackGroundImage; }
            set
            {
                _normalBackGroundImage = value;
                BackgroundImage = value;
            }
        }

        /// <summary>
        /// 获取或设置在控件中显示的高亮背景图像
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("获取或设置在控件中显示的高亮背景图像")]
        public Image LightBackgroudImage { get; set; }

        /// <summary>
        /// 文本字体信息
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("文本字体信息")]
        public new Font Font
        {
            get { return lbl_label.Font; }
            set { lbl_label.Font = value; }
        }

        /// <summary>
        /// 文本对齐方式
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("文本对齐方式")]
        public ContentAlignment TextAlign
        {
            get { return lbl_label.TextAlign; }
            set { lbl_label.TextAlign = value; }
        }
        #endregion

        #region Method -- 方法
        private void InitEvent()
        {
            FontChanged += ExtButton_FontChanged;
            BorderWidthChanged += ExtButton_BorderWidthChanged;
            SizeChanged += ExtButton_BorderWidthChanged;
            lbl_label.TextChanged += Label_TextChanged;
            lbl_label.LostFocus += ExtButton_LostFocus;
            lbl_label.GotFocus += ExtButton_GotFocus;
            lbl_label.MouseEnter += ExtButton_MouseEnter;
            lbl_label.MouseLeave += ExtButton_MouseLeave;
            lbl_label.Click += lbl_label_Click;
        }

        private void InitProperty()
        {
            //BackgroundImageLayout = ImageLayout.None;
            NormalBackgroudImage = Properties.Resources.bg_button_normal;
            LightBackgroudImage = Properties.Resources.bg_button_light;
            BackColor = DefaultStyle.DefaultBackColor;
            BorderWidth = 0;
        }
        internal void ShowErrorTip()
        {
            if (!_hasError) return;
            _changBorderColor = false;
            BorderColor = Color.Red;
            _changBorderColor = true;

            if (!ShowError || !_hasError) return;
            if (_toolTip == null) _toolTip = new ToolTip();
            _toolTip.Show(VerifyManager.GetVerifyMessage(this), this, Size.Width, 0, 3000);
        }
        internal void RemoveErrorTip()
        {
            if (!_hasError) return;
            if (ShowError && _toolTip != null)
            {
                _toolTip.Hide(this);
            }

            _changBorderColor = false;
            BorderColor = _defaultBorderColor;
            _changBorderColor = true;
        }
        internal void HideErrorTip()
        {
            if (!_hasError) return;
            if (ShowError && _toolTip != null)
            {
                _toolTip.Hide(this);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            BorderStylePaint.DrawRoundRectangle(g, this);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            var g = e.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            using (var pen = new Pen(new SolidBrush(BorderColor)))
            {
                var rectangle = new Rectangle(BorderWidth, BorderWidth, Width - 2*BorderWidth, Height - 2*BorderWidth);
                g.DrawRectangle(pen, rectangle);
            }
                    
        }

        public override string ToString()
        {
            return Value == null ? String.Empty : Value.ToString();
        }
        #endregion

        #region Event -- 事件
        public void lbl_label_Click(object sender, EventArgs e)
        {
            var handle = Click;
            if (handle != null)
            {
                Click(sender, e);
            }
        }
        private void ExtButton_FontChanged(object sender, EventArgs e)
        {
            lbl_label.Font = Font;
        }
        private void ExtButton_BorderWidthChanged(object sender, EventArgs e)
        {
            var borderWidth = BorderWidth;
            lbl_label.Location = new Point(borderWidth, borderWidth);
            lbl_label.Size = new Size(Size.Width - borderWidth * 2, Size.Height - borderWidth * 2);
            Invalidate(true);
        }
        private void Label_TextChanged(object sender, EventArgs e)
        {
            Value = lbl_label.Text;
            if (InputtingVerify())
            {
                _oldValue = Value;
            }
            else
            {
                Value = _oldValue == null ? String.Empty : _oldValue.ToString();
            }
            Invalidate();
        }
        private void ExtButton_GotFocus(object sender, EventArgs e)
        {
            RemoveErrorTip();
            BackgroundImage = LightBackgroudImage;
        }
        private void ExtButton_LostFocus(object sender, EventArgs e)
        {
            Verify(true);
            BackgroundImage = NormalBackgroudImage;
        }
        private void ExtButton_MouseLeave(object sender, EventArgs e)
        {
            HideErrorTip();
            BackgroundImage = NormalBackgroudImage;
        }
        private void ExtButton_MouseEnter(object sender, EventArgs e)
        {
            ShowErrorTip();
            BackgroundImage = LightBackgroudImage;
        }
        #endregion

        #region Interface -- 接口
        #region INotifyPropertyChanged -- 属性更改通知
        public event PropertyChangedEventHandler PropertyChanged;
        internal void OnPropertyChanged(string name)
        {
            Invalidate(true);
            var handler = PropertyChanged;
            if (handler == null) return;
            handler(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region IBorderStyle -- 控件边框
        private int _borderWidth;
        [Browsable(true)]
        [Category("Extensions")]
        public int BorderWidth
        {
            get { return _borderWidth; }
            set
            {
                if (_borderWidth == value) return;
                _borderWidth = value;
                var handler = BorderWidthChanged;
                if (handler == null) return;
                handler(this, null);
                OnPropertyChanged("BorderWidth");
            }
        }
        private Color _borderColor = DefaultStyle.DefaultBorderColor;
        [Browsable(true)]
        [Category("Extensions")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                if (Equals(_borderColor, value)) return;
                if (!_changBorderColor)
                {
                    OnPropertyChanged("BorderColor");
                    return;
                }

                OnPropertyChanged("BorderColor");
            }
        }
        private int _cornerRadiu;
        [Browsable(true)]
        [Category("Extensions")]
        public int CornerRadiu
        {
            get { return _cornerRadiu; }
            set
            {
                if (_cornerRadiu == value) return;
                _cornerRadiu = value;
                OnPropertyChanged("CornerRadiu");
            }
        }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(new Point(BorderWidth / 2, BorderWidth / 2), new Size(Size.Width - BorderWidth, Size.Height - BorderWidth));
            }
        }
        public event EventHandler BorderWidthChanged;
        #endregion

        #region IContentControl -- 内容控件接口
        private object _value = "Button";
        /// <summary>
        /// 控件值
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件值")]
        public object Value
        {
            get { return _value; }
            set
            {
                if (Equals(value, _value)) return;
                _value = value;
                if (!Equals(value, lbl_label.Text)) lbl_label.Text = value == null ? String.Empty : value.ToString();
                var valueChanged = ValueChanged;
                if (valueChanged != null)
                {
                    ValueChanged.Invoke(this, null);
                }
            }
        }
        /// <summary>
        /// 控件显示值
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件显示值")]
        public string DisplayValue
        {
            get { return Value == null ? String.Empty : Value.ToString(); }
            set
            {
                Value = value;
            }
        }
        /// <summary>
        /// 控件内容
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件内容")]
        public object Content { get; set; }
        /// <summary>
        /// 控件内容类型名称
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件内容类型名称")]
        public String ContentTypeName { get; set; }
        /// <summary>
        /// 控件内容类型参数
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件内容类型参数")]
        public Object ContentTypeParameter { get; set; }
        public void EmptyValue()
        {
            Value = String.Empty;
        }
        public bool Verify(bool showError = false)
        {
            var result = VerifyManager.Verify(this);
            if (!result && showError)
            {
                _hasError = true;
                ShowErrorTip();
            }
            else
            {
                RemoveErrorTip();
                _hasError = false;
            }
            return result;
        }
        public bool InputtingVerify(bool showError = false)
        {
            return VerifyManager.InputtingVerify(this);
        }
        /// <summary>
        /// 是否在验证后显示错误信息
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("是否在验证后显示错误信息")]
        public bool ShowError { get; set; }
        /// <summary>
        /// 控件值验证类型名称
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件值验证类型名称")]
        public string VerifyTypeName { get; set; }
        /// <summary>
        /// 控件值验证类型
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件值验证类型")]
        public Type VerifyType { get; set; }
        /// <summary>
        /// 控件值验证条件
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件值验证条件")]
        public string VerifyCondition { get; set; }
        /// <summary>
        /// 控件实时验证条件
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件实时验证条件")]
        public string InputtingVerifyCondition { get; set; }
        public event EventHandler ValueChanged;
        #endregion
        #endregion
    }
}
