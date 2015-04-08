namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.AllocationBill
{
    partial class UCImportStockBill
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCImportStockBill));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dateTimeStart = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dateTimeEnd = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.btnAllCheck = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnNotCheck = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnOK = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnColse = new ServiceStationClient.ComponentUI.ButtonEx();
            this.dgPartslist = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.purchase_billing_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.parts_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drawing_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.business_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.business_counts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_factory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgBillList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.StockBillingId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.OrderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.money = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.ddlhandle = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.ddloperator = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.ddlorg_id = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPartslist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBillList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.dateTimeStart);
            this.pnlContainer.Controls.Add(this.dateTimeEnd);
            this.pnlContainer.Controls.Add(this.btnAllCheck);
            this.pnlContainer.Controls.Add(this.btnNotCheck);
            this.pnlContainer.Controls.Add(this.btnOK);
            this.pnlContainer.Controls.Add(this.btnColse);
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
            this.pnlContainer.Controls.Add(this.btnClear);
            this.pnlContainer.Controls.Add(this.btnSearch);
            this.pnlContainer.Controls.Add(this.ddlhandle);
            this.pnlContainer.Controls.Add(this.ddloperator);
            this.pnlContainer.Controls.Add(this.ddlorg_id);
            this.pnlContainer.Controls.Add(this.label9);
            this.pnlContainer.Controls.Add(this.label8);
            this.pnlContainer.Controls.Add(this.label6);
            this.pnlContainer.Size = new System.Drawing.Size(816, 483);
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Location = new System.Drawing.Point(93, 18);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.ShowFormat = "yyyy-MM-dd";
            this.dateTimeStart.Size = new System.Drawing.Size(144, 21);
            this.dateTimeStart.TabIndex = 135;
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Location = new System.Drawing.Point(328, 17);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(142, 21);
            this.dateTimeEnd.TabIndex = 134;
            // 
            // btnAllCheck
            // 
            this.btnAllCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAllCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAllCheck.BackgroundImage")));
            this.btnAllCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAllCheck.Caption = "全选";
            this.btnAllCheck.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAllCheck.DownImage = null;
            this.btnAllCheck.Location = new System.Drawing.Point(480, 443);
            this.btnAllCheck.MoveImage = null;
            this.btnAllCheck.Name = "btnAllCheck";
            this.btnAllCheck.NormalImage = null;
            this.btnAllCheck.Size = new System.Drawing.Size(60, 26);
            this.btnAllCheck.TabIndex = 133;
            this.btnAllCheck.Click += new System.EventHandler(this.btnAllCheck_Click);
            // 
            // btnNotCheck
            // 
            this.btnNotCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNotCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNotCheck.BackgroundImage")));
            this.btnNotCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNotCheck.Caption = "反选";
            this.btnNotCheck.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnNotCheck.DownImage = null;
            this.btnNotCheck.Location = new System.Drawing.Point(546, 443);
            this.btnNotCheck.MoveImage = null;
            this.btnNotCheck.Name = "btnNotCheck";
            this.btnNotCheck.NormalImage = null;
            this.btnNotCheck.Size = new System.Drawing.Size(60, 26);
            this.btnNotCheck.TabIndex = 132;
            this.btnNotCheck.Click += new System.EventHandler(this.btnNotCheck_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.Caption = "确定";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.DownImage = null;
            this.btnOK.Location = new System.Drawing.Point(651, 443);
            this.btnOK.MoveImage = null;
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalImage = null;
            this.btnOK.Size = new System.Drawing.Size(60, 26);
            this.btnOK.TabIndex = 131;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnColse
            // 
            this.btnColse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnColse.BackgroundImage")));
            this.btnColse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnColse.Caption = "关闭";
            this.btnColse.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnColse.DownImage = null;
            this.btnColse.Location = new System.Drawing.Point(717, 443);
            this.btnColse.MoveImage = null;
            this.btnColse.Name = "btnColse";
            this.btnColse.NormalImage = null;
            this.btnColse.Size = new System.Drawing.Size(60, 26);
            this.btnColse.TabIndex = 130;
            this.btnColse.Click += new System.EventHandler(this.btnColse_Click);
            // 
            // dgPartslist
            // 
            this.dgPartslist.AllowUserToAddRows = false;
            this.dgPartslist.AllowUserToDeleteRows = false;
            this.dgPartslist.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgPartslist.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPartslist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgPartslist.BackgroundColor = System.Drawing.Color.White;
            this.dgPartslist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPartslist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgPartslist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPartslist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.purchase_billing_id,
            this.PartID,
            this.colDetailCheck,
            this.parts_code,
            this.parts_name,
            this.drawing_num,
            this.unit,
            this.business_price,
            this.Column2,
            this.business_counts,
            this.total_money,
            this.Column5,
            this.Column6,
            this.parts_factory,
            this.Column3,
            this.Column4,
            this.Column7});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPartslist.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgPartslist.EnableHeadersVisualStyles = false;
            this.dgPartslist.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgPartslist.Location = new System.Drawing.Point(11, 278);
            this.dgPartslist.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgPartslist.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgPartslist.MergeColumnNames")));
            this.dgPartslist.MultiSelect = false;
            this.dgPartslist.Name = "dgPartslist";
            this.dgPartslist.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPartslist.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgPartslist.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgPartslist.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgPartslist.RowTemplate.Height = 23;
            this.dgPartslist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPartslist.ShowCheckBox = true;
            this.dgPartslist.ShowNum = false;
            this.dgPartslist.Size = new System.Drawing.Size(792, 154);
            this.dgPartslist.TabIndex = 129;
            // 
            // purchase_billing_id
            // 
            this.purchase_billing_id.DataPropertyName = "purchase_billing_id";
            this.purchase_billing_id.HeaderText = "purchase_billing_id";
            this.purchase_billing_id.Name = "purchase_billing_id";
            this.purchase_billing_id.ReadOnly = true;
            this.purchase_billing_id.Visible = false;
            // 
            // PartID
            // 
            this.PartID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
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
            this.unit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // business_price
            // 
            this.business_price.DataPropertyName = "business_price";
            this.business_price.HeaderText = "单价";
            this.business_price.Name = "business_price";
            this.business_price.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "含税单价";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // business_counts
            // 
            this.business_counts.DataPropertyName = "business_counts";
            this.business_counts.HeaderText = "数量";
            this.business_counts.Name = "business_counts";
            this.business_counts.ReadOnly = true;
            // 
            // total_money
            // 
            this.total_money.HeaderText = "金额";
            this.total_money.Name = "total_money";
            this.total_money.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "已引用数量";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "剩余数量";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // parts_factory
            // 
            this.parts_factory.HeaderText = "到货日期";
            this.parts_factory.Name = "parts_factory";
            this.parts_factory.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "辅助单位";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "辅助数量";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "厂商编码";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // dgBillList
            // 
            this.dgBillList.AllowUserToAddRows = false;
            this.dgBillList.AllowUserToDeleteRows = false;
            this.dgBillList.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgBillList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgBillList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgBillList.BackgroundColor = System.Drawing.Color.White;
            this.dgBillList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgBillList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgBillList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBillList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StockBillingId,
            this.ID,
            this.colCheck,
            this.OrderName,
            this.order_num,
            this.OrderState,
            this.money,
            this.org_name,
            this.handle_name,
            this.operator_name,
            this.remark});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgBillList.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgBillList.EnableHeadersVisualStyles = false;
            this.dgBillList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgBillList.Location = new System.Drawing.Point(12, 104);
            this.dgBillList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgBillList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgBillList.MergeColumnNames")));
            this.dgBillList.MultiSelect = false;
            this.dgBillList.Name = "dgBillList";
            this.dgBillList.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgBillList.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgBillList.RowHeadersVisible = false;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgBillList.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgBillList.RowTemplate.Height = 23;
            this.dgBillList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBillList.ShowCheckBox = true;
            this.dgBillList.ShowNum = false;
            this.dgBillList.Size = new System.Drawing.Size(792, 168);
            this.dgBillList.TabIndex = 128;
            this.dgBillList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBillList_CellClick);
            this.dgBillList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBillList_CellContentClick);
            // 
            // StockBillingId
            // 
            this.StockBillingId.HeaderText = "库存开单主键ID";
            this.StockBillingId.Name = "StockBillingId";
            this.StockBillingId.ReadOnly = true;
            this.StockBillingId.Visible = false;
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ID.DataPropertyName = "id";
            this.ID.HeaderText = "序号";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
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
            this.OrderName.DataPropertyName = "OrderName";
            this.OrderName.HeaderText = "单据名称";
            this.OrderName.Name = "OrderName";
            this.OrderName.ReadOnly = true;
            // 
            // order_num
            // 
            this.order_num.DataPropertyName = "order_num";
            this.order_num.HeaderText = "单据号码";
            this.order_num.Name = "order_num";
            this.order_num.ReadOnly = true;
            // 
            // OrderState
            // 
            this.OrderState.DataPropertyName = "OrderState";
            this.OrderState.HeaderText = "开单日期";
            this.OrderState.Name = "OrderState";
            this.OrderState.ReadOnly = true;
            // 
            // money
            // 
            this.money.DataPropertyName = "money";
            this.money.HeaderText = "金额";
            this.money.Name = "money";
            this.money.ReadOnly = true;
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
            this.operator_name.DataPropertyName = "operatorname";
            this.operator_name.HeaderText = "操作人";
            this.operator_name.Name = "operator_name";
            this.operator_name.ReadOnly = true;
            // 
            // remark
            // 
            this.remark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            // 
            // txtparts_type
            // 
            this.txtparts_type.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.PartType;
            this.txtparts_type.Location = new System.Drawing.Point(568, 74);
            this.txtparts_type.Name = "txtparts_type";
            this.txtparts_type.ReadOnly = false;
            this.txtparts_type.Size = new System.Drawing.Size(144, 24);
            this.txtparts_type.TabIndex = 127;
            this.txtparts_type.ToolTipTitle = "";
            this.txtparts_type.Visible = false;
            this.txtparts_type.ChooserClick += new System.EventHandler(this.txtparts_type_ChooserClick);
            // 
            // txtparts_name
            // 
            this.txtparts_name.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Part;
            this.txtparts_name.Location = new System.Drawing.Point(328, 74);
            this.txtparts_name.Name = "txtparts_name";
            this.txtparts_name.ReadOnly = false;
            this.txtparts_name.Size = new System.Drawing.Size(143, 24);
            this.txtparts_name.TabIndex = 126;
            this.txtparts_name.ToolTipTitle = "";
            this.txtparts_name.Visible = false;
            this.txtparts_name.ChooserClick += new System.EventHandler(this.txtparts_name_ChooserClick);
            // 
            // txtparts_code
            // 
            this.txtparts_code.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.PartCode;
            this.txtparts_code.Location = new System.Drawing.Point(91, 74);
            this.txtparts_code.Name = "txtparts_code";
            this.txtparts_code.ReadOnly = false;
            this.txtparts_code.Size = new System.Drawing.Size(146, 24);
            this.txtparts_code.TabIndex = 125;
            this.txtparts_code.ToolTipTitle = "";
            this.txtparts_code.Visible = false;
            this.txtparts_code.ChooserClick += new System.EventHandler(this.txtparts_code_ChooserClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(497, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 124;
            this.label4.Text = "配件类别：";
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(257, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 123;
            this.label3.Text = "配件名称：";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 122;
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
            this.txtorder_num.Location = new System.Drawing.Point(568, 14);
            this.txtorder_num.MaxLengh = 32767;
            this.txtorder_num.Multiline = false;
            this.txtorder_num.Name = "txtorder_num";
            this.txtorder_num.Radius = 3;
            this.txtorder_num.ReadOnly = false;
            this.txtorder_num.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtorder_num.Size = new System.Drawing.Size(144, 23);
            this.txtorder_num.TabIndex = 121;
            this.txtorder_num.UseSystemPasswordChar = false;
            this.txtorder_num.WaterMark = null;
            this.txtorder_num.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(521, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 120;
            this.label5.Text = "单号：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 118;
            this.label7.Text = "查询日期：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(272, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 119;
            this.label1.Text = "至";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清空";
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = null;
            this.btnClear.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.Location = new System.Drawing.Point(745, 36);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClear.MoveImage = null;
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = null;
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 117;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = null;
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(745, 68);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSearch.MoveImage = null;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = null;
            this.btnSearch.Size = new System.Drawing.Size(60, 26);
            this.btnSearch.TabIndex = 116;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ddlhandle
            // 
            this.ddlhandle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlhandle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlhandle.FormattingEnabled = true;
            this.ddlhandle.Location = new System.Drawing.Point(328, 45);
            this.ddlhandle.Name = "ddlhandle";
            this.ddlhandle.Size = new System.Drawing.Size(143, 22);
            this.ddlhandle.TabIndex = 115;
            // 
            // ddloperator
            // 
            this.ddloperator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddloperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddloperator.FormattingEnabled = true;
            this.ddloperator.Location = new System.Drawing.Point(568, 45);
            this.ddloperator.Name = "ddloperator";
            this.ddloperator.Size = new System.Drawing.Size(144, 22);
            this.ddloperator.TabIndex = 114;
            // 
            // ddlorg_id
            // 
            this.ddlorg_id.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlorg_id.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlorg_id.FormattingEnabled = true;
            this.ddlorg_id.Location = new System.Drawing.Point(93, 45);
            this.ddlorg_id.Name = "ddlorg_id";
            this.ddlorg_id.Size = new System.Drawing.Size(144, 22);
            this.ddlorg_id.TabIndex = 113;
            this.ddlorg_id.SelectedIndexChanged += new System.EventHandler(this.ddlorg_id_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(509, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 112;
            this.label9.Text = "操作人：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(269, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 111;
            this.label8.Text = "经办人：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 110;
            this.label6.Text = "部门：";
            // 
            // UCImportStockBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 514);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "UCImportStockBill";
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPartslist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBillList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dateTimeStart;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dateTimeEnd;
        private ServiceStationClient.ComponentUI.ButtonEx btnAllCheck;
        private ServiceStationClient.ComponentUI.ButtonEx btnNotCheck;
        private ServiceStationClient.ComponentUI.ButtonEx btnOK;
        private ServiceStationClient.ComponentUI.ButtonEx btnColse;
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
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlhandle;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddloperator;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlorg_id;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn purchase_billing_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colDetailCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn drawing_num;
        private System.Windows.Forms.DataGridViewComboBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn business_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn business_counts;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_money;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_factory;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockBillingId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderName;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderState;
        private System.Windows.Forms.DataGridViewTextBoxColumn money;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn handle_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn operator_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
    }
}