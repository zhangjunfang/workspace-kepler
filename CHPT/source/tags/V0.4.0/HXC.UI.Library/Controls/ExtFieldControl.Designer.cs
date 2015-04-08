namespace HXC.UI.Library.Controls
{
    partial class ExtFieldControl
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
            this.control_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.key_control = new System.Windows.Forms.Label();
            this.control_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // control_panel
            // 
            this.control_panel.Controls.Add(this.key_control);
            this.control_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.control_panel.Location = new System.Drawing.Point(0, 0);
            this.control_panel.Name = "control_panel";
            this.control_panel.Size = new System.Drawing.Size(210, 23);
            this.control_panel.TabIndex = 0;
            this.control_panel.WrapContents = false;
            // 
            // key_control
            // 
            this.key_control.AutoEllipsis = true;
            this.key_control.Location = new System.Drawing.Point(0, 0);
            this.key_control.Margin = new System.Windows.Forms.Padding(0);
            this.key_control.Name = "key_control";
            this.key_control.Size = new System.Drawing.Size(60, 22);
            this.key_control.TabIndex = 0;
            this.key_control.Text = "字段名称";
            this.key_control.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ExtFieldControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderWidth = 0;
            this.Controls.Add(this.control_panel);
            this.Name = "ExtFieldControl";
            this.Size = new System.Drawing.Size(210, 23);
            this.control_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel control_panel;
        private System.Windows.Forms.Label key_control;
    }
}
