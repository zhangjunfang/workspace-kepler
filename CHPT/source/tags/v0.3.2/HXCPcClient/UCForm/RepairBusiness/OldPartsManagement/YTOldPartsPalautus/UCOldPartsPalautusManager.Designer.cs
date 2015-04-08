namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsPalautus
{
    partial class UCOldPartsPalautusManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCOldPartsPalautusManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.palInfo = new System.Windows.Forms.Panel();
            this.cobYTStatus = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.labYTStatus = new System.Windows.Forms.Label();
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.txtReceiptOrderNO = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labReceiptOrderNO = new System.Windows.Forms.Label();
            this.cobOrderStatus = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.labOrderStatus = new System.Windows.Forms.Label();
            this.dtpETime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpSTime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labReceiptTime = new System.Windows.Forms.Label();
            this.txtOrder = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labOrder = new System.Windows.Forms.Label();
            this.txtRemark = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labRemark = new System.Windows.Forms.Label();
            this.palBottom = new ServiceStationClient.ComponentUI.PanelEx();
            this.dgvRData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.receipts_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oldpart_receipts_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.info_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.info_status_yt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.change_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.send_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receive_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receipt_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time_start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.return_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time_end = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.palInfo.SuspendLayout();
            this.palBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // palInfo
            // 
            this.palInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.palInfo.Controls.Add(this.cobYTStatus);
            this.palInfo.Controls.Add(this.labYTStatus);
            this.palInfo.Controls.Add(this.btnQuery);
            this.palInfo.Controls.Add(this.btnClear);
            this.palInfo.Controls.Add(this.txtReceiptOrderNO);
            this.palInfo.Controls.Add(this.labReceiptOrderNO);
            this.palInfo.Controls.Add(this.cobOrderStatus);
            this.palInfo.Controls.Add(this.labOrderStatus);
            this.palInfo.Controls.Add(this.dtpETime);
            this.palInfo.Controls.Add(this.dtpSTime);
            this.palInfo.Controls.Add(this.label1);
            this.palInfo.Controls.Add(this.label3);
            this.palInfo.Controls.Add(this.labReceiptTime);
            this.palInfo.Controls.Add(this.txtOrder);
            this.palInfo.Controls.Add(this.labOrder);
            this.palInfo.Controls.Add(this.txtRemark);
            this.palInfo.Controls.Add(this.labRemark);
            this.palInfo.Location = new System.Drawing.Point(1, 33);
            this.palInfo.Name = "palInfo";
            this.palInfo.Size = new System.Drawing.Size(1026, 71);
            this.palInfo.TabIndex = 5;
            // 
            // cobYTStatus
            // 
            this.cobYTStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobYTStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobYTStatus.FormattingEnabled = true;
            this.cobYTStatus.Location = new System.Drawing.Point(804, 6);
            this.cobYTStatus.Name = "cobYTStatus";
            this.cobYTStatus.Size = new System.Drawing.Size(121, 22);
            this.cobYTStatus.TabIndex = 80;
            // 
            // labYTStatus
            // 
            this.labYTStatus.AutoSize = true;
            this.labYTStatus.Location = new System.Drawing.Point(673, 11);
            this.labYTStatus.Name = "labYTStatus";
            this.labYTStatus.Size = new System.Drawing.Size(125, 12);
            this.labYTStatus.TabIndex = 79;
            this.labYTStatus.Text = "宇通旧件回收单状态：";
            // 
            // btnQuery
            // 
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(929, 38);
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
            this.btnClear.Location = new System.Drawing.Point(929, 6);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 77;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtReceiptOrderNO
            // 
            this.txtReceiptOrderNO.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtReceiptOrderNO.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtReceiptOrderNO.BackColor = System.Drawing.Color.Transparent;
            this.txtReceiptOrderNO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtReceiptOrderNO.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtReceiptOrderNO.ForeImage = null;
            this.txtReceiptOrderNO.Location = new System.Drawing.Point(542, 6);
            this.txtReceiptOrderNO.MaxLengh = 32767;
            this.txtReceiptOrderNO.Multiline = false;
            this.txtReceiptOrderNO.Name = "txtReceiptOrderNO";
            this.txtReceiptOrderNO.Radius = 3;
            this.txtReceiptOrderNO.ReadOnly = false;
            this.txtReceiptOrderNO.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtReceiptOrderNO.ShowError = false;
            this.txtReceiptOrderNO.Size = new System.Drawing.Size(120, 23);
            this.txtReceiptOrderNO.TabIndex = 37;
            this.txtReceiptOrderNO.UseSystemPasswordChar = false;
            this.txtReceiptOrderNO.Value = "";
            this.txtReceiptOrderNO.VerifyCondition = null;
            this.txtReceiptOrderNO.VerifyType = null;
            this.txtReceiptOrderNO.WaterMark = null;
            this.txtReceiptOrderNO.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labReceiptOrderNO
            // 
            this.labReceiptOrderNO.AutoSize = true;
            this.labReceiptOrderNO.Location = new System.Drawing.Point(447, 11);
            this.labReceiptOrderNO.Name = "labReceiptOrderNO";
            this.labReceiptOrderNO.Size = new System.Drawing.Size(89, 12);
            this.labReceiptOrderNO.TabIndex = 33;
            this.labReceiptOrderNO.Text = "旧件回收单号：";
            // 
            // cobOrderStatus
            // 
            this.cobOrderStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobOrderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobOrderStatus.FormattingEnabled = true;
            this.cobOrderStatus.Location = new System.Drawing.Point(302, 8);
            this.cobOrderStatus.Name = "cobOrderStatus";
            this.cobOrderStatus.Size = new System.Drawing.Size(121, 22);
            this.cobOrderStatus.TabIndex = 76;
            // 
            // labOrderStatus
            // 
            this.labOrderStatus.AutoSize = true;
            this.labOrderStatus.Location = new System.Drawing.Point(231, 11);
            this.labOrderStatus.Name = "labOrderStatus";
            this.labOrderStatus.Size = new System.Drawing.Size(65, 12);
            this.labOrderStatus.TabIndex = 75;
            this.labOrderStatus.Text = "单据状态：";
            // 
            // dtpETime
            // 
            this.dtpETime.Location = new System.Drawing.Point(279, 38);
            this.dtpETime.Name = "dtpETime";
            this.dtpETime.ShowFormat = "yyyy-MM-dd HH:mm";
            this.dtpETime.Size = new System.Drawing.Size(146, 21);
            this.dtpETime.TabIndex = 74;
            this.dtpETime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // dtpSTime
            // 
            this.dtpSTime.Location = new System.Drawing.Point(93, 38);
            this.dtpSTime.Name = "dtpSTime";
            this.dtpSTime.ShowFormat = "yyyy-MM-dd HH:mm";
            this.dtpSTime.Size = new System.Drawing.Size(146, 21);
            this.dtpSTime.TabIndex = 73;
            this.dtpSTime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 72;
            this.label1.Text = "从";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(250, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 71;
            this.label3.Text = "到";
            // 
            // labReceiptTime
            // 
            this.labReceiptTime.AutoSize = true;
            this.labReceiptTime.Location = new System.Drawing.Point(21, 42);
            this.labReceiptTime.Name = "labReceiptTime";
            this.labReceiptTime.Size = new System.Drawing.Size(65, 12);
            this.labReceiptTime.TabIndex = 70;
            this.labReceiptTime.Text = "单据日期：";
            // 
            // txtOrder
            // 
            this.txtOrder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtOrder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtOrder.BackColor = System.Drawing.Color.Transparent;
            this.txtOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtOrder.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtOrder.ForeImage = null;
            this.txtOrder.Location = new System.Drawing.Point(93, 6);
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
            this.txtOrder.WaterMark = null;
            this.txtOrder.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labOrder
            // 
            this.labOrder.AutoSize = true;
            this.labOrder.Location = new System.Drawing.Point(22, 11);
            this.labOrder.Name = "labOrder";
            this.labOrder.Size = new System.Drawing.Size(65, 12);
            this.labOrder.TabIndex = 68;
            this.labOrder.Text = "返厂单号：";
            // 
            // txtRemark
            // 
            this.txtRemark.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtRemark.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtRemark.BackColor = System.Drawing.Color.Transparent;
            this.txtRemark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtRemark.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtRemark.ForeImage = null;
            this.txtRemark.Location = new System.Drawing.Point(542, 38);
            this.txtRemark.MaxLengh = 32767;
            this.txtRemark.Multiline = false;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Radius = 3;
            this.txtRemark.ReadOnly = false;
            this.txtRemark.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtRemark.ShowError = false;
            this.txtRemark.Size = new System.Drawing.Size(383, 23);
            this.txtRemark.TabIndex = 39;
            this.txtRemark.UseSystemPasswordChar = false;
            this.txtRemark.Value = "";
            this.txtRemark.VerifyCondition = null;
            this.txtRemark.VerifyType = null;
            this.txtRemark.WaterMark = null;
            this.txtRemark.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labRemark
            // 
            this.labRemark.AutoSize = true;
            this.labRemark.Location = new System.Drawing.Point(490, 44);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(41, 12);
            this.labRemark.TabIndex = 35;
            this.labRemark.Text = "备注：";
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
            this.palBottom.Location = new System.Drawing.Point(0, 110);
            this.palBottom.Name = "palBottom";
            this.palBottom.Size = new System.Drawing.Size(1027, 399);
            this.palBottom.TabIndex = 8;
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
            this.receipts_no,
            this.oldpart_receipts_no,
            this.info_status,
            this.info_status_yt,
            this.change_num,
            this.send_num,
            this.receive_num,
            this.receipt_time,
            this.create_time_start,
            this.remarks,
            this.return_id,
            this.create_time_end});
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
            this.dgvRData.Size = new System.Drawing.Size(1021, 397);
            this.dgvRData.TabIndex = 13;
            this.dgvRData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRData_CellDoubleClick);
            this.dgvRData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRData_CellFormatting);
            this.dgvRData.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRData_CellMouseClick);
            this.dgvRData.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRData_CellMouseUp);
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
            this.panelEx2.TabIndex = 21;
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
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 18;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 40;
            // 
            // receipts_no
            // 
            this.receipts_no.DataPropertyName = "receipts_no";
            this.receipts_no.HeaderText = "返厂单号";
            this.receipts_no.MinimumWidth = 120;
            this.receipts_no.Name = "receipts_no";
            this.receipts_no.ReadOnly = true;
            this.receipts_no.Width = 120;
            // 
            // oldpart_receipts_no
            // 
            this.oldpart_receipts_no.DataPropertyName = "oldpart_receipts_no";
            this.oldpart_receipts_no.HeaderText = "旧件回收单号";
            this.oldpart_receipts_no.Name = "oldpart_receipts_no";
            this.oldpart_receipts_no.ReadOnly = true;
            this.oldpart_receipts_no.Width = 110;
            // 
            // info_status
            // 
            this.info_status.DataPropertyName = "info_status";
            this.info_status.HeaderText = "单据状态";
            this.info_status.Name = "info_status";
            this.info_status.ReadOnly = true;
            this.info_status.Width = 90;
            // 
            // info_status_yt
            // 
            this.info_status_yt.DataPropertyName = "info_status_yt";
            this.info_status_yt.HeaderText = "宇通旧件回收单状态";
            this.info_status_yt.Name = "info_status_yt";
            this.info_status_yt.ReadOnly = true;
            this.info_status_yt.Width = 150;
            // 
            // change_num
            // 
            this.change_num.DataPropertyName = "change_num";
            this.change_num.HeaderText = "更换总数";
            this.change_num.Name = "change_num";
            this.change_num.ReadOnly = true;
            // 
            // send_num
            // 
            this.send_num.DataPropertyName = "send_num";
            this.send_num.HeaderText = "发运总数";
            this.send_num.Name = "send_num";
            this.send_num.ReadOnly = true;
            // 
            // receive_num
            // 
            this.receive_num.DataPropertyName = "receive_num";
            this.receive_num.HeaderText = "收到总数";
            this.receive_num.Name = "receive_num";
            this.receive_num.ReadOnly = true;
            // 
            // receipt_time
            // 
            this.receipt_time.DataPropertyName = "receipt_time";
            this.receipt_time.HeaderText = "单据时间";
            this.receipt_time.Name = "receipt_time";
            this.receipt_time.ReadOnly = true;
            // 
            // create_time_start
            // 
            this.create_time_start.DataPropertyName = "create_time_start";
            this.create_time_start.HeaderText = "创建日期范围";
            this.create_time_start.Name = "create_time_start";
            this.create_time_start.ReadOnly = true;
            this.create_time_start.Width = 105;
            // 
            // remarks
            // 
            this.remarks.DataPropertyName = "remarks";
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            // 
            // return_id
            // 
            this.return_id.DataPropertyName = "return_id";
            this.return_id.HeaderText = "return_id";
            this.return_id.Name = "return_id";
            this.return_id.ReadOnly = true;
            this.return_id.Visible = false;
            this.return_id.Width = 10;
            // 
            // create_time_end
            // 
            this.create_time_end.DataPropertyName = "create_time_end";
            this.create_time_end.HeaderText = "create_time_end";
            this.create_time_end.Name = "create_time_end";
            this.create_time_end.ReadOnly = true;
            this.create_time_end.Visible = false;
            // 
            // UCOldPartsPalautusManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.palBottom);
            this.Controls.Add(this.palInfo);
            this.Name = "UCOldPartsPalautusManager";
            this.Load += new System.EventHandler(this.UCOldPartsPalautusManager_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.palInfo, 0);
            this.Controls.SetChildIndex(this.palBottom, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.palInfo.ResumeLayout(false);
            this.palInfo.PerformLayout();
            this.palBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel palInfo;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobYTStatus;
        private System.Windows.Forms.Label labYTStatus;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.TextBoxEx txtReceiptOrderNO;
        private System.Windows.Forms.Label labReceiptOrderNO;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobOrderStatus;
        private System.Windows.Forms.Label labOrderStatus;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpETime;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpSTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labReceiptTime;
        private ServiceStationClient.ComponentUI.TextBoxEx txtOrder;
        private System.Windows.Forms.Label labOrder;
        private ServiceStationClient.ComponentUI.TextBoxEx txtRemark;
        private System.Windows.Forms.Label labRemark;
        private ServiceStationClient.ComponentUI.PanelEx palBottom;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRData;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn receipts_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn oldpart_receipts_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn info_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn info_status_yt;
        private System.Windows.Forms.DataGridViewTextBoxColumn change_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn send_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn receive_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn receipt_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time_start;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn return_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time_end;

    }
}