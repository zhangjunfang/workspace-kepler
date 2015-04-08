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
    public partial class DateTimeInterval_sms : UserControl
    {
        public DateTimeInterval_sms()
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
        public string StartDate
        {
            get
            {
                return dtpStartDate.Value;
            }
            set
            {
                dtpStartDate.Value = value;
            }
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        [Description("结束日期"), Category("自定义属性")]
        public string EndDate
        {
            get
            {
                return dtpEndDate.Value;
            }
            set
            {
                dtpEndDate.Value = value;
            }
        }

        [Description("日期格式"), Category("自定义属性")]
        public string customFormat { get; set; }

        #endregion

        #region 事件
        /// <summary>
        /// 开始日期
        /// </summary>
        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dtpStartDate.Value) || string.IsNullOrEmpty(dtpEndDate.Value))
            {
                return;
            }
            if (DateTime.Parse(dtpStartDate.Value) > DateTime.Parse(dtpEndDate.Value))
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
            if (string.IsNullOrEmpty(dtpStartDate.Value) || string.IsNullOrEmpty(dtpEndDate.Value))
            {
                return;
            }
            if (DateTime.Parse(dtpStartDate.Value) > DateTime.Parse(dtpEndDate.Value))
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
            //if (string.IsNullOrEmpty(this.customFormat))
            //{
            //    this.customFormat = "yyyy-MM-dd";
            //}
            //dtpStartDate.ShowFormat = this.customFormat;
            //dtpEndDate.ShowFormat = this.customFormat;
        }
        #endregion
    }
}
