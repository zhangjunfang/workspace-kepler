using System.Windows.Forms;
namespace ServiceStationClient.ComponentUI
{
    partial class TimeInterval
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
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.labelBase35 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dtEnd
            // 
            this.dtEnd.CalendarFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtEnd.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dtEnd.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(200)))), ((int)(((byte)(240)))));
            this.dtEnd.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText;
            this.dtEnd.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
            this.dtEnd.CustomFormat = "HH:mm";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Location = new System.Drawing.Point(76, 3);
            this.dtEnd.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dtEnd.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.ShowUpDown = true;
            this.dtEnd.Size = new System.Drawing.Size(56, 21);
            this.dtEnd.TabIndex = 25;
            // 
            // dtStart
            // 
            this.dtStart.CalendarFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtStart.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dtStart.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(200)))), ((int)(((byte)(240)))));
            this.dtStart.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText;
            this.dtStart.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
            this.dtStart.CustomFormat = "HH:mm";
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.Location = new System.Drawing.Point(3, 3);
            this.dtStart.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dtStart.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtStart.Name = "dtStart";
            this.dtStart.ShowUpDown = true;
            this.dtStart.Size = new System.Drawing.Size(56, 21);
            this.dtStart.TabIndex = 24;
            // 
            // labelBase35
            // 
            this.labelBase35.AutoSize = true;
            this.labelBase35.Location = new System.Drawing.Point(63, 6);
            this.labelBase35.Name = "labelBase35";
            this.labelBase35.Size = new System.Drawing.Size(11, 12);
            this.labelBase35.TabIndex = 23;
            this.labelBase35.Text = "-";
            // 
            // TimeInterval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.dtStart);
            this.Controls.Add(this.labelBase35);
            this.Name = "TimeInterval";
            this.Size = new System.Drawing.Size(132, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelBase35;
        private DateTimePicker dtStart;
        private DateTimePicker dtEnd;
    }
}
