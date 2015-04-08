namespace HXCPcClient.UCForm.SysManage.Organization
{
    partial class UCOrganizationManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCOrganizationManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvorganization = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.org_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.org_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.org_short_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contact_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contact_telephone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnStatus = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.org_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.pnlSearch = new ServiceStationClient.ComponentUI.PanelEx();
            this.dtpcreate_time_end = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpcreate_time = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtcontact_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.txtorg_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.txtorg_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonEx2 = new ServiceStationClient.ComponentUI.ButtonEx();
            this.buttonEx1 = new ServiceStationClient.ComponentUI.ButtonEx();
            this.tvCompany = new ServiceStationClient.ComponentUI.TreeViewEx();
            ((System.ComponentModel.ISupportInitialize)(this.dgvorganization)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1031, 25);
            // 
            // dgvorganization
            // 
            this.dgvorganization.AllowUserToAddRows = false;
            this.dgvorganization.AllowUserToDeleteRows = false;
            this.dgvorganization.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvorganization.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvorganization.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvorganization.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvorganization.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvorganization.BackgroundColor = System.Drawing.Color.White;
            this.dgvorganization.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvorganization.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvorganization.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvorganization.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.org_code,
            this.org_name,
            this.org_short_name,
            this.contact_name,
            this.contact_telephone,
            this.remark,
            this.columnStatus,
            this.org_id});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvorganization.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvorganization.EnableHeadersVisualStyles = false;
            this.dgvorganization.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvorganization.IsCheck = true;
            this.dgvorganization.Location = new System.Drawing.Point(203, 119);
            this.dgvorganization.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvorganization.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvorganization.MergeColumnNames")));
            this.dgvorganization.MultiSelect = false;
            this.dgvorganization.Name = "dgvorganization";
            this.dgvorganization.ReadOnly = true;
            this.dgvorganization.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvorganization.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvorganization.RowHeadersVisible = false;
            this.dgvorganization.RowHeadersWidth = 30;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvorganization.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvorganization.RowTemplate.Height = 23;
            this.dgvorganization.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvorganization.ShowCheckBox = true;
            this.dgvorganization.Size = new System.Drawing.Size(828, 354);
            this.dgvorganization.TabIndex = 6;
            this.dgvorganization.HeadCheckChanged += new ServiceStationClient.ComponentUI.DataGridViewEx.DelegateOnClick(this.dgvorganization_HeadCheckChanged);
            this.dgvorganization.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvorganization_CellBeginEdit);
            this.dgvorganization.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvorganization_CellDoubleClick);
            // 
            // colCheck
            // 
            this.colCheck.DataPropertyName = "org_check";
            this.colCheck.FalseValue = "False";
            this.colCheck.FillWeight = 29.02055F;
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            // 
            // org_code
            // 
            this.org_code.DataPropertyName = "org_code";
            this.org_code.FillWeight = 110.1399F;
            this.org_code.HeaderText = "组织编码";
            this.org_code.Name = "org_code";
            this.org_code.ReadOnly = true;
            // 
            // org_name
            // 
            this.org_name.DataPropertyName = "org_name";
            this.org_name.FillWeight = 110.1399F;
            this.org_name.HeaderText = "组织全称";
            this.org_name.Name = "org_name";
            this.org_name.ReadOnly = true;
            // 
            // org_short_name
            // 
            this.org_short_name.DataPropertyName = "org_short_name";
            this.org_short_name.FillWeight = 110.1399F;
            this.org_short_name.HeaderText = "组织简称";
            this.org_short_name.Name = "org_short_name";
            this.org_short_name.ReadOnly = true;
            // 
            // contact_name
            // 
            this.contact_name.DataPropertyName = "contact_name";
            this.contact_name.FillWeight = 110.1399F;
            this.contact_name.HeaderText = "联系人";
            this.contact_name.Name = "contact_name";
            this.contact_name.ReadOnly = true;
            // 
            // contact_telephone
            // 
            this.contact_telephone.DataPropertyName = "contact_telephone";
            this.contact_telephone.FillWeight = 110.1399F;
            this.contact_telephone.HeaderText = "联系电话";
            this.contact_telephone.Name = "contact_telephone";
            this.contact_telephone.ReadOnly = true;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.FillWeight = 110.1399F;
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            // 
            // columnStatus
            // 
            this.columnStatus.DataPropertyName = "status";
            this.columnStatus.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.columnStatus.FillWeight = 110.1399F;
            this.columnStatus.HeaderText = "状态";
            this.columnStatus.Name = "columnStatus";
            this.columnStatus.ReadOnly = true;
            this.columnStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // org_id
            // 
            this.org_id.DataPropertyName = "org_id";
            this.org_id.HeaderText = "org_id";
            this.org_id.Name = "org_id";
            this.org_id.ReadOnly = true;
            this.org_id.Visible = false;
            // 
            // panelEx2
            // 
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(223)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.page);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(202, 475);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.panelEx2.Size = new System.Drawing.Size(829, 28);
            this.panelEx2.TabIndex = 5;
            // 
            // page
            // 
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.page.Dock = System.Windows.Forms.DockStyle.Right;
            this.page.Location = new System.Drawing.Point(274, 0);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(455, 28);
            this.page.TabIndex = 5;
            this.page.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlSearch.BorderWidth = 1;
            this.pnlSearch.Controls.Add(this.dtpcreate_time_end);
            this.pnlSearch.Controls.Add(this.dtpcreate_time);
            this.pnlSearch.Controls.Add(this.btnQuery);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Controls.Add(this.label8);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.txtcontact_name);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.txtorg_name);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.txtorg_code);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Curvature = 0;
            this.pnlSearch.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlSearch.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlSearch.Location = new System.Drawing.Point(201, 25);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(828, 94);
            this.pnlSearch.TabIndex = 0;
            // 
            // dtpcreate_time_end
            // 
            this.dtpcreate_time_end.Location = new System.Drawing.Point(228, 52);
            this.dtpcreate_time_end.Name = "dtpcreate_time_end";
            this.dtpcreate_time_end.ShowFormat = "yyyy-MM-dd";
            this.dtpcreate_time_end.Size = new System.Drawing.Size(140, 21);
            this.dtpcreate_time_end.TabIndex = 18;
            this.dtpcreate_time_end.Value = new System.DateTime(2014, 9, 24, 0, 0, 0, 0);
            // 
            // dtpcreate_time
            // 
            this.dtpcreate_time.Location = new System.Drawing.Point(81, 52);
            this.dtpcreate_time.Name = "dtpcreate_time";
            this.dtpcreate_time.ShowFormat = "yyyy-MM-dd";
            this.dtpcreate_time.Size = new System.Drawing.Size(121, 21);
            this.dtpcreate_time.TabIndex = 13;
            this.dtpcreate_time.Value = new System.DateTime(2014, 9, 24, 0, 0, 0, 0);
            // 
            // btnQuery
            // 
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(651, 46);
            this.btnQuery.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.MoveImage")));
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.NormalImage")));
            this.btnQuery.Size = new System.Drawing.Size(70, 26);
            this.btnQuery.TabIndex = 17;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(651, 8);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(70, 26);
            this.btnClear.TabIndex = 16;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(208, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "至";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "创建时间：";
            // 
            // txtcontact_name
            // 
            this.txtcontact_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcontact_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcontact_name.BackColor = System.Drawing.Color.Transparent;
            this.txtcontact_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcontact_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcontact_name.ForeImage = null;
            this.txtcontact_name.InputtingVerifyCondition = null;
            this.txtcontact_name.Location = new System.Drawing.Point(487, 11);
            this.txtcontact_name.MaxLengh = 32767;
            this.txtcontact_name.Multiline = false;
            this.txtcontact_name.Name = "txtcontact_name";
            this.txtcontact_name.Radius = 3;
            this.txtcontact_name.ReadOnly = false;
            this.txtcontact_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcontact_name.ShowError = false;
            this.txtcontact_name.Size = new System.Drawing.Size(132, 23);
            this.txtcontact_name.TabIndex = 7;
            this.txtcontact_name.UseSystemPasswordChar = false;
            this.txtcontact_name.Value = "";
            this.txtcontact_name.VerifyCondition = null;
            this.txtcontact_name.VerifyType = null;
            this.txtcontact_name.VerifyTypeName = null;
            this.txtcontact_name.WaterMark = null;
            this.txtcontact_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(436, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "联系人：";
            // 
            // txtorg_name
            // 
            this.txtorg_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtorg_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtorg_name.BackColor = System.Drawing.Color.Transparent;
            this.txtorg_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtorg_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtorg_name.ForeImage = null;
            this.txtorg_name.InputtingVerifyCondition = null;
            this.txtorg_name.Location = new System.Drawing.Point(280, 11);
            this.txtorg_name.MaxLengh = 32767;
            this.txtorg_name.Multiline = false;
            this.txtorg_name.Name = "txtorg_name";
            this.txtorg_name.Radius = 3;
            this.txtorg_name.ReadOnly = false;
            this.txtorg_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtorg_name.ShowError = false;
            this.txtorg_name.Size = new System.Drawing.Size(140, 23);
            this.txtorg_name.TabIndex = 3;
            this.txtorg_name.UseSystemPasswordChar = false;
            this.txtorg_name.Value = "";
            this.txtorg_name.VerifyCondition = null;
            this.txtorg_name.VerifyType = null;
            this.txtorg_name.VerifyTypeName = null;
            this.txtorg_name.WaterMark = null;
            this.txtorg_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "组织名称：";
            // 
            // txtorg_code
            // 
            this.txtorg_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtorg_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtorg_code.BackColor = System.Drawing.Color.Transparent;
            this.txtorg_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtorg_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtorg_code.ForeImage = null;
            this.txtorg_code.InputtingVerifyCondition = null;
            this.txtorg_code.Location = new System.Drawing.Point(81, 11);
            this.txtorg_code.MaxLengh = 32767;
            this.txtorg_code.Multiline = false;
            this.txtorg_code.Name = "txtorg_code";
            this.txtorg_code.Radius = 3;
            this.txtorg_code.ReadOnly = false;
            this.txtorg_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtorg_code.ShowError = false;
            this.txtorg_code.Size = new System.Drawing.Size(120, 23);
            this.txtorg_code.TabIndex = 1;
            this.txtorg_code.UseSystemPasswordChar = false;
            this.txtorg_code.Value = "";
            this.txtorg_code.VerifyCondition = null;
            this.txtorg_code.VerifyType = null;
            this.txtorg_code.VerifyTypeName = null;
            this.txtorg_code.WaterMark = null;
            this.txtorg_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "组织编码：";
            // 
            // buttonEx2
            // 
            this.buttonEx2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonEx2.BackgroundImage")));
            this.buttonEx2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonEx2.Caption = "签约公司";
            this.buttonEx2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonEx2.DownImage = ((System.Drawing.Image)(resources.GetObject("buttonEx2.DownImage")));
            this.buttonEx2.Location = new System.Drawing.Point(100, 26);
            this.buttonEx2.MoveImage = ((System.Drawing.Image)(resources.GetObject("buttonEx2.MoveImage")));
            this.buttonEx2.Name = "buttonEx2";
            this.buttonEx2.NormalImage = ((System.Drawing.Image)(resources.GetObject("buttonEx2.NormalImage")));
            this.buttonEx2.Size = new System.Drawing.Size(100, 26);
            this.buttonEx2.TabIndex = 2;
            this.buttonEx2.Visible = false;
            // 
            // buttonEx1
            // 
            this.buttonEx1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonEx1.BackgroundImage")));
            this.buttonEx1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonEx1.Caption = "本公司";
            this.buttonEx1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonEx1.DownImage = ((System.Drawing.Image)(resources.GetObject("buttonEx1.DownImage")));
            this.buttonEx1.Location = new System.Drawing.Point(0, 26);
            this.buttonEx1.MoveImage = ((System.Drawing.Image)(resources.GetObject("buttonEx1.MoveImage")));
            this.buttonEx1.Name = "buttonEx1";
            this.buttonEx1.NormalImage = ((System.Drawing.Image)(resources.GetObject("buttonEx1.NormalImage")));
            this.buttonEx1.Size = new System.Drawing.Size(100, 26);
            this.buttonEx1.TabIndex = 1;
            this.buttonEx1.Visible = false;
            // 
            // tvCompany
            // 
            this.tvCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tvCompany.IgnoreAutoCheck = false;
            this.tvCompany.Location = new System.Drawing.Point(0, 25);
            this.tvCompany.Name = "tvCompany";
            this.tvCompany.Size = new System.Drawing.Size(200, 475);
            this.tvCompany.TabIndex = 0;
            // 
            // UCOrganizationManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.buttonEx2);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.buttonEx1);
            this.Controls.Add(this.dgvorganization);
            this.Controls.Add(this.tvCompany);
            this.Name = "UCOrganizationManager";
            this.Size = new System.Drawing.Size(1030, 503);
            this.Load += new System.EventHandler(this.UCOrganizationManager_Load);
            this.Controls.SetChildIndex(this.tvCompany, 0);
            this.Controls.SetChildIndex(this.dgvorganization, 0);
            this.Controls.SetChildIndex(this.buttonEx1, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.Controls.SetChildIndex(this.buttonEx2, 0);
            this.Controls.SetChildIndex(this.pnlSearch, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvorganization)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx dgvorganization;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private ServiceStationClient.ComponentUI.PanelEx pnlSearch;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpcreate_time;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcontact_name;
        private ServiceStationClient.ComponentUI.TextBoxEx txtorg_name;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx txtorg_code;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TreeViewEx tvCompany;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpcreate_time_end;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.ButtonEx buttonEx2;
        private ServiceStationClient.ComponentUI.ButtonEx buttonEx1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_short_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn contact_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn contact_telephone;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_id;
    }
}
