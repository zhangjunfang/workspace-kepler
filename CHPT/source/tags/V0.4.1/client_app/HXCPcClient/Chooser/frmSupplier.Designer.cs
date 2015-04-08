namespace HXCPcClient.Chooser
{
    partial class frmSupplier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSupplier));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tvNature = new ServiceStationClient.ComponentUI.TreeViewEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.txtSupperCode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtSupperName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtContact = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtOther = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtTelephone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.dgvSupper = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colSupperID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSupperCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSupperName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sup_type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colContact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTelephone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit_properties = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tax_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCellphone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZipCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOK = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClose = new ServiceStationClient.ComponentUI.ButtonEx();
            this.ddlCompanyNature = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.btnname = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnsave = new ServiceStationClient.ComponentUI.ButtonEx();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupper)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.btnsave);
            this.pnlContainer.Controls.Add(this.btnname);
            this.pnlContainer.Controls.Add(this.ddlCompanyNature);
            this.pnlContainer.Controls.Add(this.label6);
            this.pnlContainer.Controls.Add(this.btnClose);
            this.pnlContainer.Controls.Add(this.btnOK);
            this.pnlContainer.Controls.Add(this.dgvSupper);
            this.pnlContainer.Controls.Add(this.txtTelephone);
            this.pnlContainer.Controls.Add(this.txtOther);
            this.pnlContainer.Controls.Add(this.txtContact);
            this.pnlContainer.Controls.Add(this.txtSupperName);
            this.pnlContainer.Controls.Add(this.txtSupperCode);
            this.pnlContainer.Controls.Add(this.btnSearch);
            this.pnlContainer.Controls.Add(this.btnClear);
            this.pnlContainer.Controls.Add(this.label5);
            this.pnlContainer.Controls.Add(this.label4);
            this.pnlContainer.Controls.Add(this.label3);
            this.pnlContainer.Controls.Add(this.label2);
            this.pnlContainer.Controls.Add(this.label1);
            this.pnlContainer.Controls.Add(this.tvNature);
            this.pnlContainer.Size = new System.Drawing.Size(882, 371);
            // 
            // tvNature
            // 
            this.tvNature.IgnoreAutoCheck = false;
            this.tvNature.Location = new System.Drawing.Point(3, 3);
            this.tvNature.Name = "tvNature";
            this.tvNature.Size = new System.Drawing.Size(166, 323);
            this.tvNature.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "供应商编码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "供应商名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(402, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "联系人：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(404, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "其他：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(567, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "电话：";
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Location = new System.Drawing.Point(787, 12);
            this.btnClear.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.Size = new System.Drawing.Size(80, 24);
            this.btnClear.TabIndex = 6;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSearch.Location = new System.Drawing.Point(786, 54);
            this.btnSearch.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSearch.Size = new System.Drawing.Size(80, 24);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSupperCode
            // 
            this.txtSupperCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtSupperCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtSupperCode.BackColor = System.Drawing.Color.Transparent;
            this.txtSupperCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtSupperCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtSupperCode.ForeImage = null;
            this.txtSupperCode.InputtingVerifyCondition = null;
            this.txtSupperCode.Location = new System.Drawing.Point(281, 13);
            this.txtSupperCode.MaxLengh = 32767;
            this.txtSupperCode.Multiline = false;
            this.txtSupperCode.Name = "txtSupperCode";
            this.txtSupperCode.Radius = 3;
            this.txtSupperCode.ReadOnly = false;
            this.txtSupperCode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtSupperCode.ShowError = false;
            this.txtSupperCode.Size = new System.Drawing.Size(100, 23);
            this.txtSupperCode.TabIndex = 8;
            this.txtSupperCode.UseSystemPasswordChar = false;
            this.txtSupperCode.Value = "";
            this.txtSupperCode.VerifyCondition = null;
            this.txtSupperCode.VerifyType = null;
            this.txtSupperCode.VerifyTypeName = null;
            this.txtSupperCode.WaterMark = null;
            this.txtSupperCode.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtSupperName
            // 
            this.txtSupperName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtSupperName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtSupperName.BackColor = System.Drawing.Color.Transparent;
            this.txtSupperName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtSupperName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtSupperName.ForeImage = null;
            this.txtSupperName.InputtingVerifyCondition = null;
            this.txtSupperName.Location = new System.Drawing.Point(281, 54);
            this.txtSupperName.MaxLengh = 32767;
            this.txtSupperName.Multiline = false;
            this.txtSupperName.Name = "txtSupperName";
            this.txtSupperName.Radius = 3;
            this.txtSupperName.ReadOnly = false;
            this.txtSupperName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtSupperName.ShowError = false;
            this.txtSupperName.Size = new System.Drawing.Size(100, 23);
            this.txtSupperName.TabIndex = 9;
            this.txtSupperName.UseSystemPasswordChar = false;
            this.txtSupperName.Value = "";
            this.txtSupperName.VerifyCondition = null;
            this.txtSupperName.VerifyType = null;
            this.txtSupperName.VerifyTypeName = null;
            this.txtSupperName.WaterMark = null;
            this.txtSupperName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtContact
            // 
            this.txtContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContact.BackColor = System.Drawing.Color.Transparent;
            this.txtContact.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContact.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContact.ForeImage = null;
            this.txtContact.InputtingVerifyCondition = null;
            this.txtContact.Location = new System.Drawing.Point(461, 13);
            this.txtContact.MaxLengh = 32767;
            this.txtContact.Multiline = false;
            this.txtContact.Name = "txtContact";
            this.txtContact.Radius = 3;
            this.txtContact.ReadOnly = false;
            this.txtContact.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContact.ShowError = false;
            this.txtContact.Size = new System.Drawing.Size(100, 23);
            this.txtContact.TabIndex = 10;
            this.txtContact.UseSystemPasswordChar = false;
            this.txtContact.Value = "";
            this.txtContact.VerifyCondition = null;
            this.txtContact.VerifyType = null;
            this.txtContact.VerifyTypeName = null;
            this.txtContact.WaterMark = null;
            this.txtContact.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtOther
            // 
            this.txtOther.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtOther.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtOther.BackColor = System.Drawing.Color.Transparent;
            this.txtOther.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtOther.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtOther.ForeImage = null;
            this.txtOther.InputtingVerifyCondition = null;
            this.txtOther.Location = new System.Drawing.Point(460, 54);
            this.txtOther.MaxLengh = 32767;
            this.txtOther.Multiline = false;
            this.txtOther.Name = "txtOther";
            this.txtOther.Radius = 3;
            this.txtOther.ReadOnly = false;
            this.txtOther.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtOther.ShowError = false;
            this.txtOther.Size = new System.Drawing.Size(100, 23);
            this.txtOther.TabIndex = 11;
            this.txtOther.UseSystemPasswordChar = false;
            this.txtOther.Value = "";
            this.txtOther.VerifyCondition = null;
            this.txtOther.VerifyType = null;
            this.txtOther.VerifyTypeName = null;
            this.txtOther.WaterMark = null;
            this.txtOther.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtTelephone
            // 
            this.txtTelephone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtTelephone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtTelephone.BackColor = System.Drawing.Color.Transparent;
            this.txtTelephone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtTelephone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtTelephone.ForeImage = null;
            this.txtTelephone.InputtingVerifyCondition = null;
            this.txtTelephone.Location = new System.Drawing.Point(637, 13);
            this.txtTelephone.MaxLengh = 32767;
            this.txtTelephone.Multiline = false;
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Radius = 3;
            this.txtTelephone.ReadOnly = false;
            this.txtTelephone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtTelephone.ShowError = false;
            this.txtTelephone.Size = new System.Drawing.Size(118, 23);
            this.txtTelephone.TabIndex = 12;
            this.txtTelephone.UseSystemPasswordChar = false;
            this.txtTelephone.Value = "";
            this.txtTelephone.VerifyCondition = null;
            this.txtTelephone.VerifyType = null;
            this.txtTelephone.VerifyTypeName = null;
            this.txtTelephone.WaterMark = null;
            this.txtTelephone.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // dgvSupper
            // 
            this.dgvSupper.AllowUserToAddRows = false;
            this.dgvSupper.AllowUserToDeleteRows = false;
            this.dgvSupper.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvSupper.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSupper.BackgroundColor = System.Drawing.Color.White;
            this.dgvSupper.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSupper.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSupper.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSupper.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSupperID,
            this.colSupperCode,
            this.colSupperName,
            this.sup_type,
            this.colContact,
            this.colTelephone,
            this.unit_properties,
            this.tax_num,
            this.colCellphone,
            this.colFax,
            this.colZipCode});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSupper.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSupper.EnableHeadersVisualStyles = false;
            this.dgvSupper.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvSupper.IsCheck = true;
            this.dgvSupper.Location = new System.Drawing.Point(175, 88);
            this.dgvSupper.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvSupper.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvSupper.MergeColumnNames")));
            this.dgvSupper.MultiSelect = false;
            this.dgvSupper.Name = "dgvSupper";
            this.dgvSupper.ReadOnly = true;
            this.dgvSupper.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvSupper.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSupper.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvSupper.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSupper.RowTemplate.Height = 23;
            this.dgvSupper.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSupper.ShowCheckBox = true;
            this.dgvSupper.Size = new System.Drawing.Size(692, 238);
            this.dgvSupper.TabIndex = 13;
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
            // sup_type
            // 
            this.sup_type.DataPropertyName = "sup_type";
            this.sup_type.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.sup_type.HeaderText = "供应商分类";
            this.sup_type.Name = "sup_type";
            this.sup_type.ReadOnly = true;
            this.sup_type.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sup_type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
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
            // unit_properties
            // 
            this.unit_properties.DataPropertyName = "unit_properties";
            this.unit_properties.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.unit_properties.HeaderText = "企业性质";
            this.unit_properties.Name = "unit_properties";
            this.unit_properties.ReadOnly = true;
            this.unit_properties.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // tax_num
            // 
            this.tax_num.DataPropertyName = "tax_num";
            this.tax_num.HeaderText = "税号";
            this.tax_num.Name = "tax_num";
            this.tax_num.ReadOnly = true;
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
            // btnOK
            // 
            this.btnOK.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.Caption = "确定";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnOK.Location = new System.Drawing.Point(694, 336);
            this.btnOK.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnOK.Size = new System.Drawing.Size(80, 24);
            this.btnOK.TabIndex = 14;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Caption = "关闭";
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClose.Location = new System.Drawing.Point(791, 336);
            this.btnClose.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClose.Name = "btnClose";
            this.btnClose.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClose.Size = new System.Drawing.Size(80, 24);
            this.btnClose.TabIndex = 15;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ddlCompanyNature
            // 
            this.ddlCompanyNature.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlCompanyNature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCompanyNature.FormattingEnabled = true;
            this.ddlCompanyNature.Location = new System.Drawing.Point(637, 54);
            this.ddlCompanyNature.Name = "ddlCompanyNature";
            this.ddlCompanyNature.Size = new System.Drawing.Size(118, 22);
            this.ddlCompanyNature.TabIndex = 18;
            this.ddlCompanyNature.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(567, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "企业性质：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Visible = false;
            // 
            // btnname
            // 
            this.btnname.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnname.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnname.Caption = "新增";
            this.btnname.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnname.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnname.Location = new System.Drawing.Point(175, 336);
            this.btnname.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnname.Name = "btnname";
            this.btnname.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnname.Size = new System.Drawing.Size(80, 24);
            this.btnname.TabIndex = 19;
            this.btnname.Click += new System.EventHandler(this.btnname_Click);
            // 
            // btnsave
            // 
            this.btnsave.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnsave.Caption = "当页保存";
            this.btnsave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnsave.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnsave.Location = new System.Drawing.Point(569, 336);
            this.btnsave.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnsave.Name = "btnsave";
            this.btnsave.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnsave.Size = new System.Drawing.Size(80, 24);
            this.btnsave.TabIndex = 20;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // frmSupplier
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(884, 402);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmSupplier";
            this.Text = "供应商选择";
            this.Load += new System.EventHandler(this.frmSupplier_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupper)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.ButtonEx btnClose;
        private ServiceStationClient.ComponentUI.ButtonEx btnOK;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvSupper;
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
        private ServiceStationClient.ComponentUI.TreeViewEx tvNature;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlCompanyNature;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSupperID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSupperCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSupperName;
        private System.Windows.Forms.DataGridViewComboBoxColumn sup_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContact;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTelephone;
        private System.Windows.Forms.DataGridViewComboBoxColumn unit_properties;
        private System.Windows.Forms.DataGridViewTextBoxColumn tax_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCellphone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFax;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZipCode;
        private ServiceStationClient.ComponentUI.ButtonEx btnname;
        private ServiceStationClient.ComponentUI.ButtonEx btnsave;

    }
}