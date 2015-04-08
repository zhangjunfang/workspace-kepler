namespace ServiceStationClient.ComponentUI
{
    partial class UCDataSelector_sms
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
            this.txtHour = new System.Windows.Forms.TextBox();
            this.txtMinute = new System.Windows.Forms.TextBox();
            this.txtSecond = new System.Windows.Forms.TextBox();
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.flowPnlContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.flowPnlContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtHour
            // 
            this.txtHour.BackColor = System.Drawing.Color.White;
            this.txtHour.Location = new System.Drawing.Point(32, -1);
            this.txtHour.MaxLength = 2;
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(31, 21);
            this.txtHour.TabIndex = 1;
            this.txtHour.Text = "0";
            this.txtHour.Click += new System.EventHandler(this.txtTime_Click);
            this.txtHour.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHour_KeyPress);
            this.txtHour.Validating += new System.ComponentModel.CancelEventHandler(this.txtHour_Validating);
            // 
            // txtMinute
            // 
            this.txtMinute.BackColor = System.Drawing.Color.White;
            this.txtMinute.Location = new System.Drawing.Point(63, -1);
            this.txtMinute.MaxLength = 2;
            this.txtMinute.Name = "txtMinute";
            this.txtMinute.Size = new System.Drawing.Size(31, 21);
            this.txtMinute.TabIndex = 2;
            this.txtMinute.Text = "0";
            this.txtMinute.Click += new System.EventHandler(this.txtTime_Click);
            this.txtMinute.Validating += new System.ComponentModel.CancelEventHandler(this.txtTime_Validating);
            // 
            // txtSecond
            // 
            this.txtSecond.BackColor = System.Drawing.Color.White;
            this.txtSecond.Location = new System.Drawing.Point(94, -1);
            this.txtSecond.MaxLength = 2;
            this.txtSecond.Name = "txtSecond";
            this.txtSecond.Size = new System.Drawing.Size(31, 21);
            this.txtSecond.TabIndex = 3;
            this.txtSecond.Text = "0";
            this.txtSecond.Click += new System.EventHandler(this.txtTime_Click);
            this.txtSecond.Validating += new System.ComponentModel.CancelEventHandler(this.txtTime_Validating);
            // 
            // monthCalendar
            // 
            this.monthCalendar.Location = new System.Drawing.Point(0, 0);
            this.monthCalendar.Margin = new System.Windows.Forms.Padding(0);
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.TabIndex = 4;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(125, -1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(42, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(1, 2);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(29, 12);
            this.lblTime.TabIndex = 6;
            this.lblTime.Text = "时间";
            // 
            // flowPnlContainer
            // 
            this.flowPnlContainer.Controls.Add(this.monthCalendar);
            this.flowPnlContainer.Controls.Add(this.panel1);
            this.flowPnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPnlContainer.Location = new System.Drawing.Point(0, 0);
            this.flowPnlContainer.Name = "flowPnlContainer";
            this.flowPnlContainer.Size = new System.Drawing.Size(221, 211);
            this.flowPnlContainer.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.lblTime);
            this.panel1.Controls.Add(this.txtHour);
            this.panel1.Controls.Add(this.txtMinute);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.txtSecond);
            this.panel1.Location = new System.Drawing.Point(0, 183);
            this.panel1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(221, 25);
            this.panel1.TabIndex = 8;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(170, -1);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(42, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // UCDataSelector_sms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowPnlContainer);
            this.Name = "UCDataSelector_sms";
            this.Size = new System.Drawing.Size(221, 211);
            this.flowPnlContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtHour;
        private System.Windows.Forms.TextBox txtMinute;
        private System.Windows.Forms.TextBox txtSecond;
        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.FlowLayoutPanel flowPnlContainer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClear;
    }
}
