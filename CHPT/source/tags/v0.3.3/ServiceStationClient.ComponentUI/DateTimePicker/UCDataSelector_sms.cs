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
    public partial class UCDataSelector_sms : UserControl
    {
        private PopupControlHost popUpControl;
        private UCTimeSelector ucTimeSelector;
        private System.Windows.Forms.TextBox textBoxCurrent;
        public event DateRangeEventHandler OnDateSelected;
        public event DateRangeEventHandler OnDateClear;

        private int[] hours = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
        private int[] minutes = new int[] { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55 };
        private int[] seconds = new int[] { 0, 10, 20, 30, 40, 50};

        public UCDataSelector_sms()
        {
            InitializeComponent();
            ucTimeSelector = new UCTimeSelector();
            ucTimeSelector.OnTimeValueChanged += new DataGridViewCellEventHandler(ucTimeSelector_OnTimeValueChanged);
            popUpControl = new PopupControlHost(ucTimeSelector);

            OperatingSystem os = Environment.OSVersion;
            if (os.Version.Major < 6) 
            {
                this.Width = 270;
            }
          
        }
        //是否显示time部分  221,181
        private bool showTime = false;
        public bool ShowTime
        {
            get
            {
                return showTime;
            }
            set
            {
                showTime = value;
            }
        }

        //时间变量
        private DateTime dateTimeValue = DateTime.Now;
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime DateTimeValue
        {
            get
            {
                return dateTimeValue;
            }

            set
            {
                dateTimeValue = value;
                 ValueChange();
            }
        }
        private bool valueIsNull = true;
        /// <summary>
        /// 值是否可为空 true可为空 
        /// </summary>
        public bool ValueIsNull
        {
            get
            {
                return valueIsNull;
            }
            set
            {
                valueIsNull = value;
            }
        }
        private void ValueChange()
        {
            if (!showTime)//不显示时间部分
            {
                if (valueIsNull)//可为空
                {
                    lblTime.Visible = false;
                    txtHour.Visible = false;
                    txtMinute.Visible = false;
                    txtSecond.Visible = false;
                    btnClear.Visible = true;
                    btnOK.Visible = true;
                    this.Height = 211;
                }
                else
                {
                    this.Height = 181;
                }
            }
            else//显示时间部分
            {
                if (!valueIsNull)//不可为空
                {
                    lblTime.Visible = true;
                    txtHour.Visible = true;
                    txtMinute.Visible = true;
                    txtSecond.Visible = true;
                    btnOK.Visible = true;
                    btnClear.Visible = false;
                }
                else
                {
                    lblTime.Visible = true;
                    txtHour.Visible = true;
                    txtMinute.Visible = true;
                    txtSecond.Visible = true;
                    btnOK.Visible = true;
                    btnClear.Visible = true;
                }
                this.Height = 211;
            }

            monthCalendar.DateSelected-=new DateRangeEventHandler(monthCalendar_DateSelected);
            monthCalendar.SelectionStart = dateTimeValue;
            monthCalendar.SelectionEnd = dateTimeValue;
            
            txtHour.Text = dateTimeValue.ToString("HH");
            txtMinute.Text = dateTimeValue.ToString("mm");
            txtSecond.Text = dateTimeValue.ToString("ss");
            monthCalendar.Select();
            monthCalendar.DateSelected += new DateRangeEventHandler(monthCalendar_DateSelected);
        }

        void ucTimeSelector_OnTimeValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            textBoxCurrent.Text = ucTimeSelector.SelectedValue.ToString();
            popUpControl.Hide();
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            int hour = 0;
            int minute = 0;
            int second = 0;
            int.TryParse(txtHour.Text, out hour);
            int.TryParse(txtMinute.Text, out minute);
            int.TryParse(txtSecond.Text, out second);
            dateTimeValue = new DateTime(monthCalendar.SelectionStart.Year,
                monthCalendar.SelectionStart.Month,
                monthCalendar.SelectionStart.Day,
                hour,
                minute,
                second);
            DateRangeEventArgs de = new DateRangeEventArgs(dateTimeValue, dateTimeValue);
            OnDateSelected(sender, de);
        }

        private void txtTime_Click(object sender, EventArgs e)
        {
            textBoxCurrent = sender as System.Windows.Forms.TextBox;

            if (textBoxCurrent.Equals(txtHour))
            {
                ucTimeSelector.Items = hours;

            }
            else if (textBoxCurrent.Equals(txtMinute))
            {
                ucTimeSelector.Items = minutes;
            }
            else if (textBoxCurrent.Equals(txtSecond))
            {
                ucTimeSelector.Items = seconds;
            }
            else
            {
                return;
            }
            popUpControl.Show(textBoxCurrent, false);
            textBoxCurrent.Focus();
            if (!string.IsNullOrEmpty(textBoxCurrent.Text))
            {
                ucTimeSelector.SelectedValue = int.Parse(textBoxCurrent.Text);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int hour = 0;
            int minute = 0;
            int second = 0;
            int.TryParse(txtHour.Text,out hour);
            int.TryParse(txtMinute.Text, out minute);
            int.TryParse(txtSecond.Text, out second);
            dateTimeValue = new DateTime(monthCalendar.SelectionStart.Year,
                monthCalendar.SelectionStart.Month,
                monthCalendar.SelectionStart.Day,
                hour,
                minute,
                second);
            DateRangeEventArgs de = new DateRangeEventArgs(dateTimeValue, dateTimeValue);
            OnDateSelected(sender, de);
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            OnDateClear(sender, null);
        }
        private void txtHour_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
        }

        protected override void WndProc(ref Message m)
        {
            Console.WriteLine(m.Msg);
            base.WndProc(ref m);
        }

        private void txtHour_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                int hour=int.Parse(txtHour.Text);
                if (hour > 23)
                {
                    txtHour.Text = "23";
                }
                else if (hour <0)
                {
                    txtHour.Text = "0";
                }
            }
            catch 
            {
                txtHour.Focus();
            }
        }

        private void txtTime_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;
            try
            {
                
                int value = int.Parse(txtHour.Text);
                if (value > 59)
                {
                    textBox.Text = "59";
                }
                else if (value < 0)
                {
                    textBox.Text = "0";
                }
            }
            catch
            {
                textBox.Focus();
            }
        }

       
    }
}
