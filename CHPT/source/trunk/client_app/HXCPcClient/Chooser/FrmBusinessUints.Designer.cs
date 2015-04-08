namespace HXCPcClient.Chooser
{
    partial class FrmBusinessUints
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBusinessUints));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnClose = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnOK = new ServiceStationClient.ComponentUI.ButtonEx();
            this.dgvBusinessUnits = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.BusinUntID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BusinUntCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BusinUntName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BusUntAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Contact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Telephone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MobilePhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BusUntEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZipCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTelephone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtContact = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtBusinUnitName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtBusinUnitCode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TreeVBusinUnt = new ServiceStationClient.ComponentUI.TreeViewEx();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBusinessUnits)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.btnClose);
            this.pnlContainer.Controls.Add(this.btnOK);
            this.pnlContainer.Controls.Add(this.dgvBusinessUnits);
            this.pnlContainer.Controls.Add(this.txtTelephone);
            this.pnlContainer.Controls.Add(this.txtContact);
            this.pnlContainer.Controls.Add(this.txtBusinUnitName);
            this.pnlContainer.Controls.Add(this.txtBusinUnitCode);
            this.pnlContainer.Controls.Add(this.btnSearch);
            this.pnlContainer.Controls.Add(this.btnClear);
            this.pnlContainer.Controls.Add(this.label5);
            this.pnlContainer.Controls.Add(this.label3);
            this.pnlContainer.Controls.Add(this.label2);
            this.pnlContainer.Controls.Add(this.label1);
            this.pnlContainer.Controls.Add(this.TreeVBusinUnt);
            this.pnlContainer.Size = new System.Drawing.Size(815, 371);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Caption = "关闭";
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClose.Location = new System.Drawing.Point(726, 337);
            this.btnClose.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClose.Name = "btnClose";
            this.btnClose.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClose.Size = new System.Drawing.Size(80, 24);
            this.btnClose.TabIndex = 31;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.Caption = "确定";
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnOK.Location = new System.Drawing.Point(635, 337);
            this.btnOK.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnOK.Size = new System.Drawing.Size(80, 24);
            this.btnOK.TabIndex = 30;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dgvBusinessUnits
            // 
            this.dgvBusinessUnits.AllowUserToAddRows = false;
            this.dgvBusinessUnits.AllowUserToDeleteRows = false;
            this.dgvBusinessUnits.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvBusinessUnits.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBusinessUnits.BackgroundColor = System.Drawing.Color.White;
            this.dgvBusinessUnits.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBusinessUnits.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBusinessUnits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBusinessUnits.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BusinUntID,
            this.BusinUntCode,
            this.BusinUntName,
            this.BusUntAddress,
            this.Contact,
            this.Telephone,
            this.MobilePhone,
            this.BusUntEmail,
            this.Fax,
            this.ZipCode});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBusinessUnits.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvBusinessUnits.EnableHeadersVisualStyles = false;
            this.dgvBusinessUnits.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvBusinessUnits.IsCheck = true;
            this.dgvBusinessUnits.Location = new System.Drawing.Point(179, 93);
            this.dgvBusinessUnits.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvBusinessUnits.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvBusinessUnits.MergeColumnNames")));
            this.dgvBusinessUnits.MultiSelect = false;
            this.dgvBusinessUnits.Name = "dgvBusinessUnits";
            this.dgvBusinessUnits.ReadOnly = true;
            this.dgvBusinessUnits.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvBusinessUnits.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvBusinessUnits.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvBusinessUnits.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvBusinessUnits.RowTemplate.Height = 23;
            this.dgvBusinessUnits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBusinessUnits.ShowCheckBox = true;
            this.dgvBusinessUnits.Size = new System.Drawing.Size(629, 238);
            this.dgvBusinessUnits.TabIndex = 29;
            this.dgvBusinessUnits.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSupper_CellDoubleClick);
            // 
            // BusinUntID
            // 
            this.BusinUntID.DataPropertyName = "sup_id";
            this.BusinUntID.HeaderText = "ID";
            this.BusinUntID.Name = "BusinUntID";
            this.BusinUntID.ReadOnly = true;
            this.BusinUntID.Visible = false;
            // 
            // BusinUntCode
            // 
            this.BusinUntCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BusinUntCode.DataPropertyName = "sup_code";
            this.BusinUntCode.HeaderText = "往来单位编码";
            this.BusinUntCode.Name = "BusinUntCode";
            this.BusinUntCode.ReadOnly = true;
            this.BusinUntCode.Width = 150;
            // 
            // BusinUntName
            // 
            this.BusinUntName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BusinUntName.DataPropertyName = "sup_full_name";
            this.BusinUntName.HeaderText = "往来单位名称";
            this.BusinUntName.Name = "BusinUntName";
            this.BusinUntName.ReadOnly = true;
            this.BusinUntName.Width = 150;
            // 
            // BusUntAddress
            // 
            this.BusUntAddress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BusUntAddress.HeaderText = "往来单位地址";
            this.BusUntAddress.Name = "BusUntAddress";
            this.BusUntAddress.ReadOnly = true;
            this.BusUntAddress.Width = 150;
            // 
            // Contact
            // 
            this.Contact.HeaderText = "联系人";
            this.Contact.Name = "Contact";
            this.Contact.ReadOnly = true;
            // 
            // Telephone
            // 
            this.Telephone.DataPropertyName = "sup_tel";
            this.Telephone.HeaderText = "电话";
            this.Telephone.Name = "Telephone";
            this.Telephone.ReadOnly = true;
            // 
            // MobilePhone
            // 
            this.MobilePhone.HeaderText = "手机";
            this.MobilePhone.Name = "MobilePhone";
            this.MobilePhone.ReadOnly = true;
            // 
            // BusUntEmail
            // 
            this.BusUntEmail.HeaderText = "邮箱";
            this.BusUntEmail.Name = "BusUntEmail";
            this.BusUntEmail.ReadOnly = true;
            // 
            // Fax
            // 
            this.Fax.DataPropertyName = "sup_fax";
            this.Fax.HeaderText = "传真";
            this.Fax.Name = "Fax";
            this.Fax.ReadOnly = true;
            // 
            // ZipCode
            // 
            this.ZipCode.DataPropertyName = "zip_code";
            this.ZipCode.HeaderText = "邮编";
            this.ZipCode.Name = "ZipCode";
            this.ZipCode.ReadOnly = true;
            // 
            // txtTelephone
            // 
            this.txtTelephone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtTelephone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtTelephone.BackColor = System.Drawing.Color.Transparent;
            this.txtTelephone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtTelephone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtTelephone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTelephone.ForeImage = null;
            this.txtTelephone.InputtingVerifyCondition = null;
            this.txtTelephone.Location = new System.Drawing.Point(523, 59);
            this.txtTelephone.MaxLengh = 32767;
            this.txtTelephone.Multiline = false;
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Radius = 3;
            this.txtTelephone.ReadOnly = false;
            this.txtTelephone.SelectStart = 0;
            this.txtTelephone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtTelephone.ShowError = false;
            this.txtTelephone.Size = new System.Drawing.Size(150, 24);
            this.txtTelephone.TabIndex = 28;
            this.txtTelephone.UseSystemPasswordChar = false;
            this.txtTelephone.Value = "";
            this.txtTelephone.VerifyCondition = null;
            this.txtTelephone.VerifyType = null;
            this.txtTelephone.VerifyTypeName = null;
            this.txtTelephone.WaterMark = null;
            this.txtTelephone.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtContact
            // 
            this.txtContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContact.BackColor = System.Drawing.Color.Transparent;
            this.txtContact.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContact.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContact.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtContact.ForeImage = null;
            this.txtContact.InputtingVerifyCondition = null;
            this.txtContact.Location = new System.Drawing.Point(523, 18);
            this.txtContact.MaxLengh = 32767;
            this.txtContact.Multiline = false;
            this.txtContact.Name = "txtContact";
            this.txtContact.Radius = 3;
            this.txtContact.ReadOnly = false;
            this.txtContact.SelectStart = 0;
            this.txtContact.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContact.ShowError = false;
            this.txtContact.Size = new System.Drawing.Size(150, 24);
            this.txtContact.TabIndex = 26;
            this.txtContact.UseSystemPasswordChar = false;
            this.txtContact.Value = "";
            this.txtContact.VerifyCondition = null;
            this.txtContact.VerifyType = null;
            this.txtContact.VerifyTypeName = null;
            this.txtContact.WaterMark = null;
            this.txtContact.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtBusinUnitName
            // 
            this.txtBusinUnitName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtBusinUnitName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtBusinUnitName.BackColor = System.Drawing.Color.Transparent;
            this.txtBusinUnitName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtBusinUnitName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtBusinUnitName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBusinUnitName.ForeImage = null;
            this.txtBusinUnitName.InputtingVerifyCondition = null;
            this.txtBusinUnitName.Location = new System.Drawing.Point(270, 59);
            this.txtBusinUnitName.MaxLengh = 32767;
            this.txtBusinUnitName.Multiline = false;
            this.txtBusinUnitName.Name = "txtBusinUnitName";
            this.txtBusinUnitName.Radius = 3;
            this.txtBusinUnitName.ReadOnly = false;
            this.txtBusinUnitName.SelectStart = 0;
            this.txtBusinUnitName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtBusinUnitName.ShowError = false;
            this.txtBusinUnitName.Size = new System.Drawing.Size(150, 24);
            this.txtBusinUnitName.TabIndex = 25;
            this.txtBusinUnitName.UseSystemPasswordChar = false;
            this.txtBusinUnitName.Value = "";
            this.txtBusinUnitName.VerifyCondition = null;
            this.txtBusinUnitName.VerifyType = null;
            this.txtBusinUnitName.VerifyTypeName = null;
            this.txtBusinUnitName.WaterMark = null;
            this.txtBusinUnitName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtBusinUnitCode
            // 
            this.txtBusinUnitCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtBusinUnitCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtBusinUnitCode.BackColor = System.Drawing.Color.Transparent;
            this.txtBusinUnitCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtBusinUnitCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtBusinUnitCode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBusinUnitCode.ForeImage = null;
            this.txtBusinUnitCode.InputtingVerifyCondition = null;
            this.txtBusinUnitCode.Location = new System.Drawing.Point(270, 18);
            this.txtBusinUnitCode.MaxLengh = 32767;
            this.txtBusinUnitCode.Multiline = false;
            this.txtBusinUnitCode.Name = "txtBusinUnitCode";
            this.txtBusinUnitCode.Radius = 3;
            this.txtBusinUnitCode.ReadOnly = false;
            this.txtBusinUnitCode.SelectStart = 0;
            this.txtBusinUnitCode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtBusinUnitCode.ShowError = false;
            this.txtBusinUnitCode.Size = new System.Drawing.Size(150, 24);
            this.txtBusinUnitCode.TabIndex = 24;
            this.txtBusinUnitCode.UseSystemPasswordChar = false;
            this.txtBusinUnitCode.Value = "";
            this.txtBusinUnitCode.VerifyCondition = null;
            this.txtBusinUnitCode.VerifyType = null;
            this.txtBusinUnitCode.VerifyTypeName = null;
            this.txtBusinUnitCode.WaterMark = null;
            this.txtBusinUnitCode.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSearch.Location = new System.Drawing.Point(724, 58);
            this.btnSearch.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSearch.Size = new System.Drawing.Size(80, 24);
            this.btnSearch.TabIndex = 23;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Location = new System.Drawing.Point(724, 17);
            this.btnClear.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.Size = new System.Drawing.Size(80, 24);
            this.btnClear.TabIndex = 22;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(480, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "电话：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(468, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "联系人：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "往来单位名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "往来单位编码：";
            // 
            // TreeVBusinUnt
            // 
            this.TreeVBusinUnt.IgnoreAutoCheck = false;
            this.TreeVBusinUnt.Location = new System.Drawing.Point(7, 8);
            this.TreeVBusinUnt.Name = "TreeVBusinUnt";
            this.TreeVBusinUnt.Size = new System.Drawing.Size(166, 323);
            this.TreeVBusinUnt.TabIndex = 16;
            // 
            // FrmBusinessUints
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(817, 402);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FrmBusinessUints";
            this.Text = "往来单位选择";
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBusinessUnits)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.ButtonEx btnClose;
        private ServiceStationClient.ComponentUI.ButtonEx btnOK;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvBusinessUnits;
        private ServiceStationClient.ComponentUI.TextBoxEx txtTelephone;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContact;
        private ServiceStationClient.ComponentUI.TextBoxEx txtBusinUnitName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtBusinUnitCode;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TreeViewEx TreeVBusinUnt;
        private System.Windows.Forms.DataGridViewTextBoxColumn BusinUntID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BusinUntCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BusinUntName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BusUntAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn Contact;
        private System.Windows.Forms.DataGridViewTextBoxColumn Telephone;
        private System.Windows.Forms.DataGridViewTextBoxColumn MobilePhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn BusUntEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fax;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZipCode;
    }
}