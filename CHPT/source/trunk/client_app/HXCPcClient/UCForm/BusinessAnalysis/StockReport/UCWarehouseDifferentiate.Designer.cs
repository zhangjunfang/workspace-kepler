namespace HXCPcClient.UCForm.BusinessAnalysis.StockReport
{
    partial class UCWarehouseDifferentiate
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCWarehouseDifferentiate));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtcPartsVehicle = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txtcPartsType = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboCompany = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtcPartsCode = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txtBrand = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtPartsNum = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtPartsName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpStop = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dgvReport = new ServiceStationClient.ComponentUI.DataGridViewReport();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSearch.SuspendLayout();
            this.pnlReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.dtpStop);
            this.pnlSearch.Controls.Add(this.txtcPartsVehicle);
            this.pnlSearch.Controls.Add(this.txtcPartsType);
            this.pnlSearch.Controls.Add(this.label8);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.cboCompany);
            this.pnlSearch.Controls.Add(this.txtcPartsCode);
            this.pnlSearch.Controls.Add(this.txtBrand);
            this.pnlSearch.Controls.Add(this.txtPartsNum);
            this.pnlSearch.Controls.Add(this.txtPartsName);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.SetChildIndex(this.btnClear, 0);
            this.pnlSearch.Controls.SetChildIndex(this.btnSearch, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label3, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label4, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label5, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label2, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label6, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label1, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtPartsName, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtPartsNum, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtBrand, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtcPartsCode, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboCompany, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label7, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label8, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtcPartsType, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtcPartsVehicle, 0);
            this.pnlSearch.Controls.SetChildIndex(this.dtpStop, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlReport
            // 
            this.pnlReport.Controls.Add(this.dgvReport);
            // 
            // txtcPartsVehicle
            // 
            this.txtcPartsVehicle.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcPartsVehicle.Location = new System.Drawing.Point(810, 21);
            this.txtcPartsVehicle.Name = "txtcPartsVehicle";
            this.txtcPartsVehicle.ReadOnly = false;
            this.txtcPartsVehicle.Size = new System.Drawing.Size(130, 25);
            this.txtcPartsVehicle.TabIndex = 97;
            this.txtcPartsVehicle.ToolTipTitle = "";
            this.txtcPartsVehicle.ChooserClick += new System.EventHandler(this.txtcPartsVehicle_ChooserClick);
            // 
            // txtcPartsType
            // 
            this.txtcPartsType.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcPartsType.Location = new System.Drawing.Point(559, 24);
            this.txtcPartsType.Name = "txtcPartsType";
            this.txtcPartsType.ReadOnly = false;
            this.txtcPartsType.Size = new System.Drawing.Size(145, 25);
            this.txtcPartsType.TabIndex = 96;
            this.txtcPartsType.ToolTipTitle = "";
            this.txtcPartsType.ChooserClick += new System.EventHandler(this.txtcPartsType_ChooserClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(739, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 95;
            this.label8.Text = "配件车型：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(488, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 94;
            this.label7.Text = "配件类别：";
            // 
            // cboCompany
            // 
            this.cboCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(94, 24);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(121, 22);
            this.cboCompany.TabIndex = 88;
            // 
            // txtcPartsCode
            // 
            this.txtcPartsCode.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcPartsCode.Location = new System.Drawing.Point(94, 65);
            this.txtcPartsCode.Name = "txtcPartsCode";
            this.txtcPartsCode.ReadOnly = false;
            this.txtcPartsCode.Size = new System.Drawing.Size(145, 25);
            this.txtcPartsCode.TabIndex = 90;
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
            this.txtBrand.ForeImage = null;
            this.txtBrand.InputtingVerifyCondition = null;
            this.txtBrand.Location = new System.Drawing.Point(810, 66);
            this.txtBrand.MaxLengh = 32767;
            this.txtBrand.Multiline = false;
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Radius = 3;
            this.txtBrand.ReadOnly = false;
            this.txtBrand.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtBrand.ShowError = false;
            this.txtBrand.Size = new System.Drawing.Size(130, 23);
            this.txtBrand.TabIndex = 93;
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
            this.txtPartsNum.ForeImage = null;
            this.txtPartsNum.InputtingVerifyCondition = null;
            this.txtPartsNum.Location = new System.Drawing.Point(559, 66);
            this.txtPartsNum.MaxLengh = 32767;
            this.txtPartsNum.Multiline = false;
            this.txtPartsNum.Name = "txtPartsNum";
            this.txtPartsNum.Radius = 3;
            this.txtPartsNum.ReadOnly = false;
            this.txtPartsNum.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsNum.ShowError = false;
            this.txtPartsNum.Size = new System.Drawing.Size(139, 23);
            this.txtPartsNum.TabIndex = 92;
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
            this.txtPartsName.ForeImage = null;
            this.txtPartsName.InputtingVerifyCondition = null;
            this.txtPartsName.Location = new System.Drawing.Point(345, 66);
            this.txtPartsName.MaxLengh = 32767;
            this.txtPartsName.Multiline = false;
            this.txtPartsName.Name = "txtPartsName";
            this.txtPartsName.Radius = 3;
            this.txtPartsName.ReadOnly = false;
            this.txtPartsName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPartsName.ShowError = false;
            this.txtPartsName.Size = new System.Drawing.Size(116, 23);
            this.txtPartsName.TabIndex = 91;
            this.txtPartsName.UseSystemPasswordChar = false;
            this.txtPartsName.Value = "";
            this.txtPartsName.VerifyCondition = null;
            this.txtPartsName.VerifyType = null;
            this.txtPartsName.VerifyTypeName = null;
            this.txtPartsName.WaterMark = null;
            this.txtPartsName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 82;
            this.label1.Text = "公司：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(763, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 87;
            this.label6.Text = "品牌：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 83;
            this.label2.Text = "终止日期：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(488, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 86;
            this.label5.Text = "配件图号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(274, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 85;
            this.label4.Text = "配件名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 84;
            this.label3.Text = "配件编码：";
            // 
            // dtpStop
            // 
            this.dtpStop.Location = new System.Drawing.Point(345, 25);
            this.dtpStop.Name = "dtpStop";
            this.dtpStop.Size = new System.Drawing.Size(112, 21);
            this.dtpStop.TabIndex = 98;
            // 
            // dgvReport
            // 
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.AllowUserToResizeRows = false;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(253)))), ((int)(((byte)(252)))));
            this.dgvReport.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle17.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReport.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvReport.Location = new System.Drawing.Point(0, 0);
            this.dgvReport.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvReport.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvReport.MergeColumnNames")));
            this.dgvReport.MultiSelect = false;
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvReport.RowHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvReport.RowHeadersVisible = false;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(234)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvReport.RowsDefaultCellStyle = dataGridViewCellStyle20;
            this.dgvReport.RowTemplate.Height = 23;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(1069, 379);
            this.dgvReport.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "配件编码";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "配件名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "规格";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "配件图号";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "品牌";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "厂商编码";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "单位";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "数量";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "金额";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "数量";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "金额";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // UCWarehouseDifferentiate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCWarehouseDifferentiate";
            this.Load += new System.EventHandler(this.UCWarehouseDifferentiate_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtcPartsVehicle;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtcPartsType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboCompany;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtcPartsCode;
        private ServiceStationClient.ComponentUI.TextBoxEx txtBrand;
        private ServiceStationClient.ComponentUI.TextBoxEx txtPartsNum;
        private ServiceStationClient.ComponentUI.TextBoxEx txtPartsName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtpStop;
        private ServiceStationClient.ComponentUI.DataGridViewReport dgvReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
    }
}
