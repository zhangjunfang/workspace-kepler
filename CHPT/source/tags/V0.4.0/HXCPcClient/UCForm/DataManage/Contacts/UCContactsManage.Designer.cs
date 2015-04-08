namespace HXCPcClient.UCForm.DataManage.Contacts
{
    partial class UCContactsManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCContactsManage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlSearch = new ServiceStationClient.ComponentUI.PanelEx();
            this.diCreate = new ServiceStationClient.ComponentUI.DateTimeInterval_sms();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.cboAffiliation = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cboContPost = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cboSex = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtDianHua = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtContName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.dgvContacts = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colChk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colContName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContPost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContTel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataSources = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlSearch.SuspendLayout();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).BeginInit();
            this.cmsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(830, 25);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlSearch.BorderWidth = 1;
            this.pnlSearch.Controls.Add(this.diCreate);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Controls.Add(this.cboAffiliation);
            this.pnlSearch.Controls.Add(this.cboContPost);
            this.pnlSearch.Controls.Add(this.cboSex);
            this.pnlSearch.Controls.Add(this.txtDianHua);
            this.pnlSearch.Controls.Add(this.txtContName);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Curvature = 0;
            this.pnlSearch.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlSearch.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlSearch.Location = new System.Drawing.Point(0, 28);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(824, 132);
            this.pnlSearch.TabIndex = 3;
            // 
            // diCreate
            // 
            this.diCreate.BackColor = System.Drawing.Color.Transparent;
            this.diCreate.customFormat = null;
            this.diCreate.EndDate = "";
            this.diCreate.Location = new System.Drawing.Point(102, 88);
            this.diCreate.Margin = new System.Windows.Forms.Padding(0);
            this.diCreate.Name = "diCreate";
            this.diCreate.Size = new System.Drawing.Size(263, 27);
            this.diCreate.StartDate = "";
            this.diCreate.TabIndex = 18;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(749, 83);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(60, 26);
            this.btnSearch.TabIndex = 17;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(749, 47);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 16;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cboAffiliation
            // 
            this.cboAffiliation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboAffiliation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAffiliation.FormattingEnabled = true;
            this.cboAffiliation.Location = new System.Drawing.Point(352, 56);
            this.cboAffiliation.Name = "cboAffiliation";
            this.cboAffiliation.Size = new System.Drawing.Size(121, 22);
            this.cboAffiliation.TabIndex = 15;
            this.cboAffiliation.Visible = false;
            // 
            // cboContPost
            // 
            this.cboContPost.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboContPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboContPost.FormattingEnabled = true;
            this.cboContPost.Location = new System.Drawing.Point(603, 18);
            this.cboContPost.Name = "cboContPost";
            this.cboContPost.Size = new System.Drawing.Size(121, 22);
            this.cboContPost.TabIndex = 14;
            // 
            // cboSex
            // 
            this.cboSex.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSex.FormattingEnabled = true;
            this.cboSex.Location = new System.Drawing.Point(352, 18);
            this.cboSex.Name = "cboSex";
            this.cboSex.Size = new System.Drawing.Size(121, 22);
            this.cboSex.TabIndex = 12;
            // 
            // txtDianHua
            // 
            this.txtDianHua.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtDianHua.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtDianHua.BackColor = System.Drawing.Color.Transparent;
            this.txtDianHua.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtDianHua.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtDianHua.ForeImage = null;
            this.txtDianHua.InputtingVerifyCondition = null;
            this.txtDianHua.Location = new System.Drawing.Point(102, 56);
            this.txtDianHua.MaxLengh = 11;
            this.txtDianHua.Multiline = false;
            this.txtDianHua.Name = "txtDianHua";
            this.txtDianHua.Radius = 3;
            this.txtDianHua.ReadOnly = false;
            this.txtDianHua.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtDianHua.ShowError = false;
            this.txtDianHua.Size = new System.Drawing.Size(119, 23);
            this.txtDianHua.TabIndex = 9;
            this.txtDianHua.UseSystemPasswordChar = false;
            this.txtDianHua.Value = "";
            this.txtDianHua.VerifyCondition = null;
            this.txtDianHua.VerifyType = null;
            this.txtDianHua.VerifyTypeName = null;
            this.txtDianHua.WaterMark = null;
            this.txtDianHua.WaterMarkColor = System.Drawing.Color.Silver;
            this.txtDianHua.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDianHua_KeyPress);
            // 
            // txtContName
            // 
            this.txtContName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContName.BackColor = System.Drawing.Color.Transparent;
            this.txtContName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContName.ForeImage = null;
            this.txtContName.InputtingVerifyCondition = null;
            this.txtContName.Location = new System.Drawing.Point(102, 18);
            this.txtContName.MaxLengh = 32767;
            this.txtContName.Multiline = false;
            this.txtContName.Name = "txtContName";
            this.txtContName.Radius = 3;
            this.txtContName.ReadOnly = false;
            this.txtContName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContName.ShowError = false;
            this.txtContName.Size = new System.Drawing.Size(119, 23);
            this.txtContName.TabIndex = 8;
            this.txtContName.UseSystemPasswordChar = false;
            this.txtContName.Value = "";
            this.txtContName.VerifyCondition = null;
            this.txtContName.VerifyType = null;
            this.txtContName.VerifyTypeName = null;
            this.txtContName.WaterMark = null;
            this.txtContName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(281, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "归属类型：";
            this.label7.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(556, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "职务：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(305, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "性别：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "创建时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "电话：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "姓名：";
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Location = new System.Drawing.Point(3, 177);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(824, 263);
            this.tabControlEx1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.page);
            this.tabPage1.Controls.Add(this.dgvContacts);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(816, 233);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "联系人列表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // page
            // 
            this.page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.page.Location = new System.Drawing.Point(330, 197);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(480, 31);
            this.page.TabIndex = 8;
            this.page.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            // 
            // dgvContacts
            // 
            this.dgvContacts.AllowUserToAddRows = false;
            this.dgvContacts.AllowUserToDeleteRows = false;
            this.dgvContacts.AllowUserToResizeColumns = false;
            this.dgvContacts.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvContacts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvContacts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvContacts.BackgroundColor = System.Drawing.Color.White;
            this.dgvContacts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvContacts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContacts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChk,
            this.colContName,
            this.colSex,
            this.colContPost,
            this.colNation,
            this.colContPhone,
            this.colContTel,
            this.colDataSources,
            this.colCreateTime,
            this.colStatus,
            this.colContID});
            this.dgvContacts.ContextMenuStrip = this.cmsMenu;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvContacts.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvContacts.EnableHeadersVisualStyles = false;
            this.dgvContacts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvContacts.IsCheck = true;
            this.dgvContacts.Location = new System.Drawing.Point(3, 3);
            this.dgvContacts.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvContacts.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvContacts.MergeColumnNames")));
            this.dgvContacts.MultiSelect = false;
            this.dgvContacts.Name = "dgvContacts";
            this.dgvContacts.ReadOnly = true;
            this.dgvContacts.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvContacts.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvContacts.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvContacts.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvContacts.RowTemplate.Height = 23;
            this.dgvContacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContacts.ShowCheckBox = true;
            this.dgvContacts.Size = new System.Drawing.Size(810, 227);
            this.dgvContacts.TabIndex = 0;
            this.dgvContacts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvContacts_CellContentClick);
            this.dgvContacts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvContacts_CellDoubleClick);
            this.dgvContacts.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvContacts_CellMouseClick);
            // 
            // colChk
            // 
            this.colChk.FillWeight = 37.08282F;
            this.colChk.HeaderText = "";
            this.colChk.MinimumWidth = 30;
            this.colChk.Name = "colChk";
            this.colChk.ReadOnly = true;
            // 
            // colContName
            // 
            this.colContName.DataPropertyName = "cont_name";
            this.colContName.FillWeight = 106.9908F;
            this.colContName.HeaderText = "姓名";
            this.colContName.Name = "colContName";
            this.colContName.ReadOnly = true;
            this.colContName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colContName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colSex
            // 
            this.colSex.DataPropertyName = "sex";
            this.colSex.FillWeight = 106.9908F;
            this.colSex.HeaderText = "性别";
            this.colSex.Name = "colSex";
            this.colSex.ReadOnly = true;
            this.colSex.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colContPost
            // 
            this.colContPost.DataPropertyName = "cont_post";
            this.colContPost.FillWeight = 106.9908F;
            this.colContPost.HeaderText = "职务";
            this.colContPost.Name = "colContPost";
            this.colContPost.ReadOnly = true;
            this.colContPost.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colContPost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colNation
            // 
            this.colNation.DataPropertyName = "nation";
            this.colNation.FillWeight = 106.9908F;
            this.colNation.HeaderText = "民族";
            this.colNation.Name = "colNation";
            this.colNation.ReadOnly = true;
            this.colNation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colNation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colContPhone
            // 
            this.colContPhone.DataPropertyName = "cont_phone";
            this.colContPhone.FillWeight = 106.9908F;
            this.colContPhone.HeaderText = "手机";
            this.colContPhone.Name = "colContPhone";
            this.colContPhone.ReadOnly = true;
            this.colContPhone.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colContPhone.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colContTel
            // 
            this.colContTel.DataPropertyName = "cont_tel";
            this.colContTel.FillWeight = 106.9908F;
            this.colContTel.HeaderText = "固话";
            this.colContTel.Name = "colContTel";
            this.colContTel.ReadOnly = true;
            // 
            // colDataSources
            // 
            this.colDataSources.FillWeight = 106.9908F;
            this.colDataSources.HeaderText = "数据来源";
            this.colDataSources.Name = "colDataSources";
            this.colDataSources.ReadOnly = true;
            // 
            // colCreateTime
            // 
            this.colCreateTime.DataPropertyName = "create_time";
            this.colCreateTime.FillWeight = 106.9908F;
            this.colCreateTime.HeaderText = "创建时间";
            this.colCreateTime.Name = "colCreateTime";
            this.colCreateTime.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.FillWeight = 106.9908F;
            this.colStatus.HeaderText = "状态";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colContID
            // 
            this.colContID.DataPropertyName = "cont_id";
            this.colContID.HeaderText = "ID";
            this.colContID.Name = "colContID";
            this.colContID.ReadOnly = true;
            this.colContID.Visible = false;
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiView});
            this.cmsMenu.Name = "cmsMent";
            this.cmsMenu.Size = new System.Drawing.Size(101, 26);
            // 
            // tsmiView
            // 
            this.tsmiView.Name = "tsmiView";
            this.tsmiView.Size = new System.Drawing.Size(100, 22);
            this.tsmiView.Text = "查看";
            this.tsmiView.Click += new System.EventHandler(this.tsmiView_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(100, 22);
            this.tsmiEdit.Text = "编辑";
            this.tsmiEdit.Click += new System.EventHandler(this.tsmiEdit_Click);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(100, 22);
            this.tsmiCopy.Text = "复制";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(100, 22);
            this.tsmiDelete.Text = "删除";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // UCContactsManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.tabControlEx1);
            this.Name = "UCContactsManage";
            this.Size = new System.Drawing.Size(830, 472);
            this.Load += new System.EventHandler(this.UCContactsManage_Load);
            this.Controls.SetChildIndex(this.tabControlEx1, 0);
            this.Controls.SetChildIndex(this.pnlSearch, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).EndInit();
            this.cmsMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx pnlSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboAffiliation;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboContPost;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboSex;
        private ServiceStationClient.ComponentUI.TextBoxEx txtDianHua;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvContacts;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiView;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private ServiceStationClient.ComponentUI.DateTimeInterval_sms diCreate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChk;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContPost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContTel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataSources;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContID;
    }
}
