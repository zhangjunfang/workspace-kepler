namespace HXCPcClient.Chooser
{
    partial class frmCustomerInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCustomerInfo));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelEx3 = new ServiceStationClient.ComponentUI.PanelEx();
            this.tvCustom = new ServiceStationClient.ComponentUI.TreeViewEx();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.tcUsers = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tpUsers = new System.Windows.Forms.TabPage();
            this.dgvCustom = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cust_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_short_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cont_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dic_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dic_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.credit_rating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.palBottom = new System.Windows.Forms.Panel();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.btnSave = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClose = new ServiceStationClient.ComponentUI.ButtonEx();
            this.palTop = new System.Windows.Forms.Panel();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.cobCustType = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtCustomName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtContract = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtCustomNo = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labCustType = new System.Windows.Forms.Label();
            this.labContract = new System.Windows.Forms.Label();
            this.labCustomName = new System.Windows.Forms.Label();
            this.labCustomNo = new System.Windows.Forms.Label();
            this.btnSubmit = new ServiceStationClient.ComponentUI.ButtonEx();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.tcUsers.SuspendLayout();
            this.tpUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustom)).BeginInit();
            this.palBottom.SuspendLayout();
            this.palTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlContainer.Controls.Add(this.splitContainer1);
            this.pnlContainer.Size = new System.Drawing.Size(718, 429);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelEx3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelEx1);
            this.splitContainer1.Size = new System.Drawing.Size(718, 429);
            this.splitContainer1.SplitterDistance = 167;
            this.splitContainer1.TabIndex = 0;
            // 
            // panelEx3
            // 
            this.panelEx3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx3.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEx3.BorderWidth = 1;
            this.panelEx3.Controls.Add(this.tvCustom);
            this.panelEx3.Curvature = 0;
            this.panelEx3.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx3.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx3.Location = new System.Drawing.Point(3, 3);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(164, 423);
            this.panelEx3.TabIndex = 0;
            // 
            // tvCustom
            // 
            this.tvCustom.BackColor = System.Drawing.SystemColors.Window;
            this.tvCustom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvCustom.IgnoreAutoCheck = false;
            this.tvCustom.Location = new System.Drawing.Point(2, 3);
            this.tvCustom.Name = "tvCustom";
            this.tvCustom.Size = new System.Drawing.Size(162, 423);
            this.tvCustom.TabIndex = 1;
            this.tvCustom.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCustom_AfterSelect);
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Controls.Add(this.btnSave);
            this.panelEx1.Controls.Add(this.btnClose);
            this.panelEx1.Controls.Add(this.palTop);
            this.panelEx1.Controls.Add(this.btnSubmit);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(547, 429);
            this.panelEx1.TabIndex = 0;
            // 
            // panelEx2
            // 
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.tcUsers);
            this.panelEx2.Controls.Add(this.palBottom);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(3, 74);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(541, 314);
            this.panelEx2.TabIndex = 49;
            // 
            // tcUsers
            // 
            this.tcUsers.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tcUsers.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.tcUsers.Controls.Add(this.tpUsers);
            this.tcUsers.Location = new System.Drawing.Point(4, 3);
            this.tcUsers.Name = "tcUsers";
            this.tcUsers.SelectedIndex = 0;
            this.tcUsers.Size = new System.Drawing.Size(531, 270);
            this.tcUsers.TabIndex = 46;
            // 
            // tpUsers
            // 
            this.tpUsers.Controls.Add(this.dgvCustom);
            this.tpUsers.Location = new System.Drawing.Point(4, 26);
            this.tpUsers.Name = "tpUsers";
            this.tpUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tpUsers.Size = new System.Drawing.Size(523, 240);
            this.tpUsers.TabIndex = 0;
            this.tpUsers.Text = "客户信息列表";
            this.tpUsers.UseVisualStyleBackColor = true;
            // 
            // dgvCustom
            // 
            this.dgvCustom.AllowUserToAddRows = false;
            this.dgvCustom.AllowUserToDeleteRows = false;
            this.dgvCustom.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvCustom.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCustom.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustom.BackgroundColor = System.Drawing.Color.White;
            this.dgvCustom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustom.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCustom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustom.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.cust_code,
            this.cust_short_name,
            this.cust_name,
            this.cont_name,
            this.dic_code,
            this.dic_name,
            this.credit_rating,
            this.cust_remark,
            this.cust_id});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustom.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCustom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustom.EnableHeadersVisualStyles = false;
            this.dgvCustom.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvCustom.IsCheck = true;
            this.dgvCustom.Location = new System.Drawing.Point(3, 3);
            this.dgvCustom.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvCustom.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvCustom.MergeColumnNames")));
            this.dgvCustom.MultiSelect = false;
            this.dgvCustom.Name = "dgvCustom";
            this.dgvCustom.ReadOnly = true;
            this.dgvCustom.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvCustom.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCustom.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvCustom.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvCustom.RowTemplate.Height = 23;
            this.dgvCustom.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustom.ShowCheckBox = true;
            this.dgvCustom.Size = new System.Drawing.Size(517, 234);
            this.dgvCustom.TabIndex = 1;
            this.ToolTip.SetToolTip(this.dgvCustom, "请双击选择");
            this.dgvCustom.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustom_CellDoubleClick);
            this.dgvCustom.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCustom_CellMouseClick);
            this.dgvCustom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvCustom_KeyPress);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
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
            this.cust_code.MinimumWidth = 110;
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
            // cont_name
            // 
            this.cont_name.DataPropertyName = "cont_name";
            this.cont_name.HeaderText = "联系人";
            this.cont_name.MinimumWidth = 100;
            this.cont_name.Name = "cont_name";
            this.cont_name.ReadOnly = true;
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
            // palBottom
            // 
            this.palBottom.Controls.Add(this.page);
            this.palBottom.Location = new System.Drawing.Point(2, 275);
            this.palBottom.Name = "palBottom";
            this.palBottom.Size = new System.Drawing.Size(541, 37);
            this.palBottom.TabIndex = 47;
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
            this.page.Location = new System.Drawing.Point(16, 3);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(525, 31);
            this.page.TabIndex = 6;
            this.page.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Caption = "添加新客户";
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSave.Location = new System.Drawing.Point(10, 394);
            this.btnSave.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSave.Name = "btnSave";
            this.btnSave.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSave.Size = new System.Drawing.Size(80, 24);
            this.btnSave.TabIndex = 48;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Caption = "关闭";
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClose.Location = new System.Drawing.Point(456, 394);
            this.btnClose.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClose.Name = "btnClose";
            this.btnClose.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClose.Size = new System.Drawing.Size(80, 24);
            this.btnClose.TabIndex = 47;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // palTop
            // 
            this.palTop.Controls.Add(this.btnSearch);
            this.palTop.Controls.Add(this.btnClear);
            this.palTop.Controls.Add(this.cobCustType);
            this.palTop.Controls.Add(this.txtCustomName);
            this.palTop.Controls.Add(this.txtContract);
            this.palTop.Controls.Add(this.txtCustomNo);
            this.palTop.Controls.Add(this.labCustType);
            this.palTop.Controls.Add(this.labContract);
            this.palTop.Controls.Add(this.labCustomName);
            this.palTop.Controls.Add(this.labCustomNo);
            this.palTop.Location = new System.Drawing.Point(3, 3);
            this.palTop.Name = "palTop";
            this.palTop.Size = new System.Drawing.Size(541, 65);
            this.palTop.TabIndex = 43;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSearch.Location = new System.Drawing.Point(452, 33);
            this.btnSearch.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSearch.Size = new System.Drawing.Size(80, 24);
            this.btnSearch.TabIndex = 30;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Location = new System.Drawing.Point(452, 4);
            this.btnClear.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.Size = new System.Drawing.Size(80, 24);
            this.btnClear.TabIndex = 28;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cobCustType
            // 
            this.cobCustType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobCustType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobCustType.FormattingEnabled = true;
            this.cobCustType.Location = new System.Drawing.Point(271, 33);
            this.cobCustType.Name = "cobCustType";
            this.cobCustType.Size = new System.Drawing.Size(121, 22);
            this.cobCustType.TabIndex = 27;
            // 
            // txtCustomName
            // 
            this.txtCustomName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCustomName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCustomName.BackColor = System.Drawing.Color.Transparent;
            this.txtCustomName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCustomName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCustomName.ForeImage = null;
            this.txtCustomName.InputtingVerifyCondition = null;
            this.txtCustomName.Location = new System.Drawing.Point(72, 33);
            this.txtCustomName.MaxLengh = 32767;
            this.txtCustomName.Multiline = false;
            this.txtCustomName.Name = "txtCustomName";
            this.txtCustomName.Radius = 3;
            this.txtCustomName.ReadOnly = false;
            this.txtCustomName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCustomName.ShowError = false;
            this.txtCustomName.Size = new System.Drawing.Size(121, 23);
            this.txtCustomName.TabIndex = 26;
            this.txtCustomName.UseSystemPasswordChar = false;
            this.txtCustomName.Value = "";
            this.txtCustomName.VerifyCondition = null;
            this.txtCustomName.VerifyType = null;
            this.txtCustomName.VerifyTypeName = null;
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
            this.txtContract.InputtingVerifyCondition = null;
            this.txtContract.Location = new System.Drawing.Point(271, 4);
            this.txtContract.MaxLengh = 32767;
            this.txtContract.Multiline = false;
            this.txtContract.Name = "txtContract";
            this.txtContract.Radius = 3;
            this.txtContract.ReadOnly = false;
            this.txtContract.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContract.ShowError = false;
            this.txtContract.Size = new System.Drawing.Size(121, 23);
            this.txtContract.TabIndex = 25;
            this.txtContract.UseSystemPasswordChar = false;
            this.txtContract.Value = "";
            this.txtContract.VerifyCondition = null;
            this.txtContract.VerifyType = null;
            this.txtContract.VerifyTypeName = null;
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
            this.txtCustomNo.InputtingVerifyCondition = null;
            this.txtCustomNo.Location = new System.Drawing.Point(72, 4);
            this.txtCustomNo.MaxLengh = 32767;
            this.txtCustomNo.Multiline = false;
            this.txtCustomNo.Name = "txtCustomNo";
            this.txtCustomNo.Radius = 3;
            this.txtCustomNo.ReadOnly = false;
            this.txtCustomNo.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCustomNo.ShowError = false;
            this.txtCustomNo.Size = new System.Drawing.Size(121, 23);
            this.txtCustomNo.TabIndex = 24;
            this.txtCustomNo.UseSystemPasswordChar = false;
            this.txtCustomNo.Value = "";
            this.txtCustomNo.VerifyCondition = null;
            this.txtCustomNo.VerifyType = null;
            this.txtCustomNo.VerifyTypeName = null;
            this.txtCustomNo.WaterMark = null;
            this.txtCustomNo.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labCustType
            // 
            this.labCustType.AutoSize = true;
            this.labCustType.Location = new System.Drawing.Point(212, 39);
            this.labCustType.Name = "labCustType";
            this.labCustType.Size = new System.Drawing.Size(53, 12);
            this.labCustType.TabIndex = 3;
            this.labCustType.Text = "客户等级";
            // 
            // labContract
            // 
            this.labContract.AutoSize = true;
            this.labContract.Location = new System.Drawing.Point(224, 10);
            this.labContract.Name = "labContract";
            this.labContract.Size = new System.Drawing.Size(41, 12);
            this.labContract.TabIndex = 2;
            this.labContract.Text = "联系人";
            // 
            // labCustomName
            // 
            this.labCustomName.AutoSize = true;
            this.labCustomName.Location = new System.Drawing.Point(13, 39);
            this.labCustomName.Name = "labCustomName";
            this.labCustomName.Size = new System.Drawing.Size(53, 12);
            this.labCustomName.TabIndex = 1;
            this.labCustomName.Text = "客户名称";
            // 
            // labCustomNo
            // 
            this.labCustomNo.AutoSize = true;
            this.labCustomNo.Location = new System.Drawing.Point(13, 10);
            this.labCustomNo.Name = "labCustomNo";
            this.labCustomNo.Size = new System.Drawing.Size(53, 12);
            this.labCustomNo.TabIndex = 0;
            this.labCustomNo.Text = "客户编码";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Caption = "确定";
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSubmit.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSubmit.Location = new System.Drawing.Point(370, 394);
            this.btnSubmit.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSubmit.Size = new System.Drawing.Size(80, 24);
            this.btnSubmit.TabIndex = 46;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // frmCustomerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 460);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmCustomerInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "客户选择";
            this.pnlContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.tcUsers.ResumeLayout(false);
            this.tpUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustom)).EndInit();
            this.palBottom.ResumeLayout(false);
            this.palTop.ResumeLayout(false);
            this.palTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.TabControlEx tcUsers;
        private System.Windows.Forms.TabPage tpUsers;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvCustom;
        private System.Windows.Forms.Panel palBottom;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private ServiceStationClient.ComponentUI.ButtonEx btnSave;
        private ServiceStationClient.ComponentUI.ButtonEx btnClose;
        private System.Windows.Forms.Panel palTop;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobCustType;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCustomName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContract;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCustomNo;
        private System.Windows.Forms.Label labCustType;
        private System.Windows.Forms.Label labContract;
        private System.Windows.Forms.Label labCustomName;
        private System.Windows.Forms.Label labCustomNo;
        private ServiceStationClient.ComponentUI.ButtonEx btnSubmit;
        private ServiceStationClient.ComponentUI.PanelEx panelEx3;
        private ServiceStationClient.ComponentUI.TreeViewEx tvCustom;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_short_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn cont_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dic_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dic_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn credit_rating;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_id;
    }
}