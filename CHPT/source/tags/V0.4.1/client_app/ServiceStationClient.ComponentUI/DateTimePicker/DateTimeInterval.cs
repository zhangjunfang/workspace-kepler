using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace ServiceStationClient.ComponentUI
{
    public partial class DateTimeInterval : UserControl
    {
        public DateTimeInterval()
        {
            InitializeComponent();
        }

        public delegate void TimeSpanChanged();
        [Description("时间范围改变"), Category("自定义事件")]
        public event TimeSpanChanged eventTimeSpanChanged;

        #region 自定义属性
        DateTime startDate = DateTime.Now;
        /// <summary>
        /// 开始日期
        /// </summary>
        [Description("开始日期"), Category("自定义属性")]
        public DateTime StartDate
        {
            get
            {
                string temp = this.dtpStartDate.Value.ToString(customFormat);
                temp = temp + "2013-01-01 00:00:00".Substring(temp.Length);
                return Convert.ToDateTime(temp);
                //return Convert.ToDateTime(this.dtpStartDate.Value.ToString(customFormat));
            }
            set
            {
                dtpStartDate.Value = value;
            }
        }

        DateTime endDate = DateTime.Now;

        /// <summary>
        /// 结束日期
        /// </summary>
        [Description("结束日期"), Category("自定义属性")]
        public DateTime EndDate
        {
            get
            {
                string temp = this.dtpEndDate.Value.ToString(customFormat);
                temp = temp + "2013-12-31 23:59:59".Substring(temp.Length);
                return Convert.ToDateTime(temp);
                //return Convert.ToDateTime(this.dtpEndDate.Value.ToString(customFormat));
            }
            set
            {
                dtpEndDate.Value = value;
            }
        }

        [Description("日期格式"), Category("自定义属性")]
        public string customFormat { get; set; }

        public DateTime SetStartDate
        {
            set { this.dtpStartDate.Value = value; }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 开始日期
        /// </summary>
        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            this.StartDate = dtpStartDate.Value;
            if (dtpStartDate.Value > dtpEndDate.Value)
            {
                dtpEndDate.Value = this.StartDate;
            }
            if (eventTimeSpanChanged != null)
            {
                eventTimeSpanChanged();
            }

        }

        /// <summary>
        /// 结束日期
        /// </summary>
        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            this.EndDate = dtpEndDate.Value;
            if (dtpStartDate.Value > dtpEndDate.Value)
            {
                dtpStartDate.Value = this.EndDate;
            }
            if (eventTimeSpanChanged != null)
            {
                eventTimeSpanChanged();
            }
        }

        private void TimeSpan_Paint(object sender, PaintEventArgs e)
        {
            if (string.IsNullOrEmpty(this.customFormat))
            {
                this.customFormat = "yyyy-MM-dd";
            }
            dtpStartDate.ShowFormat = this.customFormat;
            dtpEndDate.ShowFormat = this.customFormat;
        }
        #endregion
    }
}
