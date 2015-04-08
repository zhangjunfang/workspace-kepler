namespace HXCServerWinForm.UCForm
{
    partial class UCOrganize
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCOrganize));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.cmbStatus = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.tbLinkMan = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.txtorg_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.tborg_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvRecord = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.tbCompany = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.org_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.org_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.com_id = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.contact_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contact_telephone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.org_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(900, 28);
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.cmbStatus);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.tbLinkMan);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.txtorg_code);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.tborg_name);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(0, 34);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(898, 93);
            this.panelEx1.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(759, 56);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(72, 26);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(759, 21);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(72, 26);
            this.btnClear.TabIndex = 14;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(580, 11);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(102, 22);
            this.cmbStatus.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(539, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "状态：";
            // 
            // tbLinkMan
            // 
            this.tbLinkMan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbLinkMan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbLinkMan.BackColor = System.Drawing.Color.Transparent;
            this.tbLinkMan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbLinkMan.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbLinkMan.ForeImage = null;
            this.tbLinkMan.InputtingVerifyCondition = null;
            this.tbLinkMan.Location = new System.Drawing.Point(90, 39);
            this.tbLinkMan.MaxLengh = 32767;
            this.tbLinkMan.Multiline = false;
            this.tbLinkMan.Name = "tbLinkMan";
            this.tbLinkMan.Radius = 3;
            this.tbLinkMan.ReadOnly = false;
            this.tbLinkMan.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbLinkMan.ShowError = false;
            this.tbLinkMan.Size = new System.Drawing.Size(173, 23);
            this.tbLinkMan.TabIndex = 3;
            this.tbLinkMan.UseSystemPasswordChar = false;
            this.tbLinkMan.Value = "";
            this.tbLinkMan.VerifyCondition = null;
            this.tbLinkMan.VerifyType = null;
            this.tbLinkMan.VerifyTypeName = null;
            this.tbLinkMan.WaterMark = null;
            this.tbLinkMan.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "联系人：";
            // 
            // txtorg_code
            // 
            this.txtorg_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtorg_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtorg_code.BackColor = System.Drawing.Color.Transparent;
            this.txtorg_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtorg_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtorg_code.ForeImage = null;
            this.txtorg_code.InputtingVerifyCondition = null;
            this.txtorg_code.Location = new System.Drawing.Point(90, 10);
            this.txtorg_code.MaxLengh = 32767;
            this.txtorg_code.Multiline = false;
            this.txtorg_code.Name = "txtorg_code";
            this.txtorg_code.Radius = 3;
            this.txtorg_code.ReadOnly = false;
            this.txtorg_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtorg_code.ShowError = false;
            this.txtorg_code.Size = new System.Drawing.Size(173, 23);
            this.txtorg_code.TabIndex = 1;
            this.txtorg_code.UseSystemPasswordChar = false;
            this.txtorg_code.Value = "";
            this.txtorg_code.VerifyCondition = null;
            this.txtorg_code.VerifyType = null;
            this.txtorg_code.VerifyTypeName = null;
            this.txtorg_code.WaterMark = null;
            this.txtorg_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "组织编码：";
            // 
            // tborg_name
            // 
            this.tborg_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tborg_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tborg_name.BackColor = System.Drawing.Color.Transparent;
            this.tborg_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tborg_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tborg_name.ForeImage = null;
            this.tborg_name.InputtingVerifyCondition = null;
            this.tborg_name.Location = new System.Drawing.Point(340, 11);
            this.tborg_name.MaxLengh = 32767;
            this.tborg_name.Multiline = false;
            this.tborg_name.Name = "tborg_name";
            this.tborg_name.Radius = 3;
            this.tborg_name.ReadOnly = false;
            this.tborg_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tborg_name.ShowError = false;
            this.tborg_name.Size = new System.Drawing.Size(173, 23);
            this.tborg_name.TabIndex = 1;
            this.tborg_name.UseSystemPasswordChar = false;
            this.tborg_name.Value = "";
            this.tborg_name.VerifyCondition = null;
            this.tborg_name.VerifyType = null;
            this.tborg_name.VerifyTypeName = null;
            this.tborg_name.WaterMark = null;
            this.tborg_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(277, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "组织名称：";
            // 
            // dgvRecord
            // 
            this.dgvRecord.AllowUserToAddRows = false;
            this.dgvRecord.AllowUserToDeleteRows = false;
            this.dgvRecord.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvRecord.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecord.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.org_code,
            this.org_name,
            this.com_id,
            this.contact_name,
            this.contact_telephone,
            this.create_name,
            this.create_time,
            this.status,
            this.remark,
            this.create_by,
            this.org_id});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecord.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRecord.EnableHeadersVisualStyles = false;
            this.dgvRecord.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvRecord.IsCheck = true;
            this.dgvRecord.Location = new System.Drawing.Point(0, 130);
            this.dgvRecord.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvRecord.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvRecord.MergeColumnNames")));
            this.dgvRecord.MultiSelect = false;
            this.dgvRecord.Name = "dgvRecord";
            this.dgvRecord.ReadOnly = true;
            this.dgvRecord.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvRecord.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRecord.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvRecord.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvRecord.RowTemplate.Height = 23;
            this.dgvRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecord.ShowCheckBox = true;
            this.dgvRecord.Size = new System.Drawing.Size(898, 258);
            this.dgvRecord.TabIndex = 5;
            this.dgvRecord.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvRecord_CellBeginEdit);
            this.dgvRecord.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRecord_CellFormatting);
            this.dgvRecord.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRecord_CellMouseUp);
            // 
            // tbCompany
            // 
            this.tbCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbCompany.BackColor = System.Drawing.Color.Transparent;
            this.tbCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbCompany.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbCompany.ForeImage = null;
            this.tbCompany.InputtingVerifyCondition = null;
            this.tbCompany.Location = new System.Drawing.Point(76, 11);
            this.tbCompany.MaxLengh = 32767;
            this.tbCompany.Multiline = false;
            this.tbCompany.Name = "tbCompany";
            this.tbCompany.Radius = 3;
            this.tbCompany.ReadOnly = false;
            this.tbCompany.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbCompany.ShowError = false;
            this.tbCompany.Size = new System.Drawing.Size(173, 23);
            this.tbCompany.TabIndex = 1;
            this.tbCompany.UseSystemPasswordChar = false;
            this.tbCompany.Value = "";
            this.tbCompany.VerifyCondition = null;
            this.tbCompany.VerifyType = null;
            this.tbCompany.VerifyTypeName = null;
            this.tbCompany.WaterMark = null;
            this.tbCompany.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCheck.Width = 30;
            // 
            // org_code
            // 
            this.org_code.DataPropertyName = "org_code";
            this.org_code.HeaderText = "组织代码";
            this.org_code.Name = "org_code";
            this.org_code.ReadOnly = true;
            this.org_code.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.org_code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.org_code.Width = 120;
            // 
            // org_name
            // 
            this.org_name.DataPropertyName = "org_name";
            this.org_name.HeaderText = "组织名称";
            this.org_name.Name = "org_name";
            this.org_name.ReadOnly = true;
            this.org_name.Width = 200;
            // 
            // com_id
            // 
            this.com_id.DataPropertyName = "com_id";
            this.com_id.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.com_id.HeaderText = "所属公司";
            this.com_id.Name = "com_id";
            this.com_id.ReadOnly = true;
            this.com_id.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.com_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.com_id.Width = 260;
            // 
            // contact_name
            // 
            this.contact_name.DataPropertyName = "contact_name";
            this.contact_name.HeaderText = "联系人";
            this.contact_name.Name = "contact_name";
            this.contact_name.ReadOnly = true;
            this.contact_name.Width = 120;
            // 
            // contact_telephone
            // 
            this.contact_telephone.DataPropertyName = "contact_telephone";
            this.contact_telephone.HeaderText = "联系电话";
            this.contact_telephone.Name = "contact_telephone";
            this.contact_telephone.ReadOnly = true;
            this.contact_telephone.Width = 120;
            // 
            // create_name
            // 
            this.create_name.DataPropertyName = "create_name";
            this.create_name.HeaderText = "创建人";
            this.create_name.Name = "create_name";
            this.create_name.ReadOnly = true;
            // 
            // create_time
            // 
            this.create_time.DataPropertyName = "create_time";
            this.create_time.HeaderText = "创建时间";
            this.create_time.Name = "create_time";
            this.create_time.ReadOnly = true;
            this.create_time.Width = 150;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            this.remark.Width = 200;
            // 
            // create_by
            // 
            this.create_by.DataPropertyName = "create_by";
            this.create_by.HeaderText = "create_by";
            this.create_by.Name = "create_by";
            this.create_by.ReadOnly = true;
            this.create_by.Width = 120;
            // 
            // org_id
            // 
            this.org_id.DataPropertyName = "org_id";
            this.org_id.HeaderText = "org_id";
            this.org_id.Name = "org_id";
            this.org_id.ReadOnly = true;
            this.org_id.Visible = false;
            this.org_id.Width = 60;
            // 
            // UCOrganize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.dgvRecord);
            this.Name = "UCOrganize";
            this.Size = new System.Drawing.Size(900, 400);
            this.Load += new System.EventHandler(this.UCOrganize_Load);
            this.Controls.SetChildIndex(this.dgvRecord, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.ComboBoxEx cmbStatus;
        private System.Windows.Forms.Label label3;
        private ServiceStationClient.ComponentUI.TextBoxEx tbLinkMan;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.TextBoxEx tborg_name;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRecord;
        private ServiceStationClient.ComponentUI.TextBoxEx tbCompany;
        private ServiceStationClient.ComponentUI.TextBoxEx txtorg_code;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_name;
        private System.Windows.Forms.DataGridViewComboBoxColumn com_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn contact_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn contact_telephone;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_by;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_id;
    }
}