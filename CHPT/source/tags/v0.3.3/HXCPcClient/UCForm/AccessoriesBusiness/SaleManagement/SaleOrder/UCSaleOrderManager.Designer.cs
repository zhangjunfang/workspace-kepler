namespace HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleOrder
{
    partial class UCSaleOrderManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCSaleOrderManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.txtcus_name = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.ddltrans_mode = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.ddlclosing_way = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.ddlCompany = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtorder_num = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ddlhandle = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.dateTimeStart = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.ddlDepartment = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.ddlState = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimeEnd = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRemark = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.gvPurchaseOrderList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.sale_order_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.order_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sup_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sup_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closing_unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closing_way_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.advance_money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trans_way_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contacts_tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordered_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contract_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delivery_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delivery_add = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_status_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_occupy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operator_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.winFormPager1 = new ServiceStationClient.ComponentUI.WinFormPager();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchaseOrderList)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(937, 25);
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.txtcus_name);
            this.panelEx1.Controls.Add(this.ddltrans_mode);
            this.panelEx1.Controls.Add(this.label9);
            this.panelEx1.Controls.Add(this.ddlclosing_way);
            this.panelEx1.Controls.Add(this.label12);
            this.panelEx1.Controls.Add(this.ddlCompany);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.txtorder_num);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.ddlhandle);
            this.panelEx1.Controls.Add(this.dateTimeStart);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.ddlDepartment);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.ddlState);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.dateTimeEnd);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.txtRemark);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.label8);
            this.panelEx1.Controls.Add(this.label10);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(3, 37);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(934, 159);
            this.panelEx1.TabIndex = 36;
            // 
            // txtcus_name
            // 
            this.txtcus_name.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcus_name.Location = new System.Drawing.Point(92, 18);
            this.txtcus_name.Name = "txtcus_name";
            this.txtcus_name.ReadOnly = false;
            this.txtcus_name.Size = new System.Drawing.Size(116, 24);
            this.txtcus_name.TabIndex = 122;
            this.txtcus_name.ToolTipTitle = "";
            this.txtcus_name.ChooserClick += new System.EventHandler(this.txtcus_name_ChooserClick);
            // 
            // ddltrans_mode
            // 
            this.ddltrans_mode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddltrans_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddltrans_mode.FormattingEnabled = true;
            this.ddltrans_mode.Location = new System.Drawing.Point(92, 52);
            this.ddltrans_mode.Name = "ddltrans_mode";
            this.ddltrans_mode.Size = new System.Drawing.Size(116, 22);
            this.ddltrans_mode.TabIndex = 46;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 45;
            this.label9.Text = "运输方式：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ddlclosing_way
            // 
            this.ddlclosing_way.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlclosing_way.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlclosing_way.FormattingEnabled = true;
            this.ddlclosing_way.Location = new System.Drawing.Point(320, 52);
            this.ddlclosing_way.Name = "ddlclosing_way";
            this.ddlclosing_way.Size = new System.Drawing.Size(116, 22);
            this.ddlclosing_way.TabIndex = 44;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(252, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 43;
            this.label12.Text = "结算方式：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ddlCompany
            // 
            this.ddlCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCompany.FormattingEnabled = true;
            this.ddlCompany.Location = new System.Drawing.Point(92, 87);
            this.ddlCompany.Name = "ddlCompany";
            this.ddlCompany.Size = new System.Drawing.Size(116, 22);
            this.ddlCompany.TabIndex = 40;
            this.ddlCompany.SelectedIndexChanged += new System.EventHandler(this.ddlCompany_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 39;
            this.label1.Text = "公司：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtorder_num
            // 
            this.txtorder_num.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtorder_num.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtorder_num.BackColor = System.Drawing.Color.Transparent;
            this.txtorder_num.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtorder_num.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtorder_num.ForeImage = null;
            this.txtorder_num.Location = new System.Drawing.Point(320, 18);
            this.txtorder_num.MaxLengh = 32767;
            this.txtorder_num.Multiline = false;
            this.txtorder_num.Name = "txtorder_num";
            this.txtorder_num.Radius = 3;
            this.txtorder_num.ReadOnly = false;
            this.txtorder_num.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtorder_num.ShowError = false;
            this.txtorder_num.Size = new System.Drawing.Size(116, 23);
            this.txtorder_num.TabIndex = 38;
            this.txtorder_num.UseSystemPasswordChar = false;
            this.txtorder_num.Value = "";
            this.txtorder_num.VerifyCondition = null;
            this.txtorder_num.VerifyType = null;
            this.txtorder_num.WaterMark = null;
            this.txtorder_num.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "客户名称：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(252, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 35;
            this.label5.Text = "采购单号：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ddlhandle
            // 
            this.ddlhandle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlhandle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlhandle.FormattingEnabled = true;
            this.ddlhandle.Location = new System.Drawing.Point(535, 87);
            this.ddlhandle.Name = "ddlhandle";
            this.ddlhandle.Size = new System.Drawing.Size(116, 22);
            this.ddlhandle.TabIndex = 1;
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Location = new System.Drawing.Point(535, 20);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.ShowFormat = "yyyy年MM月dd日";
            this.dateTimeStart.Size = new System.Drawing.Size(116, 21);
            this.dateTimeStart.TabIndex = 32;
            this.dateTimeStart.Value = new System.DateTime(2014, 9, 15, 20, 17, 9, 878);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(825, 117);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(60, 26);
            this.btnSearch.TabIndex = 31;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ddlDepartment
            // 
            this.ddlDepartment.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDepartment.FormattingEnabled = true;
            this.ddlDepartment.Location = new System.Drawing.Point(320, 87);
            this.ddlDepartment.Name = "ddlDepartment";
            this.ddlDepartment.Size = new System.Drawing.Size(116, 22);
            this.ddlDepartment.TabIndex = 16;
            this.ddlDepartment.SelectedIndexChanged += new System.EventHandler(this.ddlDepartment_SelectedIndexChanged);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(825, 83);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 30;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ddlState
            // 
            this.ddlState.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlState.FormattingEnabled = true;
            this.ddlState.Location = new System.Drawing.Point(537, 54);
            this.ddlState.Name = "ddlState";
            this.ddlState.Size = new System.Drawing.Size(116, 22);
            this.ddlState.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(471, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "单据状态：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Location = new System.Drawing.Point(690, 20);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.ShowFormat = "yyyy年MM月dd日";
            this.dateTimeEnd.Size = new System.Drawing.Size(116, 23);
            this.dateTimeEnd.TabIndex = 29;
            this.dateTimeEnd.Value = new System.DateTime(2014, 9, 15, 20, 17, 9, 878);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(483, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "经办人：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemark
            // 
            this.txtRemark.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtRemark.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtRemark.BackColor = System.Drawing.Color.Transparent;
            this.txtRemark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtRemark.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtRemark.ForeImage = null;
            this.txtRemark.Location = new System.Drawing.Point(92, 121);
            this.txtRemark.MaxLengh = 32767;
            this.txtRemark.Multiline = false;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Radius = 3;
            this.txtRemark.ReadOnly = false;
            this.txtRemark.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtRemark.ShowError = false;
            this.txtRemark.Size = new System.Drawing.Size(723, 23);
            this.txtRemark.TabIndex = 25;
            this.txtRemark.UseSystemPasswordChar = false;
            this.txtRemark.Value = "";
            this.txtRemark.VerifyCondition = null;
            this.txtRemark.VerifyType = null;
            this.txtRemark.WaterMark = null;
            this.txtRemark.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "备注：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(471, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "单据日期：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(662, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "至";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(273, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "部门：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gvPurchaseOrderList
            // 
            this.gvPurchaseOrderList.AllowUserToAddRows = false;
            this.gvPurchaseOrderList.AllowUserToDeleteRows = false;
            this.gvPurchaseOrderList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gvPurchaseOrderList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvPurchaseOrderList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvPurchaseOrderList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvPurchaseOrderList.BackgroundColor = System.Drawing.Color.White;
            this.gvPurchaseOrderList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPurchaseOrderList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvPurchaseOrderList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPurchaseOrderList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sale_order_id,
            this.colCheck,
            this.order_num,
            this.order_date,
            this.sup_code,
            this.sup_name,
            this.closing_unit,
            this.closing_way_name,
            this.order_quantity,
            this.payment,
            this.tax,
            this.money,
            this.advance_money,
            this.trans_way_name,
            this.contacts_tel,
            this.ordered_by,
            this.contract_no,
            this.delivery_time,
            this.delivery_add,
            this.order_status,
            this.order_status_name,
            this.is_occupy,
            this.operator_name});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPurchaseOrderList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvPurchaseOrderList.EnableHeadersVisualStyles = false;
            this.gvPurchaseOrderList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.gvPurchaseOrderList.Location = new System.Drawing.Point(3, 202);
            this.gvPurchaseOrderList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gvPurchaseOrderList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gvPurchaseOrderList.MergeColumnNames")));
            this.gvPurchaseOrderList.MultiSelect = false;
            this.gvPurchaseOrderList.Name = "gvPurchaseOrderList";
            this.gvPurchaseOrderList.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPurchaseOrderList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvPurchaseOrderList.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.gvPurchaseOrderList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvPurchaseOrderList.RowTemplate.Height = 23;
            this.gvPurchaseOrderList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPurchaseOrderList.ShowCheckBox = true;
            this.gvPurchaseOrderList.Size = new System.Drawing.Size(931, 232);
            this.gvPurchaseOrderList.TabIndex = 37;
            this.gvPurchaseOrderList.HeadCheckChanged += new ServiceStationClient.ComponentUI.DataGridViewEx.DelegateOnClick(this.gvPurchaseOrderList_HeadCheckChanged);
            this.gvPurchaseOrderList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPurchaseOrderList_CellContentClick);
            this.gvPurchaseOrderList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPurchaseOrderList_CellDoubleClick);
            this.gvPurchaseOrderList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvPurchaseOrderList_CellFormatting);
            this.gvPurchaseOrderList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvPurchaseOrderList_CellMouseClick);
            // 
            // sale_order_id
            // 
            this.sale_order_id.DataPropertyName = "sale_order_id";
            this.sale_order_id.HeaderText = "sale_order_id";
            this.sale_order_id.Name = "sale_order_id";
            this.sale_order_id.ReadOnly = true;
            this.sale_order_id.Visible = false;
            this.sale_order_id.Width = 93;
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 5;
            // 
            // order_num
            // 
            this.order_num.DataPropertyName = "order_num";
            this.order_num.HeaderText = "单据编号";
            this.order_num.Name = "order_num";
            this.order_num.ReadOnly = true;
            this.order_num.Width = 81;
            // 
            // order_date
            // 
            this.order_date.DataPropertyName = "order_date";
            this.order_date.HeaderText = "单据日期";
            this.order_date.Name = "order_date";
            this.order_date.ReadOnly = true;
            this.order_date.Width = 81;
            // 
            // sup_code
            // 
            this.sup_code.DataPropertyName = "cust_code";
            this.sup_code.HeaderText = "客户编码";
            this.sup_code.Name = "sup_code";
            this.sup_code.ReadOnly = true;
            this.sup_code.Width = 81;
            // 
            // sup_name
            // 
            this.sup_name.DataPropertyName = "cust_name";
            this.sup_name.HeaderText = "客户名称";
            this.sup_name.Name = "sup_name";
            this.sup_name.ReadOnly = true;
            this.sup_name.Width = 81;
            // 
            // closing_unit
            // 
            this.closing_unit.DataPropertyName = "closing_unit";
            this.closing_unit.HeaderText = "结算单位";
            this.closing_unit.Name = "closing_unit";
            this.closing_unit.ReadOnly = true;
            this.closing_unit.Width = 81;
            // 
            // closing_way_name
            // 
            this.closing_way_name.DataPropertyName = "closing_way_name";
            this.closing_way_name.HeaderText = "结算方式";
            this.closing_way_name.Name = "closing_way_name";
            this.closing_way_name.ReadOnly = true;
            this.closing_way_name.Width = 81;
            // 
            // order_quantity
            // 
            this.order_quantity.DataPropertyName = "order_quantity";
            this.order_quantity.HeaderText = "订货数量";
            this.order_quantity.Name = "order_quantity";
            this.order_quantity.ReadOnly = true;
            this.order_quantity.Width = 81;
            // 
            // payment
            // 
            this.payment.DataPropertyName = "payment";
            this.payment.HeaderText = "货款";
            this.payment.Name = "payment";
            this.payment.ReadOnly = true;
            this.payment.Width = 57;
            // 
            // tax
            // 
            this.tax.DataPropertyName = "tax";
            this.tax.HeaderText = "税款";
            this.tax.Name = "tax";
            this.tax.ReadOnly = true;
            this.tax.Width = 57;
            // 
            // money
            // 
            this.money.DataPropertyName = "money";
            this.money.HeaderText = "金额";
            this.money.Name = "money";
            this.money.ReadOnly = true;
            this.money.Width = 57;
            // 
            // advance_money
            // 
            this.advance_money.DataPropertyName = "advance_money";
            this.advance_money.HeaderText = "预收金额";
            this.advance_money.Name = "advance_money";
            this.advance_money.ReadOnly = true;
            this.advance_money.Width = 81;
            // 
            // trans_way_name
            // 
            this.trans_way_name.DataPropertyName = "trans_way_name";
            this.trans_way_name.HeaderText = "运输方式";
            this.trans_way_name.Name = "trans_way_name";
            this.trans_way_name.ReadOnly = true;
            this.trans_way_name.Width = 81;
            // 
            // contacts_tel
            // 
            this.contacts_tel.DataPropertyName = "contacts_tel";
            this.contacts_tel.HeaderText = "联系电话";
            this.contacts_tel.Name = "contacts_tel";
            this.contacts_tel.ReadOnly = true;
            this.contacts_tel.Width = 81;
            // 
            // ordered_by
            // 
            this.ordered_by.DataPropertyName = "ordered_by";
            this.ordered_by.HeaderText = "订货人";
            this.ordered_by.Name = "ordered_by";
            this.ordered_by.ReadOnly = true;
            this.ordered_by.Width = 69;
            // 
            // contract_no
            // 
            this.contract_no.DataPropertyName = "contract_no";
            this.contract_no.HeaderText = "合同号";
            this.contract_no.Name = "contract_no";
            this.contract_no.ReadOnly = true;
            this.contract_no.Width = 69;
            // 
            // delivery_time
            // 
            this.delivery_time.DataPropertyName = "delivery_time";
            this.delivery_time.HeaderText = "发货时间";
            this.delivery_time.Name = "delivery_time";
            this.delivery_time.ReadOnly = true;
            this.delivery_time.Width = 81;
            // 
            // delivery_add
            // 
            this.delivery_add.DataPropertyName = "delivery_add";
            this.delivery_add.HeaderText = "发货地点";
            this.delivery_add.Name = "delivery_add";
            this.delivery_add.ReadOnly = true;
            this.delivery_add.Width = 81;
            // 
            // order_status
            // 
            this.order_status.DataPropertyName = "order_status";
            this.order_status.HeaderText = "单据状态ID";
            this.order_status.Name = "order_status";
            this.order_status.ReadOnly = true;
            this.order_status.Visible = false;
            this.order_status.Width = 95;
            // 
            // order_status_name
            // 
            this.order_status_name.DataPropertyName = "order_status_name";
            this.order_status_name.HeaderText = "单据状态";
            this.order_status_name.Name = "order_status_name";
            this.order_status_name.ReadOnly = true;
            this.order_status_name.Width = 81;
            // 
            // is_occupy
            // 
            this.is_occupy.DataPropertyName = "is_occupy";
            this.is_occupy.HeaderText = "导入状态";
            this.is_occupy.Name = "is_occupy";
            this.is_occupy.ReadOnly = true;
            this.is_occupy.Visible = false;
            this.is_occupy.Width = 81;
            // 
            // operator_name
            // 
            this.operator_name.DataPropertyName = "operator_name";
            this.operator_name.HeaderText = "审核人";
            this.operator_name.Name = "operator_name";
            this.operator_name.ReadOnly = true;
            this.operator_name.Width = 69;
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
            this.panelEx2.Location = new System.Drawing.Point(6, 440);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(934, 35);
            this.panelEx2.TabIndex = 38;
            // 
            // winFormPager1
            // 
            this.winFormPager1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.winFormPager1.BackColor = System.Drawing.Color.Transparent;
            this.winFormPager1.BtnTextNext = "下页";
            this.winFormPager1.BtnTextPrevious = "上页";
            this.winFormPager1.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.winFormPager1.Location = new System.Drawing.Point(503, 1);
            this.winFormPager1.Name = "winFormPager1";
            this.winFormPager1.PageCount = 0;
            this.winFormPager1.PageSize = 15;
            this.winFormPager1.RecordCount = 0;
            this.winFormPager1.Size = new System.Drawing.Size(427, 32);
            this.winFormPager1.TabIndex = 5;
            this.winFormPager1.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.winFormPager1.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.winFormPager1_PageIndexChanged);
            // 
            // UCSaleOrderManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.gvPurchaseOrderList);
            this.Controls.Add(this.panelEx1);
            this.Name = "UCSaleOrderManager";
            this.Size = new System.Drawing.Size(937, 478);
            this.Load += new System.EventHandler(this.UCSaleOrderManager_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.gvPurchaseOrderList, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchaseOrderList)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtcus_name;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddltrans_mode;
        private System.Windows.Forms.Label label9;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlclosing_way;
        private System.Windows.Forms.Label label12;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlCompany;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TextBoxEx txtorder_num;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlhandle;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dateTimeStart;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlDepartment;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlState;
        private System.Windows.Forms.Label label6;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dateTimeEnd;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.TextBoxEx txtRemark;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private ServiceStationClient.ComponentUI.DataGridViewEx gvPurchaseOrderList;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager winFormPager1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sale_order_id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn sup_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn sup_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn closing_unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn closing_way_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn payment;
        private System.Windows.Forms.DataGridViewTextBoxColumn tax;
        private System.Windows.Forms.DataGridViewTextBoxColumn money;
        private System.Windows.Forms.DataGridViewTextBoxColumn advance_money;
        private System.Windows.Forms.DataGridViewTextBoxColumn trans_way_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn contacts_tel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordered_by;
        private System.Windows.Forms.DataGridViewTextBoxColumn contract_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn delivery_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn delivery_add;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_status_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_occupy;
        private System.Windows.Forms.DataGridViewTextBoxColumn operator_name;
    }
}
