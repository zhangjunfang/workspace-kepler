namespace HXCServerWinForm.UCForm
{
    partial class frmServiceSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServiceSet));
            this.tbServerPort = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.tbServerIp = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.tbSavePath = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBrowser = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnCancel = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnYes = new ServiceStationClient.ComponentUI.ButtonEx();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tbFilePort = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.White;
            this.pnlContainer.Controls.Add(this.tbFilePort);
            this.pnlContainer.Controls.Add(this.label5);
            this.pnlContainer.Controls.Add(this.btnBrowser);
            this.pnlContainer.Controls.Add(this.tbSavePath);
            this.pnlContainer.Controls.Add(this.label4);
            this.pnlContainer.Controls.Add(this.labelTitle);
            this.pnlContainer.Controls.Add(this.tbServerPort);
            this.pnlContainer.Controls.Add(this.label2);
            this.pnlContainer.Controls.Add(this.tbServerIp);
            this.pnlContainer.Controls.Add(this.label1);
            this.pnlContainer.Size = new System.Drawing.Size(585, 272);
            // 
            // tbServerPort
            // 
            this.tbServerPort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbServerPort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbServerPort.BackColor = System.Drawing.Color.Transparent;
            this.tbServerPort.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbServerPort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbServerPort.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbServerPort.ForeImage = null;
            this.tbServerPort.InputtingVerifyCondition = null;
            this.tbServerPort.Location = new System.Drawing.Point(269, 67);
            this.tbServerPort.MaxLengh = 32767;
            this.tbServerPort.Multiline = false;
            this.tbServerPort.Name = "tbServerPort";
            this.tbServerPort.Radius = 3;
            this.tbServerPort.ReadOnly = false;
            this.tbServerPort.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbServerPort.ShowError = false;
            this.tbServerPort.Size = new System.Drawing.Size(190, 25);
            this.tbServerPort.TabIndex = 7;
            this.tbServerPort.UseSystemPasswordChar = false;
            this.tbServerPort.Value = "";
            this.tbServerPort.VerifyCondition = null;
            this.tbServerPort.VerifyType = null;
            this.tbServerPort.VerifyTypeName = null;
            this.tbServerPort.WaterMark = null;
            this.tbServerPort.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "数据通讯端口号：";
            // 
            // tbServerIp
            // 
            this.tbServerIp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbServerIp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbServerIp.BackColor = System.Drawing.Color.Transparent;
            this.tbServerIp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbServerIp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbServerIp.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbServerIp.ForeImage = null;
            this.tbServerIp.InputtingVerifyCondition = null;
            this.tbServerIp.Location = new System.Drawing.Point(269, 23);
            this.tbServerIp.MaxLengh = 32767;
            this.tbServerIp.Multiline = false;
            this.tbServerIp.Name = "tbServerIp";
            this.tbServerIp.Radius = 3;
            this.tbServerIp.ReadOnly = true;
            this.tbServerIp.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbServerIp.ShowError = false;
            this.tbServerIp.Size = new System.Drawing.Size(190, 25);
            this.tbServerIp.TabIndex = 5;
            this.tbServerIp.UseSystemPasswordChar = false;
            this.tbServerIp.Value = "";
            this.tbServerIp.VerifyCondition = null;
            this.tbServerIp.VerifyType = null;
            this.tbServerIp.VerifyTypeName = null;
            this.tbServerIp.WaterMark = null;
            this.tbServerIp.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(157, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "服务器IP地址：";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelTitle.Location = new System.Drawing.Point(11, 25);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(121, 14);
            this.labelTitle.TabIndex = 8;
            this.labelTitle.Text = "C/S服务器配置：";
            // 
            // tbSavePath
            // 
            this.tbSavePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbSavePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbSavePath.BackColor = System.Drawing.Color.Transparent;
            this.tbSavePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbSavePath.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbSavePath.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSavePath.ForeImage = null;
            this.tbSavePath.InputtingVerifyCondition = null;
            this.tbSavePath.Location = new System.Drawing.Point(125, 192);
            this.tbSavePath.MaxLengh = 32767;
            this.tbSavePath.Multiline = false;
            this.tbSavePath.Name = "tbSavePath";
            this.tbSavePath.Radius = 3;
            this.tbSavePath.ReadOnly = false;
            this.tbSavePath.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbSavePath.ShowError = false;
            this.tbSavePath.Size = new System.Drawing.Size(331, 25);
            this.tbSavePath.TabIndex = 12;
            this.tbSavePath.UseSystemPasswordChar = false;
            this.tbSavePath.Value = "";
            this.tbSavePath.VerifyCondition = null;
            this.tbSavePath.VerifyType = null;
            this.tbSavePath.VerifyTypeName = null;
            this.tbSavePath.WaterMark = null;
            this.tbSavePath.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "默认数据存放目录：";
            // 
            // btnBrowser
            // 
            this.btnBrowser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowser.BackgroundImage")));
            this.btnBrowser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowser.Caption = "选择目录";
            this.btnBrowser.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnBrowser.DownImage = ((System.Drawing.Image)(resources.GetObject("btnBrowser.DownImage")));
            this.btnBrowser.Location = new System.Drawing.Point(459, 192);
            this.btnBrowser.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnBrowser.MoveImage")));
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnBrowser.NormalImage")));
            this.btnBrowser.Size = new System.Drawing.Size(88, 26);
            this.btnBrowser.TabIndex = 13;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Caption = "取消";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.DownImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.DownImage")));
            this.btnCancel.Location = new System.Drawing.Point(484, 311);
            this.btnCancel.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.MoveImage")));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.NormalImage")));
            this.btnCancel.Size = new System.Drawing.Size(75, 26);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnYes
            // 
            this.btnYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnYes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnYes.BackgroundImage")));
            this.btnYes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnYes.Caption = "确定";
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnYes.DownImage = ((System.Drawing.Image)(resources.GetObject("btnYes.DownImage")));
            this.btnYes.Location = new System.Drawing.Point(370, 311);
            this.btnYes.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnYes.MoveImage")));
            this.btnYes.Name = "btnYes";
            this.btnYes.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnYes.NormalImage")));
            this.btnYes.Size = new System.Drawing.Size(75, 26);
            this.btnYes.TabIndex = 3;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tbFilePort
            // 
            this.tbFilePort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbFilePort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbFilePort.BackColor = System.Drawing.Color.Transparent;
            this.tbFilePort.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbFilePort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbFilePort.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbFilePort.ForeImage = null;
            this.tbFilePort.InputtingVerifyCondition = null;
            this.tbFilePort.Location = new System.Drawing.Point(269, 112);
            this.tbFilePort.MaxLengh = 32767;
            this.tbFilePort.Multiline = false;
            this.tbFilePort.Name = "tbFilePort";
            this.tbFilePort.Radius = 3;
            this.tbFilePort.ReadOnly = false;
            this.tbFilePort.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbFilePort.ShowError = false;
            this.tbFilePort.Size = new System.Drawing.Size(190, 25);
            this.tbFilePort.TabIndex = 15;
            this.tbFilePort.UseSystemPasswordChar = false;
            this.tbFilePort.Value = "";
            this.tbFilePort.VerifyCondition = null;
            this.tbFilePort.VerifyType = null;
            this.tbFilePort.VerifyTypeName = null;
            this.tbFilePort.WaterMark = null;
            this.tbFilePort.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(157, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "文件服务端口号：";
            // 
            // frmServiceSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 346);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnYes);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmServiceSet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务器设置";
            this.Load += new System.EventHandler(this.frmServiceSet_Load);
            this.Controls.SetChildIndex(this.pnlContainer, 0);
            this.Controls.SetChildIndex(this.btnYes, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.TextBoxEx tbServerPort;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx tbServerIp;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ButtonEx btnBrowser;
        private ServiceStationClient.ComponentUI.TextBoxEx tbSavePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelTitle;
        private ServiceStationClient.ComponentUI.ButtonEx btnCancel;
        private ServiceStationClient.ComponentUI.ButtonEx btnYes;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private ServiceStationClient.ComponentUI.TextBoxEx tbFilePort;
        private System.Windows.Forms.Label label5;
    }
}