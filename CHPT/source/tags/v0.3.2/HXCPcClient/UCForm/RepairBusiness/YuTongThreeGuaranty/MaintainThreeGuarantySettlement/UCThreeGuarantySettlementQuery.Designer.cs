namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuarantySettlement
{
    partial class UCThreeGuarantySettlementQuery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCThreeGuarantySettlementQuery));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTable = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.drchk_check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.drtxt_settlement_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_info_status_yt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_station_settlement_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_info_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_service_station_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_settlement_cycle_start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_settlement_cycle_end = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_fitting_sum_money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_man_hour_sum_money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_other_sum_cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_sum_cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_settlement_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_clearing_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.cbo_info_status_yt = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.cbo_info_status = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_settlement_cycle_e = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtp_settlement_cycle_s = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.txt_service_no = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txt_station_settlement_no = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txt_settlement_no = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txt_recycle_no = new ServiceStationClient.ComponentUI.TextBoxEx();
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
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTable.BackgroundColor = System.Drawing.Color.White;
            this.dgvTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.drchk_check,
            this.drtxt_settlement_no,
            this.drtxt_info_status_yt,
            this.drtxt_station_settlement_no,
            this.drtxt_info_status,
            this.drtxt_service_station_name,
            this.drtxt_create_time,
            this.drtxt_settlement_cycle_start,
            this.drtxt_settlement_cycle_end,
            this.drtxt_fitting_sum_money,
            this.drtxt_man_hour_sum_money,
            this.drtxt_other_sum_cost,
            this.drtxt_sum_cost,
            this.drtxt_settlement_date,
            this.drtxt_clearing_time,
            this.drtxt_remark});
            this.dgvTable.EnableHeadersVisualStyles = false;
            this.dgvTable.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvTable.Location = new System.Drawing.Point(0, 128);
            this.dgvTable.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvTable.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvTable.MergeColumnNames")));
            this.dgvTable.MultiSelect = false;
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.ReadOnly = true;
            this.dgvTable.RowHeadersVisible = false;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvTable.RowsDefaultCellStyle = dataGridViewCellStyle6;
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
            this.drchk_check.Width = 25;
            // 
            // drtxt_settlement_no
            // 
            this.drtxt_settlement_no.DataPropertyName = "settlement_no";
            this.drtxt_settlement_no.HeaderText = "宇通结算单号";
            this.drtxt_settlement_no.Name = "drtxt_settlement_no";
            this.drtxt_settlement_no.ReadOnly = true;
            this.drtxt_settlement_no.Width = 120;
            // 
            // drtxt_info_status_yt
            // 
            this.drtxt_info_status_yt.DataPropertyName = "info_status_yt";
            this.drtxt_info_status_yt.HeaderText = "宇通结算单状态";
            this.drtxt_info_status_yt.Name = "drtxt_info_status_yt";
            this.drtxt_info_status_yt.ReadOnly = true;
            this.drtxt_info_status_yt.Width = 120;
            // 
            // drtxt_station_settlement_no
            // 
            this.drtxt_station_settlement_no.DataPropertyName = "station_settlement_no";
            this.drtxt_station_settlement_no.HeaderText = "服务站结算单号";
            this.drtxt_station_settlement_no.Name = "drtxt_station_settlement_no";
            this.drtxt_station_settlement_no.ReadOnly = true;
            // 
            // drtxt_info_status
            // 
            this.drtxt_info_status.DataPropertyName = "info_status";
            this.drtxt_info_status.HeaderText = "服务站结算单状态";
            this.drtxt_info_status.Name = "drtxt_info_status";
            this.drtxt_info_status.ReadOnly = true;
            // 
            // drtxt_service_station_name
            // 
            this.drtxt_service_station_name.DataPropertyName = "service_station_name";
            this.drtxt_service_station_name.HeaderText = "服务站名称";
            this.drtxt_service_station_name.Name = "drtxt_service_station_name";
            this.drtxt_service_station_name.ReadOnly = true;
            // 
            // drtxt_create_time
            // 
            this.drtxt_create_time.DataPropertyName = "create_time";
            this.drtxt_create_time.HeaderText = "创建时间";
            this.drtxt_create_time.Name = "drtxt_create_time";
            this.drtxt_create_time.ReadOnly = true;
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
            // 
            // drtxt_fitting_sum_money
            // 
            this.drtxt_fitting_sum_money.DataPropertyName = "fitting_sum_money";
            this.drtxt_fitting_sum_money.HeaderText = "配件费用";
            this.drtxt_fitting_sum_money.Name = "drtxt_fitting_sum_money";
            this.drtxt_fitting_sum_money.ReadOnly = true;
            // 
            // drtxt_man_hour_sum_money
            // 
            this.drtxt_man_hour_sum_money.DataPropertyName = "man_hour_sum_money";
            this.drtxt_man_hour_sum_money.HeaderText = "工时费用";
            this.drtxt_man_hour_sum_money.Name = "drtxt_man_hour_sum_money";
            this.drtxt_man_hour_sum_money.ReadOnly = true;
            // 
            // drtxt_other_sum_cost
            // 
            this.drtxt_other_sum_cost.DataPropertyName = "other_sum_cost";
            this.drtxt_other_sum_cost.HeaderText = "其他费用";
            this.drtxt_other_sum_cost.Name = "drtxt_other_sum_cost";
            this.drtxt_other_sum_cost.ReadOnly = true;
            // 
            // drtxt_sum_cost
            // 
            this.drtxt_sum_cost.DataPropertyName = "sum_cost";
            this.drtxt_sum_cost.FillWeight = 120F;
            this.drtxt_sum_cost.HeaderText = "费用合计";
            this.drtxt_sum_cost.Name = "drtxt_sum_cost";
            this.drtxt_sum_cost.ReadOnly = true;
            this.drtxt_sum_cost.Width = 120;
            // 
            // drtxt_settlement_date
            // 
            this.drtxt_settlement_date.DataPropertyName = "settlement_date";
            this.drtxt_settlement_date.HeaderText = "结算月份";
            this.drtxt_settlement_date.Name = "drtxt_settlement_date";
            this.drtxt_settlement_date.ReadOnly = true;
            // 
            // drtxt_clearing_time
            // 
            this.drtxt_clearing_time.DataPropertyName = "clearing_time";
            this.drtxt_clearing_time.HeaderText = "清帐日期";
            this.drtxt_clearing_time.Name = "drtxt_clearing_time";
            this.drtxt_clearing_time.ReadOnly = true;
            // 
            // drtxt_remark
            // 
            this.drtxt_remark.DataPropertyName = "remark";
            this.drtxt_remark.HeaderText = "备注";
            this.drtxt_remark.Name = "drtxt_remark";
            this.drtxt_remark.ReadOnly = true;
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.Controls.Add(this.cbo_info_status_yt);
            this.panelEx1.Controls.Add(this.cbo_info_status);
            this.panelEx1.Controls.Add(this.btnQuery);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.label12);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label10);
            this.panelEx1.Controls.Add(this.label8);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.dtp_settlement_cycle_e);
            this.panelEx1.Controls.Add(this.dtp_settlement_cycle_s);
            this.panelEx1.Controls.Add(this.txt_service_no);
            this.panelEx1.Controls.Add(this.txt_station_settlement_no);
            this.panelEx1.Controls.Add(this.txt_settlement_no);
            this.panelEx1.Controls.Add(this.txt_recycle_no);
            this.panelEx1.Location = new System.Drawing.Point(0, 34);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1234, 93);
            this.panelEx1.TabIndex = 5;
            // 
            // cbo_info_status_yt
            // 
            this.cbo_info_status_yt.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_info_status_yt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_info_status_yt.FormattingEnabled = true;
            this.cbo_info_status_yt.Location = new System.Drawing.Point(383, 15);
            this.cbo_info_status_yt.Name = "cbo_info_status_yt";
            this.cbo_info_status_yt.Size = new System.Drawing.Size(121, 21);
            this.cbo_info_status_yt.TabIndex = 111;
            // 
            // cbo_info_status
            // 
            this.cbo_info_status.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_info_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_info_status.FormattingEnabled = true;
            this.cbo_info_status.Location = new System.Drawing.Point(383, 54);
            this.cbo_info_status.Name = "cbo_info_status";
            this.cbo_info_status.Size = new System.Drawing.Size(121, 21);
            this.cbo_info_status.TabIndex = 111;
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
            this.btnQuery.Location = new System.Drawing.Point(1147, 50);
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
            this.btnClear.Location = new System.Drawing.Point(1147, 14);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 28);
            this.btnClear.TabIndex = 109;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(794, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "到";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(565, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "结算周期：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(262, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "服务站结算单状态：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(777, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "宇通服务单号：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(517, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "宇通旧件回收单号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "服务站结算单号：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(274, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "宇通结算单状态：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "宇通结算单号：";
            // 
            // dtp_settlement_cycle_e
            // 
            this.dtp_settlement_cycle_e.Location = new System.Drawing.Point(817, 54);
            this.dtp_settlement_cycle_e.Name = "dtp_settlement_cycle_e";
            this.dtp_settlement_cycle_e.ShowFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.dtp_settlement_cycle_e.Size = new System.Drawing.Size(150, 21);
            this.dtp_settlement_cycle_e.TabIndex = 2;
            this.dtp_settlement_cycle_e.Value = new System.DateTime(2014, 10, 29, 11, 6, 45, 739);
            // 
            // dtp_settlement_cycle_s
            // 
            this.dtp_settlement_cycle_s.Location = new System.Drawing.Point(638, 53);
            this.dtp_settlement_cycle_s.Name = "dtp_settlement_cycle_s";
            this.dtp_settlement_cycle_s.ShowFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.dtp_settlement_cycle_s.Size = new System.Drawing.Size(150, 22);
            this.dtp_settlement_cycle_s.TabIndex = 2;
            this.dtp_settlement_cycle_s.Value = new System.DateTime(2014, 10, 29, 11, 6, 45, 739);
            // 
            // txt_service_no
            // 
            this.txt_service_no.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_service_no.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_service_no.BackColor = System.Drawing.Color.Transparent;
            this.txt_service_no.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_service_no.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_service_no.ForeImage = null;
            this.txt_service_no.Location = new System.Drawing.Point(887, 14);
            this.txt_service_no.MaxLengh = 32767;
            this.txt_service_no.Multiline = false;
            this.txt_service_no.Name = "txt_service_no";
            this.txt_service_no.Radius = 3;
            this.txt_service_no.ReadOnly = false;
            this.txt_service_no.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_service_no.Size = new System.Drawing.Size(120, 22);
            this.txt_service_no.TabIndex = 1;
            this.txt_service_no.UseSystemPasswordChar = false;
            this.txt_service_no.WaterMark = null;
            this.txt_service_no.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txt_station_settlement_no
            // 
            this.txt_station_settlement_no.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_station_settlement_no.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_station_settlement_no.BackColor = System.Drawing.Color.Transparent;
            this.txt_station_settlement_no.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_station_settlement_no.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_station_settlement_no.ForeImage = null;
            this.txt_station_settlement_no.Location = new System.Drawing.Point(129, 53);
            this.txt_station_settlement_no.MaxLengh = 32767;
            this.txt_station_settlement_no.Multiline = false;
            this.txt_station_settlement_no.Name = "txt_station_settlement_no";
            this.txt_station_settlement_no.Radius = 3;
            this.txt_station_settlement_no.ReadOnly = false;
            this.txt_station_settlement_no.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_station_settlement_no.Size = new System.Drawing.Size(120, 22);
            this.txt_station_settlement_no.TabIndex = 1;
            this.txt_station_settlement_no.UseSystemPasswordChar = false;
            this.txt_station_settlement_no.WaterMark = null;
            this.txt_station_settlement_no.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txt_settlement_no
            // 
            this.txt_settlement_no.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_settlement_no.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_settlement_no.BackColor = System.Drawing.Color.Transparent;
            this.txt_settlement_no.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_settlement_no.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_settlement_no.ForeImage = null;
            this.txt_settlement_no.Location = new System.Drawing.Point(129, 14);
            this.txt_settlement_no.MaxLengh = 32767;
            this.txt_settlement_no.Multiline = false;
            this.txt_settlement_no.Name = "txt_settlement_no";
            this.txt_settlement_no.Radius = 3;
            this.txt_settlement_no.ReadOnly = false;
            this.txt_settlement_no.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_settlement_no.Size = new System.Drawing.Size(120, 22);
            this.txt_settlement_no.TabIndex = 1;
            this.txt_settlement_no.UseSystemPasswordChar = false;
            this.txt_settlement_no.WaterMark = null;
            this.txt_settlement_no.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txt_recycle_no
            // 
            this.txt_recycle_no.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_recycle_no.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_recycle_no.BackColor = System.Drawing.Color.Transparent;
            this.txt_recycle_no.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_recycle_no.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_recycle_no.ForeImage = null;
            this.txt_recycle_no.Location = new System.Drawing.Point(638, 14);
            this.txt_recycle_no.MaxLengh = 32767;
            this.txt_recycle_no.Multiline = false;
            this.txt_recycle_no.Name = "txt_recycle_no";
            this.txt_recycle_no.Radius = 3;
            this.txt_recycle_no.ReadOnly = false;
            this.txt_recycle_no.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_recycle_no.Size = new System.Drawing.Size(120, 22);
            this.txt_recycle_no.TabIndex = 1;
            this.txt_recycle_no.UseSystemPasswordChar = false;
            this.txt_recycle_no.WaterMark = null;
            this.txt_recycle_no.WaterMarkColor = System.Drawing.Color.Silver;
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
            // UCThreeGuarantySettlementQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.dgvTable);
            this.Controls.Add(this.panelEx1);
            this.Name = "UCThreeGuarantySettlementQuery";
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_service_no;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_settlement_no;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager pageQ;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtp_settlement_cycle_e;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtp_settlement_cycle_s;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbo_info_status;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_station_settlement_no;
        private ServiceStationClient.ComponentUI.TextBoxEx txt_recycle_no;
        private ServiceStationClient.ComponentUI.ComboBoxEx cbo_info_status_yt;
        private System.Windows.Forms.DataGridViewCheckBoxColumn drchk_check;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_settlement_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_info_status_yt;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_station_settlement_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_info_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_service_station_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_settlement_cycle_start;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_settlement_cycle_end;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_fitting_sum_money;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_man_hour_sum_money;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_other_sum_cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_sum_cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_settlement_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_clearing_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_remark;
    }
}
