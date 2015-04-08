namespace HXCPcClient.UCForm.DataManage.WorkingTimeFile
{
    partial class UCWorkingTimeManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCWorkingTimeManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gvWorkList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.whours_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.data_source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.project_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.project_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whours_num_a = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whours_num_b = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whours_num_c = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quota_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.radIsQuota = new System.Windows.Forms.RadioButton();
            this.radIsWorkTime = new System.Windows.Forms.RadioButton();
            this.dateTimeEnd = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dateTimeStart = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.txtQuotaPrice = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtWorkTimeC = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtWorkTimeB = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtWorkTimeA = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtProName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtProNo = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.ddlState = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.ddlDataSource = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.winFormPager1 = new ServiceStationClient.ComponentUI.WinFormPager();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvWorkList)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1030, 52);
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Location = new System.Drawing.Point(19, 200);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(994, 285);
            this.tabControlEx1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gvWorkList);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(986, 255);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "项目列表";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.whours_id,
            this.colCheck,
            this.data_source,
            this.project_num,
            this.project_name,
            this.whours_num_a,
            this.whours_num_b,
            this.whours_num_c,
            this.quota_price,
            this.create_username,
            this.create_time,
            this.status,
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
            this.gvWorkList.Location = new System.Drawing.Point(4, 4);
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
            this.gvWorkList.Size = new System.Drawing.Size(977, 248);
            this.gvWorkList.TabIndex = 0;
            this.gvWorkList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvWorkList_CellContentClick);
            this.gvWorkList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProList_CellDoubleClick);
            this.gvWorkList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvWorkList_CellFormatting);
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
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 5;
            // 
            // data_source
            // 
            this.data_source.DataPropertyName = "data_source";
            this.data_source.HeaderText = "数据来源";
            this.data_source.Name = "data_source";
            this.data_source.ReadOnly = true;
            this.data_source.Width = 81;
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
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 57;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "project_remark";
            this.remark.HeaderText = "备注(项目说明)";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            this.remark.Width = 115;
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.radIsQuota);
            this.panelEx1.Controls.Add(this.radIsWorkTime);
            this.panelEx1.Controls.Add(this.dateTimeEnd);
            this.panelEx1.Controls.Add(this.dateTimeStart);
            this.panelEx1.Controls.Add(this.txtQuotaPrice);
            this.panelEx1.Controls.Add(this.txtWorkTimeC);
            this.panelEx1.Controls.Add(this.txtWorkTimeB);
            this.panelEx1.Controls.Add(this.txtWorkTimeA);
            this.panelEx1.Controls.Add(this.txtProName);
            this.panelEx1.Controls.Add(this.txtProNo);
            this.panelEx1.Controls.Add(this.ddlState);
            this.panelEx1.Controls.Add(this.ddlDataSource);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.label12);
            this.panelEx1.Controls.Add(this.label11);
            this.panelEx1.Controls.Add(this.label10);
            this.panelEx1.Controls.Add(this.label8);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(19, 65);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(994, 128);
            this.panelEx1.TabIndex = 2;
            // 
            // radIsQuota
            // 
            this.radIsQuota.AutoSize = true;
            this.radIsQuota.Location = new System.Drawing.Point(69, 85);
            this.radIsQuota.Name = "radIsQuota";
            this.radIsQuota.Size = new System.Drawing.Size(47, 16);
            this.radIsQuota.TabIndex = 62;
            this.radIsQuota.TabStop = true;
            this.radIsQuota.Text = "定额";
            this.radIsQuota.UseVisualStyleBackColor = true;
            this.radIsQuota.CheckedChanged += new System.EventHandler(this.radIsQuota_CheckedChanged);
            // 
            // radIsWorkTime
            // 
            this.radIsWorkTime.AutoSize = true;
            this.radIsWorkTime.Location = new System.Drawing.Point(69, 53);
            this.radIsWorkTime.Name = "radIsWorkTime";
            this.radIsWorkTime.Size = new System.Drawing.Size(47, 16);
            this.radIsWorkTime.TabIndex = 61;
            this.radIsWorkTime.TabStop = true;
            this.radIsWorkTime.Text = "工时";
            this.radIsWorkTime.UseVisualStyleBackColor = true;
            this.radIsWorkTime.CheckedChanged += new System.EventHandler(this.radIsWorkTime_CheckedChanged);
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Location = new System.Drawing.Point(719, 86);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.ShowFormat = "yyyy年MM月dd日";
            this.dateTimeEnd.Size = new System.Drawing.Size(169, 21);
            this.dateTimeEnd.TabIndex = 25;
            this.dateTimeEnd.Value = new System.DateTime(2014, 9, 16, 15, 23, 2, 109);
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Location = new System.Drawing.Point(523, 83);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.ShowFormat = "yyyy年MM月dd日";
            this.dateTimeStart.Size = new System.Drawing.Size(164, 21);
            this.dateTimeStart.TabIndex = 24;
            this.dateTimeStart.Value = new System.DateTime(2014, 9, 16, 15, 23, 2, 109);
            // 
            // txtQuotaPrice
            // 
            this.txtQuotaPrice.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtQuotaPrice.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtQuotaPrice.BackColor = System.Drawing.Color.Transparent;
            this.txtQuotaPrice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtQuotaPrice.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtQuotaPrice.Enabled = false;
            this.txtQuotaPrice.ForeImage = null;
            this.txtQuotaPrice.Location = new System.Drawing.Point(290, 81);
            this.txtQuotaPrice.MaxLengh = 32767;
            this.txtQuotaPrice.Multiline = false;
            this.txtQuotaPrice.Name = "txtQuotaPrice";
            this.txtQuotaPrice.Radius = 3;
            this.txtQuotaPrice.ReadOnly = false;
            this.txtQuotaPrice.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtQuotaPrice.ShowError = false;
            this.txtQuotaPrice.Size = new System.Drawing.Size(137, 23);
            this.txtQuotaPrice.TabIndex = 23;
            this.txtQuotaPrice.UseSystemPasswordChar = false;
            this.txtQuotaPrice.Value = "";
            this.txtQuotaPrice.VerifyCondition = null;
            this.txtQuotaPrice.VerifyType = null;
            this.txtQuotaPrice.WaterMark = null;
            this.txtQuotaPrice.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtWorkTimeC
            // 
            this.txtWorkTimeC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtWorkTimeC.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtWorkTimeC.BackColor = System.Drawing.Color.Transparent;
            this.txtWorkTimeC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtWorkTimeC.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtWorkTimeC.Enabled = false;
            this.txtWorkTimeC.ForeImage = null;
            this.txtWorkTimeC.Location = new System.Drawing.Point(769, 50);
            this.txtWorkTimeC.MaxLengh = 32767;
            this.txtWorkTimeC.Multiline = false;
            this.txtWorkTimeC.Name = "txtWorkTimeC";
            this.txtWorkTimeC.Radius = 3;
            this.txtWorkTimeC.ReadOnly = false;
            this.txtWorkTimeC.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtWorkTimeC.ShowError = false;
            this.txtWorkTimeC.Size = new System.Drawing.Size(121, 23);
            this.txtWorkTimeC.TabIndex = 22;
            this.txtWorkTimeC.UseSystemPasswordChar = false;
            this.txtWorkTimeC.Value = "";
            this.txtWorkTimeC.VerifyCondition = null;
            this.txtWorkTimeC.VerifyType = null;
            this.txtWorkTimeC.WaterMark = null;
            this.txtWorkTimeC.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtWorkTimeB
            // 
            this.txtWorkTimeB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtWorkTimeB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtWorkTimeB.BackColor = System.Drawing.Color.Transparent;
            this.txtWorkTimeB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtWorkTimeB.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtWorkTimeB.Enabled = false;
            this.txtWorkTimeB.ForeImage = null;
            this.txtWorkTimeB.Location = new System.Drawing.Point(523, 48);
            this.txtWorkTimeB.MaxLengh = 32767;
            this.txtWorkTimeB.Multiline = false;
            this.txtWorkTimeB.Name = "txtWorkTimeB";
            this.txtWorkTimeB.Radius = 3;
            this.txtWorkTimeB.ReadOnly = false;
            this.txtWorkTimeB.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtWorkTimeB.ShowError = false;
            this.txtWorkTimeB.Size = new System.Drawing.Size(164, 23);
            this.txtWorkTimeB.TabIndex = 21;
            this.txtWorkTimeB.UseSystemPasswordChar = false;
            this.txtWorkTimeB.Value = "";
            this.txtWorkTimeB.VerifyCondition = null;
            this.txtWorkTimeB.VerifyType = null;
            this.txtWorkTimeB.WaterMark = null;
            this.txtWorkTimeB.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtWorkTimeA
            // 
            this.txtWorkTimeA.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtWorkTimeA.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtWorkTimeA.BackColor = System.Drawing.Color.Transparent;
            this.txtWorkTimeA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtWorkTimeA.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtWorkTimeA.Enabled = false;
            this.txtWorkTimeA.ForeImage = null;
            this.txtWorkTimeA.Location = new System.Drawing.Point(290, 48);
            this.txtWorkTimeA.MaxLengh = 32767;
            this.txtWorkTimeA.Multiline = false;
            this.txtWorkTimeA.Name = "txtWorkTimeA";
            this.txtWorkTimeA.Radius = 3;
            this.txtWorkTimeA.ReadOnly = false;
            this.txtWorkTimeA.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtWorkTimeA.ShowError = false;
            this.txtWorkTimeA.Size = new System.Drawing.Size(137, 23);
            this.txtWorkTimeA.TabIndex = 20;
            this.txtWorkTimeA.UseSystemPasswordChar = false;
            this.txtWorkTimeA.Value = "";
            this.txtWorkTimeA.VerifyCondition = null;
            this.txtWorkTimeA.VerifyType = null;
            this.txtWorkTimeA.WaterMark = null;
            this.txtWorkTimeA.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtProName
            // 
            this.txtProName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtProName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtProName.BackColor = System.Drawing.Color.Transparent;
            this.txtProName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtProName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtProName.ForeImage = null;
            this.txtProName.Location = new System.Drawing.Point(523, 16);
            this.txtProName.MaxLengh = 32767;
            this.txtProName.Multiline = false;
            this.txtProName.Name = "txtProName";
            this.txtProName.Radius = 3;
            this.txtProName.ReadOnly = false;
            this.txtProName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtProName.ShowError = false;
            this.txtProName.Size = new System.Drawing.Size(164, 23);
            this.txtProName.TabIndex = 19;
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
            this.txtProNo.Location = new System.Drawing.Point(290, 16);
            this.txtProNo.MaxLengh = 32767;
            this.txtProNo.Multiline = false;
            this.txtProNo.Name = "txtProNo";
            this.txtProNo.Radius = 3;
            this.txtProNo.ReadOnly = false;
            this.txtProNo.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtProNo.ShowError = false;
            this.txtProNo.Size = new System.Drawing.Size(137, 23);
            this.txtProNo.TabIndex = 18;
            this.txtProNo.UseSystemPasswordChar = false;
            this.txtProNo.Value = "";
            this.txtProNo.VerifyCondition = null;
            this.txtProNo.VerifyType = null;
            this.txtProNo.WaterMark = null;
            this.txtProNo.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // ddlState
            // 
            this.ddlState.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlState.FormattingEnabled = true;
            this.ddlState.Location = new System.Drawing.Point(769, 19);
            this.ddlState.Name = "ddlState";
            this.ddlState.Size = new System.Drawing.Size(121, 22);
            this.ddlState.TabIndex = 17;
            // 
            // ddlDataSource
            // 
            this.ddlDataSource.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDataSource.FormattingEnabled = true;
            this.ddlDataSource.Location = new System.Drawing.Point(71, 19);
            this.ddlDataSource.Name = "ddlDataSource";
            this.ddlDataSource.Size = new System.Drawing.Size(121, 22);
            this.ddlDataSource.TabIndex = 16;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(902, 83);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(76, 26);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(902, 48);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(76, 26);
            this.btnClear.TabIndex = 12;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(699, 89);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 12);
            this.label12.TabIndex = 11;
            this.label12.Text = "至 ";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(456, 87);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 10;
            this.label11.Text = "创建时间：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(197, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "定额单价(元)：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(694, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "C类工时数：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(450, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "B类工时数：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(215, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "A类工时数：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(724, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "状态：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(432, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "维修项目名称：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "维修项目编号：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据来源：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelEx2
            // 
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.winFormPager1);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(19, 489);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(994, 35);
            this.panelEx2.TabIndex = 4;
            // 
            // winFormPager1
            // 
            this.winFormPager1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.winFormPager1.BackColor = System.Drawing.Color.Transparent;
            this.winFormPager1.BtnTextNext = "下页";
            this.winFormPager1.BtnTextPrevious = "上页";
            this.winFormPager1.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.winFormPager1.Location = new System.Drawing.Point(563, 1);
            this.winFormPager1.Name = "winFormPager1";
            this.winFormPager1.PageCount = 0;
            this.winFormPager1.PageSize = 15;
            this.winFormPager1.RecordCount = 0;
            this.winFormPager1.Size = new System.Drawing.Size(427, 32);
            this.winFormPager1.TabIndex = 5;
            this.winFormPager1.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.winFormPager1.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.winFormPager1_PageIndexChanged);
            // 
            // UCWorkingTimeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.tabControlEx1);
            this.Controls.Add(this.panelEx1);
            this.Name = "UCWorkingTimeManager";
            this.Size = new System.Drawing.Size(1030, 540);
            this.Load += new System.EventHandler(this.UCWorkingTimeManager_Load);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.tabControlEx1, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvWorkList)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.DataGridViewEx gvWorkList;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dateTimeEnd;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dateTimeStart;
        private ServiceStationClient.ComponentUI.TextBoxEx txtQuotaPrice;
        private ServiceStationClient.ComponentUI.TextBoxEx txtWorkTimeC;
        private ServiceStationClient.ComponentUI.TextBoxEx txtWorkTimeB;
        private ServiceStationClient.ComponentUI.TextBoxEx txtWorkTimeA;
        private ServiceStationClient.ComponentUI.TextBoxEx txtProName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtProNo;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlState;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlDataSource;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager winFormPager1;
        private System.Windows.Forms.RadioButton radIsWorkTime;
        private System.Windows.Forms.RadioButton radIsQuota;
        private System.Windows.Forms.DataGridViewTextBoxColumn whours_id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn data_source;
        private System.Windows.Forms.DataGridViewTextBoxColumn project_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn project_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn whours_num_a;
        private System.Windows.Forms.DataGridViewTextBoxColumn whours_num_b;
        private System.Windows.Forms.DataGridViewTextBoxColumn whours_num_c;
        private System.Windows.Forms.DataGridViewTextBoxColumn quota_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_username;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
    }
}
