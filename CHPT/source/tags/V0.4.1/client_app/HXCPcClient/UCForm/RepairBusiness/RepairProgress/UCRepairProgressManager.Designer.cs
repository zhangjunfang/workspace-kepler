namespace HXCPcClient.UCForm.RepairBusiness.RepairProgress
{
    partial class UCRepairProgressManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCRepairProgressManager));
            this.palInfo = new System.Windows.Forms.Panel();
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.cobStation = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.labStation = new System.Windows.Forms.Label();
            this.txtRepairWorker = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.labRepairWorker = new System.Windows.Forms.Label();
            this.txtCarNO = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.labCarNO = new System.Windows.Forms.Label();
            this.palData = new System.Windows.Forms.Panel();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.RepairProgress = new HXCPRepairProgress.UserControl1();
            this.palInfo.SuspendLayout();
            this.palData.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1025, 25);
            // 
            // palInfo
            // 
            this.palInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.palInfo.Controls.Add(this.btnQuery);
            this.palInfo.Controls.Add(this.btnClear);
            this.palInfo.Controls.Add(this.cobStation);
            this.palInfo.Controls.Add(this.labStation);
            this.palInfo.Controls.Add(this.txtRepairWorker);
            this.palInfo.Controls.Add(this.labRepairWorker);
            this.palInfo.Controls.Add(this.txtCarNO);
            this.palInfo.Controls.Add(this.labCarNO);
            this.palInfo.Location = new System.Drawing.Point(2, 33);
            this.palInfo.Name = "palInfo";
            this.palInfo.Size = new System.Drawing.Size(1023, 82);
            this.palInfo.TabIndex = 8;
            // 
            // btnQuery
            // 
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(713, 38);
            this.btnQuery.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.MoveImage")));
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.NormalImage")));
            this.btnQuery.Size = new System.Drawing.Size(60, 26);
            this.btnQuery.TabIndex = 163;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(713, 6);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 162;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cobStation
            // 
            this.cobStation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobStation.FormattingEnabled = true;
            this.cobStation.Location = new System.Drawing.Point(567, 6);
            this.cobStation.Name = "cobStation";
            this.cobStation.Size = new System.Drawing.Size(121, 22);
            this.cobStation.TabIndex = 161;
            // 
            // labStation
            // 
            this.labStation.AutoSize = true;
            this.labStation.Location = new System.Drawing.Point(521, 11);
            this.labStation.Name = "labStation";
            this.labStation.Size = new System.Drawing.Size(41, 12);
            this.labStation.TabIndex = 160;
            this.labStation.Text = "工位：";
            // 
            // txtRepairWorker
            // 
            this.txtRepairWorker.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtRepairWorker.Location = new System.Drawing.Point(80, 6);
            this.txtRepairWorker.Name = "txtRepairWorker";
            this.txtRepairWorker.ReadOnly = false;
            this.txtRepairWorker.Size = new System.Drawing.Size(224, 24);
            this.txtRepairWorker.TabIndex = 159;
            this.txtRepairWorker.ToolTipTitle = "";
            this.txtRepairWorker.ChooserClick += new System.EventHandler(this.txtRepairWorker_ChooserClick);
            // 
            // labRepairWorker
            // 
            this.labRepairWorker.AutoSize = true;
            this.labRepairWorker.Location = new System.Drawing.Point(23, 11);
            this.labRepairWorker.Name = "labRepairWorker";
            this.labRepairWorker.Size = new System.Drawing.Size(53, 12);
            this.labRepairWorker.TabIndex = 158;
            this.labRepairWorker.Text = "维修工：";
            // 
            // txtCarNO
            // 
            this.txtCarNO.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCarNO.Location = new System.Drawing.Point(378, 6);
            this.txtCarNO.Name = "txtCarNO";
            this.txtCarNO.ReadOnly = false;
            this.txtCarNO.Size = new System.Drawing.Size(117, 24);
            this.txtCarNO.TabIndex = 63;
            this.txtCarNO.ToolTipTitle = "";
            this.txtCarNO.ChooserClick += new System.EventHandler(this.txtCarNO_ChooserClick);
            // 
            // labCarNO
            // 
            this.labCarNO.AutoSize = true;
            this.labCarNO.Location = new System.Drawing.Point(322, 13);
            this.labCarNO.Name = "labCarNO";
            this.labCarNO.Size = new System.Drawing.Size(53, 12);
            this.labCarNO.TabIndex = 5;
            this.labCarNO.Text = "车牌号：";
            // 
            // palData
            // 
            this.palData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palData.Controls.Add(this.elementHost1);
            this.palData.Location = new System.Drawing.Point(4, 119);
            this.palData.Name = "palData";
            this.palData.Size = new System.Drawing.Size(1018, 422);
            this.palData.TabIndex = 9;
            // 
            // elementHost1
            // 
            this.elementHost1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.elementHost1.BackColor = System.Drawing.Color.White;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(1015, 422);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.RepairProgress;
            // 
            // UCRepairProgressManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.palData);
            this.Controls.Add(this.palInfo);
            this.Name = "UCRepairProgressManager";
            this.Size = new System.Drawing.Size(1025, 544);
            this.Load += new System.EventHandler(this.UCRepairProgressManager_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.palInfo, 0);
            this.Controls.SetChildIndex(this.palData, 0);
            this.palInfo.ResumeLayout(false);
            this.palInfo.PerformLayout();
            this.palData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel palInfo;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtRepairWorker;
        private System.Windows.Forms.Label labRepairWorker;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCarNO;
        private System.Windows.Forms.Label labCarNO;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobStation;
        private System.Windows.Forms.Label labStation;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Panel palData;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private HXCPRepairProgress.UserControl1 RepairProgress;
       
    }
}