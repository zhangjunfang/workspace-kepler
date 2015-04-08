namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.OldPartsInventoryQuery
{
    partial class UCOldPartsInventoryQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCOldPartsInventoryQuery));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.palData = new System.Windows.Forms.Panel();
            this.splitData = new System.Windows.Forms.SplitContainer();
            this.tvData = new ServiceStationClient.ComponentUI.TreeViewEx();
            this.dgvRData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.parts_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receipt_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drawn_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.norms = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.book_inventory_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inventory_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.average_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sum_money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bar_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.origin_place = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trademark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oldpart_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.labContact = new System.Windows.Forms.Label();
            this.labRemark = new System.Windows.Forms.Label();
            this.txtAddress = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtBrand = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labOrderStatus = new System.Windows.Forms.Label();
            this.cobCompany = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.cobWearhouse = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.txtDrawingNo = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBar = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labPartsCode = new System.Windows.Forms.Label();
            this.txtPartsCode = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.labPartsName = new System.Windows.Forms.Label();
            this.txtPartsName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCarType = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.palQTop = new System.Windows.Forms.Panel();
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.palData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitData)).BeginInit();
            this.splitData.Panel1.SuspendLayout();
            this.splitData.Panel2.SuspendLayout();
            this.splitData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.palQTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // palData
            // 
            this.palData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palData.Controls.Add(this.splitData);
            this.palData.Location = new System.Drawing.Point(0, 151);
            this.palData.Name = "palData";
            this.palData.Size = new System.Drawing.Size(1027, 359);
            this.palData.TabIndex = 21;
            // 
            // splitData
            // 
            this.splitData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitData.Location = new System.Drawing.Point(0, 0);
            this.splitData.Name = "splitData";
            // 
            // splitData.Panel1
            // 
            this.splitData.Panel1.Controls.Add(this.tvData);
            // 
            // splitData.Panel2
            // 
            this.splitData.Panel2.Controls.Add(this.dgvRData);
            this.splitData.Size = new System.Drawing.Size(1027, 359);
            this.splitData.SplitterDistance = 154;
            this.splitData.TabIndex = 0;
            // 
            // tvData
            // 
            this.tvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvData.IgnoreAutoCheck = false;
            this.tvData.Location = new System.Drawing.Point(0, 0);
            this.tvData.Name = "tvData";
            this.tvData.Size = new System.Drawing.Size(152, 357);
            this.tvData.TabIndex = 4;
            this.tvData.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvData_AfterSelect);
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
            this.parts_code,
            this.receipt_time,
            this.drawn_no,
            this.norms,
            this.book_inventory_num,
            this.inventory_num,
            this.unit,
            this.average_price,
            this.sum_money,
            this.bar_code,
            this.weight,
            this.origin_place,
            this.trademark,
            this.vehicle_model,
            this.remarks,
            this.oldpart_id});
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
            this.dgvRData.Size = new System.Drawing.Size(861, 344);
            this.dgvRData.TabIndex = 14;
            this.dgvRData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRData_CellFormatting);
            this.dgvRData.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRData_CellMouseClick);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 18;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 40;
            // 
            // parts_code
            // 
            this.parts_code.DataPropertyName = "parts_code";
            this.parts_code.HeaderText = "配件编码";
            this.parts_code.Name = "parts_code";
            this.parts_code.ReadOnly = true;
            this.parts_code.Width = 110;
            // 
            // receipt_time
            // 
            this.receipt_time.DataPropertyName = "parts_name";
            this.receipt_time.HeaderText = "配件名称";
            this.receipt_time.Name = "receipt_time";
            this.receipt_time.ReadOnly = true;
            // 
            // drawn_no
            // 
            this.drawn_no.DataPropertyName = "drawn_no";
            this.drawn_no.HeaderText = "图号";
            this.drawn_no.Name = "drawn_no";
            this.drawn_no.ReadOnly = true;
            this.drawn_no.Width = 90;
            // 
            // norms
            // 
            this.norms.DataPropertyName = "norms";
            this.norms.HeaderText = "规格";
            this.norms.Name = "norms";
            this.norms.ReadOnly = true;
            // 
            // book_inventory_num
            // 
            this.book_inventory_num.DataPropertyName = "book_inventory_num";
            this.book_inventory_num.HeaderText = "账面库存";
            this.book_inventory_num.Name = "book_inventory_num";
            this.book_inventory_num.ReadOnly = true;
            // 
            // inventory_num
            // 
            this.inventory_num.DataPropertyName = "inventory_num";
            this.inventory_num.HeaderText = "实际库存";
            this.inventory_num.Name = "inventory_num";
            this.inventory_num.ReadOnly = true;
            // 
            // unit
            // 
            this.unit.DataPropertyName = "unit";
            this.unit.HeaderText = "单位名称";
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            // 
            // average_price
            // 
            this.average_price.DataPropertyName = "average_price";
            this.average_price.HeaderText = "平均价格";
            this.average_price.Name = "average_price";
            this.average_price.ReadOnly = true;
            // 
            // sum_money
            // 
            this.sum_money.DataPropertyName = "sum_money";
            this.sum_money.HeaderText = "金额";
            this.sum_money.Name = "sum_money";
            this.sum_money.ReadOnly = true;
            this.sum_money.Width = 90;
            // 
            // bar_code
            // 
            this.bar_code.DataPropertyName = "bar_code";
            this.bar_code.HeaderText = "条码";
            this.bar_code.Name = "bar_code";
            this.bar_code.ReadOnly = true;
            this.bar_code.Width = 110;
            // 
            // weight
            // 
            this.weight.DataPropertyName = "weight";
            this.weight.HeaderText = "重量";
            this.weight.Name = "weight";
            this.weight.ReadOnly = true;
            // 
            // origin_place
            // 
            this.origin_place.DataPropertyName = "origin_place";
            this.origin_place.HeaderText = "产地";
            this.origin_place.Name = "origin_place";
            this.origin_place.ReadOnly = true;
            // 
            // trademark
            // 
            this.trademark.DataPropertyName = "trademark";
            this.trademark.HeaderText = "品牌";
            this.trademark.Name = "trademark";
            this.trademark.ReadOnly = true;
            // 
            // vehicle_model
            // 
            this.vehicle_model.DataPropertyName = "vehicle_model";
            this.vehicle_model.HeaderText = "车型";
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
            // oldpart_id
            // 
            this.oldpart_id.DataPropertyName = "inventory_id";
            this.oldpart_id.HeaderText = "inventory_id";
            this.oldpart_id.Name = "oldpart_id";
            this.oldpart_id.ReadOnly = true;
            this.oldpart_id.Visible = false;
            this.oldpart_id.Width = 10;
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(223)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.page);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(0, 516);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Padding = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.panelEx1.Size = new System.Drawing.Size(1030, 28);
            this.panelEx1.TabIndex = 22;
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
            // labContact
            // 
            this.labContact.AutoSize = true;
            this.labContact.Location = new System.Drawing.Point(449, 13);
            this.labContact.Name = "labContact";
            this.labContact.Size = new System.Drawing.Size(41, 12);
            this.labContact.TabIndex = 81;
            this.labContact.Text = "产地：";
            // 
            // labRemark
            // 
            this.labRemark.AutoSize = true;
            this.labRemark.Location = new System.Drawing.Point(642, 45);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(41, 12);
            this.labRemark.TabIndex = 82;
            this.labRemark.Text = "品牌：";
            // 
            // txtAddress
            // 
            this.txtAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtAddress.BackColor = System.Drawing.Color.Transparent;
            this.txtAddress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtAddress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtAddress.ForeImage = null;
            this.txtAddress.Location = new System.Drawing.Point(496, 6);
            this.txtAddress.MaxLengh = 32767;
            this.txtAddress.Multiline = false;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Radius = 3;
            this.txtAddress.ReadOnly = false;
            this.txtAddress.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtAddress.Size = new System.Drawing.Size(120, 23);
            this.txtAddress.TabIndex = 84;
            this.txtAddress.UseSystemPasswordChar = false;
            this.txtAddress.WaterMark = null;
            this.txtAddress.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtBrand
            // 
            this.txtBrand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtBrand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtBrand.BackColor = System.Drawing.Color.Transparent;
            this.txtBrand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtBrand.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtBrand.ForeImage = null;
            this.txtBrand.Location = new System.Drawing.Point(680, 40);
            this.txtBrand.MaxLengh = 32767;
            this.txtBrand.Multiline = false;
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Radius = 3;
            this.txtBrand.ReadOnly = false;
            this.txtBrand.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtBrand.Size = new System.Drawing.Size(120, 23);
            this.txtBrand.TabIndex = 85;
            this.txtBrand.UseSystemPasswordChar = false;
            this.txtBrand.WaterMark = null;
            this.txtBrand.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labOrderStatus
            // 
            this.labOrderStatus.AutoSize = true;
            this.labOrderStatus.Location = new System.Drawing.Point(36, 13);
            this.labOrderStatus.Name = "labOrderStatus";
            this.labOrderStatus.Size = new System.Drawing.Size(41, 12);
            this.labOrderStatus.TabIndex = 94;
            this.labOrderStatus.Text = "公司：";
            // 
            // cobCompany
            // 
            this.cobCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobCompany.FormattingEnabled = true;
            this.cobCompany.Location = new System.Drawing.Point(83, 7);
            this.cobCompany.Name = "cobCompany";
            this.cobCompany.Size = new System.Drawing.Size(121, 22);
            this.cobCompany.TabIndex = 95;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 102;
            this.label3.Text = "仓库：";
            // 
            // cobWearhouse
            // 
            this.cobWearhouse.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobWearhouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobWearhouse.Enabled = false;
            this.cobWearhouse.FormattingEnabled = true;
            this.cobWearhouse.Location = new System.Drawing.Point(290, 7);
            this.cobWearhouse.Name = "cobWearhouse";
            this.cobWearhouse.Size = new System.Drawing.Size(121, 22);
            this.cobWearhouse.TabIndex = 103;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(425, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 104;
            this.label4.Text = "配件图号：";
            // 
            // txtDrawingNo
            // 
            this.txtDrawingNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtDrawingNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtDrawingNo.BackColor = System.Drawing.Color.Transparent;
            this.txtDrawingNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtDrawingNo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtDrawingNo.ForeImage = null;
            this.txtDrawingNo.Location = new System.Drawing.Point(496, 40);
            this.txtDrawingNo.MaxLengh = 32767;
            this.txtDrawingNo.Multiline = false;
            this.txtDrawingNo.Name = "txtDrawingNo";
            this.txtDrawingNo.Radius = 3;
            this.txtDrawingNo.ReadOnly = false;
            this.txtDrawingNo.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtDrawingNo.Size = new System.Drawing.Size(120, 23);
            this.txtDrawingNo.TabIndex = 105;
            this.txtDrawingNo.UseSystemPasswordChar = false;
            this.txtDrawingNo.WaterMark = null;
            this.txtDrawingNo.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(642, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 106;
            this.label5.Text = "条码：";
            // 
            // txtBar
            // 
            this.txtBar.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtBar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtBar.BackColor = System.Drawing.Color.Transparent;
            this.txtBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtBar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtBar.ForeImage = null;
            this.txtBar.Location = new System.Drawing.Point(680, 6);
            this.txtBar.MaxLengh = 32767;
            this.txtBar.Multiline = false;
            this.txtBar.Name = "txtBar";
            this.txtBar.Radius = 3;
            this.txtBar.ReadOnly = false;
            this.txtBar.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtBar.Size = new System.Drawing.Size(120, 23);
            this.txtBar.TabIndex = 107;
            this.txtBar.UseSystemPasswordChar = false;
            this.txtBar.WaterMark = null;
            this.txtBar.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labPartsCode
            // 
            this.labPartsCode.AutoSize = true;
            this.labPartsCode.Location = new System.Drawing.Point(12, 44);
            this.labPartsCode.Name = "labPartsCode";
            this.labPartsCode.Size = new System.Drawing.Size(65, 12);
            this.labPartsCode.TabIndex = 146;
            this.labPartsCode.Text = "配件编码：";
            // 
            // txtPartsCode
            // 
            this.txtPartsCode.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtPartsCode.Location = new System.Drawing.Point(83, 40);
            this.txtPartsCode.Name = "txtPartsCode";
            this.txtPartsCode.ReadOnly = false;
            this.txtPartsCode.Size = new System.Drawing.Size(117, 24);
            this.txtPartsCode.TabIndex = 147;
            this.txtPartsCode.ToolTipTitle = "";
            this.txtPartsCode.ChooserClick += new System.EventHandler(this.txtPartsCode_ChooserClick);
            // 
            // labPartsName
            // 
            this.labPartsName.AutoSize = true;
            this.labPartsName.Location = new System.Drawing.Point(223, 45);
            this.labPartsName.Name = "labPartsName";
            this.labPartsName.Size = new System.Drawing.Size(65, 12);
            this.labPartsName.TabIndex = 148;
            this.labPartsName.Text = "配件名称：";
            // 
            // txtPartsName
            // 
            this.txtPartsName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPartsName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPartsName.BackColor = System.Drawing.Color.Transparent;
            this.txtPartsName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtPartsName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtPartsName.ForeImage = null;
            this.txtPartsName.Location = new System.Drawing.Point(290, 40);
            this.txtPartsName.MaxLengh = 32767;
            this.txtPartsName.Multiline = false;
            this.txtPartsName.Name = "txtPartsName";
            this.txtPartsName.Radius = 3;
            this.txtPartsName.ReadOnly = false;
            this.txtPartsName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsName.Size = new System.Drawing.Size(120, 23);
            this.txtPartsName.TabIndex = 149;
            this.txtPartsName.UseSystemPasswordChar = false;
            this.txtPartsName.WaterMark = null;
            this.txtPartsName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 150;
            this.label1.Text = "配件车型：";
            // 
            // txtCarType
            // 
            this.txtCarType.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCarType.Location = new System.Drawing.Point(83, 70);
            this.txtCarType.Name = "txtCarType";
            this.txtCarType.ReadOnly = false;
            this.txtCarType.Size = new System.Drawing.Size(117, 24);
            this.txtCarType.TabIndex = 151;
            this.txtCarType.ToolTipTitle = "";
            this.txtCarType.ChooserClick += new System.EventHandler(this.textChooser1_ChooserClick);
            // 
            // palQTop
            // 
            this.palQTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palQTop.Controls.Add(this.btnQuery);
            this.palQTop.Controls.Add(this.btnClear);
            this.palQTop.Controls.Add(this.txtCarType);
            this.palQTop.Controls.Add(this.label1);
            this.palQTop.Controls.Add(this.txtPartsName);
            this.palQTop.Controls.Add(this.labPartsName);
            this.palQTop.Controls.Add(this.txtPartsCode);
            this.palQTop.Controls.Add(this.labPartsCode);
            this.palQTop.Controls.Add(this.txtBar);
            this.palQTop.Controls.Add(this.label5);
            this.palQTop.Controls.Add(this.txtDrawingNo);
            this.palQTop.Controls.Add(this.label4);
            this.palQTop.Controls.Add(this.cobWearhouse);
            this.palQTop.Controls.Add(this.label3);
            this.palQTop.Controls.Add(this.cobCompany);
            this.palQTop.Controls.Add(this.labOrderStatus);
            this.palQTop.Controls.Add(this.txtBrand);
            this.palQTop.Controls.Add(this.txtAddress);
            this.palQTop.Controls.Add(this.labRemark);
            this.palQTop.Controls.Add(this.labContact);
            this.palQTop.Location = new System.Drawing.Point(0, 33);
            this.palQTop.Name = "palQTop";
            this.palQTop.Size = new System.Drawing.Size(1027, 113);
            this.palQTop.TabIndex = 20;
            // 
            // btnQuery
            // 
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(818, 46);
            this.btnQuery.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.MoveImage")));
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.NormalImage")));
            this.btnQuery.Size = new System.Drawing.Size(60, 26);
            this.btnQuery.TabIndex = 155;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(818, 13);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 154;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // UCOldPartsInventoryQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.palData);
            this.Controls.Add(this.palQTop);
            this.Name = "UCOldPartsInventoryQuery";
            this.Load += new System.EventHandler(this.UCOldPartsInventoryQuery_Load);
            this.Controls.SetChildIndex(this.palQTop, 0);
            this.Controls.SetChildIndex(this.palData, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.palData.ResumeLayout(false);
            this.splitData.Panel1.ResumeLayout(false);
            this.splitData.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitData)).EndInit();
            this.splitData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.palQTop.ResumeLayout(false);
            this.palQTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

      
        private System.Windows.Forms.Panel palData;
        private System.Windows.Forms.SplitContainer splitData;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRData;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn receipt_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn drawn_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn norms;
        private System.Windows.Forms.DataGridViewTextBoxColumn book_inventory_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn inventory_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn average_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn sum_money;
        private System.Windows.Forms.DataGridViewTextBoxColumn bar_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn weight;
        private System.Windows.Forms.DataGridViewTextBoxColumn origin_place;
        private System.Windows.Forms.DataGridViewTextBoxColumn trademark;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_model;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn oldpart_id;
     
        private System.Windows.Forms.Label labContact;
        private System.Windows.Forms.Label labRemark;
        private ServiceStationClient.ComponentUI.TextBoxEx txtAddress;
        private ServiceStationClient.ComponentUI.TextBoxEx txtBrand;
        private System.Windows.Forms.Label labOrderStatus;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobCompany;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobWearhouse;
        private System.Windows.Forms.Label label4;
        private ServiceStationClient.ComponentUI.TextBoxEx txtDrawingNo;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.TextBoxEx txtBar;
        private System.Windows.Forms.Label labPartsCode;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtPartsCode;
        private System.Windows.Forms.Label labPartsName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtPartsName;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCarType;
        private System.Windows.Forms.Panel palQTop;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.TreeViewEx tvData;
    }
}