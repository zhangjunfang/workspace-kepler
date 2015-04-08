namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.InventoryBill
{
    partial class UCStockCheckManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCStockCheckManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gvCheckBillList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ChkId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WHName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PapCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirmCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfitLosCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HandlerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.winFormCheckPage = new ServiceStationClient.ComponentUI.WinFormPager();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.txtremark = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimeEnd = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dateTimeStart = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.ComBorder_status_name = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
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
            ((System.ComponentModel.ISupportInitialize)(this.gvCheckBillList)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1018, 25);
            // 
            // gvCheckBillList
            // 
            this.gvCheckBillList.AllowUserToAddRows = false;
            this.gvCheckBillList.AllowUserToDeleteRows = false;
            this.gvCheckBillList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gvCheckBillList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvCheckBillList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvCheckBillList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvCheckBillList.BackgroundColor = System.Drawing.Color.White;
            this.gvCheckBillList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCheckBillList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvCheckBillList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCheckBillList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.ChkId,
            this.BillNum,
            this.BillDate,
            this.WHName,
            this.PapCount,
            this.FirmCount,
            this.ProfitLosCount,
            this.TotalMoney,
            this.DepartName,
            this.HandlerName,
            this.OpeName,
            this.Remarks,
            this.OrderState});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvCheckBillList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvCheckBillList.EnableHeadersVisualStyles = false;
            this.gvCheckBillList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.gvCheckBillList.IsCheck = true;
            this.gvCheckBillList.Location = new System.Drawing.Point(3, 158);
            this.gvCheckBillList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gvCheckBillList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gvCheckBillList.MergeColumnNames")));
            this.gvCheckBillList.MultiSelect = false;
            this.gvCheckBillList.Name = "gvCheckBillList";
            this.gvCheckBillList.ReadOnly = true;
            this.gvCheckBillList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.gvCheckBillList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvCheckBillList.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.gvCheckBillList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvCheckBillList.RowTemplate.Height = 23;
            this.gvCheckBillList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvCheckBillList.ShowCheckBox = true;
            this.gvCheckBillList.Size = new System.Drawing.Size(1010, 247);
            this.gvCheckBillList.TabIndex = 52;
            this.gvCheckBillList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCheckBillList_CellDoubleClick);
            // 
            // colCheck
            // 
            this.colCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colCheck.Width = 30;
            // 
            // ChkId
            // 
            this.ChkId.HeaderText = "盘点单主键ID";
            this.ChkId.Name = "ChkId";
            this.ChkId.ReadOnly = true;
            this.ChkId.Visible = false;
            this.ChkId.Width = 88;
            // 
            // BillNum
            // 
            this.BillNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BillNum.HeaderText = "单据编号";
            this.BillNum.Name = "BillNum";
            this.BillNum.ReadOnly = true;
            // 
            // BillDate
            // 
            this.BillDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BillDate.DataPropertyName = "order_date";
            this.BillDate.HeaderText = "单据日期";
            this.BillDate.Name = "BillDate";
            this.BillDate.ReadOnly = true;
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
            this.FirmCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FirmCount.HeaderText = "实盘数量";
            this.FirmCount.Name = "FirmCount";
            this.FirmCount.ReadOnly = true;
            // 
            // ProfitLosCount
            // 
            this.ProfitLosCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProfitLosCount.HeaderText = "盈亏数量";
            this.ProfitLosCount.Name = "ProfitLosCount";
            this.ProfitLosCount.ReadOnly = true;
            // 
            // TotalMoney
            // 
            this.TotalMoney.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TotalMoney.HeaderText = "金额";
            this.TotalMoney.Name = "TotalMoney";
            this.TotalMoney.ReadOnly = true;
            // 
            // DepartName
            // 
            this.DepartName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DepartName.HeaderText = "部门";
            this.DepartName.Name = "DepartName";
            this.DepartName.ReadOnly = true;
            // 
            // HandlerName
            // 
            this.HandlerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.HandlerName.HeaderText = "经办人";
            this.HandlerName.Name = "HandlerName";
            this.HandlerName.ReadOnly = true;
            // 
            // OpeName
            // 
            this.OpeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.OpeName.HeaderText = "操作人";
            this.OpeName.Name = "OpeName";
            this.OpeName.ReadOnly = true;
            // 
            // Remarks
            // 
            this.Remarks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Remarks.HeaderText = "备注";
            this.Remarks.MinimumWidth = 100;
            this.Remarks.Name = "Remarks";
            this.Remarks.ReadOnly = true;
            this.Remarks.Width = 200;
            // 
            // OrderState
            // 
            this.OrderState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.OrderState.HeaderText = "单据状态";
            this.OrderState.Name = "OrderState";
            this.OrderState.ReadOnly = true;
            // 
            // panelEx2
            // 
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.winFormCheckPage);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(3, 407);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1010, 41);
            this.panelEx2.TabIndex = 51;
            // 
            // winFormCheckPage
            // 
            this.winFormCheckPage.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.winFormCheckPage.BackColor = System.Drawing.Color.Transparent;
            this.winFormCheckPage.BtnTextNext = "下页";
            this.winFormCheckPage.BtnTextPrevious = "上页";
            this.winFormCheckPage.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.winFormCheckPage.Location = new System.Drawing.Point(579, 4);
            this.winFormCheckPage.Name = "winFormCheckPage";
            this.winFormCheckPage.PageCount = 0;
            this.winFormCheckPage.PageSize = 15;
            this.winFormCheckPage.RecordCount = 0;
            this.winFormCheckPage.Size = new System.Drawing.Size(427, 32);
            this.winFormCheckPage.TabIndex = 5;
            this.winFormCheckPage.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.winFormCheckPage.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.winFormCheckPage_PageIndexChanged);
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.txtremark);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.dateTimeEnd);
            this.panelEx1.Controls.Add(this.dateTimeStart);
            this.panelEx1.Controls.Add(this.ComBorder_status_name);
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
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(3, 30);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1010, 127);
            this.panelEx1.TabIndex = 50;
            // 
            // txtremark
            // 
            this.txtremark.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtremark.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtremark.BackColor = System.Drawing.Color.Transparent;
            this.txtremark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtremark.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtremark.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtremark.ForeImage = null;
            this.txtremark.InputtingVerifyCondition = null;
            this.txtremark.Location = new System.Drawing.Point(751, 78);
            this.txtremark.MaxLengh = 32767;
            this.txtremark.Multiline = false;
            this.txtremark.Name = "txtremark";
            this.txtremark.Radius = 3;
            this.txtremark.ReadOnly = false;
            this.txtremark.SelectStart = 0;
            this.txtremark.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtremark.ShowError = false;
            this.txtremark.Size = new System.Drawing.Size(135, 23);
            this.txtremark.TabIndex = 289;
            this.txtremark.UseSystemPasswordChar = false;
            this.txtremark.Value = "";
            this.txtremark.VerifyCondition = null;
            this.txtremark.VerifyType = null;
            this.txtremark.VerifyTypeName = null;
            this.txtremark.WaterMark = null;
            this.txtremark.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(707, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 288;
            this.label2.Text = "备注：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Location = new System.Drawing.Point(502, 18);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.ShowFormat = "yyyy-MM-dd";
            this.dateTimeEnd.Size = new System.Drawing.Size(141, 21);
            this.dateTimeEnd.TabIndex = 138;
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Location = new System.Drawing.Point(280, 18);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.ShowFormat = "yyyy-MM-dd";
            this.dateTimeStart.Size = new System.Drawing.Size(141, 21);
            this.dateTimeStart.TabIndex = 137;
            // 
            // ComBorder_status_name
            // 
            this.ComBorder_status_name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComBorder_status_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComBorder_status_name.FormattingEnabled = true;
            this.ComBorder_status_name.Location = new System.Drawing.Point(751, 19);
            this.ComBorder_status_name.Name = "ComBorder_status_name";
            this.ComBorder_status_name.Size = new System.Drawing.Size(135, 22);
            this.ComBorder_status_name.TabIndex = 134;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(683, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 133;
            this.label14.Text = "单据状态：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ComBwh_name
            // 
            this.ComBwh_name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComBwh_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComBwh_name.FormattingEnabled = true;
            this.ComBwh_name.Location = new System.Drawing.Point(66, 18);
            this.ComBwh_name.Name = "ComBwh_name";
            this.ComBwh_name.Size = new System.Drawing.Size(121, 22);
            this.ComBwh_name.TabIndex = 132;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(19, 24);
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
            this.ComBcom_name.Location = new System.Drawing.Point(66, 79);
            this.ComBcom_name.Name = "ComBcom_name";
            this.ComBcom_name.Size = new System.Drawing.Size(121, 22);
            this.ComBcom_name.TabIndex = 40;
            this.ComBcom_name.SelectedIndexChanged += new System.EventHandler(this.ddlCompany_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 82);
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
            this.ComBhandle_name.Location = new System.Drawing.Point(502, 79);
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
            this.btnSearch.Location = new System.Drawing.Point(917, 69);
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
            this.ComBorg_name.Location = new System.Drawing.Point(280, 79);
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
            this.btnClear.Location = new System.Drawing.Point(917, 24);
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
            this.label3.Location = new System.Drawing.Point(445, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "经办人：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(221, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "日期从：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(466, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "至：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(233, 83);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "部门：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UCStockCheckManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gvCheckBillList);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Name = "UCStockCheckManager";
            this.Size = new System.Drawing.Size(1018, 451);
            this.Load += new System.EventHandler(this.UCStockCheckManager_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.Controls.SetChildIndex(this.gvCheckBillList, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gvCheckBillList)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx gvCheckBillList;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager winFormCheckPage;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.TextBoxEx txtremark;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dateTimeEnd;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dateTimeStart;
        private ServiceStationClient.ComponentUI.ComboBoxEx ComBorder_status_name;
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
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChkId;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn WHName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PapCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirmCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfitLosCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HandlerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderState;
    }
}
