using System;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.ServiceProcess;
using System.Collections.Generic;
using HXC_FuncUtility;
using BLL;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using CloudPlatSocket;
using SYSModel;
using HXCServerWinForm.CommonClass;
namespace HXCServerWinForm.UCForm
{
    public partial class UUCHomePage : UserControl
    {
        #region --UI交互代理
        private delegate void UiHandler();
        private UiHandler uiHandler;
        #endregion

        #region --成员变量
        public string ID;
        #endregion

        #region --构造函数
        public UUCHomePage()
        {
            InitializeComponent();
        }
        #endregion

        #region --窗体初始化
        private void UCHomePage_Load(object sender, EventArgs e)
        {
            btnServer_Click(null, null);
            BindDatatoFrm();

            #region --启动云平台通讯服务
            this.uiHandler += new UiHandler(this.InitCloundService);
            ThreadPool.QueueUserWorkItem(new WaitCallback(this._StartCloundService));
            #endregion
        }
        #endregion

        #region 事件
        /// <summary> wcf服务启停用
        /// </summary>
        private void btnServer_Click(object sender, EventArgs e)
        {
            if (btnServer.Text == "挂起")
            {
                WcfServiceProxy.WcfServiceProxy.Instance.StopWcfService();
                picServer.Image = HXCServerWinForm.Properties.Resources.stop;
                btnServer.Text = "启用";
            }
            else
            {
                bool flag = WcfServiceProxy.WcfServiceProxy.Instance.StartWcfService();
                if (flag)
                {
                    picServer.Image = HXCServerWinForm.Properties.Resources.runing;
                    btnServer.Text = "挂起";
                }
            }
        }

        /// <summary> 定时器（一分钟）
        /// </summary>
        private void timerCheckDbConn_Tick(object sender, EventArgs e)
        {
            if (btnDb.Enabled)
            {
                return;
            }
            btnDb_Click(null, null);
        }

        /// <summary> 定时器（半小时）
        /// </summary>
        private void timerBackupPlan_Tick(object sender, EventArgs e)
        {
            if (GlobalStaticObj_Server.Instance.CurrentDateTime.Hour > 2 && GlobalStaticObj_Server.Instance.CurrentDateTime.Hour < 4)
            {
                bool isAllBackUp = false;
                List<string> list = new List<string>();
                #region 记录需要备份的账套
                foreach (string str in GlobalStaticObj_Server.Instance.DicBackupPlan.Keys)
                {
                    string[] intArr = GlobalStaticObj_Server.Instance.DicBackupPlan[str];
                    DateTime dtStart = Common.UtcLongToLocalDateTime(Convert.ToInt64(intArr[2]));
                    bool isBackup = false;
                    if (intArr[3] == "")
                    {
                        isBackup = true;
                    }
                    else
                    {
                        DateTime dtLastBackupTime = Common.UtcLongToLocalDateTime(Convert.ToInt64(intArr[3]));
                        if (Convert.ToInt32(intArr[0]) == (int)DataSources.EnumAutoBackupType.EveryDay)
                        {
                            if ((GlobalStaticObj_Server.Instance.CurrentDateTime - dtLastBackupTime).Days > Convert.ToInt32(intArr[1]))
                            {
                                isBackup = true;
                            }
                        }
                        else if (Convert.ToInt32(intArr[0]) == (int)DataSources.EnumAutoBackupType.EveryWeek)
                        {
                            int weekEnum = (int)GlobalStaticObj_Server.Instance.CurrentDateTime.DayOfWeek;
                            if (weekEnum == 0)
                            {
                                weekEnum = 7;
                            }
                            if (Convert.ToInt32(intArr[1]) == weekEnum)
                            {
                                if (Common.GetWeekOfYear(dtLastBackupTime) != Common.GetWeekOfYear(GlobalStaticObj_Server.Instance.CurrentDateTime))
                                {
                                    isBackup = true;
                                }
                            }
                        }
                        else if (Convert.ToInt32(intArr[0]) == (int)DataSources.EnumAutoBackupType.EveryMonth && GlobalStaticObj_Server.Instance.CurrentDateTime.Day == Convert.ToInt32(intArr[1]))
                        {
                            if (dtLastBackupTime.Date != GlobalStaticObj_Server.Instance.CurrentDateTime.Date)
                            {
                                isBackup = true;
                            }
                        }
                    }
                    if (isBackup)
                    {
                        if (str == "")
                        {
                            DataTable dt = DBHelper.GetTable("获取账套信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_setbook", "*", "enable_flag=1", "", "order by setbook_code");
                            foreach (DataRow drAcc in dt.Rows)
                            {
                                string accCode = drAcc["setbook_code"].ToString();
                                if (!list.Contains(accCode))
                                {
                                    list.Add(accCode);
                                }
                            }
                            isAllBackUp = true;
                            break;
                        }
                        else
                        {
                            if (!list.Contains(str))
                            {
                                list.Add(str);
                            }
                        }
                    }
                }
                #endregion

                #region 开始备份
                DateTime dtBakTime = GlobalStaticObj_Server.Instance.CurrentDateTime;
                bool isSuccess = true;
                foreach (string accCode in list)
                {
                    string bak_filename = accCode + dtBakTime.ToString("yyMMdd") + ".bak";
                    string errMsg = CommonUtility.BackupDb(accCode, bak_filename);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        isSuccess = false;
                    }
                }
                #endregion

                #region
                //如果是备份 所有账套，记录备份记录
                if (isAllBackUp)
                {
                    Dictionary<string, string> dicFields = new Dictionary<string, string>();
                    dicFields.Add("bak_id", Guid.NewGuid().ToString());
                    dicFields.Add("bak_acccode", "---");
                    dicFields.Add("bak_filename", "所有账套");
                    dicFields.Add("is_success", Convert.ToInt32(isSuccess).ToString());
                    dicFields.Add("bak_method", Convert.ToInt32(DataSources.EnumBackupMethod.ManualBackup).ToString());
                    dicFields.Add("bak_time", Common.LocalDateTimeToUtcLong(dtBakTime).ToString());
                    dicFields.Add("bak_by", GlobalStaticObj_Server.Instance.UserID);
                    DBHelper.Submit_AddOrEdit("新建备份记录", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_backup_record", "", "", dicFields);
                }
                #endregion
            }
        }

        /// <summary> 数据库服务状态
        /// </summary>
        private void btnDb_Click(object sender, EventArgs e)
        {
            try
            {
                DBHelper.GetCurrentTime(GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode);
                btnDb.Text = "挂起";
                btnDb.Enabled = false;
                picdb.Image = HXCServerWinForm.Properties.Resources.runing;
            }
            catch (Exception ex)
            {
                btnDb.Text = "启用";
                btnDb.Enabled = true;
                picdb.Image = HXCServerWinForm.Properties.Resources.stop;
                Utility.Log.Log.writeLineToLog("【数据库】" + ex.Message, "数据库连接");
                MessageBoxEx.Show("数据服务启用失败,请再次尝试");
            }
        }

        /// <summary> 宇通服务状态
        /// </summary>
        private void btnYutong_Click(object sender, EventArgs e)
        {

        }

        private void btnRepairBill_Click(object sender, EventArgs e)
        {
            #region 已结算维修单
            string fileds = @"b.complete_work_time,a.create_time clearing_time,c.cust_crm_guid,b.customer_name,e.dispatch_time,b.driver_mobile,b.driver_name,b.engine_no,a.fitting_sum,
a.fitting_sum_money,b.link_man_mobile,b.linkman,b.maintain_no,h.dic_name maintain_type,a.man_hour_sum,
a.man_hour_sum_money,a.other_item_sum,a.other_item_tax_cost,a.privilege_cost,a.received_sum,
b.remark,a.should_sum,g.shut_down_time,g.start_work_time,f.sum_money,b.travel_mileage,d.dic_name vehicle_brand,vehicle_model,vehicle_no,vehicle_vin,b.maintain_id";
            string table = @"tb_maintain_settlement_info a 
inner join tb_maintain_info b on a.maintain_id=b.maintain_id
inner join tb_customer c on b.customer_id=c.cust_id
left join sys_dictionaries d on b.vehicle_brand=d.dic_id
left join (
select maintain_id,min(create_time) dispatch_time from tb_maintain_dispatch_worker group by maintain_id
) e on a.maintain_id=e.maintain_id
left join (
select maintain_id,sum(sum_money) sum_money from tb_maintain_other_toll group by maintain_id
) f on a.maintain_id=f.maintain_id
left join (
select maintain_id,min(start_work_time) start_work_time,MAX(shut_down_time) shut_down_time from tb_maintain_item group by maintain_id
) g on a.maintain_id=g.maintain_id
left join sys_dictionaries h on b.maintain_type=h.dic_id";
            DataTable dt = DBHelper.GetTable("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, table, fileds, "", "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBoxEx.Show("没有数据");
            }
            foreach (DataRow dr in dt.Rows)
            {
                Model.repairbill bill = new Model.repairbill();
                bill.clearing_time = Common.UtcLongToLocalDateTime(dr["clearing_time"]);//结算时间
                bill.complete_work_time = Utility.Common.Common.UtcLongToLocalDateTime(dr["complete_work_time"]);
                bill.cost_types = "其他费用合计";//其他费用类别
                bill.cust_id = dr["cust_crm_guid"].ToString();//客户CRM_ID
                bill.cust_name = dr["customer_name"].ToString();
                bill.dispatch_time = Common.UtcLongToLocalDateTime(dr["dispatch_time"]);//派工时间
                bill.driver_mobile = dr["driver_mobile"].ToString();
                bill.driver_name = dr["driver_name"].ToString();
                bill.engine_no = dr["engine_no"].ToString();
                bill.fitting_sum = dr["fitting_sum"].ToString();
                bill.fitting_sum_money = dr["fitting_sum_money"].ToString();//配件货款
                bill.link_man_mobile = dr["link_man_mobile"].ToString();
                bill.linkman = dr["linkman"].ToString();
                bill.maintain_no = dr["maintain_no"].ToString();
                bill.maintain_type = dr["maintain_type"].ToString();//维修类别
                bill.man_hour_sum = dr["man_hour_sum"].ToString();
                bill.man_hour_sum_money = dr["man_hour_sum_money"].ToString();
                bill.other_item_sum = dr["other_item_sum"].ToString();
                bill.other_item_tax_cost = dr["other_item_tax_cost"].ToString();
                bill.other_remarks = "";//其他费用备注
                bill.privilege_cost = dr["privilege_cost"].ToString();
                bill.received_sum = dr["received_sum"].ToString();
                bill.remark = dr["remark"].ToString();
                bill.should_sum = dr["should_sum"].ToString();
                bill.shut_down_time = Common.UtcLongToLocalDateTime(dr["shut_down_time"]);//停工时间
                bill.start_work_time = Common.UtcLongToLocalDateTime(dr["start_work_time"]);//开工时间
                bill.sum_money = CommonCtrl.IsNullToString(dr["sum_money"]);//其他费用金额
                bill.travel_mileage = dr["travel_mileage"].ToString();
                bill.vehicle_brand = dr["vehicle_brand"].ToString();
                bill.vehicle_model = dr["vehicle_model"].ToString();
                bill.vehicle_no = dr["vehicle_no"].ToString();
                bill.vehicle_vin = dr["vehicle_vin"].ToString();
                #region 维修用料
                string detailWhere = string.Format("maintain_id='{0}'", dr["maintain_id"]);
                string detailTable = "tb_maintain_material_detail a left join tb_parts b on a.parts_code=b.ser_parts_code";
                DataTable dtDetail = DBHelper.GetTable("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, detailTable, "a.*,b.car_parts_code", detailWhere, "", "");
                if (dtDetail == null || dtDetail.Rows.Count == 0)
                {
                    bill.RepairmaterialDetails = new Model.RepairmaterialDetail[0];
                }
                else
                {
                    int detailCount = dtDetail.Rows.Count;
                    bill.RepairmaterialDetails = new Model.RepairmaterialDetail[detailCount];
                    for (int i = 0; i < detailCount; i++)
                    {
                        DataRow drDetail = dtDetail.Rows[i];
                        Model.RepairmaterialDetail detail = new Model.RepairmaterialDetail();
                        detail.car_parts_code = drDetail["car_parts_code"].ToString();
                        detail.norms = drDetail["norms"].ToString();
                        detail.parts_name = drDetail["parts_name"].ToString();
                        detail.parts_remarks = drDetail["remarks"].ToString();
                        detail.quantity = drDetail["quantity"].ToString();
                        detail.sum_money = drDetail["sum_money"].ToString();
                        detail.three_warranty = drDetail["three_warranty"].ToString();
                        detail.unit = drDetail["unit"].ToString();//配件单位
                        detail.unit_price = drDetail["unit_price"].ToString();
                        detail.vehicle_brand = drDetail["vehicle_brand"].ToString();
                        bill.RepairmaterialDetails[i] = detail;
                    }
                }
                #endregion
                #region 维修项目
                string itemWhere = string.Format("maintain_id='{0}'", dr["maintain_id"]);
                DataTable dtItem = DBHelper.GetTable("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_maintain_item", "*", itemWhere, "", "");
                if (dtItem == null || dtItem.Rows.Count == 0)
                {
                    bill.RepairProjectDetails = new Model.RepairProjectDetail[0];
                }
                else
                {
                    int itemCount = dtItem.Rows.Count;
                    bill.RepairProjectDetails = new Model.RepairProjectDetail[itemCount];
                    for (int i = 0; i < itemCount; i++)
                    {
                        DataRow drItem = dtItem.Rows[i];
                        Model.RepairProjectDetail item = new Model.RepairProjectDetail();
                        item.item_name = drItem["item_name"].ToString();
                        item.item_no = drItem["item_no"].ToString();
                        item.item_remarks = drItem["remarks"].ToString();
                        item.item_type = drItem["item_type"].ToString();
                        item.man_hour_norm_unitprice = drItem["man_hour_norm_unitprice"].ToString();
                        item.man_hour_quantity = drItem["man_hour_quantity"].ToString();
                        item.man_hour_type = drItem["man_hour_type"].ToString();
                        item.sum_money_goods = drItem["sum_money_goods"].ToString();
                        item.three_warranty = drItem["three_warranty"].ToString();
                        bill.RepairProjectDetails[i] = item;
                    }
                }
                #endregion
            #endregion

                string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(bill);
                bool flag = yuTongWebService.WebServ_YT_BusiData.UpLoadRepairBill(jsonStr);
                if (!flag)
                {
                    MessageBoxEx.Show("接口调用失败", "系统提示");
                }
            }
        }

        /// <summary> 定时器，在线用户数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerOnlineCount_Tick(object sender, EventArgs e)
        {
            lbonlineuser.Text = LoginSessionInfo.Instance.dicLoginInfos.Count.ToString();
        }

        #region --宇通相关
        /// <summary> 修改个人密码
        /// </summary>
        private void llblPersionSet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersionSet frm = new frmPersionSet();
            frm.ShowDialog();
        }

        /// <summary> 重新注册
        /// </summary>
        private void llblReg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UCForm.frmSoftReg regForm = new UCForm.frmSoftReg();
            regForm.StartPosition = FormStartPosition.CenterScreen;
            DialogResult result = regForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                BindSignInfo();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //yuTongWebService.WebServ_YT_BasicData.InitData_Bus();
            //yuTongWebService.WebServ_YT_BasicData.InitData_Customer();
            //yuTongWebService.WebServ_YT_BasicData.InitData_Part();
            //yuTongWebService.WebServ_YT_BusiData.LoadPartInStore(null);
            //yuTongWebService.WebServ_YT_BusiData.LoadPartRetureStatus("60023858");
            //yuTongWebService.WebServ_YT_BusiData.LoadPartRetureStatus("60024256");
            yuTongWebService.WebServ_YT_BusiData.UpLoadPartPutStore("RK00085457");
        }

        private void btnInitContact_Click(object sender, EventArgs e)
        {
            string errMsg = yuTongWebService.WebServ_YT_BasicData.InitData_Contact();
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBoxEx.Show(errMsg, "系统提示");
            }
        }
        private void btnInitBus_Click(object sender, EventArgs e)
        {
            string errMsg = yuTongWebService.WebServ_YT_BasicData.InitData_Bus();
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBoxEx.Show(errMsg, "系统提示");
            }
        }

        private void btnInitBusCustom_Click(object sender, EventArgs e)
        {
            string errMsg = yuTongWebService.WebServ_YT_BasicData.InitData_Customer();
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBoxEx.Show(errMsg, "系统提示");
            }
        }

        private void btnPart_Click(object sender, EventArgs e)
        {
            string errMsg = yuTongWebService.WebServ_YT_BasicData.InitData_Part();
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBoxEx.Show(errMsg, "系统提示");
            }
        }

        private void btnHitchMode_Click(object sender, EventArgs e)
        {
            string errMsg = yuTongWebService.WebServ_YT_BasicData.LoadHitchMode();
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBoxEx.Show(errMsg, "系统提示");
            }
        }

        private void btnServiceStation_Click(object sender, EventArgs e)
        {
            string errMsg = yuTongWebService.WebServ_YT_BasicData.LoadServiceStation(GlobalStaticObj_Server.Instance.ClientID, yuTongWebService.GlobalStaticObj_YT.SAPCode, true);
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBoxEx.Show(errMsg, "系统提示");
            }
        }

        private void btnRepairProject_Click(object sender, EventArgs e)
        {
            string errMsg = yuTongWebService.WebServ_YT_BasicData.LoadRepairProject();
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBoxEx.Show(errMsg, "系统提示");
            }
        }

        private void btnBusModel_Click(object sender, EventArgs e)
        {
            string errMsg = yuTongWebService.WebServ_YT_BasicData.LoadBusModel();
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBoxEx.Show(errMsg, "系统提示");
            }
        }

        private void btnAddInfo_Click(object sender, EventArgs e)
        {
            GlobalStaticObj.AppMainForm.YTInterface();
        }
        #endregion
        #endregion

        #region 方法
        /// <summary> 设置TextBoxValue值
        /// </summary>
        private void SetTextBoxValue(string value, Control ctr)
        {
            Action<string> setValueAction = text => ctr.Text = text;//Action<T>本身就是delegate类型，省掉了delegate的定义
            if (ctr.InvokeRequired)
            {
                ctr.Invoke(setValueAction, value);
            }
            else
            {
                setValueAction(value);
            }
        }

        /// <summary> 界面值绑定
        /// </summary>
        public void BindDatatoFrm()
        {
            #region 基本信息
            lblwelcome.Text = string.Format("{0}({1})", GlobalStaticObj_Server.Instance.LoginName, GlobalStaticObj_Server.Instance.UserName);
            lblcom_name_u.Text = GlobalStaticObj_Server.Instance.ComName;
            lblorg_name.Text = GlobalStaticObj_Server.Instance.OrgName;
            lblrole_name.Text = GlobalStaticObj_Server.Instance.RoleName;
            lblogin_ip.Text = GlobalStaticObj_Server.Instance.LoginIP;
            lblogin_time.Text = GlobalStaticObj_Server.Instance.LoginTime;
            lblogin_ip.Text = GlobalStaticObj_Server.Instance.LoginIP;
            #endregion

            BindSignInfo();
            #region
            lbonlineuser.Text = "100";
            lbdbConn.Text = "";
            lbwcfPort.Text = GlobalStaticObj_Server.Instance.ServerPort.ToString();
            lbfilePort.Text = GlobalStaticObj_Server.Instance.FilePort.ToString();
            #endregion
        }

        /// <summary> 绑定签约信息
        /// </summary>
        public void BindSignInfo()
        {
            #region 软件信息
            DataTable dt = DBHelper.GetTable("获取软件用户信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_signing_info", "*", "", "", "");
            if (dt.Rows.Count == 0)
            {
                //弹出软件注册信息，如果用户取消注册，直接退出软件
                UCForm.frmSoftReg regForm = new UCForm.frmSoftReg();
                regForm.StartPosition = FormStartPosition.CenterScreen;
                DialogResult result = regForm.ShowDialog();
                if (result != DialogResult.OK)
                {
                    Application.Exit();
                    return;
                }
                else
                {
                    dt = DBHelper.GetTable("获取软件用户信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_signing_info", "*", "", "", "");
                }
            }

            DataRow dr = dt.Rows[0];
            lblauthentication_status.Text = dr["authentication_status"].ToString() == ((int)DataSources.EnumYesNo.Yes).ToString() ? "已授权" : "未授权";
            string dtStr = dr["protocol_expires_time"].ToString();
            if (!string.IsNullOrEmpty(dtStr))
            {
                long dateInt = Convert.ToInt64(dtStr);
                DateTime date = Common.UtcLongToLocalDateTime(dateInt);
                lblprotocol_expires_time.Text = date.ToString("yyyy-MM-dd");
            }
            else
            {
                lblprotocol_expires_time.Text = "无";
            }
            string version = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            lblver.Text = version.Substring(0, version.Length - 2);
            lblmachine_code_sequence.Text = dr["machine_code_sequence"].ToString();
            lblgrant_authorization.Text = dr["grant_authorization"].ToString();
            lblcom_name.Text = dr["com_name"].ToString();
            lblzip_code.Text = dr["zip_code"].ToString();
            string proviceCode = dr["province"].ToString();
            string cityCode = dr["city"].ToString();
            string countyCode = dr["county"].ToString();
            string detailAddr = dr["contact_address"].ToString();
            string province = "";
            if (!string.IsNullOrEmpty(proviceCode))
            {
                province = DBHelper.GetSingleValue("获取省份", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_area", "area_name", "area_code='" + proviceCode + "'", "");
            }
            string city = "";
            if (!string.IsNullOrEmpty(cityCode))
            {
                city = DBHelper.GetSingleValue("获取城市", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_area", "area_name", "area_code='" + cityCode + "'", "");
            }
            string county = "";
            if (!string.IsNullOrEmpty(countyCode))
            {
                county = DBHelper.GetSingleValue("获取区县", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_area", "area_name", "area_code='" + countyCode + "'", "");
            }
            lbCopanyAddress.Text = province + city + county + detailAddr;
            lblcontact.Text = dr["contact"].ToString();
            lblcontact_tel.Text = dr["contact_tel"].ToString();
            //lblcontact_tel.Text = dr["contact_phone"].ToString();
            lblemail.Text = dr["email"].ToString();
            lblfax.Text = dr["fax"].ToString();

            string dbName = GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode;
            yuTongWebService.GlobalStaticObj_YT.SAPCode = dr["service_station_sap"].ToString();
            GlobalStaticObj_Server.Instance.ClientID = dr["access_code"].ToString();

            //异步加载常量
            new Thread(new ThreadStart(
                delegate()
                {
                    //获取宇通加密秘钥
                    yuTongWebService.GlobalStaticObj_YT.KeySecurity_YT = DBHelper.GetSingleValue("获取服务站省份", dbName, "sys_config", "key_value", "key_name='KeySecurity_YT'", "");
                    //获取服务站所长省份，cxz
                    GlobalStaticObj_Server.Instance.ServiceStationProvince = DBHelper.GetSingleValue("获取服务站省份", dbName, "tb_company", "province",
                       string.Format("data_source='2' and sap_code='{0}'", yuTongWebService.GlobalStaticObj_YT.SAPCode), "");
                    DataTable dtBackupPlan = DBHelper.GetTable("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "v_sys_auto_backup_set", "*", "", "", "order by setbook_code");
                    foreach (DataRow row in dtBackupPlan.Rows)
                    {
                        string setbook_code = row["setbook_code"].ToString();
                        if (!GlobalStaticObj_Server.Instance.DicBackupPlan.ContainsKey(setbook_code))
                        {
                            string[] intArr = new string[4];
                            intArr[0] = row["auto_backup_type"].ToString();
                            intArr[1] = row["auto_backup_interval"].ToString();
                            intArr[2] = row["auto_backup_starttime"].ToString();
                            intArr[3] = "";
                            GlobalStaticObj_Server.Instance.DicBackupPlan.Add(setbook_code, intArr);
                        }
                    }
                }
                )).Start();
            #endregion
        }
        #endregion

        #region --云平台服务相关
        private void InitCloundService()
        {
            DataSources.EnumTaskStatus status = AutoTask.GetStatus();
            if (status == DataSources.EnumTaskStatus.Not_Started
                || status == DataSources.EnumTaskStatus.Suspend)
            {
                picCloadPlat.Image = HXCServerWinForm.Properties.Resources.stop;
                this.btnStartOrStop.Text = "启动";
            }
            else if (status == DataSources.EnumTaskStatus.Runing)
            {
                picCloadPlat.Image = HXCServerWinForm.Properties.Resources.runing;
                this.btnStartOrStop.Text = "挂起";
            }
        }

        /// <summary> 停止或者启动
        /// </summary>
        private void btnStartOrStop_Click(object sender, EventArgs e)
        {
            DataSources.EnumTaskStatus status = AutoTask.GetStatus();
            if (status == DataSources.EnumTaskStatus.Not_Started)
            {
                AutoTask.Start();
                picCloadPlat.Image = HXCServerWinForm.Properties.Resources.runing;
                this.btnStartOrStop.Text = "挂起";
            }
            else if (status == DataSources.EnumTaskStatus.Runing)
            {
                AutoTask.Stop();
                picCloadPlat.Image = HXCServerWinForm.Properties.Resources.runing;
                this.btnStartOrStop.Text = "挂起";
            }
            else if (status == DataSources.EnumTaskStatus.Suspend)
            {
                AutoTask.Continue();
                picCloadPlat.Image = HXCServerWinForm.Properties.Resources.stop;
                this.btnStartOrStop.Text = "启动";
            }
        }

        /// <summary> 运行日志
        /// </summary>
        private void btnCloundLog_Click(object sender, EventArgs e)
        {

        }

        /// <summary> 启动云平台服务
        /// </summary>
        /// <param name="state"></param>
        private void _StartCloundService(object state)
        {
            //鉴权码、授权码、公司名称、机器码
            DataTable dt = DBHelper.GetTable("获取注册信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_signing_info", "*", "", "", "");
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Columns.Contains("authentication"))
                {
                    GlobalStaticObj_Server.Instance.LicenseCode = dt.Rows[0]["authentication"].ToString();
                }
                if (dt.Columns.Contains("sign_id"))
                {
                    GlobalStaticObj_Server.Instance.StationID = dt.Rows[0]["sign_id"].ToString();
                }
                if (dt.Columns.Contains("s_user"))
                {
                    GlobalStaticObj_Server.Instance.Cloud_UserId = dt.Rows[0]["s_user"].ToString();
                }
                if (dt.Columns.Contains("s_pwd"))
                {
                    GlobalStaticObj_Server.Instance.Cloud_Password = dt.Rows[0]["s_pwd"].ToString();
                }
            }

            if (ServiceAgent.ServiceTest() || FileAgent.ServiceTest())
            {
                AutoTask.Start();
            }
            this.Invoke(this.uiHandler);
        }
        #endregion

    }
}
