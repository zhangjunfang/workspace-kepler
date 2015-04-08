
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HXC.UI.Library.Verify;

namespace HXC.UI.Library.Controls
{
    /// <summary>
    /// 文本框控件扩展
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/10 15:07:56</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public partial class ExtTextBox : Panel, IBorderStyle, INotifyPropertyChanged, IContentControl
    {
        #region Constructor -- 构造函数
        public ExtTextBox()
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
        [Browsable(false)]
        [Obsolete("已过时的属性,请使用BorderWidth", true)]
        public new BorderStyle BorderStyle { get; set; }
        /// <summary>
        /// 文本对齐方式
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        public HorizontalAlignment TextAlign
        {
            get { return txt_textbox.TextAlign; }
            set { txt_textbox.TextAlign = value; }
        }
        /// <summary>
        /// 当前控件是否只读
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        public bool ReadOnly
        {
            get { return txt_textbox.ReadOnly; }
            set { txt_textbox.ReadOnly = value; }
        }
        #endregion

        #region Method -- 方法
        private void InitEvent()
        {
            BorderWidthChanged += ExtTextBox_BorderWidthChanged;
            txt_textbox.TextChanged += txt_textbox_TextChanged;
            txt_textbox.LostFocus += txt_textbox_LostFocus;
            txt_textbox.GotFocus += txt_textbox_GotFocus;
            txt_textbox.MouseEnter += txt_textbox_MouseEnter;
            txt_textbox.MouseLeave += txt_textbox_MouseLeave;
            FontChanged += ExtTextBox_FontChanged;
            SizeChanged += ExtTextBox_SizeChanged;
        }

        private void InitProperty()
        {
            BackColor = DefaultStyle.DefaultInputBackColor;
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
        public override string ToString()
        {
            return Value == null ? String.Empty : Value.ToString();
        }
        #endregion

        #region Event -- 事件
        private void ExtTextBox_FontChanged(object sender, EventArgs e)
        {
            txt_textbox.Font = Font;
        }
        private void ExtTextBox_BorderWidthChanged(object sender, EventArgs e)
        {
            var borderWidth = BorderWidth + 2;
            txt_textbox.Location = new Point(borderWidth, borderWidth);
            txt_textbox.Size = new Size(Size.Width - borderWidth * 2, Size.Height - borderWidth * 2);
            Invalidate(true);
        }
        private void txt_textbox_TextChanged(object sender, EventArgs e)
        {
            Value = txt_textbox.Text;
            if (InputtingVerify())
            {
                _oldValue = Value;
            }
            else
            {
                Value = _oldValue == null ? String.Empty : _oldValue.ToString();
                txt_textbox.SelectionStart = txt_textbox.Text.Length;
            }
            Invalidate();
        }
        private void txt_textbox_GotFocus(object sender, EventArgs e)
        {
            RemoveErrorTip();
        }
        private void txt_textbox_LostFocus(object sender, EventArgs e)
        {
            Verify(true);
        }
        private void txt_textbox_MouseLeave(object sender, EventArgs e)
        {
            HideErrorTip();
        }
        private void txt_textbox_MouseEnter(object sender, EventArgs e)
        {
            ShowErrorTip();
        }
        private void ExtTextBox_SizeChanged(object sender, EventArgs e)
        {
            Invalidate(true);
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
        private int _borderWidth = 1;
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

        private int _cornerRadiu = 5;
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
        private object _value;
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
                if (!Equals(value, txt_textbox.Text)) txt_textbox.Text = value == null ? String.Empty : value.ToString();
                var valueChanged = ValueChanged;
                if (valueChanged != null)
                {
                    ValueChanged.Invoke(this, null);
                }
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
        public String ContentTypeParameter { get; set; }
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
