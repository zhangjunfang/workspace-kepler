namespace HXCPcClient.UCForm.RepairBusiness.Reserve
{
    partial class ReserveOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReserveOrder));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.palBlack = new System.Windows.Forms.Panel();
            this.palData = new System.Windows.Forms.Panel();
            this.dgvRData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSubmit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVerify = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOperation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.palSearch = new System.Windows.Forms.Panel();
            this.txtCustomNO = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.labCustomNO = new System.Windows.Forms.Label();
            this.txtCarNO = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.labCarNO = new System.Windows.Forms.Label();
            this.cobOrderStatus = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cobPayType = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.dtpReInETime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpReInSTime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpReserveETime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpReserveSTime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.txtReservationman = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtContactPhone = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtContact = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtRepairNO = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtReserverNO = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtCustomName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labReInTime = new System.Windows.Forms.Label();
            this.labOrderStatus = new System.Windows.Forms.Label();
            this.labPayType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labReserveTime = new System.Windows.Forms.Label();
            this.labDrivers = new System.Windows.Forms.Label();
            this.labContactPhone = new System.Windows.Forms.Label();
            this.labContact = new System.Windows.Forms.Label();
            this.labRepairNO = new System.Windows.Forms.Label();
            this.labReserverNO = new System.Windows.Forms.Label();
            this.labCustomName = new System.Windows.Forms.Label();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.reservation_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reservation_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maintain_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reservation_man = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reservation_mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whether_greet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.greet_site = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fault_describe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.document_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkman = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.link_man_mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maintain_payment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maintain_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reserv_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.palBlack.SuspendLayout();
            this.palData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).BeginInit();
            this.cmsMenu.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.palSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // palBlack
            // 
            this.palBlack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palBlack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.palBlack.Controls.Add(this.palData);
            this.palBlack.Controls.Add(this.panelEx2);
            this.palBlack.Controls.Add(this.palSearch);
            this.palBlack.Location = new System.Drawing.Point(1, 33);
            this.palBlack.Name = "palBlack";
            this.palBlack.Size = new System.Drawing.Size(1026, 509);
            this.palBlack.TabIndex = 3;
            // 
            // palData
            // 
            this.palData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.palData.Controls.Add(this.dgvRData);
            this.palData.Location = new System.Drawing.Point(3, 146);
            this.palData.Name = "palData";
            this.palData.Size = new System.Drawing.Size(1020, 334);
            this.palData.TabIndex = 14;
            // 
            // dgvRData
            // 
            this.dgvRData.AllowUserToAddRows = false;
            this.dgvRData.AllowUserToDeleteRows = false;
            this.dgvRData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvRData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRData.BackgroundColor = System.Drawing.Color.White;
            this.dgvRData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.reservation_no,
            this.reservation_date,
            this.maintain_time,
            this.reservation_man,
            this.reservation_mobile,
            this.whether_greet,
            this.greet_site,
            this.fault_describe,
            this.document_status,
            this.customer_code,
            this.customer_name,
            this.vehicle_no,
            this.linkman,
            this.link_man_mobile,
            this.maintain_payment,
            this.maintain_no,
            this.remark,
            this.reserv_id});
            this.dgvRData.ContextMenuStrip = this.cmsMenu;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRData.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRData.EnableHeadersVisualStyles = false;
            this.dgvRData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvRData.Location = new System.Drawing.Point(3, 3);
            this.dgvRData.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvRData.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvRData.MergeColumnNames")));
            this.dgvRData.MultiSelect = false;
            this.dgvRData.Name = "dgvRData";
            this.dgvRData.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRData.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRData.RowHeadersVisible = false;
            this.dgvRData.RowHeadersWidth = 30;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvRData.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvRData.RowTemplate.Height = 23;
            this.dgvRData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRData.ShowCheckBox = true;
            this.dgvRData.Size = new System.Drawing.Size(1014, 326);
            this.dgvRData.TabIndex = 12;
            this.dgvRData.HeadCheckChanged += new ServiceStationClient.ComponentUI.DataGridViewEx.DelegateOnClick(this.dgvRData_HeadCheckChanged);
            this.dgvRData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRData_CellContentClick);
            this.dgvRData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRData_CellDoubleClick);
            this.dgvRData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRData_CellFormatting);
            this.dgvRData.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRData_CellMouseClick);
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSearch,
            this.tsmiClear,
            this.toolStripSeparator1,
            this.tsmiAdd,
            this.tsmiEdit,
            this.tsmiCopy,
            this.tsmiDelete,
            this.toolStripSeparator2,
            this.tsmiSubmit,
            this.tsmiVerify,
            this.toolStripSeparator3,
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiOperation
            // 
            this.tsmiOperation.Name = "tsmiOperation";
            this.tsmiOperation.Size = new System.Drawing.Size(152, 22);
            this.tsmiOperation.Text = "操作记录";
            // 
            // tsmiView
            // 
            this.tsmiView.Name = "tsmiView";
            this.tsmiView.Size = new System.Drawing.Size(152, 22);
            this.tsmiView.Text = "预览";
            // 
            // tsmiPrint
            // 
            this.tsmiPrint.Name = "tsmiPrint";
            this.tsmiPrint.Size = new System.Drawing.Size(152, 22);
            this.tsmiPrint.Text = "打印";
            // 
            // panelEx2
            // 
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.page);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(0, 481);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.panelEx2.Size = new System.Drawing.Size(1026, 28);
            this.panelEx2.TabIndex = 13;
            // 
            // page
            // 
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.page.Dock = System.Windows.Forms.DockStyle.Right;
            this.page.Location = new System.Drawing.Point(498, 0);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(428, 28);
            this.page.TabIndex = 5;
            this.page.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            // 
            // palSearch
            // 
            this.palSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.palSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.palSearch.Controls.Add(this.txtCustomNO);
            this.palSearch.Controls.Add(this.labCustomNO);
            this.palSearch.Controls.Add(this.txtCarNO);
            this.palSearch.Controls.Add(this.labCarNO);
            this.palSearch.Controls.Add(this.cobOrderStatus);
            this.palSearch.Controls.Add(this.cobPayType);
            this.palSearch.Controls.Add(this.btnQuery);
            this.palSearch.Controls.Add(this.btnClear);
            this.palSearch.Controls.Add(this.dtpReInETime);
            this.palSearch.Controls.Add(this.dtpReInSTime);
            this.palSearch.Controls.Add(this.dtpReserveETime);
            this.palSearch.Controls.Add(this.dtpReserveSTime);
            this.palSearch.Controls.Add(this.txtReservationman);
            this.palSearch.Controls.Add(this.txtContactPhone);
            this.palSearch.Controls.Add(this.txtContact);
            this.palSearch.Controls.Add(this.txtRepairNO);
            this.palSearch.Controls.Add(this.txtReserverNO);
            this.palSearch.Controls.Add(this.txtCustomName);
            this.palSearch.Controls.Add(this.label2);
            this.palSearch.Controls.Add(this.label4);
            this.palSearch.Controls.Add(this.label1);
            this.palSearch.Controls.Add(this.labReInTime);
            this.palSearch.Controls.Add(this.labOrderStatus);
            this.palSearch.Controls.Add(this.labPayType);
            this.palSearch.Controls.Add(this.label3);
            this.palSearch.Controls.Add(this.labReserveTime);
            this.palSearch.Controls.Add(this.labDrivers);
            this.palSearch.Controls.Add(this.labContactPhone);
            this.palSearch.Controls.Add(this.labContact);
            this.palSearch.Controls.Add(this.labRepairNO);
            this.palSearch.Controls.Add(this.labReserverNO);
            this.palSearch.Controls.Add(this.labCustomName);
            this.palSearch.Location = new System.Drawing.Point(3, 3);
            this.palSearch.Name = "palSearch";
            this.palSearch.Size = new System.Drawing.Size(1020, 141);
            this.palSearch.TabIndex = 10;
            // 
            // txtCustomNO
            // 
            this.txtCustomNO.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCustomNO.Location = new System.Drawing.Point(293, 2);
            this.txtCustomNO.Name = "txtCustomNO";
            this.txtCustomNO.ReadOnly = false;
            this.txtCustomNO.Size = new System.Drawing.Size(117, 24);
            this.txtCustomNO.TabIndex = 71;
            this.txtCustomNO.ToolTipTitle = "";
            this.txtCustomNO.ChooserClick += new System.EventHandler(this.txtCustomNO_ChooserClick);
            // 
            // labCustomNO
            // 
            this.labCustomNO.AutoSize = true;
            this.labCustomNO.Location = new System.Drawing.Point(226, 8);
            this.labCustomNO.Name = "labCustomNO";
            this.labCustomNO.Size = new System.Drawing.Size(65, 12);
            this.labCustomNO.TabIndex = 70;
            this.labCustomNO.Text = "客户编码：";
            // 
            // txtCarNO
            // 
            this.txtCarNO.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCarNO.Location = new System.Drawing.Point(92, 2);
            this.txtCarNO.Name = "txtCarNO";
            this.txtCarNO.ReadOnly = false;
            this.txtCarNO.Size = new System.Drawing.Size(120, 24);
            this.txtCarNO.TabIndex = 69;
            this.txtCarNO.ToolTipTitle = "";
            this.txtCarNO.ChooserClick += new System.EventHandler(this.txtCarNO_ChooserClick);
            // 
            // labCarNO
            // 
            this.labCarNO.AutoSize = true;
            this.labCarNO.Location = new System.Drawing.Point(39, 8);
            this.labCarNO.Name = "labCarNO";
            this.labCarNO.Size = new System.Drawing.Size(53, 12);
            this.labCarNO.TabIndex = 68;
            this.labCarNO.Text = "车牌号：";
            // 
            // cobOrderStatus
            // 
            this.cobOrderStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobOrderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobOrderStatus.FormattingEnabled = true;
            this.cobOrderStatus.Location = new System.Drawing.Point(701, 71);
            this.cobOrderStatus.Name = "cobOrderStatus";
            this.cobOrderStatus.Size = new System.Drawing.Size(121, 22);
            this.cobOrderStatus.TabIndex = 35;
            // 
            // cobPayType
            // 
            this.cobPayType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobPayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobPayType.FormattingEnabled = true;
            this.cobPayType.Location = new System.Drawing.Point(504, 70);
            this.cobPayType.Name = "cobPayType";
            this.cobPayType.Size = new System.Drawing.Size(121, 22);
            this.cobPayType.TabIndex = 34;
            // 
            // btnQuery
            // 
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(839, 67);
            this.btnQuery.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.MoveImage")));
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.NormalImage")));
            this.btnQuery.Size = new System.Drawing.Size(60, 26);
            this.btnQuery.TabIndex = 33;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(839, 34);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 32;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dtpReInETime
            // 
            this.dtpReInETime.Location = new System.Drawing.Point(247, 107);
            this.dtpReInETime.Name = "dtpReInETime";
            this.dtpReInETime.ShowFormat = "yyyy-MM-dd HH:mm";
            this.dtpReInETime.Size = new System.Drawing.Size(121, 21);
            this.dtpReInETime.TabIndex = 27;
            this.dtpReInETime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // dtpReInSTime
            // 
            this.dtpReInSTime.Location = new System.Drawing.Point(101, 108);
            this.dtpReInSTime.Name = "dtpReInSTime";
            this.dtpReInSTime.ShowFormat = "yyyy-MM-dd HH:mm";
            this.dtpReInSTime.Size = new System.Drawing.Size(121, 21);
            this.dtpReInSTime.TabIndex = 26;
            this.dtpReInSTime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // dtpReserveETime
            // 
            this.dtpReserveETime.Location = new System.Drawing.Point(247, 72);
            this.dtpReserveETime.Name = "dtpReserveETime";
            this.dtpReserveETime.ShowFormat = "yyyy-MM-dd HH:mm";
            this.dtpReserveETime.Size = new System.Drawing.Size(121, 21);
            this.dtpReserveETime.TabIndex = 25;
            this.dtpReserveETime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // dtpReserveSTime
            // 
            this.dtpReserveSTime.Location = new System.Drawing.Point(101, 72);
            this.dtpReserveSTime.Name = "dtpReserveSTime";
            this.dtpReserveSTime.ShowFormat = "yyyy-MM-dd HH:mm";
            this.dtpReserveSTime.Size = new System.Drawing.Size(121, 21);
            this.dtpReserveSTime.TabIndex = 24;
            this.dtpReserveSTime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // txtReservationman
            // 
            this.txtReservationman.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtReservationman.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtReservationman.BackColor = System.Drawing.Color.Transparent;
            this.txtReservationman.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtReservationman.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtReservationman.ForeImage = null;
            this.txtReservationman.Location = new System.Drawing.Point(702, 37);
            this.txtReservationman.MaxLengh = 32767;
            this.txtReservationman.Multiline = false;
            this.txtReservationman.Name = "txtReservationman";
            this.txtReservationman.Radius = 3;
            this.txtReservationman.ReadOnly = false;
            this.txtReservationman.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtReservationman.ShowError = false;
            this.txtReservationman.Size = new System.Drawing.Size(120, 23);
            this.txtReservationman.TabIndex = 23;
            this.txtReservationman.UseSystemPasswordChar = false;
            this.txtReservationman.Value = "";
            this.txtReservationman.VerifyCondition = null;
            this.txtReservationman.VerifyType = null;
            this.txtReservationman.WaterMark = null;
            this.txtReservationman.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtContactPhone
            // 
            this.txtContactPhone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContactPhone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContactPhone.BackColor = System.Drawing.Color.Transparent;
            this.txtContactPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContactPhone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContactPhone.ForeImage = null;
            this.txtContactPhone.Location = new System.Drawing.Point(504, 37);
            this.txtContactPhone.MaxLengh = 32767;
            this.txtContactPhone.Multiline = false;
            this.txtContactPhone.Name = "txtContactPhone";
            this.txtContactPhone.Radius = 3;
            this.txtContactPhone.ReadOnly = false;
            this.txtContactPhone.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContactPhone.ShowError = false;
            this.txtContactPhone.Size = new System.Drawing.Size(120, 23);
            this.txtContactPhone.TabIndex = 22;
            this.txtContactPhone.UseSystemPasswordChar = false;
            this.txtContactPhone.Value = "";
            this.txtContactPhone.VerifyCondition = null;
            this.txtContactPhone.VerifyType = null;
            this.txtContactPhone.WaterMark = null;
            this.txtContactPhone.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtContact
            // 
            this.txtContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtContact.BackColor = System.Drawing.Color.Transparent;
            this.txtContact.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtContact.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtContact.ForeImage = null;
            this.txtContact.Location = new System.Drawing.Point(295, 37);
            this.txtContact.MaxLengh = 32767;
            this.txtContact.Multiline = false;
            this.txtContact.Name = "txtContact";
            this.txtContact.Radius = 3;
            this.txtContact.ReadOnly = false;
            this.txtContact.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtContact.ShowError = false;
            this.txtContact.Size = new System.Drawing.Size(120, 23);
            this.txtContact.TabIndex = 21;
            this.txtContact.UseSystemPasswordChar = false;
            this.txtContact.Value = "";
            this.txtContact.VerifyCondition = null;
            this.txtContact.VerifyType = null;
            this.txtContact.WaterMark = null;
            this.txtContact.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtRepairNO
            // 
            this.txtRepairNO.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtRepairNO.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtRepairNO.BackColor = System.Drawing.Color.Transparent;
            this.txtRepairNO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtRepairNO.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtRepairNO.ForeImage = null;
            this.txtRepairNO.Location = new System.Drawing.Point(92, 37);
            this.txtRepairNO.MaxLengh = 32767;
            this.txtRepairNO.Multiline = false;
            this.txtRepairNO.Name = "txtRepairNO";
            this.txtRepairNO.Radius = 3;
            this.txtRepairNO.ReadOnly = false;
            this.txtRepairNO.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtRepairNO.ShowError = false;
            this.txtRepairNO.Size = new System.Drawing.Size(120, 23);
            this.txtRepairNO.TabIndex = 20;
            this.txtRepairNO.UseSystemPasswordChar = false;
            this.txtRepairNO.Value = "";
            this.txtRepairNO.VerifyCondition = null;
            this.txtRepairNO.VerifyType = null;
            this.txtRepairNO.WaterMark = null;
            this.txtRepairNO.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtReserverNO
            // 
            this.txtReserverNO.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtReserverNO.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtReserverNO.BackColor = System.Drawing.Color.Transparent;
            this.txtReserverNO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtReserverNO.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtReserverNO.ForeImage = null;
            this.txtReserverNO.Location = new System.Drawing.Point(702, 3);
            this.txtReserverNO.MaxLengh = 32767;
            this.txtReserverNO.Multiline = false;
            this.txtReserverNO.Name = "txtReserverNO";
            this.txtReserverNO.Radius = 3;
            this.txtReserverNO.ReadOnly = false;
            this.txtReserverNO.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtReserverNO.ShowError = false;
            this.txtReserverNO.Size = new System.Drawing.Size(120, 23);
            this.txtReserverNO.TabIndex = 19;
            this.txtReserverNO.UseSystemPasswordChar = false;
            this.txtReserverNO.Value = "";
            this.txtReserverNO.VerifyCondition = null;
            this.txtReserverNO.VerifyType = null;
            this.txtReserverNO.WaterMark = null;
            this.txtReserverNO.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtCustomName
            // 
            this.txtCustomName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCustomName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCustomName.BackColor = System.Drawing.Color.Transparent;
            this.txtCustomName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCustomName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCustomName.ForeImage = null;
            this.txtCustomName.Location = new System.Drawing.Point(504, 3);
            this.txtCustomName.MaxLengh = 32767;
            this.txtCustomName.Multiline = false;
            this.txtCustomName.Name = "txtCustomName";
            this.txtCustomName.Radius = 3;
            this.txtCustomName.ReadOnly = false;
            this.txtCustomName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCustomName.ShowError = false;
            this.txtCustomName.Size = new System.Drawing.Size(120, 23);
            this.txtCustomName.TabIndex = 18;
            this.txtCustomName.UseSystemPasswordChar = false;
            this.txtCustomName.Value = "";
            this.txtCustomName.VerifyCondition = null;
            this.txtCustomName.VerifyType = null;
            this.txtCustomName.WaterMark = null;
            this.txtCustomName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "从";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(226, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "到";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "从";
            // 
            // labReInTime
            // 
            this.labReInTime.AutoSize = true;
            this.labReInTime.Location = new System.Drawing.Point(3, 110);
            this.labReInTime.Name = "labReInTime";
            this.labReInTime.Size = new System.Drawing.Size(89, 12);
            this.labReInTime.TabIndex = 12;
            this.labReInTime.Text = "预约进场时间：";
            // 
            // labOrderStatus
            // 
            this.labOrderStatus.AutoSize = true;
            this.labOrderStatus.Location = new System.Drawing.Point(631, 75);
            this.labOrderStatus.Name = "labOrderStatus";
            this.labOrderStatus.Size = new System.Drawing.Size(65, 12);
            this.labOrderStatus.TabIndex = 11;
            this.labOrderStatus.Text = "单据状态：";
            // 
            // labPayType
            // 
            this.labPayType.AutoSize = true;
            this.labPayType.Location = new System.Drawing.Point(409, 75);
            this.labPayType.Name = "labPayType";
            this.labPayType.Size = new System.Drawing.Size(89, 12);
            this.labPayType.TabIndex = 10;
            this.labPayType.Text = "维修付费方式：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(226, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "到";
            // 
            // labReserveTime
            // 
            this.labReserveTime.AutoSize = true;
            this.labReserveTime.Location = new System.Drawing.Point(27, 75);
            this.labReserveTime.Name = "labReserveTime";
            this.labReserveTime.Size = new System.Drawing.Size(65, 12);
            this.labReserveTime.TabIndex = 8;
            this.labReserveTime.Text = "预约日期：";
            // 
            // labDrivers
            // 
            this.labDrivers.AutoSize = true;
            this.labDrivers.Location = new System.Drawing.Point(643, 42);
            this.labDrivers.Name = "labDrivers";
            this.labDrivers.Size = new System.Drawing.Size(53, 12);
            this.labDrivers.TabIndex = 7;
            this.labDrivers.Text = "预约人：";
            // 
            // labContactPhone
            // 
            this.labContactPhone.AutoSize = true;
            this.labContactPhone.Location = new System.Drawing.Point(421, 42);
            this.labContactPhone.Name = "labContactPhone";
            this.labContactPhone.Size = new System.Drawing.Size(77, 12);
            this.labContactPhone.TabIndex = 6;
            this.labContactPhone.Text = "联系人手机：";
            // 
            // labContact
            // 
            this.labContact.AutoSize = true;
            this.labContact.Location = new System.Drawing.Point(238, 42);
            this.labContact.Name = "labContact";
            this.labContact.Size = new System.Drawing.Size(53, 12);
            this.labContact.TabIndex = 5;
            this.labContact.Text = "联系人：";
            // 
            // labRepairNO
            // 
            this.labRepairNO.AutoSize = true;
            this.labRepairNO.Location = new System.Drawing.Point(27, 43);
            this.labRepairNO.Name = "labRepairNO";
            this.labRepairNO.Size = new System.Drawing.Size(65, 12);
            this.labRepairNO.TabIndex = 4;
            this.labRepairNO.Text = "维修单号：";
            // 
            // labReserverNO
            // 
            this.labReserverNO.AutoSize = true;
            this.labReserverNO.Location = new System.Drawing.Point(631, 8);
            this.labReserverNO.Name = "labReserverNO";
            this.labReserverNO.Size = new System.Drawing.Size(65, 12);
            this.labReserverNO.TabIndex = 3;
            this.labReserverNO.Text = "预约单号：";
            // 
            // labCustomName
            // 
            this.labCustomName.AutoSize = true;
            this.labCustomName.Location = new System.Drawing.Point(433, 8);
            this.labCustomName.Name = "labCustomName";
            this.labCustomName.Size = new System.Drawing.Size(65, 12);
            this.labCustomName.TabIndex = 2;
            this.labCustomName.Text = "客户名称：";
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 18;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 40;
            // 
            // reservation_no
            // 
            this.reservation_no.DataPropertyName = "reservation_no";
            this.reservation_no.HeaderText = "预约单号";
            this.reservation_no.MinimumWidth = 110;
            this.reservation_no.Name = "reservation_no";
            this.reservation_no.ReadOnly = true;
            this.reservation_no.Width = 110;
            // 
            // reservation_date
            // 
            this.reservation_date.DataPropertyName = "reservation_date";
            this.reservation_date.HeaderText = "预约日期";
            this.reservation_date.Name = "reservation_date";
            this.reservation_date.ReadOnly = true;
            this.reservation_date.Width = 90;
            // 
            // maintain_time
            // 
            this.maintain_time.DataPropertyName = "maintain_time";
            this.maintain_time.HeaderText = "预约进场时间";
            this.maintain_time.Name = "maintain_time";
            this.maintain_time.ReadOnly = true;
            this.maintain_time.Width = 110;
            // 
            // reservation_man
            // 
            this.reservation_man.DataPropertyName = "reservation_man";
            this.reservation_man.HeaderText = "预约人";
            this.reservation_man.Name = "reservation_man";
            this.reservation_man.ReadOnly = true;
            this.reservation_man.Width = 70;
            // 
            // reservation_mobile
            // 
            this.reservation_mobile.DataPropertyName = "reservation_mobile";
            this.reservation_mobile.HeaderText = "预约人手机";
            this.reservation_mobile.Name = "reservation_mobile";
            this.reservation_mobile.ReadOnly = true;
            // 
            // whether_greet
            // 
            this.whether_greet.DataPropertyName = "whether_greet";
            this.whether_greet.HeaderText = "是否接车";
            this.whether_greet.Name = "whether_greet";
            this.whether_greet.ReadOnly = true;
            this.whether_greet.Width = 90;
            // 
            // greet_site
            // 
            this.greet_site.DataPropertyName = "greet_site";
            this.greet_site.HeaderText = "接车地址";
            this.greet_site.Name = "greet_site";
            this.greet_site.ReadOnly = true;
            this.greet_site.Width = 90;
            // 
            // fault_describe
            // 
            this.fault_describe.DataPropertyName = "fault_describe";
            this.fault_describe.HeaderText = "故障描述";
            this.fault_describe.Name = "fault_describe";
            this.fault_describe.ReadOnly = true;
            // 
            // document_status
            // 
            this.document_status.DataPropertyName = "document_status";
            this.document_status.HeaderText = "单据状态";
            this.document_status.Name = "document_status";
            this.document_status.ReadOnly = true;
            this.document_status.Width = 90;
            // 
            // customer_code
            // 
            this.customer_code.DataPropertyName = "customer_code";
            this.customer_code.HeaderText = "客户编码";
            this.customer_code.Name = "customer_code";
            this.customer_code.ReadOnly = true;
            this.customer_code.Width = 90;
            // 
            // customer_name
            // 
            this.customer_name.DataPropertyName = "customer_name";
            this.customer_name.HeaderText = "客户名称";
            this.customer_name.Name = "customer_name";
            this.customer_name.ReadOnly = true;
            this.customer_name.Width = 90;
            // 
            // vehicle_no
            // 
            this.vehicle_no.DataPropertyName = "vehicle_no";
            this.vehicle_no.HeaderText = "车牌号";
            this.vehicle_no.Name = "vehicle_no";
            this.vehicle_no.ReadOnly = true;
            // 
            // linkman
            // 
            this.linkman.DataPropertyName = "linkman";
            this.linkman.HeaderText = "联系人";
            this.linkman.Name = "linkman";
            this.linkman.ReadOnly = true;
            // 
            // link_man_mobile
            // 
            this.link_man_mobile.DataPropertyName = "link_man_mobile";
            this.link_man_mobile.HeaderText = "联系人手机";
            this.link_man_mobile.Name = "link_man_mobile";
            this.link_man_mobile.ReadOnly = true;
            // 
            // maintain_payment
            // 
            this.maintain_payment.DataPropertyName = "maintain_payment";
            this.maintain_payment.HeaderText = "维修付费方式";
            this.maintain_payment.Name = "maintain_payment";
            this.maintain_payment.ReadOnly = true;
            this.maintain_payment.Width = 110;
            // 
            // maintain_no
            // 
            this.maintain_no.DataPropertyName = "maintain_no";
            this.maintain_no.HeaderText = "维修单号";
            this.maintain_no.Name = "maintain_no";
            this.maintain_no.ReadOnly = true;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            // 
            // reserv_id
            // 
            this.reserv_id.DataPropertyName = "reserv_id";
            this.reserv_id.HeaderText = "reserv_id";
            this.reserv_id.Name = "reserv_id";
            this.reserv_id.ReadOnly = true;
            this.reserv_id.Visible = false;
            this.reserv_id.Width = 10;
            // 
            // ReserveOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.palBlack);
            this.Name = "ReserveOrder";
            this.Load += new System.EventHandler(this.ReserveOrder_Load);
            this.Controls.SetChildIndex(this.palBlack, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.palBlack.ResumeLayout(false);
            this.palData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRData)).EndInit();
            this.cmsMenu.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.palSearch.ResumeLayout(false);
            this.palSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel palBlack;
        private System.Windows.Forms.Panel palData;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRData;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private System.Windows.Forms.Panel palSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpReInETime;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpReInSTime;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpReserveETime;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpReserveSTime;
        private ServiceStationClient.ComponentUI.TextBoxEx txtReservationman;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContactPhone;
        private ServiceStationClient.ComponentUI.TextBoxEx txtContact;
        private ServiceStationClient.ComponentUI.TextBoxEx txtRepairNO;
        private ServiceStationClient.ComponentUI.TextBoxEx txtReserverNO;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCustomName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labReInTime;
        private System.Windows.Forms.Label labOrderStatus;
        private System.Windows.Forms.Label labPayType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labReserveTime;
        private System.Windows.Forms.Label labDrivers;
        private System.Windows.Forms.Label labContactPhone;
        private System.Windows.Forms.Label labContact;
        private System.Windows.Forms.Label labRepairNO;
        private System.Windows.Forms.Label labReserverNO;
        private System.Windows.Forms.Label labCustomName;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobOrderStatus;
        private ServiceStationClient.ComponentUI.ComboBoxEx cobPayType;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCustomNO;
        private System.Windows.Forms.Label labCustomNO;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCarNO;
        private System.Windows.Forms.Label labCarNO;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiSearch;
        private System.Windows.Forms.ToolStripMenuItem tsmiClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiSubmit;
        private System.Windows.Forms.ToolStripMenuItem tsmiVerify;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiOperation;
        private System.Windows.Forms.ToolStripMenuItem tsmiView;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrint;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn reservation_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn reservation_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn maintain_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn reservation_man;
        private System.Windows.Forms.DataGridViewTextBoxColumn reservation_mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn whether_greet;
        private System.Windows.Forms.DataGridViewTextBoxColumn greet_site;
        private System.Windows.Forms.DataGridViewTextBoxColumn fault_describe;
        private System.Windows.Forms.DataGridViewTextBoxColumn document_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkman;
        private System.Windows.Forms.DataGridViewTextBoxColumn link_man_mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn maintain_payment;
        private System.Windows.Forms.DataGridViewTextBoxColumn maintain_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn reserv_id;

    }
}