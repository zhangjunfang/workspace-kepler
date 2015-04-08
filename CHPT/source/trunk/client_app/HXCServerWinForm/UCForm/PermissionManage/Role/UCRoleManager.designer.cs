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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCRoleManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvRole
            // 
            this.dgvRole.AllowUserToAddRows = false;
            this.dgvRole.AllowUserToDeleteRows = false;
            this.dgvRole.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvRole.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRole.BackgroundColor = System.Drawing.Color.White;
            this.dgvRole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRole.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRole.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRole.EnableHeadersVisualStyles = false;
            this.dgvRole.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvRole.IsCheck = true;
            this.dgvRole.Location = new System.Drawing.Point(0, 134);
            this.dgvRole.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvRole.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvRole.MergeColumnNames")));
            this.dgvRole.MultiSelect = false;
            this.dgvRole.Name = "dgvRole";
            this.dgvRole.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRole.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRole.RowHeadersVisible = false;
            this.dgvRole.RowHeadersWidth = 30;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvRole.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvRole.RowTemplate.Height = 23;
            this.dgvRole.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRole.ShowCheckBox = true;
            this.dgvRole.Size = new System.Drawing.Size(1020, 423);
            this.dgvRole.TabIndex = 9;
            this.dgvRole.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvRole_CellBeginEdit);
            this.dgvRole.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRole_CellDoubleClick);
            this.dgvRole.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRole_CellFormatting);
            this.dgvRole.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRole_CellMouseUp);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
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
            this.pnlSearch.Curvature = 0;
            this.pnlSearch.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlSearch.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
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
            this.txtrole_name.InputtingVerifyCondition = null;
            this.txtrole_name.Location = new System.Drawing.Point(286, 8);
            this.txtrole_name.MaxLengh = 32767;
            this.txtrole_name.Multiline = false;
            this.txtrole_name.Name = "txtrole_name";
            this.txtrole_name.Radius = 3;
            this.txtrole_name.ReadOnly = false;
            this.txtrole_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtrole_name.ShowError = false;
            this.txtrole_name.Size = new System.Drawing.Size(135, 23);
            this.txtrole_name.TabIndex = 3;
            this.txtrole_name.UseSystemPasswordChar = false;
            this.txtrole_name.Value = "";
            this.txtrole_name.VerifyCondition = null;
            this.txtrole_name.VerifyType = null;
            this.txtrole_name.VerifyTypeName = null;
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
            this.txtrole_code.InputtingVerifyCondition = null;
            this.txtrole_code.Location = new System.Drawing.Point(77, 8);
            this.txtrole_code.MaxLengh = 32767;
            this.txtrole_code.Multiline = false;
            this.txtrole_code.Name = "txtrole_code";
            this.txtrole_code.Radius = 3;
            this.txtrole_code.ReadOnly = false;
            this.txtrole_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtrole_code.ShowError = false;
            this.txtrole_code.Size = new System.Drawing.Size(137, 23);
            this.txtrole_code.TabIndex = 1;
            this.txtrole_code.UseSystemPasswordChar = false;
            this.txtrole_code.Value = "";
            this.txtrole_code.VerifyCondition = null;
            this.txtrole_code.VerifyType = null;
            this.txtrole_code.VerifyTypeName = null;
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
            this.Controls.Add(this.pnlSearch);
            this.Name = "UCRoleManager";
            this.Load += new System.EventHandler(this.UCRoleManager_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.pnlSearch, 0);
            this.Controls.SetChildIndex(this.dgvRole, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRole)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRole;
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
