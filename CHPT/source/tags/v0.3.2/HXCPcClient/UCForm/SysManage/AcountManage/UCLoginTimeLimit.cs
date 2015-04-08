using System;
using System.Collections.Generic;
using System.Data;
using Utility.Common;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using System.Text.RegularExpressions;

namespace HXCPcClient.UCForm.SysManage.AcountManage
{
    public partial class UCLoginTimeLimit : UCBase
    {
        public UCLoginTimeLimit()
        {
            InitializeComponent();
        }

        private void UCLoginTimeLimit_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏 

            base.EditEvent += new ClickHandler(UCLoginTimeLimit_EditEvent);
            base.SaveEvent += new ClickHandler(UCLoginTimeLimit_SaveEvent);
            base.CancelEvent += new ClickHandler(UCLoginTimeLimit_CancelEvent);
          
            this.BindData();
            this.FrmHandle();
        }

        #region 事件
        /// <summary> 编辑
        /// </summary>
        void UCLoginTimeLimit_EditEvent(object sender, EventArgs e)
        {
            Common.SetControlEnable(this, true);
            base.SetBtnStatus(WindowStatus.Edit);
        }

        /// <summary> 保存
        /// </summary>
        void UCLoginTimeLimit_SaveEvent(object sender, EventArgs e)
        {
            if (!rbByTime.Checked)
            {
                return;
            }

            bool flag = SaveData();
            if (flag)
            {
                MessageBoxEx.Show("操作成功！");
                Common.SetControlEnable(this, false);               
            }
            else
            {
                MessageBoxEx.Show("操作失败！");
            }
        }

        /// <summary> 取消
        /// </summary>
        void UCLoginTimeLimit_CancelEvent(object sender, EventArgs e)
        {
            this.BindData();
            Common.SetControlEnable(this, false);           
        }
        /// <summary> 限制类别改变
        /// </summary>
        private void rbByTime_CheckedChanged(object sender, EventArgs e)
        {
            if (rbByTime.Checked)
            {
                this.dtpStart.Enabled = true;
                this.dtpEnd.Enabled = true;
                Common.SetControlEnable(pnlByWeek, false);
            }
            else
            {
                this.dtpStart.Enabled = false;
                this.dtpEnd.Enabled = false;
                Common.SetControlEnable(pnlByWeek, true);
            }
        }
        #endregion

        #region 方法
        /// <summary> 处理界面控件
        /// </summary>
        private void FrmHandle()
        {
            rbByTime.Enabled = false;
            rbByWeek.Enabled = false;
          
            Common.SetControlEnable(pnlByWeek, false);
        }

        /// <summary>绑定数据
        /// </summary>
        private void BindData()
        {
            DataTable dt = DBHelper.GetTable("查询登陆时间限制", GlobalStaticObj.CommAccCode, "sys_login_time_limit", "*", "", "", "");
            DataRow dr = dt.Rows[0];
            bool isByTime = dr["limit_type"].ToString() == Convert.ToInt32(SYSModel.DataSources.EnumLoginTimeLimitType.ByTime).ToString();
            rbByTime.Checked = isByTime;
            rbByWeek.Checked = !isByTime;
            cbMonday.Checked = dr["monday"].ToString().Equals("1");
            cbTuesday.Checked = dr["tuesday"].ToString().Equals("1");
            cbWednesday.Checked = dr["wednesday"].ToString().Equals("1");
            cbTuesday.Checked = dr["thursday"].ToString().Equals("1");
            cbFriday.Checked = dr["friday"].ToString().Equals("1");
            cbSaturday.Checked = dr["saturday"].ToString().Equals("1");
            cbSunday.Checked = dr["sunday"].ToString().Equals("1");
            long ticksCycleStart = Convert.ToInt64(dr["cycle_start_time"].ToString());
            this.dtpStart.Value = Common.UtcLongToLocalDateTime(ticksCycleStart);
            long ticksCycleEnd = Convert.ToInt64(dr["cycle_end_time"].ToString());
            this.dtpEnd.Value = Common.UtcLongToLocalDateTime(ticksCycleEnd);           
        }

        /// <summary>保存数据
        /// </summary>
        private bool SaveData()
        {           

            string keyName = string.Empty;
            string keyValue = string.Empty;
            string opName = "保存登录时间限制";
            Dictionary<string, string> dicFileds = new Dictionary<string, string>();
            string nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString();
            SYSModel.DataSources.EnumLoginTimeLimitType limitType = rbByTime.Checked ? SYSModel.DataSources.EnumLoginTimeLimitType.ByTime : SYSModel.DataSources.EnumLoginTimeLimitType.ByWeek;
            dicFileds.Add("limit_type", Convert.ToInt32(limitType).ToString());
            dicFileds.Add("monday", cbMonday.Checked ? "1" : "0");
            dicFileds.Add("tuesday", cbTuesday.Checked ? "1" : "0");
            dicFileds.Add("wednesday", cbWednesday.Checked ? "1" : "0");
            dicFileds.Add("thursday", cbTuesday.Checked ? "1" : "0");
            dicFileds.Add("friday", cbFriday.Checked ? "1" : "0");
            dicFileds.Add("saturday", cbSaturday.Checked ? "1" : "0");
            dicFileds.Add("sunday", cbSunday.Checked ? "1" : "0");
            long ticksStartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(this.dtpStart.Value.ToString("yyyy-MM-dd")));
            dicFileds.Add("cycle_start_time", ticksStartDate.ToString());
            long ticksEndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(this.dtpEnd.Value.ToString("yyyy-MM-dd")));
            dicFileds.Add("cycle_end_time", ticksEndDate.ToString());

            //dicFileds.Add("week_start_time", GetTotalSeconds(timeInterval.StartTime));
            //dicFileds.Add("week_end_time", GetTotalSeconds(this.dtpEnd.Value.EndTime));
            dicFileds.Add("update_time", nowTicks);
            dicFileds.Add("update_by", GlobalStaticObj.UserID);
            keyName = "login_time_limit_id";
            keyValue = "1";
            bool flag = DBHelper.Submit_AddOrEdit(opName, GlobalStaticObj.CommAccCode, "sys_login_time_limit", keyName, keyValue, dicFileds);
            return flag;
        }

        /// <summary> 通过时间串获取总秒数
        /// </summary>
        /// <param name="timeStr">时间串</param>
        /// <returns>总秒数</returns>
        private string GetTotalSeconds(string timeStr)
        {
            string[] timeArr = Regex.Split(timeStr, ":", RegexOptions.IgnorePatternWhitespace);
            long seconds = Convert.ToInt16(timeArr[1]) * 60 * 60 + Convert.ToInt16(timeArr[1]) * 60;
            return seconds.ToString();
        }

        /// <summary> 通过总秒数获取时间串
        /// </summary>
        /// <param name="seconds">总秒数</param>
        /// <returns>时间串</returns>
        private string GetTimeStr(string strSeconds)
        {
            int seconds = 0;
            if (!string.IsNullOrEmpty(strSeconds))
            {
                seconds = Convert.ToInt32(strSeconds);
            }
            int hour = seconds / 3600;
            int minute = (seconds % 3600) / 60;
            string timeStr = hour.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0');
            return timeStr;
        }
        #endregion
    }
}
