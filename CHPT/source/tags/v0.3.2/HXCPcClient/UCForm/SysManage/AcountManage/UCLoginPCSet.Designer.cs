namespace HXCPcClient.UCForm.SysManage.AcountManage
{
    partial class UCLoginPCSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCLoginPCSet));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbMonday = new System.Windows.Forms.CheckBox();
            this.dgvPCList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.login_pc_set_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pc_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mac_address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_allow_login = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.cmbis_allow_login = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtpc_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtmac_address = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtremark_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnEdit = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnForbid = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnCancel = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnSave = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnAllow = new ServiceStationClient.ComponentUI.ButtonEx();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPCList)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbMonday
            // 
            this.cbMonday.AutoSize = true;
            this.cbMonday.Location = new System.Drawing.Point(379, 7);
            this.cbMonday.Name = "cbMonday";
            this.cbMonday.Size = new System.Drawing.Size(114, 16);
            this.cbMonday.TabIndex = 30;
            this.cbMonday.Text = "开启mac地址绑定";
            this.cbMonday.UseVisualStyleBackColor = true;
            // 
            // dgvPCList
            // 
            this.dgvPCList.AllowUserToAddRows = false;
            this.dgvPCList.AllowUserToDeleteRows = false;
            this.dgvPCList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvPCList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPCList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPCList.BackgroundColor = System.Drawing.Color.White;
            this.dgvPCList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPCList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPCList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPCList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.login_pc_set_id,
            this.remark_name,
            this.pc_name,
            this.mac_address,
            this.is_allow_login,
            this.Column1});
            this.dgvPCList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvPCList.EnableHeadersVisualStyles = false;
            this.dgvPCList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvPCList.Location = new System.Drawing.Point(2, 130);
            this.dgvPCList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvPCList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvPCList.MergeColumnNames")));
            this.dgvPCList.MultiSelect = false;
            this.dgvPCList.Name = "dgvPCList";
            this.dgvPCList.ReadOnly = true;
            this.dgvPCList.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvPCList.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPCList.RowTemplate.Height = 23;
            this.dgvPCList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPCList.ShowCheckBox = true;
            this.dgvPCList.Size = new System.Drawing.Size(892, 335);
            this.dgvPCList.TabIndex = 33;
            this.dgvPCList.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvPCList_CellBeginEdit);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 18;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCheck.Width = 30;
            // 
            // login_pc_set_id
            // 
            this.login_pc_set_id.DataPropertyName = "login_pc_set_id";
            this.login_pc_set_id.HeaderText = "ID";
            this.login_pc_set_id.Name = "login_pc_set_id";
            this.login_pc_set_id.ReadOnly = true;
            this.login_pc_set_id.Visible = false;
            // 
            // remark_name
            // 
            this.remark_name.DataPropertyName = "remark_name";
            this.remark_name.HeaderText = "备注名称";
            this.remark_name.Name = "remark_name";
            this.remark_name.ReadOnly = true;
            this.remark_name.Width = 200;
            // 
            // pc_name
            // 
            this.pc_name.DataPropertyName = "pc_name";
            this.pc_name.HeaderText = "计算机名";
            this.pc_name.Name = "pc_name";
            this.pc_name.ReadOnly = true;
            this.pc_name.Width = 200;
            // 
            // mac_address
            // 
            this.mac_address.DataPropertyName = "mac_address";
            this.mac_address.HeaderText = "MAC地址";
            this.mac_address.Name = "mac_address";
            this.mac_address.ReadOnly = true;
            this.mac_address.Width = 200;
            // 
            // is_allow_login
            // 
            this.is_allow_login.DataPropertyName = "is_allow_login";
            this.is_allow_login.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.is_allow_login.HeaderText = "是否允许登陆";
            this.is_allow_login.Name = "is_allow_login";
            this.is_allow_login.ReadOnly = true;
            this.is_allow_login.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.is_allow_login.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.is_allow_login.Width = 120;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.Controls.Add(this.cmbis_allow_login);
            this.panelEx1.Controls.Add(this.txtpc_name);
            this.panelEx1.Controls.Add(this.txtmac_address);
            this.panelEx1.Controls.Add(this.txtremark_name);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Location = new System.Drawing.Point(0, 31);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(894, 98);
            this.panelEx1.TabIndex = 34;
            // 
            // cmbis_allow_login
            // 
            this.cmbis_allow_login.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbis_allow_login.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbis_allow_login.FormattingEnabled = true;
            this.cmbis_allow_login.Location = new System.Drawing.Point(453, 49);
            this.cmbis_allow_login.Name = "cmbis_allow_login";
            this.cmbis_allow_login.Size = new System.Drawing.Size(230, 22);
            this.cmbis_allow_login.TabIndex = 21;
            // 
            // txtpc_name
            // 
            this.txtpc_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtpc_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtpc_name.BackColor = System.Drawing.Color.Transparent;
            this.txtpc_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtpc_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtpc_name.ForeImage = null;
            this.txtpc_name.Location = new System.Drawing.Point(453, 14);
            this.txtpc_name.MaxLengh = 32767;
            this.txtpc_name.Multiline = false;
            this.txtpc_name.Name = "txtpc_name";
            this.txtpc_name.Radius = 3;
            this.txtpc_name.ReadOnly = false;
            this.txtpc_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtpc_name.Size = new System.Drawing.Size(230, 23);
            this.txtpc_name.TabIndex = 20;
            this.txtpc_name.UseSystemPasswordChar = false;
            this.txtpc_name.WaterMark = null;
            this.txtpc_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtmac_address
            // 
            this.txtmac_address.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtmac_address.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtmac_address.BackColor = System.Drawing.Color.Transparent;
            this.txtmac_address.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtmac_address.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtmac_address.ForeImage = null;
            this.txtmac_address.Location = new System.Drawing.Point(82, 49);
            this.txtmac_address.MaxLengh = 32767;
            this.txtmac_address.Multiline = false;
            this.txtmac_address.Name = "txtmac_address";
            this.txtmac_address.Radius = 3;
            this.txtmac_address.ReadOnly = false;
            this.txtmac_address.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtmac_address.Size = new System.Drawing.Size(230, 23);
            this.txtmac_address.TabIndex = 19;
            this.txtmac_address.UseSystemPasswordChar = false;
            this.txtmac_address.WaterMark = null;
            this.txtmac_address.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtremark_name
            // 
            this.txtremark_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtremark_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtremark_name.BackColor = System.Drawing.Color.Transparent;
            this.txtremark_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtremark_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtremark_name.ForeImage = null;
            this.txtremark_name.Location = new System.Drawing.Point(82, 14);
            this.txtremark_name.MaxLengh = 32767;
            this.txtremark_name.Multiline = false;
            this.txtremark_name.Name = "txtremark_name";
            this.txtremark_name.Radius = 3;
            this.txtremark_name.ReadOnly = false;
            this.txtremark_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtremark_name.Size = new System.Drawing.Size(230, 23);
            this.txtremark_name.TabIndex = 19;
            this.txtremark_name.UseSystemPasswordChar = false;
            this.txtremark_name.WaterMark = null;
            this.txtremark_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(748, 48);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(76, 26);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(748, 12);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(76, 26);
            this.btnClear.TabIndex = 12;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(364, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "是否允许登录：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "MAC地址：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(376, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "计算机名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "备注名称：";
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Caption = "新增";
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAdd.DownImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.DownImage")));
            this.btnAdd.Location = new System.Drawing.Point(4, 3);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(1);
            this.btnAdd.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.MoveImage")));
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.NormalImage")));
            this.btnAdd.Size = new System.Drawing.Size(60, 23);
            this.btnAdd.TabIndex = 36;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.BackgroundImage")));
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEdit.Caption = "编辑";
            this.btnEdit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnEdit.DownImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.DownImage")));
            this.btnEdit.Location = new System.Drawing.Point(65, 3);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(1);
            this.btnEdit.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.MoveImage")));
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.NormalImage")));
            this.btnEdit.Size = new System.Drawing.Size(60, 23);
            this.btnEdit.TabIndex = 37;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnForbid
            // 
            this.btnForbid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnForbid.BackgroundImage")));
            this.btnForbid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnForbid.Caption = "禁止";
            this.btnForbid.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnForbid.DownImage = ((System.Drawing.Image)(resources.GetObject("btnForbid.DownImage")));
            this.btnForbid.Location = new System.Drawing.Point(126, 3);
            this.btnForbid.Margin = new System.Windows.Forms.Padding(1);
            this.btnForbid.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnForbid.MoveImage")));
            this.btnForbid.Name = "btnForbid";
            this.btnForbid.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnForbid.NormalImage")));
            this.btnForbid.Size = new System.Drawing.Size(60, 23);
            this.btnForbid.TabIndex = 38;
            this.btnForbid.Click += new System.EventHandler(this.btnForbid_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Caption = "取消";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.DownImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.DownImage")));
            this.btnCancel.Location = new System.Drawing.Point(309, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(1);
            this.btnCancel.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.MoveImage")));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.NormalImage")));
            this.btnCancel.Size = new System.Drawing.Size(60, 23);
            this.btnCancel.TabIndex = 40;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Caption = "保存";
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSave.DownImage")));
            this.btnSave.Location = new System.Drawing.Point(248, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(1);
            this.btnSave.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSave.MoveImage")));
            this.btnSave.Name = "btnSave";
            this.btnSave.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSave.NormalImage")));
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 39;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAllow
            // 
            this.btnAllow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAllow.BackgroundImage")));
            this.btnAllow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAllow.Caption = "允许";
            this.btnAllow.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAllow.DownImage = ((System.Drawing.Image)(resources.GetObject("btnAllow.DownImage")));
            this.btnAllow.Location = new System.Drawing.Point(187, 3);
            this.btnAllow.Margin = new System.Windows.Forms.Padding(1);
            this.btnAllow.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnAllow.MoveImage")));
            this.btnAllow.Name = "btnAllow";
            this.btnAllow.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnAllow.NormalImage")));
            this.btnAllow.Size = new System.Drawing.Size(60, 23);
            this.btnAllow.TabIndex = 38;
            this.btnAllow.Click += new System.EventHandler(this.btnAllow_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.Controls.Add(this.page);
            this.panelEx2.Location = new System.Drawing.Point(0, 467);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.panelEx2.Size = new System.Drawing.Size(894, 28);
            this.panelEx2.TabIndex = 41;
            // 
            // page
            // 
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.page.Dock = System.Windows.Forms.DockStyle.Right;
            this.page.Location = new System.Drawing.Point(339, 0);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(455, 28);
            this.page.TabIndex = 5;
            this.page.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            // 
            // UCLoginPCSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.dgvPCList);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAllow);
            this.Controls.Add(this.btnForbid);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.cbMonday);
            this.Name = "UCLoginPCSet";
            this.Size = new System.Drawing.Size(894, 495);
            this.Load += new System.EventHandler(this.UCLoginPCSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPCList)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbMonday;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvPCList;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.TextBoxEx txtpc_name;
        private ServiceStationClient.ComponentUI.TextBoxEx txtremark_name;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TextBoxEx txtmac_address;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbis_allow_login;
        private System.Windows.Forms.Label label3;
        public ServiceStationClient.ComponentUI.ButtonEx btnAdd;
        public ServiceStationClient.ComponentUI.ButtonEx btnEdit;
        public ServiceStationClient.ComponentUI.ButtonEx btnForbid;
        public ServiceStationClient.ComponentUI.ButtonEx btnCancel;
        public ServiceStationClient.ComponentUI.ButtonEx btnSave;
        public ServiceStationClient.ComponentUI.ButtonEx btnAllow;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn login_pc_set_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn pc_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn mac_address;
        private System.Windows.Forms.DataGridViewComboBoxColumn is_allow_login;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager page;
    }
}
