namespace HXCPcClient.UCForm.BusinessAnalysis
{
    partial class frmReportSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportSet));
            this.dgvSet = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.btnOK = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnClose = new ServiceStationClient.ComponentUI.ButtonEx();
            this.colSetID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSetWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsShow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colIsPrint = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSet)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.btnClose);
            this.pnlContainer.Controls.Add(this.btnOK);
            this.pnlContainer.Controls.Add(this.dgvSet);
            this.pnlContainer.Size = new System.Drawing.Size(449, 371);
            // 
            // dgvSet
            // 
            this.dgvSet.AllowUserToAddRows = false;
            this.dgvSet.AllowUserToDeleteRows = false;
            this.dgvSet.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvSet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvSet.BackgroundColor = System.Drawing.Color.White;
            this.dgvSet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSetID,
            this.colSetName,
            this.colSetWidth,
            this.colIsShow,
            this.colIsPrint});
            this.dgvSet.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSet.EnableHeadersVisualStyles = false;
            this.dgvSet.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgvSet.Location = new System.Drawing.Point(35, 12);
            this.dgvSet.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvSet.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvSet.MergeColumnNames")));
            this.dgvSet.MultiSelect = false;
            this.dgvSet.Name = "dgvSet";
            this.dgvSet.ReadOnly = true;
            this.dgvSet.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgvSet.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSet.RowTemplate.Height = 23;
            this.dgvSet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSet.ShowCheckBox = true;
            this.dgvSet.Size = new System.Drawing.Size(301, 348);
            this.dgvSet.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.Caption = "确定";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.DownImage = ((System.Drawing.Image)(resources.GetObject("btnOK.DownImage")));
            this.btnOK.Location = new System.Drawing.Point(369, 43);
            this.btnOK.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnOK.MoveImage")));
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnOK.NormalImage")));
            this.btnOK.Size = new System.Drawing.Size(60, 26);
            this.btnOK.TabIndex = 1;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Caption = "关闭";
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClose.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClose.DownImage")));
            this.btnClose.Location = new System.Drawing.Point(369, 105);
            this.btnClose.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClose.MoveImage")));
            this.btnClose.Name = "btnClose";
            this.btnClose.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClose.NormalImage")));
            this.btnClose.Size = new System.Drawing.Size(60, 26);
            this.btnClose.TabIndex = 2;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // colSetID
            // 
            this.colSetID.HeaderText = "ID";
            this.colSetID.Name = "colSetID";
            this.colSetID.ReadOnly = true;
            this.colSetID.Visible = false;
            this.colSetID.Width = 28;
            // 
            // colSetName
            // 
            this.colSetName.HeaderText = "列名";
            this.colSetName.Name = "colSetName";
            this.colSetName.ReadOnly = true;
            this.colSetName.Width = 57;
            // 
            // colSetWidth
            // 
            this.colSetWidth.HeaderText = "列宽";
            this.colSetWidth.Name = "colSetWidth";
            this.colSetWidth.ReadOnly = true;
            this.colSetWidth.Width = 57;
            // 
            // colIsShow
            // 
            this.colIsShow.HeaderText = "显示";
            this.colIsShow.MinimumWidth = 18;
            this.colIsShow.Name = "colIsShow";
            this.colIsShow.ReadOnly = true;
            this.colIsShow.Width = 38;
            // 
            // colIsPrint
            // 
            this.colIsPrint.HeaderText = "打印";
            this.colIsPrint.MinimumWidth = 18;
            this.colIsPrint.Name = "colIsPrint";
            this.colIsPrint.ReadOnly = true;
            this.colIsPrint.Width = 38;
            // 
            // frmReportSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 402);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReportSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报表设置";
            this.Load += new System.EventHandler(this.frmReportSet_Load);
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.ButtonEx btnClose;
        private ServiceStationClient.ComponentUI.ButtonEx btnOK;
        private ServiceStationClient.ComponentUI.DataGridViewEx dgvSet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSetID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSetWidth;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsShow;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsPrint;
    }
}