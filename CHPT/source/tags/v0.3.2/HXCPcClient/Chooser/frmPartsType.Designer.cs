namespace HXCPcClient.Chooser
{
    partial class frmPartsType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPartsType));
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new ServiceStationClient.ComponentUI.ButtonEx();
            this.tvData = new ServiceStationClient.ComponentUI.TreeViewEx();
            this.btnColse = new ServiceStationClient.ComponentUI.ButtonEx();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.btnColse);
            this.pnlContainer.Controls.Add(this.tvData);
            this.pnlContainer.Controls.Add(this.btnOK);
            this.pnlContainer.Controls.Add(this.label1);
            this.pnlContainer.Size = new System.Drawing.Size(434, 421);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请选择你想要的类别，双击或点确定返回：";
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.Caption = "确定";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.DownImage = ((System.Drawing.Image)(resources.GetObject("btnOK.DownImage")));
            this.btnOK.Location = new System.Drawing.Point(243, 385);
            this.btnOK.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnOK.MoveImage")));
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnOK.NormalImage")));
            this.btnOK.Size = new System.Drawing.Size(60, 26);
            this.btnOK.TabIndex = 1;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tvData
            // 
            this.tvData.IgnoreAutoCheck = false;
            this.tvData.Location = new System.Drawing.Point(26, 51);
            this.tvData.Name = "tvData";
            this.tvData.Size = new System.Drawing.Size(387, 306);
            this.tvData.TabIndex = 3;
            this.tvData.OnCustomDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvData_OnCustomDoubleClick);
            // 
            // btnColse
            // 
            this.btnColse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnColse.BackgroundImage")));
            this.btnColse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnColse.Caption = "取消";
            this.btnColse.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnColse.DownImage = ((System.Drawing.Image)(resources.GetObject("btnColse.DownImage")));
            this.btnColse.Location = new System.Drawing.Point(333, 383);
            this.btnColse.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnColse.MoveImage")));
            this.btnColse.Name = "btnColse";
            this.btnColse.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnColse.NormalImage")));
            this.btnColse.Size = new System.Drawing.Size(60, 26);
            this.btnColse.TabIndex = 4;
            this.btnColse.Click += new System.EventHandler(this.btnColse_Click);
            // 
            // frmPartsType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(437, 453);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPartsType";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配件类型选择";
            this.Load += new System.EventHandler(this.frmPartsType_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.ButtonEx btnColse;
        private ServiceStationClient.ComponentUI.TreeViewEx tvData;
        private ServiceStationClient.ComponentUI.ButtonEx btnOK;
        private System.Windows.Forms.Label label1;
    }
}