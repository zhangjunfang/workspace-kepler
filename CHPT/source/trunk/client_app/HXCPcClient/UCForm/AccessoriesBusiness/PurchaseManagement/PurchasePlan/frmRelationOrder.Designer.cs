namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchasePlan
{
    partial class frmRelationOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRelationOrder));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblfinish_count = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblplan_count = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblparts_name = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gvPurchseList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.OrderType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sup_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchseList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.gvPurchseList);
            this.pnlContainer.Controls.Add(this.lblfinish_count);
            this.pnlContainer.Controls.Add(this.label6);
            this.pnlContainer.Controls.Add(this.lblplan_count);
            this.pnlContainer.Controls.Add(this.label3);
            this.pnlContainer.Controls.Add(this.lblparts_name);
            this.pnlContainer.Controls.Add(this.label1);
            this.pnlContainer.Size = new System.Drawing.Size(665, 331);
            // 
            // lblfinish_count
            // 
            this.lblfinish_count.AutoSize = true;
            this.lblfinish_count.Location = new System.Drawing.Point(565, 19);
            this.lblfinish_count.Name = "lblfinish_count";
            this.lblfinish_count.Size = new System.Drawing.Size(11, 12);
            this.lblfinish_count.TabIndex = 11;
            this.lblfinish_count.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(491, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "完成数量：";
            // 
            // lblplan_count
            // 
            this.lblplan_count.AutoSize = true;
            this.lblplan_count.Location = new System.Drawing.Point(406, 19);
            this.lblplan_count.Name = "lblplan_count";
            this.lblplan_count.Size = new System.Drawing.Size(11, 12);
            this.lblplan_count.TabIndex = 9;
            this.lblplan_count.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(335, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "计划数量：";
            // 
            // lblparts_name
            // 
            this.lblparts_name.AutoSize = true;
            this.lblparts_name.Location = new System.Drawing.Point(108, 19);
            this.lblparts_name.Name = "lblparts_name";
            this.lblparts_name.Size = new System.Drawing.Size(11, 12);
            this.lblparts_name.TabIndex = 7;
            this.lblparts_name.Text = ".";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "物料名称：";
            // 
            // gvPurchseList
            // 
            this.gvPurchseList.AllowUserToAddRows = false;
            this.gvPurchseList.AllowUserToDeleteRows = false;
            this.gvPurchseList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gvPurchseList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvPurchseList.BackgroundColor = System.Drawing.Color.White;
            this.gvPurchseList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPurchseList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvPurchseList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPurchseList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrderType,
            this.sup_name,
            this.order_num,
            this.order_date});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPurchseList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvPurchseList.EnableHeadersVisualStyles = false;
            this.gvPurchseList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.gvPurchseList.IsCheck = true;
            this.gvPurchseList.Location = new System.Drawing.Point(14, 43);
            this.gvPurchseList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gvPurchseList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gvPurchseList.MergeColumnNames")));
            this.gvPurchseList.MultiSelect = false;
            this.gvPurchseList.Name = "gvPurchseList";
            this.gvPurchseList.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPurchseList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvPurchseList.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.gvPurchseList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvPurchseList.RowTemplate.Height = 23;
            this.gvPurchseList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPurchseList.ShowCheckBox = true;
            this.gvPurchseList.Size = new System.Drawing.Size(638, 253);
            this.gvPurchseList.TabIndex = 12;
            // 
            // OrderType
            // 
            this.OrderType.DataPropertyName = "OrderType";
            this.OrderType.HeaderText = "单据类型";
            this.OrderType.MinimumWidth = 150;
            this.OrderType.Name = "OrderType";
            this.OrderType.ReadOnly = true;
            this.OrderType.Width = 150;
            // 
            // sup_name
            // 
            this.sup_name.DataPropertyName = "sup_name";
            this.sup_name.HeaderText = "供应商名称";
            this.sup_name.MinimumWidth = 150;
            this.sup_name.Name = "sup_name";
            this.sup_name.ReadOnly = true;
            this.sup_name.Width = 150;
            // 
            // order_num
            // 
            this.order_num.DataPropertyName = "order_num";
            this.order_num.HeaderText = "单号";
            this.order_num.MinimumWidth = 180;
            this.order_num.Name = "order_num";
            this.order_num.ReadOnly = true;
            this.order_num.Width = 180;
            // 
            // order_date
            // 
            this.order_date.DataPropertyName = "order_date";
            this.order_date.HeaderText = "单据日期";
            this.order_date.MinimumWidth = 150;
            this.order_date.Name = "order_date";
            this.order_date.ReadOnly = true;
            this.order_date.Width = 150;
            // 
            // frmRelationOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 364);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmRelationOrder";
            this.Text = "计划完成详情";
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchseList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblfinish_count;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblplan_count;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblparts_name;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.DataGridViewEx gvPurchseList;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderType;
        private System.Windows.Forms.DataGridViewTextBoxColumn sup_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_date;
    }
}