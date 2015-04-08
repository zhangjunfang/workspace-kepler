namespace HXCPcClient.UCForm.BusinessAnalysis.RepairReport
{
    partial class UCRepairLaborHour
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCRepairLaborHour));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cboCompany = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.dateInterval1 = new ServiceStationClient.ComponentUI.DateInterval();
            this.txtcServiceman = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPushMoney = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvReport = new ServiceStationClient.ComponentUI.DataGridViewReport();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDispatchingNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colManHour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSumMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPushMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSearch.SuspendLayout();
            this.pnlReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Controls.Add(this.txtPushMoney);
            this.pnlSearch.Controls.Add(this.cboCompany);
            this.pnlSearch.Controls.Add(this.dateInterval1);
            this.pnlSearch.Controls.Add(this.txtcServiceman);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.SetChildIndex(this.btnClear, 0);
            this.pnlSearch.Controls.SetChildIndex(this.btnSearch, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label4, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label3, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label1, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtcServiceman, 0);
            this.pnlSearch.Controls.SetChildIndex(this.dateInterval1, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboCompany, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtPushMoney, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label5, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.Caption = "";
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(80, 24);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.Caption = "";
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(80, 24);
            // 
            // pnlReport
            // 
            this.pnlReport.Controls.Add(this.dgvReport);
            // 
            // cboCompany
            // 
            this.cboCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(79, 21);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(121, 22);
            this.cboCompany.TabIndex = 14;
            this.cboCompany.Tag = "e.com_id";
            // 
            // dateInterval1
            // 
            this.dateInterval1.BackColor = System.Drawing.Color.Transparent;
            this.dateInterval1.EndDate = "2015-02-03";
            this.dateInterval1.Location = new System.Drawing.Point(34, 60);
            this.dateInterval1.Name = "dateInterval1";
            this.dateInterval1.ShowFormat = "yyyy-MM-dd";
            this.dateInterval1.Size = new System.Drawing.Size(446, 27);
            this.dateInterval1.StartDate = "2015-02-01";
            this.dateInterval1.TabIndex = 15;
            this.dateInterval1.Tag = "b.create_time";
            this.dateInterval1.Text = "结算日期从：";
            // 
            // txtcServiceman
            // 
            this.txtcServiceman.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcServiceman.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtcServiceman.Location = new System.Drawing.Point(323, 24);
            this.txtcServiceman.Name = "txtcServiceman";
            this.txtcServiceman.ReadOnly = false;
            this.txtcServiceman.Size = new System.Drawing.Size(127, 24);
            this.txtcServiceman.TabIndex = 17;
            this.txtcServiceman.Tag = "dispatch_name";
            this.txtcServiceman.ToolTipTitle = "";
            this.txtcServiceman.ChooserClick += new System.EventHandler(this.txtcServiceman_ChooserClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "公司：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(252, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "维修人员：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(502, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "提成金额：";
            // 
            // txtPushMoney
            // 
            this.txtPushMoney.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPushMoney.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPushMoney.BackColor = System.Drawing.Color.Transparent;
            this.txtPushMoney.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtPushMoney.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtPushMoney.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPushMoney.ForeImage = null;
            this.txtPushMoney.InputtingVerifyCondition = null;
            this.txtPushMoney.Location = new System.Drawing.Point(576, 22);
            this.txtPushMoney.MaxLengh = 32767;
            this.txtPushMoney.Multiline = false;
            this.txtPushMoney.Name = "txtPushMoney";
            this.txtPushMoney.Radius = 3;
            this.txtPushMoney.ReadOnly = false;
            this.txtPushMoney.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtPushMoney.ShowError = true;
            this.txtPushMoney.Size = new System.Drawing.Size(69, 23);
            this.txtPushMoney.TabIndex = 18;
            this.txtPushMoney.UseSystemPasswordChar = false;
            this.txtPushMoney.Value = "";
            this.txtPushMoney.VerifyCondition = null;
            this.txtPushMoney.VerifyType = null;
            this.txtPushMoney.VerifyTypeName = "UFloat";
            this.txtPushMoney.WaterMark = null;
            this.txtPushMoney.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(651, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "%";
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
            this.colDispatchingNum,
            this.colManHour,
            this.colSumMoney,
            this.colPushMoney});
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
            this.dgvReport.Size = new System.Drawing.Size(1069, 379);
            this.dgvReport.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "dispatch_name";
            this.Column1.HeaderText = "人员名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // colDispatchingNum
            // 
            this.colDispatchingNum.DataPropertyName = "dispatching_num";
            this.colDispatchingNum.HeaderText = "派工次数";
            this.colDispatchingNum.Name = "colDispatchingNum";
            this.colDispatchingNum.ReadOnly = true;
            // 
            // colManHour
            // 
            this.colManHour.DataPropertyName = "man_hour";
            this.colManHour.HeaderText = "维修工时合计";
            this.colManHour.Name = "colManHour";
            this.colManHour.ReadOnly = true;
            // 
            // colSumMoney
            // 
            this.colSumMoney.DataPropertyName = "sum_money";
            this.colSumMoney.HeaderText = "工时金额合计";
            this.colSumMoney.Name = "colSumMoney";
            this.colSumMoney.ReadOnly = true;
            // 
            // colPushMoney
            // 
            this.colPushMoney.DataPropertyName = "push_money";
            this.colPushMoney.HeaderText = "提成金额";
            this.colPushMoney.Name = "colPushMoney";
            this.colPushMoney.ReadOnly = true;
            // 
            // UCRepairLaborHour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCRepairLaborHour";
            this.Load += new System.EventHandler(this.UCRepairLaborHour_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.ComboBoxEx cboCompany;
        private ServiceStationClient.ComponentUI.DateInterval dateInterval1;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtcServiceman;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.TextBoxEx txtPushMoney;
        private ServiceStationClient.ComponentUI.DataGridViewReport dgvReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDispatchingNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colManHour;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSumMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPushMoney;
    }
}
