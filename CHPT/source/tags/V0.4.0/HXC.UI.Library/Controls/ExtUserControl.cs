using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HXC.UI.Library.Verify;

namespace HXC.UI.Library.Controls
{
    /// <summary>
    /// 用户控件扩展
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/10 14:01:13</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public partial class ExtUserControl : UserControl,IBorderStyle,INotifyPropertyChanged,IContentControl
    {
        #region Constructor -- 构造函数
        public ExtUserControl()
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
        [Obsolete("已过时的属性,请使用BorderWidth",true)]
        public new BorderStyle BorderStyle { get; set; }
        #endregion

        #region Method -- 方法
        private void InitEvent()
        {
            SizeChanged += ExtUserControl_SizeChanged;
        }
        private void InitProperty()
        {
            BackColor = DefaultStyle.DefaultBackColor;
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
        void ExtUserControl_SizeChanged(object sender, EventArgs e)
        {
            Invalidate(true);
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
        [Category("Extend")]
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
        [Category("Extend")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                if (!Equals(_borderColor != value)) _borderColor = value;
                if (!_changBorderColor)
                {
                    OnPropertyChanged("BorderColor");
                    return;
                }
                _defaultBorderColor = value;
                OnPropertyChanged("BorderColor");
            }
        }

        private int _cornerRadiu = 5;
        [Browsable(true)]
        [Category("Extend")]
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
        [Category("Extend")]
        [Description("控件值")]
        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                var handler = ValueChanged;
                if (handler == null) return;
                handler(this, null);
            }
        }
        /// <summary>
        /// 控件显示值
        /// </summary>
        [Browsable(true)]
        [Category("Extend")]
        [Description("控件显示值")]
        public string DisplayValue
        {
            get { return Value == null ? String.Empty : Value.ToString(); }
            set
            {
                Value = value;
            }
        }
        public virtual void EmptyValue()
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
        [Category("Extend")]
        [Description("是否在验证后显示错误信息")]
        public bool ShowError { get; set; }
        /// <summary>
        /// 控件值验证类型名称
        /// </summary>
        [Browsable(true)]
        [Category("Extend")]
        [Description("控件值验证类型名称")]
        public string VerifyTypeName { get; set; }
        /// <summary>
        /// 控件值验证类型
        /// </summary>
        [Browsable(true)]
        [Category("Extend")]
        [Description("控件值验证类型")]
        public Type VerifyType { get; set; }
        /// <summary>
        /// 控件值验证条件
        /// </summary>
        [Browsable(true)]
        [Category("Extend")]
        [Description("控件值验证条件")]
        public string VerifyCondition { get; set; }
        /// <summary>
        /// 控件实时验证条件
        /// </summary>
        [Browsable(true)]
        [Category("Extend")]
        [Description("控件实时验证条件")]
        public string InputtingVerifyCondition { get; set; }
        public event EventHandler ValueChanged;
        #endregion
        #endregion
    }
}
