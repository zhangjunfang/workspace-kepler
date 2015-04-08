using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ServiceStationClient.ComponentUI.Panel
{
    /// <summary>
    /// CardPanel
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/19 15:08:48</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    [Designer(typeof(CardPanelDesigner))]
    public partial class CardPanel : UserControl
    {
        #region Constructor -- 构造函数
        public CardPanel()
        {
            InitializeComponent();
            //pnl_content.Paint += new PaintEventHandler(PanelPaint);
        }
        #endregion

        #region Field -- 字段
        #endregion

        #region Property -- 属性
        /// <summary>
        /// 头部图标
        /// </summary>
        [Category("CardPanel")]
        public Image IconImage
        {
            get { return pic_icon.Image; }
            set { pic_icon.Image = value; }
        }
        /// <summary>
        /// 头部文本
        /// </summary>
        [Category("CardPanel")]
        public String HeaderText 
        {
            get { return lbl_header.Text; }
            set { lbl_header.Text = value; } 
        }
        /// <summary>
        /// 内容面板
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PanelEx ContentPanel
        {
            get { return pnl_content; }
        }
        #endregion

        #region Method -- 方法
        //void PanelPaint(object sender, PaintEventArgs e)
        //{
        //    var panel = sender as System.Windows.Forms.Panel;
        //    if (!DesignMode && pnl_content.Controls.Count >= 1) return;
        //    if (panel != null)
        //        TextRenderer.DrawText(
        //            e.Graphics,
        //            panel.Name,
        //            panel.Font,
        //            panel.ClientRectangle,
        //            panel.ForeColor,
        //            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        //}
        
        #endregion

        #region Event -- 事件

        #endregion
    }

    internal class CardPanelDesigner : ParentControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            var control = (CardPanel)component;
            EnableDesignMode(control.ContentPanel, "ContentPanel");
        }
    }
}
