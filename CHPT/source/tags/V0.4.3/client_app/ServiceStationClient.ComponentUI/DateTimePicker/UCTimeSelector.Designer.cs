namespace ServiceStationClient.ComponentUI
{
    partial class UCTimeSelector
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTime = new System.Windows.Forms.DataGridView();
            this.C1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTime)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTime
            // 
            this.dgvTime.AllowUserToAddRows = false;
            this.dgvTime.AllowUserToDeleteRows = false;
            this.dgvTime.AllowUserToResizeColumns = false;
            this.dgvTime.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvTime.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTime.BackgroundColor = System.Drawing.Color.White;
            this.dgvTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTime.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTime.ColumnHeadersVisible = false;
            this.dgvTime.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.C1,
            this.C2,
            this.C3,
            this.C4,
            this.C5,
            this.C6});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTime.Location = new System.Drawing.Point(0, 0);
            this.dgvTime.Name = "dgvTime";
            this.dgvTime.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvTime.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTime.RowTemplate.Height = 23;
            this.dgvTime.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvTime.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvTime.Size = new System.Drawing.Size(181, 150);
            this.dgvTime.TabIndex = 4;
            this.dgvTime.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTime_CellClick);
            this.dgvTime.SelectionChanged += new System.EventHandler(this.dgvTime_SelectionChanged);
            // 
            // C1
            // 
            this.C1.DataPropertyName = "C1";
            this.C1.HeaderText = "C1";
            this.C1.MinimumWidth = 30;
            this.C1.Name = "C1";
            this.C1.ReadOnly = true;
            this.C1.Width = 30;
            // 
            // C2
            // 
            this.C2.DataPropertyName = "C2";
            this.C2.HeaderText = "C2";
            this.C2.MinimumWidth = 30;
            this.C2.Name = "C2";
            this.C2.ReadOnly = true;
            this.C2.Width = 30;
            // 
            // C3
            // 
            this.C3.DataPropertyName = "C3";
            this.C3.HeaderText = "C3";
            this.C3.MinimumWidth = 30;
            this.C3.Name = "C3";
            this.C3.ReadOnly = true;
            this.C3.Width = 30;
            // 
            // C4
            // 
            this.C4.DataPropertyName = "C4";
            this.C4.HeaderText = "C4";
            this.C4.MinimumWidth = 30;
            this.C4.Name = "C4";
            this.C4.ReadOnly = true;
            this.C4.Width = 30;
            // 
            // C5
            // 
            this.C5.DataPropertyName = "C5";
            this.C5.HeaderText = "C5";
            this.C5.MinimumWidth = 30;
            this.C5.Name = "C5";
            this.C5.ReadOnly = true;
            this.C5.Width = 30;
            // 
            // C6
            // 
            this.C6.DataPropertyName = "C6";
            this.C6.HeaderText = "C6";
            this.C6.MinimumWidth = 30;
            this.C6.Name = "C6";
            this.C6.ReadOnly = true;
            this.C6.Width = 30;
            // 
            // UCTimeSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvTime);
            this.Name = "UCTimeSelector";
            this.Size = new System.Drawing.Size(181, 150);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1;
        private System.Windows.Forms.DataGridViewTextBoxColumn C2;
        private System.Windows.Forms.DataGridViewTextBoxColumn C3;
        private System.Windows.Forms.DataGridViewTextBoxColumn C4;
        private System.Windows.Forms.DataGridViewTextBoxColumn C5;
        private System.Windows.Forms.DataGridViewTextBoxColumn C6;
    }
}
