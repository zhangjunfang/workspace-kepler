using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HXC.UI.Library.Controls
{
    /// <summary>
    /// ExtFieldControl
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/13 10:25:19</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public partial class ExtFieldControl : ExtUserControl
    {
        #region Constructor -- 构造函数
        public ExtFieldControl()
        {
            InitializeComponent();

            InitEvent();

            InitProperty();
        }
        #endregion

        #region Field -- 字段
        
        #endregion

        #region Property -- 属性
        private Boolean _requiredField = false;
        /// <summary>
        /// 必输字段
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("必输字段")]
        public Boolean RequiredField
        {
            get { return _requiredField; }
            set
            {
                if (Equals(_requiredField, value)) return;
                _requiredField = value;
                key_control.ForeColor = value ? Color.Red : Color.Black;
            }
        }

        private String _judgeSymbols = "like '%{0}%'";
        /// <summary>
        /// 查询判断条件
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("查询判断条件")]
        public String JudgeSymbols
        {
            get { return _judgeSymbols; }
            set { _judgeSymbols = value; }
        }

        /// <summary>
        /// 显示名
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("显示名")]
        public String DisplayName
        {
            get { return key_control.Text; }
            set
            {
                if (Equals(key_control.Text, value)) return;
                var g = key_control.CreateGraphics();
                var size = g.MeasureString(value, key_control.Font);
                NameCtrlWidth = (int)size.Width + 10;   //添加10个单位,作为修正值
                key_control.Text = value;
            }
        }

        /// <summary>
        /// 对应的数据字段名称
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("对应的数据字段名称")]

        public String FieldName { get; set; }
        private Control _valueControl = new ExtTextBox();
        /// <summary>
        /// 值控件(所有的值控件均实现IContentControl)
        /// </summary>
        [Browsable(false)]
        public Control ValueControl
        {
            get { return _valueControl; }
            set
            {
                if(Equals(_valueControl, value)) return;
                if (control_panel.Controls.Contains(_valueControl))
                {
                    control_panel.Controls.Remove(_valueControl);
                }
                _valueControl = value;
                InitValueControl();
                control_panel.Controls.Add(value);
                control_panel_SizeChanged(this, null);
            }
        }
        /// <summary>
        /// 显示名称控件宽度
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("显示名称控件宽度")]
        public Int32 NameCtrlWidth
        {
            get { return key_control.Width; }
            set
            {
                if (!Equals(key_control.Width, value))
                {
                    key_control.Width = value;
                    control_panel_SizeChanged(null, null);
                    OnPropertyChanged("NameCtrlWidth");
                }
            }
        }
        /// <summary>
        /// 值控件宽度
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("值控件宽度")]
        public Int32 ValueCtrlWidth
        {
            get { return _valueControl.Width; }
            set
            {
                if (!Equals(_valueControl.Width, value))
                {
                    _valueControl.Width = value;
                    control_panel_SizeChanged(null, null);
                    OnPropertyChanged("ValueCtrlWidth");
                }
            }
        }

        private FieldCtrlType _fieldCtrlType = FieldCtrlType.TextBox;
        /// <summary>
        /// 值控件类型
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("值控件类型")]
        public FieldCtrlType FieldCtrlType {
            get { return _fieldCtrlType; }
            set
            {
                if (Equals(_fieldCtrlType, value)) return;
                _fieldCtrlType = value;
                switch (_fieldCtrlType)
                {
                    case FieldCtrlType.TextBox:
                        ValueControl = new ExtTextBox();
                        break;
                    case FieldCtrlType.TextChoser:
                        ValueControl = new ExtTextChooser();
                        break;
                    case FieldCtrlType.ComboBox:
                        ValueControl = new ExtComboBox();
                        break;
                    case FieldCtrlType.DatetimePicker:
                        ValueControl = new ExtDatetimePicker();
                        break;
                }
            } 
        }
        #endregion

        #region Method -- 方法
        private void InitEvent()
        {
            control_panel.SizeChanged += control_panel_SizeChanged;
            ValueChanged += ExtFieldControl_ValueChanged;
        }
        private void InitProperty()
        {
            control_panel.BackColor = Color.Transparent;
            InitValueControl();
            control_panel.Controls.Add(_valueControl);
        }
        private void InitValueControl()
        {
            if (_valueControl != null)
            {
                _valueControl.Margin = new Padding(0);
                if (_valueControl is IContentControl)
                {
                    (_valueControl as IContentControl).ValueChanged += ValueControl_ValueChanged;
                }
                control_panel_SizeChanged(null, null);
            }
        }
        internal int GetTrimNameCtrlWidth()
        {
            var g = key_control.CreateGraphics();
            var size = g.MeasureString(DisplayName, key_control.Font);
            return (int)size.Width + 10;   //添加10个单位,作为修正值
        }
        internal int GetTrimValueCtrlWidth()
        {
            return control_panel.Width - key_control.Margin.All - _valueControl.Margin.All - GetTrimNameCtrlWidth();
        }
        public String GetQueryString()
        {
            if (String.IsNullOrEmpty(FieldName)) return "";
            return String.Format("{0} like '%{1}%'", FieldName, this);
        }
        #endregion

        #region Event -- 事件
        public void ExtFieldControl_ValueChanged(object sender, EventArgs e)
        {
            var contentControl = _valueControl as IContentControl;
            if (contentControl != null && !Equals(contentControl.Value, Value))
            {
                contentControl.Value = Value;
            }
        }
        public void ValueControl_ValueChanged(object sender, EventArgs e)
        {
            var contentControl = _valueControl as IContentControl;
            if (contentControl != null && !Equals(contentControl.Value, Value))
            {
                Value = contentControl.Value;
            }
        }
        private void control_panel_SizeChanged(object sender, EventArgs e)
        {
            var valueCtrlWidth = control_panel.Width - key_control.Margin.All - _valueControl.Margin.All - key_control.Width;
            ValueCtrlWidth = valueCtrlWidth;
        }
        #endregion

        #region Interface -- 接口
        #region IContentControl -- 内容控件接口
        /// <summary>
        /// 控件值
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件值")]
        public new object Value
        {
            get { return (ValueControl as IContentControl) != null ? (ValueControl as IContentControl).Value : null; }
            set
            {
                if ((ValueControl as IContentControl) == null) return;
                (ValueControl as IContentControl).Value = value;
                var handler = ValueChanged;
                if (handler == null) return;
                handler(this, null);
            }
        }
        /// <summary>
        /// 控件显示值
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件显示值")]
        public new string DisplayValue
        {
            get
            {
                return (ValueControl as IContentControl) != null ? (ValueControl as IContentControl).DisplayValue : String.Empty;
            }
            set
            {
                if ((ValueControl as IContentControl) != null)
                {
                    (ValueControl as IContentControl).DisplayValue = value;
                }
            }
        }
        public new event EventHandler ValueChanged;
        /// <summary>
        /// 验证信息
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("验证信息")]
        public new bool Verify(bool showError = false)
        {
            if (!(_valueControl is IContentControl)) return true;
            var result = (_valueControl as IContentControl).InputtingVerify(showError);
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
        /// <summary>
        /// 实时验证信息
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("实时验证信息")]
        public new bool InputtingVerify(bool showError = false)
        {
            if (_valueControl is IContentControl)
            {
                return (_valueControl as IContentControl).InputtingVerify(showError);
            }
            return true;
        }
        /// <summary>
        /// 是否在验证后显示错误信息
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("是否在验证后显示错误信息")]
        public new bool ShowError
        {
            get
            {
                if (_valueControl is IContentControl)
                {
                    return (_valueControl as IContentControl).ShowError;
                }
                return false;
            }
            set
            {
                if (_valueControl is IContentControl)
                {
                    (_valueControl as IContentControl).ShowError = value;
                }
            }
        }
        /// <summary>
        /// 控件值验证类型名称
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件值验证类型名称")]
        public new string VerifyTypeName
        {
            get
            {
                if (_valueControl is IContentControl)
                {
                    return (_valueControl as IContentControl).VerifyTypeName;
                }
                return null;
            }
            set
            {
                if (_valueControl is IContentControl)
                {
                    (_valueControl as IContentControl).VerifyTypeName = value;
                }
            }
        }
        /// <summary>
        /// 控件值验证类型
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件值验证类型")]
        public new Type VerifyType
        {
            get
            {
                if (_valueControl is IContentControl)
                {
                    return (_valueControl as IContentControl).VerifyType;
                }
                return null;
            }
            set
            {
                if (_valueControl is IContentControl)
                {
                    (_valueControl as IContentControl).VerifyType = value;
                }
            }
        }
        /// <summary>
        /// 控件值验证条件
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件值验证条件")]
        public new string VerifyCondition
        {
            get
            {
                if (_valueControl is IContentControl)
                {
                    return (_valueControl as IContentControl).VerifyCondition;
                }
                return null;
            }
            set
            {
                if (_valueControl is IContentControl)
                {
                    (_valueControl as IContentControl).VerifyCondition = value;
                }
            }
        }
        /// <summary>
        /// 控件实时验证条件
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件实时验证条件")]
        public new string InputtingVerifyCondition
        {
            get
            {
                if (_valueControl is IContentControl)
                {
                    return (_valueControl as IContentControl).InputtingVerifyCondition;
                }
                return null;
            }
            set
            {
                if (_valueControl is IContentControl)
                {
                    (_valueControl as IContentControl).InputtingVerifyCondition = value;
                }
            }
        }
        /// <summary>
        /// 控件内容
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件内容")]
        public new object Content
        {
            get
            {
                return (ValueControl as IContentControl) != null ? (ValueControl as IContentControl).Content : null;
            }
            set
            {
                if ((ValueControl as IContentControl) != null)
                {
                    (ValueControl as IContentControl).Content = value;
                }
            }
        }
        /// <summary>
        /// 控件内容类型名称
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件内容类型名称")]
        public new String ContentTypeName
        {
            get
            {
                return (ValueControl as IContentControl) != null ? (ValueControl as IContentControl).ContentTypeName : null;
            }
            set
            {
                if ((ValueControl as IContentControl) != null)
                {
                    (ValueControl as IContentControl).ContentTypeName = value;
                }
            }
        }
        /// <summary>
        /// 控件内容类型参数
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("控件内容类型参数")]
        public new Object ContentTypeParameter
        {
            get
            {
                return (ValueControl as IContentControl) != null ? (ValueControl as IContentControl).ContentTypeParameter : null;
            }
            set
            {
                if ((ValueControl as IContentControl) != null)
                {
                    (ValueControl as IContentControl).ContentTypeParameter = value;
                }
            }
        }
        /// <summary>
        /// 验证信息
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("验证信息")]
        public new void EmptyValue()
        {
            if ((_valueControl as IContentControl) == null) return;
            (_valueControl as IContentControl).EmptyValue();
        }
        #endregion
        #endregion
    }

    public enum FieldCtrlType
    {
        TextBox = 1,
        TextChoser =2,
        ComboBox = 3,
        DatetimePicker = 4,
    }
}
