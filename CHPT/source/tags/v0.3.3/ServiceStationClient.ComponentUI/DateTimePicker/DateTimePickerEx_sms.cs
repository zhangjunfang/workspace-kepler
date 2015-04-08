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
    public partial class DateTimePickerEx_sms : UserControl
    {
        public event EventHandler ValueChanged;

        private PopupControlHost popUpControl;

        private bool valueIsNull = true;
        /// <summary>
        /// 值是否可为空 true可为空 
        /// </summary>
       [Description("是否可为空 默认可为空true"), Category("自定义"), DefaultValue(true)]
        public bool ValueIsNull
        {
            get
            {
                return valueIsNull;
            }
            set
            {
                valueIsNull = value;
                ValueChange();
            }
        }

        //时间格式
        private string strShowFormat = "yyyy-MM-dd HH:mm:ss";
        //时间格式
        [Description("时间格式"), Category("自定义"), DefaultValue("yyyy-MM-dd HH:mm:ss")]
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


        #region 修改时间可为空
      
        /// <summary>
        /// 时间为空
        /// </summary>
        private string dateTimeText = "";
        [Description("控件值"), Category("自定义"), DefaultValue("")]
        public string Value
        {
            get
            {
                return dateTimeText;
            }

            set
            {
                dateTimeText = value;
               
                ValueChange();
            }
        }
        
        #endregion

        private UCDataSelector_sms ud = null;

        public DateTimePickerEx_sms()
        {
            InitializeComponent();
            #region 修改时间可为空
            //dateTimeValue = DateTime.Now;
            dateTimeText = "";
            #endregion
            ud = new UCDataSelector_sms();
            ud.OnDateSelected += new DateRangeEventHandler(ud_OnDateSelected);
            ud.OnDateClear += new DateRangeEventHandler(ud_OnDateClear);
            popUpControl = new PopupControlHost(ud);
        }

      

        private void DateTimePickerEx_Load(object sender, EventArgs e)
        {
            ValueChange();
        }

        private void txtDisplay_TextChanged(object sender, EventArgs e)
        {
            if (this.Value != "" && txtDisplay.Text.Trim() == "")
            {
                Value = "";
            }
        }
        #region 验证日期
        /// <summary>
        /// 验证日期
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsDateTime(string source)
        {
            try
            {
                DateTime time = Convert.ToDateTime(source);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        /// <summary>
        /// 值更新
        /// </summary>
        private void ValueChange() 
        {
            if (valueIsNull)
            {
                if (dateTimeText == "")
                {
                    dateTimeText = "";
                }
            }
            else
            {
                if (dateTimeText == "")
                {
                    dateTimeText = DateTime.Now.ToString(strShowFormat);
                }
            }
            this.ud.ShowTime = showTime;
            this.ud.ValueIsNull = valueIsNull;
            if (dateTimeText != "")
            {
                this.txtDisplay.Text = Convert.ToDateTime(dateTimeText).ToString(strShowFormat); //dateTimeValue.ToString(strShowFormat);
                this.txtDisplay.SelectionStart = 0;
                this.txtDisplay.SelectionLength = 4;
                //this.ud.DateTimeValue = dateTimeValue;
                this.ud.DateTimeValue = Convert.ToDateTime(dateTimeText);
            }
            else
            {
                this.txtDisplay.Text = "";
                this.ud.DateTimeValue = DateTime.Now;
            }
           
            if (ValueChanged != null) 
            {
                ValueChanged(this, new EventArgs());
            }
        }
        //是否显示time部分 
        private bool showTime = false;
        [Description("控件是否显示time部分 默认不显示false"), Category("自定义"), DefaultValue(false)]
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
            //Value = e.Start;
            Value = e.Start.ToString();
            popUpControl.Hide();
            //
            //ud.Hide();
            //throw new NotImplementedException();
        }
        void ud_OnDateClear(object sender, DateRangeEventArgs e)
        {
            Value = "";
            popUpControl.Hide();
        }
        private void txtDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            int intYearIndex = strShowFormat.IndexOf("yyyy");
            int intMonthIndex = strShowFormat.IndexOf("MM");
            int intDayIndex = strShowFormat.IndexOf("dd");
            int intHourIndex = strShowFormat.IndexOf("HH");
            int intMinuteIndex = strShowFormat.IndexOf("mm");
            int intSecondIndex = strShowFormat.IndexOf("ss");
            if (txtDisplay.Text != "")
            {
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
