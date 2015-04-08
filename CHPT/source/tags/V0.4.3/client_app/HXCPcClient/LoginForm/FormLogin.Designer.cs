namespace HXCPcClient.LoginForms
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
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.labelSet = new System.Windows.Forms.Label();
            this.pbMin = new System.Windows.Forms.PictureBox();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.pbSet = new System.Windows.Forms.PictureBox();
            this.labelRemember = new System.Windows.Forms.Label();
            this.labelPwd = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.pbChecked = new System.Windows.Forms.PictureBox();
            this.panelPwd = new System.Windows.Forms.Panel();
            this.tbPwd = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.panelUser = new System.Windows.Forms.Panel();
            this.tbUser = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.panelDb = new System.Windows.Forms.Panel();
            this.tbDb = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.pbCmb = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSet)).BeginInit();
            this.panelLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbChecked)).BeginInit();
            this.panelPwd.SuspendLayout();
            this.panelUser.SuspendLayout();
            this.panelDb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCmb)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.panelTop.Controls.Add(this.pictureBox4);
            this.panelTop.Controls.Add(this.labelSet);
            this.panelTop.Controls.Add(this.pbMin);
            this.panelTop.Controls.Add(this.pbClose);
            this.panelTop.Controls.Add(this.pbSet);
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(462, 254);
            this.panelTop.TabIndex = 0;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImage = global::HXCPcClient.Properties.Resources.top;
            this.pictureBox4.Location = new System.Drawing.Point(21, 49);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(420, 182);
            this.pictureBox4.TabIndex = 7;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
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
            this.labelSet.TabIndex = 6;
            this.labelSet.Text = "登录设置";
            this.labelSet.Click += new System.EventHandler(this.labelSet_Click);
            // 
            // pbMin
            // 
            this.pbMin.BackColor = System.Drawing.Color.Transparent;
            this.pbMin.BackgroundImage = global::HXCPcClient.Properties.Resources.small_n;
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
            // pbSet
            // 
            this.pbSet.BackColor = System.Drawing.Color.Transparent;
            this.pbSet.BackgroundImage = global::HXCPcClient.Properties.Resources.set1;
            this.pbSet.Location = new System.Drawing.Point(15, 15);
            this.pbSet.Name = "pbSet";
            this.pbSet.Size = new System.Drawing.Size(12, 12);
            this.pbSet.TabIndex = 3;
            this.pbSet.TabStop = false;
            // 
            // labelRemember
            // 
            this.labelRemember.AutoSize = true;
            this.labelRemember.BackColor = System.Drawing.Color.Transparent;
            this.labelRemember.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelRemember.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.labelRemember.Location = new System.Drawing.Point(137, 462);
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
            this.labelPwd.Location = new System.Drawing.Point(287, 462);
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
            this.panelLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(154)))), ((int)(((byte)(243)))));
            this.panelLogin.Controls.Add(this.labelLogin);
            this.panelLogin.Location = new System.Drawing.Point(111, 505);
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
            this.pbChecked.BackgroundImage = global::HXCPcClient.Properties.Resources.select_n;
            this.pbChecked.Location = new System.Drawing.Point(115, 465);
            this.pbChecked.Name = "pbChecked";
            this.pbChecked.Size = new System.Drawing.Size(16, 16);
            this.pbChecked.TabIndex = 5;
            this.pbChecked.TabStop = false;
            this.pbChecked.Click += new System.EventHandler(this.pbChecked_Click);
            // 
            // panelPwd
            // 
            this.panelPwd.BackColor = System.Drawing.Color.Transparent;
            this.panelPwd.BackgroundImage = global::HXCPcClient.Properties.Resources.password;
            this.panelPwd.Controls.Add(this.tbPwd);
            this.panelPwd.Location = new System.Drawing.Point(111, 399);
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
            this.panelUser.BackgroundImage = global::HXCPcClient.Properties.Resources.user;
            this.panelUser.Controls.Add(this.tbUser);
            this.panelUser.Location = new System.Drawing.Point(111, 340);
            this.panelUser.Name = "panelUser";
            this.panelUser.Size = new System.Drawing.Size(239, 40);
            this.panelUser.TabIndex = 3;
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
            // panelDb
            // 
            this.panelDb.BackColor = System.Drawing.Color.Transparent;
            this.panelDb.BackgroundImage = global::HXCPcClient.Properties.Resources.books;
            this.panelDb.Controls.Add(this.tbDb);
            this.panelDb.Controls.Add(this.pbCmb);
            this.panelDb.Location = new System.Drawing.Point(111, 281);
            this.panelDb.Name = "panelDb";
            this.panelDb.Size = new System.Drawing.Size(239, 40);
            this.panelDb.TabIndex = 2;
            // 
            // tbDb
            // 
            this.tbDb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbDb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbDb.BackColor = System.Drawing.Color.Transparent;
            this.tbDb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbDb.BorderColor = System.Drawing.Color.White;
            this.tbDb.Caption = "未加载账套";
            this.tbDb.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tbDb.ForeImage = null;
            this.tbDb.InputtingVerifyCondition = null;
            this.tbDb.Location = new System.Drawing.Point(39, 3);
            this.tbDb.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbDb.MaxLengh = 32767;
            this.tbDb.Multiline = false;
            this.tbDb.Name = "tbDb";
            this.tbDb.Radius = 0;
            this.tbDb.ReadOnly = false;
            this.tbDb.ShadowColor = System.Drawing.Color.White;
            this.tbDb.ShowError = false;
            this.tbDb.Size = new System.Drawing.Size(171, 31);
            this.tbDb.TabIndex = 0;
            this.tbDb.UseSystemPasswordChar = false;
            this.tbDb.Value = "未加载账套";
            this.tbDb.VerifyCondition = null;
            this.tbDb.VerifyType = null;
            this.tbDb.VerifyTypeName = null;
            this.tbDb.WaterMark = "未加载账套";
            this.tbDb.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // pbCmb
            // 
            this.pbCmb.BackColor = System.Drawing.Color.Transparent;
            this.pbCmb.BackgroundImage = global::HXCPcClient.Properties.Resources.down_n;
            this.pbCmb.Location = new System.Drawing.Point(214, 16);
            this.pbCmb.Name = "pbCmb";
            this.pbCmb.Size = new System.Drawing.Size(12, 10);
            this.pbCmb.TabIndex = 2;
            this.pbCmb.TabStop = false;
            this.pbCmb.Click += new System.EventHandler(this.pbCmb_Click);
            this.pbCmb.MouseEnter += new System.EventHandler(this.pbCmb_MouseEnter);
            this.pbCmb.MouseLeave += new System.EventHandler(this.pbCmb_MouseLeave);
            // 
            // timer
            // 
            this.timer.Interval = 5000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(462, 572);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.labelPwd);
            this.Controls.Add(this.labelRemember);
            this.Controls.Add(this.pbChecked);
            this.Controls.Add(this.panelPwd);
            this.Controls.Add(this.panelUser);
            this.Controls.Add(this.panelDb);
            this.Controls.Add(this.panelTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "慧修车客户端登陆";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSet)).EndInit();
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbChecked)).EndInit();
            this.panelPwd.ResumeLayout(false);
            this.panelUser.ResumeLayout(false);
            this.panelDb.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCmb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pbSet;
        private System.Windows.Forms.PictureBox pbMin;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Label labelSet;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Panel panelDb;
        private System.Windows.Forms.Panel panelUser;
        private System.Windows.Forms.Panel panelPwd;
        private System.Windows.Forms.PictureBox pbCmb;
        private System.Windows.Forms.PictureBox pbChecked;
        private System.Windows.Forms.Label labelRemember;
        private System.Windows.Forms.Label labelPwd;
        private ServiceStationClient.ComponentUI.TextBoxEx tbDb;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Panel panelLogin;
        private ServiceStationClient.ComponentUI.TextBoxEx tbUser;
        private ServiceStationClient.ComponentUI.TextBoxEx tbPwd;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Timer timer;
    }
}