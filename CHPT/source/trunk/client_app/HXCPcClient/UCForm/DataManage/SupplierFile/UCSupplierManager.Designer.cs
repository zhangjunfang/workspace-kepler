namespace HXCPcClient.UCForm.DataManage.SupplierFile
{
    partial class UCSupplierManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCSupplierManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ddlState = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.ddlArea = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.ddlCity = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.ddlProvince = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.ddlCompanyNature = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.ddlSupplierType = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.dateTimeStart = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.dateTimeEnd = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.txtSupplierUser = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtSupplierBoss = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtAddress = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtSupplierName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtSupplierNo = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.winFormPager1 = new ServiceStationClient.ComponentUI.WinFormPager();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvSupplierList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.suppID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.credit_account_period = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.credit_line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.credit_class = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.legal_person = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sup_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sup_tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit_properties = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sup_full_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sup_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupplierList)).BeginInit();
            this.tabControlEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(937, 25);
            // 
            // ddlState
            // 
            this.ddlState.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlState.FormattingEnabled = true;
            this.ddlState.Location = new System.Drawing.Point(647, 47);
            this.ddlState.Name = "ddlState";
            this.ddlState.Size = new System.Drawing.Size(164, 22);
            this.ddlState.TabIndex = 24;
            // 
            // ddlArea
            // 
            this.ddlArea.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlArea.FormattingEnabled = true;
            this.ddlArea.Location = new System.Drawing.Point(309, 78);
            this.ddlArea.Name = "ddlArea";
            this.ddlArea.Size = new System.Drawing.Size(99, 22);
            this.ddlArea.TabIndex = 19;
            // 
            // ddlCity
            // 
            this.ddlCity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCity.FormattingEnabled = true;
            this.ddlCity.Location = new System.Drawing.Point(205, 78);
            this.ddlCity.Name = "ddlCity";
            this.ddlCity.Size = new System.Drawing.Size(99, 22);
            this.ddlCity.TabIndex = 18;
            this.ddlCity.SelectedIndexChanged += new System.EventHandler(this.ddlCity_SelectedIndexChanged);
            // 
            // ddlProvince
            // 
            this.ddlProvince.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlProvince.FormattingEnabled = true;
            this.ddlProvince.Location = new System.Drawing.Point(89, 78);
            this.ddlProvince.Name = "ddlProvince";
            this.ddlProvince.Size = new System.Drawing.Size(113, 22);
            this.ddlProvince.TabIndex = 17;
            this.ddlProvince.SelectedIndexChanged += new System.EventHandler(this.ddlProvince_SelectedIndexChanged);
            // 
            // ddlCompanyNature
            // 
            this.ddlCompanyNature.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlCompanyNature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCompanyNature.FormattingEnabled = true;
            this.ddlCompanyNature.Location = new System.Drawing.Point(647, 78);
            this.ddlCompanyNature.Name = "ddlCompanyNature";
            this.ddlCompanyNature.Size = new System.Drawing.Size(164, 22);
            this.ddlCompanyNature.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(31, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "联系人：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(600, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "法人：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(258, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "至";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "创建时间：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(600, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "状态：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "所在地：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(579, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "企业性质：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(321, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "供应商分类：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(319, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "供应商名称：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "供应商编号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.ddlSupplierType);
            this.panelEx1.Controls.Add(this.dateTimeStart);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.ddlCompanyNature);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.ddlState);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.dateTimeEnd);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.txtSupplierUser);
            this.panelEx1.Controls.Add(this.txtSupplierBoss);
            this.panelEx1.Controls.Add(this.txtAddress);
            this.panelEx1.Controls.Add(this.txtSupplierName);
            this.panelEx1.Controls.Add(this.txtSupplierNo);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.ddlArea);
            this.panelEx1.Controls.Add(this.ddlCity);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.ddlProvince);
            this.panelEx1.Controls.Add(this.label8);
            this.panelEx1.Controls.Add(this.label9);
            this.panelEx1.Controls.Add(this.label10);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(16, 28);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(903, 143);
            this.panelEx1.TabIndex = 2;
            // 
            // ddlSupplierType
            // 
            this.ddlSupplierType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlSupplierType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSupplierType.FormattingEnabled = true;
            this.ddlSupplierType.Location = new System.Drawing.Point(399, 47);
            this.ddlSupplierType.Name = "ddlSupplierType";
            this.ddlSupplierType.Size = new System.Drawing.Size(163, 22);
            this.ddlSupplierType.TabIndex = 1;
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeStart.Location = new System.Drawing.Point(89, 109);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.ShowFormat = "yyyy年MM月dd日";
            this.dateTimeStart.Size = new System.Drawing.Size(164, 21);
            this.dateTimeStart.TabIndex = 32;
            this.dateTimeStart.Value = new System.DateTime(2014, 9, 15, 20, 17, 9, 878);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(820, 106);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(60, 26);
            this.btnSearch.TabIndex = 31;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(820, 72);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 30;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeEnd.Location = new System.Drawing.Point(281, 110);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.ShowFormat = "yyyy年MM月dd日";
            this.dateTimeEnd.Size = new System.Drawing.Size(164, 21);
            this.dateTimeEnd.TabIndex = 29;
            this.dateTimeEnd.Value = new System.DateTime(2014, 9, 15, 20, 17, 9, 878);
            // 
            // txtSupplierUser
            // 
            this.txtSupplierUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtSupplierUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtSupplierUser.BackColor = System.Drawing.Color.Transparent;
            this.txtSupplierUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtSupplierUser.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtSupplierUser.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSupplierUser.ForeImage = null;
            this.txtSupplierUser.InputtingVerifyCondition = null;
            this.txtSupplierUser.Location = new System.Drawing.Point(88, 44);
            this.txtSupplierUser.MaxLengh = 32767;
            this.txtSupplierUser.Multiline = false;
            this.txtSupplierUser.Name = "txtSupplierUser";
            this.txtSupplierUser.Radius = 3;
            this.txtSupplierUser.ReadOnly = false;
            this.txtSupplierUser.SelectStart = 0;
            this.txtSupplierUser.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtSupplierUser.ShowError = false;
            this.txtSupplierUser.Size = new System.Drawing.Size(164, 23);
            this.txtSupplierUser.TabIndex = 28;
            this.txtSupplierUser.UseSystemPasswordChar = false;
            this.txtSupplierUser.Value = "";
            this.txtSupplierUser.VerifyCondition = null;
            this.txtSupplierUser.VerifyType = null;
            this.txtSupplierUser.VerifyTypeName = null;
            this.txtSupplierUser.WaterMark = null;
            this.txtSupplierUser.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtSupplierBoss
            // 
            this.txtSupplierBoss.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtSupplierBoss.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtSupplierBoss.BackColor = System.Drawing.Color.Transparent;
            this.txtSupplierBoss.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtSupplierBoss.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtSupplierBoss.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSupplierBoss.ForeImage = null;
            this.txtSupplierBoss.InputtingVerifyCondition = null;
            this.txtSupplierBoss.Location = new System.Drawing.Point(647, 15);
            this.txtSupplierBoss.MaxLengh = 32767;
            this.txtSupplierBoss.Multiline = false;
            this.txtSupplierBoss.Name = "txtSupplierBoss";
            this.txtSupplierBoss.Radius = 3;
            this.txtSupplierBoss.ReadOnly = false;
            this.txtSupplierBoss.SelectStart = 0;
            this.txtSupplierBoss.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtSupplierBoss.ShowError = false;
            this.txtSupplierBoss.Size = new System.Drawing.Size(164, 23);
            this.txtSupplierBoss.TabIndex = 27;
            this.txtSupplierBoss.UseSystemPasswordChar = false;
            this.txtSupplierBoss.Value = "";
            this.txtSupplierBoss.VerifyCondition = null;
            this.txtSupplierBoss.VerifyType = null;
            this.txtSupplierBoss.VerifyTypeName = null;
            this.txtSupplierBoss.WaterMark = null;
            this.txtSupplierBoss.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtAddress
            // 
            this.txtAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtAddress.BackColor = System.Drawing.Color.Transparent;
            this.txtAddress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtAddress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtAddress.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAddress.ForeImage = null;
            this.txtAddress.InputtingVerifyCondition = null;
            this.txtAddress.Location = new System.Drawing.Point(414, 76);
            this.txtAddress.MaxLengh = 32767;
            this.txtAddress.Multiline = false;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Radius = 3;
            this.txtAddress.ReadOnly = false;
            this.txtAddress.SelectStart = 0;
            this.txtAddress.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtAddress.ShowError = false;
            this.txtAddress.Size = new System.Drawing.Size(149, 23);
            this.txtAddress.TabIndex = 26;
            this.txtAddress.UseSystemPasswordChar = false;
            this.txtAddress.Value = "";
            this.txtAddress.VerifyCondition = null;
            this.txtAddress.VerifyType = null;
            this.txtAddress.VerifyTypeName = null;
            this.txtAddress.WaterMark = null;
            this.txtAddress.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtSupplierName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtSupplierName.BackColor = System.Drawing.Color.Transparent;
            this.txtSupplierName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtSupplierName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtSupplierName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSupplierName.ForeImage = null;
            this.txtSupplierName.InputtingVerifyCondition = null;
            this.txtSupplierName.Location = new System.Drawing.Point(399, 15);
            this.txtSupplierName.MaxLengh = 32767;
            this.txtSupplierName.Multiline = false;
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Radius = 3;
            this.txtSupplierName.ReadOnly = false;
            this.txtSupplierName.SelectStart = 0;
            this.txtSupplierName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtSupplierName.ShowError = false;
            this.txtSupplierName.Size = new System.Drawing.Size(164, 23);
            this.txtSupplierName.TabIndex = 25;
            this.txtSupplierName.UseSystemPasswordChar = false;
            this.txtSupplierName.Value = "";
            this.txtSupplierName.VerifyCondition = null;
            this.txtSupplierName.VerifyType = null;
            this.txtSupplierName.VerifyTypeName = null;
            this.txtSupplierName.WaterMark = null;
            this.txtSupplierName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtSupplierNo
            // 
            this.txtSupplierNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtSupplierNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtSupplierNo.BackColor = System.Drawing.Color.Transparent;
            this.txtSupplierNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtSupplierNo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtSupplierNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSupplierNo.ForeImage = null;
            this.txtSupplierNo.InputtingVerifyCondition = null;
            this.txtSupplierNo.Location = new System.Drawing.Point(88, 15);
            this.txtSupplierNo.MaxLengh = 32767;
            this.txtSupplierNo.Multiline = false;
            this.txtSupplierNo.Name = "txtSupplierNo";
            this.txtSupplierNo.Radius = 3;
            this.txtSupplierNo.ReadOnly = false;
            this.txtSupplierNo.SelectStart = 0;
            this.txtSupplierNo.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtSupplierNo.ShowError = false;
            this.txtSupplierNo.Size = new System.Drawing.Size(164, 23);
            this.txtSupplierNo.TabIndex = 3;
            this.txtSupplierNo.UseSystemPasswordChar = false;
            this.txtSupplierNo.Value = "";
            this.txtSupplierNo.VerifyCondition = null;
            this.txtSupplierNo.VerifyType = null;
            this.txtSupplierNo.VerifyTypeName = null;
            this.txtSupplierNo.WaterMark = null;
            this.txtSupplierNo.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // winFormPager1
            // 
            this.winFormPager1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.winFormPager1.BackColor = System.Drawing.Color.Transparent;
            this.winFormPager1.BtnTextNext = "下页";
            this.winFormPager1.BtnTextPrevious = "上页";
            this.winFormPager1.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.winFormPager1.Location = new System.Drawing.Point(472, 1);
            this.winFormPager1.Name = "winFormPager1";
            this.winFormPager1.PageCount = 0;
            this.winFormPager1.PageSize = 15;
            this.winFormPager1.RecordCount = 0;
            this.winFormPager1.Size = new System.Drawing.Size(427, 32);
            this.winFormPager1.TabIndex = 5;
            this.winFormPager1.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.winFormPager1.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.winFormPager1_PageIndexChanged);
            // 
            // panelEx2
            // 
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.winFormPager1);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(16, 523);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(903, 35);
            this.panelEx2.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvSupplierList);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(895, 270);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "供应商信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvSupplierList
            // 
            this.dgvSupplierList.AllowUserToAddRows = false;
            this.dgvSupplierList.AllowUserToDeleteRows = false;
            this.dgvSupplierList.AllowUserToResizeColumns = false;
            this.dgvSupplierList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvSupplierList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSupplierList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSupplierList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSupplierList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvSupplierList.BackgroundColor = System.Drawing.Color.White;
            this.dgvSupplierList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSupplierList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSupplierList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSupplierList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.sup_code,
            this.sup_full_name,
            this.unit_properties,
            this.sup_tel,
            this.sup_type,
            this.legal_person,
            this.credit_class,
            this.credit_line,
            this.credit_account_period,
            this.status,
            this.create_username,
            this.create_time,
            this.suppID});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSupplierList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSupplierList.EnableHeadersVisualStyles = false;
            this.dgvSupplierList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvSupplierList.IsCheck = true;
            this.dgvSupplierList.Location = new System.Drawing.Point(3, 3);
            this.dgvSupplierList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvSupplierList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvSupplierList.MergeColumnNames")));
            this.dgvSupplierList.MultiSelect = false;
            this.dgvSupplierList.Name = "dgvSupplierList";
            this.dgvSupplierList.ReadOnly = true;
            this.dgvSupplierList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvSupplierList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSupplierList.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvSupplierList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSupplierList.RowTemplate.Height = 23;
            this.dgvSupplierList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSupplierList.ShowCheckBox = true;
            this.dgvSupplierList.Size = new System.Drawing.Size(889, 264);
            this.dgvSupplierList.TabIndex = 0;
            this.dgvSupplierList.HeadCheckChanged += new ServiceStationClient.ComponentUI.DataGridViewEx.DelegateOnClick(this.dgvSupplierList_HeadCheckChanged);
            this.dgvSupplierList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSupplierList_CellContentClick);
            this.dgvSupplierList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSupplierList_CellDoubleClick);
            this.dgvSupplierList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvSupplierList_CellFormatting);
            this.dgvSupplierList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSupplierList_CellMouseClick);
            // 
            // suppID
            // 
            this.suppID.DataPropertyName = "sup_id";
            this.suppID.HeaderText = "suppID";
            this.suppID.Name = "suppID";
            this.suppID.ReadOnly = true;
            this.suppID.Visible = false;
            // 
            // create_time
            // 
            this.create_time.DataPropertyName = "create_time";
            this.create_time.FillWeight = 45F;
            this.create_time.HeaderText = "创建时间";
            this.create_time.Name = "create_time";
            this.create_time.ReadOnly = true;
            // 
            // create_username
            // 
            this.create_username.DataPropertyName = "create_username";
            this.create_username.FillWeight = 41.93419F;
            this.create_username.HeaderText = "创建人";
            this.create_username.Name = "create_username";
            this.create_username.ReadOnly = true;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.FillWeight = 41.93419F;
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // credit_account_period
            // 
            this.credit_account_period.DataPropertyName = "credit_account_period";
            this.credit_account_period.FillWeight = 41.93419F;
            this.credit_account_period.HeaderText = "信用账期";
            this.credit_account_period.Name = "credit_account_period";
            this.credit_account_period.ReadOnly = true;
            // 
            // credit_line
            // 
            this.credit_line.DataPropertyName = "credit_line";
            this.credit_line.FillWeight = 41.93419F;
            this.credit_line.HeaderText = "信用额度";
            this.credit_line.Name = "credit_line";
            this.credit_line.ReadOnly = true;
            // 
            // credit_class
            // 
            this.credit_class.DataPropertyName = "credit_class";
            this.credit_class.FillWeight = 35F;
            this.credit_class.HeaderText = "信用等级";
            this.credit_class.Name = "credit_class";
            this.credit_class.ReadOnly = true;
            // 
            // legal_person
            // 
            this.legal_person.DataPropertyName = "legal_person";
            this.legal_person.FillWeight = 104.6734F;
            this.legal_person.HeaderText = "法人";
            this.legal_person.Name = "legal_person";
            this.legal_person.ReadOnly = true;
            this.legal_person.Visible = false;
            // 
            // sup_type
            // 
            this.sup_type.DataPropertyName = "sup_type";
            this.sup_type.FillWeight = 41.93419F;
            this.sup_type.HeaderText = "供应商分类";
            this.sup_type.Name = "sup_type";
            this.sup_type.ReadOnly = true;
            // 
            // sup_tel
            // 
            this.sup_tel.DataPropertyName = "sup_tel";
            this.sup_tel.FillWeight = 41.93419F;
            this.sup_tel.HeaderText = "公司电话";
            this.sup_tel.Name = "sup_tel";
            this.sup_tel.ReadOnly = true;
            // 
            // unit_properties
            // 
            this.unit_properties.DataPropertyName = "unit_properties";
            this.unit_properties.FillWeight = 41.93419F;
            this.unit_properties.HeaderText = "企业性质";
            this.unit_properties.Name = "unit_properties";
            this.unit_properties.ReadOnly = true;
            // 
            // sup_full_name
            // 
            this.sup_full_name.DataPropertyName = "sup_full_name";
            this.sup_full_name.FillWeight = 41.93419F;
            this.sup_full_name.HeaderText = "供应商名称";
            this.sup_full_name.Name = "sup_full_name";
            this.sup_full_name.ReadOnly = true;
            // 
            // sup_code
            // 
            this.sup_code.DataPropertyName = "sup_code";
            this.sup_code.HeaderText = "供应商编号";
            this.sup_code.Name = "sup_code";
            this.sup_code.ReadOnly = true;
            // 
            // colCheck
            // 
            this.colCheck.FillWeight = 10F;
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Location = new System.Drawing.Point(16, 178);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(903, 300);
            this.tabControlEx1.TabIndex = 1;
            // 
            // UCSupplierManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.tabControlEx1);
            this.Name = "UCSupplierManager";
            this.Size = new System.Drawing.Size(937, 561);
            this.Load += new System.EventHandler(this.UCSupplierManager_Load);
            this.Controls.SetChildIndex(this.tabControlEx1, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupplierList)).EndInit();
            this.tabControlEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.ComboBoxEx ddlArea;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlCity;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlProvince;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlCompanyNature;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlState;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.TextBoxEx txtSupplierUser;
        private ServiceStationClient.ComponentUI.TextBoxEx txtSupplierBoss;
        private ServiceStationClient.ComponentUI.TextBoxEx txtAddress;
        private ServiceStationClient.ComponentUI.TextBoxEx txtSupplierName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtSupplierNo;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dateTimeEnd;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dateTimeStart;
        private ServiceStationClient.ComponentUI.WinFormPager winFormPager1;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlSupplierType;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvSupplierList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn sup_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn sup_full_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit_properties;
        private System.Windows.Forms.DataGridViewTextBoxColumn sup_tel;
        private System.Windows.Forms.DataGridViewTextBoxColumn sup_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn legal_person;
        private System.Windows.Forms.DataGridViewTextBoxColumn credit_class;
        private System.Windows.Forms.DataGridViewTextBoxColumn credit_line;
        private System.Windows.Forms.DataGridViewTextBoxColumn credit_account_period;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_username;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn suppID;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
    }
}
