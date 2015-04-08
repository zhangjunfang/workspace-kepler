namespace ServiceStationClient.ComponentUI
{
    partial class DateInterval
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
            this.lblText = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cobCustom = new ServiceStationClient.ComponentUI.ComboBoxEx(this.components);
            this.dtpStartDate = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEndDate = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(121, 7);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(41, 12);
            this.lblText.TabIndex = 1;
            this.lblText.Text = "日期：";
            this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.cobCustom, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblText, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpStartDate, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpEndDate, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(407, 27);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // cobCustom
            // 
            this.cobCustom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cobCustom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobCustom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobCustom.FormattingEnabled = true;
            this.cobCustom.Items.AddRange(new object[] {
            "自定义",
            "今天",
            "昨天",
            "本周",
            "上周",
            "本月",
            "上月",
            "本年"});
            this.cobCustom.Location = new System.Drawing.Point(3, 3);
            this.cobCustom.Name = "cobCustom";
            this.cobCustom.Size = new System.Drawing.Size(112, 22);
            this.cobCustom.TabIndex = 0;
            this.cobCustom.SelectedIndexChanged += new System.EventHandler(this.cobCustom_SelectedIndexChanged);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartDate.Location = new System.Drawing.Point(168, 3);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.ShowFormat = "yyyy-MM-dd";
            this.dtpStartDate.Size = new System.Drawing.Size(94, 21);
            this.dtpStartDate.TabIndex = 2;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(268, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "至：";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEndDate.Location = new System.Drawing.Point(303, 3);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.ShowFormat = "yyyy-MM-dd";
            this.dtpEndDate.Size = new System.Drawing.Size(101, 21);
            this.dtpEndDate.TabIndex = 4;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // DateInterval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DateInterval";
            this.Size = new System.Drawing.Size(407, 27);
            this.Load += new System.EventHandler(this.DateInterval_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBoxEx cobCustom;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DateTimePickerEx_sms dtpStartDate;
        private System.Windows.Forms.Label label1;
        private DateTimePickerEx_sms dtpEndDate;
    }
}
