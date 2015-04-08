namespace HXCPcClient.UCForm.BusinessAnalysis.StockReport
{
    partial class UCStockAging
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCStockAging));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvReport = new ServiceStationClient.ComponentUI.DataGridViewReport();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNum1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoney1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNum2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoney2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNum3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoney3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboCompany = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cboWarehouse = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtcPartsCode = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txtBrand = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtPartsNum = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtPartsName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbTiming2 = new System.Windows.Forms.CheckBox();
            this.cbTiming3 = new System.Windows.Forms.CheckBox();
            this.nudTiming1 = new System.Windows.Forms.NumericUpDown();
            this.nudTiming2 = new System.Windows.Forms.NumericUpDown();
            this.nudTiming3 = new System.Windows.Forms.NumericUpDown();
            this.pnlSearch.SuspendLayout();
            this.pnlReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTiming1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTiming2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTiming3)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.nudTiming3);
            this.pnlSearch.Controls.Add(this.nudTiming2);
            this.pnlSearch.Controls.Add(this.nudTiming1);
            this.pnlSearch.Controls.Add(this.cbTiming3);
            this.pnlSearch.Controls.Add(this.cbTiming2);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.txtBrand);
            this.pnlSearch.Controls.Add(this.txtPartsNum);
            this.pnlSearch.Controls.Add(this.txtPartsName);
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.txtcPartsCode);
            this.pnlSearch.Controls.Add(this.cboWarehouse);
            this.pnlSearch.Controls.Add(this.cboCompany);
            this.pnlSearch.Controls.SetChildIndex(this.cboCompany, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboWarehouse, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtcPartsCode, 0);
            this.pnlSearch.Controls.SetChildIndex(this.btnClear, 0);
            this.pnlSearch.Controls.SetChildIndex(this.btnSearch, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label4, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label5, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label6, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtPartsName, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtPartsNum, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtBrand, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label1, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label2, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label3, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label7, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cbTiming2, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cbTiming3, 0);
            this.pnlSearch.Controls.SetChildIndex(this.nudTiming1, 0);
            this.pnlSearch.Controls.SetChildIndex(this.nudTiming2, 0);
            this.pnlSearch.Controls.SetChildIndex(this.nudTiming3, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlReport
            // 
            this.pnlReport.Controls.Add(this.dgvReport);
            // 
            // dgvReport
            // 
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(253)))), ((int)(((byte)(252)))));
            this.dgvReport.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.colPrice,
            this.colNum,
            this.colMoney,
            this.colNum1,
            this.colMoney1,
            this.colNum2,
            this.colMoney2,
            this.colNum3,
            this.colMoney3});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReport.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvReport.Location = new System.Drawing.Point(0, 0);
            this.dgvReport.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvReport.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvReport.MergeColumnNames")));
            this.dgvReport.MultiSelect = false;
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvReport.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvReport.RowHeadersVisible = false;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(234)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvReport.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvReport.RowTemplate.Height = 23;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(1069, 379);
            this.dgvReport.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "parts_code";
            this.Column1.HeaderText = "配件编码";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "parts_name";
            this.Column2.HeaderText = "配件名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "model";
            this.Column3.HeaderText = "规格";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "drawing_num";
            this.Column4.HeaderText = "配件图号";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "parts_brand";
            this.Column5.HeaderText = "品牌";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "car_parts_code";
            this.Column6.HeaderText = "厂商编码";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "unit_name";
            this.Column7.HeaderText = "单位";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // colPrice
            // 
            this.colPrice.DataPropertyName = "ref_in_price";
            this.colPrice.HeaderText = "单价";
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            // 
            // colNum
            // 
            this.colNum.DataPropertyName = "TotalPhyCount";
            this.colNum.HeaderText = "数量";
            this.colNum.Name = "colNum";
            this.colNum.ReadOnly = true;
            // 
            // colMoney
            // 
            this.colMoney.DataPropertyName = "TotalAmount";
            this.colMoney.HeaderText = "金额";
            this.colMoney.Name = "colMoney";
            this.colMoney.ReadOnly = true;
            // 
            // colNum1
            // 
            this.colNum1.DataPropertyName = "ThirtyPhyCount";
            this.colNum1.HeaderText = "数量";
            this.colNum1.Name = "colNum1";
            this.colNum1.ReadOnly = true;
            // 
            // colMoney1
            // 
            this.colMoney1.DataPropertyName = "ThirtyAmount";
            this.colMoney1.HeaderText = "金额";
            this.colMoney1.Name = "colMoney1";
            this.colMoney1.ReadOnly = true;
            // 
            // colNum2
            // 
            this.colNum2.DataPropertyName = "SixtyPhyCount";
            this.colNum2.HeaderText = "数量";
            this.colNum2.Name = "colNum2";
            this.colNum2.ReadOnly = true;
            // 
            // colMoney2
            // 
            this.colMoney2.DataPropertyName = "SixtyAmount";
            this.colMoney2.HeaderText = "金额";
            this.colMoney2.Name = "colMoney2";
            this.colMoney2.ReadOnly = true;
            // 
            // colNum3
            // 
            this.colNum3.DataPropertyName = "MoreSixtyPhyCount";
            this.colNum3.HeaderText = "数量";
            this.colNum3.Name = "colNum3";
            this.colNum3.ReadOnly = true;
            // 
            // colMoney3
            // 
            this.colMoney3.DataPropertyName = "MoreSixtyAmount";
            this.colMoney3.HeaderText = "金额";
            this.colMoney3.Name = "colMoney3";
            this.colMoney3.ReadOnly = true;
            // 
            // cboCompany
            // 
            this.cboCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(77, 30);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(121, 22);
            this.cboCompany.TabIndex = 2;
            // 
            // cboWarehouse
            // 
            this.cboWarehouse.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWarehouse.FormattingEnabled = true;
            this.cboWarehouse.Location = new System.Drawing.Point(330, 32);
            this.cboWarehouse.Name = "cboWarehouse";
            this.cboWarehouse.Size = new System.Drawing.Size(121, 22);
            this.cboWarehouse.TabIndex = 3;
            // 
            // txtcPartsCode
            // 
            this.txtcPartsCode.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcPartsCode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtcPartsCode.Location = new System.Drawing.Point(588, 32);
            this.txtcPartsCode.Name = "txtcPartsCode";
            this.txtcPartsCode.ReadOnly = false;
            this.txtcPartsCode.Size = new System.Drawing.Size(145, 25);
            this.txtcPartsCode.TabIndex = 4;
            this.txtcPartsCode.Tag = "parts_code";
            this.txtcPartsCode.ToolTipTitle = "";
            this.txtcPartsCode.ChooserClick += new System.EventHandler(this.txtcPartsCode_ChooserClick);
            // 
            // txtBrand
            // 
            this.txtBrand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtBrand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtBrand.BackColor = System.Drawing.Color.Transparent;
            this.txtBrand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtBrand.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtBrand.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBrand.ForeImage = null;
            this.txtBrand.InputtingVerifyCondition = null;
            this.txtBrand.Location = new System.Drawing.Point(844, 75);
            this.txtBrand.MaxLengh = 32767;
            this.txtBrand.Multiline = false;
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Radius = 3;
            this.txtBrand.ReadOnly = false;
            this.txtBrand.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtBrand.ShowError = false;
            this.txtBrand.Size = new System.Drawing.Size(116, 23);
            this.txtBrand.TabIndex = 61;
            this.txtBrand.Tag = "parts_brand";
            this.txtBrand.UseSystemPasswordChar = false;
            this.txtBrand.Value = "";
            this.txtBrand.VerifyCondition = null;
            this.txtBrand.VerifyType = null;
            this.txtBrand.VerifyTypeName = null;
            this.txtBrand.WaterMark = null;
            this.txtBrand.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtPartsNum
            // 
            this.txtPartsNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPartsNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPartsNum.BackColor = System.Drawing.Color.Transparent;
            this.txtPartsNum.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtPartsNum.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtPartsNum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPartsNum.ForeImage = null;
            this.txtPartsNum.InputtingVerifyCondition = null;
            this.txtPartsNum.Location = new System.Drawing.Point(588, 75);
            this.txtPartsNum.MaxLengh = 32767;
            this.txtPartsNum.Multiline = false;
            this.txtPartsNum.Name = "txtPartsNum";
            this.txtPartsNum.Radius = 3;
            this.txtPartsNum.ReadOnly = false;
            this.txtPartsNum.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsNum.ShowError = false;
            this.txtPartsNum.Size = new System.Drawing.Size(139, 23);
            this.txtPartsNum.TabIndex = 60;
            this.txtPartsNum.Tag = "drawing_num";
            this.txtPartsNum.UseSystemPasswordChar = false;
            this.txtPartsNum.Value = "";
            this.txtPartsNum.VerifyCondition = null;
            this.txtPartsNum.VerifyType = null;
            this.txtPartsNum.VerifyTypeName = null;
            this.txtPartsNum.WaterMark = null;
            this.txtPartsNum.WaterMarkColor = System.Drawing.Color.Silver;
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
            this.txtPartsName.Location = new System.Drawing.Point(844, 35);
            this.txtPartsName.MaxLengh = 32767;
            this.txtPartsName.Multiline = false;
            this.txtPartsName.Name = "txtPartsName";
            this.txtPartsName.Radius = 3;
            this.txtPartsName.ReadOnly = false;
            this.txtPartsName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsName.ShowError = false;
            this.txtPartsName.Size = new System.Drawing.Size(116, 23);
            this.txtPartsName.TabIndex = 59;
            this.txtPartsName.Tag = "parts_name";
            this.txtPartsName.UseSystemPasswordChar = false;
            this.txtPartsName.Value = "";
            this.txtPartsName.VerifyCondition = null;
            this.txtPartsName.VerifyType = null;
            this.txtPartsName.VerifyTypeName = null;
            this.txtPartsName.WaterMark = null;
            this.txtPartsName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(792, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 58;
            this.label6.Text = "品牌：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(517, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 57;
            this.label5.Text = "配件图号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(773, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 56;
            this.label4.Text = "配件名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 62;
            this.label1.Text = "公司：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 63;
            this.label2.Text = "仓库：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(517, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 64;
            this.label3.Text = "配件编码：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 65;
            this.label7.Text = "时间点：";
            // 
            // cbTiming2
            // 
            this.cbTiming2.AutoSize = true;
            this.cbTiming2.Location = new System.Drawing.Point(190, 78);
            this.cbTiming2.Name = "cbTiming2";
            this.cbTiming2.Size = new System.Drawing.Size(66, 16);
            this.cbTiming2.TabIndex = 66;
            this.cbTiming2.Text = "时间点2";
            this.cbTiming2.UseVisualStyleBackColor = true;
            this.cbTiming2.CheckedChanged += new System.EventHandler(this.cbTiming2_CheckedChanged);
            // 
            // cbTiming3
            // 
            this.cbTiming3.AutoSize = true;
            this.cbTiming3.Location = new System.Drawing.Point(354, 78);
            this.cbTiming3.Name = "cbTiming3";
            this.cbTiming3.Size = new System.Drawing.Size(66, 16);
            this.cbTiming3.TabIndex = 67;
            this.cbTiming3.Text = "时间点3";
            this.cbTiming3.UseVisualStyleBackColor = true;
            this.cbTiming3.CheckedChanged += new System.EventHandler(this.cbTiming3_CheckedChanged);
            // 
            // nudTiming1
            // 
            this.nudTiming1.Location = new System.Drawing.Point(93, 76);
            this.nudTiming1.Name = "nudTiming1";
            this.nudTiming1.Size = new System.Drawing.Size(64, 21);
            this.nudTiming1.TabIndex = 68;
            this.nudTiming1.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudTiming1.ValueChanged += new System.EventHandler(this.nudTiming1_ValueChanged);
            // 
            // nudTiming2
            // 
            this.nudTiming2.Enabled = false;
            this.nudTiming2.Location = new System.Drawing.Point(268, 76);
            this.nudTiming2.Name = "nudTiming2";
            this.nudTiming2.Size = new System.Drawing.Size(56, 21);
            this.nudTiming2.TabIndex = 69;
            this.nudTiming2.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudTiming2.ValueChanged += new System.EventHandler(this.nudTiming2_ValueChanged);
            // 
            // nudTiming3
            // 
            this.nudTiming3.Enabled = false;
            this.nudTiming3.Location = new System.Drawing.Point(426, 76);
            this.nudTiming3.Name = "nudTiming3";
            this.nudTiming3.Size = new System.Drawing.Size(54, 21);
            this.nudTiming3.TabIndex = 70;
            this.nudTiming3.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.nudTiming3.ValueChanged += new System.EventHandler(this.nudTiming3_ValueChanged);
            // 
            // UCStockAging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCStockAging";
            this.Load += new System.EventHandler(this.UCStockAging_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTiming1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTiming2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTiming3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewReport dgvReport;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtcPartsCode;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboWarehouse;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboCompany;
        private System.Windows.Forms.NumericUpDown nudTiming3;
        private System.Windows.Forms.NumericUpDown nudTiming2;
        private System.Windows.Forms.NumericUpDown nudTiming1;
        private System.Windows.Forms.CheckBox cbTiming3;
        private System.Windows.Forms.CheckBox cbTiming2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TextBoxEx txtBrand;
        private ServiceStationClient.ComponentUI.TextBoxEx txtPartsNum;
        private ServiceStationClient.ComponentUI.TextBoxEx txtPartsName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNum1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMoney1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNum2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMoney2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNum3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMoney3;
    }
}
