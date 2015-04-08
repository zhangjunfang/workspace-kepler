namespace HXCServerWinForm.UCForm
{
    partial class UCServiceInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCServiceInfo));
            this.panelTop = new ServiceStationClient.ComponentUI.PanelEx();
            this.btnRecord = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnSet = new ServiceStationClient.ComponentUI.ButtonEx();
            this.labelPath = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelFilePort = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelServerPort = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelSeverIp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelTop.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelTop.Controls.Add(this.btnRecord);
            this.panelTop.Controls.Add(this.btnSet);
            this.panelTop.Location = new System.Drawing.Point(-1, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(702, 40);
            this.panelTop.TabIndex = 0;
            // 
            // btnRecord
            // 
            this.btnRecord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRecord.BackgroundImage")));
            this.btnRecord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRecord.Caption = "操作记录";
            this.btnRecord.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRecord.DownImage = ((System.Drawing.Image)(resources.GetObject("btnRecord.DownImage")));
            this.btnRecord.Location = new System.Drawing.Point(135, 7);
            this.btnRecord.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnRecord.MoveImage")));
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnRecord.NormalImage")));
            this.btnRecord.Size = new System.Drawing.Size(65, 26);
            this.btnRecord.TabIndex = 2;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnSet
            // 
            this.btnSet.BackColor = System.Drawing.Color.White;
            this.btnSet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSet.BackgroundImage")));
            this.btnSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSet.Caption = "服务器设置";
            this.btnSet.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSet.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSet.DownImage")));
            this.btnSet.Location = new System.Drawing.Point(16, 7);
            this.btnSet.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSet.MoveImage")));
            this.btnSet.Name = "btnSet";
            this.btnSet.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSet.NormalImage")));
            this.btnSet.Size = new System.Drawing.Size(111, 26);
            this.btnSet.TabIndex = 1;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(157, 224);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(47, 12);
            this.labelPath.TabIndex = 35;
            this.labelPath.Text = "E:\\慧联";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(43, 224);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 12);
            this.label10.TabIndex = 34;
            this.label10.Text = "附件默认存放目录：";
            // 
            // labelFilePort
            // 
            this.labelFilePort.AutoSize = true;
            this.labelFilePort.Location = new System.Drawing.Point(157, 188);
            this.labelFilePort.Name = "labelFilePort";
            this.labelFilePort.Size = new System.Drawing.Size(29, 12);
            this.labelFilePort.TabIndex = 31;
            this.labelFilePort.Text = "5000";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(43, 188);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.TabIndex = 30;
            this.label8.Text = "文件通信端口号：";
            // 
            // labelServerPort
            // 
            this.labelServerPort.AutoSize = true;
            this.labelServerPort.Location = new System.Drawing.Point(157, 152);
            this.labelServerPort.Name = "labelServerPort";
            this.labelServerPort.Size = new System.Drawing.Size(29, 12);
            this.labelServerPort.TabIndex = 29;
            this.labelServerPort.Text = "5000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "数据通信端口号：";
            // 
            // labelSeverIp
            // 
            this.labelSeverIp.AutoSize = true;
            this.labelSeverIp.Location = new System.Drawing.Point(157, 116);
            this.labelSeverIp.Name = "labelSeverIp";
            this.labelSeverIp.Size = new System.Drawing.Size(107, 12);
            this.labelSeverIp.TabIndex = 27;
            this.labelSeverIp.Text = "127.0.0.1（内网）";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "服务器IP地址：";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelTitle.Location = new System.Drawing.Point(42, 76);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(136, 14);
            this.labelTitle.TabIndex = 25;
            this.labelTitle.Text = "C/S服务器配置信息";
            // 
            // UCServiceInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.labelFilePort);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.labelServerPort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelSeverIp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.panelTop);
            this.Name = "UCServiceInfo";
            this.Size = new System.Drawing.Size(700, 400);
            this.Load += new System.EventHandler(this.UCServiceInfo_Load);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelTop;
        private ServiceStationClient.ComponentUI.ButtonEx btnSet;
        private ServiceStationClient.ComponentUI.ButtonEx btnRecord;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelFilePort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelServerPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelSeverIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelTitle;
    }
}
