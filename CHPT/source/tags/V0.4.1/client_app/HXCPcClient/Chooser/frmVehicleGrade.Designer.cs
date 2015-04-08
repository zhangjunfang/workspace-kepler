namespace HXCPcClient.Chooser
{
    partial class frmVehicleGrade
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVehicleGrade));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnClose = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnSubmit = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnSave = new ServiceStationClient.ComponentUI.ButtonEx();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtCCode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEngineNum = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVIN = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtLicensePlate = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.pageQ = new ServiceStationClient.ComponentUI.WinFormPager();
            this.tcUsers = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tpUsers = new System.Windows.Forms.TabPage();
            this.dgvVehicle = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.license_plate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cont_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.engine_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.v_model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.v_color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.v_brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.turner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.v_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlContainer.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.tcUsers.SuspendLayout();
            this.tpUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehicle)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.pnlContainer.Controls.Add(this.panelEx2);
            this.pnlContainer.Controls.Add(this.btnClose);
            this.pnlContainer.Controls.Add(this.btnSubmit);
            this.pnlContainer.Controls.Add(this.btnSave);
            this.pnlContainer.Controls.Add(this.panelEx1);
            this.pnlContainer.Size = new System.Drawing.Size(802, 430);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Caption = "关闭";
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClose.Location = new System.Drawing.Point(712, 397);
            this.btnClose.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClose.Name = "btnClose";
            this.btnClose.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClose.Size = new System.Drawing.Size(80, 24);
            this.btnClose.TabIndex = 39;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Caption = "确定";
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSubmit.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSubmit.Location = new System.Drawing.Point(618, 397);
            this.btnSubmit.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSubmit.Size = new System.Drawing.Size(80, 24);
            this.btnSubmit.TabIndex = 38;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Caption = "添加新车辆";
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSave.Location = new System.Drawing.Point(11, 396);
            this.btnSave.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSave.Name = "btnSave";
            this.btnSave.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSave.Size = new System.Drawing.Size(80, 24);
            this.btnSave.TabIndex = 37;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.txtCName);
            this.panelEx1.Controls.Add(this.txtCCode);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.txtEngineNum);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.txtVIN);
            this.panelEx1.Controls.Add(this.txtLicensePlate);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(3, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(796, 75);
            this.panelEx1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "客户编码：";
            // 
            // txtCName
            // 
            this.txtCName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCName.BackColor = System.Drawing.Color.Transparent;
            this.txtCName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCName.ForeImage = null;
            this.txtCName.InputtingVerifyCondition = null;
            this.txtCName.Location = new System.Drawing.Point(267, 47);
            this.txtCName.MaxLengh = 32767;
            this.txtCName.Multiline = false;
            this.txtCName.Name = "txtCName";
            this.txtCName.Radius = 3;
            this.txtCName.ReadOnly = false;
            this.txtCName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCName.ShowError = false;
            this.txtCName.Size = new System.Drawing.Size(121, 23);
            this.txtCName.TabIndex = 29;
            this.txtCName.UseSystemPasswordChar = false;
            this.txtCName.Value = "";
            this.txtCName.VerifyCondition = null;
            this.txtCName.VerifyType = null;
            this.txtCName.VerifyTypeName = null;
            this.txtCName.WaterMark = null;
            this.txtCName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtCCode
            // 
            this.txtCCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCCode.BackColor = System.Drawing.Color.Transparent;
            this.txtCCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCCode.ForeImage = null;
            this.txtCCode.InputtingVerifyCondition = null;
            this.txtCCode.Location = new System.Drawing.Point(75, 47);
            this.txtCCode.MaxLengh = 32767;
            this.txtCCode.Multiline = false;
            this.txtCCode.Name = "txtCCode";
            this.txtCCode.Radius = 3;
            this.txtCCode.ReadOnly = false;
            this.txtCCode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCCode.ShowError = false;
            this.txtCCode.Size = new System.Drawing.Size(121, 23);
            this.txtCCode.TabIndex = 28;
            this.txtCCode.UseSystemPasswordChar = false;
            this.txtCCode.Value = "";
            this.txtCCode.VerifyCondition = null;
            this.txtCCode.VerifyType = null;
            this.txtCCode.VerifyTypeName = null;
            this.txtCCode.WaterMark = null;
            this.txtCCode.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(196, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "客户名称：";
            // 
            // txtEngineNum
            // 
            this.txtEngineNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtEngineNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtEngineNum.BackColor = System.Drawing.Color.Transparent;
            this.txtEngineNum.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtEngineNum.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtEngineNum.ForeImage = null;
            this.txtEngineNum.InputtingVerifyCondition = null;
            this.txtEngineNum.Location = new System.Drawing.Point(476, 12);
            this.txtEngineNum.MaxLengh = 32767;
            this.txtEngineNum.Multiline = false;
            this.txtEngineNum.Name = "txtEngineNum";
            this.txtEngineNum.Radius = 3;
            this.txtEngineNum.ReadOnly = false;
            this.txtEngineNum.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtEngineNum.ShowError = false;
            this.txtEngineNum.Size = new System.Drawing.Size(121, 23);
            this.txtEngineNum.TabIndex = 26;
            this.txtEngineNum.UseSystemPasswordChar = false;
            this.txtEngineNum.Value = "";
            this.txtEngineNum.VerifyCondition = null;
            this.txtEngineNum.VerifyType = null;
            this.txtEngineNum.VerifyTypeName = null;
            this.txtEngineNum.WaterMark = null;
            this.txtEngineNum.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(405, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 25;
            this.label2.Text = "发动机号：";
            // 
            // txtVIN
            // 
            this.txtVIN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtVIN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtVIN.BackColor = System.Drawing.Color.Transparent;
            this.txtVIN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtVIN.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtVIN.ForeImage = null;
            this.txtVIN.InputtingVerifyCondition = null;
            this.txtVIN.Location = new System.Drawing.Point(267, 12);
            this.txtVIN.MaxLengh = 32767;
            this.txtVIN.Multiline = false;
            this.txtVIN.Name = "txtVIN";
            this.txtVIN.Radius = 3;
            this.txtVIN.ReadOnly = false;
            this.txtVIN.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtVIN.ShowError = false;
            this.txtVIN.Size = new System.Drawing.Size(121, 23);
            this.txtVIN.TabIndex = 24;
            this.txtVIN.UseSystemPasswordChar = false;
            this.txtVIN.Value = "";
            this.txtVIN.VerifyCondition = null;
            this.txtVIN.VerifyType = null;
            this.txtVIN.VerifyTypeName = null;
            this.txtVIN.WaterMark = null;
            this.txtVIN.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtLicensePlate
            // 
            this.txtLicensePlate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtLicensePlate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtLicensePlate.BackColor = System.Drawing.Color.Transparent;
            this.txtLicensePlate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtLicensePlate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtLicensePlate.ForeImage = null;
            this.txtLicensePlate.InputtingVerifyCondition = null;
            this.txtLicensePlate.Location = new System.Drawing.Point(75, 12);
            this.txtLicensePlate.MaxLengh = 32767;
            this.txtLicensePlate.Multiline = false;
            this.txtLicensePlate.Name = "txtLicensePlate";
            this.txtLicensePlate.Radius = 3;
            this.txtLicensePlate.ReadOnly = false;
            this.txtLicensePlate.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtLicensePlate.ShowError = false;
            this.txtLicensePlate.Size = new System.Drawing.Size(121, 23);
            this.txtLicensePlate.TabIndex = 23;
            this.txtLicensePlate.UseSystemPasswordChar = false;
            this.txtLicensePlate.Value = "";
            this.txtLicensePlate.VerifyCondition = null;
            this.txtLicensePlate.VerifyType = null;
            this.txtLicensePlate.VerifyTypeName = null;
            this.txtLicensePlate.WaterMark = null;
            this.txtLicensePlate.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(226, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "VIN：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "车牌号：";
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSearch.Location = new System.Drawing.Point(709, 43);
            this.btnSearch.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSearch.Size = new System.Drawing.Size(80, 24);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Location = new System.Drawing.Point(709, 8);
            this.btnClear.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.Size = new System.Drawing.Size(80, 24);
            this.btnClear.TabIndex = 8;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.pageQ);
            this.panelEx2.Controls.Add(this.tcUsers);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(3, 79);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(796, 312);
            this.panelEx2.TabIndex = 40;
            // 
            // pageQ
            // 
            this.pageQ.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pageQ.BackColor = System.Drawing.Color.Transparent;
            this.pageQ.BtnTextNext = "下页";
            this.pageQ.BtnTextPrevious = "上页";
            this.pageQ.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.pageQ.Location = new System.Drawing.Point(339, 279);
            this.pageQ.Name = "pageQ";
            this.pageQ.PageCount = 0;
            this.pageQ.PageSize = 15;
            this.pageQ.RecordCount = 0;
            this.pageQ.Size = new System.Drawing.Size(450, 27);
            this.pageQ.TabIndex = 22;
            this.pageQ.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            // 
            // tcUsers
            // 
            this.tcUsers.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tcUsers.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.tcUsers.Controls.Add(this.tpUsers);
            this.tcUsers.Location = new System.Drawing.Point(6, 7);
            this.tcUsers.Name = "tcUsers";
            this.tcUsers.SelectedIndex = 0;
            this.tcUsers.Size = new System.Drawing.Size(787, 266);
            this.tcUsers.TabIndex = 21;
            // 
            // tpUsers
            // 
            this.tpUsers.Controls.Add(this.dgvVehicle);
            this.tpUsers.Location = new System.Drawing.Point(4, 26);
            this.tpUsers.Name = "tpUsers";
            this.tpUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tpUsers.Size = new System.Drawing.Size(779, 236);
            this.tpUsers.TabIndex = 0;
            this.tpUsers.Text = "车辆档案列表";
            this.tpUsers.UseVisualStyleBackColor = true;
            // 
            // dgvVehicle
            // 
            this.dgvVehicle.AllowUserToAddRows = false;
            this.dgvVehicle.AllowUserToDeleteRows = false;
            this.dgvVehicle.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvVehicle.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVehicle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVehicle.BackgroundColor = System.Drawing.Color.White;
            this.dgvVehicle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVehicle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVehicle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVehicle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.license_plate,
            this.vin,
            this.cust_code,
            this.cust_name,
            this.cont_name,
            this.phone,
            this.engine_num,
            this.v_model,
            this.v_color,
            this.v_brand,
            this.turner,
            this.remark,
            this.cust_id,
            this.v_id});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVehicle.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVehicle.EnableHeadersVisualStyles = false;
            this.dgvVehicle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvVehicle.IsCheck = true;
            this.dgvVehicle.Location = new System.Drawing.Point(3, 3);
            this.dgvVehicle.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvVehicle.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvVehicle.MergeColumnNames")));
            this.dgvVehicle.MultiSelect = false;
            this.dgvVehicle.Name = "dgvVehicle";
            this.dgvVehicle.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVehicle.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvVehicle.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvVehicle.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvVehicle.RowTemplate.Height = 23;
            this.dgvVehicle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVehicle.ShowCheckBox = true;
            this.dgvVehicle.Size = new System.Drawing.Size(773, 230);
            this.dgvVehicle.TabIndex = 1;
            this.ToolTip.SetToolTip(this.dgvVehicle, "请双击选择");
            this.dgvVehicle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellDoubleClick);
            this.dgvVehicle.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvVehicle_CellFormatting);
            this.dgvVehicle.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvVehicle_CellMouseClick);
            this.dgvVehicle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvVehicle_KeyPress);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // license_plate
            // 
            this.license_plate.DataPropertyName = "license_plate";
            this.license_plate.FillWeight = 0.2182743F;
            this.license_plate.HeaderText = "车牌号";
            this.license_plate.MinimumWidth = 100;
            this.license_plate.Name = "license_plate";
            this.license_plate.ReadOnly = true;
            // 
            // vin
            // 
            this.vin.DataPropertyName = "vin";
            this.vin.FillWeight = 0.2182743F;
            this.vin.HeaderText = "VIN";
            this.vin.MinimumWidth = 100;
            this.vin.Name = "vin";
            this.vin.ReadOnly = true;
            // 
            // cust_code
            // 
            this.cust_code.DataPropertyName = "cust_code";
            this.cust_code.FillWeight = 2.025225F;
            this.cust_code.HeaderText = "所属客户编码";
            this.cust_code.MinimumWidth = 110;
            this.cust_code.Name = "cust_code";
            this.cust_code.ReadOnly = true;
            // 
            // cust_name
            // 
            this.cust_name.DataPropertyName = "cust_name";
            this.cust_name.FillWeight = 3.888061F;
            this.cust_name.HeaderText = "所属客户";
            this.cust_name.MinimumWidth = 100;
            this.cust_name.Name = "cust_name";
            this.cust_name.ReadOnly = true;
            // 
            // cont_name
            // 
            this.cont_name.DataPropertyName = "cont_name";
            this.cont_name.HeaderText = "联系人";
            this.cont_name.MinimumWidth = 100;
            this.cont_name.Name = "cont_name";
            this.cont_name.ReadOnly = true;
            // 
            // phone
            // 
            this.phone.DataPropertyName = "phone";
            this.phone.HeaderText = "联系人手机";
            this.phone.MinimumWidth = 110;
            this.phone.Name = "phone";
            this.phone.ReadOnly = true;
            // 
            // engine_num
            // 
            this.engine_num.DataPropertyName = "engine_num";
            this.engine_num.FillWeight = 7.671346F;
            this.engine_num.HeaderText = "发动机号";
            this.engine_num.MinimumWidth = 100;
            this.engine_num.Name = "engine_num";
            this.engine_num.ReadOnly = true;
            // 
            // v_model
            // 
            this.v_model.DataPropertyName = "v_model";
            this.v_model.FillWeight = 30.95972F;
            this.v_model.HeaderText = "车型";
            this.v_model.MinimumWidth = 100;
            this.v_model.Name = "v_model";
            this.v_model.ReadOnly = true;
            this.v_model.Visible = false;
            // 
            // v_color
            // 
            this.v_color.DataPropertyName = "v_color";
            this.v_color.FillWeight = 62.65192F;
            this.v_color.HeaderText = "颜色";
            this.v_color.MinimumWidth = 100;
            this.v_color.Name = "v_color";
            this.v_color.ReadOnly = true;
            this.v_color.Visible = false;
            // 
            // v_brand
            // 
            this.v_brand.DataPropertyName = "v_brand";
            this.v_brand.FillWeight = 507.6142F;
            this.v_brand.HeaderText = "车辆品牌";
            this.v_brand.MinimumWidth = 100;
            this.v_brand.Name = "v_brand";
            this.v_brand.ReadOnly = true;
            this.v_brand.Visible = false;
            // 
            // turner
            // 
            this.turner.DataPropertyName = "turner";
            this.turner.FillWeight = 127.0165F;
            this.turner.HeaderText = "车工号";
            this.turner.MinimumWidth = 100;
            this.turner.Name = "turner";
            this.turner.ReadOnly = true;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.FillWeight = 257.7364F;
            this.remark.HeaderText = "备注";
            this.remark.MinimumWidth = 100;
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            // 
            // cust_id
            // 
            this.cust_id.DataPropertyName = "cust_id";
            this.cust_id.HeaderText = "cust_id";
            this.cust_id.Name = "cust_id";
            this.cust_id.ReadOnly = true;
            this.cust_id.Visible = false;
            // 
            // v_id
            // 
            this.v_id.DataPropertyName = "v_id";
            this.v_id.HeaderText = "v_id";
            this.v_id.Name = "v_id";
            this.v_id.ReadOnly = true;
            this.v_id.Visible = false;
            // 
            // frmVehicleGrade
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(805, 463);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVehicleGrade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "车辆档案";
            this.Load += new System.EventHandler(this.frmVehicleGrade_Load);
            this.pnlContainer.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.tcUsers.ResumeLayout(false);
            this.tpUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehicle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.ButtonEx btnClose;
        private ServiceStationClient.ComponentUI.ButtonEx btnSubmit;
        private ServiceStationClient.ComponentUI.ButtonEx btnSave;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private System.Windows.Forms.Label label4;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCCode;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.TextBoxEx txtEngineNum;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx txtVIN;
        private ServiceStationClient.ComponentUI.TextBoxEx txtLicensePlate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.TabControlEx tcUsers;
        private System.Windows.Forms.TabPage tpUsers;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvVehicle;
        private ServiceStationClient.ComponentUI.WinFormPager pageQ;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn license_plate;
        private System.Windows.Forms.DataGridViewTextBoxColumn vin;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn cont_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn engine_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn v_model;
        private System.Windows.Forms.DataGridViewTextBoxColumn v_color;
        private System.Windows.Forms.DataGridViewTextBoxColumn v_brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn turner;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn v_id;

    }
}