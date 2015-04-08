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
    public partial class DateTimePickerEx : UserControl
    {
        public event EventHandler ValueChanged;

        private PopupControlHost popUpControl;

        //时间格式
        private string strShowFormat = "yyyy-MM-dd HH:mm:ss";
        //时间格式
        public string ShowFormat
        {
            get
            {
                return strShowFormat;
            }

            set
            {
                strShowFormat = value;
                ValueChange();
            }
        }

        public new bool Enabled
        {
            get
            {
                return pnlDatePicker.Enabled;
            }
            set
            {
                pnlDatePicker.Enabled = value;
                txtDisplay.ReadOnly = !value;
            }
        }


        //时间变量
        private DateTime dateTimeValue = DateTime.Now;
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Value
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

        private UCDataSelector ud = null;

        public DateTimePickerEx()
        {
            InitializeComponent();
            dateTimeValue = DateTime.Now;
            ud = new UCDataSelector();

            ud.OnDateSelected += new DateRangeEventHandler(ud_OnDateSelected);
            popUpControl = new PopupControlHost(ud);
        }

        private void DateTimePickerEx_Load(object sender, EventArgs e)
        {
            ValueChange();
        }

        /// <summary>
        /// 值更新
        /// </summary>
        private void ValueChange()
        {
            this.txtDisplay.Text = dateTimeValue.ToString(strShowFormat);
            this.txtDisplay.SelectionStart = 0;
            this.txtDisplay.SelectionLength = 4;
            this.ud.DateTimeValue = dateTimeValue;

            if (ValueChanged != null)
            {
                ValueChanged(this, new EventArgs());
            }
        }

        private void btnPopUp_Click(object sender, EventArgs e)
        {
            popUpControl.Show(this);
            //if (ud == null) 
            //{
            //    ud = new UCDataSelector();
            //    ud.OnDateSelected += new DateRangeEventHandler(ud_OnDateSelected);
            //    this.Parent.Controls.Add(ud);
            //    ud.Visible = false;
            //}

            //if (!ud.Visible)
            //{
            //    ud.Left = this.Left;
            //    ud.Top = this.Top + this.Height - 1;
            //    ud.Show();
            //    ud.BringToFront();
            //}
            //else 
            //{
            //    ud.Hide();
            //}

        }

        void ud_OnDateSelected(object sender, DateRangeEventArgs e)
        {
            Value = e.Start;
            popUpControl.Hide();
            //
            //ud.Hide();
            //throw new NotImplementedException();
        }

        private void txtDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            int intYearIndex = strShowFormat.IndexOf("yyyy");
            int intMonthIndex = strShowFormat.IndexOf("MM");
            int intDayIndex = strShowFormat.IndexOf("dd");
            int intHourIndex = strShowFormat.IndexOf("HH");
            int intMinuteIndex = strShowFormat.IndexOf("mm");
            int intSecondIndex = strShowFormat.IndexOf("ss");

            int start = txtDisplay.SelectionStart;

            if (start >= intYearIndex && start < intMonthIndex)
            {
                txtDisplay.SelectionStart = intYearIndex;
                txtDisplay.SelectionLength = 4;
            }
            else if (start >= intMonthIndex && start < intDayIndex)
            {
                txtDisplay.SelectionStart = intMonthIndex;
                txtDisplay.SelectionLength = 2;
            }
            else if (start >= intDayIndex && start < intHourIndex)
            {
                txtDisplay.SelectionStart = intDayIndex;
                txtDisplay.SelectionLength = 2;
            }
            else if (start >= intHourIndex && start < intMinuteIndex)
            {
                txtDisplay.SelectionStart = intHourIndex;
                txtDisplay.SelectionLength = 2;
            }
            else if (start >= intMinuteIndex && start < intSecondIndex)
            {
                txtDisplay.SelectionStart = intMinuteIndex;
                txtDisplay.SelectionLength = 2;
            }
            else if (start >= intSecondIndex && intSecondIndex > 0)
            {
                txtDisplay.SelectionStart = intSecondIndex;
                txtDisplay.SelectionLength = 2;
            }

        }

        private void pnlDatePicker_Click(object sender, EventArgs e)
        {
            //if (txtDisplay.ReadOnly)
            //{
            //    return;
            //}
            popUpControl.Show(this);
        }
    }
}
