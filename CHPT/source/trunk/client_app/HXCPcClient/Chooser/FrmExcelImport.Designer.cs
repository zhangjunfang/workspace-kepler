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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExcelImport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
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
            dataGridViewCellStyle36.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgMatchList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle36;
            this.dgMatchList.BackgroundColor = System.Drawing.Color.White;
            this.dgMatchList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle37.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle37.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle37.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle37.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle37.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMatchList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle37;
            this.dgMatchList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMatchList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BillField,
            this.HeardText,
            this.ExcelField});
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle38.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle38.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle38.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle38.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMatchList.DefaultCellStyle = dataGridViewCellStyle38;
            this.dgMatchList.EnableHeadersVisualStyles = false;
            this.dgMatchList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgMatchList.IsCheck = true;
            this.dgMatchList.Location = new System.Drawing.Point(0, 0);
            this.dgMatchList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgMatchList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgMatchList.MergeColumnNames")));
            this.dgMatchList.MultiSelect = false;
            this.dgMatchList.Name = "dgMatchList";
            this.dgMatchList.ReadOnly = true;
            this.dgMatchList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle39.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgMatchList.RowHeadersDefaultCellStyle = dataGridViewCellStyle39;
            this.dgMatchList.RowHeadersVisible = false;
            dataGridViewCellStyle40.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle40.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle40.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle40.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgMatchList.RowsDefaultCellStyle = dataGridViewCellStyle40;
            this.dgMatchList.RowTemplate.Height = 23;
            this.dgMatchList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMatchList.ShowCheckBox = true;
            this.dgMatchList.ShowNum = false;
            this.dgMatchList.Size = new System.Drawing.Size(303, 387);
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
            this.ExcelField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            this.btnImportExcelFields.Caption = "浏 览 ...";
            this.btnImportExcelFields.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportExcelFields.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnImportExcelFields.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnImportExcelFields.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImportExcelFields.Location = new System.Drawing.Point(317, 25);
            this.btnImportExcelFields.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImportExcelFields.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnImportExcelFields.Name = "btnImportExcelFields";
            this.btnImportExcelFields.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnImportExcelFields.Size = new System.Drawing.Size(93, 24);
            this.btnImportExcelFields.TabIndex = 32;
            this.btnImportExcelFields.Click += new System.EventHandler(this.btnImportExcelFields_Click);
            // 
            // btnImportExcelData
            // 
            this.btnImportExcelData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportExcelData.BackgroundImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnImportExcelData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImportExcelData.Caption = "导 入";
            this.btnImportExcelData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportExcelData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnImportExcelData.DownImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnImportExcelData.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImportExcelData.Location = new System.Drawing.Point(317, 193);
            this.btnImportExcelData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImportExcelData.MoveImage = global::HXCPcClient.Properties.Resources.button_d1;
            this.btnImportExcelData.Name = "btnImportExcelData";
            this.btnImportExcelData.NormalImage = global::HXCPcClient.Properties.Resources.button_n1;
            this.btnImportExcelData.Size = new System.Drawing.Size(93, 24);
            this.btnImportExcelData.TabIndex = 33;
            this.btnImportExcelData.Click += new System.EventHandler(this.btnImportExcelData_Click);
            // 
            // lblChoice
            // 
            this.lblChoice.AutoSize = true;
            this.lblChoice.BackColor = System.Drawing.Color.Transparent;
            this.lblChoice.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblChoice.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblChoice.Location = new System.Drawing.Point(315, 3);
            this.lblChoice.Name = "lblChoice";
            this.lblChoice.Size = new System.Drawing.Size(99, 20);
            this.lblChoice.TabIndex = 34;
            this.lblChoice.Text = "选择Excel文件";
            // 
            // lblmatch
            // 
            this.lblmatch.AutoSize = true;
            this.lblmatch.BackColor = System.Drawing.Color.Transparent;
            this.lblmatch.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblmatch.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblmatch.Location = new System.Drawing.Point(316, 170);
            this.lblmatch.Name = "lblmatch";
            this.lblmatch.Size = new System.Drawing.Size(93, 20);
            this.lblmatch.TabIndex = 35;
            this.lblmatch.Text = "选择匹配列名";
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
        private System.Windows.Forms.Label lblChoice;
        private System.Windows.Forms.Label lblmatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillField;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeardText;
        private System.Windows.Forms.DataGridViewComboBoxColumn ExcelField;
    }
}