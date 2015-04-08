namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.OtherReceiveGoods
{
    partial class UCStockReceiptManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCStockReceiptManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.winFormReceiptPage = new ServiceStationClient.ComponentUI.WinFormPager();
            this.gvReceiptBillList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.dateTimeEnd = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dateTimeStart = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.ComBorder_status_name = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label14 = new System.Windows.Forms.Label();
            this.ComBwh_name = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.ComBin_wh_type_name = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label2 = new System.Windows.Forms.Label();
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
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RecptId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WHName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InWhType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvReceiptBillList)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.BtnExportMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1038, 25);
            // 
            // panelEx2
            // 
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.winFormReceiptPage);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(2, 360);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1033, 41);
            this.panelEx2.TabIndex = 52;
            // 
            // winFormReceiptPage
            // 
            this.winFormReceiptPage.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.winFormReceiptPage.BackColor = System.Drawing.Color.Transparent;
            this.winFormReceiptPage.BtnTextNext = "下页";
            this.winFormReceiptPage.BtnTextPrevious = "上页";
            this.winFormReceiptPage.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.winFormReceiptPage.Location = new System.Drawing.Point(602, 4);
            this.winFormReceiptPage.Name = "winFormReceiptPage";
            this.winFormReceiptPage.PageCount = 0;
            this.winFormReceiptPage.PageSize = 15;
            this.winFormReceiptPage.RecordCount = 0;
            this.winFormReceiptPage.Size = new System.Drawing.Size(427, 32);
            this.winFormReceiptPage.TabIndex = 5;
            this.winFormReceiptPage.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.winFormReceiptPage.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.winFormReceiptPage_PageIndexChanged);
            // 
            // gvReceiptBillList
            // 
            this.gvReceiptBillList.AllowUserToAddRows = false;
            this.gvReceiptBillList.AllowUserToDeleteRows = false;
            this.gvReceiptBillList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gvReceiptBillList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvReceiptBillList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvReceiptBillList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvReceiptBillList.BackgroundColor = System.Drawing.Color.White;
            this.gvReceiptBillList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvReceiptBillList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvReceiptBillList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvReceiptBillList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.RecptId,
            this.BillNum,
            this.BillDate,
            this.WHName,
            this.InWhType,
            this.TotalCount,
            this.TotalMoney,
            this.DepartName,
            this.OpeName,
            this.Remarks,
            this.OrderState});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvReceiptBillList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvReceiptBillList.EnableHeadersVisualStyles = false;
            this.gvReceiptBillList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.gvReceiptBillList.Location = new System.Drawing.Point(1, 160);
            this.gvReceiptBillList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gvReceiptBillList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gvReceiptBillList.MergeColumnNames")));
            this.gvReceiptBillList.MultiSelect = false;
            this.gvReceiptBillList.Name = "gvReceiptBillList";
            this.gvReceiptBillList.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvReceiptBillList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvReceiptBillList.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.gvReceiptBillList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvReceiptBillList.RowTemplate.Height = 23;
            this.gvReceiptBillList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvReceiptBillList.ShowCheckBox = true;
            this.gvReceiptBillList.Size = new System.Drawing.Size(1033, 198);
            this.gvReceiptBillList.TabIndex = 51;
            this.gvReceiptBillList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvReceiptBillList_CellDoubleClick);
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.dateTimeEnd);
            this.panelEx1.Controls.Add(this.dateTimeStart);
            this.panelEx1.Controls.Add(this.ComBorder_status_name);
            this.panelEx1.Controls.Add(this.label14);
            this.panelEx1.Controls.Add(this.ComBwh_name);
            this.panelEx1.Controls.Add(this.label12);
            this.panelEx1.Controls.Add(this.ComBin_wh_type_name);
            this.panelEx1.Controls.Add(this.label2);
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
            this.panelEx1.Location = new System.Drawing.Point(2, 31);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1033, 127);
            this.panelEx1.TabIndex = 50;
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Location = new System.Drawing.Point(520, 18);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.ShowFormat = "yyyy-MM-dd";
            this.dateTimeEnd.Size = new System.Drawing.Size(141, 21);
            this.dateTimeEnd.TabIndex = 138;
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Location = new System.Drawing.Point(276, 18);
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
            this.ComBorder_status_name.Location = new System.Drawing.Point(778, 78);
            this.ComBorder_status_name.Name = "ComBorder_status_name";
            this.ComBorder_status_name.Size = new System.Drawing.Size(135, 22);
            this.ComBorder_status_name.TabIndex = 134;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(707, 82);
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
            this.ComBwh_name.Location = new System.Drawing.Point(64, 18);
            this.ComBwh_name.Name = "ComBwh_name";
            this.ComBwh_name.Size = new System.Drawing.Size(121, 22);
            this.ComBwh_name.TabIndex = 132;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 131;
            this.label12.Text = "仓库：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ComBin_wh_type_name
            // 
            this.ComBin_wh_type_name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComBin_wh_type_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComBin_wh_type_name.FormattingEnabled = true;
            this.ComBin_wh_type_name.Location = new System.Drawing.Point(778, 18);
            this.ComBin_wh_type_name.Name = "ComBin_wh_type_name";
            this.ComBin_wh_type_name.Size = new System.Drawing.Size(135, 22);
            this.ComBin_wh_type_name.TabIndex = 124;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(707, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 123;
            this.label2.Text = "入库类型：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ComBcom_name
            // 
            this.ComBcom_name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComBcom_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComBcom_name.FormattingEnabled = true;
            this.ComBcom_name.Location = new System.Drawing.Point(64, 79);
            this.ComBcom_name.Name = "ComBcom_name";
            this.ComBcom_name.Size = new System.Drawing.Size(121, 22);
            this.ComBcom_name.TabIndex = 40;
            this.ComBcom_name.SelectedIndexChanged += new System.EventHandler(this.ddlCompany_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 82);
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
            this.ComBhandle_name.Location = new System.Drawing.Point(520, 79);
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
            this.btnSearch.Location = new System.Drawing.Point(941, 67);
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
            this.ComBorg_name.Location = new System.Drawing.Point(276, 79);
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
            this.btnClear.Location = new System.Drawing.Point(941, 25);
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
            this.label3.Location = new System.Drawing.Point(463, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "经办人：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(217, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "日期从：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(484, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "至：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(229, 83);
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
            // colCheck
            // 
            this.colCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colCheck.HeaderText = "";
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colCheck.Width = 40;
            // 
            // RecptId
            // 
            this.RecptId.HeaderText = "其它收货单主键ID";
            this.RecptId.Name = "RecptId";
            this.RecptId.ReadOnly = true;
            this.RecptId.Visible = false;
            this.RecptId.Width = 112;
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
            this.WHName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.WHName.HeaderText = "仓库";
            this.WHName.Name = "WHName";
            this.WHName.ReadOnly = true;
            // 
            // InWhType
            // 
            this.InWhType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.InWhType.HeaderText = "入库类型";
            this.InWhType.Name = "InWhType";
            this.InWhType.ReadOnly = true;
            // 
            // TotalCount
            // 
            this.TotalCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TotalCount.HeaderText = "业务数量";
            this.TotalCount.Name = "TotalCount";
            this.TotalCount.ReadOnly = true;
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
            // UCStockReceiptManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gvReceiptBillList);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Name = "UCStockReceiptManager";
            this.Size = new System.Drawing.Size(1038, 404);
            this.Load += new System.EventHandler(this.UCStockReceiptManager_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.Controls.SetChildIndex(this.gvReceiptBillList, 0);
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvReceiptBillList)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.BtnExportMenu.ResumeLayout(false);
            this.BtnExportMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager winFormReceiptPage;
        private ServiceStationClient.ComponentUI.DataGridViewEx gvReceiptBillList;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dateTimeEnd;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dateTimeStart;
        private ServiceStationClient.ComponentUI.ComboBoxEx ComBorder_status_name;
        private System.Windows.Forms.Label label14;
        private ServiceStationClient.ComponentUI.ComboBoxEx ComBwh_name;
        private System.Windows.Forms.Label label12;
        private ServiceStationClient.ComponentUI.ComboBoxEx ComBin_wh_type_name;
        private System.Windows.Forms.Label label2;
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
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecptId;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn WHName;
        private System.Windows.Forms.DataGridViewTextBoxColumn InWhType;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderState;

    }
}
