namespace HXCPcClient
{
    partial class UUCHomeManage
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
            this.panelRightTop = new ServiceStationClient.ComponentUI.Panel.PanelExtend();
            this.panelLeft = new ServiceStationClient.ComponentUI.Panel.PanelExtend();
            this.panelRightButtom = new ServiceStationClient.ComponentUI.Panel.PanelExtend();
            this.SuspendLayout();
            // 
            // panelRightTop
            // 
            this.panelRightTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRightTop.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.panelRightTop.Location = new System.Drawing.Point(737, 0);
            this.panelRightTop.Name = "panelRightTop";
            this.panelRightTop.Padding = new System.Windows.Forms.Padding(1);
            this.panelRightTop.ShowBorder = true;
            this.panelRightTop.Size = new System.Drawing.Size(347, 204);
            this.panelRightTop.TabIndex = 0;
            // 
            // panelLeft
            // 
            this.panelLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLeft.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.panelLeft.Location = new System.Drawing.Point(-1, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(1);
            this.panelLeft.ShowBorder = true;
            this.panelLeft.Size = new System.Drawing.Size(732, 589);
            this.panelLeft.TabIndex = 4;
            // 
            // panelRightButtom
            // 
            this.panelRightButtom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRightButtom.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.panelRightButtom.Location = new System.Drawing.Point(737, 209);
            this.panelRightButtom.Name = "panelRightButtom";
            this.panelRightButtom.Padding = new System.Windows.Forms.Padding(1);
            this.panelRightButtom.ShowBorder = true;
            this.panelRightButtom.Size = new System.Drawing.Size(347, 380);
            this.panelRightButtom.TabIndex = 5;
            // 
            // UUCHomeManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.panelRightButtom);
            this.Controls.Add(this.panelRightTop);
            this.Controls.Add(this.panelLeft);
            this.DoubleBuffered = true;
            this.Name = "UUCHomeManage";
            this.Size = new System.Drawing.Size(1083, 588);
            this.Load += new System.EventHandler(this.UUCHomeManage_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.Panel.PanelExtend panelLeft;
        private ServiceStationClient.ComponentUI.Panel.PanelExtend panelRightTop;
        private ServiceStationClient.ComponentUI.Panel.PanelExtend panelRightButtom;






    }
}
