namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuaranty
{
    partial class UCMaintainWorkhourImport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCMaintainWorkhourImport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_primary = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.item_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.three_warranty = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.man_hour_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.man_hour_quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.man_hour_norm_unitprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sum_money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whours_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data_source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_primary)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.dgv_primary);
            this.pnlContainer.Location = new System.Drawing.Point(1, 33);
            this.pnlContainer.Size = new System.Drawing.Size(907, 465);
            // 
            // dgv_primary
            // 
            this.dgv_primary.AllowUserToAddRows = false;
            this.dgv_primary.AllowUserToDeleteRows = false;
            this.dgv_primary.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgv_primary.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_primary.BackgroundColor = System.Drawing.Color.White;
            this.dgv_primary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_primary.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_primary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_primary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.item_no,
            this.item_type,
            this.item_name,
            this.three_warranty,
            this.man_hour_type,
            this.man_hour_quantity,
            this.man_hour_norm_unitprice,
            this.sum_money,
            this.remarks,
            this.item_id,
            this.whours_id,
            this.data_source});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_primary.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_primary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_primary.EnableHeadersVisualStyles = false;
            this.dgv_primary.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgv_primary.IsCheck = true;
            this.dgv_primary.Location = new System.Drawing.Point(0, 0);
            this.dgv_primary.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgv_primary.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv_primary.MergeColumnNames")));
            this.dgv_primary.MultiSelect = false;
            this.dgv_primary.Name = "dgv_primary";
            this.dgv_primary.ReadOnly = true;
            this.dgv_primary.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgv_primary.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_primary.RowHeadersVisible = false;
            this.dgv_primary.RowHeadersWidth = 30;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgv_primary.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_primary.RowTemplate.Height = 23;
            this.dgv_primary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_primary.ShowCheckBox = true;
            this.dgv_primary.Size = new System.Drawing.Size(907, 465);
            this.dgv_primary.TabIndex = 14;
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 30;
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            this.colCheck.Width = 30;
            // 
            // item_no
            // 
            this.item_no.DataPropertyName = "item_no";
            this.item_no.HeaderText = "项目编号";
            this.item_no.Name = "item_no";
            this.item_no.ReadOnly = true;
            this.item_no.Width = 110;
            // 
            // item_type
            // 
            this.item_type.DataPropertyName = "item_type";
            this.item_type.HeaderText = "维修项目类别";
            this.item_type.Name = "item_type";
            this.item_type.ReadOnly = true;
            this.item_type.Width = 120;
            // 
            // item_name
            // 
            this.item_name.DataPropertyName = "item_name";
            this.item_name.HeaderText = "项目名称";
            this.item_name.Name = "item_name";
            this.item_name.ReadOnly = true;
            this.item_name.Width = 110;
            // 
            // three_warranty
            // 
            this.three_warranty.DataPropertyName = "three_warranty";
            this.three_warranty.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.three_warranty.DisplayStyleForCurrentCellOnly = true;
            this.three_warranty.HeaderText = "是否三包";
            this.three_warranty.Items.AddRange(new object[] {
            "否",
            "是"});
            this.three_warranty.Name = "three_warranty";
            this.three_warranty.ReadOnly = true;
            this.three_warranty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.three_warranty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // man_hour_type
            // 
            this.man_hour_type.DataPropertyName = "man_hour_type";
            this.man_hour_type.HeaderText = "工时类别";
            this.man_hour_type.Name = "man_hour_type";
            this.man_hour_type.ReadOnly = true;
            this.man_hour_type.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.man_hour_type.Width = 110;
            // 
            // man_hour_quantity
            // 
            this.man_hour_quantity.DataPropertyName = "man_hour_quantity";
            dataGridViewCellStyle3.NullValue = null;
            this.man_hour_quantity.DefaultCellStyle = dataGridViewCellStyle3;
            this.man_hour_quantity.HeaderText = "工时数量";
            this.man_hour_quantity.Name = "man_hour_quantity";
            this.man_hour_quantity.ReadOnly = true;
            // 
            // man_hour_norm_unitprice
            // 
            this.man_hour_norm_unitprice.DataPropertyName = "man_hour_norm_unitprice";
            this.man_hour_norm_unitprice.HeaderText = "工时单价";
            this.man_hour_norm_unitprice.Name = "man_hour_norm_unitprice";
            this.man_hour_norm_unitprice.ReadOnly = true;
            // 
            // sum_money
            // 
            this.sum_money.DataPropertyName = "sum_money";
            dataGridViewCellStyle4.NullValue = null;
            this.sum_money.DefaultCellStyle = dataGridViewCellStyle4;
            this.sum_money.HeaderText = "金额";
            this.sum_money.Name = "sum_money";
            this.sum_money.ReadOnly = true;
            // 
            // remarks
            // 
            this.remarks.DataPropertyName = "remarks";
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            this.remarks.Width = 133;
            // 
            // item_id
            // 
            this.item_id.DataPropertyName = "item_id";
            this.item_id.HeaderText = "item_id";
            this.item_id.Name = "item_id";
            this.item_id.ReadOnly = true;
            this.item_id.Visible = false;
            this.item_id.Width = 10;
            // 
            // whours_id
            // 
            this.whours_id.HeaderText = "whours_id";
            this.whours_id.Name = "whours_id";
            this.whours_id.ReadOnly = true;
            this.whours_id.Visible = false;
            // 
            // data_source
            // 
            this.data_source.DataPropertyName = "data_source";
            this.data_source.HeaderText = "data_source";
            this.data_source.Name = "data_source";
            this.data_source.ReadOnly = true;
            this.data_source.Visible = false;
            // 
            // UCMaintainWorkhourImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 499);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximumSize = new System.Drawing.Size(1920, 1127);
            this.Name = "UCMaintainWorkhourImport";
            this.Text = "维修项目信息导入";
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_primary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx dgv_primary;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewComboBoxColumn three_warranty;
        private System.Windows.Forms.DataGridViewTextBoxColumn man_hour_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn man_hour_quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn man_hour_norm_unitprice;
        private System.Windows.Forms.DataGridViewTextBoxColumn sum_money;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn whours_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn data_source;





    }
}