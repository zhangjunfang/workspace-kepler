namespace HXCPcClient.UCForm.SysManage.AcountManage
{
    partial class UCLoginTimeLimit
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
            this.rbByWeek = new System.Windows.Forms.RadioButton();
            this.rbByTime = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSunday = new System.Windows.Forms.CheckBox();
            this.cbSaturday = new System.Windows.Forms.CheckBox();
            this.cbFriday = new System.Windows.Forms.CheckBox();
            this.cbThursday = new System.Windows.Forms.CheckBox();
            this.cbWednesday = new System.Windows.Forms.CheckBox();
            this.cbTuesday = new System.Windows.Forms.CheckBox();
            this.cbMonday = new System.Windows.Forms.CheckBox();
            this.pnlByWeek = new ServiceStationClient.ComponentUI.PanelEx();
            this.dtpStart = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.dtpEnd = new ServiceStationClient.ComponentUI.DateTimePickerEx();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlByWeek.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOpt
            // 
            this.pnlOpt.Size = new System.Drawing.Size(1000, 28);
            // 
            // rbByWeek
            // 
            this.rbByWeek.AutoSize = true;
            this.rbByWeek.Location = new System.Drawing.Point(148, 255);
            this.rbByWeek.Name = "rbByWeek";
            this.rbByWeek.Size = new System.Drawing.Size(59, 16);
            this.rbByWeek.TabIndex = 4;
            this.rbByWeek.TabStop = true;
            this.rbByWeek.Text = "按星期";
            this.rbByWeek.UseVisualStyleBackColor = true;
            // 
            // rbByTime
            // 
            this.rbByTime.AutoSize = true;
            this.rbByTime.Checked = true;
            this.rbByTime.Location = new System.Drawing.Point(148, 198);
            this.rbByTime.Name = "rbByTime";
            this.rbByTime.Size = new System.Drawing.Size(83, 16);
            this.rbByTime.TabIndex = 5;
            this.rbByTime.TabStop = true;
            this.rbByTime.Text = "按时间周期";
            this.rbByTime.UseVisualStyleBackColor = true;
            this.rbByTime.CheckedChanged += new System.EventHandler(this.rbByTime_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(112, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "禁止登陆时间:      ";
            // 
            // cbSunday
            // 
            this.cbSunday.AutoSize = true;
            this.cbSunday.Location = new System.Drawing.Point(344, 12);
            this.cbSunday.Name = "cbSunday";
            this.cbSunday.Size = new System.Drawing.Size(48, 16);
            this.cbSunday.TabIndex = 35;
            this.cbSunday.Text = "周日";
            this.cbSunday.UseVisualStyleBackColor = true;
            // 
            // cbSaturday
            // 
            this.cbSaturday.AutoSize = true;
            this.cbSaturday.Location = new System.Drawing.Point(290, 12);
            this.cbSaturday.Name = "cbSaturday";
            this.cbSaturday.Size = new System.Drawing.Size(48, 16);
            this.cbSaturday.TabIndex = 34;
            this.cbSaturday.Text = "周六";
            this.cbSaturday.UseVisualStyleBackColor = true;
            // 
            // cbFriday
            // 
            this.cbFriday.AutoSize = true;
            this.cbFriday.Location = new System.Drawing.Point(236, 12);
            this.cbFriday.Name = "cbFriday";
            this.cbFriday.Size = new System.Drawing.Size(48, 16);
            this.cbFriday.TabIndex = 33;
            this.cbFriday.Text = "周五";
            this.cbFriday.UseVisualStyleBackColor = true;
            // 
            // cbThursday
            // 
            this.cbThursday.AutoSize = true;
            this.cbThursday.Location = new System.Drawing.Point(173, 12);
            this.cbThursday.Name = "cbThursday";
            this.cbThursday.Size = new System.Drawing.Size(48, 16);
            this.cbThursday.TabIndex = 32;
            this.cbThursday.Text = "周四";
            this.cbThursday.UseVisualStyleBackColor = true;
            // 
            // cbWednesday
            // 
            this.cbWednesday.AutoSize = true;
            this.cbWednesday.Location = new System.Drawing.Point(119, 12);
            this.cbWednesday.Name = "cbWednesday";
            this.cbWednesday.Size = new System.Drawing.Size(48, 16);
            this.cbWednesday.TabIndex = 31;
            this.cbWednesday.Text = "周三";
            this.cbWednesday.UseVisualStyleBackColor = true;
            // 
            // cbTuesday
            // 
            this.cbTuesday.AutoSize = true;
            this.cbTuesday.Location = new System.Drawing.Point(65, 12);
            this.cbTuesday.Name = "cbTuesday";
            this.cbTuesday.Size = new System.Drawing.Size(48, 16);
            this.cbTuesday.TabIndex = 30;
            this.cbTuesday.Text = "周二";
            this.cbTuesday.UseVisualStyleBackColor = true;
            // 
            // cbMonday
            // 
            this.cbMonday.AutoSize = true;
            this.cbMonday.Location = new System.Drawing.Point(11, 12);
            this.cbMonday.Name = "cbMonday";
            this.cbMonday.Size = new System.Drawing.Size(48, 16);
            this.cbMonday.TabIndex = 29;
            this.cbMonday.Text = "周一";
            this.cbMonday.UseVisualStyleBackColor = true;
            // 
            // pnlByWeek
            // 
            this.pnlByWeek.BackColor = System.Drawing.Color.Transparent;
            this.pnlByWeek.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlByWeek.Controls.Add(this.cbMonday);
            this.pnlByWeek.Controls.Add(this.cbTuesday);
            this.pnlByWeek.Controls.Add(this.cbWednesday);
            this.pnlByWeek.Controls.Add(this.cbThursday);
            this.pnlByWeek.Controls.Add(this.cbSunday);
            this.pnlByWeek.Controls.Add(this.cbFriday);
            this.pnlByWeek.Controls.Add(this.cbSaturday);
            this.pnlByWeek.Location = new System.Drawing.Point(282, 244);
            this.pnlByWeek.Name = "pnlByWeek";
            this.pnlByWeek.Size = new System.Drawing.Size(440, 81);
            this.pnlByWeek.TabIndex = 38;
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(282, 197);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.ShowFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpStart.Size = new System.Drawing.Size(150, 21);
            this.dtpStart.TabIndex = 39;
            this.dtpStart.Value = new System.DateTime(2014, 12, 19, 16, 24, 34, 53);
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(453, 197);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.ShowFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Size = new System.Drawing.Size(150, 21);
            this.dtpEnd.TabIndex = 40;
            this.dtpEnd.Value = new System.DateTime(2014, 12, 19, 16, 24, 34, 53);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(435, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 41;
            this.label2.Text = "—";
            // 
            // UCLoginTimeLimit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.pnlByWeek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbByWeek);
            this.Controls.Add(this.rbByTime);
            this.Name = "UCLoginTimeLimit";
            this.Size = new System.Drawing.Size(1000, 500);
            this.Load += new System.EventHandler(this.UCLoginTimeLimit_Load);
            this.Controls.SetChildIndex(this.rbByTime, 0);
            this.Controls.SetChildIndex(this.rbByWeek, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.pnlOpt, 0);
            this.Controls.SetChildIndex(this.pnlByWeek, 0);
            this.Controls.SetChildIndex(this.dtpStart, 0);
            this.Controls.SetChildIndex(this.dtpEnd, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.pnlByWeek.ResumeLayout(false);
            this.pnlByWeek.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbByWeek;
        private System.Windows.Forms.RadioButton rbByTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbSunday;
        private System.Windows.Forms.CheckBox cbSaturday;
        private System.Windows.Forms.CheckBox cbFriday;
        private System.Windows.Forms.CheckBox cbThursday;
        private System.Windows.Forms.CheckBox cbWednesday;
        private System.Windows.Forms.CheckBox cbTuesday;
        private System.Windows.Forms.CheckBox cbMonday;
        //private ServiceStationClient.ComponentUI.TimeInterval timeInterval;
        //private ServiceStationClient.ComponentUI.DateTimeInterval dtInterval;
        private ServiceStationClient.ComponentUI.PanelEx pnlByWeek;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpStart;
        private ServiceStationClient.ComponentUI.DateTimePickerEx dtpEnd;
        private System.Windows.Forms.Label label2;

    }
}
