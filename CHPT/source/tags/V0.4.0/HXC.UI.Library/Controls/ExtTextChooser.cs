using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HXC.UI.Library.Verify;

namespace HXC.UI.Library.Controls
{
    /// <summary>
    /// 扩展控件选择器
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/14 10:00:04</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public partial class ExtTextChooser : UserControl, IBorderStyle, INotifyPropertyChanged, IContentControl
    {
        #region Constructor -- 构造函数
        public ExtTextChooser()
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
        [Category("Extend")]
        public HorizontalAlignment TextAlign
        {
            get { return txt_textbox.TextAlign; }
            set { txt_textbox.TextAlign = value; }
        }
        /// <summary>
        /// 当前控件是否只读
        /// </summary>
        [Browsable(true)]
        [Category("Extend")]
        public bool ReadOnly
        {
            get { return txt_textbox.ReadOnly; }
            set { txt_textbox.ReadOnly = value; }
        }
        [Browsable(true)]
        [Category("Extend")]
        [Description("选择器事件")]
        public event EventHandler ChooserClick;

        private ChooserType _chooserTypeImage;
        [Browsable(true)]
        [Category("Extend")]
        [Description("选择器图标类型")]
        public ChooserType ChooserTypeImage
        {
            get { return _chooserTypeImage; }
            set
            {
                if (_chooserTypeImage == value) return;
                _chooserTypeImage = value;

                switch (_chooserTypeImage)
                {
                    case ChooserType.Company:
                        //uc_chooser_icon.BackgroundImage = Skin.Properties.Resources.Chooser_Company;
                        break;
                    case ChooserType.Contact:
                        //uc_chooser_icon.BackgroundImage = Skin.Properties.Resources.Chooser_Contact;
                        break;
                    case ChooserType.Default:
                        //uc_chooser_icon.BackgroundImage = Skin.Properties.Resources.Chooser_Default;
                        break;
                    case ChooserType.Part:
                        //uc_chooser_icon.BackgroundImage = Skin.Properties.Resources.Chooser_Part;
                        break;
                    case ChooserType.PartCode:
                        //uc_chooser_icon.BackgroundImage = Skin.Properties.Resources.Chooser_Part_Code;
                        break;
                    case ChooserType.PartType:
                        //uc_chooser_icon.BackgroundImage = Skin.Properties.Resources.Chooser_Part_Type;
                        break;
                    case ChooserType.Supplier:
                        //uc_chooser_icon.BackgroundImage = Skin.Properties.Resources.Chooser_Supplier;
                        break;
                    case ChooserType.User:
                        //uc_chooser_icon.BackgroundImage = Skin.Properties.Resources.Chooser_User;
                        break;
                    case ChooserType.Vehicle:
                        //uc_chooser_icon.BackgroundImage = Skin.Properties.Resources.Chooser_Vehicle;
                        break;
                }
            }
        }        
        #endregion

        #region Method -- 方法
        private void InitEvent()
        {
            uc_chooser_icon.Click += uc_chooser_icon_Click;
            BorderWidthChanged += ExtTextChooser_BorderWidthChanged;
            txt_textbox.TextChanged += txt_textbox_TextChanged;
            txt_textbox.LostFocus += txt_textbox_LostFocus;
            txt_textbox.GotFocus += txt_textbox_GotFocus;
            txt_textbox.MouseEnter += txt_textbox_MouseEnter;
            txt_textbox.MouseLeave += txt_textbox_MouseLeave;
            FontChanged += ExtTextChooser_FontChanged;
            SizeChanged += ExtTextChooser_SizeChanged;
        }
        private void InitProperty()
        {
            ReadOnly = false;
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
        public override string ToString()
        {
            return Value == null ? String.Empty : Value.ToString();
        }
        #endregion

        #region Event -- 事件
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            BorderStylePaint.DrawRoundRectangle(g, this);
        }
        private void ExtTextChooser_SizeChanged(object sender, EventArgs e)
        {
            Invalidate(true);
        }
        private void ExtTextChooser_FontChanged(object sender, EventArgs e)
        {
            txt_textbox.Font = Font;
        }
        private void ExtTextChooser_BorderWidthChanged(object sender, EventArgs e)
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
        private void uc_chooser_icon_Click(object sender, EventArgs e)
        {
            var click = ChooserClick;
            if (click != null)
            {
                click.Invoke(sender, e);
            }
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
                if (Equals(_borderColor, value)) return;
                if (!_changBorderColor)
                {
                    OnPropertyChanged("BorderColor");
                    return;
                }
                _borderColor = value;
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
        /// <summary>
        /// 控件值
        /// </summary>
        [Browsable(true)]
        [Category("Extend")]
        [Description("控件值")]
        public object Value { get; set; }
        /// <summary>
        /// 控件显示值
        /// </summary>
        [Browsable(true)]
        [Category("Extend")]
        [Description("控件显示值")]
        public string DisplayValue
        {
            get { return txt_textbox.Text; }
            set
            {
                if (Equals(value, txt_textbox.Text)) return;
                txt_textbox.Text = value ?? String.Empty;
                var valueChanged = ValueChanged;
                if (valueChanged != null)
                {
                    ValueChanged.Invoke(this, null);
                }
            }
        }
        public void EmptyValue()
        {
            DisplayValue = String.Empty;
            Value = null;
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

    /// <summary>
    /// 选择器类型
    /// </summary>
    public enum ChooserType
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default,
        /// <summary>
        /// 往来单位
        /// </summary>
        Company,
        /// <summary>
        /// 联系人
        /// </summary>
        Contact,
        /// <summary>
        /// 配件
        /// </summary>
        Part,
        /// <summary>
        /// 配件编码
        /// </summary>
        PartCode,
        /// <summary>
        /// 配件类型
        /// </summary>
        PartType,
        /// <summary>
        /// 供应商
        /// </summary>
        Supplier,
        /// <summary>
        /// 用户
        /// </summary>
        User,
        /// <summary>
        /// 车型
        /// </summary>
        Vehicle,
    }
}
