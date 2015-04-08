namespace HXCPcClient.UCForm.CustomerService.Member
{
    partial class UCMemberAddOrEdit
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
            this.palQTop = new ServiceStationClient.ComponentUI.PanelEx();
            this.txt_cust_address = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label11 = new System.Windows.Forms.LabelExt();
            this.txt_cust_job = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label9 = new System.Windows.Forms.LabelExt();
            this.txt_cust_phone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label5 = new System.Windows.Forms.LabelExt();
            this.txt_cust_tel = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.LabelExt();
            this.txt_legal_person = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txt_cust_name = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.labCustCode = new System.Windows.Forms.LabelExt();
            this.labOrderStatus = new System.Windows.Forms.LabelExt();
            this.label20 = new System.Windows.Forms.LabelExt();
            this.label13 = new System.Windows.Forms.LabelExt();
            this.dtp_validity_time = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.cbo_member_grade = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label12 = new System.Windows.Forms.LabelExt();
            this.txt_vip_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label6 = new System.Windows.Forms.LabelExt();
            this.rtx_remark = new System.Windows.Forms.RichTextBox();
            this.errprovider = new System.Windows.Forms.ErrorProvider(this.components);
            this.palQTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errprovider)).BeginInit();
            this.SuspendLayout();
            // 
            // palQTop
            // 
            this.palQTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.palQTop.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.palQTop.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.palQTop.BorderWidth = 1;
            this.palQTop.Controls.Add(this.txt_cust_address);
            this.palQTop.Controls.Add(this.label11);
            this.palQTop.Controls.Add(this.txt_cust_job);
            this.palQTop.Controls.Add(this.label9);
            this.palQTop.Controls.Add(this.txt_cust_phone);
            this.palQTop.Controls.Add(this.label5);
            this.palQTop.Controls.Add(this.txt_cust_tel);
            this.palQTop.Controls.Add(this.label4);
            this.palQTop.Controls.Add(this.txt_legal_person);
            this.palQTop.Controls.Add(this.txt_cust_name);
            this.palQTop.Controls.Add(this.labCustCode);
            this.palQTop.Controls.Add(this.labOrderStatus);
            this.palQTop.Controls.Add(this.label20);
            this.palQTop.Controls.Add(this.label13);
            this.palQTop.Controls.Add(this.dtp_validity_time);
            this.palQTop.Controls.Add(this.cbo_member_grade);
            this.palQTop.Controls.Add(this.label12);
            this.palQTop.Controls.Add(this.txt_vip_code);
            this.palQTop.Controls.Add(this.label6);
            this.palQTop.Controls.Add(this.rtx_remark);
            this.palQTop.Curvature = 0;
            this.palQTop.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.palQTop.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.palQTop.Location = new System.Drawing.Point(0, 28);
            this.palQTop.Name = "palQTop";
            this.palQTop.Size = new System.Drawing.Size(1027, 642);
            this.palQTop.TabIndex = 21;
            // 
            // txt_cust_address
            // 
            this.txt_cust_address.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_cust_address.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_cust_address.BackColor = System.Drawing.Color.Transparent;
            this.txt_cust_address.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_cust_address.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_cust_address.ForeImage = null;
            this.txt_cust_address.Location = new System.Drawing.Point(339, 367);
            this.txt_cust_address.MaxLengh = 32767;
            this.txt_cust_address.Multiline = false;
            this.txt_cust_address.Name = "txt_cust_address";
            this.txt_cust_address.Radius = 3;
            this.txt_cust_address.ReadOnly = true;
            this.txt_cust_address.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_cust_address.Size = new System.Drawing.Size(359, 22);
            this.txt_cust_address.TabIndex = 171;
            this.txt_cust_address.UseSystemPasswordChar = false;
            this.txt_cust_address.WaterMark = null;
            this.txt_cust_address.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.label11.Location = new System.Drawing.Point(282, 373);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 170;
            this.label11.Text = "地址：";
            // 
            // txt_cust_job
            // 
            this.txt_cust_job.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_cust_job.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_cust_job.BackColor = System.Drawing.Color.Transparent;
            this.txt_cust_job.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_cust_job.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_cust_job.ForeImage = null;
            this.txt_cust_job.Location = new System.Drawing.Point(105, 403);
            this.txt_cust_job.MaxLengh = 32767;
            this.txt_cust_job.Multiline = false;
            this.txt_cust_job.Name = "txt_cust_job";
            this.txt_cust_job.Radius = 3;
            this.txt_cust_job.ReadOnly = true;
            this.txt_cust_job.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_cust_job.Size = new System.Drawing.Size(125, 22);
            this.txt_cust_job.TabIndex = 169;
            this.txt_cust_job.UseSystemPasswordChar = false;
            this.txt_cust_job.WaterMark = null;
            this.txt_cust_job.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.label9.Location = new System.Drawing.Point(58, 408);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 168;
            this.label9.Text = "职务：";
            // 
            // txt_cust_phone
            // 
            this.txt_cust_phone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_cust_phone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_cust_phone.BackColor = System.Drawing.Color.Transparent;
            this.txt_cust_phone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_cust_phone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_cust_phone.ForeImage = null;
            this.txt_cust_phone.Location = new System.Drawing.Point(105, 367);
            this.txt_cust_phone.MaxLengh = 32767;
            this.txt_cust_phone.Multiline = false;
            this.txt_cust_phone.Name = "txt_cust_phone";
            this.txt_cust_phone.Radius = 3;
            this.txt_cust_phone.ReadOnly = true;
            this.txt_cust_phone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_cust_phone.Size = new System.Drawing.Size(125, 22);
            this.txt_cust_phone.TabIndex = 167;
            this.txt_cust_phone.UseSystemPasswordChar = false;
            this.txt_cust_phone.WaterMark = null;
            this.txt_cust_phone.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.label5.Location = new System.Drawing.Point(22, 373);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 166;
            this.label5.Text = "联系人手机：";
            // 
            // txt_cust_tel
            // 
            this.txt_cust_tel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_cust_tel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_cust_tel.BackColor = System.Drawing.Color.Transparent;
            this.txt_cust_tel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_cust_tel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_cust_tel.ForeImage = null;
            this.txt_cust_tel.Location = new System.Drawing.Point(329, 330);
            this.txt_cust_tel.MaxLengh = 32767;
            this.txt_cust_tel.Multiline = false;
            this.txt_cust_tel.Name = "txt_cust_tel";
            this.txt_cust_tel.Radius = 3;
            this.txt_cust_tel.ReadOnly = true;
            this.txt_cust_tel.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_cust_tel.Size = new System.Drawing.Size(125, 22);
            this.txt_cust_tel.TabIndex = 165;
            this.txt_cust_tel.UseSystemPasswordChar = false;
            this.txt_cust_tel.WaterMark = null;
            this.txt_cust_tel.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.label4.Location = new System.Drawing.Point(282, 336);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 164;
            this.label4.Text = "电话：";
            // 
            // txt_legal_person
            // 
            this.txt_legal_person.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_legal_person.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_legal_person.BackColor = System.Drawing.Color.Transparent;
            this.txt_legal_person.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_legal_person.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_legal_person.ForeImage = null;
            this.txt_legal_person.Location = new System.Drawing.Point(563, 330);
            this.txt_legal_person.MaxLengh = 32767;
            this.txt_legal_person.Multiline = false;
            this.txt_legal_person.Name = "txt_legal_person";
            this.txt_legal_person.Radius = 3;
            this.txt_legal_person.ReadOnly = true;
            this.txt_legal_person.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_legal_person.Size = new System.Drawing.Size(135, 22);
            this.txt_legal_person.TabIndex = 163;
            this.txt_legal_person.UseSystemPasswordChar = false;
            this.txt_legal_person.WaterMark = null;
            this.txt_legal_person.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txt_cust_name
            // 
            this.txt_cust_name.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txt_cust_name.Location = new System.Drawing.Point(107, 330);
            this.txt_cust_name.Name = "txt_cust_name";
            this.txt_cust_name.ReadOnly = true;
            this.txt_cust_name.Size = new System.Drawing.Size(125, 27);
            this.txt_cust_name.TabIndex = 162;
            this.txt_cust_name.ToolTipTitle = "";
            // 
            // labCustCode
            // 
            this.labCustCode.AutoSize = true;
            this.labCustCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.labCustCode.Location = new System.Drawing.Point(34, 336);
            this.labCustCode.Name = "labCustCode";
            this.labCustCode.Size = new System.Drawing.Size(67, 13);
            this.labCustCode.TabIndex = 161;
            this.labCustCode.Text = "客户名称：";
            // 
            // labOrderStatus
            // 
            this.labOrderStatus.AutoSize = true;
            this.labOrderStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.labOrderStatus.Location = new System.Drawing.Point(500, 336);
            this.labOrderStatus.Name = "labOrderStatus";
            this.labOrderStatus.Size = new System.Drawing.Size(55, 13);
            this.labOrderStatus.TabIndex = 160;
            this.labOrderStatus.Text = "联系人：";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.label20.Location = new System.Drawing.Point(65, 72);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(43, 13);
            this.label20.TabIndex = 158;
            this.label20.Text = "备注：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.label13.Location = new System.Drawing.Point(511, 35);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 13);
            this.label13.TabIndex = 141;
            this.label13.Text = "有效期：";
            // 
            // dtp_validity_time
            // 
            this.dtp_validity_time.Location = new System.Drawing.Point(572, 31);
            this.dtp_validity_time.Name = "dtp_validity_time";
            this.dtp_validity_time.ShowFormat = "yyyy-MM-dd";
            this.dtp_validity_time.Size = new System.Drawing.Size(126, 21);
            this.dtp_validity_time.TabIndex = 140;
            this.dtp_validity_time.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // cbo_member_grade
            // 
            this.cbo_member_grade.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_member_grade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_member_grade.FormattingEnabled = true;
            this.cbo_member_grade.Location = new System.Drawing.Point(331, 31);
            this.cbo_member_grade.Name = "cbo_member_grade";
            this.cbo_member_grade.Size = new System.Drawing.Size(125, 21);
            this.cbo_member_grade.TabIndex = 139;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.label12.Location = new System.Drawing.Point(260, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 13);
            this.label12.TabIndex = 138;
            this.label12.Text = "会员等级：";
            // 
            // txt_vip_code
            // 
            this.txt_vip_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_vip_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_vip_code.BackColor = System.Drawing.Color.Transparent;
            this.txt_vip_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_vip_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_vip_code.ForeImage = null;
            this.txt_vip_code.Location = new System.Drawing.Point(107, 30);
            this.txt_vip_code.MaxLengh = 32767;
            this.txt_vip_code.Multiline = false;
            this.txt_vip_code.Name = "txt_vip_code";
            this.txt_vip_code.Radius = 3;
            this.txt_vip_code.ReadOnly = true;
            this.txt_vip_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_vip_code.Size = new System.Drawing.Size(125, 22);
            this.txt_vip_code.TabIndex = 127;
            this.txt_vip_code.UseSystemPasswordChar = false;
            this.txt_vip_code.WaterMark = null;
            this.txt_vip_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.label6.Location = new System.Drawing.Point(44, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 126;
            this.label6.Text = "会员编号：";
            // 
            // rtx_remark
            // 
            this.rtx_remark.Location = new System.Drawing.Point(107, 72);
            this.rtx_remark.MaxLength = 200;
            this.rtx_remark.Name = "rtx_remark";
            this.rtx_remark.Size = new System.Drawing.Size(591, 235);
            this.rtx_remark.TabIndex = 159;
            this.rtx_remark.Text = "";
            // 
            // errprovider
            // 
            this.errprovider.ContainerControl = this;
            // 
            // UCMemberAddOrEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.palQTop);
            this.Name = "UCMemberAddOrEdit";
            this.Size = new System.Drawing.Size(1030, 673);
            this.Controls.SetChildIndex(this.palQTop, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.palQTop.ResumeLayout(false);
            this.palQTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errprovider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx palQTop;
        private System.Windows.Forms.LabelExt label13;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtp_validity_time;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbo_member_grade;
        private System.Windows.Forms.LabelExt label12;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_vip_code;
        private System.Windows.Forms.LabelExt label6;
        private System.Windows.Forms.LabelExt label20;
        private System.Windows.Forms.RichTextBox rtx_remark;
        private System.Windows.Forms.ErrorProvider errprovider;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_cust_address;
        private System.Windows.Forms.LabelExt label11;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_cust_job;
        private System.Windows.Forms.LabelExt label9;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_cust_phone;
        private System.Windows.Forms.LabelExt label5;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_cust_tel;
        private System.Windows.Forms.LabelExt label4;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_legal_person;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txt_cust_name;
        private System.Windows.Forms.LabelExt labCustCode;
        private System.Windows.Forms.LabelExt labOrderStatus;
    }
}
