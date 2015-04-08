namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.AllocationBill
{
    partial class UCImportBilling
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCImportBilling));
            this.dgPartslist = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.PartBillID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.parts_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drawing_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrigPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BusPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BusCounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valorem_together = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReferenceCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemainCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arrival_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_parts_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgBillList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.billingID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.OrderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_type_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.balance_money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.org_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.handle_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operator_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtparts_type = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txtparts_name = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txtparts_code = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtorder_num = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ddlhandle = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.ddloperator = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.ddlorg_id = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimeEnd = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dateTimeStart = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClose = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnOK = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnNotCheck = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnAllCheck = new ServiceStationClient.ComponentUI.ButtonEx();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPartslist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBillList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.btnAllCheck);
            this.pnlContainer.Controls.Add(this.btnNotCheck);
            this.pnlContainer.Controls.Add(this.btnOK);
            this.pnlContainer.Controls.Add(this.btnClose);
            this.pnlContainer.Controls.Add(this.btnSearch);
            this.pnlContainer.Controls.Add(this.btnClear);
            this.pnlContainer.Controls.Add(this.dateTimeStart);
            this.pnlContainer.Controls.Add(this.dateTimeEnd);
            this.pnlContainer.Controls.Add(this.dgPartslist);
            this.pnlContainer.Controls.Add(this.dgBillList);
            this.pnlContainer.Controls.Add(this.txtparts_type);
            this.pnlContainer.Controls.Add(this.txtparts_name);
            this.pnlContainer.Controls.Add(this.txtparts_code);
            this.pnlContainer.Controls.Add(this.label4);
            this.pnlContainer.Controls.Add(this.label3);
            this.pnlContainer.Controls.Add(this.label2);
            this.pnlContainer.Controls.Add(this.txtorder_num);
            this.pnlContainer.Controls.Add(this.label5);
            this.pnlContainer.Controls.Add(this.label7);
            this.pnlContainer.Controls.Add(this.label1);
            this.pnlContainer.Controls.Add(this.ddlhandle);
            this.pnlContainer.Controls.Add(this.ddloperator);
            this.pnlContainer.Controls.Add(this.ddlorg_id);
            this.pnlContainer.Controls.Add(this.label9);
            this.pnlContainer.Controls.Add(this.label8);
            this.pnlContainer.Controls.Add(this.label6);
            this.pnlContainer.Size = new System.Drawing.Size(817, 482);
            // 
            // dgPartslist
            // 
            this.dgPartslist.AllowUserToAddRows = false;
            this.dgPartslist.AllowUserToDeleteRows = false;
            this.dgPartslist.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgPartslist.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgPartslist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgPartslist.BackgroundColor = System.Drawing.Color.White;
            this.dgPartslist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPartslist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgPartslist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPartslist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PartBillID,
            this.PartID,
            this.colDetailCheck,
            this.parts_code,
            this.parts_name,
            this.drawing_num,
            this.unit,
            this.OrigPrice,
            this.BusPrice,
            this.BusCounts,
            this.valorem_together,
            this.ReferenceCount,
            this.RemainCount,
            this.arrival_date,
            this.car_parts_code});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPartslist.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgPartslist.EnableHeadersVisualStyles = false;
            this.dgPartslist.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgPartslist.Location = new System.Drawing.Point(10, 277);
            this.dgPartslist.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgPartslist.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgPartslist.MergeColumnNames")));
            this.dgPartslist.MultiSelect = false;
            this.dgPartslist.Name = "dgPartslist";
            this.dgPartslist.ReadOnly = true;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPartslist.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgPartslist.RowHeadersVisible = false;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgPartslist.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dgPartslist.RowTemplate.Height = 23;
            this.dgPartslist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPartslist.ShowCheckBox = true;
            this.dgPartslist.ShowNum = false;
            this.dgPartslist.Size = new System.Drawing.Size(792, 154);
            this.dgPartslist.TabIndex = 103;
            this.dgPartslist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPartslist_CellContentClick);
            // 
            // PartBillID
            // 
            this.PartBillID.HeaderText = "开单主键ID";
            this.PartBillID.Name = "PartBillID";
            this.PartBillID.ReadOnly = true;
            this.PartBillID.Visible = false;
            // 
            // PartID
            // 
            this.PartID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PartID.DataPropertyName = "PartID";
            this.PartID.HeaderText = "序号";
            this.PartID.Name = "PartID";
            this.PartID.ReadOnly = true;
            this.PartID.Width = 57;
            // 
            // colDetailCheck
            // 
            this.colDetailCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colDetailCheck.HeaderText = "";
            this.colDetailCheck.Name = "colDetailCheck";
            this.colDetailCheck.ReadOnly = true;
            this.colDetailCheck.Width = 30;
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
            // drawing_num
            // 
            this.drawing_num.DataPropertyName = "drawing_num";
            this.drawing_num.HeaderText = "图号";
            this.drawing_num.Name = "drawing_num";
            this.drawing_num.ReadOnly = true;
            // 
            // unit
            // 
            this.unit.DataPropertyName = "unit";
            this.unit.HeaderText = "单位";
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            this.unit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // OrigPrice
            // 
            this.OrigPrice.DataPropertyName = "original_price";
            this.OrigPrice.HeaderText = "单价";
            this.OrigPrice.Name = "OrigPrice";
            this.OrigPrice.ReadOnly = true;
            // 
            // BusPrice
            // 
            this.BusPrice.DataPropertyName = "business_price";
            this.BusPrice.HeaderText = "含税单价";
            this.BusPrice.Name = "BusPrice";
            this.BusPrice.ReadOnly = true;
            // 
            // BusCounts
            // 
            this.BusCounts.DataPropertyName = "business_counts";
            this.BusCounts.HeaderText = "数量";
            this.BusCounts.Name = "BusCounts";
            this.BusCounts.ReadOnly = true;
            // 
            // valorem_together
            // 
            this.valorem_together.DataPropertyName = "valorem_together";
            this.valorem_together.HeaderText = "金额";
            this.valorem_together.Name = "valorem_together";
            this.valorem_together.ReadOnly = true;
            // 
            // ReferenceCount
            // 
            this.ReferenceCount.HeaderText = "引用数量";
            this.ReferenceCount.Name = "ReferenceCount";
            this.ReferenceCount.ReadOnly = true;
            // 
            // RemainCount
            // 
            this.RemainCount.DataPropertyName = "return_bus_count";
            this.RemainCount.HeaderText = "剩余数量";
            this.RemainCount.Name = "RemainCount";
            this.RemainCount.ReadOnly = true;
            // 
            // arrival_date
            // 
            this.arrival_date.DataPropertyName = "arrival_date";
            this.arrival_date.HeaderText = "到货日期";
            this.arrival_date.Name = "arrival_date";
            this.arrival_date.ReadOnly = true;
            // 
            // car_parts_code
            // 
            this.car_parts_code.DataPropertyName = "car_parts_code";
            this.car_parts_code.HeaderText = "厂商编码";
            this.car_parts_code.Name = "car_parts_code";
            this.car_parts_code.ReadOnly = true;
            // 
            // dgBillList
            // 
            this.dgBillList.AllowUserToAddRows = false;
            this.dgBillList.AllowUserToDeleteRows = false;
            this.dgBillList.AllowUserToResizeRows = false;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgBillList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgBillList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgBillList.BackgroundColor = System.Drawing.Color.White;
            this.dgBillList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgBillList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dgBillList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBillList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.billingID,
            this.ID,
            this.colCheck,
            this.OrderName,
            this.order_type_name,
            this.order_num,
            this.order_date,
            this.balance_money,
            this.org_name,
            this.handle_name,
            this.operator_name,
            this.remark});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgBillList.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgBillList.EnableHeadersVisualStyles = false;
            this.dgBillList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgBillList.Location = new System.Drawing.Point(11, 103);
            this.dgBillList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgBillList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgBillList.MergeColumnNames")));
            this.dgBillList.MultiSelect = false;
            this.dgBillList.Name = "dgBillList";
            this.dgBillList.ReadOnly = true;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgBillList.RowHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgBillList.RowHeadersVisible = false;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgBillList.RowsDefaultCellStyle = dataGridViewCellStyle20;
            this.dgBillList.RowTemplate.Height = 23;
            this.dgBillList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBillList.ShowCheckBox = true;
            this.dgBillList.ShowNum = false;
            this.dgBillList.Size = new System.Drawing.Size(792, 168);
            this.dgBillList.TabIndex = 102;
            this.dgBillList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBillList_CellClick);
            this.dgBillList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBillList_CellContentClick);
            // 
            // billingID
            // 
            this.billingID.HeaderText = "开单主键ID";
            this.billingID.Name = "billingID";
            this.billingID.ReadOnly = true;
            this.billingID.Visible = false;
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ID.DataPropertyName = "BillID";
            this.ID.HeaderText = "序号";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 57;
            // 
            // colCheck
            // 
            this.colCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colCheck.HeaderText = "";
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 30;
            // 
            // OrderName
            // 
            this.OrderName.HeaderText = "单据名称";
            this.OrderName.Name = "OrderName";
            this.OrderName.ReadOnly = true;
            // 
            // order_type_name
            // 
            this.order_type_name.DataPropertyName = "order_type_name";
            this.order_type_name.HeaderText = "单据类型";
            this.order_type_name.Name = "order_type_name";
            this.order_type_name.ReadOnly = true;
            // 
            // order_num
            // 
            this.order_num.DataPropertyName = "order_num";
            this.order_num.HeaderText = "单据号码";
            this.order_num.Name = "order_num";
            this.order_num.ReadOnly = true;
            // 
            // order_date
            // 
            this.order_date.DataPropertyName = "order_date";
            this.order_date.HeaderText = "开单日期";
            this.order_date.Name = "order_date";
            this.order_date.ReadOnly = true;
            // 
            // balance_money
            // 
            this.balance_money.DataPropertyName = "balance_money";
            this.balance_money.HeaderText = "金额";
            this.balance_money.Name = "balance_money";
            this.balance_money.ReadOnly = true;
            // 
            // org_name
            // 
            this.org_name.DataPropertyName = "org_name";
            this.org_name.HeaderText = "部门";
            this.org_name.Name = "org_name";
            this.org_name.ReadOnly = true;
            // 
            // handle_name
            // 
            this.handle_name.DataPropertyName = "handle_name";
            this.handle_name.HeaderText = "经办人";
            this.handle_name.Name = "handle_name";
            this.handle_name.ReadOnly = true;
            // 
            // operator_name
            // 
            this.operator_name.DataPropertyName = "operator_name";
            this.operator_name.HeaderText = "操作人";
            this.operator_name.Name = "operator_name";
            this.operator_name.ReadOnly = true;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            // 
            // txtparts_type
            // 
            this.txtparts_type.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.PartType;
            this.txtparts_type.Location = new System.Drawing.Point(567, 73);
            this.txtparts_type.Name = "txtparts_type";
            this.txtparts_type.ReadOnly = false;
            this.txtparts_type.Size = new System.Drawing.Size(144, 24);
            this.txtparts_type.TabIndex = 101;
            this.txtparts_type.ToolTipTitle = "";
            this.txtparts_type.Visible = false;
            this.txtparts_type.ChooserClick += new System.EventHandler(this.txtparts_type_ChooserClick);
            // 
            // txtparts_name
            // 
            this.txtparts_name.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Part;
            this.txtparts_name.Location = new System.Drawing.Point(327, 73);
            this.txtparts_name.Name = "txtparts_name";
            this.txtparts_name.ReadOnly = false;
            this.txtparts_name.Size = new System.Drawing.Size(143, 24);
            this.txtparts_name.TabIndex = 100;
            this.txtparts_name.ToolTipTitle = "";
            this.txtparts_name.Visible = false;
            this.txtparts_name.ChooserClick += new System.EventHandler(this.txtparts_name_ChooserClick);
            // 
            // txtparts_code
            // 
            this.txtparts_code.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.PartCode;
            this.txtparts_code.Location = new System.Drawing.Point(90, 73);
            this.txtparts_code.Name = "txtparts_code";
            this.txtparts_code.ReadOnly = false;
            this.txtparts_code.Size = new System.Drawing.Size(146, 24);
            this.txtparts_code.TabIndex = 99;
            this.txtparts_code.ToolTipTitle = "";
            this.txtparts_code.Visible = false;
            this.txtparts_code.ChooserClick += new System.EventHandler(this.txtparts_code_ChooserClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(496, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 98;
            this.label4.Text = "配件类别：";
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(256, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 97;
            this.label3.Text = "配件名称：";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 96;
            this.label2.Text = "配件编码：";
            this.label2.Visible = false;
            // 
            // txtorder_num
            // 
            this.txtorder_num.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtorder_num.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtorder_num.BackColor = System.Drawing.Color.Transparent;
            this.txtorder_num.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtorder_num.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtorder_num.ForeImage = null;
            this.txtorder_num.Location = new System.Drawing.Point(567, 13);
            this.txtorder_num.MaxLengh = 32767;
            this.txtorder_num.Multiline = false;
            this.txtorder_num.Name = "txtorder_num";
            this.txtorder_num.Radius = 3;
            this.txtorder_num.ReadOnly = false;
            this.txtorder_num.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtorder_num.Size = new System.Drawing.Size(144, 23);
            this.txtorder_num.TabIndex = 95;
            this.txtorder_num.UseSystemPasswordChar = false;
            this.txtorder_num.WaterMark = null;
            this.txtorder_num.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(520, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 94;
            this.label5.Text = "单号：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 90;
            this.label7.Text = "查询日期：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(271, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 91;
            this.label1.Text = "至";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ddlhandle
            // 
            this.ddlhandle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlhandle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlhandle.FormattingEnabled = true;
            this.ddlhandle.Location = new System.Drawing.Point(327, 44);
            this.ddlhandle.Name = "ddlhandle";
            this.ddlhandle.Size = new System.Drawing.Size(143, 22);
            this.ddlhandle.TabIndex = 87;
            // 
            // ddloperator
            // 
            this.ddloperator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddloperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddloperator.FormattingEnabled = true;
            this.ddloperator.Location = new System.Drawing.Point(567, 44);
            this.ddloperator.Name = "ddloperator";
            this.ddloperator.Size = new System.Drawing.Size(144, 22);
            this.ddloperator.TabIndex = 86;
            // 
            // ddlorg_id
            // 
            this.ddlorg_id.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlorg_id.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlorg_id.FormattingEnabled = true;
            this.ddlorg_id.Location = new System.Drawing.Point(92, 44);
            this.ddlorg_id.Name = "ddlorg_id";
            this.ddlorg_id.Size = new System.Drawing.Size(144, 22);
            this.ddlorg_id.TabIndex = 85;
            this.ddlorg_id.SelectedIndexChanged += new System.EventHandler(this.ddlorg_id_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(508, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 84;
            this.label9.Text = "操作人：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(268, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 83;
            this.label8.Text = "经办人：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 82;
            this.label6.Text = "部门：";
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Location = new System.Drawing.Point(327, 16);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.ShowFormat = "yyyy-MM-dd";
            this.dateTimeEnd.Size = new System.Drawing.Size(142, 21);
            this.dateTimeEnd.TabIndex = 108;
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Location = new System.Drawing.Point(92, 17);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.ShowFormat = "yyyy-MM-dd";
            this.dateTimeStart.Size = new System.Drawing.Size(144, 21);
            this.dateTimeStart.TabIndex = 109;
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.Location = new System.Drawing.Point(744, 30);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 110;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(744, 65);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(60, 26);
            this.btnSearch.TabIndex = 111;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Caption = "关闭";
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClose.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClose.DownImage")));
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(742, 442);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClose.MoveImage")));
            this.btnClose.Name = "btnClose";
            this.btnClose.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClose.NormalImage")));
            this.btnClose.Size = new System.Drawing.Size(60, 26);
            this.btnClose.TabIndex = 112;
            this.btnClose.Click += new System.EventHandler(this.btnColse_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.Caption = "确定";
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.DownImage = ((System.Drawing.Image)(resources.GetObject("btnOK.DownImage")));
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(651, 442);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnOK.MoveImage")));
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnOK.NormalImage")));
            this.btnOK.Size = new System.Drawing.Size(60, 26);
            this.btnOK.TabIndex = 113;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnNotCheck
            // 
            this.btnNotCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNotCheck.BackgroundImage")));
            this.btnNotCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNotCheck.Caption = "反选";
            this.btnNotCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNotCheck.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnNotCheck.DownImage = ((System.Drawing.Image)(resources.GetObject("btnNotCheck.DownImage")));
            this.btnNotCheck.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNotCheck.Location = new System.Drawing.Point(567, 442);
            this.btnNotCheck.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNotCheck.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnNotCheck.MoveImage")));
            this.btnNotCheck.Name = "btnNotCheck";
            this.btnNotCheck.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnNotCheck.NormalImage")));
            this.btnNotCheck.Size = new System.Drawing.Size(60, 26);
            this.btnNotCheck.TabIndex = 114;
            this.btnNotCheck.Click += new System.EventHandler(this.btnNotCheck_Click);
            // 
            // btnAllCheck
            // 
            this.btnAllCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAllCheck.BackgroundImage")));
            this.btnAllCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAllCheck.Caption = "全选";
            this.btnAllCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAllCheck.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAllCheck.DownImage = ((System.Drawing.Image)(resources.GetObject("btnAllCheck.DownImage")));
            this.btnAllCheck.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAllCheck.Location = new System.Drawing.Point(481, 442);
            this.btnAllCheck.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAllCheck.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnAllCheck.MoveImage")));
            this.btnAllCheck.Name = "btnAllCheck";
            this.btnAllCheck.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnAllCheck.NormalImage")));
            this.btnAllCheck.Size = new System.Drawing.Size(60, 26);
            this.btnAllCheck.TabIndex = 115;
            this.btnAllCheck.Click += new System.EventHandler(this.btnAllCheck_Click);
            // 
            // UCImportBilling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 514);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "UCImportBilling";
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPartslist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBillList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx dgPartslist;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgBillList;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtparts_type;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtparts_name;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtparts_code;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx txtorder_num;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlhandle;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddloperator;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlorg_id;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dateTimeEnd;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dateTimeStart;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClose;
        private ServiceStationClient.ComponentUI.ButtonEx btnOK;
        private ServiceStationClient.ComponentUI.ButtonEx btnNotCheck;
        private ServiceStationClient.ComponentUI.ButtonEx btnAllCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn billingID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderName;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_type_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn balance_money;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn handle_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn operator_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartBillID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colDetailCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn drawing_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrigPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn BusPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn BusCounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn valorem_together;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReferenceCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemainCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn arrival_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_parts_code;
    }
}