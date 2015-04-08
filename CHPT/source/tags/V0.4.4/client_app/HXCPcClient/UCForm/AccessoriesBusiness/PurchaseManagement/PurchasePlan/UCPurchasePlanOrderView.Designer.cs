namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchasePlan
{
    partial class UCPurchasePlanOrderView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPurchasePlanOrderView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chkis_suspend = new System.Windows.Forms.CheckBox();
            this.lblorder_status_name = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblorder_num = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblorder_date = new System.Windows.Forms.Label();
            this.lblplan_start_time = new System.Windows.Forms.Label();
            this.lblplan_end_time = new System.Windows.Forms.Label();
            this.lblplan_money = new System.Windows.Forms.Label();
            this.lblplan_finish_money = new System.Windows.Forms.Label();
            this.lblremark = new System.Windows.Forms.Label();
            this.panelEx1 = new HXC.UI.Library.Controls.ExtUserControl();
            this.lblhandle_name = new System.Windows.Forms.Label();
            this.lblorg_name = new System.Windows.Forms.Label();
            this.lblupdate_time = new System.Windows.Forms.Label();
            this.lblupdate_name = new System.Windows.Forms.Label();
            this.lblcreate_time = new System.Windows.Forms.Label();
            this.lblcreate_name = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbloperator_name = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gvPurchasePlanList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.parts_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drawing_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_brand_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.business_counts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.original_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.business_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_factory_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recent_supplier_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recent_purchase_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recent_purchase_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.finish_counts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.relation_order = new System.Windows.Forms.DataGridViewLinkColumn();
            this.is_suspend = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtsuspend_reason = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.panelEx2 = new HXC.UI.Library.Controls.ExtUserControl();
            this.panelEx3 = new HXC.UI.Library.Controls.ExtUserControl();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchasePlanList)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1023, 25);
            // 
            // chkis_suspend
            // 
            this.chkis_suspend.AutoSize = true;
            this.chkis_suspend.Location = new System.Drawing.Point(850, 84);
            this.chkis_suspend.Name = "chkis_suspend";
            this.chkis_suspend.Size = new System.Drawing.Size(48, 16);
            this.chkis_suspend.TabIndex = 172;
            this.chkis_suspend.Text = "中止";
            this.chkis_suspend.UseVisualStyleBackColor = true;
            this.chkis_suspend.Click += new System.EventHandler(this.chkis_suspend_Click);
            // 
            // lblorder_status_name
            // 
            this.lblorder_status_name.AutoSize = true;
            this.lblorder_status_name.Location = new System.Drawing.Point(596, 18);
            this.lblorder_status_name.Name = "lblorder_status_name";
            this.lblorder_status_name.Size = new System.Drawing.Size(11, 12);
            this.lblorder_status_name.TabIndex = 169;
            this.lblorder_status_name.Text = ".";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(525, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 168;
            this.label11.Text = "单据状态：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblorder_num
            // 
            this.lblorder_num.AutoSize = true;
            this.lblorder_num.Location = new System.Drawing.Point(100, 18);
            this.lblorder_num.Name = "lblorder_num";
            this.lblorder_num.Size = new System.Drawing.Size(0, 12);
            this.lblorder_num.TabIndex = 165;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(57, 121);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 12);
            this.label16.TabIndex = 161;
            this.label16.Text = "备注：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(777, 50);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 12);
            this.label18.TabIndex = 162;
            this.label18.Text = "完成金额：";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 159;
            this.label5.Text = "中止原因：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(525, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 158;
            this.label4.Text = "计划金额：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(262, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 160;
            this.label10.Text = "单据日期：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 156;
            this.label2.Text = "计划区间从：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(290, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 157;
            this.label3.Text = "至";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 155;
            this.label1.Text = "采购计划单号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblorder_date
            // 
            this.lblorder_date.AutoSize = true;
            this.lblorder_date.Location = new System.Drawing.Point(333, 15);
            this.lblorder_date.Name = "lblorder_date";
            this.lblorder_date.Size = new System.Drawing.Size(11, 12);
            this.lblorder_date.TabIndex = 175;
            this.lblorder_date.Text = ".";
            // 
            // lblplan_start_time
            // 
            this.lblplan_start_time.AutoSize = true;
            this.lblplan_start_time.Location = new System.Drawing.Point(100, 50);
            this.lblplan_start_time.Name = "lblplan_start_time";
            this.lblplan_start_time.Size = new System.Drawing.Size(11, 12);
            this.lblplan_start_time.TabIndex = 176;
            this.lblplan_start_time.Text = ".";
            // 
            // lblplan_end_time
            // 
            this.lblplan_end_time.AutoSize = true;
            this.lblplan_end_time.Location = new System.Drawing.Point(333, 50);
            this.lblplan_end_time.Name = "lblplan_end_time";
            this.lblplan_end_time.Size = new System.Drawing.Size(11, 12);
            this.lblplan_end_time.TabIndex = 177;
            this.lblplan_end_time.Text = ".";
            // 
            // lblplan_money
            // 
            this.lblplan_money.AutoSize = true;
            this.lblplan_money.Location = new System.Drawing.Point(596, 50);
            this.lblplan_money.Name = "lblplan_money";
            this.lblplan_money.Size = new System.Drawing.Size(11, 12);
            this.lblplan_money.TabIndex = 178;
            this.lblplan_money.Text = ".";
            // 
            // lblplan_finish_money
            // 
            this.lblplan_finish_money.AutoSize = true;
            this.lblplan_finish_money.Location = new System.Drawing.Point(848, 50);
            this.lblplan_finish_money.Name = "lblplan_finish_money";
            this.lblplan_finish_money.Size = new System.Drawing.Size(11, 12);
            this.lblplan_finish_money.TabIndex = 179;
            this.lblplan_finish_money.Text = ".";
            // 
            // lblremark
            // 
            this.lblremark.AutoSize = true;
            this.lblremark.Location = new System.Drawing.Point(104, 121);
            this.lblremark.Name = "lblremark";
            this.lblremark.Size = new System.Drawing.Size(11, 12);
            this.lblremark.TabIndex = 181;
            this.lblremark.Text = ".";
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Content = null;
            this.panelEx1.ContentTypeName = null;
            this.panelEx1.ContentTypeParameter = null;
            this.panelEx1.Controls.Add(this.lblhandle_name);
            this.panelEx1.Controls.Add(this.lblorg_name);
            this.panelEx1.Controls.Add(this.lblupdate_time);
            this.panelEx1.Controls.Add(this.lblupdate_name);
            this.panelEx1.Controls.Add(this.lblcreate_time);
            this.panelEx1.Controls.Add(this.lblcreate_name);
            this.panelEx1.Controls.Add(this.label15);
            this.panelEx1.Controls.Add(this.label14);
            this.panelEx1.Controls.Add(this.label13);
            this.panelEx1.Controls.Add(this.label12);
            this.panelEx1.Controls.Add(this.lbloperator_name);
            this.panelEx1.Controls.Add(this.label8);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.CornerRadiu = 5;
            this.panelEx1.DisplayValue = "";
            this.panelEx1.InputtingVerifyCondition = null;
            this.panelEx1.Location = new System.Drawing.Point(3, 430);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.ShowError = false;
            this.panelEx1.Size = new System.Drawing.Size(1017, 66);
            this.panelEx1.TabIndex = 183;
            this.panelEx1.Value = null;
            this.panelEx1.VerifyCondition = null;
            this.panelEx1.VerifyType = null;
            this.panelEx1.VerifyTypeName = null;
            // 
            // lblhandle_name
            // 
            this.lblhandle_name.AutoSize = true;
            this.lblhandle_name.Location = new System.Drawing.Point(605, 13);
            this.lblhandle_name.Name = "lblhandle_name";
            this.lblhandle_name.Size = new System.Drawing.Size(11, 12);
            this.lblhandle_name.TabIndex = 121;
            this.lblhandle_name.Text = ".";
            // 
            // lblorg_name
            // 
            this.lblorg_name.AutoSize = true;
            this.lblorg_name.Location = new System.Drawing.Point(391, 13);
            this.lblorg_name.Name = "lblorg_name";
            this.lblorg_name.Size = new System.Drawing.Size(11, 12);
            this.lblorg_name.TabIndex = 120;
            this.lblorg_name.Text = ".";
            // 
            // lblupdate_time
            // 
            this.lblupdate_time.AutoSize = true;
            this.lblupdate_time.Location = new System.Drawing.Point(834, 35);
            this.lblupdate_time.Name = "lblupdate_time";
            this.lblupdate_time.Size = new System.Drawing.Size(11, 12);
            this.lblupdate_time.TabIndex = 119;
            this.lblupdate_time.Text = ".";
            // 
            // lblupdate_name
            // 
            this.lblupdate_name.AutoSize = true;
            this.lblupdate_name.Location = new System.Drawing.Point(605, 35);
            this.lblupdate_name.Name = "lblupdate_name";
            this.lblupdate_name.Size = new System.Drawing.Size(11, 12);
            this.lblupdate_name.TabIndex = 118;
            this.lblupdate_name.Text = ".";
            // 
            // lblcreate_time
            // 
            this.lblcreate_time.AutoSize = true;
            this.lblcreate_time.Location = new System.Drawing.Point(396, 35);
            this.lblcreate_time.Name = "lblcreate_time";
            this.lblcreate_time.Size = new System.Drawing.Size(11, 12);
            this.lblcreate_time.TabIndex = 117;
            this.lblcreate_time.Text = ".";
            // 
            // lblcreate_name
            // 
            this.lblcreate_name.AutoSize = true;
            this.lblcreate_name.Location = new System.Drawing.Point(200, 35);
            this.lblcreate_name.Name = "lblcreate_name";
            this.lblcreate_name.Size = new System.Drawing.Size(11, 12);
            this.lblcreate_name.TabIndex = 116;
            this.lblcreate_name.Text = ".";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(739, 35);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 12);
            this.label15.TabIndex = 115;
            this.label15.Text = "最后编辑时间：";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(522, 35);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 12);
            this.label14.TabIndex = 114;
            this.label14.Text = "最后编辑人：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(325, 35);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 113;
            this.label13.Text = "创建时间：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(141, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 112;
            this.label12.Text = "创建人：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbloperator_name
            // 
            this.lbloperator_name.AutoSize = true;
            this.lbloperator_name.Location = new System.Drawing.Point(834, 13);
            this.lbloperator_name.Name = "lbloperator_name";
            this.lbloperator_name.Size = new System.Drawing.Size(11, 12);
            this.lbloperator_name.TabIndex = 111;
            this.lbloperator_name.Text = ".";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(775, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 110;
            this.label8.Text = "操作人：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(546, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "经办人：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(349, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "部门：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gvPurchasePlanList
            // 
            this.gvPurchasePlanList.AllowUserToAddRows = false;
            this.gvPurchasePlanList.AllowUserToDeleteRows = false;
            this.gvPurchasePlanList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gvPurchasePlanList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvPurchasePlanList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvPurchasePlanList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvPurchasePlanList.BackgroundColor = System.Drawing.Color.White;
            this.gvPurchasePlanList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPurchasePlanList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvPurchasePlanList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPurchasePlanList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.parts_code,
            this.parts_name,
            this.drawing_num,
            this.unit_name,
            this.parts_brand_name,
            this.business_counts,
            this.original_price,
            this.discount,
            this.business_price,
            this.total_money,
            this.parts_barcode,
            this.car_factory_code,
            this.recent_supplier_code,
            this.recent_purchase_name,
            this.recent_purchase_price,
            this.finish_counts,
            this.relation_order,
            this.is_suspend,
            this.remark,
            this.create_by,
            this.create_time,
            this.create_name});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPurchasePlanList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvPurchasePlanList.EnableHeadersVisualStyles = false;
            this.gvPurchasePlanList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.gvPurchasePlanList.IsCheck = true;
            this.gvPurchasePlanList.Location = new System.Drawing.Point(3, 2);
            this.gvPurchasePlanList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gvPurchasePlanList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gvPurchasePlanList.MergeColumnNames")));
            this.gvPurchasePlanList.MultiSelect = false;
            this.gvPurchasePlanList.Name = "gvPurchasePlanList";
            this.gvPurchasePlanList.ReadOnly = true;
            this.gvPurchasePlanList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.gvPurchasePlanList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvPurchasePlanList.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.gvPurchasePlanList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvPurchasePlanList.RowTemplate.Height = 23;
            this.gvPurchasePlanList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPurchasePlanList.ShowCheckBox = true;
            this.gvPurchasePlanList.Size = new System.Drawing.Size(1010, 240);
            this.gvPurchasePlanList.TabIndex = 147;
            this.gvPurchasePlanList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPurchasePlanList_CellContentClick);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 30;
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
            this.parts_name.HeaderText = "名称";
            this.parts_name.MinimumWidth = 100;
            this.parts_name.Name = "parts_name";
            this.parts_name.ReadOnly = true;
            // 
            // drawing_num
            // 
            this.drawing_num.DataPropertyName = "drawing_num";
            this.drawing_num.HeaderText = "图号";
            this.drawing_num.MinimumWidth = 100;
            this.drawing_num.Name = "drawing_num";
            this.drawing_num.ReadOnly = true;
            // 
            // unit_name
            // 
            this.unit_name.DataPropertyName = "unit_name";
            this.unit_name.HeaderText = "单位";
            this.unit_name.MinimumWidth = 100;
            this.unit_name.Name = "unit_name";
            this.unit_name.ReadOnly = true;
            this.unit_name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // parts_brand_name
            // 
            this.parts_brand_name.DataPropertyName = "parts_brand_name";
            this.parts_brand_name.HeaderText = "品牌";
            this.parts_brand_name.MinimumWidth = 100;
            this.parts_brand_name.Name = "parts_brand_name";
            this.parts_brand_name.ReadOnly = true;
            // 
            // business_counts
            // 
            this.business_counts.DataPropertyName = "business_counts";
            this.business_counts.HeaderText = "业务数量";
            this.business_counts.MinimumWidth = 100;
            this.business_counts.Name = "business_counts";
            this.business_counts.ReadOnly = true;
            // 
            // original_price
            // 
            this.original_price.DataPropertyName = "original_price";
            this.original_price.HeaderText = "原始单价";
            this.original_price.MinimumWidth = 100;
            this.original_price.Name = "original_price";
            this.original_price.ReadOnly = true;
            // 
            // discount
            // 
            this.discount.DataPropertyName = "discount";
            this.discount.HeaderText = "折扣(%)";
            this.discount.MinimumWidth = 100;
            this.discount.Name = "discount";
            this.discount.ReadOnly = true;
            // 
            // business_price
            // 
            this.business_price.DataPropertyName = "business_price";
            this.business_price.HeaderText = "业务单价";
            this.business_price.MinimumWidth = 100;
            this.business_price.Name = "business_price";
            this.business_price.ReadOnly = true;
            // 
            // total_money
            // 
            this.total_money.DataPropertyName = "total_money";
            this.total_money.HeaderText = "金额";
            this.total_money.MinimumWidth = 100;
            this.total_money.Name = "total_money";
            this.total_money.ReadOnly = true;
            // 
            // parts_barcode
            // 
            this.parts_barcode.DataPropertyName = "parts_barcode";
            this.parts_barcode.HeaderText = "条码";
            this.parts_barcode.MinimumWidth = 100;
            this.parts_barcode.Name = "parts_barcode";
            this.parts_barcode.ReadOnly = true;
            // 
            // car_factory_code
            // 
            this.car_factory_code.DataPropertyName = "car_factory_code";
            this.car_factory_code.HeaderText = "厂商编码";
            this.car_factory_code.MinimumWidth = 100;
            this.car_factory_code.Name = "car_factory_code";
            this.car_factory_code.ReadOnly = true;
            // 
            // recent_supplier_code
            // 
            this.recent_supplier_code.DataPropertyName = "recent_supplier_code";
            this.recent_supplier_code.HeaderText = "最后一次供应商编码";
            this.recent_supplier_code.MinimumWidth = 200;
            this.recent_supplier_code.Name = "recent_supplier_code";
            this.recent_supplier_code.ReadOnly = true;
            this.recent_supplier_code.Width = 200;
            // 
            // recent_purchase_name
            // 
            this.recent_purchase_name.DataPropertyName = "recent_purchase_name";
            this.recent_purchase_name.HeaderText = "最后一次供应商名称";
            this.recent_purchase_name.MinimumWidth = 200;
            this.recent_purchase_name.Name = "recent_purchase_name";
            this.recent_purchase_name.ReadOnly = true;
            this.recent_purchase_name.Width = 200;
            // 
            // recent_purchase_price
            // 
            this.recent_purchase_price.DataPropertyName = "recent_purchase_price";
            this.recent_purchase_price.HeaderText = "最后一次进货价格";
            this.recent_purchase_price.MinimumWidth = 200;
            this.recent_purchase_price.Name = "recent_purchase_price";
            this.recent_purchase_price.ReadOnly = true;
            this.recent_purchase_price.Width = 200;
            // 
            // finish_counts
            // 
            this.finish_counts.DataPropertyName = "finish_counts";
            this.finish_counts.HeaderText = "完成数量";
            this.finish_counts.MinimumWidth = 100;
            this.finish_counts.Name = "finish_counts";
            this.finish_counts.ReadOnly = true;
            // 
            // relation_order
            // 
            this.relation_order.DataPropertyName = "relation_order";
            this.relation_order.HeaderText = "关联单号";
            this.relation_order.MinimumWidth = 100;
            this.relation_order.Name = "relation_order";
            this.relation_order.ReadOnly = true;
            this.relation_order.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.relation_order.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.relation_order.Text = "查看";
            this.relation_order.UseColumnTextForLinkValue = true;
            // 
            // is_suspend
            // 
            this.is_suspend.DataPropertyName = "is_suspend";
            this.is_suspend.FalseValue = "1";
            this.is_suspend.HeaderText = "中止";
            this.is_suspend.MinimumWidth = 100;
            this.is_suspend.Name = "is_suspend";
            this.is_suspend.ReadOnly = true;
            this.is_suspend.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.is_suspend.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.is_suspend.TrueValue = "0";
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.MinimumWidth = 100;
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            // 
            // create_by
            // 
            this.create_by.HeaderText = "create_by";
            this.create_by.Name = "create_by";
            this.create_by.ReadOnly = true;
            this.create_by.Visible = false;
            this.create_by.Width = 90;
            // 
            // create_time
            // 
            this.create_time.HeaderText = "create_time";
            this.create_time.Name = "create_time";
            this.create_time.ReadOnly = true;
            this.create_time.Visible = false;
            this.create_time.Width = 103;
            // 
            // create_name
            // 
            this.create_name.HeaderText = "create_name";
            this.create_name.Name = "create_name";
            this.create_name.ReadOnly = true;
            this.create_name.Visible = false;
            this.create_name.Width = 109;
            // 
            // txtsuspend_reason
            // 
            this.txtsuspend_reason.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtsuspend_reason.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtsuspend_reason.BackColor = System.Drawing.Color.Transparent;
            this.txtsuspend_reason.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtsuspend_reason.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtsuspend_reason.Enabled = false;
            this.txtsuspend_reason.ForeImage = null;
            this.txtsuspend_reason.InputtingVerifyCondition = null;
            this.txtsuspend_reason.Location = new System.Drawing.Point(102, 83);
            this.txtsuspend_reason.MaxLengh = 32767;
            this.txtsuspend_reason.Multiline = false;
            this.txtsuspend_reason.Name = "txtsuspend_reason";
            this.txtsuspend_reason.Radius = 3;
            this.txtsuspend_reason.ReadOnly = false;
            this.txtsuspend_reason.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtsuspend_reason.ShowError = false;
            this.txtsuspend_reason.Size = new System.Drawing.Size(668, 23);
            this.txtsuspend_reason.TabIndex = 184;
            this.txtsuspend_reason.UseSystemPasswordChar = false;
            this.txtsuspend_reason.Value = "";
            this.txtsuspend_reason.VerifyCondition = null;
            this.txtsuspend_reason.VerifyType = null;
            this.txtsuspend_reason.VerifyTypeName = null;
            this.txtsuspend_reason.WaterMark = null;
            this.txtsuspend_reason.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // panelEx2
            // 
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Content = null;
            this.panelEx2.ContentTypeName = null;
            this.panelEx2.ContentTypeParameter = null;
            this.panelEx2.Controls.Add(this.gvPurchasePlanList);
            this.panelEx2.CornerRadiu = 5;
            this.panelEx2.DisplayValue = "";
            this.panelEx2.InputtingVerifyCondition = null;
            this.panelEx2.Location = new System.Drawing.Point(3, 181);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.ShowError = false;
            this.panelEx2.Size = new System.Drawing.Size(1017, 245);
            this.panelEx2.TabIndex = 185;
            this.panelEx2.Value = null;
            this.panelEx2.VerifyCondition = null;
            this.panelEx2.VerifyType = null;
            this.panelEx2.VerifyTypeName = null;
            // 
            // panelEx3
            // 
            this.panelEx3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx3.BorderWidth = 1;
            this.panelEx3.Content = null;
            this.panelEx3.ContentTypeName = null;
            this.panelEx3.ContentTypeParameter = null;
            this.panelEx3.Controls.Add(this.label5);
            this.panelEx3.Controls.Add(this.label3);
            this.panelEx3.Controls.Add(this.label1);
            this.panelEx3.Controls.Add(this.txtsuspend_reason);
            this.panelEx3.Controls.Add(this.label4);
            this.panelEx3.Controls.Add(this.label2);
            this.panelEx3.Controls.Add(this.label18);
            this.panelEx3.Controls.Add(this.label16);
            this.panelEx3.Controls.Add(this.lblremark);
            this.panelEx3.Controls.Add(this.lblorder_num);
            this.panelEx3.Controls.Add(this.label10);
            this.panelEx3.Controls.Add(this.label11);
            this.panelEx3.Controls.Add(this.lblplan_finish_money);
            this.panelEx3.Controls.Add(this.lblorder_status_name);
            this.panelEx3.Controls.Add(this.lblplan_money);
            this.panelEx3.Controls.Add(this.chkis_suspend);
            this.panelEx3.Controls.Add(this.lblplan_end_time);
            this.panelEx3.Controls.Add(this.lblorder_date);
            this.panelEx3.Controls.Add(this.lblplan_start_time);
            this.panelEx3.CornerRadiu = 5;
            this.panelEx3.DisplayValue = "";
            this.panelEx3.InputtingVerifyCondition = null;
            this.panelEx3.Location = new System.Drawing.Point(3, 28);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.ShowError = false;
            this.panelEx3.Size = new System.Drawing.Size(1017, 150);
            this.panelEx3.TabIndex = 186;
            this.panelEx3.Value = null;
            this.panelEx3.VerifyCondition = null;
            this.panelEx3.VerifyType = null;
            this.panelEx3.VerifyTypeName = null;
            // 
            // UCPurchasePlanOrderView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.panelEx3);
            this.Name = "UCPurchasePlanOrderView";
            this.Size = new System.Drawing.Size(1023, 502);
            this.Controls.SetChildIndex(this.panelEx3, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchasePlanList)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.panelEx3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkis_suspend;
        private System.Windows.Forms.Label lblorder_status_name;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblorder_num;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblorder_date;
        private System.Windows.Forms.Label lblplan_start_time;
        private System.Windows.Forms.Label lblplan_end_time;
        private System.Windows.Forms.Label lblplan_money;
        private System.Windows.Forms.Label lblplan_finish_money;
        private System.Windows.Forms.Label lblremark;
        private HXC.UI.Library.Controls.ExtUserControl panelEx1;
        private System.Windows.Forms.Label lblupdate_time;
        private System.Windows.Forms.Label lblupdate_name;
        private System.Windows.Forms.Label lblcreate_time;
        private System.Windows.Forms.Label lblcreate_name;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbloperator_name;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblhandle_name;
        private System.Windows.Forms.Label lblorg_name;
        private ServiceStationClient.ComponentUI.DataGridViewEx gvPurchasePlanList;
        private ServiceStationClient.ComponentUI.TextBoxEx txtsuspend_reason;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn drawing_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_brand_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn business_counts;
        private System.Windows.Forms.DataGridViewTextBoxColumn original_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn business_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_money;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_factory_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn recent_supplier_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn recent_purchase_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn recent_purchase_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn finish_counts;
        private System.Windows.Forms.DataGridViewLinkColumn relation_order;
        private System.Windows.Forms.DataGridViewCheckBoxColumn is_suspend;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_by;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_name;
        private HXC.UI.Library.Controls.ExtUserControl panelEx2;
        private HXC.UI.Library.Controls.ExtUserControl panelEx3;
    }
}
