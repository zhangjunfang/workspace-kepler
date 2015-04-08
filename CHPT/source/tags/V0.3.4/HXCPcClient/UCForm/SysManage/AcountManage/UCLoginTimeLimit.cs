using System;
using System.Collections.Generic;
using System.Data;
using Utility.Common;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using System.Text.RegularExpressions;
using SYSModel;
using System.Threading;

namespace HXCPcClient.UCForm.SysManage.AcountManage
{
    public partial class UCLoginTimeLimit : UCBase
    {
        private string limit_id = string.Empty;
        private DataTable dt;       

        #region --构造函数
        public UCLoginTimeLimit()
        {
            InitializeComponent();
        }
        #endregion

        #region --窗体加载
        private void UCLoginTimeLimit_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏 

            this.uiHandler -= new UiHandler(this.ShowData);
            this.uiHandler += new UiHandler(this.ShowData);
            base.EditEvent += new ClickHandler(UCLoginTimeLimit_EditEvent);
            base.SaveEvent += new ClickHandler(UCLoginTimeLimit_SaveEvent);
            base.CancelEvent += new ClickHandler(UCLoginTimeLimit_CancelEvent);
          
            this.BindData();           
        }
        #endregion

        #region 事件
        /// <summary> 编辑
        /// </summary>
        void UCLoginTimeLimit_EditEvent(object sender, EventArgs e)
        {
            this.panelMain.Enabled = true;            
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
                this.panelMain.Enabled = false;      
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

        /// <summary>
        /// 查询绑定数据
        /// </summary>
        public void BindData()
        {    
            ThreadPool.QueueUserWorkItem(new WaitCallback(this._BindData));
        }
        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _BindData(object obj)
        {
            this.dt = DBHelper.GetTable("查询登陆时间限制", GlobalStaticObj.CommAccCode, "sys_login_time_limit", "*", "", "", "");

            this.Invoke(this.uiHandler,dt);
        }
        /// <summary> 异步绑定数据 
        /// </summary>
        /// <param name="obj"></param>
        private void ShowData(object obj)
        {
            if (dt == null)
            {
                return;
            }

            DataRow dr = this.dt.Rows[0];
            this.limit_id = dr["login_time_limit_id"].ToString();
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

            this.panelMain.Enabled = false;
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
            long ticksStartDate = Common.LocalDateTimeToUtcLong(this.dtpStart.Value);
            dicFileds.Add("cycle_start_time", ticksStartDate.ToString());
            long ticksEndDate = Common.LocalDateTimeToUtcLong(this.dtpEnd.Value);
            dicFileds.Add("cycle_end_time", ticksEndDate.ToString());

            if (this.limit_id.Length == 0)
            {
                dicFileds.Add("login_pc_set_id", Guid.NewGuid().ToString());
                dicFileds.Add("create_by", GlobalStaticObj.UserID);
                dicFileds.Add("create_time", nowTicks);
            }
            else
            {
                dicFileds.Add("update_time", nowTicks);
                dicFileds.Add("update_by", GlobalStaticObj.UserID);
                keyName = "login_time_limit_id";
                keyValue = this.limit_id;
            }
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
