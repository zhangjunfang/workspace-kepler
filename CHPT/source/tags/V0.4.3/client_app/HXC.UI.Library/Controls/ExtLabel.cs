using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HXC.UI.Library.Verify;

namespace HXC.UI.Library.Controls
{
    /// <summary>
    /// ExtLabel
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/16 9:30:48</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public class ExtLabel : Label, INotifyPropertyChanged, IContentControl
    {
        #region Constructor -- 构造函数
        public ExtLabel()
        {
            InitEvent();

            InitProperty();
        }
        #endregion

        #region Field -- 字段
        #region Field -- 字段
        internal Object _oldValue;
        internal Color _defaultBorderColor = DefaultStyle.DefaultBorderColor;
        internal ToolTip _toolTip;
        internal Boolean _hasError;
        internal Boolean _changBorderColor = true;
        #endregion
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        private void InitEvent()
        {
            TextChanged += ExtLabel_TextChanged;
        }
        private void InitProperty()
        {
            BackColor = DefaultStyle.DefaultBackColor;
        }
        public override string ToString()
        {
            return Value == null ? String.Empty : Value.ToString();
        }
        #endregion

        #region Event -- 事件
        private void ExtLabel_TextChanged(object sender, EventArgs e)
        {
            Value = Text;
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
                if (!Equals(value, Text)) Text = value == null ? String.Empty : value.ToString();
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
            return VerifyManager.Verify(this);
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
