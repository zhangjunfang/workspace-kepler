namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseOrder
{
    partial class UCPurchaseOrderManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPurchaseOrderManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx2 = new HXC.UI.Library.Controls.ExtUserControl();
            this.winFormPager1 = new ServiceStationClient.ComponentUI.WinFormPager();
            this.gvPurchaseOrderList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.order_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.prepaid_money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trans_mode_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contacts_tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordered_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contract_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arrival_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_status_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_occupy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arrival_place = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.org_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.handle_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operator_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suspend_reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1 = new HXC.UI.Library.Controls.ExtUserControl();
            this.txtsup_name = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
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
            this.extUserControl1 = new HXC.UI.Library.Controls.ExtUserControl();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchaseOrderList)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.extUserControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(937, 25);
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
            this.panelEx2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx2.InputtingVerifyCondition = null;
            this.panelEx2.Location = new System.Drawing.Point(3, 451);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.ShowError = false;
            this.panelEx2.Size = new System.Drawing.Size(934, 35);
            this.panelEx2.TabIndex = 37;
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
            this.winFormPager1.Location = new System.Drawing.Point(503, 1);
            this.winFormPager1.Name = "winFormPager1";
            this.winFormPager1.PageCount = 0;
            this.winFormPager1.PageSize = 15;
            this.winFormPager1.RecordCount = 0;
            this.winFormPager1.Size = new System.Drawing.Size(427, 32);
            this.winFormPager1.TabIndex = 5;
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPurchaseOrderList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvPurchaseOrderList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPurchaseOrderList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.order_id,
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
            this.prepaid_money,
            this.trans_mode_name,
            this.contacts_tel,
            this.ordered_by,
            this.contract_no,
            this.arrival_date,
            this.order_status,
            this.order_status_name,
            this.is_occupy,
            this.arrival_place,
            this.org_name,
            this.handle_name,
            this.operator_name,
            this.remark,
            this.suspend_reason});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.gvPurchaseOrderList.Size = new System.Drawing.Size(926, 251);
            this.gvPurchaseOrderList.TabIndex = 36;
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
            // order_id
            // 
            this.order_id.DataPropertyName = "order_id";
            this.order_id.HeaderText = "order_id";
            this.order_id.Name = "order_id";
            this.order_id.ReadOnly = true;
            this.order_id.Visible = false;
            this.order_id.Width = 67;
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
            // prepaid_money
            // 
            this.prepaid_money.DataPropertyName = "prepaid_money";
            this.prepaid_money.HeaderText = "预付金额";
            this.prepaid_money.Name = "prepaid_money";
            this.prepaid_money.ReadOnly = true;
            this.prepaid_money.Width = 81;
            // 
            // trans_mode_name
            // 
            this.trans_mode_name.DataPropertyName = "trans_mode_name";
            this.trans_mode_name.HeaderText = "运输方式";
            this.trans_mode_name.Name = "trans_mode_name";
            this.trans_mode_name.ReadOnly = true;
            this.trans_mode_name.Width = 81;
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
            // arrival_date
            // 
            this.arrival_date.DataPropertyName = "arrival_date";
            this.arrival_date.HeaderText = "到货时间";
            this.arrival_date.Name = "arrival_date";
            this.arrival_date.ReadOnly = true;
            this.arrival_date.Width = 81;
            // 
            // order_status
            // 
            this.order_status.DataPropertyName = "order_status";
            this.order_status.HeaderText = "单据状态ID";
            this.order_status.Name = "order_status";
            this.order_status.ReadOnly = true;
            this.order_status.Visible = false;
            this.order_status.Width = 96;
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
            this.is_occupy.Width = 82;
            // 
            // arrival_place
            // 
            this.arrival_place.DataPropertyName = "arrival_place";
            this.arrival_place.HeaderText = "到货地点";
            this.arrival_place.Name = "arrival_place";
            this.arrival_place.ReadOnly = true;
            this.arrival_place.Width = 81;
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
            // suspend_reason
            // 
            this.suspend_reason.DataPropertyName = "suspend_reason";
            this.suspend_reason.HeaderText = "中止原因";
            this.suspend_reason.Name = "suspend_reason";
            this.suspend_reason.ReadOnly = true;
            this.suspend_reason.Width = 81;
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
            this.panelEx1.Controls.Add(this.txtsup_name);
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
            this.panelEx1.CornerRadiu = 5;
            this.panelEx1.DisplayValue = "";
            this.panelEx1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx1.InputtingVerifyCondition = null;
            this.panelEx1.Location = new System.Drawing.Point(2, 28);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.ShowError = false;
            this.panelEx1.Size = new System.Drawing.Size(933, 159);
            this.panelEx1.TabIndex = 35;
            this.panelEx1.Value = null;
            this.panelEx1.VerifyCondition = null;
            this.panelEx1.VerifyType = null;
            this.panelEx1.VerifyTypeName = null;
            // 
            // txtsup_name
            // 
            this.txtsup_name.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtsup_name.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtsup_name.Location = new System.Drawing.Point(92, 18);
            this.txtsup_name.Name = "txtsup_name";
            this.txtsup_name.ReadOnly = false;
            this.txtsup_name.Size = new System.Drawing.Size(116, 24);
            this.txtsup_name.TabIndex = 122;
            this.txtsup_name.ToolTipTitle = "";
            this.txtsup_name.ChooserClick += new System.EventHandler(this.txtsup_name_ChooserClick);
            // 
            // ddltrans_mode
            // 
            this.ddltrans_mode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddltrans_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddltrans_mode.FormattingEnabled = true;
            this.ddltrans_mode.Location = new System.Drawing.Point(92, 52);
            this.ddltrans_mode.Name = "ddltrans_mode";
            this.ddltrans_mode.Size = new System.Drawing.Size(116, 24);
            this.ddltrans_mode.TabIndex = 46;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 17);
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
            this.ddlclosing_way.Size = new System.Drawing.Size(116, 24);
            this.ddlclosing_way.TabIndex = 44;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(252, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 17);
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
            this.ddlCompany.Size = new System.Drawing.Size(116, 24);
            this.ddlCompany.TabIndex = 40;
            this.ddlCompany.SelectedIndexChanged += new System.EventHandler(this.ddlCompany_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
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
            this.txtorder_num.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.label4.Location = new System.Drawing.Point(9, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 33;
            this.label4.Text = "供应商名称：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(252, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
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
            this.ddlhandle.Size = new System.Drawing.Size(116, 24);
            this.ddlhandle.TabIndex = 1;
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.btnSearch.Location = new System.Drawing.Point(824, 117);
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
            this.ddlDepartment.Size = new System.Drawing.Size(116, 24);
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
            this.btnClear.Location = new System.Drawing.Point(824, 83);
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
            this.ddlState.Size = new System.Drawing.Size(116, 24);
            this.ddlState.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(471, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "单据状态：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.label3.Size = new System.Drawing.Size(56, 17);
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
            this.txtRemark.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRemark.ForeImage = null;
            this.txtRemark.InputtingVerifyCondition = null;
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
            this.txtRemark.VerifyTypeName = null;
            this.txtRemark.WaterMark = null;
            this.txtRemark.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "备注：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(471, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "单据日期：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(662, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 17);
            this.label8.TabIndex = 7;
            this.label8.Text = "至";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(273, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 17);
            this.label10.TabIndex = 9;
            this.label10.Text = "部门：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.extUserControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extUserControl1.InputtingVerifyCondition = null;
            this.extUserControl1.Location = new System.Drawing.Point(3, 190);
            this.extUserControl1.Name = "extUserControl1";
            this.extUserControl1.ShowError = false;
            this.extUserControl1.Size = new System.Drawing.Size(932, 257);
            this.extUserControl1.TabIndex = 38;
            this.extUserControl1.Value = null;
            this.extUserControl1.VerifyCondition = null;
            this.extUserControl1.VerifyType = null;
            this.extUserControl1.VerifyTypeName = null;
            // 
            // UCPurchaseOrderManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.extUserControl1);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Name = "UCPurchaseOrderManager";
            this.Size = new System.Drawing.Size(937, 494);
            this.Load += new System.EventHandler(this.UCPurchaseOrderManager_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.Controls.SetChildIndex(this.extUserControl1, 0);
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchaseOrderList)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.extUserControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private  HXC.UI.Library.Controls.ExtUserControl panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager winFormPager1;
        private ServiceStationClient.ComponentUI.DataGridViewEx gvPurchaseOrderList;
        private  HXC.UI.Library.Controls.ExtUserControl panelEx1;
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
        private ServiceStationClient.ComponentUI.TextBoxEx txtorder_num;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddltrans_mode;
        private System.Windows.Forms.Label label9;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlclosing_way;
        private System.Windows.Forms.Label label12;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlCompany;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtsup_name;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_id;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn prepaid_money;
        private System.Windows.Forms.DataGridViewTextBoxColumn trans_mode_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn contacts_tel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordered_by;
        private System.Windows.Forms.DataGridViewTextBoxColumn contract_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn arrival_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_status_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_occupy;
        private System.Windows.Forms.DataGridViewTextBoxColumn arrival_place;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn handle_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn operator_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn suspend_reason;
        private HXC.UI.Library.Controls.ExtUserControl extUserControl1;
    }
}
