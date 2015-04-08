namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.OldPartsSend
{
    partial class UCPartsReceiptImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPartsReceiptImport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlSearch = new ServiceStationClient.ComponentUI.PanelEx();
            this.txtOrder = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labOrder = new System.Windows.Forms.Label();
            this.dtpETime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpSTime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labReserveTime = new System.Windows.Forms.Label();
            this.btnSubmit = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.palBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnImport = new ServiceStationClient.ComponentUI.ButtonEx();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.tabControlEx2 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvRDetailData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.parts_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drawn_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sum_money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whether_imported = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maintain_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvRData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.receipts_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkman = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receipt_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oldpart_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlContainer.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.palBottom.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.tabControlEx2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRDetailData)).BeginInit();
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
            this.pnlContainer.Size = new System.Drawing.Size(683, 479);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlSearch.BorderWidth = 1;
            this.pnlSearch.Controls.Add(this.txtOrder);
            this.pnlSearch.Controls.Add(this.labOrder);
            this.pnlSearch.Controls.Add(this.dtpETime);
            this.pnlSearch.Controls.Add(this.dtpSTime);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.labReserveTime);
            this.pnlSearch.Controls.Add(this.btnSubmit);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Curvature = 0;
            this.pnlSearch.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlSearch.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlSearch.Location = new System.Drawing.Point(0, 2);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(677, 78);
            this.pnlSearch.TabIndex = 8;
            // 
            // txtOrder
            // 
            this.txtOrder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtOrder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtOrder.BackColor = System.Drawing.Color.Transparent;
            this.txtOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtOrder.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtOrder.ForeImage = null;
            this.txtOrder.Location = new System.Drawing.Point(77, 10);
            this.txtOrder.MaxLengh = 32767;
            this.txtOrder.Multiline = false;
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Radius = 3;
            this.txtOrder.ReadOnly = false;
            this.txtOrder.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtOrder.ShowError = false;
            this.txtOrder.Size = new System.Drawing.Size(120, 23);
            this.txtOrder.TabIndex = 71;
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
            this.labOrder.Location = new System.Drawing.Point(6, 15);
            this.labOrder.Name = "labOrder";
            this.labOrder.Size = new System.Drawing.Size(65, 12);
            this.labOrder.TabIndex = 70;
            this.labOrder.Text = "收货单号：";
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
            this.labReserveTime.Text = "收货日期：";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Caption = "查询";
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSubmit.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSubmit.Location = new System.Drawing.Point(456, 43);
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
            this.btnClear.Location = new System.Drawing.Point(456, 10);
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
            this.palBottom.Location = new System.Drawing.Point(2, 435);
            this.palBottom.Name = "palBottom";
            this.palBottom.Size = new System.Drawing.Size(679, 39);
            this.palBottom.TabIndex = 13;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Caption = "取消";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnCancel.Location = new System.Drawing.Point(586, 8);
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
            this.btnImport.Location = new System.Drawing.Point(494, 8);
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
            this.panelEx1.Controls.Add(this.tabControlEx2);
            this.panelEx1.Controls.Add(this.tabControlEx1);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(3, 86);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(674, 345);
            this.panelEx1.TabIndex = 14;
            // 
            // tabControlEx2
            // 
            this.tabControlEx2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlEx2.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.tabControlEx2.Controls.Add(this.tabPage2);
            this.tabControlEx2.Location = new System.Drawing.Point(3, 193);
            this.tabControlEx2.Name = "tabControlEx2";
            this.tabControlEx2.SelectedIndex = 0;
            this.tabControlEx2.Size = new System.Drawing.Size(668, 145);
            this.tabControlEx2.TabIndex = 14;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvRDetailData);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(660, 115);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "旧件收货单详情信息";
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
            this.parts_code,
            this.parts_name,
            this.drawn_no,
            this.unit,
            this.quantity,
            this.unit_price,
            this.sum_money,
            this.whether_imported,
            this.vehicle_model,
            this.remarks,
            this.parts_id,
            this.maintain_id});
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
            this.dgvRDetailData.Size = new System.Drawing.Size(654, 109);
            this.dgvRDetailData.TabIndex = 0;
            this.dgvRDetailData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRDetailData_CellDoubleClick);
            this.dgvRDetailData.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRDetailData_CellMouseClick);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 30;
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
            // drawn_no
            // 
            this.drawn_no.DataPropertyName = "drawn_no";
            this.drawn_no.HeaderText = "图号";
            this.drawn_no.Name = "drawn_no";
            this.drawn_no.ReadOnly = true;
            // 
            // unit
            // 
            this.unit.DataPropertyName = "unit";
            this.unit.HeaderText = "单位";
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            // 
            // quantity
            // 
            this.quantity.DataPropertyName = "quantity";
            this.quantity.HeaderText = "数量";
            this.quantity.Name = "quantity";
            this.quantity.ReadOnly = true;
            // 
            // unit_price
            // 
            this.unit_price.DataPropertyName = "unit_price";
            this.unit_price.HeaderText = "单价";
            this.unit_price.Name = "unit_price";
            this.unit_price.ReadOnly = true;
            // 
            // sum_money
            // 
            this.sum_money.DataPropertyName = "sum_money";
            this.sum_money.HeaderText = "金额";
            this.sum_money.Name = "sum_money";
            this.sum_money.ReadOnly = true;
            // 
            // whether_imported
            // 
            this.whether_imported.DataPropertyName = "whether_imported";
            this.whether_imported.HeaderText = "是否进口";
            this.whether_imported.Name = "whether_imported";
            this.whether_imported.ReadOnly = true;
            this.whether_imported.Width = 150;
            // 
            // vehicle_model
            // 
            this.vehicle_model.DataPropertyName = "vehicle_model";
            this.vehicle_model.HeaderText = "适用车型";
            this.vehicle_model.Name = "vehicle_model";
            this.vehicle_model.ReadOnly = true;
            // 
            // remarks
            // 
            this.remarks.DataPropertyName = "remarks";
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            // 
            // parts_id
            // 
            this.parts_id.DataPropertyName = "parts_id";
            this.parts_id.HeaderText = "parts_id";
            this.parts_id.Name = "parts_id";
            this.parts_id.ReadOnly = true;
            this.parts_id.Visible = false;
            // 
            // maintain_id
            // 
            this.maintain_id.DataPropertyName = "maintain_id";
            this.maintain_id.HeaderText = "maintain_id";
            this.maintain_id.Name = "maintain_id";
            this.maintain_id.ReadOnly = true;
            this.maintain_id.Visible = false;
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Location = new System.Drawing.Point(3, 6);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(668, 170);
            this.tabControlEx1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvRData);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(660, 140);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "旧件收货单信息";
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
            this.customer_code,
            this.customer_name,
            this.linkman,
            this.receipt_time,
            this.oldpart_id});
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
            this.dgvRData.Size = new System.Drawing.Size(654, 134);
            this.dgvRData.TabIndex = 0;
            this.dgvRData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRData_CellClick);
            // 
            // receipts_no
            // 
            this.receipts_no.DataPropertyName = "receipts_no";
            this.receipts_no.HeaderText = "收货单号";
            this.receipts_no.Name = "receipts_no";
            this.receipts_no.ReadOnly = true;
            this.receipts_no.Width = 120;
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
            this.customer_name.Width = 130;
            // 
            // linkman
            // 
            this.linkman.DataPropertyName = "linkman";
            this.linkman.HeaderText = "联系人";
            this.linkman.Name = "linkman";
            this.linkman.ReadOnly = true;
            // 
            // receipt_time
            // 
            this.receipt_time.DataPropertyName = "receipt_time";
            this.receipt_time.HeaderText = "收货日期";
            this.receipt_time.Name = "receipt_time";
            this.receipt_time.ReadOnly = true;
            this.receipt_time.Width = 120;
            // 
            // oldpart_id
            // 
            this.oldpart_id.DataPropertyName = "oldpart_id";
            this.oldpart_id.HeaderText = "oldpart_id";
            this.oldpart_id.Name = "oldpart_id";
            this.oldpart_id.ReadOnly = true;
            this.oldpart_id.Visible = false;
            // 
            // UCPartsReceiptImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 509);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "UCPartsReceiptImport";
            this.Text = "旧件收货单导入";
            this.Load += new System.EventHandler(this.UCPartsReceiptImport_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.palBottom.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.tabControlEx2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRDetailData)).EndInit();
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx pnlSearch;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpETime;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpSTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labReserveTime;
        private ServiceStationClient.ComponentUI.ButtonEx btnSubmit;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.TextBoxEx txtOrder;
        private System.Windows.Forms.Label labOrder;
        private System.Windows.Forms.Panel palBottom;
        private ServiceStationClient.ComponentUI.ButtonEx btnCancel;
        private ServiceStationClient.ComponentUI.ButtonEx btnImport;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx2;
        private System.Windows.Forms.TabPage tabPage2;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRDetailData;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn drawn_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn sum_money;
        private System.Windows.Forms.DataGridViewTextBoxColumn whether_imported;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_model;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn maintain_id;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRData;
        private System.Windows.Forms.DataGridViewTextBoxColumn receipts_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkman;
        private System.Windows.Forms.DataGridViewTextBoxColumn receipt_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn oldpart_id;
    }
}