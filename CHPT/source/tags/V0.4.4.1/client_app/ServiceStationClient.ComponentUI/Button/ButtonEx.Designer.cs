namespace ServiceStationClient.ComponentUI
{
    partial class ButtonEx
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
            this.label_Button = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Button
            // 
            this.label_Button.BackColor = System.Drawing.Color.Transparent;
            this.label_Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Button.Location = new System.Drawing.Point(0, 0);
            this.label_Button.Name = "label_Button";
            this.label_Button.Size = new System.Drawing.Size(60, 26);
            this.label_Button.TabIndex = 0;
            this.label_Button.Text = "btn1";
            this.label_Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_Button.Click += new System.EventHandler(this.label_Button_Click);
            this.label_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_Button_MouseDown);
            this.label_Button.MouseEnter += new System.EventHandler(this.label_Button_MouseEnter);
            this.label_Button.MouseLeave += new System.EventHandler(this.label_Button_MouseLeave);
            this.label_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label_Button_MouseUp);
            // 
            // ZJXLbutton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.label_Button);
            this.Name = "ZJXLbutton";
            this.Size = new System.Drawing.Size(60, 26);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_Button;
    }
}
