using System;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.ServiceProcess;
using HXC_FuncUtility;
using BLL;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using CloudPlatSocket;
using SYSModel;
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
        Thread ControlWCFServiceThread = null;
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

        /// <summary> 数据库服务状态
        /// </summary>
        private void btnDb_Click(object sender, EventArgs e)
        {

        }

        /// <summary> 宇通服务状态
        /// </summary>
        private void btnYutong_Click(object sender, EventArgs e)
        {

        }

        private void btnRepairBill_Click(object sender, EventArgs e)
        {
            #region 已结算维修单
            string fileds = @"b.complete_work_time,a.create_time clearing_time,c.cust_crm_guid,b.customer_name,b.driver_mobile,b.driver_name,b.engine_no,a.fitting_sum,
a.fitting_sum_money,b.link_man_mobile,b.linkman,b.maintain_no,b.maintain_type,a.man_hour_sum,
a.man_hour_sum_money,a.other_item_sum,a.other_item_tax_cost,a.privilege_cost,a.received_sum,
b.remark,a.should_sum,b.travel_mileage,d.dic_name vehicle_brand,vehicle_model,vehicle_no,vehicle_vin,b.maintain_id";
            string table = @"tb_maintain_settlement_info a 
inner join tb_maintain_info b on a.maintain_id=b.maintain_id
inner join tb_customer c on b.customer_id=c.cust_id
left join sys_dictionaries d on b.vehicle_brand=d.dic_id";
            DataTable dt = DBHelper.GetTable("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, table, fileds, "", "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBoxEx.Show("没有数据");
            }
            foreach (DataRow dr in dt.Rows)
            {
                Model.repairbill bill = new Model.repairbill();
                bill.clearing_time = "";
                bill.complete_work_time = Utility.Common.Common.UtcLongToLocalDateTime(dr["complete_work_time"]);
                bill.cost_types = "";
                bill.cust_id = dr["cust_crm_guid"].ToString();//客户CRM_ID
                bill.cust_name = dr["customer_name"].ToString();
                bill.dispatch_time = "";
                bill.driver_mobile = dr["driver_mobile"].ToString();
                bill.driver_name = dr["driver_name"].ToString();
                bill.engine_no = dr["engine_no"].ToString();
                bill.fitting_sum = dr["fitting_sum"].ToString();
                bill.fitting_sum_money = dr["fitting_sum_money"].ToString();
                bill.link_man_mobile = dr["link_man_mobile"].ToString();
                bill.linkman = dr["linkman"].ToString();
                bill.maintain_no = dr["maintain_no"].ToString();
                bill.maintain_type = dr["maintain_type"].ToString();
                bill.man_hour_sum = dr["man_hour_sum"].ToString();
                bill.man_hour_sum_money = dr["man_hour_sum_money"].ToString();
                bill.other_item_sum = dr["other_item_sum"].ToString();
                bill.other_item_tax_cost = dr["other_item_tax_cost"].ToString();
                bill.other_remarks = "";
                bill.privilege_cost = dr["privilege_cost"].ToString();
                bill.received_sum = dr["received_sum"].ToString();
                bill.remark = dr["remark"].ToString();
                bill.should_sum = dr["should_sum"].ToString();
                bill.shut_down_time = "";
                bill.start_work_time = "";
                bill.sum_money = "";
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
                        detail.unit = drDetail["unit"].ToString();
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
            //获取宇通加密秘钥
            yuTongWebService.GlobalStaticObj_YT.KeySecurity_YT = DBHelper.GetSingleValue("获取服务站省份", dbName, "sys_config", "key_value", "key_name='KeySecurity_YT'", "");
            //获取服务站所长省份，cxz
            GlobalStaticObj_Server.Instance.ServiceStationProvince = DBHelper.GetSingleValue("获取服务站省份", dbName, "tb_company", "province",
               string.Format("data_source='2' and sap_code='{0}'", yuTongWebService.GlobalStaticObj_YT.SAPCode), "");
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


        #region --按钮操作

        #endregion
    }
}
