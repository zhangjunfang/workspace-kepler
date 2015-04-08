namespace HXCPcClient.LoginForms
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pbMin = new System.Windows.Forms.PictureBox();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelDb = new System.Windows.Forms.Panel();
            this.tbServerIp = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbServerPort = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbFilePort = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tbFileServerIp = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelCancel = new System.Windows.Forms.Panel();
            this.labelCancal = new System.Windows.Forms.Label();
            this.panelYes = new System.Windows.Forms.Panel();
            this.labelYes = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.panelDb.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelCancel.SuspendLayout();
            this.panelYes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.panelTop.Controls.Add(this.pictureBox4);
            this.panelTop.Controls.Add(this.pbMin);
            this.panelTop.Controls.Add(this.pbClose);
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(462, 179);
            this.panelTop.TabIndex = 1;
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
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.label3.Location = new System.Drawing.Point(18, 343);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(426, 1);
            this.label3.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.label1.Location = new System.Drawing.Point(51, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 36;
            this.label1.Text = "数据服务器地址：";
            // 
            // panelDb
            // 
            this.panelDb.BackColor = System.Drawing.Color.Transparent;
            this.panelDb.BackgroundImage = global::HXCPcClient.Properties.Resources.input;
            this.panelDb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelDb.Controls.Add(this.tbServerIp);
            this.panelDb.Location = new System.Drawing.Point(163, 213);
            this.panelDb.Name = "panelDb";
            this.panelDb.Size = new System.Drawing.Size(261, 31);
            this.panelDb.TabIndex = 37;
            // 
            // tbServerIp
            // 
            this.tbServerIp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServerIp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbServerIp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbServerIp.BackColor = System.Drawing.Color.Transparent;
            this.tbServerIp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbServerIp.BorderColor = System.Drawing.Color.White;
            this.tbServerIp.Caption = "例如：192.168.1.10";
            this.tbServerIp.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tbServerIp.ForeImage = null;
            this.tbServerIp.Location = new System.Drawing.Point(5, 1);
            this.tbServerIp.Margin = new System.Windows.Forms.Padding(5);
            this.tbServerIp.MaxLengh = 32767;
            this.tbServerIp.Multiline = false;
            this.tbServerIp.Name = "tbServerIp";
            this.tbServerIp.Radius = 0;
            this.tbServerIp.ReadOnly = false;
            this.tbServerIp.ShadowColor = System.Drawing.Color.White;
            this.tbServerIp.ShowError = false;
            this.tbServerIp.Size = new System.Drawing.Size(226, 27);
            this.tbServerIp.TabIndex = 0;
            this.tbServerIp.UseSystemPasswordChar = false;
            this.tbServerIp.Value = "例如：192.168.1.10";
            this.tbServerIp.VerifyCondition = null;
            this.tbServerIp.VerifyType = null;
            this.tbServerIp.WaterMark = "例如：192.168.1.10";
            this.tbServerIp.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::HXCPcClient.Properties.Resources.input;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.tbServerPort);
            this.panel1.Location = new System.Drawing.Point(163, 279);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 31);
            this.panel1.TabIndex = 39;
            // 
            // tbServerPort
            // 
            this.tbServerPort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServerPort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbServerPort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbServerPort.BackColor = System.Drawing.Color.Transparent;
            this.tbServerPort.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbServerPort.BorderColor = System.Drawing.Color.White;
            this.tbServerPort.Caption = "例如：9001";
            this.tbServerPort.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tbServerPort.ForeImage = null;
            this.tbServerPort.Location = new System.Drawing.Point(5, 1);
            this.tbServerPort.Margin = new System.Windows.Forms.Padding(5);
            this.tbServerPort.MaxLengh = 32767;
            this.tbServerPort.Multiline = false;
            this.tbServerPort.Name = "tbServerPort";
            this.tbServerPort.Radius = 0;
            this.tbServerPort.ReadOnly = false;
            this.tbServerPort.ShadowColor = System.Drawing.Color.White;
            this.tbServerPort.ShowError = false;
            this.tbServerPort.Size = new System.Drawing.Size(226, 27);
            this.tbServerPort.TabIndex = 0;
            this.tbServerPort.UseSystemPasswordChar = false;
            this.tbServerPort.Value = "例如：9001";
            this.tbServerPort.VerifyCondition = null;
            this.tbServerPort.VerifyType = null;
            this.tbServerPort.WaterMark = "例如：9001";
            this.tbServerPort.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.label2.Location = new System.Drawing.Point(51, 288);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 17);
            this.label2.TabIndex = 38;
            this.label2.Text = "数据通讯端口号：";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::HXCPcClient.Properties.Resources.input;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.tbFilePort);
            this.panel2.Location = new System.Drawing.Point(163, 442);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(261, 31);
            this.panel2.TabIndex = 43;
            // 
            // tbFilePort
            // 
            this.tbFilePort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilePort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbFilePort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbFilePort.BackColor = System.Drawing.Color.Transparent;
            this.tbFilePort.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbFilePort.BorderColor = System.Drawing.Color.White;
            this.tbFilePort.Caption = "例如：9000";
            this.tbFilePort.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tbFilePort.ForeImage = null;
            this.tbFilePort.Location = new System.Drawing.Point(5, 1);
            this.tbFilePort.Margin = new System.Windows.Forms.Padding(5);
            this.tbFilePort.MaxLengh = 32767;
            this.tbFilePort.Multiline = false;
            this.tbFilePort.Name = "tbFilePort";
            this.tbFilePort.Radius = 0;
            this.tbFilePort.ReadOnly = false;
            this.tbFilePort.ShadowColor = System.Drawing.Color.White;
            this.tbFilePort.ShowError = false;
            this.tbFilePort.Size = new System.Drawing.Size(226, 27);
            this.tbFilePort.TabIndex = 0;
            this.tbFilePort.UseSystemPasswordChar = false;
            this.tbFilePort.Value = "例如：9000";
            this.tbFilePort.VerifyCondition = null;
            this.tbFilePort.VerifyType = null;
            this.tbFilePort.WaterMark = "例如：9000";
            this.tbFilePort.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.label4.Location = new System.Drawing.Point(51, 451);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 17);
            this.label4.TabIndex = 42;
            this.label4.Text = "文件服务端口号：";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::HXCPcClient.Properties.Resources.input;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.tbFileServerIp);
            this.panel3.Location = new System.Drawing.Point(163, 376);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(261, 31);
            this.panel3.TabIndex = 41;
            // 
            // tbFileServerIp
            // 
            this.tbFileServerIp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileServerIp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbFileServerIp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbFileServerIp.BackColor = System.Drawing.Color.Transparent;
            this.tbFileServerIp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbFileServerIp.BorderColor = System.Drawing.Color.White;
            this.tbFileServerIp.Caption = "例如：192.168.1.10";
            this.tbFileServerIp.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tbFileServerIp.ForeImage = null;
            this.tbFileServerIp.Location = new System.Drawing.Point(5, 1);
            this.tbFileServerIp.Margin = new System.Windows.Forms.Padding(5);
            this.tbFileServerIp.MaxLengh = 32767;
            this.tbFileServerIp.Multiline = false;
            this.tbFileServerIp.Name = "tbFileServerIp";
            this.tbFileServerIp.Radius = 0;
            this.tbFileServerIp.ReadOnly = false;
            this.tbFileServerIp.ShadowColor = System.Drawing.Color.White;
            this.tbFileServerIp.ShowError = false;
            this.tbFileServerIp.Size = new System.Drawing.Size(226, 27);
            this.tbFileServerIp.TabIndex = 0;
            this.tbFileServerIp.UseSystemPasswordChar = false;
            this.tbFileServerIp.Value = "例如：192.168.1.10";
            this.tbFileServerIp.VerifyCondition = null;
            this.tbFileServerIp.VerifyType = null;
            this.tbFileServerIp.WaterMark = "例如：192.168.1.10";
            this.tbFileServerIp.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.label5.Location = new System.Drawing.Point(51, 384);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 17);
            this.label5.TabIndex = 40;
            this.label5.Text = "文件服务器地址：";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(225)))), ((int)(((byte)(226)))));
            this.panel4.Controls.Add(this.panelCancel);
            this.panel4.Controls.Add(this.panelYes);
            this.panel4.Location = new System.Drawing.Point(0, 503);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(462, 69);
            this.panel4.TabIndex = 44;
            // 
            // panelCancel
            // 
            this.panelCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(187)))), ((int)(((byte)(202)))));
            this.panelCancel.Controls.Add(this.labelCancal);
            this.panelCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelCancel.Location = new System.Drawing.Point(324, 17);
            this.panelCancel.Name = "panelCancel";
            this.panelCancel.Size = new System.Drawing.Size(102, 35);
            this.panelCancel.TabIndex = 5;
            this.panelCancel.Click += new System.EventHandler(this.panelCancel_Click);
            this.panelCancel.MouseEnter += new System.EventHandler(this.panelCancel_MouseEnter);
            this.panelCancel.MouseLeave += new System.EventHandler(this.panelCancel_MouseLeave);
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
            this.labelCancal.MouseEnter += new System.EventHandler(this.panelCancel_MouseEnter);
            this.labelCancal.MouseLeave += new System.EventHandler(this.panelCancel_MouseLeave);
            // 
            // panelYes
            // 
            this.panelYes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.panelYes.Controls.Add(this.labelYes);
            this.panelYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelYes.Location = new System.Drawing.Point(203, 17);
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
            this.labelYes.MouseEnter += new System.EventHandler(this.panelYes_MouseEnter);
            this.labelYes.MouseLeave += new System.EventHandler(this.panelYes_MouseLeave);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
            // 
            // FormSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(462, 572);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel3);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSet_FormClosing);
            this.Load += new System.EventHandler(this.FormSet_Load);
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.panelDb.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panelCancel.ResumeLayout(false);
            this.panelCancel.PerformLayout();
            this.panelYes.ResumeLayout(false);
            this.panelYes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pbMin;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelDb;
        private ServiceStationClient.ComponentUI.TextBoxEx tbServerIp;
        private System.Windows.Forms.Panel panel1;
        private ServiceStationClient.ComponentUI.TextBoxEx tbServerPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private ServiceStationClient.ComponentUI.TextBoxEx tbFilePort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private ServiceStationClient.ComponentUI.TextBoxEx tbFileServerIp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelYes;
        private System.Windows.Forms.Label labelYes;
        private System.Windows.Forms.Panel panelCancel;
        private System.Windows.Forms.Label labelCancal;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}