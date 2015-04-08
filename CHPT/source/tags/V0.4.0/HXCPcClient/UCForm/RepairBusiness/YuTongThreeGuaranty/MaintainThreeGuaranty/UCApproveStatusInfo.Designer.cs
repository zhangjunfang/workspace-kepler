namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuaranty
{
    partial class UCApproveStatusInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCApproveStatusInfo));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTable = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.drtxt_approve_state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_approve_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_approve_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_approve_idea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drtxt_remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlContainer.Controls.Add(this.dgvTable);
            this.pnlContainer.Size = new System.Drawing.Size(875, 287);
            // 
            // dgvTable
            // 
            this.dgvTable.AllowUserToAddRows = false;
            this.dgvTable.AllowUserToDeleteRows = false;
            this.dgvTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTable.BackgroundColor = System.Drawing.Color.White;
            this.dgvTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.drtxt_approve_state,
            this.drtxt_approve_name,
            this.drtxt_approve_time,
            this.drtxt_approve_idea,
            this.drtxt_remark});
            this.dgvTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTable.EnableHeadersVisualStyles = false;
            this.dgvTable.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvTable.Location = new System.Drawing.Point(0, 0);
            this.dgvTable.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvTable.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvTable.MergeColumnNames")));
            this.dgvTable.MultiSelect = false;
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.ReadOnly = true;
            this.dgvTable.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvTable.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTable.RowTemplate.Height = 23;
            this.dgvTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTable.ShowCheckBox = true;
            this.dgvTable.Size = new System.Drawing.Size(875, 287);
            this.dgvTable.TabIndex = 7;
            // 
            // drtxt_approve_state
            // 
            this.drtxt_approve_state.DataPropertyName = "approve_state";
            this.drtxt_approve_state.HeaderText = "审核状态";
            this.drtxt_approve_state.Name = "drtxt_approve_state";
            this.drtxt_approve_state.ReadOnly = true;
            // 
            // drtxt_approve_name
            // 
            this.drtxt_approve_name.DataPropertyName = "approve_name";
            this.drtxt_approve_name.HeaderText = "审核人";
            this.drtxt_approve_name.Name = "drtxt_approve_name";
            this.drtxt_approve_name.ReadOnly = true;
            // 
            // drtxt_approve_time
            // 
            this.drtxt_approve_time.DataPropertyName = "approve_time";
            this.drtxt_approve_time.HeaderText = "审核时间";
            this.drtxt_approve_time.Name = "drtxt_approve_time";
            this.drtxt_approve_time.ReadOnly = true;
            // 
            // drtxt_approve_idea
            // 
            this.drtxt_approve_idea.DataPropertyName = "approve_idea";
            this.drtxt_approve_idea.HeaderText = "审核意见";
            this.drtxt_approve_idea.Name = "drtxt_approve_idea";
            this.drtxt_approve_idea.ReadOnly = true;
            // 
            // drtxt_remark
            // 
            this.drtxt_remark.DataPropertyName = "remark";
            this.drtxt_remark.HeaderText = "其他信息";
            this.drtxt_remark.Name = "drtxt_remark";
            this.drtxt_remark.ReadOnly = true;
            // 
            // UCApproveStatusInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(876, 318);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "UCApproveStatusInfo";
            this.Text = "服务单审核详情";
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx dgvTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_approve_state;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_approve_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_approve_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_approve_idea;
        private System.Windows.Forms.DataGridViewTextBoxColumn drtxt_remark;


    }
}