using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm.RepairBusiness.RepairCallback;
using Newtonsoft.Json;
using ServiceStationClient.ComponentUI;
using SYSModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using Utility.Common;

namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuaranty
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     MaintainThreeGuarantyEdit         
    /// Author:       Kord
    /// Date:         2014/10/29 11:00:20
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	维修服务-宇通三包-三包服务单-新增/复制
    ///***************************************************************************//
    public partial class UCMaintainThreeGuarantyEdit : UCBase
    {
        #region Constructor -- 构造函数
        public UCMaintainThreeGuarantyEdit()
        {
            InitializeComponent();

            Load += (sender, args) =>
            {
                Init();
                BindData();
                if (windowStatus == WindowStatus.Add)
                {
                    if (cbo_receipt_type.Items.Count > 1)
                    {
                        cbo_receipt_type.SelectedIndex = 1;
                        cbo_receipt_type.SelectedIndex = 0;
                    }
                }
                dtp_repairs_time.Focus();
            };
        }
        #endregion

        #region Field -- 字段
        private String _idValue = String.Empty;
        private String _idColName = String.Empty;
        private String _newTgId = String.Empty;
        private String _orderStatus = String.Empty;
        private String _vmClass = String.Empty;
        #endregion

        #region Property -- 属性
        public UCMaintainThreeGuarantyManager UCForm { get; set; }
        public String BeforeOrderId { get; set; }
        /// <summary>
        /// 服务单ID
        /// </summary>
        public String TgId { get; set; }
        #endregion

        #region Method -- 方法
        private void Init() //初始化
        {
            SetFuncationButtonVisible();
            InitEventHandle();
            InitDataGridCellFormatting();
            InitControlDataSource();
            SumCostTimer();
        }
        private void SetFuncationButtonVisible() //设置功能按钮可见性
        {

            var btnCols = new ObservableCollection<ButtonEx_sms>
            {
                btnSave, btnImport, btnCancel
            };
            UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
        }
        private void InitEventHandle() //注册控件相关事件
        {
            btnSave.Click += (sender, args) => Save();
            btnCancel.Click += delegate
            {
                UCForm.BindPageData();
                deleteMenuByTag(Tag.ToString(), UCForm.Name);
            };
            btnImport.Click += delegate
            {
                var callbackImport = new UCRepairCallbackImport {MaintainThreeGuarantyEdit = this, strTag = "4"};
                callbackImport.ShowDialog();
            };
            cbo_whether_yt.SelectedIndexChanged += delegate  //是否宇通车
            {
                //if (cbo_receipt_type.SelectedValue == null) return;
                //var selectedValue4BillType = cbo_receipt_type.SelectedValue == null ? "" : cbo_receipt_type.SelectedValue.ToString().ToUpper();
                if (cbo_whether_yt.SelectedValue == null) return;
                var selectedValue = cbo_whether_yt.SelectedValue == null ? "" : cbo_whether_yt.SelectedValue.ToString().ToUpper();
                if (selectedValue == DbDic2Enum.TRUE)
                {
                    lbl__vehicle_vin.Visible = true;
                    lbl_depot_no.Visible = true;
                    lbl_engine_num.Visible = true;
                }
                else
                {
                    lbl__vehicle_vin.Visible = false;
                    lbl_depot_no.Visible = false;
                    lbl_engine_num.Visible = false;
                }
            };
            cbo_whether_go_out.SelectedIndexChanged += delegate   //根据是否外出不同显示不同的可填字段
            {
                if (cbo_whether_go_out.SelectedValue == null) return;
                var selectedValue = cbo_whether_go_out.SelectedValue == null ? "" : cbo_whether_go_out.SelectedValue.ToString().ToUpper();
                pnl_goout_info.Visible = selectedValue == DbDic2Enum.TRUE;  //外出情况
                dtp_repairs_time.Focus();
            };
            btnCommit.Click += delegate
            {
                //if (!CheckControlValue()) return;
                //上报厂家/宇通
                if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
                {
                    lbl_service_no.Text = CommonUtility.GetNewNo(DataSources.EnumProjectType.ThreeGuarantyService);
                }
                var result = SaveThreeGuarantyItem(DbDic2Enum.SYS_SERVICE_INFO_STATUS_YTJ);
                var id = windowStatus == WindowStatus.Edit ? TgId : _newTgId;
                if (UIAssistants.ThreeServiceAudit)
                {
                    if (result)
                    {
                        SaveAttachement(id);
                        SaveMaterialDetail(id);
                        if ((cbo_contain_man_hour_cost.SelectedValue == null ? "" : cbo_contain_man_hour_cost.SelectedValue.ToString().ToUpper()) == DbDic2Enum.TRUE)
                            SaveProjectData(id);
                        MessageBoxEx.Show("数据提交成功!", "操作提示");
                        return;
                    }
                    MessageBoxEx.Show("数据提交失败!", "操作提示");
                    return;
                }
                if (result)
                {
                    SaveAttachement(id);
                    SaveMaterialDetail(id);
                    if ((cbo_contain_man_hour_cost.SelectedValue == null
                        ? ""
                        : cbo_contain_man_hour_cost.SelectedValue.ToString().ToUpper()) == DbDic2Enum.TRUE)
                        SaveProjectData(id);
                    var resultStr = UCForm.Submit2Company(id, "100000001");
                    if (String.IsNullOrEmpty(resultStr))
                    {
                        MessageBoxEx.Show("三包服务单上报厂家成功!", "操作提示");
                    }
                    else
                    {
                        MessageBoxEx.Show(resultStr, "操作提示");
                    }
                }
                else
                {
                    MessageBoxEx.Show("上报厂家失败!", "操作提示");
                }
            };
            cbo_contain_man_hour_cost.SelectedIndexChanged += delegate   //根据是否包含工时费决定是否显示维修项目表
            {
                if (cbo_contain_man_hour_cost.SelectedValue == null) return;
                var selectedValueManHourCost = cbo_contain_man_hour_cost.SelectedValue == null ? "" : cbo_contain_man_hour_cost.SelectedValue.ToString().ToUpper();
                if (selectedValueManHourCost == DbDic2Enum.TRUE) //是
                {
                    if(!tabControlExBottom.TabPages.Contains(tb_wxxm))
                        tabControlExBottom.TabPages.Insert(0,tb_wxxm);
                    tabControlExBottom.SelectTab(tb_wxxm);
                }
                else
                {
                    if (tabControlExBottom.TabPages.Contains(tb_wxxm))
                        tabControlExBottom.TabPages.Remove(tb_wxxm);
                }
                dtp_repairs_time.Focus();
            };
            #region 根据宇通单据类型不同显示不同的可填字段
            cbo_receipt_type.SelectedIndexChanged += delegate   
            {
                pnl_promise_guarantee.Visible = false;  //维修 -- 特殊约定质保

                pnl_approver_name_yt.Visible = false;   //政策照顾 -- 宇通批准信息

                pnl_product_notice_no.Visible = false;  //产品改进 -- 产品改进通知

                pnl_cost_type_service_fwhd.Visible = false; //服务活动 -- 费用类型

                pnl_custome_property.Visible = true;
                pnl_cust_address.Visible = true;
                pnl_customer_postcode.Visible = true;
                pnl_linkman.Visible = true;
                pnl_link_man_mobi.Visible = true;
                pnl_vehicle_use_corp.Visible = true;
                pnl_vehicle_location.Visible = true;

                pnl_driving_license_no.Visible = true;
                pnl_travel_mileage.Visible = true;
                pnl_whether_yt.Visible = true;
                pnl_maintain_time.Visible = true;
                pnl_maintain_mileage.Visible = true;
                pnl_whether_yt.Visible = false;
                lbl_vehicle_model.Visible = true;

                pnl_customer_info.Height = 130;
                pnl_vehicle.Height = 100;

                tableLayoutPanel3.Controls.Remove(pnl_driving_license_no);

                var selectedValue = cbo_receipt_type.SelectedValue == null ? "" : cbo_receipt_type.SelectedValue.ToString().ToUpper();

                if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000007) //配件三包
                {
                    tableLayoutPanel3.Controls.Add(pnl_driving_license_no, 1, 2);

                    pnl_part_info.Visible = true;
                    var selectedValueManHourCost = cbo_contain_man_hour_cost.SelectedValue == null ? "" : cbo_contain_man_hour_cost.SelectedValue.ToString().ToUpper();
                    if (selectedValueManHourCost == DbDic2Enum.TRUE) //是
                    {
                        if (!tabControlExBottom.TabPages.Contains(tb_wxxm))
                            tabControlExBottom.TabPages.Insert(0, tb_wxxm);
                        tabControlExBottom.SelectTab(tb_wxxm);
                    }
                    else
                    {
                        if (!tabControlExBottom.TabPages.Contains(tb_wxxm))
                            tabControlExBottom.TabPages.Insert(0, tb_wxxm);
                        tabControlExBottom.SelectTab(tb_wxxm);
                    }
                }
                else
                {
                    tableLayoutPanel3.Controls.Add(pnl_driving_license_no, 3, 0);

                    if (!tabControlExBottom.TabPages.Contains(tb_wxxm))
                        tabControlExBottom.TabPages.Insert(0, tb_wxxm);
                    tabControlExBottom.SelectTab(tb_wxxm);
                    pnl_part_info.Visible = false;

                    var selectedValue4WhetherYt = cbo_whether_yt.SelectedValue == null ? "" : cbo_whether_yt.SelectedValue.ToString().ToUpper();
                    if (selectedValue4WhetherYt == DbDic2Enum.TRUE)
                    {
                        lbl__vehicle_vin.Visible = true;
                        lbl_depot_no.Visible = true;
                        lbl_engine_num.Visible = true;
                    }
                    else
                    {
                        lbl__vehicle_vin.Visible = false;
                        lbl_depot_no.Visible = false;
                        lbl_engine_num.Visible = false;
                    }
                }

                if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000007) //配件三包
                {
                    pnl_cust_address.Visible = false;
                    pnl_customer_postcode.Visible = false;
                    pnl_linkman.Visible = false;
                    pnl_link_man_mobi.Visible = false;
                    pnl_vehicle_location.Visible = false;

                    pnl_driving_license_no.Visible = false;
                    pnl_travel_mileage.Visible = false;
                    pnl_whether_yt.Visible = false;
                    pnl_maintain_time.Visible = false;
                    pnl_whether_yt.Visible = true;
                    pnl_maintain_mileage.Visible = false;

                }
                else if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000003) //维修
                {
                    pnl_promise_guarantee.Visible = true;   //特殊约定质保
                }
                else if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000005) //政策照顾
                {
                    pnl_approver_name_yt.Visible = true;    //宇通批准信息
                }
                else if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000006) //产品改进
                {
                    pnl_product_notice_no.Visible = true;   //产品改进通知
                }
                else if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000004) //服务活动
                {
                    pnl_cost_type_service_fwhd.Visible = true;  //费用类型
                }

                if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000007 ||
                    selectedValue == DbDic2Enum.BILL_TYPE_YT_100000003 ||
                    selectedValue == DbDic2Enum.BILL_TYPE_YT_100000005 ||
                    selectedValue == DbDic2Enum.BILL_TYPE_YT_100000006 ||
                    selectedValue == DbDic2Enum.BILL_TYPE_YT_100000004)
                {
                    pnl_fault.Height = 170;
                    pnl_fault_duty_corp.Visible = true;
                    pnl_fault_cause.Visible = true;
                    pnl_fault_system.Visible = true;
                    pnl_fault_assembly.Visible = true;
                    pnl_fault_part.Visible = true;
                    pnl_fault_schema.Visible = true;
                    pnl_fault_describe.Visible = true;
                    pnl_reason_analysis.Visible = true;
                    pnl_dispose_result.Visible = true;
                }
                else
                {
                    pnl_fault.Height = 75;
                    pnl_fault_duty_corp.Visible = false;
                    pnl_fault_cause.Visible = false;
                    pnl_fault_system.Visible = false;
                    pnl_fault_assembly.Visible = false;
                    pnl_fault_part.Visible = false;
                    pnl_fault_schema.Visible = false;
                    pnl_fault_describe.Visible = false;
                    pnl_reason_analysis.Visible = false;
                    pnl_dispose_result.Visible = false;
                }

                if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000000) //新车报道
                {
                    if (tabControlExBottom.TabPages.Contains(tb_wxyl))
                        tabControlExBottom.TabPages.Remove(tb_wxyl);
                }
                else
                {
                    if (!tabControlExBottom.TabPages.Contains(tb_wxyl))
                        tabControlExBottom.TabPages.Insert(0, tb_wxyl);
                    tabControlExBottom.SelectTab(tb_wxyl);
                }
                if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000007 ||
                    selectedValue == DbDic2Enum.BILL_TYPE_YT_100000003 ||
                    selectedValue == DbDic2Enum.BILL_TYPE_YT_100000005 ||
                    selectedValue == DbDic2Enum.BILL_TYPE_YT_100000006 ||
                    selectedValue == DbDic2Enum.BILL_TYPE_YT_100000004 ||
                    selectedValue == DbDic2Enum.BILL_TYPE_YT_100000000)
                {
                    tb_wxxm.Enabled = true;
                }
                else
                {
                    tb_wxxm.Enabled = false;
                }
                dtp_repairs_time.Focus();
            };
            #endregion
        }
        private void InitControlDataSource() //初始化数据字段控件数据
        {
            try
            {
                #region 设置控件验证值
                txt_repairer_mobile.VerifyType = "Phone";
                txt_customer_postcode.VerifyType = "Postcode";
                txt_link_man_mobile.VerifyType = "Phone";
                txt_other_money.VerifyType = txt_man_hour_subsidy.VerifyType = txt_ccf.VerifyType = txt_journey_subsidy.VerifyType = txt_maintain_mileage.VerifyType = txt_travel_mileage.VerifyType = "UFloat";
                txt_feedback_num.VerifyType = "Integer";
                #endregion

                dtp_goout_back_time.Value = dtp_goout_time.Value = dtp_maintain_time.Value = dtp_parts_buy_time.Value = dtp_repairs_time.Value = dtp_start_work_time.Value = dtp_complete_work_time.Value = DBHelper.GetCurrentTime();
                


                dgv_tb_maintain_three_guaranty_accessory.TableName = "tb_maintain_three_guaranty";
                dgv_tb_maintain_three_guaranty_accessory.TableNameKeyValue = TgId;

                #region 基础数据
                var isSecondStation = DBHelper.GetSingleValue("根据公司编码获取是否为二级站", GlobalStaticObj.CommAccCode, "tb_company", "category", "sap_code = '" + GlobalStaticObj.ServerStationCode+ "'", "");
                if (!String.IsNullOrEmpty(isSecondStation))
                {
                    chk_whether_second_station_true.Checked = isSecondStation == "1";    //一级站
                    chk_whether_second_station_false.Checked = isSecondStation == "2";    //二级站
                }
                txt_service_station_code.Text = GlobalStaticObj.ServerStationCode; //当前服务站代码
                txt_service_station_name.Caption = GlobalStaticObj.ServerStationName; //当前服务站名称

                dtp_repairs_time.Value = DateTime.Now;  //报修日期(系统默认当前时间)

                //txt_service_station_code.ChooserClick += delegate   //服务站选择器
                //{
                //    var signInfoChooser = new FormSignInfoChooser();
                //    var result = signInfoChooser.ShowDialog();
                //    if (result != DialogResult.OK) return;
                //    txt_service_station_code.Text = signInfoChooser.SignCode;
                //    txt_service_station_code.Tag = signInfoChooser.SignId;
                //    txt_service_station_name.Caption = signInfoChooser.SignName;
                //};

                CommonCtrl.CmbBindDict(cbo_receipt_type, "bill_type_yt", false);   //宇通单据类型
                CommonCtrl.CmbBindDict(cbo_cost_type_service_zczg, "cost_type_care_policy_yt", false);   //费用类型(政策照顾)
                CommonCtrl.BindComboBoxByTable(cbo_product_notice_no, "tb_product_no", "service_code", "activities", false);   //产品改进通知

                CommonCtrl.CmbBindDict(cbo_whether_go_out, "sys_true_false", false);   //是否外出
                cbo_whether_go_out.SelectedValue = DbDic2Enum.FALSE;    //是否外出(默认选择为否)

                CommonCtrl.CmbBindDict(cbo_refit_case, "refit_case", false);   //改装情况
                cbo_refit_case.SelectedValue = DbDic2Enum.REFIT_CASE_FALSE;    //改装请款(默认选择为无改装)

                DataSources.BindComBoxDataEnum(cbo_promise_guarantee, typeof(DataSources.EnumYesNo), false);   //特殊约定质保
                cbo_promise_guarantee.SelectedValue = DataSources.EnumYesNo.NO;    //特殊约定质保(默认选择为否)
                UIAssistants.BindingServiceStationUser(cbo_approver_name_yt, true, "请选择");

                txt_appraiser_name.ChooserClick += delegate   //鉴定人选择器
                {
                    var chooser = new frmUsers{ cbo_data_source = { SelectedValue = "1", Enabled = false } };
                    var result = chooser.ShowDialog();
                    if (result != DialogResult.OK) return;
                    if (String.IsNullOrEmpty(chooser.User_Name) || String.IsNullOrEmpty(chooser.CrmId))
                    {
                        MessageBoxEx.Show("无效的数据,获取不到用户的CRM信息");
                        return;
                    }
                    //txt_approver_name_yt.Text = chooser.contName;
                    //txt_approver_name_yt.Tag = chooser.crmId;
                    txt_appraiser_name.Text = chooser.User_Name;
                    txt_appraiser_name.Tag = chooser.CrmId;
                };

                txt_repair_man.ChooserClick += delegate   //维修人选择器
                {
                    var chooser = new frmUsers{ cbo_data_source = { SelectedValue = "1", Enabled = false } };
                    var result = chooser.ShowDialog();
                    if (result != DialogResult.OK) return;
                    if (String.IsNullOrEmpty(chooser.User_Name) || String.IsNullOrEmpty(chooser.CrmId))
                    {
                        MessageBoxEx.Show("无效的数据,获取不到用户的CRM信息");
                        return;
                    }
                    //txt_approver_name_yt.Text = chooser.contName;
                    //txt_approver_name_yt.Tag = chooser.crmId;
                    txt_repair_man.Text = chooser.User_Name;
                    txt_repair_man.Tag = chooser.CrmId;
                };

                CommonCtrl.BindComboBoxByTable(cbo_cost_type_service_fwhd, "tb_product_no", "service_code", "activities", false);   //费用类型(服务活动) unknow

                CommonCtrl.CmbBindDict(cbo_customer_property, "custom_property_yt", false);   //客户性质
                txt_vehicle_use_corp.ChooserClick += delegate  //车辆使用单位选择器
                {
                    var custChooser = new frmCustomerInfo();
                    var result = custChooser.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        if (String.IsNullOrEmpty(custChooser.CustomerInfo.CustName) || String.IsNullOrEmpty(custChooser.CustomerInfo.SAPCode))
                        {
                            MessageBoxEx.Show("选择的数据不符合使用要求");
                            return;
                        }
                        txt_vehicle_use_corp.Text = custChooser.CustomerInfo.CustName;
                        txt_vehicle_use_corp.Tag = custChooser.CustomerInfo.SAPCode;
                    }
                };
                txt_customer_code.ChooserClick += delegate  //客户信息选择器
                {
                    var custChooser = new frmCustomerInfo();
                    var result = custChooser.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        if (String.IsNullOrEmpty(custChooser.CustomerInfo.CustCode))
                        {
                            MessageBoxEx.Show("选择的数据不符合使用要求");
                            return;
                        }
                        txt_customer_code.Text = custChooser.CustomerInfo.CustCode;
                        txt_customer_code.Tag = custChooser.CustomerInfo.CustId;
                        txt_customer_name.Caption = custChooser.CustomerInfo.CustName;
                        txt_customer_address.Caption = custChooser.CustomerInfo.CustAddress;
                        txt_customer_postcode.Caption = custChooser.CustomerInfo.ZipCode;

                        //联系人数据                
                        var dpt = DBHelper.GetTable("联系人数据", "(select tr.relation_object_id, tr.is_default tr_is_default, tb.*  from tb_contacts tb inner join tr_base_contacts tr on tb.cont_id = tr.cont_id) a", "*",
                            string.Format(" a.relation_object_id='{0}'", custChooser.CustomerInfo.CustId), "", "");
                        if (dpt != null && dpt.Rows.Count > 0)
                        {
                            for (var i = 0; i < dpt.Rows.Count; i++)
                            {
                                var dpr = dpt.Rows[i];
                                if (CommonCtrl.IsNullToString(dpr["tr_is_default"]) == "1")
                                {
                                    txt_linkman.Caption = CommonCtrl.IsNullToString(dpr["cont_name"]);
                                    txt_link_man_mobile.Caption = CommonCtrl.IsNullToString(dpr["cont_phone"]);
                                }
                            }
                        }
                        ValidationCustomer();
                    }
                };
                txt_customer_code.Leave += delegate     //客户信息校验
                {
                    ValidationCustomer();
                };
                #endregion

                #region 车辆信息
                CommonCtrl.BindComboBoxByDictionarr(cbo_fault_cause, "cause_fault_yt", false);   //故障原因
                CommonCtrl.BindComboBoxByDictionarr(cbo_fault_duty_corp, "fault_company_yt", false);   //故障责任单位
                txt_vehicle_no.ChooserClick += delegate  //车辆选择器
                {
                    var chooser = new frmVehicleGrade();
                    var result = chooser.ShowDialog();
                    if (result != DialogResult.OK) return;
                    txt_vehicle_no.Text = chooser.strLicensePlate; //车牌号
                    txt_depot_no.Text = chooser.Turner;  //车工号
                    txt_vehicle_vin.Caption = chooser.strVIN;   //VIN
                    txt_engine_num.Caption = chooser.strEngineNum; //发动机号
                    txt_vehicle_model.Tag = chooser.strModel;  //车型
                    ValidationVehicle();
                    //txt_maintain_mileage.Caption = chooser.MaintainMileage; //协议保养里程
                    //txt_maintain_time.Caption = chooser.MaintainTime;    //协议保养日期
                };
                txt_depot_no.ChooserClick += delegate  //车工号选择器
                {
                    var chooser = new frmVehicleGrade();
                    var result = chooser.ShowDialog();
                    if (result != DialogResult.OK) return;
                    txt_vehicle_no.Text = chooser.strLicensePlate; //车牌号
                    txt_depot_no.Text = chooser.Turner;  //车工号
                    txt_vehicle_vin.Caption = chooser.strVIN;   //VIN
                    txt_engine_num.Caption = chooser.strEngineNum; //发动机号
                    txt_vehicle_model.Tag = chooser.strModel;  //车型
                    ValidationDepto();
                    //txt_maintain_mileage.Caption = chooser.MaintainMileage; //协议保养里程
                    //txt_maintain_time.Caption = chooser.MaintainTime;    //协议保养日期
                };
                txt_vehicle_no.Leave += delegate     //车辆信息校验
                {
                    ValidationVehicle();
                };
                txt_depot_no.Leave += delegate     //车辆信息校验
                {
                    ValidationDepto();
                };

                txt_vehicle_model.ChooserClick += delegate  //车型选择器
                {
                    var chooser = new frmVehicleModels();
                    var result = chooser.ShowDialog();
                    if (result != DialogResult.OK) return;
                    txt_vehicle_model.Text = chooser.VMName;  //车型
                    txt_vehicle_model.Tag = chooser.VMID;
                    _vmClass = chooser.VMClass;
                };

                txt_fault_system.ChooserClick += delegate  //故障系统选择器
                {
                    var chooser = new FormChooserFault();
                    var result = chooser.ShowDialog();
                    if (result != DialogResult.OK) return;

                    txt_fault_system.Text = chooser.SystemName;
                    txt_fault_system.Tag = chooser.SystemCode;
                    txt_fault_assembly.Caption = chooser.AssemblyName;
                    txt_fault_assembly.Tag = chooser.AssemblyCode;
                    txt_fault_part.Caption = chooser.PartmName;
                    txt_fault_part.Tag = chooser.PartCode;
                    //txt_fault_schema.Text = chooser.SchemaName;
                    //txt_fault_schema.Tag = chooser.SchemaCode;

                };
                txt_fault_schema.ChooserClick += delegate  //故障模式选择器
                {
                    var chooser = new FormChooserFaultModel();
                    var result = chooser.ShowDialog();
                    if (result != DialogResult.OK) return;
                    txt_fault_schema.Text = chooser.FmeaName;
                    txt_fault_schema.Tag = chooser.FmeaCode;
                };
                #endregion

                #region 外出信息
                CommonCtrl.BindComboBoxByDictionarr(cbo_whether_yt, "sys_true_false", false);   //是否宇通车
                if(cbo_whether_yt.Items.Contains("0")) cbo_whether_yt.SelectedValue = "0";
                CommonCtrl.BindComboBoxByDictionarr(cbo_means_traffic, "traffic_mode_yt", false);   //外出交通方式
                if (cbo_means_traffic.Items.Count > 0) cbo_means_traffic.SelectedIndex = 0;
                cbo_means_traffic.SelectedIndexChanged += delegate
                {
                    if (CommonCtrl.IsNullToString(cbo_means_traffic.SelectedValue) ==
                        DbDic2Enum.TRAFFIC_MODE_YT_100000000)
                    {
                        txt_journey_subsidy.Enabled = false;
                    }
                    else
                    {
                        txt_journey_subsidy.Enabled = true;
                    }
                };

                UIAssistants.BindingServiceStationUser(cbo_goout_approver, true, "请选择"); //外出批准人选择器
                
                #endregion

                #region 配件信息
                dtp_parts_buy_time.Value = DateTime.Now;
                txt_parts_buy_corp.ChooserClick += delegate  //配件购买单位(客户选择器)
                {
                    var custChooser = new frmChooseCompany();
                    var result = custChooser.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        txt_parts_buy_corp.Text = custChooser.Sap_Code;
                    }
                };
                CommonCtrl.BindComboBoxByDictionarr(cbo_contain_man_hour_cost, "sys_true_false", false);   //是否包含工时费
                cbo_contain_man_hour_cost.SelectedValue = DbDic2Enum.TRUE;

                txt_parts_code.ChooserClick += delegate  //配件编号(配件选择器)
                {
                    var chooser = new frmParts();
                    var result = chooser.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        txt_parts_code.Text = chooser.PartsCode;
                        txt_materiel_describe.Caption = chooser.PartsName;
                    }
                };
                txt_first_install_station.ChooserClick += delegate  //首次安装服务站(客户选择器)
                {
                    var custChooser = new frmChooseCompany();
                    var result = custChooser.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        txt_first_install_station.Text = custChooser.Sap_Code;
                    }
                };
                CommonCtrl.BindComboBoxByDictionarr(cbo_part_guarantee_period, "parts_warranty_agreement_yt", false);   //协议保养日期
                #endregion

                #region 经办人
                txt_responsible_opid.ChooserClick += delegate   //经办人选择器
                {
                    var chooser = new frmUsers();
                    var result = chooser.ShowDialog();
                    if (result != DialogResult.OK) return;
                    txt_responsible_opid.Text = chooser.User_Name;
                    txt_responsible_opid.Tag = chooser.User_ID;
                    txt_org_name.Caption = chooser.OrgName;
                    txt_org_name.Tag = chooser.OrgId;
                };
                #endregion
            }
            catch (Exception ex)
            {
                //日记记录
            }
        }
        private void InitDataGridCellFormatting()   //初始化数据表格
        {
            try
            {
                #region 维修项目数据表格设置
                dgv_tb_maintain_three_guaranty_item.ReadOnly = false;
                dgv_tb_maintain_three_guaranty_item.Rows.Add(3);
                dgv_tb_maintain_three_guaranty_item.AllowUserToAddRows = true;
                drtxt1_item_no.ReadOnly = true;
                drtxt1_item_type.ReadOnly = true;
                drtxt1_item_name.ReadOnly = true;
                drtxt1_man_hour_type.ReadOnly = true;
                drtxt1_man_hour_quantity.ReadOnly = false;
                drtxt1_man_hour_unitprice.ReadOnly = true;
                drtxt1_sum_money.ReadOnly = false;
                drtxt1_remarks.ReadOnly = false;

                dgv_tb_maintain_three_guaranty_item.CellClick += delegate(object sender, DataGridViewCellEventArgs args)
                {
                    if (args.RowIndex == -1 || args.ColumnIndex == -1) return;
                    string msg;
                    if (dgv_tb_maintain_three_guaranty_item.Columns[args.ColumnIndex] != drtxt1_item_no) return;
                    var frm = new frmWorkHours();
                    var result = frm.ShowDialog();
                    if (result != DialogResult.OK) return;
                    dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_item_no"].Value = frm.strProjectNum;
                    dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_item_type"].Value = UIAssistants.GetDicName(frm.strRepairType, out msg);
                    dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_item_name"].Value = frm.strProjectName;
                    dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_man_hour_type"].Value = frm.strWhoursType == "1" ? "工时" : "定额";
                    dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_man_hour_type"].Tag = frm.strWhoursChange;
                    dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_man_hour_quantity"].Value = frm.strWhoursNum;
                    dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_man_hour_quantity"].Tag = frm.strWhoursNum;
                    dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_man_hour_unitprice"].Value = frm.strQuotaPrice;
                    dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_remarks"].Value = frm.strRemark;
                    dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_sum_money"].Value = Convert.ToString(Convert.ToDecimal(frm.strWhoursNum) * Convert.ToDecimal(String.IsNullOrEmpty(frm.strQuotaPrice) ? "0" : frm.strQuotaPrice));
                };
                dgv_tb_maintain_three_guaranty_item.CellEndEdit += delegate(object sender, DataGridViewCellEventArgs args)
                {
                    if (dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_item_no"].Value == null) return;
                    //if (dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_man_hour_type"].Value.ToString() == "工时")
                    //{
                    if (dgv_tb_maintain_three_guaranty_item.Columns[args.ColumnIndex] != drtxt1_man_hour_quantity && dgv_tb_maintain_three_guaranty_item.Columns[args.ColumnIndex] != drtxt1_man_hour_unitprice) return;

                    var strNum = dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_man_hour_quantity"].Value != null ? dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_man_hour_quantity"].Value.ToString() : "0";
                    var strMoney = dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_man_hour_unitprice"].Value != null ? dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_man_hour_unitprice"].Value.ToString() : "0";
                    dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum == "" ? "0" : strNum) * Convert.ToDecimal(strMoney == "" ? "0" : strMoney));
                    //}
                    //else
                    //{
                    //    dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_man_hour_quantity"].Value = "";
                    //    dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_sum_money"].Value = "0";
                    //}
                };
                dgv_tb_maintain_three_guaranty_item.CellBeginEdit +=
                    delegate(object sender, DataGridViewCellCancelEventArgs args)
                    {
                        if (dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_item_no"].Value == null) return;
                        if (dgv_tb_maintain_three_guaranty_item.Columns[args.ColumnIndex] != drtxt1_man_hour_quantity) return;
                        if (dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_man_hour_type"].Tag !=
                            null &&
                            dgv_tb_maintain_three_guaranty_item.Rows[args.RowIndex].Cells["drtxt1_man_hour_type"].Tag
                                .ToString() == "0")
                        {
                            args.Cancel = true;
                        }

                    };
                #endregion

                #region 维修用料数据表格设置
                dgv_tb_maintain_three_matrial_detail.Dock = DockStyle.Fill;
                dgv_tb_maintain_three_matrial_detail.ReadOnly = false;
                dgv_tb_maintain_three_matrial_detail.Rows.Add(3);
                dgv_tb_maintain_three_matrial_detail.AllowUserToAddRows = true;
                drtxt2_depot_code.ReadOnly = true;
                drtxt2_parts_code.ReadOnly = true;
                drtxt2_parts_name.ReadOnly = true;
                drtxt2_norms.ReadOnly = true;
                drtxt2_unit.ReadOnly = true;
                drtxt2_is_import.ReadOnly = true;
                drtxt2_parts_source.ReadOnly = true;
                drtxt2_redeploy_no.ReadOnly = true;
                drtxt2_quantity.ReadOnly = false;
                drtxt2_unit_price.ReadOnly = true;
                drtxt2_sum_money.ReadOnly = true;
                drtxt2_parts_source.ReadOnly = false;
                drtxt2_redeploy_no.ReadOnly = true;
                drtxt2_remarks.ReadOnly = false;

                CommonCtrl.BindComboBoxByDictionarr(drtxt2_parts_source, "new_parts_source_yt");

                dgv_tb_maintain_three_matrial_detail.CellClick += delegate(object sender, DataGridViewCellEventArgs args)
                {
                    if (args.RowIndex == -1) return;
                    if (
                        String.IsNullOrEmpty(
                            CommonCtrl.IsNullToString(
                                dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells[drtxt2_parts_source.Name]
                                    .Value)))
                    {
                        dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells[drtxt2_parts_source.Name]
                            .Value = "677BCEA4-8E61-4224-AE6F-C8D1EA9E7C8A";
                    }
                    if (dgv_tb_maintain_three_matrial_detail.Columns[args.ColumnIndex] == drtxt2_quantity || dgv_tb_maintain_three_matrial_detail.Columns[args.ColumnIndex] == drtxt2_remarks || dgv_tb_maintain_three_matrial_detail.Columns[args.ColumnIndex] == drtxt2_parts_source) return;
                    var frmPart = new frmParts();
                    var result = frmPart.ShowDialog();
                    if (result != DialogResult.OK) return;
                    var strPId = frmPart.PartsID;
                    var dpt = DBHelper.GetTable("", "v_parts", string.Format("*,{0} price2a1", EncryptByDB.GetDesFieldValue("price2a")), " parts_id='" + strPId + "'", "", "");
                    if (dpt.Rows.Count <= 0) return;
                    var dpr = dpt.Rows[0];
                    //dgv_tb_maintain_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_depot_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]); //车厂编码 unknow
                    dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_depot_code"].Value = CommonCtrl.IsNullToString(dpr["car_parts_code"]);
                    dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                    dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                    dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_norms"].Value = CommonCtrl.IsNullToString(dpr["model"]);
                    dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_unit"].Value = CommonCtrl.IsNullToString(dpr["sales_unit_name"]);
                    dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_is_import"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                    dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_quantity"].Value = "1";
                    if (Convert.ToDouble(CommonCtrl.IsNullToString(dpr["retail"])) <= 300)
                    {
                        dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_unit_price"].Value = Convert.ToDouble(CommonCtrl.IsNullToString(dpr["price2a1"]))*1.17;
                    }
                    else
                    {
                        dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_unit_price"].Value = Convert.ToDouble(CommonCtrl.IsNullToString(dpr["price2a1"])) * 1.12;
                    }
                    //dgv_tb_maintain_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_redeploy_no"].Value = CommonCtrl.IsNullToString(dpr["highest_out_price"]);   //三包调件号 unknow
                    dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_remarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                };
                dgv_tb_maintain_three_matrial_detail.CellEndEdit += delegate(object sender, DataGridViewCellEventArgs args)
                {
                    if (dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_parts_code"].Value == null) return;
                    if (dgv_tb_maintain_three_matrial_detail.Columns[args.ColumnIndex] != drtxt2_quantity &&
                        dgv_tb_maintain_three_matrial_detail.Columns[args.ColumnIndex] != drtxt2_unit_price) return;
                    var strNum = dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_quantity"].Value != null ? dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_quantity"].Value.ToString() : "0";
                    var strUMoney = dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_unit_price"].Value != null ? dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_unit_price"].Value.ToString() : "0";
                    dgv_tb_maintain_three_matrial_detail.Rows[args.RowIndex].Cells["drtxt2_sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney == "" ? "0" : strUMoney));
                };
                #endregion
            }
            catch (Exception)
            {
                
            }
        }

        #region 数据保存
        private Boolean CheckControlValue() //数据必填项非空判断
        {
            var selectedValue = cbo_receipt_type.SelectedValue == null ? "" : cbo_receipt_type.SelectedValue.ToString().ToUpper();

            #region 宇通单据类型
            if (cbo_receipt_type.SelectedValue == null)
            {
                erp_error.SetError(cbo_receipt_type,"宇通单据类型不允许为空!!!");
                return false;
            }
            #endregion

            #region 政策照顾
            if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000005)
            {
                #region 宇通批准人
                if (cbo_approver_name_yt.SelectedIndex == -1)
                {
                    erp_error.SetError(cbo_approver_name_yt,"宇通批准人不允许为空!");
                    return false;
                }
                #endregion

                #region 费用类型
                if (cbo_cost_type_service_zczg.SelectedIndex == -1)
                {
                    erp_error.SetError(cbo_cost_type_service_zczg,"费用类型不允许为空!");
                    return false;
                }
                #endregion

                #region 情况简介
                if (String.IsNullOrEmpty(txt_describes.Caption))
                {
                    erp_error.SetError(txt_describes,"情况简介不允许为空!");
                    return false;
                }
                #endregion
            }
            #endregion

            #region 产品改进
            if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000006)
            {
                #region 产品改进通知
                if (cbo_product_notice_no.SelectedIndex == -1)
                {
                    erp_error.SetError(cbo_product_notice_no,"产品改进通知不允许为空!");
                    return false;
                }
                #endregion
            }
            #endregion

            #region 服务活动
            if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000004)    //服务活动
            {
                #region 费用类型
                if (cbo_cost_type_service_fwhd.SelectedIndex == -1)
                {
                    erp_error.SetError(cbo_cost_type_service_fwhd, "费用类型不允许为空!");
                    return false;
                }
                #endregion
            }
            #endregion

            #region 配件三包
            if (selectedValue != DbDic2Enum.BILL_TYPE_YT_100000007) //配件三包
            {
                #region 客户信息
                if (String.IsNullOrEmpty(txt_customer_code.Text))
                {
                    erp_error.SetError(txt_customer_code,"客户编码不允许为空!");
                    return false;
                }
                if (cbo_customer_property.SelectedIndex == -1)
                {
                    erp_error.SetError(cbo_customer_property,"客户性质不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(txt_customer_address.Caption))
                {
                    erp_error.SetError(txt_customer_address,"客户地址不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(txt_vehicle_use_corp.Tag)))
                {
                    erp_error.SetError(txt_vehicle_use_corp,"车辆使用单位不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(txt_vehicle_location.Caption))
                {
                    erp_error.SetError(txt_vehicle_location,"车辆所在地不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(txt_repairer_name.Caption))
                {
                    erp_error.SetError(txt_repairer_name,"报修人不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(txt_repairer_mobile.Caption))
                {
                    erp_error.SetError(txt_repairer_mobile,"报修人手机不允许为空!");
                    return false;
                }
                if (!txt_repairer_mobile.HasError())
                {
                    erp_error.SetError(txt_repairer_mobile, "报修人手机格式校验位通过");
                    return false;
                }
                if (!txt_customer_postcode.HasError())
                {
                    erp_error.SetError(txt_customer_postcode, "客户邮编格式校验位通过");
                    return false;
                }
                if (!txt_link_man_mobile.HasError())
                {
                    erp_error.SetError(txt_link_man_mobile, "联系人手机格式校验位通过");
                    return false;
                }
                #endregion

                #region 车辆信息
                if (String.IsNullOrEmpty(txt_vehicle_no.Text))
                {
                    erp_error.SetError(txt_vehicle_no,"车牌号不允许为空!");
                    return false;
                }

                if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(txt_vehicle_model.Tag)))
                {
                    erp_error.SetError(txt_vehicle_model,"车型不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(txt_travel_mileage.Caption))
                {
                    erp_error.SetError(txt_travel_mileage,"行驶里程不允许为空!");
                    return false;
                }
                #endregion
            }
            #endregion

            #region 车工号
            if (String.IsNullOrEmpty(txt_depot_no.Text))
            {
                erp_error.SetError(txt_depot_no,"车工号不允许为空!");
                return false;
            }
            #endregion

            #region VIN
            if (String.IsNullOrEmpty(txt_vehicle_vin.Caption))
            {
                erp_error.SetError(txt_vehicle_vin,"VIN不允许为空!");
                return false;
            }
            #endregion

            #region 发动机号
            if (String.IsNullOrEmpty(txt_engine_num.Caption))
            {
                erp_error.SetError(txt_engine_num, "发动机号不允许为空!");
                return false;
            }
            #endregion

            #region 维修开始时间
            if (String.IsNullOrEmpty(dtp_start_work_time.Value.ToString()))
            {
                erp_error.SetError(dtp_start_work_time, "维修开始时间不允许为空!");
                return false;
            }
            #endregion

            #region 维修结束时间
            if (String.IsNullOrEmpty(dtp_complete_work_time.Value.ToString()))
            {
                erp_error.SetError(dtp_complete_work_time, "维修结束时间不允许为空!");
                return false;
            }
            #endregion

            #region 鉴定人
            if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(txt_appraiser_name.Tag)))
            {
                erp_error.SetError(txt_appraiser_name, "鉴定人不允许为空!");
                return false;
            }
            #endregion

            #region 故障责任单位
            if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000007 || selectedValue == DbDic2Enum.BILL_TYPE_YT_100000003 ||
                selectedValue == DbDic2Enum.BILL_TYPE_YT_100000005 || selectedValue == DbDic2Enum.BILL_TYPE_YT_100000006 ||
                selectedValue == DbDic2Enum.BILL_TYPE_YT_100000004)
            {
                if (cbo_fault_duty_corp.SelectedIndex == -1)
                {
                    erp_error.SetError(cbo_fault_duty_corp,"故障责任单位不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(txt_fault_system.Tag)))
                {
                    erp_error.SetError(txt_fault_system,"故障系统不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(txt_fault_assembly.Caption)))
                {
                    erp_error.SetError(txt_fault_assembly,"故障总成不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(txt_fault_part.Caption))
                {
                    erp_error.SetError(txt_fault_part,"故障部件不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(txt_fault_schema.Tag)))
                {
                    erp_error.SetError(txt_fault_schema,"故障模式不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(txt_fault_describe.Caption))
                {
                    erp_error.SetError(txt_fault_describe,"故障描述不允许为空!");
                    return false;
                }
                if (cbo_fault_cause.SelectedIndex == -1)
                {
                    erp_error.SetError(cbo_fault_cause,"故障原因不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(txt_dispose_result.Caption))
                {
                    erp_error.SetError(txt_dispose_result,"处理结果不允许为空!");
                    return false;
                }
            }
            #endregion

            #region 外出情况
            if (Equals(cbo_whether_go_out.SelectedValue.ToString(), DbDic2Enum.TRUE))
            {
                if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(txt_goout_cause.Caption)))
                {
                    erp_error.SetError(txt_goout_cause,"外出事由不允许为空!");
                    return false;
                }
                if (cbo_goout_approver.SelectedIndex == -1)
                {
                    erp_error.SetError(cbo_goout_approver,"外出批准人不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(txt_goout_place.Caption)))
                {
                    erp_error.SetError(txt_goout_place,"外出地点不允许为空!");
                    return false;
                }
                if (cbo_means_traffic.SelectedIndex == -1)
                {
                    erp_error.SetError(cbo_means_traffic,"交通方式不允许为空!");
                    return false;
                }
                if (cbo_means_traffic.SelectedIndex == -1)
                {
                    erp_error.SetError(cbo_means_traffic,"交通方式不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(dtp_goout_time.Value.ToString()))
                {
                    erp_error.SetError(dtp_goout_time,"外出时间不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(dtp_goout_back_time.Value.ToString()))
                {
                    erp_error.SetError(dtp_goout_back_time,"外出回长时间不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(txt_goout_mileage.Caption))
                {
                    erp_error.SetError(txt_goout_mileage,"外出里程不允许为空!");
                    return false;
                }

                #region 外出人数
                var selectedValue4GooutMan = cbo_whether_go_out.SelectedValue == null ? "" : cbo_whether_go_out.SelectedValue.ToString().ToUpper();
                if (selectedValue4GooutMan == DbDic2Enum.TRUE)
                {
                    var gooutManCount = 0;
                    Int32.TryParse(txt_goout_people_num.Caption, out gooutManCount);
                    if (gooutManCount < 1 || gooutManCount > 3)
                    {
                        erp_error.SetError(txt_goout_people_num,"外出人数必须为1到3之间!");
                        return false;
                    }
                }
                #endregion

                if (String.IsNullOrEmpty(txt_journey_subsidy.Caption))
                {
                    erp_error.SetError(txt_journey_subsidy,"路程补助不允许为空!");
                    return false;
                }

                if (String.IsNullOrEmpty(txt_ccf.Caption))
                {
                    erp_error.SetError(txt_ccf,"车船费不允许为空!");
                    return false;
                }

                if (String.IsNullOrEmpty(txt_man_hour_subsidy.Caption))
                {
                    erp_error.SetError(txt_man_hour_subsidy,"工时补助不允许为空!");
                    return false;
                }


            }
            
            #endregion

            #region 配件信息
            if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000007) //配件三包
            {
                if (String.IsNullOrEmpty(dtp_parts_buy_time.Value.ToString()))
                {
                    erp_error.SetError(dtp_parts_buy_time ,"配件购买日期不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(txt_parts_buy_corp.Text)))
                {
                    erp_error.SetError(txt_parts_buy_corp,"配件购买单位不允许为空!");
                    return false;
                }
                if (cbo_contain_man_hour_cost.SelectedIndex == -1)
                {
                    erp_error.SetError(cbo_contain_man_hour_cost,"是否包含工时费不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(txt_parts_code.Text)))
                {
                    erp_error.SetError(txt_parts_code,"配件编号不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(txt_materiel_describe.Caption)))
                {
                    erp_error.SetError(txt_materiel_describe,"物料描述不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(txt_first_install_station.Text)))
                {
                    erp_error.SetError(txt_first_install_station,"首次安装服务站不允许为空!");
                    return false;
                }
                if (cbo_part_guarantee_period.SelectedIndex == -1)
                {
                    erp_error.SetError(cbo_part_guarantee_period,"协议保养日期不允许为空!");
                    return false;
                }
                if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(txt_feedback_num.Caption)))
                {
                    erp_error.SetError(txt_feedback_num,"反馈数量不允许为空!");
                    return false;
                }
            }
            #endregion

            #region 数据有效性验证


            if (!txt_man_hour_subsidy.HasError())
            {
                erp_error.SetError(txt_man_hour_subsidy, "数据格式校验位通过");
                return false;
            }

            if (!txt_ccf.HasError())
            {
                erp_error.SetError(txt_ccf, "数据格式校验位通过");
                return false;
            }

            if (!txt_journey_subsidy.HasError())
            {
                erp_error.SetError(txt_journey_subsidy, "数据格式校验位通过");
                return false;
            }

            if (!txt_maintain_mileage.HasError())
            {
                erp_error.SetError(txt_maintain_mileage, "协议保养里程格式校验位通过");
                return false;
            }

            if (!txt_travel_mileage.HasError())
            {
                erp_error.SetError(txt_travel_mileage, "行驶里程格式校验位通过");
                return false;
            }

            if (!txt_feedback_num.HasError())
            {
                erp_error.SetError(txt_feedback_num, "反馈数量格式校验位通过");
                return false;
            }
            #endregion
            return true;
        }
        private Dictionary<string, string> GetDicFieldsByBillTypeYt()
        {
            var dicFields = new Dictionary<string, string>();
            if (cbo_receipt_type.SelectedValue == null) return dicFields;
                var selectedValue = cbo_receipt_type.SelectedValue == null ? "": cbo_receipt_type.SelectedValue.ToString().ToUpper();
            if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000001) //走保
            {

            }
            else if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000002)    //强保
            {

            }
            else if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000003)    //维修
            {
                dicFields.Add("promise_guarantee", cbo_promise_guarantee.SelectedValue == null ? "" : cbo_promise_guarantee.SelectedValue.ToString());                 //特殊约定质保（是、否） -- varchar(4)
            }
            else if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000004)    //服务活动
            {
                dicFields.Add("cost_type_service", cbo_cost_type_service_fwhd.SelectedValue == null ? "" : cbo_cost_type_service_fwhd.SelectedValue.ToString());            //费用类型(服务活动) -- varchar(40)
            }
            else if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000005)    //政策照顾
            {
                dicFields.Add("approver_id_yt", CommonCtrl.IsNullToString(cbo_approver_name_yt.SelectedValue));                               //宇通审批人 -- varchar(40)
                dicFields.Add("approver_name_yt", CommonCtrl.IsNullToString(cbo_approver_name_yt.Text));                                       //宇通审批人姓名 -- varchar(40)
                dicFields.Add("policy_approval_no", txt_policy_approval_no.Caption);                                //政策照顾审批编码 -- varchar(40)
                dicFields.Add("cost_type_policy", cbo_cost_type_service_zczg.SelectedValue == null ? "" : cbo_cost_type_service_zczg.SelectedValue.ToString());             //费用类型(政策照顾) -- varchar(40)
                dicFields.Add("describes", txt_describes.Caption);                                                  //情况简述 -- varchar(200)
            }
            else if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000006)    //产品改进
            {

                dicFields.Add("product_notice_no", cbo_product_notice_no.SelectedValue == null ? "" : cbo_product_notice_no.SelectedValue.ToString());                 //产品改进通知号 -- varchar(40)
            }
            else if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000007)    //配件三包
            {
                dicFields.Add("whether_yt", cbo_whether_yt.SelectedValue == null ? "" : cbo_whether_yt.SelectedValue.ToString());                               //是否宇通车 -- varchar(4)
            }
            else if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000008)    //服务产品
            {

            }
            return dicFields;
        }
        private void Save()
        {
            if (!CheckControlValue()) return;
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                //lbl_service_no.Text = CommonUtility.GetNewNo(DataSources.EnumProjectType.ThreeGuarantyService);
            }
            var result = SaveThreeGuarantyItem((_orderStatus == "" || _orderStatus == DbDic2Enum.SYS_SERVICE_INFO_STATUS_CG) ? DbDic2Enum.SYS_SERVICE_INFO_STATUS_CG : DbDic2Enum.SYS_SERVICE_INFO_STATUS_SHWTG);
            var id = windowStatus == WindowStatus.Edit ? TgId : _newTgId;
            if (result)
            {
                SaveAttachement(id);
                if(tabControlExBottom.Contains(tb_wxyl))
                    SaveMaterialDetail(id);
                if (tabControlExBottom.Contains(tb_wxxm))
                    SaveProjectData(id);
                MessageBoxEx.Show("数据保存成功!", "操作提示");
                UCForm.BindPageData();
                deleteMenuByTag(Tag.ToString(), UCForm.Name);
            }
            else
            {
                MessageBoxEx.Show("数据保存失败!", "操作提示");
            }
        }
        private Boolean SaveThreeGuarantyItem(String infoStatus)
        {
            var selectedValue = cbo_receipt_type.SelectedValue == null ? "" : cbo_receipt_type.SelectedValue.ToString();
            var dicFields = new Dictionary<string, string>();

            dicFields.Add("repairs_time", dtp_repairs_time.Value.ToUniversalTime().Ticks.ToString());               //报修时间 -- bigint
            dicFields.Add("service_station_code", txt_service_station_code.Text);                                   //服务站编码 -- varchar(40)
            dicFields.Add("service_station_name", txt_service_station_name.Caption);                                //服务站名称 -- varchar(50)
            dicFields.Add("receipt_type", cbo_receipt_type.SelectedValue == null ? "" : cbo_receipt_type.SelectedValue.ToString());                               
                                                                                                                    //宇通单据类型 -- varchar(40)
            foreach (var keyValuePair in GetDicFieldsByBillTypeYt())                                        //添加单据类型所对应数据字段
            {
                dicFields.Add(keyValuePair.Key, keyValuePair.Value);
            }
            dicFields.Add("whether_go_out", cbo_whether_go_out.SelectedValue == null ? "" : cbo_whether_go_out.SelectedValue.ToString());                           
                                                                                                                    //是否外出 -- varchar(5)
            dicFields.Add("refit_case", cbo_refit_case.SelectedValue == null ? "" : cbo_refit_case.SelectedValue.ToString());                                   
                                                                                                                    //改装情况 -- varchar(40)
            dicFields.Add("customer_name", txt_customer_name.Caption);                                              //客户名称 -- varchar(20)
            dicFields.Add("customer_id", txt_customer_code.Tag == null ? "" : txt_customer_code.Tag.ToString());    //客户关联id -- varchar(40)
            dicFields.Add("customer_address", txt_customer_address.Caption);                                        //客户地址 -- varchar(40)
            dicFields.Add("customer_code", txt_customer_code.Text);                                             //客户编码 -- varchar(40)
            dicFields.Add("customer_property", cbo_customer_property.SelectedValue == null ? "" : cbo_customer_property.SelectedValue.ToString()); 
            dicFields.Add("vehicle_use_corp", CommonCtrl.IsNullToString(txt_vehicle_use_corp.Tag));                                    //车辆使用单位 -- varchar(50)   
            dicFields.Add("vehicle_use_corp_name", CommonCtrl.IsNullToString(txt_vehicle_use_corp.Text));                                    //车辆使用单位 -- varchar(50)   
            if (selectedValue != DbDic2Enum.BILL_TYPE_YT_100000007)             //单据类型为三包服务单则不包含以下客户信息数据字段
            {
                dicFields.Add("customer_postcode", txt_customer_postcode.Caption);                                  //客户邮编 -- varchar(40)               
                                                                                                                    //客户性质 -- varchar(40)
                dicFields.Add("linkman", txt_linkman.Caption);                                                      //联系人 -- varchar(40)
                dicFields.Add("link_man_mobile", txt_link_man_mobile.Caption);                                      //联系人手机 -- varchar(15)
                //dicFields.Add("vehicle_use_corp_name", CommonCtrl.IsNullToString(txt_vehicle_use_corp.Text));                                    //车辆使用单位 -- varchar(50)

                dicFields.Add("vehicle_location", txt_vehicle_location.Caption);                                       //车辆所在地 -- varchar(40)
            }
            dicFields.Add("repairer_id", txt_repairer_name.Caption);   //报修人id -- varchar(40)
            dicFields.Add("repairer_name", txt_repairer_name.Caption);                                              //报修人姓名 -- varchar(40)
            dicFields.Add("repairer_mobile", txt_repairer_mobile.Caption);                                          //报修人电话 -- varchar(15)
            dicFields.Add("vehicle_no", txt_vehicle_no.Text);                                                   //车牌号 -- varchar(40)
            dicFields.Add("vehicle_model", CommonCtrl.IsNullToString(txt_vehicle_model.Tag));                       //车型 -- varchar(40)
            dicFields.Add("vehicle_model_name", txt_vehicle_model.Text);                                            //车型名称 -- varchar(40)
            dicFields.Add("vehicle_model_class", _vmClass);                                            //车型类型 -- varchar(40)


            if (selectedValue != DbDic2Enum.BILL_TYPE_YT_100000007)             //单据类型为三包服务单则不包含以下车辆信息数据字段
            {
                dicFields.Add("driving_license_no", txt_driving_license_no.Caption);                                //行驶证号 -- varchar(40)
                //dicFields.Add("production_time", "production_time");                                      //出厂时间 -- bigint(根据车工号,从宇通数据获取带入)
                //dicFields.Add("producing_area", "producing_area");                                        //产地 -- varchar(40)(根据车工号,从宇通数据获取带入)
                dicFields.Add("travel_mileage", txt_travel_mileage.Caption);                                        //行驶里程 -- decimal(15,1)
                dicFields.Add("maintain_mileage", txt_maintain_mileage.Caption);                          //建议保养里程 -- decimal(15,2)(根据车工号,从宇通数据获取带入)

            }
            dicFields.Add("depot_no", txt_depot_no.Text);                                                        //车工号(车厂编号） -- varchar(40)
            dicFields.Add("vehicle_vin", txt_vehicle_vin.Caption);                                                  //车辆vin -- varchar(40)
            dicFields.Add("engine_num", txt_engine_num.Caption);                                                    //发动机号 -- varchar(40)
            dicFields.Add("whether_second_station", chk_whether_second_station_true.Checked ? "1" : "2");           //站别 1级站 2级站 -- varchar(2)
            dicFields.Add("start_work_time", dtp_start_work_time.Value.ToUniversalTime().Ticks.ToString());         //维修开始时间 -- bigint
            dicFields.Add("complete_work_time", dtp_complete_work_time.Value.ToUniversalTime().Ticks.ToString());   //维修结束时间 -- bigint
            dicFields.Add("appraiser_id", txt_appraiser_name.Tag == null ? "" : txt_appraiser_name.Tag.ToString());   //鉴定人id -- varchar(40)
            dicFields.Add("appraiser_name", txt_appraiser_name.Text);                                            //鉴定人姓名 -- varchar(40)
            dicFields.Add("repair_man_id", txt_repair_man.Tag == null ? "" : txt_repair_man.Tag.ToString()); //维修人id -- varchar(40)
            dicFields.Add("repair_man_name", txt_repair_man.Text);                                            //维修人姓名 -- varchar(40)
            dicFields.Add("remarks", txt_remarks.Caption);                                                          //备注 -- varchar(200)
            
            if (selectedValue == DbDic2Enum.BILL_TYPE_YT_100000007 ||
                    selectedValue == DbDic2Enum.BILL_TYPE_YT_100000003 ||
                    selectedValue == DbDic2Enum.BILL_TYPE_YT_100000005 ||
                    selectedValue == DbDic2Enum.BILL_TYPE_YT_100000006 ||
                    selectedValue == DbDic2Enum.BILL_TYPE_YT_100000004)
            {
                dicFields.Add("fault_duty_corp", CommonCtrl.IsNullToString(cbo_fault_duty_corp.SelectedValue));     //故障责任单位 -- varchar(40)
                dicFields.Add("fault_cause", cbo_fault_cause.SelectedValue == null ? "" : cbo_fault_cause.SelectedValue.ToString());                             
                                                                                                                    //故障原因 -- varchar(200)
                dicFields.Add("fault_system", CommonCtrl.IsNullToString(txt_fault_system.Tag));                     //故障系统 -- varchar(40)
                dicFields.Add("fault_system_name", CommonCtrl.IsNullToString(txt_fault_system.Text));                     //故障系统 -- varchar(40)
                dicFields.Add("fault_assembly", CommonCtrl.IsNullToString(txt_fault_assembly.Tag));            //故障总成 -- varchar(40)
                dicFields.Add("fault_assembly_name", CommonCtrl.IsNullToString(txt_fault_assembly.Caption));         //故障总成 -- varchar(40)
                dicFields.Add("fault_part", CommonCtrl.IsNullToString(txt_fault_part.Tag));                    //故障部件 -- varchar(40)
                dicFields.Add("fault_part_name", CommonCtrl.IsNullToString(txt_fault_part.Caption));                //故障部件 -- varchar(40)
                dicFields.Add("fault_schema", CommonCtrl.IsNullToString(txt_fault_schema.Tag));                //故障模式 -- varchar(40)
                dicFields.Add("fault_schema_name", CommonCtrl.IsNullToString(txt_fault_schema.Text));               //故障模式 -- varchar(40)
                dicFields.Add("fault_describe", txt_fault_describe.Caption);                                        //故障描述 -- varchar(200)
                dicFields.Add("reason_analysis", txt_reason_analysis.Caption);                                      //原因分析 -- varchar(100)
                dicFields.Add("dispose_result", txt_dispose_result.Caption);                                        //处理结果 -- varchar(200)
            }
            if ((cbo_whether_go_out.SelectedValue == null ? "" : cbo_whether_go_out.SelectedValue.ToString().ToUpper()) == DbDic2Enum.TRUE)
            {
                dicFields.Add("goout_cause", txt_goout_cause.Caption);                                              //外出事由 -- varchar(50)
                dicFields.Add("goout_approver", CommonCtrl.IsNullToString(cbo_goout_approver.SelectedValue));                                           //外出批准人 -- varchar(40)
                dicFields.Add("goout_place", txt_goout_place.Caption);                                              //外出地点 -- varchar(100)
                dicFields.Add("means_traffic", cbo_means_traffic.SelectedValue == null ? "" : cbo_means_traffic.SelectedValue.ToString());                         
                                                                                                                    //交通方式 -- varchar(40)
                dicFields.Add("goout_time", dtp_goout_time.Value.ToUniversalTime().Ticks.ToString());               //外出时间 -- bigint
                dicFields.Add("goout_back_time", dtp_goout_back_time.Value.ToUniversalTime().Ticks.ToString());     //外出返回时间 -- bigint
                dicFields.Add("goout_mileage", txt_goout_mileage.Caption);                                          //外出里程 -- decimal(15,2)
                dicFields.Add("traffic_fee", txt_ccf.Caption);                                    //工时补助 -- decimal(15,2)
                dicFields.Add("goout_people_num", txt_goout_people_num.Caption);                                    //外出人数 -- decimal(15)
                dicFields.Add("journey_subsidy", txt_journey_subsidy.Caption);                                      //路程补助 -- decimal(15,2)
                dicFields.Add("man_hour_subsidy", txt_man_hour_subsidy.Caption);                                    //工时补助 -- decimal(15,2)
            }
            dicFields.Add("parts_buy_time", dtp_parts_buy_time.Value.ToUniversalTime().Ticks.ToString());           //配件购买日期 -- bigint
            dicFields.Add("parts_buy_corp", CommonCtrl.IsNullToString(txt_parts_buy_corp.Text));                                               //配件购买单位 -- varchar(40)
            dicFields.Add("contain_man_hour_cost", cbo_contain_man_hour_cost.SelectedValue.ToString());             //是否包含工时费 -- varchar(4)
            dicFields.Add("parts_code", txt_parts_code.Text);                                                       //配件编码 -- varchar(40)
            dicFields.Add("materiel_describe", txt_materiel_describe.Caption);                                       //物料描述 -- varchar(200)
            dicFields.Add("first_install_station", txt_first_install_station.Text);                                 //配件首次安装服务站 -- varchar(40)
            dicFields.Add("part_guarantee_period", cbo_part_guarantee_period.SelectedValue == null ? "" : cbo_part_guarantee_period.SelectedValue.ToString());            
                                                                                                                    //配件协议保养期 -- varchar(40)
            dicFields.Add("feedback_num", txt_feedback_num.Caption);                                                //反馈数量 -- decimal(15,2)
            dicFields.Add("enable_flag", "1");                                                                      //信息删除状态（1|有效；0|删除） -- varchar(1)

            dicFields.Add("responsible_opid", txt_responsible_opid.Tag == null ? "" : txt_responsible_opid.Tag.ToString());                                   
                                                                                                                    //经办人id -- varchar(40)
            dicFields.Add("responsible_name", txt_responsible_opid.Text);                                           //经办人姓名 -- varchar(40)
            dicFields.Add("org_name", txt_org_name.Tag == null ? "" : txt_org_name.Tag.ToString());                 //部门 -- varchar(40)

            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                _newTgId = Guid.NewGuid().ToString();
                dicFields.Add("tg_id", _newTgId);                                                                       //信息id -- varchar(40) 
                dicFields.Add("create_time", DBHelper.GetCurrentTime().ToUniversalTime().Ticks.ToString());         //创建时间 -- bigint
                dicFields.Add("create_by", GlobalStaticObj.UserID);                                                 //创建人（制单人） -- varchar(40)
                dicFields.Add("create_name", GlobalStaticObj.UserName);                                             //创建人姓名 -- varchar(40)
            }
            else if (windowStatus == WindowStatus.Edit)
            {
                dicFields.Add("update_time", DBHelper.GetCurrentTime().ToUniversalTime().Ticks.ToString());             //修改时间 -- bigint
                dicFields.Add("update_by", GlobalStaticObj.UserID);                                                     //最后修改人id -- varchar(40)
                dicFields.Add("update_name", GlobalStaticObj.UserName);                                                 //修改人姓名 -- varchar(40)
            }                           
            dicFields.Add("service_no", lbl_service_no.Text);                             //服务单号 -- varchar(40)
            dicFields.Add("service_no_yt", txt_service_no_yt.Caption);               //宇通服务单号 -- varchar(40)
            //dicFields.Add("approve_status_yt", txt_approve_status_yt.Caption);               //宇通审批状态 -- varchar(40)
            dicFields.Add("series_num_yt", txt_series_num_yt.Caption);               //宇通保单序列号 -- varchar(60)

            dicFields.Add("travel_cost", GetClf().ToString());                                   //差旅费用总计 -- decimal(15,2)
            dicFields.Add("man_hour_sum_money", GetManHourSumMoney().ToString());                                   //工时费用 -- decimal(15,2)
            dicFields.Add("fitting_sum_money", GetFittingSumMoney().ToString());                                    //配件费用 -- decimal(15,2)
            dicFields.Add("other_item_sum_money", GetOtherItemSumMoney().ToString());                               //其它费用 -- decimal(15,2)
            dicFields.Add("other_item_sum_money_remark", txt_other_money_remark.Caption);                               //其它费用 -- decimal(15,2)
            dicFields.Add("service_sum_cost", GetSumMoney().ToString());                                                  //合计费用 -- decimal(15,2)
            dicFields.Add("info_status", infoStatus);                                                               //单据状态 -- varchar(40)

            //var msg = "";
            //foreach (var dicField in dicFields)
            //{
            //    msg += (dicField.Key + " -- " )+ (dicField.Value == null ? "" : dicField.Value) + "\n";
            //}
            //MessageBoxEx.Show(msg);
            var result = DBHelper.Submit_AddOrEdit("保存三包服务单数据", "tb_maintain_three_guaranty", "tg_id", TgId, dicFields);
            if (result)
            {
                //更新前置单据导入状态
                if (!string.IsNullOrEmpty(BeforeOrderId))
                {
                    var listSql = new List<SQLObj>();
                    UpdateMaintainInfo(listSql, BeforeOrderId, "2");
                    DBHelper.BatchExeSQLMultiByTrans("更新前置单据导入状态", listSql);
                }
                return true;
            }
            //更新前置单据导入状态
            if (!string.IsNullOrEmpty(BeforeOrderId))
            {
                var listSql = new List<SQLObj>();
                UpdateMaintainInfo(listSql, BeforeOrderId, "0");
                DBHelper.BatchExeSQLMultiByTrans("更新前置单据导入状态", listSql);
            }
            return false;
        }
        private void SaveAttachement(String tgId) //保存附件信息
        {
            if (!dgv_tb_maintain_three_guaranty_accessory.CheckAttachment()) return;
            var listSql = new List<SQLObj>();
            const string opName = "附件信息";
            dgv_tb_maintain_three_guaranty_accessory.TableNameKeyValue = tgId;
            listSql.AddRange(dgv_tb_maintain_three_guaranty_accessory.AttachmentSql);
            DBHelper.BatchExeSQLMultiByTrans(opName, listSql);
        }
        private void SaveMaterialDetail(string tgId)    //三包服务单维修用料
        {
            var opName = "新增三包服务单维修用料";
            var diFileds = new Dictionary<string, string>();
            if (dgv_tb_maintain_three_matrial_detail.Rows.Count <= 0) return;
            for (var i = 0; i < dgv_tb_maintain_three_matrial_detail.Rows.Count; i++)
            {
                if (dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_parts_code"].Value == null) continue;
                diFileds.Add("depot_code", CommonCtrl.IsNullToString(dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_depot_code"].Value));
                diFileds.Add("parts_code",  CommonCtrl.IsNullToString(dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_parts_code"].Value));
                diFileds.Add("parts_name",  CommonCtrl.IsNullToString(dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_parts_name"].Value));
                diFileds.Add("norms",  CommonCtrl.IsNullToString(dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_norms"].Value));
                diFileds.Add("unit",  CommonCtrl.IsNullToString(dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_unit"].Value));
                diFileds.Add("quantity",  CommonCtrl.IsNullToString(dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_quantity"].Value));
                diFileds.Add("unit_price",  CommonCtrl.IsNullToString(dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_unit_price"].Value));
                diFileds.Add("sum_money",  CommonCtrl.IsNullToString(dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_sum_money"].Value));
                diFileds.Add("parts_source",  CommonCtrl.IsNullToString(dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_parts_source"].Value));
                diFileds.Add("redeploy_no",  CommonCtrl.IsNullToString(dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_redeploy_no"].Value));
                diFileds.Add("is_import",  CommonCtrl.IsNullToString(dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_is_import"].Value));
                diFileds.Add("remarks",  CommonCtrl.IsNullToString(dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_remarks"].Value));

                switch (windowStatus)
                {
                    case WindowStatus.Copy:
                    case WindowStatus.Add:
                        diFileds.Add("material_id", Guid.NewGuid().ToString());
                        diFileds.Add("tg_id", tgId);
                        _idColName = "material_id";
                        _idValue = string.Empty;
                        break;
                    case WindowStatus.Edit:
                        if (DBHelper.IsExist("三包服务单维修用料是否存在", "tb_maintain_three_guaranty_material_detail", " tg_id = '" + tgId + "'"))
                        {
                            var strId = dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_material_id"].Value != null ? dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_material_id"].Value.ToString() : "";
                            if (!string.IsNullOrEmpty(strId))
                            {
                                _idColName = "material_id";
                                _idValue = strId;
                                opName = "更新三包服务单维修用料";
                            }
                            else
                            {
                                diFileds.Add("material_id", Guid.NewGuid().ToString());
                                diFileds.Add("tg_id", tgId);
                                _idColName = "material_id";
                                _idValue = string.Empty;
                            }
                        }
                        else
                        {
                            diFileds.Add("material_id", Guid.NewGuid().ToString());
                            diFileds.Add("tg_id", tgId);
                            _idColName = string.Empty;
                            _idValue = string.Empty;
                        }
                        break;
                }
                DBHelper.Submit_AddOrEdit(opName, "tb_maintain_three_guaranty_material_detail", _idColName, _idValue, diFileds);
                diFileds.Clear();
            }
        }
        private void SaveProjectData(string tgId)   //三包服务单维修项目
        {
            var opName = "新增三包服务单维修项目";
            var dicFileds = new Dictionary<string, string>();
            if (dgv_tb_maintain_three_guaranty_item.Rows.Count <= 0) return;
            for (var i = 0; i < dgv_tb_maintain_three_guaranty_item.Rows.Count; i++)
            {
                if (dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_item_no"].Value == null) continue;
                dicFileds.Add("item_no", CommonCtrl.IsNullToString(dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_item_no"].Value));
                dicFileds.Add("item_name", CommonCtrl.IsNullToString(dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_item_name"].Value));
                dicFileds.Add("item_type", CommonCtrl.IsNullToString(dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_item_type"].Value));
                dicFileds.Add("man_hour_type", CommonCtrl.IsNullToString(dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_man_hour_type"].Value) == "工时" ? "1" : "2");
                dicFileds.Add("man_hour_quantity", CommonCtrl.IsNullToString(dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_man_hour_quantity"].Value));
                dicFileds.Add("man_hour_unitprice", CommonCtrl.IsNullToString(dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_man_hour_unitprice"].Value));
                dicFileds.Add("sum_money", CommonCtrl.IsNullToString(dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_sum_money"].Value));
                dicFileds.Add("remarks", CommonCtrl.IsNullToString(dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_remarks"].Value));
                switch (windowStatus)
                {
                    case WindowStatus.Copy:
                    case WindowStatus.Add:
                        dicFileds.Add("item_id", Guid.NewGuid().ToString());
                        dicFileds.Add("tg_id", tgId);
                        _idColName = "item_id";
                        _idValue = String.Empty;
                        break;
                    case WindowStatus.Edit:
                        if (DBHelper.IsExist("三包服务单维修项目是否存在", "tb_maintain_three_guaranty_item", " tg_id='" + tgId + "'"))
                        {
                            var strId = dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_item_id"].Value != null ? dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_item_id"].Value.ToString() : "";
                            if (!string.IsNullOrEmpty(strId))
                            {
                                _idColName = "item_id";
                                _idValue = strId;
                                opName = "更新三包服务单维修项目";
                            }
                            else
                            {
                                dicFileds.Add("item_id", Guid.NewGuid().ToString());
                                dicFileds.Add("tg_Id", tgId);
                                _idColName = string.Empty;
                                _idValue = string.Empty;
                            }
                        }
                        else
                        {
                            dicFileds.Add("item_id", Guid.NewGuid().ToString());
                            dicFileds.Add("tg_Id", tgId);
                            _idColName = "item_id";
                            _idValue = string.Empty;
                        }
                        break;
                }
                DBHelper.Submit_AddOrEdit(opName, "tb_maintain_three_guaranty_item", _idColName, _idValue, dicFileds);
                dicFileds.Clear();
            }
        }
        private Double GetManHourSumMoney() //工时费用
        {
            var sumMoney = 0.0;
            if (dgv_tb_maintain_three_guaranty_item.Rows.Count <= 0) return sumMoney;
            for (var i = 0; i < dgv_tb_maintain_three_guaranty_item.Rows.Count; i++)
            {
                sumMoney += Convert.ToDouble(dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_sum_money"].Value);
            }
            //其中“走保、强保”两种类型的工时合计，除了表格显示的工时费用合计，还会根据车型，再自动加一个工时补贴费，系统自算，A、B类型补贴200.00元，C类型（高档车）补贴300.00元
            if (CommonCtrl.IsNullToString(cbo_receipt_type.SelectedValue) == DbDic2Enum.BILL_TYPE_YT_100000001 || CommonCtrl.IsNullToString(cbo_receipt_type.SelectedValue) == DbDic2Enum.BILL_TYPE_YT_100000002)
            {
                if (_vmClass == DbDic2Enum.VM_CLASS_100000000 || _vmClass == DbDic2Enum.VM_CLASS_100000001) sumMoney += 200;
                if (_vmClass == DbDic2Enum.VM_CLASS_10000003) sumMoney += 300;
            }
            return sumMoney;
        }
        private Double GetFittingSumMoney() //配件费用
        {
            var sumMoney = 0.0;
            if (dgv_tb_maintain_three_matrial_detail.Rows.Count <= 0) return sumMoney;
            for (var i = 0; i < dgv_tb_maintain_three_matrial_detail.Rows.Count; i++)
            {
                var sumMoneyStr = CommonCtrl.IsNullToString(dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_sum_money"].Value);
                sumMoney += String.IsNullOrEmpty(sumMoneyStr) ? 0 : Convert.ToDouble(sumMoneyStr);
            }
            //三包单中“新车报道”“走保”“强保”类型，服务站上报宇通时可以多添加一个“辅料”，来源于宇通数据，A、B车型金额：40.00元，C车型（高档车）金额50.00元！
            if (CommonCtrl.IsNullToString(cbo_receipt_type.SelectedValue) == DbDic2Enum.BILL_TYPE_YT_100000001 || CommonCtrl.IsNullToString(cbo_receipt_type.SelectedValue) == DbDic2Enum.BILL_TYPE_YT_100000002)
            {
                if (_vmClass == DbDic2Enum.VM_CLASS_100000000 || _vmClass == DbDic2Enum.VM_CLASS_100000001) sumMoney += 40;
                if (_vmClass == DbDic2Enum.VM_CLASS_10000003) sumMoney += 50;
            }
            return sumMoney;
        }
        private Double GetOtherItemSumMoney()   //其它费用
        {
            var money = 0.0;
            Double.TryParse(txt_other_money.Caption, out money);
            return money;
        }
        private Double GetClf()   //差旅费
        {
            double journeySubsidy;
            double ccf;
            double manHourSubsidy;
            Double.TryParse(txt_journey_subsidy.Caption, out journeySubsidy);
            Double.TryParse(txt_ccf.Caption, out ccf);
            Double.TryParse(txt_man_hour_subsidy.Caption, out manHourSubsidy);
            return journeySubsidy + ccf + manHourSubsidy;
        }

        private Double GetGooutMileageMoney()
        {
            double money;
            if (cbo_means_traffic.SelectedValue == DbDic2Enum.TRAFFIC_MODE_YT_100000001)
            {
                double.TryParse(txt_journey_subsidy.Caption, out money);
            }
            else
            {

                double mileage;
                Double.TryParse(txt_goout_mileage.Caption, out mileage);
                if ((dtp_goout_time.Value.Hour <= 6 && dtp_goout_time.Value.Hour >= 0) ||
                    (dtp_goout_time.Value.Hour <= 24 && dtp_goout_time.Value.Hour >= 22))
                {
                    money = mileage*6;
                }
                else
                {
                    money = mileage*5;
                }
            }
            return money;
        }
        private Double GetManHourSubsidy()
        {
            var mancount = 0;
            Int32.TryParse(txt_goout_people_num.Caption, out mancount);
            var hours = (dtp_goout_back_time.Value - dtp_goout_time.Value).TotalHours;
            return mancount * hours * 12;
        }
        private Double GetSumMoney()    //总费用
        {
            return GetManHourSumMoney() + GetFittingSumMoney() + GetOtherItemSumMoney() + GetClf();
        }
        private void SumCostTimer() //更新费用控件
        {
            tmr_cost_sum.Start();
        }
        public void BindSetmentData()
        {
            var dt = DBHelper.GetTable("查询维修单", "tb_maintain_info", "*", " maintain_id ='" + BeforeOrderId + "'", "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            var dr = dt.Rows[0];
            txt_vehicle_no.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);
            txt_vehicle_vin.Text = CommonCtrl.IsNullToString(dr["vehicle_vin"]);
            txt_vehicle_model.Text = CommonCtrl.IsNullToString(dr["vehicle_model"]);
            txt_engine_num.Text = CommonCtrl.IsNullToString(dr["engine_no"]);
            txt_customer_code.Text = CommonCtrl.IsNullToString(dr["customer_code"]);
            txt_customer_name.Text = CommonCtrl.IsNullToString(dr["customer_name"]);

            var result = DBHelper.GetTable("验证客户编码有效性", "tb_customer", "*", " cust_code = '" + txt_customer_code.Text + "'", "", "");
            if (result == null || result.Rows.Count <= 0)
            {
                MessageBoxEx.Show("无效的客户编码", "操作提示");
                txt_customer_code.Text = String.Empty;
                txt_customer_code.Tag = String.Empty;
            }
            else
            {
                txt_customer_code.Text = result.Rows[0]["cust_code"].ToString();
                txt_customer_code.Tag = result.Rows[0]["cust_id"].ToString();
                txt_customer_name.Caption = result.Rows[0]["cust_name"].ToString();
                txt_customer_address.Caption = result.Rows[0]["cust_address"].ToString();
                txt_customer_postcode.Caption = result.Rows[0]["zip_code"].ToString();
            }
            var result1 = DBHelper.GetTable("验证车辆信息有效性", "tb_vehicle", "*", " license_plate = '" + txt_vehicle_no.Text + "'", "", "");
            if (result1 == null || result1.Rows.Count <= 0)
            {
                MessageBoxEx.Show("无效的车辆信息", "操作提示");
                txt_vehicle_no.Text = String.Empty;
            }
            else
            {
                txt_vehicle_no.Text = result1.Rows[0]["vehicle_no"].ToString();
                txt_vehicle_vin.Caption = result1.Rows[0]["vin"].ToString();
                txt_engine_num.Caption = result1.Rows[0]["engine_mun"].ToString();
                txt_vehicle_model.Text = result1.Rows[0]["v_model"].ToString();
            }

            #region 维修用料
            //var dt4MaterialDetail = DBHelper.GetTable("查询维修单用料", "tb_maintain_material_detail", "*", " maintain_id ='" + BeforeOrderId + "'", "", "");
            //if (dt4MaterialDetail == null || dt4MaterialDetail.Rows.Count == 0)
            //{
            //    return;
            //}
            //if (dt4MaterialDetail.Rows.Count > dgv_tb_maintain_three_matrial_detail.Rows.Count)
            //{
            //    dgv_tb_maintain_three_matrial_detail.Rows.Add(dt4MaterialDetail.Rows.Count -
            //                                            dgv_tb_maintain_three_matrial_detail.Rows.Count + 1);
            //}
            //for (var i = 0; i < dt4MaterialDetail.Rows.Count; i++)
            //{
            //    var dmr = dt4MaterialDetail.Rows[i];
            //    dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_material_id"].Value = CommonCtrl.IsNullToString(dmr["material_id"]);
            //    dgv_tb_maintain_three_matrial_detail.Rows[i].Cells[drtxt2_depot_code.Name].Value = CommonCtrl.IsNullToString(dmr["depot_code"]);
            //    dgv_tb_maintain_three_matrial_detail.Rows[i].Cells[drtxt2_parts_code.Name].Value = CommonCtrl.IsNullToString(dmr["parts_code"]);
            //    dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
            //    dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_norms"].Value = CommonCtrl.IsNullToString(dmr["norms"]);
            //    dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
            //    dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_quantity"].Value = CommonCtrl.IsNullToString(dmr["quantity"]);
            //    dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_unit_price"].Value = CommonCtrl.IsNullToString(dmr["unit_price"]);
            //    dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_sum_money"].Value =CommonCtrl.IsNullToString(dmr["sum_money"]);
            //    dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_parts_source"].Value =CommonCtrl.IsNullToString(dmr["parts_source"]);
            //    dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_redeploy_no"].Value =CommonCtrl.IsNullToString(dmr["redeploy_no"]);
            //    dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_remarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);

            //}
            #endregion

            var listSql = new List<SQLObj>();
            UpdateMaintainInfo(listSql, BeforeOrderId, "1");
            DBHelper.BatchExeSQLMultiByTrans("更新前置单据导入状态", listSql);
        }
        #endregion

        /// <summary>
        /// 导入后更新维修单导入状态
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="strReservId">维修单Id</param>
        /// <param name="status">操作状体，0保存开放、1导入占用、2提交审核锁定</param>
        private void UpdateMaintainInfo(ICollection<SQLObj> listSql, string strReservId, string status)
        {

            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();

            dicParam.Add("maintain_id", new ParamObj("maintain_id", strReservId, SysDbType.VarChar, 40));
            if (status == "0")
            {
                //保存时，前置单据被释放               
                dicParam.Add("Import_status", new ParamObj("Import_status", "0", SysDbType.VarChar, 40));//开放
                obj.sqlString = "update tb_maintain_info set Import_status=@Import_status where maintain_id=@maintain_id";

            }
            else if (status == "1")
            {
                //导入时，前置单据被占用 
                dicParam.Add("Import_status", new ParamObj("Import_status", "1", SysDbType.VarChar, 40));//占用
                obj.sqlString = "update tb_maintain_info set Import_status=@Import_status where maintain_id=@maintain_id";
            }
            else if (status == "2")
            {
                //审核提交时，前置单据被锁定
                dicParam.Add("Import_status", new ParamObj("Import_status", "2", SysDbType.VarChar, 40));//锁定
                obj.sqlString = "update tb_maintain_info set Import_status=@Import_status where maintain_id=@maintain_id";

            }
            obj.Param = dicParam;
            listSql.Add(obj);
        }
        /// <summary>
        /// 根据服务单ID获取信息，复制和编辑用
        /// </summary>
        private void BindData()
        {
            try
            {
                if (windowStatus != WindowStatus.Copy && windowStatus != WindowStatus.Edit) return;

                #region 基础信息

                var strWhere = string.Format(" tg_id='{0}'", TgId);
                var dt = DBHelper.GetTable("根据服务单ID查询三包服务单信息", "tb_maintain_three_guaranty", "*", strWhere, "", "");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                var dr = dt.Rows[0];
                if (windowStatus == WindowStatus.Edit)
                {
                    _orderStatus = CommonCtrl.IsNullToString(dr["info_status"]);
                    lbl_service_no.Text = CommonCtrl.IsNullToString(dr["service_no"]);
                    txt_service_no_yt.Caption = CommonCtrl.IsNullToString(dr["service_no_yt"]);
                    txt_series_num_yt.Caption = CommonCtrl.IsNullToString(dr["series_num_yt"]);
                    var errMsg = "";
                    txt_approve_status_yt.Caption = UIAssistants.GetDicName(CommonCtrl.IsNullToString(dr["approve_status_yt"]), out errMsg);
                }
                dtp_repairs_time.Value = DBHelper.GetCurrentTime(); //!String.IsNullOrEmpty(Common.UtcLongToLocalDateTime(dr["repairs_time"])) ? Convert.ToDateTime(Common.UtcLongToLocalDateTime(dr["repairs_time"])) : DBHelper.GetCurrentTime();
                txt_service_station_code.Text = GlobalStaticObj.ServerStationCode; //当前服务站代码
                txt_service_station_name.Caption = GlobalStaticObj.ServerStationName; //当前服务站名称
                chk_whether_second_station_false.Checked = GlobalStaticObj.CurrUserCom_Category == "2";    //二级站
                chk_whether_second_station_true.Checked = !chk_whether_second_station_false.Checked;    //一级站
                cbo_receipt_type.SelectedValue = CommonCtrl.IsNullToString(dr["receipt_type"]);
                cbo_whether_go_out.SelectedValue = CommonCtrl.IsNullToString(dr["whether_go_out"]);
                cbo_refit_case.SelectedValue = CommonCtrl.IsNullToString(dr["refit_case"]);
                cbo_promise_guarantee.SelectedValue = CommonCtrl.IsNullToString(dr["promise_guarantee"]);
                cbo_cost_type_service_fwhd.SelectedValue = CommonCtrl.IsNullToString(dr["cost_type_service"]);
                cbo_cost_type_service_zczg.SelectedValue = CommonCtrl.IsNullToString(dr["cost_type_policy"]);
                cbo_product_notice_no.SelectedValue = CommonCtrl.IsNullToString(dr["product_notice_no"]);
                cbo_approver_name_yt.SelectedValue = CommonCtrl.IsNullToString(dr["approver_id_yt"]);
                txt_policy_approval_no.Caption = CommonCtrl.IsNullToString(dr["policy_approval_no"]);
                txt_describes.Caption = CommonCtrl.IsNullToString(dr["describes"]);
                txt_customer_name.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);
                txt_repairer_name.Caption = CommonCtrl.IsNullToString(dr["repairer_name"]);
                txt_repairer_mobile.Caption = CommonCtrl.IsNullToString(dr["repairer_mobile"]);
                txt_customer_address.Caption = CommonCtrl.IsNullToString(dr["customer_address"]);
                txt_customer_code.Text = CommonCtrl.IsNullToString(dr["customer_code"]);
                txt_customer_postcode.Caption = CommonCtrl.IsNullToString(dr["customer_postcode"]);
                cbo_customer_property.SelectedValue = CommonCtrl.IsNullToString(dr["customer_property"]);
                txt_vehicle_use_corp.Text = CommonCtrl.IsNullToString(dr["vehicle_use_corp_name"]);          //到底是啥
                txt_vehicle_use_corp.Tag = CommonCtrl.IsNullToString(dr["vehicle_use_corp"]);
                txt_vehicle_location.Caption = CommonCtrl.IsNullToString(dr["vehicle_location"]);
                txt_linkman.Caption = CommonCtrl.IsNullToString(dr["linkman"]);
                txt_link_man_mobile.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);
                txt_repairer_name.Caption = CommonCtrl.IsNullToString(dr["repairer_name"]);
                txt_repairer_mobile.Caption = CommonCtrl.IsNullToString(dr["repairer_mobile"]);
                txt_depot_no.Text = CommonCtrl.IsNullToString(dr["depot_no"]);
                txt_vehicle_vin.Caption = CommonCtrl.IsNullToString(dr["vehicle_vin"]);
                txt_engine_num.Caption = CommonCtrl.IsNullToString(dr["engine_num"]);
                cbo_whether_yt.SelectedValue = CommonCtrl.IsNullToString(dr["whether_yt"]);
                txt_maintain_mileage.Caption = CommonCtrl.IsNullToString(dr["maintain_mileage"]);
                txt_vehicle_no.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);
                txt_travel_mileage.Caption = CommonCtrl.IsNullToString(dr["travel_mileage"]);
                dtp_maintain_time.Value = !String.IsNullOrEmpty(Common.UtcLongToLocalDateTime(dr["maintain_time"]))
                    ? Convert.ToDateTime(Common.UtcLongToLocalDateTime(dr["maintain_time"]))
                    : DBHelper.GetCurrentTime();
                txt_vehicle_model.Tag = CommonCtrl.IsNullToString(dr["vehicle_model"]);
                txt_vehicle_model.Text = CommonCtrl.IsNullToString(dr["vehicle_model_name"]);
                _vmClass = CommonCtrl.IsNullToString(dr["vehicle_model_class"]);
                txt_driving_license_no.Caption = CommonCtrl.IsNullToString(dr["driving_license_no"]);
                dtp_start_work_time.Value = !String.IsNullOrEmpty(Common.UtcLongToLocalDateTime(dr["start_work_time"]))
                    ? Convert.ToDateTime(Common.UtcLongToLocalDateTime(dr["start_work_time"]))
                    : DBHelper.GetCurrentTime();
                dtp_complete_work_time.Value = !String.IsNullOrEmpty(Common.UtcLongToLocalDateTime(dr["complete_work_time"]))
                    ? Convert.ToDateTime(Common.UtcLongToLocalDateTime(dr["complete_work_time"]))
                    : DBHelper.GetCurrentTime();
                txt_appraiser_name.Text = CommonCtrl.IsNullToString(dr["appraiser_name"]);
                txt_appraiser_name.Tag = CommonCtrl.IsNullToString(dr["appraiser_id"]);
                txt_repair_man.Text = CommonCtrl.IsNullToString(dr["repair_man_name"]);
                txt_repair_man.Tag = CommonCtrl.IsNullToString(dr["repair_man_id"]);
                txt_remarks.Caption = CommonCtrl.IsNullToString(dr["remarks"]);
                txt_fault_system.Tag = CommonCtrl.IsNullToString(dr["fault_system"]);
                txt_fault_system.Text = CommonCtrl.IsNullToString(dr["fault_system_name"]);
                txt_fault_schema.Tag = CommonCtrl.IsNullToString(dr["fault_schema"]);
                txt_fault_schema.Text = CommonCtrl.IsNullToString(dr["fault_schema_name"]);
                txt_fault_part.Tag = CommonCtrl.IsNullToString(dr["fault_part"]);
                txt_fault_part.Caption = CommonCtrl.IsNullToString(dr["fault_part_name"]);
                txt_fault_assembly.Tag = CommonCtrl.IsNullToString(dr["fault_assembly"]);
                txt_fault_assembly.Caption = CommonCtrl.IsNullToString(dr["fault_assembly_name"]);
                if (cbo_fault_duty_corp.Items.Contains(CommonCtrl.IsNullToString(dr["fault_duty_corp"])))
                    cbo_fault_duty_corp.SelectedValue = CommonCtrl.IsNullToString(dr["fault_duty_corp"]);
                txt_fault_describe.Caption = CommonCtrl.IsNullToString(dr["fault_describe"]);
                cbo_fault_cause.SelectedValue = CommonCtrl.IsNullToString(dr["fault_cause"]);
                txt_reason_analysis.Caption = CommonCtrl.IsNullToString(dr["reason_analysis"]);
                txt_dispose_result.Caption = CommonCtrl.IsNullToString(dr["dispose_result"]);
                txt_goout_cause.Caption = CommonCtrl.IsNullToString(dr["goout_cause"]);
                txt_goout_place.Caption = CommonCtrl.IsNullToString(dr["goout_place"]);
                cbo_goout_approver.SelectedValue = CommonCtrl.IsNullToString(dr["goout_approver"]);
                txt_ccf.Caption = CommonCtrl.IsNullToString(dr["traffic_fee"]);
                cbo_means_traffic.SelectedValue = CommonCtrl.IsNullToString(dr["means_traffic"]);
                dtp_goout_time.Value = !String.IsNullOrEmpty(Common.UtcLongToLocalDateTime(dr["goout_time"]))
                    ? Convert.ToDateTime(Common.UtcLongToLocalDateTime(dr["goout_time"]))
                    : DBHelper.GetCurrentTime();
                dtp_goout_back_time.Value = !String.IsNullOrEmpty(Common.UtcLongToLocalDateTime(dr["goout_back_time"]))
                    ? Convert.ToDateTime(Common.UtcLongToLocalDateTime(dr["goout_back_time"]))
                    : DBHelper.GetCurrentTime();
                txt_goout_mileage.Caption = CommonCtrl.IsNullToString(dr["goout_mileage"]);
                txt_goout_people_num.Caption = CommonCtrl.IsNullToString(dr["goout_people_num"]);
                txt_journey_subsidy.Caption = CommonCtrl.IsNullToString(dr["journey_subsidy"]);
                txt_man_hour_subsidy.Caption = CommonCtrl.IsNullToString(dr["man_hour_subsidy"]);
                dtp_parts_buy_time.Value = !String.IsNullOrEmpty(Common.UtcLongToLocalDateTime(dr["parts_buy_time"]))
                    ? Convert.ToDateTime(Common.UtcLongToLocalDateTime(dr["parts_buy_time"]))
                    : DBHelper.GetCurrentTime();
                txt_parts_buy_corp.Text = CommonCtrl.IsNullToString(dr["parts_buy_corp"]);
                cbo_contain_man_hour_cost.SelectedValue = CommonCtrl.IsNullToString(dr["contain_man_hour_cost"]);
                txt_parts_code.Text = CommonCtrl.IsNullToString(dr["parts_code"]);
                txt_materiel_describe.Caption = CommonCtrl.IsNullToString(dr["materiel_describe"]);
                txt_first_install_station.Text = CommonCtrl.IsNullToString(dr["first_install_station"]);
                cbo_part_guarantee_period.Text = CommonCtrl.IsNullToString(dr["part_guarantee_period"]);
                txt_feedback_num.Caption = CommonCtrl.IsNullToString(dr["feedback_num"]);
                txt_other_money.Caption = CommonCtrl.IsNullToString(dr["other_item_sum_money"]);
                txt_other_money_remark.Caption = CommonCtrl.IsNullToString(dr["other_item_sum_money_remark"]);
                ValidationCustomer();
                ValidationDepto();
                #endregion

                #region 底部datagridview数据

                #region 维修项目数据

                //维修项目数据                
                var dpt = DBHelper.GetTable("维修项目数据", "tb_maintain_three_guaranty_item", "*",
                    string.Format(" tg_id='{0}'", TgId), "", "");
                if (dpt.Rows.Count > 0)
                {
                    if (dpt.Rows.Count > dgv_tb_maintain_three_guaranty_item.Rows.Count)
                    {
                        dgv_tb_maintain_three_guaranty_item.Rows.Add(dpt.Rows.Count - dgv_tb_maintain_three_guaranty_item.Rows.Count +
                                                               1);
                    }
                    for (var i = 0; i < dpt.Rows.Count; i++)
                    {
                        var dpr = dpt.Rows[i];
                        dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_item_id"].Value =
                            CommonCtrl.IsNullToString(dpr["item_id"]);
                        dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_item_no"].Value =
                            CommonCtrl.IsNullToString(dpr["item_no"]);
                        dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_item_type"].Value =
                            CommonCtrl.IsNullToString(dpr["item_type"]);
                        dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_item_name"].Value =
                            CommonCtrl.IsNullToString(dpr["item_name"]);
                        dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_man_hour_type"].Value =
                            CommonCtrl.IsNullToString(dpr["man_hour_type"]) == "1" ? "工时" : "定额";
                        dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_man_hour_quantity"].Value =
                            CommonCtrl.IsNullToString(dpr["man_hour_quantity"]);
                        dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_man_hour_unitprice"].Value =
                            CommonCtrl.IsNullToString(dpr["man_hour_unitprice"]);
                        dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_sum_money"].Value =
                            CommonCtrl.IsNullToString(dpr["sum_money"]);
                        dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_remarks"].Value =
                            CommonCtrl.IsNullToString(dpr["remarks"]);

                    }
                }

                #endregion

                #region 维修用料数据

                //维修用料数据
                var dmt = DBHelper.GetTable("维修用料数据", "tb_maintain_three_guaranty_material_detail", "*",
                    string.Format(" tg_id='{0}'", TgId), "", "");
                if (dmt.Rows.Count > 0)
                {

                    if (dmt.Rows.Count > dgv_tb_maintain_three_matrial_detail.Rows.Count)
                    {
                        dgv_tb_maintain_three_matrial_detail.Rows.Add(dmt.Rows.Count -
                                                                dgv_tb_maintain_three_matrial_detail.Rows.Count + 1);
                    }
                    for (var i = 0; i < dmt.Rows.Count; i++)
                    {
                        var dmr = dmt.Rows[i];
                        dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_material_id"].Value = CommonCtrl.IsNullToString(dmr["material_id"]);
                        dgv_tb_maintain_three_matrial_detail.Rows[i].Cells[drtxt2_depot_code.Name].Value = CommonCtrl.IsNullToString(dmr["depot_code"]);
                        dgv_tb_maintain_three_matrial_detail.Rows[i].Cells[drtxt2_parts_code.Name].Value =  CommonCtrl.IsNullToString(dmr["parts_code"]);
                        dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
                        dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_norms"].Value =
                            CommonCtrl.IsNullToString(dmr["norms"]);
                        dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_unit"].Value =
                            CommonCtrl.IsNullToString(dmr["unit"]);
                        dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_quantity"].Value =
                            CommonCtrl.IsNullToString(dmr["quantity"]);
                        dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_unit_price"].Value =
                            CommonCtrl.IsNullToString(dmr["unit_price"]);
                        dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_sum_money"].Value =
                            CommonCtrl.IsNullToString(dmr["sum_money"]);
                        dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_parts_source"].Value =
                            CommonCtrl.IsNullToString(dmr["parts_source"]);
                        dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_redeploy_no"].Value =
                            CommonCtrl.IsNullToString(dmr["redeploy_no"]);
                        dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_remarks"].Value =
                            CommonCtrl.IsNullToString(dmr["remarks"]);

                    }
                }

                #endregion

                //附件信息数据
                dgv_tb_maintain_three_guaranty_accessory.BindAttachment();

                #endregion

                if (windowStatus == WindowStatus.Copy)
                {
                    TgId = String.Empty;
                }
            }
            catch (Exception ex)
            {
                //write log
            }
        }

        #region 选择器信息验证
        private void ValidationCustomer()
        {
            if (!String.IsNullOrEmpty(txt_customer_code.Text))
            {
                var result = DBHelper.GetTable("验证客户编码有效性", "tb_customer", "*", " cust_code = '" + txt_customer_code.Text + "'", "", "");
                if (result == null || result.Rows.Count <= 0)
                {
                    MessageBoxEx.Show("无效的客户编码", "操作提示");
                    txt_customer_code.Text = String.Empty;
                    txt_customer_code.Tag = String.Empty;
                }
                else
                {
                    txt_customer_code.Text = result.Rows[0]["cust_code"].ToString();
                    txt_customer_code.Tag = result.Rows[0]["cust_id"].ToString();
                    txt_customer_name.Caption = result.Rows[0]["cust_name"].ToString();
                    txt_customer_address.Caption = result.Rows[0]["cust_address"].ToString();
                    txt_customer_postcode.Caption = result.Rows[0]["zip_code"].ToString();
                }
            }
            else
            {
                txt_customer_code.Text = String.Empty;
                txt_customer_code.Tag = String.Empty;
            }
        }
        private void ValidationVehicle()
        {
            if (!String.IsNullOrEmpty(txt_vehicle_no.Text))
            {
                var result = DBHelper.GetTable("验证车辆信息有效性", "tb_vehicle", "*", " license_plate = '" + txt_vehicle_no.Text + "'", "", "");
                if (result == null || result.Rows.Count <= 0)
                {
                    MessageBoxEx.Show("无效的车辆信息", "操作提示");
                    txt_vehicle_no.Text = String.Empty;
                    txt_vehicle_no.Tag = String.Empty;
                }
                else
                {
                    txt_vehicle_no.Text = result.Rows[0]["license_plate"].ToString();
                    txt_vehicle_vin.Caption = result.Rows[0]["vin"].ToString();
                    txt_engine_num.Caption = result.Rows[0]["engine_num"].ToString();
                    txt_vehicle_model.Tag = result.Rows[0]["v_model"].ToString();
                }
            }
            else
            {
                txt_vehicle_no.Text = String.Empty;
                txt_vehicle_no.Tag = String.Empty;
            }
            if (!String.IsNullOrEmpty(CommonCtrl.IsNullToString(txt_vehicle_model.Tag)))
            {
                var result1 = DBHelper.GetTable("验证车型信息有效性", "tb_vehicle_models", "*", " vm_id = '" + txt_vehicle_model.Tag + "'", "", "");
                if (result1 == null || result1.Rows.Count <= 0)
                {
                    MessageBoxEx.Show("无效的车型信息", "操作提示");
                    txt_vehicle_model.Text = String.Empty;
                    txt_vehicle_model.Tag = String.Empty;
                }
                else
                {
                    txt_vehicle_model.Text = result1.Rows[0]["vm_name"].ToString();
                    _vmClass = result1.Rows[0]["vm_class"].ToString();
                }
            }
            else
            {
                txt_vehicle_model.Text = String.Empty;
                txt_vehicle_model.Tag = String.Empty;
            }
        }
        private void ValidationDepto()
        {
            if (!String.IsNullOrEmpty(txt_vehicle_no.Text))
            {
                var result = DBHelper.GetTable("验证车辆信息有效性", "tb_vehicle", "*", " turner = '" + txt_depot_no.Text + "'", "", "");
                if (result == null || result.Rows.Count <= 0)
                {
                    MessageBoxEx.Show("无效的车辆信息", "操作提示");
                    txt_vehicle_no.Text = String.Empty;
                }
                else
                {
                    txt_vehicle_no.Text = result.Rows[0]["license_plate"].ToString();
                    txt_vehicle_vin.Caption = result.Rows[0]["vin"].ToString();
                    txt_engine_num.Caption = result.Rows[0]["engine_num"].ToString();
                    txt_vehicle_model.Text = result.Rows[0]["v_model"].ToString();
                }
            }
            else
            {
                txt_vehicle_no.Text = String.Empty;
            }
            if (!String.IsNullOrEmpty(CommonCtrl.IsNullToString(txt_vehicle_model.Tag)))
            {
                var result1 = DBHelper.GetTable("验证车型信息有效性", "tb_vehicle_models", "*", " vm_id = '" + txt_vehicle_model.Tag + "'", "", "");
                if (result1 == null || result1.Rows.Count <= 0)
                {
                    MessageBoxEx.Show("无效的车型信息", "操作提示");
                    txt_vehicle_model.Text = String.Empty;
                    txt_vehicle_model.Tag = String.Empty;
                }
                else
                {
                    txt_vehicle_model.Text = result1.Rows[0]["vm_name"].ToString();
                    _vmClass = result1.Rows[0]["vm_class"].ToString();
                }
            }
            else
            {
                txt_vehicle_model.Text = String.Empty;
                txt_vehicle_model.Tag = String.Empty;
            }
        }

        //故障系统

        //故障模式
        #endregion
        #endregion

        #region Event -- 事件
        private void tmr_cost_sum_Tick(object sender, EventArgs e)
        {
            lbl_fitting_sum_money.Text = GetFittingSumMoney().ToString();
            lbl_man_hour_sum_money.Text = GetManHourSumMoney().ToString();
            lbl_other_item_sum_money.Text = GetOtherItemSumMoney().ToString();
            lbl_service_sum_cost.Text = GetSumMoney().ToString();
            lbl_clf.Text = GetClf().ToString();
            txt_journey_subsidy.Caption = GetGooutMileageMoney().ToString();
            txt_man_hour_subsidy.Caption = GetManHourSubsidy().ToString();
        }
        #endregion
    }
}
