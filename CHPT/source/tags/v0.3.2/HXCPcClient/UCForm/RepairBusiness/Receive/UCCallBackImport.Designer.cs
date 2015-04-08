namespace HXCPcClient.UCForm.RepairBusiness.Receive
{
    partial class UCCallBackImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCallBackImport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.palBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnImport = new ServiceStationClient.ComponentUI.ButtonEx();
            this.pnlSearch = new ServiceStationClient.ComponentUI.PanelEx();
            this.dtpReserveETime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpReserveSTime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labReserveTime = new System.Windows.Forms.Label();
            this.txtCustomName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labCustomName = new System.Windows.Forms.Label();
            this.txtDriverPhone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labDriverPhone = new System.Windows.Forms.Label();
            this.txtCarNO = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labCarNO = new System.Windows.Forms.Label();
            this.btnSubmit = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvRData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.repair_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reception_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.driver_mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.driver_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_vin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.engine_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repair_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlContainer.SuspendLayout();
            this.palBottom.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.palBottom);
            this.pnlContainer.Controls.Add(this.pnlSearch);
            this.pnlContainer.Controls.Add(this.tabControlEx1);
            // 
            // palBottom
            // 
            this.palBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.palBottom.Controls.Add(this.btnCancel);
            this.palBottom.Controls.Add(this.btnImport);
            this.palBottom.Location = new System.Drawing.Point(0, 322);
            this.palBottom.Name = "palBottom";
            this.palBottom.Size = new System.Drawing.Size(679, 50);
            this.palBottom.TabIndex = 9;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Caption = "取消";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.DownImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.DownImage")));
            this.btnCancel.Location = new System.Drawing.Point(611, 13);
            this.btnCancel.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.MoveImage")));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.NormalImage")));
            this.btnCancel.Size = new System.Drawing.Size(60, 26);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnImport
            // 
            this.btnImport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImport.BackgroundImage")));
            this.btnImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImport.Caption = "导入";
            this.btnImport.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnImport.DownImage = ((System.Drawing.Image)(resources.GetObject("btnImport.DownImage")));
            this.btnImport.Location = new System.Drawing.Point(519, 13);
            this.btnImport.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnImport.MoveImage")));
            this.btnImport.Name = "btnImport";
            this.btnImport.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnImport.NormalImage")));
            this.btnImport.Size = new System.Drawing.Size(60, 26);
            this.btnImport.TabIndex = 18;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlSearch.BorderWidth = 1;
            this.pnlSearch.Controls.Add(this.dtpReserveETime);
            this.pnlSearch.Controls.Add(this.dtpReserveSTime);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.labReserveTime);
            this.pnlSearch.Controls.Add(this.txtCustomName);
            this.pnlSearch.Controls.Add(this.labCustomName);
            this.pnlSearch.Controls.Add(this.txtDriverPhone);
            this.pnlSearch.Controls.Add(this.labDriverPhone);
            this.pnlSearch.Controls.Add(this.txtCarNO);
            this.pnlSearch.Controls.Add(this.labCarNO);
            this.pnlSearch.Controls.Add(this.btnSubmit);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Curvature = 0;
            this.pnlSearch.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlSearch.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlSearch.Location = new System.Drawing.Point(0, -2);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(681, 114);
            this.pnlSearch.TabIndex = 7;
            // 
            // dtpReserveETime
            // 
            this.dtpReserveETime.Location = new System.Drawing.Point(223, 80);
            this.dtpReserveETime.Name = "dtpReserveETime";
            this.dtpReserveETime.ShowFormat = "yyyy-MM-dd";
            this.dtpReserveETime.Size = new System.Drawing.Size(121, 21);
            this.dtpReserveETime.TabIndex = 54;
            this.dtpReserveETime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // dtpReserveSTime
            // 
            this.dtpReserveSTime.Location = new System.Drawing.Point(77, 80);
            this.dtpReserveSTime.Name = "dtpReserveSTime";
            this.dtpReserveSTime.ShowFormat = "yyyy-MM-dd";
            this.dtpReserveSTime.Size = new System.Drawing.Size(121, 21);
            this.dtpReserveSTime.TabIndex = 53;
            this.dtpReserveSTime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 52;
            this.label1.Text = "从";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(202, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 51;
            this.label3.Text = "到";
            // 
            // labReserveTime
            // 
            this.labReserveTime.AutoSize = true;
            this.labReserveTime.Location = new System.Drawing.Point(3, 83);
            this.labReserveTime.Name = "labReserveTime";
            this.labReserveTime.Size = new System.Drawing.Size(65, 12);
            this.labReserveTime.TabIndex = 50;
            this.labReserveTime.Text = "接待时间：";
            // 
            // txtCustomName
            // 
            this.txtCustomName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCustomName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCustomName.BackColor = System.Drawing.Color.Transparent;
            this.txtCustomName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCustomName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCustomName.ForeImage = null;
            this.txtCustomName.Location = new System.Drawing.Point(78, 44);
            this.txtCustomName.MaxLengh = 32767;
            this.txtCustomName.Multiline = false;
            this.txtCustomName.Name = "txtCustomName";
            this.txtCustomName.Radius = 3;
            this.txtCustomName.ReadOnly = false;
            this.txtCustomName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCustomName.ShowError = false;
            this.txtCustomName.Size = new System.Drawing.Size(120, 23);
            this.txtCustomName.TabIndex = 39;
            this.txtCustomName.UseSystemPasswordChar = false;
            this.txtCustomName.Value = "";
            this.txtCustomName.VerifyCondition = null;
            this.txtCustomName.VerifyType = null;
            this.txtCustomName.WaterMark = null;
            this.txtCustomName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labCustomName
            // 
            this.labCustomName.AutoSize = true;
            this.labCustomName.Location = new System.Drawing.Point(5, 50);
            this.labCustomName.Name = "labCustomName";
            this.labCustomName.Size = new System.Drawing.Size(65, 12);
            this.labCustomName.TabIndex = 38;
            this.labCustomName.Text = "客户名称：";
            // 
            // txtDriverPhone
            // 
            this.txtDriverPhone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtDriverPhone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtDriverPhone.BackColor = System.Drawing.Color.Transparent;
            this.txtDriverPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtDriverPhone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtDriverPhone.ForeImage = null;
            this.txtDriverPhone.Location = new System.Drawing.Point(303, 14);
            this.txtDriverPhone.MaxLengh = 32767;
            this.txtDriverPhone.Multiline = false;
            this.txtDriverPhone.Name = "txtDriverPhone";
            this.txtDriverPhone.Radius = 3;
            this.txtDriverPhone.ReadOnly = false;
            this.txtDriverPhone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtDriverPhone.ShowError = false;
            this.txtDriverPhone.Size = new System.Drawing.Size(120, 23);
            this.txtDriverPhone.TabIndex = 29;
            this.txtDriverPhone.UseSystemPasswordChar = false;
            this.txtDriverPhone.Value = "";
            this.txtDriverPhone.VerifyCondition = null;
            this.txtDriverPhone.VerifyType = null;
            this.txtDriverPhone.WaterMark = null;
            this.txtDriverPhone.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labDriverPhone
            // 
            this.labDriverPhone.AutoSize = true;
            this.labDriverPhone.Location = new System.Drawing.Point(232, 19);
            this.labDriverPhone.Name = "labDriverPhone";
            this.labDriverPhone.Size = new System.Drawing.Size(65, 12);
            this.labDriverPhone.TabIndex = 28;
            this.labDriverPhone.Text = "司机手机：";
            // 
            // txtCarNO
            // 
            this.txtCarNO.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCarNO.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCarNO.BackColor = System.Drawing.Color.Transparent;
            this.txtCarNO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCarNO.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCarNO.ForeImage = null;
            this.txtCarNO.Location = new System.Drawing.Point(78, 15);
            this.txtCarNO.MaxLengh = 32767;
            this.txtCarNO.Multiline = false;
            this.txtCarNO.Name = "txtCarNO";
            this.txtCarNO.Radius = 3;
            this.txtCarNO.ReadOnly = false;
            this.txtCarNO.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCarNO.ShowError = false;
            this.txtCarNO.Size = new System.Drawing.Size(120, 23);
            this.txtCarNO.TabIndex = 21;
            this.txtCarNO.UseSystemPasswordChar = false;
            this.txtCarNO.Value = "";
            this.txtCarNO.VerifyCondition = null;
            this.txtCarNO.VerifyType = null;
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
            this.btnSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.BackgroundImage")));
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Caption = "查询";
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSubmit.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.DownImage")));
            this.btnSubmit.Location = new System.Drawing.Point(458, 77);
            this.btnSubmit.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.MoveImage")));
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.NormalImage")));
            this.btnSubmit.Size = new System.Drawing.Size(60, 26);
            this.btnSubmit.TabIndex = 17;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(458, 43);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 16;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Location = new System.Drawing.Point(3, 114);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(676, 207);
            this.tabControlEx1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvRData);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(668, 177);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "返修单信息";
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
            this.repair_no,
            this.reception_time,
            this.customer_name,
            this.driver_mobile,
            this.driver_name,
            this.vehicle_no,
            this.vehicle_vin,
            this.engine_type,
            this.repair_id});
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
            this.dgvRData.Size = new System.Drawing.Size(662, 171);
            this.dgvRData.TabIndex = 0;
            this.dgvRData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRData_CellFormatting);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 40;
            // 
            // repair_no
            // 
            this.repair_no.DataPropertyName = "repair_no";
            this.repair_no.HeaderText = "返修单号";
            this.repair_no.Name = "repair_no";
            this.repair_no.ReadOnly = true;
            // 
            // reception_time
            // 
            this.reception_time.DataPropertyName = "reception_time";
            this.reception_time.HeaderText = "接待时间";
            this.reception_time.Name = "reception_time";
            this.reception_time.ReadOnly = true;
            // 
            // customer_name
            // 
            this.customer_name.DataPropertyName = "customer_name";
            this.customer_name.HeaderText = "客户名称";
            this.customer_name.Name = "customer_name";
            this.customer_name.ReadOnly = true;
            this.customer_name.Width = 150;
            // 
            // driver_mobile
            // 
            this.driver_mobile.DataPropertyName = "driver_mobile";
            this.driver_mobile.HeaderText = "司机手机";
            this.driver_mobile.Name = "driver_mobile";
            this.driver_mobile.ReadOnly = true;
            // 
            // driver_name
            // 
            this.driver_name.DataPropertyName = "driver_name";
            this.driver_name.HeaderText = "司机";
            this.driver_name.Name = "driver_name";
            this.driver_name.ReadOnly = true;
            // 
            // vehicle_no
            // 
            this.vehicle_no.DataPropertyName = "vehicle_no";
            this.vehicle_no.HeaderText = "车牌号";
            this.vehicle_no.Name = "vehicle_no";
            this.vehicle_no.ReadOnly = true;
            // 
            // vehicle_vin
            // 
            this.vehicle_vin.DataPropertyName = "vehicle_vin";
            this.vehicle_vin.HeaderText = "VIN";
            this.vehicle_vin.Name = "vehicle_vin";
            this.vehicle_vin.ReadOnly = true;
            // 
            // engine_type
            // 
            this.engine_type.DataPropertyName = "engine_type";
            this.engine_type.HeaderText = "发动机号";
            this.engine_type.Name = "engine_type";
            this.engine_type.ReadOnly = true;
            // 
            // repair_id
            // 
            this.repair_id.DataPropertyName = "repair_id";
            this.repair_id.HeaderText = "repair_id";
            this.repair_id.Name = "repair_id";
            this.repair_id.ReadOnly = true;
            this.repair_id.Visible = false;
            // 
            // UCCallBackImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 402);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "UCCallBackImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "返修单导入";
            this.Load += new System.EventHandler(this.UCCallBackImport_Load);
            this.pnlContainer.ResumeLayout(false);
            this.palBottom.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel palBottom;
        private ServiceStationClient.ComponentUI.ButtonEx btnCancel;
        private ServiceStationClient.ComponentUI.ButtonEx btnImport;
        private ServiceStationClient.ComponentUI.PanelEx pnlSearch;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpReserveETime;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpReserveSTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labReserveTime;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCustomName;
        private System.Windows.Forms.Label labCustomName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtDriverPhone;
        private System.Windows.Forms.Label labDriverPhone;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCarNO;
        private System.Windows.Forms.Label labCarNO;
        private ServiceStationClient.ComponentUI.ButtonEx btnSubmit;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRData;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn repair_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn reception_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn driver_mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn driver_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_vin;
        private System.Windows.Forms.DataGridViewTextBoxColumn engine_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn repair_id;
    }
}