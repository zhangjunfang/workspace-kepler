namespace HXCServerWinForm.UCForm
{
    partial class UCOrganizeAddOrEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCOrganizeAddOrEdit));
            this.label20 = new System.Windows.Forms.Label();
            this.tbRemark = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label13 = new System.Windows.Forms.Label();
            this.tbName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label10 = new System.Windows.Forms.Label();
            this.tbTelephone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label9 = new System.Windows.Forms.Label();
            this.tbContract = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbOrganize = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCompany = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.errProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(939, 25);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(599, 118);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(11, 12);
            this.label20.TabIndex = 139;
            this.label20.Text = "*";
            // 
            // tbRemark
            // 
            this.tbRemark.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbRemark.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbRemark.BackColor = System.Drawing.Color.Transparent;
            this.tbRemark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbRemark.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbRemark.ForeImage = null;
            this.tbRemark.Location = new System.Drawing.Point(84, 205);
            this.tbRemark.MaxLengh = 32767;
            this.tbRemark.Multiline = true;
            this.tbRemark.Name = "tbRemark";
            this.tbRemark.Radius = 3;
            this.tbRemark.ReadOnly = false;
            this.tbRemark.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbRemark.Size = new System.Drawing.Size(425, 90);
            this.tbRemark.TabIndex = 138;
            this.tbRemark.UseSystemPasswordChar = false;
            this.tbRemark.WaterMark = null;
            this.tbRemark.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(46, 211);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 137;
            this.label13.Text = "备注：";
            // 
            // tbName
            // 
            this.tbName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbName.BackColor = System.Drawing.Color.Transparent;
            this.tbName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbName.ForeImage = null;
            this.tbName.Location = new System.Drawing.Point(396, 112);
            this.tbName.MaxLengh = 32767;
            this.tbName.Multiline = false;
            this.tbName.Name = "tbName";
            this.tbName.Radius = 3;
            this.tbName.ReadOnly = false;
            this.tbName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbName.Size = new System.Drawing.Size(201, 23);
            this.tbName.TabIndex = 132;
            this.tbName.UseSystemPasswordChar = false;
            this.tbName.WaterMark = null;
            this.tbName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(334, 118);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 131;
            this.label10.Text = "组织名称：";
            // 
            // tbTelephone
            // 
            this.tbTelephone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbTelephone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbTelephone.BackColor = System.Drawing.Color.Transparent;
            this.tbTelephone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbTelephone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbTelephone.ForeImage = null;
            this.tbTelephone.Location = new System.Drawing.Point(396, 157);
            this.tbTelephone.MaxLengh = 32767;
            this.tbTelephone.Multiline = false;
            this.tbTelephone.Name = "tbTelephone";
            this.tbTelephone.Radius = 3;
            this.tbTelephone.ReadOnly = false;
            this.tbTelephone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbTelephone.Size = new System.Drawing.Size(201, 23);
            this.tbTelephone.TabIndex = 130;
            this.tbTelephone.UseSystemPasswordChar = false;
            this.tbTelephone.WaterMark = null;
            this.tbTelephone.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(334, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 129;
            this.label9.Text = "联系电话：";
            // 
            // tbContract
            // 
            this.tbContract.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbContract.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbContract.BackColor = System.Drawing.Color.Transparent;
            this.tbContract.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbContract.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbContract.ForeImage = null;
            this.tbContract.Location = new System.Drawing.Point(84, 157);
            this.tbContract.MaxLengh = 32767;
            this.tbContract.Multiline = false;
            this.tbContract.Name = "tbContract";
            this.tbContract.Radius = 3;
            this.tbContract.ReadOnly = false;
            this.tbContract.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbContract.Size = new System.Drawing.Size(201, 23);
            this.tbContract.TabIndex = 128;
            this.tbContract.UseSystemPasswordChar = false;
            this.tbContract.WaterMark = null;
            this.tbContract.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 127;
            this.label8.Text = "联系人：";
            // 
            // cmbOrganize
            // 
            this.cmbOrganize.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbOrganize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrganize.FormattingEnabled = true;
            this.cmbOrganize.Location = new System.Drawing.Point(727, 112);
            this.cmbOrganize.Name = "cmbOrganize";
            this.cmbOrganize.Size = new System.Drawing.Size(201, 22);
            this.cmbOrganize.TabIndex = 124;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(665, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 119;
            this.label3.Text = "上级组织：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 113;
            this.label2.Text = "所属公司：";
            // 
            // tbCode
            // 
            this.tbCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbCode.BackColor = System.Drawing.Color.Transparent;
            this.tbCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbCode.ForeImage = null;
            this.tbCode.Location = new System.Drawing.Point(84, 112);
            this.tbCode.MaxLengh = 32767;
            this.tbCode.Multiline = false;
            this.tbCode.Name = "tbCode";
            this.tbCode.Radius = 3;
            this.tbCode.ReadOnly = false;
            this.tbCode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbCode.Size = new System.Drawing.Size(201, 23);
            this.tbCode.TabIndex = 112;
            this.tbCode.UseSystemPasswordChar = false;
            this.tbCode.WaterMark = null;
            this.tbCode.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 111;
            this.label1.Text = "组织编码：";
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Caption = "删除";
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDelete.DownImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.DownImage")));
            this.btnDelete.Location = new System.Drawing.Point(119, 4);
            this.btnDelete.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.MoveImage")));
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.NormalImage")));
            this.btnDelete.Size = new System.Drawing.Size(52, 22);
            this.btnDelete.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(288, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 140;
            this.label4.Text = "*";
            // 
            // cmbCompany
            // 
            this.cmbCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(84, 68);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(201, 22);
            this.cmbCompany.TabIndex = 141;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(288, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 140;
            this.label5.Text = "*";
            // 
            // errProvider
            // 
            this.errProvider.ContainerControl = this;
            // 
            // UCOrganizeAddOrEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.cmbCompany);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.tbRemark);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbTelephone);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbContract);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbOrganize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.label1);
            this.Name = "UCOrganizeAddOrEdit";
            this.Size = new System.Drawing.Size(939, 400);
            this.Load += new System.EventHandler(this.UCOrganizeAdd_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.tbCode, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmbOrganize, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.tbContract, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.tbTelephone, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.tbRemark, 0);
            this.Controls.SetChildIndex(this.label20, 0);
            this.Controls.SetChildIndex(this.tbName, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.cmbCompany, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label20;
        private ServiceStationClient.ComponentUI.TextBoxEx tbRemark;
        private System.Windows.Forms.Label label13;
        private ServiceStationClient.ComponentUI.TextBoxEx tbName;
        private System.Windows.Forms.Label label10;
        private ServiceStationClient.ComponentUI.TextBoxEx tbTelephone;
        private System.Windows.Forms.Label label9;
        private ServiceStationClient.ComponentUI.TextBoxEx tbContract;
        private System.Windows.Forms.Label label8;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbOrganize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx tbCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbCompany;
        private ServiceStationClient.ComponentUI.ButtonEx btnDelete;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider errProvider;
    }
}
