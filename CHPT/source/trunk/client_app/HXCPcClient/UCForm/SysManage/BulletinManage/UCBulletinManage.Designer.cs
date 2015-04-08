namespace HXCPcClient.UCForm.SysManage.BulletinManage
{
    partial class UCBulletinManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCBulletinManage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvRecord = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.pnlSearch = new ServiceStationClient.ComponentUI.PanelEx();
            this.dtpETime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpSTime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbOrg = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cmbStatus = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cboannouncement_type = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cmbUser = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKeyNmae = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.columnCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.columnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.announcement_title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrg = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnStatus = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvRecord
            // 
            this.dgvRecord.AllowUserToAddRows = false;
            this.dgvRecord.AllowUserToDeleteRows = false;
            this.dgvRecord.AllowUserToResizeColumns = false;
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
            this.columnCheck,
            this.columnId,
            this.columnType,
            this.announcement_title,
            this.columnTime,
            this.columnOrg,
            this.columnUser,
            this.columnStatus});
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
            this.dgvRecord.Location = new System.Drawing.Point(-1, 136);
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
            this.dgvRecord.Size = new System.Drawing.Size(1032, 233);
            this.dgvRecord.TabIndex = 1;
            this.dgvRecord.Tag = "";
            this.dgvRecord.HeadCheckChanged += new ServiceStationClient.ComponentUI.DataGridViewEx.DelegateOnClick(this.dgvRecord_HeadCheckChanged);
            this.dgvRecord.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvannouncement_CellDoubleClick);
            this.dgvRecord.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvannouncement_CellFormatting);
            // 
            // panelEx2
            // 
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.page);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(0, 372);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.panelEx2.Size = new System.Drawing.Size(1030, 28);
            this.panelEx2.TabIndex = 5;
            // 
            // page
            // 
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.Dock = System.Windows.Forms.DockStyle.Right;
            this.page.Location = new System.Drawing.Point(475, 0);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(455, 28);
            this.page.TabIndex = 5;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            this.pnlSearch.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlSearch.BorderWidth = 1;
            this.pnlSearch.Controls.Add(this.dtpETime);
            this.pnlSearch.Controls.Add(this.dtpSTime);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.cmbOrg);
            this.pnlSearch.Controls.Add(this.cmbStatus);
            this.pnlSearch.Controls.Add(this.cboannouncement_type);
            this.pnlSearch.Controls.Add(this.cmbUser);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Controls.Add(this.btnQuery);
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.txtKeyNmae);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Curvature = 0;
            this.pnlSearch.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlSearch.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlSearch.Location = new System.Drawing.Point(0, 24);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1030, 112);
            this.pnlSearch.TabIndex = 0;
            // 
            // dtpETime
            // 
            this.dtpETime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpETime.Location = new System.Drawing.Point(211, 56);
            this.dtpETime.Name = "dtpETime";
            this.dtpETime.ShowFormat = "yyyy-MM-dd";
            this.dtpETime.Size = new System.Drawing.Size(121, 21);
            this.dtpETime.TabIndex = 61;
            this.dtpETime.Value = new System.DateTime(2014, 9, 24, 0, 0, 0, 0);
            // 
            // dtpSTime
            // 
            this.dtpSTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpSTime.Location = new System.Drawing.Point(77, 56);
            this.dtpSTime.Name = "dtpSTime";
            this.dtpSTime.ShowFormat = "yyyy-MM-dd";
            this.dtpSTime.Size = new System.Drawing.Size(121, 21);
            this.dtpSTime.TabIndex = 60;
            this.dtpSTime.Value = new System.DateTime(2014, 9, 24, 0, 0, 0, 0);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(196, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 35;
            this.label7.Text = "至";
            // 
            // cmbOrg
            // 
            this.cmbOrg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbOrg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrg.FormattingEnabled = true;
            this.cmbOrg.Location = new System.Drawing.Point(434, 56);
            this.cmbOrg.Name = "cmbOrg";
            this.cmbOrg.Size = new System.Drawing.Size(121, 22);
            this.cmbOrg.TabIndex = 33;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(650, 16);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(121, 22);
            this.cmbStatus.TabIndex = 32;
            // 
            // cboannouncement_type
            // 
            this.cboannouncement_type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboannouncement_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboannouncement_type.FormattingEnabled = true;
            this.cboannouncement_type.Location = new System.Drawing.Point(434, 16);
            this.cboannouncement_type.Name = "cboannouncement_type";
            this.cboannouncement_type.Size = new System.Drawing.Size(121, 22);
            this.cboannouncement_type.TabIndex = 31;
            // 
            // cmbUser
            // 
            this.cmbUser.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(650, 56);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(121, 22);
            this.cmbUser.TabIndex = 30;
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(841, 15);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(70, 26);
            this.btnClear.TabIndex = 16;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(841, 53);
            this.btnQuery.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.MoveImage")));
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.NormalImage")));
            this.btnQuery.Size = new System.Drawing.Size(70, 26);
            this.btnQuery.TabIndex = 17;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(597, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "发布人：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(370, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "发布部门：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "发布时间：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(609, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "状态：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(394, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "分类：";
            // 
            // txtKeyNmae
            // 
            this.txtKeyNmae.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtKeyNmae.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtKeyNmae.BackColor = System.Drawing.Color.Transparent;
            this.txtKeyNmae.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtKeyNmae.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtKeyNmae.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtKeyNmae.ForeImage = null;
            this.txtKeyNmae.InputtingVerifyCondition = null;
            this.txtKeyNmae.Location = new System.Drawing.Point(77, 16);
            this.txtKeyNmae.MaxLengh = 32767;
            this.txtKeyNmae.Multiline = false;
            this.txtKeyNmae.Name = "txtKeyNmae";
            this.txtKeyNmae.Radius = 3;
            this.txtKeyNmae.ReadOnly = false;
            this.txtKeyNmae.SelectStart = 0;
            this.txtKeyNmae.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtKeyNmae.ShowError = false;
            this.txtKeyNmae.Size = new System.Drawing.Size(255, 23);
            this.txtKeyNmae.TabIndex = 1;
            this.txtKeyNmae.UseSystemPasswordChar = false;
            this.txtKeyNmae.Value = "";
            this.txtKeyNmae.VerifyCondition = null;
            this.txtKeyNmae.VerifyType = null;
            this.txtKeyNmae.VerifyTypeName = null;
            this.txtKeyNmae.WaterMark = null;
            this.txtKeyNmae.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "关键字：";
            // 
            // columnCheck
            // 
            this.columnCheck.FalseValue = "false";
            this.columnCheck.HeaderText = "";
            this.columnCheck.MinimumWidth = 30;
            this.columnCheck.Name = "columnCheck";
            this.columnCheck.ReadOnly = true;
            this.columnCheck.TrueValue = "true";
            this.columnCheck.Width = 30;
            // 
            // columnId
            // 
            this.columnId.DataPropertyName = "announcement_id";
            this.columnId.HeaderText = "announcement_id";
            this.columnId.Name = "columnId";
            this.columnId.ReadOnly = true;
            this.columnId.Visible = false;
            // 
            // columnType
            // 
            this.columnType.DataPropertyName = "announcement_type";
            this.columnType.HeaderText = "分类";
            this.columnType.Name = "columnType";
            this.columnType.ReadOnly = true;
            this.columnType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.columnType.Width = 150;
            // 
            // announcement_title
            // 
            this.announcement_title.DataPropertyName = "announcement_title";
            this.announcement_title.HeaderText = "标题";
            this.announcement_title.Name = "announcement_title";
            this.announcement_title.ReadOnly = true;
            this.announcement_title.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.announcement_title.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.announcement_title.Width = 260;
            // 
            // columnTime
            // 
            this.columnTime.DataPropertyName = "date_up";
            this.columnTime.HeaderText = "发布日期";
            this.columnTime.Name = "columnTime";
            this.columnTime.ReadOnly = true;
            this.columnTime.Width = 160;
            // 
            // columnOrg
            // 
            this.columnOrg.DataPropertyName = "org_id";
            this.columnOrg.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.columnOrg.HeaderText = "发布部门";
            this.columnOrg.Name = "columnOrg";
            this.columnOrg.ReadOnly = true;
            this.columnOrg.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnOrg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.columnOrg.Width = 140;
            // 
            // columnUser
            // 
            this.columnUser.DataPropertyName = "user_id";
            this.columnUser.HeaderText = "发布人";
            this.columnUser.Name = "columnUser";
            this.columnUser.ReadOnly = true;
            this.columnUser.Resizable = System.Windows.Forms.DataGridViewTriState.True;
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
            // UCBulletinManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.dgvRecord);
            this.Name = "UCBulletinManage";
            this.Size = new System.Drawing.Size(1030, 400);
            this.Load += new System.EventHandler(this.UCBulletinManage_Load);
            this.Controls.SetChildIndex(this.dgvRecord, 0);
            this.Controls.SetChildIndex(this.pnlSearch, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private ServiceStationClient.ComponentUI.PanelEx pnlSearch;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbUser;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx txtKeyNmae;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbOrg;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbStatus;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboannouncement_type;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRecord;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpETime;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpSTime;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn announcement_title;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTime;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnOrg;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnUser;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnStatus;
    }
}
