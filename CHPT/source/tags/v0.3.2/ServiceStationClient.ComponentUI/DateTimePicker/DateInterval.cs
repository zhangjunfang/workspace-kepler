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
    public partial class DateInterval : UserControl
    {
        public DateInterval()
        {
            InitializeComponent();
        }

        [Browsable(true),
        DefaultValue("日期："),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return lblText.Text;
            }
            set
            {
                lblText.Text = value;
            }
        }

        [Browsable(true),
        Description("日期格式"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("自定义")]
        public string ShowFormat
        {
            get
            {
                return dtpStartDate.ShowFormat;
            }
            set
            {
                dtpStartDate.ShowFormat = value;
                dtpEndDate.ShowFormat = value;
            }
        }
        /// <summary>
        /// 清空
        /// </summary>
        public void Empty()
        {
            dtpStartDate.Value = string.Empty;
            dtpEndDate.Value = string.Empty;
            cobCustom.SelectedIndex = 0;
        }

        /// 加载
        private void DateInterval_Load(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Now.ToString("yyyy-MM-01");
            dtpEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            cobCustom.SelectedIndex = 0;
        }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string StartDate
        {
            get { return dtpStartDate.Value; }
            set { dtpStartDate.Value = value; }
        }

        /// <summary>
        /// 结算日期
        /// </summary>
        public string EndDate
        {
            get { return dtpEndDate.Value; }
            set { dtpEndDate.Value = value; }
        }

        private void cobCustom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobCustom.Text == null)
            {
                return;
            }
            string custom = cobCustom.Text.Trim();
            switch (custom)
            {
                case "今天":
                    dtpStartDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                    dtpEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                    break;
                case "昨天":
                    dtpStartDate.Value = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    dtpEndDate.Value = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    break;
                case "本周":
                    dtpStartDate.Value = DateTime.Now.AddDays(-((int)DateTime.Now.DayOfWeek) + 1).ToString("yyyy-MM-dd");
                    dtpEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                    break;
                case "上周":
                    dtpStartDate.Value = DateTime.Now.AddDays(-7 - ((int)DateTime.Now.DayOfWeek) + 1).ToString("yyyy-MM-dd");
                    dtpEndDate.Value = Convert.ToDateTime(dtpStartDate.Value).AddDays(6).ToString();
                    break;
                case "本月":
                    dtpStartDate.Value = DateTime.Now.ToString("yyyy-MM-01");
                    dtpEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                    break;
                case "上月":
                    dtpStartDate.Value = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01");
                    dtpEndDate.Value = Convert.ToDateTime(dtpStartDate.Value).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
                    break;
                case "本年":
                    dtpStartDate.Value = DateTime.Now.ToString("yyyy-01-01");
                    dtpEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                    break;
            }
        }

        //修改开始时间，如果开始时间大于结束时间，让开始时间和结束时间相同
        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            //this.StartDate = dtpStartDate.Value;
            if (string.IsNullOrEmpty(dtpStartDate.Value) || string.IsNullOrEmpty(dtpEndDate.Value))
            {
                return;
            }
            if (Convert.ToDateTime(dtpStartDate.Value) > Convert.ToDateTime(dtpEndDate.Value))
            {
                dtpEndDate.Value = this.StartDate;
            }
        }

        //修改开始时间，如果开始时间大于结束时间，让开始时间和结束时间相同
        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            //this.EndDate = dtpEndDate.Value;
            if (string.IsNullOrEmpty(dtpStartDate.Value) || string.IsNullOrEmpty(dtpEndDate.Value))
            {
                return;
            }
            if (Convert.ToDateTime(dtpStartDate.Value) > Convert.ToDateTime(dtpEndDate.Value))
            {
                dtpStartDate.Value = this.EndDate;
            }
        }

    }
}
