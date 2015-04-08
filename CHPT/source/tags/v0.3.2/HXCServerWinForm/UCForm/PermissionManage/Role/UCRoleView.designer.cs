namespace HXCServerWinForm.UCForm.Role
{
    partial class UCRoleView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvFunction = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.tvFunction = new ServiceStationClient.ComponentUI.TreeViewEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvUser = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.user_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.com_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.org_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSearch = new ServiceStationClient.ComponentUI.PanelEx();
            this.cbodata_sources = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cbostate = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblupdate_time = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblupdate_by = new System.Windows.Forms.Label();
            this.lblcreate_time = new System.Windows.Forms.Label();
            this.lblcreate_by = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lable1 = new System.Windows.Forms.Label();
            this.lblremark = new System.Windows.Forms.Label();
            this.lblRole_name = new System.Windows.Forms.Label();
            this.lblRole_code = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.fun_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isall = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_browse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_add = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_edit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_copy = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_delete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_cancel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_activate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_enable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_disable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_save = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_submit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_examine = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_import = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_export = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_print = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_operation_record = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_dispatching = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_settle_accounts = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fun_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parent_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControlEx1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFunction)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.Controls.Add(this.tabPage2);
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Location = new System.Drawing.Point(0, 154);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(1030, 387);
            this.tabControlEx1.TabIndex = 6;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvFunction);
            this.tabPage2.Controls.Add(this.tvFunction);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1022, 357);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "权限清单";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvFunction
            // 
            this.dgvFunction.AllowUserToAddRows = false;
            this.dgvFunction.AllowUserToDeleteRows = false;
            this.dgvFunction.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvFunction.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFunction.BackgroundColor = System.Drawing.Color.White;
            this.dgvFunction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFunction.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFunction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFunction.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fun_name,
            this.isall,
            this.button_browse,
            this.button_add,
            this.button_edit,
            this.button_copy,
            this.button_delete,
            this.button_cancel,
            this.button_activate,
            this.button_enable,
            this.button_disable,
            this.button_save,
            this.button_submit,
            this.button_examine,
            this.button_import,
            this.button_export,
            this.button_print,
            this.button_operation_record,
            this.button_dispatching,
            this.button_settle_accounts,
            this.fun_id,
            this.parent_id});
            this.dgvFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFunction.EnableHeadersVisualStyles = false;
            this.dgvFunction.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvFunction.Location = new System.Drawing.Point(173, 3);
            this.dgvFunction.MultiSelect = false;
            this.dgvFunction.Name = "dgvFunction";
            this.dgvFunction.ReadOnly = true;
            this.dgvFunction.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvFunction.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFunction.RowTemplate.Height = 23;
            this.dgvFunction.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFunction.Size = new System.Drawing.Size(846, 351);
            this.dgvFunction.TabIndex = 1;
            // 
            // tvFunction
            // 
            this.tvFunction.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvFunction.IgnoreAutoCheck = false;
            this.tvFunction.Location = new System.Drawing.Point(3, 3);
            this.tvFunction.Name = "tvFunction";
            this.tvFunction.Size = new System.Drawing.Size(170, 351);
            this.tvFunction.TabIndex = 0;
            this.tvFunction.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvFunction_NodeMouseClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvUser);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1022, 357);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "操作员";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvUser
            // 
            this.dgvUser.AllowUserToAddRows = false;
            this.dgvUser.AllowUserToDeleteRows = false;
            this.dgvUser.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvUser.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvUser.BackgroundColor = System.Drawing.Color.White;
            this.dgvUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUser.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.user_code,
            this.user_name,
            this.user_phone,
            this.com_name,
            this.org_name,
            this.remark,
            this.user_id});
            this.dgvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUser.EnableHeadersVisualStyles = false;
            this.dgvUser.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvUser.Location = new System.Drawing.Point(3, 3);
            this.dgvUser.MultiSelect = false;
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.ReadOnly = true;
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.RowHeadersWidth = 30;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvUser.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvUser.RowTemplate.Height = 23;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.ShowCheckBox = false;
            this.dgvUser.Size = new System.Drawing.Size(186, 64);
            this.dgvUser.TabIndex = 7;
            // 
            // user_code
            // 
            this.user_code.DataPropertyName = "user_code";
            this.user_code.HeaderText = "人员编码";
            this.user_code.Name = "user_code";
            this.user_code.ReadOnly = true;
            // 
            // user_name
            // 
            this.user_name.DataPropertyName = "user_name";
            this.user_name.HeaderText = "人员姓名";
            this.user_name.Name = "user_name";
            this.user_name.ReadOnly = true;
            // 
            // user_phone
            // 
            this.user_phone.DataPropertyName = "user_phone";
            this.user_phone.HeaderText = "手机";
            this.user_phone.Name = "user_phone";
            this.user_phone.ReadOnly = true;
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
            this.pnlSearch.Controls.Add(this.cbodata_sources);
            this.pnlSearch.Controls.Add(this.cbostate);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Controls.Add(this.lblupdate_time);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.lblupdate_by);
            this.pnlSearch.Controls.Add(this.lblcreate_time);
            this.pnlSearch.Controls.Add(this.lblcreate_by);
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.lable1);
            this.pnlSearch.Controls.Add(this.lblremark);
            this.pnlSearch.Controls.Add(this.lblRole_name);
            this.pnlSearch.Controls.Add(this.lblRole_code);
            this.pnlSearch.Controls.Add(this.label8);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Location = new System.Drawing.Point(0, 34);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1030, 114);
            this.pnlSearch.TabIndex = 5;
            // 
            // cbodata_sources
            // 
            this.cbodata_sources.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbodata_sources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbodata_sources.Enabled = false;
            this.cbodata_sources.FormattingEnabled = true;
            this.cbodata_sources.Location = new System.Drawing.Point(590, 80);
            this.cbodata_sources.Name = "cbodata_sources";
            this.cbodata_sources.Size = new System.Drawing.Size(154, 22);
            this.cbodata_sources.TabIndex = 29;
            // 
            // cbostate
            // 
            this.cbostate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbostate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbostate.Enabled = false;
            this.cbostate.FormattingEnabled = true;
            this.cbostate.Location = new System.Drawing.Point(590, 52);
            this.cbostate.Name = "cbostate";
            this.cbostate.Size = new System.Drawing.Size(154, 22);
            this.cbostate.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(542, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "状态：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(518, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "数据来源：";
            // 
            // lblupdate_time
            // 
            this.lblupdate_time.AutoSize = true;
            this.lblupdate_time.Location = new System.Drawing.Point(371, 85);
            this.lblupdate_time.Name = "lblupdate_time";
            this.lblupdate_time.Size = new System.Drawing.Size(0, 12);
            this.lblupdate_time.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(276, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "最后编辑时间：";
            // 
            // lblupdate_by
            // 
            this.lblupdate_by.AutoSize = true;
            this.lblupdate_by.Location = new System.Drawing.Point(93, 85);
            this.lblupdate_by.Name = "lblupdate_by";
            this.lblupdate_by.Size = new System.Drawing.Size(0, 12);
            this.lblupdate_by.TabIndex = 23;
            // 
            // lblcreate_time
            // 
            this.lblcreate_time.AutoSize = true;
            this.lblcreate_time.Location = new System.Drawing.Point(371, 58);
            this.lblcreate_time.Name = "lblcreate_time";
            this.lblcreate_time.Size = new System.Drawing.Size(0, 12);
            this.lblcreate_time.TabIndex = 22;
            // 
            // lblcreate_by
            // 
            this.lblcreate_by.AutoSize = true;
            this.lblcreate_by.Location = new System.Drawing.Point(93, 58);
            this.lblcreate_by.Name = "lblcreate_by";
            this.lblcreate_by.Size = new System.Drawing.Size(0, 12);
            this.lblcreate_by.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "最后编辑人：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(300, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "创建时间：";
            // 
            // lable1
            // 
            this.lable1.AutoSize = true;
            this.lable1.Location = new System.Drawing.Point(34, 58);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(53, 12);
            this.lable1.TabIndex = 18;
            this.lable1.Text = "创建人：";
            // 
            // lblremark
            // 
            this.lblremark.AutoSize = true;
            this.lblremark.Location = new System.Drawing.Point(93, 37);
            this.lblremark.Name = "lblremark";
            this.lblremark.Size = new System.Drawing.Size(0, 12);
            this.lblremark.TabIndex = 17;
            // 
            // lblRole_name
            // 
            this.lblRole_name.AutoSize = true;
            this.lblRole_name.Location = new System.Drawing.Point(371, 13);
            this.lblRole_name.Name = "lblRole_name";
            this.lblRole_name.Size = new System.Drawing.Size(0, 12);
            this.lblRole_name.TabIndex = 16;
            // 
            // lblRole_code
            // 
            this.lblRole_code.AutoSize = true;
            this.lblRole_code.Location = new System.Drawing.Point(93, 13);
            this.lblRole_code.Name = "lblRole_code";
            this.lblRole_code.Size = new System.Drawing.Size(0, 12);
            this.lblRole_code.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(46, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "备注：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(300, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "角色名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "角色编码：";
            // 
            // fun_name
            // 
            this.fun_name.DataPropertyName = "fun_name";
            this.fun_name.HeaderText = "功能菜单";
            this.fun_name.Name = "fun_name";
            this.fun_name.ReadOnly = true;
            // 
            // isall
            // 
            this.isall.DataPropertyName = "isall";
            this.isall.HeaderText = "全部";
            this.isall.Name = "isall";
            this.isall.ReadOnly = true;
            this.isall.Width = 40;
            // 
            // button_browse
            // 
            this.button_browse.DataPropertyName = "button_browse";
            this.button_browse.HeaderText = "浏览";
            this.button_browse.Name = "button_browse";
            this.button_browse.ReadOnly = true;
            this.button_browse.Width = 40;
            // 
            // button_add
            // 
            this.button_add.DataPropertyName = "button_add";
            this.button_add.HeaderText = "新增";
            this.button_add.Name = "button_add";
            this.button_add.ReadOnly = true;
            this.button_add.Width = 40;
            // 
            // button_edit
            // 
            this.button_edit.DataPropertyName = "button_edit";
            this.button_edit.HeaderText = "编辑";
            this.button_edit.Name = "button_edit";
            this.button_edit.ReadOnly = true;
            this.button_edit.Width = 40;
            // 
            // button_copy
            // 
            this.button_copy.DataPropertyName = "button_copy";
            this.button_copy.HeaderText = "复制";
            this.button_copy.Name = "button_copy";
            this.button_copy.ReadOnly = true;
            this.button_copy.Width = 40;
            // 
            // button_delete
            // 
            this.button_delete.DataPropertyName = "button_delete";
            this.button_delete.HeaderText = "删除";
            this.button_delete.Name = "button_delete";
            this.button_delete.ReadOnly = true;
            this.button_delete.Width = 40;
            // 
            // button_cancel
            // 
            this.button_cancel.DataPropertyName = "button_cancel";
            this.button_cancel.HeaderText = "作废";
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.ReadOnly = true;
            this.button_cancel.Width = 40;
            // 
            // button_activate
            // 
            this.button_activate.DataPropertyName = "button_activate";
            this.button_activate.HeaderText = "激活";
            this.button_activate.Name = "button_activate";
            this.button_activate.ReadOnly = true;
            this.button_activate.Width = 40;
            // 
            // button_enable
            // 
            this.button_enable.DataPropertyName = "button_enable";
            this.button_enable.HeaderText = "启用";
            this.button_enable.Name = "button_enable";
            this.button_enable.ReadOnly = true;
            this.button_enable.Width = 40;
            // 
            // button_disable
            // 
            this.button_disable.DataPropertyName = "button_disable";
            this.button_disable.HeaderText = "停用";
            this.button_disable.Name = "button_disable";
            this.button_disable.ReadOnly = true;
            this.button_disable.Width = 40;
            // 
            // button_save
            // 
            this.button_save.DataPropertyName = "button_save";
            this.button_save.HeaderText = "保存";
            this.button_save.Name = "button_save";
            this.button_save.ReadOnly = true;
            this.button_save.Width = 40;
            // 
            // button_submit
            // 
            this.button_submit.DataPropertyName = "button_submit";
            this.button_submit.HeaderText = "提交";
            this.button_submit.Name = "button_submit";
            this.button_submit.ReadOnly = true;
            this.button_submit.Width = 40;
            // 
            // button_examine
            // 
            this.button_examine.DataPropertyName = "button_examine";
            this.button_examine.HeaderText = "审核";
            this.button_examine.Name = "button_examine";
            this.button_examine.ReadOnly = true;
            this.button_examine.Width = 40;
            // 
            // button_import
            // 
            this.button_import.DataPropertyName = "button_import";
            this.button_import.HeaderText = "导入";
            this.button_import.Name = "button_import";
            this.button_import.ReadOnly = true;
            this.button_import.Width = 40;
            // 
            // button_export
            // 
            this.button_export.DataPropertyName = "button_export";
            this.button_export.HeaderText = "导出";
            this.button_export.Name = "button_export";
            this.button_export.ReadOnly = true;
            this.button_export.Width = 40;
            // 
            // button_print
            // 
            this.button_print.DataPropertyName = "button_print";
            this.button_print.HeaderText = "打印";
            this.button_print.Name = "button_print";
            this.button_print.ReadOnly = true;
            this.button_print.Width = 40;
            // 
            // button_operation_record
            // 
            this.button_operation_record.DataPropertyName = "button_operation_record";
            this.button_operation_record.HeaderText = "操作记录";
            this.button_operation_record.Name = "button_operation_record";
            this.button_operation_record.ReadOnly = true;
            this.button_operation_record.Width = 80;
            // 
            // button_dispatching
            // 
            this.button_dispatching.DataPropertyName = "button_dispatching";
            this.button_dispatching.HeaderText = "派工";
            this.button_dispatching.Name = "button_dispatching";
            this.button_dispatching.ReadOnly = true;
            this.button_dispatching.Width = 40;
            // 
            // button_settle_accounts
            // 
            this.button_settle_accounts.DataPropertyName = "button_settle_accounts";
            this.button_settle_accounts.HeaderText = "结算";
            this.button_settle_accounts.Name = "button_settle_accounts";
            this.button_settle_accounts.ReadOnly = true;
            this.button_settle_accounts.Width = 40;
            // 
            // fun_id
            // 
            this.fun_id.DataPropertyName = "fun_id";
            this.fun_id.HeaderText = "fun_id";
            this.fun_id.Name = "fun_id";
            this.fun_id.ReadOnly = true;
            this.fun_id.Visible = false;
            // 
            // parent_id
            // 
            this.parent_id.DataPropertyName = "parent_id";
            this.parent_id.HeaderText = "parent_id";
            this.parent_id.Name = "parent_id";
            this.parent_id.ReadOnly = true;
            this.parent_id.Visible = false;
            // 
            // UCRoleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlEx1);
            this.Controls.Add(this.pnlSearch);
            this.Name = "UCRoleView";
            this.Load += new System.EventHandler(this.UCRoleView_Load);
            this.Controls.SetChildIndex(this.pnlSearch, 0);
            this.Controls.SetChildIndex(this.tabControlEx1, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFunction)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage2;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvFunction;
        private ServiceStationClient.ComponentUI.TreeViewEx tvFunction;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn com_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_id;
        private ServiceStationClient.ComponentUI.PanelEx pnlSearch;
        private System.Windows.Forms.Label lblremark;
        private System.Windows.Forms.Label lblRole_name;
        private System.Windows.Forms.Label lblRole_code;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblupdate_time;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblupdate_by;
        private System.Windows.Forms.Label lblcreate_time;
        private System.Windows.Forms.Label lblcreate_by;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lable1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbodata_sources;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbostate;
        private System.Windows.Forms.DataGridViewTextBoxColumn fun_name;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isall;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_browse;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_add;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_edit;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_copy;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_delete;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_cancel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_activate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_enable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_disable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_save;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_submit;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_examine;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_import;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_export;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_print;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_operation_record;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_dispatching;
        private System.Windows.Forms.DataGridViewCheckBoxColumn button_settle_accounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn fun_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn parent_id;
    }
}
