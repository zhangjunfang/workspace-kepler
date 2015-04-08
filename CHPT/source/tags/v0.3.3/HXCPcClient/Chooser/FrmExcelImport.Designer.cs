namespace HXCPcClient.Chooser
{
    partial class FrmExcelImport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExcelImport));
            this.dgMatchList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnImportExcelFields = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnImportExcelData = new ServiceStationClient.ComponentUI.ButtonEx();
            this.lblImportStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BillField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HeardText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExcelField = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMatchList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.label2);
            this.pnlContainer.Controls.Add(this.lblImportStatus);
            this.pnlContainer.Controls.Add(this.btnImportExcelData);
            this.pnlContainer.Controls.Add(this.btnImportExcelFields);
            this.pnlContainer.Controls.Add(this.label1);
            this.pnlContainer.Controls.Add(this.dgMatchList);
            this.pnlContainer.Size = new System.Drawing.Size(529, 416);
            // 
            // dgMatchList
            // 
            this.dgMatchList.AllowUserToAddRows = false;
            this.dgMatchList.AllowUserToDeleteRows = false;
            this.dgMatchList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgMatchList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgMatchList.BackgroundColor = System.Drawing.Color.White;
            this.dgMatchList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMatchList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgMatchList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMatchList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BillField,
            this.HeardText,
            this.ExcelField});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMatchList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgMatchList.EnableHeadersVisualStyles = false;
            this.dgMatchList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgMatchList.Location = new System.Drawing.Point(0, 0);
            this.dgMatchList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgMatchList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgMatchList.MergeColumnNames")));
            this.dgMatchList.MultiSelect = false;
            this.dgMatchList.Name = "dgMatchList";
            this.dgMatchList.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMatchList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgMatchList.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgMatchList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgMatchList.RowTemplate.Height = 23;
            this.dgMatchList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMatchList.ShowCheckBox = true;
            this.dgMatchList.ShowNum = false;
            this.dgMatchList.Size = new System.Drawing.Size(358, 413);
            this.dgMatchList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(385, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "选 择 匹 配 列 名";
            // 
            // btnImportExcelFields
            // 
            this.btnImportExcelFields.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportExcelFields.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImportExcelFields.BackgroundImage")));
            this.btnImportExcelFields.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImportExcelFields.Caption = "浏   览......";
            this.btnImportExcelFields.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportExcelFields.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnImportExcelFields.DownImage = ((System.Drawing.Image)(resources.GetObject("btnImportExcelFields.DownImage")));
            this.btnImportExcelFields.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImportExcelFields.Location = new System.Drawing.Point(385, 31);
            this.btnImportExcelFields.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImportExcelFields.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnImportExcelFields.MoveImage")));
            this.btnImportExcelFields.Name = "btnImportExcelFields";
            this.btnImportExcelFields.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnImportExcelFields.NormalImage")));
            this.btnImportExcelFields.Size = new System.Drawing.Size(104, 42);
            this.btnImportExcelFields.TabIndex = 32;
            this.btnImportExcelFields.Click += new System.EventHandler(this.btnImportExcelFields_Click);
            // 
            // btnImportExcelData
            // 
            this.btnImportExcelData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportExcelData.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImportExcelData.BackgroundImage")));
            this.btnImportExcelData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImportExcelData.Caption = "导入Excel数据";
            this.btnImportExcelData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportExcelData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnImportExcelData.DownImage = ((System.Drawing.Image)(resources.GetObject("btnImportExcelData.DownImage")));
            this.btnImportExcelData.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImportExcelData.Location = new System.Drawing.Point(385, 188);
            this.btnImportExcelData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImportExcelData.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnImportExcelData.MoveImage")));
            this.btnImportExcelData.Name = "btnImportExcelData";
            this.btnImportExcelData.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnImportExcelData.NormalImage")));
            this.btnImportExcelData.Size = new System.Drawing.Size(104, 42);
            this.btnImportExcelData.TabIndex = 33;
            this.btnImportExcelData.Click += new System.EventHandler(this.btnImportExcelData_Click);
            // 
            // lblImportStatus
            // 
            this.lblImportStatus.AutoSize = true;
            this.lblImportStatus.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblImportStatus.ForeColor = System.Drawing.Color.Red;
            this.lblImportStatus.Location = new System.Drawing.Point(390, 326);
            this.lblImportStatus.Name = "lblImportStatus";
            this.lblImportStatus.Size = new System.Drawing.Size(0, 20);
            this.lblImportStatus.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(381, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 20);
            this.label2.TabIndex = 35;
            this.label2.Text = "选择导入的Excel文件";
            // 
            // BillField
            // 
            this.BillField.HeaderText = "字段名称";
            this.BillField.Name = "BillField";
            this.BillField.ReadOnly = true;
            this.BillField.Visible = false;
            // 
            // HeardText
            // 
            this.HeardText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.HeardText.HeaderText = "单据列名";
            this.HeardText.Name = "HeardText";
            this.HeardText.ReadOnly = true;
            this.HeardText.Width = 150;
            // 
            // ExcelField
            // 
            this.ExcelField.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ExcelField.HeaderText = "Excel列名";
            this.ExcelField.Name = "ExcelField";
            this.ExcelField.ReadOnly = true;
            this.ExcelField.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ExcelField.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ExcelField.Width = 200;
            // 
            // FrmExcelImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 447);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FrmExcelImport";
            this.Load += new System.EventHandler(this.FrmExcelImport_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMatchList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx dgMatchList;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ButtonEx btnImportExcelData;
        private ServiceStationClient.ComponentUI.ButtonEx btnImportExcelFields;
        private System.Windows.Forms.Label lblImportStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillField;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeardText;
        private System.Windows.Forms.DataGridViewComboBoxColumn ExcelField;
    }
}