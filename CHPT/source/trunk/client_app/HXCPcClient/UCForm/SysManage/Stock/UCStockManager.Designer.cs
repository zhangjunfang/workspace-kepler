namespace HXCPcClient.UCForm.SysManage.Stock
{
    partial class UCStockManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCStockManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvRecord = new ServiceStationClient.ComponentUI.DataGridViewEx(this.components);
            this.column_wh_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_parts_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_wh_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_wh_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_wh = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.columnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tvStock = new ServiceStationClient.ComponentUI.TreeViewEx();
            this.tbName = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCode = new ServiceStationClient.ComponentUI.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new ServiceStationClient.ComponentUI.ButtonEx();
            this.btnQuery = new ServiceStationClient.ComponentUI.ButtonEx();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRecord
            // 
            this.dgvRecord.AllowUserToAddRows = false;
            this.dgvRecord.AllowUserToDeleteRows = false;
            this.dgvRecord.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvRecord.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecord.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column_wh_id,
            this.column_parts_id,
            this.column_wh_code,
            this.column_wh_name,
            this.columnCode,
            this.columnName,
            this.columnCount,
            this.columnUnit,
            this.columnMin,
            this.columnMax,
            this.column_wh,
            this.columnId,
            this.Column1});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecord.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRecord.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvRecord.EnableHeadersVisualStyles = false;
            this.dgvRecord.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvRecord.IsCheck = true;
            this.dgvRecord.Location = new System.Drawing.Point(186, 100);
            this.dgvRecord.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dgvRecord.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvRecord.MergeColumnNames")));
            this.dgvRecord.MultiSelect = false;
            this.dgvRecord.Name = "dgvRecord";
            this.dgvRecord.ReadOnly = true;
            this.dgvRecord.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dgvRecord.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRecord.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(233)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvRecord.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvRecord.RowTemplate.Height = 23;
            this.dgvRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecord.ShowCheckBox = true;
            this.dgvRecord.Size = new System.Drawing.Size(844, 444);
            this.dgvRecord.TabIndex = 1;
            this.dgvRecord.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvRecord_CellBeginEdit);
            // 
            // column_wh_id
            // 
            this.column_wh_id.HeaderText = "column_wh_id";
            this.column_wh_id.Name = "column_wh_id";
            this.column_wh_id.ReadOnly = true;
            this.column_wh_id.Visible = false;
            // 
            // column_parts_id
            // 
            this.column_parts_id.HeaderText = "column_parts_id";
            this.column_parts_id.Name = "column_parts_id";
            this.column_parts_id.ReadOnly = true;
            this.column_parts_id.Visible = false;
            // 
            // column_wh_code
            // 
            this.column_wh_code.DataPropertyName = "wh_id";
            this.column_wh_code.HeaderText = "仓库编码";
            this.column_wh_code.Name = "column_wh_code";
            this.column_wh_code.ReadOnly = true;
            this.column_wh_code.Width = 150;
            // 
            // column_wh_name
            // 
            this.column_wh_name.DataPropertyName = "wh_name";
            this.column_wh_name.HeaderText = "仓库名称";
            this.column_wh_name.Name = "column_wh_name";
            this.column_wh_name.ReadOnly = true;
            this.column_wh_name.Width = 140;
            // 
            // columnCode
            // 
            this.columnCode.DataPropertyName = "parts_code";
            this.columnCode.HeaderText = "配件编码";
            this.columnCode.Name = "columnCode";
            this.columnCode.ReadOnly = true;
            this.columnCode.Width = 130;
            // 
            // columnName
            // 
            this.columnName.DataPropertyName = "parts_name";
            this.columnName.HeaderText = "配件名称";
            this.columnName.Name = "columnName";
            this.columnName.ReadOnly = true;
            this.columnName.Width = 150;
            // 
            // columnCount
            // 
            this.columnCount.DataPropertyName = "statistic_count";
            this.columnCount.HeaderText = "数量";
            this.columnCount.Name = "columnCount";
            this.columnCount.ReadOnly = true;
            this.columnCount.Visible = false;
            // 
            // columnUnit
            // 
            this.columnUnit.DataPropertyName = "unit_name";
            this.columnUnit.HeaderText = "单位";
            this.columnUnit.Name = "columnUnit";
            this.columnUnit.ReadOnly = true;
            // 
            // columnMin
            // 
            this.columnMin.HeaderText = "库存下限";
            this.columnMin.Name = "columnMin";
            this.columnMin.ReadOnly = true;
            this.columnMin.Width = 82;
            // 
            // columnMax
            // 
            this.columnMax.HeaderText = "库存上限";
            this.columnMax.Name = "columnMax";
            this.columnMax.ReadOnly = true;
            this.columnMax.Width = 82;
            // 
            // column_wh
            // 
            this.column_wh.FalseValue = "0";
            this.column_wh.HeaderText = "默认库存";
            this.column_wh.Name = "column_wh";
            this.column_wh.ReadOnly = true;
            this.column_wh.TrueValue = "1";
            this.column_wh.Visible = false;
            this.column_wh.Width = 82;
            // 
            // columnId
            // 
            this.columnId.HeaderText = "id";
            this.columnId.Name = "columnId";
            this.columnId.ReadOnly = true;
            this.columnId.Visible = false;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // tvStock
            // 
            this.tvStock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tvStock.Font = new System.Drawing.Font("宋体", 10F);
            this.tvStock.IgnoreAutoCheck = false;
            this.tvStock.Location = new System.Drawing.Point(1, 26);
            this.tvStock.Name = "tvStock";
            this.tvStock.Size = new System.Drawing.Size(185, 518);
            this.tvStock.TabIndex = 0;
            this.tvStock.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvStock_AfterSelect);
            // 
            // tbName
            // 
            this.tbName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbName.BackColor = System.Drawing.Color.Transparent;
            this.tbName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbName.ForeImage = null;
            this.tbName.InputtingVerifyCondition = null;
            this.tbName.Location = new System.Drawing.Point(546, 49);
            this.tbName.MaxLengh = 32767;
            this.tbName.Multiline = false;
            this.tbName.Name = "tbName";
            this.tbName.Radius = 3;
            this.tbName.ReadOnly = false;
            this.tbName.SelectStart = 0;
            this.tbName.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbName.ShowError = false;
            this.tbName.Size = new System.Drawing.Size(230, 23);
            this.tbName.TabIndex = 11;
            this.tbName.UseSystemPasswordChar = false;
            this.tbName.Value = "";
            this.tbName.VerifyCondition = null;
            this.tbName.VerifyType = null;
            this.tbName.VerifyTypeName = null;
            this.tbName.WaterMark = null;
            this.tbName.WaterMarkColor = System.Drawing.Color.Silver;

            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(477, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "配件名称：";
            // 
            // tbCode
            // 
            this.tbCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbCode.BackColor = System.Drawing.Color.Transparent;
            this.tbCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.tbCode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCode.ForeImage = null;
            this.tbCode.InputtingVerifyCondition = null;
            this.tbCode.Location = new System.Drawing.Point(273, 49);
            this.tbCode.MaxLengh = 32767;
            this.tbCode.Multiline = false;
            this.tbCode.Name = "tbCode";
            this.tbCode.Radius = 3;
            this.tbCode.ReadOnly = false;
            this.tbCode.SelectStart = 0;
            this.tbCode.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            this.tbCode.ShowError = false;
            this.tbCode.Size = new System.Drawing.Size(166, 23);
            this.tbCode.TabIndex = 9;
            this.tbCode.UseSystemPasswordChar = false;
            this.tbCode.Value = "";
            this.tbCode.VerifyCondition = null;
            this.tbCode.VerifyType = null;
            this.tbCode.VerifyTypeName = null;
            this.tbCode.WaterMark = null;
            this.tbCode.WaterMarkColor = System.Drawing.Color.Silver;

            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(208, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "配件编码：";
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Caption = "清除";
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.Location = new System.Drawing.Point(941, 48);
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(70, 26);
            this.btnClear.TabIndex = 18;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.Caption = "查询";
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnQuery.DownImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.DownImage")));
            this.btnQuery.Location = new System.Drawing.Point(850, 48);
            this.btnQuery.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.MoveImage")));
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.NormalImage")));
            this.btnQuery.Size = new System.Drawing.Size(70, 26);
            this.btnQuery.TabIndex = 19;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // UCStockManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tvStock);
            this.Controls.Add(this.dgvRecord);
            this.Name = "UCStockManager";
            this.Load += new System.EventHandler(this.UCStockManager_Load);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.dgvRecord, 0);
            this.Controls.SetChildIndex(this.tvStock, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.tbCode, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.tbName, 0);
            this.Controls.SetChildIndex(this.btnQuery, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ServiceStationClient.ComponentUI.DataGridViewEx dgvRecord;
        private ServiceStationClient.ComponentUI.TreeViewEx tvStock;
        private ServiceStationClient.ComponentUI.TextBoxEx tbName;
        private System.Windows.Forms.Label label4;
        private ServiceStationClient.ComponentUI.TextBoxEx tbCode;
        private System.Windows.Forms.Label label1;
        private ServiceStationClient.ComponentUI.ButtonEx btnClear;
        private ServiceStationClient.ComponentUI.ButtonEx btnQuery;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_wh_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_parts_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_wh_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_wh_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnMax;
        private System.Windows.Forms.DataGridViewCheckBoxColumn column_wh;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}
