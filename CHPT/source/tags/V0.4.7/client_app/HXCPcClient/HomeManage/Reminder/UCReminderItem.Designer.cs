namespace HXCPcClient.HomeManage
{
    partial class UCReminderItem
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
            this.labelCount = new System.Windows.Forms.Label();
            this.labelDot = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelContent
            // 
            this.labelContent.AutoSize = true;
            this.labelContent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.labelContent.Location = new System.Drawing.Point(26, 11);
            this.labelContent.Name = "labelContent";
            this.labelContent.Size = new System.Drawing.Size(29, 12);
            this.labelContent.TabIndex = 3;
            this.labelContent.Text = "内容";
            this.labelContent.Click += new System.EventHandler(this.UCReminderItem_Click);
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.ForeColor = System.Drawing.Color.Red;
            this.labelCount.Location = new System.Drawing.Point(71, 11);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(17, 12);
            this.labelCount.TabIndex = 4;
            this.labelCount.Text = "条";
            this.labelCount.Click += new System.EventHandler(this.UCReminderItem_Click);
            // 
            // labelDot
            // 
            this.labelDot.AutoSize = true;
            this.labelDot.Font = new System.Drawing.Font("宋体", 5F);
            this.labelDot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.labelDot.Location = new System.Drawing.Point(15, 14);
            this.labelDot.Name = "labelDot";
            this.labelDot.Size = new System.Drawing.Size(11, 7);
            this.labelDot.TabIndex = 5;
            this.labelDot.Text = "■";
            this.labelDot.Click += new System.EventHandler(this.UCReminderItem_Click);
            // 
            // UCReminderItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.labelContent);
            this.Controls.Add(this.labelDot);
            this.Controls.Add(this.labelCount);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "UCReminderItem";
            this.Size = new System.Drawing.Size(275, 35);
            this.Load += new System.EventHandler(this.UCReminderItem_Load);
            this.Click += new System.EventHandler(this.UCReminderItem_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UCReminderItem_Paint);
            this.MouseEnter += new System.EventHandler(this.UCReminderItem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.UCReminderItem_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelContent;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Label labelDot;
    }
}
