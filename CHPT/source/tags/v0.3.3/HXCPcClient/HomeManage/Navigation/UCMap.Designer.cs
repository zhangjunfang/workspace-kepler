namespace HXCPcClient.HomeManage
{
    partial class UCMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCMap));
            this.panelMap = new ServiceStationClient.ComponentUI.Panel.PanelExtend();
            this.btnTitle = new ServiceStationClient.ComponentUI.ButtonEx();
            this.SuspendLayout();
            // 
            // panelMap
            // 
            this.panelMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.panelMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelMap.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.panelMap.Location = new System.Drawing.Point(11, 33);
            this.panelMap.Name = "panelMap";
            this.panelMap.Padding = new System.Windows.Forms.Padding(1);
            this.panelMap.ShowBorder = true;
            this.panelMap.Size = new System.Drawing.Size(496, 243);
            this.panelMap.TabIndex = 2;
            this.panelMap.SizeChanged += new System.EventHandler(this.panelMap_SizeChanged);
            // 
            // btnTitle
            // 
            this.btnTitle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTitle.BackgroundImage")));
            this.btnTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTitle.Caption = "业务-XX";
            this.btnTitle.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnTitle.DownImage = ((System.Drawing.Image)(resources.GetObject("btnTitle.DownImage")));
            this.btnTitle.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.btnTitle.Location = new System.Drawing.Point(11, 6);
            this.btnTitle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnTitle.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnTitle.MoveImage")));
            this.btnTitle.Name = "btnTitle";
            this.btnTitle.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnTitle.NormalImage")));
            this.btnTitle.Size = new System.Drawing.Size(92, 30);
            this.btnTitle.TabIndex = 3;
            // 
            // UCMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.panelMap);
            this.Controls.Add(this.btnTitle);
            this.DoubleBuffered = true;
            this.Name = "UCMap";
            this.Size = new System.Drawing.Size(518, 286);            
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.Panel.PanelExtend panelMap;
        private ServiceStationClient.ComponentUI.ButtonEx btnTitle;
    }
}
