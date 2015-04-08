namespace HXCPcClient.HomeManage
{
    partial class UCAnnounceItem
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
            this.labelContent = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
           
            // 
            // labelContent
            // 
            this.labelContent.AutoSize = true;
            this.labelContent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelContent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.labelContent.Location = new System.Drawing.Point(10, 10);
            this.labelContent.Name = "labelContent";
            this.labelContent.Size = new System.Drawing.Size(95, 12);
            this.labelContent.TabIndex = 0;
            this.labelContent.Text = "[系统公告] 内容";
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.AutoSize = true;
            this.labelTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.labelTime.Location = new System.Drawing.Point(199, 10);
            this.labelTime.Name = "labelTime";
            this.labelTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelTime.Size = new System.Drawing.Size(65, 12);
            this.labelTime.TabIndex = 1;
            this.labelTime.Text = "2013-11-26";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UCAnnounceItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.labelContent);
            this.Name = "UCAnnounceItem";
            this.Size = new System.Drawing.Size(273, 33);
            this.Load += new System.EventHandler(this.UCAnnounceItem_Load);
            this.Click += new System.EventHandler(this.UCAnnounceItem_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UCAnnounceItem_Paint);
            this.MouseEnter += new System.EventHandler(this.UCAnnounceItem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.UCAnnounceItem_MouseLeave);
            this.Controls.SetChildIndex(this.labelContent, 0);
            this.Controls.SetChildIndex(this.labelTime, 0);          
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelContent;
        private System.Windows.Forms.Label labelTime;
    }
}
