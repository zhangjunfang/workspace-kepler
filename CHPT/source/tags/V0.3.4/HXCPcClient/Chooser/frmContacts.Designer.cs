namespace HXCPcClient.Chooser
{
    partial class frmContacts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmContacts));
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.cbo_data_source = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.txtTel = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.cboSex = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvContacts = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colContID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContPost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContTel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cont_crm_guid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.btnAdd = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnOK = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnColse = new ServiceStationClient.ComponentUI.ButtonEx();
            this.pnlContainer.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.page);
            this.pnlContainer.Controls.Add(this.btnAdd);
            this.pnlContainer.Controls.Add(this.btnOK);
            this.pnlContainer.Controls.Add(this.btnColse);
            this.pnlContainer.Controls.Add(this.tabControlEx1);
            this.pnlContainer.Controls.Add(this.panelEx1);
            this.pnlContainer.Size = new System.Drawing.Size(719, 489);
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.cbo_data_source);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.txtTel);
            this.panelEx1.Controls.Add(this.txtName);
            this.panelEx1.Controls.Add(this.cboSex);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(3, 3);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(713, 106);
            this.panelEx1.TabIndex = 0;
            // 
            // cbo_data_source
            // 
            this.cbo_data_source.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_data_source.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_data_source.FormattingEnabled = true;
            this.cbo_data_source.Location = new System.Drawing.Point(105, 65);
            this.cbo_data_source.Name = "cbo_data_source";
            this.cbo_data_source.Size = new System.Drawing.Size(154, 22);
            this.cbo_data_source.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(584, 67);
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
            this.btnClear.Location = new System.Drawing.Point(584, 26);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 8;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtTel
            // 
            this.txtTel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtTel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtTel.BackColor = System.Drawing.Color.Transparent;
            this.txtTel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtTel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtTel.ForeImage = null;
            this.txtTel.Location = new System.Drawing.Point(343, 61);
            this.txtTel.MaxLengh = 32767;
            this.txtTel.Multiline = false;
            this.txtTel.Name = "txtTel";
            this.txtTel.Radius = 3;
            this.txtTel.ReadOnly = false;
            this.txtTel.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtTel.ShowError = false;
            this.txtTel.Size = new System.Drawing.Size(121, 23);
            this.txtTel.TabIndex = 7;
            this.txtTel.UseSystemPasswordChar = false;
            this.txtTel.Value = "";
            this.txtTel.VerifyCondition = null;
            this.txtTel.VerifyType = null;
            this.txtTel.WaterMark = null;
            this.txtTel.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtName
            // 
            this.txtName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtName.BackColor = System.Drawing.Color.Transparent;
            this.txtName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtName.ForeImage = null;
            this.txtName.Location = new System.Drawing.Point(105, 22);
            this.txtName.MaxLengh = 32767;
            this.txtName.Multiline = false;
            this.txtName.Name = "txtName";
            this.txtName.Radius = 3;
            this.txtName.ReadOnly = false;
            this.txtName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtName.ShowError = false;
            this.txtName.Size = new System.Drawing.Size(154, 23);
            this.txtName.TabIndex = 5;
            this.txtName.UseSystemPasswordChar = false;
            this.txtName.Value = "";
            this.txtName.VerifyCondition = null;
            this.txtName.VerifyType = null;
            this.txtName.WaterMark = null;
            this.txtName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // cboSex
            // 
            this.cboSex.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSex.FormattingEnabled = true;
            this.cboSex.Location = new System.Drawing.Point(343, 22);
            this.cboSex.Name = "cboSex";
            this.cboSex.Size = new System.Drawing.Size(121, 22);
            this.cboSex.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(296, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "电话：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(296, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "性别：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据来源：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "姓名：";
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Location = new System.Drawing.Point(11, 115);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(696, 268);
            this.tabControlEx1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvContacts);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(688, 238);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "联系人列表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvContacts
            // 
            this.dgvContacts.AllowUserToAddRows = false;
            this.dgvContacts.AllowUserToDeleteRows = false;
            this.dgvContacts.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvContacts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvContacts.BackgroundColor = System.Drawing.Color.White;
            this.dgvContacts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvContacts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContacts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colContID,
            this.colContName,
            this.colSex,
            this.colContPost,
            this.colNation,
            this.colContPhone,
            this.colContTel,
            this.col_cont_crm_guid,
            this.colCreateTime});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvContacts.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvContacts.EnableHeadersVisualStyles = false;
            this.dgvContacts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvContacts.Location = new System.Drawing.Point(0, 3);
            this.dgvContacts.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvContacts.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvContacts.MergeColumnNames")));
            this.dgvContacts.MultiSelect = false;
            this.dgvContacts.Name = "dgvContacts";
            this.dgvContacts.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvContacts.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvContacts.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvContacts.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvContacts.RowTemplate.Height = 23;
            this.dgvContacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContacts.ShowCheckBox = true;
            this.dgvContacts.Size = new System.Drawing.Size(685, 235);
            this.dgvContacts.TabIndex = 0;
            this.dgvContacts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvContacts_CellDoubleClick);
            // 
            // colContID
            // 
            this.colContID.HeaderText = "ID";
            this.colContID.Name = "colContID";
            this.colContID.ReadOnly = true;
            this.colContID.Visible = false;
            // 
            // colContName
            // 
            this.colContName.HeaderText = "姓名";
            this.colContName.Name = "colContName";
            this.colContName.ReadOnly = true;
            // 
            // colSex
            // 
            this.colSex.HeaderText = "性别";
            this.colSex.Name = "colSex";
            this.colSex.ReadOnly = true;
            // 
            // colContPost
            // 
            this.colContPost.HeaderText = "职务";
            this.colContPost.Name = "colContPost";
            this.colContPost.ReadOnly = true;
            // 
            // colNation
            // 
            this.colNation.HeaderText = "民族";
            this.colNation.Name = "colNation";
            this.colNation.ReadOnly = true;
            // 
            // colContPhone
            // 
            this.colContPhone.HeaderText = "手机";
            this.colContPhone.Name = "colContPhone";
            this.colContPhone.ReadOnly = true;
            // 
            // colContTel
            // 
            this.colContTel.HeaderText = "固话";
            this.colContTel.Name = "colContTel";
            this.colContTel.ReadOnly = true;
            // 
            // col_cont_crm_guid
            // 
            this.col_cont_crm_guid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_cont_crm_guid.DataPropertyName = "cont_crm_guid";
            this.col_cont_crm_guid.HeaderText = "CRM联系人GUID";
            this.col_cont_crm_guid.Name = "col_cont_crm_guid";
            this.col_cont_crm_guid.ReadOnly = true;
            this.col_cont_crm_guid.Visible = false;
            this.col_cont_crm_guid.Width = 150;
            // 
            // colCreateTime
            // 
            this.colCreateTime.HeaderText = "创建时间";
            this.colCreateTime.Name = "colCreateTime";
            this.colCreateTime.ReadOnly = true;
            // 
            // page
            // 
            this.page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.page.Location = new System.Drawing.Point(243, 389);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(471, 37);
            this.page.TabIndex = 7;
            this.page.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Caption = "添加联系人";
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAdd.DownImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.DownImage")));
            this.btnAdd.Location = new System.Drawing.Point(11, 432);
            this.btnAdd.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.MoveImage")));
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.NormalImage")));
            this.btnAdd.Size = new System.Drawing.Size(116, 26);
            this.btnAdd.TabIndex = 31;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.Caption = "确定";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.DownImage = ((System.Drawing.Image)(resources.GetObject("btnOK.DownImage")));
            this.btnOK.Location = new System.Drawing.Point(529, 432);
            this.btnOK.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnOK.MoveImage")));
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnOK.NormalImage")));
            this.btnOK.Size = new System.Drawing.Size(60, 26);
            this.btnOK.TabIndex = 30;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnColse
            // 
            this.btnColse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnColse.BackgroundImage")));
            this.btnColse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnColse.Caption = "关闭";
            this.btnColse.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnColse.DownImage = ((System.Drawing.Image)(resources.GetObject("btnColse.DownImage")));
            this.btnColse.Location = new System.Drawing.Point(647, 432);
            this.btnColse.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnColse.MoveImage")));
            this.btnColse.Name = "btnColse";
            this.btnColse.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnColse.NormalImage")));
            this.btnColse.Size = new System.Drawing.Size(60, 26);
            this.btnColse.TabIndex = 29;
            this.btnColse.Click += new System.EventHandler(this.btnColse_Click);
            // 
            // frmContacts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(720, 520);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmContacts";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "联系人选择";
            this.pnlContainer.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.TextBoxEx txtTel;
        private ServiceStationClient.ComponentUI.TextBoxEx txtName;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboSex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvContacts;
        public ServiceStationClient.ComponentUI.ComboBoxEx cbo_data_source;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContPost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContTel;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cont_crm_guid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreateTime;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private ServiceStationClient.ComponentUI.ButtonEx btnAdd;
        private ServiceStationClient.ComponentUI.ButtonEx btnOK;
        private ServiceStationClient.ComponentUI.ButtonEx btnColse;
    }
}