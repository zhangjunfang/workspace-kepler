namespace HXCPcClient.HomeManage
{
    partial class UCReminder
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.pbSet = new System.Windows.Forms.PictureBox();
            this.labelText = new System.Windows.Forms.Label();
            this.picImg = new System.Windows.Forms.PictureBox();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImg)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackgroundImage = global::HXCPcClient.Properties.Resources.title1;
            this.panel3.Controls.Add(this.pbSet);
            this.panel3.Controls.Add(this.labelText);
            this.panel3.Controls.Add(this.picImg);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(320, 26);
            this.panel3.TabIndex = 2;
            // 
            // pbSet
            // 
            this.pbSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSet.BackColor = System.Drawing.Color.Transparent;
            this.pbSet.BackgroundImage = global::HXCPcClient.Properties.Resources.set;
            this.pbSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbSet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSet.Location = new System.Drawing.Point(293, 4);
            this.pbSet.Name = "pbSet";
            this.pbSet.Size = new System.Drawing.Size(19, 19);
            this.pbSet.TabIndex = 5;
            this.pbSet.TabStop = false;
            this.pbSet.Click += new System.EventHandler(this.pbSet_Click);
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.BackColor = System.Drawing.Color.Transparent;
            this.labelText.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.labelText.Location = new System.Drawing.Point(30, 6);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(37, 14);
            this.labelText.TabIndex = 4;
            this.labelText.Text = "提醒";
            // 
            // picImg
            // 
            this.picImg.BackColor = System.Drawing.Color.Transparent;
            this.picImg.BackgroundImage = global::HXCPcClient.Properties.Resources.reminder;
            this.picImg.Location = new System.Drawing.Point(9, 5);
            this.picImg.Name = "picImg";
            this.picImg.Size = new System.Drawing.Size(16, 16);
            this.picImg.TabIndex = 2;
            this.picImg.TabStop = false;
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.BackColor = System.Drawing.Color.Transparent;
            this.panelContent.Location = new System.Drawing.Point(0, 26);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(320, 334);
            this.panelContent.TabIndex = 5;
            // 
            // UCReminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panelContent);
            this.DoubleBuffered = true;
            this.Name = "UCReminder";
            this.Size = new System.Drawing.Size(320, 360);
            this.Load += new System.EventHandler(this.UCReminder_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox picImg;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.PictureBox pbSet;
        private System.Windows.Forms.Panel panelContent;
    }
}
