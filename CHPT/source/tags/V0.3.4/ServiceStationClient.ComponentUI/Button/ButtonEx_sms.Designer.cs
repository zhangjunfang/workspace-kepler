namespace ServiceStationClient.ComponentUI
{
    partial class ButtonEx_sms
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
            this.pic_box = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_box)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Button
            // 
            this.label_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Button.BackColor = System.Drawing.Color.Transparent;
            this.label_Button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(157)))));
            this.label_Button.Location = new System.Drawing.Point(19, 0);
            this.label_Button.Name = "label_Button";
            this.label_Button.Size = new System.Drawing.Size(33, 24);
            this.label_Button.TabIndex = 0;
            this.label_Button.Text = "btn1";
            this.label_Button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_Button.Click += new System.EventHandler(this.label_Button_Click);
            this.label_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_Button_MouseDown);
            this.label_Button.MouseEnter += new System.EventHandler(this.label_Button_MouseEnter);
            this.label_Button.MouseLeave += new System.EventHandler(this.label_Button_MouseLeave);
            this.label_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label_Button_MouseUp);
            // 
            // pic_box
            // 
            this.pic_box.BackColor = System.Drawing.Color.Transparent;
            this.pic_box.Location = new System.Drawing.Point(4, 4);
            this.pic_box.Name = "pic_box";
            this.pic_box.Size = new System.Drawing.Size(16, 17);
            this.pic_box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pic_box.TabIndex = 1;
            this.pic_box.TabStop = false;
            this.pic_box.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pic_box.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pic_box.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.pic_box.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.pic_box.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // ButtonEx_sms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pic_box);
            this.Controls.Add(this.label_Button);
            this.Margin = new System.Windows.Forms.Padding(1, 1, 0, 0);
            this.Name = "ButtonEx_sms";
            this.Size = new System.Drawing.Size(52, 24);
            this.Load += new System.EventHandler(this.ButtonEx_sms_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_box)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_Button;
        private System.Windows.Forms.PictureBox pic_box;
    }
}
