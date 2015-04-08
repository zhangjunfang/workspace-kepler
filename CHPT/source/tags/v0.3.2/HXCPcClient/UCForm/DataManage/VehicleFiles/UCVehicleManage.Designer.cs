namespace HXCPcClient.UCForm.DataManage.VehicleFiles
{
    partial class UCVehicleManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCVehicleManage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.palTop = new ServiceStationClient.ComponentUI.PanelEx();
            this.cobDataSources = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txtYardCode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labYardCode = new System.Windows.Forms.Label();
            this.txtCustomNO = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.labCustomNO = new System.Windows.Forms.Label();
            this.txtCarType = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVIN = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labVIN = new System.Windows.Forms.Label();
            this.dtpCETime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpCSTime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.label1 = new System.Windows.Forms.Label();
            this.labCTime = new System.Windows.Forms.Label();
            this.cobCarBrand = new System.Windows.Forms.ComboBox();
            this.labCarBrand = new System.Windows.Forms.Label();
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.cobStatus = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.labOrderStatus = new System.Windows.Forms.Label();
            this.txtCarNO = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.labCarNO = new System.Windows.Forms.Label();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.license_plate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.v_brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.v_model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.carbuy_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_factory_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data_source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.v_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiStart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOperation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.palTop.SuspendLayout();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.cmsMenu.SuspendLayout();
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
            this.palTop.Controls.Add(this.cobDataSources);
            this.palTop.Controls.Add(this.label3);
            this.palTop.Controls.Add(this.txtYardCode);
            this.palTop.Controls.Add(this.labYardCode);
            this.palTop.Controls.Add(this.txtCustomNO);
            this.palTop.Controls.Add(this.labCustomNO);
            this.palTop.Controls.Add(this.txtCarType);
            this.palTop.Controls.Add(this.label2);
            this.palTop.Controls.Add(this.txtVIN);
            this.palTop.Controls.Add(this.labVIN);
            this.palTop.Controls.Add(this.dtpCETime);
            this.palTop.Controls.Add(this.dtpCSTime);
            this.palTop.Controls.Add(this.label1);
            this.palTop.Controls.Add(this.labCTime);
            this.palTop.Controls.Add(this.cobCarBrand);
            this.palTop.Controls.Add(this.labCarBrand);
            this.palTop.Controls.Add(this.btnQuery);
            this.palTop.Controls.Add(this.btnClear);
            this.palTop.Controls.Add(this.cobStatus);
            this.palTop.Controls.Add(this.labOrderStatus);
            this.palTop.Controls.Add(this.txtCarNO);
            this.palTop.Controls.Add(this.labCarNO);
            this.palTop.Curvature = 0;
            this.palTop.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.palTop.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.palTop.Location = new System.Drawing.Point(0, 33);
            this.palTop.Name = "palTop";
            this.palTop.Size = new System.Drawing.Size(1030, 104);
            this.palTop.TabIndex = 5;
            // 
            // cobDataSources
            // 
            this.cobDataSources.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobDataSources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobDataSources.FormattingEnabled = true;
            this.cobDataSources.Location = new System.Drawing.Point(474, 9);
            this.cobDataSources.Name = "cobDataSources";
            this.cobDataSources.Size = new System.Drawing.Size(121, 22);
            this.cobDataSources.TabIndex = 138;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(410, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 137;
            this.label3.Text = "数据来源：";
            // 
            // txtYardCode
            // 
            this.txtYardCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtYardCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtYardCode.BackColor = System.Drawing.Color.Transparent;
            this.txtYardCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtYardCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtYardCode.ForeImage = null;
            this.txtYardCode.Location = new System.Drawing.Point(662, 6);
            this.txtYardCode.MaxLengh = 32767;
            this.txtYardCode.Multiline = false;
            this.txtYardCode.Name = "txtYardCode";
            this.txtYardCode.Radius = 3;
            this.txtYardCode.ReadOnly = false;
            this.txtYardCode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtYardCode.ShowError = false;
            this.txtYardCode.Size = new System.Drawing.Size(120, 23);
            this.txtYardCode.TabIndex = 136;
            this.txtYardCode.UseSystemPasswordChar = false;
            this.txtYardCode.Value = "";
            this.txtYardCode.VerifyCondition = null;
            this.txtYardCode.VerifyType = null;
            this.txtYardCode.WaterMark = null;
            this.txtYardCode.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labYardCode
            // 
            this.labYardCode.AutoSize = true;
            this.labYardCode.Location = new System.Drawing.Point(601, 12);
            this.labYardCode.Name = "labYardCode";
            this.labYardCode.Size = new System.Drawing.Size(65, 12);
            this.labYardCode.TabIndex = 135;
            this.labYardCode.Text = "车场编号：";
            // 
            // txtCustomNO
            // 
            this.txtCustomNO.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCustomNO.Location = new System.Drawing.Point(478, 35);
            this.txtCustomNO.Name = "txtCustomNO";
            this.txtCustomNO.ReadOnly = false;
            this.txtCustomNO.Size = new System.Drawing.Size(117, 24);
            this.txtCustomNO.TabIndex = 126;
            this.txtCustomNO.ToolTipTitle = "";
            this.txtCustomNO.ChooserClick += new System.EventHandler(this.txtCustomNO_ChooserClick);
            // 
            // labCustomNO
            // 
            this.labCustomNO.AutoSize = true;
            this.labCustomNO.Location = new System.Drawing.Point(410, 42);
            this.labCustomNO.Name = "labCustomNO";
            this.labCustomNO.Size = new System.Drawing.Size(65, 12);
            this.labCustomNO.TabIndex = 125;
            this.labCustomNO.Text = "所属客户：";
            // 
            // txtCarType
            // 
            this.txtCarType.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCarType.Location = new System.Drawing.Point(276, 35);
            this.txtCarType.Name = "txtCarType";
            this.txtCarType.ReadOnly = false;
            this.txtCarType.Size = new System.Drawing.Size(120, 24);
            this.txtCarType.TabIndex = 124;
            this.txtCarType.ToolTipTitle = "";
            this.txtCarType.ChooserClick += new System.EventHandler(this.txtCarType_ChooserClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 123;
            this.label2.Text = "车型：";
            // 
            // txtVIN
            // 
            this.txtVIN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtVIN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtVIN.BackColor = System.Drawing.Color.Transparent;
            this.txtVIN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtVIN.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtVIN.ForeImage = null;
            this.txtVIN.Location = new System.Drawing.Point(62, 36);
            this.txtVIN.MaxLengh = 32767;
            this.txtVIN.Multiline = false;
            this.txtVIN.Name = "txtVIN";
            this.txtVIN.Radius = 3;
            this.txtVIN.ReadOnly = false;
            this.txtVIN.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtVIN.ShowError = false;
            this.txtVIN.Size = new System.Drawing.Size(120, 23);
            this.txtVIN.TabIndex = 122;
            this.txtVIN.UseSystemPasswordChar = false;
            this.txtVIN.Value = "";
            this.txtVIN.VerifyCondition = null;
            this.txtVIN.VerifyType = null;
            this.txtVIN.WaterMark = null;
            this.txtVIN.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labVIN
            // 
            this.labVIN.AutoSize = true;
            this.labVIN.Location = new System.Drawing.Point(33, 39);
            this.labVIN.Name = "labVIN";
            this.labVIN.Size = new System.Drawing.Size(35, 12);
            this.labVIN.TabIndex = 121;
            this.labVIN.Text = "VIN：";
            // 
            // dtpCETime
            // 
            this.dtpCETime.Location = new System.Drawing.Point(248, 67);
            this.dtpCETime.Name = "dtpCETime";
            this.dtpCETime.ShowFormat = "yyyy-MM-dd";
            this.dtpCETime.Size = new System.Drawing.Size(148, 21);
            this.dtpCETime.TabIndex = 120;
            this.dtpCETime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // dtpCSTime
            // 
            this.dtpCSTime.Location = new System.Drawing.Point(62, 67);
            this.dtpCSTime.Name = "dtpCSTime";
            this.dtpCSTime.ShowFormat = "yyyy-MM-dd";
            this.dtpCSTime.Size = new System.Drawing.Size(157, 21);
            this.dtpCSTime.TabIndex = 119;
            this.dtpCSTime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(225, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 118;
            this.label1.Text = "至";
            // 
            // labCTime
            // 
            this.labCTime.AutoSize = true;
            this.labCTime.Location = new System.Drawing.Point(3, 69);
            this.labCTime.Name = "labCTime";
            this.labCTime.Size = new System.Drawing.Size(65, 12);
            this.labCTime.TabIndex = 117;
            this.labCTime.Text = "创建时间：";
            // 
            // cobCarBrand
            // 
            this.cobCarBrand.FormattingEnabled = true;
            this.cobCarBrand.Location = new System.Drawing.Point(276, 9);
            this.cobCarBrand.Name = "cobCarBrand";
            this.cobCarBrand.Size = new System.Drawing.Size(117, 20);
            this.cobCarBrand.TabIndex = 114;
            // 
            // labCarBrand
            // 
            this.labCarBrand.AutoSize = true;
            this.labCarBrand.Location = new System.Drawing.Point(202, 12);
            this.labCarBrand.Name = "labCarBrand";
            this.labCarBrand.Size = new System.Drawing.Size(65, 12);
            this.labCarBrand.TabIndex = 113;
            this.labCarBrand.Text = "车辆品牌：";
            // 
            // btnQuery
            // 
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(797, 55);
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
            this.btnClear.Location = new System.Drawing.Point(797, 23);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 109;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cobStatus
            // 
            this.cobStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobStatus.FormattingEnabled = true;
            this.cobStatus.Location = new System.Drawing.Point(661, 36);
            this.cobStatus.Name = "cobStatus";
            this.cobStatus.Size = new System.Drawing.Size(121, 22);
            this.cobStatus.TabIndex = 78;
            // 
            // labOrderStatus
            // 
            this.labOrderStatus.AutoSize = true;
            this.labOrderStatus.Location = new System.Drawing.Point(611, 39);
            this.labOrderStatus.Name = "labOrderStatus";
            this.labOrderStatus.Size = new System.Drawing.Size(41, 12);
            this.labOrderStatus.TabIndex = 77;
            this.labOrderStatus.Text = "状态：";
            // 
            // txtCarNO
            // 
            this.txtCarNO.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCarNO.Location = new System.Drawing.Point(63, 6);
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
            this.labCarNO.Location = new System.Drawing.Point(15, 12);
            this.labCarNO.Name = "labCarNO";
            this.labCarNO.Size = new System.Drawing.Size(53, 12);
            this.labCarNO.TabIndex = 64;
            this.labCarNO.Text = "车牌号：";
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Location = new System.Drawing.Point(0, 146);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(1025, 367);
            this.tabControlEx1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvData);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1017, 337);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "车辆列表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.license_plate,
            this.vin,
            this.v_brand,
            this.v_model,
            this.carbuy_date,
            this.car_factory_num,
            this.cust_name,
            this.data_source,
            this.user_name,
            this.create_time,
            this.status,
            this.remark,
            this.v_id});
            this.dgvData.ContextMenuStrip = this.cmsMenu;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.EnableHeadersVisualStyles = false;
            this.dgvData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvData.Location = new System.Drawing.Point(6, 4);
            this.dgvData.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvData.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvData.MergeColumnNames")));
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvData.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.ShowCheckBox = true;
            this.dgvData.Size = new System.Drawing.Size(1005, 325);
            this.dgvData.TabIndex = 1;
            this.dgvData.HeadCheckChanged += new ServiceStationClient.ComponentUI.DataGridViewEx.DelegateOnClick(this.dgvData_HeadCheckChanged);
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            this.dgvData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellDoubleClick);
            this.dgvData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvData_CellFormatting);
            this.dgvData.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_CellMouseClick);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 18;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCheck.Width = 19;
            // 
            // license_plate
            // 
            this.license_plate.DataPropertyName = "license_plate";
            this.license_plate.HeaderText = "车牌号";
            this.license_plate.MinimumWidth = 100;
            this.license_plate.Name = "license_plate";
            this.license_plate.ReadOnly = true;
            // 
            // vin
            // 
            this.vin.DataPropertyName = "vin";
            this.vin.HeaderText = "VIN";
            this.vin.MinimumWidth = 100;
            this.vin.Name = "vin";
            this.vin.ReadOnly = true;
            // 
            // v_brand
            // 
            this.v_brand.DataPropertyName = "v_brand";
            this.v_brand.HeaderText = "车辆品牌";
            this.v_brand.MinimumWidth = 100;
            this.v_brand.Name = "v_brand";
            this.v_brand.ReadOnly = true;
            // 
            // v_model
            // 
            this.v_model.DataPropertyName = "v_model";
            this.v_model.HeaderText = "车型";
            this.v_model.MinimumWidth = 100;
            this.v_model.Name = "v_model";
            this.v_model.ReadOnly = true;
            // 
            // carbuy_date
            // 
            this.carbuy_date.DataPropertyName = "carbuy_date";
            this.carbuy_date.HeaderText = "购车日期";
            this.carbuy_date.MinimumWidth = 100;
            this.carbuy_date.Name = "carbuy_date";
            this.carbuy_date.ReadOnly = true;
            // 
            // car_factory_num
            // 
            this.car_factory_num.DataPropertyName = "car_factory_num";
            this.car_factory_num.HeaderText = "车场编号";
            this.car_factory_num.MinimumWidth = 100;
            this.car_factory_num.Name = "car_factory_num";
            this.car_factory_num.ReadOnly = true;
            // 
            // cust_name
            // 
            this.cust_name.DataPropertyName = "cust_name";
            this.cust_name.HeaderText = "所属客户";
            this.cust_name.MinimumWidth = 100;
            this.cust_name.Name = "cust_name";
            this.cust_name.ReadOnly = true;
            // 
            // data_source
            // 
            this.data_source.DataPropertyName = "data_source";
            this.data_source.HeaderText = "数据来源";
            this.data_source.MinimumWidth = 100;
            this.data_source.Name = "data_source";
            this.data_source.ReadOnly = true;
            // 
            // user_name
            // 
            this.user_name.DataPropertyName = "user_name";
            this.user_name.HeaderText = "创建人";
            this.user_name.MinimumWidth = 100;
            this.user_name.Name = "user_name";
            this.user_name.ReadOnly = true;
            // 
            // create_time
            // 
            this.create_time.DataPropertyName = "create_time";
            this.create_time.HeaderText = "创建时间";
            this.create_time.MinimumWidth = 100;
            this.create_time.Name = "create_time";
            this.create_time.ReadOnly = true;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 57;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.MinimumWidth = 100;
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            // 
            // v_id
            // 
            this.v_id.DataPropertyName = "v_id";
            this.v_id.HeaderText = "v_id";
            this.v_id.Name = "v_id";
            this.v_id.ReadOnly = true;
            this.v_id.Visible = false;
            this.v_id.Width = 57;
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSearch,
            this.tsmiClear,
            this.toolStripSeparator1,
            this.tsmiAdd,
            this.tsmiEdit,
            this.tsmiCopy,
            this.tsmiDelete,
            this.toolStripSeparator2,
            this.tsmiStart,
            this.toolStripSeparator3,
            this.tsmiOperation,
            this.tsmiView,
            this.tsmiPrint});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(153, 264);
            // 
            // tsmiSearch
            // 
            this.tsmiSearch.Name = "tsmiSearch";
            this.tsmiSearch.Size = new System.Drawing.Size(152, 22);
            this.tsmiSearch.Text = "查询";
            this.tsmiSearch.Click += new System.EventHandler(this.tsmiSearch_Click);
            // 
            // tsmiClear
            // 
            this.tsmiClear.Name = "tsmiClear";
            this.tsmiClear.Size = new System.Drawing.Size(152, 22);
            this.tsmiClear.Text = "清除";
            this.tsmiClear.Click += new System.EventHandler(this.tsmiClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(152, 22);
            this.tsmiAdd.Text = "新建";
            this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(152, 22);
            this.tsmiEdit.Text = "编辑";
            this.tsmiEdit.Click += new System.EventHandler(this.tsmiEdit_Click);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(152, 22);
            this.tsmiCopy.Text = "复制";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(152, 22);
            this.tsmiDelete.Text = "删除";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiStart
            // 
            this.tsmiStart.Name = "tsmiStart";
            this.tsmiStart.Size = new System.Drawing.Size(152, 22);
            this.tsmiStart.Text = "启用";
            this.tsmiStart.Click += new System.EventHandler(this.tsmiStart_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiOperation
            // 
            this.tsmiOperation.Name = "tsmiOperation";
            this.tsmiOperation.Size = new System.Drawing.Size(152, 22);
            this.tsmiOperation.Text = "设置";
            // 
            // tsmiView
            // 
            this.tsmiView.Name = "tsmiView";
            this.tsmiView.Size = new System.Drawing.Size(152, 22);
            this.tsmiView.Text = "预览";
            // 
            // tsmiPrint
            // 
            this.tsmiPrint.Name = "tsmiPrint";
            this.tsmiPrint.Size = new System.Drawing.Size(152, 22);
            this.tsmiPrint.Text = "打印";
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
            this.panelEx2.TabIndex = 20;
            // 
            // page
            // 
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.page.Dock = System.Windows.Forms.DockStyle.Right;
            this.page.Location = new System.Drawing.Point(363, 0);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(567, 28);
            this.page.TabIndex = 5;
            this.page.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            // 
            // UCVehicleManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.tabControlEx1);
            this.Controls.Add(this.palTop);
            this.Name = "UCVehicleManage";
            this.Load += new System.EventHandler(this.UCVehicleManage_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.palTop, 0);
            this.Controls.SetChildIndex(this.tabControlEx1, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.palTop.ResumeLayout(false);
            this.palTop.PerformLayout();
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.cmsMenu.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx palTop;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobStatus;
        private System.Windows.Forms.Label labOrderStatus;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCarNO;
        private System.Windows.Forms.Label labCarNO;
        private System.Windows.Forms.ComboBox cobCarBrand;
        private System.Windows.Forms.Label labCarBrand;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpCETime;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpCSTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labCTime;
        private ServiceStationClient.ComponentUI.TextBoxEx txtVIN;
        private System.Windows.Forms.Label labVIN;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCarType;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCustomNO;
        private System.Windows.Forms.Label labCustomNO;
        private ServiceStationClient.ComponentUI.TextBoxEx txtYardCode;
        private System.Windows.Forms.Label labYardCode;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobDataSources;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvData;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn license_plate;
        private System.Windows.Forms.DataGridViewTextBoxColumn vin;
        private System.Windows.Forms.DataGridViewTextBoxColumn v_brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn v_model;
        private System.Windows.Forms.DataGridViewTextBoxColumn carbuy_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_factory_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn data_source;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn v_id;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiSearch;
        private System.Windows.Forms.ToolStripMenuItem tsmiClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiStart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiOperation;
        private System.Windows.Forms.ToolStripMenuItem tsmiView;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrint;
    }
}
