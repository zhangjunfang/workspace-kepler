namespace HXCPcClient.UCForm.DataManage.WorkingTimeFile
{
    partial class UCWorkingTimeAddOrEdit
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
            this.label28 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ddlrepair_type = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtproject_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtwhours_num_a = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtwhours_num_b = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtwhours_num_c = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtquota_price = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtproject_remark = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.radIsWorkTime = new System.Windows.Forms.RadioButton();
            this.radIsQuota = new System.Windows.Forms.RadioButton();
            this.lblproject_num = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1037, 39);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(21, 201);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(101, 12);
            this.label28.TabIndex = 47;
            this.label28.Text = "备注(项目说明)：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(225, 153);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 12);
            this.label12.TabIndex = 41;
            this.label12.Text = "定额单价(元)：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(774, 117);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 12);
            this.label11.TabIndex = 39;
            this.label11.Text = "C类工时数：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(523, 117);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 12);
            this.label10.TabIndex = 38;
            this.label10.Text = "B类工时数：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(243, 116);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 36;
            this.label8.Text = "A类工时数：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(780, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 34;
            this.label6.Text = "项目分类：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(505, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 33;
            this.label5.Text = "维修项目名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 31;
            this.label3.Text = "维修项目编号：";
            // 
            // ddlrepair_type
            // 
            this.ddlrepair_type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlrepair_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlrepair_type.FormattingEnabled = true;
            this.ddlrepair_type.Location = new System.Drawing.Point(851, 74);
            this.ddlrepair_type.Name = "ddlrepair_type";
            this.ddlrepair_type.Size = new System.Drawing.Size(150, 22);
            this.ddlrepair_type.TabIndex = 50;
            // 
            // txtproject_name
            // 
            this.txtproject_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtproject_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtproject_name.BackColor = System.Drawing.Color.Transparent;
            this.txtproject_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtproject_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtproject_name.ForeImage = null;
            this.txtproject_name.InputtingVerifyCondition = null;
            this.txtproject_name.Location = new System.Drawing.Point(597, 72);
            this.txtproject_name.MaxLengh = 40;
            this.txtproject_name.Multiline = false;
            this.txtproject_name.Name = "txtproject_name";
            this.txtproject_name.Radius = 3;
            this.txtproject_name.ReadOnly = false;
            this.txtproject_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtproject_name.ShowError = false;
            this.txtproject_name.Size = new System.Drawing.Size(150, 23);
            this.txtproject_name.TabIndex = 52;
            this.txtproject_name.UseSystemPasswordChar = false;
            this.txtproject_name.Value = "";
            this.txtproject_name.VerifyCondition = null;
            this.txtproject_name.VerifyType = null;
            this.txtproject_name.VerifyTypeName = null;
            this.txtproject_name.WaterMark = null;
            this.txtproject_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtwhours_num_a
            // 
            this.txtwhours_num_a.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtwhours_num_a.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtwhours_num_a.BackColor = System.Drawing.Color.Transparent;
            this.txtwhours_num_a.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtwhours_num_a.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtwhours_num_a.ForeImage = null;
            this.txtwhours_num_a.InputtingVerifyCondition = null;
            this.txtwhours_num_a.Location = new System.Drawing.Point(317, 111);
            this.txtwhours_num_a.MaxLengh = 12;
            this.txtwhours_num_a.Multiline = false;
            this.txtwhours_num_a.Name = "txtwhours_num_a";
            this.txtwhours_num_a.Radius = 3;
            this.txtwhours_num_a.ReadOnly = false;
            this.txtwhours_num_a.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtwhours_num_a.ShowError = false;
            this.txtwhours_num_a.Size = new System.Drawing.Size(150, 23);
            this.txtwhours_num_a.TabIndex = 53;
            this.txtwhours_num_a.UseSystemPasswordChar = false;
            this.txtwhours_num_a.Value = "";
            this.txtwhours_num_a.VerifyCondition = null;
            this.txtwhours_num_a.VerifyType = null;
            this.txtwhours_num_a.VerifyTypeName = null;
            this.txtwhours_num_a.WaterMark = null;
            this.txtwhours_num_a.WaterMarkColor = System.Drawing.Color.Silver;
            this.txtwhours_num_a.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtwhours_num_a_KeyPress);
            // 
            // txtwhours_num_b
            // 
            this.txtwhours_num_b.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtwhours_num_b.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtwhours_num_b.BackColor = System.Drawing.Color.Transparent;
            this.txtwhours_num_b.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtwhours_num_b.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtwhours_num_b.ForeImage = null;
            this.txtwhours_num_b.InputtingVerifyCondition = null;
            this.txtwhours_num_b.Location = new System.Drawing.Point(597, 111);
            this.txtwhours_num_b.MaxLengh = 12;
            this.txtwhours_num_b.Multiline = false;
            this.txtwhours_num_b.Name = "txtwhours_num_b";
            this.txtwhours_num_b.Radius = 3;
            this.txtwhours_num_b.ReadOnly = false;
            this.txtwhours_num_b.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtwhours_num_b.ShowError = false;
            this.txtwhours_num_b.Size = new System.Drawing.Size(150, 23);
            this.txtwhours_num_b.TabIndex = 54;
            this.txtwhours_num_b.UseSystemPasswordChar = false;
            this.txtwhours_num_b.Value = "";
            this.txtwhours_num_b.VerifyCondition = null;
            this.txtwhours_num_b.VerifyType = null;
            this.txtwhours_num_b.VerifyTypeName = null;
            this.txtwhours_num_b.WaterMark = null;
            this.txtwhours_num_b.WaterMarkColor = System.Drawing.Color.Silver;
            this.txtwhours_num_b.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtwhours_num_b_KeyPress);
            // 
            // txtwhours_num_c
            // 
            this.txtwhours_num_c.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtwhours_num_c.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtwhours_num_c.BackColor = System.Drawing.Color.Transparent;
            this.txtwhours_num_c.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtwhours_num_c.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtwhours_num_c.ForeImage = null;
            this.txtwhours_num_c.InputtingVerifyCondition = null;
            this.txtwhours_num_c.Location = new System.Drawing.Point(851, 111);
            this.txtwhours_num_c.MaxLengh = 12;
            this.txtwhours_num_c.Multiline = false;
            this.txtwhours_num_c.Name = "txtwhours_num_c";
            this.txtwhours_num_c.Radius = 3;
            this.txtwhours_num_c.ReadOnly = false;
            this.txtwhours_num_c.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtwhours_num_c.ShowError = false;
            this.txtwhours_num_c.Size = new System.Drawing.Size(150, 23);
            this.txtwhours_num_c.TabIndex = 55;
            this.txtwhours_num_c.UseSystemPasswordChar = false;
            this.txtwhours_num_c.Value = "";
            this.txtwhours_num_c.VerifyCondition = null;
            this.txtwhours_num_c.VerifyType = null;
            this.txtwhours_num_c.VerifyTypeName = null;
            this.txtwhours_num_c.WaterMark = null;
            this.txtwhours_num_c.WaterMarkColor = System.Drawing.Color.Silver;
            this.txtwhours_num_c.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtwhours_num_c_KeyPress);
            // 
            // txtquota_price
            // 
            this.txtquota_price.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtquota_price.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtquota_price.BackColor = System.Drawing.Color.Transparent;
            this.txtquota_price.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtquota_price.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtquota_price.ForeImage = null;
            this.txtquota_price.InputtingVerifyCondition = null;
            this.txtquota_price.Location = new System.Drawing.Point(317, 149);
            this.txtquota_price.MaxLengh = 12;
            this.txtquota_price.Multiline = false;
            this.txtquota_price.Name = "txtquota_price";
            this.txtquota_price.Radius = 3;
            this.txtquota_price.ReadOnly = false;
            this.txtquota_price.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtquota_price.ShowError = false;
            this.txtquota_price.Size = new System.Drawing.Size(150, 23);
            this.txtquota_price.TabIndex = 56;
            this.txtquota_price.UseSystemPasswordChar = false;
            this.txtquota_price.Value = "";
            this.txtquota_price.VerifyCondition = null;
            this.txtquota_price.VerifyType = null;
            this.txtquota_price.VerifyTypeName = null;
            this.txtquota_price.WaterMark = null;
            this.txtquota_price.WaterMarkColor = System.Drawing.Color.Silver;
            this.txtquota_price.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtquota_price_KeyPress);
            // 
            // txtproject_remark
            // 
            this.txtproject_remark.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtproject_remark.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtproject_remark.BackColor = System.Drawing.Color.Transparent;
            this.txtproject_remark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtproject_remark.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtproject_remark.ForeImage = null;
            this.txtproject_remark.InputtingVerifyCondition = null;
            this.txtproject_remark.Location = new System.Drawing.Point(125, 196);
            this.txtproject_remark.MaxLengh = 300;
            this.txtproject_remark.Multiline = false;
            this.txtproject_remark.Name = "txtproject_remark";
            this.txtproject_remark.Radius = 3;
            this.txtproject_remark.ReadOnly = false;
            this.txtproject_remark.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtproject_remark.ShowError = false;
            this.txtproject_remark.Size = new System.Drawing.Size(876, 23);
            this.txtproject_remark.TabIndex = 57;
            this.txtproject_remark.UseSystemPasswordChar = false;
            this.txtproject_remark.Value = "";
            this.txtproject_remark.VerifyCondition = null;
            this.txtproject_remark.VerifyType = null;
            this.txtproject_remark.VerifyTypeName = null;
            this.txtproject_remark.WaterMark = null;
            this.txtproject_remark.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // radIsWorkTime
            // 
            this.radIsWorkTime.AutoSize = true;
            this.radIsWorkTime.Location = new System.Drawing.Point(128, 115);
            this.radIsWorkTime.Name = "radIsWorkTime";
            this.radIsWorkTime.Size = new System.Drawing.Size(47, 16);
            this.radIsWorkTime.TabIndex = 60;
            this.radIsWorkTime.TabStop = true;
            this.radIsWorkTime.Text = "工时";
            this.radIsWorkTime.UseVisualStyleBackColor = true;
            this.radIsWorkTime.CheckedChanged += new System.EventHandler(this.radIsWorkTime_CheckedChanged);
            // 
            // radIsQuota
            // 
            this.radIsQuota.AutoSize = true;
            this.radIsQuota.Location = new System.Drawing.Point(128, 151);
            this.radIsQuota.Name = "radIsQuota";
            this.radIsQuota.Size = new System.Drawing.Size(47, 16);
            this.radIsQuota.TabIndex = 61;
            this.radIsQuota.TabStop = true;
            this.radIsQuota.Text = "定额";
            this.radIsQuota.UseVisualStyleBackColor = true;
            this.radIsQuota.CheckedChanged += new System.EventHandler(this.radIsQuota_CheckedChanged);
            // 
            // lblproject_num
            // 
            this.lblproject_num.AutoSize = true;
            this.lblproject_num.Location = new System.Drawing.Point(315, 77);
            this.lblproject_num.Name = "lblproject_num";
            this.lblproject_num.Size = new System.Drawing.Size(11, 12);
            this.lblproject_num.TabIndex = 62;
            this.lblproject_num.Text = ".";
            // 
            // UCWorkingTimeAddOrEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblproject_num);
            this.Controls.Add(this.radIsQuota);
            this.Controls.Add(this.txtproject_remark);
            this.Controls.Add(this.radIsWorkTime);
            this.Controls.Add(this.txtquota_price);
            this.Controls.Add(this.txtwhours_num_c);
            this.Controls.Add(this.txtwhours_num_b);
            this.Controls.Add(this.txtwhours_num_a);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.txtproject_name);
            this.Controls.Add(this.ddlrepair_type);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Name = "UCWorkingTimeAddOrEdit";
            this.Size = new System.Drawing.Size(1037, 452);
            this.Load += new System.EventHandler(this.UCWorkingTimeAddOrEdit_Load);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.ddlrepair_type, 0);
            this.Controls.SetChildIndex(this.txtproject_name, 0);
            this.Controls.SetChildIndex(this.label28, 0);
            this.Controls.SetChildIndex(this.txtwhours_num_a, 0);
            this.Controls.SetChildIndex(this.txtwhours_num_b, 0);
            this.Controls.SetChildIndex(this.txtwhours_num_c, 0);
            this.Controls.SetChildIndex(this.txtquota_price, 0);
            this.Controls.SetChildIndex(this.radIsWorkTime, 0);
            this.Controls.SetChildIndex(this.txtproject_remark, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.radIsQuota, 0);
            this.Controls.SetChildIndex(this.lblproject_num, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlrepair_type;
        private ServiceStationClient.ComponentUI.TextBoxEx txtproject_name;
        private ServiceStationClient.ComponentUI.TextBoxEx txtwhours_num_a;
        private ServiceStationClient.ComponentUI.TextBoxEx txtwhours_num_b;
        private ServiceStationClient.ComponentUI.TextBoxEx txtwhours_num_c;
        private ServiceStationClient.ComponentUI.TextBoxEx txtquota_price;
        private ServiceStationClient.ComponentUI.TextBoxEx txtproject_remark;
        private System.Windows.Forms.RadioButton radIsWorkTime;
        private System.Windows.Forms.RadioButton radIsQuota;
        private System.Windows.Forms.Label lblproject_num;
    }
}
