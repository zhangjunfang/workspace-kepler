namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseBilling
{
    partial class UCPurchaseBillManang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPurchaseBillManang));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new HXC.UI.Library.Controls.ExtUserControl();
            this.txtsup_code = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.ddlreceipt_type = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.txtsup_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label11 = new System.Windows.Forms.Label();
            this.ddlorder_type = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.ddltrans_way = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label9 = new System.Windows.Forms.Label();
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
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panelEx2 = new HXC.UI.Library.Controls.ExtUserControl();
            this.winFormPager1 = new ServiceStationClient.ComponentUI.WinFormPager();
            this.gvPurchaseOrderList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.purchase_billing_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_type_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sup_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sup_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trans_way_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receipt_type_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receipt_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AllMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.this_payment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whythe_discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payment_limit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.check_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delivery_address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.org_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.handle_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operator_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_status_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_occupy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extUserControl1 = new HXC.UI.Library.Controls.ExtUserControl();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchaseOrderList)).BeginInit();
            this.extUserControl1.SuspendLayout();
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
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Content = null;
            this.panelEx1.ContentTypeName = null;
            this.panelEx1.ContentTypeParameter = null;
            this.panelEx1.Controls.Add(this.txtsup_code);
            this.panelEx1.Controls.Add(this.ddlreceipt_type);
            this.panelEx1.Controls.Add(this.label13);
            this.panelEx1.Controls.Add(this.txtsup_name);
            this.panelEx1.Controls.Add(this.label11);
            this.panelEx1.Controls.Add(this.ddlorder_type);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.ddltrans_way);
            this.panelEx1.Controls.Add(this.label9);
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
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.label8);
            this.panelEx1.Controls.Add(this.label10);
            this.panelEx1.CornerRadiu = 5;
            this.panelEx1.DisplayValue = "";
            this.panelEx1.InputtingVerifyCondition = null;
            this.panelEx1.Location = new System.Drawing.Point(4, 28);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.ShowError = false;
            this.panelEx1.Size = new System.Drawing.Size(928, 138);
            this.panelEx1.TabIndex = 38;
            this.panelEx1.Value = null;
            this.panelEx1.VerifyCondition = null;
            this.panelEx1.VerifyType = null;
            this.panelEx1.VerifyTypeName = null;
            // 
            // txtsup_code
            // 
            this.txtsup_code.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtsup_code.Location = new System.Drawing.Point(92, 46);
            this.txtsup_code.Name = "txtsup_code";
            this.txtsup_code.ReadOnly = false;
            this.txtsup_code.Size = new System.Drawing.Size(116, 24);
            this.txtsup_code.TabIndex = 129;
            this.txtsup_code.ToolTipTitle = "";
            this.txtsup_code.ChooserClick += new System.EventHandler(this.txtsup_code_ChooserClick);
            // 
            // ddlreceipt_type
            // 
            this.ddlreceipt_type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlreceipt_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlreceipt_type.FormattingEnabled = true;
            this.ddlreceipt_type.Location = new System.Drawing.Point(320, 77);
            this.ddlreceipt_type.Name = "ddlreceipt_type";
            this.ddlreceipt_type.Size = new System.Drawing.Size(116, 22);
            this.ddlreceipt_type.TabIndex = 128;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(249, 80);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 127;
            this.label13.Text = "发票类型：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtsup_name
            // 
            this.txtsup_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtsup_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtsup_name.BackColor = System.Drawing.Color.Transparent;
            this.txtsup_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtsup_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtsup_name.ForeImage = null;
            this.txtsup_name.InputtingVerifyCondition = null;
            this.txtsup_name.Location = new System.Drawing.Point(320, 46);
            this.txtsup_name.MaxLengh = 32767;
            this.txtsup_name.Multiline = false;
            this.txtsup_name.Name = "txtsup_name";
            this.txtsup_name.Radius = 3;
            this.txtsup_name.ReadOnly = false;
            this.txtsup_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtsup_name.ShowError = false;
            this.txtsup_name.Size = new System.Drawing.Size(116, 23);
            this.txtsup_name.TabIndex = 126;
            this.txtsup_name.UseSystemPasswordChar = false;
            this.txtsup_name.Value = "";
            this.txtsup_name.VerifyCondition = null;
            this.txtsup_name.VerifyType = null;
            this.txtsup_name.VerifyTypeName = null;
            this.txtsup_name.WaterMark = null;
            this.txtsup_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(240, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 125;
            this.label11.Text = "供应商名称：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ddlorder_type
            // 
            this.ddlorder_type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlorder_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlorder_type.FormattingEnabled = true;
            this.ddlorder_type.Location = new System.Drawing.Point(92, 18);
            this.ddlorder_type.Name = "ddlorder_type";
            this.ddlorder_type.Size = new System.Drawing.Size(116, 22);
            this.ddlorder_type.TabIndex = 124;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 123;
            this.label2.Text = "单据类型：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ddltrans_way
            // 
            this.ddltrans_way.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddltrans_way.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddltrans_way.FormattingEnabled = true;
            this.ddltrans_way.Location = new System.Drawing.Point(92, 75);
            this.ddltrans_way.Name = "ddltrans_way";
            this.ddltrans_way.Size = new System.Drawing.Size(116, 22);
            this.ddltrans_way.TabIndex = 46;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 45;
            this.label9.Text = "运输方式：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ddlCompany
            // 
            this.ddlCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCompany.FormattingEnabled = true;
            this.ddlCompany.Location = new System.Drawing.Point(92, 105);
            this.ddlCompany.Name = "ddlCompany";
            this.ddlCompany.Size = new System.Drawing.Size(116, 22);
            this.ddlCompany.TabIndex = 40;
            this.ddlCompany.SelectedIndexChanged += new System.EventHandler(this.ddlCompany_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 111);
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
            this.txtorder_num.InputtingVerifyCondition = null;
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
            this.txtorder_num.VerifyTypeName = null;
            this.txtorder_num.WaterMark = null;
            this.txtorder_num.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "供应商编码：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(240, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 35;
            this.label5.Text = "采购开单号：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ddlhandle
            // 
            this.ddlhandle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlhandle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlhandle.FormattingEnabled = true;
            this.ddlhandle.Location = new System.Drawing.Point(537, 105);
            this.ddlhandle.Name = "ddlhandle";
            this.ddlhandle.Size = new System.Drawing.Size(116, 22);
            this.ddlhandle.TabIndex = 1;
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Location = new System.Drawing.Point(535, 46);
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
            this.btnSearch.Location = new System.Drawing.Point(844, 97);
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
            this.ddlDepartment.Location = new System.Drawing.Point(320, 105);
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
            this.btnClear.Location = new System.Drawing.Point(844, 63);
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
            this.ddlState.Location = new System.Drawing.Point(537, 77);
            this.ddlState.Name = "ddlState";
            this.ddlState.Size = new System.Drawing.Size(116, 22);
            this.ddlState.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(471, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "单据状态：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Location = new System.Drawing.Point(690, 46);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.ShowFormat = "yyyy年MM月dd日";
            this.dateTimeEnd.Size = new System.Drawing.Size(116, 23);
            this.dateTimeEnd.TabIndex = 29;
            this.dateTimeEnd.Value = new System.DateTime(2014, 9, 15, 20, 17, 9, 878);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(483, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "经办人：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(471, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "单据日期：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(662, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "至";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(273, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "部门：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelEx2
            // 
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Content = null;
            this.panelEx2.ContentTypeName = null;
            this.panelEx2.ContentTypeParameter = null;
            this.panelEx2.Controls.Add(this.winFormPager1);
            this.panelEx2.CornerRadiu = 5;
            this.panelEx2.DisplayValue = "";
            this.panelEx2.InputtingVerifyCondition = null;
            this.panelEx2.Location = new System.Drawing.Point(4, 449);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.ShowError = false;
            this.panelEx2.Size = new System.Drawing.Size(928, 35);
            this.panelEx2.TabIndex = 40;
            this.panelEx2.Value = null;
            this.panelEx2.VerifyCondition = null;
            this.panelEx2.VerifyType = null;
            this.panelEx2.VerifyTypeName = null;
            // 
            // winFormPager1
            // 
            this.winFormPager1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.winFormPager1.BackColor = System.Drawing.Color.Transparent;
            this.winFormPager1.BtnTextNext = "下页";
            this.winFormPager1.BtnTextPrevious = "上页";
            this.winFormPager1.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.winFormPager1.Location = new System.Drawing.Point(497, 1);
            this.winFormPager1.Name = "winFormPager1";
            this.winFormPager1.PageCount = 0;
            this.winFormPager1.PageSize = 15;
            this.winFormPager1.RecordCount = 0;
            this.winFormPager1.Size = new System.Drawing.Size(427, 32);
            this.winFormPager1.TabIndex = 5;
            this.winFormPager1.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.winFormPager1.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.winFormPager1_PageIndexChanged);
            // 
            // gvPurchaseOrderList
            // 
            this.gvPurchaseOrderList.AllowUserToAddRows = false;
            this.gvPurchaseOrderList.AllowUserToDeleteRows = false;
            this.gvPurchaseOrderList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gvPurchaseOrderList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvPurchaseOrderList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvPurchaseOrderList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvPurchaseOrderList.BackgroundColor = System.Drawing.Color.White;
            this.gvPurchaseOrderList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPurchaseOrderList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvPurchaseOrderList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPurchaseOrderList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.purchase_billing_id,
            this.order_type_name,
            this.order_num,
            this.order_date,
            this.sup_code,
            this.sup_name,
            this.trans_way_name,
            this.receipt_type_name,
            this.receipt_no,
            this.AllMoney,
            this.this_payment,
            this.whythe_discount,
            this.payment_limit,
            this.check_number,
            this.delivery_address,
            this.org_name,
            this.handle_name,
            this.operator_name,
            this.remark,
            this.order_status,
            this.order_status_name,
            this.is_occupy});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPurchaseOrderList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvPurchaseOrderList.EnableHeadersVisualStyles = false;
            this.gvPurchaseOrderList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.gvPurchaseOrderList.IsCheck = true;
            this.gvPurchaseOrderList.Location = new System.Drawing.Point(3, 3);
            this.gvPurchaseOrderList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gvPurchaseOrderList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gvPurchaseOrderList.MergeColumnNames")));
            this.gvPurchaseOrderList.MultiSelect = false;
            this.gvPurchaseOrderList.Name = "gvPurchaseOrderList";
            this.gvPurchaseOrderList.ReadOnly = true;
            this.gvPurchaseOrderList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.gvPurchaseOrderList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvPurchaseOrderList.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.gvPurchaseOrderList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvPurchaseOrderList.RowTemplate.Height = 23;
            this.gvPurchaseOrderList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPurchaseOrderList.ShowCheckBox = true;
            this.gvPurchaseOrderList.Size = new System.Drawing.Size(921, 263);
            this.gvPurchaseOrderList.TabIndex = 39;
            this.gvPurchaseOrderList.HeadCheckChanged += new ServiceStationClient.ComponentUI.DataGridViewEx.DelegateOnClick(this.gvPurchaseOrderList_HeadCheckChanged);
            this.gvPurchaseOrderList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPurchaseOrderList_CellContentClick);
            this.gvPurchaseOrderList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPurchaseOrderList_CellDoubleClick);
            this.gvPurchaseOrderList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvPurchaseOrderList_CellFormatting);
            this.gvPurchaseOrderList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvPurchaseOrderList_CellMouseClick);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 30;
            // 
            // purchase_billing_id
            // 
            this.purchase_billing_id.DataPropertyName = "purchase_billing_id";
            this.purchase_billing_id.HeaderText = "purchase_billing_id";
            this.purchase_billing_id.Name = "purchase_billing_id";
            this.purchase_billing_id.ReadOnly = true;
            this.purchase_billing_id.Visible = false;
            this.purchase_billing_id.Width = 131;
            // 
            // order_type_name
            // 
            this.order_type_name.DataPropertyName = "order_type_name";
            this.order_type_name.HeaderText = "单据类型";
            this.order_type_name.Name = "order_type_name";
            this.order_type_name.ReadOnly = true;
            this.order_type_name.Width = 81;
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
            this.sup_code.DataPropertyName = "sup_code";
            this.sup_code.HeaderText = "供应商编码";
            this.sup_code.Name = "sup_code";
            this.sup_code.ReadOnly = true;
            this.sup_code.Width = 93;
            // 
            // sup_name
            // 
            this.sup_name.DataPropertyName = "sup_name";
            this.sup_name.HeaderText = "供应商";
            this.sup_name.Name = "sup_name";
            this.sup_name.ReadOnly = true;
            this.sup_name.Width = 69;
            // 
            // trans_way_name
            // 
            this.trans_way_name.DataPropertyName = "trans_way_name";
            this.trans_way_name.HeaderText = "运输方式";
            this.trans_way_name.Name = "trans_way_name";
            this.trans_way_name.ReadOnly = true;
            this.trans_way_name.Width = 81;
            // 
            // receipt_type_name
            // 
            this.receipt_type_name.DataPropertyName = "receipt_type_name";
            this.receipt_type_name.HeaderText = "发票类型";
            this.receipt_type_name.Name = "receipt_type_name";
            this.receipt_type_name.ReadOnly = true;
            this.receipt_type_name.Width = 81;
            // 
            // receipt_no
            // 
            this.receipt_no.DataPropertyName = "receipt_no";
            this.receipt_no.HeaderText = "发票号";
            this.receipt_no.Name = "receipt_no";
            this.receipt_no.ReadOnly = true;
            this.receipt_no.Width = 69;
            // 
            // AllMoney
            // 
            this.AllMoney.DataPropertyName = "allmoney";
            this.AllMoney.HeaderText = "总金额";
            this.AllMoney.Name = "AllMoney";
            this.AllMoney.ReadOnly = true;
            this.AllMoney.Width = 69;
            // 
            // this_payment
            // 
            this.this_payment.DataPropertyName = "this_payment";
            this.this_payment.HeaderText = "本次现付";
            this.this_payment.Name = "this_payment";
            this.this_payment.ReadOnly = true;
            this.this_payment.Width = 81;
            // 
            // whythe_discount
            // 
            this.whythe_discount.DataPropertyName = "whythe_discount";
            this.whythe_discount.HeaderText = "整单折扣";
            this.whythe_discount.Name = "whythe_discount";
            this.whythe_discount.ReadOnly = true;
            this.whythe_discount.Width = 81;
            // 
            // payment_limit
            // 
            this.payment_limit.DataPropertyName = "payment_limit";
            this.payment_limit.HeaderText = "付款期限";
            this.payment_limit.Name = "payment_limit";
            this.payment_limit.ReadOnly = true;
            this.payment_limit.Visible = false;
            this.payment_limit.Width = 81;
            // 
            // check_number
            // 
            this.check_number.DataPropertyName = "check_number";
            this.check_number.HeaderText = "支票号";
            this.check_number.Name = "check_number";
            this.check_number.ReadOnly = true;
            this.check_number.Width = 69;
            // 
            // delivery_address
            // 
            this.delivery_address.DataPropertyName = "delivery_address";
            this.delivery_address.HeaderText = "到货地点";
            this.delivery_address.Name = "delivery_address";
            this.delivery_address.ReadOnly = true;
            this.delivery_address.Visible = false;
            this.delivery_address.Width = 81;
            // 
            // org_name
            // 
            this.org_name.DataPropertyName = "org_name";
            this.org_name.HeaderText = "部门";
            this.org_name.Name = "org_name";
            this.org_name.ReadOnly = true;
            this.org_name.Width = 57;
            // 
            // handle_name
            // 
            this.handle_name.DataPropertyName = "handle_name";
            this.handle_name.HeaderText = "经办人";
            this.handle_name.Name = "handle_name";
            this.handle_name.ReadOnly = true;
            this.handle_name.Width = 69;
            // 
            // operator_name
            // 
            this.operator_name.DataPropertyName = "operator_name";
            this.operator_name.HeaderText = "操作人";
            this.operator_name.Name = "operator_name";
            this.operator_name.ReadOnly = true;
            this.operator_name.Width = 69;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            this.remark.Width = 57;
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
            this.is_occupy.ToolTipText = "is_occupy";
            this.is_occupy.Visible = false;
            this.is_occupy.Width = 81;
            // 
            // extUserControl1
            // 
            this.extUserControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.extUserControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.extUserControl1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extUserControl1.BorderWidth = 1;
            this.extUserControl1.Content = null;
            this.extUserControl1.ContentTypeName = null;
            this.extUserControl1.ContentTypeParameter = null;
            this.extUserControl1.Controls.Add(this.gvPurchaseOrderList);
            this.extUserControl1.CornerRadiu = 5;
            this.extUserControl1.DisplayValue = "";
            this.extUserControl1.InputtingVerifyCondition = null;
            this.extUserControl1.Location = new System.Drawing.Point(4, 172);
            this.extUserControl1.Name = "extUserControl1";
            this.extUserControl1.ShowError = false;
            this.extUserControl1.Size = new System.Drawing.Size(928, 271);
            this.extUserControl1.TabIndex = 41;
            this.extUserControl1.Value = null;
            this.extUserControl1.VerifyCondition = null;
            this.extUserControl1.VerifyType = null;
            this.extUserControl1.VerifyTypeName = null;
            // 
            // UCPurchaseBillManang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.extUserControl1);
            this.Name = "UCPurchaseBillManang";
            this.Size = new System.Drawing.Size(937, 494);
            this.Load += new System.EventHandler(this.UCPurchaseBillManang_Load);
            this.Controls.SetChildIndex(this.extUserControl1, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchaseOrderList)).EndInit();
            this.extUserControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private  HXC.UI.Library.Controls.ExtUserControl panelEx1;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddltrans_way;
        private System.Windows.Forms.Label label9;
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private  HXC.UI.Library.Controls.ExtUserControl panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager winFormPager1;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlorder_type;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.DataGridViewEx gvPurchaseOrderList;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlreceipt_type;
        private System.Windows.Forms.Label label13;
        private ServiceStationClient.ComponentUI.TextBoxEx txtsup_name;
        private System.Windows.Forms.Label label11;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtsup_code;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn purchase_billing_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_type_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn sup_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn sup_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn trans_way_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn receipt_type_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn receipt_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn AllMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn this_payment;
        private System.Windows.Forms.DataGridViewTextBoxColumn whythe_discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn payment_limit;
        private System.Windows.Forms.DataGridViewTextBoxColumn check_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn delivery_address;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn handle_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn operator_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_status_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_occupy;
        private HXC.UI.Library.Controls.ExtUserControl extUserControl1;
    }
}
