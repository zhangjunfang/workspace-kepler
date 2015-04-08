namespace HXCServerWinForm.LoginForms
{
    partial class FormLogin
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelSet = new System.Windows.Forms.Label();
            this.pbSet = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pbMin = new System.Windows.Forms.PictureBox();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.labelRemember = new System.Windows.Forms.Label();
            this.labelPwd = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.pbChecked = new System.Windows.Forms.PictureBox();
            this.panelPwd = new System.Windows.Forms.Panel();
            this.tbPwd = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.panelUser = new System.Windows.Forms.Panel();
            this.pbCmb = new System.Windows.Forms.PictureBox();
            this.tbUser = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.panelLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbChecked)).BeginInit();
            this.panelPwd.SuspendLayout();
            this.panelUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCmb)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(194)))), ((int)(((byte)(159)))));
            this.panelTop.Controls.Add(this.labelSet);
            this.panelTop.Controls.Add(this.pbSet);
            this.panelTop.Controls.Add(this.pictureBox4);
            this.panelTop.Controls.Add(this.pbMin);
            this.panelTop.Controls.Add(this.pbClose);
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(462, 254);
            this.panelTop.TabIndex = 0;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            // 
            // labelSet
            // 
            this.labelSet.AutoSize = true;
            this.labelSet.BackColor = System.Drawing.Color.Transparent;
            this.labelSet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSet.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.labelSet.Location = new System.Drawing.Point(33, 12);
            this.labelSet.Name = "labelSet";
            this.labelSet.Size = new System.Drawing.Size(56, 17);
            this.labelSet.TabIndex = 9;
            this.labelSet.Text = "登录设置";
            this.labelSet.Click += new System.EventHandler(this.labelSet_Click);
            // 
            // pbSet
            // 
            this.pbSet.BackColor = System.Drawing.Color.Transparent;
            this.pbSet.BackgroundImage = global::HXCServerWinForm.Properties.Resources.set;
            this.pbSet.Location = new System.Drawing.Point(15, 15);
            this.pbSet.Name = "pbSet";
            this.pbSet.Size = new System.Drawing.Size(12, 12);
            this.pbSet.TabIndex = 8;
            this.pbSet.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImage = global::HXCServerWinForm.Properties.Resources.top;
            this.pictureBox4.Location = new System.Drawing.Point(21, 49);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(420, 182);
            this.pictureBox4.TabIndex = 7;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            // 
            // pbMin
            // 
            this.pbMin.BackColor = System.Drawing.Color.Transparent;
            this.pbMin.BackgroundImage = global::HXCServerWinForm.Properties.Resources.small_n;
            this.pbMin.Location = new System.Drawing.Point(415, 15);
            this.pbMin.Name = "pbMin";
            this.pbMin.Size = new System.Drawing.Size(11, 9);
            this.pbMin.TabIndex = 5;
            this.pbMin.TabStop = false;
            this.pbMin.Click += new System.EventHandler(this.pbMin_Click);
            this.pbMin.MouseEnter += new System.EventHandler(this.pbMin_MouseEnter);
            this.pbMin.MouseLeave += new System.EventHandler(this.pbMin_MouseLeave);
            // 
            // pbClose
            // 
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.BackgroundImage = global::HXCServerWinForm.Properties.Resources.close_n;
            this.pbClose.Location = new System.Drawing.Point(436, 15);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(11, 9);
            this.pbClose.TabIndex = 4;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            this.pbClose.MouseEnter += new System.EventHandler(this.pbClose_MouseEnter);
            this.pbClose.MouseLeave += new System.EventHandler(this.pbClose_MouseLeave);
            // 
            // labelRemember
            // 
            this.labelRemember.AutoSize = true;
            this.labelRemember.BackColor = System.Drawing.Color.Transparent;
            this.labelRemember.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelRemember.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(194)))), ((int)(((byte)(159)))));
            this.labelRemember.Location = new System.Drawing.Point(137, 413);
            this.labelRemember.Name = "labelRemember";
            this.labelRemember.Size = new System.Drawing.Size(107, 20);
            this.labelRemember.TabIndex = 6;
            this.labelRemember.Text = "记住用户名密码";
            // 
            // labelPwd
            // 
            this.labelPwd.AutoSize = true;
            this.labelPwd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelPwd.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPwd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.labelPwd.Location = new System.Drawing.Point(287, 413);
            this.labelPwd.Name = "labelPwd";
            this.labelPwd.Size = new System.Drawing.Size(65, 20);
            this.labelPwd.TabIndex = 7;
            this.labelPwd.Text = "忘记密码";
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.BackColor = System.Drawing.Color.Transparent;
            this.labelLogin.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.labelLogin.ForeColor = System.Drawing.Color.White;
            this.labelLogin.Location = new System.Drawing.Point(87, 2);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(71, 30);
            this.labelLogin.TabIndex = 0;
            this.labelLogin.Text = "登  录";
            this.labelLogin.Click += new System.EventHandler(this.panelLogin_Click);
            this.labelLogin.MouseEnter += new System.EventHandler(this.panelLogin_MouseEnter);
            this.labelLogin.MouseLeave += new System.EventHandler(this.panelLogin_MouseLeave);
            // 
            // panelLogin
            // 
            this.panelLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(194)))), ((int)(((byte)(159)))));
            this.panelLogin.Controls.Add(this.labelLogin);
            this.panelLogin.Location = new System.Drawing.Point(111, 456);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(239, 38);
            this.panelLogin.TabIndex = 3;
            this.panelLogin.Click += new System.EventHandler(this.panelLogin_Click);
            this.panelLogin.MouseEnter += new System.EventHandler(this.panelLogin_MouseEnter);
            this.panelLogin.MouseLeave += new System.EventHandler(this.panelLogin_MouseLeave);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
            // 
            // pbChecked
            // 
            this.pbChecked.BackColor = System.Drawing.Color.Transparent;
            this.pbChecked.BackgroundImage = global::HXCServerWinForm.Properties.Resources.select_n;
            this.pbChecked.Location = new System.Drawing.Point(115, 416);
            this.pbChecked.Name = "pbChecked";
            this.pbChecked.Size = new System.Drawing.Size(16, 16);
            this.pbChecked.TabIndex = 5;
            this.pbChecked.TabStop = false;
            this.pbChecked.Click += new System.EventHandler(this.pbChecked_Click);
            // 
            // panelPwd
            // 
            this.panelPwd.BackColor = System.Drawing.Color.Transparent;
            this.panelPwd.BackgroundImage = global::HXCServerWinForm.Properties.Resources.password;
            this.panelPwd.Controls.Add(this.tbPwd);
            this.panelPwd.Location = new System.Drawing.Point(111, 350);
            this.panelPwd.Name = "panelPwd";
            this.panelPwd.Size = new System.Drawing.Size(239, 40);
            this.panelPwd.TabIndex = 4;
            // 
            // tbPwd
            // 
            this.tbPwd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPwd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbPwd.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbPwd.BackColor = System.Drawing.Color.Transparent;
            this.tbPwd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbPwd.BorderColor = System.Drawing.Color.White;
            this.tbPwd.Caption = "密码";
            this.tbPwd.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tbPwd.ForeImage = null;
            this.tbPwd.InputtingVerifyCondition = null;
            this.tbPwd.Location = new System.Drawing.Point(39, 4);
            this.tbPwd.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbPwd.MaxLengh = 32767;
            this.tbPwd.Multiline = false;
            this.tbPwd.Name = "tbPwd";
            this.tbPwd.Radius = 0;
            this.tbPwd.ReadOnly = false;
            this.tbPwd.ShadowColor = System.Drawing.Color.White;
            this.tbPwd.ShowError = false;
            this.tbPwd.Size = new System.Drawing.Size(171, 31);
            this.tbPwd.TabIndex = 2;
            this.tbPwd.UseSystemPasswordChar = false;
            this.tbPwd.Value = "密码";
            this.tbPwd.VerifyCondition = null;
            this.tbPwd.VerifyType = null;
            this.tbPwd.VerifyTypeName = null;
            this.tbPwd.WaterMark = "密码";
            this.tbPwd.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // panelUser
            // 
            this.panelUser.BackColor = System.Drawing.Color.Transparent;
            this.panelUser.BackgroundImage = global::HXCServerWinForm.Properties.Resources.user;
            this.panelUser.Controls.Add(this.pbCmb);
            this.panelUser.Controls.Add(this.tbUser);
            this.panelUser.Location = new System.Drawing.Point(111, 291);
            this.panelUser.Name = "panelUser";
            this.panelUser.Size = new System.Drawing.Size(239, 40);
            this.panelUser.TabIndex = 3;
            // 
            // pbCmb
            // 
            this.pbCmb.BackColor = System.Drawing.Color.Transparent;
            this.pbCmb.BackgroundImage = global::HXCServerWinForm.Properties.Resources.down_n;
            this.pbCmb.Location = new System.Drawing.Point(214, 16);
            this.pbCmb.Name = "pbCmb";
            this.pbCmb.Size = new System.Drawing.Size(12, 10);
            this.pbCmb.TabIndex = 3;
            this.pbCmb.TabStop = false;
            this.pbCmb.Click += new System.EventHandler(this.pbCmb_Click);
            this.pbCmb.MouseEnter += new System.EventHandler(this.pbCmb_MouseEnter);
            this.pbCmb.MouseLeave += new System.EventHandler(this.pbCmb_MouseLeave);
            // 
            // tbUser
            // 
            this.tbUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbUser.BackColor = System.Drawing.Color.Transparent;
            this.tbUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbUser.BorderColor = System.Drawing.Color.White;
            this.tbUser.Caption = "用户名";
            this.tbUser.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tbUser.ForeImage = null;
            this.tbUser.InputtingVerifyCondition = null;
            this.tbUser.Location = new System.Drawing.Point(39, 4);
            this.tbUser.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbUser.MaxLengh = 32767;
            this.tbUser.Multiline = false;
            this.tbUser.Name = "tbUser";
            this.tbUser.Radius = 0;
            this.tbUser.ReadOnly = false;
            this.tbUser.ShadowColor = System.Drawing.Color.White;
            this.tbUser.ShowError = false;
            this.tbUser.Size = new System.Drawing.Size(171, 31);
            this.tbUser.TabIndex = 1;
            this.tbUser.UseSystemPasswordChar = false;
            this.tbUser.Value = "用户名";
            this.tbUser.VerifyCondition = null;
            this.tbUser.VerifyType = null;
            this.tbUser.VerifyTypeName = null;
            this.tbUser.WaterMark = "用户名";
            this.tbUser.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(462, 535);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.labelPwd);
            this.Controls.Add(this.labelRemember);
            this.Controls.Add(this.pbChecked);
            this.Controls.Add(this.panelPwd);
            this.Controls.Add(this.panelUser);
            this.Controls.Add(this.panelTop);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "慧修车服务端登陆";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbChecked)).EndInit();
            this.panelPwd.ResumeLayout(false);
            this.panelUser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCmb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pbMin;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Panel panelUser;
        private System.Windows.Forms.Panel panelPwd;
        private System.Windows.Forms.PictureBox pbChecked;
        private System.Windows.Forms.Label labelRemember;
        private System.Windows.Forms.Label labelPwd;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Panel panelLogin;
        private ServiceStationClient.ComponentUI.TextBoxEx tbUser;
        private ServiceStationClient.ComponentUI.TextBoxEx tbPwd;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.PictureBox pbCmb;
        private System.Windows.Forms.Label labelSet;
        private System.Windows.Forms.PictureBox pbSet;
    }
}