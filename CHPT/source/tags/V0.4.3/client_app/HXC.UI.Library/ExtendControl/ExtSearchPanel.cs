using System;
using System.ComponentModel;
using System.Windows.Forms.Design;
using HXC.UI.Library.Controls;

namespace HXC.UI.Library.ExtendControl
{
    /// <summary>
    /// 数据字段查询面板
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/16 14:32:29</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    [Designer(typeof(ExtSearchDesigner))]
    public partial class ExtSearchPanel : ExtUserControl
    {
        #region Constructor -- 构造函数
        public ExtSearchPanel()
        {
            InitializeComponent();

            InitEvent();
        }
        #endregion

        #region Field -- 字段

        #endregion

        #region Property -- 属性
        /// <summary>
        /// 内容面板
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ExtFieldPanel ContentPanel
        {
            get { return pnl_field_panel; }
        }

        /// <summary>
        /// 查询字符串
        /// </summary>
        public String QueryString
        {
            get { return pnl_field_panel.GetQueryString(); }
        }

        /// <summary>
        /// 查询按钮点击事件
        /// </summary>
        [Browsable(true)]
        [Category("Extensions")]
        [Description("查询按钮点击事件")]
        public event EventHandler QueryClick;
        #endregion

        #region Method -- 方法
        private void InitEvent()
        {
            btn_clear.Click += btn_clear_Click;
            btn_query.Click += btn_query_Click;
        }
        #endregion

        #region Event -- 事件
        private void btn_clear_Click(object sender, EventArgs e)
        {
            pnl_field_panel.ClearItemValue();
        }
        private void btn_query_Click(object sender, EventArgs e)
        {
            var handle = QueryClick;
            if (handle != null)
            {
                handle(this, e);
            }
        }
        #endregion
    }

    internal class ExtSearchDesigner : ParentControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            var control = (ExtSearchPanel)component;
            EnableDesignMode(control.ContentPanel, "ContentPanel");
        }
    }
}
