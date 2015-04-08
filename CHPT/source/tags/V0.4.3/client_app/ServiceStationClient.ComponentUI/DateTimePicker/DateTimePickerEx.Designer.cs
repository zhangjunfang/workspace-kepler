namespace ServiceStationClient.ComponentUI
{
    partial class DateTimePickerEx
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
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.pnlDatePicker = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // txtDisplay
            // 
            this.txtDisplay.BackColor = System.Drawing.Color.White;
            this.txtDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDisplay.Location = new System.Drawing.Point(0, 0);
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.ReadOnly = true;
            this.txtDisplay.Size = new System.Drawing.Size(188, 21);
            this.txtDisplay.TabIndex = 0;
            this.txtDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtDisplay_MouseDown);
            // 
            // pnlDatePicker
            // 
            this.pnlDatePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDatePicker.BackColor = System.Drawing.Color.White;
            this.pnlDatePicker.BackgroundImage = global::ServiceStationClient.ComponentUI.Properties.Resources.timespan;
            this.pnlDatePicker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlDatePicker.Location = new System.Drawing.Point(169, 3);
            this.pnlDatePicker.Name = "pnlDatePicker";
            this.pnlDatePicker.Size = new System.Drawing.Size(16, 16);
            this.pnlDatePicker.TabIndex = 2;
            this.pnlDatePicker.Click += new System.EventHandler(this.pnlDatePicker_Click);
            // 
            // DateTimePickerEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlDatePicker);
            this.Controls.Add(this.txtDisplay);
            this.Name = "DateTimePickerEx";
            this.Size = new System.Drawing.Size(188, 21);
            this.Load += new System.EventHandler(this.DateTimePickerEx_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.Panel pnlDatePicker;
    }
}
