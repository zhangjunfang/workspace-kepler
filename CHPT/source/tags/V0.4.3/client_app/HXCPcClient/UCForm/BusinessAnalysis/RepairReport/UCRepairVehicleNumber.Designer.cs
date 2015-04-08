namespace HXCPcClient.UCForm.BusinessAnalysis.RepairReport
{
    partial class UCRepairVehicleNumber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCRepairVehicleNumber));
            this.rbDay = new System.Windows.Forms.RadioButton();
            this.rbMonth = new System.Windows.Forms.RadioButton();
            this.rbYear = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.dateInterval1 = new ServiceStationClient.ComponentUI.DateInterval();
            this.cboCompany = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.rbDay);
            this.pnlSearch.Controls.Add(this.rbMonth);
            this.pnlSearch.Controls.Add(this.rbYear);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.dateInterval1);
            this.pnlSearch.Controls.Add(this.cboCompany);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.SetChildIndex(this.btnClear, 0);
            this.pnlSearch.Controls.SetChildIndex(this.btnSearch, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label1, 0);
            this.pnlSearch.Controls.SetChildIndex(this.cboCompany, 0);
            this.pnlSearch.Controls.SetChildIndex(this.dateInterval1, 0);
            this.pnlSearch.Controls.SetChildIndex(this.label2, 0);
            this.pnlSearch.Controls.SetChildIndex(this.rbYear, 0);
            this.pnlSearch.Controls.SetChildIndex(this.rbMonth, 0);
            this.pnlSearch.Controls.SetChildIndex(this.rbDay, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.Caption = "";
            this.btnSearch.DownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.DownImage")));
            this.btnSearch.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MoveImage")));
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.Size = new System.Drawing.Size(80, 24);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.Caption = "";
            this.btnClear.DownImage = ((System.Drawing.Image)(resources.GetObject("btnClear.DownImage")));
            this.btnClear.MoveImage = ((System.Drawing.Image)(resources.GetObject("btnClear.MoveImage")));
            this.btnClear.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnClear.NormalImage")));
            this.btnClear.Size = new System.Drawing.Size(80, 24);
            // 
            // rbDay
            // 
            this.rbDay.AutoSize = true;
            this.rbDay.Location = new System.Drawing.Point(399, 50);
            this.rbDay.Name = "rbDay";
            this.rbDay.Size = new System.Drawing.Size(35, 16);
            this.rbDay.TabIndex = 20;
            this.rbDay.TabStop = true;
            this.rbDay.Text = "日";
            this.rbDay.UseVisualStyleBackColor = true;
            // 
            // rbMonth
            // 
            this.rbMonth.AutoSize = true;
            this.rbMonth.Location = new System.Drawing.Point(358, 50);
            this.rbMonth.Name = "rbMonth";
            this.rbMonth.Size = new System.Drawing.Size(35, 16);
            this.rbMonth.TabIndex = 19;
            this.rbMonth.TabStop = true;
            this.rbMonth.Text = "月";
            this.rbMonth.UseVisualStyleBackColor = true;
            // 
            // rbYear
            // 
            this.rbYear.AutoSize = true;
            this.rbYear.Location = new System.Drawing.Point(317, 50);
            this.rbYear.Name = "rbYear";
            this.rbYear.Size = new System.Drawing.Size(35, 16);
            this.rbYear.TabIndex = 18;
            this.rbYear.TabStop = true;
            this.rbYear.Text = "年";
            this.rbYear.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(246, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "汇总方式：";
            // 
            // dateInterval1
            // 
            this.dateInterval1.BackColor = System.Drawing.Color.Transparent;
            this.dateInterval1.EndDate = "2015-01-09";
            this.dateInterval1.Location = new System.Drawing.Point(473, 45);
            this.dateInterval1.Name = "dateInterval1";
            this.dateInterval1.ShowFormat = "yyyy-MM-dd";
            this.dateInterval1.Size = new System.Drawing.Size(414, 27);
            this.dateInterval1.StartDate = "2015-01-01";
            this.dateInterval1.TabIndex = 16;
            this.dateInterval1.Text = "dateInterval1";
            // 
            // cboCompany
            // 
            this.cboCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(90, 47);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(121, 22);
            this.cboCompany.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "公司：";
            // 
            // UCRepairVehicleNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCRepairVehicleNumber";
            this.Load += new System.EventHandler(this.UCRepairVehicleNumber_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbDay;
        private System.Windows.Forms.RadioButton rbMonth;
        private System.Windows.Forms.RadioButton rbYear;
        private System.Windows.Forms.Label label2;
        private ServiceStationClient.ComponentUI.DateInterval dateInterval1;
        private ServiceStationClient.ComponentUI.ComboBoxEx cboCompany;
        private System.Windows.Forms.Label label1;
    }
}
