using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL;
using SYSModel;
using Utility.Common;
using HXC_FuncUtility;
using ServiceStationClient.ComponentUI;

namespace HXCServerWinForm.UCForm.SyncDataLog
{
    public partial class UCSyncDataLog : UCBase
    {
        public UCSyncDataLog()
        {
            InitializeComponent();
            base.ExportEvent += new ClickHandler(UCSyncDataLog_ExportEvent);
        }

        private void UCSyncDataLog_Load(object sender, EventArgs e)
        {
            base.SetOpButtonVisible(this.Name);//按钮权限-是否隐藏
            DataGridViewEx.SetDataGridViewStyle(dgvSyncDataLog);
            BindCmb();
            BindPageData();
        }

        #region 事件
        /// <summary> 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                page.PageIndex = 1;
                BindPageData();
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("数据同步日志", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 清除查询条件
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                cmbbusiness.SelectedIndex = 0;
                cmbexternal_sys.SelectedIndex = 0;
                cmbsync_direction.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("数据同步日志", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 导出
        /// </summary>
        void UCSyncDataLog_ExportEvent(object sender, EventArgs e)
        {
            try
            {
                string fileName = "同步日志" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                ExcelHandler.ExportExcel(fileName, dgvSyncDataLog);
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("数据同步日志", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 格式化
        /// </summary>
        private void dgvSyncData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
                {
                    return;
                }
                string fieldNmae = dgvSyncDataLog.Columns[e.ColumnIndex].DataPropertyName;
                if (fieldNmae.Equals("business_name"))
                {
                    DataSources.EnumInterfaceType enumInterfaceType = (DataSources.EnumInterfaceType)Convert.ToInt16(e.Value.ToString());
                    e.Value = DataSources.GetDescription(enumInterfaceType, true);
                }
                if (fieldNmae.Equals("external_sys"))
                {
                    DataSources.EnumExternalSys enumExternalSys = (DataSources.EnumExternalSys)Convert.ToInt16(e.Value.ToString());
                    e.Value = DataSources.GetDescription(enumExternalSys, true);
                }
                if (fieldNmae.Equals("sync_direction"))
                {
                    DataSources.EnumSyncDirection enumSyncDirection = (DataSources.EnumSyncDirection)Convert.ToInt16(e.Value.ToString());
                    e.Value = DataSources.GetDescription(enumSyncDirection, true);
                }
                if (fieldNmae.Equals("sync_start_time") || fieldNmae.Equals("sync_end_time"))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("数据同步日志", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 跳转页
        /// </summary>
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindPageData();
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("数据同步日志", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        #endregion

        #region 方法
        /// <summary> 绑定下拉框
        /// </summary>
        public void BindCmb()
        {
            List<ListItem> list = DataSources.EnumToList(typeof(DataSources.EnumInterfaceType), true);
            cmbbusiness.DataSource = list;
            cmbbusiness.ValueMember = "value";
            cmbbusiness.DisplayMember = "text";

            list = DataSources.EnumToList(typeof(DataSources.EnumExternalSys), true);
            cmbexternal_sys.DataSource = list;
            cmbexternal_sys.ValueMember = "value";
            cmbexternal_sys.DisplayMember = "text";

            list = DataSources.EnumToList(typeof(DataSources.EnumSyncDirection), true);
            cmbsync_direction.DataSource = list;
            cmbsync_direction.ValueMember = "value";
            cmbsync_direction.DisplayMember = "text";
        }

        /// <summary> 绑定数据
        /// </summary>
        private void BindPageData()
        {
            string where = "1=1 ";
            if (cmbbusiness.SelectedIndex > 0)
            {
                where += string.Format(" and  business_name = '{0}'", cmbbusiness.SelectedValue);
            }

            if (cmbexternal_sys.SelectedIndex > 0)
            {
                where += string.Format(" and  external_sys = '{0}'", cmbexternal_sys.SelectedValue);
            }

            if (cmbsync_direction.SelectedIndex > 0)
            {
                where += string.Format(" and  sync_direction = '{0}'", cmbsync_direction.SelectedValue);
            }
            int recordCount = 0;
            DataTable dt = DBHelper.GetTableByPage("分页查询数据同步日志信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_data_sync_log", "*", where, "", "order by sync_start_time desc", page.PageIndex, page.PageSize, out recordCount);
            dgvSyncDataLog.DataSource = dt;
            page.RecordCount = recordCount;
            page.SetBtnState();
        }
        #endregion

    }
}
