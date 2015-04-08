using System.ComponentModel;
using System.Windows.Forms;

namespace ServiceStationClient.ComponentUI.Controls
{
    /// <summary>
    /// ExtFieldPanel
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/14 11:26:19</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public class ExtFieldPanel : FlowLayoutPanel
    {
        #region Constructor -- 构造函数

        #endregion

        #region Field -- 字段
        
        #endregion

        #region Property -- 属性
        private int _itemWidth;
        /// <summary>
        /// 子控件宽度
        /// </summary>
        [Browsable(true)]
        [Category("Extend")]
        [Description("子控件宽度")]
        public int ItemWidth
        {
            get { return _itemWidth; }
            set
            {
                if (Equals(_itemWidth, value)) return;
                _itemWidth = value;
                SetItemSize();
            }
        }

        private int _itemHeight;
        /// <summary>
        /// 子控件高度
        /// </summary>
        [Browsable(true)]
        [Category("Extend")]
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
        #endregion

        #region Method -- 方法

        private void SetItemSize()
        {
            
        }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
        }
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
