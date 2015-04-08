using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HXCPcClient.UCForm.SysManage.ReminderSet
{
    public partial class UCPreTime : UserControl
    {
        public UCPreTime()
        {
            InitializeComponent();
        }

        public void SetTime(int time)
        {
            int day = time / (24 * 60 * 60);
            if (day > 0)
            {
                time = time % 24 * 60 * 60;
            }
            int hour = time / (60 * 60);
            if (hour > 0)
            {
                time = time % 60;
            }
            int minute = time / 60;
            if (minute > 0)
            {
                time = time % 60;
            }
            int second = time;

            this.tbDay.Text = day.ToString();
            this.tbHour.Text = hour.ToString();
            this.tbMinute.Text = minute.ToString();
            this.tbSecond.Text = second.ToString();
        }

        public int GetTime()
        {
            int day = 0;
            if (this.tbDay.Text.Length > 0)
            {
                if (!int.TryParse(this.tbDay.Text, out day))
                {
                    day = 0;
                }
            }

            int hour = 0;
            if (this.tbHour.Text.Length > 0)
            {
                if (!int.TryParse(this.tbHour.Text, out hour))
                {
                    hour = 0;
                }
            }

            int minute = 0;
            if (this.tbMinute.Text.Length > 0)
            {
                if (!int.TryParse(this.tbMinute.Text, out minute))
                {
                    minute = 0;
                }
            }

            int second = 0;
            if (this.tbMinute.Text.Length > 0)
            {
                if (!int.TryParse(this.tbSecond.Text, out second))
                {
                    second = 0;
                }
            }

            return day * 24 * 60 * 60 + hour * 60 * 60 + minute * 60 + second;
        }
    }
}
