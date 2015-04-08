namespace HXCServerWinForm.LoginForms
{
    partial class FormSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSet));
            this.labelYes = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pbMin = new System.Windows.Forms.PictureBox();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.panelCancel = new System.Windows.Forms.Panel();
            this.labelCancal = new System.Windows.Forms.Label();
            this.panelYes = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbDBPwd = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tbDBName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbDBUser = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.panelDb = new System.Windows.Forms.Panel();
            this.tbDBIp = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.panelCancel.SuspendLayout();
            this.panelYes.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelDb.SuspendLayout();
            this.SuspendLayout();
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
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(194)))), ((int)(((byte)(159)))));
            this.panelTop.Controls.Add(this.pictureBox4);
            this.panelTop.Controls.Add(this.pbMin);
            this.panelTop.Controls.Add(this.pbClose);
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(462, 164);
            this.panelTop.TabIndex = 45;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = global::HXCServerWinForm.Properties.Resources.logo;
            this.pictureBox4.Location = new System.Drawing.Point(156, 46);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(150, 90);
            this.pictureBox4.TabIndex = 7;
            this.pictureBox4.TabStop = false;
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
            // panelCancel
            // 
            this.panelCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(187)))), ((int)(((byte)(202)))));
            this.panelCancel.Controls.Add(this.labelCancal);
            this.panelCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelCancel.Location = new System.Drawing.Point(324, 16);
            this.panelCancel.Name = "panelCancel";
            this.panelCancel.Size = new System.Drawing.Size(102, 35);
            this.panelCancel.TabIndex = 5;
            this.panelCancel.Click += new System.EventHandler(this.panelCancel_Click);
            // 
            // labelCancal
            // 
            this.labelCancal.AutoSize = true;
            this.labelCancal.BackColor = System.Drawing.Color.Transparent;
            this.labelCancal.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.labelCancal.ForeColor = System.Drawing.Color.White;
            this.labelCancal.Location = new System.Drawing.Point(30, 8);
            this.labelCancal.Name = "labelCancal";
            this.labelCancal.Size = new System.Drawing.Size(47, 19);
            this.labelCancal.TabIndex = 0;
            this.labelCancal.Text = "取  消";
            this.labelCancal.Click += new System.EventHandler(this.panelCancel_Click);
            // 
            // panelYes
            // 
            this.panelYes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(243)))), ((int)(((byte)(172)))));
            this.panelYes.Controls.Add(this.labelYes);
            this.panelYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelYes.Location = new System.Drawing.Point(203, 16);
            this.panelYes.Name = "panelYes";
            this.panelYes.Size = new System.Drawing.Size(102, 35);
            this.panelYes.TabIndex = 4;
            this.panelYes.Click += new System.EventHandler(this.panelYes_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::HXCServerWinForm.Properties.Resources.input;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.tbDBPwd);
            this.panel2.Location = new System.Drawing.Point(146, 397);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(261, 31);
            this.panel2.TabIndex = 54;
            // 
            // tbDBPwd
            // 
            this.tbDBPwd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDBPwd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbDBPwd.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbDBPwd.BackColor = System.Drawing.Color.Transparent;
            this.tbDBPwd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbDBPwd.BorderColor = System.Drawing.Color.White;
            this.tbDBPwd.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tbDBPwd.ForeImage = null;
            this.tbDBPwd.Location = new System.Drawing.Point(5, 1);
            this.tbDBPwd.Margin = new System.Windows.Forms.Padding(5);
            this.tbDBPwd.MaxLengh = 32767;
            this.tbDBPwd.Multiline = false;
            this.tbDBPwd.Name = "tbDBPwd";
            this.tbDBPwd.Radius = 0;
            this.tbDBPwd.ReadOnly = false;
            this.tbDBPwd.ShadowColor = System.Drawing.Color.White;
            this.tbDBPwd.ShowError = false;
            this.tbDBPwd.Size = new System.Drawing.Size(226, 27);
            this.tbDBPwd.TabIndex = 0;
            this.tbDBPwd.UseSystemPasswordChar = false;
            this.tbDBPwd.Value = "";
            this.tbDBPwd.VerifyCondition = null;
            this.tbDBPwd.VerifyType = null;
            this.tbDBPwd.WaterMark = "";
            this.tbDBPwd.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(225)))), ((int)(((byte)(226)))));
            this.panel4.Controls.Add(this.panelCancel);
            this.panel4.Controls.Add(this.panelYes);
            this.panel4.Location = new System.Drawing.Point(0, 470);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(462, 65);
            this.panel4.TabIndex = 55;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::HXCServerWinForm.Properties.Resources.input;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.tbDBName);
            this.panel3.Location = new System.Drawing.Point(146, 338);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(261, 31);
            this.panel3.TabIndex = 52;
            // 
            // tbDBName
            // 
            this.tbDBName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDBName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbDBName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbDBName.BackColor = System.Drawing.Color.Transparent;
            this.tbDBName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbDBName.BorderColor = System.Drawing.Color.White;
            this.tbDBName.Caption = "例如：db";
            this.tbDBName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tbDBName.ForeImage = null;
            this.tbDBName.Location = new System.Drawing.Point(5, 1);
            this.tbDBName.Margin = new System.Windows.Forms.Padding(5);
            this.tbDBName.MaxLengh = 32767;
            this.tbDBName.Multiline = false;
            this.tbDBName.Name = "tbDBName";
            this.tbDBName.Radius = 0;
            this.tbDBName.ReadOnly = false;
            this.tbDBName.ShadowColor = System.Drawing.Color.White;
            this.tbDBName.ShowError = false;
            this.tbDBName.Size = new System.Drawing.Size(226, 27);
            this.tbDBName.TabIndex = 0;
            this.tbDBName.UseSystemPasswordChar = false;
            this.tbDBName.Value = "例如：db";
            this.tbDBName.VerifyCondition = null;
            this.tbDBName.VerifyType = null;
            this.tbDBName.WaterMark = "例如：db";
            this.tbDBName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(194)))), ((int)(((byte)(159)))));
            this.label4.Location = new System.Drawing.Point(54, 407);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 53;
            this.label4.Text = "口令：";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(194)))), ((int)(((byte)(159)))));
            this.label5.Location = new System.Drawing.Point(54, 347);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 17);
            this.label5.TabIndex = 51;
            this.label5.Text = "数据库实例：";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::HXCServerWinForm.Properties.Resources.input;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.tbDBUser);
            this.panel1.Location = new System.Drawing.Point(146, 253);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 31);
            this.panel1.TabIndex = 50;
            // 
            // tbDBUser
            // 
            this.tbDBUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDBUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbDBUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbDBUser.BackColor = System.Drawing.Color.Transparent;
            this.tbDBUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbDBUser.BorderColor = System.Drawing.Color.White;
            this.tbDBUser.Caption = "例如：user";
            this.tbDBUser.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tbDBUser.ForeImage = null;
            this.tbDBUser.Location = new System.Drawing.Point(5, 1);
            this.tbDBUser.Margin = new System.Windows.Forms.Padding(5);
            this.tbDBUser.MaxLengh = 32767;
            this.tbDBUser.Multiline = false;
            this.tbDBUser.Name = "tbDBUser";
            this.tbDBUser.Radius = 0;
            this.tbDBUser.ReadOnly = false;
            this.tbDBUser.ShadowColor = System.Drawing.Color.White;
            this.tbDBUser.ShowError = false;
            this.tbDBUser.Size = new System.Drawing.Size(226, 27);
            this.tbDBUser.TabIndex = 0;
            this.tbDBUser.UseSystemPasswordChar = false;
            this.tbDBUser.Value = "例如：user";
            this.tbDBUser.VerifyCondition = null;
            this.tbDBUser.VerifyType = null;
            this.tbDBUser.WaterMark = "例如：user";
            this.tbDBUser.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(194)))), ((int)(((byte)(159)))));
            this.label2.Location = new System.Drawing.Point(54, 262);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 49;
            this.label2.Text = "用户名：";
            // 
            // panelDb
            // 
            this.panelDb.BackColor = System.Drawing.Color.Transparent;
            this.panelDb.BackgroundImage = global::HXCServerWinForm.Properties.Resources.input;
            this.panelDb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelDb.Controls.Add(this.tbDBIp);
            this.panelDb.Location = new System.Drawing.Point(146, 194);
            this.panelDb.Name = "panelDb";
            this.panelDb.Size = new System.Drawing.Size(261, 31);
            this.panelDb.TabIndex = 48;
            // 
            // tbDBIp
            // 
            this.tbDBIp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDBIp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbDBIp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbDBIp.BackColor = System.Drawing.Color.Transparent;
            this.tbDBIp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbDBIp.BorderColor = System.Drawing.Color.White;
            this.tbDBIp.Caption = "例如：192.168.1.10";
            this.tbDBIp.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tbDBIp.ForeImage = null;
            this.tbDBIp.Location = new System.Drawing.Point(5, 1);
            this.tbDBIp.Margin = new System.Windows.Forms.Padding(5);
            this.tbDBIp.MaxLengh = 32767;
            this.tbDBIp.Multiline = false;
            this.tbDBIp.Name = "tbDBIp";
            this.tbDBIp.Radius = 0;
            this.tbDBIp.ReadOnly = false;
            this.tbDBIp.ShadowColor = System.Drawing.Color.White;
            this.tbDBIp.ShowError = false;
            this.tbDBIp.Size = new System.Drawing.Size(226, 27);
            this.tbDBIp.TabIndex = 0;
            this.tbDBIp.UseSystemPasswordChar = false;
            this.tbDBIp.Value = "例如：192.168.1.10";
            this.tbDBIp.VerifyCondition = null;
            this.tbDBIp.VerifyType = null;
            this.tbDBIp.WaterMark = "例如：192.168.1.10";
            this.tbDBIp.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(194)))), ((int)(((byte)(159)))));
            this.label1.Location = new System.Drawing.Point(54, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 47;
            this.label1.Text = "数据库IP：";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.label3.Location = new System.Drawing.Point(18, 312);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(426, 1);
            this.label3.TabIndex = 46;
            // 
            // FormSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(462, 535);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelDb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSet";
            this.ShowInTaskbar = false;
            this.Text = "FormSet";
            this.Load += new System.EventHandler(this.FormSet_Load);
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.panelCancel.ResumeLayout(false);
            this.panelCancel.PerformLayout();
            this.panelYes.ResumeLayout(false);
            this.panelYes.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panelDb.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ServiceStationClient.ComponentUI.TextBoxEx tbDBPwd;
        private System.Windows.Forms.Label labelYes;
        private ServiceStationClient.ComponentUI.TextBoxEx tbDBName;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pbMin;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Panel panelCancel;
        private System.Windows.Forms.Label labelCancal;
        private System.Windows.Forms.Panel panelYes;
        private ServiceStationClient.ComponentUI.TextBoxEx tbDBUser;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelDb;
        private ServiceStationClient.ComponentUI.TextBoxEx tbDBIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}