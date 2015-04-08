namespace HXCServerWinForm.UCForm.AutoBackupSet
{
    partial class frmAutoBackupSetEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAutoBackupSetEdit));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbAcc = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.textBoxEx1 = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txttaskname = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.rbNoBack = new System.Windows.Forms.RadioButton();
            this.rbNoticeUser = new System.Windows.Forms.RadioButton();
            this.pnlUsingHandler = new ServiceStationClient.ComponentUI.PanelEx();
            this.label4 = new System.Windows.Forms.Label();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.numMonth = new System.Windows.Forms.NumericUpDown();
            this.numDay = new System.Windows.Forms.NumericUpDown();
            this.rbEveryDay = new System.Windows.Forms.RadioButton();
            this.rbEveryMonth = new System.Windows.Forms.RadioButton();
            this.rbEveryWeek = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbWeek = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.dtpStart = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.btnSave = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnCancel = new ServiceStationClient.ComponentUI.ButtonEx();
            this.errProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlContainer.SuspendLayout();
            this.pnlUsingHandler.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlContainer.Controls.Add(this.btnCancel);
            this.pnlContainer.Controls.Add(this.btnSave);
            this.pnlContainer.Controls.Add(this.dtpStart);
            this.pnlContainer.Controls.Add(this.panelEx1);
            this.pnlContainer.Controls.Add(this.pnlUsingHandler);
            this.pnlContainer.Controls.Add(this.txttaskname);
            this.pnlContainer.Controls.Add(this.textBoxEx1);
            this.pnlContainer.Controls.Add(this.cmbAcc);
            this.pnlContainer.Controls.Add(this.label2);
            this.pnlContainer.Controls.Add(this.label8);
            this.pnlContainer.Controls.Add(this.label4);
            this.pnlContainer.Controls.Add(this.label3);
            this.pnlContainer.Controls.Add(this.label1);
            this.pnlContainer.Size = new System.Drawing.Size(540, 339);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "帐套名称:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "任务名称:";
            // 
            // cmbAcc
            // 
            this.cmbAcc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAcc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAcc.FormattingEnabled = true;
            this.cmbAcc.Location = new System.Drawing.Point(182, 63);
            this.cmbAcc.Name = "cmbAcc";
            this.cmbAcc.Size = new System.Drawing.Size(250, 22);
            this.cmbAcc.TabIndex = 1;
            // 
            // textBoxEx1
            // 
            this.textBoxEx1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.textBoxEx1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.textBoxEx1.BackColor = System.Drawing.Color.Transparent;
            this.textBoxEx1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.textBoxEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.textBoxEx1.ForeImage = null;
            this.textBoxEx1.InputtingVerifyCondition = null;
            this.textBoxEx1.Location = new System.Drawing.Point(157, -56);
            this.textBoxEx1.MaxLengh = 32767;
            this.textBoxEx1.Multiline = false;
            this.textBoxEx1.Name = "textBoxEx1";
            this.textBoxEx1.Radius = 3;
            this.textBoxEx1.ReadOnly = false;
            this.textBoxEx1.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.textBoxEx1.ShowError = false;
            this.textBoxEx1.Size = new System.Drawing.Size(236, 23);
            this.textBoxEx1.TabIndex = 2;
            this.textBoxEx1.UseSystemPasswordChar = false;
            this.textBoxEx1.Value = "";
            this.textBoxEx1.VerifyCondition = null;
            this.textBoxEx1.VerifyType = null;
            this.textBoxEx1.VerifyTypeName = null;
            this.textBoxEx1.WaterMark = null;
            this.textBoxEx1.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txttaskname
            // 
            this.txttaskname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txttaskname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txttaskname.BackColor = System.Drawing.Color.Transparent;
            this.txttaskname.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txttaskname.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txttaskname.ForeImage = null;
            this.txttaskname.InputtingVerifyCondition = null;
            this.txttaskname.Location = new System.Drawing.Point(182, 32);
            this.txttaskname.MaxLengh = 32767;
            this.txttaskname.Multiline = false;
            this.txttaskname.Name = "txttaskname";
            this.txttaskname.Radius = 3;
            this.txttaskname.ReadOnly = false;
            this.txttaskname.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txttaskname.ShowError = false;
            this.txttaskname.Size = new System.Drawing.Size(250, 23);
            this.txttaskname.TabIndex = 0;
            this.txttaskname.UseSystemPasswordChar = false;
            this.txttaskname.Value = "";
            this.txttaskname.VerifyCondition = null;
            this.txttaskname.VerifyType = null;
            this.txttaskname.VerifyTypeName = null;
            this.txttaskname.WaterMark = null;
            this.txttaskname.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "当用户使用本帐套时:";
            // 
            // rbNoBack
            // 
            this.rbNoBack.AutoSize = true;
            this.rbNoBack.Checked = true;
            this.rbNoBack.Location = new System.Drawing.Point(3, 3);
            this.rbNoBack.Name = "rbNoBack";
            this.rbNoBack.Size = new System.Drawing.Size(83, 16);
            this.rbNoBack.TabIndex = 0;
            this.rbNoBack.TabStop = true;
            this.rbNoBack.Text = "不进行备份";
            this.rbNoBack.UseVisualStyleBackColor = true;
            // 
            // rbNoticeUser
            // 
            this.rbNoticeUser.AutoSize = true;
            this.rbNoticeUser.Location = new System.Drawing.Point(3, 25);
            this.rbNoticeUser.Name = "rbNoticeUser";
            this.rbNoticeUser.Size = new System.Drawing.Size(215, 16);
            this.rbNoticeUser.TabIndex = 1;
            this.rbNoticeUser.Text = "发信息通知用户，五分钟后开始备份";
            this.rbNoticeUser.UseVisualStyleBackColor = true;
            // 
            // pnlUsingHandler
            // 
            this.pnlUsingHandler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlUsingHandler.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlUsingHandler.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlUsingHandler.BorderWidth = 1;
            this.pnlUsingHandler.Controls.Add(this.rbNoBack);
            this.pnlUsingHandler.Controls.Add(this.rbNoticeUser);
            this.pnlUsingHandler.Curvature = 0;
            this.pnlUsingHandler.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlUsingHandler.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlUsingHandler.Location = new System.Drawing.Point(182, 98);
            this.pnlUsingHandler.Name = "pnlUsingHandler";
            this.pnlUsingHandler.Size = new System.Drawing.Size(250, 44);
            this.pnlUsingHandler.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(98, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "自动备份周期:";
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.numMonth);
            this.panelEx1.Controls.Add(this.numDay);
            this.panelEx1.Controls.Add(this.rbEveryDay);
            this.panelEx1.Controls.Add(this.rbEveryMonth);
            this.panelEx1.Controls.Add(this.rbEveryWeek);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label9);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.cmbWeek);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(182, 150);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(250, 90);
            this.panelEx1.TabIndex = 3;
            // 
            // numMonth
            // 
            this.numMonth.Location = new System.Drawing.Point(125, 62);
            this.numMonth.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.numMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMonth.Name = "numMonth";
            this.numMonth.Size = new System.Drawing.Size(72, 21);
            this.numMonth.TabIndex = 5;
            this.numMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numDay
            // 
            this.numDay.Location = new System.Drawing.Point(125, 4);
            this.numDay.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numDay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDay.Name = "numDay";
            this.numDay.Size = new System.Drawing.Size(72, 21);
            this.numDay.TabIndex = 1;
            this.numDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rbEveryDay
            // 
            this.rbEveryDay.AutoSize = true;
            this.rbEveryDay.Checked = true;
            this.rbEveryDay.Location = new System.Drawing.Point(3, 6);
            this.rbEveryDay.Name = "rbEveryDay";
            this.rbEveryDay.Size = new System.Drawing.Size(47, 16);
            this.rbEveryDay.TabIndex = 0;
            this.rbEveryDay.TabStop = true;
            this.rbEveryDay.Text = "每天";
            this.rbEveryDay.UseVisualStyleBackColor = true;
            // 
            // rbEveryMonth
            // 
            this.rbEveryMonth.AutoSize = true;
            this.rbEveryMonth.Location = new System.Drawing.Point(3, 63);
            this.rbEveryMonth.Name = "rbEveryMonth";
            this.rbEveryMonth.Size = new System.Drawing.Size(47, 16);
            this.rbEveryMonth.TabIndex = 4;
            this.rbEveryMonth.Text = "每月";
            this.rbEveryMonth.UseVisualStyleBackColor = true;
            // 
            // rbEveryWeek
            // 
            this.rbEveryWeek.AutoSize = true;
            this.rbEveryWeek.Location = new System.Drawing.Point(3, 33);
            this.rbEveryWeek.Name = "rbEveryWeek";
            this.rbEveryWeek.Size = new System.Drawing.Size(47, 16);
            this.rbEveryWeek.TabIndex = 2;
            this.rbEveryWeek.Text = "每周";
            this.rbEveryWeek.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(199, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "天";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(94, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "每月";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(82, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "每星期";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(106, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "每";
            // 
            // cmbWeek
            // 
            this.cmbWeek.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeek.FormattingEnabled = true;
            this.cmbWeek.Location = new System.Drawing.Point(125, 34);
            this.cmbWeek.Name = "cmbWeek";
            this.cmbWeek.Size = new System.Drawing.Size(72, 22);
            this.cmbWeek.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(74, 251);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "自动备份开始时间:";
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(182, 248);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.ShowFormat = "yyyy-MM-dd HH:mm";
            this.dtpStart.Size = new System.Drawing.Size(149, 21);
            this.dtpStart.TabIndex = 4;
            this.dtpStart.Value = new System.DateTime(2015, 1, 13, 0, 0, 0, 0);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Caption = "保存";
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSave.DownImage")));
            this.btnSave.Location = new System.Drawing.Point(215, 278);
            this.btnSave.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSave.MoveImage")));
            this.btnSave.Name = "btnSave";
            this.btnSave.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSave.NormalImage")));
            this.btnSave.Size = new System.Drawing.Size(60, 26);
            this.btnSave.TabIndex = 5;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Caption = "取消";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.DownImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.DownImage")));
            this.btnCancel.Location = new System.Drawing.Point(281, 278);
            this.btnCancel.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.MoveImage")));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.NormalImage")));
            this.btnCancel.Size = new System.Drawing.Size(60, 26);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errProvider
            // 
            this.errProvider.ContainerControl = this;
            // 
            // frmAutoBackupSetEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 370);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAutoBackupSetEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动备份设置-新增";
            this.Load += new System.EventHandler(this.frmAutoBackupSetEdit_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.pnlUsingHandler.ResumeLayout(false);
            this.pnlUsingHandler.PerformLayout();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.TextBoxEx txttaskname;
        private ServiceStationClient.ComponentUI.TextBoxEx textBoxEx1;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbAcc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbNoBack;
        private ServiceStationClient.ComponentUI.PanelEx pnlUsingHandler;
        private System.Windows.Forms.RadioButton rbNoticeUser;
        private System.Windows.Forms.Label label4;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private System.Windows.Forms.RadioButton rbEveryDay;
        private System.Windows.Forms.RadioButton rbEveryMonth;
        private System.Windows.Forms.RadioButton rbEveryWeek;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbWeek;
        private System.Windows.Forms.Label label8;
        private ServiceStationClient.ComponentUI.ButtonEx btnCancel;
        private ServiceStationClient.ComponentUI.ButtonEx btnSave;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpStart;
        private System.Windows.Forms.NumericUpDown numDay;
        private System.Windows.Forms.NumericUpDown numMonth;
        private System.Windows.Forms.ErrorProvider errProvider;
    }
}