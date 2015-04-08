namespace HXCPcClient.Chooser
{
    partial class frmChooseCompany
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChooseCompany));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlSearch = new ServiceStationClient.ComponentUI.PanelEx();
            this.btnSubmit = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.txtcom_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label7 = new System.Windows.Forms.Label();
            this.txtcom_type = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label6 = new System.Windows.Forms.Label();
            this.txtcom_tel = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.txtcom_short_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.txtrepair_qualification = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLegal_person = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCom_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvCompany = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colcom_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcom_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_sap_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcom_short_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcom_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colrepair_qualification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcom_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.collegal_person = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcom_tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Colremark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.winFormPager1 = new ServiceStationClient.ComponentUI.WinFormPager();
            this.pnlContainer.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompany)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.pnlSearch);
            this.pnlContainer.Location = new System.Drawing.Point(1, 29);
            this.pnlContainer.Size = new System.Drawing.Size(708, 339);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlSearch.BorderWidth = 1;
            this.pnlSearch.Controls.Add(this.btnSubmit);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Controls.Add(this.txtcom_name);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.txtcom_type);
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.txtcom_tel);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Controls.Add(this.txtcom_short_name);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.txtrepair_qualification);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.txtLegal_person);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.txtCom_code);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Curvature = 0;
            this.pnlSearch.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlSearch.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlSearch.Location = new System.Drawing.Point(1, 2);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(706, 114);
            this.pnlSearch.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Caption = "查询";
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSubmit.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSubmit.Location = new System.Drawing.Point(616, 57);
            this.btnSubmit.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSubmit.Size = new System.Drawing.Size(80, 24);
            this.btnSubmit.TabIndex = 17;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Location = new System.Drawing.Point(616, 23);
            this.btnClear.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.Size = new System.Drawing.Size(80, 24);
            this.btnClear.TabIndex = 16;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtcom_name
            // 
            this.txtcom_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcom_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcom_name.BackColor = System.Drawing.Color.Transparent;
            this.txtcom_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcom_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcom_name.ForeImage = null;
            this.txtcom_name.Location = new System.Drawing.Point(76, 75);
            this.txtcom_name.MaxLengh = 32767;
            this.txtcom_name.Multiline = false;
            this.txtcom_name.Name = "txtcom_name";
            this.txtcom_name.Radius = 3;
            this.txtcom_name.ReadOnly = false;
            this.txtcom_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcom_name.ShowError = false;
            this.txtcom_name.Size = new System.Drawing.Size(120, 23);
            this.txtcom_name.TabIndex = 13;
            this.txtcom_name.UseSystemPasswordChar = false;
            this.txtcom_name.Value = "";
            this.txtcom_name.VerifyCondition = null;
            this.txtcom_name.VerifyType = null;
            this.txtcom_name.WaterMark = null;
            this.txtcom_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "公司全称：";
            // 
            // txtcom_type
            // 
            this.txtcom_type.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcom_type.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcom_type.BackColor = System.Drawing.Color.Transparent;
            this.txtcom_type.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcom_type.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcom_type.ForeImage = null;
            this.txtcom_type.Location = new System.Drawing.Point(478, 41);
            this.txtcom_type.MaxLengh = 32767;
            this.txtcom_type.Multiline = false;
            this.txtcom_type.Name = "txtcom_type";
            this.txtcom_type.Radius = 3;
            this.txtcom_type.ReadOnly = false;
            this.txtcom_type.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcom_type.ShowError = false;
            this.txtcom_type.Size = new System.Drawing.Size(120, 23);
            this.txtcom_type.TabIndex = 11;
            this.txtcom_type.UseSystemPasswordChar = false;
            this.txtcom_type.Value = "";
            this.txtcom_type.VerifyCondition = null;
            this.txtcom_type.VerifyType = null;
            this.txtcom_type.WaterMark = null;
            this.txtcom_type.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(416, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "公司类型：";
            // 
            // txtcom_tel
            // 
            this.txtcom_tel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcom_tel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcom_tel.BackColor = System.Drawing.Color.Transparent;
            this.txtcom_tel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcom_tel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcom_tel.ForeImage = null;
            this.txtcom_tel.Location = new System.Drawing.Point(286, 41);
            this.txtcom_tel.MaxLengh = 32767;
            this.txtcom_tel.Multiline = false;
            this.txtcom_tel.Name = "txtcom_tel";
            this.txtcom_tel.Radius = 3;
            this.txtcom_tel.ReadOnly = false;
            this.txtcom_tel.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcom_tel.ShowError = false;
            this.txtcom_tel.Size = new System.Drawing.Size(120, 23);
            this.txtcom_tel.TabIndex = 9;
            this.txtcom_tel.UseSystemPasswordChar = false;
            this.txtcom_tel.Value = "";
            this.txtcom_tel.VerifyCondition = null;
            this.txtcom_tel.VerifyType = null;
            this.txtcom_tel.WaterMark = null;
            this.txtcom_tel.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(220, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "联系电话：";
            // 
            // txtcom_short_name
            // 
            this.txtcom_short_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcom_short_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcom_short_name.BackColor = System.Drawing.Color.Transparent;
            this.txtcom_short_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcom_short_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcom_short_name.ForeImage = null;
            this.txtcom_short_name.Location = new System.Drawing.Point(76, 41);
            this.txtcom_short_name.MaxLengh = 32767;
            this.txtcom_short_name.Multiline = false;
            this.txtcom_short_name.Name = "txtcom_short_name";
            this.txtcom_short_name.Radius = 3;
            this.txtcom_short_name.ReadOnly = false;
            this.txtcom_short_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcom_short_name.ShowError = false;
            this.txtcom_short_name.Size = new System.Drawing.Size(120, 23);
            this.txtcom_short_name.TabIndex = 7;
            this.txtcom_short_name.UseSystemPasswordChar = false;
            this.txtcom_short_name.Value = "";
            this.txtcom_short_name.VerifyCondition = null;
            this.txtcom_short_name.VerifyType = null;
            this.txtcom_short_name.WaterMark = null;
            this.txtcom_short_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "公司简称：";
            // 
            // txtrepair_qualification
            // 
            this.txtrepair_qualification.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtrepair_qualification.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtrepair_qualification.BackColor = System.Drawing.Color.Transparent;
            this.txtrepair_qualification.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtrepair_qualification.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtrepair_qualification.ForeImage = null;
            this.txtrepair_qualification.Location = new System.Drawing.Point(478, 8);
            this.txtrepair_qualification.MaxLengh = 32767;
            this.txtrepair_qualification.Multiline = false;
            this.txtrepair_qualification.Name = "txtrepair_qualification";
            this.txtrepair_qualification.Radius = 3;
            this.txtrepair_qualification.ReadOnly = false;
            this.txtrepair_qualification.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtrepair_qualification.ShowError = false;
            this.txtrepair_qualification.Size = new System.Drawing.Size(120, 23);
            this.txtrepair_qualification.TabIndex = 5;
            this.txtrepair_qualification.UseSystemPasswordChar = false;
            this.txtrepair_qualification.Value = "";
            this.txtrepair_qualification.VerifyCondition = null;
            this.txtrepair_qualification.VerifyType = null;
            this.txtrepair_qualification.WaterMark = null;
            this.txtrepair_qualification.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(416, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "维修资质：";
            // 
            // txtLegal_person
            // 
            this.txtLegal_person.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtLegal_person.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtLegal_person.BackColor = System.Drawing.Color.Transparent;
            this.txtLegal_person.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtLegal_person.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtLegal_person.ForeImage = null;
            this.txtLegal_person.Location = new System.Drawing.Point(286, 8);
            this.txtLegal_person.MaxLengh = 32767;
            this.txtLegal_person.Multiline = false;
            this.txtLegal_person.Name = "txtLegal_person";
            this.txtLegal_person.Radius = 3;
            this.txtLegal_person.ReadOnly = false;
            this.txtLegal_person.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtLegal_person.ShowError = false;
            this.txtLegal_person.Size = new System.Drawing.Size(120, 23);
            this.txtLegal_person.TabIndex = 3;
            this.txtLegal_person.UseSystemPasswordChar = false;
            this.txtLegal_person.Value = "";
            this.txtLegal_person.VerifyCondition = null;
            this.txtLegal_person.VerifyType = null;
            this.txtLegal_person.WaterMark = null;
            this.txtLegal_person.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "法人|负责人：";
            // 
            // txtCom_code
            // 
            this.txtCom_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCom_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCom_code.BackColor = System.Drawing.Color.Transparent;
            this.txtCom_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCom_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCom_code.ForeImage = null;
            this.txtCom_code.Location = new System.Drawing.Point(77, 8);
            this.txtCom_code.MaxLengh = 32767;
            this.txtCom_code.Multiline = false;
            this.txtCom_code.Name = "txtCom_code";
            this.txtCom_code.Radius = 3;
            this.txtCom_code.ReadOnly = false;
            this.txtCom_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCom_code.ShowError = false;
            this.txtCom_code.Size = new System.Drawing.Size(120, 23);
            this.txtCom_code.TabIndex = 1;
            this.txtCom_code.UseSystemPasswordChar = false;
            this.txtCom_code.Value = "";
            this.txtCom_code.VerifyCondition = null;
            this.txtCom_code.VerifyType = null;
            this.txtCom_code.WaterMark = null;
            this.txtCom_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "公司编码：";
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Location = new System.Drawing.Point(1, 148);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(708, 241);
            this.tabControlEx1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvCompany);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(700, 211);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "公司信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvCompany
            // 
            this.dgvCompany.AllowUserToAddRows = false;
            this.dgvCompany.AllowUserToDeleteRows = false;
            this.dgvCompany.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvCompany.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCompany.BackgroundColor = System.Drawing.Color.White;
            this.dgvCompany.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCompany.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCompany.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompany.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.colcom_id,
            this.colcom_code,
            this.drtxt_sap_code,
            this.colcom_short_name,
            this.colcom_name,
            this.colrepair_qualification,
            this.colcom_type,
            this.collegal_person,
            this.colcom_tel,
            this.Colremark});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCompany.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCompany.EnableHeadersVisualStyles = false;
            this.dgvCompany.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvCompany.Location = new System.Drawing.Point(3, 3);
            this.dgvCompany.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvCompany.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvCompany.MergeColumnNames")));
            this.dgvCompany.MultiSelect = false;
            this.dgvCompany.Name = "dgvCompany";
            this.dgvCompany.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCompany.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCompany.RowHeadersVisible = false;
            this.dgvCompany.RowHeadersWidth = 30;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvCompany.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvCompany.RowTemplate.Height = 23;
            this.dgvCompany.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCompany.ShowCheckBox = true;
            this.dgvCompany.Size = new System.Drawing.Size(694, 205);
            this.dgvCompany.TabIndex = 0;
            this.dgvCompany.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCompany_CellDoubleClick);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 18;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 30;
            // 
            // colcom_id
            // 
            this.colcom_id.DataPropertyName = "com_id";
            this.colcom_id.HeaderText = "com_id";
            this.colcom_id.Name = "colcom_id";
            this.colcom_id.ReadOnly = true;
            this.colcom_id.Visible = false;
            // 
            // colcom_code
            // 
            this.colcom_code.DataPropertyName = "com_code";
            this.colcom_code.HeaderText = "公司编码";
            this.colcom_code.Name = "colcom_code";
            this.colcom_code.ReadOnly = true;
            // 
            // drtxt_sap_code
            // 
            this.drtxt_sap_code.DataPropertyName = "sap_code";
            this.drtxt_sap_code.HeaderText = "宇通SAP";
            this.drtxt_sap_code.Name = "drtxt_sap_code";
            this.drtxt_sap_code.ReadOnly = true;
            // 
            // colcom_short_name
            // 
            this.colcom_short_name.DataPropertyName = "com_short_name";
            this.colcom_short_name.HeaderText = "公司简称";
            this.colcom_short_name.Name = "colcom_short_name";
            this.colcom_short_name.ReadOnly = true;
            // 
            // colcom_name
            // 
            this.colcom_name.DataPropertyName = "com_name";
            this.colcom_name.HeaderText = "公司全称";
            this.colcom_name.Name = "colcom_name";
            this.colcom_name.ReadOnly = true;
            this.colcom_name.Width = 150;
            // 
            // colrepair_qualification
            // 
            this.colrepair_qualification.DataPropertyName = "repair_qualification";
            this.colrepair_qualification.HeaderText = "维修资质";
            this.colrepair_qualification.Name = "colrepair_qualification";
            this.colrepair_qualification.ReadOnly = true;
            // 
            // colcom_type
            // 
            this.colcom_type.DataPropertyName = "com_type";
            this.colcom_type.HeaderText = "公司类型";
            this.colcom_type.Name = "colcom_type";
            this.colcom_type.ReadOnly = true;
            // 
            // collegal_person
            // 
            this.collegal_person.DataPropertyName = "legal_person";
            this.collegal_person.HeaderText = "法人|负责人";
            this.collegal_person.Name = "collegal_person";
            this.collegal_person.ReadOnly = true;
            // 
            // colcom_tel
            // 
            this.colcom_tel.DataPropertyName = "com_tel";
            this.colcom_tel.HeaderText = "联系电话";
            this.colcom_tel.Name = "colcom_tel";
            this.colcom_tel.ReadOnly = true;
            // 
            // Colremark
            // 
            this.Colremark.DataPropertyName = "remark";
            this.Colremark.HeaderText = "备注";
            this.Colremark.Name = "Colremark";
            this.Colremark.ReadOnly = true;
            this.Colremark.Width = 200;
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.winFormPager1);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(2, 390);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(707, 46);
            this.panelEx1.TabIndex = 4;
            // 
            // winFormPager1
            // 
            this.winFormPager1.BackColor = System.Drawing.Color.Transparent;
            this.winFormPager1.BtnTextNext = "下页";
            this.winFormPager1.BtnTextPrevious = "上页";
            this.winFormPager1.Location = new System.Drawing.Point(252, 3);
            this.winFormPager1.Name = "winFormPager1";
            this.winFormPager1.PageCount = 0;
            this.winFormPager1.PageSize = 15;
            this.winFormPager1.RecordCount = 0;
            this.winFormPager1.Size = new System.Drawing.Size(429, 31);
            this.winFormPager1.TabIndex = 0;
            // 
            // frmChooseCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 440);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.tabControlEx1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmChooseCompany";
            this.Text = "选择公司";
            this.Controls.SetChildIndex(this.pnlContainer, 0);
            this.Controls.SetChildIndex(this.tabControlEx1, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.pnlContainer.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompany)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx pnlSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnSubmit;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcom_name;
        private System.Windows.Forms.Label label7;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcom_type;
        private System.Windows.Forms.Label label6;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcom_tel;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcom_short_name;
        private System.Windows.Forms.Label label4;
        private ServiceStationClient.ComponentUI.TextBoxEx txtrepair_qualification;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.TextBoxEx txtLegal_person;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCom_code;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvCompany;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.WinFormPager winFormPager1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcom_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcom_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_sap_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcom_short_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcom_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn colrepair_qualification;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcom_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn collegal_person;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcom_tel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colremark;
    }
}