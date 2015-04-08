namespace HXCPcClient.Chooser
{
    partial class frmContactsQuickADD
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
            this.btnSave = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnColse = new ServiceStationClient.ComponentUI.ButtonEx();
            this.txtContTel = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtContPhone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtContName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlContainer.Controls.Add(this.txtContTel);
            this.pnlContainer.Controls.Add(this.label8);
            this.pnlContainer.Controls.Add(this.btnSave);
            this.pnlContainer.Controls.Add(this.txtContPhone);
            this.pnlContainer.Controls.Add(this.btnColse);
            this.pnlContainer.Controls.Add(this.label3);
            this.pnlContainer.Controls.Add(this.txtContName);
            this.pnlContainer.Controls.Add(this.label1);
            this.pnlContainer.Size = new System.Drawing.Size(701, 93);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Caption = "保存并添加";
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSave.Location = new System.Drawing.Point(514, 58);
            this.btnSave.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSave.Name = "btnSave";
            this.btnSave.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSave.Size = new System.Drawing.Size(90, 24);
            this.btnSave.TabIndex = 32;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnColse
            // 
            this.btnColse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColse.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnColse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnColse.Caption = "取消";
            this.btnColse.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnColse.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnColse.Location = new System.Drawing.Point(610, 58);
            this.btnColse.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnColse.Name = "btnColse";
            this.btnColse.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnColse.Size = new System.Drawing.Size(80, 24);
            this.btnColse.TabIndex = 31;
            this.btnColse.Click += new System.EventHandler(this.btnColse_Click);
            // 
            // txtContTel
            // 
            this.txtContTel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContTel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContTel.BackColor = System.Drawing.Color.Transparent;
            this.txtContTel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContTel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContTel.ForeImage = null;
            this.txtContTel.InputtingVerifyCondition = null;
            this.txtContTel.Location = new System.Drawing.Point(545, 20);
            this.txtContTel.MaxLengh = 32767;
            this.txtContTel.Multiline = false;
            this.txtContTel.Name = "txtContTel";
            this.txtContTel.Radius = 3;
            this.txtContTel.ReadOnly = false;
            this.txtContTel.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContTel.ShowError = false;
            this.txtContTel.Size = new System.Drawing.Size(137, 23);
            this.txtContTel.TabIndex = 29;
            this.txtContTel.UseSystemPasswordChar = false;
            this.txtContTel.Value = "";
            this.txtContTel.VerifyCondition = null;
            this.txtContTel.VerifyType = null;
            this.txtContTel.VerifyTypeName = null;
            this.txtContTel.WaterMark = null;
            this.txtContTel.WaterMarkColor = System.Drawing.Color.Silver;
            this.txtContTel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContTel_KeyPress);
            // 
            // txtContPhone
            // 
            this.txtContPhone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContPhone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContPhone.BackColor = System.Drawing.Color.Transparent;
            this.txtContPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContPhone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContPhone.ForeImage = null;
            this.txtContPhone.InputtingVerifyCondition = null;
            this.txtContPhone.Location = new System.Drawing.Point(310, 20);
            this.txtContPhone.MaxLengh = 32767;
            this.txtContPhone.Multiline = false;
            this.txtContPhone.Name = "txtContPhone";
            this.txtContPhone.Radius = 3;
            this.txtContPhone.ReadOnly = false;
            this.txtContPhone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContPhone.ShowError = false;
            this.txtContPhone.Size = new System.Drawing.Size(163, 23);
            this.txtContPhone.TabIndex = 28;
            this.txtContPhone.UseSystemPasswordChar = false;
            this.txtContPhone.Value = "";
            this.txtContPhone.VerifyCondition = null;
            this.txtContPhone.VerifyType = null;
            this.txtContPhone.VerifyTypeName = null;
            this.txtContPhone.WaterMark = null;
            this.txtContPhone.WaterMarkColor = System.Drawing.Color.Silver;
            this.txtContPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContPhone_KeyPress);
            // 
            // txtContName
            // 
            this.txtContName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContName.BackColor = System.Drawing.Color.Transparent;
            this.txtContName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContName.ForeImage = null;
            this.txtContName.InputtingVerifyCondition = null;
            this.txtContName.Location = new System.Drawing.Point(63, 20);
            this.txtContName.MaxLengh = 32767;
            this.txtContName.Multiline = false;
            this.txtContName.Name = "txtContName";
            this.txtContName.Radius = 3;
            this.txtContName.ReadOnly = false;
            this.txtContName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContName.ShowError = false;
            this.txtContName.Size = new System.Drawing.Size(163, 23);
            this.txtContName.TabIndex = 27;
            this.txtContName.UseSystemPasswordChar = false;
            this.txtContName.Value = "";
            this.txtContName.VerifyCondition = null;
            this.txtContName.VerifyType = null;
            this.txtContName.VerifyTypeName = null;
            this.txtContName.WaterMark = null;
            this.txtContName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(482, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "固话：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "姓名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "手机：";
            // 
            // frmContactsQuickADD
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnColse;
            this.ClientSize = new System.Drawing.Size(703, 124);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmContactsQuickADD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "快速添加联系人";
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.ButtonEx btnSave;
        private ServiceStationClient.ComponentUI.ButtonEx btnColse;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContPhone;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContName;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContTel;
        private System.Windows.Forms.Label label8;
    }
}