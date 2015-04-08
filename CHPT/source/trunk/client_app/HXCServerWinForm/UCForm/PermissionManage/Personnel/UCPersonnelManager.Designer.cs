namespace HXCServerWinForm.UCForm.Personnel
{
    partial class UCPersonnelManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPersonnelManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnl = new System.Windows.Forms.Panel();
            this.pnlRight = new ServiceStationClient.ComponentUI.PanelEx();
            this.dgvUser = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.user_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_operator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.land_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.com_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.org_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_telephone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSearch = new ServiceStationClient.ComponentUI.PanelEx();
            this.dtpend = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dtpstart = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.cbbis_operator = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cbbstatus = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtuser_telephone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.txtuser_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtidcard_num = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.txtuser_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLeft = new ServiceStationClient.ComponentUI.PanelEx();
            this.tvCompany = new ServiceStationClient.ComponentUI.TreeViewEx();
            this.pnl.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl
            // 
            this.pnl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl.Controls.Add(this.pnlRight);
            this.pnl.Controls.Add(this.pnlLeft);
            this.pnl.Location = new System.Drawing.Point(2, 33);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(1027, 508);
            this.pnl.TabIndex = 8;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlRight.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlRight.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlRight.BorderWidth = 1;
            this.pnlRight.Controls.Add(this.dgvUser);
            this.pnlRight.Controls.Add(this.pnlSearch);
            this.pnlRight.Curvature = 0;
            this.pnlRight.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlRight.Location = new System.Drawing.Point(200, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(827, 508);
            this.pnlRight.TabIndex = 1;
            // 
            // dgvUser
            // 
            this.dgvUser.AllowUserToAddRows = false;
            this.dgvUser.AllowUserToDeleteRows = false;
            this.dgvUser.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvUser.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUser.BackgroundColor = System.Drawing.Color.White;
            this.dgvUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUser.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.user_name,
            this.user_code,
            this.is_operator,
            this.land_name,
            this.com_name,
            this.org_name,
            this.user_phone,
            this.user_telephone,
            this.user_address,
            this.status,
            this.remark,
            this.user_id});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUser.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvUser.EnableHeadersVisualStyles = false;
            this.dgvUser.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvUser.IsCheck = true;
            this.dgvUser.Location = new System.Drawing.Point(0, 121);
            this.dgvUser.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvUser.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvUser.MergeColumnNames")));
            this.dgvUser.MultiSelect = false;
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.ReadOnly = true;
            this.dgvUser.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvUser.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.RowHeadersWidth = 30;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvUser.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvUser.RowTemplate.Height = 23;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.ShowCheckBox = true;
            this.dgvUser.Size = new System.Drawing.Size(815, 387);
            this.dgvUser.TabIndex = 6;
            this.dgvUser.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvUser_CellBeginEdit);
            this.dgvUser.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUser_CellDoubleClick);
            this.dgvUser.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvUser_CellFormatting);
            this.dgvUser.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvUser_CellMouseUp);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 30;
            // 
            // user_name
            // 
            this.user_name.DataPropertyName = "user_name";
            this.user_name.HeaderText = "人员姓名";
            this.user_name.Name = "user_name";
            this.user_name.ReadOnly = true;
            // 
            // user_code
            // 
            this.user_code.DataPropertyName = "user_code";
            this.user_code.HeaderText = "人员编码";
            this.user_code.Name = "user_code";
            this.user_code.ReadOnly = true;
            // 
            // is_operator
            // 
            this.is_operator.DataPropertyName = "is_operator";
            this.is_operator.HeaderText = "操作员";
            this.is_operator.Name = "is_operator";
            this.is_operator.ReadOnly = true;
            this.is_operator.Width = 70;
            // 
            // land_name
            // 
            this.land_name.DataPropertyName = "land_name";
            this.land_name.HeaderText = "账号";
            this.land_name.Name = "land_name";
            this.land_name.ReadOnly = true;
            // 
            // com_name
            // 
            this.com_name.DataPropertyName = "com_name";
            this.com_name.HeaderText = "所属公司";
            this.com_name.Name = "com_name";
            this.com_name.ReadOnly = true;
            // 
            // org_name
            // 
            this.org_name.DataPropertyName = "org_name";
            this.org_name.HeaderText = "所属组织";
            this.org_name.Name = "org_name";
            this.org_name.ReadOnly = true;
            // 
            // user_phone
            // 
            this.user_phone.DataPropertyName = "user_phone";
            this.user_phone.HeaderText = "手机";
            this.user_phone.Name = "user_phone";
            this.user_phone.ReadOnly = true;
            // 
            // user_telephone
            // 
            this.user_telephone.DataPropertyName = "user_telephone";
            this.user_telephone.HeaderText = "电话";
            this.user_telephone.Name = "user_telephone";
            this.user_telephone.ReadOnly = true;
            // 
            // user_address
            // 
            this.user_address.DataPropertyName = "user_address";
            this.user_address.HeaderText = "地址";
            this.user_address.Name = "user_address";
            this.user_address.ReadOnly = true;
            this.user_address.Width = 200;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 60;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            this.remark.Width = 200;
            // 
            // user_id
            // 
            this.user_id.DataPropertyName = "user_id";
            this.user_id.HeaderText = "user_id";
            this.user_id.Name = "user_id";
            this.user_id.ReadOnly = true;
            this.user_id.Visible = false;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlSearch.BorderWidth = 1;
            this.pnlSearch.Controls.Add(this.dtpend);
            this.pnlSearch.Controls.Add(this.dtpstart);
            this.pnlSearch.Controls.Add(this.cbbis_operator);
            this.pnlSearch.Controls.Add(this.cbbstatus);
            this.pnlSearch.Controls.Add(this.btnQuery);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Controls.Add(this.label8);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.txtuser_telephone);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Controls.Add(this.txtuser_name);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.txtidcard_num);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.txtuser_code);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Curvature = 0;
            this.pnlSearch.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlSearch.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlSearch.Location = new System.Drawing.Point(0, 3);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(815, 114);
            this.pnlSearch.TabIndex = 0;
            // 
            // dtpend
            // 
            this.dtpend.Location = new System.Drawing.Point(254, 74);
            this.dtpend.Name = "dtpend";
            this.dtpend.ShowFormat = "yyyy-MM-dd";
            this.dtpend.Size = new System.Drawing.Size(137, 21);
            this.dtpend.TabIndex = 21;
            // 
            // dtpstart
            // 
            this.dtpstart.Location = new System.Drawing.Point(77, 74);
            this.dtpstart.Name = "dtpstart";
            this.dtpstart.ShowFormat = "yyyy-MM-dd";
            this.dtpstart.Size = new System.Drawing.Size(146, 21);
            this.dtpstart.TabIndex = 21;
            // 
            // cbbis_operator
            // 
            this.cbbis_operator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbis_operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbis_operator.FormattingEnabled = true;
            this.cbbis_operator.Location = new System.Drawing.Point(497, 41);
            this.cbbis_operator.Name = "cbbis_operator";
            this.cbbis_operator.Size = new System.Drawing.Size(121, 22);
            this.cbbis_operator.TabIndex = 20;
            // 
            // cbbstatus
            // 
            this.cbbstatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbstatus.FormattingEnabled = true;
            this.cbbstatus.Location = new System.Drawing.Point(497, 10);
            this.cbbstatus.Name = "cbbstatus";
            this.cbbstatus.Size = new System.Drawing.Size(121, 22);
            this.cbbstatus.TabIndex = 19;
            // 
            // btnQuery
            // 
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(667, 65);
            this.btnQuery.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.MoveImage")));
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.NormalImage")));
            this.btnQuery.Size = new System.Drawing.Size(60, 26);
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
            this.btnClear.Location = new System.Drawing.Point(667, 32);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 16;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(229, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "至";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "入职时间：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(416, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "是否操作员：";
            // 
            // txtuser_telephone
            // 
            this.txtuser_telephone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtuser_telephone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtuser_telephone.BackColor = System.Drawing.Color.Transparent;
            this.txtuser_telephone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtuser_telephone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtuser_telephone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtuser_telephone.ForeImage = null;
            this.txtuser_telephone.InputtingVerifyCondition = null;
            this.txtuser_telephone.Location = new System.Drawing.Point(286, 41);
            this.txtuser_telephone.MaxLengh = 32767;
            this.txtuser_telephone.Multiline = false;
            this.txtuser_telephone.Name = "txtuser_telephone";
            this.txtuser_telephone.Radius = 3;
            this.txtuser_telephone.ReadOnly = false;
            this.txtuser_telephone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtuser_telephone.ShowError = false;
            this.txtuser_telephone.Size = new System.Drawing.Size(120, 23);
            this.txtuser_telephone.TabIndex = 9;
            this.txtuser_telephone.UseSystemPasswordChar = false;
            this.txtuser_telephone.Value = "";
            this.txtuser_telephone.VerifyCondition = null;
            this.txtuser_telephone.VerifyType = null;
            this.txtuser_telephone.VerifyTypeName = null;
            this.txtuser_telephone.WaterMark = null;
            this.txtuser_telephone.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(220, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "联系电话：";
            // 
            // txtuser_name
            // 
            this.txtuser_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtuser_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtuser_name.BackColor = System.Drawing.Color.Transparent;
            this.txtuser_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtuser_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtuser_name.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtuser_name.ForeImage = null;
            this.txtuser_name.InputtingVerifyCondition = null;
            this.txtuser_name.Location = new System.Drawing.Point(76, 41);
            this.txtuser_name.MaxLengh = 32767;
            this.txtuser_name.Multiline = false;
            this.txtuser_name.Name = "txtuser_name";
            this.txtuser_name.Radius = 3;
            this.txtuser_name.ReadOnly = false;
            this.txtuser_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtuser_name.ShowError = false;
            this.txtuser_name.Size = new System.Drawing.Size(120, 23);
            this.txtuser_name.TabIndex = 7;
            this.txtuser_name.UseSystemPasswordChar = false;
            this.txtuser_name.Value = "";
            this.txtuser_name.VerifyCondition = null;
            this.txtuser_name.VerifyType = null;
            this.txtuser_name.VerifyTypeName = null;
            this.txtuser_name.WaterMark = null;
            this.txtuser_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "人员名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(449, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "状态：";
            // 
            // txtidcard_num
            // 
            this.txtidcard_num.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtidcard_num.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtidcard_num.BackColor = System.Drawing.Color.Transparent;
            this.txtidcard_num.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtidcard_num.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtidcard_num.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtidcard_num.ForeImage = null;
            this.txtidcard_num.InputtingVerifyCondition = null;
            this.txtidcard_num.Location = new System.Drawing.Point(286, 8);
            this.txtidcard_num.MaxLengh = 32767;
            this.txtidcard_num.Multiline = false;
            this.txtidcard_num.Name = "txtidcard_num";
            this.txtidcard_num.Radius = 3;
            this.txtidcard_num.ReadOnly = false;
            this.txtidcard_num.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtidcard_num.ShowError = false;
            this.txtidcard_num.Size = new System.Drawing.Size(120, 23);
            this.txtidcard_num.TabIndex = 3;
            this.txtidcard_num.UseSystemPasswordChar = false;
            this.txtidcard_num.Value = "";
            this.txtidcard_num.VerifyCondition = null;
            this.txtidcard_num.VerifyType = null;
            this.txtidcard_num.VerifyTypeName = null;
            this.txtidcard_num.WaterMark = null;
            this.txtidcard_num.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "证件号码：";
            // 
            // txtuser_code
            // 
            this.txtuser_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtuser_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtuser_code.BackColor = System.Drawing.Color.Transparent;
            this.txtuser_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtuser_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtuser_code.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtuser_code.ForeImage = null;
            this.txtuser_code.InputtingVerifyCondition = null;
            this.txtuser_code.Location = new System.Drawing.Point(77, 8);
            this.txtuser_code.MaxLengh = 32767;
            this.txtuser_code.Multiline = false;
            this.txtuser_code.Name = "txtuser_code";
            this.txtuser_code.Radius = 3;
            this.txtuser_code.ReadOnly = false;
            this.txtuser_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtuser_code.ShowError = false;
            this.txtuser_code.Size = new System.Drawing.Size(120, 23);
            this.txtuser_code.TabIndex = 1;
            this.txtuser_code.UseSystemPasswordChar = false;
            this.txtuser_code.Value = "";
            this.txtuser_code.VerifyCondition = null;
            this.txtuser_code.VerifyType = null;
            this.txtuser_code.VerifyTypeName = null;
            this.txtuser_code.WaterMark = null;
            this.txtuser_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "人员编码：";
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlLeft.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlLeft.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlLeft.BorderWidth = 1;
            this.pnlLeft.Controls.Add(this.tvCompany);
            this.pnlLeft.Curvature = 0;
            this.pnlLeft.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(200, 508);
            this.pnlLeft.TabIndex = 0;
            // 
            // tvCompany
            // 
            this.tvCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCompany.IgnoreAutoCheck = false;
            this.tvCompany.Location = new System.Drawing.Point(0, 0);
            this.tvCompany.Name = "tvCompany";
            this.tvCompany.Size = new System.Drawing.Size(200, 508);
            this.tvCompany.TabIndex = 0;
            this.tvCompany.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCompany_AfterSelect);
            // 
            // UCPersonnelManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl);
            this.Name = "UCPersonnelManager";
            this.Load += new System.EventHandler(this.UCPersonnelManager_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.pnl, 0);
            this.pnl.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl;
        private ServiceStationClient.ComponentUI.PanelEx pnlRight;
        private ServiceStationClient.ComponentUI.PanelEx pnlSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private ServiceStationClient.ComponentUI.TextBoxEx txtuser_telephone;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.TextBoxEx txtuser_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.TextBoxEx txtidcard_num;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx txtuser_code;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.PanelEx pnlLeft;
        private ServiceStationClient.ComponentUI.TreeViewEx tvCompany;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvUser;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbbis_operator;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbbstatus;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtpend;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtpstart;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_operator;
        private System.Windows.Forms.DataGridViewTextBoxColumn land_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn com_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_telephone;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_address;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_id;
    }
}
