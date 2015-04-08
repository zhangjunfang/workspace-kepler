namespace HXCPcClient.UCForm.SysManage.ReminderSet
{
    partial class UCPreTime
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
            this.tbDay = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbHour = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSecond = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMinute = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbDay
            // 
            this.tbDay.Location = new System.Drawing.Point(10, 6);
            this.tbDay.Name = "tbDay";
            this.tbDay.Size = new System.Drawing.Size(30, 21);
            this.tbDay.TabIndex = 0;
            this.tbDay.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "天";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(103, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "时";
            // 
            // tbHour
            // 
            this.tbHour.Location = new System.Drawing.Point(68, 7);
            this.tbHour.Name = "tbHour";
            this.tbHour.Size = new System.Drawing.Size(30, 21);
            this.tbHour.TabIndex = 2;
            this.tbHour.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "秒";
            // 
            // tbSecond
            // 
            this.tbSecond.Location = new System.Drawing.Point(185, 7);
            this.tbSecond.Name = "tbSecond";
            this.tbSecond.Size = new System.Drawing.Size(30, 21);
            this.tbSecond.TabIndex = 6;
            this.tbSecond.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(162, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "分";
            // 
            // tbMinute
            // 
            this.tbMinute.Location = new System.Drawing.Point(127, 6);
            this.tbMinute.Name = "tbMinute";
            this.tbMinute.Size = new System.Drawing.Size(30, 21);
            this.tbMinute.TabIndex = 4;
            this.tbMinute.Text = "0";
            // 
            // UCPreTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbSecond);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbMinute);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbHour);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbDay);
            this.Name = "UCPreTime";
            this.Size = new System.Drawing.Size(245, 33);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbDay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbHour;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSecond;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMinute;
    }
}
