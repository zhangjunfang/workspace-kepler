using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HXC.UI.Library.Controls
{
    /// <summary>
    /// ExtFieldPanel
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/14 17:34:34</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public class ExtFieldPanel : FlowLayoutPanel
    {
        #region Constructor -- 构造函数
        public ExtFieldPanel()
        {
            InitEvent();

            InitProperty();
        }
        #endregion

        #region Field -- 字段

        #endregion

        #region Property -- 属性
        private int _itemWidth = 150;
        /// <summary>
        /// 子控件宽度
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("子控件宽度")]
        public int ItemValueWidth
        {
            get { return _itemWidth; }
            set
            {
                if (Equals(_itemWidth, value)) return;
                _itemWidth = value;
                SetItemSize();
            }
        }

        private int _itemHeight = 23;
        /// <summary>
        /// 子控件高度
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("子控件高度")]
        public int ItemHeight
        {
            get { return _itemHeight; }
            set
            {
                if (Equals(_itemHeight, value)) return; 
                _itemHeight = value;
                SetItemSize();
            }
        }

        private Boolean _autoPlacement = true;
        /// <summary>
        /// 是否自动设定子控件大小
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("是否自动设定子控件大小")]
        public Boolean AutoPlacement
        {
            get { return _autoPlacement; }
            set
            {
                if (Equals(_autoPlacement, value)) return;
                _autoPlacement = value;
                SetItemSize();
            }
        }

        /// <summary>
        /// 子控件布局是否自动换行
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("子控件布局是否自动换行")]
        public new Boolean WrapContents
        {
            get { return base.WrapContents; }
            set
            {
                if (Equals(base.WrapContents, value)) return;
                base.WrapContents = value;
            }
        }
        #endregion

        #region Method -- 方法
        private void InitEvent()
        {
            ControlAdded += (sender, e) =>
            {
                if (e.Control is ExtFieldControl)
                {
                    SetItemSize();
                }
                else
                {
                    Controls.Remove(e.Control);
                }
            };
           ControlRemoved += (sender, e) => SetItemSize();
        }
        private void InitProperty()
        {
            base.WrapContents = true;
            BackColor = DefaultStyle.DefaultBackColor;
        }
        private void SetItemSize()
        {
            if (AutoPlacement || ItemValueWidth == 0 || ItemHeight == 0)
            {
                var keyMaxWidth = GetItemKeyWidth();
                //var valueMaxWidth = GetItemMaxValueWidth();
                var itemMaxHeight = GetItemHeight();
                foreach (ExtFieldControl control in Controls)
                {
                    control.Size = new Size(keyMaxWidth + ItemValueWidth, itemMaxHeight);
                    control.NameCtrlWidth = keyMaxWidth;
                    //control.ValueCtrlWidth = valueMaxWidth;
                }
            }
            else
            {
                foreach (ExtFieldControl control in Controls)
                {
                    control.Size = new Size(control.GetTrimNameCtrlWidth() + ItemValueWidth, ItemHeight);
                    control.NameCtrlWidth = control.GetTrimNameCtrlWidth();
                }
            }
        }
        private int GetItemKeyWidth()
        {
            return (from ExtFieldControl control in Controls select control.GetTrimNameCtrlWidth()).Concat(new[] { 0 }).Max();
        }
        private int GetItemHeight()
        {
            return (from ExtFieldControl control in Controls select control.Size.Height).Concat(new[] { 0 }).Max();
        }
        public void ClearItemValue()
        {
            foreach (ExtFieldControl control in Controls)
            {
                control.Value = String.Empty;
            }
        }
        public String GetQueryString()
        {
            var sb = new StringBuilder();
            sb.Append("1=1");
            foreach (ExtFieldControl control in Controls)
            {
                if(String.IsNullOrEmpty(control.FieldName)) continue;
                sb.Append(" and ");
                sb.Append(control.FieldName);
                sb.Append(String.Format(String.IsNullOrEmpty(control.JudgeSymbols) ? " like '%{0}%'" : " " + control.JudgeSymbols,  control.Value));
            }
            return sb.ToString();
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
