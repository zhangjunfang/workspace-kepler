namespace HXCPcClient.Chooser
{
    partial class frmDispatching
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDispatching));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvCompany = new ServiceStationClient.ComponentUI.TreeViewEx();
            this.palBottom = new System.Windows.Forms.Panel();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnSubmit = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnSave = new ServiceStationClient.ComponentUI.ButtonEx();
            this.tcUsers = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tpUsers = new System.Windows.Forms.TabPage();
            this.dgvUser = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.user_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.org_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dic_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.palTop = new System.Windows.Forms.Panel();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.txtName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtCode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labContract = new System.Windows.Forms.Label();
            this.labCustomNo = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.palBottom.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tcUsers.SuspendLayout();
            this.tpUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.palTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlContainer.Controls.Add(this.splitContainer1);
            this.pnlContainer.Size = new System.Drawing.Size(716, 428);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvCompany);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.palBottom);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.tcUsers);
            this.splitContainer1.Panel2.Controls.Add(this.palTop);
            this.splitContainer1.Size = new System.Drawing.Size(716, 428);
            this.splitContainer1.SplitterDistance = 169;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvCompany
            // 
            this.tvCompany.IgnoreAutoCheck = false;
            this.tvCompany.Location = new System.Drawing.Point(3, 8);
            this.tvCompany.Name = "tvCompany";
            this.tvCompany.Size = new System.Drawing.Size(162, 415);
            this.tvCompany.TabIndex = 1;
            this.tvCompany.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCompany_AfterSelect);
            // 
            // palBottom
            // 
            this.palBottom.Controls.Add(this.page);
            this.palBottom.Location = new System.Drawing.Point(6, 329);
            this.palBottom.Name = "palBottom";
            this.palBottom.Size = new System.Drawing.Size(537, 40);
            this.palBottom.TabIndex = 38;
            // 
            // page
            // 
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.page.Dock = System.Windows.Forms.DockStyle.Right;
            this.page.Location = new System.Drawing.Point(109, 0);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(428, 40);
            this.page.TabIndex = 6;
            this.page.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnSubmit);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Location = new System.Drawing.Point(6, 375);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(533, 48);
            this.panel1.TabIndex = 11;
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Caption = "关闭";
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClose.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClose.DownImage")));
            this.btnClose.Location = new System.Drawing.Point(460, 11);
            this.btnClose.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClose.MoveImage")));
            this.btnClose.Name = "btnClose";
            this.btnClose.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClose.NormalImage")));
            this.btnClose.Size = new System.Drawing.Size(60, 26);
            this.btnClose.TabIndex = 36;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.BackgroundImage")));
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Caption = "确定";
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSubmit.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.DownImage")));
            this.btnSubmit.Location = new System.Drawing.Point(366, 11);
            this.btnSubmit.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.MoveImage")));
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.NormalImage")));
            this.btnSubmit.Size = new System.Drawing.Size(60, 26);
            this.btnSubmit.TabIndex = 35;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Caption = "当页保存";
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSave.DownImage")));
            this.btnSave.Location = new System.Drawing.Point(271, 11);
            this.btnSave.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSave.MoveImage")));
            this.btnSave.Name = "btnSave";
            this.btnSave.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSave.NormalImage")));
            this.btnSave.Size = new System.Drawing.Size(60, 26);
            this.btnSave.TabIndex = 34;
            this.btnSave.Visible = false;
            // 
            // tcUsers
            // 
            this.tcUsers.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tcUsers.Controls.Add(this.tpUsers);
            this.tcUsers.Location = new System.Drawing.Point(2, 68);
            this.tcUsers.Name = "tcUsers";
            this.tcUsers.SelectedIndex = 0;
            this.tcUsers.Size = new System.Drawing.Size(541, 259);
            this.tcUsers.TabIndex = 10;
            // 
            // tpUsers
            // 
            this.tpUsers.Controls.Add(this.dgvUser);
            this.tpUsers.Location = new System.Drawing.Point(4, 26);
            this.tpUsers.Name = "tpUsers";
            this.tpUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tpUsers.Size = new System.Drawing.Size(533, 229);
            this.tpUsers.TabIndex = 0;
            this.tpUsers.Text = "员工信息列表";
            this.tpUsers.UseVisualStyleBackColor = true;
            // 
            // dgvUser
            // 
            this.dgvUser.AllowUserToAddRows = false;
            this.dgvUser.AllowUserToDeleteRows = false;
            this.dgvUser.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvUser.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.user_code,
            this.user_name,
            this.user_phone,
            this.org_name,
            this.dic_name,
            this.remark,
            this.user_id});
            this.dgvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUser.EnableHeadersVisualStyles = false;
            this.dgvUser.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvUser.Location = new System.Drawing.Point(3, 3);
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
            this.dgvUser.Size = new System.Drawing.Size(527, 223);
            this.dgvUser.TabIndex = 1;
            this.ToolTip.SetToolTip(this.dgvUser, "请双击选择");
            this.dgvUser.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUser_CellDoubleClick);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 18;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // user_code
            // 
            this.user_code.DataPropertyName = "user_code";
            this.user_code.FillWeight = 365.4822F;
            this.user_code.HeaderText = "员工编码";
            this.user_code.MinimumWidth = 90;
            this.user_code.Name = "user_code";
            this.user_code.ReadOnly = true;
            // 
            // user_name
            // 
            this.user_name.DataPropertyName = "user_name";
            this.user_name.FillWeight = 1.957035F;
            this.user_name.HeaderText = "员工姓名";
            this.user_name.MinimumWidth = 90;
            this.user_name.Name = "user_name";
            this.user_name.ReadOnly = true;
            // 
            // user_phone
            // 
            this.user_phone.DataPropertyName = "user_phone";
            this.user_phone.FillWeight = 18.9564F;
            this.user_phone.HeaderText = "联系电话";
            this.user_phone.MinimumWidth = 90;
            this.user_phone.Name = "user_phone";
            this.user_phone.ReadOnly = true;
            // 
            // org_name
            // 
            this.org_name.DataPropertyName = "org_name";
            this.org_name.FillWeight = 28.90812F;
            this.org_name.HeaderText = "所属部门";
            this.org_name.MinimumWidth = 100;
            this.org_name.Name = "org_name";
            this.org_name.ReadOnly = true;
            // 
            // dic_name
            // 
            this.dic_name.DataPropertyName = "dic_name";
            this.dic_name.FillWeight = 65.22065F;
            this.dic_name.HeaderText = "所属班组";
            this.dic_name.MinimumWidth = 100;
            this.dic_name.Name = "dic_name";
            this.dic_name.ReadOnly = true;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.FillWeight = 98.08826F;
            this.remark.HeaderText = "备注";
            this.remark.MinimumWidth = 90;
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            // 
            // user_id
            // 
            this.user_id.DataPropertyName = "user_id";
            this.user_id.HeaderText = "user_id";
            this.user_id.Name = "user_id";
            this.user_id.ReadOnly = true;
            this.user_id.Visible = false;
            // 
            // palTop
            // 
            this.palTop.Controls.Add(this.btnSearch);
            this.palTop.Controls.Add(this.btnClear);
            this.palTop.Controls.Add(this.txtName);
            this.palTop.Controls.Add(this.txtCode);
            this.palTop.Controls.Add(this.labContract);
            this.palTop.Controls.Add(this.labCustomNo);
            this.palTop.Location = new System.Drawing.Point(2, 3);
            this.palTop.Name = "palTop";
            this.palTop.Size = new System.Drawing.Size(541, 65);
            this.palTop.TabIndex = 9;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(416, 5);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(60, 26);
            this.btnSearch.TabIndex = 30;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(416, 33);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 28;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtName
            // 
            this.txtName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtName.BackColor = System.Drawing.Color.Transparent;
            this.txtName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtName.ForeImage = null;
            this.txtName.Location = new System.Drawing.Point(278, 16);
            this.txtName.MaxLengh = 32767;
            this.txtName.Multiline = false;
            this.txtName.Name = "txtName";
            this.txtName.Radius = 3;
            this.txtName.ReadOnly = false;
            this.txtName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtName.Size = new System.Drawing.Size(121, 23);
            this.txtName.TabIndex = 25;
            this.txtName.UseSystemPasswordChar = false;
            this.txtName.WaterMark = null;
            this.txtName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtCode
            // 
            this.txtCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCode.BackColor = System.Drawing.Color.Transparent;
            this.txtCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCode.ForeImage = null;
            this.txtCode.Location = new System.Drawing.Point(76, 16);
            this.txtCode.MaxLengh = 32767;
            this.txtCode.Multiline = false;
            this.txtCode.Name = "txtCode";
            this.txtCode.Radius = 3;
            this.txtCode.ReadOnly = false;
            this.txtCode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCode.Size = new System.Drawing.Size(121, 23);
            this.txtCode.TabIndex = 24;
            this.txtCode.UseSystemPasswordChar = false;
            this.txtCode.WaterMark = null;
            this.txtCode.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labContract
            // 
            this.labContract.AutoSize = true;
            this.labContract.Location = new System.Drawing.Point(213, 22);
            this.labContract.Name = "labContract";
            this.labContract.Size = new System.Drawing.Size(65, 12);
            this.labContract.TabIndex = 2;
            this.labContract.Text = "员工姓名：";
            // 
            // labCustomNo
            // 
            this.labCustomNo.AutoSize = true;
            this.labCustomNo.Location = new System.Drawing.Point(14, 22);
            this.labCustomNo.Name = "labCustomNo";
            this.labCustomNo.Size = new System.Drawing.Size(65, 12);
            this.labCustomNo.TabIndex = 0;
            this.labCustomNo.Text = "员工编码：";
            // 
            // frmDispatching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 460);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmDispatching";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "派工选择";
            this.Load += new System.EventHandler(this.frmDispatching_Load);
            this.pnlContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.palBottom.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tcUsers.ResumeLayout(false);
            this.tpUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.palTop.ResumeLayout(false);
            this.palTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ServiceStationClient.ComponentUI.TreeViewEx tvCompany;
        private ServiceStationClient.ComponentUI.TabControlEx tcUsers;
        private System.Windows.Forms.TabPage tpUsers;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvUser;
        private System.Windows.Forms.Panel palTop;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.TextBoxEx txtName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCode;
        private System.Windows.Forms.Label labContract;
        private System.Windows.Forms.Label labCustomNo;
        private System.Windows.Forms.Panel panel1;
        private ServiceStationClient.ComponentUI.ButtonEx btnClose;
        private ServiceStationClient.ComponentUI.ButtonEx btnSubmit;
        private ServiceStationClient.ComponentUI.ButtonEx btnSave;
        private System.Windows.Forms.Panel palBottom;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dic_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_id;
    }
}