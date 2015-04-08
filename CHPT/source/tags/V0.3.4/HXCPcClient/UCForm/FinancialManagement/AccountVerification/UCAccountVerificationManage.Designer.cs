namespace HXCPcClient.UCForm.FinancialManagement.AccountVerification
{
    partial class UCAccountVerificationManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCAccountVerificationManage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlSearch = new ServiceStationClient.ComponentUI.PanelEx();
            this.dtInterval = new ServiceStationClient.ComponentUI.DateTimeInterval_sms();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.cboOrderStatus = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtOrderNum = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.cboOrgId = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cboCompany = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cboOrderType = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cboHandle = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtcCustName2 = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txtcCustName1 = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblCustName2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCustName1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvVerification = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colChk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colAccountVerificationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustName1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustName2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdvanceBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrgId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHandle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClear = new System.Windows.Forms.ToolStripMenuItem();
            this.tss1 = new System.Windows.Forms.ToolStripSeparator();
            this.tss2 = new System.Windows.Forms.ToolStripSeparator();
            this.tss3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOperation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSubmit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVerify = new System.Windows.Forms.ToolStripMenuItem();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVerification)).BeginInit();
            this.cmsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.pnlSearch.BorderWidth = 1;
            this.pnlSearch.Controls.Add(this.dtInterval);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Controls.Add(this.cboOrderStatus);
            this.pnlSearch.Controls.Add(this.txtOrderNum);
            this.pnlSearch.Controls.Add(this.cboOrgId);
            this.pnlSearch.Controls.Add(this.cboCompany);
            this.pnlSearch.Controls.Add(this.cboOrderType);
            this.pnlSearch.Controls.Add(this.cboHandle);
            this.pnlSearch.Controls.Add(this.txtcCustName2);
            this.pnlSearch.Controls.Add(this.txtcCustName1);
            this.pnlSearch.Controls.Add(this.lblStatus);
            this.pnlSearch.Controls.Add(this.lblCustName2);
            this.pnlSearch.Controls.Add(this.label8);
            this.pnlSearch.Controls.Add(this.lblCustName1);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Curvature = 0;
            this.pnlSearch.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlSearch.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlSearch.Location = new System.Drawing.Point(3, 37);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1024, 142);
            this.pnlSearch.TabIndex = 3;
            // 
            // dtInterval
            // 
            this.dtInterval.BackColor = System.Drawing.Color.Transparent;
            this.dtInterval.customFormat = "yyyy-MM-dd";
            this.dtInterval.EndDate = "";
            this.dtInterval.Location = new System.Drawing.Point(87, 101);
            this.dtInterval.Margin = new System.Windows.Forms.Padding(0);
            this.dtInterval.Name = "dtInterval";
            this.dtInterval.Size = new System.Drawing.Size(263, 27);
            this.dtInterval.StartDate = "";
            this.dtInterval.TabIndex = 22;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(902, 99);
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
            this.btnClear.Location = new System.Drawing.Point(789, 99);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 20;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cboOrderStatus
            // 
            this.cboOrderStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboOrderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrderStatus.FormattingEnabled = true;
            this.cboOrderStatus.Location = new System.Drawing.Point(855, 59);
            this.cboOrderStatus.Name = "cboOrderStatus";
            this.cboOrderStatus.Size = new System.Drawing.Size(150, 22);
            this.cboOrderStatus.TabIndex = 19;
            // 
            // txtOrderNum
            // 
            this.txtOrderNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtOrderNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtOrderNum.BackColor = System.Drawing.Color.Transparent;
            this.txtOrderNum.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtOrderNum.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtOrderNum.ForeImage = null;
            this.txtOrderNum.Location = new System.Drawing.Point(331, 15);
            this.txtOrderNum.MaxLengh = 32767;
            this.txtOrderNum.Multiline = false;
            this.txtOrderNum.Name = "txtOrderNum";
            this.txtOrderNum.Radius = 3;
            this.txtOrderNum.ReadOnly = false;
            this.txtOrderNum.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtOrderNum.ShowError = false;
            this.txtOrderNum.Size = new System.Drawing.Size(137, 23);
            this.txtOrderNum.TabIndex = 18;
            this.txtOrderNum.UseSystemPasswordChar = false;
            this.txtOrderNum.Value = "";
            this.txtOrderNum.VerifyCondition = null;
            this.txtOrderNum.VerifyType = null;
            this.txtOrderNum.WaterMark = null;
            this.txtOrderNum.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // cboOrgId
            // 
            this.cboOrgId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboOrgId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrgId.FormattingEnabled = true;
            this.cboOrgId.Location = new System.Drawing.Point(331, 59);
            this.cboOrgId.Name = "cboOrgId";
            this.cboOrgId.Size = new System.Drawing.Size(137, 22);
            this.cboOrgId.TabIndex = 15;
            this.cboOrgId.SelectedIndexChanged += new System.EventHandler(this.cboOrgId_SelectedIndexChanged);
            // 
            // cboCompany
            // 
            this.cboCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(87, 59);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(121, 22);
            this.cboCompany.TabIndex = 14;
            this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.cboCompany_SelectedIndexChanged);
            // 
            // cboOrderType
            // 
            this.cboOrderType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrderType.FormattingEnabled = true;
            this.cboOrderType.Location = new System.Drawing.Point(87, 15);
            this.cboOrderType.Name = "cboOrderType";
            this.cboOrderType.Size = new System.Drawing.Size(120, 22);
            this.cboOrderType.TabIndex = 13;
            this.cboOrderType.SelectedIndexChanged += new System.EventHandler(this.cboOrderType_SelectedIndexChanged);
            // 
            // cboHandle
            // 
            this.cboHandle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboHandle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHandle.FormattingEnabled = true;
            this.cboHandle.Location = new System.Drawing.Point(585, 59);
            this.cboHandle.Name = "cboHandle";
            this.cboHandle.Size = new System.Drawing.Size(150, 22);
            this.cboHandle.TabIndex = 12;
            // 
            // txtcCustName2
            // 
            this.txtcCustName2.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcCustName2.Location = new System.Drawing.Point(855, 14);
            this.txtcCustName2.Name = "txtcCustName2";
            this.txtcCustName2.ReadOnly = false;
            this.txtcCustName2.Size = new System.Drawing.Size(150, 24);
            this.txtcCustName2.TabIndex = 11;
            this.txtcCustName2.ToolTipTitle = "";
            this.txtcCustName2.ChooserClick += new System.EventHandler(this.txtcCustName2_ChooserClick);
            // 
            // txtcCustName1
            // 
            this.txtcCustName1.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcCustName1.Location = new System.Drawing.Point(585, 14);
            this.txtcCustName1.Name = "txtcCustName1";
            this.txtcCustName1.ReadOnly = false;
            this.txtcCustName1.Size = new System.Drawing.Size(150, 24);
            this.txtcCustName1.TabIndex = 10;
            this.txtcCustName1.ToolTipTitle = "";
            this.txtcCustName1.ChooserClick += new System.EventHandler(this.txtcCustName1_ChooserClick);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(784, 64);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(65, 12);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "单据状态：";
            // 
            // lblCustName2
            // 
            this.lblCustName2.Location = new System.Drawing.Point(758, 20);
            this.lblCustName2.Name = "lblCustName2";
            this.lblCustName2.Size = new System.Drawing.Size(91, 12);
            this.lblCustName2.TabIndex = 8;
            this.lblCustName2.Text = "往来单位2：";
            this.lblCustName2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(526, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "经办人：";
            // 
            // lblCustName1
            // 
            this.lblCustName1.Location = new System.Drawing.Point(479, 20);
            this.lblCustName1.Name = "lblCustName1";
            this.lblCustName1.Size = new System.Drawing.Size(100, 12);
            this.lblCustName1.TabIndex = 6;
            this.lblCustName1.Text = "往来单位1：";
            this.lblCustName1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(284, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "部门：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(260, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "单据编号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "单据日期：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "公司：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "单据类型：";
            // 
            // dgvVerification
            // 
            this.dgvVerification.AllowUserToAddRows = false;
            this.dgvVerification.AllowUserToDeleteRows = false;
            this.dgvVerification.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvVerification.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVerification.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVerification.BackgroundColor = System.Drawing.Color.White;
            this.dgvVerification.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVerification.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVerification.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVerification.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChk,
            this.colAccountVerificationID,
            this.colOrderType,
            this.colOrderNum,
            this.colOrderDate,
            this.colCustName1,
            this.colCustName2,
            this.colAdvanceBalance,
            this.colOrgId,
            this.colHandle,
            this.colOperator,
            this.colRemark,
            this.colOrderStatus});
            this.dgvVerification.ContextMenuStrip = this.cmsMenu;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVerification.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvVerification.EnableHeadersVisualStyles = false;
            this.dgvVerification.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvVerification.IsCheck = true;
            this.dgvVerification.Location = new System.Drawing.Point(3, 185);
            this.dgvVerification.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvVerification.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvVerification.MergeColumnNames")));
            this.dgvVerification.MultiSelect = false;
            this.dgvVerification.Name = "dgvVerification";
            this.dgvVerification.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVerification.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvVerification.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvVerification.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvVerification.RowTemplate.Height = 23;
            this.dgvVerification.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVerification.ShowCheckBox = true;
            this.dgvVerification.Size = new System.Drawing.Size(1024, 319);
            this.dgvVerification.TabIndex = 4;
            this.dgvVerification.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVerification_CellContentClick);
            this.dgvVerification.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVerification_CellDoubleClick);
            this.dgvVerification.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvVerification_CellMouseClick);
            // 
            // colChk
            // 
            this.colChk.HeaderText = "";
            this.colChk.MinimumWidth = 18;
            this.colChk.Name = "colChk";
            this.colChk.ReadOnly = true;
            this.colChk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colChk.Width = 25;
            // 
            // colAccountVerificationID
            // 
            this.colAccountVerificationID.HeaderText = "ID";
            this.colAccountVerificationID.Name = "colAccountVerificationID";
            this.colAccountVerificationID.ReadOnly = true;
            this.colAccountVerificationID.Visible = false;
            // 
            // colOrderType
            // 
            this.colOrderType.HeaderText = "单据类型";
            this.colOrderType.Name = "colOrderType";
            this.colOrderType.ReadOnly = true;
            // 
            // colOrderNum
            // 
            this.colOrderNum.HeaderText = "单据编号";
            this.colOrderNum.Name = "colOrderNum";
            this.colOrderNum.ReadOnly = true;
            this.colOrderNum.Width = 150;
            // 
            // colOrderDate
            // 
            this.colOrderDate.HeaderText = "单据日期";
            this.colOrderDate.Name = "colOrderDate";
            this.colOrderDate.ReadOnly = true;
            // 
            // colCustName1
            // 
            this.colCustName1.HeaderText = "往来单位1";
            this.colCustName1.Name = "colCustName1";
            this.colCustName1.ReadOnly = true;
            // 
            // colCustName2
            // 
            this.colCustName2.HeaderText = "往来单位2";
            this.colCustName2.Name = "colCustName2";
            this.colCustName2.ReadOnly = true;
            // 
            // colAdvanceBalance
            // 
            this.colAdvanceBalance.HeaderText = "冲销金额";
            this.colAdvanceBalance.Name = "colAdvanceBalance";
            this.colAdvanceBalance.ReadOnly = true;
            // 
            // colOrgId
            // 
            this.colOrgId.HeaderText = "部门";
            this.colOrgId.Name = "colOrgId";
            this.colOrgId.ReadOnly = true;
            // 
            // colHandle
            // 
            this.colHandle.HeaderText = "经办人";
            this.colHandle.Name = "colHandle";
            this.colHandle.ReadOnly = true;
            // 
            // colOperator
            // 
            this.colOperator.HeaderText = "操作人";
            this.colOperator.Name = "colOperator";
            this.colOperator.ReadOnly = true;
            // 
            // colRemark
            // 
            this.colRemark.HeaderText = "备注";
            this.colRemark.Name = "colRemark";
            this.colRemark.ReadOnly = true;
            // 
            // colOrderStatus
            // 
            this.colOrderStatus.HeaderText = "单据状态";
            this.colOrderStatus.Name = "colOrderStatus";
            this.colOrderStatus.ReadOnly = true;
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSearch,
            this.tsmiClear,
            this.tss1,
            this.tss2,
            this.tss3,
            this.tsmiOperation,
            this.tsmiView,
            this.tsmiPrint});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(153, 286);
            // 
            // tsmiSearch
            // 
            this.tsmiSearch.Name = "tsmiSearch";
            this.tsmiSearch.Size = new System.Drawing.Size(152, 22);
            this.tsmiSearch.Text = "查询";
            this.tsmiSearch.Click += new System.EventHandler(this.tsmiSearch_Click);
            // 
            // tsmiClear
            // 
            this.tsmiClear.Name = "tsmiClear";
            this.tsmiClear.Size = new System.Drawing.Size(152, 22);
            this.tsmiClear.Text = "清除";
            this.tsmiClear.Click += new System.EventHandler(this.tsmiClear_Click);
            // 
            // tss1
            // 
            this.tss1.Name = "tss1";
            this.tss1.Size = new System.Drawing.Size(149, 6);
            // 
            // tss2
            // 
            this.tss2.Name = "tss2";
            this.tss2.Size = new System.Drawing.Size(149, 6);
            // 
            // tss3
            // 
            this.tss3.Name = "tss3";
            this.tss3.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiOperation
            // 
            this.tsmiOperation.Name = "tsmiOperation";
            this.tsmiOperation.Size = new System.Drawing.Size(152, 22);
            this.tsmiOperation.Text = "操作记录";
            this.tsmiOperation.Click += new System.EventHandler(this.tsmiOperation_Click);
            // 
            // tsmiView
            // 
            this.tsmiView.Name = "tsmiView";
            this.tsmiView.Size = new System.Drawing.Size(152, 22);
            this.tsmiView.Text = "预览";
            this.tsmiView.Click += new System.EventHandler(this.tsmiView_Click);
            // 
            // tsmiPrint
            // 
            this.tsmiPrint.Name = "tsmiPrint";
            this.tsmiPrint.Size = new System.Drawing.Size(152, 22);
            this.tsmiPrint.Text = "打印";
            this.tsmiPrint.Click += new System.EventHandler(this.tsmiPrint_Click);
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(152, 22);
            this.tsmiAdd.Text = "新建";
            this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(152, 22);
            this.tsmiEdit.Text = "编辑";
            this.tsmiEdit.Click += new System.EventHandler(this.tsmiEdit_Click);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(152, 22);
            this.tsmiCopy.Text = "复制";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(152, 22);
            this.tsmiDelete.Text = "删除";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // tsmiSubmit
            // 
            this.tsmiSubmit.Name = "tsmiSubmit";
            this.tsmiSubmit.Size = new System.Drawing.Size(152, 22);
            this.tsmiSubmit.Text = "提交";
            this.tsmiSubmit.Click += new System.EventHandler(this.tsmiSubmit_Click);
            // 
            // tsmiVerify
            // 
            this.tsmiVerify.Name = "tsmiVerify";
            this.tsmiVerify.Size = new System.Drawing.Size(152, 22);
            this.tsmiVerify.Text = "审核";
            this.tsmiVerify.Click += new System.EventHandler(this.tsmiVerify_Click);
            // 
            // page
            // 
            this.page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.page.Location = new System.Drawing.Point(547, 510);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(480, 31);
            this.page.TabIndex = 5;
            this.page.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            // 
            // UCAccountVerificationManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.page);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.dgvVerification);
            this.Name = "UCAccountVerificationManage";
            this.Load += new System.EventHandler(this.UCAccountVerificationManage_Load);
            this.Controls.SetChildIndex(this.dgvVerification, 0);
            this.Controls.SetChildIndex(this.pnlSearch, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.page, 0);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVerification)).EndInit();
            this.cmsMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx pnlSearch;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboOrderStatus;
        private ServiceStationClient.ComponentUI.TextBoxEx txtOrderNum;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboOrgId;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboCompany;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboOrderType;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboHandle;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtcCustName2;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtcCustName1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblCustName2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCustName1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvVerification;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private ServiceStationClient.ComponentUI.DateTimeInterval_sms dtInterval;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiSearch;
        private System.Windows.Forms.ToolStripMenuItem tsmiClear;
        private System.Windows.Forms.ToolStripSeparator tss1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripSeparator tss2;
        private System.Windows.Forms.ToolStripMenuItem tsmiSubmit;
        private System.Windows.Forms.ToolStripMenuItem tsmiVerify;
        private System.Windows.Forms.ToolStripSeparator tss3;
        private System.Windows.Forms.ToolStripMenuItem tsmiOperation;
        private System.Windows.Forms.ToolStripMenuItem tsmiView;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrint;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChk;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccountVerificationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustName1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustName2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdvanceBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrgId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHandle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderStatus;
    }
}
