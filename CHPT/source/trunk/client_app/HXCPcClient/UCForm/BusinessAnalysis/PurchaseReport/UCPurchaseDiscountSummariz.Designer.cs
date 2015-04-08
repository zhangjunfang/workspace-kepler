namespace HXCPcClient.UCForm.BusinessAnalysis.PurchaseReport
{
    partial class UCPurchaseDiscountSummariz
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPurchaseDiscountSummariz));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtPartsName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtdrawing_num = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.cboorg_id = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cboCompany = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtparts_brand = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.cbowh_code = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtcparts_code = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txtsup_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.cbosup_type = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cbobalance_way = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cbobalance_account = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.txtcsup_code = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.cboreceipt_type = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cboorder_type = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dicreate_time = new ServiceStationClient.ComponentUI.DateInterval();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvReport = new ServiceStationClient.ComponentUI.DataGridViewReport();
            this.colPartsCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDrawingNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartsBrand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiscountPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiscountMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSearch.SuspendLayout();
            this.pnlReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.txtPartsName);
            this.pnlSearch.Controls.Add(this.txtdrawing_num);
            this.pnlSearch.Controls.Add(this.cboorg_id);
            this.pnlSearch.Controls.Add(this.cboCompany);
            this.pnlSearch.Controls.Add(this.txtparts_brand);
            this.pnlSearch.Controls.Add(this.cbowh_code);
            this.pnlSearch.Controls.Add(this.txtcparts_code);
            this.pnlSearch.Controls.Add(this.txtsup_name);
            this.pnlSearch.Controls.Add(this.cbosup_type);
            this.pnlSearch.Controls.Add(this.cbobalance_way);
            this.pnlSearch.Controls.Add(this.cbobalance_account);
            this.pnlSearch.Controls.Add(this.label13);
            this.pnlSearch.Controls.Add(this.txtcsup_code);
            this.pnlSearch.Controls.Add(this.cboreceipt_type);
            this.pnlSearch.Controls.Add(this.cboorder_type);
            this.pnlSearch.Controls.Add(this.label10);
            this.pnlSearch.Controls.Add(this.label14);
            this.pnlSearch.Controls.Add(this.label11);
            this.pnlSearch.Controls.Add(this.label12);
            this.pnlSearch.Controls.Add(this.dicreate_time);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.label8);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.label9);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Size = new System.Drawing.Size(1069, 153);
            this.pnlSearch.Controls.SetChildIndex(this.btnClear, 0);
            this.pnlSearch.Controls.SetChildIndex(this.btnSearch, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label5, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label1, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label2, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label4, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label9, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label3, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label8, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label6, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label7, 0);
            this.pnlSearch.Controls.SetChildIndex(this.dicreate_time, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label12, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label11, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label14, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label10, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboorder_type, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboreceipt_type, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtcsup_code, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label13, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cbobalance_account, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cbobalance_way, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cbosup_type, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtsup_name, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtcparts_code, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cbowh_code, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtparts_brand, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboCompany, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboorg_id, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtdrawing_num, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtPartsName, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlReport
            // 
            this.pnlReport.Controls.Add(this.dgvReport);
            this.pnlReport.Location = new System.Drawing.Point(3, 196);
            this.pnlReport.Size = new System.Drawing.Size(1069, 345);
            // 
            // txtPartsName
            // 
            this.txtPartsName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPartsName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPartsName.BackColor = System.Drawing.Color.Transparent;
            this.txtPartsName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtPartsName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtPartsName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPartsName.ForeImage = null;
            this.txtPartsName.InputtingVerifyCondition = null;
            this.txtPartsName.Location = new System.Drawing.Point(346, 81);
            this.txtPartsName.MaxLengh = 32767;
            this.txtPartsName.Multiline = false;
            this.txtPartsName.Name = "txtPartsName";
            this.txtPartsName.Radius = 3;
            this.txtPartsName.ReadOnly = false;
            this.txtPartsName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsName.ShowError = false;
            this.txtPartsName.Size = new System.Drawing.Size(121, 23);
            this.txtPartsName.TabIndex = 84;
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
            this.txtdrawing_num.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtdrawing_num.ForeImage = null;
            this.txtdrawing_num.InputtingVerifyCondition = null;
            this.txtdrawing_num.Location = new System.Drawing.Point(572, 81);
            this.txtdrawing_num.MaxLengh = 32767;
            this.txtdrawing_num.Multiline = false;
            this.txtdrawing_num.Name = "txtdrawing_num";
            this.txtdrawing_num.Radius = 3;
            this.txtdrawing_num.ReadOnly = false;
            this.txtdrawing_num.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtdrawing_num.ShowError = false;
            this.txtdrawing_num.Size = new System.Drawing.Size(126, 23);
            this.txtdrawing_num.TabIndex = 85;
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
            this.cboorg_id.Location = new System.Drawing.Point(787, 117);
            this.cboorg_id.Name = "cboorg_id";
            this.cboorg_id.Size = new System.Drawing.Size(121, 22);
            this.cboorg_id.TabIndex = 88;
            this.cboorg_id.Tag = "org_id";
            // 
            // cboCompany
            // 
            this.cboCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(572, 117);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(121, 22);
            this.cboCompany.TabIndex = 87;
            this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.cboCompany_SelectedIndexChanged);
            // 
            // txtparts_brand
            // 
            this.txtparts_brand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtparts_brand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtparts_brand.BackColor = System.Drawing.Color.Transparent;
            this.txtparts_brand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtparts_brand.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtparts_brand.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtparts_brand.ForeImage = null;
            this.txtparts_brand.InputtingVerifyCondition = null;
            this.txtparts_brand.Location = new System.Drawing.Point(787, 81);
            this.txtparts_brand.MaxLengh = 32767;
            this.txtparts_brand.Multiline = false;
            this.txtparts_brand.Name = "txtparts_brand";
            this.txtparts_brand.Radius = 3;
            this.txtparts_brand.ReadOnly = false;
            this.txtparts_brand.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtparts_brand.ShowError = false;
            this.txtparts_brand.Size = new System.Drawing.Size(121, 23);
            this.txtparts_brand.TabIndex = 86;
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
            this.cbowh_code.Location = new System.Drawing.Point(787, 45);
            this.cbowh_code.Name = "cbowh_code";
            this.cbowh_code.Size = new System.Drawing.Size(121, 22);
            this.cbowh_code.TabIndex = 82;
            this.cbowh_code.Tag = "wh_code";
            // 
            // txtcparts_code
            // 
            this.txtcparts_code.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcparts_code.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtcparts_code.Location = new System.Drawing.Point(105, 80);
            this.txtcparts_code.Name = "txtcparts_code";
            this.txtcparts_code.ReadOnly = false;
            this.txtcparts_code.Size = new System.Drawing.Size(121, 24);
            this.txtcparts_code.TabIndex = 83;
            this.txtcparts_code.Tag = "配件编码";
            this.txtcparts_code.ToolTipTitle = "";
            this.txtcparts_code.ChooserClick += new System.EventHandler(this.txtcparts_code_ChooserClick);
            // 
            // txtsup_name
            // 
            this.txtsup_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtsup_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtsup_name.BackColor = System.Drawing.Color.Transparent;
            this.txtsup_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtsup_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtsup_name.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtsup_name.ForeImage = null;
            this.txtsup_name.InputtingVerifyCondition = null;
            this.txtsup_name.Location = new System.Drawing.Point(346, 45);
            this.txtsup_name.MaxLengh = 32767;
            this.txtsup_name.Multiline = false;
            this.txtsup_name.Name = "txtsup_name";
            this.txtsup_name.Radius = 3;
            this.txtsup_name.ReadOnly = false;
            this.txtsup_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtsup_name.ShowError = false;
            this.txtsup_name.Size = new System.Drawing.Size(121, 23);
            this.txtsup_name.TabIndex = 80;
            this.txtsup_name.Tag = "sup_name";
            this.txtsup_name.UseSystemPasswordChar = false;
            this.txtsup_name.Value = "";
            this.txtsup_name.VerifyCondition = null;
            this.txtsup_name.VerifyType = null;
            this.txtsup_name.VerifyTypeName = null;
            this.txtsup_name.WaterMark = null;
            this.txtsup_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // cbosup_type
            // 
            this.cbosup_type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbosup_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbosup_type.FormattingEnabled = true;
            this.cbosup_type.Location = new System.Drawing.Point(572, 45);
            this.cbosup_type.Name = "cbosup_type";
            this.cbosup_type.Size = new System.Drawing.Size(121, 22);
            this.cbosup_type.TabIndex = 81;
            this.cbosup_type.Tag = "sup_type";
            // 
            // cbobalance_way
            // 
            this.cbobalance_way.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbobalance_way.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbobalance_way.FormattingEnabled = true;
            this.cbobalance_way.Location = new System.Drawing.Point(572, 9);
            this.cbobalance_way.Name = "cbobalance_way";
            this.cbobalance_way.Size = new System.Drawing.Size(121, 22);
            this.cbobalance_way.TabIndex = 77;
            this.cbobalance_way.Tag = "balance_way";
            this.cbobalance_way.SelectedIndexChanged += new System.EventHandler(this.cbobalance_way_SelectedIndexChanged);
            // 
            // cbobalance_account
            // 
            this.cbobalance_account.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbobalance_account.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbobalance_account.FormattingEnabled = true;
            this.cbobalance_account.Location = new System.Drawing.Point(787, 9);
            this.cbobalance_account.Name = "cbobalance_account";
            this.cbobalance_account.Size = new System.Drawing.Size(121, 22);
            this.cbobalance_account.TabIndex = 78;
            this.cbobalance_account.Tag = "balance_account";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(523, 122);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 72;
            this.label13.Text = "公司：";
            // 
            // txtcsup_code
            // 
            this.txtcsup_code.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcsup_code.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtcsup_code.Location = new System.Drawing.Point(105, 44);
            this.txtcsup_code.Name = "txtcsup_code";
            this.txtcsup_code.ReadOnly = false;
            this.txtcsup_code.Size = new System.Drawing.Size(121, 24);
            this.txtcsup_code.TabIndex = 79;
            this.txtcsup_code.Tag = "sup_code";
            this.txtcsup_code.ToolTipTitle = "";
            this.txtcsup_code.ChooserClick += new System.EventHandler(this.txtcsup_code_ChooserClick);
            // 
            // cboreceipt_type
            // 
            this.cboreceipt_type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboreceipt_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboreceipt_type.FormattingEnabled = true;
            this.cboreceipt_type.Location = new System.Drawing.Point(346, 9);
            this.cboreceipt_type.Name = "cboreceipt_type";
            this.cboreceipt_type.Size = new System.Drawing.Size(121, 22);
            this.cboreceipt_type.TabIndex = 76;
            this.cboreceipt_type.Tag = "receipt_type";
            // 
            // cboorder_type
            // 
            this.cboorder_type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboorder_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboorder_type.FormattingEnabled = true;
            this.cboorder_type.Location = new System.Drawing.Point(105, 9);
            this.cboorder_type.Name = "cboorder_type";
            this.cboorder_type.Size = new System.Drawing.Size(121, 22);
            this.cboorder_type.TabIndex = 75;
            this.cboorder_type.Tag = "order_type";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(275, 86);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 69;
            this.label10.Text = "配件名称：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(740, 122);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 73;
            this.label14.Text = "部门：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(501, 86);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 70;
            this.label11.Text = "配件图号：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(740, 86);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 71;
            this.label12.Text = "品牌：";
            // 
            // dicreate_time
            // 
            this.dicreate_time.BackColor = System.Drawing.Color.Transparent;
            this.dicreate_time.EndDate = "2015-02-09";
            this.dicreate_time.Location = new System.Drawing.Point(41, 114);
            this.dicreate_time.Name = "dicreate_time";
            this.dicreate_time.ShowFormat = "yyyy-MM-dd";
            this.dicreate_time.Size = new System.Drawing.Size(411, 28);
            this.dicreate_time.StartDate = "2015-02-01";
            this.dicreate_time.TabIndex = 74;
            this.dicreate_time.Tag = "create_time";
            this.dicreate_time.Text = "日期从：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(489, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 66;
            this.label7.Text = "供应商类别：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(263, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 65;
            this.label6.Text = "供应商名称：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(740, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 67;
            this.label8.Text = "仓库：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(501, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 62;
            this.label3.Text = "结算方式：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 86);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 68;
            this.label9.Text = "配件编码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(716, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 63;
            this.label4.Text = "结算账户：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(275, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 61;
            this.label2.Text = "发票类型：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 60;
            this.label1.Text = "单据类型：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 64;
            this.label5.Text = "供应商编码：";
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
            this.colPartsCode,
            this.colPartsName,
            this.colDrawingNum,
            this.colPartsBrand,
            this.Column5,
            this.Column6,
            this.Column7,
            this.colNum,
            this.colPrice,
            this.colMoney,
            this.colDiscountPrice,
            this.colDiscountMoney,
            this.colDiscount});
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
            this.dgvReport.Size = new System.Drawing.Size(1069, 345);
            this.dgvReport.TabIndex = 0;
            this.dgvReport.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReport_CellDoubleClick);
            // 
            // colPartsCode
            // 
            this.colPartsCode.DataPropertyName = "配件编码";
            this.colPartsCode.HeaderText = "配件编码";
            this.colPartsCode.Name = "colPartsCode";
            this.colPartsCode.ReadOnly = true;
            // 
            // colPartsName
            // 
            this.colPartsName.DataPropertyName = "配件名称";
            this.colPartsName.HeaderText = "配件名称";
            this.colPartsName.Name = "colPartsName";
            this.colPartsName.ReadOnly = true;
            // 
            // colDrawingNum
            // 
            this.colDrawingNum.DataPropertyName = "图号";
            this.colDrawingNum.HeaderText = "图号";
            this.colDrawingNum.Name = "colDrawingNum";
            this.colDrawingNum.ReadOnly = true;
            this.colDrawingNum.Width = 80;
            // 
            // colPartsBrand
            // 
            this.colPartsBrand.DataPropertyName = "配件品牌";
            this.colPartsBrand.HeaderText = "配件品牌";
            this.colPartsBrand.Name = "colPartsBrand";
            this.colPartsBrand.ReadOnly = true;
            this.colPartsBrand.Width = 80;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "配件类别";
            this.Column5.HeaderText = "配件类别";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 80;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "厂商编码";
            this.Column6.HeaderText = "厂商编码";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "单位";
            this.Column7.HeaderText = "单位";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 60;
            // 
            // colNum
            // 
            this.colNum.DataPropertyName = "数量";
            this.colNum.HeaderText = "数量";
            this.colNum.Name = "colNum";
            this.colNum.ReadOnly = true;
            this.colNum.Width = 60;
            // 
            // colPrice
            // 
            this.colPrice.DataPropertyName = "价格";
            this.colPrice.HeaderText = "价格";
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            this.colPrice.Width = 60;
            // 
            // colMoney
            // 
            this.colMoney.DataPropertyName = "金额";
            this.colMoney.HeaderText = "金额";
            this.colMoney.Name = "colMoney";
            this.colMoney.ReadOnly = true;
            this.colMoney.Width = 60;
            // 
            // colDiscountPrice
            // 
            this.colDiscountPrice.DataPropertyName = "折后单价";
            this.colDiscountPrice.HeaderText = "折后单价";
            this.colDiscountPrice.Name = "colDiscountPrice";
            this.colDiscountPrice.ReadOnly = true;
            this.colDiscountPrice.Width = 70;
            // 
            // colDiscountMoney
            // 
            this.colDiscountMoney.DataPropertyName = "折后金额";
            this.colDiscountMoney.HeaderText = "折后金额";
            this.colDiscountMoney.Name = "colDiscountMoney";
            this.colDiscountMoney.ReadOnly = true;
            this.colDiscountMoney.Width = 70;
            // 
            // colDiscount
            // 
            this.colDiscount.DataPropertyName = "折扣金额";
            this.colDiscount.HeaderText = "折扣金额";
            this.colDiscount.Name = "colDiscount";
            this.colDiscount.ReadOnly = true;
            this.colDiscount.Width = 70;
            // 
            // UCPurchaseDiscountSummariz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCPurchaseDiscountSummariz";
            this.Load += new System.EventHandler(this.UCPurchaseDiscountSummariz_Load);
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
        private ServiceStationClient.ComponentUI.TextBoxEx txtsup_name;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbosup_type;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbobalance_way;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbobalance_account;
        private System.Windows.Forms.Label label13;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtcsup_code;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboreceipt_type;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboorder_type;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private ServiceStationClient.ComponentUI.DateInterval dicreate_time;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.DataGridViewReport dgvReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartsCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDrawingNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartsBrand;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiscountPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiscountMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiscount;
    }
}
