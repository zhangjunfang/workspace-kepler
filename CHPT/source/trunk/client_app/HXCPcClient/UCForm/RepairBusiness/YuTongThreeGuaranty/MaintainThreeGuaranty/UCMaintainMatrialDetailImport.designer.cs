namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuaranty
{
    partial class UCMaintainMatrialDetailImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCMaintainMatrialDetailImport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvMaterials = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.Mcheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.parts_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mthree_warranty = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.norms = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.whether_imported = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Msum_money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drawn_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicle_model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mremarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.material_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mdata_source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.dgvMaterials);
            this.pnlContainer.Location = new System.Drawing.Point(1, 33);
            this.pnlContainer.Size = new System.Drawing.Size(907, 465);
            // 
            // dgvMaterials
            // 
            this.dgvMaterials.AllowUserToAddRows = false;
            this.dgvMaterials.AllowUserToDeleteRows = false;
            this.dgvMaterials.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvMaterials.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMaterials.BackgroundColor = System.Drawing.Color.White;
            this.dgvMaterials.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMaterials.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMaterials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterials.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Mcheck,
            this.parts_code,
            this.parts_name,
            this.Mthree_warranty,
            this.norms,
            this.unit,
            this.whether_imported,
            this.quantity,
            this.unit_price,
            this.Msum_money,
            this.drawn_no,
            this.vehicle_model,
            this.Mremarks,
            this.material_id,
            this.parts_id,
            this.Mdata_source});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMaterials.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMaterials.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaterials.EnableHeadersVisualStyles = false;
            this.dgvMaterials.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvMaterials.IsCheck = true;
            this.dgvMaterials.Location = new System.Drawing.Point(0, 0);
            this.dgvMaterials.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvMaterials.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvMaterials.MergeColumnNames")));
            this.dgvMaterials.MultiSelect = false;
            this.dgvMaterials.Name = "dgvMaterials";
            this.dgvMaterials.ReadOnly = true;
            this.dgvMaterials.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvMaterials.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMaterials.RowHeadersVisible = false;
            this.dgvMaterials.RowHeadersWidth = 30;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvMaterials.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvMaterials.RowTemplate.Height = 23;
            this.dgvMaterials.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaterials.ShowCheckBox = true;
            this.dgvMaterials.Size = new System.Drawing.Size(907, 465);
            this.dgvMaterials.TabIndex = 15;
            // 
            // Mcheck
            // 
            this.Mcheck.HeaderText = "";
            this.Mcheck.MinimumWidth = 30;
            this.Mcheck.Name = "Mcheck";
            this.Mcheck.ReadOnly = true;
            this.Mcheck.Width = 30;
            // 
            // parts_code
            // 
            this.parts_code.DataPropertyName = "parts_code";
            this.parts_code.HeaderText = "配件编码";
            this.parts_code.Name = "parts_code";
            this.parts_code.ReadOnly = true;
            this.parts_code.Width = 90;
            // 
            // parts_name
            // 
            this.parts_name.DataPropertyName = "parts_name";
            this.parts_name.HeaderText = "配件名称";
            this.parts_name.Name = "parts_name";
            this.parts_name.ReadOnly = true;
            this.parts_name.Width = 90;
            // 
            // Mthree_warranty
            // 
            this.Mthree_warranty.DataPropertyName = "three_warranty";
            this.Mthree_warranty.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Mthree_warranty.DisplayStyleForCurrentCellOnly = true;
            this.Mthree_warranty.HeaderText = "是否三包";
            this.Mthree_warranty.Items.AddRange(new object[] {
            "否",
            "是"});
            this.Mthree_warranty.Name = "Mthree_warranty";
            this.Mthree_warranty.ReadOnly = true;
            this.Mthree_warranty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Mthree_warranty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // norms
            // 
            this.norms.DataPropertyName = "norms";
            this.norms.HeaderText = "规格";
            this.norms.Name = "norms";
            this.norms.ReadOnly = true;
            this.norms.Width = 60;
            // 
            // unit
            // 
            this.unit.DataPropertyName = "unit";
            this.unit.HeaderText = "单位";
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            this.unit.Width = 60;
            // 
            // whether_imported
            // 
            this.whether_imported.DataPropertyName = "whether_imported";
            this.whether_imported.HeaderText = "进口";
            this.whether_imported.Name = "whether_imported";
            this.whether_imported.ReadOnly = true;
            this.whether_imported.Width = 60;
            // 
            // quantity
            // 
            this.quantity.DataPropertyName = "quantity";
            this.quantity.HeaderText = "数量";
            this.quantity.Name = "quantity";
            this.quantity.ReadOnly = true;
            this.quantity.Width = 60;
            // 
            // unit_price
            // 
            this.unit_price.DataPropertyName = "unit_price";
            this.unit_price.HeaderText = "原始单价";
            this.unit_price.Name = "unit_price";
            this.unit_price.ReadOnly = true;
            this.unit_price.Width = 90;
            // 
            // Msum_money
            // 
            this.Msum_money.DataPropertyName = "sum_money";
            this.Msum_money.HeaderText = "金额";
            this.Msum_money.Name = "Msum_money";
            this.Msum_money.ReadOnly = true;
            this.Msum_money.Width = 80;
            // 
            // drawn_no
            // 
            this.drawn_no.DataPropertyName = "drawn_no";
            this.drawn_no.HeaderText = "图号";
            this.drawn_no.Name = "drawn_no";
            this.drawn_no.ReadOnly = true;
            this.drawn_no.Width = 80;
            // 
            // vehicle_model
            // 
            this.vehicle_model.DataPropertyName = "vehicle_model";
            this.vehicle_model.HeaderText = "适用车型(品牌)";
            this.vehicle_model.Name = "vehicle_model";
            this.vehicle_model.ReadOnly = true;
            this.vehicle_model.Width = 120;
            // 
            // Mremarks
            // 
            this.Mremarks.DataPropertyName = "remarks";
            this.Mremarks.HeaderText = "备注";
            this.Mremarks.Name = "Mremarks";
            this.Mremarks.ReadOnly = true;
            this.Mremarks.Width = 90;
            // 
            // material_id
            // 
            this.material_id.DataPropertyName = "material_id";
            this.material_id.HeaderText = "material_id";
            this.material_id.Name = "material_id";
            this.material_id.ReadOnly = true;
            this.material_id.Visible = false;
            this.material_id.Width = 10;
            // 
            // parts_id
            // 
            this.parts_id.DataPropertyName = "parts_id";
            this.parts_id.HeaderText = "parts_id";
            this.parts_id.Name = "parts_id";
            this.parts_id.ReadOnly = true;
            this.parts_id.Visible = false;
            // 
            // Mdata_source
            // 
            this.Mdata_source.DataPropertyName = "data_source";
            this.Mdata_source.HeaderText = "data_source";
            this.Mdata_source.Name = "Mdata_source";
            this.Mdata_source.ReadOnly = true;
            this.Mdata_source.Visible = false;
            // 
            // UCMaintainMatrialDetailImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 499);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximumSize = new System.Drawing.Size(1920, 1127);
            this.Name = "UCMaintainMatrialDetailImport";
            this.Text = "维修用料信息导入";
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx dgvMaterials;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Mcheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_name;
        private System.Windows.Forms.DataGridViewComboBoxColumn Mthree_warranty;
        private System.Windows.Forms.DataGridViewTextBoxColumn norms;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn whether_imported;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Msum_money;
        private System.Windows.Forms.DataGridViewTextBoxColumn drawn_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicle_model;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mremarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn material_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mdata_source;






    }
}