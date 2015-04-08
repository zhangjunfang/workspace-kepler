using System.Security.AccessControl;

namespace HXCPcClient.UCForm.CustomerService.CallBack
{
    partial class UCCallBackManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCallBackManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.palQTop = new System.Windows.Forms.Panel();
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.txtCBPerson = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.txtCustomerName = new ServiceStationClient.ComponentUI.TextBox.TextChooser();
            this.labCBPerson = new System.Windows.Forms.Label();
            this.labCustomerName = new System.Windows.Forms.Label();
            this.cboCBType = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.dtpCBETime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpCBSTime = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.label3 = new System.Windows.Forms.Label();
            this.labCBTime = new System.Windows.Forms.Label();
            this.cboCBMode = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.labOrderStatus = new System.Windows.Forms.Label();
            this.labCBType = new System.Windows.Forms.Label();
            this.txtCBTitle = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.labCBTitle = new System.Windows.Forms.Label();
            this.dgvQData = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.drchk_check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.drtxt_Callback_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drlnk_Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drlnk_Callback_corp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_Callback_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_Callback_mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_Callback_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_Callback_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_Callback_by_org = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_handle_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new ServiceStationClient.ComponentUI.PanelEx();
            this.pageQ = new ServiceStationClient.ComponentUI.WinFormPager();
            this.palQTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQData)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // palQTop
            // 
            this.palQTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.palQTop.Controls.Add(this.btnQuery);
            this.palQTop.Controls.Add(this.btnClear);
            this.palQTop.Controls.Add(this.txtCBPerson);
            this.palQTop.Controls.Add(this.txtCustomerName);
            this.palQTop.Controls.Add(this.labCBPerson);
            this.palQTop.Controls.Add(this.labCustomerName);
            this.palQTop.Controls.Add(this.cboCBType);
            this.palQTop.Controls.Add(this.label1);
            this.palQTop.Controls.Add(this.dtpCBETime);
            this.palQTop.Controls.Add(this.dtpCBSTime);
            this.palQTop.Controls.Add(this.label3);
            this.palQTop.Controls.Add(this.labCBTime);
            this.palQTop.Controls.Add(this.cboCBMode);
            this.palQTop.Controls.Add(this.labOrderStatus);
            this.palQTop.Controls.Add(this.labCBType);
            this.palQTop.Controls.Add(this.txtCBTitle);
            this.palQTop.Controls.Add(this.labCBTitle);
            this.palQTop.Location = new System.Drawing.Point(3, 40);
            this.palQTop.Name = "palQTop";
            this.palQTop.Size = new System.Drawing.Size(1024, 101);
            this.palQTop.TabIndex = 20;
            // 
            // btnQuery
            // 
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(934, 52);
            this.btnQuery.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.MoveImage")));
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.NormalImage")));
            this.btnQuery.Size = new System.Drawing.Size(60, 28);
            this.btnQuery.TabIndex = 115;
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(934, 17);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(60, 28);
            this.btnClear.TabIndex = 114;
            // 
            // txtCBPerson
            // 
            this.txtCBPerson.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCBPerson.Location = new System.Drawing.Point(722, 52);
            this.txtCBPerson.Name = "txtCBPerson";
            this.txtCBPerson.ReadOnly = true;
            this.txtCBPerson.Size = new System.Drawing.Size(120, 26);
            this.txtCBPerson.TabIndex = 113;
            this.txtCBPerson.ToolTipTitle = "";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.ChooserTypeImage = ServiceStationClient.ComponentUI.TextBox.ChooserType.Default;
            this.txtCustomerName.Location = new System.Drawing.Point(515, 52);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = false;
            this.txtCustomerName.Size = new System.Drawing.Size(118, 26);
            this.txtCustomerName.TabIndex = 112;
            this.txtCustomerName.ToolTipTitle = "";
            // 
            // labCBPerson
            // 
            this.labCBPerson.AutoSize = true;
            this.labCBPerson.Location = new System.Drawing.Point(662, 57);
            this.labCBPerson.Name = "labCBPerson";
            this.labCBPerson.Size = new System.Drawing.Size(65, 12);
            this.labCBPerson.TabIndex = 111;
            this.labCBPerson.Text = "被回访人：";
            // 
            // labCustomerName
            // 
            this.labCustomerName.AutoSize = true;
            this.labCustomerName.Location = new System.Drawing.Point(455, 59);
            this.labCustomerName.Name = "labCustomerName";
            this.labCustomerName.Size = new System.Drawing.Size(65, 12);
            this.labCustomerName.TabIndex = 110;
            this.labCustomerName.Text = "客户名称：";
            // 
            // cboCBType
            // 
            this.cboCBType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCBType.FormattingEnabled = true;
            this.cboCBType.Location = new System.Drawing.Point(515, 17);
            this.cboCBType.Name = "cboCBType";
            this.cboCBType.Size = new System.Drawing.Size(118, 22);
            this.cboCBType.TabIndex = 109;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 93;
            this.label1.Text = "从";
            // 
            // dtpCBETime
            // 
            this.dtpCBETime.Location = new System.Drawing.Point(285, 55);
            this.dtpCBETime.Name = "dtpCBETime";
            this.dtpCBETime.ShowFormat = "yyyy-MM-dd";
            this.dtpCBETime.Size = new System.Drawing.Size(140, 21);
            this.dtpCBETime.TabIndex = 92;
            this.dtpCBETime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // dtpCBSTime
            // 
            this.dtpCBSTime.Location = new System.Drawing.Point(111, 54);
            this.dtpCBSTime.Name = "dtpCBSTime";
            this.dtpCBSTime.ShowFormat = "yyyy-MM-dd";
            this.dtpCBSTime.Size = new System.Drawing.Size(144, 21);
            this.dtpCBSTime.TabIndex = 91;
            this.dtpCBSTime.Value = new System.DateTime(2014, 10, 8, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(262, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 90;
            this.label3.Text = "到";
            // 
            // labCBTime
            // 
            this.labCBTime.AutoSize = true;
            this.labCBTime.Location = new System.Drawing.Point(25, 59);
            this.labCBTime.Name = "labCBTime";
            this.labCBTime.Size = new System.Drawing.Size(65, 12);
            this.labCBTime.TabIndex = 89;
            this.labCBTime.Text = "回访时间：";
            // 
            // cboCBMode
            // 
            this.cboCBMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCBMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCBMode.FormattingEnabled = true;
            this.cboCBMode.Location = new System.Drawing.Point(721, 17);
            this.cboCBMode.Name = "cboCBMode";
            this.cboCBMode.Size = new System.Drawing.Size(121, 22);
            this.cboCBMode.TabIndex = 88;
            // 
            // labOrderStatus
            // 
            this.labOrderStatus.AutoSize = true;
            this.labOrderStatus.Location = new System.Drawing.Point(661, 23);
            this.labOrderStatus.Name = "labOrderStatus";
            this.labOrderStatus.Size = new System.Drawing.Size(65, 12);
            this.labOrderStatus.TabIndex = 87;
            this.labOrderStatus.Text = "回访形式：";
            // 
            // labCBType
            // 
            this.labCBType.AutoSize = true;
            this.labCBType.Location = new System.Drawing.Point(455, 23);
            this.labCBType.Name = "labCBType";
            this.labCBType.Size = new System.Drawing.Size(65, 12);
            this.labCBType.TabIndex = 85;
            this.labCBType.Text = "回访类型：";
            // 
            // txtCBTitle
            // 
            this.txtCBTitle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCBTitle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCBTitle.BackColor = System.Drawing.Color.Transparent;
            this.txtCBTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtCBTitle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtCBTitle.ForeImage = null;
            this.txtCBTitle.InputtingVerifyCondition = null;
            this.txtCBTitle.Location = new System.Drawing.Point(90, 17);
            this.txtCBTitle.MaxLengh = 32767;
            this.txtCBTitle.Multiline = false;
            this.txtCBTitle.Name = "txtCBTitle";
            this.txtCBTitle.Radius = 3;
            this.txtCBTitle.ReadOnly = false;
            this.txtCBTitle.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtCBTitle.ShowError = false;
            this.txtCBTitle.Size = new System.Drawing.Size(335, 23);
            this.txtCBTitle.TabIndex = 82;
            this.txtCBTitle.UseSystemPasswordChar = false;
            this.txtCBTitle.Value = "";
            this.txtCBTitle.VerifyCondition = null;
            this.txtCBTitle.VerifyType = null;
            this.txtCBTitle.VerifyTypeName = null;
            this.txtCBTitle.WaterMark = null;
            this.txtCBTitle.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // labCBTitle
            // 
            this.labCBTitle.AutoSize = true;
            this.labCBTitle.Location = new System.Drawing.Point(25, 23);
            this.labCBTitle.Name = "labCBTitle";
            this.labCBTitle.Size = new System.Drawing.Size(65, 12);
            this.labCBTitle.TabIndex = 81;
            this.labCBTitle.Text = "回访标题：";
            // 
            // dgvQData
            // 
            this.dgvQData.AllowUserToAddRows = false;
            this.dgvQData.AllowUserToDeleteRows = false;
            this.dgvQData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvQData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvQData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvQData.BackgroundColor = System.Drawing.Color.White;
            this.dgvQData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvQData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvQData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.drchk_check,
            this.drtxt_Callback_id,
            this.drlnk_Title,
            this.drlnk_Callback_corp,
            this.drtxt_Callback_type,
            this.drtxt_Callback_mode,
            this.drtxt_Callback_time,
            this.drtxt_Callback_by,
            this.drtxt_Callback_by_org,
            this.drtxt_handle_name,
            this.drtxt_status});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvQData.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvQData.EnableHeadersVisualStyles = false;
            this.dgvQData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvQData.IsCheck = true;
            this.dgvQData.Location = new System.Drawing.Point(3, 147);
            this.dgvQData.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvQData.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvQData.MergeColumnNames")));
            this.dgvQData.MultiSelect = false;
            this.dgvQData.Name = "dgvQData";
            this.dgvQData.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvQData.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvQData.RowHeadersVisible = false;
            this.dgvQData.RowHeadersWidth = 30;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvQData.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvQData.RowTemplate.Height = 23;
            this.dgvQData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQData.ShowCheckBox = true;
            this.dgvQData.Size = new System.Drawing.Size(1024, 362);
            this.dgvQData.TabIndex = 21;
            // 
            // drchk_check
            // 
            this.drchk_check.HeaderText = "";
            this.drchk_check.MinimumWidth = 30;
            this.drchk_check.Name = "drchk_check";
            this.drchk_check.ReadOnly = true;
            this.drchk_check.Width = 30;
            // 
            // drtxt_Callback_id
            // 
            this.drtxt_Callback_id.DataPropertyName = "Callback_id";
            this.drtxt_Callback_id.HeaderText = "回访标识";
            this.drtxt_Callback_id.Name = "drtxt_Callback_id";
            this.drtxt_Callback_id.ReadOnly = true;
            this.drtxt_Callback_id.Visible = false;
            // 
            // drlnk_Title
            // 
            this.drlnk_Title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.drlnk_Title.DataPropertyName = "title";
            this.drlnk_Title.HeaderText = "回访标题";
            this.drlnk_Title.Name = "drlnk_Title";
            this.drlnk_Title.ReadOnly = true;
            this.drlnk_Title.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.drlnk_Title.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.drlnk_Title.Width = 62;
            // 
            // drlnk_Callback_corp
            // 
            this.drlnk_Callback_corp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.drlnk_Callback_corp.DataPropertyName = "Callback_corp";
            this.drlnk_Callback_corp.HeaderText = "客户名称";
            this.drlnk_Callback_corp.Name = "drlnk_Callback_corp";
            this.drlnk_Callback_corp.ReadOnly = true;
            this.drlnk_Callback_corp.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.drlnk_Callback_corp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.drlnk_Callback_corp.Width = 62;
            // 
            // drtxt_Callback_type
            // 
            this.drtxt_Callback_type.DataPropertyName = "Callback_type";
            this.drtxt_Callback_type.HeaderText = "回访类型";
            this.drtxt_Callback_type.Name = "drtxt_Callback_type";
            this.drtxt_Callback_type.ReadOnly = true;
            // 
            // drtxt_Callback_mode
            // 
            this.drtxt_Callback_mode.DataPropertyName = "Callback_mode";
            this.drtxt_Callback_mode.HeaderText = "回访形式";
            this.drtxt_Callback_mode.Name = "drtxt_Callback_mode";
            this.drtxt_Callback_mode.ReadOnly = true;
            // 
            // drtxt_Callback_time
            // 
            this.drtxt_Callback_time.DataPropertyName = "Callback_time";
            this.drtxt_Callback_time.HeaderText = "回访时间";
            this.drtxt_Callback_time.Name = "drtxt_Callback_time";
            this.drtxt_Callback_time.ReadOnly = true;
            // 
            // drtxt_Callback_by
            // 
            this.drtxt_Callback_by.DataPropertyName = "Callback_by";
            this.drtxt_Callback_by.HeaderText = "被回访人";
            this.drtxt_Callback_by.Name = "drtxt_Callback_by";
            this.drtxt_Callback_by.ReadOnly = true;
            // 
            // drtxt_Callback_by_org
            // 
            this.drtxt_Callback_by_org.DataPropertyName = "Callback_by_org";
            this.drtxt_Callback_by_org.HeaderText = "被回访部门";
            this.drtxt_Callback_by_org.Name = "drtxt_Callback_by_org";
            this.drtxt_Callback_by_org.ReadOnly = true;
            // 
            // drtxt_handle_name
            // 
            this.drtxt_handle_name.DataPropertyName = "handle_name";
            this.drtxt_handle_name.HeaderText = "经办人";
            this.drtxt_handle_name.Name = "drtxt_handle_name";
            this.drtxt_handle_name.ReadOnly = true;
            // 
            // drtxt_status
            // 
            this.drtxt_status.DataPropertyName = "status";
            this.drtxt_status.HeaderText = "状态";
            this.drtxt_status.Name = "drtxt_status";
            this.drtxt_status.ReadOnly = true;
            // 
            // panelEx2
            // 
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(223)))), ((int)(((byte)(255)))));
            this.panelEx2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx2.BorderWidth = 1;
            this.panelEx2.Controls.Add(this.pageQ);
            this.panelEx2.Curvature = 0;
            this.panelEx2.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx2.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx2.Location = new System.Drawing.Point(3, 514);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.panelEx2.Size = new System.Drawing.Size(1024, 30);
            this.panelEx2.TabIndex = 22;
            // 
            // pageQ
            // 
            this.pageQ.BackColor = System.Drawing.Color.Transparent;
            this.pageQ.BtnTextNext = "下页";
            this.pageQ.BtnTextPrevious = "上页";
            this.pageQ.Dock = System.Windows.Forms.DockStyle.Right;
            this.pageQ.Location = new System.Drawing.Point(496, 0);
            this.pageQ.Name = "pageQ";
            this.pageQ.PageCount = 0;
            this.pageQ.PageSize = 15;
            this.pageQ.RecordCount = 0;
            this.pageQ.Size = new System.Drawing.Size(428, 30);
            this.pageQ.TabIndex = 5;
            // 
            // UCCallBackManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.dgvQData);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.palQTop);
            this.Name = "UCCallBackManager";
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.palQTop, 0);
            this.Controls.SetChildIndex(this.panelEx2, 0);
            this.Controls.SetChildIndex(this.dgvQData, 0);
            this.palQTop.ResumeLayout(false);
            this.palQTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQData)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel palQTop;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboCBType;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpCBETime;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpCBSTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labCBTime;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboCBMode;
        private System.Windows.Forms.Label labOrderStatus;
        private System.Windows.Forms.Label labCBType;
        private ServiceStationClient.ComponentUI.TextBoxEx txtCBTitle;
        private System.Windows.Forms.Label labCBTitle;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCBPerson;
        private ServiceStationClient.ComponentUI.TextBox.TextChooser txtCustomerName;
        private System.Windows.Forms.Label labCBPerson;
        private System.Windows.Forms.Label labCustomerName;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvQData;
        private ServiceStationClient.ComponentUI.PanelEx panelEx2;
        private ServiceStationClient.ComponentUI.WinFormPager pageQ;
        private System.Windows.Forms.DataGridViewCheckBoxColumn drchk_check;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_Callback_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn drlnk_Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn drlnk_Callback_corp;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_Callback_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_Callback_mode;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_Callback_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_Callback_by;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_Callback_by_org;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_handle_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_status;

    }
}
