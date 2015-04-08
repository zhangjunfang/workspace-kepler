namespace HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SalePlan
{
    partial class UCSalePlanManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCSalePlanManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.ddlResponsiblePerson = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.dateTimeStart = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.ddlDepartment = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.ddlState = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimeEnd = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRemark = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtPlanNo = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.gvSalePlanList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.sale_plan_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colorder_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorder_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colplan_counts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colplan_money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colplan_from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colplan_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colfinish_counts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colfinish_money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorg_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colhandle_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coloperator_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorder_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorder_status_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.import_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colremark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colsuspend_reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.winFormPager1 = new ServiceStationClient.ComponentUI.WinFormPager();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSalePlanList)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.ddlResponsiblePerson);
            this.panelEx1.Controls.Add(this.dateTimeStart);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.ddlDepartment);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.ddlState);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.dateTimeEnd);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.txtRemark);
            this.panelEx1.Controls.Add(this.txtPlanNo);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.label8);
            this.panelEx1.Controls.Add(this.label10);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(0, 57);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1030, 114);
            this.panelEx1.TabIndex = 5;
            // 
            // ddlResponsiblePerson
            // 
            this.ddlResponsiblePerson.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlResponsiblePerson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlResponsiblePerson.FormattingEnabled = true;
            this.ddlResponsiblePerson.Location = new System.Drawing.Point(399, 47);
            this.ddlResponsiblePerson.Name = "ddlResponsiblePerson";
            this.ddlResponsiblePerson.Size = new System.Drawing.Size(163, 22);
            this.ddlResponsiblePerson.TabIndex = 1;
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Location = new System.Drawing.Point(402, 17);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.ShowFormat = "yyyy年MM月dd日";
            this.dateTimeStart.Size = new System.Drawing.Size(164, 21);
            this.dateTimeStart.TabIndex = 32;
            this.dateTimeStart.Value = new System.DateTime(2014, 9, 15, 20, 17, 9, 878);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(921, 72);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(60, 26);
            this.btnSearch.TabIndex = 31;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ddlDepartment
            // 
            this.ddlDepartment.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDepartment.FormattingEnabled = true;
            this.ddlDepartment.Location = new System.Drawing.Point(88, 47);
            this.ddlDepartment.Name = "ddlDepartment";
            this.ddlDepartment.Size = new System.Drawing.Size(164, 22);
            this.ddlDepartment.TabIndex = 16;
            this.ddlDepartment.SelectedIndexChanged += new System.EventHandler(this.ddlDepartment_SelectedIndexChanged);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(921, 38);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 30;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ddlState
            // 
            this.ddlState.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlState.FormattingEnabled = true;
            this.ddlState.Location = new System.Drawing.Point(647, 47);
            this.ddlState.Name = "ddlState";
            this.ddlState.Size = new System.Drawing.Size(164, 22);
            this.ddlState.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(576, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "单据状态：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Location = new System.Drawing.Point(647, 20);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.ShowFormat = "yyyy年MM月dd日";
            this.dateTimeEnd.Size = new System.Drawing.Size(164, 21);
            this.dateTimeEnd.TabIndex = 29;
            this.dateTimeEnd.Value = new System.DateTime(2014, 9, 15, 20, 17, 9, 878);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(345, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "经办人：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemark
            // 
            this.txtRemark.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtRemark.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtRemark.BackColor = System.Drawing.Color.Transparent;
            this.txtRemark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtRemark.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtRemark.ForeImage = null;
            this.txtRemark.Location = new System.Drawing.Point(88, 75);
            this.txtRemark.MaxLengh = 32767;
            this.txtRemark.Multiline = false;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Radius = 3;
            this.txtRemark.ReadOnly = false;
            this.txtRemark.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtRemark.ShowError = false;
            this.txtRemark.Size = new System.Drawing.Size(723, 23);
            this.txtRemark.TabIndex = 25;
            this.txtRemark.UseSystemPasswordChar = false;
            this.txtRemark.Value = "";
            this.txtRemark.VerifyCondition = null;
            this.txtRemark.VerifyType = null;
            this.txtRemark.WaterMark = null;
            this.txtRemark.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtPlanNo
            // 
            this.txtPlanNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPlanNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPlanNo.BackColor = System.Drawing.Color.Transparent;
            this.txtPlanNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtPlanNo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtPlanNo.ForeImage = null;
            this.txtPlanNo.Location = new System.Drawing.Point(88, 15);
            this.txtPlanNo.MaxLengh = 32767;
            this.txtPlanNo.Multiline = false;
            this.txtPlanNo.Name = "txtPlanNo";
            this.txtPlanNo.Radius = 3;
            this.txtPlanNo.ReadOnly = false;
            this.txtPlanNo.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPlanNo.ShowError = false;
            this.txtPlanNo.Size = new System.Drawing.Size(164, 23);
            this.txtPlanNo.TabIndex = 3;
            this.txtPlanNo.UseSystemPasswordChar = false;
            this.txtPlanNo.Value = "";
            this.txtPlanNo.VerifyCondition = null;
            this.txtPlanNo.VerifyType = null;
            this.txtPlanNo.WaterMark = null;
            this.txtPlanNo.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "计划单号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "备注：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(333, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "单据日期：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(597, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "至";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(44, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "部门：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gvSalePlanList
            // 
            this.gvSalePlanList.AllowUserToAddRows = false;
            this.gvSalePlanList.AllowUserToDeleteRows = false;
            this.gvSalePlanList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gvSalePlanList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvSalePlanList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvSalePlanList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvSalePlanList.BackgroundColor = System.Drawing.Color.White;
            this.gvSalePlanList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSalePlanList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvSalePlanList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSalePlanList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sale_plan_id,
            this.colCheck,
            this.colorder_num,
            this.colorder_date,
            this.colplan_counts,
            this.colplan_money,
            this.colplan_from,
            this.colplan_to,
            this.colfinish_counts,
            this.colfinish_money,
            this.colorg_name,
            this.colhandle_name,
            this.coloperator_name,
            this.colorder_status,
            this.colorder_status_name,
            this.import_status,
            this.colremark,
            this.colsuspend_reason});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvSalePlanList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvSalePlanList.EnableHeadersVisualStyles = false;
            this.gvSalePlanList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.gvSalePlanList.Location = new System.Drawing.Point(0, 177);
            this.gvSalePlanList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gvSalePlanList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gvSalePlanList.MergeColumnNames")));
            this.gvSalePlanList.MultiSelect = false;
            this.gvSalePlanList.Name = "gvSalePlanList";
            this.gvSalePlanList.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSalePlanList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvSalePlanList.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.gvSalePlanList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvSalePlanList.RowTemplate.Height = 23;
            this.gvSalePlanList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvSalePlanList.ShowCheckBox = true;
            this.gvSalePlanList.Size = new System.Drawing.Size(1030, 227);
            this.gvSalePlanList.TabIndex = 34;
            this.gvSalePlanList.HeadCheckChanged += new ServiceStationClient.ComponentUI.DataGridViewEx.DelegateOnClick(this.gvSalePlanList_HeadCheckChanged);
            this.gvSalePlanList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSalePlanList_CellContentClick);
            this.gvSalePlanList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSalePlanList_CellDoubleClick);
            this.gvSalePlanList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvSalePlanList_CellFormatting);
            this.gvSalePlanList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvSalePlanList_CellMouseClick);
            // 
            // sale_plan_id
            // 
            this.sale_plan_id.DataPropertyName = "sale_plan_id";
            this.sale_plan_id.HeaderText = "sale_plan_id";
            this.sale_plan_id.Name = "sale_plan_id";
            this.sale_plan_id.ReadOnly = true;
            this.sale_plan_id.Visible = false;
            this.sale_plan_id.Width = 87;
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 18;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 18;
            // 
            // colorder_num
            // 
            this.colorder_num.DataPropertyName = "order_num";
            this.colorder_num.HeaderText = "销售计划单号";
            this.colorder_num.Name = "colorder_num";
            this.colorder_num.ReadOnly = true;
            this.colorder_num.Width = 105;
            // 
            // colorder_date
            // 
            this.colorder_date.DataPropertyName = "order_date";
            this.colorder_date.HeaderText = "日期";
            this.colorder_date.Name = "colorder_date";
            this.colorder_date.ReadOnly = true;
            this.colorder_date.Width = 57;
            // 
            // colplan_counts
            // 
            this.colplan_counts.DataPropertyName = "plan_counts";
            this.colplan_counts.HeaderText = "计划数量";
            this.colplan_counts.Name = "colplan_counts";
            this.colplan_counts.ReadOnly = true;
            this.colplan_counts.Width = 81;
            // 
            // colplan_money
            // 
            this.colplan_money.DataPropertyName = "plan_money";
            this.colplan_money.HeaderText = "计划金额";
            this.colplan_money.Name = "colplan_money";
            this.colplan_money.ReadOnly = true;
            this.colplan_money.Width = 81;
            // 
            // colplan_from
            // 
            this.colplan_from.DataPropertyName = "plan_from";
            this.colplan_from.HeaderText = "开始时间";
            this.colplan_from.Name = "colplan_from";
            this.colplan_from.ReadOnly = true;
            this.colplan_from.Width = 81;
            // 
            // colplan_to
            // 
            this.colplan_to.DataPropertyName = "plan_to";
            this.colplan_to.HeaderText = "结束时间";
            this.colplan_to.Name = "colplan_to";
            this.colplan_to.ReadOnly = true;
            this.colplan_to.Width = 81;
            // 
            // colfinish_counts
            // 
            this.colfinish_counts.DataPropertyName = "finish_counts";
            this.colfinish_counts.HeaderText = "完成数量";
            this.colfinish_counts.Name = "colfinish_counts";
            this.colfinish_counts.ReadOnly = true;
            this.colfinish_counts.Width = 81;
            // 
            // colfinish_money
            // 
            this.colfinish_money.DataPropertyName = "finish_money";
            this.colfinish_money.HeaderText = "完成金额";
            this.colfinish_money.Name = "colfinish_money";
            this.colfinish_money.ReadOnly = true;
            this.colfinish_money.Width = 81;
            // 
            // colorg_name
            // 
            this.colorg_name.DataPropertyName = "org_name";
            this.colorg_name.HeaderText = "部门名称";
            this.colorg_name.Name = "colorg_name";
            this.colorg_name.ReadOnly = true;
            this.colorg_name.Width = 81;
            // 
            // colhandle_name
            // 
            this.colhandle_name.DataPropertyName = "handle_name";
            this.colhandle_name.HeaderText = "经办人";
            this.colhandle_name.Name = "colhandle_name";
            this.colhandle_name.ReadOnly = true;
            this.colhandle_name.Width = 69;
            // 
            // coloperator_name
            // 
            this.coloperator_name.DataPropertyName = "operator_name";
            this.coloperator_name.HeaderText = "操作人";
            this.coloperator_name.Name = "coloperator_name";
            this.coloperator_name.ReadOnly = true;
            this.coloperator_name.Width = 69;
            // 
            // colorder_status
            // 
            this.colorder_status.DataPropertyName = "order_status";
            this.colorder_status.HeaderText = "单据状态ID";
            this.colorder_status.Name = "colorder_status";
            this.colorder_status.ReadOnly = true;
            this.colorder_status.Visible = false;
            this.colorder_status.Width = 95;
            // 
            // colorder_status_name
            // 
            this.colorder_status_name.DataPropertyName = "order_status_name";
            this.colorder_status_name.HeaderText = "单据状态";
            this.colorder_status_name.Name = "colorder_status_name";
            this.colorder_status_name.ReadOnly = true;
            this.colorder_status_name.Width = 81;
            // 
            // import_status
            // 
            this.import_status.DataPropertyName = "import_status";
            this.import_status.HeaderText = "导入状态";
            this.import_status.Name = "import_status";
            this.import_status.ReadOnly = true;
            this.import_status.Visible = false;
            this.import_status.Width = 81;
            // 
            // colremark
            // 
            this.colremark.DataPropertyName = "remark";
            this.colremark.HeaderText = "备注";
            this.colremark.Name = "colremark";
            this.colremark.ReadOnly = true;
            this.colremark.Width = 57;
            // 
            // colsuspend_reason
            // 
            this.colsuspend_reason.DataPropertyName = "suspend_reason";
            this.colsuspend_reason.HeaderText = "中止原因";
            this.colsuspend_reason.Name = "colsuspend_reason";
            this.colsuspend_reason.ReadOnly = true;
            this.colsuspend_reason.Width = 81;
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
            this.panelEx2.Location = new System.Drawing.Point(0, 410);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1030, 35);
            this.panelEx2.TabIndex = 35;
            // 
            // winFormPager1
            // 
            this.winFormPager1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.winFormPager1.BackColor = System.Drawing.Color.Transparent;
            this.winFormPager1.BtnTextNext = "下页";
            this.winFormPager1.BtnTextPrevious = "上页";
            this.winFormPager1.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.winFormPager1.Location = new System.Drawing.Point(599, 1);
            this.winFormPager1.Name = "winFormPager1";
            this.winFormPager1.PageCount = 0;
            this.winFormPager1.PageSize = 15;
            this.winFormPager1.RecordCount = 0;
            this.winFormPager1.Size = new System.Drawing.Size(427, 32);
            this.winFormPager1.TabIndex = 5;
            this.winFormPager1.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.winFormPager1.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.winFormPager1_PageIndexChanged);
            // 
            // UCSalePlanManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.gvSalePlanList);
            this.Controls.Add(this.panelEx1);
            this.Name = "UCSalePlanManager";
            this.Size = new System.Drawing.Size(1030, 448);
            this.Load += new System.EventHandler(this.UCSalePlanManager_Load);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.gvSalePlanList, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSalePlanList)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlResponsiblePerson;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dateTimeStart;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlDepartment;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlState;
        private System.Windows.Forms.Label label6;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dateTimeEnd;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.TextBoxEx txtRemark;
        private ServiceStationClient.ComponentUI.TextBoxEx txtPlanNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private ServiceStationClient.ComponentUI.DataGridViewEx gvSalePlanList;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager winFormPager1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sale_plan_id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn colorder_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn colorder_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn colplan_counts;
        private System.Windows.Forms.DataGridViewTextBoxColumn colplan_money;
        private System.Windows.Forms.DataGridViewTextBoxColumn colplan_from;
        private System.Windows.Forms.DataGridViewTextBoxColumn colplan_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn colfinish_counts;
        private System.Windows.Forms.DataGridViewTextBoxColumn colfinish_money;
        private System.Windows.Forms.DataGridViewTextBoxColumn colorg_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn colhandle_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn coloperator_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn colorder_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn colorder_status_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn import_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn colremark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colsuspend_reason;
    }
}
