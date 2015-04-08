namespace HXCServerWinForm.UCForm.OnlineQuery
{
    partial class UCLoginLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCLoginLog));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.txtname = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.cmbcom = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cmbrole = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cmborg = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cmbacc = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.dtploginend = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dtpexitend = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dtploginstart = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dtpexitstart = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.pnlBottom = new ServiceStationClient.ComponentUI.PanelEx();
            this.dgvUser = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.land_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.role_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.org_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.com_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.login_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exit_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.computer_ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.computer_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.computer_mac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1000, 28);
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.Controls.Add(this.txtname);
            this.panelEx1.Controls.Add(this.cmbcom);
            this.panelEx1.Controls.Add(this.cmbrole);
            this.panelEx1.Controls.Add(this.cmborg);
            this.panelEx1.Controls.Add(this.cmbacc);
            this.panelEx1.Controls.Add(this.dtploginend);
            this.panelEx1.Controls.Add(this.dtpexitend);
            this.panelEx1.Controls.Add(this.dtploginstart);
            this.panelEx1.Controls.Add(this.dtpexitstart);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.label12);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.label11);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 31);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1000, 99);
            this.panelEx1.TabIndex = 10;
            // 
            // txtname
            // 
            this.txtname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtname.BackColor = System.Drawing.Color.Transparent;
            this.txtname.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtname.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtname.ForeImage = null;
            this.txtname.Location = new System.Drawing.Point(724, 49);
            this.txtname.MaxLengh = 32767;
            this.txtname.Multiline = false;
            this.txtname.Name = "txtname";
            this.txtname.Radius = 3;
            this.txtname.ReadOnly = false;
            this.txtname.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtname.Size = new System.Drawing.Size(120, 23);
            this.txtname.TabIndex = 29;
            this.txtname.UseSystemPasswordChar = false;
            this.txtname.WaterMark = null;
            this.txtname.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // cmbcom
            // 
            this.cmbcom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbcom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcom.FormattingEnabled = true;
            this.cmbcom.Location = new System.Drawing.Point(258, 15);
            this.cmbcom.Name = "cmbcom";
            this.cmbcom.Size = new System.Drawing.Size(183, 22);
            this.cmbcom.TabIndex = 28;
            // 
            // cmbrole
            // 
            this.cmbrole.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbrole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbrole.FormattingEnabled = true;
            this.cmbrole.Location = new System.Drawing.Point(723, 15);
            this.cmbrole.Name = "cmbrole";
            this.cmbrole.Size = new System.Drawing.Size(121, 22);
            this.cmbrole.TabIndex = 28;
            // 
            // cmborg
            // 
            this.cmborg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmborg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmborg.FormattingEnabled = true;
            this.cmborg.Location = new System.Drawing.Point(530, 15);
            this.cmborg.Name = "cmborg";
            this.cmborg.Size = new System.Drawing.Size(121, 22);
            this.cmborg.TabIndex = 28;
            // 
            // cmbacc
            // 
            this.cmbacc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbacc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbacc.FormattingEnabled = true;
            this.cmbacc.Items.AddRange(new object[] {
            "不限"});
            this.cmbacc.Location = new System.Drawing.Point(78, 15);
            this.cmbacc.Name = "cmbacc";
            this.cmbacc.Size = new System.Drawing.Size(121, 22);
            this.cmbacc.TabIndex = 28;
            this.cmbacc.SelectedIndexChanged += new System.EventHandler(this.cmbacc_SelectedIndexChanged);
            // 
            // dtploginend
            // 
            this.dtploginend.Location = new System.Drawing.Point(200, 49);
            this.dtploginend.Name = "dtploginend";
            this.dtploginend.ShowFormat = "yyyy-MM-dd";
            this.dtploginend.Size = new System.Drawing.Size(116, 21);
            this.dtploginend.TabIndex = 27;
            // 
            // dtpexitend
            // 
            this.dtpexitend.Location = new System.Drawing.Point(520, 49);
            this.dtpexitend.Name = "dtpexitend";
            this.dtpexitend.ShowFormat = "yyyy-MM-dd";
            this.dtpexitend.Size = new System.Drawing.Size(116, 21);
            this.dtpexitend.TabIndex = 27;
            // 
            // dtploginstart
            // 
            this.dtploginstart.Location = new System.Drawing.Point(78, 49);
            this.dtploginstart.Name = "dtploginstart";
            this.dtploginstart.ShowFormat = "yyyy-MM-dd";
            this.dtploginstart.Size = new System.Drawing.Size(99, 21);
            this.dtploginstart.TabIndex = 27;
            // 
            // dtpexitstart
            // 
            this.dtpexitstart.Location = new System.Drawing.Point(398, 49);
            this.dtpexitstart.Name = "dtpexitstart";
            this.dtpexitstart.ShowFormat = "yyyy-MM-dd";
            this.dtpexitstart.Size = new System.Drawing.Size(99, 21);
            this.dtpexitstart.TabIndex = 27;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(856, 49);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(76, 26);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(181, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "至 ";
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(856, 13);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(76, 26);
            this.btnClear.TabIndex = 12;
            this.btnClear.Tag = "1";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(501, 53);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 12);
            this.label12.TabIndex = 11;
            this.label12.Text = "至 ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "登录时间：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(676, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "用户名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(676, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "角色：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(335, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 10;
            this.label11.Text = "退出时间：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(483, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "组织：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "公司：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "帐套：";
            // 
            // page
            // 
            this.page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.page.Location = new System.Drawing.Point(517, 6);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(480, 31);
            this.page.TabIndex = 9;
            this.page.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlBottom.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlBottom.Controls.Add(this.page);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 457);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1000, 43);
            this.pnlBottom.TabIndex = 12;
            // 
            // dgvUser
            // 
            this.dgvUser.AllowUserToAddRows = false;
            this.dgvUser.AllowUserToDeleteRows = false;
            this.dgvUser.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvUser.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUser.BackgroundColor = System.Drawing.Color.White;
            this.dgvUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUser.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.land_name,
            this.user_code,
            this.user_name,
            this.role_name,
            this.org_name,
            this.com_name,
            this.accname,
            this.login_time,
            this.exit_time,
            this.computer_ip,
            this.computer_name,
            this.computer_mac,
            this.user_id});
            this.dgvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUser.EnableHeadersVisualStyles = false;
            this.dgvUser.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvUser.Location = new System.Drawing.Point(0, 130);
            this.dgvUser.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvUser.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvUser.MergeColumnNames")));
            this.dgvUser.MultiSelect = false;
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.ReadOnly = true;
            this.dgvUser.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvUser.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvUser.RowTemplate.Height = 23;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.ShowCheckBox = true;
            this.dgvUser.Size = new System.Drawing.Size(1000, 327);
            this.dgvUser.TabIndex = 13;
            this.dgvUser.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvUser_CellFormatting);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 18;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCheck.Width = 26;
            // 
            // land_name
            // 
            this.land_name.DataPropertyName = "land_name";
            this.land_name.HeaderText = "登录账号";
            this.land_name.Name = "land_name";
            this.land_name.ReadOnly = true;
            // 
            // user_code
            // 
            this.user_code.DataPropertyName = "user_code";
            this.user_code.HeaderText = "用户编码";
            this.user_code.Name = "user_code";
            this.user_code.ReadOnly = true;
            // 
            // user_name
            // 
            this.user_name.DataPropertyName = "user_name";
            this.user_name.HeaderText = "用户名";
            this.user_name.Name = "user_name";
            this.user_name.ReadOnly = true;
            // 
            // role_name
            // 
            this.role_name.DataPropertyName = "role_name";
            this.role_name.HeaderText = "角色";
            this.role_name.Name = "role_name";
            this.role_name.ReadOnly = true;
            // 
            // org_name
            // 
            this.org_name.DataPropertyName = "org_name";
            this.org_name.HeaderText = "所属组织";
            this.org_name.Name = "org_name";
            this.org_name.ReadOnly = true;
            // 
            // com_name
            // 
            this.com_name.DataPropertyName = "com_name";
            this.com_name.HeaderText = "所属公司";
            this.com_name.Name = "com_name";
            this.com_name.ReadOnly = true;
            // 
            // accname
            // 
            this.accname.DataPropertyName = "AccName";
            this.accname.HeaderText = "所属帐套";
            this.accname.Name = "accname";
            this.accname.ReadOnly = true;
            // 
            // login_time
            // 
            this.login_time.DataPropertyName = "login_time";
            this.login_time.HeaderText = "登录时间";
            this.login_time.Name = "login_time";
            this.login_time.ReadOnly = true;
            this.login_time.Width = 120;
            // 
            // exit_time
            // 
            this.exit_time.DataPropertyName = "exit_time";
            this.exit_time.HeaderText = "退出时间";
            this.exit_time.Name = "exit_time";
            this.exit_time.ReadOnly = true;
            this.exit_time.Width = 120;
            // 
            // computer_ip
            // 
            this.computer_ip.DataPropertyName = "computer_ip";
            this.computer_ip.HeaderText = "计算机IP";
            this.computer_ip.Name = "computer_ip";
            this.computer_ip.ReadOnly = true;
            // 
            // computer_name
            // 
            this.computer_name.DataPropertyName = "computer_name";
            this.computer_name.HeaderText = "计算机名";
            this.computer_name.Name = "computer_name";
            this.computer_name.ReadOnly = true;
            // 
            // computer_mac
            // 
            this.computer_mac.DataPropertyName = "computer_mac";
            this.computer_mac.HeaderText = "计算机MAC";
            this.computer_mac.Name = "computer_mac";
            this.computer_mac.ReadOnly = true;
            this.computer_mac.Width = 200;
            // 
            // user_id
            // 
            this.user_id.DataPropertyName = "user_id";
            this.user_id.HeaderText = "user_id";
            this.user_id.Name = "user_id";
            this.user_id.ReadOnly = true;
            this.user_id.Visible = false;
            // 
            // UCLoginLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvUser);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.panelEx1);
            this.Name = "UCLoginLog";
            this.Size = new System.Drawing.Size(1000, 500);
            this.Load += new System.EventHandler(this.UCOnLineUser_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.pnlBottom, 0);
            this.Controls.SetChildIndex(this.dgvUser, 0);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtpexitend;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtpexitstart;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private ServiceStationClient.ComponentUI.TextBoxEx txtname;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbcom;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbrole;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmborg;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbacc;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtploginend;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtploginstart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.PanelEx pnlBottom;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvUser;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn land_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn role_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn com_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn accname;
        private System.Windows.Forms.DataGridViewTextBoxColumn login_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn exit_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn computer_ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn computer_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn computer_mac;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_id;

    }
}
