namespace HXCPcClient.UCForm.CustomerService.Member
{
    partial class UCMemberManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCMemberManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.palQTop = new System.Windows.Forms.Panel();
            this.cbo_status = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txt_legal_person = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.txt_cust_name = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.labCBPerson = new System.Windows.Forms.Label();
            this.labCustomerName = new System.Windows.Forms.Label();
            this.cbo_member_grade = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.dtp_create_time_e = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtp_create_time_s = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.label3 = new System.Windows.Forms.Label();
            this.labCBTime = new System.Windows.Forms.Label();
            this.labOrderStatus = new System.Windows.Forms.Label();
            this.labCBType = new System.Windows.Forms.Label();
            this.txt_vip_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labCBTitle = new System.Windows.Forms.Label();
            this.dgvQData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.drchk_check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.drtxt_vip_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_vip_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_cust_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_legal_person = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_member_grade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_validity_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_handle_result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_integral = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.pageQ = new ServiceStationClient.ComponentUI.WinFormPager();
            this.palQTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQData)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // palQTop
            // 
            this.palQTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.palQTop.Controls.Add(this.cbo_status);
            this.palQTop.Controls.Add(this.txt_legal_person);
            this.palQTop.Controls.Add(this.btnQuery);
            this.palQTop.Controls.Add(this.btnClear);
            this.palQTop.Controls.Add(this.txt_cust_name);
            this.palQTop.Controls.Add(this.labCBPerson);
            this.palQTop.Controls.Add(this.labCustomerName);
            this.palQTop.Controls.Add(this.cbo_member_grade);
            this.palQTop.Controls.Add(this.dtp_create_time_e);
            this.palQTop.Controls.Add(this.dtp_create_time_s);
            this.palQTop.Controls.Add(this.label3);
            this.palQTop.Controls.Add(this.labCBTime);
            this.palQTop.Controls.Add(this.labOrderStatus);
            this.palQTop.Controls.Add(this.labCBType);
            this.palQTop.Controls.Add(this.txt_vip_code);
            this.palQTop.Controls.Add(this.labCBTitle);
            this.palQTop.Location = new System.Drawing.Point(3, 40);
            this.palQTop.Name = "palQTop";
            this.palQTop.Size = new System.Drawing.Size(1016, 101);
            this.palQTop.TabIndex = 20;
            // 
            // cbo_status
            // 
            this.cbo_status.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_status.FormattingEnabled = true;
            this.cbo_status.Location = new System.Drawing.Point(90, 56);
            this.cbo_status.Name = "cbo_status";
            this.cbo_status.Size = new System.Drawing.Size(118, 21);
            this.cbo_status.TabIndex = 117;
            // 
            // txt_legal_person
            // 
            this.txt_legal_person.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_legal_person.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_legal_person.BackColor = System.Drawing.Color.Transparent;
            this.txt_legal_person.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_legal_person.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_legal_person.ForeImage = null;
            this.txt_legal_person.Location = new System.Drawing.Point(722, 16);
            this.txt_legal_person.MaxLengh = 32767;
            this.txt_legal_person.Multiline = false;
            this.txt_legal_person.Name = "txt_legal_person";
            this.txt_legal_person.Radius = 3;
            this.txt_legal_person.ReadOnly = false;
            this.txt_legal_person.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_legal_person.Size = new System.Drawing.Size(118, 22);
            this.txt_legal_person.TabIndex = 116;
            this.txt_legal_person.UseSystemPasswordChar = false;
            this.txt_legal_person.WaterMark = null;
            this.txt_legal_person.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // btnQuery
            // 
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(927, 51);
            this.btnQuery.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.MoveImage")));
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.NormalImage")));
            this.btnQuery.Size = new System.Drawing.Size(60, 28);
            this.btnQuery.TabIndex = 115;
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(927, 16);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 28);
            this.btnClear.TabIndex = 114;
            // 
            // txt_cust_name
            // 
            this.txt_cust_name.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txt_cust_name.Location = new System.Drawing.Point(512, 16);
            this.txt_cust_name.Name = "txt_cust_name";
            this.txt_cust_name.ReadOnly = false;
            this.txt_cust_name.Size = new System.Drawing.Size(118, 26);
            this.txt_cust_name.TabIndex = 112;
            this.txt_cust_name.ToolTipTitle = "";
            // 
            // labCBPerson
            // 
            this.labCBPerson.AutoSize = true;
            this.labCBPerson.Location = new System.Drawing.Point(43, 62);
            this.labCBPerson.Name = "labCBPerson";
            this.labCBPerson.Size = new System.Drawing.Size(43, 13);
            this.labCBPerson.TabIndex = 111;
            this.labCBPerson.Text = "状态：";
            // 
            // labCustomerName
            // 
            this.labCustomerName.AutoSize = true;
            this.labCustomerName.Location = new System.Drawing.Point(441, 23);
            this.labCustomerName.Name = "labCustomerName";
            this.labCustomerName.Size = new System.Drawing.Size(67, 13);
            this.labCustomerName.TabIndex = 110;
            this.labCustomerName.Text = "会员名称：";
            // 
            // cbo_member_grade
            // 
            this.cbo_member_grade.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_member_grade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_member_grade.FormattingEnabled = true;
            this.cbo_member_grade.Location = new System.Drawing.Point(307, 17);
            this.cbo_member_grade.Name = "cbo_member_grade";
            this.cbo_member_grade.Size = new System.Drawing.Size(118, 21);
            this.cbo_member_grade.TabIndex = 109;
            // 
            // dtp_create_time_e
            // 
            this.dtp_create_time_e.Location = new System.Drawing.Point(480, 57);
            this.dtp_create_time_e.Name = "dtp_create_time_e";
            this.dtp_create_time_e.ShowFormat = "yyyy-MM-dd";
            this.dtp_create_time_e.Size = new System.Drawing.Size(162, 21);
            this.dtp_create_time_e.TabIndex = 92;
            this.dtp_create_time_e.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // dtp_create_time_s
            // 
            this.dtp_create_time_s.Location = new System.Drawing.Point(307, 57);
            this.dtp_create_time_s.Name = "dtp_create_time_s";
            this.dtp_create_time_s.ShowFormat = "yyyy-MM-dd";
            this.dtp_create_time_s.Size = new System.Drawing.Size(144, 22);
            this.dtp_create_time_s.TabIndex = 91;
            this.dtp_create_time_s.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(457, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 90;
            this.label3.Text = "到";
            // 
            // labCBTime
            // 
            this.labCBTime.AutoSize = true;
            this.labCBTime.Location = new System.Drawing.Point(236, 62);
            this.labCBTime.Name = "labCBTime";
            this.labCBTime.Size = new System.Drawing.Size(67, 13);
            this.labCBTime.TabIndex = 89;
            this.labCBTime.Text = "创建时间：";
            // 
            // labOrderStatus
            // 
            this.labOrderStatus.AutoSize = true;
            this.labOrderStatus.Location = new System.Drawing.Point(663, 23);
            this.labOrderStatus.Name = "labOrderStatus";
            this.labOrderStatus.Size = new System.Drawing.Size(55, 13);
            this.labOrderStatus.TabIndex = 87;
            this.labOrderStatus.Text = "联系人：";
            // 
            // labCBType
            // 
            this.labCBType.AutoSize = true;
            this.labCBType.Location = new System.Drawing.Point(236, 23);
            this.labCBType.Name = "labCBType";
            this.labCBType.Size = new System.Drawing.Size(67, 13);
            this.labCBType.TabIndex = 85;
            this.labCBType.Text = "会员等级：";
            // 
            // txt_vip_code
            // 
            this.txt_vip_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_vip_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_vip_code.BackColor = System.Drawing.Color.Transparent;
            this.txt_vip_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_vip_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_vip_code.ForeImage = null;
            this.txt_vip_code.Location = new System.Drawing.Point(90, 17);
            this.txt_vip_code.MaxLengh = 32767;
            this.txt_vip_code.Multiline = false;
            this.txt_vip_code.Name = "txt_vip_code";
            this.txt_vip_code.Radius = 3;
            this.txt_vip_code.ReadOnly = false;
            this.txt_vip_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_vip_code.Size = new System.Drawing.Size(118, 22);
            this.txt_vip_code.TabIndex = 82;
            this.txt_vip_code.UseSystemPasswordChar = false;
            this.txt_vip_code.WaterMark = null;
            this.txt_vip_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labCBTitle
            // 
            this.labCBTitle.AutoSize = true;
            this.labCBTitle.Location = new System.Drawing.Point(19, 23);
            this.labCBTitle.Name = "labCBTitle";
            this.labCBTitle.Size = new System.Drawing.Size(67, 13);
            this.labCBTitle.TabIndex = 81;
            this.labCBTitle.Text = "会员编号：";
            // 
            // dgvQData
            // 
            this.dgvQData.AllowUserToAddRows = false;
            this.dgvQData.AllowUserToDeleteRows = false;
            this.dgvQData.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvQData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvQData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvQData.BackgroundColor = System.Drawing.Color.White;
            this.dgvQData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvQData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvQData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.drchk_check,
            this.drtxt_vip_id,
            this.drtxt_vip_code,
            this.drtxt_cust_name,
            this.drtxt_legal_person,
            this.drtxt_member_grade,
            this.drtxt_create_time,
            this.drtxt_validity_time,
            this.drtxt_handle_result,
            this.drtxt_integral,
            this.drtxt_status,
            this.drtxt_remark});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvQData.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvQData.EnableHeadersVisualStyles = false;
            this.dgvQData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvQData.Location = new System.Drawing.Point(3, 147);
            this.dgvQData.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvQData.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvQData.MergeColumnNames")));
            this.dgvQData.MultiSelect = false;
            this.dgvQData.Name = "dgvQData";
            this.dgvQData.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvQData.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvQData.RowHeadersVisible = false;
            this.dgvQData.RowHeadersWidth = 30;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvQData.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvQData.RowTemplate.Height = 23;
            this.dgvQData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQData.ShowCheckBox = true;
            this.dgvQData.Size = new System.Drawing.Size(1016, 405);
            this.dgvQData.TabIndex = 21;
            // 
            // drchk_check
            // 
            this.drchk_check.HeaderText = "";
            this.drchk_check.MinimumWidth = 18;
            this.drchk_check.Name = "drchk_check";
            this.drchk_check.ReadOnly = true;
            this.drchk_check.Width = 25;
            // 
            // drtxt_vip_id
            // 
            this.drtxt_vip_id.DataPropertyName = "vip_id";
            this.drtxt_vip_id.HeaderText = "会员标识";
            this.drtxt_vip_id.Name = "drtxt_vip_id";
            this.drtxt_vip_id.ReadOnly = true;
            this.drtxt_vip_id.Visible = false;
            // 
            // drtxt_vip_code
            // 
            this.drtxt_vip_code.DataPropertyName = "vip_code";
            this.drtxt_vip_code.HeaderText = "会员编码";
            this.drtxt_vip_code.Name = "drtxt_vip_code";
            this.drtxt_vip_code.ReadOnly = true;
            // 
            // drtxt_cust_name
            // 
            this.drtxt_cust_name.DataPropertyName = "cust_name";
            this.drtxt_cust_name.HeaderText = "会员名称";
            this.drtxt_cust_name.Name = "drtxt_cust_name";
            this.drtxt_cust_name.ReadOnly = true;
            // 
            // drtxt_legal_person
            // 
            this.drtxt_legal_person.DataPropertyName = "legal_person";
            this.drtxt_legal_person.HeaderText = "联系人";
            this.drtxt_legal_person.Name = "drtxt_legal_person";
            this.drtxt_legal_person.ReadOnly = true;
            // 
            // drtxt_member_grade
            // 
            this.drtxt_member_grade.DataPropertyName = "member_grade";
            this.drtxt_member_grade.HeaderText = "会员等级";
            this.drtxt_member_grade.Name = "drtxt_member_grade";
            this.drtxt_member_grade.ReadOnly = true;
            // 
            // drtxt_create_time
            // 
            this.drtxt_create_time.DataPropertyName = "create_time";
            this.drtxt_create_time.HeaderText = "创建时间";
            this.drtxt_create_time.Name = "drtxt_create_time";
            this.drtxt_create_time.ReadOnly = true;
            // 
            // drtxt_validity_time
            // 
            this.drtxt_validity_time.DataPropertyName = "validity_time";
            this.drtxt_validity_time.HeaderText = "到期时间";
            this.drtxt_validity_time.Name = "drtxt_validity_time";
            this.drtxt_validity_time.ReadOnly = true;
            // 
            // drtxt_handle_result
            // 
            this.drtxt_handle_result.DataPropertyName = "handle_result";
            this.drtxt_handle_result.HeaderText = "账户余额";
            this.drtxt_handle_result.Name = "drtxt_handle_result";
            this.drtxt_handle_result.ReadOnly = true;
            // 
            // drtxt_integral
            // 
            this.drtxt_integral.DataPropertyName = "integral";
            this.drtxt_integral.HeaderText = "积分";
            this.drtxt_integral.Name = "drtxt_integral";
            this.drtxt_integral.ReadOnly = true;
            // 
            // drtxt_status
            // 
            this.drtxt_status.DataPropertyName = "status";
            this.drtxt_status.HeaderText = "状态";
            this.drtxt_status.Name = "drtxt_status";
            this.drtxt_status.ReadOnly = true;
            // 
            // drtxt_remark
            // 
            this.drtxt_remark.DataPropertyName = "remark";
            this.drtxt_remark.HeaderText = "备注";
            this.drtxt_remark.Name = "drtxt_remark";
            this.drtxt_remark.ReadOnly = true;
            this.drtxt_remark.Width = 115;
            // 
            // panelEx2
            // 
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(223)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.pageQ);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(0, 559);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.panelEx2.Size = new System.Drawing.Size(1030, 30);
            this.panelEx2.TabIndex = 22;
            // 
            // pageQ
            // 
            this.pageQ.BackColor = System.Drawing.Color.Transparent;
            this.pageQ.BtnTextNext = "下页";
            this.pageQ.BtnTextPrevious = "上页";
            this.pageQ.Dock = System.Windows.Forms.DockStyle.Right;
            this.pageQ.Location = new System.Drawing.Point(502, 0);
            this.pageQ.Name = "pageQ";
            this.pageQ.PageCount = 0;
            this.pageQ.PageSize = 15;
            this.pageQ.RecordCount = 0;
            this.pageQ.Size = new System.Drawing.Size(428, 30);
            this.pageQ.TabIndex = 5;
            // 
            // UCMemberManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.dgvQData);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.palQTop);
            this.Name = "UCMemberManager";
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.palQTop, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.Controls.SetChildIndex(this.dgvQData, 0);
            this.palQTop.ResumeLayout(false);
            this.palQTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQData)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel palQTop;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbo_member_grade;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtp_create_time_e;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtp_create_time_s;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labCBTime;
        private System.Windows.Forms.Label labOrderStatus;
        private System.Windows.Forms.Label labCBType;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_vip_code;
        private System.Windows.Forms.Label labCBTitle;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txt_cust_name;
        private System.Windows.Forms.Label labCBPerson;
        private System.Windows.Forms.Label labCustomerName;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvQData;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager pageQ;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbo_status;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_legal_person;
        private System.Windows.Forms.DataGridViewCheckBoxColumn drchk_check;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_vip_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_vip_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_cust_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_legal_person;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_member_grade;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_validity_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_handle_result;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_integral;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_remark;

    }
}
