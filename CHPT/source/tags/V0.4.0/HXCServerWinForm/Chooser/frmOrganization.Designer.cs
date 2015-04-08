namespace HXCServerWinForm.Chooser
{
    partial class frmOrganization
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrganization));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlSearch = new ServiceStationClient.ComponentUI.PanelEx();
            this.btnSubmit = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.txtlegal_person = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label7 = new System.Windows.Forms.Label();
            this.txtcontact_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label6 = new System.Windows.Forms.Label();
            this.txtcom_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.txtorg_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPhone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCom_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.txtorg_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvOrg = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.com_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.com_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.org_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.org_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.legal_person = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.com_contact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.com_tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contact_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contact_telephone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.org_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlContainer.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrg)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlContainer.Controls.Add(this.tabControlEx1);
            this.pnlContainer.Controls.Add(this.pnlSearch);
            this.pnlContainer.Location = new System.Drawing.Point(1, 29);
            this.pnlContainer.Size = new System.Drawing.Size(683, 399);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.Controls.Add(this.btnSubmit);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Controls.Add(this.txtlegal_person);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.txtcontact_name);
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.txtcom_name);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Controls.Add(this.txtorg_name);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.txtPhone);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.txtCom_code);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.txtorg_code);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Location = new System.Drawing.Point(1, 2);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(681, 114);
            this.pnlSearch.TabIndex = 2;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.BackgroundImage")));
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Caption = "查询";
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSubmit.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.DownImage")));
            this.btnSubmit.Location = new System.Drawing.Point(608, 75);
            this.btnSubmit.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.MoveImage")));
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.NormalImage")));
            this.btnSubmit.Size = new System.Drawing.Size(60, 26);
            this.btnSubmit.TabIndex = 17;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(608, 41);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 16;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtlegal_person
            // 
            this.txtlegal_person.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtlegal_person.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtlegal_person.BackColor = System.Drawing.Color.Transparent;
            this.txtlegal_person.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtlegal_person.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtlegal_person.ForeImage = null;
            this.txtlegal_person.Location = new System.Drawing.Point(92, 75);
            this.txtlegal_person.MaxLengh = 32767;
            this.txtlegal_person.Multiline = false;
            this.txtlegal_person.Name = "txtlegal_person";
            this.txtlegal_person.Radius = 3;
            this.txtlegal_person.ReadOnly = false;
            this.txtlegal_person.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtlegal_person.Size = new System.Drawing.Size(120, 23);
            this.txtlegal_person.TabIndex = 13;
            this.txtlegal_person.UseSystemPasswordChar = false;
            this.txtlegal_person.WaterMark = null;
            this.txtlegal_person.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "法人/负责人：";
            // 
            // txtcontact_name
            // 
            this.txtcontact_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcontact_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcontact_name.BackColor = System.Drawing.Color.Transparent;
            this.txtcontact_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcontact_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcontact_name.ForeImage = null;
            this.txtcontact_name.Location = new System.Drawing.Point(479, 41);
            this.txtcontact_name.MaxLengh = 32767;
            this.txtcontact_name.Multiline = false;
            this.txtcontact_name.Name = "txtcontact_name";
            this.txtcontact_name.Radius = 3;
            this.txtcontact_name.ReadOnly = false;
            this.txtcontact_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcontact_name.Size = new System.Drawing.Size(120, 23);
            this.txtcontact_name.TabIndex = 11;
            this.txtcontact_name.UseSystemPasswordChar = false;
            this.txtcontact_name.WaterMark = null;
            this.txtcontact_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(428, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "联系人：";
            // 
            // txtcom_name
            // 
            this.txtcom_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcom_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcom_name.BackColor = System.Drawing.Color.Transparent;
            this.txtcom_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcom_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcom_name.ForeImage = null;
            this.txtcom_name.Location = new System.Drawing.Point(287, 41);
            this.txtcom_name.MaxLengh = 32767;
            this.txtcom_name.Multiline = false;
            this.txtcom_name.Name = "txtcom_name";
            this.txtcom_name.Radius = 3;
            this.txtcom_name.ReadOnly = false;
            this.txtcom_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcom_name.Size = new System.Drawing.Size(120, 23);
            this.txtcom_name.TabIndex = 9;
            this.txtcom_name.UseSystemPasswordChar = false;
            this.txtcom_name.WaterMark = null;
            this.txtcom_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(221, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "公司名称：";
            // 
            // txtorg_name
            // 
            this.txtorg_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtorg_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtorg_name.BackColor = System.Drawing.Color.Transparent;
            this.txtorg_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtorg_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtorg_name.ForeImage = null;
            this.txtorg_name.Location = new System.Drawing.Point(92, 41);
            this.txtorg_name.MaxLengh = 32767;
            this.txtorg_name.Multiline = false;
            this.txtorg_name.Name = "txtorg_name";
            this.txtorg_name.Radius = 3;
            this.txtorg_name.ReadOnly = false;
            this.txtorg_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtorg_name.Size = new System.Drawing.Size(120, 23);
            this.txtorg_name.TabIndex = 7;
            this.txtorg_name.UseSystemPasswordChar = false;
            this.txtorg_name.WaterMark = null;
            this.txtorg_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "角色名称：";
            // 
            // txtPhone
            // 
            this.txtPhone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPhone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPhone.BackColor = System.Drawing.Color.Transparent;
            this.txtPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtPhone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtPhone.ForeImage = null;
            this.txtPhone.Location = new System.Drawing.Point(479, 8);
            this.txtPhone.MaxLengh = 32767;
            this.txtPhone.Multiline = false;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Radius = 3;
            this.txtPhone.ReadOnly = false;
            this.txtPhone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPhone.Size = new System.Drawing.Size(120, 23);
            this.txtPhone.TabIndex = 5;
            this.txtPhone.UseSystemPasswordChar = false;
            this.txtPhone.WaterMark = null;
            this.txtPhone.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(417, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "联系电话：";
            // 
            // txtCom_code
            // 
            this.txtCom_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCom_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCom_code.BackColor = System.Drawing.Color.Transparent;
            this.txtCom_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCom_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCom_code.ForeImage = null;
            this.txtCom_code.Location = new System.Drawing.Point(287, 8);
            this.txtCom_code.MaxLengh = 32767;
            this.txtCom_code.Multiline = false;
            this.txtCom_code.Name = "txtCom_code";
            this.txtCom_code.Radius = 3;
            this.txtCom_code.ReadOnly = false;
            this.txtCom_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCom_code.Size = new System.Drawing.Size(120, 23);
            this.txtCom_code.TabIndex = 3;
            this.txtCom_code.UseSystemPasswordChar = false;
            this.txtCom_code.WaterMark = null;
            this.txtCom_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "公司编码：";
            // 
            // txtorg_code
            // 
            this.txtorg_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtorg_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtorg_code.BackColor = System.Drawing.Color.Transparent;
            this.txtorg_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtorg_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtorg_code.ForeImage = null;
            this.txtorg_code.Location = new System.Drawing.Point(93, 8);
            this.txtorg_code.MaxLengh = 32767;
            this.txtorg_code.Multiline = false;
            this.txtorg_code.Name = "txtorg_code";
            this.txtorg_code.Radius = 3;
            this.txtorg_code.ReadOnly = false;
            this.txtorg_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtorg_code.Size = new System.Drawing.Size(120, 23);
            this.txtorg_code.TabIndex = 1;
            this.txtorg_code.UseSystemPasswordChar = false;
            this.txtorg_code.WaterMark = null;
            this.txtorg_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "角色编码：";
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Location = new System.Drawing.Point(1, 120);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(681, 276);
            this.tabControlEx1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvOrg);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(673, 246);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "组织信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvOrg
            // 
            this.dgvOrg.AllowUserToAddRows = false;
            this.dgvOrg.AllowUserToDeleteRows = false;
            this.dgvOrg.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvOrg.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOrg.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvOrg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.com_code,
            this.com_name,
            this.org_code,
            this.org_name,
            this.legal_person,
            this.com_contact,
            this.com_tel,
            this.contact_name,
            this.contact_telephone,
            this.org_id});
            this.dgvOrg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrg.EnableHeadersVisualStyles = false;
            this.dgvOrg.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvOrg.Location = new System.Drawing.Point(3, 3);
            this.dgvOrg.MultiSelect = false;
            this.dgvOrg.Name = "dgvOrg";
            this.dgvOrg.ReadOnly = true;
            this.dgvOrg.RowHeadersVisible = false;
            this.dgvOrg.RowHeadersWidth = 30;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvOrg.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvOrg.RowTemplate.Height = 23;
            this.dgvOrg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrg.ShowCheckBox = false;
            this.dgvOrg.Size = new System.Drawing.Size(667, 240);
            this.dgvOrg.TabIndex = 0;
            this.dgvOrg.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrg_CellDoubleClick);
            // 
            // com_code
            // 
            this.com_code.DataPropertyName = "com_code";
            this.com_code.HeaderText = "公司编码";
            this.com_code.Name = "com_code";
            this.com_code.ReadOnly = true;
            // 
            // com_name
            // 
            this.com_name.DataPropertyName = "com_name";
            this.com_name.HeaderText = "公司简称";
            this.com_name.Name = "com_name";
            this.com_name.ReadOnly = true;
            // 
            // org_code
            // 
            this.org_code.DataPropertyName = "org_code";
            this.org_code.HeaderText = "组织编码";
            this.org_code.Name = "org_code";
            this.org_code.ReadOnly = true;
            this.org_code.Width = 150;
            // 
            // org_name
            // 
            this.org_name.DataPropertyName = "org_name";
            this.org_name.HeaderText = "组织名称";
            this.org_name.Name = "org_name";
            this.org_name.ReadOnly = true;
            // 
            // legal_person
            // 
            this.legal_person.DataPropertyName = "legal_person";
            this.legal_person.HeaderText = "法人|负责人";
            this.legal_person.Name = "legal_person";
            this.legal_person.ReadOnly = true;
            // 
            // com_contact
            // 
            this.com_contact.DataPropertyName = "com_contact";
            this.com_contact.HeaderText = "公司联系人";
            this.com_contact.Name = "com_contact";
            this.com_contact.ReadOnly = true;
            // 
            // com_tel
            // 
            this.com_tel.DataPropertyName = "com_tel";
            this.com_tel.HeaderText = "公司联系电话";
            this.com_tel.Name = "com_tel";
            this.com_tel.ReadOnly = true;
            this.com_tel.Width = 120;
            // 
            // contact_name
            // 
            this.contact_name.DataPropertyName = "contact_name";
            this.contact_name.HeaderText = "组织联系人";
            this.contact_name.Name = "contact_name";
            this.contact_name.ReadOnly = true;
            this.contact_name.Width = 200;
            // 
            // contact_telephone
            // 
            this.contact_telephone.DataPropertyName = "contact_telephone";
            this.contact_telephone.HeaderText = "组织联系电话";
            this.contact_telephone.Name = "contact_telephone";
            this.contact_telephone.ReadOnly = true;
            this.contact_telephone.Width = 120;
            // 
            // org_id
            // 
            this.org_id.DataPropertyName = "org_id";
            this.org_id.HeaderText = "org_id";
            this.org_id.Name = "org_id";
            this.org_id.ReadOnly = true;
            this.org_id.Visible = false;
            // 
            // frmOrganization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 430);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmOrganization";
            this.Text = "选择组织";
            this.Load += new System.EventHandler(this.frmOrganization_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx pnlSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnSubmit;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.TextBoxEx txtlegal_person;
        private System.Windows.Forms.Label label7;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcontact_name;
        private System.Windows.Forms.Label label6;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcom_name;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.TextBoxEx txtorg_name;
        private System.Windows.Forms.Label label4;
        private ServiceStationClient.ComponentUI.TextBoxEx txtPhone;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCom_code;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx txtorg_code;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvOrg;
        private System.Windows.Forms.DataGridViewTextBoxColumn com_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn com_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn legal_person;
        private System.Windows.Forms.DataGridViewTextBoxColumn com_contact;
        private System.Windows.Forms.DataGridViewTextBoxColumn com_tel;
        private System.Windows.Forms.DataGridViewTextBoxColumn contact_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn contact_telephone;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_id;
    }
}