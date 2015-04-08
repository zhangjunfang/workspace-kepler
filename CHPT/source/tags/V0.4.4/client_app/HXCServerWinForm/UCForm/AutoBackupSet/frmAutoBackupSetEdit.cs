using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using BLL;
using HXC_FuncUtility;
using SYSModel;
using Utility.Common;
using HXCServerWinForm.CommonClass;

namespace HXCServerWinForm.UCForm.AutoBackupSet
{
    public partial class frmAutoBackupSetEdit : FormEx
    {
        #region 属性
        /// <summary> 0-新增 1-编辑 2-查看
        /// </summary>
        public int OpType = 0;
        /// <summary> 帐套id
        /// </summary>
        public string auto_backup_set_id = "";
        #endregion
        public frmAutoBackupSetEdit()
        {
            InitializeComponent();
        }

        private void frmAutoBackupSetEdit_Load(object sender, EventArgs e)
        {
           
            dtpStart.Value = GlobalStaticObj_Server.Instance.CurrentDateTime;
            BindCmx();
            BindData();
            if (OpType == 2)
            {
                Common.SetControlForLable(pnlContainer, false);
            }
        }

        #region 事件
        /// <summary> 新增或更新
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            errProvider.Clear();
            try
            {
                if (string.IsNullOrEmpty(txttaskname.Caption))
                {
                    Validator.SetError(errProvider, txttaskname, "请录入任务名称");
                    return;
                }
                if (cmbAcc.SelectedIndex == 0)
                {
                    Validator.SetError(errProvider, cmbAcc, "请选择备份帐套");
                    return;
                }
                if (DBHelper.IsExist("判断帐套备份信息是否存在", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_auto_backup_set", "book_id='" + cmbWeek.SelectedValue.ToString() + "'"))
                {
                    Validator.SetError(errProvider, cmbWeek, "已存在该帐套的自动备份任务");
                    return;
                }
                if (rbEveryWeek.Checked && cmbWeek.SelectedIndex == 0)
                {
                    Validator.SetError(errProvider, cmbWeek, "请选择备份时间");
                    return;
                }
                Dictionary<string, string> dicFields = new Dictionary<string, string>();
                dicFields.Add("task_name", txttaskname.Caption.Trim());
                dicFields.Add("book_id", cmbAcc.SelectedValue.ToString());
                dicFields.Add("when_use_handle_method", rbNoticeUser.Checked ? ((int)DataSources.EnumYesNo.Yes).ToString() : ((int)DataSources.EnumYesNo.NO).ToString());
                if (rbEveryDay.Checked)
                {
                    dicFields.Add("auto_backup_type", ((int)DataSources.EnumAutoBackupType.EveryDay).ToString());
                    dicFields.Add("auto_backup_interval", numDay.Value.ToString());
                }
                else if (rbEveryWeek.Checked)
                {
                    dicFields.Add("auto_backup_type", ((int)DataSources.EnumAutoBackupType.EveryWeek).ToString());
                    dicFields.Add("auto_backup_interval", cmbWeek.SelectedValue.ToString());
                }
                else if (rbEveryMonth.Checked)
                {
                    dicFields.Add("auto_backup_type", ((int)DataSources.EnumAutoBackupType.EveryMonth).ToString());
                    dicFields.Add("auto_backup_interval", numMonth.Value.ToString());
                }
                dicFields.Add("auto_backup_starttime", Common.LocalDateTimeToUtcLong(dtpStart.Value).ToString());
                string pkName = "";
                string pkValue = "";
                if (OpType == 0)
                {
                    dicFields.Add("auto_backup_set_id", Guid.NewGuid().ToString());
                    dicFields.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                    dicFields.Add("create_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
                    dicFields.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
                    dicFields.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                }
                else if (OpType == 1)
                {
                    pkName = "auto_backup_set_id";
                    pkValue = auto_backup_set_id;
                    dicFields.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                    dicFields.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
                }
                else
                {
                    MessageBoxEx.ShowInformation("操作类型不正确");
                    return;
                }
                bool flag = DBHelper.Submit_AddOrEdit("创建自动备份任务", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_auto_backup_set", pkName, pkValue, dicFields);
                if (!flag)
                {
                    MessageBoxEx.Show("保存失败", "系统提示");
                    return;
                }
                CommonUtility.LoadAutoBackUpPlanInfo();
                MessageBoxEx.Show("保存成功", "系统提示");
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("自动备份设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 方法
        /// <summary> 下拉框初始化绑定
        /// </summary>
        public void BindCmx()
        {
            DataTable dt = DBHelper.GetTable("获取帐套信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_setbook", "id,setbook_name", "enable_flag='1'", "", "order by setbook_code");
            DataRow dr = dt.NewRow();
            dr["id"] = "";
            dr["setbook_name"] = "请选择";
            dt.Rows.InsertAt(dr, 0);
            dr = dt.NewRow();
            dr["id"] = "000";//通用库代码代表备份所有库
            dr["setbook_name"] = "所有账套";
            dt.Rows.InsertAt(dr, 1);
            //帐套信息
            cmbAcc.DataSource = dt;
            cmbAcc.ValueMember = "id";
            cmbAcc.DisplayMember = "setbook_name";
            cmbAcc.SelectedIndex = 0;
            //周
            List<ListItem> list = DataSources.EnumToListByValueString(typeof(DataSources.EnumWeek), true, "请选择");
            cmbWeek.DataSource = list;
            cmbWeek.DisplayMember = "text";
            cmbWeek.ValueMember = "value";
            cmbWeek.SelectedIndex = 0;
        }

        /// <summary> 绑定数据
        /// </summary>
        private void BindData()
        {
            if (OpType == 0)
            {
                auto_backup_set_id = Guid.NewGuid().ToString();
                return;
            }

            DataTable dtTask = DBHelper.GetTable("获取自动备份设置信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_auto_backup_set", "*", "auto_backup_set_id='" + auto_backup_set_id + "'", "", "");
            if (dtTask.Rows.Count > 0)
            {
                #region 自动备份信息
                DataRow dr = dtTask.Rows[0];
                txttaskname.Caption = dr["task_name"].ToString();
                cmbAcc.SelectedValue = dr["book_id"].ToString();
                rbNoBack.Checked = dr["when_use_handle_method"].ToString() == ((int)DataSources.EnumYesNo.NO).ToString();
                rbNoticeUser.Checked = dr["when_use_handle_method"].ToString() == ((int)DataSources.EnumYesNo.Yes).ToString();
                int backType = Convert.ToInt32(dr["auto_backup_type"].ToString());
                rbEveryDay.Checked = backType == (int)DataSources.EnumAutoBackupType.EveryDay;
                rbEveryWeek.Checked = backType == (int)DataSources.EnumAutoBackupType.EveryWeek;
                rbEveryMonth.Checked = backType == (int)DataSources.EnumAutoBackupType.EveryMonth;
                if (backType == (int)DataSources.EnumAutoBackupType.EveryDay)
                {
                    numDay.Value = Convert.ToDecimal(dr["auto_backup_interval"].ToString());
                }
                if (backType == (int)DataSources.EnumAutoBackupType.EveryDay)
                {
                    cmbWeek.SelectedValue = dr["auto_backup_interval"].ToString();
                }
                if (backType == (int)DataSources.EnumAutoBackupType.EveryDay)
                {
                    numMonth.Value = Convert.ToDecimal(dr["auto_backup_interval"].ToString());
                }
                long ticks = Convert.ToInt64(dr["auto_backup_starttime"].ToString());
                dtpStart.Value = Common.UtcLongToLocalDateTime(ticks);
                #endregion
            }
        }
        #endregion
    }
}
