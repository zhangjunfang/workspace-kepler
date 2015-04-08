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
            this.btnOK.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.Caption = "确定";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnOK.Location = new System.Drawing.Point(258, 387);
            this.btnOK.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnOK.Size = new System.Drawing.Size(80, 24);
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
            this.btnColse.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnColse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnColse.Caption = "取消";
            this.btnColse.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnColse.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnColse.Location = new System.Drawing.Point(344, 387);
            this.btnColse.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnColse.Name = "btnColse";
            this.btnColse.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnColse.Size = new System.Drawing.Size(80, 24);
            this.btnColse.TabIndex = 4;
            this.btnColse.Click += new System.EventHandler(this.btnColse_Click);
            // 
            // frmPartsType
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnColse;
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