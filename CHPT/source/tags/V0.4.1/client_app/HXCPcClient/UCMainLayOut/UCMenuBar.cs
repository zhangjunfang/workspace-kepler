using System;
using System.Data;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
//using ServiceStationClient.Log;

namespace HXCPcClient.UCMainLayOut
{
    // public delegate void SetMainFormTopMostHandle(string str); //定义委托 
    public partial class UCMenuBar : UserControl
    {
        //
        private bool loadFlag = true;

        ///// <summary>
        ///// 日志相关操作
        ///// </summary>
        //operateLog clsLog = new operateLog();

        private string SystemManagement;//系统管理Tag
        private string DataManagement;//数据管理Tag
        private string BusinessAnalysis;//经营分析Tag
        private string CustomerService;//客户服务Tag
        private string FinancialManagement;//财务管理Tag
        private string AccessoriesBusiness;//配件业务Tag
        private string RepairBusiness;//维修业务Tag

        /// <summary>
        /// 系统管理 
        /// </summary>
        public event EventHandler SystemManagementClick;
        /// <summary>
        /// 数据管理 
        /// </summary>
        public event EventHandler DataManagementClick;
        /// <summary>
        /// 经营分析
        /// </summary>
        public event EventHandler BusinessAnalysisClick;
        /// <summary>
        /// 客户服务
        /// </summary>
        public event EventHandler CustomerServiceClick;
        /// <summary>
        /// 财务管理
        /// </summary>
        public event EventHandler FinancialManagementClick;
        /// <summary>
        /// 配件业务
        /// </summary>
        public event EventHandler AccessoriesBusinessClick;
        /// <summary>
        /// 维修业务
        /// </summary>
        public event EventHandler RepairBusinessClick;

        public UCMenuBar()
        {
            InitializeComponent();

            #region 系统管理
            this.ibtnSystemManagement.ImageDown = ServiceStationClient.Skin.Properties.Resources.icon_7_d;
            this.ibtnSystemManagement.ImageHover = ServiceStationClient.Skin.Properties.Resources.icon_7_d;
            this.ibtnSystemManagement.ImageNormal = ServiceStationClient.Skin.Properties.Resources.icon_7;
            this.ibtnSystemManagement.ButtonStatus = ServiceStationClient.ComponentUI.ImageButton.Status.Normal;
            this.ibtnSystemManagement.BorderStyle = System.Windows.Forms.BorderStyle.None;

            //this.tsMenuItemMonitor.Image = ServiceStationClient.Skin.Properties.Resources.icon_menu_monitor;
            //this.tsMenuItemTrack.Image = ServiceStationClient.Skin.Properties.Resources.icon_menu_track;
            //this.tsMenuItemImage.Image = ServiceStationClient.Skin.Properties.Resources.icon_menu_capture_image;
            #endregion

            #region 数据管理
            this.ibtnDataManagement.ImageDown = ServiceStationClient.Skin.Properties.Resources.icon_6_d;
            this.ibtnDataManagement.ImageHover = ServiceStationClient.Skin.Properties.Resources.icon_6_d;
            this.ibtnDataManagement.ImageNormal = ServiceStationClient.Skin.Properties.Resources.icon_6;
            this.ibtnDataManagement.ButtonStatus = ServiceStationClient.ComponentUI.ImageButton.Status.Normal;
            this.ibtnDataManagement.BorderStyle = System.Windows.Forms.BorderStyle.None;
            // this.tsMenuItemRealPlay.Image = ServiceStationClient.Skin.Properties.Resources.icon_menu_video_monitor;
            // this.tsMenuItemPlayBack.Image = ServiceStationClient.Skin.Properties.Resources.icon_menu_video_playback;
            #endregion

            #region 经营分析
            this.ibtnBusinessAnalysis.ImageDown = ServiceStationClient.Skin.Properties.Resources.icon_5_d;
            this.ibtnBusinessAnalysis.ImageHover = ServiceStationClient.Skin.Properties.Resources.icon_5_d;
            this.ibtnBusinessAnalysis.ImageNormal = ServiceStationClient.Skin.Properties.Resources.icon_5;
            this.ibtnBusinessAnalysis.ButtonStatus = ServiceStationClient.ComponentUI.ImageButton.Status.Normal;
            this.ibtnBusinessAnalysis.BorderStyle = System.Windows.Forms.BorderStyle.None;

            //this.tsMenuItemHistoryAlarm.Image = ServiceStationClient.Skin.Properties.Resources.icon_menu_history_alarm;
            //this.tsMenuItemOperateStatistic.Image = ServiceStationClient.Skin.Properties.Resources.icon_menu_operate_statistic;
            //this.tsMenuItemOperateStatistic.HideDropDown();
            #endregion

            #region 客户服务
            this.ibtnCustomerService.ImageDown = ServiceStationClient.Skin.Properties.Resources.icon_4_d;
            this.ibtnCustomerService.ImageHover = ServiceStationClient.Skin.Properties.Resources.icon_4_d;
            this.ibtnCustomerService.ImageNormal = ServiceStationClient.Skin.Properties.Resources.icon_4;
            this.ibtnCustomerService.ButtonStatus = ServiceStationClient.ComponentUI.ImageButton.Status.Normal;
            this.ibtnCustomerService.BorderStyle = System.Windows.Forms.BorderStyle.None;

            //this.tsMenuItemSysConfig.Image = ServiceStationClient.Skin.Properties.Resources.icon_menu_system_config;
            //this.tsMenuItemCaptureConfig.Image = ServiceStationClient.Skin.Properties.Resources.icon_menu_capture_config;
            #endregion

            #region 财务管理
            this.ibtnFinancialManagement.ImageDown = ServiceStationClient.Skin.Properties.Resources.icon_3_d;
            this.ibtnFinancialManagement.ImageHover = ServiceStationClient.Skin.Properties.Resources.icon_3_d;
            this.ibtnFinancialManagement.ImageNormal = ServiceStationClient.Skin.Properties.Resources.icon_3;
            this.ibtnFinancialManagement.ButtonStatus = ServiceStationClient.ComponentUI.ImageButton.Status.Normal;
            this.ibtnFinancialManagement.BorderStyle = System.Windows.Forms.BorderStyle.None;
            #endregion

            #region 配件业务
            this.ibtnAccessoriesBusiness.ImageDown = ServiceStationClient.Skin.Properties.Resources.icon_2_d;
            this.ibtnAccessoriesBusiness.ImageHover = ServiceStationClient.Skin.Properties.Resources.icon_2_d;
            this.ibtnAccessoriesBusiness.ImageNormal = ServiceStationClient.Skin.Properties.Resources.icon_2;
            this.ibtnAccessoriesBusiness.ButtonStatus = ServiceStationClient.ComponentUI.ImageButton.Status.Normal;
            this.ibtnAccessoriesBusiness.BorderStyle = System.Windows.Forms.BorderStyle.None;
            #endregion

            #region 维修业务
            this.ibtnRepairBusiness.ImageDown = ServiceStationClient.Skin.Properties.Resources.icon_1_d;
            this.ibtnRepairBusiness.ImageHover = ServiceStationClient.Skin.Properties.Resources.icon_1_d;
            this.ibtnRepairBusiness.ImageNormal = ServiceStationClient.Skin.Properties.Resources.icon_1;
            this.ibtnRepairBusiness.ButtonStatus = ServiceStationClient.ComponentUI.ImageButton.Status.Normal;
            this.ibtnRepairBusiness.BorderStyle = System.Windows.Forms.BorderStyle.None;
            #endregion
        }
        #region Tag属性
        public string SystemManagement_Tag
        {
            get
            {
                return SystemManagement;
            }
            set
            {
                SystemManagement = ibtnSystemManagement.Tag.ToString();
            }
        }

        public string DataManagement_Tag
        {
            get
            {
                return DataManagement;
            }
            set
            {
                DataManagement = ibtnDataManagement.Tag.ToString();
            }
        }

        public string BusinessAnalysis_Tag
        {
            get
            {
                return BusinessAnalysis;
            }
            set
            {
                BusinessAnalysis = ibtnBusinessAnalysis.Tag.ToString();
            }
        }

        public string CustomerService_Tag
        {
            get
            {
                return CustomerService;
            }
            set
            {
                CustomerService = ibtnCustomerService.Tag.ToString();
            }
        }

        public string FinancialManagement_Tag
        {
            get
            {
                return FinancialManagement;
            }
            set
            {
                FinancialManagement = ibtnFinancialManagement.Tag.ToString();
            }
        }

        public string AccessoriesBusiness_Tag
        {
            get
            {
                return AccessoriesBusiness;
            }
            set
            {
                AccessoriesBusiness = ibtnAccessoriesBusiness.Tag.ToString();
            }
        }
        public string RepairBusiness_Tag
        {
            get
            {
                return RepairBusiness;
            }
            set
            {
                RepairBusiness = ibtnRepairBusiness.Tag.ToString();
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
        /// 系统管理 按钮点击事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnSystemManagement_Click(object sender, EventArgs e)
        {
            imgBtnCurrent = sender as ImageButton;
            if (SystemManagementClick != null)
            {
                SystemManagementClick(sender, e);
            }
            ButtonStatus();
            //SetMainFormTopMost(SYSModel.clsSysConfig.STR_CS_MEMU_USERMANAGE);//执行委托实例 
            Action<string, string> OpLog = CommonClass.DBHelper.LogFunctionCall;

            OpLog.BeginInvoke(imgBtnCurrent.Tag.ToString(), GlobalStaticObj.CurrAccID, null, null);
        }
        /// <summary>
        /// 数据管理 按钮点击事件  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnDataManagement_Click(object sender, EventArgs e)
        {
            imgBtnCurrent = sender as ImageButton;
            if (DataManagementClick != null)
            {
                DataManagementClick(sender, e);
            }
            ButtonStatus();
            Action<string, string> OpLog = CommonClass.DBHelper.LogFunctionCall;
            OpLog.BeginInvoke(imgBtnCurrent.Tag.ToString(), GlobalStaticObj.CurrAccID, null, null);
        }
        /// <summary>
        /// 经营分析 按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnBusinessAnalysis_Click(object sender, EventArgs e)
        {
            imgBtnCurrent = sender as ImageButton;
            if (BusinessAnalysisClick != null)
            {
                BusinessAnalysisClick(sender, e);
            }
            ButtonStatus();
            Action<string, string> OpLog = CommonClass.DBHelper.LogFunctionCall;
            OpLog.BeginInvoke(imgBtnCurrent.Tag.ToString(), GlobalStaticObj.CurrAccID, null, null);
        }

        /// <summary>
        /// 客户服务 按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnCustomerService_Click(object sender, EventArgs e)
        {
            imgBtnCurrent = sender as ImageButton;
            if (CustomerServiceClick != null)
            {
                CustomerServiceClick(sender, e);
            }
            ButtonStatus();
            Action<string, string> OpLog = CommonClass.DBHelper.LogFunctionCall;
            OpLog.BeginInvoke(imgBtnCurrent.Tag.ToString(), GlobalStaticObj.CurrAccID, null, null);
        }
        /// <summary>
        /// 财务管理 按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnFinancialManagement_Click(object sender, EventArgs e)
        {
            imgBtnCurrent = sender as ImageButton;
            if (FinancialManagementClick != null)
            {
                FinancialManagementClick(sender, e);
            }
            ButtonStatus();
            Action<string, string> OpLog = CommonClass.DBHelper.LogFunctionCall;
            OpLog.BeginInvoke(imgBtnCurrent.Tag.ToString(), GlobalStaticObj.CurrAccID, null, null);
        }
        /// <summary>
        /// 配件业务 按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnAccessoriesBusiness_Click(object sender, EventArgs e)
        {
            imgBtnCurrent = sender as ImageButton;
            if (AccessoriesBusinessClick != null)
            {
                AccessoriesBusinessClick(sender, e);
            }
            ButtonStatus();
            Action<string, string> OpLog = CommonClass.DBHelper.LogFunctionCall;
            OpLog.BeginInvoke(imgBtnCurrent.Tag.ToString(), GlobalStaticObj.CurrAccID, null, null);
        }
        /// <summary>
        /// 维修业务 按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnRepairBusiness_Click(object sender, EventArgs e)
        {
            imgBtnCurrent = sender as ImageButton;
            if (RepairBusinessClick != null)
            {
                RepairBusinessClick(sender, e);
            }
            ButtonStatus();
            Action<string, string> OpLog = CommonClass.DBHelper.LogFunctionCall;
            OpLog.BeginInvoke(imgBtnCurrent.Tag.ToString(), GlobalStaticObj.CurrAccID, null, null);
        }

        private void UCMenuBar_Load(object sender, EventArgs e)
        {
            if (this.loadFlag)
            {
                this.loadFlag = false;

                if (HXCPcClient.GlobalStaticObj.gLoginDataSet != null
                    && HXCPcClient.GlobalStaticObj.gLoginDataSet.Tables.Count > 1
                    && HXCPcClient.GlobalStaticObj.gLoginDataSet.Tables[2].Rows.Count > 0)
                {
                    //alter by 杨超逸
                    //2015.1.8
                    //修改权限测试
                    //DataView dv = new DataView();
                    //dv = HXCPcClient.GlobalStaticObj.gLoginDataSet.Tables[2].DefaultView;
                    //dv.RowFilter = "parent_id='CL_ROOT'";
                    //DataTable dt = dv.ToTable();
                    DataTable dt = HXCPcClient.GlobalStaticObj.gLoginDataSet.Tables[2];
                    //alter end
                    //TODO:后期将此代码段优化为动态设置按钮显隐
                    ibtnSystemManagement.Visible = CheckPermission(dt, ibtnSystemManagement.Tag.ToString());
                    ibtnDataManagement.Visible = CheckPermission(dt, ibtnDataManagement.Tag.ToString());
                    ibtnBusinessAnalysis.Visible = CheckPermission(dt, ibtnBusinessAnalysis.Tag.ToString());
                    ibtnCustomerService.Visible = CheckPermission(dt, ibtnCustomerService.Tag.ToString());
                    ibtnFinancialManagement.Visible = CheckPermission(dt, ibtnFinancialManagement.Tag.ToString());
                    ibtnAccessoriesBusiness.Visible = CheckPermission(dt, ibtnAccessoriesBusiness.Tag.ToString());
                    ibtnRepairBusiness.Visible = CheckPermission(dt, ibtnRepairBusiness.Tag.ToString());
                }
                else
                {
                    ibtnSystemManagement.Visible = false;
                    ibtnDataManagement.Visible = false;
                    ibtnBusinessAnalysis.Visible = false;
                    ibtnCustomerService.Visible = false;
                    ibtnFinancialManagement.Visible = false;
                    ibtnAccessoriesBusiness.Visible = false;
                    ibtnRepairBusiness.Visible = false;
                }
            }
        }

        private bool CheckPermission(DataTable dt, string fun_id)
        {
            bool bln = false;
            DataView dv = new DataView();

            //alter by 杨超逸
            //2015.1.8
            //修改权限测试
            // dv = dt.DefaultView;
            //dv.RowFilter = "fun_id='" + fun_id + "'";
            //dt = dv.ToTable();
            //if (dt.Rows.Count > 0)
            //{
            //    bln = true;
            //}

            DataRow[] dr = dt.Select("fun_id='" + fun_id + "' or parent_id= '" + fun_id + "'");
            if (dr.Length > 0)
            {
                bln = true;
            }
            //alter end


            return bln;
        }

        /// <summary>
        /// 获得首个显示的控件ID
        /// </summary>
        /// <returns>显示控件的ID</returns>
        public string GetFirstVisibleIbtnID()
        {
            for (int i = flpnlContainer.Controls.Count - 1; i >= 0; i--)
            {
                if (flpnlContainer.Controls[i].Visible)
                {
                    ImageButton imageBtn = flpnlContainer.Controls[i] as ImageButton;
                    if (imageBtn != null && imageBtn.Tag != null)
                    {
                        return imageBtn.Tag.ToString();
                    }
                }
            }
            //foreach (Control item in flpnlContainer.Controls)
            //{
            //    if (item.Visible)
            //    {
            //        ImageButton imageBtn = item as ImageButton;
            //        if (imageBtn != null && imageBtn.Tag != null)
            //        {
            //            return imageBtn.Tag.ToString();
            //        }
            //    }
            //}
            return string.Empty;
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
        //public HXCPcClient.FunctionName CheckPermission()
        //{
        //    HXCPcClient.FunctionName menuFunctionName = HXCPcClient.FunctionName.None;
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
