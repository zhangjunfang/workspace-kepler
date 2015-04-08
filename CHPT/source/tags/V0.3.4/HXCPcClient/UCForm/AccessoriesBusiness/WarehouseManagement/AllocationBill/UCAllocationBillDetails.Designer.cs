namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.AllocationBill
{
    partial class UCAllocationBillDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCAllocationBillDetails));
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
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.TxtAllocBill_WareHouse = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.TxtAllocBilling_Type = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.TxtAllocBill_Type = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.DTPickorder_date = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAllocBill_Comment = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAllocBill_State = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAllocationBill_No = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.gvBillPartsList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.SerialNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partscode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partsname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Specification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drawingnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partsbrandname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.businesscounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductionDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValidityDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isgift = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx4.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvBillPartsList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1099, 25);
            // 
            // panelEx4
            // 
            this.panelEx4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx4.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx4.Controls.Add(this.winFormPartPager);
            this.panelEx4.Location = new System.Drawing.Point(1, 325);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(1095, 48);
            this.panelEx4.TabIndex = 273;
            // 
            // winFormPartPager
            // 
            this.winFormPartPager.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.winFormPartPager.BackColor = System.Drawing.Color.Transparent;
            this.winFormPartPager.BtnTextNext = "下页";
            this.winFormPartPager.BtnTextPrevious = "上页";
            this.winFormPartPager.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.winFormPartPager.Location = new System.Drawing.Point(623, 2);
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
            this.panelEx1.Location = new System.Drawing.Point(2, 374);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1096, 97);
            this.panelEx1.TabIndex = 271;
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
            // panelEx2
            // 
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.Controls.Add(this.TxtAllocBill_WareHouse);
            this.panelEx2.Controls.Add(this.TxtAllocBilling_Type);
            this.panelEx2.Controls.Add(this.TxtAllocBill_Type);
            this.panelEx2.Controls.Add(this.DTPickorder_date);
            this.panelEx2.Controls.Add(this.label10);
            this.panelEx2.Controls.Add(this.label9);
            this.panelEx2.Controls.Add(this.label8);
            this.panelEx2.Controls.Add(this.txtAllocBill_Comment);
            this.panelEx2.Controls.Add(this.label7);
            this.panelEx2.Controls.Add(this.label6);
            this.panelEx2.Controls.Add(this.label5);
            this.panelEx2.Controls.Add(this.label4);
            this.panelEx2.Controls.Add(this.txtAllocBill_State);
            this.panelEx2.Controls.Add(this.label3);
            this.panelEx2.Controls.Add(this.label2);
            this.panelEx2.Controls.Add(this.txtAllocationBill_No);
            this.panelEx2.Controls.Add(this.label1);
            this.panelEx2.Location = new System.Drawing.Point(2, 29);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1093, 104);
            this.panelEx2.TabIndex = 272;
            // 
            // TxtAllocBill_WareHouse
            // 
            this.TxtAllocBill_WareHouse.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.TxtAllocBill_WareHouse.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.TxtAllocBill_WareHouse.BackColor = System.Drawing.Color.Transparent;
            this.TxtAllocBill_WareHouse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TxtAllocBill_WareHouse.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.TxtAllocBill_WareHouse.ForeImage = null;
            this.TxtAllocBill_WareHouse.Location = new System.Drawing.Point(632, 52);
            this.TxtAllocBill_WareHouse.MaxLengh = 32767;
            this.TxtAllocBill_WareHouse.Multiline = false;
            this.TxtAllocBill_WareHouse.Name = "TxtAllocBill_WareHouse";
            this.TxtAllocBill_WareHouse.Radius = 3;
            this.TxtAllocBill_WareHouse.ReadOnly = true;
            this.TxtAllocBill_WareHouse.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.TxtAllocBill_WareHouse.Size = new System.Drawing.Size(150, 23);
            this.TxtAllocBill_WareHouse.TabIndex = 288;
            this.TxtAllocBill_WareHouse.UseSystemPasswordChar = false;
            this.TxtAllocBill_WareHouse.WaterMark = null;
            this.TxtAllocBill_WareHouse.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // TxtAllocBilling_Type
            // 
            this.TxtAllocBilling_Type.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.TxtAllocBilling_Type.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.TxtAllocBilling_Type.BackColor = System.Drawing.Color.Transparent;
            this.TxtAllocBilling_Type.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TxtAllocBilling_Type.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.TxtAllocBilling_Type.ForeImage = null;
            this.TxtAllocBilling_Type.Location = new System.Drawing.Point(363, 53);
            this.TxtAllocBilling_Type.MaxLengh = 32767;
            this.TxtAllocBilling_Type.Multiline = false;
            this.TxtAllocBilling_Type.Name = "TxtAllocBilling_Type";
            this.TxtAllocBilling_Type.Radius = 3;
            this.TxtAllocBilling_Type.ReadOnly = true;
            this.TxtAllocBilling_Type.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.TxtAllocBilling_Type.Size = new System.Drawing.Size(150, 23);
            this.TxtAllocBilling_Type.TabIndex = 287;
            this.TxtAllocBilling_Type.UseSystemPasswordChar = false;
            this.TxtAllocBilling_Type.WaterMark = null;
            this.TxtAllocBilling_Type.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // TxtAllocBill_Type
            // 
            this.TxtAllocBill_Type.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.TxtAllocBill_Type.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.TxtAllocBill_Type.BackColor = System.Drawing.Color.Transparent;
            this.TxtAllocBill_Type.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TxtAllocBill_Type.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.TxtAllocBill_Type.ForeImage = null;
            this.TxtAllocBill_Type.Location = new System.Drawing.Point(99, 54);
            this.TxtAllocBill_Type.MaxLengh = 32767;
            this.TxtAllocBill_Type.Multiline = false;
            this.TxtAllocBill_Type.Name = "TxtAllocBill_Type";
            this.TxtAllocBill_Type.Radius = 3;
            this.TxtAllocBill_Type.ReadOnly = true;
            this.TxtAllocBill_Type.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.TxtAllocBill_Type.Size = new System.Drawing.Size(150, 23);
            this.TxtAllocBill_Type.TabIndex = 286;
            this.TxtAllocBill_Type.UseSystemPasswordChar = false;
            this.TxtAllocBill_Type.WaterMark = null;
            this.TxtAllocBill_Type.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // DTPickorder_date
            // 
            this.DTPickorder_date.Location = new System.Drawing.Point(363, 14);
            this.DTPickorder_date.Name = "DTPickorder_date";
            this.DTPickorder_date.ShowFormat = "yyyy-MM-dd";
            this.DTPickorder_date.Size = new System.Drawing.Size(150, 21);
            this.DTPickorder_date.TabIndex = 285;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(785, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 281;
            this.label10.Text = "*";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(516, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 280;
            this.label9.Text = "*";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(249, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 279;
            this.label8.Text = "*";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAllocBill_Comment
            // 
            this.txtAllocBill_Comment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtAllocBill_Comment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtAllocBill_Comment.BackColor = System.Drawing.Color.Transparent;
            this.txtAllocBill_Comment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtAllocBill_Comment.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtAllocBill_Comment.ForeImage = null;
            this.txtAllocBill_Comment.Location = new System.Drawing.Point(878, 52);
            this.txtAllocBill_Comment.MaxLengh = 32767;
            this.txtAllocBill_Comment.Multiline = false;
            this.txtAllocBill_Comment.Name = "txtAllocBill_Comment";
            this.txtAllocBill_Comment.Radius = 3;
            this.txtAllocBill_Comment.ReadOnly = true;
            this.txtAllocBill_Comment.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtAllocBill_Comment.Size = new System.Drawing.Size(171, 23);
            this.txtAllocBill_Comment.TabIndex = 278;
            this.txtAllocBill_Comment.UseSystemPasswordChar = false;
            this.txtAllocBill_Comment.WaterMark = null;
            this.txtAllocBill_Comment.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(831, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 277;
            this.label7.Text = "备注：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(589, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 276;
            this.label6.Text = "仓库：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(298, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 275;
            this.label5.Text = "开单类型：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 274;
            this.label4.Text = "单据类型：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAllocBill_State
            // 
            this.txtAllocBill_State.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtAllocBill_State.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtAllocBill_State.BackColor = System.Drawing.Color.Transparent;
            this.txtAllocBill_State.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtAllocBill_State.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtAllocBill_State.ForeImage = null;
            this.txtAllocBill_State.Location = new System.Drawing.Point(632, 13);
            this.txtAllocBill_State.MaxLengh = 32767;
            this.txtAllocBill_State.Multiline = false;
            this.txtAllocBill_State.Name = "txtAllocBill_State";
            this.txtAllocBill_State.Radius = 3;
            this.txtAllocBill_State.ReadOnly = true;
            this.txtAllocBill_State.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtAllocBill_State.Size = new System.Drawing.Size(150, 23);
            this.txtAllocBill_State.TabIndex = 273;
            this.txtAllocBill_State.UseSystemPasswordChar = false;
            this.txtAllocBill_State.WaterMark = null;
            this.txtAllocBill_State.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(566, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 272;
            this.label3.Text = "单据状态：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(319, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 271;
            this.label2.Text = "日期：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAllocationBill_No
            // 
            this.txtAllocationBill_No.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtAllocationBill_No.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtAllocationBill_No.BackColor = System.Drawing.Color.Transparent;
            this.txtAllocationBill_No.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtAllocationBill_No.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtAllocationBill_No.ForeImage = null;
            this.txtAllocationBill_No.Location = new System.Drawing.Point(99, 13);
            this.txtAllocationBill_No.MaxLengh = 32767;
            this.txtAllocationBill_No.Multiline = false;
            this.txtAllocationBill_No.Name = "txtAllocationBill_No";
            this.txtAllocationBill_No.Radius = 3;
            this.txtAllocationBill_No.ReadOnly = true;
            this.txtAllocationBill_No.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtAllocationBill_No.Size = new System.Drawing.Size(150, 23);
            this.txtAllocationBill_No.TabIndex = 270;
            this.txtAllocationBill_No.UseSystemPasswordChar = false;
            this.txtAllocationBill_No.WaterMark = null;
            this.txtAllocationBill_No.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 269;
            this.label1.Text = "单号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gvBillPartsList
            // 
            this.gvBillPartsList.AllowUserToAddRows = false;
            this.gvBillPartsList.AllowUserToDeleteRows = false;
            this.gvBillPartsList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gvBillPartsList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvBillPartsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvBillPartsList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvBillPartsList.BackgroundColor = System.Drawing.Color.White;
            this.gvBillPartsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvBillPartsList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvBillPartsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvBillPartsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SerialNum,
            this.partscode,
            this.partsname,
            this.Specification,
            this.drawingnum,
            this.unit,
            this.partsbrandname,
            this.businesscounts,
            this.ProductionDate,
            this.ValidityDate,
            this.isgift,
            this.Remark});
            this.gvBillPartsList.EnableHeadersVisualStyles = false;
            this.gvBillPartsList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.gvBillPartsList.Location = new System.Drawing.Point(2, 133);
            this.gvBillPartsList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gvBillPartsList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gvBillPartsList.MergeColumnNames")));
            this.gvBillPartsList.MultiSelect = false;
            this.gvBillPartsList.Name = "gvBillPartsList";
            this.gvBillPartsList.ReadOnly = true;
            this.gvBillPartsList.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.gvBillPartsList.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gvBillPartsList.RowTemplate.Height = 23;
            this.gvBillPartsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvBillPartsList.ShowCheckBox = true;
            this.gvBillPartsList.ShowNum = false;
            this.gvBillPartsList.Size = new System.Drawing.Size(1093, 193);
            this.gvBillPartsList.TabIndex = 270;
            // 
            // SerialNum
            // 
            this.SerialNum.HeaderText = "序号";
            this.SerialNum.Name = "SerialNum";
            this.SerialNum.ReadOnly = true;
            this.SerialNum.Width = 57;
            // 
            // partscode
            // 
            this.partscode.DataPropertyName = "parts_code";
            this.partscode.HeaderText = "配件编码";
            this.partscode.Name = "partscode";
            this.partscode.ReadOnly = true;
            this.partscode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.partscode.Width = 81;
            // 
            // partsname
            // 
            this.partsname.DataPropertyName = "parts_name";
            this.partsname.HeaderText = "配件名称";
            this.partsname.Name = "partsname";
            this.partsname.ReadOnly = true;
            this.partsname.Width = 81;
            // 
            // Specification
            // 
            this.Specification.HeaderText = "规格";
            this.Specification.Name = "Specification";
            this.Specification.ReadOnly = true;
            this.Specification.Width = 57;
            // 
            // drawingnum
            // 
            this.drawingnum.DataPropertyName = "drawing_num";
            this.drawingnum.HeaderText = "图号";
            this.drawingnum.Name = "drawingnum";
            this.drawingnum.ReadOnly = true;
            this.drawingnum.Width = 57;
            // 
            // unit
            // 
            this.unit.DataPropertyName = "unit";
            this.unit.HeaderText = "单位";
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            this.unit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.unit.Width = 57;
            // 
            // partsbrandname
            // 
            this.partsbrandname.HeaderText = "品牌";
            this.partsbrandname.Name = "partsbrandname";
            this.partsbrandname.ReadOnly = true;
            this.partsbrandname.Width = 57;
            // 
            // businesscounts
            // 
            this.businesscounts.DataPropertyName = "business_counts";
            this.businesscounts.HeaderText = "数量";
            this.businesscounts.Name = "businesscounts";
            this.businesscounts.ReadOnly = true;
            this.businesscounts.Width = 57;
            // 
            // ProductionDate
            // 
            this.ProductionDate.HeaderText = "生产日期";
            this.ProductionDate.Name = "ProductionDate";
            this.ProductionDate.ReadOnly = true;
            this.ProductionDate.Width = 81;
            // 
            // ValidityDate
            // 
            this.ValidityDate.HeaderText = "有效期至";
            this.ValidityDate.Name = "ValidityDate";
            this.ValidityDate.ReadOnly = true;
            this.ValidityDate.Width = 81;
            // 
            // isgift
            // 
            this.isgift.DataPropertyName = "is_gift";
            this.isgift.HeaderText = "赠    品";
            this.isgift.Name = "isgift";
            this.isgift.ReadOnly = true;
            this.isgift.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isgift.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.isgift.Width = 73;
            // 
            // Remark
            // 
            this.Remark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Remark.DataPropertyName = "Remark";
            this.Remark.HeaderText = "备   注";
            this.Remark.Name = "Remark";
            this.Remark.ReadOnly = true;
            // 
            // UCAllocationBillDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx4);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.gvBillPartsList);
            this.Name = "UCAllocationBillDetails";
            this.Size = new System.Drawing.Size(1099, 474);
            this.Load += new System.EventHandler(this.UCAllocationBillDetails_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.gvBillPartsList, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.panelEx4, 0);
            this.panelEx4.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvBillPartsList)).EndInit();
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
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.TextBoxEx TxtAllocBill_WareHouse;
        private ServiceStationClient.ComponentUI.TextBoxEx TxtAllocBilling_Type;
        private ServiceStationClient.ComponentUI.TextBoxEx TxtAllocBill_Type;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms DTPickorder_date;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private ServiceStationClient.ComponentUI.TextBoxEx txtAllocBill_Comment;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private ServiceStationClient.ComponentUI.TextBoxEx txtAllocBill_State;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx txtAllocationBill_No;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.DataGridViewEx gvBillPartsList;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn partscode;
        private System.Windows.Forms.DataGridViewTextBoxColumn partsname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Specification;
        private System.Windows.Forms.DataGridViewTextBoxColumn drawingnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn partsbrandname;
        private System.Windows.Forms.DataGridViewTextBoxColumn businesscounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductionDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValidityDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isgift;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
    }
}
