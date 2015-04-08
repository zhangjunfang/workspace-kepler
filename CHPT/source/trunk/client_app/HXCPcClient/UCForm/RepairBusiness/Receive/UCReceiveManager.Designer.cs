namespace HXCPcClient.UCForm.RepairBusiness.Receive
{
    partial class UCReceiveManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCReceiveManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.palInfo = new System.Windows.Forms.Panel();
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.cobOrderStatus = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.labOrderStatus = new System.Windows.Forms.Label();
            this.dtpReserveETime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpReserveSTime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labReserveTime = new System.Windows.Forms.Label();
            this.txtOrder = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labOrder = new System.Windows.Forms.Label();
            this.txtAdvisor = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labAdvisor = new System.Windows.Forms.Label();
            this.txtCustomNO = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txtCarNO = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.cobPayType = new System.Windows.Forms.ComboBox();
            this.labPayType = new System.Windows.Forms.Label();
            this.txtContactPhone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtContact = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtCustomName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labContactPhone = new System.Windows.Forms.Label();
            this.labContact = new System.Windows.Forms.Label();
            this.labCustomName = new System.Windows.Forms.Label();
            this.labCustomNO = new System.Windows.Forms.Label();
            this.txtEngineNo = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labEngineNo = new System.Windows.Forms.Label();
            this.labCarNO = new System.Windows.Forms.Label();
            this.palData = new System.Windows.Forms.Panel();
            this.dgvRData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.maintain_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reception_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fault_describe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maintain_man = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oil_into_factory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.travel_mileage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkman = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.link_man_mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.info_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.completion_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maintain_payment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maintain_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.before_orderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orders_source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSubmit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVerify = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.预约单导入 = new System.Windows.Forms.ToolStripMenuItem();
            this.返修单导入 = new System.Windows.Forms.ToolStripMenuItem();
            this.palInfo.SuspendLayout();
            this.palData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // palInfo
            // 
            this.palInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.palInfo.Controls.Add(this.btnQuery);
            this.palInfo.Controls.Add(this.btnClear);
            this.palInfo.Controls.Add(this.cobOrderStatus);
            this.palInfo.Controls.Add(this.labOrderStatus);
            this.palInfo.Controls.Add(this.dtpReserveETime);
            this.palInfo.Controls.Add(this.dtpReserveSTime);
            this.palInfo.Controls.Add(this.label1);
            this.palInfo.Controls.Add(this.label3);
            this.palInfo.Controls.Add(this.labReserveTime);
            this.palInfo.Controls.Add(this.txtOrder);
            this.palInfo.Controls.Add(this.labOrder);
            this.palInfo.Controls.Add(this.txtAdvisor);
            this.palInfo.Controls.Add(this.labAdvisor);
            this.palInfo.Controls.Add(this.txtCustomNO);
            this.palInfo.Controls.Add(this.txtCarNO);
            this.palInfo.Controls.Add(this.cobPayType);
            this.palInfo.Controls.Add(this.labPayType);
            this.palInfo.Controls.Add(this.txtContactPhone);
            this.palInfo.Controls.Add(this.txtContact);
            this.palInfo.Controls.Add(this.txtCustomName);
            this.palInfo.Controls.Add(this.labContactPhone);
            this.palInfo.Controls.Add(this.labContact);
            this.palInfo.Controls.Add(this.labCustomName);
            this.palInfo.Controls.Add(this.labCustomNO);
            this.palInfo.Controls.Add(this.txtEngineNo);
            this.palInfo.Controls.Add(this.labEngineNo);
            this.palInfo.Controls.Add(this.labCarNO);
            this.palInfo.Location = new System.Drawing.Point(0, 33);
            this.palInfo.Name = "palInfo";
            this.palInfo.Size = new System.Drawing.Size(1030, 109);
            this.palInfo.TabIndex = 3;
            // 
            // btnQuery
            // 
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(855, 60);
            this.btnQuery.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.MoveImage")));
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.NormalImage")));
            this.btnQuery.Size = new System.Drawing.Size(60, 26);
            this.btnQuery.TabIndex = 78;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(855, 27);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 77;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cobOrderStatus
            // 
            this.cobOrderStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobOrderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobOrderStatus.FormattingEnabled = true;
            this.cobOrderStatus.Location = new System.Drawing.Point(724, 65);
            this.cobOrderStatus.Name = "cobOrderStatus";
            this.cobOrderStatus.Size = new System.Drawing.Size(121, 22);
            this.cobOrderStatus.TabIndex = 76;
            // 
            // labOrderStatus
            // 
            this.labOrderStatus.AutoSize = true;
            this.labOrderStatus.Location = new System.Drawing.Point(653, 68);
            this.labOrderStatus.Name = "labOrderStatus";
            this.labOrderStatus.Size = new System.Drawing.Size(65, 12);
            this.labOrderStatus.TabIndex = 75;
            this.labOrderStatus.Text = "单据状态：";
            // 
            // dtpReserveETime
            // 
            this.dtpReserveETime.Location = new System.Drawing.Point(233, 64);
            this.dtpReserveETime.Name = "dtpReserveETime";
            this.dtpReserveETime.ShowFormat = "yyyy-MM-dd HH:mm";
            this.dtpReserveETime.Size = new System.Drawing.Size(121, 21);
            this.dtpReserveETime.TabIndex = 74;
            this.dtpReserveETime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // dtpReserveSTime
            // 
            this.dtpReserveSTime.Location = new System.Drawing.Point(87, 64);
            this.dtpReserveSTime.Name = "dtpReserveSTime";
            this.dtpReserveSTime.ShowFormat = "yyyy-MM-dd HH:mm";
            this.dtpReserveSTime.Size = new System.Drawing.Size(121, 21);
            this.dtpReserveSTime.TabIndex = 73;
            this.dtpReserveSTime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 72;
            this.label1.Text = "从";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 71;
            this.label3.Text = "到";
            // 
            // labReserveTime
            // 
            this.labReserveTime.AutoSize = true;
            this.labReserveTime.Location = new System.Drawing.Point(13, 67);
            this.labReserveTime.Name = "labReserveTime";
            this.labReserveTime.Size = new System.Drawing.Size(65, 12);
            this.labReserveTime.TabIndex = 70;
            this.labReserveTime.Text = "接待时间：";
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
            this.txtOrder.Location = new System.Drawing.Point(88, 35);
            this.txtOrder.MaxLengh = 32767;
            this.txtOrder.Multiline = false;
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Radius = 3;
            this.txtOrder.ReadOnly = false;
            this.txtOrder.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtOrder.ShowError = false;
            this.txtOrder.Size = new System.Drawing.Size(120, 23);
            this.txtOrder.TabIndex = 69;
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
            this.labOrder.Location = new System.Drawing.Point(17, 41);
            this.labOrder.Name = "labOrder";
            this.labOrder.Size = new System.Drawing.Size(65, 12);
            this.labOrder.TabIndex = 68;
            this.labOrder.Text = "维修单号：";
            // 
            // txtAdvisor
            // 
            this.txtAdvisor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtAdvisor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtAdvisor.BackColor = System.Drawing.Color.Transparent;
            this.txtAdvisor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtAdvisor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtAdvisor.ForeImage = null;
            this.txtAdvisor.InputtingVerifyCondition = null;
            this.txtAdvisor.Location = new System.Drawing.Point(291, 36);
            this.txtAdvisor.MaxLengh = 32767;
            this.txtAdvisor.Multiline = false;
            this.txtAdvisor.Name = "txtAdvisor";
            this.txtAdvisor.Radius = 3;
            this.txtAdvisor.ReadOnly = false;
            this.txtAdvisor.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtAdvisor.ShowError = false;
            this.txtAdvisor.Size = new System.Drawing.Size(120, 23);
            this.txtAdvisor.TabIndex = 67;
            this.txtAdvisor.UseSystemPasswordChar = false;
            this.txtAdvisor.Value = "";
            this.txtAdvisor.VerifyCondition = null;
            this.txtAdvisor.VerifyType = null;
            this.txtAdvisor.VerifyTypeName = null;
            this.txtAdvisor.WaterMark = null;
            this.txtAdvisor.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labAdvisor
            // 
            this.labAdvisor.AutoSize = true;
            this.labAdvisor.Location = new System.Drawing.Point(220, 42);
            this.labAdvisor.Name = "labAdvisor";
            this.labAdvisor.Size = new System.Drawing.Size(65, 12);
            this.labAdvisor.TabIndex = 66;
            this.labAdvisor.Text = "服务顾问：";
            // 
            // txtCustomNO
            // 
            this.txtCustomNO.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCustomNO.Location = new System.Drawing.Point(500, 6);
            this.txtCustomNO.Name = "txtCustomNO";
            this.txtCustomNO.ReadOnly = false;
            this.txtCustomNO.Size = new System.Drawing.Size(117, 24);
            this.txtCustomNO.TabIndex = 65;
            this.txtCustomNO.ToolTipTitle = "";
            this.txtCustomNO.ChooserClick += new System.EventHandler(this.txtCustomNO_ChooserClick);
            // 
            // txtCarNO
            // 
            this.txtCarNO.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCarNO.Location = new System.Drawing.Point(88, 5);
            this.txtCarNO.Name = "txtCarNO";
            this.txtCarNO.ReadOnly = false;
            this.txtCarNO.Size = new System.Drawing.Size(120, 24);
            this.txtCarNO.TabIndex = 63;
            this.txtCarNO.ToolTipTitle = "";
            this.txtCarNO.ChooserClick += new System.EventHandler(this.txtCarNO_ChooserClick);
            // 
            // cobPayType
            // 
            this.cobPayType.FormattingEnabled = true;
            this.cobPayType.Location = new System.Drawing.Point(503, 65);
            this.cobPayType.Name = "cobPayType";
            this.cobPayType.Size = new System.Drawing.Size(119, 20);
            this.cobPayType.TabIndex = 48;
            // 
            // labPayType
            // 
            this.labPayType.AutoSize = true;
            this.labPayType.Location = new System.Drawing.Point(408, 68);
            this.labPayType.Name = "labPayType";
            this.labPayType.Size = new System.Drawing.Size(89, 12);
            this.labPayType.TabIndex = 40;
            this.labPayType.Text = "维修付费方式：";
            // 
            // txtContactPhone
            // 
            this.txtContactPhone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContactPhone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContactPhone.BackColor = System.Drawing.Color.Transparent;
            this.txtContactPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContactPhone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContactPhone.ForeImage = null;
            this.txtContactPhone.InputtingVerifyCondition = null;
            this.txtContactPhone.Location = new System.Drawing.Point(724, 36);
            this.txtContactPhone.MaxLengh = 32767;
            this.txtContactPhone.Multiline = false;
            this.txtContactPhone.Name = "txtContactPhone";
            this.txtContactPhone.Radius = 3;
            this.txtContactPhone.ReadOnly = false;
            this.txtContactPhone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContactPhone.ShowError = false;
            this.txtContactPhone.Size = new System.Drawing.Size(120, 23);
            this.txtContactPhone.TabIndex = 39;
            this.txtContactPhone.UseSystemPasswordChar = false;
            this.txtContactPhone.Value = "";
            this.txtContactPhone.VerifyCondition = null;
            this.txtContactPhone.VerifyType = null;
            this.txtContactPhone.VerifyTypeName = null;
            this.txtContactPhone.WaterMark = null;
            this.txtContactPhone.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtContact
            // 
            this.txtContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContact.BackColor = System.Drawing.Color.Transparent;
            this.txtContact.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContact.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContact.ForeImage = null;
            this.txtContact.InputtingVerifyCondition = null;
            this.txtContact.Location = new System.Drawing.Point(500, 36);
            this.txtContact.MaxLengh = 32767;
            this.txtContact.Multiline = false;
            this.txtContact.Name = "txtContact";
            this.txtContact.Radius = 3;
            this.txtContact.ReadOnly = false;
            this.txtContact.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContact.ShowError = false;
            this.txtContact.Size = new System.Drawing.Size(120, 23);
            this.txtContact.TabIndex = 38;
            this.txtContact.UseSystemPasswordChar = false;
            this.txtContact.Value = "";
            this.txtContact.VerifyCondition = null;
            this.txtContact.VerifyType = null;
            this.txtContact.VerifyTypeName = null;
            this.txtContact.WaterMark = null;
            this.txtContact.WaterMarkColor = System.Drawing.Color.Silver;
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
            this.txtCustomName.Location = new System.Drawing.Point(723, 6);
            this.txtCustomName.MaxLengh = 32767;
            this.txtCustomName.Multiline = false;
            this.txtCustomName.Name = "txtCustomName";
            this.txtCustomName.Radius = 3;
            this.txtCustomName.ReadOnly = false;
            this.txtCustomName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCustomName.ShowError = false;
            this.txtCustomName.Size = new System.Drawing.Size(120, 23);
            this.txtCustomName.TabIndex = 37;
            this.txtCustomName.UseSystemPasswordChar = false;
            this.txtCustomName.Value = "";
            this.txtCustomName.VerifyCondition = null;
            this.txtCustomName.VerifyType = null;
            this.txtCustomName.VerifyTypeName = null;
            this.txtCustomName.WaterMark = null;
            this.txtCustomName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labContactPhone
            // 
            this.labContactPhone.AutoSize = true;
            this.labContactPhone.Location = new System.Drawing.Point(641, 42);
            this.labContactPhone.Name = "labContactPhone";
            this.labContactPhone.Size = new System.Drawing.Size(77, 12);
            this.labContactPhone.TabIndex = 35;
            this.labContactPhone.Text = "联系人手机：";
            // 
            // labContact
            // 
            this.labContact.AutoSize = true;
            this.labContact.Location = new System.Drawing.Point(441, 42);
            this.labContact.Name = "labContact";
            this.labContact.Size = new System.Drawing.Size(53, 12);
            this.labContact.TabIndex = 34;
            this.labContact.Text = "联系人：";
            // 
            // labCustomName
            // 
            this.labCustomName.AutoSize = true;
            this.labCustomName.Location = new System.Drawing.Point(653, 12);
            this.labCustomName.Name = "labCustomName";
            this.labCustomName.Size = new System.Drawing.Size(65, 12);
            this.labCustomName.TabIndex = 33;
            this.labCustomName.Text = "客户名称：";
            // 
            // labCustomNO
            // 
            this.labCustomNO.AutoSize = true;
            this.labCustomNO.Location = new System.Drawing.Point(429, 12);
            this.labCustomNO.Name = "labCustomNO";
            this.labCustomNO.Size = new System.Drawing.Size(65, 12);
            this.labCustomNO.TabIndex = 32;
            this.labCustomNO.Text = "客户编码：";
            // 
            // txtEngineNo
            // 
            this.txtEngineNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtEngineNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtEngineNo.BackColor = System.Drawing.Color.Transparent;
            this.txtEngineNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtEngineNo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtEngineNo.ForeImage = null;
            this.txtEngineNo.InputtingVerifyCondition = null;
            this.txtEngineNo.Location = new System.Drawing.Point(292, 6);
            this.txtEngineNo.MaxLengh = 32767;
            this.txtEngineNo.Multiline = false;
            this.txtEngineNo.Name = "txtEngineNo";
            this.txtEngineNo.Radius = 3;
            this.txtEngineNo.ReadOnly = false;
            this.txtEngineNo.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtEngineNo.ShowError = false;
            this.txtEngineNo.Size = new System.Drawing.Size(120, 23);
            this.txtEngineNo.TabIndex = 21;
            this.txtEngineNo.UseSystemPasswordChar = false;
            this.txtEngineNo.Value = "";
            this.txtEngineNo.VerifyCondition = null;
            this.txtEngineNo.VerifyType = null;
            this.txtEngineNo.VerifyTypeName = null;
            this.txtEngineNo.WaterMark = null;
            this.txtEngineNo.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labEngineNo
            // 
            this.labEngineNo.AutoSize = true;
            this.labEngineNo.Location = new System.Drawing.Point(221, 12);
            this.labEngineNo.Name = "labEngineNo";
            this.labEngineNo.Size = new System.Drawing.Size(65, 12);
            this.labEngineNo.TabIndex = 20;
            this.labEngineNo.Text = "发动机号：";
            // 
            // labCarNO
            // 
            this.labCarNO.AutoSize = true;
            this.labCarNO.Location = new System.Drawing.Point(27, 12);
            this.labCarNO.Name = "labCarNO";
            this.labCarNO.Size = new System.Drawing.Size(53, 12);
            this.labCarNO.TabIndex = 5;
            this.labCarNO.Text = "车牌号：";
            // 
            // palData
            // 
            this.palData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palData.Controls.Add(this.dgvRData);
            this.palData.Location = new System.Drawing.Point(0, 145);
            this.palData.Name = "palData";
            this.palData.Size = new System.Drawing.Size(1030, 369);
            this.palData.TabIndex = 15;
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
            this.maintain_no,
            this.reception_time,
            this.customer_code,
            this.customer_name,
            this.fault_describe,
            this.maintain_man,
            this.oil_into_factory,
            this.travel_mileage,
            this.vehicle_no,
            this.linkman,
            this.link_man_mobile,
            this.info_status,
            this.completion_time,
            this.maintain_payment,
            this.remark,
            this.maintain_id,
            this.before_orderId,
            this.orders_source});
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
            this.dgvRData.Size = new System.Drawing.Size(1024, 363);
            this.dgvRData.TabIndex = 12;
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
            // maintain_no
            // 
            this.maintain_no.DataPropertyName = "maintain_no";
            this.maintain_no.HeaderText = "维修单号";
            this.maintain_no.MinimumWidth = 120;
            this.maintain_no.Name = "maintain_no";
            this.maintain_no.ReadOnly = true;
            this.maintain_no.Width = 120;
            // 
            // reception_time
            // 
            this.reception_time.DataPropertyName = "reception_time";
            this.reception_time.HeaderText = "接待时间";
            this.reception_time.Name = "reception_time";
            this.reception_time.ReadOnly = true;
            this.reception_time.Width = 90;
            // 
            // customer_code
            // 
            this.customer_code.DataPropertyName = "customer_code";
            this.customer_code.HeaderText = "客户编码";
            this.customer_code.Name = "customer_code";
            this.customer_code.ReadOnly = true;
            this.customer_code.Width = 90;
            // 
            // customer_name
            // 
            this.customer_name.DataPropertyName = "customer_name";
            this.customer_name.HeaderText = "客户名称";
            this.customer_name.Name = "customer_name";
            this.customer_name.ReadOnly = true;
            this.customer_name.Width = 90;
            // 
            // fault_describe
            // 
            this.fault_describe.DataPropertyName = "fault_describe";
            this.fault_describe.HeaderText = "故障描述";
            this.fault_describe.Name = "fault_describe";
            this.fault_describe.ReadOnly = true;
            // 
            // maintain_man
            // 
            this.maintain_man.DataPropertyName = "maintain_man";
            this.maintain_man.HeaderText = "服务顾问";
            this.maintain_man.Name = "maintain_man";
            this.maintain_man.ReadOnly = true;
            // 
            // oil_into_factory
            // 
            this.oil_into_factory.DataPropertyName = "oil_into_factory";
            this.oil_into_factory.HeaderText = "进厂油量(%)";
            this.oil_into_factory.Name = "oil_into_factory";
            this.oil_into_factory.ReadOnly = true;
            this.oil_into_factory.Width = 120;
            // 
            // travel_mileage
            // 
            this.travel_mileage.DataPropertyName = "travel_mileage";
            this.travel_mileage.HeaderText = "行驶里程(Km)";
            this.travel_mileage.Name = "travel_mileage";
            this.travel_mileage.ReadOnly = true;
            this.travel_mileage.Width = 120;
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
            this.linkman.Width = 70;
            // 
            // link_man_mobile
            // 
            this.link_man_mobile.DataPropertyName = "link_man_mobile";
            this.link_man_mobile.HeaderText = "联系人手机";
            this.link_man_mobile.Name = "link_man_mobile";
            this.link_man_mobile.ReadOnly = true;
            // 
            // info_status
            // 
            this.info_status.DataPropertyName = "info_status";
            this.info_status.HeaderText = "单据状态";
            this.info_status.Name = "info_status";
            this.info_status.ReadOnly = true;
            this.info_status.Width = 90;
            // 
            // completion_time
            // 
            this.completion_time.DataPropertyName = "completion_time";
            this.completion_time.HeaderText = "预计完工时间";
            this.completion_time.Name = "completion_time";
            this.completion_time.ReadOnly = true;
            this.completion_time.Width = 110;
            // 
            // maintain_payment
            // 
            this.maintain_payment.DataPropertyName = "maintain_payment";
            this.maintain_payment.HeaderText = "维修付费方式";
            this.maintain_payment.Name = "maintain_payment";
            this.maintain_payment.ReadOnly = true;
            this.maintain_payment.Width = 110;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            // 
            // maintain_id
            // 
            this.maintain_id.DataPropertyName = "maintain_id";
            this.maintain_id.HeaderText = "maintain_id";
            this.maintain_id.Name = "maintain_id";
            this.maintain_id.ReadOnly = true;
            this.maintain_id.Visible = false;
            this.maintain_id.Width = 10;
            // 
            // before_orderId
            // 
            this.before_orderId.DataPropertyName = "before_orderId";
            this.before_orderId.HeaderText = "before_orderId";
            this.before_orderId.Name = "before_orderId";
            this.before_orderId.ReadOnly = true;
            this.before_orderId.Visible = false;
            // 
            // orders_source
            // 
            this.orders_source.DataPropertyName = "orders_source";
            this.orders_source.HeaderText = "orders_source";
            this.orders_source.Name = "orders_source";
            this.orders_source.ReadOnly = true;
            this.orders_source.Visible = false;
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
            this.panelEx2.TabIndex = 16;
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
            // 预约单导入
            // 
            this.预约单导入.Name = "预约单导入";
            this.预约单导入.Size = new System.Drawing.Size(192, 22);
            this.预约单导入.Text = "toolStripMenuItem1";
            // 
            // 返修单导入
            // 
            this.返修单导入.Name = "返修单导入";
            this.返修单导入.Size = new System.Drawing.Size(192, 22);
            this.返修单导入.Text = "toolStripMenuItem2";
            // 
            // UCReceiveManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.palData);
            this.Controls.Add(this.palInfo);
            this.Name = "UCReceiveManager";
            this.Load += new System.EventHandler(this.UCReceiveManager_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.palInfo, 0);
            this.Controls.SetChildIndex(this.palData, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.palInfo.ResumeLayout(false);
            this.palInfo.PerformLayout();
            this.palData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel palInfo;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCustomNO;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCarNO;
        private System.Windows.Forms.ComboBox cobPayType;
        private System.Windows.Forms.Label labPayType;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContactPhone;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContact;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCustomName;
        private System.Windows.Forms.Label labContactPhone;
        private System.Windows.Forms.Label labContact;
        private System.Windows.Forms.Label labCustomName;
        private System.Windows.Forms.Label labCustomNO;
        private ServiceStationClient.ComponentUI.TextBoxEx txtEngineNo;
        private System.Windows.Forms.Label labEngineNo;
        private System.Windows.Forms.Label labCarNO;
        private ServiceStationClient.ComponentUI.TextBoxEx txtOrder;
        private System.Windows.Forms.Label labOrder;
        private ServiceStationClient.ComponentUI.TextBoxEx txtAdvisor;
        private System.Windows.Forms.Label labAdvisor;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpReserveETime;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpReserveSTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labReserveTime;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobOrderStatus;
        private System.Windows.Forms.Label labOrderStatus;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Panel palData;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRData;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private System.Windows.Forms.ToolStripMenuItem 预约单导入;
        private System.Windows.Forms.ToolStripMenuItem 返修单导入;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn maintain_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn reception_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn fault_describe;
        private System.Windows.Forms.DataGridViewTextBoxColumn maintain_man;
        private System.Windows.Forms.DataGridViewTextBoxColumn oil_into_factory;
        private System.Windows.Forms.DataGridViewTextBoxColumn travel_mileage;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkman;
        private System.Windows.Forms.DataGridViewTextBoxColumn link_man_mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn info_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn completion_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn maintain_payment;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn maintain_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn before_orderId;
        private System.Windows.Forms.DataGridViewTextBoxColumn orders_source;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiSubmit;
        private System.Windows.Forms.ToolStripMenuItem tsmiVerify;
    }
}