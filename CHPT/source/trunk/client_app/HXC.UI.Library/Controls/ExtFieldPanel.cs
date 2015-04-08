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

        private Padding _itemMargin = new Padding(5);
        /// <summary>
        /// 子控件高度
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("子控件高度")]
        public Padding ItemMargin
        {
            get { return _itemMargin; }
            set
            {
                _itemMargin = value;
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

        private Boolean _autoSize = false;
        /// <summary>
        /// 是否自动设置面板大小
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("是否自动设置面板大小")]
        public new Boolean AutoSize
        {
            get { return _autoSize; }
            set
            {
                if (Equals(_autoSize, value)) return;
                _autoSize = value;
                SetItemSize();
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
                    e.Control.VisibleChanged += (o, args) => SetItemSize();
                    SetItemSize();
                }
                else
                {
                    Controls.Remove(e.Control);
                }
            };
            ControlRemoved += (sender, e) => SetItemSize();
            Resize += delegate
            {
                SetSize();
            };
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
                var itemMaxHeight = GetItemHeight();
                foreach (var control in Controls.Cast<ExtFieldControl>().Where(control => control.Visible))
                {
                    control.Size = new Size(keyMaxWidth + ItemValueWidth, itemMaxHeight);
                    control.NameCtrlWidth = keyMaxWidth;
                    control.Margin = ItemMargin;
                }
                SetSize();
            }
            else
            {
                foreach (ExtFieldControl control in Controls)
                {
                    control.Size = new Size(control.GetTrimNameCtrlWidth() + ItemValueWidth, ItemHeight);
                    control.NameCtrlWidth = control.GetTrimNameCtrlWidth();
                    control.Margin = ItemMargin;
                }
            }
        }
        public void SetSize()
        {
            var rouCount = RowCount();
            if (rouCount < 1) return;
            var itemHeight = GetItemHeight();
            var width = itemHeight + ItemMargin.Top + ItemMargin.Bottom;
            Height = rouCount * width;
            Invalidate();
        }
        public Int32 ColumnCount()
        {
            if (!AutoPlacement && ItemValueWidth != 0 && ItemHeight != 0) return -1;
            var keyMaxWidth = GetItemKeyWidth();
            //alter by 杨超逸
            //2015.2.2
            //修改列数量算法
            //var width = keyMaxWidth + ItemValueWidth + Margin.Left + Margin.Right;
            var width = keyMaxWidth + ItemValueWidth + ItemMargin.Left + ItemMargin.Right;
            return Width / width;
            //alter end
        }
        public Int32 RowCount()
        {
            if (!AutoPlacement && ItemValueWidth != 0 && ItemHeight != 0) return -1;
            var ctrlsCount = Controls.Cast<ExtFieldControl>().Count(control => control.Visible);
            var colCount = ColumnCount();
            if (colCount <= 0) return 0;
            if (ctrlsCount % colCount == 0)
            {
                return ctrlsCount / colCount;
            }
            return ctrlsCount / colCount + 1;
        }
        private int GetItemKeyWidth()
        {
            return (from ExtFieldControl control in Controls where control.Visible select control.GetTrimNameCtrlWidth()).Concat(new[] { 0 }).Max();
        }
        private int GetItemHeight()
        {
            return (from ExtFieldControl control in Controls where control.Visible select control.Size.Height).Concat(new[] { 0 }).Max();
        }
        public void ClearItemValue()
        {
            foreach (ExtFieldControl control in Controls)
            {
                control.EmptyValue();
            }
        }
        public String GetQueryString()
        {
            var sb = new StringBuilder();
            sb.Append("1=1");
            foreach (ExtFieldControl control in Controls)
            {
                if (String.IsNullOrEmpty(control.FieldName) || control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) continue;
                sb.Append(" and ");
                sb.Append(control.FieldName);
                sb.Append(String.Format(String.IsNullOrEmpty(control.JudgeSymbols) ? " like '%{0}%'" : " " + control.JudgeSymbols, control.Value));
            }
            return sb.ToString();
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
