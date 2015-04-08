namespace HXCPcClient.Chooser
{
    partial class FormSignInfoChooser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSignInfoChooser));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.winFormPager1 = new ServiceStationClient.ComponentUI.WinFormPager();
            this.btnSubmit = new ServiceStationClient.ComponentUI.ButtonEx();
            this.pnlSearch = new ServiceStationClient.ComponentUI.PanelEx();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvCompany = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.drchk_check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.drtxt_sign_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_sign_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_sign_brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_com_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_sign_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_com_short_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_com_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcom_tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_approved_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.txt_com_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_com_short_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_sign_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompany)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.panelEx1);
            this.pnlContainer.Location = new System.Drawing.Point(1, 28);
            this.pnlContainer.Size = new System.Drawing.Size(681, 391);
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.winFormPager1);
            this.panelEx1.Controls.Add(this.btnSubmit);
            this.panelEx1.Controls.Add(this.pnlSearch);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.txt_com_name);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.txt_com_short_name);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.txt_sign_code);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(681, 391);
            this.panelEx1.TabIndex = 0;
            // 
            // winFormPager1
            // 
            this.winFormPager1.BackColor = System.Drawing.Color.Transparent;
            this.winFormPager1.BtnTextNext = "下页";
            this.winFormPager1.BtnTextPrevious = "上页";
            this.winFormPager1.Location = new System.Drawing.Point(246, 349);
            this.winFormPager1.Name = "winFormPager1";
            this.winFormPager1.PageCount = 0;
            this.winFormPager1.PageSize = 15;
            this.winFormPager1.RecordCount = 0;
            this.winFormPager1.Size = new System.Drawing.Size(429, 31);
            this.winFormPager1.TabIndex = 26;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Caption = "查询";
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSubmit.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSubmit.Location = new System.Drawing.Point(590, 15);
            this.btnSubmit.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSubmit.Size = new System.Drawing.Size(80, 24);
            this.btnSubmit.TabIndex = 25;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearch.BorderWidth = 1;
            this.pnlSearch.Controls.Add(this.tabControlEx1);
            this.pnlSearch.Curvature = 0;
            this.pnlSearch.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlSearch.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlSearch.Location = new System.Drawing.Point(7, 94);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSearch.Size = new System.Drawing.Size(668, 249);
            this.pnlSearch.TabIndex = 8;
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEx1.Location = new System.Drawing.Point(3, 3);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(662, 243);
            this.tabControlEx1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvCompany);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(654, 213);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "服务站信息";
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
            this.drchk_check,
            this.drtxt_sign_id,
            this.drtxt_sign_code,
            this.drtxt_sign_brand,
            this.drtxt_com_code,
            this.drtxt_sign_type,
            this.drtxt_com_short_name,
            this.drtxt_com_name,
            this.colcom_tel,
            this.drtxt_approved_time});
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
            this.dgvCompany.Size = new System.Drawing.Size(648, 207);
            this.dgvCompany.TabIndex = 0;
            // 
            // drchk_check
            // 
            this.drchk_check.HeaderText = "";
            this.drchk_check.MinimumWidth = 18;
            this.drchk_check.Name = "drchk_check";
            this.drchk_check.ReadOnly = true;
            this.drchk_check.Width = 30;
            // 
            // drtxt_sign_id
            // 
            this.drtxt_sign_id.DataPropertyName = "sign_id";
            this.drtxt_sign_id.HeaderText = "服务站标识";
            this.drtxt_sign_id.Name = "drtxt_sign_id";
            this.drtxt_sign_id.ReadOnly = true;
            this.drtxt_sign_id.Visible = false;
            // 
            // drtxt_sign_code
            // 
            this.drtxt_sign_code.DataPropertyName = "sign_code";
            this.drtxt_sign_code.HeaderText = "编码";
            this.drtxt_sign_code.Name = "drtxt_sign_code";
            this.drtxt_sign_code.ReadOnly = true;
            // 
            // drtxt_sign_brand
            // 
            this.drtxt_sign_brand.DataPropertyName = "sign_brand";
            this.drtxt_sign_brand.HeaderText = "签约品牌";
            this.drtxt_sign_brand.Name = "drtxt_sign_brand";
            this.drtxt_sign_brand.ReadOnly = true;
            // 
            // drtxt_com_code
            // 
            this.drtxt_com_code.DataPropertyName = "com_code";
            this.drtxt_com_code.HeaderText = "签约服务站编码";
            this.drtxt_com_code.Name = "drtxt_com_code";
            this.drtxt_com_code.ReadOnly = true;
            this.drtxt_com_code.Width = 150;
            // 
            // drtxt_sign_type
            // 
            this.drtxt_sign_type.DataPropertyName = "sign_type";
            this.drtxt_sign_type.HeaderText = "签约类型";
            this.drtxt_sign_type.Name = "drtxt_sign_type";
            this.drtxt_sign_type.ReadOnly = true;
            // 
            // drtxt_com_short_name
            // 
            this.drtxt_com_short_name.DataPropertyName = "com_short_name";
            this.drtxt_com_short_name.HeaderText = "服务站简称";
            this.drtxt_com_short_name.Name = "drtxt_com_short_name";
            this.drtxt_com_short_name.ReadOnly = true;
            // 
            // drtxt_com_name
            // 
            this.drtxt_com_name.DataPropertyName = "com_name";
            this.drtxt_com_name.HeaderText = "服务站全称";
            this.drtxt_com_name.Name = "drtxt_com_name";
            this.drtxt_com_name.ReadOnly = true;
            // 
            // colcom_tel
            // 
            this.colcom_tel.DataPropertyName = "com_tel";
            this.colcom_tel.HeaderText = "联系电话";
            this.colcom_tel.Name = "colcom_tel";
            this.colcom_tel.ReadOnly = true;
            // 
            // drtxt_approved_time
            // 
            this.drtxt_approved_time.DataPropertyName = "approved_time";
            this.drtxt_approved_time.HeaderText = "建站时间";
            this.drtxt_approved_time.Name = "drtxt_approved_time";
            this.drtxt_approved_time.ReadOnly = true;
            this.drtxt_approved_time.Width = 200;
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Location = new System.Drawing.Point(590, 51);
            this.btnClear.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.Size = new System.Drawing.Size(80, 24);
            this.btnClear.TabIndex = 24;
            // 
            // txt_com_name
            // 
            this.txt_com_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_com_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_com_name.BackColor = System.Drawing.Color.Transparent;
            this.txt_com_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_com_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_com_name.ForeImage = null;
            this.txt_com_name.Location = new System.Drawing.Point(99, 52);
            this.txt_com_name.MaxLengh = 32767;
            this.txt_com_name.Multiline = false;
            this.txt_com_name.Name = "txt_com_name";
            this.txt_com_name.Radius = 3;
            this.txt_com_name.ReadOnly = false;
            this.txt_com_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_com_name.ShowError = false;
            this.txt_com_name.Size = new System.Drawing.Size(120, 23);
            this.txt_com_name.TabIndex = 23;
            this.txt_com_name.UseSystemPasswordChar = false;
            this.txt_com_name.Value = "";
            this.txt_com_name.VerifyCondition = null;
            this.txt_com_name.VerifyType = null;
            this.txt_com_name.WaterMark = null;
            this.txt_com_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "服务站全称：";
            // 
            // txt_com_short_name
            // 
            this.txt_com_short_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_com_short_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_com_short_name.BackColor = System.Drawing.Color.Transparent;
            this.txt_com_short_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_com_short_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_com_short_name.ForeImage = null;
            this.txt_com_short_name.Location = new System.Drawing.Point(326, 16);
            this.txt_com_short_name.MaxLengh = 32767;
            this.txt_com_short_name.Multiline = false;
            this.txt_com_short_name.Name = "txt_com_short_name";
            this.txt_com_short_name.Radius = 3;
            this.txt_com_short_name.ReadOnly = false;
            this.txt_com_short_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_com_short_name.ShowError = false;
            this.txt_com_short_name.Size = new System.Drawing.Size(120, 23);
            this.txt_com_short_name.TabIndex = 21;
            this.txt_com_short_name.UseSystemPasswordChar = false;
            this.txt_com_short_name.Value = "";
            this.txt_com_short_name.VerifyCondition = null;
            this.txt_com_short_name.VerifyType = null;
            this.txt_com_short_name.WaterMark = null;
            this.txt_com_short_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(251, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "服务站简称：";
            // 
            // txt_sign_code
            // 
            this.txt_sign_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_sign_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_sign_code.BackColor = System.Drawing.Color.Transparent;
            this.txt_sign_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_sign_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_sign_code.ForeImage = null;
            this.txt_sign_code.Location = new System.Drawing.Point(99, 16);
            this.txt_sign_code.MaxLengh = 32767;
            this.txt_sign_code.Multiline = false;
            this.txt_sign_code.Name = "txt_sign_code";
            this.txt_sign_code.Radius = 3;
            this.txt_sign_code.ReadOnly = false;
            this.txt_sign_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_sign_code.ShowError = false;
            this.txt_sign_code.Size = new System.Drawing.Size(120, 23);
            this.txt_sign_code.TabIndex = 19;
            this.txt_sign_code.UseSystemPasswordChar = false;
            this.txt_sign_code.Value = "";
            this.txt_sign_code.VerifyCondition = null;
            this.txt_sign_code.VerifyType = null;
            this.txt_sign_code.WaterMark = null;
            this.txt_sign_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "服务站编码：";
            // 
            // FormSignInfoChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 420);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximumSize = new System.Drawing.Size(1600, 794);
            this.Name = "FormSignInfoChooser";
            this.Text = "Form";
            this.pnlContainer.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompany)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.WinFormPager winFormPager1;
        private ServiceStationClient.ComponentUI.ButtonEx btnSubmit;
        private ServiceStationClient.ComponentUI.PanelEx pnlSearch;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvCompany;
        private System.Windows.Forms.DataGridViewCheckBoxColumn drchk_check;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_sign_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_sign_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_sign_brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_com_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_sign_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_com_short_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_com_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcom_tel;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_approved_time;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_com_name;
        private System.Windows.Forms.Label label7;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_com_short_name;
        private System.Windows.Forms.Label label4;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_sign_code;
        private System.Windows.Forms.Label label1;

    }
}