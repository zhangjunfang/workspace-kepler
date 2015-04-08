namespace HXCServerWinForm.UCForm.SyncDataLog
{
    partial class UCSyncDataLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCSyncDataLog));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvSyncDataLog = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.pnlTop = new ServiceStationClient.ComponentUI.PanelEx();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.cmbbusiness = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cmbsync_direction = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.cmbexternal_sys = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.page = new ServiceStationClient.ComponentUI.WinFormPager();
            this.business_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.table_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sync_start_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sync_end_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.changes_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sync_direction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.external_sys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sync_result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSyncDataLog)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSyncDataLog
            // 
            this.dgvSyncDataLog.AllowUserToAddRows = false;
            this.dgvSyncDataLog.AllowUserToDeleteRows = false;
            this.dgvSyncDataLog.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvSyncDataLog.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSyncDataLog.BackgroundColor = System.Drawing.Color.White;
            this.dgvSyncDataLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSyncDataLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSyncDataLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSyncDataLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.business_name,
            this.table_name,
            this.sync_start_time,
            this.sync_end_time,
            this.changes_num,
            this.sync_direction,
            this.external_sys,
            this.sync_result});
            this.dgvSyncDataLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSyncDataLog.EnableHeadersVisualStyles = false;
            this.dgvSyncDataLog.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvSyncDataLog.Location = new System.Drawing.Point(0, 80);
            this.dgvSyncDataLog.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvSyncDataLog.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvSyncDataLog.MergeColumnNames")));
            this.dgvSyncDataLog.MultiSelect = false;
            this.dgvSyncDataLog.Name = "dgvSyncDataLog";
            this.dgvSyncDataLog.ReadOnly = true;
            this.dgvSyncDataLog.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvSyncDataLog.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSyncDataLog.RowTemplate.Height = 23;
            this.dgvSyncDataLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSyncDataLog.ShowCheckBox = true;
            this.dgvSyncDataLog.Size = new System.Drawing.Size(1020, 442);
            this.dgvSyncDataLog.TabIndex = 19;
            this.dgvSyncDataLog.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvSyncData_CellFormatting);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlTop.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlTop.Controls.Add(this.btnSearch);
            this.pnlTop.Controls.Add(this.btnClear);
            this.pnlTop.Controls.Add(this.cmbbusiness);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.cmbsync_direction);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.cmbexternal_sys);
            this.pnlTop.Controls.Add(this.label6);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 28);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1020, 52);
            this.pnlTop.TabIndex = 20;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(783, 10);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(76, 26);
            this.btnSearch.TabIndex = 35;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(865, 10);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(76, 26);
            this.btnClear.TabIndex = 34;
            this.btnClear.Tag = "1";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cmbbusiness
            // 
            this.cmbbusiness.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbbusiness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbusiness.FormattingEnabled = true;
            this.cmbbusiness.Location = new System.Drawing.Point(286, 14);
            this.cmbbusiness.Name = "cmbbusiness";
            this.cmbbusiness.Size = new System.Drawing.Size(251, 22);
            this.cmbbusiness.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(237, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 32;
            this.label1.Text = "业务名：";
            // 
            // cmbsync_direction
            // 
            this.cmbsync_direction.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbsync_direction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbsync_direction.FormattingEnabled = true;
            this.cmbsync_direction.Location = new System.Drawing.Point(621, 14);
            this.cmbsync_direction.Name = "cmbsync_direction";
            this.cmbsync_direction.Size = new System.Drawing.Size(121, 22);
            this.cmbsync_direction.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(556, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "同步方向：";
            // 
            // cmbexternal_sys
            // 
            this.cmbexternal_sys.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbexternal_sys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbexternal_sys.FormattingEnabled = true;
            this.cmbexternal_sys.Location = new System.Drawing.Point(84, 14);
            this.cmbexternal_sys.Name = "cmbexternal_sys";
            this.cmbexternal_sys.Size = new System.Drawing.Size(121, 22);
            this.cmbexternal_sys.TabIndex = 33;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 32;
            this.label6.Text = "外部系统：";
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.Controls.Add(this.page);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx1.Location = new System.Drawing.Point(0, 522);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1020, 38);
            this.panelEx1.TabIndex = 21;
            // 
            // page
            // 
            this.page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.page.BackColor = System.Drawing.Color.Transparent;
            this.page.BtnTextNext = "下页";
            this.page.BtnTextPrevious = "上页";
            this.page.DisplayStyle = ServiceStationClient.ComponentUI.WinFormPager.DisplayStyleEnum.图片;
            this.page.Location = new System.Drawing.Point(540, 5);
            this.page.Name = "page";
            this.page.PageCount = 0;
            this.page.PageSize = 15;
            this.page.RecordCount = 0;
            this.page.Size = new System.Drawing.Size(480, 31);
            this.page.TabIndex = 10;
            this.page.TextImageRalitions = ServiceStationClient.ComponentUI.WinFormPager.TextImageRalitionEnum.图片显示在文字前方;
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            // 
            // business_name
            // 
            this.business_name.DataPropertyName = "business_name";
            this.business_name.FillWeight = 102.6831F;
            this.business_name.HeaderText = "本地业务名";
            this.business_name.Name = "business_name";
            this.business_name.ReadOnly = true;
            this.business_name.Width = 200;
            // 
            // table_name
            // 
            this.table_name.DataPropertyName = "table_name";
            this.table_name.HeaderText = "表名";
            this.table_name.Name = "table_name";
            this.table_name.ReadOnly = true;
            this.table_name.Width = 200;
            // 
            // sync_start_time
            // 
            this.sync_start_time.DataPropertyName = "sync_start_time";
            this.sync_start_time.FillWeight = 102.6831F;
            this.sync_start_time.HeaderText = "同步开始时间";
            this.sync_start_time.Name = "sync_start_time";
            this.sync_start_time.ReadOnly = true;
            this.sync_start_time.Width = 200;
            // 
            // sync_end_time
            // 
            this.sync_end_time.DataPropertyName = "sync_end_time";
            this.sync_end_time.HeaderText = "同步结束时间";
            this.sync_end_time.Name = "sync_end_time";
            this.sync_end_time.ReadOnly = true;
            this.sync_end_time.Width = 200;
            // 
            // changes_num
            // 
            this.changes_num.DataPropertyName = "changes_num";
            this.changes_num.FillWeight = 102.6831F;
            this.changes_num.HeaderText = "变化条目数";
            this.changes_num.Name = "changes_num";
            this.changes_num.ReadOnly = true;
            this.changes_num.Width = 123;
            // 
            // sync_direction
            // 
            this.sync_direction.DataPropertyName = "sync_direction";
            this.sync_direction.HeaderText = "同步方向";
            this.sync_direction.Name = "sync_direction";
            this.sync_direction.ReadOnly = true;
            // 
            // external_sys
            // 
            this.external_sys.DataPropertyName = "external_sys";
            this.external_sys.FillWeight = 102.6831F;
            this.external_sys.HeaderText = "外部系统";
            this.external_sys.Name = "external_sys";
            this.external_sys.ReadOnly = true;
            this.external_sys.Width = 200;
            // 
            // sync_result
            // 
            this.sync_result.DataPropertyName = "sync_result";
            this.sync_result.HeaderText = "同步情况";
            this.sync_result.Name = "sync_result";
            this.sync_result.ReadOnly = true;
            this.sync_result.Width = 300;
            // 
            // UCSyncDataLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvSyncDataLog);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.pnlTop);
            this.Name = "UCSyncDataLog";
            this.Load += new System.EventHandler(this.UCSyncDataLog_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.pnlTop, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.dgvSyncDataLog, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSyncDataLog)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx dgvSyncDataLog;
        private ServiceStationClient.ComponentUI.PanelEx pnlTop;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbbusiness;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbexternal_sys;
        private System.Windows.Forms.Label label6;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbsync_direction;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.WinFormPager page;
        private System.Windows.Forms.DataGridViewTextBoxColumn business_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn table_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn sync_start_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn sync_end_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn changes_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn sync_direction;
        private System.Windows.Forms.DataGridViewTextBoxColumn external_sys;
        private System.Windows.Forms.DataGridViewTextBoxColumn sync_result;
    }
}
