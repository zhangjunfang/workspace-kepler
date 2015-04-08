namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuarantySettlement
{
    partial class UCThreeGuarantySettlementManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCThreeGuarantySettlementManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTable = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.drchk_check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.drtxt_st_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_tg_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_settlement_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_info_status_yt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_station_settlement_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_info_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_service_station_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_settlement_cycle_start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_settlement_cycle_end = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.cbo_yt_info_status = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cbo_station_info_status = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtp_settlement_cycle_end = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtp_settlement_cycle_start = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.pageQ = new ServiceStationClient.ComponentUI.WinFormPager();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1234, 30);
            // 
            // dgvTable
            // 
            this.dgvTable.AllowUserToAddRows = false;
            this.dgvTable.AllowUserToDeleteRows = false;
            this.dgvTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTable.BackgroundColor = System.Drawing.Color.White;
            this.dgvTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.drchk_check,
            this.drtxt_st_id,
            this.drtxt_tg_id,
            this.drtxt_settlement_no,
            this.drtxt_info_status_yt,
            this.drtxt_station_settlement_no,
            this.drtxt_info_status,
            this.drtxt_service_station_name,
            this.drtxt_create_time,
            this.drtxt_settlement_cycle_start,
            this.drtxt_settlement_cycle_end});
            this.dgvTable.EnableHeadersVisualStyles = false;
            this.dgvTable.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvTable.Location = new System.Drawing.Point(0, 128);
            this.dgvTable.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvTable.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvTable.MergeColumnNames")));
            this.dgvTable.MultiSelect = false;
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.ReadOnly = true;
            this.dgvTable.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvTable.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTable.RowTemplate.Height = 23;
            this.dgvTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTable.ShowCheckBox = true;
            this.dgvTable.Size = new System.Drawing.Size(1234, 529);
            this.dgvTable.TabIndex = 6;
            // 
            // drchk_check
            // 
            this.drchk_check.HeaderText = "";
            this.drchk_check.MinimumWidth = 18;
            this.drchk_check.Name = "drchk_check";
            this.drchk_check.ReadOnly = true;
            this.drchk_check.Width = 18;
            // 
            // drtxt_st_id
            // 
            this.drtxt_st_id.DataPropertyName = "st_id";
            this.drtxt_st_id.HeaderText = "标识";
            this.drtxt_st_id.Name = "drtxt_st_id";
            this.drtxt_st_id.ReadOnly = true;
            this.drtxt_st_id.Visible = false;
            this.drtxt_st_id.Width = 38;
            // 
            // drtxt_tg_id
            // 
            this.drtxt_tg_id.DataPropertyName = "tg_id";
            this.drtxt_tg_id.HeaderText = "三包服务单标识";
            this.drtxt_tg_id.Name = "drtxt_tg_id";
            this.drtxt_tg_id.ReadOnly = true;
            this.drtxt_tg_id.Visible = false;
            // 
            // drtxt_settlement_no
            // 
            this.drtxt_settlement_no.DataPropertyName = "settlement_no";
            this.drtxt_settlement_no.HeaderText = "宇通结算单号";
            this.drtxt_settlement_no.Name = "drtxt_settlement_no";
            this.drtxt_settlement_no.ReadOnly = true;
            this.drtxt_settlement_no.Width = 105;
            // 
            // drtxt_info_status_yt
            // 
            this.drtxt_info_status_yt.DataPropertyName = "info_status_yt";
            this.drtxt_info_status_yt.HeaderText = "宇通结算单状态";
            this.drtxt_info_status_yt.Name = "drtxt_info_status_yt";
            this.drtxt_info_status_yt.ReadOnly = true;
            this.drtxt_info_status_yt.Width = 117;
            // 
            // drtxt_station_settlement_no
            // 
            this.drtxt_station_settlement_no.DataPropertyName = "station_settlement_no";
            this.drtxt_station_settlement_no.HeaderText = "服务站结算单号";
            this.drtxt_station_settlement_no.Name = "drtxt_station_settlement_no";
            this.drtxt_station_settlement_no.ReadOnly = true;
            this.drtxt_station_settlement_no.Width = 117;
            // 
            // drtxt_info_status
            // 
            this.drtxt_info_status.DataPropertyName = "info_status";
            this.drtxt_info_status.HeaderText = "服务站结算单状态";
            this.drtxt_info_status.Name = "drtxt_info_status";
            this.drtxt_info_status.ReadOnly = true;
            this.drtxt_info_status.Width = 129;
            // 
            // drtxt_service_station_name
            // 
            this.drtxt_service_station_name.DataPropertyName = "service_station_name";
            this.drtxt_service_station_name.HeaderText = "服务站名称";
            this.drtxt_service_station_name.Name = "drtxt_service_station_name";
            this.drtxt_service_station_name.ReadOnly = true;
            this.drtxt_service_station_name.Width = 93;
            // 
            // drtxt_create_time
            // 
            this.drtxt_create_time.DataPropertyName = "create_time";
            this.drtxt_create_time.HeaderText = "创建时间";
            this.drtxt_create_time.Name = "drtxt_create_time";
            this.drtxt_create_time.ReadOnly = true;
            this.drtxt_create_time.Width = 81;
            // 
            // drtxt_settlement_cycle_start
            // 
            this.drtxt_settlement_cycle_start.DataPropertyName = "settlement_cycle_start";
            this.drtxt_settlement_cycle_start.HeaderText = "结算周期开始时间";
            this.drtxt_settlement_cycle_start.Name = "drtxt_settlement_cycle_start";
            this.drtxt_settlement_cycle_start.ReadOnly = true;
            this.drtxt_settlement_cycle_start.Width = 150;
            // 
            // drtxt_settlement_cycle_end
            // 
            this.drtxt_settlement_cycle_end.DataPropertyName = "settlement_cycle_end";
            this.drtxt_settlement_cycle_end.HeaderText = "结算周期结束时间";
            this.drtxt_settlement_cycle_end.Name = "drtxt_settlement_cycle_end";
            this.drtxt_settlement_cycle_end.ReadOnly = true;
            this.drtxt_settlement_cycle_end.Width = 150;
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.Controls.Add(this.cbo_yt_info_status);
            this.panelEx1.Controls.Add(this.cbo_station_info_status);
            this.panelEx1.Controls.Add(this.btnQuery);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.label12);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.dtp_settlement_cycle_end);
            this.panelEx1.Controls.Add(this.dtp_settlement_cycle_start);
            this.panelEx1.Location = new System.Drawing.Point(0, 34);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1234, 93);
            this.panelEx1.TabIndex = 5;
            // 
            // cbo_yt_info_status
            // 
            this.cbo_yt_info_status.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_yt_info_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_yt_info_status.FormattingEnabled = true;
            this.cbo_yt_info_status.Location = new System.Drawing.Point(129, 33);
            this.cbo_yt_info_status.Name = "cbo_yt_info_status";
            this.cbo_yt_info_status.Size = new System.Drawing.Size(121, 21);
            this.cbo_yt_info_status.TabIndex = 111;
            // 
            // cbo_station_info_status
            // 
            this.cbo_station_info_status.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_station_info_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_station_info_status.FormattingEnabled = true;
            this.cbo_station_info_status.Location = new System.Drawing.Point(393, 33);
            this.cbo_station_info_status.Name = "cbo_station_info_status";
            this.cbo_station_info_status.Size = new System.Drawing.Size(121, 21);
            this.cbo_station_info_status.TabIndex = 111;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(1142, 46);
            this.btnQuery.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.MoveImage")));
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.NormalImage")));
            this.btnQuery.Size = new System.Drawing.Size(60, 28);
            this.btnQuery.TabIndex = 110;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(1142, 12);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 28);
            this.btnClear.TabIndex = 109;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(764, 37);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "到";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(535, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "结算周期：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(272, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "服务站结算单状态：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "宇通结算单状态：";
            // 
            // dtp_settlement_cycle_end
            // 
            this.dtp_settlement_cycle_end.Location = new System.Drawing.Point(787, 33);
            this.dtp_settlement_cycle_end.Name = "dtp_settlement_cycle_end";
            this.dtp_settlement_cycle_end.ShowFormat = "yyyy年MM月dd日";
            this.dtp_settlement_cycle_end.Size = new System.Drawing.Size(128, 21);
            this.dtp_settlement_cycle_end.TabIndex = 2;
            this.dtp_settlement_cycle_end.Value = new System.DateTime(2014, 10, 29, 11, 6, 45, 739);
            // 
            // dtp_settlement_cycle_start
            // 
            this.dtp_settlement_cycle_start.Location = new System.Drawing.Point(608, 32);
            this.dtp_settlement_cycle_start.Name = "dtp_settlement_cycle_start";
            this.dtp_settlement_cycle_start.ShowFormat = "yyyy年MM月dd日";
            this.dtp_settlement_cycle_start.Size = new System.Drawing.Size(131, 22);
            this.dtp_settlement_cycle_start.TabIndex = 2;
            this.dtp_settlement_cycle_start.Value = new System.DateTime(2014, 10, 29, 11, 6, 45, 739);
            // 
            // panelEx2
            // 
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(223)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.Controls.Add(this.pageQ);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Location = new System.Drawing.Point(0, 657);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.panelEx2.Size = new System.Drawing.Size(1234, 30);
            this.panelEx2.TabIndex = 18;
            // 
            // pageQ
            // 
            this.pageQ.BackColor = System.Drawing.Color.Transparent;
            this.pageQ.BtnTextNext = "下页";
            this.pageQ.BtnTextPrevious = "上页";
            this.pageQ.Dock = System.Windows.Forms.DockStyle.Right;
            this.pageQ.Location = new System.Drawing.Point(706, 0);
            this.pageQ.Name = "pageQ";
            this.pageQ.PageCount = 0;
            this.pageQ.PageSize = 15;
            this.pageQ.RecordCount = 0;
            this.pageQ.Size = new System.Drawing.Size(428, 30);
            this.pageQ.TabIndex = 5;
            // 
            // UCThreeGuarantySettlementManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.dgvTable);
            this.Controls.Add(this.panelEx1);
            this.Name = "UCThreeGuarantySettlementManager";
            this.Size = new System.Drawing.Size(1234, 687);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.dgvTable, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx dgvTable;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager pageQ;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtp_settlement_cycle_end;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtp_settlement_cycle_start;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbo_yt_info_status;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbo_station_info_status;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn drchk_check;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_st_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_tg_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_settlement_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_info_status_yt;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_station_settlement_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_info_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_service_station_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_settlement_cycle_start;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_settlement_cycle_end;
    }
}
