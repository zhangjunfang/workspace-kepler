namespace HXCPcClient.UCForm.DataManage.WareHouse
{
    partial class UCWareHouseManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCWareHouseManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.txtBelongCompany = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.ddlState = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.dateTimeEnd = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dateTimeStart = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.txtWareHouseNo = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtWareHouseName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.ddlDataSource = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvWareHouseList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.data_source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wh_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wh_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.com_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cargospacenum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wh_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.winFormPager1 = new ServiceStationClient.ComponentUI.WinFormPager();
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx1.SuspendLayout();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWareHouseList)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1020, 25);
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.txtBelongCompany);
            this.panelEx1.Controls.Add(this.ddlState);
            this.panelEx1.Controls.Add(this.dateTimeEnd);
            this.panelEx1.Controls.Add(this.dateTimeStart);
            this.panelEx1.Controls.Add(this.txtWareHouseNo);
            this.panelEx1.Controls.Add(this.txtWareHouseName);
            this.panelEx1.Controls.Add(this.ddlDataSource);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.label12);
            this.panelEx1.Controls.Add(this.label11);
            this.panelEx1.Controls.Add(this.label10);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(14, 28);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(991, 111);
            this.panelEx1.TabIndex = 0;
            // 
            // txtBelongCompany
            // 
            this.txtBelongCompany.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtBelongCompany.Location = new System.Drawing.Point(413, 14);
            this.txtBelongCompany.Name = "txtBelongCompany";
            this.txtBelongCompany.ReadOnly = false;
            this.txtBelongCompany.Size = new System.Drawing.Size(164, 24);
            this.txtBelongCompany.TabIndex = 29;
            this.txtBelongCompany.ToolTipTitle = "";
            this.txtBelongCompany.ChooserClick += new System.EventHandler(this.txtBelongCompany_ChooserClick);
            // 
            // ddlState
            // 
            this.ddlState.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlState.FormattingEnabled = true;
            this.ddlState.Location = new System.Drawing.Point(117, 75);
            this.ddlState.Name = "ddlState";
            this.ddlState.Size = new System.Drawing.Size(164, 22);
            this.ddlState.TabIndex = 26;
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Location = new System.Drawing.Point(602, 76);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.ShowFormat = "yyyy年MM月dd日";
            this.dateTimeEnd.Size = new System.Drawing.Size(164, 21);
            this.dateTimeEnd.TabIndex = 25;
            this.dateTimeEnd.Value = new System.DateTime(2014, 9, 16, 15, 23, 2, 109);
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Location = new System.Drawing.Point(413, 76);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.ShowFormat = "yyyy年MM月dd日";
            this.dateTimeStart.Size = new System.Drawing.Size(164, 21);
            this.dateTimeStart.TabIndex = 24;
            this.dateTimeStart.Value = new System.DateTime(2014, 9, 16, 15, 23, 2, 109);
            // 
            // txtWareHouseNo
            // 
            this.txtWareHouseNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtWareHouseNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtWareHouseNo.BackColor = System.Drawing.Color.Transparent;
            this.txtWareHouseNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtWareHouseNo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtWareHouseNo.ForeImage = null;
            this.txtWareHouseNo.InputtingVerifyCondition = null;
            this.txtWareHouseNo.Location = new System.Drawing.Point(117, 44);
            this.txtWareHouseNo.MaxLengh = 32767;
            this.txtWareHouseNo.Multiline = false;
            this.txtWareHouseNo.Name = "txtWareHouseNo";
            this.txtWareHouseNo.Radius = 3;
            this.txtWareHouseNo.ReadOnly = false;
            this.txtWareHouseNo.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtWareHouseNo.ShowError = false;
            this.txtWareHouseNo.Size = new System.Drawing.Size(164, 23);
            this.txtWareHouseNo.TabIndex = 20;
            this.txtWareHouseNo.UseSystemPasswordChar = false;
            this.txtWareHouseNo.Value = "";
            this.txtWareHouseNo.VerifyCondition = null;
            this.txtWareHouseNo.VerifyType = null;
            this.txtWareHouseNo.VerifyTypeName = null;
            this.txtWareHouseNo.WaterMark = null;
            this.txtWareHouseNo.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtWareHouseName
            // 
            this.txtWareHouseName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtWareHouseName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtWareHouseName.BackColor = System.Drawing.Color.Transparent;
            this.txtWareHouseName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtWareHouseName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtWareHouseName.ForeImage = null;
            this.txtWareHouseName.InputtingVerifyCondition = null;
            this.txtWareHouseName.Location = new System.Drawing.Point(413, 44);
            this.txtWareHouseName.MaxLengh = 32767;
            this.txtWareHouseName.Multiline = false;
            this.txtWareHouseName.Name = "txtWareHouseName";
            this.txtWareHouseName.Radius = 3;
            this.txtWareHouseName.ReadOnly = false;
            this.txtWareHouseName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtWareHouseName.ShowError = false;
            this.txtWareHouseName.Size = new System.Drawing.Size(164, 23);
            this.txtWareHouseName.TabIndex = 19;
            this.txtWareHouseName.UseSystemPasswordChar = false;
            this.txtWareHouseName.Value = "";
            this.txtWareHouseName.VerifyCondition = null;
            this.txtWareHouseName.VerifyType = null;
            this.txtWareHouseName.VerifyTypeName = null;
            this.txtWareHouseName.WaterMark = null;
            this.txtWareHouseName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // ddlDataSource
            // 
            this.ddlDataSource.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDataSource.FormattingEnabled = true;
            this.ddlDataSource.Location = new System.Drawing.Point(117, 16);
            this.ddlDataSource.Name = "ddlDataSource";
            this.ddlDataSource.Size = new System.Drawing.Size(164, 22);
            this.ddlDataSource.TabIndex = 16;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(894, 71);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(76, 26);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(894, 31);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(76, 26);
            this.btnClear.TabIndex = 12;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(582, 79);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 12);
            this.label12.TabIndex = 11;
            this.label12.Text = "至 ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(344, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 10;
            this.label11.Text = "创建时间：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(70, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "状态：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "仓库编号：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(344, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "仓库名称：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(344, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "所属公司：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据来源：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Location = new System.Drawing.Point(14, 160);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(995, 306);
            this.tabControlEx1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvWareHouseList);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(987, 276);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "仓库列表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvWareHouseList
            // 
            this.dgvWareHouseList.AllowUserToAddRows = false;
            this.dgvWareHouseList.AllowUserToDeleteRows = false;
            this.dgvWareHouseList.AllowUserToResizeColumns = false;
            this.dgvWareHouseList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvWareHouseList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvWareHouseList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWareHouseList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWareHouseList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvWareHouseList.BackgroundColor = System.Drawing.Color.White;
            this.dgvWareHouseList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWareHouseList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvWareHouseList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWareHouseList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.data_source,
            this.wh_code,
            this.wh_name,
            this.com_name,
            this.cargospacenum,
            this.user_name,
            this.create_time,
            this.status,
            this.remark,
            this.wh_id});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvWareHouseList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvWareHouseList.EnableHeadersVisualStyles = false;
            this.dgvWareHouseList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvWareHouseList.IsCheck = true;
            this.dgvWareHouseList.Location = new System.Drawing.Point(3, 3);
            this.dgvWareHouseList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvWareHouseList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvWareHouseList.MergeColumnNames")));
            this.dgvWareHouseList.MultiSelect = false;
            this.dgvWareHouseList.Name = "dgvWareHouseList";
            this.dgvWareHouseList.ReadOnly = true;
            this.dgvWareHouseList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvWareHouseList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvWareHouseList.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvWareHouseList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvWareHouseList.RowTemplate.Height = 23;
            this.dgvWareHouseList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWareHouseList.ShowCheckBox = true;
            this.dgvWareHouseList.Size = new System.Drawing.Size(981, 270);
            this.dgvWareHouseList.TabIndex = 0;
            this.dgvWareHouseList.HeadCheckChanged += new ServiceStationClient.ComponentUI.DataGridViewEx.DelegateOnClick(this.dgvWareHouseList_HeadCheckChanged);
            this.dgvWareHouseList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvWareHouseList_CellContentClick);
            this.dgvWareHouseList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvWareHouseList_CellDoubleClick);
            this.dgvWareHouseList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvWareHouseList_CellFormatting);
            // 
            // colCheck
            // 
            this.colCheck.FillWeight = 30.61223F;
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            // 
            // data_source
            // 
            this.data_source.DataPropertyName = "data_source";
            this.data_source.FillWeight = 107.7097F;
            this.data_source.HeaderText = "数据来源";
            this.data_source.Name = "data_source";
            this.data_source.ReadOnly = true;
            // 
            // wh_code
            // 
            this.wh_code.DataPropertyName = "wh_code";
            this.wh_code.FillWeight = 107.7097F;
            this.wh_code.HeaderText = "仓库编号";
            this.wh_code.Name = "wh_code";
            this.wh_code.ReadOnly = true;
            // 
            // wh_name
            // 
            this.wh_name.DataPropertyName = "wh_name";
            this.wh_name.FillWeight = 107.7097F;
            this.wh_name.HeaderText = "仓库名称";
            this.wh_name.Name = "wh_name";
            this.wh_name.ReadOnly = true;
            // 
            // com_name
            // 
            this.com_name.DataPropertyName = "com_name";
            this.com_name.FillWeight = 107.7097F;
            this.com_name.HeaderText = "所属公司";
            this.com_name.Name = "com_name";
            this.com_name.ReadOnly = true;
            // 
            // cargospacenum
            // 
            this.cargospacenum.DataPropertyName = "cargospacenum";
            this.cargospacenum.FillWeight = 107.7097F;
            this.cargospacenum.HeaderText = "库位数目";
            this.cargospacenum.Name = "cargospacenum";
            this.cargospacenum.ReadOnly = true;
            // 
            // user_name
            // 
            this.user_name.DataPropertyName = "user_name";
            this.user_name.FillWeight = 107.7097F;
            this.user_name.HeaderText = "创建人";
            this.user_name.Name = "user_name";
            this.user_name.ReadOnly = true;
            // 
            // create_time
            // 
            this.create_time.DataPropertyName = "create_time";
            this.create_time.FillWeight = 107.7097F;
            this.create_time.HeaderText = "创建时间";
            this.create_time.Name = "create_time";
            this.create_time.ReadOnly = true;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.FillWeight = 107.7097F;
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.FillWeight = 107.7097F;
            this.remark.HeaderText = "备注(项目说明)";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            // 
            // wh_id
            // 
            this.wh_id.DataPropertyName = "wh_id";
            this.wh_id.HeaderText = "wh_id";
            this.wh_id.Name = "wh_id";
            this.wh_id.ReadOnly = true;
            this.wh_id.Visible = false;
            // 
            // panelEx2
            // 
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.winFormPager1);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(13, 504);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(991, 35);
            this.panelEx2.TabIndex = 4;
            // 
            // winFormPager1
            // 
            this.winFormPager1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.winFormPager1.BackColor = System.Drawing.Color.Transparent;
            this.winFormPager1.BtnTextNext = "下页";
            this.winFormPager1.BtnTextPrevious = "上页";
            this.winFormPager1.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.winFormPager1.Location = new System.Drawing.Point(560, 1);
            this.winFormPager1.Name = "winFormPager1";
            this.winFormPager1.PageCount = 0;
            this.winFormPager1.PageSize = 15;
            this.winFormPager1.RecordCount = 0;
            this.winFormPager1.Size = new System.Drawing.Size(431, 32);
            this.winFormPager1.TabIndex = 5;
            this.winFormPager1.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.winFormPager1.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.winFormPager1_PageIndexChanged);
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(100, 22);
            this.tsmiAdd.Text = "新增";
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(100, 22);
            this.tsmiEdit.Text = "编辑";
            // 
            // UCWareHouseManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.tabControlEx1);
            this.Controls.Add(this.panelEx1);
            this.Name = "UCWareHouseManager";
            this.Size = new System.Drawing.Size(1020, 549);
            this.Load += new System.EventHandler(this.UCWareHouseManager_Load);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.tabControlEx1, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWareHouseList)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dateTimeEnd;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dateTimeStart;
        private ServiceStationClient.ComponentUI.TextBoxEx txtWareHouseNo;
        private ServiceStationClient.ComponentUI.TextBoxEx txtWareHouseName;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlDataSource;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvWareHouseList;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager winFormPager1;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlState;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtBelongCompany;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn data_source;
        private System.Windows.Forms.DataGridViewTextBoxColumn wh_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn wh_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn com_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn cargospacenum;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn wh_id;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
    }
}
