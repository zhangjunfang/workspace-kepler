namespace HXCPcClient.UCForm.SysManage.BankAccount
{
    partial class UCBankAccountManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCBankAccountManage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new ServiceStationClient.ComponentUI.PanelEx();
            this.btnSearch = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.cboStatus = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.txtBankAccount = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.txtBankName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvBank = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.columnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBankName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBankAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnCreate_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCreate_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnUpdate_By = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnUpdate_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBank)).BeginInit();
            this.SuspendLayout();
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
            this.panelEx1.Controls.Add(this.cboStatus);
            this.panelEx1.Controls.Add(this.txtBankAccount);
            this.panelEx1.Controls.Add(this.txtBankName);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Curvature = 0;
            this.panelEx1.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft)
                        | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelEx1.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelEx1.Location = new System.Drawing.Point(0, 25);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1030, 82);
            this.panelEx1.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Caption = "查询";
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.Location = new System.Drawing.Point(871, 22);
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(70, 26);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(782, 22);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(70, 26);
            this.btnClear.TabIndex = 6;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cboStatus
            // 
            this.cboStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(585, 25);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(126, 22);
            this.cboStatus.TabIndex = 5;
            // 
            // txtBankAccount
            // 
            this.txtBankAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtBankAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtBankAccount.BackColor = System.Drawing.Color.Transparent;
            this.txtBankAccount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtBankAccount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtBankAccount.ForeImage = null;
            this.txtBankAccount.InputtingVerifyCondition = null;
            this.txtBankAccount.Location = new System.Drawing.Point(334, 25);
            this.txtBankAccount.MaxLengh = 32767;
            this.txtBankAccount.Multiline = false;
            this.txtBankAccount.Name = "txtBankAccount";
            this.txtBankAccount.Radius = 3;
            this.txtBankAccount.ReadOnly = false;
            this.txtBankAccount.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtBankAccount.ShowError = false;
            this.txtBankAccount.Size = new System.Drawing.Size(175, 23);
            this.txtBankAccount.TabIndex = 4;
            this.txtBankAccount.UseSystemPasswordChar = false;
            this.txtBankAccount.Value = "";
            this.txtBankAccount.VerifyCondition = null;
            this.txtBankAccount.VerifyType = null;
            this.txtBankAccount.VerifyTypeName = null;
            this.txtBankAccount.WaterMark = null;
            this.txtBankAccount.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // txtBankName
            // 
            this.txtBankName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtBankName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtBankName.BackColor = System.Drawing.Color.Transparent;
            this.txtBankName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtBankName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.txtBankName.ForeImage = null;
            this.txtBankName.InputtingVerifyCondition = null;
            this.txtBankName.Location = new System.Drawing.Point(86, 25);
            this.txtBankName.MaxLengh = 32767;
            this.txtBankName.Multiline = false;
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Radius = 3;
            this.txtBankName.ReadOnly = false;
            this.txtBankName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.txtBankName.ShowError = false;
            this.txtBankName.Size = new System.Drawing.Size(150, 23);
            this.txtBankName.TabIndex = 3;
            this.txtBankName.UseSystemPasswordChar = false;
            this.txtBankName.Value = "";
            this.txtBankName.VerifyCondition = null;
            this.txtBankName.VerifyType = null;
            this.txtBankName.VerifyTypeName = null;
            this.txtBankName.WaterMark = null;
            this.txtBankName.WaterMarkColor = System.Drawing.Color.Silver;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(538, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "状态：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(263, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "银行账号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "银行名称：";
            // 
            // dgvBank
            // 
            this.dgvBank.AllowUserToAddRows = false;
            this.dgvBank.AllowUserToDeleteRows = false;
            this.dgvBank.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvBank.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBank.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBank.BackgroundColor = System.Drawing.Color.White;
            this.dgvBank.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBank.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBank.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBank.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.columnId,
            this.colBankName,
            this.colBankAccount,
            this.colStatus,
            this.columnCreate_by,
            this.columnCreate_Time,
            this.columnUpdate_By,
            this.columnUpdate_Time});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBank.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvBank.EnableHeadersVisualStyles = false;
            this.dgvBank.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvBank.IsCheck = true;
            this.dgvBank.Location = new System.Drawing.Point(-1, 109);
            this.dgvBank.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvBank.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvBank.MergeColumnNames")));
            this.dgvBank.MultiSelect = false;
            this.dgvBank.Name = "dgvBank";
            this.dgvBank.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBank.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvBank.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvBank.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvBank.RowTemplate.Height = 23;
            this.dgvBank.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBank.ShowCheckBox = true;
            this.dgvBank.Size = new System.Drawing.Size(1032, 385);
            this.dgvBank.TabIndex = 4;
            this.dgvBank.HeadCheckChanged += new ServiceStationClient.ComponentUI.DataGridViewEx.DelegateOnClick(this.dgvRecord_HeadCheckChanged);
            this.dgvBank.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvBank_CellFormatting);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 30;
            // 
            // columnId
            // 
            this.columnId.DataPropertyName = "bank_account_id";
            this.columnId.HeaderText = "ID";
            this.columnId.Name = "columnId";
            this.columnId.ReadOnly = true;
            this.columnId.Visible = false;
            // 
            // colBankName
            // 
            this.colBankName.DataPropertyName = "bank_name";
            this.colBankName.HeaderText = "银行名称";
            this.colBankName.Name = "colBankName";
            this.colBankName.ReadOnly = true;
            this.colBankName.Width = 150;
            // 
            // colBankAccount
            // 
            this.colBankAccount.DataPropertyName = "bank_account";
            this.colBankAccount.HeaderText = "银行账号";
            this.colBankAccount.Name = "colBankAccount";
            this.colBankAccount.ReadOnly = true;
            this.colBankAccount.Width = 200;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "status";
            this.colStatus.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.colStatus.HeaderText = "状态";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colStatus.Width = 110;
            // 
            // columnCreate_by
            // 
            this.columnCreate_by.DataPropertyName = "create_by";
            this.columnCreate_by.HeaderText = "创建人";
            this.columnCreate_by.Name = "columnCreate_by";
            this.columnCreate_by.ReadOnly = true;
            // 
            // columnCreate_Time
            // 
            this.columnCreate_Time.DataPropertyName = "create_time";
            this.columnCreate_Time.HeaderText = "创建时间";
            this.columnCreate_Time.Name = "columnCreate_Time";
            this.columnCreate_Time.ReadOnly = true;
            this.columnCreate_Time.Width = 150;
            // 
            // columnUpdate_By
            // 
            this.columnUpdate_By.DataPropertyName = "update_by";
            this.columnUpdate_By.HeaderText = "最后编辑人";
            this.columnUpdate_By.Name = "columnUpdate_By";
            this.columnUpdate_By.ReadOnly = true;
            // 
            // columnUpdate_Time
            // 
            this.columnUpdate_Time.DataPropertyName = "update_time";
            this.columnUpdate_Time.HeaderText = "最后编辑时间";
            this.columnUpdate_Time.Name = "columnUpdate_Time";
            this.columnUpdate_Time.ReadOnly = true;
            this.columnUpdate_Time.Width = 150;
            // 
            // UCBankAccountManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.dgvBank);
            this.Name = "UCBankAccountManage";
            this.Size = new System.Drawing.Size(1030, 494);
            this.Load += new System.EventHandler(this.UCBankAccountManage_Load);
            this.Controls.SetChildIndex(this.dgvBank, 0);
            this.Controls.SetChildIndex(this.panelEx1, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBank)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.PanelEx panelEx1;
        private ServiceStationClient.ComponentUI.ButtonEx btnSearch;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboStatus;
        private ServiceStationClient.ComponentUI.TextBoxEx txtBankAccount;
        private ServiceStationClient.ComponentUI.TextBoxEx txtBankName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvBank;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBankName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBankAccount;
        private System.Windows.Forms.DataGridViewComboBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCreate_by;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCreate_Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnUpdate_By;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnUpdate_Time;
    }
}
