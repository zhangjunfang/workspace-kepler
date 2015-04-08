namespace HXCPcClient.LoginForms
{
    partial class FormMsg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelYes = new System.Windows.Forms.Panel();
            this.labelYes = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.panel4.SuspendLayout();
            this.panelYes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.panelTop.Controls.Add(this.pictureBox4);
            this.panelTop.Controls.Add(this.pbClose);
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(462, 179);
            this.panelTop.TabIndex = 2;            
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImage = global::HXCPcClient.Properties.Resources.top;
            this.pictureBox4.Image = global::HXCPcClient.Properties.Resources.logo;
            this.pictureBox4.Location = new System.Drawing.Point(156, 47);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(150, 90);
            this.pictureBox4.TabIndex = 7;
            this.pictureBox4.TabStop = false;       
            // 
            // pbClose
            // 
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.BackgroundImage = global::HXCPcClient.Properties.Resources.close_n;
            this.pbClose.Location = new System.Drawing.Point(436, 15);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(11, 9);
            this.pbClose.TabIndex = 4;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            this.pbClose.MouseEnter += new System.EventHandler(this.pbClose_MouseEnter);
            this.pbClose.MouseLeave += new System.EventHandler(this.pbClose_MouseLeave);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(225)))), ((int)(((byte)(226)))));
            this.panel4.Controls.Add(this.panelYes);
            this.panel4.Location = new System.Drawing.Point(0, 503);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(462, 69);
            this.panel4.TabIndex = 45;
            // 
            // panelYes
            // 
            this.panelYes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.panelYes.Controls.Add(this.labelYes);
            this.panelYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelYes.Location = new System.Drawing.Point(321, 17);
            this.panelYes.Name = "panelYes";
            this.panelYes.Size = new System.Drawing.Size(102, 35);
            this.panelYes.TabIndex = 4;
            this.panelYes.Click += new System.EventHandler(this.panelYes_Click);
            this.panelYes.MouseEnter += new System.EventHandler(this.panelYes_MouseEnter);
            this.panelYes.MouseLeave += new System.EventHandler(this.panelYes_MouseLeave);
            // 
            // labelYes
            // 
            this.labelYes.AutoSize = true;
            this.labelYes.BackColor = System.Drawing.Color.Transparent;
            this.labelYes.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.labelYes.ForeColor = System.Drawing.Color.White;
            this.labelYes.Location = new System.Drawing.Point(30, 8);
            this.labelYes.Name = "labelYes";
            this.labelYes.Size = new System.Drawing.Size(47, 19);
            this.labelYes.TabIndex = 0;
            this.labelYes.Text = "确  认";
            this.labelYes.Click += new System.EventHandler(this.panelYes_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.label1.Location = new System.Drawing.Point(171, 259);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 77);
            this.label1.TabIndex = 47;
            this.label1.Text = "您输入的密码不正确，原因可能是：\r\n忘记密码；账号输入有误；\r\n未区分字母大小写；未开启小键盘。";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::HXCPcClient.Properties.Resources.info;
            this.pictureBox1.Location = new System.Drawing.Point(90, 267);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 52);
            this.pictureBox1.TabIndex = 46;
            this.pictureBox1.TabStop = false;
            // 
            // FormMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 572);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMsg";
            this.Text = "FormMsg";
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panelYes.ResumeLayout(false);
            this.panelYes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelYes;
        private System.Windows.Forms.Label labelYes;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}