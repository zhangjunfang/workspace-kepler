using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using SYSModel;
using System.Threading;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using HXCPcClient.Chooser;

namespace HXCPcClient.UCForm.SysManage.ReminderSet
{
    /// <summary>
    /// 提醒设置
    /// 创建人：杨天帅
    /// </summary>
    public partial class UCReminderSet : UCBase
    {       

        #region --构造函数
        public UCReminderSet()
        {
            InitializeComponent();

            this.uiHandler -= new UiHandler(this.ShowData);
            this.uiHandler += new UiHandler(this.ShowData);

            base.EditEvent += new ClickHandler(UC_EditEvent);
            base.SaveEvent += new ClickHandler(UC_SaveEvent);
            base.CancelEvent += new ClickHandler(UC_CancelEvent);
        }
        #endregion

        #region --窗体加载
        private void UCReminderSet_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏  
            InitBtn();

            ThreadPool.QueueUserWorkItem(new WaitCallback(this._LoadData));
        }

        /// <summary>
        /// 保存后
        /// </summary>
        private void InitBtn()
        {
            this.btnDelete.Visible = false;
            this.btnStatus.Visible = false;
            this.btnSave.Visible = true;
            this.btnSave.Enabled = false;
            this.btnCancel.Visible = true;
            this.btnCancel.Enabled = false;

            this.panelConent.Enabled = false;
            this.btnEdit.Enabled = true;

        }
        #endregion

        #region --成员方法
        private void _LoadData(object obj)
        {           
            DataTable dt = DBHelper.GetTable("查询提醒设置信息", "sys_reminding_set", "*", "", "", "");
            
            if (dt != null)
            {
                this.Invoke(this.uiHandler, dt);
            }
        }
        /// <summary> 显示数据
        /// </summary>
        /// <param name="obj"></param>
        private void ShowData(object obj)
        {
            DataTable dt = obj as DataTable;

            string project=string.Empty; 
            string name=string.Empty;
            string id=string.Empty;
            if (dt.Rows.Count > 0)
            {
                DataTable dtNew = dt.DefaultView.ToTable(true, "projec");

                foreach (DataRow drNew in dtNew.Rows)
                {
                    name = string.Empty;
                    project = drNew["projec"].ToString();

                    DataRow[] drs = dt.Select("projec='" + project + "'");
                    for(int i=0;i<drs.Length;i++)
                    {                        
                        DataRow dr = drs[i];

                        id = dr["reminding_set_id"].ToString();
                        if (i == 0)
                        {
                            name = "cb_" + project;
                            if (this.panelConent.Controls.ContainsKey(name))
                            {
                                CheckBox cb = this.panelConent.Controls.Find(name, false)[0] as CheckBox;
                                cb.Checked = true;
                                cb.Tag = id;
                            }

                            name = "ucPreTime_" + project;
                            if (this.panelConent.Controls.ContainsKey(name))
                            {
                                UCPreTime ucPreTime = this.panelConent.Controls.Find(name, false)[0] as UCPreTime;
                                ucPreTime.SetTime(Convert.ToInt32(dr["happen_time"].ToString()));
                            }

                            name = "ucReminderType_" + project;
                            if (this.panelConent.Controls.ContainsKey(name))
                            {
                                UCReminderType ucType = this.panelConent.Controls.Find(name, false)[0] as UCReminderType;
                                ucType.SetChecked(dr["bubble"].ToString() == "1", dr["sounds"].ToString() == "1");
                            }
                        }
                        name = "labelUser_" + project;
                        if (this.panelConent.Controls.ContainsKey(name))
                        {
                            Label label = this.panelConent.Controls.Find(name, false)[0] as Label;
                            name = dr["user_name"].ToString();
                            if (i == 0)
                            {
                                label.Text = name;
                                label.Tag = dr["user_id"].ToString();
                            }
                            else
                            {
                                label.Text += ("," + name);
                                label.Tag += ("," + dr["user_id"].ToString());
                            }
                        }
                    }                   
                }
            }            
        }
        #endregion

        #region --按钮事件
        //编辑
        void UC_EditEvent(object sender, EventArgs e)
        {
            if (!this.panelConent.Enabled)
            {
                this.panelConent.Enabled = true;
            }
            this.btnEdit.Enabled = false;
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
        }   
        //保存
        void UC_SaveEvent(object sender, EventArgs e)
        {
            Array enumValues = Enum.GetValues(typeof(DataSources.EnumReminderType));
            string project = string.Empty;
            string name = string.Empty;
            int time = 0;
            string bubble = "0";
            string sound = "0";
            string nowUtcTicks = Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString();
          
            List<SQLObj> listSql = new List<SQLObj>();

            foreach (Enum enumValue in enumValues)
            {
                project = enumValue.ToString();
                name = "cb_" + project;
                if (this.panelConent.Controls.ContainsKey(name))
                {
                    CheckBox cb = this.panelConent.Controls.Find(name, false)[0] as CheckBox;
                    if (!cb.Checked)
                    {
                        continue;
                    }

                    name = "ucPreTime_" + project;
                    if (this.panelConent.Controls.ContainsKey(name))
                    {
                        UCPreTime ucTime=this.panelConent.Controls.Find(name, false)[0] as UCPreTime;
                        time = ucTime.GetTime();
                    }

                    name = "ucReminderType_" + project;
                    if (this.panelConent.Controls.ContainsKey(name))
                    {
                        UCReminderType ucType = this.panelConent.Controls.Find(name, false)[0] as UCReminderType;
                        bubble = ucType.BubbleChecked == true ? "1" : "0";
                        sound = ucType.SoundChecked == true ? "1" : "0";
                    }

                    //判断人员是否为空
                    name = "labelUser_" + project;
                    if (this.panelConent.Controls.ContainsKey(name))
                    {
                        Label label = this.panelConent.Controls.Find(name, false)[0] as Label;
                        if (label.Text.Length == 0)
                        {
                            continue;
                        }
                        if (label.Text == "[全部]")
                        {
                            foreach (DataRow dr in LocalCache.DtUser.Rows)
                            {
                                SQLObj obj = new SQLObj();
                                obj.cmdType = CommandType.Text;
                                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                                obj.Param = dicParam;
                                obj.sqlString = string.Format("insert into sys_reminding_set(reminding_set_id,projec,happen_time,user_id,user_name,bubble,sounds,create_by,create_time,update_by,update_time) values('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}',{8},'{9}',{10})",
                                             Guid.NewGuid().ToString(), project, time, dr["user_id"], LocalCache.GetUserById(dr["user_id"].ToString()), bubble, sound, GlobalStaticObj.UserID, nowUtcTicks, GlobalStaticObj.UserID, nowUtcTicks);
                                listSql.Add(obj);
                            }
                        }
                        else
                        {
                            string[] userIds = label.Tag.ToString().Split(',');
                            foreach (string userId in userIds)
                            {
                                SQLObj obj = new SQLObj();
                                obj.cmdType = CommandType.Text;
                                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                                obj.Param = dicParam;
                                obj.sqlString = string.Format("insert into sys_reminding_set(reminding_set_id,projec,happen_time,user_id,user_name,bubble,sounds,create_by,create_time,update_by,update_time) values('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}',{8},'{9}',{10})",
                                             Guid.NewGuid().ToString(), project, time, userId, LocalCache.GetUserById(userId), bubble, sound, GlobalStaticObj.UserID, nowUtcTicks, GlobalStaticObj.UserID, nowUtcTicks);
                                listSql.Add(obj);
                            }
                        }
                    }
                }
            }

            if (listSql.Count == 0)
            {
                return;
            }

            SQLObj objDelete = new SQLObj();
            objDelete.cmdType = CommandType.Text;
            objDelete.Param = new Dictionary<string, ParamObj>();
            objDelete.sqlString = string.Format("delete from sys_reminding_set");
            listSql.Insert(0,objDelete);

            if (DBHelper.BatchExeSQLMultiByTrans("", listSql))
            {
                MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                ThreadPool.QueueUserWorkItem(new WaitCallback(this._LoadData));
                //InitBtn();
                this.btnEdit.Enabled = true;
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;
                this.panelConent.Enabled = false;
            }
            else            
            {
                MessageBoxEx.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }           
        }
        //取消
        void UC_CancelEvent(object sender, EventArgs e)
        {
            if (this.panelConent.Enabled)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(this._LoadData));
            }
            InitBtn();
        }
        #endregion

        #region --双击编辑用户
        private void labelUser_YYDZ_DoubleClick(object sender, EventArgs e)
        {
            Label label = sender as Label;

            frmPersonnelSelector frmSelector = new frmPersonnelSelector();
            frmSelector.isMoreCheck = true;
            DialogResult result = frmSelector.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (frmSelector.strPersonName.Length > 0)
                {
                    label.Text = frmSelector.strPersonName.Substring(0, frmSelector.strPersonName.Length - 1);
                    label.Tag = frmSelector.strUserId.Substring(0, frmSelector.strUserId.Length - 1);
                }
            }
        }
        #endregion
    }
}
