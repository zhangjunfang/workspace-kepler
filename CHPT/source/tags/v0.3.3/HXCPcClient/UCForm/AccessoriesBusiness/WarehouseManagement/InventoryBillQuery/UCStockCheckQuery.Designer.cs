namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.InventoryBillQuery
{
    partial class UCStockCheckQuery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCStockCheckQuery));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gvCheckQueryBillList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.ChkId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WHName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PapCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirmCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfitLosCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Calcmoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HandlerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InOutState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.winFormCheckQueryPage = new ServiceStationClient.ComponentUI.WinFormPager();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.txtremark = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimeEnd = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dateTimeStart = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.ComBInOutStatus = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label14 = new System.Windows.Forms.Label();
            this.ComBwh_name = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.ComBcom_name = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.ComBhandle_name = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.ComBorg_name = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.BtnExportMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExcelExport = new System.Windows.Forms.ToolStripTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gvCheckQueryBillList)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.BtnExportMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1023, 25);
            // 
            // gvCheckQueryBillList
            // 
            this.gvCheckQueryBillList.AllowUserToAddRows = false;
            this.gvCheckQueryBillList.AllowUserToDeleteRows = false;
            this.gvCheckQueryBillList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gvCheckQueryBillList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvCheckQueryBillList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvCheckQueryBillList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvCheckQueryBillList.BackgroundColor = System.Drawing.Color.White;
            this.gvCheckQueryBillList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCheckQueryBillList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvCheckQueryBillList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCheckQueryBillList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChkId,
            this.colIndex,
            this.BillNum,
            this.BillDate,
            this.WHName,
            this.PapCount,
            this.FirmCount,
            this.ProfitLosCount,
            this.Calcmoney,
            this.DepartName,
            this.HandlerName,
            this.OpeName,
            this.Remarks,
            this.InOutState});
            this.gvCheckQueryBillList.EnableHeadersVisualStyles = false;
            this.gvCheckQueryBillList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.gvCheckQueryBillList.Location = new System.Drawing.Point(4, 157);
            this.gvCheckQueryBillList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gvCheckQueryBillList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gvCheckQueryBillList.MergeColumnNames")));
            this.gvCheckQueryBillList.MultiSelect = false;
            this.gvCheckQueryBillList.Name = "gvCheckQueryBillList";
            this.gvCheckQueryBillList.ReadOnly = true;
            this.gvCheckQueryBillList.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.gvCheckQueryBillList.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gvCheckQueryBillList.RowTemplate.Height = 23;
            this.gvCheckQueryBillList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvCheckQueryBillList.ShowCheckBox = true;
            this.gvCheckQueryBillList.Size = new System.Drawing.Size(1015, 189);
            this.gvCheckQueryBillList.TabIndex = 55;
            this.gvCheckQueryBillList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCheckQueryBillList_CellDoubleClick);
            // 
            // ChkId
            // 
            this.ChkId.HeaderText = "盘点单主键ID";
            this.ChkId.Name = "ChkId";
            this.ChkId.ReadOnly = true;
            this.ChkId.Visible = false;
            this.ChkId.Width = 88;
            // 
            // colIndex
            // 
            this.colIndex.HeaderText = "序号";
            this.colIndex.Name = "colIndex";
            this.colIndex.ReadOnly = true;
            this.colIndex.Width = 57;
            // 
            // BillNum
            // 
            this.BillNum.HeaderText = "单据编号";
            this.BillNum.Name = "BillNum";
            this.BillNum.ReadOnly = true;
            this.BillNum.Width = 81;
            // 
            // BillDate
            // 
            this.BillDate.DataPropertyName = "order_date";
            this.BillDate.HeaderText = "单据日期";
            this.BillDate.Name = "BillDate";
            this.BillDate.ReadOnly = true;
            this.BillDate.Width = 81;
            // 
            // WHName
            // 
            this.WHName.HeaderText = "仓库";
            this.WHName.Name = "WHName";
            this.WHName.ReadOnly = true;
            this.WHName.Width = 57;
            // 
            // PapCount
            // 
            this.PapCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PapCount.HeaderText = "账面数量";
            this.PapCount.Name = "PapCount";
            this.PapCount.ReadOnly = true;
            // 
            // FirmCount
            // 
            this.FirmCount.HeaderText = "实盘数量";
            this.FirmCount.Name = "FirmCount";
            this.FirmCount.ReadOnly = true;
            this.FirmCount.Width = 81;
            // 
            // ProfitLosCount
            // 
            this.ProfitLosCount.HeaderText = "盈亏数量";
            this.ProfitLosCount.Name = "ProfitLosCount";
            this.ProfitLosCount.ReadOnly = true;
            this.ProfitLosCount.Width = 81;
            // 
            // Calcmoney
            // 
            this.Calcmoney.HeaderText = "金额";
            this.Calcmoney.Name = "Calcmoney";
            this.Calcmoney.ReadOnly = true;
            this.Calcmoney.Width = 57;
            // 
            // DepartName
            // 
            this.DepartName.HeaderText = "部门";
            this.DepartName.Name = "DepartName";
            this.DepartName.ReadOnly = true;
            this.DepartName.Width = 57;
            // 
            // HandlerName
            // 
            this.HandlerName.HeaderText = "经办人";
            this.HandlerName.Name = "HandlerName";
            this.HandlerName.ReadOnly = true;
            this.HandlerName.Width = 69;
            // 
            // OpeName
            // 
            this.OpeName.HeaderText = "操作人";
            this.OpeName.Name = "OpeName";
            this.OpeName.ReadOnly = true;
            this.OpeName.Width = 69;
            // 
            // Remarks
            // 
            this.Remarks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Remarks.HeaderText = "备注";
            this.Remarks.Name = "Remarks";
            this.Remarks.ReadOnly = true;
            // 
            // InOutState
            // 
            this.InOutState.HeaderText = "出入库状态";
            this.InOutState.Name = "InOutState";
            this.InOutState.ReadOnly = true;
            this.InOutState.Width = 93;
            // 
            // panelEx2
            // 
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.Controls.Add(this.winFormCheckQueryPage);
            this.panelEx2.Location = new System.Drawing.Point(3, 349);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1015, 41);
            this.panelEx2.TabIndex = 54;
            // 
            // winFormCheckQueryPage
            // 
            this.winFormCheckQueryPage.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.winFormCheckQueryPage.BackColor = System.Drawing.Color.Transparent;
            this.winFormCheckQueryPage.BtnTextNext = "下页";
            this.winFormCheckQueryPage.BtnTextPrevious = "上页";
            this.winFormCheckQueryPage.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.winFormCheckQueryPage.Location = new System.Drawing.Point(583, 4);
            this.winFormCheckQueryPage.Name = "winFormCheckQueryPage";
            this.winFormCheckQueryPage.PageCount = 0;
            this.winFormCheckQueryPage.PageSize = 15;
            this.winFormCheckQueryPage.RecordCount = 0;
            this.winFormCheckQueryPage.Size = new System.Drawing.Size(431, 32);
            this.winFormCheckQueryPage.TabIndex = 5;
            this.winFormCheckQueryPage.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.winFormCheckQueryPage.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.winFormCheckQueryPage_PageIndexChanged);
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.Controls.Add(this.txtremark);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.dateTimeEnd);
            this.panelEx1.Controls.Add(this.dateTimeStart);
            this.panelEx1.Controls.Add(this.ComBInOutStatus);
            this.panelEx1.Controls.Add(this.label14);
            this.panelEx1.Controls.Add(this.ComBwh_name);
            this.panelEx1.Controls.Add(this.label12);
            this.panelEx1.Controls.Add(this.ComBcom_name);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.ComBhandle_name);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.ComBorg_name);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.label8);
            this.panelEx1.Controls.Add(this.label10);
            this.panelEx1.Location = new System.Drawing.Point(3, 29);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1015, 127);
            this.panelEx1.TabIndex = 53;
            // 
            // txtremark
            // 
            this.txtremark.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtremark.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtremark.BackColor = System.Drawing.Color.Transparent;
            this.txtremark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtremark.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtremark.ForeImage = null;
            this.txtremark.Location = new System.Drawing.Point(755, 78);
            this.txtremark.MaxLengh = 32767;
            this.txtremark.Multiline = false;
            this.txtremark.Name = "txtremark";
            this.txtremark.Radius = 3;
            this.txtremark.ReadOnly = false;
            this.txtremark.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtremark.Size = new System.Drawing.Size(135, 23);
            this.txtremark.TabIndex = 289;
            this.txtremark.UseSystemPasswordChar = false;
            this.txtremark.WaterMark = null;
            this.txtremark.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(711, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 288;
            this.label2.Text = "备注：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Location = new System.Drawing.Point(497, 18);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(141, 21);
            this.dateTimeEnd.TabIndex = 138;
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Location = new System.Drawing.Point(273, 18);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.Size = new System.Drawing.Size(141, 21);
            this.dateTimeStart.TabIndex = 137;
            // 
            // ComBInOutStatus
            // 
            this.ComBInOutStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComBInOutStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComBInOutStatus.FormattingEnabled = true;
            this.ComBInOutStatus.Location = new System.Drawing.Point(755, 19);
            this.ComBInOutStatus.Name = "ComBInOutStatus";
            this.ComBInOutStatus.Size = new System.Drawing.Size(135, 22);
            this.ComBInOutStatus.TabIndex = 134;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(675, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 12);
            this.label14.TabIndex = 133;
            this.label14.Text = "出入库状态：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ComBwh_name
            // 
            this.ComBwh_name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComBwh_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComBwh_name.FormattingEnabled = true;
            this.ComBwh_name.Location = new System.Drawing.Point(70, 18);
            this.ComBwh_name.Name = "ComBwh_name";
            this.ComBwh_name.Size = new System.Drawing.Size(121, 22);
            this.ComBwh_name.TabIndex = 132;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 131;
            this.label12.Text = "仓库：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ComBcom_name
            // 
            this.ComBcom_name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComBcom_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComBcom_name.FormattingEnabled = true;
            this.ComBcom_name.Location = new System.Drawing.Point(70, 79);
            this.ComBcom_name.Name = "ComBcom_name";
            this.ComBcom_name.Size = new System.Drawing.Size(121, 22);
            this.ComBcom_name.TabIndex = 40;
            this.ComBcom_name.SelectedIndexChanged += new System.EventHandler(this.ddlCompany_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 39;
            this.label1.Text = "公司：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ComBhandle_name
            // 
            this.ComBhandle_name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComBhandle_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComBhandle_name.FormattingEnabled = true;
            this.ComBhandle_name.Location = new System.Drawing.Point(497, 79);
            this.ComBhandle_name.Name = "ComBhandle_name";
            this.ComBhandle_name.Size = new System.Drawing.Size(139, 22);
            this.ComBhandle_name.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(926, 66);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(60, 26);
            this.btnSearch.TabIndex = 31;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ComBorg_name
            // 
            this.ComBorg_name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComBorg_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComBorg_name.FormattingEnabled = true;
            this.ComBorg_name.Location = new System.Drawing.Point(273, 79);
            this.ComBorg_name.Name = "ComBorg_name";
            this.ComBorg_name.Size = new System.Drawing.Size(141, 22);
            this.ComBorg_name.TabIndex = 16;
            this.ComBorg_name.SelectedIndexChanged += new System.EventHandler(this.ddlDepartment_SelectedIndexChanged);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.Location = new System.Drawing.Point(926, 27);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 30;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(440, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "经办人：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(214, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "日期从：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(461, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "至：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(226, 83);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "部门：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnExportMenu
            // 
            this.BtnExportMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExcelExport});
            this.BtnExportMenu.Name = "BtnImportMenu";
            this.BtnExportMenu.Size = new System.Drawing.Size(161, 32);
            // 
            // ExcelExport
            // 
            this.ExcelExport.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ExcelExport.Name = "ExcelExport";
            this.ExcelExport.Size = new System.Drawing.Size(100, 26);
            this.ExcelExport.Text = "● Excel导出";
            this.ExcelExport.Click += new System.EventHandler(this.ExcelExport_Click);
            // 
            // UCStockCheckQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gvCheckQueryBillList);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Name = "UCStockCheckQuery";
            this.Size = new System.Drawing.Size(1023, 393);
            this.Load += new System.EventHandler(this.UCRequisitionQuery_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.Controls.SetChildIndex(this.gvCheckQueryBillList, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gvCheckQueryBillList)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.BtnExportMenu.ResumeLayout(false);
            this.BtnExportMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx gvCheckQueryBillList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChkId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn WHName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PapCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirmCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfitLosCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Calcmoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HandlerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn InOutState;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager winFormCheckQueryPage;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.TextBoxEx txtremark;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dateTimeEnd;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dateTimeStart;
        private ServiceStationClient.ComponentUI.ComboBoxEx ComBInOutStatus;
        private System.Windows.Forms.Label label14;
        private ServiceStationClient.ComponentUI.ComboBoxEx ComBwh_name;
        private System.Windows.Forms.Label label12;
        private ServiceStationClient.ComponentUI.ComboBoxEx ComBcom_name;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ComboBoxEx ComBhandle_name;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ComboBoxEx ComBorg_name;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ContextMenuStrip BtnExportMenu;
        private System.Windows.Forms.ToolStripTextBox ExcelExport;
    }
}
