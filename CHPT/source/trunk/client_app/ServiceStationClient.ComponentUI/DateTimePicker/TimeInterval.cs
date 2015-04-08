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
    public partial class TimeInterval : UserControl
    {
        public TimeInterval()
        {
            InitializeComponent();
        }

        #region 属性
        public string StartTime
        {
            get { return this.dtStart.Value.ToString("HH:MM"); }
            set
            {
                if (string.IsNullOrEmpty(value)) this.dtStart.Value = DateTime.Now;
                else this.dtStart.Value = Convert.ToDateTime(value);
            }
        }
        public string EndTime
        {
            get { return this.dtEnd.Value.ToString("HH:MM"); }
            set
            {
                if (string.IsNullOrEmpty(value)) this.dtEnd.Value = DateTime.Now;
                else this.dtEnd.Value = Convert.ToDateTime(value);
            }
        }
        #endregion     
    }
}
