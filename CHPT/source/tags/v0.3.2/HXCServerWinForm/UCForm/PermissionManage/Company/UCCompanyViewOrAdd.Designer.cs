namespace HXCServerWinForm.UCForm
{
    partial class UCCompanyViewOrAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCompanyViewOrAdd));
            this.btnCancel = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnSave = new ServiceStationClient.ComponentUI.ButtonEx();
            this.tbName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label20 = new System.Windows.Forms.Label();
            this.tbRemark = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label13 = new System.Windows.Forms.Label();
            this.tbWeb = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label12 = new System.Windows.Forms.Label();
            this.tbFax = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label11 = new System.Windows.Forms.Label();
            this.tbEmail = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label10 = new System.Windows.Forms.Label();
            this.tbTelephone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label9 = new System.Windows.Forms.Label();
            this.tbContract = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label8 = new System.Windows.Forms.Label();
            this.tbLegal_Person = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbFather = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.tbPostCode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.tbAddress = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTown = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cmbCity = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cmbProvince = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.errProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(950, 25);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Caption = "取消";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.DownImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.DownImage")));
            this.btnCancel.Location = new System.Drawing.Point(62, 4);
            this.btnCancel.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.MoveImage")));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.NormalImage")));
            this.btnCancel.Size = new System.Drawing.Size(52, 22);
            this.btnCancel.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Caption = "保存";
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSave.DownImage")));
            this.btnSave.Location = new System.Drawing.Point(4, 4);
            this.btnSave.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSave.MoveImage")));
            this.btnSave.Name = "btnSave";
            this.btnSave.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSave.NormalImage")));
            this.btnSave.Size = new System.Drawing.Size(52, 22);
            this.btnSave.TabIndex = 4;
            // 
            // tbName
            // 
            this.tbName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbName.BackColor = System.Drawing.Color.Transparent;
            this.tbName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbName.ForeImage = null;
            this.tbName.Location = new System.Drawing.Point(403, 62);
            this.tbName.MaxLengh = 32767;
            this.tbName.Multiline = false;
            this.tbName.Name = "tbName";
            this.tbName.Radius = 3;
            this.tbName.ReadOnly = false;
            this.tbName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbName.Size = new System.Drawing.Size(201, 23);
            this.tbName.TabIndex = 84;
            this.tbName.UseSystemPasswordChar = false;
            this.tbName.WaterMark = null;
            this.tbName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(606, 66);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(11, 12);
            this.label20.TabIndex = 109;
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
            this.tbRemark.Location = new System.Drawing.Point(84, 229);
            this.tbRemark.MaxLengh = 32767;
            this.tbRemark.Multiline = true;
            this.tbRemark.Name = "tbRemark";
            this.tbRemark.Radius = 3;
            this.tbRemark.ReadOnly = false;
            this.tbRemark.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbRemark.Size = new System.Drawing.Size(425, 90);
            this.tbRemark.TabIndex = 108;
            this.tbRemark.UseSystemPasswordChar = false;
            this.tbRemark.WaterMark = null;
            this.tbRemark.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(46, 235);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 107;
            this.label13.Text = "备注：";
            // 
            // tbWeb
            // 
            this.tbWeb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbWeb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbWeb.BackColor = System.Drawing.Color.Transparent;
            this.tbWeb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbWeb.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbWeb.ForeImage = null;
            this.tbWeb.Location = new System.Drawing.Point(728, 185);
            this.tbWeb.MaxLengh = 32767;
            this.tbWeb.Multiline = false;
            this.tbWeb.Name = "tbWeb";
            this.tbWeb.Radius = 3;
            this.tbWeb.ReadOnly = false;
            this.tbWeb.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbWeb.Size = new System.Drawing.Size(201, 23);
            this.tbWeb.TabIndex = 106;
            this.tbWeb.UseSystemPasswordChar = false;
            this.tbWeb.WaterMark = null;
            this.tbWeb.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(690, 191);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 105;
            this.label12.Text = "网址：";
            // 
            // tbFax
            // 
            this.tbFax.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbFax.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbFax.BackColor = System.Drawing.Color.Transparent;
            this.tbFax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbFax.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbFax.ForeImage = null;
            this.tbFax.Location = new System.Drawing.Point(403, 185);
            this.tbFax.MaxLengh = 32767;
            this.tbFax.Multiline = false;
            this.tbFax.Name = "tbFax";
            this.tbFax.Radius = 3;
            this.tbFax.ReadOnly = false;
            this.tbFax.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbFax.Size = new System.Drawing.Size(201, 23);
            this.tbFax.TabIndex = 104;
            this.tbFax.UseSystemPasswordChar = false;
            this.tbFax.WaterMark = null;
            this.tbFax.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(365, 191);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 103;
            this.label11.Text = "传真：";
            // 
            // tbEmail
            // 
            this.tbEmail.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbEmail.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbEmail.BackColor = System.Drawing.Color.Transparent;
            this.tbEmail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbEmail.ForeImage = null;
            this.tbEmail.Location = new System.Drawing.Point(84, 185);
            this.tbEmail.MaxLengh = 32767;
            this.tbEmail.Multiline = false;
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Radius = 3;
            this.tbEmail.ReadOnly = false;
            this.tbEmail.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbEmail.Size = new System.Drawing.Size(201, 23);
            this.tbEmail.TabIndex = 102;
            this.tbEmail.UseSystemPasswordChar = false;
            this.tbEmail.WaterMark = null;
            this.tbEmail.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 191);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 101;
            this.label10.Text = "电子邮件：";
            // 
            // tbTelephone
            // 
            this.tbTelephone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbTelephone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbTelephone.BackColor = System.Drawing.Color.Transparent;
            this.tbTelephone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbTelephone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbTelephone.ForeImage = null;
            this.tbTelephone.Location = new System.Drawing.Point(728, 143);
            this.tbTelephone.MaxLengh = 32767;
            this.tbTelephone.Multiline = false;
            this.tbTelephone.Name = "tbTelephone";
            this.tbTelephone.Radius = 3;
            this.tbTelephone.ReadOnly = false;
            this.tbTelephone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbTelephone.Size = new System.Drawing.Size(201, 23);
            this.tbTelephone.TabIndex = 100;
            this.tbTelephone.UseSystemPasswordChar = false;
            this.tbTelephone.WaterMark = null;
            this.tbTelephone.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(666, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 99;
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
            this.tbContract.Location = new System.Drawing.Point(403, 143);
            this.tbContract.MaxLengh = 32767;
            this.tbContract.Multiline = false;
            this.tbContract.Name = "tbContract";
            this.tbContract.Radius = 3;
            this.tbContract.ReadOnly = false;
            this.tbContract.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbContract.Size = new System.Drawing.Size(201, 23);
            this.tbContract.TabIndex = 98;
            this.tbContract.UseSystemPasswordChar = false;
            this.tbContract.WaterMark = null;
            this.tbContract.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(353, 149);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 97;
            this.label8.Text = "联系人：";
            // 
            // tbLegal_Person
            // 
            this.tbLegal_Person.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbLegal_Person.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbLegal_Person.BackColor = System.Drawing.Color.Transparent;
            this.tbLegal_Person.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbLegal_Person.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbLegal_Person.ForeImage = null;
            this.tbLegal_Person.Location = new System.Drawing.Point(84, 143);
            this.tbLegal_Person.MaxLengh = 32767;
            this.tbLegal_Person.Multiline = false;
            this.tbLegal_Person.Name = "tbLegal_Person";
            this.tbLegal_Person.Radius = 3;
            this.tbLegal_Person.ReadOnly = false;
            this.tbLegal_Person.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbLegal_Person.Size = new System.Drawing.Size(201, 23);
            this.tbLegal_Person.TabIndex = 96;
            this.tbLegal_Person.UseSystemPasswordChar = false;
            this.tbLegal_Person.WaterMark = null;
            this.tbLegal_Person.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 95;
            this.label7.Text = "法人/负责人：";
            // 
            // cmbFather
            // 
            this.cmbFather.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFather.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFather.FormattingEnabled = true;
            this.cmbFather.Location = new System.Drawing.Point(728, 62);
            this.cmbFather.Name = "cmbFather";
            this.cmbFather.Size = new System.Drawing.Size(201, 22);
            this.cmbFather.TabIndex = 94;
            // 
            // tbPostCode
            // 
            this.tbPostCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbPostCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbPostCode.BackColor = System.Drawing.Color.Transparent;
            this.tbPostCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbPostCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbPostCode.ForeImage = null;
            this.tbPostCode.Location = new System.Drawing.Point(728, 101);
            this.tbPostCode.MaxLengh = 32767;
            this.tbPostCode.Multiline = false;
            this.tbPostCode.Name = "tbPostCode";
            this.tbPostCode.Radius = 3;
            this.tbPostCode.ReadOnly = false;
            this.tbPostCode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbPostCode.Size = new System.Drawing.Size(201, 23);
            this.tbPostCode.TabIndex = 93;
            this.tbPostCode.UseSystemPasswordChar = false;
            this.tbPostCode.WaterMark = null;
            this.tbPostCode.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(690, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 92;
            this.label5.Text = "邮编：";
            // 
            // tbAddress
            // 
            this.tbAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbAddress.BackColor = System.Drawing.Color.Transparent;
            this.tbAddress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbAddress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbAddress.ForeImage = null;
            this.tbAddress.Location = new System.Drawing.Point(403, 101);
            this.tbAddress.MaxLengh = 32767;
            this.tbAddress.Multiline = false;
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Radius = 3;
            this.tbAddress.ReadOnly = false;
            this.tbAddress.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbAddress.Size = new System.Drawing.Size(201, 23);
            this.tbAddress.TabIndex = 91;
            this.tbAddress.UseSystemPasswordChar = false;
            this.tbAddress.WaterMark = null;
            this.tbAddress.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(341, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 90;
            this.label4.Text = "联系地址：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(666, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 89;
            this.label3.Text = "上级公司：";
            // 
            // cmbTown
            // 
            this.cmbTown.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTown.FormattingEnabled = true;
            this.cmbTown.Location = new System.Drawing.Point(220, 103);
            this.cmbTown.Name = "cmbTown";
            this.cmbTown.Size = new System.Drawing.Size(65, 22);
            this.cmbTown.TabIndex = 88;
            // 
            // cmbCity
            // 
            this.cmbCity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCity.FormattingEnabled = true;
            this.cmbCity.Location = new System.Drawing.Point(152, 103);
            this.cmbCity.Name = "cmbCity";
            this.cmbCity.Size = new System.Drawing.Size(65, 22);
            this.cmbCity.TabIndex = 87;
            this.cmbCity.SelectedIndexChanged += new System.EventHandler(this.cmbCity_SelectedIndexChanged);
            // 
            // cmbProvince
            // 
            this.cmbProvince.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvince.FormattingEnabled = true;
            this.cmbProvince.Location = new System.Drawing.Point(84, 103);
            this.cmbProvince.Name = "cmbProvince";
            this.cmbProvince.Size = new System.Drawing.Size(65, 22);
            this.cmbProvince.TabIndex = 86;
            this.cmbProvince.SelectedIndexChanged += new System.EventHandler(this.cmbProvince_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 85;
            this.label6.Text = "所在地：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(341, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 83;
            this.label2.Text = "公司名称：";
            // 
            // tbCode
            // 
            this.tbCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbCode.BackColor = System.Drawing.Color.Transparent;
            this.tbCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbCode.ForeImage = null;
            this.tbCode.Location = new System.Drawing.Point(84, 62);
            this.tbCode.MaxLengh = 32767;
            this.tbCode.Multiline = false;
            this.tbCode.Name = "tbCode";
            this.tbCode.Radius = 3;
            this.tbCode.ReadOnly = false;
            this.tbCode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbCode.Size = new System.Drawing.Size(201, 23);
            this.tbCode.TabIndex = 82;
            this.tbCode.UseSystemPasswordChar = false;
            this.tbCode.WaterMark = null;
            this.tbCode.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 81;
            this.label1.Text = "公司编码：";
            // 
            // errProvider
            // 
            this.errProvider.ContainerControl = this;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(932, 66);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(11, 12);
            this.label14.TabIndex = 109;
            this.label14.Text = "*";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(287, 67);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(11, 12);
            this.label15.TabIndex = 109;
            this.label15.Text = "*";
            // 
            // UCCompanyViewOrAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tbRemark);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tbWeb);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbFax);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbEmail);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbTelephone);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbContract);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbLegal_Person);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbFather);
            this.Controls.Add(this.tbPostCode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbAddress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbTown);
            this.Controls.Add(this.cmbCity);
            this.Controls.Add(this.cmbProvince);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.label1);
            this.Name = "UCCompanyViewOrAdd";
            this.Size = new System.Drawing.Size(950, 400);
            this.Load += new System.EventHandler(this.UCCompanyViewOrAdd_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.tbCode, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cmbProvince, 0);
            this.Controls.SetChildIndex(this.cmbCity, 0);
            this.Controls.SetChildIndex(this.cmbTown, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.tbAddress, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.tbPostCode, 0);
            this.Controls.SetChildIndex(this.cmbFather, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.tbLegal_Person, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.tbContract, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.tbTelephone, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.tbEmail, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.tbFax, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.tbWeb, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.tbRemark, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.label20, 0);
            this.Controls.SetChildIndex(this.tbName, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ServiceStationClient.ComponentUI.TextBoxEx tbName;
        private System.Windows.Forms.Label label20;
        private ServiceStationClient.ComponentUI.TextBoxEx tbRemark;
        private System.Windows.Forms.Label label13;
        private ServiceStationClient.ComponentUI.TextBoxEx tbWeb;
        private System.Windows.Forms.Label label12;
        private ServiceStationClient.ComponentUI.TextBoxEx tbFax;
        private System.Windows.Forms.Label label11;
        private ServiceStationClient.ComponentUI.TextBoxEx tbEmail;
        private System.Windows.Forms.Label label10;
        private ServiceStationClient.ComponentUI.TextBoxEx tbTelephone;
        private System.Windows.Forms.Label label9;
        private ServiceStationClient.ComponentUI.TextBoxEx tbContract;
        private System.Windows.Forms.Label label8;
        private ServiceStationClient.ComponentUI.TextBoxEx tbLegal_Person;
        private System.Windows.Forms.Label label7;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbFather;
        private ServiceStationClient.ComponentUI.TextBoxEx tbPostCode;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.TextBoxEx tbAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbTown;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbCity;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbProvince;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx tbCode;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ButtonEx btnCancel;
        private ServiceStationClient.ComponentUI.ButtonEx btnSave;
        private System.Windows.Forms.ErrorProvider errProvider;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
    }
}
