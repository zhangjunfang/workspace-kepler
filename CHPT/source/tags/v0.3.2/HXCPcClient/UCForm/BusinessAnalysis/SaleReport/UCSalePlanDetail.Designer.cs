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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCSalePlanDetail));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.已订金额 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.未完成数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.未完成金额 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.diCreateTime.EndDate = "2014-12-02";
            this.diCreateTime.Location = new System.Drawing.Point(33, 68);
            this.diCreateTime.Name = "diCreateTime";
            this.diCreateTime.ShowFormat = "yyyy-MM-dd";
            this.diCreateTime.Size = new System.Drawing.Size(411, 27);
            this.diCreateTime.StartDate = "2014-12-01";
            this.diCreateTime.TabIndex = 61;
            this.diCreateTime.Tag = "create_time";
            // 
            // txtPartsBrand
            // 
            this.txtPartsBrand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPartsBrand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPartsBrand.BackColor = System.Drawing.Color.Transparent;
            this.txtPartsBrand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtPartsBrand.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtPartsBrand.ForeImage = null;
            this.txtPartsBrand.Location = new System.Drawing.Point(772, 68);
            this.txtPartsBrand.MaxLengh = 32767;
            this.txtPartsBrand.Multiline = false;
            this.txtPartsBrand.Name = "txtPartsBrand";
            this.txtPartsBrand.Radius = 3;
            this.txtPartsBrand.ReadOnly = false;
            this.txtPartsBrand.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsBrand.Size = new System.Drawing.Size(148, 23);
            this.txtPartsBrand.TabIndex = 60;
            this.txtPartsBrand.Tag = "品牌";
            this.txtPartsBrand.UseSystemPasswordChar = false;
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
            this.txtPartsName.Location = new System.Drawing.Point(772, 25);
            this.txtPartsName.MaxLengh = 32767;
            this.txtPartsName.Multiline = false;
            this.txtPartsName.Name = "txtPartsName";
            this.txtPartsName.Radius = 3;
            this.txtPartsName.ReadOnly = false;
            this.txtPartsName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsName.Size = new System.Drawing.Size(148, 23);
            this.txtPartsName.TabIndex = 59;
            this.txtPartsName.Tag = "parts_name";
            this.txtPartsName.UseSystemPasswordChar = false;
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
            this.txtPartsNum.Location = new System.Drawing.Point(521, 68);
            this.txtPartsNum.MaxLengh = 32767;
            this.txtPartsNum.Multiline = false;
            this.txtPartsNum.Name = "txtPartsNum";
            this.txtPartsNum.Radius = 3;
            this.txtPartsNum.ReadOnly = false;
            this.txtPartsNum.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsNum.Size = new System.Drawing.Size(148, 23);
            this.txtPartsNum.TabIndex = 58;
            this.txtPartsNum.Tag = "drawing_num";
            this.txtPartsNum.UseSystemPasswordChar = false;
            this.txtPartsNum.WaterMark = null;
            this.txtPartsNum.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtcPartsCode
            // 
            this.txtcPartsCode.Location = new System.Drawing.Point(521, 24);
            this.txtcPartsCode.Name = "txtcPartsCode";
            this.txtcPartsCode.ReadOnly = false;
            this.txtcPartsCode.Size = new System.Drawing.Size(150, 24);
            this.txtcPartsCode.TabIndex = 57;
            this.txtcPartsCode.Tag = "parts_code";
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
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvReport.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
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
            this.Column4,
            this.Column5,
            this.Column6,
            this.已订金额,
            this.Column8,
            this.Column9,
            this.未完成数量,
            this.未完成金额,
            this.Column12,
            this.Column13});
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.EnableHeadersVisualStyles = false;
            this.dgvReport.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvReport.Location = new System.Drawing.Point(0, 0);
            this.dgvReport.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvReport.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvReport.MergeColumnNames")));
            this.dgvReport.MultiSelect = false;
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvReport.RowsDefaultCellStyle = dataGridViewCellStyle4;
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
            // Column4
            // 
            this.Column4.DataPropertyName = "单价";
            this.Column4.HeaderText = "单价";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 60;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "计划数量";
            this.Column5.HeaderText = "计划数量";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 60;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "计划金额";
            this.Column6.HeaderText = "计划金额";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 60;
            // 
            // 已订金额
            // 
            this.已订金额.DataPropertyName = "已订金额";
            this.已订金额.HeaderText = "已订金额";
            this.已订金额.Name = "已订金额";
            this.已订金额.ReadOnly = true;
            this.已订金额.Width = 60;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "完成数量";
            this.Column8.HeaderText = "完成金额";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 60;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "完成金额";
            this.Column9.HeaderText = "完成数量";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 60;
            // 
            // 未完成数量
            // 
            this.未完成数量.DataPropertyName = "未完成数量";
            this.未完成数量.HeaderText = "未完成数量";
            this.未完成数量.Name = "未完成数量";
            this.未完成数量.ReadOnly = true;
            this.未完成数量.Width = 70;
            // 
            // 未完成金额
            // 
            this.未完成金额.DataPropertyName = "未完成金额";
            this.未完成金额.HeaderText = "未完成金额";
            this.未完成金额.Name = "未完成金额";
            this.未完成金额.ReadOnly = true;
            this.未完成金额.Width = 70;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "完成比率";
            dataGridViewCellStyle3.Format = "P";
            dataGridViewCellStyle3.NullValue = null;
            this.Column12.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column12.HeaderText = "完成比率";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 60;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn 已订金额;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn 未完成数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 未完成金额;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
    }
}
