namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsTagPrint
{
    partial class UCYTOldPartsImport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCYTOldPartsImport));
            this.pnlSearch = new ServiceStationClient.ComponentUI.PanelEx();
            this.dtpETime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpSTime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labReserveTime = new System.Windows.Forms.Label();
            this.txtOrderNo = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labOrderNo = new System.Windows.Forms.Label();
            this.btnSubmit = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvRData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.receipts_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.info_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oldpart_receipts_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tchange_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tsend_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Treceive_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receipt_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time_start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.return_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time_end = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControlEx2 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvRDetailData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.service_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.change_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.send_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receive_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.need_recycle_mark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.all_recycle_mark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.original = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.process_mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receive_explain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.palBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnImport = new ServiceStationClient.ComponentUI.ButtonEx();
            this.pnlContainer.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).BeginInit();
            this.tabControlEx2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRDetailData)).BeginInit();
            this.palBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlContainer.Controls.Add(this.palBottom);
            this.pnlContainer.Controls.Add(this.tabControlEx2);
            this.pnlContainer.Controls.Add(this.tabControlEx1);
            this.pnlContainer.Controls.Add(this.pnlSearch);
            this.pnlContainer.Size = new System.Drawing.Size(682, 477);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlSearch.BorderWidth = 1;
            this.pnlSearch.Controls.Add(this.dtpETime);
            this.pnlSearch.Controls.Add(this.dtpSTime);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.labReserveTime);
            this.pnlSearch.Controls.Add(this.txtOrderNo);
            this.pnlSearch.Controls.Add(this.labOrderNo);
            this.pnlSearch.Controls.Add(this.btnSubmit);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Curvature = 0;
            this.pnlSearch.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlSearch.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlSearch.Location = new System.Drawing.Point(1, 3);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(681, 78);
            this.pnlSearch.TabIndex = 7;
            // 
            // dtpETime
            // 
            this.dtpETime.Location = new System.Drawing.Point(223, 49);
            this.dtpETime.Name = "dtpETime";
            this.dtpETime.ShowFormat = "yyyy-MM-dd";
            this.dtpETime.Size = new System.Drawing.Size(121, 21);
            this.dtpETime.TabIndex = 54;
            this.dtpETime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // dtpSTime
            // 
            this.dtpSTime.Location = new System.Drawing.Point(77, 49);
            this.dtpSTime.Name = "dtpSTime";
            this.dtpSTime.ShowFormat = "yyyy-MM-dd";
            this.dtpSTime.Size = new System.Drawing.Size(121, 21);
            this.dtpSTime.TabIndex = 53;
            this.dtpSTime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 52;
            this.label1.Text = "从";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(202, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 51;
            this.label3.Text = "到";
            // 
            // labReserveTime
            // 
            this.labReserveTime.AutoSize = true;
            this.labReserveTime.Location = new System.Drawing.Point(3, 52);
            this.labReserveTime.Name = "labReserveTime";
            this.labReserveTime.Size = new System.Drawing.Size(65, 12);
            this.labReserveTime.TabIndex = 50;
            this.labReserveTime.Text = "单据时间：";
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtOrderNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtOrderNo.BackColor = System.Drawing.Color.Transparent;
            this.txtOrderNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtOrderNo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtOrderNo.ForeImage = null;
            this.txtOrderNo.Location = new System.Drawing.Point(78, 15);
            this.txtOrderNo.MaxLengh = 32767;
            this.txtOrderNo.Multiline = false;
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Radius = 3;
            this.txtOrderNo.ReadOnly = false;
            this.txtOrderNo.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtOrderNo.ShowError = false;
            this.txtOrderNo.Size = new System.Drawing.Size(120, 23);
            this.txtOrderNo.TabIndex = 21;
            this.txtOrderNo.UseSystemPasswordChar = false;
            this.txtOrderNo.Value = "";
            this.txtOrderNo.VerifyCondition = null;
            this.txtOrderNo.VerifyType = null;
            this.txtOrderNo.WaterMark = null;
            this.txtOrderNo.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labOrderNo
            // 
            this.labOrderNo.AutoSize = true;
            this.labOrderNo.Location = new System.Drawing.Point(17, 19);
            this.labOrderNo.Name = "labOrderNo";
            this.labOrderNo.Size = new System.Drawing.Size(65, 12);
            this.labOrderNo.TabIndex = 20;
            this.labOrderNo.Text = "返厂单号：";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.BackgroundImage")));
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Caption = "查询";
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSubmit.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.DownImage")));
            this.btnSubmit.Location = new System.Drawing.Point(456, 43);
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
            this.btnClear.Location = new System.Drawing.Point(456, 10);
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
            this.tabControlEx1.Location = new System.Drawing.Point(1, 73);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(680, 187);
            this.tabControlEx1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvRData);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(672, 157);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "宇通旧件返厂单信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvRData
            // 
            this.dgvRData.AllowUserToAddRows = false;
            this.dgvRData.AllowUserToDeleteRows = false;
            this.dgvRData.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvRData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvRData.BackgroundColor = System.Drawing.Color.White;
            this.dgvRData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvRData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.receipts_no,
            this.info_status,
            this.oldpart_receipts_status,
            this.Tchange_num,
            this.Tsend_num,
            this.Treceive_num,
            this.receipt_time,
            this.create_time_start,
            this.return_id,
            this.create_time_end});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRData.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvRData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRData.EnableHeadersVisualStyles = false;
            this.dgvRData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvRData.Location = new System.Drawing.Point(3, 3);
            this.dgvRData.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvRData.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvRData.MergeColumnNames")));
            this.dgvRData.MultiSelect = false;
            this.dgvRData.Name = "dgvRData";
            this.dgvRData.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRData.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvRData.RowHeadersVisible = false;
            this.dgvRData.RowHeadersWidth = 30;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvRData.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvRData.RowTemplate.Height = 23;
            this.dgvRData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRData.ShowCheckBox = true;
            this.dgvRData.Size = new System.Drawing.Size(666, 151);
            this.dgvRData.TabIndex = 0;
            this.dgvRData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRData_CellClick);
            // 
            // receipts_no
            // 
            this.receipts_no.DataPropertyName = "receipts_no";
            this.receipts_no.HeaderText = "返厂单号";
            this.receipts_no.Name = "receipts_no";
            this.receipts_no.ReadOnly = true;
            this.receipts_no.Width = 110;
            // 
            // info_status
            // 
            this.info_status.DataPropertyName = "info_status";
            this.info_status.HeaderText = "单据状态";
            this.info_status.Name = "info_status";
            this.info_status.ReadOnly = true;
            // 
            // oldpart_receipts_status
            // 
            this.oldpart_receipts_status.DataPropertyName = "oldpart_receipts_status";
            this.oldpart_receipts_status.HeaderText = "宇通旧件回收状态";
            this.oldpart_receipts_status.Name = "oldpart_receipts_status";
            this.oldpart_receipts_status.ReadOnly = true;
            this.oldpart_receipts_status.Width = 130;
            // 
            // Tchange_num
            // 
            this.Tchange_num.DataPropertyName = "change_num";
            this.Tchange_num.HeaderText = "更换总数";
            this.Tchange_num.Name = "Tchange_num";
            this.Tchange_num.ReadOnly = true;
            // 
            // Tsend_num
            // 
            this.Tsend_num.DataPropertyName = "send_num";
            this.Tsend_num.HeaderText = "发运总数";
            this.Tsend_num.Name = "Tsend_num";
            this.Tsend_num.ReadOnly = true;
            // 
            // Treceive_num
            // 
            this.Treceive_num.DataPropertyName = "receive_num";
            this.Treceive_num.HeaderText = "收到总数";
            this.Treceive_num.Name = "Treceive_num";
            this.Treceive_num.ReadOnly = true;
            // 
            // receipt_time
            // 
            this.receipt_time.DataPropertyName = "receipt_time";
            this.receipt_time.HeaderText = "单据时间";
            this.receipt_time.Name = "receipt_time";
            this.receipt_time.ReadOnly = true;
            this.receipt_time.Width = 150;
            // 
            // create_time_start
            // 
            this.create_time_start.DataPropertyName = "create_time_start";
            this.create_time_start.HeaderText = "创建日期范围";
            this.create_time_start.Name = "create_time_start";
            this.create_time_start.ReadOnly = true;
            this.create_time_start.Width = 110;
            // 
            // return_id
            // 
            this.return_id.DataPropertyName = "return_id";
            this.return_id.HeaderText = "return_id";
            this.return_id.Name = "return_id";
            this.return_id.ReadOnly = true;
            this.return_id.Visible = false;
            // 
            // create_time_end
            // 
            this.create_time_end.DataPropertyName = "create_time_end";
            this.create_time_end.HeaderText = "create_time_end";
            this.create_time_end.Name = "create_time_end";
            this.create_time_end.ReadOnly = true;
            this.create_time_end.Visible = false;
            // 
            // tabControlEx2
            // 
            this.tabControlEx2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlEx2.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx2.Controls.Add(this.tabPage2);
            this.tabControlEx2.Location = new System.Drawing.Point(1, 266);
            this.tabControlEx2.Name = "tabControlEx2";
            this.tabControlEx2.SelectedIndex = 0;
            this.tabControlEx2.Size = new System.Drawing.Size(680, 162);
            this.tabControlEx2.TabIndex = 11;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvRDetailData);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(672, 132);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "宇通旧件返厂单详情信息";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvRDetailData
            // 
            this.dgvRDetailData.AllowUserToAddRows = false;
            this.dgvRDetailData.AllowUserToDeleteRows = false;
            this.dgvRDetailData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvRDetailData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRDetailData.BackgroundColor = System.Drawing.Color.White;
            this.dgvRDetailData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRDetailData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRDetailData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRDetailData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.service_no,
            this.parts_code,
            this.parts_name,
            this.change_num,
            this.send_num,
            this.receive_num,
            this.unit,
            this.need_recycle_mark,
            this.all_recycle_mark,
            this.original,
            this.process_mode,
            this.remarks,
            this.receive_explain,
            this.parts_id});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRDetailData.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRDetailData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRDetailData.EnableHeadersVisualStyles = false;
            this.dgvRDetailData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvRDetailData.Location = new System.Drawing.Point(3, 3);
            this.dgvRDetailData.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvRDetailData.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvRDetailData.MergeColumnNames")));
            this.dgvRDetailData.MultiSelect = false;
            this.dgvRDetailData.Name = "dgvRDetailData";
            this.dgvRDetailData.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRDetailData.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRDetailData.RowHeadersVisible = false;
            this.dgvRDetailData.RowHeadersWidth = 30;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvRDetailData.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvRDetailData.RowTemplate.Height = 23;
            this.dgvRDetailData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRDetailData.ShowCheckBox = true;
            this.dgvRDetailData.Size = new System.Drawing.Size(666, 126);
            this.dgvRDetailData.TabIndex = 0;
            this.dgvRDetailData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRDetailData_CellDoubleClick);
            this.dgvRDetailData.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRDetailData_CellMouseClick);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 18;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 40;
            // 
            // service_no
            // 
            this.service_no.DataPropertyName = "service_no";
            this.service_no.HeaderText = "服务单号";
            this.service_no.Name = "service_no";
            this.service_no.ReadOnly = true;
            this.service_no.Width = 110;
            // 
            // parts_code
            // 
            this.parts_code.DataPropertyName = "parts_code";
            this.parts_code.HeaderText = "配件编码";
            this.parts_code.Name = "parts_code";
            this.parts_code.ReadOnly = true;
            // 
            // parts_name
            // 
            this.parts_name.DataPropertyName = "parts_name";
            this.parts_name.HeaderText = "配件名称";
            this.parts_name.Name = "parts_name";
            this.parts_name.ReadOnly = true;
            // 
            // change_num
            // 
            this.change_num.DataPropertyName = "change_num";
            this.change_num.HeaderText = "更换数量";
            this.change_num.Name = "change_num";
            this.change_num.ReadOnly = true;
            // 
            // send_num
            // 
            this.send_num.DataPropertyName = "send_num";
            this.send_num.HeaderText = "发运数量";
            this.send_num.Name = "send_num";
            this.send_num.ReadOnly = true;
            // 
            // receive_num
            // 
            this.receive_num.DataPropertyName = "receive_num";
            this.receive_num.HeaderText = "收到数量";
            this.receive_num.Name = "receive_num";
            this.receive_num.ReadOnly = true;
            // 
            // unit
            // 
            this.unit.DataPropertyName = "unit";
            this.unit.HeaderText = "计量单位";
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            // 
            // need_recycle_mark
            // 
            this.need_recycle_mark.DataPropertyName = "need_recycle_mark";
            this.need_recycle_mark.HeaderText = "需回收标记";
            this.need_recycle_mark.Name = "need_recycle_mark";
            this.need_recycle_mark.ReadOnly = true;
            // 
            // all_recycle_mark
            // 
            this.all_recycle_mark.DataPropertyName = "all_recycle_mark";
            this.all_recycle_mark.HeaderText = "完全回收标记";
            this.all_recycle_mark.Name = "all_recycle_mark";
            this.all_recycle_mark.ReadOnly = true;
            this.all_recycle_mark.Width = 150;
            // 
            // original
            // 
            this.original.DataPropertyName = "original";
            this.original.HeaderText = "原厂件";
            this.original.Name = "original";
            this.original.ReadOnly = true;
            // 
            // process_mode
            // 
            this.process_mode.DataPropertyName = "process_mode";
            this.process_mode.HeaderText = "处理方式";
            this.process_mode.Name = "process_mode";
            this.process_mode.ReadOnly = true;
            // 
            // remarks
            // 
            this.remarks.DataPropertyName = "remarks";
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            // 
            // receive_explain
            // 
            this.receive_explain.DataPropertyName = "receive_explain";
            this.receive_explain.HeaderText = "接受说明";
            this.receive_explain.Name = "receive_explain";
            this.receive_explain.ReadOnly = true;
            // 
            // parts_id
            // 
            this.parts_id.DataPropertyName = "parts_id";
            this.parts_id.HeaderText = "parts_id";
            this.parts_id.Name = "parts_id";
            this.parts_id.ReadOnly = true;
            this.parts_id.Visible = false;
            // 
            // palBottom
            // 
            this.palBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.palBottom.Controls.Add(this.btnCancel);
            this.palBottom.Controls.Add(this.btnImport);
            this.palBottom.Location = new System.Drawing.Point(2, 434);
            this.palBottom.Name = "palBottom";
            this.palBottom.Size = new System.Drawing.Size(679, 39);
            this.palBottom.TabIndex = 12;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Caption = "取消";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.DownImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.DownImage")));
            this.btnCancel.Location = new System.Drawing.Point(611, 7);
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
            this.btnImport.Location = new System.Drawing.Point(519, 7);
            this.btnImport.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnImport.MoveImage")));
            this.btnImport.Name = "btnImport";
            this.btnImport.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnImport.NormalImage")));
            this.btnImport.Size = new System.Drawing.Size(60, 26);
            this.btnImport.TabIndex = 18;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // UCYTOldPartsImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 509);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "UCYTOldPartsImport";
            this.Text = "宇通旧件返厂单导入";
            this.Load += new System.EventHandler(this.UCYTOldPartsImport_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).EndInit();
            this.tabControlEx2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRDetailData)).EndInit();
            this.palBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx pnlSearch;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpETime;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpSTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labReserveTime;
        private ServiceStationClient.ComponentUI.TextBoxEx txtOrderNo;
        private System.Windows.Forms.Label labOrderNo;
        private ServiceStationClient.ComponentUI.ButtonEx btnSubmit;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx2;
        private System.Windows.Forms.TabPage tabPage2;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRDetailData;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRData;
        private System.Windows.Forms.Panel palBottom;
        private ServiceStationClient.ComponentUI.ButtonEx btnCancel;
        private ServiceStationClient.ComponentUI.ButtonEx btnImport;
        private System.Windows.Forms.DataGridViewTextBoxColumn receipts_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn info_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn oldpart_receipts_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tchange_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tsend_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn Treceive_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn receipt_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time_start;
        private System.Windows.Forms.DataGridViewTextBoxColumn return_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time_end;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn service_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn change_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn send_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn receive_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn need_recycle_mark;
        private System.Windows.Forms.DataGridViewTextBoxColumn all_recycle_mark;
        private System.Windows.Forms.DataGridViewTextBoxColumn original;
        private System.Windows.Forms.DataGridViewTextBoxColumn process_mode;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn receive_explain;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_id;
    }
}