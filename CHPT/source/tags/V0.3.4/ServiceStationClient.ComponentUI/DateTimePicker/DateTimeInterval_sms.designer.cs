using System.Windows.Forms;
namespace ServiceStationClient.ComponentUI
{
    partial class DateTimeInterval_sms
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
            this.tableLayoutPanelBase1 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpStartDate = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.dtpEndDate = new ServiceStationClient.ComponentUI.DateTimePickerEx_sms();
            this.tableLayoutPanelBase1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelBase1
            // 
            this.tableLayoutPanelBase1.ColumnCount = 3;
            this.tableLayoutPanelBase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelBase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelBase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelBase1.Controls.Add(this.label10, 1, 0);
            this.tableLayoutPanelBase1.Controls.Add(this.dtpStartDate, 0, 0);
            this.tableLayoutPanelBase1.Controls.Add(this.dtpEndDate, 2, 0);
            this.tableLayoutPanelBase1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelBase1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelBase1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelBase1.Name = "tableLayoutPanelBase1";
            this.tableLayoutPanelBase1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tableLayoutPanelBase1.RowCount = 1;
            this.tableLayoutPanelBase1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanelBase1.Size = new System.Drawing.Size(263, 27);
            this.tableLayoutPanelBase1.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(124, 7);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 44;
            this.label10.Text = "至";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartDate.Location = new System.Drawing.Point(3, 3);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.ShowFormat = "yyyy-MM-dd";
            this.dtpStartDate.Size = new System.Drawing.Size(115, 21);
            this.dtpStartDate.TabIndex = 47;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEndDate.Location = new System.Drawing.Point(144, 3);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.ShowFormat = "yyyy-MM-dd";
            this.dtpEndDate.Size = new System.Drawing.Size(116, 21);
            this.dtpEndDate.TabIndex = 48;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // DateTimeInterval_sms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanelBase1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DateTimeInterval_sms";
            this.Size = new System.Drawing.Size(263, 27);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TimeSpan_Paint);
            this.tableLayoutPanelBase1.ResumeLayout(false);
            this.tableLayoutPanelBase1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanelBase1;
        private Label label10;
        private DateTimePickerEx_sms dtpStartDate;
        private DateTimePickerEx_sms dtpEndDate;

    }
}
