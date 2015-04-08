using HXC.UI.Library.Controls;

namespace HXC.UI.Library.ExtendControl
{
    partial class ExtSearchPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_query = new HXC.UI.Library.Controls.ExtButton();
            this.btn_clear = new HXC.UI.Library.Controls.ExtButton();
            this.pnl_field_panel = new HXC.UI.Library.Controls.ExtFieldPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btn_query);
            this.panel1.Controls.Add(this.btn_clear);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(607, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(93, 100);
            this.panel1.TabIndex = 1;
            // 
            // btn_query
            // 
            this.btn_query.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_query.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.btn_query.BackgroundImage = global::HXC.UI.Library.Properties.Resources.search_normal;
            this.btn_query.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_query.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.btn_query.BorderWidth = 0;
            this.btn_query.Content = null;
            this.btn_query.ContentTypeName = null;
            this.btn_query.ContentTypeParameter = null;
            this.btn_query.CornerRadiu = 0;
            this.btn_query.DisplayValue = "";
            this.btn_query.InputtingVerifyCondition = null;
            this.btn_query.LightBackgroudImage = global::HXC.UI.Library.Properties.Resources.Search_light;
            this.btn_query.Location = new System.Drawing.Point(8, 55);
            this.btn_query.Name = "btn_query";
            this.btn_query.NormalBackgroudImage = global::HXC.UI.Library.Properties.Resources.search_normal;
            this.btn_query.ShowError = false;
            this.btn_query.Size = new System.Drawing.Size(80, 24);
            this.btn_query.TabIndex = 0;
            this.btn_query.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_query.Value = "";
            this.btn_query.VerifyCondition = null;
            this.btn_query.VerifyType = null;
            this.btn_query.VerifyTypeName = null;
            // 
            // btn_clear
            // 
            this.btn_clear.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_clear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.btn_clear.BackgroundImage = global::HXC.UI.Library.Properties.Resources.clear_normal;
            this.btn_clear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_clear.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.btn_clear.BorderWidth = 0;
            this.btn_clear.Content = null;
            this.btn_clear.ContentTypeName = null;
            this.btn_clear.ContentTypeParameter = null;
            this.btn_clear.CornerRadiu = 0;
            this.btn_clear.DisplayValue = "";
            this.btn_clear.InputtingVerifyCondition = null;
            this.btn_clear.LightBackgroudImage = global::HXC.UI.Library.Properties.Resources.clear_light;
            this.btn_clear.Location = new System.Drawing.Point(8, 22);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.NormalBackgroudImage = global::HXC.UI.Library.Properties.Resources.clear_normal;
            this.btn_clear.ShowError = false;
            this.btn_clear.Size = new System.Drawing.Size(80, 24);
            this.btn_clear.TabIndex = 0;
            this.btn_clear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_clear.Value = "";
            this.btn_clear.VerifyCondition = null;
            this.btn_clear.VerifyType = null;
            this.btn_clear.VerifyTypeName = null;
            // 
            // pnl_field_panel
            // 
            this.pnl_field_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_field_panel.AutoPlacement = true;
            this.pnl_field_panel.BackColor = System.Drawing.Color.Transparent;
            this.pnl_field_panel.ItemHeight = 23;
            this.pnl_field_panel.ItemMargin = new System.Windows.Forms.Padding(5);
            this.pnl_field_panel.ItemValueWidth = 150;
            this.pnl_field_panel.Location = new System.Drawing.Point(0, 0);
            this.pnl_field_panel.Name = "pnl_field_panel";
            this.pnl_field_panel.Size = new System.Drawing.Size(608, 100);
            this.pnl_field_panel.TabIndex = 2;
            // 
            // ExtSearchPanel
            // 
            this.Controls.Add(this.pnl_field_panel);
            this.Controls.Add(this.panel1);
            this.Name = "ExtSearchPanel";
            this.Size = new System.Drawing.Size(700, 100);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public Controls.ExtButton btn_query;
        public Controls.ExtButton btn_clear;
        private ExtFieldPanel pnl_field_panel;
    }
}
