namespace HXCPcClient.Chooser
{
    partial class frmParts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmParts));
            this.tvPartsType = new ServiceStationClient.ComponentUI.TreeViewEx();
            this.btnColse = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnOK = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPartsName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtPartsCode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtCarPartsCode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtDrawingNum = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.cboPartsBrand = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cboWarehouse = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.dgvParts = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colPartsID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSerPartsCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPartsCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVehicle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paper_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colref_out_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaintain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDetail = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colPartsNameDetail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWarehouse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsableNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOpenNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnSave = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnAdd = new ServiceStationClient.ComponentUI.ButtonEx();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.cboDataSource = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtSupperOne = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.txtSupperOne);
            this.pnlContainer.Controls.Add(this.cboDataSource);
            this.pnlContainer.Controls.Add(this.page);
            this.pnlContainer.Controls.Add(this.btnAdd);
            this.pnlContainer.Controls.Add(this.btnSave);
            this.pnlContainer.Controls.Add(this.btnClear);
            this.pnlContainer.Controls.Add(this.btnSearch);
            this.pnlContainer.Controls.Add(this.dgvDetail);
            this.pnlContainer.Controls.Add(this.dgvParts);
            this.pnlContainer.Controls.Add(this.cboWarehouse);
            this.pnlContainer.Controls.Add(this.cboPartsBrand);
            this.pnlContainer.Controls.Add(this.txtDrawingNum);
            this.pnlContainer.Controls.Add(this.txtCarPartsCode);
            this.pnlContainer.Controls.Add(this.txtPartsCode);
            this.pnlContainer.Controls.Add(this.txtPartsName);
            this.pnlContainer.Controls.Add(this.label10);
            this.pnlContainer.Controls.Add(this.label8);
            this.pnlContainer.Controls.Add(this.label7);
            this.pnlContainer.Controls.Add(this.label6);
            this.pnlContainer.Controls.Add(this.label5);
            this.pnlContainer.Controls.Add(this.label3);
            this.pnlContainer.Controls.Add(this.label2);
            this.pnlContainer.Controls.Add(this.label1);
            this.pnlContainer.Controls.Add(this.btnOK);
            this.pnlContainer.Controls.Add(this.btnColse);
            this.pnlContainer.Controls.Add(this.tvPartsType);
            this.pnlContainer.Size = new System.Drawing.Size(791, 528);
            // 
            // tvPartsType
            // 
            this.tvPartsType.IgnoreAutoCheck = false;
            this.tvPartsType.Location = new System.Drawing.Point(0, 3);
            this.tvPartsType.Name = "tvPartsType";
            this.tvPartsType.Size = new System.Drawing.Size(121, 473);
            this.tvPartsType.TabIndex = 0;
            this.tvPartsType.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvPartsType_NodeMouseClick);
            // 
            // btnColse
            // 
            this.btnColse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnColse.BackgroundImage")));
            this.btnColse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnColse.Caption = "关闭";
            this.btnColse.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnColse.DownImage = ((System.Drawing.Image)(resources.GetObject("btnColse.DownImage")));
            this.btnColse.Location = new System.Drawing.Point(707, 482);
            this.btnColse.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnColse.MoveImage")));
            this.btnColse.Name = "btnColse";
            this.btnColse.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnColse.NormalImage")));
            this.btnColse.Size = new System.Drawing.Size(60, 26);
            this.btnColse.TabIndex = 1;
            this.btnColse.Click += new System.EventHandler(this.btnColse_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.Caption = "确定";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.DownImage = ((System.Drawing.Image)(resources.GetObject("btnOK.DownImage")));
            this.btnOK.Location = new System.Drawing.Point(589, 482);
            this.btnOK.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnOK.MoveImage")));
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnOK.NormalImage")));
            this.btnOK.Size = new System.Drawing.Size(60, 26);
            this.btnOK.TabIndex = 2;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "配件名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "配件编码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(346, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "车厂编码：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(346, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "数据来源：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(565, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "供应商：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(164, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "图号：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(370, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "品牌：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(577, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "仓库：";
            // 
            // txtPartsName
            // 
            this.txtPartsName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPartsName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPartsName.BackColor = System.Drawing.Color.Transparent;
            this.txtPartsName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtPartsName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtPartsName.ForeImage = null;
            this.txtPartsName.Location = new System.Drawing.Point(211, 20);
            this.txtPartsName.MaxLengh = 32767;
            this.txtPartsName.Multiline = false;
            this.txtPartsName.Name = "txtPartsName";
            this.txtPartsName.Radius = 3;
            this.txtPartsName.ReadOnly = false;
            this.txtPartsName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsName.Size = new System.Drawing.Size(109, 23);
            this.txtPartsName.TabIndex = 13;
            this.txtPartsName.UseSystemPasswordChar = false;
            this.txtPartsName.WaterMark = null;
            this.txtPartsName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtPartsCode
            // 
            this.txtPartsCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPartsCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPartsCode.BackColor = System.Drawing.Color.Transparent;
            this.txtPartsCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtPartsCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtPartsCode.ForeImage = null;
            this.txtPartsCode.Location = new System.Drawing.Point(211, 57);
            this.txtPartsCode.MaxLengh = 32767;
            this.txtPartsCode.Multiline = false;
            this.txtPartsCode.Name = "txtPartsCode";
            this.txtPartsCode.Radius = 3;
            this.txtPartsCode.ReadOnly = false;
            this.txtPartsCode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsCode.Size = new System.Drawing.Size(109, 23);
            this.txtPartsCode.TabIndex = 14;
            this.txtPartsCode.UseSystemPasswordChar = false;
            this.txtPartsCode.WaterMark = null;
            this.txtPartsCode.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtCarPartsCode
            // 
            this.txtCarPartsCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCarPartsCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCarPartsCode.BackColor = System.Drawing.Color.Transparent;
            this.txtCarPartsCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCarPartsCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCarPartsCode.ForeImage = null;
            this.txtCarPartsCode.Location = new System.Drawing.Point(417, 57);
            this.txtCarPartsCode.MaxLengh = 32767;
            this.txtCarPartsCode.Multiline = false;
            this.txtCarPartsCode.Name = "txtCarPartsCode";
            this.txtCarPartsCode.Radius = 3;
            this.txtCarPartsCode.ReadOnly = false;
            this.txtCarPartsCode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCarPartsCode.Size = new System.Drawing.Size(114, 23);
            this.txtCarPartsCode.TabIndex = 15;
            this.txtCarPartsCode.UseSystemPasswordChar = false;
            this.txtCarPartsCode.WaterMark = null;
            this.txtCarPartsCode.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtDrawingNum
            // 
            this.txtDrawingNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtDrawingNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtDrawingNum.BackColor = System.Drawing.Color.Transparent;
            this.txtDrawingNum.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtDrawingNum.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtDrawingNum.ForeImage = null;
            this.txtDrawingNum.Location = new System.Drawing.Point(211, 94);
            this.txtDrawingNum.MaxLengh = 32767;
            this.txtDrawingNum.Multiline = false;
            this.txtDrawingNum.Name = "txtDrawingNum";
            this.txtDrawingNum.Radius = 3;
            this.txtDrawingNum.ReadOnly = false;
            this.txtDrawingNum.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtDrawingNum.Size = new System.Drawing.Size(109, 23);
            this.txtDrawingNum.TabIndex = 18;
            this.txtDrawingNum.UseSystemPasswordChar = false;
            this.txtDrawingNum.WaterMark = null;
            this.txtDrawingNum.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // cboPartsBrand
            // 
            this.cboPartsBrand.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboPartsBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPartsBrand.FormattingEnabled = true;
            this.cboPartsBrand.Location = new System.Drawing.Point(417, 94);
            this.cboPartsBrand.Name = "cboPartsBrand";
            this.cboPartsBrand.Size = new System.Drawing.Size(106, 22);
            this.cboPartsBrand.TabIndex = 21;
            // 
            // cboWarehouse
            // 
            this.cboWarehouse.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWarehouse.FormattingEnabled = true;
            this.cboWarehouse.Location = new System.Drawing.Point(624, 57);
            this.cboWarehouse.Name = "cboWarehouse";
            this.cboWarehouse.Size = new System.Drawing.Size(108, 22);
            this.cboWarehouse.TabIndex = 22;
            this.cboWarehouse.SelectedIndexChanged += new System.EventHandler(this.cboWarehouse_SelectedIndexChanged);
            // 
            // dgvParts
            // 
            this.dgvParts.AllowUserToAddRows = false;
            this.dgvParts.AllowUserToDeleteRows = false;
            this.dgvParts.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvParts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvParts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvParts.BackgroundColor = System.Drawing.Color.White;
            this.dgvParts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvParts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPartsID,
            this.colSerPartsCode,
            this.colCarPartsCode,
            this.colPartsName,
            this.colVehicle,
            this.paper_count,
            this.colref_out_price,
            this.colSalePrice,
            this.colMaintain,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvParts.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvParts.EnableHeadersVisualStyles = false;
            this.dgvParts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvParts.Location = new System.Drawing.Point(128, 158);
            this.dgvParts.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvParts.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvParts.MergeColumnNames")));
            this.dgvParts.MultiSelect = false;
            this.dgvParts.Name = "dgvParts";
            this.dgvParts.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvParts.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvParts.RowHeadersVisible = false;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvParts.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvParts.RowTemplate.Height = 23;
            this.dgvParts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvParts.ShowCheckBox = true;
            this.dgvParts.Size = new System.Drawing.Size(645, 191);
            this.dgvParts.TabIndex = 23;
            this.dgvParts.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvParts_RowEnter);
            // 
            // colPartsID
            // 
            this.colPartsID.DataPropertyName = "parts_id";
            this.colPartsID.HeaderText = "ID";
            this.colPartsID.Name = "colPartsID";
            this.colPartsID.ReadOnly = true;
            this.colPartsID.Visible = false;
            // 
            // colSerPartsCode
            // 
            this.colSerPartsCode.DataPropertyName = "ser_parts_code";
            this.colSerPartsCode.HeaderText = "配件编码";
            this.colSerPartsCode.Name = "colSerPartsCode";
            this.colSerPartsCode.ReadOnly = true;
            // 
            // colCarPartsCode
            // 
            this.colCarPartsCode.DataPropertyName = "car_parts_code";
            this.colCarPartsCode.HeaderText = "车厂编码";
            this.colCarPartsCode.Name = "colCarPartsCode";
            this.colCarPartsCode.ReadOnly = true;
            // 
            // colPartsName
            // 
            this.colPartsName.DataPropertyName = "parts_name";
            this.colPartsName.HeaderText = "配件名称";
            this.colPartsName.Name = "colPartsName";
            this.colPartsName.ReadOnly = true;
            // 
            // colVehicle
            // 
            this.colVehicle.DataPropertyName = "model";
            this.colVehicle.HeaderText = "规格";
            this.colVehicle.Name = "colVehicle";
            this.colVehicle.ReadOnly = true;
            // 
            // paper_count
            // 
            this.paper_count.DataPropertyName = "paper_count";
            this.paper_count.HeaderText = "库存数量";
            this.paper_count.Name = "paper_count";
            this.paper_count.ReadOnly = true;
            // 
            // colref_out_price
            // 
            this.colref_out_price.DataPropertyName = "retail";
            this.colref_out_price.HeaderText = "销售价格";
            this.colref_out_price.Name = "colref_out_price";
            this.colref_out_price.ReadOnly = true;
            // 
            // colSalePrice
            // 
            this.colSalePrice.DataPropertyName = "brand_name";
            this.colSalePrice.HeaderText = "品牌";
            this.colSalePrice.Name = "colSalePrice";
            this.colSalePrice.ReadOnly = true;
            // 
            // colMaintain
            // 
            this.colMaintain.DataPropertyName = "unit";
            this.colMaintain.HeaderText = "单位";
            this.colMaintain.Name = "colMaintain";
            this.colMaintain.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "import";
            this.Column2.HeaderText = "进口";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "drawing_num";
            this.Column3.HeaderText = "图号";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "sup_full_name";
            this.Column4.HeaderText = "供应商";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "remark";
            this.Column5.HeaderText = "备注";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AllowUserToDeleteRows = false;
            this.dgvDetail.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvDetail.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetail.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPartsNameDetail,
            this.colWarehouse,
            this.colUsableNum,
            this.colOpenNum});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetail.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDetail.EnableHeadersVisualStyles = false;
            this.dgvDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvDetail.Location = new System.Drawing.Point(128, 392);
            this.dgvDetail.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvDetail.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvDetail.MergeColumnNames")));
            this.dgvDetail.MultiSelect = false;
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDetail.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvDetail.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetail.ShowCheckBox = true;
            this.dgvDetail.Size = new System.Drawing.Size(645, 84);
            this.dgvDetail.TabIndex = 24;
            // 
            // colPartsNameDetail
            // 
            this.colPartsNameDetail.DataPropertyName = "parts_name";
            this.colPartsNameDetail.HeaderText = "配件名称";
            this.colPartsNameDetail.Name = "colPartsNameDetail";
            this.colPartsNameDetail.ReadOnly = true;
            // 
            // colWarehouse
            // 
            this.colWarehouse.DataPropertyName = "wh_name";
            this.colWarehouse.HeaderText = "所在仓库";
            this.colWarehouse.Name = "colWarehouse";
            this.colWarehouse.ReadOnly = true;
            // 
            // colUsableNum
            // 
            this.colUsableNum.DataPropertyName = "paper_count";
            this.colUsableNum.HeaderText = "账面库存";
            this.colUsableNum.Name = "colUsableNum";
            this.colUsableNum.ReadOnly = true;
            // 
            // colOpenNum
            // 
            this.colOpenNum.DataPropertyName = "actual_count";
            this.colOpenNum.HeaderText = "实际库存";
            this.colOpenNum.Name = "colOpenNum";
            this.colOpenNum.ReadOnly = true;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(684, 126);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(60, 26);
            this.btnSearch.TabIndex = 25;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清空";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(684, 94);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 26;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Caption = "保存当页";
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSave.DownImage")));
            this.btnSave.Location = new System.Drawing.Point(511, 482);
            this.btnSave.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSave.MoveImage")));
            this.btnSave.Name = "btnSave";
            this.btnSave.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSave.NormalImage")));
            this.btnSave.Size = new System.Drawing.Size(60, 26);
            this.btnSave.TabIndex = 27;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Caption = "新增";
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAdd.DownImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.DownImage")));
            this.btnAdd.Location = new System.Drawing.Point(11, 482);
            this.btnAdd.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.MoveImage")));
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.NormalImage")));
            this.btnAdd.Size = new System.Drawing.Size(60, 26);
            this.btnAdd.TabIndex = 28;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // page
            // 
            this.page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.page.Location = new System.Drawing.Point(293, 355);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(480, 31);
            this.page.TabIndex = 29;
            this.page.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            // 
            // cboDataSource
            // 
            this.cboDataSource.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataSource.FormattingEnabled = true;
            this.cboDataSource.Location = new System.Drawing.Point(417, 20);
            this.cboDataSource.Name = "cboDataSource";
            this.cboDataSource.Size = new System.Drawing.Size(121, 22);
            this.cboDataSource.TabIndex = 30;
            // 
            // txtSupperOne
            // 
            this.txtSupperOne.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtSupperOne.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtSupperOne.BackColor = System.Drawing.Color.Transparent;
            this.txtSupperOne.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtSupperOne.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtSupperOne.ForeImage = null;
            this.txtSupperOne.Location = new System.Drawing.Point(624, 20);
            this.txtSupperOne.MaxLengh = 32767;
            this.txtSupperOne.Multiline = false;
            this.txtSupperOne.Name = "txtSupperOne";
            this.txtSupperOne.Radius = 3;
            this.txtSupperOne.ReadOnly = false;
            this.txtSupperOne.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtSupperOne.Size = new System.Drawing.Size(108, 23);
            this.txtSupperOne.TabIndex = 31;
            this.txtSupperOne.UseSystemPasswordChar = false;
            this.txtSupperOne.WaterMark = null;
            this.txtSupperOne.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // frmParts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 559);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmParts";
            this.Text = "配件选择";
            this.Load += new System.EventHandler(this.frmParts_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.ButtonEx btnOK;
        private ServiceStationClient.ComponentUI.ButtonEx btnColse;
        private ServiceStationClient.ComponentUI.TreeViewEx tvPartsType;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboWarehouse;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboPartsBrand;
        private ServiceStationClient.ComponentUI.TextBoxEx txtDrawingNum;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCarPartsCode;
        private ServiceStationClient.ComponentUI.TextBoxEx txtPartsCode;
        private ServiceStationClient.ComponentUI.TextBoxEx txtPartsName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvDetail;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvParts;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnSave;
        private ServiceStationClient.ComponentUI.ButtonEx btnAdd;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private ServiceStationClient.ComponentUI.TextBoxEx txtSupperOne;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartsID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSerPartsCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPartsCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVehicle;
        private System.Windows.Forms.DataGridViewTextBoxColumn paper_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn colref_out_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaintain;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartsNameDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWarehouse;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsableNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOpenNum;
        protected ServiceStationClient.ComponentUI.ComboBoxEx cboDataSource;
    }
}