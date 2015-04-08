namespace HXCPcClient.UCForm.SysManage.AcountManage
{
    partial class UCBillCodeRule
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCBillCodeRule));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvBillCodeRule = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.bill_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bill_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code_method = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.delimiter = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.start_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.example = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.last_bill_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bill_code_rule_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillCodeRule)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Location = new System.Drawing.Point(0, 3);
            this.pnlOpt.Size = new System.Drawing.Size(1000, 28);
            // 
            // dgvBillCodeRule
            // 
            this.dgvBillCodeRule.AllowUserToAddRows = false;
            this.dgvBillCodeRule.AllowUserToDeleteRows = false;
            this.dgvBillCodeRule.AllowUserToOrderColumns = true;
            this.dgvBillCodeRule.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvBillCodeRule.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBillCodeRule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBillCodeRule.BackgroundColor = System.Drawing.Color.White;
            this.dgvBillCodeRule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBillCodeRule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBillCodeRule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBillCodeRule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bill_type,
            this.bill_code,
            this.code_method,
            this.delimiter,
            this.start_num,
            this.example,
            this.last_bill_no,
            this.bill_code_rule_id});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBillCodeRule.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvBillCodeRule.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvBillCodeRule.EnableHeadersVisualStyles = false;
            this.dgvBillCodeRule.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvBillCodeRule.Location = new System.Drawing.Point(0, 30);
            this.dgvBillCodeRule.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvBillCodeRule.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvBillCodeRule.MergeColumnNames")));
            this.dgvBillCodeRule.MultiSelect = false;
            this.dgvBillCodeRule.Name = "dgvBillCodeRule";
            this.dgvBillCodeRule.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBillCodeRule.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvBillCodeRule.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvBillCodeRule.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvBillCodeRule.RowTemplate.Height = 23;
            this.dgvBillCodeRule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBillCodeRule.ShowCheckBox = true;
            this.dgvBillCodeRule.Size = new System.Drawing.Size(1000, 470);
            this.dgvBillCodeRule.TabIndex = 3;
            this.dgvBillCodeRule.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvBillCodeRule_CellBeginEdit);
            this.dgvBillCodeRule.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBillCodeRule_CellDoubleClick);
            this.dgvBillCodeRule.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBillCodeRule_CellValueChanged);
            // 
            // bill_type
            // 
            this.bill_type.DataPropertyName = "bill_type";
            this.bill_type.HeaderText = "项目类型";
            this.bill_type.Name = "bill_type";
            this.bill_type.ReadOnly = true;
            this.bill_type.Width = 120;
            // 
            // bill_code
            // 
            this.bill_code.DataPropertyName = "bill_code";
            this.bill_code.HeaderText = "项目编码";
            this.bill_code.Name = "bill_code";
            this.bill_code.ReadOnly = true;
            this.bill_code.Width = 120;
            // 
            // code_method
            // 
            this.code_method.DataPropertyName = "code_method";
            this.code_method.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.code_method.HeaderText = "编码方式";
            this.code_method.Name = "code_method";
            this.code_method.ReadOnly = true;
            this.code_method.Width = 120;
            // 
            // delimiter
            // 
            this.delimiter.DataPropertyName = "delimiter";
            this.delimiter.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.delimiter.HeaderText = "分隔符";
            this.delimiter.Name = "delimiter";
            this.delimiter.ReadOnly = true;
            this.delimiter.Width = 120;
            // 
            // start_num
            // 
            this.start_num.DataPropertyName = "start_num";
            this.start_num.HeaderText = "起始编号";
            this.start_num.Name = "start_num";
            this.start_num.ReadOnly = true;
            this.start_num.Width = 120;
            // 
            // example
            // 
            this.example.DataPropertyName = "example";
            this.example.HeaderText = "例子";
            this.example.Name = "example";
            this.example.ReadOnly = true;
            this.example.Width = 200;
            // 
            // last_bill_no
            // 
            this.last_bill_no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.last_bill_no.DataPropertyName = "last_bill_no";
            this.last_bill_no.HeaderText = "最后生成编码";
            this.last_bill_no.Name = "last_bill_no";
            this.last_bill_no.ReadOnly = true;
            // 
            // bill_code_rule_id
            // 
            this.bill_code_rule_id.DataPropertyName = "bill_code_rule_id";
            this.bill_code_rule_id.HeaderText = "id";
            this.bill_code_rule_id.Name = "bill_code_rule_id";
            this.bill_code_rule_id.ReadOnly = true;
            this.bill_code_rule_id.Visible = false;
            // 
            // UCBillCodeRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvBillCodeRule);
            this.Name = "UCBillCodeRule";
            this.Size = new System.Drawing.Size(1000, 500);
            this.Load += new System.EventHandler(this.UCBillCodeRule_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.dgvBillCodeRule, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillCodeRule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx dgvBillCodeRule;
        private System.Windows.Forms.DataGridViewTextBoxColumn bill_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn bill_code;
        private System.Windows.Forms.DataGridViewComboBoxColumn code_method;
        private System.Windows.Forms.DataGridViewComboBoxColumn delimiter;
        private System.Windows.Forms.DataGridViewTextBoxColumn start_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn example;
        private System.Windows.Forms.DataGridViewTextBoxColumn last_bill_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn bill_code_rule_id;

    }
}
