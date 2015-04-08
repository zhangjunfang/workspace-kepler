namespace HXCServerWinForm.UCForm
{
    partial class frmPersionSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPersionSet));
            this.errProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtloginid = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.txtusername = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.txtoldpwd = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.txtnewpwd = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.txtnewpwd_again = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.btnSummit = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnCancel = new ServiceStationClient.ComponentUI.ButtonEx();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.pnlContainer.Controls.Add(this.btnCancel);
            this.pnlContainer.Controls.Add(this.btnSummit);
            this.pnlContainer.Controls.Add(this.txtnewpwd_again);
            this.pnlContainer.Controls.Add(this.label5);
            this.pnlContainer.Controls.Add(this.txtnewpwd);
            this.pnlContainer.Controls.Add(this.label4);
            this.pnlContainer.Controls.Add(this.txtoldpwd);
            this.pnlContainer.Controls.Add(this.label3);
            this.pnlContainer.Controls.Add(this.txtusername);
            this.pnlContainer.Controls.Add(this.label2);
            this.pnlContainer.Controls.Add(this.txtloginid);
            this.pnlContainer.Controls.Add(this.label1);
            this.pnlContainer.Size = new System.Drawing.Size(376, 255);
            // 
            // errProvider
            // 
            this.errProvider.ContainerControl = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "账号：";
            // 
            // txtloginid
            // 
            this.txtloginid.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtloginid.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtloginid.BackColor = System.Drawing.Color.Transparent;
            this.txtloginid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtloginid.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtloginid.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtloginid.ForeImage = null;
            this.txtloginid.InputtingVerifyCondition = null;
            this.txtloginid.Location = new System.Drawing.Point(136, 32);
            this.txtloginid.MaxLengh = 32767;
            this.txtloginid.Multiline = false;
            this.txtloginid.Name = "txtloginid";
            this.txtloginid.Radius = 3;
            this.txtloginid.ReadOnly = true;
            this.txtloginid.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtloginid.ShowError = false;
            this.txtloginid.Size = new System.Drawing.Size(169, 23);
            this.txtloginid.TabIndex = 1;
            this.txtloginid.UseSystemPasswordChar = false;
            this.txtloginid.Value = "";
            this.txtloginid.VerifyCondition = null;
            this.txtloginid.VerifyType = null;
            this.txtloginid.VerifyTypeName = null;
            this.txtloginid.WaterMark = null;
            this.txtloginid.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "姓名：";
            // 
            // txtusername
            // 
            this.txtusername.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtusername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtusername.BackColor = System.Drawing.Color.Transparent;
            this.txtusername.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtusername.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtusername.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtusername.ForeImage = null;
            this.txtusername.InputtingVerifyCondition = null;
            this.txtusername.Location = new System.Drawing.Point(136, 61);
            this.txtusername.MaxLengh = 32767;
            this.txtusername.Multiline = false;
            this.txtusername.Name = "txtusername";
            this.txtusername.Radius = 3;
            this.txtusername.ReadOnly = false;
            this.txtusername.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtusername.ShowError = false;
            this.txtusername.Size = new System.Drawing.Size(169, 23);
            this.txtusername.TabIndex = 1;
            this.txtusername.UseSystemPasswordChar = false;
            this.txtusername.Value = "";
            this.txtusername.VerifyCondition = null;
            this.txtusername.VerifyType = null;
            this.txtusername.VerifyTypeName = null;
            this.txtusername.WaterMark = null;
            this.txtusername.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "旧密码：";
            // 
            // txtoldpwd
            // 
            this.txtoldpwd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtoldpwd.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtoldpwd.BackColor = System.Drawing.Color.Transparent;
            this.txtoldpwd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtoldpwd.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtoldpwd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtoldpwd.ForeImage = null;
            this.txtoldpwd.InputtingVerifyCondition = null;
            this.txtoldpwd.Location = new System.Drawing.Point(136, 91);
            this.txtoldpwd.MaxLengh = 32767;
            this.txtoldpwd.Multiline = false;
            this.txtoldpwd.Name = "txtoldpwd";
            this.txtoldpwd.Radius = 3;
            this.txtoldpwd.ReadOnly = false;
            this.txtoldpwd.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtoldpwd.ShowError = false;
            this.txtoldpwd.Size = new System.Drawing.Size(169, 23);
            this.txtoldpwd.TabIndex = 1;
            this.txtoldpwd.UseSystemPasswordChar = true;
            this.txtoldpwd.Value = "";
            this.txtoldpwd.VerifyCondition = null;
            this.txtoldpwd.VerifyType = null;
            this.txtoldpwd.VerifyTypeName = null;
            this.txtoldpwd.WaterMark = null;
            this.txtoldpwd.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(81, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "新密码：";
            // 
            // txtnewpwd
            // 
            this.txtnewpwd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtnewpwd.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtnewpwd.BackColor = System.Drawing.Color.Transparent;
            this.txtnewpwd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtnewpwd.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtnewpwd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtnewpwd.ForeImage = null;
            this.txtnewpwd.InputtingVerifyCondition = null;
            this.txtnewpwd.Location = new System.Drawing.Point(136, 120);
            this.txtnewpwd.MaxLengh = 32767;
            this.txtnewpwd.Multiline = false;
            this.txtnewpwd.Name = "txtnewpwd";
            this.txtnewpwd.Radius = 3;
            this.txtnewpwd.ReadOnly = false;
            this.txtnewpwd.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtnewpwd.ShowError = false;
            this.txtnewpwd.Size = new System.Drawing.Size(169, 23);
            this.txtnewpwd.TabIndex = 1;
            this.txtnewpwd.UseSystemPasswordChar = true;
            this.txtnewpwd.Value = "";
            this.txtnewpwd.VerifyCondition = null;
            this.txtnewpwd.VerifyType = null;
            this.txtnewpwd.VerifyTypeName = null;
            this.txtnewpwd.WaterMark = null;
            this.txtnewpwd.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(57, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "确认新密码：";
            // 
            // txtnewpwd_again
            // 
            this.txtnewpwd_again.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtnewpwd_again.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtnewpwd_again.BackColor = System.Drawing.Color.Transparent;
            this.txtnewpwd_again.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtnewpwd_again.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtnewpwd_again.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtnewpwd_again.ForeImage = null;
            this.txtnewpwd_again.InputtingVerifyCondition = null;
            this.txtnewpwd_again.Location = new System.Drawing.Point(136, 149);
            this.txtnewpwd_again.MaxLengh = 32767;
            this.txtnewpwd_again.Multiline = false;
            this.txtnewpwd_again.Name = "txtnewpwd_again";
            this.txtnewpwd_again.Radius = 3;
            this.txtnewpwd_again.ReadOnly = false;
            this.txtnewpwd_again.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtnewpwd_again.ShowError = false;
            this.txtnewpwd_again.Size = new System.Drawing.Size(169, 23);
            this.txtnewpwd_again.TabIndex = 1;
            this.txtnewpwd_again.UseSystemPasswordChar = true;
            this.txtnewpwd_again.Value = "";
            this.txtnewpwd_again.VerifyCondition = null;
            this.txtnewpwd_again.VerifyType = null;
            this.txtnewpwd_again.VerifyTypeName = null;
            this.txtnewpwd_again.WaterMark = null;
            this.txtnewpwd_again.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // btnSummit
            // 
            this.btnSummit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSummit.BackgroundImage")));
            this.btnSummit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSummit.Caption = "提交";
            this.btnSummit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSummit.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSummit.DownImage")));
            this.btnSummit.Location = new System.Drawing.Point(136, 196);
            this.btnSummit.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSummit.MoveImage")));
            this.btnSummit.Name = "btnSummit";
            this.btnSummit.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSummit.NormalImage")));
            this.btnSummit.Size = new System.Drawing.Size(60, 26);
            this.btnSummit.TabIndex = 2;
            this.btnSummit.Click += new System.EventHandler(this.btnSummit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Caption = "取消";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.DownImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.DownImage")));
            this.btnCancel.Location = new System.Drawing.Point(202, 196);
            this.btnCancel.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.MoveImage")));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.NormalImage")));
            this.btnCancel.Size = new System.Drawing.Size(60, 26);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmPersionSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 286);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(378, 286);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(378, 286);
            this.Name = "frmPersionSet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "个人资料设置";
            this.Load += new System.EventHandler(this.frmPersionSet_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errProvider;
        private ServiceStationClient.ComponentUI.TextBoxEx txtoldpwd;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.TextBoxEx txtusername;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx txtloginid;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ButtonEx btnCancel;
        private ServiceStationClient.ComponentUI.ButtonEx btnSummit;
        private ServiceStationClient.ComponentUI.TextBoxEx txtnewpwd_again;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.TextBoxEx txtnewpwd;
        private System.Windows.Forms.Label label4;
    }
}