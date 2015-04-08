namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.YuTongPurchaseOrder
{
    partial class frmDistributionView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDistributionView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.lblYTOrder_num = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBusinessCount = new System.Windows.Forms.Label();
            this.lblcount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gvDisList = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.OrderType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ration_send_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.send_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dis_status_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDisList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.label7);
            this.pnlContainer.Controls.Add(this.gvDisList);
            this.pnlContainer.Controls.Add(this.lblcount);
            this.pnlContainer.Controls.Add(this.label6);
            this.pnlContainer.Controls.Add(this.lblBusinessCount);
            this.pnlContainer.Controls.Add(this.label3);
            this.pnlContainer.Controls.Add(this.lblYTOrder_num);
            this.pnlContainer.Controls.Add(this.label1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "宇通采购订单号：";
            // 
            // lblYTOrder_num
            // 
            this.lblYTOrder_num.AutoSize = true;
            this.lblYTOrder_num.Location = new System.Drawing.Point(133, 19);
            this.lblYTOrder_num.Name = "lblYTOrder_num";
            this.lblYTOrder_num.Size = new System.Drawing.Size(11, 12);
            this.lblYTOrder_num.TabIndex = 1;
            this.lblYTOrder_num.Text = ".";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(333, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "订单数量：";
            // 
            // lblBusinessCount
            // 
            this.lblBusinessCount.AutoSize = true;
            this.lblBusinessCount.Location = new System.Drawing.Point(404, 19);
            this.lblBusinessCount.Name = "lblBusinessCount";
            this.lblBusinessCount.Size = new System.Drawing.Size(11, 12);
            this.lblBusinessCount.TabIndex = 3;
            this.lblBusinessCount.Text = "0";
            // 
            // lblcount
            // 
            this.lblcount.AutoSize = true;
            this.lblcount.Location = new System.Drawing.Point(568, 19);
            this.lblcount.Name = "lblcount";
            this.lblcount.Size = new System.Drawing.Size(11, 12);
            this.lblcount.TabIndex = 5;
            this.lblcount.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(494, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "配送数量：";
            // 
            // gvDisList
            // 
            this.gvDisList.AllowUserToAddRows = false;
            this.gvDisList.AllowUserToDeleteRows = false;
            this.gvDisList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gvDisList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvDisList.BackgroundColor = System.Drawing.Color.White;
            this.gvDisList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvDisList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvDisList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDisList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrderType,
            this.ration_send_code,
            this.send_count,
            this.dis_status_name,
            this.order_num});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvDisList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvDisList.EnableHeadersVisualStyles = false;
            this.gvDisList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
            this.gvDisList.Location = new System.Drawing.Point(23, 49);
            this.gvDisList.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.gvDisList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("gvDisList.MergeColumnNames")));
            this.gvDisList.MultiSelect = false;
            this.gvDisList.Name = "gvDisList";
            this.gvDisList.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvDisList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvDisList.RowHeadersVisible = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            this.gvDisList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvDisList.RowTemplate.Height = 23;
            this.gvDisList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvDisList.ShowCheckBox = true;
            this.gvDisList.Size = new System.Drawing.Size(638, 279);
            this.gvDisList.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 339);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(557, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "说明：系统会根据宇通的配送单自动生成一张采购开单，待采购开单确认审核后，配送状态变为已收货。";
            // 
            // OrderType
            // 
            this.OrderType.DataPropertyName = "OrderType";
            this.OrderType.HeaderText = "单据类型";
            this.OrderType.Name = "OrderType";
            this.OrderType.ReadOnly = true;
            // 
            // ration_send_code
            // 
            this.ration_send_code.DataPropertyName = "ration_send_code";
            this.ration_send_code.HeaderText = "单号";
            this.ration_send_code.Name = "ration_send_code";
            this.ration_send_code.ReadOnly = true;
            this.ration_send_code.Width = 130;
            // 
            // send_count
            // 
            this.send_count.DataPropertyName = "send_count";
            this.send_count.HeaderText = "配送数量";
            this.send_count.Name = "send_count";
            this.send_count.ReadOnly = true;
            // 
            // dis_status_name
            // 
            this.dis_status_name.DataPropertyName = "dis_status_name";
            this.dis_status_name.HeaderText = "配送状态";
            this.dis_status_name.Name = "dis_status_name";
            this.dis_status_name.ReadOnly = true;
            // 
            // order_num
            // 
            this.order_num.DataPropertyName = "order_num";
            this.order_num.HeaderText = "关联开单号";
            this.order_num.Name = "order_num";
            this.order_num.ReadOnly = true;
            this.order_num.Width = 200;
            // 
            // frmDistributionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 402);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmDistributionView";
            this.Text = "宇通配送状态";
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDisList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblYTOrder_num;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBusinessCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblcount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private ServiceStationClient.ComponentUI.DataGridViewEx gvDisList;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ration_send_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn send_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn dis_status_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_num;
    }
}