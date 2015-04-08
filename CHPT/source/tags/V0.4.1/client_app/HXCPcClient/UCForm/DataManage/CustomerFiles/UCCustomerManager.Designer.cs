namespace HXCPcClient.UCForm.DataManage.CustomerFiles
{
    partial class UCCustomerManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCustomerManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlRight = new ServiceStationClient.ComponentUI.PanelEx();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.pageQ = new ServiceStationClient.ComponentUI.WinFormPager();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv_table = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.pnl_query = new ServiceStationClient.ComponentUI.PanelEx();
            this.uccont_name = new ServiceStationClient.ComponentUI.TextBox.UCAutoContacts();
            this.dtp_create_time_e = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dtp_create_time_s = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.txt_yt_sap_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_yt_customer_manager = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label11 = new System.Windows.Forms.Label();
            this.cbo_data_source = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtcust_address = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.cbocounty = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cbocity = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cboprovince = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.txt_cust_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbo_is_member = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cbo_cust_type = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cbo_status = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.btn_clear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btn_query = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_cust_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cust_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enterprise_nature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_member = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data_source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.legal_person = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlRight.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_table)).BeginInit();
            this.pnl_query.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRight
            // 
            this.pnlRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.pnlRight.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlRight.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlRight.BorderWidth = 1;
            this.pnlRight.Controls.Add(this.panelEx2);
            this.pnlRight.Controls.Add(this.tabControlEx1);
            this.pnlRight.Controls.Add(this.pnl_query);
            this.pnlRight.Curvature = 0;
            this.pnlRight.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlRight.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlRight.Location = new System.Drawing.Point(0, 28);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(1030, 516);
            this.pnlRight.TabIndex = 3;
            // 
            // panelEx2
            // 
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(223)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.pageQ);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(0, 488);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.panelEx2.Size = new System.Drawing.Size(1030, 28);
            this.panelEx2.TabIndex = 5;
            // 
            // pageQ
            // 
            this.pageQ.BackColor = System.Drawing.Color.Transparent;
            this.pageQ.BtnTextNext = "下页";
            this.pageQ.BtnTextPrevious = "上页";
            this.pageQ.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.pageQ.Dock = System.Windows.Forms.DockStyle.Right;
            this.pageQ.Location = new System.Drawing.Point(475, 0);
            this.pageQ.Name = "pageQ";
            this.pageQ.PageCount = 0;
            this.pageQ.PageSize = 15;
            this.pageQ.RecordCount = 0;
            this.pageQ.Size = new System.Drawing.Size(455, 28);
            this.pageQ.TabIndex = 5;
            this.pageQ.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Location = new System.Drawing.Point(0, 102);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(1030, 383);
            this.tabControlEx1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv_table);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1022, 353);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "客户列表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv_table
            // 
            this.dgv_table.AllowUserToAddRows = false;
            this.dgv_table.AllowUserToDeleteRows = false;
            this.dgv_table.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgv_table.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_table.BackgroundColor = System.Drawing.Color.White;
            this.dgv_table.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_table.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.cust_code,
            this.cust_name,
            this.enterprise_nature,
            this.cust_tel,
            this.cust_type,
            this.is_member,
            this.data_source,
            this.legal_person,
            this.status,
            this.user_name,
            this.create_time,
            this.cust_id});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_table.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_table.EnableHeadersVisualStyles = false;
            this.dgv_table.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgv_table.IsCheck = true;
            this.dgv_table.Location = new System.Drawing.Point(3, 3);
            this.dgv_table.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgv_table.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv_table.MergeColumnNames")));
            this.dgv_table.MultiSelect = false;
            this.dgv_table.Name = "dgv_table";
            this.dgv_table.ReadOnly = true;
            this.dgv_table.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgv_table.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_table.RowHeadersVisible = false;
            this.dgv_table.RowHeadersWidth = 30;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgv_table.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_table.RowTemplate.Height = 23;
            this.dgv_table.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_table.ShowCheckBox = true;
            this.dgv_table.Size = new System.Drawing.Size(1016, 347);
            this.dgv_table.TabIndex = 0;
            this.dgv_table.Tag = "";
            // 
            // pnl_query
            // 
            this.pnl_query.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_query.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnl_query.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnl_query.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnl_query.BorderWidth = 1;
            this.pnl_query.Controls.Add(this.uccont_name);
            this.pnl_query.Controls.Add(this.dtp_create_time_e);
            this.pnl_query.Controls.Add(this.dtp_create_time_s);
            this.pnl_query.Controls.Add(this.txt_yt_sap_code);
            this.pnl_query.Controls.Add(this.label12);
            this.pnl_query.Controls.Add(this.txt_yt_customer_manager);
            this.pnl_query.Controls.Add(this.label11);
            this.pnl_query.Controls.Add(this.cbo_data_source);
            this.pnl_query.Controls.Add(this.label10);
            this.pnl_query.Controls.Add(this.label5);
            this.pnl_query.Controls.Add(this.txtcust_address);
            this.pnl_query.Controls.Add(this.cbocounty);
            this.pnl_query.Controls.Add(this.cbocity);
            this.pnl_query.Controls.Add(this.cboprovince);
            this.pnl_query.Controls.Add(this.label9);
            this.pnl_query.Controls.Add(this.txt_cust_name);
            this.pnl_query.Controls.Add(this.label8);
            this.pnl_query.Controls.Add(this.label7);
            this.pnl_query.Controls.Add(this.cbo_is_member);
            this.pnl_query.Controls.Add(this.cbo_cust_type);
            this.pnl_query.Controls.Add(this.cbo_status);
            this.pnl_query.Controls.Add(this.btn_clear);
            this.pnl_query.Controls.Add(this.btn_query);
            this.pnl_query.Controls.Add(this.label6);
            this.pnl_query.Controls.Add(this.label4);
            this.pnl_query.Controls.Add(this.label3);
            this.pnl_query.Controls.Add(this.label2);
            this.pnl_query.Controls.Add(this.txt_cust_code);
            this.pnl_query.Controls.Add(this.label1);
            this.pnl_query.Curvature = 0;
            this.pnl_query.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnl_query.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnl_query.Location = new System.Drawing.Point(0, 0);
            this.pnl_query.Name = "pnl_query";
            this.pnl_query.Size = new System.Drawing.Size(1030, 95);
            this.pnl_query.TabIndex = 0;
            // 
            // uccont_name
            // 
            this.uccont_name.DataSource = null;
            this.uccont_name.IsVideoTree = true;
            this.uccont_name.ListBoxParent = this.pnlRight;
            this.uccont_name.ListBoxVisable = true;
            this.uccont_name.Location = new System.Drawing.Point(73, 65);
            this.uccont_name.Name = "uccont_name";
            this.uccont_name.Poit = new System.Drawing.Point(0, 0);
            this.uccont_name.ResultWords = "请输入名称、手机或电话";
            this.uccont_name.Size = new System.Drawing.Size(159, 22);
            this.uccont_name.TabIndex = 57;
            // 
            // dtp_create_time_e
            // 
            this.dtp_create_time_e.Location = new System.Drawing.Point(595, 37);
            this.dtp_create_time_e.Name = "dtp_create_time_e";
            this.dtp_create_time_e.ShowFormat = "yyyy-MM-dd";
            this.dtp_create_time_e.Size = new System.Drawing.Size(87, 19);
            this.dtp_create_time_e.TabIndex = 55;
            // 
            // dtp_create_time_s
            // 
            this.dtp_create_time_s.Location = new System.Drawing.Point(489, 37);
            this.dtp_create_time_s.Name = "dtp_create_time_s";
            this.dtp_create_time_s.ShowFormat = "yyyy-MM-dd";
            this.dtp_create_time_s.Size = new System.Drawing.Size(87, 19);
            this.dtp_create_time_s.TabIndex = 54;
            // 
            // txt_yt_sap_code
            // 
            this.txt_yt_sap_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_yt_sap_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_yt_sap_code.BackColor = System.Drawing.Color.Transparent;
            this.txt_yt_sap_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_yt_sap_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_yt_sap_code.ForeImage = null;
            this.txt_yt_sap_code.InputtingVerifyCondition = null;
            this.txt_yt_sap_code.Location = new System.Drawing.Point(723, 66);
            this.txt_yt_sap_code.MaxLengh = 32767;
            this.txt_yt_sap_code.Multiline = false;
            this.txt_yt_sap_code.Name = "txt_yt_sap_code";
            this.txt_yt_sap_code.Radius = 3;
            this.txt_yt_sap_code.ReadOnly = false;
            this.txt_yt_sap_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_yt_sap_code.ShowError = false;
            this.txt_yt_sap_code.Size = new System.Drawing.Size(121, 23);
            this.txt_yt_sap_code.TabIndex = 53;
            this.txt_yt_sap_code.UseSystemPasswordChar = false;
            this.txt_yt_sap_code.Value = "";
            this.txt_yt_sap_code.VerifyCondition = null;
            this.txt_yt_sap_code.VerifyType = null;
            this.txt_yt_sap_code.VerifyTypeName = null;
            this.txt_yt_sap_code.WaterMark = null;
            this.txt_yt_sap_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(644, 70);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 12);
            this.label12.TabIndex = 52;
            this.label12.Text = "宇通SPA代码：";
            // 
            // txt_yt_customer_manager
            // 
            this.txt_yt_customer_manager.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_yt_customer_manager.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_yt_customer_manager.BackColor = System.Drawing.Color.Transparent;
            this.txt_yt_customer_manager.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_yt_customer_manager.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_yt_customer_manager.ForeImage = null;
            this.txt_yt_customer_manager.InputtingVerifyCondition = null;
            this.txt_yt_customer_manager.Location = new System.Drawing.Point(513, 66);
            this.txt_yt_customer_manager.MaxLengh = 32767;
            this.txt_yt_customer_manager.Multiline = false;
            this.txt_yt_customer_manager.Name = "txt_yt_customer_manager";
            this.txt_yt_customer_manager.Radius = 3;
            this.txt_yt_customer_manager.ReadOnly = false;
            this.txt_yt_customer_manager.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_yt_customer_manager.ShowError = false;
            this.txt_yt_customer_manager.Size = new System.Drawing.Size(121, 23);
            this.txt_yt_customer_manager.TabIndex = 51;
            this.txt_yt_customer_manager.UseSystemPasswordChar = false;
            this.txt_yt_customer_manager.Value = "";
            this.txt_yt_customer_manager.VerifyCondition = null;
            this.txt_yt_customer_manager.VerifyType = null;
            this.txt_yt_customer_manager.VerifyTypeName = null;
            this.txt_yt_customer_manager.WaterMark = null;
            this.txt_yt_customer_manager.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(424, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 50;
            this.label11.Text = "宇通客户经理：";
            // 
            // cbo_data_source
            // 
            this.cbo_data_source.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_data_source.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_data_source.FormattingEnabled = true;
            this.cbo_data_source.Location = new System.Drawing.Point(302, 66);
            this.cbo_data_source.Name = "cbo_data_source";
            this.cbo_data_source.Size = new System.Drawing.Size(121, 22);
            this.cbo_data_source.TabIndex = 49;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(237, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 48;
            this.label10.Text = "数据来源：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 46;
            this.label5.Text = "联系人：";
            // 
            // txtcust_address
            // 
            this.txtcust_address.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcust_address.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcust_address.BackColor = System.Drawing.Color.Transparent;
            this.txtcust_address.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcust_address.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcust_address.ForeImage = null;
            this.txtcust_address.InputtingVerifyCondition = null;
            this.txtcust_address.Location = new System.Drawing.Point(304, 37);
            this.txtcust_address.MaxLengh = 32767;
            this.txtcust_address.Multiline = false;
            this.txtcust_address.Name = "txtcust_address";
            this.txtcust_address.Radius = 3;
            this.txtcust_address.ReadOnly = false;
            this.txtcust_address.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcust_address.ShowError = false;
            this.txtcust_address.Size = new System.Drawing.Size(121, 23);
            this.txtcust_address.TabIndex = 45;
            this.txtcust_address.UseSystemPasswordChar = false;
            this.txtcust_address.Value = "";
            this.txtcust_address.VerifyCondition = null;
            this.txtcust_address.VerifyType = null;
            this.txtcust_address.VerifyTypeName = null;
            this.txtcust_address.WaterMark = null;
            this.txtcust_address.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // cbocounty
            // 
            this.cbocounty.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbocounty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbocounty.FormattingEnabled = true;
            this.cbocounty.Location = new System.Drawing.Point(227, 38);
            this.cbocounty.Name = "cbocounty";
            this.cbocounty.Size = new System.Drawing.Size(77, 22);
            this.cbocounty.TabIndex = 44;
            // 
            // cbocity
            // 
            this.cbocity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbocity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbocity.FormattingEnabled = true;
            this.cbocity.Location = new System.Drawing.Point(150, 38);
            this.cbocity.Name = "cbocity";
            this.cbocity.Size = new System.Drawing.Size(77, 22);
            this.cbocity.TabIndex = 42;
            // 
            // cboprovince
            // 
            this.cboprovince.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboprovince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboprovince.FormattingEnabled = true;
            this.cboprovince.Location = new System.Drawing.Point(73, 38);
            this.cboprovince.Name = "cboprovince";
            this.cboprovince.Size = new System.Drawing.Size(77, 22);
            this.cboprovince.TabIndex = 40;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 39;
            this.label9.Text = "所在地：";
            // 
            // txt_cust_name
            // 
            this.txt_cust_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_cust_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_cust_name.BackColor = System.Drawing.Color.Transparent;
            this.txt_cust_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_cust_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_cust_name.ForeImage = null;
            this.txt_cust_name.InputtingVerifyCondition = null;
            this.txt_cust_name.Location = new System.Drawing.Point(304, 8);
            this.txt_cust_name.MaxLengh = 32767;
            this.txt_cust_name.Multiline = false;
            this.txt_cust_name.Name = "txt_cust_name";
            this.txt_cust_name.Radius = 3;
            this.txt_cust_name.ReadOnly = false;
            this.txt_cust_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_cust_name.ShowError = false;
            this.txt_cust_name.Size = new System.Drawing.Size(121, 23);
            this.txt_cust_name.TabIndex = 38;
            this.txt_cust_name.UseSystemPasswordChar = false;
            this.txt_cust_name.Value = "";
            this.txt_cust_name.VerifyCondition = null;
            this.txt_cust_name.VerifyType = null;
            this.txt_cust_name.VerifyTypeName = null;
            this.txt_cust_name.WaterMark = null;
            this.txt_cust_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(237, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 37;
            this.label8.Text = "客户名称：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(578, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 35;
            this.label7.Text = "至";
            // 
            // cbo_is_member
            // 
            this.cbo_is_member.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_is_member.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_is_member.FormattingEnabled = true;
            this.cbo_is_member.Location = new System.Drawing.Point(723, 9);
            this.cbo_is_member.Name = "cbo_is_member";
            this.cbo_is_member.Size = new System.Drawing.Size(121, 22);
            this.cbo_is_member.TabIndex = 32;
            // 
            // cbo_cust_type
            // 
            this.cbo_cust_type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_cust_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_cust_type.FormattingEnabled = true;
            this.cbo_cust_type.Location = new System.Drawing.Point(513, 9);
            this.cbo_cust_type.Name = "cbo_cust_type";
            this.cbo_cust_type.Size = new System.Drawing.Size(121, 22);
            this.cbo_cust_type.TabIndex = 31;
            // 
            // cbo_status
            // 
            this.cbo_status.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_status.FormattingEnabled = true;
            this.cbo_status.Location = new System.Drawing.Point(723, 38);
            this.cbo_status.Name = "cbo_status";
            this.cbo_status.Size = new System.Drawing.Size(121, 22);
            this.cbo_status.TabIndex = 30;
            // 
            // btn_clear
            // 
            this.btn_clear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_clear.BackgroundImage")));
            this.btn_clear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_clear.Caption = "清除";
            this.btn_clear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_clear.DownImage = ((System.Drawing.Image)(resources.GetObject("btn_clear.DownImage")));
            this.btn_clear.Location = new System.Drawing.Point(949, 13);
            this.btn_clear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btn_clear.MoveImage")));
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btn_clear.NormalImage")));
            this.btn_clear.Size = new System.Drawing.Size(60, 26);
            this.btn_clear.TabIndex = 16;
            // 
            // btn_query
            // 
            this.btn_query.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_query.BackgroundImage")));
            this.btn_query.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_query.Caption = "查询";
            this.btn_query.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_query.DownImage = ((System.Drawing.Image)(resources.GetObject("btn_query.DownImage")));
            this.btn_query.Location = new System.Drawing.Point(949, 51);
            this.btn_query.MoveImage = ((System.Drawing.Image)(resources.GetObject("btn_query.MoveImage")));
            this.btn_query.Name = "btn_query";
            this.btn_query.NormalImage = ((System.Drawing.Image)(resources.GetObject("btn_query.NormalImage")));
            this.btn_query.Size = new System.Drawing.Size(60, 26);
            this.btn_query.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(686, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "状态：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(424, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "创建时间：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(662, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "是否会员：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(448, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "客户分类：";
            // 
            // txt_cust_code
            // 
            this.txt_cust_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_cust_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_cust_code.BackColor = System.Drawing.Color.Transparent;
            this.txt_cust_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_cust_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_cust_code.ForeImage = null;
            this.txt_cust_code.InputtingVerifyCondition = null;
            this.txt_cust_code.Location = new System.Drawing.Point(73, 8);
            this.txt_cust_code.MaxLengh = 32767;
            this.txt_cust_code.Multiline = false;
            this.txt_cust_code.Name = "txt_cust_code";
            this.txt_cust_code.Radius = 3;
            this.txt_cust_code.ReadOnly = false;
            this.txt_cust_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_cust_code.ShowError = false;
            this.txt_cust_code.Size = new System.Drawing.Size(121, 23);
            this.txt_cust_code.TabIndex = 1;
            this.txt_cust_code.UseSystemPasswordChar = false;
            this.txt_cust_code.Value = "";
            this.txt_cust_code.VerifyCondition = null;
            this.txt_cust_code.VerifyType = null;
            this.txt_cust_code.VerifyTypeName = null;
            this.txt_cust_code.WaterMark = null;
            this.txt_cust_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "客户编码：";
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 30;
            // 
            // cust_code
            // 
            this.cust_code.DataPropertyName = "cust_code";
            this.cust_code.HeaderText = "客户编码";
            this.cust_code.Name = "cust_code";
            this.cust_code.ReadOnly = true;
            // 
            // cust_name
            // 
            this.cust_name.DataPropertyName = "cust_name";
            this.cust_name.HeaderText = "客户名称";
            this.cust_name.Name = "cust_name";
            this.cust_name.ReadOnly = true;
            // 
            // enterprise_nature
            // 
            this.enterprise_nature.DataPropertyName = "enterprise_nature";
            this.enterprise_nature.HeaderText = "企业性质";
            this.enterprise_nature.Name = "enterprise_nature";
            this.enterprise_nature.ReadOnly = true;
            // 
            // cust_tel
            // 
            this.cust_tel.DataPropertyName = "cust_tel";
            this.cust_tel.HeaderText = "公司电话";
            this.cust_tel.Name = "cust_tel";
            this.cust_tel.ReadOnly = true;
            // 
            // cust_type
            // 
            this.cust_type.DataPropertyName = "cust_type";
            this.cust_type.HeaderText = "客户分类";
            this.cust_type.Name = "cust_type";
            this.cust_type.ReadOnly = true;
            // 
            // is_member
            // 
            this.is_member.DataPropertyName = "is_member";
            this.is_member.HeaderText = "是否会员";
            this.is_member.Name = "is_member";
            this.is_member.ReadOnly = true;
            // 
            // data_source
            // 
            this.data_source.DataPropertyName = "data_source";
            this.data_source.HeaderText = "数据来源";
            this.data_source.Name = "data_source";
            this.data_source.ReadOnly = true;
            // 
            // legal_person
            // 
            this.legal_person.DataPropertyName = "legal_person";
            this.legal_person.HeaderText = "法人";
            this.legal_person.Name = "legal_person";
            this.legal_person.ReadOnly = true;
            this.legal_person.Visible = false;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // user_name
            // 
            this.user_name.DataPropertyName = "user_name";
            this.user_name.HeaderText = "创建人";
            this.user_name.Name = "user_name";
            this.user_name.ReadOnly = true;
            // 
            // create_time
            // 
            this.create_time.DataPropertyName = "create_time";
            this.create_time.HeaderText = "创建时间";
            this.create_time.Name = "create_time";
            this.create_time.ReadOnly = true;
            this.create_time.Width = 150;
            // 
            // cust_id
            // 
            this.cust_id.DataPropertyName = "cust_id";
            this.cust_id.HeaderText = "cust_id";
            this.cust_id.Name = "cust_id";
            this.cust_id.ReadOnly = true;
            this.cust_id.Visible = false;
            // 
            // UCCustomerManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlRight);
            this.Name = "UCCustomerManager";
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.pnlRight, 0);
            this.pnlRight.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_table)).EndInit();
            this.pnl_query.ResumeLayout(false);
            this.pnl_query.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx pnlRight;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager pageQ;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgv_table;
        private ServiceStationClient.ComponentUI.PanelEx pnl_query;
        private ServiceStationClient.ComponentUI.TextBox.UCAutoContacts uccont_name;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtp_create_time_e;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtp_create_time_s;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_yt_sap_code;
        private System.Windows.Forms.Label label12;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_yt_customer_manager;
        private System.Windows.Forms.Label label11;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbo_data_source;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcust_address;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbocounty;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbocity;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboprovince;
        private System.Windows.Forms.Label label9;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_cust_name;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbo_is_member;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbo_cust_type;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbo_status;
        private ServiceStationClient.ComponentUI.ButtonEx btn_clear;
        private ServiceStationClient.ComponentUI.ButtonEx btn_query;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_cust_code;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn enterprise_nature;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_tel;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_member;
        private System.Windows.Forms.DataGridViewTextBoxColumn data_source;
        private System.Windows.Forms.DataGridViewTextBoxColumn legal_person;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_id;
    }
}
