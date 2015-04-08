namespace HXCPcClient.Chooser
{
    partial class frmWorkHours
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWorkHours));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.cboDataSources = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtProName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProNo = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClose = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnSubmit = new ServiceStationClient.ComponentUI.ButtonEx();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.winFormPager1 = new ServiceStationClient.ComponentUI.WinFormPager();
            this.tcWorkHours = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tpUsers = new System.Windows.Forms.TabPage();
            this.dgvWorkList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.whours_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.project_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repair_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.project_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whours_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_whours_change = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whours_num_a = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whours_num_b = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whours_num_c = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quota_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataSource = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.create_username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data_source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlContainer.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.tcWorkHours.SuspendLayout();
            this.tpUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlContainer.Controls.Add(this.panelEx2);
            this.pnlContainer.Controls.Add(this.btnClose);
            this.pnlContainer.Controls.Add(this.btnSubmit);
            this.pnlContainer.Controls.Add(this.panelEx1);
            this.pnlContainer.Size = new System.Drawing.Size(814, 429);
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.cboDataSources);
            this.panelEx1.Controls.Add(this.txtProName);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.txtProNo);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(1, 2);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(811, 48);
            this.panelEx1.TabIndex = 6;
            // 
            // cboDataSources
            // 
            this.cboDataSources.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDataSources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataSources.FormattingEnabled = true;
            this.cboDataSources.Location = new System.Drawing.Point(499, 12);
            this.cboDataSources.Name = "cboDataSources";
            this.cboDataSources.Size = new System.Drawing.Size(121, 22);
            this.cboDataSources.TabIndex = 42;
            // 
            // txtProName
            // 
            this.txtProName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtProName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtProName.BackColor = System.Drawing.Color.Transparent;
            this.txtProName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtProName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtProName.ForeImage = null;
            this.txtProName.InputtingVerifyCondition = null;
            this.txtProName.Location = new System.Drawing.Point(318, 12);
            this.txtProName.MaxLengh = 32767;
            this.txtProName.Multiline = false;
            this.txtProName.Name = "txtProName";
            this.txtProName.Radius = 3;
            this.txtProName.ReadOnly = false;
            this.txtProName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtProName.ShowError = false;
            this.txtProName.Size = new System.Drawing.Size(114, 23);
            this.txtProName.TabIndex = 23;
            this.txtProName.UseSystemPasswordChar = false;
            this.txtProName.Value = "";
            this.txtProName.VerifyCondition = null;
            this.txtProName.VerifyType = null;
            this.txtProName.VerifyTypeName = null;
            this.txtProName.WaterMark = null;
            this.txtProName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(438, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 41;
            this.label5.Text = "数据来源：";
            // 
            // txtProNo
            // 
            this.txtProNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtProNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtProNo.BackColor = System.Drawing.Color.Transparent;
            this.txtProNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtProNo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtProNo.ForeImage = null;
            this.txtProNo.InputtingVerifyCondition = null;
            this.txtProNo.Location = new System.Drawing.Point(102, 12);
            this.txtProNo.MaxLengh = 32767;
            this.txtProNo.Multiline = false;
            this.txtProNo.Name = "txtProNo";
            this.txtProNo.Radius = 3;
            this.txtProNo.ReadOnly = false;
            this.txtProNo.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtProNo.ShowError = false;
            this.txtProNo.Size = new System.Drawing.Size(114, 23);
            this.txtProNo.TabIndex = 22;
            this.txtProNo.UseSystemPasswordChar = false;
            this.txtProNo.Value = "";
            this.txtProNo.VerifyCondition = null;
            this.txtProNo.VerifyType = null;
            this.txtProNo.VerifyTypeName = null;
            this.txtProNo.WaterMark = null;
            this.txtProNo.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(227, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "维修项目名称：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "维修项目编号：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSearch.Location = new System.Drawing.Point(631, 12);
            this.btnSearch.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSearch.Size = new System.Drawing.Size(80, 24);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Location = new System.Drawing.Point(717, 12);
            this.btnClear.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.Size = new System.Drawing.Size(80, 24);
            this.btnClear.TabIndex = 8;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Caption = "关闭";
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClose.Location = new System.Drawing.Point(723, 398);
            this.btnClose.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClose.Name = "btnClose";
            this.btnClose.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClose.Size = new System.Drawing.Size(80, 24);
            this.btnClose.TabIndex = 41;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Caption = "确定";
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSubmit.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSubmit.Location = new System.Drawing.Point(632, 398);
            this.btnSubmit.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSubmit.Size = new System.Drawing.Size(80, 24);
            this.btnSubmit.TabIndex = 40;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.winFormPager1);
            this.panelEx2.Controls.Add(this.tcWorkHours);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(3, 56);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(808, 336);
            this.panelEx2.TabIndex = 42;
            // 
            // winFormPager1
            // 
            this.winFormPager1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.winFormPager1.BackColor = System.Drawing.Color.Transparent;
            this.winFormPager1.BtnTextNext = "下页";
            this.winFormPager1.BtnTextPrevious = "上页";
            this.winFormPager1.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.winFormPager1.Location = new System.Drawing.Point(350, 298);
            this.winFormPager1.Name = "winFormPager1";
            this.winFormPager1.PageCount = 0;
            this.winFormPager1.PageSize = 15;
            this.winFormPager1.RecordCount = 0;
            this.winFormPager1.Size = new System.Drawing.Size(450, 32);
            this.winFormPager1.TabIndex = 10;
            this.winFormPager1.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.winFormPager1.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.winFormPager1_PageIndexChanged);
            // 
            // tcWorkHours
            // 
            this.tcWorkHours.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tcWorkHours.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.tcWorkHours.Controls.Add(this.tpUsers);
            this.tcWorkHours.Location = new System.Drawing.Point(3, 3);
            this.tcWorkHours.Name = "tcWorkHours";
            this.tcWorkHours.SelectedIndex = 0;
            this.tcWorkHours.Size = new System.Drawing.Size(797, 289);
            this.tcWorkHours.TabIndex = 9;
            // 
            // tpUsers
            // 
            this.tpUsers.Controls.Add(this.dgvWorkList);
            this.tpUsers.Location = new System.Drawing.Point(4, 26);
            this.tpUsers.Name = "tpUsers";
            this.tpUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tpUsers.Size = new System.Drawing.Size(789, 259);
            this.tpUsers.TabIndex = 0;
            this.tpUsers.Text = "工时档案列表";
            this.tpUsers.UseVisualStyleBackColor = true;
            // 
            // dgvWorkList
            // 
            this.dgvWorkList.AllowUserToAddRows = false;
            this.dgvWorkList.AllowUserToDeleteRows = false;
            this.dgvWorkList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvWorkList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvWorkList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWorkList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvWorkList.BackgroundColor = System.Drawing.Color.White;
            this.dgvWorkList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWorkList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvWorkList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWorkList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.whours_id,
            this.project_num,
            this.repair_type,
            this.project_name,
            this.whours_type,
            this.drtxt_whours_change,
            this.whours_num_a,
            this.whours_num_b,
            this.whours_num_c,
            this.quota_price,
            this.colDataSource,
            this.create_username,
            this.create_time,
            this.remark,
            this.data_source});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvWorkList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvWorkList.EnableHeadersVisualStyles = false;
            this.dgvWorkList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvWorkList.IsCheck = true;
            this.dgvWorkList.Location = new System.Drawing.Point(0, 2);
            this.dgvWorkList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvWorkList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvWorkList.MergeColumnNames")));
            this.dgvWorkList.MultiSelect = false;
            this.dgvWorkList.Name = "dgvWorkList";
            this.dgvWorkList.ReadOnly = true;
            this.dgvWorkList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvWorkList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvWorkList.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvWorkList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvWorkList.RowTemplate.Height = 23;
            this.dgvWorkList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWorkList.ShowCheckBox = true;
            this.dgvWorkList.Size = new System.Drawing.Size(786, 251);
            this.dgvWorkList.TabIndex = 1;
            this.dgvWorkList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvWorkList_CellDoubleClick);
            this.dgvWorkList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvWorkList_CellFormatting);
            this.dgvWorkList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvWorkList_CellMouseClick);
            this.dgvWorkList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gvWorkList_KeyPress);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCheck.Width = 30;
            // 
            // whours_id
            // 
            this.whours_id.DataPropertyName = "whours_id";
            this.whours_id.HeaderText = "whours_id";
            this.whours_id.Name = "whours_id";
            this.whours_id.ReadOnly = true;
            this.whours_id.Visible = false;
            this.whours_id.Width = 76;
            // 
            // project_num
            // 
            this.project_num.DataPropertyName = "project_num";
            this.project_num.HeaderText = "维修项目编号";
            this.project_num.Name = "project_num";
            this.project_num.ReadOnly = true;
            this.project_num.Width = 105;
            // 
            // repair_type
            // 
            this.repair_type.DataPropertyName = "repair_type";
            this.repair_type.HeaderText = "维修项目类别";
            this.repair_type.Name = "repair_type";
            this.repair_type.ReadOnly = true;
            this.repair_type.Width = 105;
            // 
            // project_name
            // 
            this.project_name.DataPropertyName = "project_name";
            this.project_name.HeaderText = "维修项目名称";
            this.project_name.Name = "project_name";
            this.project_name.ReadOnly = true;
            this.project_name.Width = 105;
            // 
            // whours_type
            // 
            this.whours_type.DataPropertyName = "whours_type";
            this.whours_type.HeaderText = "工时类型";
            this.whours_type.Name = "whours_type";
            this.whours_type.ReadOnly = true;
            this.whours_type.Width = 81;
            // 
            // drtxt_whours_change
            // 
            this.drtxt_whours_change.DataPropertyName = "whours_change";
            this.drtxt_whours_change.HeaderText = "工时调整";
            this.drtxt_whours_change.Name = "drtxt_whours_change";
            this.drtxt_whours_change.ReadOnly = true;
            this.drtxt_whours_change.Width = 81;
            // 
            // whours_num_a
            // 
            this.whours_num_a.DataPropertyName = "whours_num_a";
            this.whours_num_a.HeaderText = "A类工时数";
            this.whours_num_a.Name = "whours_num_a";
            this.whours_num_a.ReadOnly = true;
            this.whours_num_a.Width = 90;
            // 
            // whours_num_b
            // 
            this.whours_num_b.DataPropertyName = "whours_num_b";
            this.whours_num_b.HeaderText = "B类工时数";
            this.whours_num_b.Name = "whours_num_b";
            this.whours_num_b.ReadOnly = true;
            this.whours_num_b.Width = 89;
            // 
            // whours_num_c
            // 
            this.whours_num_c.DataPropertyName = "whours_num_c";
            this.whours_num_c.HeaderText = "C类工时数";
            this.whours_num_c.Name = "whours_num_c";
            this.whours_num_c.ReadOnly = true;
            this.whours_num_c.Width = 89;
            // 
            // quota_price
            // 
            this.quota_price.DataPropertyName = "quota_price";
            this.quota_price.HeaderText = "定额单价(元)";
            this.quota_price.Name = "quota_price";
            this.quota_price.ReadOnly = true;
            this.quota_price.Width = 103;
            // 
            // colDataSource
            // 
            this.colDataSource.DataPropertyName = "data_source";
            this.colDataSource.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.colDataSource.HeaderText = "数据来源";
            this.colDataSource.Name = "colDataSource";
            this.colDataSource.ReadOnly = true;
            this.colDataSource.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDataSource.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colDataSource.Width = 81;
            // 
            // create_username
            // 
            this.create_username.DataPropertyName = "create_username";
            this.create_username.HeaderText = "创建人";
            this.create_username.Name = "create_username";
            this.create_username.ReadOnly = true;
            this.create_username.Width = 69;
            // 
            // create_time
            // 
            this.create_time.DataPropertyName = "create_time";
            this.create_time.HeaderText = "创建时间";
            this.create_time.Name = "create_time";
            this.create_time.ReadOnly = true;
            this.create_time.Width = 81;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "project_remark";
            this.remark.HeaderText = "备注(项目说明)";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            this.remark.Width = 115;
            // 
            // data_source
            // 
            this.data_source.DataPropertyName = "data_source";
            this.data_source.HeaderText = "data_source";
            this.data_source.Name = "data_source";
            this.data_source.ReadOnly = true;
            this.data_source.Visible = false;
            this.data_source.Width = 105;
            // 
            // frmWorkHours
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(816, 460);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmWorkHours";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "工时档案";
            this.pnlContainer.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.tcWorkHours.ResumeLayout(false);
            this.tpUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.TextBoxEx txtProName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtProNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.ButtonEx btnClose;
        private ServiceStationClient.ComponentUI.ButtonEx btnSubmit;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager winFormPager1;
        private ServiceStationClient.ComponentUI.TabControlEx tcWorkHours;
        private System.Windows.Forms.TabPage tpUsers;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvWorkList;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboDataSources;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn whours_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn project_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn repair_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn project_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn whours_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_whours_change;
        private System.Windows.Forms.DataGridViewTextBoxColumn whours_num_a;
        private System.Windows.Forms.DataGridViewTextBoxColumn whours_num_b;
        private System.Windows.Forms.DataGridViewTextBoxColumn whours_num_c;
        private System.Windows.Forms.DataGridViewTextBoxColumn quota_price;
        private System.Windows.Forms.DataGridViewComboBoxColumn colDataSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_username;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn data_source;
    }
}