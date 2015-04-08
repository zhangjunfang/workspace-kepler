namespace HXCPcClient.UCForm.DataManage.Contacts
{
    partial class UCContactsAddOrEdit
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.rbMan = new System.Windows.Forms.RadioButton();
            this.rbWoman = new System.Windows.Forms.RadioButton();
            this.txtContName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtContPhone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtContEmail = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtContTel = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.cboNation = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cboContPost = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtRemark = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(844, 48);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "姓名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "职务：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(121, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "手机：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(121, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "邮箱：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(121, 251);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "备注：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(482, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "性别：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(482, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "民族：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(482, 167);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "固话：";
            // 
            // rbMan
            // 
            this.rbMan.AutoSize = true;
            this.rbMan.Checked = true;
            this.rbMan.Location = new System.Drawing.Point(545, 80);
            this.rbMan.Name = "rbMan";
            this.rbMan.Size = new System.Drawing.Size(35, 16);
            this.rbMan.TabIndex = 13;
            this.rbMan.TabStop = true;
            this.rbMan.Text = "男";
            this.rbMan.UseVisualStyleBackColor = true;
            // 
            // rbWoman
            // 
            this.rbWoman.AutoSize = true;
            this.rbWoman.Location = new System.Drawing.Point(628, 80);
            this.rbWoman.Name = "rbWoman";
            this.rbWoman.Size = new System.Drawing.Size(35, 16);
            this.rbWoman.TabIndex = 14;
            this.rbWoman.Text = "女";
            this.rbWoman.UseVisualStyleBackColor = true;
            // 
            // txtContName
            // 
            this.txtContName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContName.BackColor = System.Drawing.Color.Transparent;
            this.txtContName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContName.ForeImage = null;
            this.txtContName.Location = new System.Drawing.Point(180, 80);
            this.txtContName.MaxLengh = 32767;
            this.txtContName.Multiline = false;
            this.txtContName.Name = "txtContName";
            this.txtContName.Radius = 3;
            this.txtContName.ReadOnly = false;
            this.txtContName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContName.ShowError = false;
            this.txtContName.Size = new System.Drawing.Size(163, 23);
            this.txtContName.TabIndex = 19;
            this.txtContName.UseSystemPasswordChar = false;
            this.txtContName.Value = "";
            this.txtContName.VerifyCondition = null;
            this.txtContName.VerifyType = null;
            this.txtContName.WaterMark = null;
            this.txtContName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtContPhone
            // 
            this.txtContPhone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContPhone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContPhone.BackColor = System.Drawing.Color.Transparent;
            this.txtContPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContPhone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContPhone.ForeImage = null;
            this.txtContPhone.Location = new System.Drawing.Point(180, 156);
            this.txtContPhone.MaxLengh = 32767;
            this.txtContPhone.Multiline = false;
            this.txtContPhone.Name = "txtContPhone";
            this.txtContPhone.Radius = 3;
            this.txtContPhone.ReadOnly = false;
            this.txtContPhone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContPhone.ShowError = false;
            this.txtContPhone.Size = new System.Drawing.Size(163, 23);
            this.txtContPhone.TabIndex = 21;
            this.txtContPhone.UseSystemPasswordChar = false;
            this.txtContPhone.Value = "";
            this.txtContPhone.VerifyCondition = null;
            this.txtContPhone.VerifyType = null;
            this.txtContPhone.WaterMark = null;
            this.txtContPhone.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtContEmail
            // 
            this.txtContEmail.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContEmail.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContEmail.BackColor = System.Drawing.Color.Transparent;
            this.txtContEmail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContEmail.ForeImage = null;
            this.txtContEmail.Location = new System.Drawing.Point(180, 199);
            this.txtContEmail.MaxLengh = 32767;
            this.txtContEmail.Multiline = false;
            this.txtContEmail.Name = "txtContEmail";
            this.txtContEmail.Radius = 3;
            this.txtContEmail.ReadOnly = false;
            this.txtContEmail.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContEmail.ShowError = false;
            this.txtContEmail.Size = new System.Drawing.Size(163, 23);
            this.txtContEmail.TabIndex = 22;
            this.txtContEmail.UseSystemPasswordChar = false;
            this.txtContEmail.Value = "";
            this.txtContEmail.VerifyCondition = null;
            this.txtContEmail.VerifyType = null;
            this.txtContEmail.WaterMark = null;
            this.txtContEmail.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtContTel
            // 
            this.txtContTel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContTel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContTel.BackColor = System.Drawing.Color.Transparent;
            this.txtContTel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContTel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContTel.ForeImage = null;
            this.txtContTel.Location = new System.Drawing.Point(545, 156);
            this.txtContTel.MaxLengh = 32767;
            this.txtContTel.Multiline = false;
            this.txtContTel.Name = "txtContTel";
            this.txtContTel.Radius = 3;
            this.txtContTel.ReadOnly = false;
            this.txtContTel.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContTel.ShowError = false;
            this.txtContTel.Size = new System.Drawing.Size(137, 23);
            this.txtContTel.TabIndex = 23;
            this.txtContTel.UseSystemPasswordChar = false;
            this.txtContTel.Value = "";
            this.txtContTel.VerifyCondition = null;
            this.txtContTel.VerifyType = null;
            this.txtContTel.WaterMark = null;
            this.txtContTel.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // cboNation
            // 
            this.cboNation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNation.FormattingEnabled = true;
            this.cboNation.Location = new System.Drawing.Point(545, 122);
            this.cboNation.Name = "cboNation";
            this.cboNation.Size = new System.Drawing.Size(137, 22);
            this.cboNation.TabIndex = 24;
            // 
            // cboContPost
            // 
            this.cboContPost.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboContPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboContPost.FormattingEnabled = true;
            this.cboContPost.Location = new System.Drawing.Point(180, 122);
            this.cboContPost.Name = "cboContPost";
            this.cboContPost.Size = new System.Drawing.Size(163, 22);
            this.cboContPost.TabIndex = 25;
            // 
            // txtRemark
            // 
            this.txtRemark.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtRemark.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtRemark.BackColor = System.Drawing.Color.Transparent;
            this.txtRemark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtRemark.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtRemark.ForeImage = null;
            this.txtRemark.Location = new System.Drawing.Point(180, 237);
            this.txtRemark.MaxLengh = 32767;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Radius = 3;
            this.txtRemark.ReadOnly = false;
            this.txtRemark.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtRemark.ShowError = false;
            this.txtRemark.Size = new System.Drawing.Size(504, 77);
            this.txtRemark.TabIndex = 26;
            this.txtRemark.UseSystemPasswordChar = false;
            this.txtRemark.Value = "";
            this.txtRemark.VerifyCondition = null;
            this.txtRemark.VerifyType = null;
            this.txtRemark.WaterMark = null;
            this.txtRemark.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // UCContactsAddOrEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboNation);
            this.Controls.Add(this.cboContPost);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.txtContTel);
            this.Controls.Add(this.txtContEmail);
            this.Controls.Add(this.txtContPhone);
            this.Controls.Add(this.txtContName);
            this.Controls.Add(this.rbWoman);
            this.Controls.Add(this.rbMan);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "UCContactsAddOrEdit";
            this.Size = new System.Drawing.Size(844, 418);
            this.Load += new System.EventHandler(this.UCContactsAddOrEdit_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.rbMan, 0);
            this.Controls.SetChildIndex(this.rbWoman, 0);
            this.Controls.SetChildIndex(this.txtContName, 0);
            this.Controls.SetChildIndex(this.txtContPhone, 0);
            this.Controls.SetChildIndex(this.txtContEmail, 0);
            this.Controls.SetChildIndex(this.txtContTel, 0);
            this.Controls.SetChildIndex(this.txtRemark, 0);
            this.Controls.SetChildIndex(this.cboContPost, 0);
            this.Controls.SetChildIndex(this.cboNation, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rbMan;
        private System.Windows.Forms.RadioButton rbWoman;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContPhone;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContEmail;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContTel;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboNation;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboContPost;
        private ServiceStationClient.ComponentUI.TextBoxEx txtRemark;
    }
}
