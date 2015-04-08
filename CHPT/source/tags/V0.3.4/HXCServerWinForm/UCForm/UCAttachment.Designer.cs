namespace HXCServerWinForm.UCForm
{
    partial class UCAttachment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAttachment = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheckAtt = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colAttID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAttName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAttType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAttPath = new ServiceStationClient.ComponentUI.DataGrid.DataGridViewFileUploadColumn();
            this.colAttRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsAttMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDown = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttachment)).BeginInit();
            this.cmsAttMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAttachment
            // 
            this.dgvAttachment.AllowUserToAddRows = false;
            this.dgvAttachment.AllowUserToDeleteRows = false;
            this.dgvAttachment.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvAttachment.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAttachment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAttachment.BackgroundColor = System.Drawing.Color.White;
            this.dgvAttachment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAttachment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAttachment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttachment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheckAtt,
            this.colAttID,
            this.colAttName,
            this.colAttType,
            this.colAttPath,
            this.colAttRemark});
            this.dgvAttachment.ContextMenuStrip = this.cmsAttMenu;
            this.dgvAttachment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAttachment.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvAttachment.EnableHeadersVisualStyles = false;
            this.dgvAttachment.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvAttachment.Location = new System.Drawing.Point(0, 0);
            this.dgvAttachment.MultiSelect = false;
            this.dgvAttachment.Name = "dgvAttachment";
            this.dgvAttachment.ReadOnly = true;
            this.dgvAttachment.RowHeadersVisible = false;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvAttachment.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAttachment.RowTemplate.Height = 23;
            this.dgvAttachment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAttachment.Size = new System.Drawing.Size(450, 262);
            this.dgvAttachment.TabIndex = 1;
            this.dgvAttachment.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAttachment_CellValueChanged);
            // 
            // colCheckAtt
            // 
            this.colCheckAtt.HeaderText = "选择";
            this.colCheckAtt.Name = "colCheckAtt";
            this.colCheckAtt.ReadOnly = true;
            this.colCheckAtt.Width = 38;
            // 
            // colAttID
            // 
            this.colAttID.HeaderText = "ID";
            this.colAttID.Name = "colAttID";
            this.colAttID.ReadOnly = true;
            this.colAttID.Visible = false;
            this.colAttID.Width = 47;
            // 
            // colAttName
            // 
            this.colAttName.HeaderText = "附件名称";
            this.colAttName.Name = "colAttName";
            this.colAttName.ReadOnly = true;
            this.colAttName.Width = 81;
            // 
            // colAttType
            // 
            this.colAttType.HeaderText = "类别";
            this.colAttType.Name = "colAttType";
            this.colAttType.ReadOnly = true;
            this.colAttType.Width = 57;
            // 
            // colAttPath
            // 
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Blue;
            this.colAttPath.DefaultCellStyle = dataGridViewCellStyle3;
            this.colAttPath.HeaderText = "文件详情";
            this.colAttPath.MinimumWidth = 190;
            this.colAttPath.Name = "colAttPath";
            this.colAttPath.ReadOnly = true;
            this.colAttPath.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colAttPath.Width = 190;
            // 
            // colAttRemark
            // 
            this.colAttRemark.HeaderText = "备注";
            this.colAttRemark.Name = "colAttRemark";
            this.colAttRemark.ReadOnly = true;
            this.colAttRemark.Width = 57;
            // 
            // cmsAttMenu
            // 
            this.cmsAttMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdd,
            this.tsmiDelete,
            this.tsmiDown});
            this.cmsAttMenu.Name = "cmsAttMenu";
            this.cmsAttMenu.Size = new System.Drawing.Size(153, 92);
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(152, 22);
            this.tsmiAdd.Text = "新增";
            this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(152, 22);
            this.tsmiDelete.Text = "删除";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // tsmiDown
            // 
            this.tsmiDown.Name = "tsmiDown";
            this.tsmiDown.Size = new System.Drawing.Size(152, 22);
            this.tsmiDown.Text = "下载";
            this.tsmiDown.Click += new System.EventHandler(this.tsmiDown_Click);
            // 
            // UCAttachment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvAttachment);
            this.Name = "UCAttachment";
            this.Size = new System.Drawing.Size(450, 262);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttachment)).EndInit();
            this.cmsAttMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx dgvAttachment;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheckAtt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAttID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAttName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAttType;
        private ServiceStationClient.ComponentUI.DataGrid.DataGridViewFileUploadColumn colAttPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAttRemark;
        private System.Windows.Forms.ContextMenuStrip cmsAttMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiDown;
    }
}
