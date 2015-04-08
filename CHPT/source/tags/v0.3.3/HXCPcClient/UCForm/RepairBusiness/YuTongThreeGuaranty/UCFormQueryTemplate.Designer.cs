namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty
{
    partial class UCFormQueryTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCFormQueryTemplate));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnl_query = new ServiceStationClient.ComponentUI.PanelEx();
            this.dtp_date_cycle_e = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dtp_date_cycle_s = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dtp_datetime = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_chooser = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txt_textbox = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.cbo_combobox = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btn_query = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btn_clear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgv_table = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.drchk_check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.drtxt_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_page = new ServiceStationClient.ComponentUI.PanelEx();
            this.pageQ = new ServiceStationClient.ComponentUI.WinFormPager();
            this.pnl_query.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_table)).BeginInit();
            this.pnl_page.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_query
            // 
            this.pnl_query.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnl_query.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnl_query.Controls.Add(this.dtp_date_cycle_e);
            this.pnl_query.Controls.Add(this.dtp_date_cycle_s);
            this.pnl_query.Controls.Add(this.dtp_datetime);
            this.pnl_query.Controls.Add(this.label2);
            this.pnl_query.Controls.Add(this.txt_chooser);
            this.pnl_query.Controls.Add(this.txt_textbox);
            this.pnl_query.Controls.Add(this.cbo_combobox);
            this.pnl_query.Controls.Add(this.label1);
            this.pnl_query.Controls.Add(this.btn_query);
            this.pnl_query.Controls.Add(this.btn_clear);
            this.pnl_query.Controls.Add(this.label12);
            this.pnl_query.Controls.Add(this.label7);
            this.pnl_query.Controls.Add(this.label6);
            this.pnl_query.Controls.Add(this.label5);
            this.pnl_query.Location = new System.Drawing.Point(3, 36);
            this.pnl_query.Name = "pnl_query";
            this.pnl_query.Size = new System.Drawing.Size(1024, 98);
            this.pnl_query.TabIndex = 7;
            // 
            // dtp_date_cycle_e
            // 
            this.dtp_date_cycle_e.Location = new System.Drawing.Point(741, 58);
            this.dtp_date_cycle_e.Name = "dtp_date_cycle_e";
            this.dtp_date_cycle_e.Size = new System.Drawing.Size(125, 21);
            this.dtp_date_cycle_e.TabIndex = 120;
            // 
            // dtp_date_cycle_s
            // 
            this.dtp_date_cycle_s.Location = new System.Drawing.Point(421, 58);
            this.dtp_date_cycle_s.Name = "dtp_date_cycle_s";
            this.dtp_date_cycle_s.Size = new System.Drawing.Size(125, 21);
            this.dtp_date_cycle_s.TabIndex = 119;
            // 
            // dtp_datetime
            // 
            this.dtp_datetime.Location = new System.Drawing.Point(146, 58);
            this.dtp_datetime.Name = "dtp_datetime";
            this.dtp_datetime.Size = new System.Drawing.Size(125, 21);
            this.dtp_datetime.TabIndex = 118;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 117;
            this.label2.Text = "时间查询条件：";
            // 
            // txt_chooser
            // 
            this.txt_chooser.Location = new System.Drawing.Point(421, 16);
            this.txt_chooser.Name = "txt_chooser";
            this.txt_chooser.ReadOnly = false;
            this.txt_chooser.Size = new System.Drawing.Size(125, 22);
            this.txt_chooser.TabIndex = 115;
            // 
            // txt_textbox
            // 
            this.txt_textbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_textbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_textbox.BackColor = System.Drawing.Color.Transparent;
            this.txt_textbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_textbox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_textbox.ForeImage = null;
            this.txt_textbox.Location = new System.Drawing.Point(146, 16);
            this.txt_textbox.MaxLengh = 32767;
            this.txt_textbox.Multiline = false;
            this.txt_textbox.Name = "txt_textbox";
            this.txt_textbox.Radius = 3;
            this.txt_textbox.ReadOnly = false;
            this.txt_textbox.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_textbox.Size = new System.Drawing.Size(125, 22);
            this.txt_textbox.TabIndex = 114;
            this.txt_textbox.UseSystemPasswordChar = false;
            this.txt_textbox.WaterMark = null;
            this.txt_textbox.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // cbo_combobox
            // 
            this.cbo_combobox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_combobox.FormattingEnabled = true;
            this.cbo_combobox.Location = new System.Drawing.Point(741, 17);
            this.cbo_combobox.Name = "cbo_combobox";
            this.cbo_combobox.Size = new System.Drawing.Size(125, 21);
            this.cbo_combobox.TabIndex = 113;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(632, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 112;
            this.label1.Text = "下拉框查询条件：";
            // 
            // btn_query
            // 
            this.btn_query.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_query.BackgroundImage")));
            this.btn_query.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_query.Caption = "查询";
            this.btn_query.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_query.DownImage = ((System.Drawing.Image)(resources.GetObject("btn_query.DownImage")));
            this.btn_query.Location = new System.Drawing.Point(938, 55);
            this.btn_query.MoveImage = ((System.Drawing.Image)(resources.GetObject("btn_query.MoveImage")));
            this.btn_query.Name = "btn_query";
            this.btn_query.NormalImage = ((System.Drawing.Image)(resources.GetObject("btn_query.NormalImage")));
            this.btn_query.Size = new System.Drawing.Size(60, 28);
            this.btn_query.TabIndex = 110;
            // 
            // btn_clear
            // 
            this.btn_clear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_clear.BackgroundImage")));
            this.btn_clear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_clear.Caption = "清除";
            this.btn_clear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_clear.DownImage = ((System.Drawing.Image)(resources.GetObject("btn_clear.DownImage")));
            this.btn_clear.Location = new System.Drawing.Point(938, 13);
            this.btn_clear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btn_clear.MoveImage")));
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btn_clear.NormalImage")));
            this.btn_clear.Size = new System.Drawing.Size(60, 28);
            this.btn_clear.TabIndex = 109;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(628, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "到";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(324, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "时间查询条件：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(312, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "选择器查询条件：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "文本查询条件：";
            // 
            // dgv_table
            // 
            this.dgv_table.AllowUserToAddRows = false;
            this.dgv_table.AllowUserToDeleteRows = false;
            this.dgv_table.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgv_table.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_table.BackgroundColor = System.Drawing.Color.White;
            this.dgv_table.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_table.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.drchk_check,
            this.drtxt_id,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgv_table.EnableHeadersVisualStyles = false;
            this.dgv_table.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgv_table.Location = new System.Drawing.Point(3, 140);
            this.dgv_table.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgv_table.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv_table.MergeColumnNames")));
            this.dgv_table.MultiSelect = false;
            this.dgv_table.Name = "dgv_table";
            this.dgv_table.ReadOnly = true;
            this.dgv_table.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgv_table.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_table.RowTemplate.Height = 23;
            this.dgv_table.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_table.ShowCheckBox = true;
            this.dgv_table.Size = new System.Drawing.Size(1024, 413);
            this.dgv_table.TabIndex = 8;
            // 
            // drchk_check
            // 
            this.drchk_check.HeaderText = "";
            this.drchk_check.MinimumWidth = 18;
            this.drchk_check.Name = "drchk_check";
            this.drchk_check.ReadOnly = true;
            this.drchk_check.Width = 18;
            // 
            // drtxt_id
            // 
            this.drtxt_id.DataPropertyName = "id";
            this.drtxt_id.HeaderText = "标识";
            this.drtxt_id.Name = "drtxt_id";
            this.drtxt_id.ReadOnly = true;
            this.drtxt_id.Width = 38;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Column7";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // pnl_page
            // 
            this.pnl_page.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(223)))), ((int)(((byte)(255)))));
            this.pnl_page.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnl_page.Controls.Add(this.pageQ);
            this.pnl_page.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_page.Location = new System.Drawing.Point(0, 559);
            this.pnl_page.Name = "pnl_page";
            this.pnl_page.Padding = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.pnl_page.Size = new System.Drawing.Size(1030, 30);
            this.pnl_page.TabIndex = 19;
            // 
            // pageQ
            // 
            this.pageQ.BackColor = System.Drawing.Color.Transparent;
            this.pageQ.BtnTextNext = "下页";
            this.pageQ.BtnTextPrevious = "上页";
            this.pageQ.Dock = System.Windows.Forms.DockStyle.Right;
            this.pageQ.Location = new System.Drawing.Point(502, 0);
            this.pageQ.Name = "pageQ";
            this.pageQ.PageCount = 0;
            this.pageQ.PageSize = 15;
            this.pageQ.RecordCount = 0;
            this.pageQ.Size = new System.Drawing.Size(428, 30);
            this.pageQ.TabIndex = 5;
            // 
            // UCFormQueryTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_page);
            this.Controls.Add(this.dgv_table);
            this.Controls.Add(this.pnl_query);
            this.Name = "UCFormQueryTemplate";
            this.Controls.SetChildIndex(this.pnl_query, 0);
            this.Controls.SetChildIndex(this.dgv_table, 0);
            this.Controls.SetChildIndex(this.pnl_page, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.pnl_query.ResumeLayout(false);
            this.pnl_query.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_table)).EndInit();
            this.pnl_page.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx pnl_query;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txt_chooser;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_textbox;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbo_combobox;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ButtonEx btn_query;
        private ServiceStationClient.ComponentUI.ButtonEx btn_clear;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgv_table;
        private System.Windows.Forms.DataGridViewCheckBoxColumn drchk_check;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private ServiceStationClient.ComponentUI.PanelEx pnl_page;
        private ServiceStationClient.ComponentUI.WinFormPager pageQ;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtp_datetime;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtp_date_cycle_s;
        private ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtp_date_cycle_e;

    }
}
