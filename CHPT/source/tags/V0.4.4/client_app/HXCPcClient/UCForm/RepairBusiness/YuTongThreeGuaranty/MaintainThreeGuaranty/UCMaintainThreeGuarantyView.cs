using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuaranty
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     MaintainThreeGuarantyBrowser         
    /// Author:       Kord
    /// Date:         2014/10/29 11:01:09
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	维修服务-宇通三包-三包服务单浏览
    ///***************************************************************************//
    public partial class UCMaintainThreeGuarantyView : UCBase
    {
        #region Constructor -- 构造函数
        public UCMaintainThreeGuarantyView()
        {
            InitializeComponent();

            Init();
        }
        #endregion

        #region Field -- 字段

        #endregion

        #region Property -- 属性
        /// <summary>
        /// 服务单信息ID
        /// </summary>
        public string TgId { get; set; }
        #endregion

        #region Method -- 方法
        private void Init() //初始化
        {
            SetFuncationButtonVisible();

            InitEventHandle();
        }
        private void InitEventHandle()  //注册控件相关事件
        {
            lnk_approve_status_yt.LinkClicked += delegate   //服务单审核详情查看
            {
                if (String.IsNullOrEmpty(TgId)) return;
                var approveStateInfo = new UCApproveStatusInfo(TgId) {StartPosition = FormStartPosition.CenterParent};
                approveStateInfo.ShowDialog(this);
            };
        }
        private void SetFuncationButtonVisible() //设置功能按钮可见性
        {
            var btnCols = new ObservableCollection<ButtonEx_sms>
            {
                btnAdd, btnCopy, btnEdit, btnDelete, btnActivation, btnVerify, btnSave, btnCommit, btnCancel, btnExport, btnImport, btnRevoke, btnView, btnPrint, btnSet
            };
            UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
        }
        /// <summary>
        /// 根据服务单ID获取信息，复制和编辑用
        /// </summary>
        //private void BindData()
        //{
        //    if (windowStatus != WindowStatus.Copy && windowStatus != WindowStatus.Edit) return;

        //    #region 基础信息

        //    var strWhere = string.Format(" tg_id='{0}'", TgId);
        //    var dt = DBHelper.GetTable("根据服务单ID查询三包服务单信息", "tb_maintain_three_guaranty", "*", strWhere, "", "");
        //    if (dt == null || dt.Rows.Count == 0)
        //    {
        //        return;
        //    }
        //    var dr = dt.Rows[0];
        //    dtp_repairs_time.Value = !String.IsNullOrEmpty(Common.UtcLongToLocalDateTime(dr["repairs_time"])) ? Convert.ToDateTime(Common.UtcLongToLocalDateTime(dr["repairs_time"])) : DBHelper.GetCurrentTime();
        //    //txt_service_station_code.Text = GlobalStaticObj.unknow; //当前服务站代码
        //    //txt_service_station_name.Caption = GlobalStaticObj.unknow; //当前服务站名称
        //    //chk_whether_second_station_false.Checked = GlobalStaticObj.unknow;    //是否为二级站
        //    //chk_whether_second_station_false.Checked = GlobalStaticObj.unknow;    //是否为二级站
        //    cbo_receipt_type.SelectedValue = CommonCtrl.IsNullToString(dr["receipt_type"]);
        //    cbo_whether_go_out.SelectedValue = CommonCtrl.IsNullToString(dr["whether_go_out"]);
        //    cbo_refit_case.SelectedValue = CommonCtrl.IsNullToString(dr["refit_case"]);
        //    cbo_promise_guarantee.SelectedValue = CommonCtrl.IsNullToString(dr["promise_guarantee"]);
        //    cbo_cost_type_service_fwhd.SelectedValue = CommonCtrl.IsNullToString(dr["cost_type_service"]);
        //    cbo_cost_type_service_zczg.SelectedValue = CommonCtrl.IsNullToString(dr["cost_type_policy"]);
        //    cbo_product_notice_no.SelectedValue = CommonCtrl.IsNullToString(dr["product_notice_no"]);
        //    txt_approver_name_yt.Text = CommonCtrl.IsNullToString(dr["approver_name_yt"]);
        //    txt_policy_approval_no.Caption = CommonCtrl.IsNullToString(dr["policy_approval_no"]);
        //    txt_describes.Caption = CommonCtrl.IsNullToString(dr["describes"]);
        //    txt_customer_name.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);
        //    txt_repairer_name.Caption = CommonCtrl.IsNullToString(dr["repairer_name"]);
        //    txt_repairer_mobile.Caption = CommonCtrl.IsNullToString(dr["repairer_mobile"]);
        //    txt_customer_address.Caption = CommonCtrl.IsNullToString(dr["customer_address"]);
        //    txt_customer_code.Text = CommonCtrl.IsNullToString(dr["customer_code"]);
        //    txt_customer_postcode.Caption = CommonCtrl.IsNullToString(dr["customer_postcode"]);
        //    cbo_customer_property.SelectedValue = CommonCtrl.IsNullToString(dr["customer_property"]);
        //    txt_vehicle_use_corp.Caption = CommonCtrl.IsNullToString(dr["vehicle_use_corp"]);
        //    txt_vehicle_location.Text = CommonCtrl.IsNullToString(dr["vehicle_location"]);
        //    txt_linkman.Caption = CommonCtrl.IsNullToString(dr["linkman"]);
        //    txt_link_man_mobile.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);
        //    txt_repairer_name.Caption = CommonCtrl.IsNullToString(dr["repairer_name"]);
        //    txt_repairer_mobile.Caption = CommonCtrl.IsNullToString(dr["repairer_mobile"]);
        //    txt_depot_no.Caption = CommonCtrl.IsNullToString(dr["depot_no"]);
        //    txt_vehicle_vin.Caption = CommonCtrl.IsNullToString(dr["vehicle_vin"]);
        //    txt_engine_num.Caption = CommonCtrl.IsNullToString(dr["engine_num"]);
        //    cbo_whether_yt.SelectedValue = CommonCtrl.IsNullToString(dr["whether_yt"]);
        //    txt_maintain_mileage.Caption = CommonCtrl.IsNullToString(dr["maintain_mileage"]);
        //    txt_vehicle_no.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);
        //    txt_travel_mileage.Caption = CommonCtrl.IsNullToString(dr["travel_mileage"]);
        //    txt_maintain_time.Caption = CommonCtrl.IsNullToString(dr["maintain_time"]);
        //    txt_driver_mobile.Caption = CommonCtrl.IsNullToString(dr["driver_mobile"]);
        //    txt_vehicle_model.Text = CommonCtrl.IsNullToString(dr["vehicle_model"]);
        //    txt_driving_license_no.Caption = CommonCtrl.IsNullToString(dr["driving_license_no"]);
        //    txt_driver_name.Caption = CommonCtrl.IsNullToString(dr["driver_name"]);
        //    dtp_start_work_time.Value = !String.IsNullOrEmpty(Common.UtcLongToLocalDateTime(dr["start_work_time"]))
        //        ? Convert.ToDateTime(Common.UtcLongToLocalDateTime(dr["start_work_time"]))
        //        : DBHelper.GetCurrentTime();
        //    dtp_complete_work_time.Value = !String.IsNullOrEmpty(Common.UtcLongToLocalDateTime(dr["complete_work_time"]))
        //        ? Convert.ToDateTime(Common.UtcLongToLocalDateTime(dr["complete_work_time"]))
        //        : DBHelper.GetCurrentTime();
        //    txt_appraiser_name.Caption = CommonCtrl.IsNullToString(dr["appraiser_name"]);

        //    txt_remarks.Caption = CommonCtrl.IsNullToString(dr["remarks"]);
        //    txt_fault_system.Text = CommonCtrl.IsNullToString(dr["fault_system"]);
        //    txt_fault_schema.Caption = CommonCtrl.IsNullToString(dr["fault_schema"]);
        //    txt_fault_part.Caption = CommonCtrl.IsNullToString(dr["fault_part"]);
        //    txt_fault_assembly.Caption = CommonCtrl.IsNullToString(dr["fault_assembly"]);
        //    txt_fault_duty_corp.Caption = CommonCtrl.IsNullToString(dr["fault_duty_corp"]);
        //    txt_fault_describe.Caption = CommonCtrl.IsNullToString(dr["fault_describe"]);
        //    cbo_fault_cause.SelectedValue = CommonCtrl.IsNullToString(dr["fault_cause"]);
        //    txt_reason_analysis.Caption = CommonCtrl.IsNullToString(dr["reason_analysis"]);
        //    txt_dispose_result.Caption = CommonCtrl.IsNullToString(dr["dispose_result"]);
        //    txt_goout_cause.Caption = CommonCtrl.IsNullToString(dr["goout_cause"]);
        //    txt_goout_approver.Text = CommonCtrl.IsNullToString(dr["goout_approver"]);
        //    txt_goout_place.Caption = CommonCtrl.IsNullToString(dr["goout_place"]);
        //    cbo_means_traffic.SelectedValue = CommonCtrl.IsNullToString(dr["means_traffic"]);
        //    dtp_goout_time.Value = !String.IsNullOrEmpty(Common.UtcLongToLocalDateTime(dr["goout_time"]))
        //        ? Convert.ToDateTime(Common.UtcLongToLocalDateTime(dr["goout_time"]))
        //        : DBHelper.GetCurrentTime();
        //    dtp_goout_back_time.Value = !String.IsNullOrEmpty(Common.UtcLongToLocalDateTime(dr["goout_back_time"]))
        //        ? Convert.ToDateTime(Common.UtcLongToLocalDateTime(dr["goout_back_time"]))
        //        : DBHelper.GetCurrentTime();
        //    txt_goout_mileage.Caption = CommonCtrl.IsNullToString(dr["goout_mileage"]);
        //    txt_goout_people_num.Caption = CommonCtrl.IsNullToString(dr["goout_people_num"]);
        //    txt_journey_subsidy.Caption = CommonCtrl.IsNullToString(dr["journey_subsidy"]);
        //    txt_man_hour_subsidy.Caption = CommonCtrl.IsNullToString(dr["man_hour_subsidy"]);
        //    dtp_parts_buy_time.Value = !String.IsNullOrEmpty(Common.UtcLongToLocalDateTime(dr["parts_buy_time"]))
        //        ? Convert.ToDateTime(Common.UtcLongToLocalDateTime(dr["parts_buy_time"]))
        //        : DBHelper.GetCurrentTime();
        //    txt_parts_buy_corp.Text = CommonCtrl.IsNullToString(dr["parts_buy_corp"]);
        //    cbo_contain_man_hour_cost.SelectedValue = CommonCtrl.IsNullToString(dr["contain_man_hour_cost"]);
        //    txt_parts_code.Text = CommonCtrl.IsNullToString(dr["contain_man_hour_cost"]);
        //    txt_materiel_describe.Caption = CommonCtrl.IsNullToString(dr["materiel_describe"]);
        //    txt_first_install_station.Text = CommonCtrl.IsNullToString(dr["first_install_station"]);
        //    cbo_part_guarantee_period.Text = CommonCtrl.IsNullToString(dr["part_guarantee_period"]);
        //    txt_feedback_num.Caption = CommonCtrl.IsNullToString(dr["feedback_num"]);

        //    lbl_responsible_name.Text = CommonCtrl.IsNullToString(dr["responsible_name"]);
        //    lbl_org_name.Text = CommonCtrl.IsNullToString(dr["org_name"]);
        //    lbl_create_name.Text = CommonCtrl.IsNullToString(dr["create_name"]);
        //    lbl_create_time.Text = Common.UtcLongToLocalDateTime(dr["create_time"]);
        //    lbl_update_name.Text = CommonCtrl.IsNullToString(dr["update_name"]);
        //    lbl_update_time.Text = Common.UtcLongToLocalDateTime(dr["update_time"]);


        //    #endregion

        //    #region 底部datagridview数据

        //    #region 维修项目数据

        //    //维修项目数据                
        //    var dpt = DBHelper.GetTable("维修项目数据", "tb_maintain_three_guaranty_item", "*",
        //        string.Format(" tg_id='{0}'", TgId), "", "");
        //    if (dpt.Rows.Count > 0)
        //    {
        //        if (dpt.Rows.Count > dgv_tb_maintain_three_guaranty_item.Rows.Count)
        //        {
        //            dgv_tb_maintain_three_guaranty_item.Rows.Add(dpt.Rows.Count - dgv_tb_maintain_three_guaranty_item.Rows.Count +
        //                                                   1);
        //        }
        //        for (var i = 0; i < dpt.Rows.Count; i++)
        //        {
        //            var dpr = dpt.Rows[i];
        //            dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_item_id"].Value =
        //                CommonCtrl.IsNullToString(dpr["item_id"]);
        //            dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_item_no"].Value =
        //                CommonCtrl.IsNullToString(dpr["item_no"]);
        //            dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_item_type"].Value =
        //                CommonCtrl.IsNullToString(dpr["item_type"]);
        //            dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_item_name"].Value =
        //                CommonCtrl.IsNullToString(dpr["item_name"]);
        //            dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_man_hour_type"].Value =
        //                CommonCtrl.IsNullToString(dpr["man_hour_type"]);
        //            dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_man_hour_quantity"].Value =
        //                CommonCtrl.IsNullToString(dpr["man_hour_quantity"]);
        //            dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_man_hour_unitprice"].Value =
        //                CommonCtrl.IsNullToString(dpr["man_hour_unitprice"]);
        //            dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_sum_money"].Value =
        //                CommonCtrl.IsNullToString(dpr["sum_money"]);
        //            dgv_tb_maintain_three_guaranty_item.Rows[i].Cells["drtxt1_remarks"].Value =
        //                CommonCtrl.IsNullToString(dpr["remarks"]);

        //        }
        //    }

        //    #endregion

        //    #region 维修用料数据

        //    //维修用料数据
        //    var dmt = DBHelper.GetTable("维修用料数据", "tb_maintain_three_guaranty_material_detail", "*",
        //        string.Format(" tg_id='{0}'", TgId), "", "");
        //    if (dmt.Rows.Count > 0)
        //    {

        //        if (dmt.Rows.Count > dgv_tb_maintain_three_matrial_detail.Rows.Count)
        //        {
        //            dgv_tb_maintain_three_matrial_detail.Rows.Add(dmt.Rows.Count -
        //                                                    dgv_tb_maintain_three_matrial_detail.Rows.Count + 1);
        //        }
        //        for (var i = 0; i < dmt.Rows.Count; i++)
        //        {
        //            var dmr = dmt.Rows[i];
        //            dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_material_id"].Value =
        //                CommonCtrl.IsNullToString(dmr["material_id"]);
        //            dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_parts_code"].Value =
        //                CommonCtrl.IsNullToString(dmr["parts_code"]);
        //            dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_parts_name"].Value =
        //                CommonCtrl.IsNullToString(dmr["parts_name"]);
        //            dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_norms"].Value =
        //                CommonCtrl.IsNullToString(dmr["norms"]);
        //            dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_unit"].Value =
        //                CommonCtrl.IsNullToString(dmr["unit"]);
        //            dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_quantity"].Value =
        //                CommonCtrl.IsNullToString(dmr["quantity"]);
        //            dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_unit_price"].Value =
        //                CommonCtrl.IsNullToString(dmr["unit_price"]);
        //            dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_sum_money"].Value =
        //                CommonCtrl.IsNullToString(dmr["sum_money"]);
        //            dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_parts_source"].Value =
        //                CommonCtrl.IsNullToString(dmr["parts_source"]);
        //            dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_redeploy_no"].Value =
        //                CommonCtrl.IsNullToString(dmr["redeploy_no"]);
        //            dgv_tb_maintain_three_matrial_detail.Rows[i].Cells["drtxt2_remarks"].Value =
        //                CommonCtrl.IsNullToString(dmr["remarks"]);

        //        }
        //    }

        //    #endregion

        //    #region 其他项目收费数据

        //    ////其他项目收费数据
        //    var dot = DBHelper.GetTable("其他项目收费数据", "tb_maintain_three_guaranty_toll", "*",
        //        string.Format(" tg_id='{0}'", TgId), "", "");
        //    if (dot.Rows.Count > 0)
        //    {

        //        if (dot.Rows.Count > dgv_tb_maintain_three_guaranty_toll.Rows.Count)
        //        {
        //            dgv_tb_maintain_three_guaranty_toll.Rows.Add(dot.Rows.Count -
        //                                                         dgv_tb_maintain_three_guaranty_toll.Rows.Count + 1);
        //        }
        //        for (var i = 0; i < dot.Rows.Count; i++)
        //        {
        //            var dor = dot.Rows[i];
        //            dgv_tb_maintain_three_guaranty_toll.Rows[i].Cells["drtxt3_toll_id"].Value =
        //                CommonCtrl.IsNullToString(dor["toll_id"]);
        //            dgv_tb_maintain_three_guaranty_toll.Rows[i].Cells["drtxt3_sum_money"].Value =
        //                CommonCtrl.IsNullToString(dor["sum_money"]);
        //            dgv_tb_maintain_three_guaranty_toll.Rows[i].Cells["drtxt3_remarks"].Value =
        //                CommonCtrl.IsNullToString(dor["remarks"]);
        //            dgv_tb_maintain_three_guaranty_toll.Rows[i].Cells["drtxt3_cost_types"].Value =
        //                CommonCtrl.IsNullToString(dor["cost_types"]);
        //        }
        //    }

        //    #endregion

        //    #region 附件信息数据
        //    dgv_tb_maintain_three_guaranty_accessory.BindAttachment();

        //    #endregion

        //    if (windowStatus == WindowStatus.Copy)
        //    {
        //        TgId = String.Empty;
        //    }
        //}
        #endregion

        #region Event -- 事件

        #endregion
    }
}
