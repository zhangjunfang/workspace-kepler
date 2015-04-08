namespace HXCPcClient.UCForm.SysManage.Organization
{
    partial class UCOrganizationAddOrEdit
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
            this.pnl = new System.Windows.Forms.Panel();
            this.pnlRight = new ServiceStationClient.ComponentUI.PanelEx();
            this.txtcontact_telephone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.txtcontact_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label7 = new System.Windows.Forms.Label();
            this.txtremark = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label6 = new System.Windows.Forms.Label();
            this.txtorg_short_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.txtorg_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.txtorg_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.txtparent_id = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLeft = new ServiceStationClient.ComponentUI.PanelEx();
            this.tvCompany = new ServiceStationClient.ComponentUI.TreeViewEx();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnl.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl
            // 
            this.pnl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl.Controls.Add(this.pnlRight);
            this.pnl.Controls.Add(this.pnlLeft);
            this.pnl.Location = new System.Drawing.Point(2, 33);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(1027, 508);
            this.pnl.TabIndex = 10;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlRight.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlRight.Controls.Add(this.txtcontact_telephone);
            this.pnlRight.Controls.Add(this.label4);
            this.pnlRight.Controls.Add(this.txtcontact_name);
            this.pnlRight.Controls.Add(this.label7);
            this.pnlRight.Controls.Add(this.txtremark);
            this.pnlRight.Controls.Add(this.label6);
            this.pnlRight.Controls.Add(this.txtorg_short_name);
            this.pnlRight.Controls.Add(this.label5);
            this.pnlRight.Controls.Add(this.txtorg_name);
            this.pnlRight.Controls.Add(this.label3);
            this.pnlRight.Controls.Add(this.txtorg_code);
            this.pnlRight.Controls.Add(this.label2);
            this.pnlRight.Controls.Add(this.txtparent_id);
            this.pnlRight.Controls.Add(this.label1);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(200, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(827, 508);
            this.pnlRight.TabIndex = 1;
            // 
            // txtcontact_telephone
            // 
            this.txtcontact_telephone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcontact_telephone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcontact_telephone.BackColor = System.Drawing.Color.Transparent;
            this.txtcontact_telephone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcontact_telephone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcontact_telephone.ForeImage = null;
            this.txtcontact_telephone.Location = new System.Drawing.Point(423, 152);
            this.txtcontact_telephone.MaxLengh = 32767;
            this.txtcontact_telephone.Multiline = false;
            this.txtcontact_telephone.Name = "txtcontact_telephone";
            this.txtcontact_telephone.Radius = 3;
            this.txtcontact_telephone.ReadOnly = false;
            this.txtcontact_telephone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcontact_telephone.Size = new System.Drawing.Size(172, 23);
            this.txtcontact_telephone.TabIndex = 34;
            this.txtcontact_telephone.UseSystemPasswordChar = false;
            this.txtcontact_telephone.WaterMark = null;
            this.txtcontact_telephone.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(352, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "联系电话：";
            // 
            // txtcontact_name
            // 
            this.txtcontact_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcontact_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcontact_name.BackColor = System.Drawing.Color.Transparent;
            this.txtcontact_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcontact_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcontact_name.ForeImage = null;
            this.txtcontact_name.Location = new System.Drawing.Point(423, 113);
            this.txtcontact_name.MaxLengh = 15;
            this.txtcontact_name.Multiline = false;
            this.txtcontact_name.Name = "txtcontact_name";
            this.txtcontact_name.Radius = 3;
            this.txtcontact_name.ReadOnly = false;
            this.txtcontact_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcontact_name.Size = new System.Drawing.Size(172, 23);
            this.txtcontact_name.TabIndex = 32;
            this.txtcontact_name.UseSystemPasswordChar = false;
            this.txtcontact_name.WaterMark = null;
            this.txtcontact_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(361, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 31;
            this.label7.Text = "联系人：";
            // 
            // txtremark
            // 
            this.txtremark.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtremark.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtremark.BackColor = System.Drawing.Color.Transparent;
            this.txtremark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtremark.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtremark.ForeImage = null;
            this.txtremark.Location = new System.Drawing.Point(138, 190);
            this.txtremark.MaxLengh = 300;
            this.txtremark.Multiline = false;
            this.txtremark.Name = "txtremark";
            this.txtremark.Radius = 3;
            this.txtremark.ReadOnly = false;
            this.txtremark.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtremark.Size = new System.Drawing.Size(457, 23);
            this.txtremark.TabIndex = 30;
            this.txtremark.UseSystemPasswordChar = false;
            this.txtremark.WaterMark = null;
            this.txtremark.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(90, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 29;
            this.label6.Text = "备注：";
            // 
            // txtorg_short_name
            // 
            this.txtorg_short_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtorg_short_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtorg_short_name.BackColor = System.Drawing.Color.Transparent;
            this.txtorg_short_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtorg_short_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtorg_short_name.ForeImage = null;
            this.txtorg_short_name.Location = new System.Drawing.Point(137, 152);
            this.txtorg_short_name.MaxLengh = 10;
            this.txtorg_short_name.Multiline = false;
            this.txtorg_short_name.Name = "txtorg_short_name";
            this.txtorg_short_name.Radius = 3;
            this.txtorg_short_name.ReadOnly = false;
            this.txtorg_short_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtorg_short_name.Size = new System.Drawing.Size(172, 23);
            this.txtorg_short_name.TabIndex = 26;
            this.txtorg_short_name.UseSystemPasswordChar = false;
            this.txtorg_short_name.WaterMark = null;
            this.txtorg_short_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "组织简称：";
            // 
            // txtorg_name
            // 
            this.txtorg_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtorg_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtorg_name.BackColor = System.Drawing.Color.Transparent;
            this.txtorg_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtorg_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtorg_name.ForeImage = null;
            this.txtorg_name.Location = new System.Drawing.Point(137, 113);
            this.txtorg_name.MaxLengh = 40;
            this.txtorg_name.Multiline = false;
            this.txtorg_name.Name = "txtorg_name";
            this.txtorg_name.Radius = 3;
            this.txtorg_name.ReadOnly = false;
            this.txtorg_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtorg_name.Size = new System.Drawing.Size(172, 23);
            this.txtorg_name.TabIndex = 24;
            this.txtorg_name.UseSystemPasswordChar = false;
            this.txtorg_name.WaterMark = null;
            this.txtorg_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "组织名称：";
            // 
            // txtorg_code
            // 
            this.txtorg_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtorg_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtorg_code.BackColor = System.Drawing.Color.Transparent;
            this.txtorg_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtorg_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtorg_code.ForeImage = null;
            this.txtorg_code.Location = new System.Drawing.Point(137, 72);
            this.txtorg_code.MaxLengh = 32767;
            this.txtorg_code.Multiline = false;
            this.txtorg_code.Name = "txtorg_code";
            this.txtorg_code.Radius = 3;
            this.txtorg_code.ReadOnly = true;
            this.txtorg_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtorg_code.Size = new System.Drawing.Size(172, 23);
            this.txtorg_code.TabIndex = 22;
            this.txtorg_code.UseSystemPasswordChar = false;
            this.txtorg_code.WaterMark = null;
            this.txtorg_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "组织编码：";
            // 
            // txtparent_id
            // 
            this.txtparent_id.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtparent_id.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtparent_id.BackColor = System.Drawing.Color.Transparent;
            this.txtparent_id.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtparent_id.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtparent_id.ForeImage = null;
            this.txtparent_id.Location = new System.Drawing.Point(137, 35);
            this.txtparent_id.MaxLengh = 32767;
            this.txtparent_id.Multiline = false;
            this.txtparent_id.Name = "txtparent_id";
            this.txtparent_id.Radius = 3;
            this.txtparent_id.ReadOnly = true;
            this.txtparent_id.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtparent_id.Size = new System.Drawing.Size(172, 23);
            this.txtparent_id.TabIndex = 20;
            this.txtparent_id.UseSystemPasswordChar = false;
            this.txtparent_id.WaterMark = null;
            this.txtparent_id.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "上级组织名称：";
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlLeft.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlLeft.Controls.Add(this.tvCompany);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(200, 508);
            this.pnlLeft.TabIndex = 0;
            // 
            // tvCompany
            // 
            this.tvCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvCompany.IgnoreAutoCheck = false;
            this.tvCompany.Location = new System.Drawing.Point(0, 11);
            this.tvCompany.Name = "tvCompany";
            this.tvCompany.Size = new System.Drawing.Size(200, 484);
            this.tvCompany.TabIndex = 0;
            this.tvCompany.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCompany_AfterSelect);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // UCOrganizationAddOrEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl);
            this.Name = "UCOrganizationAddOrEdit";
            this.Load += new System.EventHandler(this.UCOrganizationAddOrEdit_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.pnl, 0);
            this.pnl.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl;
        private ServiceStationClient.ComponentUI.PanelEx pnlRight;
        private ServiceStationClient.ComponentUI.PanelEx pnlLeft;
        private ServiceStationClient.ComponentUI.TreeViewEx tvCompany;
        private ServiceStationClient.ComponentUI.TextBoxEx txtorg_name;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.TextBoxEx txtorg_code;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx txtparent_id;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcontact_telephone;
        private System.Windows.Forms.Label label4;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcontact_name;
        private System.Windows.Forms.Label label7;
        private ServiceStationClient.ComponentUI.TextBoxEx txtremark;
        private System.Windows.Forms.Label label6;
        private ServiceStationClient.ComponentUI.TextBoxEx txtorg_short_name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
