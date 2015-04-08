namespace HXCPcClient.UCForm.SysManage.ReminderSet
{
    partial class UCReminderType
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
            this.cbSound = new System.Windows.Forms.CheckBox();
            this.cbBubble = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbSound
            // 
            this.cbSound.AutoSize = true;
            this.cbSound.Location = new System.Drawing.Point(95, 9);
            this.cbSound.Name = "cbSound";
            this.cbSound.Size = new System.Drawing.Size(48, 16);
            this.cbSound.TabIndex = 39;
            this.cbSound.Text = "语音";
            this.cbSound.UseVisualStyleBackColor = true;
            // 
            // cbBubble
            // 
            this.cbBubble.AutoSize = true;
            this.cbBubble.Location = new System.Drawing.Point(11, 9);
            this.cbBubble.Name = "cbBubble";
            this.cbBubble.Size = new System.Drawing.Size(72, 16);
            this.cbBubble.TabIndex = 38;
            this.cbBubble.Text = "气泡弹窗";
            this.cbBubble.UseVisualStyleBackColor = true;
            // 
            // UCReminderType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cbSound);
            this.Controls.Add(this.cbBubble);
            this.Name = "UCReminderType";
            this.Size = new System.Drawing.Size(151, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbSound;
        private System.Windows.Forms.CheckBox cbBubble;
    }
}
