using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYSModel;
using Utility.Common;
using BLL;
using HXC_FuncUtility;
using ServiceStationClient.ComponentUI;
using yuTongWebService;

namespace HXCServerWinForm.UCForm.SyncData
{
    public partial class UCSyncData : UCBase
    {
        public Control parentCol;

        public UCSyncData()
        {
            InitializeComponent();
        }

        private void UCSyncData_Load(object sender, EventArgs e)
        {
            base.SetOpButtonVisible(this.Name);//按钮权限-是否隐藏
            DataGridViewEx.SetDataGridViewStyle(dgvSyncData);
            base.SyncEvent += new ClickHandler(UCSyncData_SyncEvent);
            parentCol = this.Parent;
            dgvSyncData.ReadOnly = false;
            BindCmb();
            BindData();
        }

        #region 事件
        void UCSyncData_SyncEvent(object sender, EventArgs e)
        {
            try
            {
                List<DataSources.EnumInterfaceType> list = new List<DataSources.EnumInterfaceType>();
                foreach (DataGridViewRow dr in dgvSyncData.Rows)
                {
                    object value = dr.Cells["colCheck"].EditedFormattedValue;
                    if (value != null && (bool)value)
                    {
                        DataSources.EnumInterfaceType enumInterfaceType = (DataSources.EnumInterfaceType)Convert.ToInt32(dr.Cells["data_sync_id"].Value.ToString().Substring(2, 1));
                        list.Add(enumInterfaceType);
                    }
                }

                if (list.Count == 0)
                {
                    MessageBoxEx.ShowInformation("请选择同步项");
                    return;
                }
                if (MessageBoxEx.ShowQuestion("本操作数据量大，时间长，会占用服务器大量资源，请谨慎操作，是否继续？如果继续，同步数据会转入后台执行。"))
                {
                    foreach (DataSources.EnumInterfaceType enumType in list)
                    {
                        SyncData(enumType);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("数据同步", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindData();
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("数据同步", ex);
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
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("数据同步", ex);
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
                string fieldNmae = dgvSyncData.Columns[e.ColumnIndex].DataPropertyName;
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
                if (fieldNmae.Equals("last_sync_time"))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("数据同步", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }


        private void dgvSyncData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)//复选框列可编辑
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region 方法
        /// <summary> 提示同步信息
        /// </summary>
        public void ShowMsg(string msg, DataSources.EnumInterfaceType enumType)
        {
            if (parentCol == null)
            {
                return;
            }
            if (parentCol.IsDisposed || !parentCol.Parent.IsHandleCreated) return;
            string enumDesc = DataSources.GetDescription(enumType, true);
            if (!string.IsNullOrEmpty(msg))
            {
                parentCol.Invoke(new Action(() => { MessageBoxEx.ShowWarning(enumDesc + "同步失败！失败原因：" + msg); }));
            }
            else
            {
                parentCol.Invoke(new Action(() => { MessageBoxEx.ShowInformation(enumDesc + "同步成功！请进入系统监控-数据同步查看"); BindData(); }));
            }
        }
        /// <summary> 同步信息
        /// </summary>
        public void SyncData(DataSources.EnumInterfaceType enumInterfaceType)
        {
            switch (enumInterfaceType)
            {
                case DataSources.EnumInterfaceType.ServiceStation:
                    new System.Threading.Thread(SysServiceStationData).Start();
                    break;
                case DataSources.EnumInterfaceType.Bus:
                    new System.Threading.Thread(SysBusData).Start();
                    break;
                case DataSources.EnumInterfaceType.BusCustomer:
                    new System.Threading.Thread(SysCustomerData).Start();
                    break;
                case DataSources.EnumInterfaceType.Contact:
                    new System.Threading.Thread(SysContactData).Start();
                    break;
                case DataSources.EnumInterfaceType.RepairProject:
                    new System.Threading.Thread(SysRepairProjectData).Start();
                    break;
                case DataSources.EnumInterfaceType.Part:
                    new System.Threading.Thread(SysPartData).Start();
                    break;
                case DataSources.EnumInterfaceType.BusModel:
                    new System.Threading.Thread(SysBusModelData).Start();
                    break;
                case DataSources.EnumInterfaceType.HitchMode:
                    new System.Threading.Thread(SysHitchModeData).Start();
                    break;
                case DataSources.EnumInterfaceType.ProdImprovement:
                    new System.Threading.Thread(SysProdImprovementData).Start();
                    break;
                default:
                    MessageBoxEx.ShowInformation("无对应同步项");
                    break;
            }
        }


        /// <summary> 同步服务站信息
        /// </summary>
        public void SysServiceStationData()
        {
            string errMsg = yuTongWebService.WebServ_YT_BasicData.LoadServiceStation(GlobalStaticObj_YT.ClientID, GlobalStaticObj_YT.SAPCode, true);
            ShowMsg(errMsg, DataSources.EnumInterfaceType.ServiceStation);
        }

        /// <summary> 同步车辆信息
        /// </summary>
        public void SysBusData()
        {
            string errMsg;
            string lastTime = new SysConfig().GetLastTime("BusLastTime");//车辆客户最后更新时间
            if (string.IsNullOrEmpty(lastTime))
            {
                errMsg = yuTongWebService.WebServ_YT_BasicData.InitData_Bus();
            }
            else
            {
                errMsg = yuTongWebService.WebServ_YT_BasicData.LoadBus(lastTime);
            }
            ShowMsg(errMsg, DataSources.EnumInterfaceType.Bus);
        }

        /// <summary> 同步车辆客户信息
        /// </summary>
        public void SysCustomerData()
        {
            string errMsg;
            string lastTime = new SysConfig().GetLastTime("CustomerLastTime");//车辆客户最后更新时间
            if (string.IsNullOrEmpty(lastTime))
            {
                errMsg = yuTongWebService.WebServ_YT_BasicData.InitData_Customer();
            }
            else
            {
                errMsg = yuTongWebService.WebServ_YT_BasicData.LoadCustomer(lastTime);
            }
            ShowMsg(errMsg, DataSources.EnumInterfaceType.BusCustomer);
        }

        /// <summary> 同步联系人信息
        /// </summary>
        public void SysContactData()
        {
            string errMsg;
            string lastTime = new SysConfig().GetLastTime("ContactLastTime");//车辆客户最后更新时间
            if (string.IsNullOrEmpty(lastTime))
            {
                errMsg = yuTongWebService.WebServ_YT_BasicData.InitData_Contact();
            }
            else
            {
                errMsg = yuTongWebService.WebServ_YT_BasicData.LoadContact(lastTime, "01");
                errMsg = yuTongWebService.WebServ_YT_BasicData.LoadContact(lastTime, "02");
            }
            ShowMsg(errMsg, DataSources.EnumInterfaceType.Contact);
        }

        /// <summary> 同步工时信息
        /// </summary>
        public void SysRepairProjectData()
        {
            string errMsg;
            errMsg = yuTongWebService.WebServ_YT_BasicData.LoadRepairProject();
            ShowMsg(errMsg, DataSources.EnumInterfaceType.RepairProject);
        }

        /// <summary> 同步配件信息
        /// </summary>
        public void SysPartData()
        {
            string errMsg;
            string lastTime = new SysConfig().GetLastTime("PartLastTime");//车辆客户最后更新时间
            if (string.IsNullOrEmpty(lastTime))
            {
                errMsg = yuTongWebService.WebServ_YT_BasicData.InitData_Part();
            }
            else
            {
                errMsg = yuTongWebService.WebServ_YT_BasicData.LoadPart(lastTime);
            }
            ShowMsg(errMsg, DataSources.EnumInterfaceType.Part);
        }

        /// <summary> 同步车型信息
        /// </summary>
        public void SysBusModelData()
        {
            string errMsg = yuTongWebService.WebServ_YT_BasicData.LoadBusModel();
            ShowMsg(errMsg, DataSources.EnumInterfaceType.BusModel);
        }

        /// <summary> 同步故障模式
        /// </summary>
        public void SysHitchModeData()
        {
            string errMsg = yuTongWebService.WebServ_YT_BasicData.LoadHitchMode();
            ShowMsg(errMsg, DataSources.EnumInterfaceType.HitchMode);
        }

        /// <summary> 同步产品改进号信息
        /// </summary>
        public void SysProdImprovementData()
        {
            string errMsg = yuTongWebService.WebServ_YT_BasicData.LoadProdImprovement("1900-01-01");
            ShowMsg(errMsg, DataSources.EnumInterfaceType.ProdImprovement);
        }

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
        }

        /// <summary> 绑定数据
        /// </summary>
        private void BindData()
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

            DataTable dt = DBHelper.GetTable("分页查询帐套信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_data_sync", "*", where, "", "order by data_sync_id");
            dgvSyncData.DataSource = dt;
        }
        #endregion
    }
}
