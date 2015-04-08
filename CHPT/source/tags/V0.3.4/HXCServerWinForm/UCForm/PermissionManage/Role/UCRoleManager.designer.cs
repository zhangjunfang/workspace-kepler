namespace HXCServerWinForm.UCForm.Role
{
    partial class UCRoleManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCRoleManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvRole = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.role_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.role_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data_sources = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.update_username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.update_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.role_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.update_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.pnlSearch = new ServiceStationClient.ComponentUI.PanelEx();
            this.dtpend = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dtpstart = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.ddlstate = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtrole_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.txtrole_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRole)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvRole
            // 
            this.dgvRole.AllowUserToAddRows = false;
            this.dgvRole.AllowUserToDeleteRows = false;
            this.dgvRole.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvRole.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRole.BackgroundColor = System.Drawing.Color.White;
            this.dgvRole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRole.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvRole.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRole.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.role_code,
            this.role_name,
            this.remark,
            this.state,
            this.data_sources,
            this.create_Username,
            this.create_time,
            this.update_username,
            this.update_time,
            this.role_id,
            this.create_by,
            this.update_by});
            this.dgvRole.EnableHeadersVisualStyles = false;
            this.dgvRole.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvRole.Location = new System.Drawing.Point(0, 134);
            this.dgvRole.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvRole.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvRole.MergeColumnNames")));
            this.dgvRole.MultiSelect = false;
            this.dgvRole.Name = "dgvRole";
            this.dgvRole.ReadOnly = true;
            this.dgvRole.RowHeadersVisible = false;
            this.dgvRole.RowHeadersWidth = 30;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvRole.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvRole.RowTemplate.Height = 23;
            this.dgvRole.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRole.ShowCheckBox = true;
            this.dgvRole.Size = new System.Drawing.Size(1020, 392);
            this.dgvRole.TabIndex = 9;
            this.dgvRole.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvRole_CellBeginEdit);
            this.dgvRole.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRole_CellDoubleClick);
            this.dgvRole.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRole_CellFormatting);
            // 
            // colCheck
            // 
            this.colCheck.MinimumWidth = 18;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 30;
            // 
            // role_code
            // 
            this.role_code.DataPropertyName = "role_code";
            this.role_code.HeaderText = "角色编码";
            this.role_code.Name = "role_code";
            this.role_code.ReadOnly = true;
            // 
            // role_name
            // 
            this.role_name.DataPropertyName = "role_name";
            this.role_name.HeaderText = "角色名称";
            this.role_name.Name = "role_name";
            this.role_name.ReadOnly = true;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            this.remark.Width = 200;
            // 
            // state
            // 
            this.state.DataPropertyName = "state";
            this.state.HeaderText = "状态";
            this.state.Name = "state";
            this.state.ReadOnly = true;
            this.state.Width = 150;
            // 
            // data_sources
            // 
            this.data_sources.DataPropertyName = "data_sources";
            this.data_sources.HeaderText = "数据来源";
            this.data_sources.Name = "data_sources";
            this.data_sources.ReadOnly = true;
            // 
            // create_Username
            // 
            this.create_Username.DataPropertyName = "create_Username";
            this.create_Username.HeaderText = "创建人";
            this.create_Username.Name = "create_Username";
            this.create_Username.ReadOnly = true;
            // 
            // create_time
            // 
            this.create_time.DataPropertyName = "create_time";
            this.create_time.HeaderText = "创建时间";
            this.create_time.Name = "create_time";
            this.create_time.ReadOnly = true;
            // 
            // update_username
            // 
            this.update_username.DataPropertyName = "update_username";
            this.update_username.HeaderText = "最后编辑人";
            this.update_username.Name = "update_username";
            this.update_username.ReadOnly = true;
            // 
            // update_time
            // 
            this.update_time.DataPropertyName = "update_time";
            this.update_time.HeaderText = "最后编辑时间";
            this.update_time.Name = "update_time";
            this.update_time.ReadOnly = true;
            this.update_time.Width = 200;
            // 
            // role_id
            // 
            this.role_id.DataPropertyName = "role_id";
            this.role_id.HeaderText = "role_id";
            this.role_id.Name = "role_id";
            this.role_id.ReadOnly = true;
            this.role_id.Visible = false;
            // 
            // create_by
            // 
            this.create_by.DataPropertyName = "create_by";
            this.create_by.HeaderText = "create_by";
            this.create_by.Name = "create_by";
            this.create_by.ReadOnly = true;
            this.create_by.Visible = false;
            // 
            // update_by
            // 
            this.update_by.DataPropertyName = "update_by";
            this.update_by.HeaderText = "update_by";
            this.update_by.Name = "update_by";
            this.update_by.ReadOnly = true;
            this.update_by.Visible = false;
            // 
            // panelEx2
            // 
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(223)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.Controls.Add(this.page);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Location = new System.Drawing.Point(0, 532);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.panelEx2.Size = new System.Drawing.Size(1020, 28);
            this.panelEx2.TabIndex = 8;
            // 
            // page
            // 
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.Dock = System.Windows.Forms.DockStyle.Right;
            this.page.Location = new System.Drawing.Point(492, 0);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(428, 28);
            this.page.TabIndex = 5;
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.Controls.Add(this.dtpend);
            this.pnlSearch.Controls.Add(this.dtpstart);
            this.pnlSearch.Controls.Add(this.ddlstate);
            this.pnlSearch.Controls.Add(this.btnQuery);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Controls.Add(this.label8);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.txtrole_name);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.txtrole_code);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Location = new System.Drawing.Point(0, 34);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1017, 95);
            this.pnlSearch.TabIndex = 7;
            // 
            // dtpend
            // 
            this.dtpend.Location = new System.Drawing.Point(252, 45);
            this.dtpend.Name = "dtpend";
            this.dtpend.ShowFormat = "yyyy-MM-dd";
            this.dtpend.Size = new System.Drawing.Size(137, 21);
            this.dtpend.TabIndex = 19;
            // 
            // dtpstart
            // 
            this.dtpstart.Location = new System.Drawing.Point(77, 45);
            this.dtpstart.Name = "dtpstart";
            this.dtpstart.ShowFormat = "yyyy-MM-dd";
            this.dtpstart.Size = new System.Drawing.Size(137, 21);
            this.dtpstart.TabIndex = 19;
            // 
            // ddlstate
            // 
            this.ddlstate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlstate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlstate.FormattingEnabled = true;
            this.ddlstate.Location = new System.Drawing.Point(491, 10);
            this.ddlstate.Name = "ddlstate";
            this.ddlstate.Size = new System.Drawing.Size(121, 22);
            this.ddlstate.TabIndex = 5;
            // 
            // btnQuery
            // 
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(654, 55);
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
            this.btnClear.Location = new System.Drawing.Point(654, 17);
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
            this.label8.Location = new System.Drawing.Point(226, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "至";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "创建时间：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(444, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "状态：";
            // 
            // txtrole_name
            // 
            this.txtrole_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtrole_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtrole_name.BackColor = System.Drawing.Color.Transparent;
            this.txtrole_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtrole_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtrole_name.ForeImage = null;
            this.txtrole_name.Location = new System.Drawing.Point(286, 8);
            this.txtrole_name.MaxLengh = 32767;
            this.txtrole_name.Multiline = false;
            this.txtrole_name.Name = "txtrole_name";
            this.txtrole_name.Radius = 3;
            this.txtrole_name.ReadOnly = false;
            this.txtrole_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtrole_name.Size = new System.Drawing.Size(135, 23);
            this.txtrole_name.TabIndex = 3;
            this.txtrole_name.UseSystemPasswordChar = false;
            this.txtrole_name.WaterMark = null;
            this.txtrole_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "角色名称：";
            // 
            // txtrole_code
            // 
            this.txtrole_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtrole_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtrole_code.BackColor = System.Drawing.Color.Transparent;
            this.txtrole_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtrole_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtrole_code.ForeImage = null;
            this.txtrole_code.Location = new System.Drawing.Point(77, 8);
            this.txtrole_code.MaxLengh = 32767;
            this.txtrole_code.Multiline = false;
            this.txtrole_code.Name = "txtrole_code";
            this.txtrole_code.Radius = 3;
            this.txtrole_code.ReadOnly = false;
            this.txtrole_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtrole_code.Size = new System.Drawing.Size(137, 23);
            this.txtrole_code.TabIndex = 1;
            this.txtrole_code.UseSystemPasswordChar = false;
            this.txtrole_code.WaterMark = null;
            this.txtrole_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "角色编码：";
            // 
            // UCRoleManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvRole);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.pnlSearch);
            this.Name = "UCRoleManager";
            this.Load += new System.EventHandler(this.UCRoleManager_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.pnlSearch, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.Controls.SetChildIndex(this.dgvRole, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRole)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRole;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private ServiceStationClient.ComponentUI.PanelEx pnlSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.TextBoxEx txtrole_name;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx txtrole_code;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlstate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn role_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn role_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
        private System.Windows.Forms.DataGridViewTextBoxColumn data_sources;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_Username;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn update_username;
        private System.Windows.Forms.DataGridViewTextBoxColumn update_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn role_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_by;
        private System.Windows.Forms.DataGridViewTextBoxColumn update_by;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtpend;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtpstart;

    }
}
