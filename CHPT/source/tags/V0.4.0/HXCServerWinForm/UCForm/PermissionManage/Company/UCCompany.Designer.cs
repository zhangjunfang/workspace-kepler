namespace HXCServerWinForm.UCForm
{
    partial class UCCompany
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCompany));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.dtpend = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dtpstart = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.cmbTown = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cmbCity = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cmbProvince = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbStatus = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.tbLinkMan = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCompany = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnEdit = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnAdd = new ServiceStationClient.ComponentUI.ButtonEx();
            this.dgvRecord = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.columnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnContact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCreator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.com_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(900, 28);
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.dtpend);
            this.panelEx1.Controls.Add(this.dtpstart);
            this.panelEx1.Controls.Add(this.label8);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.cmbTown);
            this.panelEx1.Controls.Add(this.cmbCity);
            this.panelEx1.Controls.Add(this.cmbProvince);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.cmbStatus);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.tbLinkMan);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.tbCompany);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(0, 30);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(900, 80);
            this.panelEx1.TabIndex = 0;
            // 
            // dtpend
            // 
            this.dtpend.Location = new System.Drawing.Point(253, 44);
            this.dtpend.Name = "dtpend";
            this.dtpend.ShowFormat = "yyyy-MM-dd";
            this.dtpend.Size = new System.Drawing.Size(137, 21);
            this.dtpend.TabIndex = 21;
            // 
            // dtpstart
            // 
            this.dtpstart.Location = new System.Drawing.Point(78, 44);
            this.dtpstart.Name = "dtpstart";
            this.dtpstart.ShowFormat = "yyyy-MM-dd";
            this.dtpstart.Size = new System.Drawing.Size(137, 21);
            this.dtpstart.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(227, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "至";
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(759, 44);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(72, 26);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(759, 9);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(72, 26);
            this.btnClear.TabIndex = 14;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cmbTown
            // 
            this.cmbTown.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTown.FormattingEnabled = true;
            this.cmbTown.Location = new System.Drawing.Point(648, 44);
            this.cmbTown.Name = "cmbTown";
            this.cmbTown.Size = new System.Drawing.Size(81, 22);
            this.cmbTown.TabIndex = 13;
            // 
            // cmbCity
            // 
            this.cmbCity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCity.FormattingEnabled = true;
            this.cmbCity.Location = new System.Drawing.Point(560, 44);
            this.cmbCity.Name = "cmbCity";
            this.cmbCity.Size = new System.Drawing.Size(81, 22);
            this.cmbCity.TabIndex = 12;
            this.cmbCity.SelectedIndexChanged += new System.EventHandler(this.cmbCity_SelectedIndexChanged);
            // 
            // cmbProvince
            // 
            this.cmbProvince.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvince.FormattingEnabled = true;
            this.cmbProvince.Location = new System.Drawing.Point(471, 44);
            this.cmbProvince.Name = "cmbProvince";
            this.cmbProvince.Size = new System.Drawing.Size(81, 22);
            this.cmbProvince.TabIndex = 11;
            this.cmbProvince.SelectedIndexChanged += new System.EventHandler(this.cmbProvince_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(417, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "所在地：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "创建时间：";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(500, 11);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(100, 22);
            this.cmbStatus.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(459, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "状态：";
            // 
            // tbLinkMan
            // 
            this.tbLinkMan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbLinkMan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbLinkMan.BackColor = System.Drawing.Color.Transparent;
            this.tbLinkMan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbLinkMan.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbLinkMan.ForeImage = null;
            this.tbLinkMan.InputtingVerifyCondition = null;
            this.tbLinkMan.Location = new System.Drawing.Point(322, 11);
            this.tbLinkMan.MaxLengh = 32767;
            this.tbLinkMan.Multiline = false;
            this.tbLinkMan.Name = "tbLinkMan";
            this.tbLinkMan.Radius = 3;
            this.tbLinkMan.ReadOnly = false;
            this.tbLinkMan.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbLinkMan.ShowError = false;
            this.tbLinkMan.Size = new System.Drawing.Size(118, 23);
            this.tbLinkMan.TabIndex = 3;
            this.tbLinkMan.UseSystemPasswordChar = false;
            this.tbLinkMan.Value = "";
            this.tbLinkMan.VerifyCondition = null;
            this.tbLinkMan.VerifyType = null;
            this.tbLinkMan.VerifyTypeName = null;
            this.tbLinkMan.WaterMark = null;
            this.tbLinkMan.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "联系人：";
            // 
            // tbCompany
            // 
            this.tbCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbCompany.BackColor = System.Drawing.Color.Transparent;
            this.tbCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbCompany.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbCompany.ForeImage = null;
            this.tbCompany.InputtingVerifyCondition = null;
            this.tbCompany.Location = new System.Drawing.Point(76, 11);
            this.tbCompany.MaxLengh = 32767;
            this.tbCompany.Multiline = false;
            this.tbCompany.Name = "tbCompany";
            this.tbCompany.Radius = 3;
            this.tbCompany.ReadOnly = false;
            this.tbCompany.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbCompany.ShowError = false;
            this.tbCompany.Size = new System.Drawing.Size(173, 23);
            this.tbCompany.TabIndex = 1;
            this.tbCompany.UseSystemPasswordChar = false;
            this.tbCompany.Value = "";
            this.tbCompany.VerifyCondition = null;
            this.tbCompany.VerifyType = null;
            this.tbCompany.VerifyTypeName = null;
            this.tbCompany.WaterMark = null;
            this.tbCompany.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "公司名称：";
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Caption = "删除";
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDelete.DownImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.DownImage")));
            this.btnDelete.Location = new System.Drawing.Point(270, 7);
            this.btnDelete.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.MoveImage")));
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.NormalImage")));
            this.btnDelete.Size = new System.Drawing.Size(60, 26);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.BackgroundImage")));
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEdit.Caption = "编辑";
            this.btnEdit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnEdit.DownImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.DownImage")));
            this.btnEdit.Location = new System.Drawing.Point(203, 7);
            this.btnEdit.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.MoveImage")));
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.NormalImage")));
            this.btnEdit.Size = new System.Drawing.Size(60, 26);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Caption = "新增";
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAdd.DownImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.DownImage")));
            this.btnAdd.Location = new System.Drawing.Point(136, 7);
            this.btnAdd.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.MoveImage")));
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.NormalImage")));
            this.btnAdd.Size = new System.Drawing.Size(60, 26);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvRecord
            // 
            this.dgvRecord.AllowUserToAddRows = false;
            this.dgvRecord.AllowUserToDeleteRows = false;
            this.dgvRecord.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvRecord.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecord.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.columnCode,
            this.columnName,
            this.columnAddress,
            this.columnContact,
            this.ColumnPhone,
            this.columnCreator,
            this.columnCreateTime,
            this.status,
            this.columnRemark,
            this.com_id});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecord.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRecord.EnableHeadersVisualStyles = false;
            this.dgvRecord.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvRecord.IsCheck = true;
            this.dgvRecord.Location = new System.Drawing.Point(-1, 111);
            this.dgvRecord.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvRecord.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvRecord.MergeColumnNames")));
            this.dgvRecord.MultiSelect = false;
            this.dgvRecord.Name = "dgvRecord";
            this.dgvRecord.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecord.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRecord.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvRecord.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvRecord.RowTemplate.Height = 23;
            this.dgvRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecord.ShowCheckBox = true;
            this.dgvRecord.Size = new System.Drawing.Size(901, 288);
            this.dgvRecord.TabIndex = 2;
            this.dgvRecord.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvRecord_CellBeginEdit);
            this.dgvRecord.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRecord_CellFormatting);
            this.dgvRecord.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRecord_CellMouseUp);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 30;
            // 
            // columnCode
            // 
            this.columnCode.DataPropertyName = "com_code";
            this.columnCode.HeaderText = "公司编码";
            this.columnCode.Name = "columnCode";
            this.columnCode.ReadOnly = true;
            this.columnCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.columnCode.Width = 120;
            // 
            // columnName
            // 
            this.columnName.DataPropertyName = "com_name";
            this.columnName.HeaderText = "公司名称";
            this.columnName.Name = "columnName";
            this.columnName.ReadOnly = true;
            this.columnName.Width = 260;
            // 
            // columnAddress
            // 
            this.columnAddress.DataPropertyName = "com_address";
            this.columnAddress.HeaderText = "所在地";
            this.columnAddress.Name = "columnAddress";
            this.columnAddress.ReadOnly = true;
            this.columnAddress.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.columnAddress.Width = 150;
            // 
            // columnContact
            // 
            this.columnContact.DataPropertyName = "com_contact";
            this.columnContact.HeaderText = "联系人";
            this.columnContact.Name = "columnContact";
            this.columnContact.ReadOnly = true;
            this.columnContact.Width = 120;
            // 
            // ColumnPhone
            // 
            this.ColumnPhone.DataPropertyName = "com_tel";
            this.ColumnPhone.HeaderText = "联系电话";
            this.ColumnPhone.Name = "ColumnPhone";
            this.ColumnPhone.ReadOnly = true;
            this.ColumnPhone.Width = 120;
            // 
            // columnCreator
            // 
            this.columnCreator.DataPropertyName = "create_by";
            this.columnCreator.HeaderText = "创建人";
            this.columnCreator.Name = "columnCreator";
            this.columnCreator.ReadOnly = true;
            this.columnCreator.Width = 120;
            // 
            // columnCreateTime
            // 
            this.columnCreateTime.DataPropertyName = "create_time";
            this.columnCreateTime.HeaderText = "创建时间";
            this.columnCreateTime.Name = "columnCreateTime";
            this.columnCreateTime.ReadOnly = true;
            this.columnCreateTime.Width = 150;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.status.Width = 90;
            // 
            // columnRemark
            // 
            this.columnRemark.DataPropertyName = "remark";
            this.columnRemark.HeaderText = "备注";
            this.columnRemark.Name = "columnRemark";
            this.columnRemark.ReadOnly = true;
            this.columnRemark.Width = 300;
            // 
            // com_id
            // 
            this.com_id.DataPropertyName = "com_id";
            this.com_id.HeaderText = "com_id";
            this.com_id.Name = "com_id";
            this.com_id.ReadOnly = true;
            this.com_id.Visible = false;
            // 
            // UCCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.dgvRecord);
            this.DoubleBuffered = true;
            this.Name = "UCCompany";
            this.Size = new System.Drawing.Size(900, 400);
            this.Load += new System.EventHandler(this.UCCompany_Load);
            this.Controls.SetChildIndex(this.dgvRecord, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbTown;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbCity;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbProvince;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbStatus;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.TextBoxEx tbLinkMan;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx tbCompany;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.ButtonEx btnEdit;
        private ServiceStationClient.ComponentUI.ButtonEx btnAdd;
        private ServiceStationClient.ComponentUI.ButtonEx btnDelete;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRecord;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtpend;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtpstart;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnContact;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCreator;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn com_id;


    }
}
