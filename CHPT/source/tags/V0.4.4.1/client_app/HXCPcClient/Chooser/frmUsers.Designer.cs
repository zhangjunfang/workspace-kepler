namespace HXCPcClient.Chooser
{
    partial class frmUsers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUsers));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tcUsers = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tpUsers = new System.Windows.Forms.TabPage();
            this.dgvUsers = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.txt_com_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.cbo_data_source = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.txtuser_name = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtuser_code = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.user_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_crm_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_com_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_org_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_org_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlContainer.SuspendLayout();
            this.tcUsers.SuspendLayout();
            this.tpUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.tcUsers);
            this.pnlContainer.Controls.Add(this.panelEx1);
            this.pnlContainer.Size = new System.Drawing.Size(718, 429);
            // 
            // tcUsers
            // 
            this.tcUsers.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tcUsers.Controls.Add(this.tpUsers);
            this.tcUsers.Location = new System.Drawing.Point(11, 93);
            this.tcUsers.Name = "tcUsers";
            this.tcUsers.SelectedIndex = 0;
            this.tcUsers.Size = new System.Drawing.Size(696, 329);
            this.tcUsers.TabIndex = 5;
            // 
            // tpUsers
            // 
            this.tpUsers.Controls.Add(this.dgvUsers);
            this.tpUsers.Location = new System.Drawing.Point(4, 26);
            this.tpUsers.Name = "tpUsers";
            this.tpUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tpUsers.Size = new System.Drawing.Size(688, 299);
            this.tpUsers.TabIndex = 0;
            this.tpUsers.Text = "   人员列表  ";
            this.tpUsers.UseVisualStyleBackColor = true;
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvUsers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.user_code,
            this.drtxt_crm_id,
            this.drtxt_com_name,
            this.drtxt_org_name,
            this.user_name,
            this.drtxt_org_id,
            this.user_id});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsers.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsers.EnableHeadersVisualStyles = false;
            this.dgvUsers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvUsers.IsCheck = true;
            this.dgvUsers.Location = new System.Drawing.Point(3, 3);
            this.dgvUsers.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvUsers.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvUsers.MergeColumnNames")));
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvUsers.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvUsers.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvUsers.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvUsers.RowTemplate.Height = 23;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.ShowCheckBox = true;
            this.dgvUsers.Size = new System.Drawing.Size(682, 293);
            this.dgvUsers.TabIndex = 1;
            this.ToolTip.SetToolTip(this.dgvUsers, "请双击选择");
            this.dgvUsers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellDoubleClick);
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.txt_com_code);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.cbo_data_source);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.txtuser_name);
            this.panelEx1.Controls.Add(this.txtuser_code);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(3, 7);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(713, 80);
            this.panelEx1.TabIndex = 4;
            // 
            // txt_com_code
            // 
            this.txt_com_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txt_com_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txt_com_code.BackColor = System.Drawing.Color.Transparent;
            this.txt_com_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txt_com_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txt_com_code.ForeImage = null;
            this.txt_com_code.InputtingVerifyCondition = null;
            this.txt_com_code.Location = new System.Drawing.Point(351, 50);
            this.txt_com_code.MaxLengh = 32767;
            this.txt_com_code.Multiline = false;
            this.txt_com_code.Name = "txt_com_code";
            this.txt_com_code.Radius = 3;
            this.txt_com_code.ReadOnly = false;
            this.txt_com_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txt_com_code.ShowError = false;
            this.txt_com_code.Size = new System.Drawing.Size(136, 23);
            this.txt_com_code.TabIndex = 30;
            this.txt_com_code.UseSystemPasswordChar = false;
            this.txt_com_code.Value = "";
            this.txt_com_code.VerifyCondition = null;
            this.txt_com_code.VerifyType = null;
            this.txt_com_code.VerifyTypeName = null;
            this.txt_com_code.WaterMark = null;
            this.txt_com_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(278, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 29;
            this.label3.Text = "公司编码：";
            // 
            // cbo_data_source
            // 
            this.cbo_data_source.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_data_source.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_data_source.FormattingEnabled = true;
            this.cbo_data_source.Location = new System.Drawing.Point(119, 51);
            this.cbo_data_source.Name = "cbo_data_source";
            this.cbo_data_source.Size = new System.Drawing.Size(125, 22);
            this.cbo_data_source.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "数据来源：";
            // 
            // txtuser_name
            // 
            this.txtuser_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtuser_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtuser_name.BackColor = System.Drawing.Color.Transparent;
            this.txtuser_name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtuser_name.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtuser_name.ForeImage = null;
            this.txtuser_name.InputtingVerifyCondition = null;
            this.txtuser_name.Location = new System.Drawing.Point(351, 15);
            this.txtuser_name.MaxLengh = 32767;
            this.txtuser_name.Multiline = false;
            this.txtuser_name.Name = "txtuser_name";
            this.txtuser_name.Radius = 3;
            this.txtuser_name.ReadOnly = false;
            this.txtuser_name.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtuser_name.ShowError = false;
            this.txtuser_name.Size = new System.Drawing.Size(136, 23);
            this.txtuser_name.TabIndex = 24;
            this.txtuser_name.UseSystemPasswordChar = false;
            this.txtuser_name.Value = "";
            this.txtuser_name.VerifyCondition = null;
            this.txtuser_name.VerifyType = null;
            this.txtuser_name.VerifyTypeName = null;
            this.txtuser_name.WaterMark = null;
            this.txtuser_name.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtuser_code
            // 
            this.txtuser_code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtuser_code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtuser_code.BackColor = System.Drawing.Color.Transparent;
            this.txtuser_code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtuser_code.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtuser_code.ForeImage = null;
            this.txtuser_code.InputtingVerifyCondition = null;
            this.txtuser_code.Location = new System.Drawing.Point(119, 15);
            this.txtuser_code.MaxLengh = 32767;
            this.txtuser_code.Multiline = false;
            this.txtuser_code.Name = "txtuser_code";
            this.txtuser_code.Radius = 3;
            this.txtuser_code.ReadOnly = false;
            this.txtuser_code.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtuser_code.ShowError = false;
            this.txtuser_code.Size = new System.Drawing.Size(125, 23);
            this.txtuser_code.TabIndex = 23;
            this.txtuser_code.UseSystemPasswordChar = false;
            this.txtuser_code.Value = "";
            this.txtuser_code.VerifyCondition = null;
            this.txtuser_code.VerifyType = null;
            this.txtuser_code.VerifyTypeName = null;
            this.txtuser_code.WaterMark = null;
            this.txtuser_code.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(307, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "姓名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "人员编码：";
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSearch.Location = new System.Drawing.Point(624, 42);
            this.btnSearch.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnSearch.Size = new System.Drawing.Size(80, 24);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Location = new System.Drawing.Point(624, 11);
            this.btnClear.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnClear.Size = new System.Drawing.Size(80, 24);
            this.btnClear.TabIndex = 8;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // user_code
            // 
            this.user_code.DataPropertyName = "user_code";
            this.user_code.HeaderText = "人员编号";
            this.user_code.Name = "user_code";
            this.user_code.ReadOnly = true;
            // 
            // drtxt_crm_id
            // 
            this.drtxt_crm_id.DataPropertyName = "cont_crm_guid";
            this.drtxt_crm_id.HeaderText = "用户CRM";
            this.drtxt_crm_id.Name = "drtxt_crm_id";
            this.drtxt_crm_id.ReadOnly = true;
            // 
            // drtxt_com_name
            // 
            this.drtxt_com_name.HeaderText = "公司名称";
            this.drtxt_com_name.Name = "drtxt_com_name";
            this.drtxt_com_name.ReadOnly = true;
            // 
            // drtxt_org_name
            // 
            this.drtxt_org_name.DataPropertyName = "org_name";
            this.drtxt_org_name.HeaderText = "部门名称";
            this.drtxt_org_name.Name = "drtxt_org_name";
            this.drtxt_org_name.ReadOnly = true;
            // 
            // user_name
            // 
            this.user_name.DataPropertyName = "user_name";
            this.user_name.HeaderText = "人员名称";
            this.user_name.Name = "user_name";
            this.user_name.ReadOnly = true;
            // 
            // drtxt_org_id
            // 
            this.drtxt_org_id.DataPropertyName = "org_id";
            this.drtxt_org_id.HeaderText = "部门ID";
            this.drtxt_org_id.Name = "drtxt_org_id";
            this.drtxt_org_id.ReadOnly = true;
            this.drtxt_org_id.Visible = false;
            // 
            // user_id
            // 
            this.user_id.DataPropertyName = "user_id";
            this.user_id.HeaderText = "人员ID";
            this.user_id.Name = "user_id";
            this.user_id.ReadOnly = true;
            this.user_id.Visible = false;
            // 
            // frmUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 460);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmUsers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户选择";
            this.Load += new System.EventHandler(this.frmUsers_Load);
            this.pnlContainer.ResumeLayout(false);
            this.tcUsers.ResumeLayout(false);
            this.tpUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.TabControlEx tcUsers;
        private System.Windows.Forms.TabPage tpUsers;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvUsers;
        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public ServiceStationClient.ComponentUI.TextBoxEx txt_com_code;
        public ServiceStationClient.ComponentUI.ComboBoxEx cbo_data_source;
        public ServiceStationClient.ComponentUI.TextBoxEx txtuser_name;
        public ServiceStationClient.ComponentUI.TextBoxEx txtuser_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_crm_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_com_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_org_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_org_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_id;
    }
}