namespace HXCPcClient.UCForm.BusinessAnalysis.PurchaseReport
{
    partial class UCPurchasePlanSummariz
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPurchasePlanSummariz));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvSummariz = new ServiceStationClient.ComponentUI.DataGridViewReport();
            this.colPartsCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartsNme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDrawing_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParts_brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateInterval1 = new ServiceStationClient.ComponentUI.DateInterval();
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
            this.pnlSearch.SuspendLayout();
            this.pnlReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSummariz)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.dateInterval1);
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
            this.pnlSearch.Controls.SetChildIndex(this.dateInterval1, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlReport
            // 
            this.pnlReport.Controls.Add(this.dgvSummariz);
            // 
            // dgvSummariz
            // 
            this.dgvSummariz.AllowUserToAddRows = false;
            this.dgvSummariz.AllowUserToDeleteRows = false;
            this.dgvSummariz.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvSummariz.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSummariz.BackgroundColor = System.Drawing.Color.White;
            this.dgvSummariz.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSummariz.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSummariz.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSummariz.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPartsCode,
            this.colPartsNme,
            this.colDrawing_num,
            this.colParts_brand,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13});
            this.dgvSummariz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSummariz.EnableHeadersVisualStyles = false;
            this.dgvSummariz.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvSummariz.Location = new System.Drawing.Point(0, 0);
            this.dgvSummariz.MultiSelect = false;
            this.dgvSummariz.Name = "dgvSummariz";
            this.dgvSummariz.ReadOnly = true;
            this.dgvSummariz.RowHeadersVisible = false;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvSummariz.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSummariz.RowTemplate.Height = 23;
            this.dgvSummariz.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSummariz.Size = new System.Drawing.Size(1069, 379);
            this.dgvSummariz.TabIndex = 5;
            this.dgvSummariz.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSummariz_CellDoubleClick);
            // 
            // colPartsCode
            // 
            this.colPartsCode.DataPropertyName = "配件编码";
            this.colPartsCode.HeaderText = "配件编码";
            this.colPartsCode.Name = "colPartsCode";
            this.colPartsCode.ReadOnly = true;
            // 
            // colPartsNme
            // 
            this.colPartsNme.DataPropertyName = "配件名称";
            this.colPartsNme.HeaderText = "配件名称";
            this.colPartsNme.Name = "colPartsNme";
            this.colPartsNme.ReadOnly = true;
            // 
            // colDrawing_num
            // 
            this.colDrawing_num.DataPropertyName = "配件图号";
            this.colDrawing_num.HeaderText = "配件图号";
            this.colDrawing_num.Name = "colDrawing_num";
            this.colDrawing_num.ReadOnly = true;
            this.colDrawing_num.Width = 80;
            // 
            // colParts_brand
            // 
            this.colParts_brand.DataPropertyName = "品牌";
            this.colParts_brand.HeaderText = "品牌";
            this.colParts_brand.Name = "colParts_brand";
            this.colParts_brand.ReadOnly = true;
            this.colParts_brand.Width = 80;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "厂商编码";
            this.Column4.HeaderText = "厂商编码";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "单位";
            this.Column5.HeaderText = "单位";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 60;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "计划数量";
            this.Column6.HeaderText = "计划数量";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 60;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "计划金额";
            this.Column7.HeaderText = "计划金额";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 60;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "已订金额";
            this.Column8.HeaderText = "已订金额";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 60;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "完成数量";
            this.Column9.HeaderText = "完成数量";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 60;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "完成金额";
            this.Column10.HeaderText = "完成金额";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 60;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "未完成数量";
            this.Column11.HeaderText = "未完成数量";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 70;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "未完成金额";
            this.Column12.HeaderText = "未完成金额";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 70;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "完成比率";
            dataGridViewCellStyle3.Format = "P";
            dataGridViewCellStyle3.NullValue = null;
            this.Column13.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column13.HeaderText = "完成比率";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 60;
            // 
            // dateInterval1
            // 
            this.dateInterval1.BackColor = System.Drawing.Color.Transparent;
            this.dateInterval1.EndDate = "2014-12-02";
            this.dateInterval1.Location = new System.Drawing.Point(28, 68);
            this.dateInterval1.Name = "dateInterval1";
            this.dateInterval1.ShowFormat = "yyyy-MM-dd";
            this.dateInterval1.Size = new System.Drawing.Size(411, 27);
            this.dateInterval1.StartDate = "2014-12-01";
            this.dateInterval1.TabIndex = 61;
            this.dateInterval1.Tag = "create_time";
            // 
            // txtPartsBrand
            // 
            this.txtPartsBrand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPartsBrand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPartsBrand.BackColor = System.Drawing.Color.Transparent;
            this.txtPartsBrand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtPartsBrand.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtPartsBrand.ForeImage = null;
            this.txtPartsBrand.Location = new System.Drawing.Point(767, 68);
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
            this.txtPartsName.Location = new System.Drawing.Point(767, 25);
            this.txtPartsName.MaxLengh = 32767;
            this.txtPartsName.Multiline = false;
            this.txtPartsName.Name = "txtPartsName";
            this.txtPartsName.Radius = 3;
            this.txtPartsName.ReadOnly = false;
            this.txtPartsName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsName.Size = new System.Drawing.Size(148, 23);
            this.txtPartsName.TabIndex = 59;
            this.txtPartsName.Tag = "配件名称";
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
            this.txtPartsNum.Location = new System.Drawing.Point(516, 68);
            this.txtPartsNum.MaxLengh = 32767;
            this.txtPartsNum.Multiline = false;
            this.txtPartsNum.Name = "txtPartsNum";
            this.txtPartsNum.Radius = 3;
            this.txtPartsNum.ReadOnly = false;
            this.txtPartsNum.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsNum.Size = new System.Drawing.Size(148, 23);
            this.txtPartsNum.TabIndex = 58;
            this.txtPartsNum.Tag = "配件图号";
            this.txtPartsNum.UseSystemPasswordChar = false;
            this.txtPartsNum.WaterMark = null;
            this.txtPartsNum.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtcPartsCode
            // 
            this.txtcPartsCode.Location = new System.Drawing.Point(516, 24);
            this.txtcPartsCode.Name = "txtcPartsCode";
            this.txtcPartsCode.ReadOnly = false;
            this.txtcPartsCode.Size = new System.Drawing.Size(150, 24);
            this.txtcPartsCode.TabIndex = 57;
            this.txtcPartsCode.Tag = "配件编码";
            this.txtcPartsCode.ChooserClick += new System.EventHandler(this.txtcPartsCode_ChooserClick);
            // 
            // cboOrg
            // 
            this.cboOrg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboOrg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrg.FormattingEnabled = true;
            this.cboOrg.Location = new System.Drawing.Point(318, 25);
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
            this.cboCompany.Location = new System.Drawing.Point(73, 25);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(121, 22);
            this.cboCompany.TabIndex = 55;
            this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.cboCompany_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(720, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 54;
            this.label8.Text = "品牌：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(445, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 53;
            this.label7.Text = "配件图号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(696, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 52;
            this.label4.Text = "配件名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(445, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 51;
            this.label3.Text = "配件编码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 50;
            this.label2.Text = "部门：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 49;
            this.label1.Text = "公司：";
            // 
            // UCPurchasePlanSummariz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCPurchasePlanSummariz";
            this.Load += new System.EventHandler(this.UCPurchasePlanSummariz1_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSummariz)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewReport dgvSummariz;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartsCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartsNme;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDrawing_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParts_brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private ServiceStationClient.ComponentUI.DateInterval dateInterval1;
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
    }
}
