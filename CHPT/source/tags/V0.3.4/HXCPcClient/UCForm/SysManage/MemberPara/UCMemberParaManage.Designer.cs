namespace HXCPcClient.UCForm.SysManage.MemberPara
{
    partial class UCMemberParaManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCMemberParaManage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvRecord = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subscription_Ratio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.service_project_discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.member_grade_id = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.setInfo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRecord
            // 
            this.dgvRecord.AllowUserToAddRows = false;
            this.dgvRecord.AllowUserToDeleteRows = false;
            this.dgvRecord.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvRecord.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecord.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.setInfo_id,
            this.member_grade_id,
            this.service_project_discount,
            this.parts_discount,
            this.Subscription_Ratio,
            this.column1});
            this.dgvRecord.EnableHeadersVisualStyles = false;
            this.dgvRecord.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvRecord.Location = new System.Drawing.Point(-1, 26);
            this.dgvRecord.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvRecord.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvRecord.MergeColumnNames")));
            this.dgvRecord.MultiSelect = false;
            this.dgvRecord.Name = "dgvRecord";
            this.dgvRecord.ReadOnly = true;
            this.dgvRecord.RowHeadersVisible = false;
            this.dgvRecord.RowHeadersWidth = 30;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvRecord.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRecord.RowTemplate.Height = 23;
            this.dgvRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecord.ShowCheckBox = true;
            this.dgvRecord.Size = new System.Drawing.Size(1032, 518);
            this.dgvRecord.TabIndex = 10;
            this.dgvRecord.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvsetInfo_CellDoubleClick);
            // 
            // column1
            // 
            this.column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.column1.HeaderText = "";
            this.column1.Name = "column1";
            this.column1.ReadOnly = true;
            // 
            // Subscription_Ratio
            // 
            this.Subscription_Ratio.DataPropertyName = "Subscription_Ratio";
            this.Subscription_Ratio.HeaderText = "积分比例(消费金额/每分)";
            this.Subscription_Ratio.Name = "Subscription_Ratio";
            this.Subscription_Ratio.ReadOnly = true;
            this.Subscription_Ratio.Width = 200;
            // 
            // parts_discount
            // 
            this.parts_discount.DataPropertyName = "parts_discount";
            this.parts_discount.HeaderText = "配件折扣";
            this.parts_discount.Name = "parts_discount";
            this.parts_discount.ReadOnly = true;
            this.parts_discount.Width = 170;
            // 
            // service_project_discount
            // 
            this.service_project_discount.DataPropertyName = "service_project_discount";
            this.service_project_discount.HeaderText = "维修项目折扣";
            this.service_project_discount.Name = "service_project_discount";
            this.service_project_discount.ReadOnly = true;
            this.service_project_discount.Width = 200;
            // 
            // member_grade_id
            // 
            this.member_grade_id.DataPropertyName = "member_grade_id";
            this.member_grade_id.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.member_grade_id.HeaderText = "会员等级";
            this.member_grade_id.Name = "member_grade_id";
            this.member_grade_id.ReadOnly = true;
            this.member_grade_id.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.member_grade_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.member_grade_id.Width = 130;
            // 
            // setInfo_id
            // 
            this.setInfo_id.DataPropertyName = "setInfo_id";
            this.setInfo_id.HeaderText = "setInfo_id";
            this.setInfo_id.Name = "setInfo_id";
            this.setInfo_id.ReadOnly = true;
            this.setInfo_id.Visible = false;
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 18;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 30;
            // 
            // UCMemberParaManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvRecord);
            this.Name = "UCMemberParaManage";
            this.Load += new System.EventHandler(this.UCMemberParaManage_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.dgvRecord, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRecord;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn setInfo_id;
        private System.Windows.Forms.DataGridViewComboBoxColumn member_grade_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn service_project_discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subscription_Ratio;
        private System.Windows.Forms.DataGridViewTextBoxColumn column1;
    }
}
