namespace HXCPcClient.UCForm.RepairBusiness.RepairRescue
{
    partial class UCRepairRescueManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCRepairRescueManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.palTop = new ServiceStationClient.ComponentUI.PanelEx();
            this.cobRescueType = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpETime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpSTime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.label4 = new System.Windows.Forms.Label();
            this.labBalanceTime = new System.Windows.Forms.Label();
            this.labRescueType = new System.Windows.Forms.Label();
            this.cobOrderStatus = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.labOrderStatus = new System.Windows.Forms.Label();
            this.txtOrder = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labOrder = new System.Windows.Forms.Label();
            this.txtServerCarNo = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labServerCarNo = new System.Windows.Forms.Label();
            this.txtCustomName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labCustomName = new System.Windows.Forms.Label();
            this.txtCustomNO = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.labCustomNO = new System.Windows.Forms.Label();
            this.txtCarNO = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.labCarNO = new System.Windows.Forms.Label();
            this.palBottom = new ServiceStationClient.ComponentUI.PanelEx();
            this.dgvRData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.rescue_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.depart_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkman = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkman_mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.document_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_vin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fault_describe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.apply_rescue_place = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rescue_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.service_vehicle_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.man_hour_sum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fitting_sum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.other_item_sum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.privilege_cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.should_sum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.received_sum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.debt_cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rescue_mileage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rescue_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSubmit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVerify = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.palTop.SuspendLayout();
            this.palBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // palTop
            // 
            this.palTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.palTop.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.palTop.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.palTop.BorderWidth = 1;
            this.palTop.Controls.Add(this.cobRescueType);
            this.palTop.Controls.Add(this.btnQuery);
            this.palTop.Controls.Add(this.btnClear);
            this.palTop.Controls.Add(this.label2);
            this.palTop.Controls.Add(this.dtpETime);
            this.palTop.Controls.Add(this.dtpSTime);
            this.palTop.Controls.Add(this.label4);
            this.palTop.Controls.Add(this.labBalanceTime);
            this.palTop.Controls.Add(this.labRescueType);
            this.palTop.Controls.Add(this.cobOrderStatus);
            this.palTop.Controls.Add(this.labOrderStatus);
            this.palTop.Controls.Add(this.txtOrder);
            this.palTop.Controls.Add(this.labOrder);
            this.palTop.Controls.Add(this.txtServerCarNo);
            this.palTop.Controls.Add(this.labServerCarNo);
            this.palTop.Controls.Add(this.txtCustomName);
            this.palTop.Controls.Add(this.labCustomName);
            this.palTop.Controls.Add(this.txtCustomNO);
            this.palTop.Controls.Add(this.labCustomNO);
            this.palTop.Controls.Add(this.txtCarNO);
            this.palTop.Controls.Add(this.labCarNO);
            this.palTop.Curvature = 0;
            this.palTop.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.palTop.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.palTop.Location = new System.Drawing.Point(0, 33);
            this.palTop.Name = "palTop";
            this.palTop.Size = new System.Drawing.Size(1030, 121);
            this.palTop.TabIndex = 5;
            // 
            // cobRescueType
            // 
            this.cobRescueType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobRescueType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobRescueType.FormattingEnabled = true;
            this.cobRescueType.Location = new System.Drawing.Point(291, 36);
            this.cobRescueType.Name = "cobRescueType";
            this.cobRescueType.Size = new System.Drawing.Size(121, 22);
            this.cobRescueType.TabIndex = 111;
            // 
            // btnQuery
            // 
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(768, 76);
            this.btnQuery.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.MoveImage")));
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.NormalImage")));
            this.btnQuery.Size = new System.Drawing.Size(60, 26);
            this.btnQuery.TabIndex = 110;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(768, 43);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 109;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 103;
            this.label2.Text = "从";
            // 
            // dtpETime
            // 
            this.dtpETime.Location = new System.Drawing.Point(272, 66);
            this.dtpETime.Name = "dtpETime";
            this.dtpETime.ShowFormat = "yyyy-MM-dd HH:mm";
            this.dtpETime.Size = new System.Drawing.Size(138, 21);
            this.dtpETime.TabIndex = 102;
            this.dtpETime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // dtpSTime
            // 
            this.dtpSTime.Location = new System.Drawing.Point(104, 66);
            this.dtpSTime.Name = "dtpSTime";
            this.dtpSTime.ShowFormat = "yyyy-MM-dd HH:mm";
            this.dtpSTime.Size = new System.Drawing.Size(139, 21);
            this.dtpSTime.TabIndex = 101;
            this.dtpSTime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 100;
            this.label4.Text = "到";
            // 
            // labBalanceTime
            // 
            this.labBalanceTime.AutoSize = true;
            this.labBalanceTime.Location = new System.Drawing.Point(12, 69);
            this.labBalanceTime.Name = "labBalanceTime";
            this.labBalanceTime.Size = new System.Drawing.Size(65, 12);
            this.labBalanceTime.TabIndex = 99;
            this.labBalanceTime.Text = "出发时间：";
            // 
            // labRescueType
            // 
            this.labRescueType.AutoSize = true;
            this.labRescueType.Location = new System.Drawing.Point(220, 40);
            this.labRescueType.Name = "labRescueType";
            this.labRescueType.Size = new System.Drawing.Size(65, 12);
            this.labRescueType.TabIndex = 79;
            this.labRescueType.Text = "救援类型：";
            // 
            // cobOrderStatus
            // 
            this.cobOrderStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobOrderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobOrderStatus.FormattingEnabled = true;
            this.cobOrderStatus.Location = new System.Drawing.Point(501, 33);
            this.cobOrderStatus.Name = "cobOrderStatus";
            this.cobOrderStatus.Size = new System.Drawing.Size(121, 22);
            this.cobOrderStatus.TabIndex = 78;
            // 
            // labOrderStatus
            // 
            this.labOrderStatus.AutoSize = true;
            this.labOrderStatus.Location = new System.Drawing.Point(431, 40);
            this.labOrderStatus.Name = "labOrderStatus";
            this.labOrderStatus.Size = new System.Drawing.Size(65, 12);
            this.labOrderStatus.TabIndex = 77;
            this.labOrderStatus.Text = "单据状态：";
            // 
            // txtOrder
            // 
            this.txtOrder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtOrder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtOrder.BackColor = System.Drawing.Color.Transparent;
            this.txtOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtOrder.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtOrder.ForeImage = null;
            this.txtOrder.InputtingVerifyCondition = null;
            this.txtOrder.Location = new System.Drawing.Point(84, 36);
            this.txtOrder.MaxLengh = 32767;
            this.txtOrder.Multiline = false;
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Radius = 3;
            this.txtOrder.ReadOnly = false;
            this.txtOrder.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtOrder.ShowError = false;
            this.txtOrder.Size = new System.Drawing.Size(120, 23);
            this.txtOrder.TabIndex = 75;
            this.txtOrder.UseSystemPasswordChar = false;
            this.txtOrder.Value = "";
            this.txtOrder.VerifyCondition = null;
            this.txtOrder.VerifyType = null;
            this.txtOrder.VerifyTypeName = null;
            this.txtOrder.WaterMark = null;
            this.txtOrder.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labOrder
            // 
            this.labOrder.AutoSize = true;
            this.labOrder.Location = new System.Drawing.Point(14, 43);
            this.labOrder.Name = "labOrder";
            this.labOrder.Size = new System.Drawing.Size(65, 12);
            this.labOrder.TabIndex = 74;
            this.labOrder.Text = "救援单号：";
            // 
            // txtServerCarNo
            // 
            this.txtServerCarNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtServerCarNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtServerCarNo.BackColor = System.Drawing.Color.Transparent;
            this.txtServerCarNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtServerCarNo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtServerCarNo.ForeImage = null;
            this.txtServerCarNo.InputtingVerifyCondition = null;
            this.txtServerCarNo.Location = new System.Drawing.Point(708, 6);
            this.txtServerCarNo.MaxLengh = 32767;
            this.txtServerCarNo.Multiline = false;
            this.txtServerCarNo.Name = "txtServerCarNo";
            this.txtServerCarNo.Radius = 3;
            this.txtServerCarNo.ReadOnly = false;
            this.txtServerCarNo.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtServerCarNo.ShowError = false;
            this.txtServerCarNo.Size = new System.Drawing.Size(120, 23);
            this.txtServerCarNo.TabIndex = 71;
            this.txtServerCarNo.UseSystemPasswordChar = false;
            this.txtServerCarNo.Value = "";
            this.txtServerCarNo.VerifyCondition = null;
            this.txtServerCarNo.VerifyType = null;
            this.txtServerCarNo.VerifyTypeName = null;
            this.txtServerCarNo.WaterMark = null;
            this.txtServerCarNo.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labServerCarNo
            // 
            this.labServerCarNo.AutoSize = true;
            this.labServerCarNo.Location = new System.Drawing.Point(637, 12);
            this.labServerCarNo.Name = "labServerCarNo";
            this.labServerCarNo.Size = new System.Drawing.Size(65, 12);
            this.labServerCarNo.TabIndex = 70;
            this.labServerCarNo.Text = "服务车号：";
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
            this.txtCustomName.Location = new System.Drawing.Point(501, 6);
            this.txtCustomName.MaxLengh = 32767;
            this.txtCustomName.Multiline = false;
            this.txtCustomName.Name = "txtCustomName";
            this.txtCustomName.Radius = 3;
            this.txtCustomName.ReadOnly = false;
            this.txtCustomName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCustomName.ShowError = false;
            this.txtCustomName.Size = new System.Drawing.Size(120, 23);
            this.txtCustomName.TabIndex = 69;
            this.txtCustomName.UseSystemPasswordChar = false;
            this.txtCustomName.Value = "";
            this.txtCustomName.VerifyCondition = null;
            this.txtCustomName.VerifyType = null;
            this.txtCustomName.VerifyTypeName = null;
            this.txtCustomName.WaterMark = null;
            this.txtCustomName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labCustomName
            // 
            this.labCustomName.AutoSize = true;
            this.labCustomName.Location = new System.Drawing.Point(430, 12);
            this.labCustomName.Name = "labCustomName";
            this.labCustomName.Size = new System.Drawing.Size(65, 12);
            this.labCustomName.TabIndex = 68;
            this.labCustomName.Text = "客户名称：";
            // 
            // txtCustomNO
            // 
            this.txtCustomNO.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCustomNO.Location = new System.Drawing.Point(293, 6);
            this.txtCustomNO.Name = "txtCustomNO";
            this.txtCustomNO.ReadOnly = false;
            this.txtCustomNO.Size = new System.Drawing.Size(117, 24);
            this.txtCustomNO.TabIndex = 67;
            this.txtCustomNO.ToolTipTitle = "";
            this.txtCustomNO.ChooserClick += new System.EventHandler(this.txtCustomNO_ChooserClick);
            // 
            // labCustomNO
            // 
            this.labCustomNO.AutoSize = true;
            this.labCustomNO.Location = new System.Drawing.Point(222, 12);
            this.labCustomNO.Name = "labCustomNO";
            this.labCustomNO.Size = new System.Drawing.Size(65, 12);
            this.labCustomNO.TabIndex = 66;
            this.labCustomNO.Text = "客户编码：";
            // 
            // txtCarNO
            // 
            this.txtCarNO.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCarNO.Location = new System.Drawing.Point(84, 6);
            this.txtCarNO.Name = "txtCarNO";
            this.txtCarNO.ReadOnly = false;
            this.txtCarNO.Size = new System.Drawing.Size(120, 24);
            this.txtCarNO.TabIndex = 65;
            this.txtCarNO.ToolTipTitle = "";
            this.txtCarNO.ChooserClick += new System.EventHandler(this.txtCarNO_ChooserClick);
            // 
            // labCarNO
            // 
            this.labCarNO.AutoSize = true;
            this.labCarNO.Location = new System.Drawing.Point(30, 12);
            this.labCarNO.Name = "labCarNO";
            this.labCarNO.Size = new System.Drawing.Size(53, 12);
            this.labCarNO.TabIndex = 64;
            this.labCarNO.Text = "车牌号：";
            // 
            // palBottom
            // 
            this.palBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.palBottom.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.palBottom.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.palBottom.BorderWidth = 1;
            this.palBottom.Controls.Add(this.dgvRData);
            this.palBottom.Curvature = 0;
            this.palBottom.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.palBottom.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.palBottom.Location = new System.Drawing.Point(0, 158);
            this.palBottom.Name = "palBottom";
            this.palBottom.Size = new System.Drawing.Size(1030, 352);
            this.palBottom.TabIndex = 6;
            // 
            // dgvRData
            // 
            this.dgvRData.AllowUserToAddRows = false;
            this.dgvRData.AllowUserToDeleteRows = false;
            this.dgvRData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvRData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
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
            this.rescue_no,
            this.depart_time,
            this.customer_name,
            this.customer_code,
            this.vehicle_no,
            this.linkman,
            this.linkman_mobile,
            this.document_status,
            this.vehicle_brand,
            this.vehicle_model,
            this.vehicle_vin,
            this.fault_describe,
            this.apply_rescue_place,
            this.rescue_type,
            this.service_vehicle_no,
            this.man_hour_sum,
            this.fitting_sum,
            this.other_item_sum,
            this.privilege_cost,
            this.should_sum,
            this.received_sum,
            this.debt_cost,
            this.rescue_mileage,
            this.remarks,
            this.rescue_id});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRData.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRData.EnableHeadersVisualStyles = false;
            this.dgvRData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvRData.IsCheck = true;
            this.dgvRData.Location = new System.Drawing.Point(3, 4);
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
            this.dgvRData.Size = new System.Drawing.Size(1024, 343);
            this.dgvRData.TabIndex = 13;
            this.dgvRData.HeadCheckChanged += new ServiceStationClient.ComponentUI.DataGridViewEx.DelegateOnClick(this.dgvRData_HeadCheckChanged);
            this.dgvRData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRData_CellContentClick);
            this.dgvRData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRData_CellDoubleClick);
            this.dgvRData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRData_CellFormatting);
            this.dgvRData.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRData_CellMouseClick);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 30;
            // 
            // rescue_no
            // 
            this.rescue_no.DataPropertyName = "rescue_no";
            this.rescue_no.HeaderText = "救援单号";
            this.rescue_no.MinimumWidth = 120;
            this.rescue_no.Name = "rescue_no";
            this.rescue_no.ReadOnly = true;
            this.rescue_no.Width = 120;
            // 
            // depart_time
            // 
            this.depart_time.DataPropertyName = "depart_time";
            this.depart_time.HeaderText = "救援出发时间";
            this.depart_time.Name = "depart_time";
            this.depart_time.ReadOnly = true;
            this.depart_time.Width = 110;
            // 
            // customer_name
            // 
            this.customer_name.DataPropertyName = "customer_name";
            this.customer_name.HeaderText = "客户名称";
            this.customer_name.Name = "customer_name";
            this.customer_name.ReadOnly = true;
            this.customer_name.Width = 110;
            // 
            // customer_code
            // 
            this.customer_code.DataPropertyName = "customer_code";
            this.customer_code.HeaderText = "客户编码";
            this.customer_code.Name = "customer_code";
            this.customer_code.ReadOnly = true;
            // 
            // vehicle_no
            // 
            this.vehicle_no.DataPropertyName = "vehicle_no";
            this.vehicle_no.HeaderText = "车牌号";
            this.vehicle_no.Name = "vehicle_no";
            this.vehicle_no.ReadOnly = true;
            this.vehicle_no.Width = 110;
            // 
            // linkman
            // 
            this.linkman.DataPropertyName = "linkman";
            this.linkman.HeaderText = "联系人";
            this.linkman.Name = "linkman";
            this.linkman.ReadOnly = true;
            // 
            // linkman_mobile
            // 
            this.linkman_mobile.DataPropertyName = "linkman_mobile";
            this.linkman_mobile.HeaderText = "联系人手机";
            this.linkman_mobile.Name = "linkman_mobile";
            this.linkman_mobile.ReadOnly = true;
            // 
            // document_status
            // 
            this.document_status.DataPropertyName = "document_status";
            this.document_status.HeaderText = "单据状态";
            this.document_status.Name = "document_status";
            this.document_status.ReadOnly = true;
            this.document_status.Width = 90;
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
            // vehicle_vin
            // 
            this.vehicle_vin.DataPropertyName = "vehicle_vin";
            this.vehicle_vin.HeaderText = "VIN";
            this.vehicle_vin.Name = "vehicle_vin";
            this.vehicle_vin.ReadOnly = true;
            // 
            // fault_describe
            // 
            this.fault_describe.DataPropertyName = "fault_describe";
            this.fault_describe.HeaderText = "故障描述";
            this.fault_describe.Name = "fault_describe";
            this.fault_describe.ReadOnly = true;
            // 
            // apply_rescue_place
            // 
            this.apply_rescue_place.DataPropertyName = "apply_rescue_place";
            this.apply_rescue_place.HeaderText = "申救地点";
            this.apply_rescue_place.Name = "apply_rescue_place";
            this.apply_rescue_place.ReadOnly = true;
            // 
            // rescue_type
            // 
            this.rescue_type.DataPropertyName = "rescue_type";
            this.rescue_type.HeaderText = "救援类型";
            this.rescue_type.Name = "rescue_type";
            this.rescue_type.ReadOnly = true;
            // 
            // service_vehicle_no
            // 
            this.service_vehicle_no.DataPropertyName = "service_vehicle_no";
            this.service_vehicle_no.HeaderText = "服务车号";
            this.service_vehicle_no.Name = "service_vehicle_no";
            this.service_vehicle_no.ReadOnly = true;
            // 
            // man_hour_sum
            // 
            this.man_hour_sum.DataPropertyName = "man_hour_sum";
            this.man_hour_sum.HeaderText = "工时价税合计";
            this.man_hour_sum.Name = "man_hour_sum";
            this.man_hour_sum.ReadOnly = true;
            this.man_hour_sum.Width = 110;
            // 
            // fitting_sum
            // 
            this.fitting_sum.DataPropertyName = "fitting_sum";
            this.fitting_sum.HeaderText = "材料价税合计";
            this.fitting_sum.Name = "fitting_sum";
            this.fitting_sum.ReadOnly = true;
            this.fitting_sum.Width = 110;
            // 
            // other_item_sum
            // 
            this.other_item_sum.DataPropertyName = "other_item_sum";
            this.other_item_sum.HeaderText = "其他项目价税合计";
            this.other_item_sum.Name = "other_item_sum";
            this.other_item_sum.ReadOnly = true;
            this.other_item_sum.Width = 130;
            // 
            // privilege_cost
            // 
            this.privilege_cost.DataPropertyName = "privilege_cost";
            this.privilege_cost.HeaderText = "优惠费用";
            this.privilege_cost.Name = "privilege_cost";
            this.privilege_cost.ReadOnly = true;
            // 
            // should_sum
            // 
            this.should_sum.DataPropertyName = "should_sum";
            this.should_sum.HeaderText = "应收总额";
            this.should_sum.Name = "should_sum";
            this.should_sum.ReadOnly = true;
            // 
            // received_sum
            // 
            this.received_sum.DataPropertyName = "received_sum";
            this.received_sum.HeaderText = "实收总额";
            this.received_sum.Name = "received_sum";
            this.received_sum.ReadOnly = true;
            // 
            // debt_cost
            // 
            this.debt_cost.DataPropertyName = "debt_cost";
            this.debt_cost.HeaderText = "本次欠款金额";
            this.debt_cost.Name = "debt_cost";
            this.debt_cost.ReadOnly = true;
            this.debt_cost.Width = 110;
            // 
            // rescue_mileage
            // 
            this.rescue_mileage.DataPropertyName = "rescue_mileage";
            this.rescue_mileage.HeaderText = "救援里程";
            this.rescue_mileage.Name = "rescue_mileage";
            this.rescue_mileage.ReadOnly = true;
            this.rescue_mileage.Width = 90;
            // 
            // remarks
            // 
            this.remarks.DataPropertyName = "remarks";
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            // 
            // rescue_id
            // 
            this.rescue_id.DataPropertyName = "rescue_id";
            this.rescue_id.HeaderText = "rescue_id";
            this.rescue_id.Name = "rescue_id";
            this.rescue_id.ReadOnly = true;
            this.rescue_id.Visible = false;
            this.rescue_id.Width = 10;
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(32, 19);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(32, 19);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(32, 19);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(32, 19);
            // 
            // tsmiSubmit
            // 
            this.tsmiSubmit.Name = "tsmiSubmit";
            this.tsmiSubmit.Size = new System.Drawing.Size(32, 19);
            // 
            // tsmiVerify
            // 
            this.tsmiVerify.Name = "tsmiVerify";
            this.tsmiVerify.Size = new System.Drawing.Size(32, 19);
            // 
            // panelEx2
            // 
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(223)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.page);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(0, 516);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.panelEx2.Size = new System.Drawing.Size(1030, 28);
            this.panelEx2.TabIndex = 19;
            // 
            // page
            // 
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.page.Dock = System.Windows.Forms.DockStyle.Right;
            this.page.Location = new System.Drawing.Point(502, 0);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(428, 28);
            this.page.TabIndex = 5;
            this.page.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            // 
            // UCRepairRescueManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.palBottom);
            this.Controls.Add(this.palTop);
            this.Name = "UCRepairRescueManager";
            this.Load += new System.EventHandler(this.UCRepairRescueManager_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.palTop, 0);
            this.Controls.SetChildIndex(this.palBottom, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.palTop.ResumeLayout(false);
            this.palTop.PerformLayout();
            this.palBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx palTop;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobRescueType;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpETime;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpSTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labBalanceTime;
        private System.Windows.Forms.Label labRescueType;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobOrderStatus;
        private System.Windows.Forms.Label labOrderStatus;
        private ServiceStationClient.ComponentUI.TextBoxEx txtOrder;
        private System.Windows.Forms.Label labOrder;
        private ServiceStationClient.ComponentUI.TextBoxEx txtServerCarNo;
        private System.Windows.Forms.Label labServerCarNo;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCustomName;
        private System.Windows.Forms.Label labCustomName;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCustomNO;
        private System.Windows.Forms.Label labCustomNO;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCarNO;
        private System.Windows.Forms.Label labCarNO;
        private ServiceStationClient.ComponentUI.PanelEx palBottom;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRData;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn rescue_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn depart_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkman;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkman_mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn document_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_model;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_vin;
        private System.Windows.Forms.DataGridViewTextBoxColumn fault_describe;
        private System.Windows.Forms.DataGridViewTextBoxColumn apply_rescue_place;
        private System.Windows.Forms.DataGridViewTextBoxColumn rescue_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn service_vehicle_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn man_hour_sum;
        private System.Windows.Forms.DataGridViewTextBoxColumn fitting_sum;
        private System.Windows.Forms.DataGridViewTextBoxColumn other_item_sum;
        private System.Windows.Forms.DataGridViewTextBoxColumn privilege_cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn should_sum;
        private System.Windows.Forms.DataGridViewTextBoxColumn received_sum;
        private System.Windows.Forms.DataGridViewTextBoxColumn debt_cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn rescue_mileage;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn rescue_id;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiSubmit;
        private System.Windows.Forms.ToolStripMenuItem tsmiVerify;
    }
}