namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.YuTongPurchaseOrder
{
    partial class UCYTAddOrEdit
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCYTAddOrEdit));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblorder_status_name = new System.Windows.Forms.Label();
            this.ddtorder_date = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.lblorder_num = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ddlorder_type = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.lblcrm_bill_id = new System.Windows.Forms.Label();
            this.gvPurchaseList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.cmsPurchaseAccessories = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsPurchaseAccessoriesAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPurchaseAccessoriesEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPurchaseAccessoriesDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.lblupdate_time1 = new System.Windows.Forms.Label();
            this.lblupdate_name = new System.Windows.Forms.Label();
            this.lblcreate_time1 = new System.Windows.Forms.Label();
            this.lblcreate_name = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbloperator_name = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ddlhandle = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.ddlorg_id = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.panelArea = new ServiceStationClient.ComponentUI.PanelEx();
            this.ImportMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ImporPurchasePlan = new System.Windows.Forms.ToolStripMenuItem();
            this.ImporSaleOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.parts_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_factory_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drawing_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit_id = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.application_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conf_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_explain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.replaces = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.center_library_explain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cancel_reasons = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.relation_order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImportType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchaseList)).BeginInit();
            this.cmsPurchaseAccessories.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.ImportMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblorder_status_name
            // 
            this.lblorder_status_name.AutoSize = true;
            this.lblorder_status_name.Location = new System.Drawing.Point(516, 67);
            this.lblorder_status_name.Name = "lblorder_status_name";
            this.lblorder_status_name.Size = new System.Drawing.Size(11, 12);
            this.lblorder_status_name.TabIndex = 175;
            this.lblorder_status_name.Text = ".";
            // 
            // ddtorder_date
            // 
            this.ddtorder_date.Location = new System.Drawing.Point(306, 63);
            this.ddtorder_date.Name = "ddtorder_date";
            this.ddtorder_date.ShowFormat = "yyyy年MM月dd日";
            this.ddtorder_date.Size = new System.Drawing.Size(119, 21);
            this.ddtorder_date.TabIndex = 174;
            this.ddtorder_date.Value = new System.DateTime(2014, 11, 5, 20, 17, 0, 0);
            // 
            // lblorder_num
            // 
            this.lblorder_num.AutoSize = true;
            this.lblorder_num.Location = new System.Drawing.Point(78, 67);
            this.lblorder_num.Name = "lblorder_num";
            this.lblorder_num.Size = new System.Drawing.Size(11, 12);
            this.lblorder_num.TabIndex = 173;
            this.lblorder_num.Text = ".";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(239, 66);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 172;
            this.label10.Text = "单据日期：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 171;
            this.label1.Text = "订单号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(445, 67);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 176;
            this.label11.Text = "单据状态：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(589, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 177;
            this.label2.Text = "订单类型：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(796, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 178;
            this.label3.Text = "宇通单号：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ddlorder_type
            // 
            this.ddlorder_type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlorder_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlorder_type.FormattingEnabled = true;
            this.ddlorder_type.Location = new System.Drawing.Point(657, 63);
            this.ddlorder_type.Name = "ddlorder_type";
            this.ddlorder_type.Size = new System.Drawing.Size(121, 22);
            this.ddlorder_type.TabIndex = 179;
            this.ddlorder_type.SelectedIndexChanged += new System.EventHandler(this.ddlorder_type_SelectedIndexChanged);
            // 
            // lblcrm_bill_id
            // 
            this.lblcrm_bill_id.AutoSize = true;
            this.lblcrm_bill_id.Location = new System.Drawing.Point(867, 67);
            this.lblcrm_bill_id.Name = "lblcrm_bill_id";
            this.lblcrm_bill_id.Size = new System.Drawing.Size(11, 12);
            this.lblcrm_bill_id.TabIndex = 182;
            this.lblcrm_bill_id.Text = ".";
            // 
            // gvPurchaseList
            // 
            this.gvPurchaseList.AllowUserToAddRows = false;
            this.gvPurchaseList.AllowUserToDeleteRows = false;
            this.gvPurchaseList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gvPurchaseList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvPurchaseList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvPurchaseList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvPurchaseList.BackgroundColor = System.Drawing.Color.White;
            this.gvPurchaseList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPurchaseList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvPurchaseList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPurchaseList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.parts_code,
            this.parts_name,
            this.car_factory_code,
            this.drawing_num,
            this.unit_id,
            this.model,
            this.application_count,
            this.price,
            this.money,
            this.conf_count,
            this.parts_explain,
            this.replaces,
            this.center_library_explain,
            this.cancel_reasons,
            this.relation_order,
            this.create_by,
            this.create_name,
            this.create_time,
            this.ImportType});
            this.gvPurchaseList.ContextMenuStrip = this.cmsPurchaseAccessories;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPurchaseList.DefaultCellStyle = dataGridViewCellStyle7;
            this.gvPurchaseList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvPurchaseList.EnableHeadersVisualStyles = false;
            this.gvPurchaseList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.gvPurchaseList.Location = new System.Drawing.Point(18, 280);
            this.gvPurchaseList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gvPurchaseList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gvPurchaseList.MergeColumnNames")));
            this.gvPurchaseList.MultiSelect = false;
            this.gvPurchaseList.Name = "gvPurchaseList";
            this.gvPurchaseList.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPurchaseList.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.gvPurchaseList.RowHeadersVisible = false;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.gvPurchaseList.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.gvPurchaseList.RowTemplate.Height = 23;
            this.gvPurchaseList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPurchaseList.ShowCheckBox = true;
            this.gvPurchaseList.Size = new System.Drawing.Size(994, 153);
            this.gvPurchaseList.TabIndex = 183;
            this.gvPurchaseList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvPurchaseList_CellFormatting);
            this.gvPurchaseList.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPurchaseList_CellMouseEnter);
            this.gvPurchaseList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvPurchaseList_EditingControlShowing);
            // 
            // cmsPurchaseAccessories
            // 
            this.cmsPurchaseAccessories.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsPurchaseAccessoriesAdd,
            this.cmsPurchaseAccessoriesEdit,
            this.cmsPurchaseAccessoriesDelete});
            this.cmsPurchaseAccessories.Name = "cmsPurchasePlan";
            this.cmsPurchaseAccessories.Size = new System.Drawing.Size(101, 70);
            // 
            // cmsPurchaseAccessoriesAdd
            // 
            this.cmsPurchaseAccessoriesAdd.Name = "cmsPurchaseAccessoriesAdd";
            this.cmsPurchaseAccessoriesAdd.Size = new System.Drawing.Size(100, 22);
            this.cmsPurchaseAccessoriesAdd.Text = "新增";
            this.cmsPurchaseAccessoriesAdd.Click += new System.EventHandler(this.cmsPurchaseAccessoriesAdd_Click);
            // 
            // cmsPurchaseAccessoriesEdit
            // 
            this.cmsPurchaseAccessoriesEdit.Name = "cmsPurchaseAccessoriesEdit";
            this.cmsPurchaseAccessoriesEdit.Size = new System.Drawing.Size(100, 22);
            this.cmsPurchaseAccessoriesEdit.Text = "编辑";
            this.cmsPurchaseAccessoriesEdit.Click += new System.EventHandler(this.cmsPurchaseAccessoriesEdit_Click);
            // 
            // cmsPurchaseAccessoriesDelete
            // 
            this.cmsPurchaseAccessoriesDelete.Name = "cmsPurchaseAccessoriesDelete";
            this.cmsPurchaseAccessoriesDelete.Size = new System.Drawing.Size(100, 22);
            this.cmsPurchaseAccessoriesDelete.Text = "删除";
            this.cmsPurchaseAccessoriesDelete.Click += new System.EventHandler(this.cmsPurchaseAccessoriesDelete_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.lblupdate_time1);
            this.panelEx1.Controls.Add(this.lblupdate_name);
            this.panelEx1.Controls.Add(this.lblcreate_time1);
            this.panelEx1.Controls.Add(this.lblcreate_name);
            this.panelEx1.Controls.Add(this.label15);
            this.panelEx1.Controls.Add(this.label14);
            this.panelEx1.Controls.Add(this.label13);
            this.panelEx1.Controls.Add(this.label12);
            this.panelEx1.Controls.Add(this.lbloperator_name);
            this.panelEx1.Controls.Add(this.label8);
            this.panelEx1.Controls.Add(this.ddlhandle);
            this.panelEx1.Controls.Add(this.ddlorg_id);
            this.panelEx1.Controls.Add(this.label27);
            this.panelEx1.Controls.Add(this.label28);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(18, 438);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(994, 100);
            this.panelEx1.TabIndex = 199;
            // 
            // lblupdate_time1
            // 
            this.lblupdate_time1.AutoSize = true;
            this.lblupdate_time1.Location = new System.Drawing.Point(822, 71);
            this.lblupdate_time1.Name = "lblupdate_time1";
            this.lblupdate_time1.Size = new System.Drawing.Size(11, 12);
            this.lblupdate_time1.TabIndex = 119;
            this.lblupdate_time1.Text = ".";
            // 
            // lblupdate_name
            // 
            this.lblupdate_name.AutoSize = true;
            this.lblupdate_name.Location = new System.Drawing.Point(533, 71);
            this.lblupdate_name.Name = "lblupdate_name";
            this.lblupdate_name.Size = new System.Drawing.Size(11, 12);
            this.lblupdate_name.TabIndex = 118;
            this.lblupdate_name.Text = ".";
            // 
            // lblcreate_time1
            // 
            this.lblcreate_time1.AutoSize = true;
            this.lblcreate_time1.Location = new System.Drawing.Point(822, 41);
            this.lblcreate_time1.Name = "lblcreate_time1";
            this.lblcreate_time1.Size = new System.Drawing.Size(11, 12);
            this.lblcreate_time1.TabIndex = 117;
            this.lblcreate_time1.Text = ".";
            // 
            // lblcreate_name
            // 
            this.lblcreate_name.AutoSize = true;
            this.lblcreate_name.Location = new System.Drawing.Point(533, 41);
            this.lblcreate_name.Name = "lblcreate_name";
            this.lblcreate_name.Size = new System.Drawing.Size(11, 12);
            this.lblcreate_name.TabIndex = 116;
            this.lblcreate_name.Text = ".";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(727, 71);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 12);
            this.label15.TabIndex = 115;
            this.label15.Text = "最后编辑时间：";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(450, 71);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 12);
            this.label14.TabIndex = 114;
            this.label14.Text = "最后编辑人：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(751, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 113;
            this.label13.Text = "创建时间：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(474, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 112;
            this.label12.Text = "创建人：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbloperator_name
            // 
            this.lbloperator_name.AutoSize = true;
            this.lbloperator_name.Location = new System.Drawing.Point(822, 13);
            this.lbloperator_name.Name = "lbloperator_name";
            this.lbloperator_name.Size = new System.Drawing.Size(11, 12);
            this.lbloperator_name.TabIndex = 111;
            this.lbloperator_name.Text = ".";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(763, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 110;
            this.label8.Text = "操作人：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ddlhandle
            // 
            this.ddlhandle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlhandle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlhandle.FormattingEnabled = true;
            this.ddlhandle.Location = new System.Drawing.Point(530, 7);
            this.ddlhandle.Name = "ddlhandle";
            this.ddlhandle.Size = new System.Drawing.Size(116, 22);
            this.ddlhandle.TabIndex = 17;
            // 
            // ddlorg_id
            // 
            this.ddlorg_id.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlorg_id.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlorg_id.FormattingEnabled = true;
            this.ddlorg_id.Location = new System.Drawing.Point(283, 7);
            this.ddlorg_id.Name = "ddlorg_id";
            this.ddlorg_id.Size = new System.Drawing.Size(116, 22);
            this.ddlorg_id.TabIndex = 20;
            this.ddlorg_id.SelectedIndexChanged += new System.EventHandler(this.ddlorg_id_SelectedIndexChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(474, 13);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(53, 12);
            this.label27.TabIndex = 18;
            this.label27.Text = "经办人：";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(236, 13);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(41, 12);
            this.label28.TabIndex = 19;
            this.label28.Text = "部门：";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelArea
            // 
            this.panelArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelArea.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelArea.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelArea.BorderWidth = 1;
            this.panelArea.Curvature = 0;
            this.panelArea.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelArea.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelArea.Location = new System.Drawing.Point(16, 91);
            this.panelArea.Name = "panelArea";
            this.panelArea.Size = new System.Drawing.Size(996, 183);
            this.panelArea.TabIndex = 200;
            // 
            // ImportMenuStrip
            // 
            this.ImportMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImporPurchasePlan,
            this.ImporSaleOrder});
            this.ImportMenuStrip.Name = "contextMenuStrip1";
            this.ImportMenuStrip.Size = new System.Drawing.Size(161, 70);
            // 
            // ImporPurchasePlan
            // 
            this.ImporPurchasePlan.Name = "ImporPurchasePlan";
            this.ImporPurchasePlan.Size = new System.Drawing.Size(160, 22);
            this.ImporPurchasePlan.Text = "导入采购计划单";
            this.ImporPurchasePlan.Click += new System.EventHandler(this.ImporPurchasePlan_Click);
            // 
            // ImporSaleOrder
            // 
            this.ImporSaleOrder.Name = "ImporSaleOrder";
            this.ImporSaleOrder.Size = new System.Drawing.Size(160, 22);
            this.ImporSaleOrder.Text = "导入销售订单";
            this.ImporSaleOrder.Click += new System.EventHandler(this.ImporSaleOrder_Click);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 18;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 18;
            // 
            // parts_code
            // 
            this.parts_code.DataPropertyName = "parts_code";
            this.parts_code.HeaderText = "配件编码";
            this.parts_code.MinimumWidth = 100;
            this.parts_code.Name = "parts_code";
            this.parts_code.ReadOnly = true;
            this.parts_code.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // parts_name
            // 
            this.parts_name.DataPropertyName = "parts_name";
            this.parts_name.HeaderText = "配件名称";
            this.parts_name.MinimumWidth = 100;
            this.parts_name.Name = "parts_name";
            this.parts_name.ReadOnly = true;
            // 
            // car_factory_code
            // 
            this.car_factory_code.DataPropertyName = "car_factory_code";
            this.car_factory_code.HeaderText = "厂商编号";
            this.car_factory_code.MinimumWidth = 100;
            this.car_factory_code.Name = "car_factory_code";
            this.car_factory_code.ReadOnly = true;
            // 
            // drawing_num
            // 
            this.drawing_num.DataPropertyName = "drawing_num";
            this.drawing_num.HeaderText = "图号";
            this.drawing_num.MinimumWidth = 100;
            this.drawing_num.Name = "drawing_num";
            this.drawing_num.ReadOnly = true;
            // 
            // unit_id
            // 
            this.unit_id.DataPropertyName = "unit_id";
            this.unit_id.HeaderText = "单位";
            this.unit_id.MinimumWidth = 100;
            this.unit_id.Name = "unit_id";
            this.unit_id.ReadOnly = true;
            this.unit_id.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.unit_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // model
            // 
            this.model.DataPropertyName = "model";
            this.model.HeaderText = "规格型号";
            this.model.MinimumWidth = 100;
            this.model.Name = "model";
            this.model.ReadOnly = true;
            // 
            // application_count
            // 
            this.application_count.DataPropertyName = "application_count";
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.application_count.DefaultCellStyle = dataGridViewCellStyle3;
            this.application_count.HeaderText = "申请数量";
            this.application_count.MinimumWidth = 100;
            this.application_count.Name = "application_count";
            this.application_count.ReadOnly = true;
            // 
            // price
            // 
            this.price.DataPropertyName = "price";
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0";
            this.price.DefaultCellStyle = dataGridViewCellStyle4;
            this.price.HeaderText = "单价";
            this.price.MinimumWidth = 100;
            this.price.Name = "price";
            this.price.ReadOnly = true;
            // 
            // money
            // 
            this.money.DataPropertyName = "money";
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = "0";
            this.money.DefaultCellStyle = dataGridViewCellStyle5;
            this.money.HeaderText = "金额";
            this.money.MinimumWidth = 100;
            this.money.Name = "money";
            this.money.ReadOnly = true;
            // 
            // conf_count
            // 
            this.conf_count.DataPropertyName = "conf_count";
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = "0";
            this.conf_count.DefaultCellStyle = dataGridViewCellStyle6;
            this.conf_count.HeaderText = "确认数量";
            this.conf_count.MinimumWidth = 100;
            this.conf_count.Name = "conf_count";
            this.conf_count.ReadOnly = true;
            // 
            // parts_explain
            // 
            this.parts_explain.DataPropertyName = "parts_explain";
            this.parts_explain.HeaderText = "配件说明";
            this.parts_explain.MinimumWidth = 100;
            this.parts_explain.Name = "parts_explain";
            this.parts_explain.ReadOnly = true;
            // 
            // replaces
            // 
            this.replaces.DataPropertyName = "replaces";
            this.replaces.HeaderText = "替代";
            this.replaces.MinimumWidth = 100;
            this.replaces.Name = "replaces";
            this.replaces.ReadOnly = true;
            // 
            // center_library_explain
            // 
            this.center_library_explain.DataPropertyName = "center_library_explain";
            this.center_library_explain.HeaderText = "中心站/库处理说明";
            this.center_library_explain.MinimumWidth = 250;
            this.center_library_explain.Name = "center_library_explain";
            this.center_library_explain.ReadOnly = true;
            this.center_library_explain.Width = 250;
            // 
            // cancel_reasons
            // 
            this.cancel_reasons.DataPropertyName = "cancel_reasons";
            this.cancel_reasons.HeaderText = "取消原因";
            this.cancel_reasons.MinimumWidth = 100;
            this.cancel_reasons.Name = "cancel_reasons";
            this.cancel_reasons.ReadOnly = true;
            // 
            // relation_order
            // 
            this.relation_order.DataPropertyName = "relation_order";
            this.relation_order.HeaderText = "引用单号";
            this.relation_order.MinimumWidth = 100;
            this.relation_order.Name = "relation_order";
            this.relation_order.ReadOnly = true;
            this.relation_order.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.relation_order.Visible = false;
            // 
            // create_by
            // 
            this.create_by.HeaderText = "create_by";
            this.create_by.Name = "create_by";
            this.create_by.ReadOnly = true;
            this.create_by.Visible = false;
            this.create_by.Width = 90;
            // 
            // create_name
            // 
            this.create_name.HeaderText = "create_name";
            this.create_name.Name = "create_name";
            this.create_name.ReadOnly = true;
            this.create_name.Visible = false;
            this.create_name.Width = 109;
            // 
            // create_time
            // 
            this.create_time.HeaderText = "create_time";
            this.create_time.Name = "create_time";
            this.create_time.ReadOnly = true;
            this.create_time.Visible = false;
            this.create_time.Width = 103;
            // 
            // ImportType
            // 
            this.ImportType.DataPropertyName = "ImportType";
            this.ImportType.HeaderText = "导入类型";
            this.ImportType.MinimumWidth = 100;
            this.ImportType.Name = "ImportType";
            this.ImportType.ReadOnly = true;
            this.ImportType.Visible = false;
            // 
            // UCYTAddOrEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelArea);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.gvPurchaseList);
            this.Controls.Add(this.lblcrm_bill_id);
            this.Controls.Add(this.ddlorder_type);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblorder_status_name);
            this.Controls.Add(this.ddtorder_date);
            this.Controls.Add(this.lblorder_num);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Name = "UCYTAddOrEdit";
            this.Load += new System.EventHandler(this.UCYTAddOrEdit_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.lblorder_num, 0);
            this.Controls.SetChildIndex(this.ddtorder_date, 0);
            this.Controls.SetChildIndex(this.lblorder_status_name, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.ddlorder_type, 0);
            this.Controls.SetChildIndex(this.lblcrm_bill_id, 0);
            this.Controls.SetChildIndex(this.gvPurchaseList, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.panelArea, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchaseList)).EndInit();
            this.cmsPurchaseAccessories.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ImportMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblorder_status_name;
        private ServiceStationClient.ComponentUI.DateTimePickerEx ddtorder_date;
        private System.Windows.Forms.Label lblorder_num;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlorder_type;
        private System.Windows.Forms.Label lblcrm_bill_id;
        private ServiceStationClient.ComponentUI.DataGridViewEx gvPurchaseList;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private System.Windows.Forms.Label lblupdate_time1;
        private System.Windows.Forms.Label lblupdate_name;
        private System.Windows.Forms.Label lblcreate_time1;
        private System.Windows.Forms.Label lblcreate_name;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbloperator_name;
        private System.Windows.Forms.Label label8;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlhandle;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlorg_id;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private ServiceStationClient.ComponentUI.PanelEx panelArea;
        private System.Windows.Forms.ContextMenuStrip cmsPurchaseAccessories;
        private System.Windows.Forms.ToolStripMenuItem cmsPurchaseAccessoriesAdd;
        private System.Windows.Forms.ToolStripMenuItem cmsPurchaseAccessoriesEdit;
        private System.Windows.Forms.ToolStripMenuItem cmsPurchaseAccessoriesDelete;
        private System.Windows.Forms.ContextMenuStrip ImportMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ImporPurchasePlan;
        private System.Windows.Forms.ToolStripMenuItem ImporSaleOrder;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_factory_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn drawing_num;
        private System.Windows.Forms.DataGridViewComboBoxColumn unit_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn model;
        private System.Windows.Forms.DataGridViewTextBoxColumn application_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn money;
        private System.Windows.Forms.DataGridViewTextBoxColumn conf_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_explain;
        private System.Windows.Forms.DataGridViewTextBoxColumn replaces;
        private System.Windows.Forms.DataGridViewTextBoxColumn center_library_explain;
        private System.Windows.Forms.DataGridViewTextBoxColumn cancel_reasons;
        private System.Windows.Forms.DataGridViewTextBoxColumn relation_order;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_by;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImportType;
    }
}
