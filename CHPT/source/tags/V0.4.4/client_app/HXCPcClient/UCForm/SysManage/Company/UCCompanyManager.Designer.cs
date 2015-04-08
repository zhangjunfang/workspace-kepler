namespace HXCPcClient.UCForm.SysManage.Company
{
    partial class UCCompanyManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCompanyManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.dgvRecord = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.dgvMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pnlSearch = new ServiceStationClient.ComponentUI.PanelEx();
            this.cmbCountry = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cmbCity = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cmbProvince = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.cmbWXZZ = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cmbGSXZ = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPhone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.tbName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbCode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.columnCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.columnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnShortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWXZZ = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colGSXZ = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.collegal_person = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcom_tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Colremark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
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
            this.panelEx2.Location = new System.Drawing.Point(0, 437);
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
            this.page.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.page.Dock = System.Windows.Forms.DockStyle.Right;
            this.page.Location = new System.Drawing.Point(475, 0);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(455, 28);
            this.page.TabIndex = 5;
            this.page.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
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
            this.dgvRecord.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecord.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
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
            this.columnCode,
            this.columnName,
            this.columnShortName,
            this.colWXZZ,
            this.colGSXZ,
            this.collegal_person,
            this.colcom_tel,
            this.colStatus,
            this.Colremark});
            this.dgvRecord.ContextMenuStrip = this.dgvMenu;
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
            this.dgvRecord.Location = new System.Drawing.Point(-1, 143);
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
            this.dgvRecord.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvRecord.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvRecord.RowTemplate.Height = 23;
            this.dgvRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecord.ShowCheckBox = true;
            this.dgvRecord.Size = new System.Drawing.Size(1032, 292);
            this.dgvRecord.TabIndex = 0;
            this.dgvRecord.Tag = "";
            this.dgvRecord.HeadCheckChanged += new ServiceStationClient.ComponentUI.DataGridViewEx.DelegateOnClick(this.dgvRecord_HeadCheckChanged);
            this.dgvRecord.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvRecord_CellBeginEdit);
            this.dgvRecord.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecord_CellDoubleClick);
            // 
            // dgvMenu
            // 
            this.dgvMenu.Name = "dgvMenu";
            this.dgvMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlSearch.BorderWidth = 1;
            this.pnlSearch.Controls.Add(this.cmbCountry);
            this.pnlSearch.Controls.Add(this.cmbCity);
            this.pnlSearch.Controls.Add(this.cmbProvince);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.cmbWXZZ);
            this.pnlSearch.Controls.Add(this.cmbGSXZ);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Controls.Add(this.btnQuery);
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.tbPhone);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Controls.Add(this.tbName);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.tbCode);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Curvature = 0;
            this.pnlSearch.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlSearch.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlSearch.Location = new System.Drawing.Point(0, 26);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1030, 117);
            this.pnlSearch.TabIndex = 0;
            // 
            // cmbCountry
            // 
            this.cmbCountry.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(779, 16);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(81, 22);
            this.cmbCountry.TabIndex = 35;
            // 
            // cmbCity
            // 
            this.cmbCity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCity.FormattingEnabled = true;
            this.cmbCity.Location = new System.Drawing.Point(691, 16);
            this.cmbCity.Name = "cmbCity";
            this.cmbCity.Size = new System.Drawing.Size(81, 22);
            this.cmbCity.TabIndex = 34;
            this.cmbCity.SelectionChangeCommitted += new System.EventHandler(this.cmbCity_SelectionChangeCommitted);
            // 
            // cmbProvince
            // 
            this.cmbProvince.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvince.FormattingEnabled = true;
            this.cmbProvince.Location = new System.Drawing.Point(602, 16);
            this.cmbProvince.Name = "cmbProvince";
            this.cmbProvince.Size = new System.Drawing.Size(81, 22);
            this.cmbProvince.TabIndex = 33;
            this.cmbProvince.SelectionChangeCommitted += new System.EventHandler(this.cmbProvince_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(548, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "所在地：";
            // 
            // cmbWXZZ
            // 
            this.cmbWXZZ.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWXZZ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWXZZ.FormattingEnabled = true;
            this.cmbWXZZ.Location = new System.Drawing.Point(352, 17);
            this.cmbWXZZ.Name = "cmbWXZZ";
            this.cmbWXZZ.Size = new System.Drawing.Size(121, 22);
            this.cmbWXZZ.TabIndex = 31;
            // 
            // cmbGSXZ
            // 
            this.cmbGSXZ.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGSXZ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGSXZ.FormattingEnabled = true;
            this.cmbGSXZ.Location = new System.Drawing.Point(697, 58);
            this.cmbGSXZ.Name = "cmbGSXZ";
            this.cmbGSXZ.Size = new System.Drawing.Size(163, 22);
            this.cmbGSXZ.TabIndex = 30;
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(921, 18);
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
            this.btnQuery.Location = new System.Drawing.Point(921, 57);
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
            this.label6.Location = new System.Drawing.Point(626, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "公司性质：";
            // 
            // tbPhone
            // 
            this.tbPhone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbPhone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbPhone.BackColor = System.Drawing.Color.Transparent;
            this.tbPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbPhone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbPhone.ForeImage = null;
            this.tbPhone.InputtingVerifyCondition = null;
            this.tbPhone.Location = new System.Drawing.Point(430, 58);
            this.tbPhone.MaxLengh = 32767;
            this.tbPhone.Multiline = false;
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Radius = 3;
            this.tbPhone.ReadOnly = false;
            this.tbPhone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbPhone.ShowError = false;
            this.tbPhone.Size = new System.Drawing.Size(126, 23);
            this.tbPhone.TabIndex = 9;
            this.tbPhone.UseSystemPasswordChar = false;
            this.tbPhone.Value = "";
            this.tbPhone.VerifyCondition = null;
            this.tbPhone.VerifyType = null;
            this.tbPhone.VerifyTypeName = null;
            this.tbPhone.WaterMark = null;
            this.tbPhone.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(360, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "联系电话：";
            // 
            // tbName
            // 
            this.tbName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbName.BackColor = System.Drawing.Color.Transparent;
            this.tbName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbName.ForeImage = null;
            this.tbName.InputtingVerifyCondition = null;
            this.tbName.Location = new System.Drawing.Point(83, 58);
            this.tbName.MaxLengh = 32767;
            this.tbName.Multiline = false;
            this.tbName.Name = "tbName";
            this.tbName.Radius = 3;
            this.tbName.ReadOnly = false;
            this.tbName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbName.ShowError = false;
            this.tbName.Size = new System.Drawing.Size(221, 23);
            this.tbName.TabIndex = 7;
            this.tbName.UseSystemPasswordChar = false;
            this.tbName.Value = "";
            this.tbName.VerifyCondition = null;
            this.tbName.VerifyType = null;
            this.tbName.VerifyTypeName = null;
            this.tbName.WaterMark = null;
            this.tbName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "公司名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(282, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "维修资质：";
            // 
            // tbCode
            // 
            this.tbCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbCode.BackColor = System.Drawing.Color.Transparent;
            this.tbCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbCode.ForeImage = null;
            this.tbCode.InputtingVerifyCondition = null;
            this.tbCode.Location = new System.Drawing.Point(84, 16);
            this.tbCode.MaxLengh = 32767;
            this.tbCode.Multiline = false;
            this.tbCode.Name = "tbCode";
            this.tbCode.Radius = 3;
            this.tbCode.ReadOnly = false;
            this.tbCode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbCode.ShowError = false;
            this.tbCode.Size = new System.Drawing.Size(139, 23);
            this.tbCode.TabIndex = 1;
            this.tbCode.UseSystemPasswordChar = false;
            this.tbCode.Value = "";
            this.tbCode.VerifyCondition = null;
            this.tbCode.VerifyType = null;
            this.tbCode.VerifyTypeName = null;
            this.tbCode.WaterMark = null;
            this.tbCode.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "公司编码：";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 3);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1030, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // columnCheck
            // 
            this.columnCheck.FillWeight = 29.09796F;
            this.columnCheck.HeaderText = "";
            this.columnCheck.MinimumWidth = 30;
            this.columnCheck.Name = "columnCheck";
            this.columnCheck.ReadOnly = true;
            // 
            // columnId
            // 
            this.columnId.DataPropertyName = "com_id";
            this.columnId.HeaderText = "com_id";
            this.columnId.Name = "columnId";
            this.columnId.ReadOnly = true;
            this.columnId.Visible = false;
            // 
            // columnCode
            // 
            this.columnCode.DataPropertyName = "com_code";
            this.columnCode.FillWeight = 107.878F;
            this.columnCode.HeaderText = "公司编码";
            this.columnCode.Name = "columnCode";
            this.columnCode.ReadOnly = true;
            // 
            // columnName
            // 
            this.columnName.DataPropertyName = "com_name";
            this.columnName.FillWeight = 107.878F;
            this.columnName.HeaderText = "公司全称";
            this.columnName.Name = "columnName";
            this.columnName.ReadOnly = true;
            // 
            // columnShortName
            // 
            this.columnShortName.DataPropertyName = "com_short_name";
            this.columnShortName.FillWeight = 107.878F;
            this.columnShortName.HeaderText = "公司简称";
            this.columnShortName.Name = "columnShortName";
            this.columnShortName.ReadOnly = true;
            // 
            // colWXZZ
            // 
            this.colWXZZ.DataPropertyName = "repair_qualification";
            this.colWXZZ.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.colWXZZ.FillWeight = 107.878F;
            this.colWXZZ.HeaderText = "维修资质";
            this.colWXZZ.Name = "colWXZZ";
            this.colWXZZ.ReadOnly = true;
            this.colWXZZ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colWXZZ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colGSXZ
            // 
            this.colGSXZ.DataPropertyName = "unit_properties";
            this.colGSXZ.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.colGSXZ.FillWeight = 107.878F;
            this.colGSXZ.HeaderText = "公司性质";
            this.colGSXZ.Name = "colGSXZ";
            this.colGSXZ.ReadOnly = true;
            this.colGSXZ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colGSXZ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // collegal_person
            // 
            this.collegal_person.DataPropertyName = "legal_person";
            this.collegal_person.FillWeight = 107.878F;
            this.collegal_person.HeaderText = "法人|负责人";
            this.collegal_person.Name = "collegal_person";
            this.collegal_person.ReadOnly = true;
            // 
            // colcom_tel
            // 
            this.colcom_tel.DataPropertyName = "com_tel";
            this.colcom_tel.FillWeight = 107.878F;
            this.colcom_tel.HeaderText = "联系电话";
            this.colcom_tel.Name = "colcom_tel";
            this.colcom_tel.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "status";
            this.colStatus.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.colStatus.FillWeight = 107.878F;
            this.colStatus.HeaderText = "状态";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // Colremark
            // 
            this.Colremark.DataPropertyName = "remark";
            this.Colremark.FillWeight = 107.878F;
            this.Colremark.HeaderText = "备注";
            this.Colremark.Name = "Colremark";
            this.Colremark.ReadOnly = true;
            // 
            // UCCompanyManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.dgvRecord);
            this.Controls.Add(this.menuStrip1);
            this.Name = "UCCompanyManager";
            this.Size = new System.Drawing.Size(1030, 465);
            this.Load += new System.EventHandler(this.UCCompanyManager_Load);
            this.Controls.SetChildIndex(this.menuStrip1, 0);
            this.Controls.SetChildIndex(this.dgvRecord, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.pnlSearch, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx pnlSearch;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TextBoxEx tbCode;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRecord;
        private System.Windows.Forms.Label label6;
        private ServiceStationClient.ComponentUI.TextBoxEx tbPhone;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.TextBoxEx tbName;
        private System.Windows.Forms.Label label4;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbGSXZ;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbWXZZ;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbCountry;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbCity;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbProvince;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ContextMenuStrip dgvMenu;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnShortName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colWXZZ;
        private System.Windows.Forms.DataGridViewComboBoxColumn colGSXZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn collegal_person;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcom_tel;
        private System.Windows.Forms.DataGridViewComboBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colremark;






    }
}
