namespace HXCPcClient.UCForm.RepairBusiness.RepairBalance
{
    partial class UCRepairBalanceImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCRepairBalanceImport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlSearch = new ServiceStationClient.ComponentUI.PanelEx();
            this.txtCustomName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpReserveETime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpReserveSTime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labReserveTime = new System.Windows.Forms.Label();
            this.txtPhone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labCustomName = new System.Windows.Forms.Label();
            this.txtCarNO = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labCarNO = new System.Windows.Forms.Label();
            this.btnSubmit = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.palBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnImport = new ServiceStationClient.ComponentUI.ButtonEx();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvRData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.maintain_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reception_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_vin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.engine_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.driver_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.driver_mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maintain_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maintain_payment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fault_describe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.travel_mileage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkman = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.link_man_mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maintain_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlContainer.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.palBottom.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlContainer.Controls.Add(this.panelEx1);
            this.pnlContainer.Controls.Add(this.palBottom);
            this.pnlContainer.Controls.Add(this.pnlSearch);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlSearch.BorderWidth = 1;
            this.pnlSearch.Controls.Add(this.txtCustomName);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.dtpReserveETime);
            this.pnlSearch.Controls.Add(this.dtpReserveSTime);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.labReserveTime);
            this.pnlSearch.Controls.Add(this.txtPhone);
            this.pnlSearch.Controls.Add(this.labCustomName);
            this.pnlSearch.Controls.Add(this.txtCarNO);
            this.pnlSearch.Controls.Add(this.labCarNO);
            this.pnlSearch.Controls.Add(this.btnSubmit);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Curvature = 0;
            this.pnlSearch.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlSearch.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlSearch.Location = new System.Drawing.Point(3, 1);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(676, 81);
            this.pnlSearch.TabIndex = 6;
            // 
            // txtCustomName
            // 
            this.txtCustomName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCustomName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCustomName.BackColor = System.Drawing.Color.Transparent;
            this.txtCustomName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCustomName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCustomName.ForeImage = null;
            this.txtCustomName.InputtingVerifyCondition = null;
            this.txtCustomName.Location = new System.Drawing.Point(437, 15);
            this.txtCustomName.MaxLengh = 32767;
            this.txtCustomName.Multiline = false;
            this.txtCustomName.Name = "txtCustomName";
            this.txtCustomName.Radius = 3;
            this.txtCustomName.ReadOnly = false;
            this.txtCustomName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCustomName.ShowError = false;
            this.txtCustomName.Size = new System.Drawing.Size(102, 23);
            this.txtCustomName.TabIndex = 56;
            this.txtCustomName.UseSystemPasswordChar = false;
            this.txtCustomName.Value = "";
            this.txtCustomName.VerifyCondition = null;
            this.txtCustomName.VerifyType = null;
            this.txtCustomName.VerifyTypeName = null;
            this.txtCustomName.WaterMark = null;
            this.txtCustomName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(364, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 55;
            this.label2.Text = "客户名称：";
            // 
            // dtpReserveETime
            // 
            this.dtpReserveETime.Location = new System.Drawing.Point(223, 50);
            this.dtpReserveETime.Name = "dtpReserveETime";
            this.dtpReserveETime.ShowFormat = "yyyy-MM-dd";
            this.dtpReserveETime.Size = new System.Drawing.Size(121, 21);
            this.dtpReserveETime.TabIndex = 54;
            this.dtpReserveETime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // dtpReserveSTime
            // 
            this.dtpReserveSTime.Location = new System.Drawing.Point(77, 50);
            this.dtpReserveSTime.Name = "dtpReserveSTime";
            this.dtpReserveSTime.ShowFormat = "yyyy-MM-dd";
            this.dtpReserveSTime.Size = new System.Drawing.Size(121, 21);
            this.dtpReserveSTime.TabIndex = 53;
            this.dtpReserveSTime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 52;
            this.label1.Text = "从";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(202, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 51;
            this.label3.Text = "到";
            // 
            // labReserveTime
            // 
            this.labReserveTime.AutoSize = true;
            this.labReserveTime.Location = new System.Drawing.Point(3, 53);
            this.labReserveTime.Name = "labReserveTime";
            this.labReserveTime.Size = new System.Drawing.Size(65, 12);
            this.labReserveTime.TabIndex = 50;
            this.labReserveTime.Text = "完工时间：";
            // 
            // txtPhone
            // 
            this.txtPhone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPhone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPhone.BackColor = System.Drawing.Color.Transparent;
            this.txtPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtPhone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtPhone.ForeImage = null;
            this.txtPhone.InputtingVerifyCondition = null;
            this.txtPhone.Location = new System.Drawing.Point(257, 15);
            this.txtPhone.MaxLengh = 32767;
            this.txtPhone.Multiline = false;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Radius = 3;
            this.txtPhone.ReadOnly = false;
            this.txtPhone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPhone.ShowError = false;
            this.txtPhone.Size = new System.Drawing.Size(102, 23);
            this.txtPhone.TabIndex = 39;
            this.txtPhone.UseSystemPasswordChar = false;
            this.txtPhone.Value = "";
            this.txtPhone.VerifyCondition = null;
            this.txtPhone.VerifyType = null;
            this.txtPhone.VerifyTypeName = null;
            this.txtPhone.WaterMark = null;
            this.txtPhone.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labCustomName
            // 
            this.labCustomName.AutoSize = true;
            this.labCustomName.Location = new System.Drawing.Point(186, 19);
            this.labCustomName.Name = "labCustomName";
            this.labCustomName.Size = new System.Drawing.Size(77, 12);
            this.labCustomName.TabIndex = 38;
            this.labCustomName.Text = "报修人手机：";
            // 
            // txtCarNO
            // 
            this.txtCarNO.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCarNO.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCarNO.BackColor = System.Drawing.Color.Transparent;
            this.txtCarNO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCarNO.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCarNO.ForeImage = null;
            this.txtCarNO.InputtingVerifyCondition = null;
            this.txtCarNO.Location = new System.Drawing.Point(78, 15);
            this.txtCarNO.MaxLengh = 32767;
            this.txtCarNO.Multiline = false;
            this.txtCarNO.Name = "txtCarNO";
            this.txtCarNO.Radius = 3;
            this.txtCarNO.ReadOnly = false;
            this.txtCarNO.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCarNO.ShowError = false;
            this.txtCarNO.Size = new System.Drawing.Size(102, 23);
            this.txtCarNO.TabIndex = 21;
            this.txtCarNO.UseSystemPasswordChar = false;
            this.txtCarNO.Value = "";
            this.txtCarNO.VerifyCondition = null;
            this.txtCarNO.VerifyType = null;
            this.txtCarNO.VerifyTypeName = null;
            this.txtCarNO.WaterMark = null;
            this.txtCarNO.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labCarNO
            // 
            this.labCarNO.AutoSize = true;
            this.labCarNO.Location = new System.Drawing.Point(17, 19);
            this.labCarNO.Name = "labCarNO";
            this.labCarNO.Size = new System.Drawing.Size(53, 12);
            this.labCarNO.TabIndex = 20;
            this.labCarNO.Text = "车牌号：";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Caption = "查询";
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSubmit.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSubmit.Location = new System.Drawing.Point(561, 47);
            this.btnSubmit.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.MoveImage")));
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.NormalImage")));
            this.btnSubmit.Size = new System.Drawing.Size(80, 24);
            this.btnSubmit.TabIndex = 17;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Location = new System.Drawing.Point(561, 17);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(80, 24);
            this.btnClear.TabIndex = 16;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // palBottom
            // 
            this.palBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.palBottom.Controls.Add(this.btnCancel);
            this.palBottom.Controls.Add(this.btnImport);
            this.palBottom.Location = new System.Drawing.Point(2, 329);
            this.palBottom.Name = "palBottom";
            this.palBottom.Size = new System.Drawing.Size(679, 39);
            this.palBottom.TabIndex = 9;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Caption = "取消";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnCancel.Location = new System.Drawing.Point(580, 7);
            this.btnCancel.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.MoveImage")));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.NormalImage")));
            this.btnCancel.Size = new System.Drawing.Size(80, 24);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnImport
            // 
            this.btnImport.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImport.Caption = "导入";
            this.btnImport.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnImport.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnImport.Location = new System.Drawing.Point(488, 7);
            this.btnImport.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnImport.MoveImage")));
            this.btnImport.Name = "btnImport";
            this.btnImport.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnImport.NormalImage")));
            this.btnImport.Size = new System.Drawing.Size(80, 24);
            this.btnImport.TabIndex = 18;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.tabControlEx1);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(3, 86);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(676, 237);
            this.panelEx1.TabIndex = 10;
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Location = new System.Drawing.Point(8, 6);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(665, 224);
            this.tabControlEx1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvRData);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(657, 194);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "维修单信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvRData
            // 
            this.dgvRData.AllowUserToAddRows = false;
            this.dgvRData.AllowUserToDeleteRows = false;
            this.dgvRData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvRData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRData.BackgroundColor = System.Drawing.Color.White;
            this.dgvRData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.maintain_no,
            this.reception_time,
            this.customer_code,
            this.customer_name,
            this.vehicle_no,
            this.vehicle_vin,
            this.vehicle_brand,
            this.vehicle_model,
            this.engine_no,
            this.driver_name,
            this.driver_mobile,
            this.maintain_type,
            this.maintain_payment,
            this.fault_describe,
            this.travel_mileage,
            this.vehicle_color,
            this.linkman,
            this.link_man_mobile,
            this.maintain_id});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRData.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRData.EnableHeadersVisualStyles = false;
            this.dgvRData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvRData.IsCheck = true;
            this.dgvRData.Location = new System.Drawing.Point(3, 3);
            this.dgvRData.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvRData.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvRData.MergeColumnNames")));
            this.dgvRData.MultiSelect = false;
            this.dgvRData.Name = "dgvRData";
            this.dgvRData.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRData.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRData.RowHeadersVisible = false;
            this.dgvRData.RowHeadersWidth = 30;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvRData.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvRData.RowTemplate.Height = 23;
            this.dgvRData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRData.ShowCheckBox = true;
            this.dgvRData.Size = new System.Drawing.Size(651, 188);
            this.dgvRData.TabIndex = 0;
            this.dgvRData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRData_CellDoubleClick);
            this.dgvRData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRData_CellFormatting);
            this.dgvRData.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRData_CellMouseClick);
            this.dgvRData.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvRData_KeyPress);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 30;
            // 
            // maintain_no
            // 
            this.maintain_no.DataPropertyName = "maintain_no";
            this.maintain_no.HeaderText = "维修单号";
            this.maintain_no.Name = "maintain_no";
            this.maintain_no.ReadOnly = true;
            // 
            // reception_time
            // 
            this.reception_time.DataPropertyName = "reception_time";
            this.reception_time.HeaderText = "接待时间";
            this.reception_time.Name = "reception_time";
            this.reception_time.ReadOnly = true;
            // 
            // customer_code
            // 
            this.customer_code.DataPropertyName = "customer_code";
            this.customer_code.HeaderText = "客户编码";
            this.customer_code.Name = "customer_code";
            this.customer_code.ReadOnly = true;
            // 
            // customer_name
            // 
            this.customer_name.DataPropertyName = "customer_name";
            this.customer_name.HeaderText = "客户名称";
            this.customer_name.Name = "customer_name";
            this.customer_name.ReadOnly = true;
            // 
            // vehicle_no
            // 
            this.vehicle_no.DataPropertyName = "vehicle_no";
            this.vehicle_no.HeaderText = "车牌号";
            this.vehicle_no.Name = "vehicle_no";
            this.vehicle_no.ReadOnly = true;
            this.vehicle_no.Width = 150;
            // 
            // vehicle_vin
            // 
            this.vehicle_vin.DataPropertyName = "vehicle_vin";
            this.vehicle_vin.HeaderText = "VIN";
            this.vehicle_vin.Name = "vehicle_vin";
            this.vehicle_vin.ReadOnly = true;
            this.vehicle_vin.Width = 110;
            // 
            // vehicle_brand
            // 
            this.vehicle_brand.DataPropertyName = "vehicle_brand";
            this.vehicle_brand.HeaderText = "车辆品牌";
            this.vehicle_brand.Name = "vehicle_brand";
            this.vehicle_brand.ReadOnly = true;
            // 
            // vehicle_model
            // 
            this.vehicle_model.DataPropertyName = "vehicle_model";
            this.vehicle_model.HeaderText = "车型";
            this.vehicle_model.Name = "vehicle_model";
            this.vehicle_model.ReadOnly = true;
            // 
            // engine_no
            // 
            this.engine_no.DataPropertyName = "engine_no";
            this.engine_no.HeaderText = "发动机号";
            this.engine_no.Name = "engine_no";
            this.engine_no.ReadOnly = true;
            // 
            // driver_name
            // 
            this.driver_name.DataPropertyName = "driver_name";
            this.driver_name.HeaderText = "报修人";
            this.driver_name.Name = "driver_name";
            this.driver_name.ReadOnly = true;
            // 
            // driver_mobile
            // 
            this.driver_mobile.DataPropertyName = "driver_mobile";
            this.driver_mobile.HeaderText = "报修人手机";
            this.driver_mobile.Name = "driver_mobile";
            this.driver_mobile.ReadOnly = true;
            this.driver_mobile.Width = 110;
            // 
            // maintain_type
            // 
            this.maintain_type.DataPropertyName = "maintain_type";
            this.maintain_type.HeaderText = "维修类别";
            this.maintain_type.Name = "maintain_type";
            this.maintain_type.ReadOnly = true;
            // 
            // maintain_payment
            // 
            this.maintain_payment.DataPropertyName = "maintain_payment";
            this.maintain_payment.HeaderText = "维修付费方式";
            this.maintain_payment.Name = "maintain_payment";
            this.maintain_payment.ReadOnly = true;
            this.maintain_payment.Width = 120;
            // 
            // fault_describe
            // 
            this.fault_describe.DataPropertyName = "fault_describe";
            this.fault_describe.HeaderText = "故障描述";
            this.fault_describe.Name = "fault_describe";
            this.fault_describe.ReadOnly = true;
            // 
            // travel_mileage
            // 
            this.travel_mileage.DataPropertyName = "travel_mileage";
            this.travel_mileage.HeaderText = "行驶里程";
            this.travel_mileage.Name = "travel_mileage";
            this.travel_mileage.ReadOnly = true;
            // 
            // vehicle_color
            // 
            this.vehicle_color.DataPropertyName = "vehicle_color";
            this.vehicle_color.HeaderText = "颜色";
            this.vehicle_color.Name = "vehicle_color";
            this.vehicle_color.ReadOnly = true;
            // 
            // linkman
            // 
            this.linkman.DataPropertyName = "linkman";
            this.linkman.HeaderText = "联系人";
            this.linkman.Name = "linkman";
            this.linkman.ReadOnly = true;
            // 
            // link_man_mobile
            // 
            this.link_man_mobile.DataPropertyName = "link_man_mobile";
            this.link_man_mobile.HeaderText = "联系人手机";
            this.link_man_mobile.Name = "link_man_mobile";
            this.link_man_mobile.ReadOnly = true;
            // 
            // maintain_id
            // 
            this.maintain_id.DataPropertyName = "maintain_id";
            this.maintain_id.HeaderText = "maintain_id";
            this.maintain_id.Name = "maintain_id";
            this.maintain_id.ReadOnly = true;
            this.maintain_id.Visible = false;
            // 
            // UCRepairBalanceImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 402);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "UCRepairBalanceImport";
            this.Text = "维修单导入";
            this.Load += new System.EventHandler(this.UCRepairBalanceImport_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.palBottom.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx pnlSearch;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpReserveETime;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpReserveSTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labReserveTime;
        private ServiceStationClient.ComponentUI.TextBoxEx txtPhone;
        private System.Windows.Forms.Label labCustomName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCarNO;
        private System.Windows.Forms.Label labCarNO;
        private ServiceStationClient.ComponentUI.ButtonEx btnSubmit;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Panel palBottom;
        private ServiceStationClient.ComponentUI.ButtonEx btnCancel;
        private ServiceStationClient.ComponentUI.ButtonEx btnImport;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCustomName;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRData;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn maintain_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn reception_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_vin;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_model;
        private System.Windows.Forms.DataGridViewTextBoxColumn engine_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn driver_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn driver_mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn maintain_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn maintain_payment;
        private System.Windows.Forms.DataGridViewTextBoxColumn fault_describe;
        private System.Windows.Forms.DataGridViewTextBoxColumn travel_mileage;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_color;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkman;
        private System.Windows.Forms.DataGridViewTextBoxColumn link_man_mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn maintain_id;
    }
}