namespace HXCPcClient.Chooser
{
    partial class frmWorkHours
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWorkHours));
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.txtProName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtProNo = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.tcWorkHours = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tpUsers = new System.Windows.Forms.TabPage();
            this.gvWorkList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.whours_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_whours_change = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repair_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.project_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.project_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whours_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whours_num_a = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whours_num_b = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whours_num_c = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quota_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.winFormPager1 = new ServiceStationClient.ComponentUI.WinFormPager();
            this.btnClose = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnSubmit = new ServiceStationClient.ComponentUI.ButtonEx();
            this.pnlContainer.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.tcWorkHours.SuspendLayout();
            this.tpUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvWorkList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlContainer.Controls.Add(this.btnClose);
            this.pnlContainer.Controls.Add(this.btnSubmit);
            this.pnlContainer.Controls.Add(this.winFormPager1);
            this.pnlContainer.Controls.Add(this.tcWorkHours);
            this.pnlContainer.Controls.Add(this.panelEx1);
            this.pnlContainer.Size = new System.Drawing.Size(718, 429);
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.txtProName);
            this.panelEx1.Controls.Add(this.txtProNo);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(1, 2);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(714, 48);
            this.panelEx1.TabIndex = 6;
            // 
            // txtProName
            // 
            this.txtProName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtProName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtProName.BackColor = System.Drawing.Color.Transparent;
            this.txtProName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtProName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtProName.ForeImage = null;
            this.txtProName.Location = new System.Drawing.Point(318, 12);
            this.txtProName.MaxLengh = 32767;
            this.txtProName.Multiline = false;
            this.txtProName.Name = "txtProName";
            this.txtProName.Radius = 3;
            this.txtProName.ReadOnly = false;
            this.txtProName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtProName.ShowError = false;
            this.txtProName.Size = new System.Drawing.Size(114, 23);
            this.txtProName.TabIndex = 23;
            this.txtProName.UseSystemPasswordChar = false;
            this.txtProName.Value = "";
            this.txtProName.VerifyCondition = null;
            this.txtProName.VerifyType = null;
            this.txtProName.WaterMark = null;
            this.txtProName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtProNo
            // 
            this.txtProNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtProNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtProNo.BackColor = System.Drawing.Color.Transparent;
            this.txtProNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtProNo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtProNo.ForeImage = null;
            this.txtProNo.Location = new System.Drawing.Point(102, 12);
            this.txtProNo.MaxLengh = 32767;
            this.txtProNo.Multiline = false;
            this.txtProNo.Name = "txtProNo";
            this.txtProNo.Radius = 3;
            this.txtProNo.ReadOnly = false;
            this.txtProNo.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtProNo.ShowError = false;
            this.txtProNo.Size = new System.Drawing.Size(114, 23);
            this.txtProNo.TabIndex = 22;
            this.txtProNo.UseSystemPasswordChar = false;
            this.txtProNo.Value = "";
            this.txtProNo.VerifyCondition = null;
            this.txtProNo.VerifyType = null;
            this.txtProNo.WaterMark = null;
            this.txtProNo.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(227, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "维修项目名称：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "维修项目编号：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(467, 9);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(60, 26);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(577, 9);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 8;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tcWorkHours
            // 
            this.tcWorkHours.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tcWorkHours.Controls.Add(this.tpUsers);
            this.tcWorkHours.Location = new System.Drawing.Point(3, 51);
            this.tcWorkHours.Name = "tcWorkHours";
            this.tcWorkHours.SelectedIndex = 0;
            this.tcWorkHours.Size = new System.Drawing.Size(712, 290);
            this.tcWorkHours.TabIndex = 7;
            // 
            // tpUsers
            // 
            this.tpUsers.Controls.Add(this.gvWorkList);
            this.tpUsers.Location = new System.Drawing.Point(4, 26);
            this.tpUsers.Name = "tpUsers";
            this.tpUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tpUsers.Size = new System.Drawing.Size(704, 260);
            this.tpUsers.TabIndex = 0;
            this.tpUsers.Text = "工时档案列表";
            this.tpUsers.UseVisualStyleBackColor = true;
            // 
            // gvWorkList
            // 
            this.gvWorkList.AllowUserToAddRows = false;
            this.gvWorkList.AllowUserToDeleteRows = false;
            this.gvWorkList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gvWorkList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvWorkList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvWorkList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvWorkList.BackgroundColor = System.Drawing.Color.White;
            this.gvWorkList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvWorkList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvWorkList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvWorkList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.whours_id,
            this.drtxt_whours_change,
            this.repair_type,
            this.project_num,
            this.project_name,
            this.whours_type,
            this.whours_num_a,
            this.whours_num_b,
            this.whours_num_c,
            this.quota_price,
            this.create_username,
            this.create_time,
            this.remark});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvWorkList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvWorkList.EnableHeadersVisualStyles = false;
            this.gvWorkList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.gvWorkList.Location = new System.Drawing.Point(0, 2);
            this.gvWorkList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gvWorkList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gvWorkList.MergeColumnNames")));
            this.gvWorkList.MultiSelect = false;
            this.gvWorkList.Name = "gvWorkList";
            this.gvWorkList.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvWorkList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvWorkList.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.gvWorkList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvWorkList.RowTemplate.Height = 23;
            this.gvWorkList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvWorkList.ShowCheckBox = true;
            this.gvWorkList.Size = new System.Drawing.Size(701, 252);
            this.gvWorkList.TabIndex = 1;
            this.gvWorkList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvWorkList_CellDoubleClick);
            this.gvWorkList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvWorkList_CellFormatting);
            this.gvWorkList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvWorkList_CellMouseClick);
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
            // whours_id
            // 
            this.whours_id.DataPropertyName = "whours_id";
            this.whours_id.HeaderText = "whours_id";
            this.whours_id.Name = "whours_id";
            this.whours_id.ReadOnly = true;
            this.whours_id.Visible = false;
            this.whours_id.Width = 76;
            // 
            // drtxt_whours_change
            // 
            this.drtxt_whours_change.DataPropertyName = "whours_change";
            this.drtxt_whours_change.HeaderText = "工时调整";
            this.drtxt_whours_change.Name = "drtxt_whours_change";
            this.drtxt_whours_change.ReadOnly = true;
            this.drtxt_whours_change.Width = 81;
            // 
            // repair_type
            // 
            this.repair_type.DataPropertyName = "repair_type";
            this.repair_type.HeaderText = "维修项目类别";
            this.repair_type.Name = "repair_type";
            this.repair_type.ReadOnly = true;
            this.repair_type.Width = 105;
            // 
            // project_num
            // 
            this.project_num.DataPropertyName = "project_num";
            this.project_num.HeaderText = "维修项目编号";
            this.project_num.Name = "project_num";
            this.project_num.ReadOnly = true;
            this.project_num.Width = 105;
            // 
            // project_name
            // 
            this.project_name.DataPropertyName = "project_name";
            this.project_name.HeaderText = "维修项目名称";
            this.project_name.Name = "project_name";
            this.project_name.ReadOnly = true;
            this.project_name.Width = 105;
            // 
            // whours_type
            // 
            this.whours_type.DataPropertyName = "whours_type";
            this.whours_type.HeaderText = "工时类型";
            this.whours_type.Name = "whours_type";
            this.whours_type.ReadOnly = true;
            this.whours_type.Width = 81;
            // 
            // whours_num_a
            // 
            this.whours_num_a.DataPropertyName = "whours_num_a";
            this.whours_num_a.HeaderText = "A类工时数";
            this.whours_num_a.Name = "whours_num_a";
            this.whours_num_a.ReadOnly = true;
            this.whours_num_a.Width = 90;
            // 
            // whours_num_b
            // 
            this.whours_num_b.DataPropertyName = "whours_num_b";
            this.whours_num_b.HeaderText = "B类工时数";
            this.whours_num_b.Name = "whours_num_b";
            this.whours_num_b.ReadOnly = true;
            this.whours_num_b.Width = 89;
            // 
            // whours_num_c
            // 
            this.whours_num_c.DataPropertyName = "whours_num_c";
            this.whours_num_c.HeaderText = "C类工时数";
            this.whours_num_c.Name = "whours_num_c";
            this.whours_num_c.ReadOnly = true;
            this.whours_num_c.Width = 89;
            // 
            // quota_price
            // 
            this.quota_price.DataPropertyName = "quota_price";
            this.quota_price.HeaderText = "定额单价(元)";
            this.quota_price.Name = "quota_price";
            this.quota_price.ReadOnly = true;
            this.quota_price.Width = 103;
            // 
            // create_username
            // 
            this.create_username.DataPropertyName = "create_username";
            this.create_username.HeaderText = "创建人";
            this.create_username.Name = "create_username";
            this.create_username.ReadOnly = true;
            this.create_username.Width = 69;
            // 
            // create_time
            // 
            this.create_time.DataPropertyName = "create_time";
            this.create_time.HeaderText = "创建时间";
            this.create_time.Name = "create_time";
            this.create_time.ReadOnly = true;
            this.create_time.Width = 81;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "project_remark";
            this.remark.HeaderText = "备注(项目说明)";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            this.remark.Width = 115;
            // 
            // winFormPager1
            // 
            this.winFormPager1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.winFormPager1.BackColor = System.Drawing.Color.Transparent;
            this.winFormPager1.BtnTextNext = "下页";
            this.winFormPager1.BtnTextPrevious = "上页";
            this.winFormPager1.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.winFormPager1.Location = new System.Drawing.Point(188, 347);
            this.winFormPager1.Name = "winFormPager1";
            this.winFormPager1.PageCount = 0;
            this.winFormPager1.PageSize = 15;
            this.winFormPager1.RecordCount = 0;
            this.winFormPager1.Size = new System.Drawing.Size(450, 32);
            this.winFormPager1.TabIndex = 8;
            this.winFormPager1.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.winFormPager1.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.winFormPager1_PageIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Caption = "关闭";
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClose.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClose.DownImage")));
            this.btnClose.Location = new System.Drawing.Point(647, 392);
            this.btnClose.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClose.MoveImage")));
            this.btnClose.Name = "btnClose";
            this.btnClose.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClose.NormalImage")));
            this.btnClose.Size = new System.Drawing.Size(60, 26);
            this.btnClose.TabIndex = 41;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.BackgroundImage")));
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Caption = "确定";
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSubmit.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.DownImage")));
            this.btnSubmit.Location = new System.Drawing.Point(553, 392);
            this.btnSubmit.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.MoveImage")));
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.NormalImage")));
            this.btnSubmit.Size = new System.Drawing.Size(60, 26);
            this.btnSubmit.TabIndex = 40;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // frmWorkHours
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 460);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmWorkHours";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "工时档案";
            this.pnlContainer.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.tcWorkHours.ResumeLayout(false);
            this.tpUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvWorkList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.TextBoxEx txtProName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtProNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TabControlEx tcWorkHours;
        private System.Windows.Forms.TabPage tpUsers;
        private ServiceStationClient.ComponentUI.DataGridViewEx gvWorkList;
        private ServiceStationClient.ComponentUI.WinFormPager winFormPager1;
        private ServiceStationClient.ComponentUI.ButtonEx btnClose;
        private ServiceStationClient.ComponentUI.ButtonEx btnSubmit;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn whours_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_whours_change;
        private System.Windows.Forms.DataGridViewTextBoxColumn repair_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn project_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn project_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn whours_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn whours_num_a;
        private System.Windows.Forms.DataGridViewTextBoxColumn whours_num_b;
        private System.Windows.Forms.DataGridViewTextBoxColumn whours_num_c;
        private System.Windows.Forms.DataGridViewTextBoxColumn quota_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_username;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
    }
}