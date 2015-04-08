namespace HXCPcClient.UCForm.RepairBusiness.FetchMaterial
{
    partial class UCFetchMaterialManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCFetchMaterialManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.palTop = new ServiceStationClient.ComponentUI.PanelEx();
            this.cobWarehouse = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.labAllocation = new System.Windows.Forms.Label();
            this.txtFetchOpid = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txtMaterialNo = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labMaterialNo = new System.Windows.Forms.Label();
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEFetchTime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpSFetchTime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.label4 = new System.Windows.Forms.Label();
            this.labFetchTime = new System.Windows.Forms.Label();
            this.cobOrderStatus = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.labOrderStatus = new System.Windows.Forms.Label();
            this.labFetchOpid = new System.Windows.Forms.Label();
            this.txtMaintainId = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labMaintainId = new System.Windows.Forms.Label();
            this.txtCustomName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labCustomName = new System.Windows.Forms.Label();
            this.txtCustomNO = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.labCustomNO = new System.Windows.Forms.Label();
            this.txtCarNO = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.labCarNO = new System.Windows.Forms.Label();
            this.palBottom = new ServiceStationClient.ComponentUI.PanelEx();
            this.dgvRData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.material_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fetch_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fetch_opid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.info_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maintain_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkman = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.link_man_mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.warehouse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fetch_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.palTop.Controls.Add(this.cobWarehouse);
            this.palTop.Controls.Add(this.labAllocation);
            this.palTop.Controls.Add(this.txtFetchOpid);
            this.palTop.Controls.Add(this.txtMaterialNo);
            this.palTop.Controls.Add(this.labMaterialNo);
            this.palTop.Controls.Add(this.btnQuery);
            this.palTop.Controls.Add(this.btnClear);
            this.palTop.Controls.Add(this.label2);
            this.palTop.Controls.Add(this.dtpEFetchTime);
            this.palTop.Controls.Add(this.dtpSFetchTime);
            this.palTop.Controls.Add(this.label4);
            this.palTop.Controls.Add(this.labFetchTime);
            this.palTop.Controls.Add(this.cobOrderStatus);
            this.palTop.Controls.Add(this.labOrderStatus);
            this.palTop.Controls.Add(this.labFetchOpid);
            this.palTop.Controls.Add(this.txtMaintainId);
            this.palTop.Controls.Add(this.labMaintainId);
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
            this.palTop.Size = new System.Drawing.Size(1030, 111);
            this.palTop.TabIndex = 4;
            // 
            // cobWarehouse
            // 
            this.cobWarehouse.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobWarehouse.FormattingEnabled = true;
            this.cobWarehouse.Location = new System.Drawing.Point(502, 68);
            this.cobWarehouse.Name = "cobWarehouse";
            this.cobWarehouse.Size = new System.Drawing.Size(121, 22);
            this.cobWarehouse.TabIndex = 115;
            this.cobWarehouse.Visible = false;
            // 
            // labAllocation
            // 
            this.labAllocation.AutoSize = true;
            this.labAllocation.Location = new System.Drawing.Point(431, 72);
            this.labAllocation.Name = "labAllocation";
            this.labAllocation.Size = new System.Drawing.Size(65, 12);
            this.labAllocation.TabIndex = 114;
            this.labAllocation.Text = "仓库名称：";
            this.labAllocation.Visible = false;
            // 
            // txtFetchOpid
            // 
            this.txtFetchOpid.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtFetchOpid.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFetchOpid.Location = new System.Drawing.Point(292, 39);
            this.txtFetchOpid.Name = "txtFetchOpid";
            this.txtFetchOpid.ReadOnly = false;
            this.txtFetchOpid.Size = new System.Drawing.Size(120, 24);
            this.txtFetchOpid.TabIndex = 113;
            this.txtFetchOpid.ToolTipTitle = "";
            this.txtFetchOpid.ChooserClick += new System.EventHandler(this.txtFetchOpid_ChooserClick);
            // 
            // txtMaterialNo
            // 
            this.txtMaterialNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtMaterialNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtMaterialNo.BackColor = System.Drawing.Color.Transparent;
            this.txtMaterialNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtMaterialNo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtMaterialNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMaterialNo.ForeImage = null;
            this.txtMaterialNo.InputtingVerifyCondition = null;
            this.txtMaterialNo.Location = new System.Drawing.Point(85, 6);
            this.txtMaterialNo.MaxLengh = 32767;
            this.txtMaterialNo.Multiline = false;
            this.txtMaterialNo.Name = "txtMaterialNo";
            this.txtMaterialNo.Radius = 3;
            this.txtMaterialNo.ReadOnly = false;
            this.txtMaterialNo.SelectStart = 0;
            this.txtMaterialNo.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtMaterialNo.ShowError = false;
            this.txtMaterialNo.Size = new System.Drawing.Size(120, 23);
            this.txtMaterialNo.TabIndex = 112;
            this.txtMaterialNo.UseSystemPasswordChar = false;
            this.txtMaterialNo.Value = "";
            this.txtMaterialNo.VerifyCondition = null;
            this.txtMaterialNo.VerifyType = null;
            this.txtMaterialNo.VerifyTypeName = null;
            this.txtMaterialNo.WaterMark = null;
            this.txtMaterialNo.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labMaterialNo
            // 
            this.labMaterialNo.AutoSize = true;
            this.labMaterialNo.Location = new System.Drawing.Point(18, 12);
            this.labMaterialNo.Name = "labMaterialNo";
            this.labMaterialNo.Size = new System.Drawing.Size(65, 12);
            this.labMaterialNo.TabIndex = 111;
            this.labMaterialNo.Text = "领料单号：";
            // 
            // btnQuery
            // 
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(845, 65);
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
            this.btnClear.Location = new System.Drawing.Point(845, 32);
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
            this.label2.Location = new System.Drawing.Point(82, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 103;
            this.label2.Text = "从";
            // 
            // dtpEFetchTime
            // 
            this.dtpEFetchTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpEFetchTime.Location = new System.Drawing.Point(273, 69);
            this.dtpEFetchTime.Name = "dtpEFetchTime";
            this.dtpEFetchTime.ShowFormat = "yyyy-MM-dd HH:mm";
            this.dtpEFetchTime.Size = new System.Drawing.Size(138, 21);
            this.dtpEFetchTime.TabIndex = 102;
            this.dtpEFetchTime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // dtpSFetchTime
            // 
            this.dtpSFetchTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpSFetchTime.Location = new System.Drawing.Point(105, 69);
            this.dtpSFetchTime.Name = "dtpSFetchTime";
            this.dtpSFetchTime.ShowFormat = "yyyy-MM-dd HH:mm";
            this.dtpSFetchTime.Size = new System.Drawing.Size(139, 21);
            this.dtpSFetchTime.TabIndex = 101;
            this.dtpSFetchTime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(250, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 100;
            this.label4.Text = "到";
            // 
            // labFetchTime
            // 
            this.labFetchTime.AutoSize = true;
            this.labFetchTime.Location = new System.Drawing.Point(13, 72);
            this.labFetchTime.Name = "labFetchTime";
            this.labFetchTime.Size = new System.Drawing.Size(65, 12);
            this.labFetchTime.TabIndex = 99;
            this.labFetchTime.Text = "领料时间：";
            // 
            // cobOrderStatus
            // 
            this.cobOrderStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobOrderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobOrderStatus.FormattingEnabled = true;
            this.cobOrderStatus.Location = new System.Drawing.Point(501, 36);
            this.cobOrderStatus.Name = "cobOrderStatus";
            this.cobOrderStatus.Size = new System.Drawing.Size(121, 22);
            this.cobOrderStatus.TabIndex = 78;
            // 
            // labOrderStatus
            // 
            this.labOrderStatus.AutoSize = true;
            this.labOrderStatus.Location = new System.Drawing.Point(430, 43);
            this.labOrderStatus.Name = "labOrderStatus";
            this.labOrderStatus.Size = new System.Drawing.Size(65, 12);
            this.labOrderStatus.TabIndex = 77;
            this.labOrderStatus.Text = "单据状态：";
            // 
            // labFetchOpid
            // 
            this.labFetchOpid.AutoSize = true;
            this.labFetchOpid.Location = new System.Drawing.Point(238, 44);
            this.labFetchOpid.Name = "labFetchOpid";
            this.labFetchOpid.Size = new System.Drawing.Size(53, 12);
            this.labFetchOpid.TabIndex = 74;
            this.labFetchOpid.Text = "领料人：";
            // 
            // txtMaintainId
            // 
            this.txtMaintainId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtMaintainId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtMaintainId.BackColor = System.Drawing.Color.Transparent;
            this.txtMaintainId.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtMaintainId.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtMaintainId.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMaintainId.ForeImage = null;
            this.txtMaintainId.InputtingVerifyCondition = null;
            this.txtMaintainId.Location = new System.Drawing.Point(85, 40);
            this.txtMaintainId.MaxLengh = 32767;
            this.txtMaintainId.Multiline = false;
            this.txtMaintainId.Name = "txtMaintainId";
            this.txtMaintainId.Radius = 3;
            this.txtMaintainId.ReadOnly = false;
            this.txtMaintainId.SelectStart = 0;
            this.txtMaintainId.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtMaintainId.ShowError = false;
            this.txtMaintainId.Size = new System.Drawing.Size(120, 23);
            this.txtMaintainId.TabIndex = 73;
            this.txtMaintainId.UseSystemPasswordChar = false;
            this.txtMaintainId.Value = "";
            this.txtMaintainId.VerifyCondition = null;
            this.txtMaintainId.VerifyType = null;
            this.txtMaintainId.VerifyTypeName = null;
            this.txtMaintainId.WaterMark = null;
            this.txtMaintainId.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labMaintainId
            // 
            this.labMaintainId.AutoSize = true;
            this.labMaintainId.Location = new System.Drawing.Point(14, 46);
            this.labMaintainId.Name = "labMaintainId";
            this.labMaintainId.Size = new System.Drawing.Size(65, 12);
            this.labMaintainId.TabIndex = 72;
            this.labMaintainId.Text = "业务单号：";
            // 
            // txtCustomName
            // 
            this.txtCustomName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCustomName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCustomName.BackColor = System.Drawing.Color.Transparent;
            this.txtCustomName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCustomName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCustomName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCustomName.ForeImage = null;
            this.txtCustomName.InputtingVerifyCondition = null;
            this.txtCustomName.Location = new System.Drawing.Point(709, 6);
            this.txtCustomName.MaxLengh = 32767;
            this.txtCustomName.Multiline = false;
            this.txtCustomName.Name = "txtCustomName";
            this.txtCustomName.Radius = 3;
            this.txtCustomName.ReadOnly = false;
            this.txtCustomName.SelectStart = 0;
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
            this.labCustomName.Location = new System.Drawing.Point(638, 12);
            this.labCustomName.Name = "labCustomName";
            this.labCustomName.Size = new System.Drawing.Size(65, 12);
            this.labCustomName.TabIndex = 68;
            this.labCustomName.Text = "客户名称：";
            // 
            // txtCustomNO
            // 
            this.txtCustomNO.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCustomNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCustomNO.Location = new System.Drawing.Point(501, 6);
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
            this.labCustomNO.Location = new System.Drawing.Point(430, 12);
            this.labCustomNO.Name = "labCustomNO";
            this.labCustomNO.Size = new System.Drawing.Size(65, 12);
            this.labCustomNO.TabIndex = 66;
            this.labCustomNO.Text = "客户编码：";
            // 
            // txtCarNO
            // 
            this.txtCarNO.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCarNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarNO.Location = new System.Drawing.Point(292, 6);
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
            this.labCarNO.Location = new System.Drawing.Point(238, 12);
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
            this.palBottom.Location = new System.Drawing.Point(0, 148);
            this.palBottom.Name = "palBottom";
            this.palBottom.Size = new System.Drawing.Size(1030, 362);
            this.palBottom.TabIndex = 5;
            // 
            // dgvRData
            // 
            this.dgvRData.AllowUserToAddRows = false;
            this.dgvRData.AllowUserToDeleteRows = false;
            this.dgvRData.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvRData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvRData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRData.BackgroundColor = System.Drawing.Color.White;
            this.dgvRData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvRData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.material_num,
            this.fetch_time,
            this.customer_name,
            this.customer_code,
            this.vehicle_no,
            this.fetch_opid,
            this.info_status,
            this.quantity,
            this.maintain_id,
            this.linkman,
            this.link_man_mobile,
            this.warehouse,
            this.remarks,
            this.fetch_id});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRData.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvRData.EnableHeadersVisualStyles = false;
            this.dgvRData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvRData.IsCheck = true;
            this.dgvRData.Location = new System.Drawing.Point(3, 3);
            this.dgvRData.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvRData.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvRData.MergeColumnNames")));
            this.dgvRData.MultiSelect = false;
            this.dgvRData.Name = "dgvRData";
            this.dgvRData.ReadOnly = true;
            this.dgvRData.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvRData.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvRData.RowHeadersVisible = false;
            this.dgvRData.RowHeadersWidth = 30;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvRData.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvRData.RowTemplate.Height = 23;
            this.dgvRData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRData.ShowCheckBox = true;
            this.dgvRData.Size = new System.Drawing.Size(1024, 354);
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
            // material_num
            // 
            this.material_num.DataPropertyName = "material_num";
            this.material_num.HeaderText = "领料单号";
            this.material_num.MinimumWidth = 120;
            this.material_num.Name = "material_num";
            this.material_num.ReadOnly = true;
            this.material_num.Width = 120;
            // 
            // fetch_time
            // 
            this.fetch_time.DataPropertyName = "fetch_time";
            this.fetch_time.HeaderText = "领料时间";
            this.fetch_time.Name = "fetch_time";
            this.fetch_time.ReadOnly = true;
            // 
            // customer_name
            // 
            this.customer_name.DataPropertyName = "customer_name";
            this.customer_name.HeaderText = "客户名称";
            this.customer_name.Name = "customer_name";
            this.customer_name.ReadOnly = true;
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
            // 
            // fetch_opid
            // 
            this.fetch_opid.DataPropertyName = "fetch_opid";
            this.fetch_opid.HeaderText = "领料人";
            this.fetch_opid.Name = "fetch_opid";
            this.fetch_opid.ReadOnly = true;
            this.fetch_opid.Width = 90;
            // 
            // info_status
            // 
            this.info_status.DataPropertyName = "info_status";
            this.info_status.HeaderText = "单据状态";
            this.info_status.Name = "info_status";
            this.info_status.ReadOnly = true;
            this.info_status.Width = 90;
            // 
            // quantity
            // 
            this.quantity.DataPropertyName = "quantity";
            this.quantity.HeaderText = "数量";
            this.quantity.Name = "quantity";
            this.quantity.ReadOnly = true;
            this.quantity.Visible = false;
            // 
            // maintain_id
            // 
            this.maintain_id.DataPropertyName = "maintain_id";
            this.maintain_id.HeaderText = "业务单号";
            this.maintain_id.Name = "maintain_id";
            this.maintain_id.ReadOnly = true;
            this.maintain_id.Width = 110;
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
            // warehouse
            // 
            this.warehouse.DataPropertyName = "warehouse";
            this.warehouse.HeaderText = "仓库名称";
            this.warehouse.Name = "warehouse";
            this.warehouse.ReadOnly = true;
            this.warehouse.Visible = false;
            this.warehouse.Width = 90;
            // 
            // remarks
            // 
            this.remarks.DataPropertyName = "remarks";
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            // 
            // fetch_id
            // 
            this.fetch_id.DataPropertyName = "fetch_id";
            this.fetch_id.HeaderText = "fetch_id";
            this.fetch_id.Name = "fetch_id";
            this.fetch_id.ReadOnly = true;
            this.fetch_id.Visible = false;
            this.fetch_id.Width = 10;
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
            this.panelEx2.TabIndex = 18;
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
            // UCFetchMaterialManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.palBottom);
            this.Controls.Add(this.palTop);
            this.Name = "UCFetchMaterialManager";
            this.Load += new System.EventHandler(this.UCFetchMaterialManager_Load);
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
        private ServiceStationClient.ComponentUI.TextBoxEx txtMaterialNo;
        private System.Windows.Forms.Label labMaterialNo;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpEFetchTime;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpSFetchTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labFetchTime;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobOrderStatus;
        private System.Windows.Forms.Label labOrderStatus;
        private System.Windows.Forms.Label labFetchOpid;
        private ServiceStationClient.ComponentUI.TextBoxEx txtMaintainId;
        private System.Windows.Forms.Label labMaintainId;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCustomName;
        private System.Windows.Forms.Label labCustomName;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCustomNO;
        private System.Windows.Forms.Label labCustomNO;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCarNO;
        private System.Windows.Forms.Label labCarNO;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobWarehouse;
        private System.Windows.Forms.Label labAllocation;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtFetchOpid;
        private ServiceStationClient.ComponentUI.PanelEx palBottom;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRData;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiSubmit;
        private System.Windows.Forms.ToolStripMenuItem tsmiVerify;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn material_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn fetch_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn fetch_opid;
        private System.Windows.Forms.DataGridViewTextBoxColumn info_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn maintain_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkman;
        private System.Windows.Forms.DataGridViewTextBoxColumn link_man_mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn warehouse;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn fetch_id;
    }
}