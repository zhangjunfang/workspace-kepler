namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.ModifyPriceBill
{
    partial class UCModifyPriceDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCModifyPriceDetail));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx4 = new ServiceStationClient.ComponentUI.PanelEx();
            this.winFormPartPager = new ServiceStationClient.ComponentUI.WinFormPager();
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
            this.ddlhandle = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.ddlorg_id = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.gvPartsMsgList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partsnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drawingnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partbrand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarFactoryCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.counts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifyUnitprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModAfterPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Calcmoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.txtmodifyratio = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.txtcom_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtwh_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtmodifydocnum = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label19 = new System.Windows.Forms.Label();
            this.DTPickorder_date = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.lblorder_num = new System.Windows.Forms.Label();
            this.txtremark = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtorder_status_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx4.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPartsMsgList)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1151, 25);
            // 
            // panelEx4
            // 
            this.panelEx4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx4.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx4.Controls.Add(this.winFormPartPager);
            this.panelEx4.Location = new System.Drawing.Point(4, 374);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(1145, 48);
            this.panelEx4.TabIndex = 319;
            // 
            // winFormPartPager
            // 
            this.winFormPartPager.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.winFormPartPager.BackColor = System.Drawing.Color.Transparent;
            this.winFormPartPager.BtnTextNext = "下页";
            this.winFormPartPager.BtnTextPrevious = "上页";
            this.winFormPartPager.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.winFormPartPager.Location = new System.Drawing.Point(670, 3);
            this.winFormPartPager.Name = "winFormPartPager";
            this.winFormPartPager.PageCount = 0;
            this.winFormPartPager.PageSize = 15;
            this.winFormPartPager.RecordCount = 0;
            this.winFormPartPager.Size = new System.Drawing.Size(472, 43);
            this.winFormPartPager.TabIndex = 5;
            this.winFormPartPager.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.winFormPartPager.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.winFormPartPager_PageIndexChanged);
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
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
            this.panelEx1.Controls.Add(this.ddlhandle);
            this.panelEx1.Controls.Add(this.ddlorg_id);
            this.panelEx1.Controls.Add(this.label12);
            this.panelEx1.Controls.Add(this.label13);
            this.panelEx1.Location = new System.Drawing.Point(4, 423);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1145, 106);
            this.panelEx1.TabIndex = 318;
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
            // gvPartsMsgList
            // 
            this.gvPartsMsgList.AllowUserToAddRows = false;
            this.gvPartsMsgList.AllowUserToDeleteRows = false;
            this.gvPartsMsgList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gvPartsMsgList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvPartsMsgList.BackgroundColor = System.Drawing.Color.White;
            this.gvPartsMsgList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPartsMsgList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvPartsMsgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPartsMsgList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.partsnum,
            this.partname,
            this.PartSpec,
            this.drawingnum,
            this.unitname,
            this.partbrand,
            this.CarFactoryCode,
            this.BarCode,
            this.counts,
            this.ModifyUnitprice,
            this.ModRate,
            this.ModAfterPrice,
            this.Calcmoney,
            this.remarks});
            this.gvPartsMsgList.EnableHeadersVisualStyles = false;
            this.gvPartsMsgList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.gvPartsMsgList.Location = new System.Drawing.Point(3, 159);
            this.gvPartsMsgList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gvPartsMsgList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gvPartsMsgList.MergeColumnNames")));
            this.gvPartsMsgList.MultiSelect = false;
            this.gvPartsMsgList.Name = "gvPartsMsgList";
            this.gvPartsMsgList.ReadOnly = true;
            this.gvPartsMsgList.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.gvPartsMsgList.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gvPartsMsgList.RowTemplate.Height = 23;
            this.gvPartsMsgList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPartsMsgList.ShowCheckBox = true;
            this.gvPartsMsgList.Size = new System.Drawing.Size(1144, 216);
            this.gvPartsMsgList.TabIndex = 317;
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ID.HeaderText = "序号";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 57;
            // 
            // partsnum
            // 
            this.partsnum.HeaderText = "配件编码";
            this.partsnum.Name = "partsnum";
            this.partsnum.ReadOnly = true;
            // 
            // partname
            // 
            this.partname.HeaderText = "配件名称";
            this.partname.Name = "partname";
            this.partname.ReadOnly = true;
            // 
            // PartSpec
            // 
            this.PartSpec.HeaderText = "规格";
            this.PartSpec.Name = "PartSpec";
            this.PartSpec.ReadOnly = true;
            // 
            // drawingnum
            // 
            this.drawingnum.HeaderText = "图号";
            this.drawingnum.Name = "drawingnum";
            this.drawingnum.ReadOnly = true;
            // 
            // unitname
            // 
            this.unitname.HeaderText = "单位";
            this.unitname.Name = "unitname";
            this.unitname.ReadOnly = true;
            // 
            // partbrand
            // 
            this.partbrand.HeaderText = "品牌";
            this.partbrand.Name = "partbrand";
            this.partbrand.ReadOnly = true;
            // 
            // CarFactoryCode
            // 
            this.CarFactoryCode.HeaderText = "厂家编码";
            this.CarFactoryCode.Name = "CarFactoryCode";
            this.CarFactoryCode.ReadOnly = true;
            // 
            // BarCode
            // 
            this.BarCode.HeaderText = "条形码";
            this.BarCode.Name = "BarCode";
            this.BarCode.ReadOnly = true;
            // 
            // counts
            // 
            this.counts.HeaderText = "数量";
            this.counts.Name = "counts";
            this.counts.ReadOnly = true;
            // 
            // ModifyUnitprice
            // 
            this.ModifyUnitprice.HeaderText = "调整单价";
            this.ModifyUnitprice.Name = "ModifyUnitprice";
            this.ModifyUnitprice.ReadOnly = true;
            // 
            // ModRate
            // 
            this.ModRate.HeaderText = "调整比率";
            this.ModRate.Name = "ModRate";
            this.ModRate.ReadOnly = true;
            // 
            // ModAfterPrice
            // 
            this.ModAfterPrice.HeaderText = "调后单价";
            this.ModAfterPrice.Name = "ModAfterPrice";
            this.ModAfterPrice.ReadOnly = true;
            // 
            // Calcmoney
            // 
            this.Calcmoney.HeaderText = "金额";
            this.Calcmoney.Name = "Calcmoney";
            this.Calcmoney.ReadOnly = true;
            // 
            // remarks
            // 
            this.remarks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            this.remarks.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.remarks.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panelEx2
            // 
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.Controls.Add(this.txtmodifyratio);
            this.panelEx2.Controls.Add(this.label5);
            this.panelEx2.Controls.Add(this.txtcom_name);
            this.panelEx2.Controls.Add(this.txtwh_name);
            this.panelEx2.Controls.Add(this.txtmodifydocnum);
            this.panelEx2.Controls.Add(this.label19);
            this.panelEx2.Controls.Add(this.DTPickorder_date);
            this.panelEx2.Controls.Add(this.lblorder_num);
            this.panelEx2.Controls.Add(this.txtremark);
            this.panelEx2.Controls.Add(this.label6);
            this.panelEx2.Controls.Add(this.label4);
            this.panelEx2.Controls.Add(this.txtorder_status_name);
            this.panelEx2.Controls.Add(this.label7);
            this.panelEx2.Controls.Add(this.label3);
            this.panelEx2.Controls.Add(this.label2);
            this.panelEx2.Controls.Add(this.label1);
            this.panelEx2.Location = new System.Drawing.Point(3, 30);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1143, 128);
            this.panelEx2.TabIndex = 320;
            // 
            // txtmodifyratio
            // 
            this.txtmodifyratio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtmodifyratio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtmodifyratio.BackColor = System.Drawing.Color.Transparent;
            this.txtmodifyratio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtmodifyratio.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtmodifyratio.ForeImage = null;
            this.txtmodifyratio.Location = new System.Drawing.Point(624, 69);
            this.txtmodifyratio.MaxLengh = 32767;
            this.txtmodifyratio.Multiline = false;
            this.txtmodifyratio.Name = "txtmodifyratio";
            this.txtmodifyratio.Radius = 3;
            this.txtmodifyratio.ReadOnly = true;
            this.txtmodifyratio.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtmodifyratio.Size = new System.Drawing.Size(151, 23);
            this.txtmodifyratio.TabIndex = 339;
            this.txtmodifyratio.UseSystemPasswordChar = false;
            this.txtmodifyratio.WaterMark = null;
            this.txtmodifyratio.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(550, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 338;
            this.label5.Text = "调价比率%：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtcom_name
            // 
            this.txtcom_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtcom_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtcom_name.BackColor = System.Drawing.Color.Transparent;
            this.txtcom_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtcom_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtcom_name.ForeImage = null;
            this.txtcom_name.Location = new System.Drawing.Point(858, 26);
            this.txtcom_name.MaxLengh = 32767;
            this.txtcom_name.Multiline = false;
            this.txtcom_name.Name = "txtcom_name";
            this.txtcom_name.Radius = 3;
            this.txtcom_name.ReadOnly = true;
            this.txtcom_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtcom_name.Size = new System.Drawing.Size(258, 23);
            this.txtcom_name.TabIndex = 337;
            this.txtcom_name.UseSystemPasswordChar = false;
            this.txtcom_name.WaterMark = null;
            this.txtcom_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtwh_name
            // 
            this.txtwh_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtwh_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtwh_name.BackColor = System.Drawing.Color.Transparent;
            this.txtwh_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtwh_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtwh_name.ForeImage = null;
            this.txtwh_name.Location = new System.Drawing.Point(68, 69);
            this.txtwh_name.MaxLengh = 32767;
            this.txtwh_name.Multiline = false;
            this.txtwh_name.Name = "txtwh_name";
            this.txtwh_name.Radius = 3;
            this.txtwh_name.ReadOnly = true;
            this.txtwh_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtwh_name.Size = new System.Drawing.Size(151, 23);
            this.txtwh_name.TabIndex = 336;
            this.txtwh_name.UseSystemPasswordChar = false;
            this.txtwh_name.WaterMark = null;
            this.txtwh_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtmodifydocnum
            // 
            this.txtmodifydocnum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtmodifydocnum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtmodifydocnum.BackColor = System.Drawing.Color.Transparent;
            this.txtmodifydocnum.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtmodifydocnum.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtmodifydocnum.ForeImage = null;
            this.txtmodifydocnum.Location = new System.Drawing.Point(347, 69);
            this.txtmodifydocnum.MaxLengh = 32767;
            this.txtmodifydocnum.Multiline = false;
            this.txtmodifydocnum.Name = "txtmodifydocnum";
            this.txtmodifydocnum.Radius = 3;
            this.txtmodifydocnum.ReadOnly = true;
            this.txtmodifydocnum.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtmodifydocnum.Size = new System.Drawing.Size(151, 23);
            this.txtmodifydocnum.TabIndex = 335;
            this.txtmodifydocnum.UseSystemPasswordChar = false;
            this.txtmodifydocnum.WaterMark = null;
            this.txtmodifydocnum.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(816, 31);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(41, 12);
            this.label19.TabIndex = 334;
            this.label19.Text = "公司：";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DTPickorder_date
            // 
            this.DTPickorder_date.Location = new System.Drawing.Point(347, 27);
            this.DTPickorder_date.Name = "DTPickorder_date";
            this.DTPickorder_date.Size = new System.Drawing.Size(150, 21);
            this.DTPickorder_date.TabIndex = 333;
            // 
            // lblorder_num
            // 
            this.lblorder_num.AutoSize = true;
            this.lblorder_num.Location = new System.Drawing.Point(70, 27);
            this.lblorder_num.Name = "lblorder_num";
            this.lblorder_num.Size = new System.Drawing.Size(11, 12);
            this.lblorder_num.TabIndex = 332;
            this.lblorder_num.Text = ".";
            // 
            // txtremark
            // 
            this.txtremark.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtremark.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtremark.BackColor = System.Drawing.Color.Transparent;
            this.txtremark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtremark.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtremark.ForeImage = null;
            this.txtremark.Location = new System.Drawing.Point(858, 69);
            this.txtremark.MaxLengh = 32767;
            this.txtremark.Multiline = false;
            this.txtremark.Name = "txtremark";
            this.txtremark.Radius = 3;
            this.txtremark.ReadOnly = true;
            this.txtremark.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtremark.Size = new System.Drawing.Size(258, 23);
            this.txtremark.TabIndex = 331;
            this.txtremark.UseSystemPasswordChar = false;
            this.txtremark.WaterMark = null;
            this.txtremark.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 329;
            this.label6.Text = "仓库：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(279, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 328;
            this.label4.Text = "调价文号：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtorder_status_name
            // 
            this.txtorder_status_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtorder_status_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtorder_status_name.BackColor = System.Drawing.Color.Transparent;
            this.txtorder_status_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtorder_status_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtorder_status_name.ForeImage = null;
            this.txtorder_status_name.Location = new System.Drawing.Point(625, 26);
            this.txtorder_status_name.MaxLengh = 32767;
            this.txtorder_status_name.Multiline = false;
            this.txtorder_status_name.Name = "txtorder_status_name";
            this.txtorder_status_name.Radius = 3;
            this.txtorder_status_name.ReadOnly = true;
            this.txtorder_status_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtorder_status_name.Size = new System.Drawing.Size(150, 23);
            this.txtorder_status_name.TabIndex = 327;
            this.txtorder_status_name.UseSystemPasswordChar = false;
            this.txtorder_status_name.WaterMark = null;
            this.txtorder_status_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(815, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 330;
            this.label7.Text = "备注：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(557, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 326;
            this.label3.Text = "单据状态：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(303, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 325;
            this.label2.Text = "日期：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 324;
            this.label1.Text = "单号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UCModifyPriceDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx4);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.gvPartsMsgList);
            this.Name = "UCModifyPriceDetail";
            this.Size = new System.Drawing.Size(1151, 531);
            this.Load += new System.EventHandler(this.UCModifyPriceDetails_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.gvPartsMsgList, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.panelEx4, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.panelEx4.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPartsMsgList)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx4;
        private ServiceStationClient.ComponentUI.WinFormPager winFormPartPager;
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
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlhandle;
        private ServiceStationClient.ComponentUI.ComboBoxEx ddlorg_id;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private ServiceStationClient.ComponentUI.DataGridViewEx gvPartsMsgList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn partsnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn partname;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn drawingnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitname;
        private System.Windows.Forms.DataGridViewTextBoxColumn partbrand;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarFactoryCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn counts;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifyUnitprice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModAfterPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Calcmoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.TextBoxEx txtmodifyratio;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.TextBoxEx txtcom_name;
        private ServiceStationClient.ComponentUI.TextBoxEx txtwh_name;
        private ServiceStationClient.ComponentUI.TextBoxEx txtmodifydocnum;
        private System.Windows.Forms.Label label19;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms DTPickorder_date;
        private System.Windows.Forms.Label lblorder_num;
        private ServiceStationClient.ComponentUI.TextBoxEx txtremark;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private ServiceStationClient.ComponentUI.TextBoxEx txtorder_status_name;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
