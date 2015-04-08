namespace HXCPcClient.UCForm.SysManage.RepairMeal
{
    partial class UCRepairMealManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCRepairMealManage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.palTop = new ServiceStationClient.ComponentUI.PanelEx();
            this.dtiVaild = new ServiceStationClient.ComponentUI.DateTimeInterval_sms();
            this.cbbstatus = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.labTime = new System.Windows.Forms.Label();
            this.txtMealName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labMealName = new System.Windows.Forms.Label();
            this.txtMealCode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labMealCode = new System.Windows.Forms.Label();
            this.palBottom = new ServiceStationClient.ComponentUI.PanelEx();
            this.dgvRecord = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.package_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.package_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnStatus = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.period_validity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valid_until = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.palTop.SuspendLayout();
            this.palBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // palTop
            // 
            this.palTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.palTop.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.palTop.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.palTop.BorderWidth = 1;
            this.palTop.Controls.Add(this.dtiVaild);
            this.palTop.Controls.Add(this.cbbstatus);
            this.palTop.Controls.Add(this.label3);
            this.palTop.Controls.Add(this.btnQuery);
            this.palTop.Controls.Add(this.btnClear);
            this.palTop.Controls.Add(this.labTime);
            this.palTop.Controls.Add(this.txtMealName);
            this.palTop.Controls.Add(this.labMealName);
            this.palTop.Controls.Add(this.txtMealCode);
            this.palTop.Controls.Add(this.labMealCode);
            this.palTop.Curvature = 0;
            this.palTop.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.palTop.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.palTop.Location = new System.Drawing.Point(0, 33);
            this.palTop.Name = "palTop";
            this.palTop.Size = new System.Drawing.Size(1030, 76);
            this.palTop.TabIndex = 4;
            // 
            // dtiVaild
            // 
            this.dtiVaild.BackColor = System.Drawing.Color.Transparent;
            this.dtiVaild.customFormat = null;
            this.dtiVaild.EndDate = "";
            this.dtiVaild.Location = new System.Drawing.Point(87, 41);
            this.dtiVaild.Margin = new System.Windows.Forms.Padding(0);
            this.dtiVaild.Name = "dtiVaild";
            this.dtiVaild.Size = new System.Drawing.Size(263, 27);
            this.dtiVaild.StartDate = "";
            this.dtiVaild.TabIndex = 115;
            // 
            // cbbstatus
            // 
            this.cbbstatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbstatus.FormattingEnabled = true;
            this.cbbstatus.Location = new System.Drawing.Point(555, 6);
            this.cbbstatus.Name = "cbbstatus";
            this.cbbstatus.Size = new System.Drawing.Size(121, 22);
            this.cbbstatus.TabIndex = 114;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(507, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 113;
            this.label3.Text = "状态：";
            // 
            // btnQuery
            // 
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(711, 42);
            this.btnQuery.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.MoveImage")));
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.NormalImage")));
            this.btnQuery.Size = new System.Drawing.Size(60, 26);
            this.btnQuery.TabIndex = 110;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(711, 10);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 109;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // labTime
            // 
            this.labTime.AutoSize = true;
            this.labTime.Location = new System.Drawing.Point(16, 45);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(65, 12);
            this.labTime.TabIndex = 99;
            this.labTime.Text = "有效日期：";
            // 
            // txtMealName
            // 
            this.txtMealName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtMealName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtMealName.BackColor = System.Drawing.Color.Transparent;
            this.txtMealName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtMealName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtMealName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMealName.ForeImage = null;
            this.txtMealName.InputtingVerifyCondition = null;
            this.txtMealName.Location = new System.Drawing.Point(330, 6);
            this.txtMealName.MaxLengh = 32767;
            this.txtMealName.Multiline = false;
            this.txtMealName.Name = "txtMealName";
            this.txtMealName.Radius = 3;
            this.txtMealName.ReadOnly = false;
            this.txtMealName.SelectStart = 0;
            this.txtMealName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtMealName.ShowError = false;
            this.txtMealName.Size = new System.Drawing.Size(120, 23);
            this.txtMealName.TabIndex = 71;
            this.txtMealName.UseSystemPasswordChar = false;
            this.txtMealName.Value = "";
            this.txtMealName.VerifyCondition = null;
            this.txtMealName.VerifyType = null;
            this.txtMealName.VerifyTypeName = null;
            this.txtMealName.WaterMark = null;
            this.txtMealName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labMealName
            // 
            this.labMealName.AutoSize = true;
            this.labMealName.Location = new System.Drawing.Point(262, 12);
            this.labMealName.Name = "labMealName";
            this.labMealName.Size = new System.Drawing.Size(65, 12);
            this.labMealName.TabIndex = 70;
            this.labMealName.Text = "套餐名称：";
            // 
            // txtMealCode
            // 
            this.txtMealCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtMealCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtMealCode.BackColor = System.Drawing.Color.Transparent;
            this.txtMealCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtMealCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtMealCode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMealCode.ForeImage = null;
            this.txtMealCode.InputtingVerifyCondition = null;
            this.txtMealCode.Location = new System.Drawing.Point(87, 6);
            this.txtMealCode.MaxLengh = 32767;
            this.txtMealCode.Multiline = false;
            this.txtMealCode.Name = "txtMealCode";
            this.txtMealCode.Radius = 3;
            this.txtMealCode.ReadOnly = false;
            this.txtMealCode.SelectStart = 0;
            this.txtMealCode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtMealCode.ShowError = false;
            this.txtMealCode.Size = new System.Drawing.Size(120, 23);
            this.txtMealCode.TabIndex = 69;
            this.txtMealCode.UseSystemPasswordChar = false;
            this.txtMealCode.Value = "";
            this.txtMealCode.VerifyCondition = null;
            this.txtMealCode.VerifyType = null;
            this.txtMealCode.VerifyTypeName = null;
            this.txtMealCode.WaterMark = null;
            this.txtMealCode.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labMealCode
            // 
            this.labMealCode.AutoSize = true;
            this.labMealCode.Location = new System.Drawing.Point(16, 12);
            this.labMealCode.Name = "labMealCode";
            this.labMealCode.Size = new System.Drawing.Size(65, 12);
            this.labMealCode.TabIndex = 68;
            this.labMealCode.Text = "套餐编码：";
            // 
            // palBottom
            // 
            this.palBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.palBottom.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.palBottom.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.palBottom.BorderWidth = 1;
            this.palBottom.Controls.Add(this.dgvRecord);
            this.palBottom.Curvature = 0;
            this.palBottom.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.palBottom.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.palBottom.Location = new System.Drawing.Point(0, 114);
            this.palBottom.Name = "palBottom";
            this.palBottom.Size = new System.Drawing.Size(1030, 396);
            this.palBottom.TabIndex = 6;
            // 
            // dgvRecord
            // 
            this.dgvRecord.AllowUserToAddRows = false;
            this.dgvRecord.AllowUserToDeleteRows = false;
            this.dgvRecord.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvRecord.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecord.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.package_code,
            this.package_name,
            this.columnStatus,
            this.period_validity,
            this.remarks,
            this.columnId,
            this.valid_until});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecord.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRecord.EnableHeadersVisualStyles = false;
            this.dgvRecord.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvRecord.IsCheck = true;
            this.dgvRecord.Location = new System.Drawing.Point(3, 3);
            this.dgvRecord.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvRecord.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvRecord.MergeColumnNames")));
            this.dgvRecord.MultiSelect = false;
            this.dgvRecord.Name = "dgvRecord";
            this.dgvRecord.ReadOnly = true;
            this.dgvRecord.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvRecord.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRecord.RowHeadersVisible = false;
            this.dgvRecord.RowHeadersWidth = 30;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvRecord.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvRecord.RowTemplate.Height = 23;
            this.dgvRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecord.ShowCheckBox = true;
            this.dgvRecord.Size = new System.Drawing.Size(1024, 388);
            this.dgvRecord.TabIndex = 13;
            this.dgvRecord.HeadCheckChanged += new ServiceStationClient.ComponentUI.DataGridViewEx.DelegateOnClick(this.dgvRecord_HeadCheckChanged);
            this.dgvRecord.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRData_CellDoubleClick);
            this.dgvRecord.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRData_CellFormatting);
            this.dgvRecord.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRecord_CellMouseClick);
            // 
            // panelEx2
            // 
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(223)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.page);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(0, 516);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.panelEx2.Size = new System.Drawing.Size(1030, 28);
            this.panelEx2.TabIndex = 19;
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
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 30;
            // 
            // package_code
            // 
            this.package_code.DataPropertyName = "package_code";
            this.package_code.HeaderText = "套餐编码";
            this.package_code.Name = "package_code";
            this.package_code.ReadOnly = true;
            this.package_code.Width = 140;
            // 
            // package_name
            // 
            this.package_name.DataPropertyName = "package_name";
            this.package_name.HeaderText = "套餐名称";
            this.package_name.Name = "package_name";
            this.package_name.ReadOnly = true;
            this.package_name.Width = 140;
            // 
            // columnStatus
            // 
            this.columnStatus.DataPropertyName = "status";
            this.columnStatus.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.columnStatus.HeaderText = "状态";
            this.columnStatus.Name = "columnStatus";
            this.columnStatus.ReadOnly = true;
            this.columnStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // period_validity
            // 
            this.period_validity.DataPropertyName = "period_validity";
            this.period_validity.HeaderText = "有效时间";
            this.period_validity.Name = "period_validity";
            this.period_validity.ReadOnly = true;
            this.period_validity.Width = 260;
            // 
            // remarks
            // 
            this.remarks.DataPropertyName = "remarks";
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            this.remarks.Width = 300;
            // 
            // columnId
            // 
            this.columnId.DataPropertyName = "repair_package_set_id";
            this.columnId.HeaderText = "repair_package_set_id";
            this.columnId.Name = "columnId";
            this.columnId.ReadOnly = true;
            this.columnId.Visible = false;
            this.columnId.Width = 10;
            // 
            // valid_until
            // 
            this.valid_until.DataPropertyName = "valid_until";
            this.valid_until.HeaderText = "valid_until";
            this.valid_until.Name = "valid_until";
            this.valid_until.ReadOnly = true;
            this.valid_until.Visible = false;
            // 
            // UCRepairMealManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.palBottom);
            this.Controls.Add(this.palTop);
            this.Name = "UCRepairMealManage";
            this.Load += new System.EventHandler(this.UCRepairMealManage_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.palTop, 0);
            this.Controls.SetChildIndex(this.palBottom, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.palTop.ResumeLayout(false);
            this.palTop.PerformLayout();
            this.palBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx palTop;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Label labTime;
        private ServiceStationClient.ComponentUI.TextBoxEx txtMealName;
        private System.Windows.Forms.Label labMealName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtMealCode;
        private System.Windows.Forms.Label labMealCode;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbbstatus;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.PanelEx palBottom;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRecord;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private ServiceStationClient.ComponentUI.DateTimeInterval_sms dtiVaild;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn package_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn package_name;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn period_validity;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn valid_until;
    }
}
