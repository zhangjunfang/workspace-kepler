using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using SYSModel;
namespace HXCServerWinForm.UCMainLayOut
{
    public partial class UCMenuBar : UserControl
    {
        ///// <summary>
        ///// 日志相关操作
        ///// </summary>
        //operateLog clsLog = new operateLog();
        private string SysMonitor;//系统监控Tag
        private string SysMaintenance;//系统维护Tag
        private string PermissionManage;//权限管理Tag
        /// <summary>
        /// 系统监控 
        /// </summary>
        public event EventHandler SysMonitorClick;
        /// <summary>
        /// 系统维护 
        /// </summary>
        public event EventHandler SysMaintenanceClick;
        /// <summary>
        /// 权限管理
        /// </summary>
        public event EventHandler PermisssionManageClick;       

        public UCMenuBar()
        {
            InitializeComponent();
            //this.BackgroundImage = ServiceStationClient.Skin.Properties.Resources.head_menu_back;
            #region 系统监控
            this.ibtnSysMonitor.ImageDown = ServiceStationClient.Skin.Properties.Resources.xtjk_d;
            this.ibtnSysMonitor.ImageHover = ServiceStationClient.Skin.Properties.Resources.xtjk_d;
            this.ibtnSysMonitor.ImageNormal = ServiceStationClient.Skin.Properties.Resources.xtjk;
            this.ibtnSysMonitor.ButtonStatus = ServiceStationClient.ComponentUI.ImageButton.Status.Normal;
            this.ibtnSysMonitor.BorderStyle = System.Windows.Forms.BorderStyle.None;

            //this.tsMenuItemMonitor.Image = ServiceStationClient.Skin.Properties.Resources.icon_menu_monitor;
            //this.tsMenuItemTrack.Image = ServiceStationClient.Skin.Properties.Resources.icon_menu_track;
            //this.tsMenuItemImage.Image = ServiceStationClient.Skin.Properties.Resources.icon_menu_capture_image;
            #endregion

            #region 系统维护
            this.ibtnSysMaintenance.ImageDown = ServiceStationClient.Skin.Properties.Resources.xtwh_d;
            this.ibtnSysMaintenance.ImageHover = ServiceStationClient.Skin.Properties.Resources.xtwh_d;
            this.ibtnSysMaintenance.ImageNormal = ServiceStationClient.Skin.Properties.Resources.xtwh;
            this.ibtnSysMaintenance.ButtonStatus = ServiceStationClient.ComponentUI.ImageButton.Status.Normal;
            this.ibtnSysMaintenance.BorderStyle = System.Windows.Forms.BorderStyle.None;
            // this.tsMenuItemRealPlay.Image = ServiceStationClient.Skin.Properties.Resources.icon_menu_video_monitor;
            // this.tsMenuItemPlayBack.Image = ServiceStationClient.Skin.Properties.Resources.icon_menu_video_playback;
            #endregion

            #region 权限管理
            this.ibtnPermisssionManage.ImageDown = ServiceStationClient.Skin.Properties.Resources.permission_default;
            this.ibtnPermisssionManage.ImageHover = ServiceStationClient.Skin.Properties.Resources.permission_default;
            this.ibtnPermisssionManage.ImageNormal = ServiceStationClient.Skin.Properties.Resources.permission_selected;
            this.ibtnPermisssionManage.ButtonStatus = ServiceStationClient.ComponentUI.ImageButton.Status.Normal;
            this.ibtnPermisssionManage.BorderStyle = System.Windows.Forms.BorderStyle.None;

            //this.tsMenuItemHistoryAlarm.Image = ServiceStationClient.Skin.Properties.Resources.icon_menu_history_alarm;
            //this.tsMenuItemOperateStatistic.Image = ServiceStationClient.Skin.Properties.Resources.icon_menu_operate_statistic;
            //this.tsMenuItemOperateStatistic.HideDropDown();
            #endregion       
        }
        #region Tag属性
        public string SysMonitor_Tag
        {
            get
            {
                return SysMonitor;
            }
            set
            {
                SysMonitor = ibtnSysMonitor.Tag.ToString();
            }
        }

        public string SysMaintenance_Tag
        {
            get
            {
                return SysMaintenance;
            }
            set
            {
                SysMaintenance = ibtnSysMaintenance.Tag.ToString();
            }
        }

        public string PermissionManage_Tag
        {
            get
            {
                return PermissionManage;
            }
            set
            {
                PermissionManage = ibtnPermisssionManage.Tag.ToString();
            }
        }
        #endregion
        private ImageButton imgBtnCurrent = null;
        private void imgbtn_MouseLeave(object sender, EventArgs e)
        {
            ImageButton iBtnCurrent = sender as ImageButton;
            if (imgBtnCurrent != null)
            {
                if (imgBtnCurrent.Equals(iBtnCurrent))
                {
                    iBtnCurrent.ButtonStatus = ImageButton.Status.Hover;
                }
                else
                {
                    iBtnCurrent.ButtonStatus = ImageButton.Status.Normal;
                }
            }
            else
            {
                iBtnCurrent.ButtonStatus = ImageButton.Status.Normal;
            }
        }

        private void ButtonStatus()
        {

            foreach (Control item in flpnlContainer.Controls)
            {
                if (item.Visible)
                {
                    if (!item.Equals(imgBtnCurrent))
                    {
                        ImageButton otherImageBtn = item as ImageButton;
                        if (otherImageBtn != null && otherImageBtn.ButtonStatus != ImageButton.Status.Normal)
                        {
                            otherImageBtn.ButtonStatus = ImageButton.Status.Normal;
                        }
                    }
                    else
                    {
                        imgBtnCurrent.ButtonStatus = ImageButton.Status.Down;
                    }
                }

            }
        }

        private void imgbtn_MouseMove(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("鼠标移动测试");
        }       
        /// <summary>
        /// 系统维护 按钮点击事件  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        private void ibtnSysMaintenance_Click(object sender, EventArgs e)
        {
            imgBtnCurrent = sender as ImageButton;
            if (SysMaintenanceClick != null)
            {
                SysMaintenanceClick(sender, e);
            }
            ButtonStatus();
            Action<string,string> OpLog = HXCServerWinForm.CommonClass.CommonFuncCall.LogFunctionCall;
            OpLog.BeginInvoke(imgBtnCurrent.Tag.ToString(), "000", null, null);
        }

        /// <summary>
        /// 系统监控 按钮点击事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void ibtnSysMonitor_Click(object sender, EventArgs e)
        {
            imgBtnCurrent = sender as ImageButton;
            if (SysMonitorClick != null)
            {
                SysMonitorClick(sender, e);
            }
            ButtonStatus();
            Action<string, string> OpLog = HXCServerWinForm.CommonClass.CommonFuncCall.LogFunctionCall;
            OpLog.BeginInvoke(imgBtnCurrent.Tag.ToString(), "000", null, null);
        }
        /// <summary>
        /// 权限管理 按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnPermisssionManage_Click(object sender, EventArgs e)
        {
            imgBtnCurrent = sender as ImageButton;
            if (PermisssionManageClick != null)
            {
                PermisssionManageClick(sender, e);
            }
            ButtonStatus();
            Action<string, string> OpLog = HXCServerWinForm.CommonClass.CommonFuncCall.LogFunctionCall;
            OpLog.BeginInvoke(imgBtnCurrent.Tag.ToString(), "000", null, null);
        }

        private void UCMenuBar_Load(object sender, EventArgs e)
        {
            if (HXCServerWinForm.GlobalStaticObj.gLoginDataSet != null && HXCServerWinForm.GlobalStaticObj.gLoginDataSet.Tables[2].Rows.Count > 0)
            {
                DataView dv = new DataView();
                dv = HXCServerWinForm.GlobalStaticObj.gLoginDataSet.Tables[2].DefaultView;
                dv.RowFilter = "parent_id='S_ROOT'";
                DataTable dt = dv.ToTable();
                //ibtnSystemManagement.Visible = CheckPermission(dt);
                //ibtnDataManagement.Visible = CheckPermission(dt);
                //ibtnBusinessAnalysis.Visible = CheckPermission(dt);
                //ibtnCustomerService.Visible = CheckPermission(dt);
                //ibtnFinancialManagement.Visible = CheckPermission(dt);
                //ibtnAccessoriesBusiness.Visible = CheckPermission(dt);
                //ibtnRepairBusiness.Visible = CheckPermission(dt);
            }
            else
            {
                //ibtnSystemManagement.Visible = false;
                //ibtnDataManagement.Visible = false;
                //ibtnBusinessAnalysis.Visible = false;
                //ibtnCustomerService.Visible = false;
                //ibtnFinancialManagement.Visible = false;
                //ibtnAccessoriesBusiness.Visible = false;
                //ibtnRepairBusiness.Visible = false;
            }
        }
        private bool CheckPermission(DataTable dt)
        {
            bool bln = false;
            foreach (DataRow row in dt.Rows)
            {
                foreach (Control item in flpnlContainer.Controls)
                {
                    ImageButton imageBtn = item as ImageButton;
                    if (imageBtn.Tag.ToString() == row["fun_id"].ToString())
                    {
                        bln = true;
                    }
                }
            }
            return bln;
        }

        /// <summary>
        /// 三级菜单选中时 一级菜单的选中状态
        /// </summary>
        public void MenuButtonStatus()
        {
            foreach (Control item in flpnlContainer.Controls)
            {
                if (item.Visible)
                {
                    ImageButton imageBtn = item as ImageButton;
                    if (!item.Tag.Equals(SYSModel.clsSysConfig.STR_CURR_MAINMEMU))
                    {
                        imageBtn.ButtonStatus = ImageButton.Status.Normal;
                    }
                    else
                    {
                        imgBtnCurrent = imageBtn;
                        imgBtnCurrent.ButtonStatus = ImageButton.Status.Down;
                    }
                }
            }
        }
       
        ///// <summary>
        ///// 权限验证
        ///// </summary>
        //public HXCServerWinForm.FunctionName CheckPermission()
        //{
        //    HXCServerWinForm.FunctionName menuFunctionName = HXCServerWinForm.FunctionName.None;
        //    //#region 权限判断，菜单显示
        //    //this.imgbtnMonitor.Visible = ServiceStationClient.BLL.CommonMethod.CheckPermission(this.imgbtnMonitor.Tag.ToString());
        //    //this.imgbtnVideo.Visible = ServiceStationClient.BLL.CommonMethod.CheckPermission(this.imgbtnVideo.Tag.ToString());
        //    //this.imgbtnStatistic.Visible = ServiceStationClient.BLL.CommonMethod.CheckPermission(this.imgbtnStatistic.Tag.ToString());
        //    //this.imgbtnConfig.Visible = ServiceStationClient.BLL.CommonMethod.CheckPermission(this.imgbtnConfig.Tag.ToString());
        //    //#endregion
        //    return menuFunctionName;
        //}
        //public bool CheckPermission(string strFunID)
        //{
        //    bool result = false;
        //    foreach (MonitorClient.Model.Json.SysFunction item in MonitorClient.Model.clsSysConfig.USERPERMISSION.sysFunctionArray)
        //    {
        //        if (item.funId.Equals(strFunID))
        //        {
        //            result = true;
        //            break;
        //        }
        //    }
        //    return result;
        //}
    }
}
