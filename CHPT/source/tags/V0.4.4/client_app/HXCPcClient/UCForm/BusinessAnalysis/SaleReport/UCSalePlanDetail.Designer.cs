namespace HXCPcClient.UCForm.BusinessAnalysis.SaleReport
{
    partial class UCSalePlanDetail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCSalePlanDetail));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.diCreateTime = new ServiceStationClient.ComponentUI.DateInterval();
            this.txtPartsBrand = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtPartsName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtPartsNum = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtcPartsCode = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.cboOrg = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cboCompany = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvReport = new ServiceStationClient.ComponentUI.DataGridViewReport();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlanNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlanMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompleteMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompleteNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnfinishedNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnfinishedMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompleteRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSearch.SuspendLayout();
            this.pnlReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.diCreateTime);
            this.pnlSearch.Controls.Add(this.txtPartsBrand);
            this.pnlSearch.Controls.Add(this.txtPartsName);
            this.pnlSearch.Controls.Add(this.txtPartsNum);
            this.pnlSearch.Controls.Add(this.txtcPartsCode);
            this.pnlSearch.Controls.Add(this.cboOrg);
            this.pnlSearch.Controls.Add(this.cboCompany);
            this.pnlSearch.Controls.Add(this.label8);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.SetChildIndex(this.btnClear, 0);
            this.pnlSearch.Controls.SetChildIndex(this.btnSearch, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label1, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label2, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label3, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label4, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label7, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label8, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboCompany, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboOrg, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtcPartsCode, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtPartsNum, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtPartsName, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtPartsBrand, 0);
            this.pnlSearch.Controls.SetChildIndex(this.diCreateTime, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlReport
            // 
            this.pnlReport.Controls.Add(this.dgvReport);
            // 
            // diCreateTime
            // 
            this.diCreateTime.BackColor = System.Drawing.Color.Transparent;
            this.diCreateTime.EndDate = "2015-01-15";
            this.diCreateTime.Location = new System.Drawing.Point(33, 68);
            this.diCreateTime.Name = "diCreateTime";
            this.diCreateTime.ShowFormat = "yyyy-MM-dd";
            this.diCreateTime.Size = new System.Drawing.Size(411, 27);
            this.diCreateTime.StartDate = "2015-01-01";
            this.diCreateTime.TabIndex = 61;
            this.diCreateTime.Tag = "create_time";
            this.diCreateTime.Text = "日期从：";
            // 
            // txtPartsBrand
            // 
            this.txtPartsBrand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPartsBrand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPartsBrand.BackColor = System.Drawing.Color.Transparent;
            this.txtPartsBrand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtPartsBrand.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtPartsBrand.ForeImage = null;
            this.txtPartsBrand.InputtingVerifyCondition = null;
            this.txtPartsBrand.Location = new System.Drawing.Point(772, 68);
            this.txtPartsBrand.MaxLengh = 32767;
            this.txtPartsBrand.Multiline = false;
            this.txtPartsBrand.Name = "txtPartsBrand";
            this.txtPartsBrand.Radius = 3;
            this.txtPartsBrand.ReadOnly = false;
            this.txtPartsBrand.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsBrand.ShowError = false;
            this.txtPartsBrand.Size = new System.Drawing.Size(148, 23);
            this.txtPartsBrand.TabIndex = 60;
            this.txtPartsBrand.Tag = "品牌";
            this.txtPartsBrand.UseSystemPasswordChar = false;
            this.txtPartsBrand.Value = "";
            this.txtPartsBrand.VerifyCondition = null;
            this.txtPartsBrand.VerifyType = null;
            this.txtPartsBrand.VerifyTypeName = null;
            this.txtPartsBrand.WaterMark = null;
            this.txtPartsBrand.WaterMarkColor = System.Drawing.Color.Silver;
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
            this.txtPartsName.Location = new System.Drawing.Point(772, 25);
            this.txtPartsName.MaxLengh = 32767;
            this.txtPartsName.Multiline = false;
            this.txtPartsName.Name = "txtPartsName";
            this.txtPartsName.Radius = 3;
            this.txtPartsName.ReadOnly = false;
            this.txtPartsName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsName.ShowError = false;
            this.txtPartsName.Size = new System.Drawing.Size(148, 23);
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
            // txtPartsNum
            // 
            this.txtPartsNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPartsNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPartsNum.BackColor = System.Drawing.Color.Transparent;
            this.txtPartsNum.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtPartsNum.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtPartsNum.ForeImage = null;
            this.txtPartsNum.InputtingVerifyCondition = null;
            this.txtPartsNum.Location = new System.Drawing.Point(521, 68);
            this.txtPartsNum.MaxLengh = 32767;
            this.txtPartsNum.Multiline = false;
            this.txtPartsNum.Name = "txtPartsNum";
            this.txtPartsNum.Radius = 3;
            this.txtPartsNum.ReadOnly = false;
            this.txtPartsNum.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsNum.ShowError = false;
            this.txtPartsNum.Size = new System.Drawing.Size(148, 23);
            this.txtPartsNum.TabIndex = 58;
            this.txtPartsNum.Tag = "drawing_num";
            this.txtPartsNum.UseSystemPasswordChar = false;
            this.txtPartsNum.Value = "";
            this.txtPartsNum.VerifyCondition = null;
            this.txtPartsNum.VerifyType = null;
            this.txtPartsNum.VerifyTypeName = null;
            this.txtPartsNum.WaterMark = null;
            this.txtPartsNum.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtcPartsCode
            // 
            this.txtcPartsCode.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcPartsCode.Location = new System.Drawing.Point(521, 24);
            this.txtcPartsCode.Name = "txtcPartsCode";
            this.txtcPartsCode.ReadOnly = false;
            this.txtcPartsCode.Size = new System.Drawing.Size(150, 24);
            this.txtcPartsCode.TabIndex = 57;
            this.txtcPartsCode.Tag = "parts_code";
            this.txtcPartsCode.ToolTipTitle = "";
            this.txtcPartsCode.ChooserClick += new System.EventHandler(this.txtcPartsCode_ChooserClick);
            // 
            // cboOrg
            // 
            this.cboOrg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboOrg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrg.FormattingEnabled = true;
            this.cboOrg.Location = new System.Drawing.Point(323, 25);
            this.cboOrg.Name = "cboOrg";
            this.cboOrg.Size = new System.Drawing.Size(121, 22);
            this.cboOrg.TabIndex = 56;
            this.cboOrg.Tag = "org_id";
            // 
            // cboCompany
            // 
            this.cboCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(78, 25);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(121, 22);
            this.cboCompany.TabIndex = 55;
            this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.cboCompany_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(725, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 54;
            this.label8.Text = "品牌：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(450, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 53;
            this.label7.Text = "配件图号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(701, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 52;
            this.label4.Text = "配件名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(450, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 51;
            this.label3.Text = "配件编码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(276, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 50;
            this.label2.Text = "部门：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 49;
            this.label1.Text = "公司：";
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
            this.colID,
            this.Column1,
            this.Column2,
            this.Column3,
            this.colPrice,
            this.colPlanNum,
            this.colPlanMoney,
            this.colOrderMoney,
            this.colCompleteMoney,
            this.colCompleteNum,
            this.colUnfinishedNum,
            this.colUnfinishedMoney,
            this.colCompleteRate,
            this.Column13});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReport.DefaultCellStyle = dataGridViewCellStyle4;
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
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvReport.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvReport.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(234)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvReport.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvReport.RowTemplate.Height = 23;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(1069, 379);
            this.dgvReport.TabIndex = 1;
            this.dgvReport.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReport_CellDoubleClick);
            // 
            // colID
            // 
            this.colID.DataPropertyName = "sale_plan_id";
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Visible = false;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "公司";
            this.Column1.HeaderText = "公司";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "单据编码";
            this.Column2.HeaderText = "单据编码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "单据日期";
            this.Column3.HeaderText = "单据日期";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // colPrice
            // 
            this.colPrice.DataPropertyName = "单价";
            this.colPrice.HeaderText = "单价";
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            this.colPrice.Width = 60;
            // 
            // colPlanNum
            // 
            this.colPlanNum.DataPropertyName = "计划数量";
            this.colPlanNum.HeaderText = "计划数量";
            this.colPlanNum.Name = "colPlanNum";
            this.colPlanNum.ReadOnly = true;
            this.colPlanNum.Width = 60;
            // 
            // colPlanMoney
            // 
            this.colPlanMoney.DataPropertyName = "计划金额";
            this.colPlanMoney.HeaderText = "计划金额";
            this.colPlanMoney.Name = "colPlanMoney";
            this.colPlanMoney.ReadOnly = true;
            this.colPlanMoney.Width = 60;
            // 
            // colOrderMoney
            // 
            this.colOrderMoney.DataPropertyName = "已订金额";
            this.colOrderMoney.HeaderText = "已订金额";
            this.colOrderMoney.Name = "colOrderMoney";
            this.colOrderMoney.ReadOnly = true;
            this.colOrderMoney.Width = 60;
            // 
            // colCompleteMoney
            // 
            this.colCompleteMoney.DataPropertyName = "完成数量";
            this.colCompleteMoney.HeaderText = "完成金额";
            this.colCompleteMoney.Name = "colCompleteMoney";
            this.colCompleteMoney.ReadOnly = true;
            this.colCompleteMoney.Width = 60;
            // 
            // colCompleteNum
            // 
            this.colCompleteNum.DataPropertyName = "完成金额";
            this.colCompleteNum.HeaderText = "完成数量";
            this.colCompleteNum.Name = "colCompleteNum";
            this.colCompleteNum.ReadOnly = true;
            this.colCompleteNum.Width = 60;
            // 
            // colUnfinishedNum
            // 
            this.colUnfinishedNum.DataPropertyName = "未完成数量";
            this.colUnfinishedNum.HeaderText = "未完成数量";
            this.colUnfinishedNum.Name = "colUnfinishedNum";
            this.colUnfinishedNum.ReadOnly = true;
            this.colUnfinishedNum.Width = 70;
            // 
            // colUnfinishedMoney
            // 
            this.colUnfinishedMoney.DataPropertyName = "未完成金额";
            this.colUnfinishedMoney.HeaderText = "未完成金额";
            this.colUnfinishedMoney.Name = "colUnfinishedMoney";
            this.colUnfinishedMoney.ReadOnly = true;
            this.colUnfinishedMoney.Width = 70;
            // 
            // colCompleteRate
            // 
            this.colCompleteRate.DataPropertyName = "完成比率";
            dataGridViewCellStyle3.Format = "P";
            dataGridViewCellStyle3.NullValue = null;
            this.colCompleteRate.DefaultCellStyle = dataGridViewCellStyle3;
            this.colCompleteRate.HeaderText = "完成比率";
            this.colCompleteRate.Name = "colCompleteRate";
            this.colCompleteRate.ReadOnly = true;
            this.colCompleteRate.Width = 60;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "备注";
            this.Column13.HeaderText = "备注";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // UCSalePlanDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCSalePlanDetail";
            this.Load += new System.EventHandler(this.UCSalePlanDetail_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DateInterval diCreateTime;
        private ServiceStationClient.ComponentUI.TextBoxEx txtPartsBrand;
        private ServiceStationClient.ComponentUI.TextBoxEx txtPartsName;
        private ServiceStationClient.ComponentUI.TextBoxEx txtPartsNum;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtcPartsCode;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboOrg;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboCompany;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.DataGridViewReport dgvReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlanNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlanMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompleteMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompleteNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnfinishedNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnfinishedMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompleteRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
    }
}
