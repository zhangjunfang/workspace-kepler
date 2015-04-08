namespace HXCPcClient.Chooser
{
    partial class frmDictionaries
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDictionaries));
            this.tcDictionaries = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tpDictionaries = new System.Windows.Forms.TabPage();
            this.dgvDicList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.txtdic_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtdic_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.dic_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dic_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data_sources = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.update_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.update_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dic_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enable_flag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlContainer.SuspendLayout();
            this.tcDictionaries.SuspendLayout();
            this.tpDictionaries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDicList)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.tcDictionaries);
            this.pnlContainer.Controls.Add(this.panelEx1);
            this.pnlContainer.Size = new System.Drawing.Size(718, 429);
            // 
            // tcDictionaries
            // 
            this.tcDictionaries.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tcDictionaries.Controls.Add(this.tpDictionaries);
            this.tcDictionaries.Location = new System.Drawing.Point(11, 61);
            this.tcDictionaries.Name = "tcDictionaries";
            this.tcDictionaries.SelectedIndex = 0;
            this.tcDictionaries.Size = new System.Drawing.Size(696, 361);
            this.tcDictionaries.TabIndex = 3;
            // 
            // tpDictionaries
            // 
            this.tpDictionaries.Controls.Add(this.dgvDicList);
            this.tpDictionaries.Location = new System.Drawing.Point(4, 26);
            this.tpDictionaries.Name = "tpDictionaries";
            this.tpDictionaries.Padding = new System.Windows.Forms.Padding(3);
            this.tpDictionaries.Size = new System.Drawing.Size(688, 331);
            this.tpDictionaries.TabIndex = 0;
            this.tpDictionaries.Text = "   码表列表  ";
            this.tpDictionaries.UseVisualStyleBackColor = true;
            // 
            // dgvDicList
            // 
            this.dgvDicList.AllowUserToAddRows = false;
            this.dgvDicList.AllowUserToDeleteRows = false;
            this.dgvDicList.AllowUserToOrderColumns = true;
            this.dgvDicList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvDicList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDicList.BackgroundColor = System.Drawing.Color.White;
            this.dgvDicList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDicList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDicList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDicList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dic_code,
            this.dic_name,
            this.data_sources,
            this.create_time,
            this.create_name,
            this.update_time,
            this.update_name,
            this.remark,
            this.dic_id,
            this.enable_flag});
            this.dgvDicList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDicList.EnableHeadersVisualStyles = false;
            this.dgvDicList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvDicList.Location = new System.Drawing.Point(3, 3);
            this.dgvDicList.MultiSelect = false;
            this.dgvDicList.Name = "dgvDicList";
            this.dgvDicList.ReadOnly = true;
            this.dgvDicList.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvDicList.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDicList.RowTemplate.Height = 23;
            this.dgvDicList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDicList.Size = new System.Drawing.Size(682, 325);
            this.dgvDicList.TabIndex = 1;
            this.ToolTip.SetToolTip(this.dgvDicList, "请双击选择");
            this.dgvDicList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDicList_CellDoubleClick);
            this.dgvDicList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDicList_CellFormatting);
            // 
            // panelEx1
            // 
            this.panelEx1.Controls.Add(this.txtdic_name);
            this.panelEx1.Controls.Add(this.txtdic_code);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Location = new System.Drawing.Point(3, 7);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(713, 48);
            this.panelEx1.TabIndex = 2;
            // 
            // txtdic_name
            // 
            this.txtdic_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtdic_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtdic_name.BackColor = System.Drawing.Color.Transparent;
            this.txtdic_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtdic_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtdic_name.ForeImage = null;
            this.txtdic_name.Location = new System.Drawing.Point(336, 12);
            this.txtdic_name.MaxLengh = 32767;
            this.txtdic_name.Multiline = false;
            this.txtdic_name.Name = "txtdic_name";
            this.txtdic_name.Radius = 3;
            this.txtdic_name.ReadOnly = false;
            this.txtdic_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtdic_name.Size = new System.Drawing.Size(202, 23);
            this.txtdic_name.TabIndex = 24;
            this.txtdic_name.UseSystemPasswordChar = false;
            this.txtdic_name.WaterMark = null;
            this.txtdic_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtdic_code
            // 
            this.txtdic_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtdic_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtdic_code.BackColor = System.Drawing.Color.Transparent;
            this.txtdic_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtdic_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtdic_code.ForeImage = null;
            this.txtdic_code.Location = new System.Drawing.Point(79, 12);
            this.txtdic_code.MaxLengh = 32767;
            this.txtdic_code.Multiline = false;
            this.txtdic_code.Name = "txtdic_code";
            this.txtdic_code.Radius = 3;
            this.txtdic_code.ReadOnly = false;
            this.txtdic_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtdic_code.Size = new System.Drawing.Size(121, 23);
            this.txtdic_code.TabIndex = 23;
            this.txtdic_code.UseSystemPasswordChar = false;
            this.txtdic_code.WaterMark = null;
            this.txtdic_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(229, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "码表名称/关键字：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "码表编码：";
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(568, 9);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(60, 26);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(636, 9);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 26);
            this.btnClear.TabIndex = 8;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dic_code
            // 
            this.dic_code.DataPropertyName = "dic_code";
            this.dic_code.HeaderText = "类别编码";
            this.dic_code.Name = "dic_code";
            this.dic_code.ReadOnly = true;
            this.dic_code.Width = 120;
            // 
            // dic_name
            // 
            this.dic_name.DataPropertyName = "dic_name";
            this.dic_name.HeaderText = "类别名称";
            this.dic_name.Name = "dic_name";
            this.dic_name.ReadOnly = true;
            this.dic_name.Width = 120;
            // 
            // data_sources
            // 
            this.data_sources.DataPropertyName = "data_sources";
            this.data_sources.HeaderText = "来源";
            this.data_sources.Name = "data_sources";
            this.data_sources.ReadOnly = true;
            this.data_sources.Width = 120;
            // 
            // create_time
            // 
            this.create_time.DataPropertyName = "create_time";
            this.create_time.HeaderText = "创建时间";
            this.create_time.Name = "create_time";
            this.create_time.ReadOnly = true;
            this.create_time.Width = 120;
            // 
            // create_name
            // 
            this.create_name.DataPropertyName = "create_name";
            this.create_name.HeaderText = "创建人";
            this.create_name.Name = "create_name";
            this.create_name.ReadOnly = true;
            this.create_name.Width = 120;
            // 
            // update_time
            // 
            this.update_time.DataPropertyName = "update_time";
            this.update_time.HeaderText = "最后编辑时间";
            this.update_time.Name = "update_time";
            this.update_time.ReadOnly = true;
            this.update_time.Width = 120;
            // 
            // update_name
            // 
            this.update_name.DataPropertyName = "update_name";
            this.update_name.HeaderText = "最后编辑人";
            this.update_name.Name = "update_name";
            this.update_name.ReadOnly = true;
            this.update_name.Width = 120;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            this.remark.Width = 150;
            // 
            // dic_id
            // 
            this.dic_id.DataPropertyName = "dic_id";
            this.dic_id.HeaderText = "类别ID";
            this.dic_id.Name = "dic_id";
            this.dic_id.ReadOnly = true;
            this.dic_id.Visible = false;
            this.dic_id.Width = 71;
            // 
            // enable_flag
            // 
            this.enable_flag.DataPropertyName = "enable_flag";
            this.enable_flag.HeaderText = "删除标记";
            this.enable_flag.Name = "enable_flag";
            this.enable_flag.ReadOnly = true;
            this.enable_flag.Visible = false;
            // 
            // frmDictionaries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 460);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmDictionaries";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "码表选择";
            this.Load += new System.EventHandler(this.frmDictionaries_Load);
            this.pnlContainer.ResumeLayout(false);
            this.tcDictionaries.ResumeLayout(false);
            this.tpDictionaries.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDicList)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.TabControlEx tcDictionaries;
        private System.Windows.Forms.TabPage tpDictionaries;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvDicList;
        private ServiceStationClient.ComponentUI.TextBoxEx txtdic_name;
        private ServiceStationClient.ComponentUI.TextBoxEx txtdic_code;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dic_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dic_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn data_sources;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn update_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn update_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn dic_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn enable_flag;
    }
}