namespace HXCPcClient.UCForm.DataManage.Dic
{
    partial class UCDictionaryManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCDictionaryManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.dtpEnd = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dtpStart = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.txtparent_id = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txtcreate_by = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txtdic_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtdic_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCleare = new ServiceStationClient.ComponentUI.ButtonEx();
            this.tcDicList = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tpDicList = new System.Windows.Forms.TabPage();
            this.dgvDicList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.dic_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dic_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parent_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parent_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data_source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.update_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.update_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dic_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parent_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enable_flag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cobDataSources = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.panelEx1.SuspendLayout();
            this.tcDicList.SuspendLayout();
            this.tpDicList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDicList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1000, 25);
            // 
            // page
            // 
            this.page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.page.Location = new System.Drawing.Point(520, 463);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(480, 31);
            this.page.TabIndex = 6;
            this.page.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.cobDataSources);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.dtpEnd);
            this.panelEx1.Controls.Add(this.dtpStart);
            this.panelEx1.Controls.Add(this.txtparent_id);
            this.panelEx1.Controls.Add(this.txtcreate_by);
            this.panelEx1.Controls.Add(this.txtdic_name);
            this.panelEx1.Controls.Add(this.txtdic_code);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.label12);
            this.panelEx1.Controls.Add(this.label11);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(1, 27);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(999, 99);
            this.panelEx1.TabIndex = 7;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(402, 59);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.ShowFormat = "yyyy-MM-dd";
            this.dtpEnd.Size = new System.Drawing.Size(116, 21);
            this.dtpEnd.TabIndex = 27;
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(280, 59);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.ShowFormat = "yyyy-MM-dd";
            this.dtpStart.Size = new System.Drawing.Size(99, 21);
            this.dtpStart.TabIndex = 27;
            // 
            // txtparent_id
            // 
            this.txtparent_id.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtparent_id.Location = new System.Drawing.Point(280, 15);
            this.txtparent_id.Name = "txtparent_id";
            this.txtparent_id.ReadOnly = true;
            this.txtparent_id.Size = new System.Drawing.Size(211, 24);
            this.txtparent_id.TabIndex = 26;
            this.txtparent_id.ToolTipTitle = "";
            this.txtparent_id.ChooserClick += new System.EventHandler(this.txtparent_id_ChooserClick);
            // 
            // txtcreate_by
            // 
            this.txtcreate_by.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcreate_by.Location = new System.Drawing.Point(78, 59);
            this.txtcreate_by.Name = "txtcreate_by";
            this.txtcreate_by.ReadOnly = true;
            this.txtcreate_by.Size = new System.Drawing.Size(121, 24);
            this.txtcreate_by.TabIndex = 26;
            this.txtcreate_by.ToolTipTitle = "";
            this.txtcreate_by.ChooserClick += new System.EventHandler(this.txtcreate_by_ChooserClick);
            // 
            // txtdic_name
            // 
            this.txtdic_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtdic_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtdic_name.BackColor = System.Drawing.Color.Transparent;
            this.txtdic_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtdic_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtdic_name.ForeImage = null;
            this.txtdic_name.Location = new System.Drawing.Point(633, 16);
            this.txtdic_name.MaxLengh = 32767;
            this.txtdic_name.Multiline = false;
            this.txtdic_name.Name = "txtdic_name";
            this.txtdic_name.Radius = 3;
            this.txtdic_name.ReadOnly = false;
            this.txtdic_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtdic_name.ShowError = false;
            this.txtdic_name.Size = new System.Drawing.Size(202, 23);
            this.txtdic_name.TabIndex = 20;
            this.txtdic_name.UseSystemPasswordChar = false;
            this.txtdic_name.Value = "";
            this.txtdic_name.VerifyCondition = null;
            this.txtdic_name.VerifyType = null;
            this.txtdic_name.WaterMark = null;
            this.txtdic_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtdic_code
            // 
            this.txtdic_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtdic_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtdic_code.BackColor = System.Drawing.Color.Transparent;
            this.txtdic_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtdic_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtdic_code.ForeImage = null;
            this.txtdic_code.Location = new System.Drawing.Point(78, 16);
            this.txtdic_code.MaxLengh = 32767;
            this.txtdic_code.Multiline = false;
            this.txtdic_code.Name = "txtdic_code";
            this.txtdic_code.Radius = 3;
            this.txtdic_code.ReadOnly = false;
            this.txtdic_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtdic_code.ShowError = false;
            this.txtdic_code.Size = new System.Drawing.Size(121, 23);
            this.txtdic_code.TabIndex = 19;
            this.txtdic_code.UseSystemPasswordChar = false;
            this.txtdic_code.Value = "";
            this.txtdic_code.VerifyCondition = null;
            this.txtdic_code.VerifyType = null;
            this.txtdic_code.WaterMark = null;
            this.txtdic_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(850, 49);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(76, 26);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(850, 13);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(76, 26);
            this.btnClear.TabIndex = 12;
            this.btnClear.Tag = "1";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(383, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 12);
            this.label12.TabIndex = 11;
            this.label12.Text = "至 ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(217, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 10;
            this.label11.Text = "创建时间：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(526, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "码表名称/关键字：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "创建人：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "类别名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "码表编码：";
            // 
            // btnCleare
            // 
            this.btnCleare.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCleare.BackgroundImage")));
            this.btnCleare.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCleare.Caption = "清除";
            this.btnCleare.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCleare.DownImage = null;
            this.btnCleare.Location = new System.Drawing.Point(850, 16);
            this.btnCleare.MoveImage = null;
            this.btnCleare.Name = "btnCleare";
            this.btnCleare.NormalImage = null;
            this.btnCleare.Size = new System.Drawing.Size(76, 26);
            this.btnCleare.TabIndex = 12;
            // 
            // tcDicList
            // 
            this.tcDicList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcDicList.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tcDicList.Controls.Add(this.tpDicList);
            this.tcDicList.Location = new System.Drawing.Point(1, 127);
            this.tcDicList.Name = "tcDicList";
            this.tcDicList.SelectedIndex = 0;
            this.tcDicList.Size = new System.Drawing.Size(996, 336);
            this.tcDicList.TabIndex = 5;
            // 
            // tpDicList
            // 
            this.tpDicList.Controls.Add(this.dgvDicList);
            this.tpDicList.Location = new System.Drawing.Point(4, 26);
            this.tpDicList.Name = "tpDicList";
            this.tpDicList.Padding = new System.Windows.Forms.Padding(3);
            this.tpDicList.Size = new System.Drawing.Size(988, 306);
            this.tpDicList.TabIndex = 0;
            this.tpDicList.Text = "  码表列表  ";
            this.tpDicList.UseVisualStyleBackColor = true;
            // 
            // dgvDicList
            // 
            this.dgvDicList.AllowUserToAddRows = false;
            this.dgvDicList.AllowUserToDeleteRows = false;
            this.dgvDicList.AllowUserToOrderColumns = true;
            this.dgvDicList.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvDicList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDicList.BackgroundColor = System.Drawing.Color.White;
            this.dgvDicList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDicList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvDicList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDicList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dic_code,
            this.dic_name,
            this.parent_code,
            this.parent_name,
            this.data_source,
            this.create_time,
            this.create_name,
            this.update_time,
            this.update_name,
            this.remark,
            this.dic_id,
            this.parent_id,
            this.Num,
            this.enable_flag});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDicList.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvDicList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDicList.EnableHeadersVisualStyles = false;
            this.dgvDicList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvDicList.Location = new System.Drawing.Point(3, 3);
            this.dgvDicList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvDicList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvDicList.MergeColumnNames")));
            this.dgvDicList.MultiSelect = false;
            this.dgvDicList.Name = "dgvDicList";
            this.dgvDicList.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDicList.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvDicList.RowHeadersVisible = false;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvDicList.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvDicList.RowTemplate.Height = 23;
            this.dgvDicList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDicList.ShowCheckBox = true;
            this.dgvDicList.Size = new System.Drawing.Size(982, 300);
            this.dgvDicList.TabIndex = 0;
            this.dgvDicList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDicList_CellDoubleClick);
            this.dgvDicList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDicList_CellFormatting);
            // 
            // dic_code
            // 
            this.dic_code.DataPropertyName = "dic_code";
            this.dic_code.HeaderText = "类别编码";
            this.dic_code.Name = "dic_code";
            this.dic_code.ReadOnly = true;
            this.dic_code.Width = 120;
            // 
            // dic_name
            // 
            this.dic_name.DataPropertyName = "dic_name";
            this.dic_name.HeaderText = "类别名称";
            this.dic_name.Name = "dic_name";
            this.dic_name.ReadOnly = true;
            this.dic_name.Width = 120;
            // 
            // parent_code
            // 
            this.parent_code.DataPropertyName = "parent_code";
            this.parent_code.HeaderText = "父级类别编码";
            this.parent_code.Name = "parent_code";
            this.parent_code.ReadOnly = true;
            this.parent_code.Width = 120;
            // 
            // parent_name
            // 
            this.parent_name.DataPropertyName = "parent_name";
            this.parent_name.HeaderText = "父级类别名称";
            this.parent_name.Name = "parent_name";
            this.parent_name.ReadOnly = true;
            this.parent_name.Width = 120;
            // 
            // data_source
            // 
            this.data_source.DataPropertyName = "data_source";
            this.data_source.HeaderText = "来源";
            this.data_source.Name = "data_source";
            this.data_source.ReadOnly = true;
            this.data_source.Width = 120;
            // 
            // create_time
            // 
            this.create_time.DataPropertyName = "create_time";
            this.create_time.HeaderText = "创建时间";
            this.create_time.Name = "create_time";
            this.create_time.ReadOnly = true;
            this.create_time.Width = 120;
            // 
            // create_name
            // 
            this.create_name.DataPropertyName = "create_name";
            this.create_name.HeaderText = "创建人";
            this.create_name.Name = "create_name";
            this.create_name.ReadOnly = true;
            this.create_name.Width = 120;
            // 
            // update_time
            // 
            this.update_time.DataPropertyName = "update_time";
            this.update_time.HeaderText = "最后编辑时间";
            this.update_time.Name = "update_time";
            this.update_time.ReadOnly = true;
            this.update_time.Width = 120;
            // 
            // update_name
            // 
            this.update_name.DataPropertyName = "update_name";
            this.update_name.HeaderText = "最后编辑人";
            this.update_name.Name = "update_name";
            this.update_name.ReadOnly = true;
            this.update_name.Width = 120;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            this.remark.Width = 150;
            // 
            // dic_id
            // 
            this.dic_id.DataPropertyName = "dic_id";
            this.dic_id.HeaderText = "类别ID";
            this.dic_id.Name = "dic_id";
            this.dic_id.ReadOnly = true;
            this.dic_id.Visible = false;
            this.dic_id.Width = 71;
            // 
            // parent_id
            // 
            this.parent_id.DataPropertyName = "parent_id";
            this.parent_id.HeaderText = "父类别ID";
            this.parent_id.Name = "parent_id";
            this.parent_id.ReadOnly = true;
            this.parent_id.Visible = false;
            this.parent_id.Width = 83;
            // 
            // Num
            // 
            this.Num.DataPropertyName = "Num";
            this.Num.HeaderText = "序号";
            this.Num.Name = "Num";
            this.Num.ReadOnly = true;
            this.Num.Visible = false;
            this.Num.Width = 38;
            // 
            // enable_flag
            // 
            this.enable_flag.DataPropertyName = "enable_flag";
            this.enable_flag.HeaderText = "删除标记";
            this.enable_flag.Name = "enable_flag";
            this.enable_flag.ReadOnly = true;
            this.enable_flag.Visible = false;
            // 
            // cobDataSources
            // 
            this.cobDataSources.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobDataSources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobDataSources.FormattingEnabled = true;
            this.cobDataSources.Location = new System.Drawing.Point(633, 56);
            this.cobDataSources.Name = "cobDataSources";
            this.cobDataSources.Size = new System.Drawing.Size(121, 22);
            this.cobDataSources.TabIndex = 142;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(569, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 141;
            this.label2.Text = "数据来源：";
            // 
            // UCDictionaryManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.page);
            this.Controls.Add(this.tcDicList);
            this.Name = "UCDictionaryManager";
            this.Size = new System.Drawing.Size(1000, 500);
            this.Load += new System.EventHandler(this.UCDictionaryManager_Load);
            this.Controls.SetChildIndex(this.tcDicList, 0);
            this.Controls.SetChildIndex(this.page, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.tcDicList.ResumeLayout(false);
            this.tpDicList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDicList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.WinFormPager page;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.TextBoxEx txtdic_name;
        private ServiceStationClient.ComponentUI.TextBoxEx txtdic_code;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ButtonEx btnCleare;
        private ServiceStationClient.ComponentUI.TabControlEx tcDicList;
        private System.Windows.Forms.TabPage tpDicList;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvDicList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cbCheck;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtcreate_by;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtparent_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dic_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dic_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn parent_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn parent_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn data_source;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn update_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn update_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn dic_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn parent_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn enable_flag;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtpEnd;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtpStart;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobDataSources;
        private System.Windows.Forms.Label label2;

    }
}
