namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.InventoryBill
{
    partial class UCStockCheckAddOrEdit
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCStockCheckAddOrEdit));
            this.gvPartsMsgList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.partsnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drawingnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partbrand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarFactoryCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Papcounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FmOffCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProLossCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BusinesPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Calcmoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartsSelMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PartAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.PartDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.lblupdate_time = new System.Windows.Forms.Label();
            this.lblupdate_name = new System.Windows.Forms.Label();
            this.lblcreate_time = new System.Windows.Forms.Label();
            this.lblcreate_name = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lbloperator_name = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.Combhandle_name = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.Comborg_name = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.DTPickorder_date = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.lblorder_num = new System.Windows.Forms.Label();
            this.Combwh_name = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.txtremark = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label6 = new System.Windows.Forms.Label();
            this.txtorder_status_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnImportMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExcelImport = new System.Windows.Forms.ToolStripMenuItem();
            this.PartFormImport = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gvPartsMsgList)).BeginInit();
            this.PartsSelMenu.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.BtnImportMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1154, 25);
            // 
            // gvPartsMsgList
            // 
            this.gvPartsMsgList.AllowUserToAddRows = false;
            this.gvPartsMsgList.AllowUserToDeleteRows = false;
            this.gvPartsMsgList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gvPartsMsgList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvPartsMsgList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvPartsMsgList.BackgroundColor = System.Drawing.Color.White;
            this.gvPartsMsgList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPartsMsgList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvPartsMsgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPartsMsgList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.partsnum,
            this.partname,
            this.PartSpec,
            this.drawingnum,
            this.unitname,
            this.partbrand,
            this.CarFactoryCode,
            this.BarCode,
            this.Papcounts,
            this.FmOffCount,
            this.ProLossCount,
            this.BusinesPrice,
            this.Calcmoney,
            this.remarks});
            this.gvPartsMsgList.ContextMenuStrip = this.PartsSelMenu;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPartsMsgList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvPartsMsgList.EnableHeadersVisualStyles = false;
            this.gvPartsMsgList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.gvPartsMsgList.IsCheck = true;
            this.gvPartsMsgList.Location = new System.Drawing.Point(2, 185);
            this.gvPartsMsgList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gvPartsMsgList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gvPartsMsgList.MergeColumnNames")));
            this.gvPartsMsgList.MultiSelect = false;
            this.gvPartsMsgList.Name = "gvPartsMsgList";
            this.gvPartsMsgList.ReadOnly = true;
            this.gvPartsMsgList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.gvPartsMsgList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvPartsMsgList.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.gvPartsMsgList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvPartsMsgList.RowTemplate.Height = 23;
            this.gvPartsMsgList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPartsMsgList.ShowCheckBox = true;
            this.gvPartsMsgList.Size = new System.Drawing.Size(1147, 226);
            this.gvPartsMsgList.TabIndex = 297;
            this.gvPartsMsgList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPartsMsgList_CellEndEdit);
            // 
            // colCheck
            // 
            this.colCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colCheck.Width = 30;
            // 
            // partsnum
            // 
            this.partsnum.DataPropertyName = "parts_code";
            this.partsnum.HeaderText = "配件编码";
            this.partsnum.Name = "partsnum";
            this.partsnum.ReadOnly = true;
            // 
            // partname
            // 
            this.partname.DataPropertyName = "parts_name";
            this.partname.HeaderText = "配件名称";
            this.partname.Name = "partname";
            this.partname.ReadOnly = true;
            // 
            // PartSpec
            // 
            this.PartSpec.DataPropertyName = "model";
            this.PartSpec.HeaderText = "规格";
            this.PartSpec.Name = "PartSpec";
            this.PartSpec.ReadOnly = true;
            // 
            // drawingnum
            // 
            this.drawingnum.DataPropertyName = "drawing_num";
            this.drawingnum.HeaderText = "图号";
            this.drawingnum.Name = "drawingnum";
            this.drawingnum.ReadOnly = true;
            // 
            // unitname
            // 
            this.unitname.DataPropertyName = "unit_name";
            this.unitname.HeaderText = "单位";
            this.unitname.Name = "unitname";
            this.unitname.ReadOnly = true;
            // 
            // partbrand
            // 
            this.partbrand.DataPropertyName = "parts_brand";
            this.partbrand.HeaderText = "品牌";
            this.partbrand.Name = "partbrand";
            this.partbrand.ReadOnly = true;
            // 
            // CarFactoryCode
            // 
            this.CarFactoryCode.DataPropertyName = "car_parts_code";
            this.CarFactoryCode.HeaderText = "厂家编码";
            this.CarFactoryCode.Name = "CarFactoryCode";
            this.CarFactoryCode.ReadOnly = true;
            // 
            // BarCode
            // 
            this.BarCode.DataPropertyName = "parts_barcode";
            this.BarCode.HeaderText = "条形码";
            this.BarCode.Name = "BarCode";
            this.BarCode.ReadOnly = true;
            // 
            // Papcounts
            // 
            this.Papcounts.DataPropertyName = "paper_count";
            this.Papcounts.HeaderText = "账面数量";
            this.Papcounts.Name = "Papcounts";
            this.Papcounts.ReadOnly = true;
            // 
            // FmOffCount
            // 
            this.FmOffCount.DataPropertyName = "firmoffer_count";
            this.FmOffCount.HeaderText = "实盘数量";
            this.FmOffCount.Name = "FmOffCount";
            this.FmOffCount.ReadOnly = true;
            // 
            // ProLossCount
            // 
            this.ProLossCount.DataPropertyName = "profitloss_count";
            this.ProLossCount.HeaderText = "盈亏数量";
            this.ProLossCount.Name = "ProLossCount";
            this.ProLossCount.ReadOnly = true;
            // 
            // BusinesPrice
            // 
            this.BusinesPrice.DataPropertyName = "business_price";
            this.BusinesPrice.HeaderText = "业务单价";
            this.BusinesPrice.Name = "BusinesPrice";
            this.BusinesPrice.ReadOnly = true;
            // 
            // Calcmoney
            // 
            this.Calcmoney.DataPropertyName = "money";
            this.Calcmoney.HeaderText = "金额";
            this.Calcmoney.Name = "Calcmoney";
            this.Calcmoney.ReadOnly = true;
            // 
            // remarks
            // 
            this.remarks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.remarks.DataPropertyName = "remark";
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            this.remarks.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.remarks.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.remarks.Width = 200;
            // 
            // PartsSelMenu
            // 
            this.PartsSelMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PartAdd,
            this.PartDelete});
            this.PartsSelMenu.Name = "PartsSelMenu";
            this.PartsSelMenu.Size = new System.Drawing.Size(127, 52);
            // 
            // PartAdd
            // 
            this.PartAdd.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PartAdd.Image = ((System.Drawing.Image)(resources.GetObject("PartAdd.Image")));
            this.PartAdd.Name = "PartAdd";
            this.PartAdd.Size = new System.Drawing.Size(126, 24);
            this.PartAdd.Text = "● 新  增";
            this.PartAdd.Click += new System.EventHandler(this.PartAdd_Click);
            // 
            // PartDelete
            // 
            this.PartDelete.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PartDelete.Image = global::HXCPcClient.Properties.Resources.Delete_E;
            this.PartDelete.Name = "PartDelete";
            this.PartDelete.Size = new System.Drawing.Size(152, 24);
            this.PartDelete.Text = "● 删  除";
            this.PartDelete.Click += new System.EventHandler(this.PartDelete_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.lblupdate_time);
            this.panelEx1.Controls.Add(this.lblupdate_name);
            this.panelEx1.Controls.Add(this.lblcreate_time);
            this.panelEx1.Controls.Add(this.lblcreate_name);
            this.panelEx1.Controls.Add(this.label15);
            this.panelEx1.Controls.Add(this.label14);
            this.panelEx1.Controls.Add(this.label16);
            this.panelEx1.Controls.Add(this.label17);
            this.panelEx1.Controls.Add(this.lbloperator_name);
            this.panelEx1.Controls.Add(this.label11);
            this.panelEx1.Controls.Add(this.Combhandle_name);
            this.panelEx1.Controls.Add(this.Comborg_name);
            this.panelEx1.Controls.Add(this.label12);
            this.panelEx1.Controls.Add(this.label13);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(2, 415);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1148, 98);
            this.panelEx1.TabIndex = 296;
            // 
            // lblupdate_time
            // 
            this.lblupdate_time.AutoSize = true;
            this.lblupdate_time.Location = new System.Drawing.Point(822, 71);
            this.lblupdate_time.Name = "lblupdate_time";
            this.lblupdate_time.Size = new System.Drawing.Size(11, 12);
            this.lblupdate_time.TabIndex = 119;
            this.lblupdate_time.Text = ".";
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
            // lblcreate_time
            // 
            this.lblcreate_time.AutoSize = true;
            this.lblcreate_time.Location = new System.Drawing.Point(822, 41);
            this.lblcreate_time.Name = "lblcreate_time";
            this.lblcreate_time.Size = new System.Drawing.Size(11, 12);
            this.lblcreate_time.TabIndex = 117;
            this.lblcreate_time.Text = ".";
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
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(751, 41);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 113;
            this.label16.Text = "创建时间：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(474, 41);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 112;
            this.label17.Text = "创建人：";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(763, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 110;
            this.label11.Text = "操作人：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Combhandle_name
            // 
            this.Combhandle_name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Combhandle_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combhandle_name.FormattingEnabled = true;
            this.Combhandle_name.Location = new System.Drawing.Point(530, 7);
            this.Combhandle_name.Name = "Combhandle_name";
            this.Combhandle_name.Size = new System.Drawing.Size(116, 22);
            this.Combhandle_name.TabIndex = 17;
            this.Combhandle_name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Combhandle_name_MouseClick);
            // 
            // Comborg_name
            // 
            this.Comborg_name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Comborg_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Comborg_name.FormattingEnabled = true;
            this.Comborg_name.Location = new System.Drawing.Point(283, 7);
            this.Comborg_name.Name = "Comborg_name";
            this.Comborg_name.Size = new System.Drawing.Size(116, 22);
            this.Comborg_name.TabIndex = 20;
            this.Comborg_name.SelectedIndexChanged += new System.EventHandler(this.Comborg_name_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(474, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 18;
            this.label12.Text = "经办人：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(236, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 19;
            this.label13.Text = "部门：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelEx2
            // 
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.DTPickorder_date);
            this.panelEx2.Controls.Add(this.lblorder_num);
            this.panelEx2.Controls.Add(this.Combwh_name);
            this.panelEx2.Controls.Add(this.label10);
            this.panelEx2.Controls.Add(this.txtremark);
            this.panelEx2.Controls.Add(this.label6);
            this.panelEx2.Controls.Add(this.txtorder_status_name);
            this.panelEx2.Controls.Add(this.label7);
            this.panelEx2.Controls.Add(this.label3);
            this.panelEx2.Controls.Add(this.label2);
            this.panelEx2.Controls.Add(this.label1);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(3, 29);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1146, 155);
            this.panelEx2.TabIndex = 298;
            // 
            // DTPickorder_date
            // 
            this.DTPickorder_date.Location = new System.Drawing.Point(89, 29);
            this.DTPickorder_date.Name = "DTPickorder_date";
            this.DTPickorder_date.ShowFormat = "yyyy-MM-dd";
            this.DTPickorder_date.Size = new System.Drawing.Size(150, 21);
            this.DTPickorder_date.TabIndex = 305;
            // 
            // lblorder_num
            // 
            this.lblorder_num.AutoSize = true;
            this.lblorder_num.Location = new System.Drawing.Point(1102, 27);
            this.lblorder_num.Name = "lblorder_num";
            this.lblorder_num.Size = new System.Drawing.Size(11, 12);
            this.lblorder_num.TabIndex = 304;
            this.lblorder_num.Text = ".";
            this.lblorder_num.Visible = false;
            // 
            // Combwh_name
            // 
            this.Combwh_name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Combwh_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combwh_name.FormattingEnabled = true;
            this.Combwh_name.Location = new System.Drawing.Point(89, 75);
            this.Combwh_name.Name = "Combwh_name";
            this.Combwh_name.Size = new System.Drawing.Size(150, 22);
            this.Combwh_name.TabIndex = 303;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(242, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 302;
            this.label10.Text = "*";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtremark
            // 
            this.txtremark.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtremark.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtremark.BackColor = System.Drawing.Color.Transparent;
            this.txtremark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtremark.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtremark.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtremark.ForeImage = null;
            this.txtremark.InputtingVerifyCondition = null;
            this.txtremark.Location = new System.Drawing.Point(408, 75);
            this.txtremark.MaxLengh = 32767;
            this.txtremark.Multiline = false;
            this.txtremark.Name = "txtremark";
            this.txtremark.Radius = 3;
            this.txtremark.ReadOnly = false;
            this.txtremark.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtremark.ShowError = false;
            this.txtremark.Size = new System.Drawing.Size(150, 23);
            this.txtremark.TabIndex = 301;
            this.txtremark.UseSystemPasswordChar = false;
            this.txtremark.Value = "";
            this.txtremark.VerifyCondition = null;
            this.txtremark.VerifyType = null;
            this.txtremark.VerifyTypeName = null;
            this.txtremark.WaterMark = null;
            this.txtremark.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 299;
            this.label6.Text = "仓库：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtorder_status_name
            // 
            this.txtorder_status_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtorder_status_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtorder_status_name.BackColor = System.Drawing.Color.Transparent;
            this.txtorder_status_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtorder_status_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtorder_status_name.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtorder_status_name.ForeImage = null;
            this.txtorder_status_name.InputtingVerifyCondition = null;
            this.txtorder_status_name.Location = new System.Drawing.Point(408, 28);
            this.txtorder_status_name.MaxLengh = 32767;
            this.txtorder_status_name.Multiline = false;
            this.txtorder_status_name.Name = "txtorder_status_name";
            this.txtorder_status_name.Radius = 3;
            this.txtorder_status_name.ReadOnly = true;
            this.txtorder_status_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtorder_status_name.ShowError = false;
            this.txtorder_status_name.Size = new System.Drawing.Size(150, 23);
            this.txtorder_status_name.TabIndex = 298;
            this.txtorder_status_name.UseSystemPasswordChar = false;
            this.txtorder_status_name.Value = "";
            this.txtorder_status_name.VerifyCondition = null;
            this.txtorder_status_name.VerifyType = null;
            this.txtorder_status_name.VerifyTypeName = null;
            this.txtorder_status_name.WaterMark = null;
            this.txtorder_status_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(364, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 300;
            this.label7.Text = "备注：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(340, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 297;
            this.label3.Text = "单据状态：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 296;
            this.label2.Text = "日期：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1054, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 295;
            this.label1.Text = "单号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // BtnImportMenu
            // 
            this.BtnImportMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExcelImport,
            this.PartFormImport});
            this.BtnImportMenu.Name = "BtnImportMenu";
            this.BtnImportMenu.Size = new System.Drawing.Size(153, 74);
            // 
            // ExcelImport
            // 
            this.ExcelImport.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ExcelImport.Image = ((System.Drawing.Image)(resources.GetObject("ExcelImport.Image")));
            this.ExcelImport.Name = "ExcelImport";
            this.ExcelImport.Size = new System.Drawing.Size(152, 24);
            this.ExcelImport.Text = "● Excel导入";
            this.ExcelImport.Click += new System.EventHandler(this.ExcelImport_Click);
            // 
            // PartFormImport
            // 
            this.PartFormImport.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PartFormImport.Image = ((System.Drawing.Image)(resources.GetObject("PartFormImport.Image")));
            this.PartFormImport.Name = "PartFormImport";
            this.PartFormImport.Size = new System.Drawing.Size(152, 24);
            this.PartFormImport.Text = "● 配件选择";
            this.PartFormImport.Click += new System.EventHandler(this.PartAdd_Click);
            // 
            // UCStockCheckAddOrEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.gvPartsMsgList);
            this.Controls.Add(this.panelEx1);
            this.Name = "UCStockCheckAddOrEdit";
            this.Size = new System.Drawing.Size(1154, 517);
            this.Load += new System.EventHandler(this.UCStockReceiptAddOrEdit_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.gvPartsMsgList, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gvPartsMsgList)).EndInit();
            this.PartsSelMenu.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.BtnImportMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx gvPartsMsgList;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private System.Windows.Forms.Label lblupdate_time;
        private System.Windows.Forms.Label lblupdate_name;
        private System.Windows.Forms.Label lblcreate_time;
        private System.Windows.Forms.Label lblcreate_name;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbloperator_name;
        private System.Windows.Forms.Label label11;
        private ServiceStationClient.ComponentUI.ComboBoxEx Combhandle_name;
        private ServiceStationClient.ComponentUI.ComboBoxEx Comborg_name;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms DTPickorder_date;
        private System.Windows.Forms.Label lblorder_num;
        private ServiceStationClient.ComponentUI.ComboBoxEx Combwh_name;
        private System.Windows.Forms.Label label10;
        private ServiceStationClient.ComponentUI.TextBoxEx txtremark;
        private System.Windows.Forms.Label label6;
        private ServiceStationClient.ComponentUI.TextBoxEx txtorder_status_name;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip BtnImportMenu;
        private System.Windows.Forms.ContextMenuStrip PartsSelMenu;
        private System.Windows.Forms.ToolStripMenuItem PartAdd;
        private System.Windows.Forms.ToolStripMenuItem PartDelete;
        private System.Windows.Forms.ToolStripMenuItem ExcelImport;
        private System.Windows.Forms.ToolStripMenuItem PartFormImport;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn partsnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn partname;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn drawingnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitname;
        private System.Windows.Forms.DataGridViewTextBoxColumn partbrand;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarFactoryCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Papcounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn FmOffCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProLossCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn BusinesPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Calcmoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
    }
}
