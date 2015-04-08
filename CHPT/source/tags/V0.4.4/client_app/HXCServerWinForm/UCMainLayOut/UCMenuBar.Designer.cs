namespace HXCServerWinForm.UCMainLayOut
{
    partial class UCMenuBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCMenuBar));
            this.flpnlContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.ibtnSysMonitor = new ServiceStationClient.ComponentUI.ImageButton();
            this.ibtnSysMaintenance = new ServiceStationClient.ComponentUI.ImageButton();
            this.ibtnPermisssionManage = new ServiceStationClient.ComponentUI.ImageButton();
            this.flpnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpnlContainer
            // 
            this.flpnlContainer.BackColor = System.Drawing.Color.Transparent;
            this.flpnlContainer.Controls.Add(this.ibtnSysMonitor);
            this.flpnlContainer.Controls.Add(this.ibtnSysMaintenance);
            this.flpnlContainer.Controls.Add(this.ibtnPermisssionManage);
            this.flpnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpnlContainer.Location = new System.Drawing.Point(0, 0);
            this.flpnlContainer.Margin = new System.Windows.Forms.Padding(0);
            this.flpnlContainer.Name = "flpnlContainer";
            this.flpnlContainer.Size = new System.Drawing.Size(373, 69);
            this.flpnlContainer.TabIndex = 5;
            // 
            // ibtnSysMonitor
            // 
            this.ibtnSysMonitor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ibtnSysMonitor.BackgroundImage")));
            this.ibtnSysMonitor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ibtnSysMonitor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ibtnSysMonitor.ButtonStatus = ServiceStationClient.ComponentUI.ImageButton.Status.Down;
            this.ibtnSysMonitor.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ibtnSysMonitor.ImageDown = null;
            this.ibtnSysMonitor.ImageHover = null;
            this.ibtnSysMonitor.ImageNormal = null;
            this.ibtnSysMonitor.IngoreLeave = true;
            this.ibtnSysMonitor.Location = new System.Drawing.Point(299, 0);
            this.ibtnSysMonitor.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.ibtnSysMonitor.Name = "ibtnSysMonitor";
            this.ibtnSysMonitor.Size = new System.Drawing.Size(69, 66);
            this.ibtnSysMonitor.TabIndex = 0;
            this.ibtnSysMonitor.Tag = "S_SysMonitor";
            this.ibtnSysMonitor.Click += new System.EventHandler(this.ibtnSysMonitor_Click);
            this.ibtnSysMonitor.MouseLeave += new System.EventHandler(this.imgbtn_MouseLeave);
            this.ibtnSysMonitor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imgbtn_MouseMove);
            // 
            // ibtnSysMaintenance
            // 
            this.ibtnSysMaintenance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ibtnSysMaintenance.BackgroundImage")));
            this.ibtnSysMaintenance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ibtnSysMaintenance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ibtnSysMaintenance.ButtonStatus = ServiceStationClient.ComponentUI.ImageButton.Status.Normal;
            this.ibtnSysMaintenance.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ibtnSysMaintenance.ImageDown = null;
            this.ibtnSysMaintenance.ImageHover = null;
            this.ibtnSysMaintenance.ImageNormal = null;
            this.ibtnSysMaintenance.IngoreLeave = true;
            this.ibtnSysMaintenance.Location = new System.Drawing.Point(225, 0);
            this.ibtnSysMaintenance.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.ibtnSysMaintenance.Name = "ibtnSysMaintenance";
            this.ibtnSysMaintenance.Size = new System.Drawing.Size(69, 66);
            this.ibtnSysMaintenance.TabIndex = 1;
            this.ibtnSysMaintenance.Tag = "S_SysMaintenance";
            this.ibtnSysMaintenance.Click += new System.EventHandler(this.ibtnSysMaintenance_Click);
            this.ibtnSysMaintenance.MouseLeave += new System.EventHandler(this.imgbtn_MouseLeave);
            this.ibtnSysMaintenance.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imgbtn_MouseMove);
            // 
            // ibtnPermisssionManage
            // 
            this.ibtnPermisssionManage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ibtnPermisssionManage.BackgroundImage")));
            this.ibtnPermisssionManage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ibtnPermisssionManage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ibtnPermisssionManage.ButtonStatus = ServiceStationClient.ComponentUI.ImageButton.Status.Normal;
            this.ibtnPermisssionManage.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ibtnPermisssionManage.ImageDown = null;
            this.ibtnPermisssionManage.ImageHover = null;
            this.ibtnPermisssionManage.ImageNormal = null;
            this.ibtnPermisssionManage.IngoreLeave = true;
            this.ibtnPermisssionManage.Location = new System.Drawing.Point(151, 0);
            this.ibtnPermisssionManage.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.ibtnPermisssionManage.Name = "ibtnPermisssionManage";
            this.ibtnPermisssionManage.Size = new System.Drawing.Size(69, 66);
            this.ibtnPermisssionManage.TabIndex = 2;
            this.ibtnPermisssionManage.Tag = "S_PermissionManage";
            this.ibtnPermisssionManage.Click += new System.EventHandler(this.ibtnPermisssionManage_Click);
            this.ibtnPermisssionManage.MouseLeave += new System.EventHandler(this.imgbtn_MouseLeave);
            this.ibtnPermisssionManage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imgbtn_MouseMove);
            // 
            // UCMenuBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.flpnlContainer);
            this.Name = "UCMenuBar";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(373, 69);
            this.Load += new System.EventHandler(this.UCMenuBar_Load);
            this.flpnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public ServiceStationClient.ComponentUI.ImageButton ibtnSysMonitor;
        public ServiceStationClient.ComponentUI.ImageButton ibtnSysMaintenance;
        public ServiceStationClient.ComponentUI.ImageButton ibtnPermisssionManage;
        private System.Windows.Forms.FlowLayoutPanel flpnlContainer;
    }
}
