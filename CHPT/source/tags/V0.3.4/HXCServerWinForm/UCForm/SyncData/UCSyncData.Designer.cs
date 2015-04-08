namespace HXCServerWinForm.UCForm.SyncData
{
    partial class UCSyncData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCSyncData));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvSyncData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.pnlTop = new ServiceStationClient.ComponentUI.PanelEx();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.cmbbusiness = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cmbexternal_sys = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.external_sys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.business_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.local_total_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.update_total_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.last_sync_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data_sync_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSyncData)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(828, 25);
            // 
            // dgvSyncData
            // 
            this.dgvSyncData.AllowUserToAddRows = false;
            this.dgvSyncData.AllowUserToDeleteRows = false;
            this.dgvSyncData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvSyncData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSyncData.BackgroundColor = System.Drawing.Color.White;
            this.dgvSyncData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSyncData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSyncData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSyncData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.Code,
            this.external_sys,
            this.business_name,
            this.local_total_num,
            this.update_total_num,
            this.last_sync_time,
            this.data_sync_id});
            this.dgvSyncData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSyncData.EnableHeadersVisualStyles = false;
            this.dgvSyncData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvSyncData.Location = new System.Drawing.Point(0, 80);
            this.dgvSyncData.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvSyncData.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvSyncData.MergeColumnNames")));
            this.dgvSyncData.MultiSelect = false;
            this.dgvSyncData.Name = "dgvSyncData";
            this.dgvSyncData.ReadOnly = true;
            this.dgvSyncData.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvSyncData.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSyncData.RowTemplate.Height = 23;
            this.dgvSyncData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSyncData.ShowCheckBox = true;
            this.dgvSyncData.Size = new System.Drawing.Size(828, 480);
            this.dgvSyncData.TabIndex = 17;
            this.dgvSyncData.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvSyncData_CellBeginEdit);
            this.dgvSyncData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvSyncData_CellFormatting);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlTop.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlTop.Controls.Add(this.btnSearch);
            this.pnlTop.Controls.Add(this.btnClear);
            this.pnlTop.Controls.Add(this.cmbbusiness);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.cmbexternal_sys);
            this.pnlTop.Controls.Add(this.label6);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 28);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(828, 52);
            this.pnlTop.TabIndex = 18;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(622, 11);
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
            this.btnClear.Location = new System.Drawing.Point(704, 11);
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
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 26;
            // 
            // Code
            // 
            this.Code.DataPropertyName = "Code";
            this.Code.FillWeight = 102.6831F;
            this.Code.HeaderText = "编码";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Width = 122;
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
            // business_name
            // 
            this.business_name.DataPropertyName = "business_name";
            this.business_name.FillWeight = 102.6831F;
            this.business_name.HeaderText = "本地业务名";
            this.business_name.Name = "business_name";
            this.business_name.ReadOnly = true;
            this.business_name.Width = 200;
            // 
            // local_total_num
            // 
            this.local_total_num.DataPropertyName = "local_total_num";
            this.local_total_num.FillWeight = 102.6831F;
            this.local_total_num.HeaderText = "本地总条目数";
            this.local_total_num.Name = "local_total_num";
            this.local_total_num.ReadOnly = true;
            this.local_total_num.Width = 122;
            // 
            // update_total_num
            // 
            this.update_total_num.DataPropertyName = "update_total_num";
            this.update_total_num.FillWeight = 102.6831F;
            this.update_total_num.HeaderText = "更新总条目数";
            this.update_total_num.Name = "update_total_num";
            this.update_total_num.ReadOnly = true;
            this.update_total_num.Width = 123;
            // 
            // last_sync_time
            // 
            this.last_sync_time.DataPropertyName = "last_sync_time";
            this.last_sync_time.FillWeight = 102.6831F;
            this.last_sync_time.HeaderText = "最后更新时间";
            this.last_sync_time.Name = "last_sync_time";
            this.last_sync_time.ReadOnly = true;
            this.last_sync_time.Width = 200;
            // 
            // data_sync_id
            // 
            this.data_sync_id.DataPropertyName = "data_sync_id";
            this.data_sync_id.HeaderText = "data_sync_id";
            this.data_sync_id.Name = "data_sync_id";
            this.data_sync_id.ReadOnly = true;
            this.data_sync_id.Visible = false;
            // 
            // UCSyncData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvSyncData);
            this.Controls.Add(this.pnlTop);
            this.Name = "UCSyncData";
            this.Size = new System.Drawing.Size(828, 560);
            this.Load += new System.EventHandler(this.UCSyncData_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.pnlTop, 0);
            this.Controls.SetChildIndex(this.dgvSyncData, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSyncData)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx dgvSyncData;
        private ServiceStationClient.ComponentUI.PanelEx pnlTop;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbbusiness;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbexternal_sys;
        private System.Windows.Forms.Label label6;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn external_sys;
        private System.Windows.Forms.DataGridViewTextBoxColumn business_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn local_total_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn update_total_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn last_sync_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn data_sync_id;
    }
}
