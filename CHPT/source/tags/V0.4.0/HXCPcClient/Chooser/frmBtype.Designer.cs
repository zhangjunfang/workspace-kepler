namespace HXCPcClient.Chooser
{
    partial class frmBtype
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBtype));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tcBtype = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tpSup = new System.Windows.Forms.TabPage();
            this.dgvSupper = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colSupperID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSupperCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSupperName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTelephone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCellphone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZipCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTelephone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtOther = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtContact = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtSupperName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtSupperCode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tvSupType = new ServiceStationClient.ComponentUI.TreeViewEx();
            this.tpCust = new System.Windows.Forms.TabPage();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.dgvCustom = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cust_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_short_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.legal_person = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dic_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dic_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.credit_rating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearchCust = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClearCust = new ServiceStationClient.ComponentUI.ButtonEx();
            this.cobCustType = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtCustomName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtContract = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtCustomNo = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labCustType = new System.Windows.Forms.Label();
            this.labContract = new System.Windows.Forms.Label();
            this.labCustomName = new System.Windows.Forms.Label();
            this.labCustomNo = new System.Windows.Forms.Label();
            this.tvCustom = new ServiceStationClient.ComponentUI.TreeViewEx();
            this.btnOK = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClose = new ServiceStationClient.ComponentUI.ButtonEx();
            this.pnlContainer.SuspendLayout();
            this.tcBtype.SuspendLayout();
            this.tpSup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupper)).BeginInit();
            this.tpCust.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustom)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.btnClose);
            this.pnlContainer.Controls.Add(this.btnOK);
            this.pnlContainer.Size = new System.Drawing.Size(854, 425);
            // 
            // tcBtype
            // 
            this.tcBtype.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcBtype.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tcBtype.Controls.Add(this.tpSup);
            this.tcBtype.Controls.Add(this.tpCust);
            this.tcBtype.Location = new System.Drawing.Point(1, 33);
            this.tcBtype.Name = "tcBtype";
            this.tcBtype.SelectedIndex = 0;
            this.tcBtype.Size = new System.Drawing.Size(854, 369);
            this.tcBtype.TabIndex = 0;
            // 
            // tpSup
            // 
            this.tpSup.Controls.Add(this.dgvSupper);
            this.tpSup.Controls.Add(this.txtTelephone);
            this.tpSup.Controls.Add(this.txtOther);
            this.tpSup.Controls.Add(this.txtContact);
            this.tpSup.Controls.Add(this.txtSupperName);
            this.tpSup.Controls.Add(this.txtSupperCode);
            this.tpSup.Controls.Add(this.btnSearch);
            this.tpSup.Controls.Add(this.btnClear);
            this.tpSup.Controls.Add(this.label5);
            this.tpSup.Controls.Add(this.label4);
            this.tpSup.Controls.Add(this.label3);
            this.tpSup.Controls.Add(this.label2);
            this.tpSup.Controls.Add(this.label1);
            this.tpSup.Controls.Add(this.tvSupType);
            this.tpSup.Location = new System.Drawing.Point(4, 26);
            this.tpSup.Name = "tpSup";
            this.tpSup.Padding = new System.Windows.Forms.Padding(3);
            this.tpSup.Size = new System.Drawing.Size(846, 339);
            this.tpSup.TabIndex = 0;
            this.tpSup.Text = "供应商";
            this.tpSup.UseVisualStyleBackColor = true;
            // 
            // dgvSupper
            // 
            this.dgvSupper.AllowUserToAddRows = false;
            this.dgvSupper.AllowUserToDeleteRows = false;
            this.dgvSupper.AllowUserToResizeRows = false;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvSupper.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvSupper.BackgroundColor = System.Drawing.Color.White;
            this.dgvSupper.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSupper.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dgvSupper.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSupper.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSupperID,
            this.colSupperCode,
            this.colSupperName,
            this.colContact,
            this.colTelephone,
            this.colCellphone,
            this.colFax,
            this.colZipCode});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSupper.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgvSupper.EnableHeadersVisualStyles = false;
            this.dgvSupper.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvSupper.Location = new System.Drawing.Point(182, 93);
            this.dgvSupper.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvSupper.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvSupper.MergeColumnNames")));
            this.dgvSupper.MultiSelect = false;
            this.dgvSupper.Name = "dgvSupper";
            this.dgvSupper.ReadOnly = true;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSupper.RowHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvSupper.RowHeadersVisible = false;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvSupper.RowsDefaultCellStyle = dataGridViewCellStyle20;
            this.dgvSupper.RowTemplate.Height = 23;
            this.dgvSupper.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSupper.ShowCheckBox = true;
            this.dgvSupper.Size = new System.Drawing.Size(657, 238);
            this.dgvSupper.TabIndex = 27;
            this.dgvSupper.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSupper_CellDoubleClick);
            // 
            // colSupperID
            // 
            this.colSupperID.DataPropertyName = "sup_id";
            this.colSupperID.HeaderText = "ID";
            this.colSupperID.Name = "colSupperID";
            this.colSupperID.ReadOnly = true;
            this.colSupperID.Visible = false;
            // 
            // colSupperCode
            // 
            this.colSupperCode.DataPropertyName = "sup_code";
            this.colSupperCode.HeaderText = "供应商编码";
            this.colSupperCode.Name = "colSupperCode";
            this.colSupperCode.ReadOnly = true;
            // 
            // colSupperName
            // 
            this.colSupperName.DataPropertyName = "sup_full_name";
            this.colSupperName.HeaderText = "供应商名称";
            this.colSupperName.Name = "colSupperName";
            this.colSupperName.ReadOnly = true;
            // 
            // colContact
            // 
            this.colContact.HeaderText = "联系人";
            this.colContact.Name = "colContact";
            this.colContact.ReadOnly = true;
            // 
            // colTelephone
            // 
            this.colTelephone.DataPropertyName = "sup_tel";
            this.colTelephone.HeaderText = "电话";
            this.colTelephone.Name = "colTelephone";
            this.colTelephone.ReadOnly = true;
            // 
            // colCellphone
            // 
            this.colCellphone.HeaderText = "手机";
            this.colCellphone.Name = "colCellphone";
            this.colCellphone.ReadOnly = true;
            // 
            // colFax
            // 
            this.colFax.DataPropertyName = "sup_fax";
            this.colFax.HeaderText = "传真";
            this.colFax.Name = "colFax";
            this.colFax.ReadOnly = true;
            // 
            // colZipCode
            // 
            this.colZipCode.DataPropertyName = "zip_code";
            this.colZipCode.HeaderText = "邮编";
            this.colZipCode.Name = "colZipCode";
            this.colZipCode.ReadOnly = true;
            // 
            // txtTelephone
            // 
            this.txtTelephone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtTelephone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtTelephone.BackColor = System.Drawing.Color.Transparent;
            this.txtTelephone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtTelephone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtTelephone.ForeImage = null;
            this.txtTelephone.Location = new System.Drawing.Point(628, 18);
            this.txtTelephone.MaxLengh = 32767;
            this.txtTelephone.Multiline = false;
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Radius = 3;
            this.txtTelephone.ReadOnly = false;
            this.txtTelephone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtTelephone.ShowError = false;
            this.txtTelephone.Size = new System.Drawing.Size(100, 23);
            this.txtTelephone.TabIndex = 26;
            this.txtTelephone.UseSystemPasswordChar = false;
            this.txtTelephone.Value = "";
            this.txtTelephone.VerifyCondition = null;
            this.txtTelephone.VerifyType = null;
            this.txtTelephone.WaterMark = null;
            this.txtTelephone.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtOther
            // 
            this.txtOther.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtOther.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtOther.BackColor = System.Drawing.Color.Transparent;
            this.txtOther.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtOther.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtOther.ForeImage = null;
            this.txtOther.Location = new System.Drawing.Point(470, 59);
            this.txtOther.MaxLengh = 32767;
            this.txtOther.Multiline = false;
            this.txtOther.Name = "txtOther";
            this.txtOther.Radius = 3;
            this.txtOther.ReadOnly = false;
            this.txtOther.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtOther.ShowError = false;
            this.txtOther.Size = new System.Drawing.Size(100, 23);
            this.txtOther.TabIndex = 25;
            this.txtOther.UseSystemPasswordChar = false;
            this.txtOther.Value = "";
            this.txtOther.VerifyCondition = null;
            this.txtOther.VerifyType = null;
            this.txtOther.WaterMark = null;
            this.txtOther.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtContact
            // 
            this.txtContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContact.BackColor = System.Drawing.Color.Transparent;
            this.txtContact.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContact.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContact.ForeImage = null;
            this.txtContact.Location = new System.Drawing.Point(468, 18);
            this.txtContact.MaxLengh = 32767;
            this.txtContact.Multiline = false;
            this.txtContact.Name = "txtContact";
            this.txtContact.Radius = 3;
            this.txtContact.ReadOnly = false;
            this.txtContact.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContact.ShowError = false;
            this.txtContact.Size = new System.Drawing.Size(100, 23);
            this.txtContact.TabIndex = 24;
            this.txtContact.UseSystemPasswordChar = false;
            this.txtContact.Value = "";
            this.txtContact.VerifyCondition = null;
            this.txtContact.VerifyType = null;
            this.txtContact.WaterMark = null;
            this.txtContact.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtSupperName
            // 
            this.txtSupperName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtSupperName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtSupperName.BackColor = System.Drawing.Color.Transparent;
            this.txtSupperName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtSupperName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtSupperName.ForeImage = null;
            this.txtSupperName.Location = new System.Drawing.Point(288, 59);
            this.txtSupperName.MaxLengh = 32767;
            this.txtSupperName.Multiline = false;
            this.txtSupperName.Name = "txtSupperName";
            this.txtSupperName.Radius = 3;
            this.txtSupperName.ReadOnly = false;
            this.txtSupperName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtSupperName.ShowError = false;
            this.txtSupperName.Size = new System.Drawing.Size(100, 23);
            this.txtSupperName.TabIndex = 23;
            this.txtSupperName.UseSystemPasswordChar = false;
            this.txtSupperName.Value = "";
            this.txtSupperName.VerifyCondition = null;
            this.txtSupperName.VerifyType = null;
            this.txtSupperName.WaterMark = null;
            this.txtSupperName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtSupperCode
            // 
            this.txtSupperCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtSupperCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtSupperCode.BackColor = System.Drawing.Color.Transparent;
            this.txtSupperCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtSupperCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtSupperCode.ForeImage = null;
            this.txtSupperCode.Location = new System.Drawing.Point(288, 18);
            this.txtSupperCode.MaxLengh = 32767;
            this.txtSupperCode.Multiline = false;
            this.txtSupperCode.Name = "txtSupperCode";
            this.txtSupperCode.Radius = 3;
            this.txtSupperCode.ReadOnly = false;
            this.txtSupperCode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtSupperCode.ShowError = false;
            this.txtSupperCode.Size = new System.Drawing.Size(100, 23);
            this.txtSupperCode.TabIndex = 22;
            this.txtSupperCode.UseSystemPasswordChar = false;
            this.txtSupperCode.Value = "";
            this.txtSupperCode.VerifyCondition = null;
            this.txtSupperCode.VerifyType = null;
            this.txtSupperCode.WaterMark = null;
            this.txtSupperCode.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(751, 60);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(60, 26);
            this.btnSearch.TabIndex = 21;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(751, 25);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 20;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(581, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "电话：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(421, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "其他：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(409, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "联系人：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "供应商名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(205, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "供应商编码：";
            // 
            // tvSupType
            // 
            this.tvSupType.IgnoreAutoCheck = false;
            this.tvSupType.Location = new System.Drawing.Point(10, 8);
            this.tvSupType.Name = "tvSupType";
            this.tvSupType.Size = new System.Drawing.Size(166, 323);
            this.tvSupType.TabIndex = 14;
            // 
            // tpCust
            // 
            this.tpCust.Controls.Add(this.page);
            this.tpCust.Controls.Add(this.dgvCustom);
            this.tpCust.Controls.Add(this.btnSearchCust);
            this.tpCust.Controls.Add(this.btnClearCust);
            this.tpCust.Controls.Add(this.cobCustType);
            this.tpCust.Controls.Add(this.txtCustomName);
            this.tpCust.Controls.Add(this.txtContract);
            this.tpCust.Controls.Add(this.txtCustomNo);
            this.tpCust.Controls.Add(this.labCustType);
            this.tpCust.Controls.Add(this.labContract);
            this.tpCust.Controls.Add(this.labCustomName);
            this.tpCust.Controls.Add(this.labCustomNo);
            this.tpCust.Controls.Add(this.tvCustom);
            this.tpCust.Location = new System.Drawing.Point(4, 26);
            this.tpCust.Name = "tpCust";
            this.tpCust.Padding = new System.Windows.Forms.Padding(3);
            this.tpCust.Size = new System.Drawing.Size(846, 339);
            this.tpCust.TabIndex = 1;
            this.tpCust.Text = "客户";
            this.tpCust.UseVisualStyleBackColor = true;
            // 
            // page
            // 
            this.page.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.page.Location = new System.Drawing.Point(315, 302);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(524, 31);
            this.page.TabIndex = 42;
            this.page.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            // 
            // dgvCustom
            // 
            this.dgvCustom.AllowUserToAddRows = false;
            this.dgvCustom.AllowUserToDeleteRows = false;
            this.dgvCustom.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvCustom.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvCustom.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustom.BackgroundColor = System.Drawing.Color.White;
            this.dgvCustom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustom.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvCustom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustom.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.cust_code,
            this.cust_short_name,
            this.cust_name,
            this.legal_person,
            this.dic_code,
            this.dic_name,
            this.credit_rating,
            this.cust_remark,
            this.cust_id});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustom.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvCustom.EnableHeadersVisualStyles = false;
            this.dgvCustom.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvCustom.IsCheck = true;
            this.dgvCustom.Location = new System.Drawing.Point(175, 67);
            this.dgvCustom.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvCustom.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvCustom.MergeColumnNames")));
            this.dgvCustom.MultiSelect = false;
            this.dgvCustom.Name = "dgvCustom";
            this.dgvCustom.ReadOnly = true;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustom.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvCustom.RowHeadersVisible = false;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvCustom.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvCustom.RowTemplate.Height = 23;
            this.dgvCustom.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustom.ShowCheckBox = true;
            this.dgvCustom.Size = new System.Drawing.Size(664, 229);
            this.dgvCustom.TabIndex = 41;
            this.ToolTip.SetToolTip(this.dgvCustom, "请双击选择");
            this.dgvCustom.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustom_CellDoubleClick);
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
            // cust_code
            // 
            this.cust_code.DataPropertyName = "cust_code";
            this.cust_code.FillWeight = 365.4822F;
            this.cust_code.HeaderText = "客户编码";
            this.cust_code.MinimumWidth = 90;
            this.cust_code.Name = "cust_code";
            this.cust_code.ReadOnly = true;
            // 
            // cust_short_name
            // 
            this.cust_short_name.DataPropertyName = "cust_short_name";
            this.cust_short_name.FillWeight = 1.957035F;
            this.cust_short_name.HeaderText = "客户简称";
            this.cust_short_name.MinimumWidth = 90;
            this.cust_short_name.Name = "cust_short_name";
            this.cust_short_name.ReadOnly = true;
            // 
            // cust_name
            // 
            this.cust_name.DataPropertyName = "cust_name";
            this.cust_name.FillWeight = 18.9564F;
            this.cust_name.HeaderText = "客户全称";
            this.cust_name.MinimumWidth = 90;
            this.cust_name.Name = "cust_name";
            this.cust_name.ReadOnly = true;
            // 
            // legal_person
            // 
            this.legal_person.DataPropertyName = "legal_person";
            this.legal_person.HeaderText = "联系人";
            this.legal_person.MinimumWidth = 100;
            this.legal_person.Name = "legal_person";
            this.legal_person.ReadOnly = true;
            // 
            // dic_code
            // 
            this.dic_code.DataPropertyName = "dic_code";
            this.dic_code.FillWeight = 28.90812F;
            this.dic_code.HeaderText = "客户分类编码";
            this.dic_code.MinimumWidth = 110;
            this.dic_code.Name = "dic_code";
            this.dic_code.ReadOnly = true;
            // 
            // dic_name
            // 
            this.dic_name.DataPropertyName = "dic_name";
            this.dic_name.FillWeight = 65.22065F;
            this.dic_name.HeaderText = "客户分类名称";
            this.dic_name.MinimumWidth = 110;
            this.dic_name.Name = "dic_name";
            this.dic_name.ReadOnly = true;
            // 
            // credit_rating
            // 
            this.credit_rating.DataPropertyName = "credit_rating";
            this.credit_rating.FillWeight = 98.08826F;
            this.credit_rating.HeaderText = "客户等级";
            this.credit_rating.MinimumWidth = 90;
            this.credit_rating.Name = "credit_rating";
            this.credit_rating.ReadOnly = true;
            // 
            // cust_remark
            // 
            this.cust_remark.DataPropertyName = "cust_remark";
            this.cust_remark.FillWeight = 221.1506F;
            this.cust_remark.HeaderText = "备注";
            this.cust_remark.MinimumWidth = 100;
            this.cust_remark.Name = "cust_remark";
            this.cust_remark.ReadOnly = true;
            // 
            // cust_id
            // 
            this.cust_id.DataPropertyName = "cust_id";
            this.cust_id.HeaderText = "cust_id";
            this.cust_id.Name = "cust_id";
            this.cust_id.ReadOnly = true;
            this.cust_id.Visible = false;
            // 
            // btnSearchCust
            // 
            this.btnSearchCust.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchCust.BackgroundImage")));
            this.btnSearchCust.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchCust.Caption = "查询";
            this.btnSearchCust.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearchCust.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearchCust.DownImage")));
            this.btnSearchCust.Location = new System.Drawing.Point(623, 35);
            this.btnSearchCust.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearchCust.MoveImage")));
            this.btnSearchCust.Name = "btnSearchCust";
            this.btnSearchCust.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearchCust.NormalImage")));
            this.btnSearchCust.Size = new System.Drawing.Size(60, 26);
            this.btnSearchCust.TabIndex = 40;
            this.btnSearchCust.Click += new System.EventHandler(this.btnSearchCust_Click);
            // 
            // btnClearCust
            // 
            this.btnClearCust.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearCust.BackgroundImage")));
            this.btnClearCust.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearCust.Caption = "清除";
            this.btnClearCust.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClearCust.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClearCust.DownImage")));
            this.btnClearCust.Location = new System.Drawing.Point(623, 6);
            this.btnClearCust.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClearCust.MoveImage")));
            this.btnClearCust.Name = "btnClearCust";
            this.btnClearCust.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClearCust.NormalImage")));
            this.btnClearCust.Size = new System.Drawing.Size(60, 26);
            this.btnClearCust.TabIndex = 39;
            this.btnClearCust.Click += new System.EventHandler(this.btnClearCust_Click);
            // 
            // cobCustType
            // 
            this.cobCustType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobCustType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobCustType.FormattingEnabled = true;
            this.cobCustType.Location = new System.Drawing.Point(477, 35);
            this.cobCustType.Name = "cobCustType";
            this.cobCustType.Size = new System.Drawing.Size(121, 22);
            this.cobCustType.TabIndex = 38;
            // 
            // txtCustomName
            // 
            this.txtCustomName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCustomName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCustomName.BackColor = System.Drawing.Color.Transparent;
            this.txtCustomName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCustomName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCustomName.ForeImage = null;
            this.txtCustomName.Location = new System.Drawing.Point(278, 35);
            this.txtCustomName.MaxLengh = 32767;
            this.txtCustomName.Multiline = false;
            this.txtCustomName.Name = "txtCustomName";
            this.txtCustomName.Radius = 3;
            this.txtCustomName.ReadOnly = false;
            this.txtCustomName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCustomName.ShowError = false;
            this.txtCustomName.Size = new System.Drawing.Size(121, 23);
            this.txtCustomName.TabIndex = 37;
            this.txtCustomName.UseSystemPasswordChar = false;
            this.txtCustomName.Value = "";
            this.txtCustomName.VerifyCondition = null;
            this.txtCustomName.VerifyType = null;
            this.txtCustomName.WaterMark = null;
            this.txtCustomName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtContract
            // 
            this.txtContract.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContract.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContract.BackColor = System.Drawing.Color.Transparent;
            this.txtContract.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContract.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContract.ForeImage = null;
            this.txtContract.Location = new System.Drawing.Point(477, 6);
            this.txtContract.MaxLengh = 32767;
            this.txtContract.Multiline = false;
            this.txtContract.Name = "txtContract";
            this.txtContract.Radius = 3;
            this.txtContract.ReadOnly = false;
            this.txtContract.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContract.ShowError = false;
            this.txtContract.Size = new System.Drawing.Size(121, 23);
            this.txtContract.TabIndex = 36;
            this.txtContract.UseSystemPasswordChar = false;
            this.txtContract.Value = "";
            this.txtContract.VerifyCondition = null;
            this.txtContract.VerifyType = null;
            this.txtContract.WaterMark = null;
            this.txtContract.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtCustomNo
            // 
            this.txtCustomNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCustomNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCustomNo.BackColor = System.Drawing.Color.Transparent;
            this.txtCustomNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCustomNo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCustomNo.ForeImage = null;
            this.txtCustomNo.Location = new System.Drawing.Point(278, 6);
            this.txtCustomNo.MaxLengh = 32767;
            this.txtCustomNo.Multiline = false;
            this.txtCustomNo.Name = "txtCustomNo";
            this.txtCustomNo.Radius = 3;
            this.txtCustomNo.ReadOnly = false;
            this.txtCustomNo.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCustomNo.ShowError = false;
            this.txtCustomNo.Size = new System.Drawing.Size(121, 23);
            this.txtCustomNo.TabIndex = 35;
            this.txtCustomNo.UseSystemPasswordChar = false;
            this.txtCustomNo.Value = "";
            this.txtCustomNo.VerifyCondition = null;
            this.txtCustomNo.VerifyType = null;
            this.txtCustomNo.WaterMark = null;
            this.txtCustomNo.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labCustType
            // 
            this.labCustType.AutoSize = true;
            this.labCustType.Location = new System.Drawing.Point(418, 41);
            this.labCustType.Name = "labCustType";
            this.labCustType.Size = new System.Drawing.Size(53, 12);
            this.labCustType.TabIndex = 34;
            this.labCustType.Text = "客户等级";
            // 
            // labContract
            // 
            this.labContract.AutoSize = true;
            this.labContract.Location = new System.Drawing.Point(430, 12);
            this.labContract.Name = "labContract";
            this.labContract.Size = new System.Drawing.Size(41, 12);
            this.labContract.TabIndex = 33;
            this.labContract.Text = "联系人";
            // 
            // labCustomName
            // 
            this.labCustomName.AutoSize = true;
            this.labCustomName.Location = new System.Drawing.Point(219, 41);
            this.labCustomName.Name = "labCustomName";
            this.labCustomName.Size = new System.Drawing.Size(53, 12);
            this.labCustomName.TabIndex = 32;
            this.labCustomName.Text = "客户名称";
            // 
            // labCustomNo
            // 
            this.labCustomNo.AutoSize = true;
            this.labCustomNo.Location = new System.Drawing.Point(219, 12);
            this.labCustomNo.Name = "labCustomNo";
            this.labCustomNo.Size = new System.Drawing.Size(53, 12);
            this.labCustomNo.TabIndex = 31;
            this.labCustomNo.Text = "客户编码";
            // 
            // tvCustom
            // 
            this.tvCustom.IgnoreAutoCheck = false;
            this.tvCustom.Location = new System.Drawing.Point(7, 6);
            this.tvCustom.Name = "tvCustom";
            this.tvCustom.Size = new System.Drawing.Size(162, 327);
            this.tvCustom.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.Caption = "确定";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.DownImage = ((System.Drawing.Image)(resources.GetObject("btnOK.DownImage")));
            this.btnOK.Location = new System.Drawing.Point(672, 378);
            this.btnOK.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnOK.MoveImage")));
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnOK.NormalImage")));
            this.btnOK.Size = new System.Drawing.Size(60, 26);
            this.btnOK.TabIndex = 0;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Caption = "取消";
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClose.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClose.DownImage")));
            this.btnClose.Location = new System.Drawing.Point(755, 378);
            this.btnClose.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClose.MoveImage")));
            this.btnClose.Name = "btnClose";
            this.btnClose.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClose.NormalImage")));
            this.btnClose.Size = new System.Drawing.Size(60, 26);
            this.btnClose.TabIndex = 1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmBtype
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 456);
            this.Controls.Add(this.tcBtype);
            this.Location = new System.Drawing.Point(0, 0);
            this.BtypeName = "frmBtype";
            this.Text = "往来单位选择器";
            this.Load += new System.EventHandler(this.frmBtype_Load);
            this.Controls.SetChildIndex(this.pnlContainer, 0);
            this.Controls.SetChildIndex(this.tcBtype, 0);
            this.pnlContainer.ResumeLayout(false);
            this.tcBtype.ResumeLayout(false);
            this.tpSup.ResumeLayout(false);
            this.tpSup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupper)).EndInit();
            this.tpCust.ResumeLayout(false);
            this.tpCust.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.TabControlEx tcBtype;
        private System.Windows.Forms.TabPage tpSup;
        private System.Windows.Forms.TabPage tpCust;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvSupper;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSupperID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSupperCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSupperName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContact;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTelephone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCellphone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFax;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZipCode;
        private ServiceStationClient.ComponentUI.TextBoxEx txtTelephone;
        private ServiceStationClient.ComponentUI.TextBoxEx txtOther;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContact;
        private ServiceStationClient.ComponentUI.TextBoxEx txtSupperName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtSupperCode;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TreeViewEx tvSupType;
        private ServiceStationClient.ComponentUI.TreeViewEx tvCustom;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearchCust;
        private ServiceStationClient.ComponentUI.ButtonEx btnClearCust;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobCustType;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCustomName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContract;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCustomNo;
        private System.Windows.Forms.Label labCustType;
        private System.Windows.Forms.Label labContract;
        private System.Windows.Forms.Label labCustomName;
        private System.Windows.Forms.Label labCustomNo;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvCustom;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_short_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn legal_person;
        private System.Windows.Forms.DataGridViewTextBoxColumn dic_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dic_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn credit_rating;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_id;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private ServiceStationClient.ComponentUI.ButtonEx btnClose;
        private ServiceStationClient.ComponentUI.ButtonEx btnOK;
    }
}