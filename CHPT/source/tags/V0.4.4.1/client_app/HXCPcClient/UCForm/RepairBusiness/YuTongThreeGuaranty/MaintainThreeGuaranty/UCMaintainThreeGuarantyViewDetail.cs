using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
using Utility.Common;

namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuaranty
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     UCMaintainThreeGuarantyViewDetail         
    /// Author:       Kord
    /// Date:         2014/10/29 11:00:20
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	维修服务-宇通三包-三包服务单-新增/复制
    ///***************************************************************************//
    public partial class UCMaintainThreeGuarantyViewDetail : UCBase
    {
        #region Constructor -- 构造函数
        public UCMaintainThreeGuarantyViewDetail()
        {
            InitializeComponent();

            Load += (sender, args) =>
            {
                Init();
                BindData();
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
              btnCancel, btnExport, btnSet, btnPrint
            };
            UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
        }
        private void InitEventHandle() //注册控件相关事件
        {
            lnk_yt_approve_status.LinkClicked += delegate
            {
                new UCApproveStatusInfo(TgId).ShowDialog(this);
            };
            btnCancel.Click += delegate
            {
                UCForm.BindPageData("enable_flag = '1'");
                deleteMenuByTag(Tag.ToString(), UCForm.Name);
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

                CommonCtrl.CmbBindDict(cbo_receipt_type, "bill_type_yt", false);   //宇通单据类型
                CommonCtrl.CmbBindDict(cbo_cost_type_service_zczg, "cost_type_care_policy_yt", false);   //费用类型(政策照顾)
                CommonCtrl.BindComboBoxByTable(cbo_product_notice_no, "tb_product_no", "service_code", "activities", false);   //产品改进通知

                CommonCtrl.CmbBindDict(cbo_whether_go_out, "sys_true_false", false);   //是否外出
                cbo_whether_go_out.SelectedValue = DbDic2Enum.FALSE;    //是否外出(默认选择为否)

                CommonCtrl.CmbBindDict(cbo_refit_case, "refit_case", false);   //改装情况
                cbo_refit_case.SelectedValue = DbDic2Enum.REFIT_CASE_FALSE;    //改装请款(默认选择为无改装)

                DataSources.BindComBoxDataEnum(cbo_promise_guarantee, typeof(DataSources.EnumYesNo), false);   //特殊约定质保
                cbo_promise_guarantee.SelectedIndex = 1;    //特殊约定质保(默认选择为否)
                UIAssistants.BindingServiceStationUser(cbo_approver_name_yt, true, "请选择");
                txt_appraiser_name.ChooserClick += delegate   //宇通批准人(鉴定人)选择器
                {
                    var chooser = new frmUsers{ cbo_data_source = { SelectedValue = "2", Enabled = false } };
                    var result = chooser.ShowDialog();
                    if (result != DialogResult.OK) return;
                    if (String.IsNullOrEmpty(chooser.User_Name) || String.IsNullOrEmpty(chooser.CrmId))
                    {
                        MessageBoxEx.Show("选择的数据不符合使用要求");
                        return;
                    }
                    //txt_approver_name_yt.Text = chooser.contName;
                    //txt_approver_name_yt.Tag = chooser.crmId;
                    txt_appraiser_name.Text = chooser.User_Name;
                    txt_appraiser_name.Tag = chooser.CrmId;
                };
                txt_repair_man.ChooserClick += delegate   //维修人
                {
                    var chooser = new frmUsers{ cbo_data_source = { SelectedValue = "2", Enabled = false } };
                    var result = chooser.ShowDialog();
                    if (result != DialogResult.OK) return;
                    if (String.IsNullOrEmpty(chooser.User_Name) || String.IsNullOrEmpty(chooser.CrmId))
                    {
                        MessageBoxEx.Show("选择的数据不符合使用要求");
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
                txt_fault_system.Leave += delegate
                {
                    ValidationJDR();
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
                dgv_tb_maintain_three_guaranty_item.ReadOnly = true;
                dgv_tb_maintain_three_guaranty_item.Rows.Add(3);
                dgv_tb_maintain_three_guaranty_item.AllowUserToAddRows = true;
                drtxt1_item_no.ReadOnly = true;
                drtxt1_item_type.ReadOnly = true;
                drtxt1_item_name.ReadOnly = true;
                drtxt1_man_hour_type.ReadOnly = true;
                drtxt1_man_hour_quantity.ReadOnly = true;
                drtxt1_man_hour_unitprice.ReadOnly = true;
                drtxt1_sum_money.ReadOnly = true;
                drtxt1_remarks.ReadOnly = true;
                #endregion

                #region 维修用料数据表格设置
                dgv_tb_maintain_three_matrial_detail.Dock = DockStyle.Fill;
                dgv_tb_maintain_three_matrial_detail.ReadOnly = true;
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
                drtxt2_quantity.ReadOnly = true;
                drtxt2_unit_price.ReadOnly = true;
                drtxt2_sum_money.ReadOnly = true;
                drtxt2_parts_source.ReadOnly = true;
                drtxt2_redeploy_no.ReadOnly = true;
                drtxt2_remarks.ReadOnly = true;

                CommonCtrl.BindComboBoxByDictionarr(drtxt2_parts_source, "new_parts_source_yt");
                #endregion
            }
            catch (Exception)
            {
                
            }
        }

        #region 数据保存
        private Double GetManHourSumMoney() //工时费用
        {
            var sumMoney = 0.0;
            if (dgv_tb_maintain_three_guaranty_item.Rows.Count <= 0) return sumMoney;
            for (var i = 0; i < dgv_tb_maintain_three_guaranty_item.Rows.Count; i++)
            {
                Double a;
                var result = Double.TryParse(CommonCtrl.IsNullToString(dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_sum_money"].Value), out a);
                if (result)
                {
                    sumMoney += a;
                }
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
        #endregion

        /// <summary>
        /// 根据服务单ID获取信息，复制和编辑用
        /// </summary>
        private void BindData()
        {
            try
            {
                if (windowStatus != WindowStatus.View) return;

                #region 基础信息

                var strWhere = string.Format(" tg_id='{0}'", TgId);
                var dt = DBHelper.GetTable("根据服务单ID查询三包服务单信息", "tb_maintain_three_guaranty", "*", strWhere, "", "");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                var dr = dt.Rows[0];
                _orderStatus = CommonCtrl.IsNullToString(dr["info_status"]);
                lbl_service_no.Text = CommonCtrl.IsNullToString(dr["service_no"]);
                txt_service_no_yt.Caption = CommonCtrl.IsNullToString(dr["service_no_yt"]);
                txt_series_num_yt.Caption = CommonCtrl.IsNullToString(dr["series_num_yt"]);
                var errMsg = "";
                lnk_yt_approve_status.Text = UIAssistants.GetDicName(CommonCtrl.IsNullToString(dr["approve_status_yt"]));
                dtp_repairs_time.Value = !String.IsNullOrEmpty(Common.UtcLongToLocalDateTime(dr["repairs_time"])) ? Convert.ToDateTime(Common.UtcLongToLocalDateTime(dr["repairs_time"])) : DBHelper.GetCurrentTime();
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
                dtp_repair_completion_time.Value = !String.IsNullOrEmpty(Common.UtcLongToLocalDateTime(dr["repair_completion_time"]))
                    ? Convert.ToDateTime(Common.UtcLongToLocalDateTime(dr["repair_completion_time"]))
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
                if (!String.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["fault_duty_corp"])))
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
        private void ValidationJDR()
        {
            return;
            if (!String.IsNullOrEmpty(CommonCtrl.IsNullToString(txt_fault_system.Tag)))
            {
                var result = DBHelper.GetTable("验证鉴定人有效性", "tb_vehicle", "*", " license_plate = '" + txt_vehicle_no.Text + "'", "", "");
                if (result == null || result.Rows.Count <= 0)
                {
                    MessageBoxEx.Show("无效的鉴定人信息", "操作提示");
                    txt_fault_system.Text = String.Empty;
                    txt_fault_system.Tag = String.Empty;
                }
                else
                {
                    //txt_fault_system.Text = chooser.SystemName;
                    //txt_fault_system.Tag = chooser.SystemCode;
                    //txt_fault_assembly.Caption = chooser.AssemblyName;
                    //txt_fault_assembly.Tag = chooser.AssemblyCode;
                    //txt_fault_part.Caption = chooser.PartmName;
                    //txt_fault_system.Tag = chooser.PartCode;
                    //txt_fault_schema.Text = chooser.SchemaName;
                    //txt_fault_schema.Tag = chooser.SchemaCode;
                }
            }
            else
            {
                txt_vehicle_no.Text = String.Empty;
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
