namespace HXCPcClient.UCForm.BusinessAnalysis.ARAPReport
{
    partial class UCPayableSummariz
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPayableSummariz));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cboorg_id = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cboCompany = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.dicreate_time = new ServiceStationClient.ComponentUI.DateInterval();
            this.txtSup_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.cboSup_type = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtcSup_code = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvReport = new ServiceStationClient.ComponentUI.DataGridViewReport();
            this.colSupCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBenQi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShangYe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYingFu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXianJin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQiMo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSearch.SuspendLayout();
            this.pnlReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.cboorg_id);
            this.pnlSearch.Controls.Add(this.cboCompany);
            this.pnlSearch.Controls.Add(this.label13);
            this.pnlSearch.Controls.Add(this.label14);
            this.pnlSearch.Controls.Add(this.dicreate_time);
            this.pnlSearch.Controls.Add(this.txtSup_name);
            this.pnlSearch.Controls.Add(this.cboSup_type);
            this.pnlSearch.Controls.Add(this.txtcSup_code);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Controls.SetChildIndex(this.btnClear, 0);
            this.pnlSearch.Controls.SetChildIndex(this.btnSearch, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label5, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label6, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label7, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtcSup_code, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboSup_type, 0);
            this.pnlSearch.Controls.SetChildIndex(this.txtSup_name, 0);
            this.pnlSearch.Controls.SetChildIndex(this.dicreate_time, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label14, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label13, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboCompany, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboorg_id, 0);
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
            // cboorg_id
            // 
            this.cboorg_id.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboorg_id.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboorg_id.FormattingEnabled = true;
            this.cboorg_id.Location = new System.Drawing.Point(801, 71);
            this.cboorg_id.Name = "cboorg_id";
            this.cboorg_id.Size = new System.Drawing.Size(121, 22);
            this.cboorg_id.TabIndex = 231;
            this.cboorg_id.Tag = "org_id";
            // 
            // cboCompany
            // 
            this.cboCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(586, 71);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(121, 22);
            this.cboCompany.TabIndex = 230;
            this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.cboCompany_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(537, 76);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 227;
            this.label13.Text = "公司：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(754, 76);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 228;
            this.label14.Text = "部门：";
            // 
            // dicreate_time
            // 
            this.dicreate_time.BackColor = System.Drawing.Color.Transparent;
            this.dicreate_time.EndDate = "2014-12-31";
            this.dicreate_time.Location = new System.Drawing.Point(55, 68);
            this.dicreate_time.Name = "dicreate_time";
            this.dicreate_time.ShowFormat = "yyyy-MM-dd";
            this.dicreate_time.Size = new System.Drawing.Size(411, 28);
            this.dicreate_time.StartDate = "2014-12-01";
            this.dicreate_time.TabIndex = 229;
            this.dicreate_time.Tag = "";
            // 
            // txtSup_name
            // 
            this.txtSup_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtSup_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtSup_name.BackColor = System.Drawing.Color.Transparent;
            this.txtSup_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtSup_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtSup_name.ForeImage = null;
            this.txtSup_name.Location = new System.Drawing.Point(360, 24);
            this.txtSup_name.MaxLengh = 32767;
            this.txtSup_name.Multiline = false;
            this.txtSup_name.Name = "txtSup_name";
            this.txtSup_name.Radius = 3;
            this.txtSup_name.ReadOnly = false;
            this.txtSup_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtSup_name.ShowError = false;
            this.txtSup_name.Size = new System.Drawing.Size(121, 23);
            this.txtSup_name.TabIndex = 223;
            this.txtSup_name.Tag = "sup_full_name";
            this.txtSup_name.UseSystemPasswordChar = false;
            this.txtSup_name.Value = "";
            this.txtSup_name.VerifyCondition = null;
            this.txtSup_name.VerifyType = null;
            this.txtSup_name.WaterMark = null;
            this.txtSup_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // cboSup_type
            // 
            this.cboSup_type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSup_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSup_type.FormattingEnabled = true;
            this.cboSup_type.Location = new System.Drawing.Point(584, 24);
            this.cboSup_type.Name = "cboSup_type";
            this.cboSup_type.Size = new System.Drawing.Size(123, 22);
            this.cboSup_type.TabIndex = 224;
            this.cboSup_type.Tag = "sup_type";
            // 
            // txtcSup_code
            // 
            this.txtcSup_code.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtcSup_code.Location = new System.Drawing.Point(119, 23);
            this.txtcSup_code.Name = "txtcSup_code";
            this.txtcSup_code.ReadOnly = false;
            this.txtcSup_code.Size = new System.Drawing.Size(121, 24);
            this.txtcSup_code.TabIndex = 222;
            this.txtcSup_code.Tag = "sup_code";
            this.txtcSup_code.ToolTipTitle = "";
            this.txtcSup_code.ChooserClick += new System.EventHandler(this.txtcSup_code_ChooserClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(501, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 221;
            this.label7.Text = "供应商类别：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(277, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 220;
            this.label6.Text = "供应商名称：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 219;
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
            this.colSupCode,
            this.colSupName,
            this.colQiChu,
            this.colBenQi,
            this.colShangYe,
            this.colYingFu,
            this.colXianJin,
            this.colQiMo});
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
            this.dgvReport.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReport_CellDoubleClick);
            // 
            // colSupCode
            // 
            this.colSupCode.DataPropertyName = "sup_code";
            this.colSupCode.HeaderText = "供应商编码";
            this.colSupCode.Name = "colSupCode";
            this.colSupCode.ReadOnly = true;
            this.colSupCode.Width = 130;
            // 
            // colSupName
            // 
            this.colSupName.DataPropertyName = "sup_full_name";
            this.colSupName.HeaderText = "供应商名称";
            this.colSupName.Name = "colSupName";
            this.colSupName.ReadOnly = true;
            this.colSupName.Width = 130;
            // 
            // colQiChu
            // 
            this.colQiChu.DataPropertyName = "期初应付款";
            this.colQiChu.HeaderText = "期初应付款";
            this.colQiChu.Name = "colQiChu";
            this.colQiChu.ReadOnly = true;
            this.colQiChu.Width = 120;
            // 
            // colBenQi
            // 
            this.colBenQi.DataPropertyName = "本期增加应付款";
            this.colBenQi.HeaderText = "本期增加应付款";
            this.colBenQi.Name = "colBenQi";
            this.colBenQi.ReadOnly = true;
            this.colBenQi.Width = 130;
            // 
            // colShangYe
            // 
            this.colShangYe.DataPropertyName = "其中商业折扣";
            this.colShangYe.HeaderText = "其中商业折扣";
            this.colShangYe.Name = "colShangYe";
            this.colShangYe.ReadOnly = true;
            this.colShangYe.Width = 120;
            // 
            // colYingFu
            // 
            this.colYingFu.DataPropertyName = "本期承收应付款";
            this.colYingFu.HeaderText = "本期承收应付款";
            this.colYingFu.Name = "colYingFu";
            this.colYingFu.ReadOnly = true;
            this.colYingFu.Width = 130;
            // 
            // colXianJin
            // 
            this.colXianJin.DataPropertyName = "其中现金折扣";
            this.colXianJin.HeaderText = "其中现金折扣";
            this.colXianJin.Name = "colXianJin";
            this.colXianJin.ReadOnly = true;
            this.colXianJin.Width = 120;
            // 
            // colQiMo
            // 
            this.colQiMo.DataPropertyName = "期末结存应付额";
            this.colQiMo.HeaderText = "期末结存应付额";
            this.colQiMo.Name = "colQiMo";
            this.colQiMo.ReadOnly = true;
            this.colQiMo.Width = 130;
            // 
            // UCPayableSummariz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCPayableSummariz";
            this.Load += new System.EventHandler(this.UCPayableSummariz_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.ComboBoxEx cboorg_id;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboCompany;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private ServiceStationClient.ComponentUI.DateInterval dicreate_time;
        private ServiceStationClient.ComponentUI.TextBoxEx txtSup_name;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboSup_type;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtcSup_code;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.DataGridViewReport dgvReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSupCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQiChu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBenQi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShangYe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYingFu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colXianJin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQiMo;
    }
}
