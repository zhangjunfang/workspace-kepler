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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExcelImport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgMatchList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.BillField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HeardText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExcelField = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btnImportExcelFields = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnImportExcelData = new ServiceStationClient.ComponentUI.ButtonEx();
            this.lblChoice = new System.Windows.Forms.Label();
            this.lblmatch = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMatchList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlContainer.Controls.Add(this.lblmatch);
            this.pnlContainer.Controls.Add(this.lblChoice);
            this.pnlContainer.Controls.Add(this.btnImportExcelData);
            this.pnlContainer.Controls.Add(this.btnImportExcelFields);
            this.pnlContainer.Controls.Add(this.dgMatchList);
            this.pnlContainer.Size = new System.Drawing.Size(437, 389);
            // 
            // dgMatchList
            // 
            this.dgMatchList.AllowUserToAddRows = false;
            this.dgMatchList.AllowUserToDeleteRows = false;
            this.dgMatchList.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgMatchList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgMatchList.BackgroundColor = System.Drawing.Color.White;
            this.dgMatchList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMatchList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgMatchList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMatchList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BillField,
            this.HeardText,
            this.ExcelField});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMatchList.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgMatchList.EnableHeadersVisualStyles = false;
            this.dgMatchList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.dgMatchList.IsCheck = true;
            this.dgMatchList.Location = new System.Drawing.Point(0, 0);
            this.dgMatchList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgMatchList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgMatchList.MergeColumnNames")));
            this.dgMatchList.MultiSelect = false;
            this.dgMatchList.Name = "dgMatchList";
            this.dgMatchList.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMatchList.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgMatchList.RowHeadersVisible = false;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.dgMatchList.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgMatchList.RowTemplate.Height = 23;
            this.dgMatchList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMatchList.ShowCheckBox = true;
            this.dgMatchList.ShowNum = false;
            this.dgMatchList.Size = new System.Drawing.Size(303, 397);
            this.dgMatchList.TabIndex = 0;
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
            this.ExcelField.Width = 150;
            // 
            // btnImportExcelFields
            // 
            this.btnImportExcelFields.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportExcelFields.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnImportExcelFields.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImportExcelFields.Caption = "浏   览......";
            this.btnImportExcelFields.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportExcelFields.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnImportExcelFields.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnImportExcelFields.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImportExcelFields.Location = new System.Drawing.Point(315, 25);
            this.btnImportExcelFields.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImportExcelFields.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnImportExcelFields.Name = "btnImportExcelFields";
            this.btnImportExcelFields.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnImportExcelFields.Size = new System.Drawing.Size(100, 30);
            this.btnImportExcelFields.TabIndex = 32;
            this.btnImportExcelFields.Click += new System.EventHandler(this.btnImportExcelFields_Click);
            // 
            // btnImportExcelData
            // 
            this.btnImportExcelData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportExcelData.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnImportExcelData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImportExcelData.Caption = "导入Excel数据";
            this.btnImportExcelData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportExcelData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnImportExcelData.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnImportExcelData.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImportExcelData.Location = new System.Drawing.Point(315, 193);
            this.btnImportExcelData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImportExcelData.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnImportExcelData.Name = "btnImportExcelData";
            this.btnImportExcelData.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnImportExcelData.Size = new System.Drawing.Size(100, 30);
            this.btnImportExcelData.TabIndex = 33;
            this.btnImportExcelData.Click += new System.EventHandler(this.btnImportExcelData_Click);
            // 
            // lblChoice
            // 
            this.lblChoice.AutoSize = true;
            this.lblChoice.BackColor = System.Drawing.Color.Transparent;
            this.lblChoice.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblChoice.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblChoice.Location = new System.Drawing.Point(307, 3);
            this.lblChoice.Name = "lblChoice";
            this.lblChoice.Size = new System.Drawing.Size(127, 20);
            this.lblChoice.TabIndex = 34;
            this.lblChoice.Text = "选择导入Excel文件";
            // 
            // lblmatch
            // 
            this.lblmatch.AutoSize = true;
            this.lblmatch.BackColor = System.Drawing.Color.Transparent;
            this.lblmatch.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblmatch.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblmatch.Location = new System.Drawing.Point(307, 168);
            this.lblmatch.Name = "lblmatch";
            this.lblmatch.Size = new System.Drawing.Size(113, 20);
            this.lblmatch.TabIndex = 35;
            this.lblmatch.Text = "选 择 匹 配 列 名";
            // 
            // FrmExcelImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(440, 420);
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
        private ServiceStationClient.ComponentUI.ButtonEx btnImportExcelData;
        private ServiceStationClient.ComponentUI.ButtonEx btnImportExcelFields;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillField;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeardText;
        private System.Windows.Forms.DataGridViewComboBoxColumn ExcelField;
        private System.Windows.Forms.Label lblChoice;
        private System.Windows.Forms.Label lblmatch;
    }
}