namespace HXCPcClient.UCForm.BusinessAnalysis.SaleReport
{
    partial class UCSaleProfitSummariz
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCSaleProfitSummariz));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtPartsName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtdrawing_num = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.cboorg_id = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cboCompany = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtparts_brand = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.cbowh_code = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtcparts_code = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txtCust_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.cboCust_type = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.txtcCust_code = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.cboorder_type = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dicreate_time = new ServiceStationClient.ComponentUI.DateInterval();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboIsMember = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtcPartsType = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txtcVehicleModels = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.dgvReport = new ServiceStationClient.ComponentUI.DataGridViewReport();
            this.chbGift = new System.Windows.Forms.CheckBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIncome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGrossMargin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSearch.SuspendLayout();
            this.pnlReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.chbGift);
            this.pnlSearch.Controls.Add(this.txtcVehicleModels);
            this.pnlSearch.Controls.Add(this.txtcPartsType);
            this.pnlSearch.Controls.Add(this.cboIsMember);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.txtPartsName);
            this.pnlSearch.Controls.Add(this.txtdrawing_num);
            this.pnlSearch.Controls.Add(this.cboorg_id);
            this.pnlSearch.Controls.Add(this.cboCompany);
            this.pnlSearch.Controls.Add(this.txtparts_brand);
            this.pnlSearch.Controls.Add(this.cbowh_code);
            this.pnlSearch.Controls.Add(this.txtcparts_code);
            this.pnlSearch.Controls.Add(this.txtCust_name);
            this.pnlSearch.Controls.Add(this.cboCust_type);
            this.pnlSearch.Controls.Add(this.label13);
            this.pnlSearch.Controls.Add(this.txtcCust_code);
            this.pnlSearch.Controls.Add(this.cboorder_type);
            this.pnlSearch.Controls.Add(this.label10);
            this.pnlSearch.Controls.Add(this.label14);
            this.pnlSearch.Controls.Add(this.label11);
            this.pnlSearch.Controls.Add(this.label12);
            this.pnlSearch.Controls.Add(this.dicreate_time);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.label8);
            this.pnlSearch.Controls.Add(this.label9);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Size = new System.Drawing.Size(1069, 151);
            this.pnlSearch.Controls.SetChildIndex(this.btnClear, 0);
            this.pnlSearch.Controls.SetChildIndex(this.btnSearch, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label5, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label1, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label9, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label8, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label6, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label7, 0);
            this.pnlSearch.Controls.SetChildIndex(this.dicreate_time, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label12, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label11, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label14, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label10, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboorder_type, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtcCust_code, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label13, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboCust_type, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtCust_name, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtcparts_code, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cbowh_code, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtparts_brand, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboCompany, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboorg_id, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtdrawing_num, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtPartsName, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label2, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label3, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label4, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboIsMember, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtcPartsType, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtcVehicleModels, 0);
            this.pnlSearch.Controls.SetChildIndex(this.chbGift, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlReport
            // 
            this.pnlReport.Controls.Add(this.dgvReport);
            this.pnlReport.Location = new System.Drawing.Point(3, 194);
            this.pnlReport.Size = new System.Drawing.Size(1069, 347);
            // 
            // txtPartsName
            // 
            this.txtPartsName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPartsName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPartsName.BackColor = System.Drawing.Color.Transparent;
            this.txtPartsName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtPartsName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtPartsName.ForeImage = null;
            this.txtPartsName.InputtingVerifyCondition = null;
            this.txtPartsName.Location = new System.Drawing.Point(349, 81);
            this.txtPartsName.MaxLengh = 32767;
            this.txtPartsName.Multiline = false;
            this.txtPartsName.Name = "txtPartsName";
            this.txtPartsName.Radius = 3;
            this.txtPartsName.ReadOnly = false;
            this.txtPartsName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsName.ShowError = false;
            this.txtPartsName.Size = new System.Drawing.Size(121, 23);
            this.txtPartsName.TabIndex = 171;
            this.txtPartsName.Tag = "配件名称";
            this.txtPartsName.UseSystemPasswordChar = false;
            this.txtPartsName.Value = "";
            this.txtPartsName.VerifyCondition = null;
            this.txtPartsName.VerifyType = null;
            this.txtPartsName.VerifyTypeName = null;
            this.txtPartsName.WaterMark = null;
            this.txtPartsName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtdrawing_num
            // 
            this.txtdrawing_num.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtdrawing_num.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtdrawing_num.BackColor = System.Drawing.Color.Transparent;
            this.txtdrawing_num.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtdrawing_num.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtdrawing_num.ForeImage = null;
            this.txtdrawing_num.InputtingVerifyCondition = null;
            this.txtdrawing_num.Location = new System.Drawing.Point(573, 81);
            this.txtdrawing_num.MaxLengh = 32767;
            this.txtdrawing_num.Multiline = false;
            this.txtdrawing_num.Name = "txtdrawing_num";
            this.txtdrawing_num.Radius = 3;
            this.txtdrawing_num.ReadOnly = false;
            this.txtdrawing_num.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtdrawing_num.ShowError = false;
            this.txtdrawing_num.Size = new System.Drawing.Size(123, 23);
            this.txtdrawing_num.TabIndex = 172;
            this.txtdrawing_num.Tag = "图号";
            this.txtdrawing_num.UseSystemPasswordChar = false;
            this.txtdrawing_num.Value = "";
            this.txtdrawing_num.VerifyCondition = null;
            this.txtdrawing_num.VerifyType = null;
            this.txtdrawing_num.VerifyTypeName = null;
            this.txtdrawing_num.WaterMark = null;
            this.txtdrawing_num.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // cboorg_id
            // 
            this.cboorg_id.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboorg_id.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboorg_id.FormattingEnabled = true;
            this.cboorg_id.Location = new System.Drawing.Point(790, 117);
            this.cboorg_id.Name = "cboorg_id";
            this.cboorg_id.Size = new System.Drawing.Size(121, 22);
            this.cboorg_id.TabIndex = 175;
            this.cboorg_id.Tag = "org_id";
            // 
            // cboCompany
            // 
            this.cboCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(575, 117);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(121, 22);
            this.cboCompany.TabIndex = 174;
            this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.cboCompany_SelectedIndexChanged);
            // 
            // txtparts_brand
            // 
            this.txtparts_brand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtparts_brand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtparts_brand.BackColor = System.Drawing.Color.Transparent;
            this.txtparts_brand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtparts_brand.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtparts_brand.ForeImage = null;
            this.txtparts_brand.InputtingVerifyCondition = null;
            this.txtparts_brand.Location = new System.Drawing.Point(790, 81);
            this.txtparts_brand.MaxLengh = 32767;
            this.txtparts_brand.Multiline = false;
            this.txtparts_brand.Name = "txtparts_brand";
            this.txtparts_brand.Radius = 3;
            this.txtparts_brand.ReadOnly = false;
            this.txtparts_brand.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtparts_brand.ShowError = false;
            this.txtparts_brand.Size = new System.Drawing.Size(121, 23);
            this.txtparts_brand.TabIndex = 173;
            this.txtparts_brand.Tag = "配件品牌";
            this.txtparts_brand.UseSystemPasswordChar = false;
            this.txtparts_brand.Value = "";
            this.txtparts_brand.VerifyCondition = null;
            this.txtparts_brand.VerifyType = null;
            this.txtparts_brand.VerifyTypeName = null;
            this.txtparts_brand.WaterMark = null;
            this.txtparts_brand.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // cbowh_code
            // 
            this.cbowh_code.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbowh_code.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbowh_code.FormattingEnabled = true;
            this.cbowh_code.Location = new System.Drawing.Point(349, 45);
            this.cbowh_code.Name = "cbowh_code";
            this.cbowh_code.Size = new System.Drawing.Size(121, 22);
            this.cbowh_code.TabIndex = 169;
            this.cbowh_code.Tag = "wh_code";
            // 
            // txtcparts_code
            // 
            this.txtcparts_code.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcparts_code.Location = new System.Drawing.Point(108, 80);
            this.txtcparts_code.Name = "txtcparts_code";
            this.txtcparts_code.ReadOnly = false;
            this.txtcparts_code.Size = new System.Drawing.Size(121, 24);
            this.txtcparts_code.TabIndex = 170;
            this.txtcparts_code.Tag = "配件编码";
            this.txtcparts_code.ToolTipTitle = "";
            this.txtcparts_code.ChooserClick += new System.EventHandler(this.txtcparts_code_ChooserClick);
            // 
            // txtCust_name
            // 
            this.txtCust_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCust_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCust_name.BackColor = System.Drawing.Color.Transparent;
            this.txtCust_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCust_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCust_name.ForeImage = null;
            this.txtCust_name.InputtingVerifyCondition = null;
            this.txtCust_name.Location = new System.Drawing.Point(349, 9);
            this.txtCust_name.MaxLengh = 32767;
            this.txtCust_name.Multiline = false;
            this.txtCust_name.Name = "txtCust_name";
            this.txtCust_name.Radius = 3;
            this.txtCust_name.ReadOnly = false;
            this.txtCust_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCust_name.ShowError = false;
            this.txtCust_name.Size = new System.Drawing.Size(121, 23);
            this.txtCust_name.TabIndex = 167;
            this.txtCust_name.Tag = "客户名称";
            this.txtCust_name.UseSystemPasswordChar = false;
            this.txtCust_name.Value = "";
            this.txtCust_name.VerifyCondition = null;
            this.txtCust_name.VerifyType = null;
            this.txtCust_name.VerifyTypeName = null;
            this.txtCust_name.WaterMark = null;
            this.txtCust_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // cboCust_type
            // 
            this.cboCust_type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCust_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCust_type.FormattingEnabled = true;
            this.cboCust_type.Location = new System.Drawing.Point(573, 9);
            this.cboCust_type.Name = "cboCust_type";
            this.cboCust_type.Size = new System.Drawing.Size(123, 22);
            this.cboCust_type.TabIndex = 168;
            this.cboCust_type.Tag = "cust_type";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(526, 122);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 159;
            this.label13.Text = "公司：";
            // 
            // txtcCust_code
            // 
            this.txtcCust_code.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcCust_code.Location = new System.Drawing.Point(108, 8);
            this.txtcCust_code.Name = "txtcCust_code";
            this.txtcCust_code.ReadOnly = false;
            this.txtcCust_code.Size = new System.Drawing.Size(121, 24);
            this.txtcCust_code.TabIndex = 166;
            this.txtcCust_code.Tag = "客户编码";
            this.txtcCust_code.ToolTipTitle = "";
            this.txtcCust_code.ChooserClick += new System.EventHandler(this.txtcCust_code_ChooserClick);
            // 
            // cboorder_type
            // 
            this.cboorder_type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboorder_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboorder_type.FormattingEnabled = true;
            this.cboorder_type.Location = new System.Drawing.Point(108, 45);
            this.cboorder_type.Name = "cboorder_type";
            this.cboorder_type.Size = new System.Drawing.Size(121, 22);
            this.cboorder_type.TabIndex = 162;
            this.cboorder_type.Tag = "order_type";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(278, 86);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 156;
            this.label10.Text = "配件名称：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(743, 122);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 160;
            this.label14.Text = "部门：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(502, 86);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 157;
            this.label11.Text = "配件图号：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(743, 86);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 158;
            this.label12.Text = "品牌：";
            // 
            // dicreate_time
            // 
            this.dicreate_time.BackColor = System.Drawing.Color.Transparent;
            this.dicreate_time.EndDate = "2015-01-15";
            this.dicreate_time.Location = new System.Drawing.Point(44, 114);
            this.dicreate_time.Name = "dicreate_time";
            this.dicreate_time.ShowFormat = "yyyy-MM-dd";
            this.dicreate_time.Size = new System.Drawing.Size(411, 28);
            this.dicreate_time.StartDate = "2015-01-01";
            this.dicreate_time.TabIndex = 161;
            this.dicreate_time.Tag = "单据日期";
            this.dicreate_time.Text = "日期从：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(502, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 153;
            this.label7.Text = "客户类别：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(278, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 152;
            this.label6.Text = "客户名称：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(302, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 154;
            this.label8.Text = "仓库：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 86);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 155;
            this.label9.Text = "配件编码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 147;
            this.label1.Text = "单据类型：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 151;
            this.label5.Text = "客户编码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(719, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 176;
            this.label2.Text = "是否会员：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(502, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 177;
            this.label3.Text = "配件类型：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(719, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 178;
            this.label4.Text = "配件车型：";
            // 
            // cboIsMember
            // 
            this.cboIsMember.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboIsMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIsMember.FormattingEnabled = true;
            this.cboIsMember.Location = new System.Drawing.Point(790, 9);
            this.cboIsMember.Name = "cboIsMember";
            this.cboIsMember.Size = new System.Drawing.Size(121, 22);
            this.cboIsMember.TabIndex = 179;
            // 
            // txtcPartsType
            // 
            this.txtcPartsType.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcPartsType.Location = new System.Drawing.Point(573, 44);
            this.txtcPartsType.Name = "txtcPartsType";
            this.txtcPartsType.ReadOnly = false;
            this.txtcPartsType.Size = new System.Drawing.Size(123, 24);
            this.txtcPartsType.TabIndex = 180;
            this.txtcPartsType.ToolTipTitle = "";
            this.txtcPartsType.ChooserClick += new System.EventHandler(this.txtcPartsType_ChooserClick);
            // 
            // txtcVehicleModels
            // 
            this.txtcVehicleModels.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcVehicleModels.Location = new System.Drawing.Point(790, 44);
            this.txtcVehicleModels.Name = "txtcVehicleModels";
            this.txtcVehicleModels.ReadOnly = false;
            this.txtcVehicleModels.Size = new System.Drawing.Size(121, 24);
            this.txtcVehicleModels.TabIndex = 181;
            this.txtcVehicleModels.ToolTipTitle = "";
            this.txtcVehicleModels.ChooserClick += new System.EventHandler(this.txtcVehicleModels_ChooserClick);
            // 
            // dgvReport
            // 
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(253)))), ((int)(((byte)(252)))));
            this.dgvReport.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.colIncome,
            this.colCost,
            this.colGrossMargin,
            this.colRate});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReport.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.EnableHeadersVisualStyles = false;
            this.dgvReport.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvReport.Location = new System.Drawing.Point(0, 0);
            this.dgvReport.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvReport.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvReport.MergeColumnNames")));
            this.dgvReport.MultiSelect = false;
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvReport.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvReport.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(234)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvReport.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvReport.RowTemplate.Height = 23;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(1069, 347);
            this.dgvReport.TabIndex = 0;
            this.dgvReport.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReport_CellDoubleClick);
            // 
            // chbGift
            // 
            this.chbGift.AutoSize = true;
            this.chbGift.Location = new System.Drawing.Point(939, 44);
            this.chbGift.Name = "chbGift";
            this.chbGift.Size = new System.Drawing.Size(72, 16);
            this.chbGift.TabIndex = 212;
            this.chbGift.Text = "包括赠品";
            this.chbGift.UseVisualStyleBackColor = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "客户编码";
            this.Column1.HeaderText = "客户编码";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "客户名称";
            this.Column2.HeaderText = "客户名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // colIncome
            // 
            this.colIncome.DataPropertyName = "销售收入";
            this.colIncome.HeaderText = "销售收入（本）";
            this.colIncome.Name = "colIncome";
            this.colIncome.ReadOnly = true;
            // 
            // colCost
            // 
            this.colCost.DataPropertyName = "销售成本";
            this.colCost.HeaderText = "销售成本（本）";
            this.colCost.Name = "colCost";
            this.colCost.ReadOnly = true;
            // 
            // colGrossMargin
            // 
            this.colGrossMargin.DataPropertyName = "销售毛利";
            this.colGrossMargin.HeaderText = "销售毛利（本）";
            this.colGrossMargin.Name = "colGrossMargin";
            this.colGrossMargin.ReadOnly = true;
            // 
            // colRate
            // 
            this.colRate.DataPropertyName = "销售毛利率";
            this.colRate.HeaderText = "销售毛利率";
            this.colRate.Name = "colRate";
            this.colRate.ReadOnly = true;
            // 
            // UCSaleProfitSummariz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCSaleProfitSummariz";
            this.Load += new System.EventHandler(this.UCSaleProfitSummariz_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.TextBoxEx txtPartsName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtdrawing_num;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboorg_id;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboCompany;
        private ServiceStationClient.ComponentUI.TextBoxEx txtparts_brand;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbowh_code;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtcparts_code;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCust_name;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboCust_type;
        private System.Windows.Forms.Label label13;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtcCust_code;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboorder_type;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private ServiceStationClient.ComponentUI.DateInterval dicreate_time;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtcVehicleModels;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtcPartsType;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboIsMember;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.DataGridViewReport dgvReport;
        private System.Windows.Forms.CheckBox chbGift;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIncome;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGrossMargin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRate;
    }
}
