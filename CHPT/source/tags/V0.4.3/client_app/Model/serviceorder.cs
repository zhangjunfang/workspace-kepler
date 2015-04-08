using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary> 三包内服务单
    /// </summary>
    public class serviceorder
    {
        #region 字段
        private string tg_idField;

        private string crm_service_bill_codeField;

        private string dsn_service_bill_codeField;

        private string sap_codeField;

        private string service_station_nameField;

        private string bill_type_ytField;

        private string whether_go_outField;

        private string refit_caseField;

        private string repairs_timeField;

        private string create_timeField;

        private string depot_noField;

        private string vehicle_noField;

        private string travel_mileageField;

        private string vehicle_useField;

        private string vehicle_locationField;

        private string vehicle_location_nameField;

        private string vehicle_use_corpField;

        private string linkmanField;

        private string link_man_mobileField;

        private string repairer_nameField;

        private string repairer_mobileField;

        private string driver_nameField;

        private string driver_mobileField;

        private string start_work_timeField;

        private string complete_work_timeField;

        private string appraiser_nameField;

        private string travel_descField;

        private string approve_by_peopleField;

        private string apply_rescue_placeField;

        private string travel_lookup_codeField;

        private string depart_timeField;

        private string back_timeField;

        private string rescue_mileageField;

        private string travel_employeesField;

        private string hour_amountField;

        private string mile_allowanceField;

        private string hotel_feeField;

        private string traffic_feeField;

        private string travel_amountField;

        private string other_item_sum_moneyField;

        private string other_remarksField;

        private string should_sumField;

        private string remarksField;

        private string approve_status_ytField;

        private string fault_class_codeField;

        private string fault_class_nameField;

        private string fault_assembly_codeField;

        private string fault_assembly_nameField;

        private string fault_part_codeField;

        private string fault_part_nameField;

        private string fault_mode_codeField;

        private string fault_mode_nameField;

        private string fault_causeField;

        private string repair_manField;

        private string reason_analysisField;

        private string dispose_resultField;

        private string approver_name_ytField;

        private string policy_approval_noField;

        private string policy_cost_typeField;

        private string describeField;

        private string product_notice_noField;

        private string activity_cost_typeField;

        private string luxury_cost_typeField;

        private string fault_duty_corpField;

        private string fault_describeField;

        private string man_hour_costField;

        private string parts_costField;

        private string parts_buy_timeField;

        private string parts_buy_corpField;

        private string contain_man_hour_costField;

        private string car_parts_codeField;

        private string parts_nameField;

        private string first_install_stationField;

        private string part_guarantee_periodField;

        private string part_lotField;

        private string feedback_numField;

        private string is_special_agreed_warrantyField;

        private ChangePartsDetail[] changePartsDetailsField;

        private RepairItems[] repairItemsDetailsField;

        private Files[] filesDetailsField;
        #endregion

        #region 属性
        /// <summary> tg_id
        /// </summary>
        public string tg_id
        {
            get
            {
                return this.tg_idField;
            }
            set
            {
                this.tg_idField = value;
            }
        }

        /// <summary> 服务工单编号
        /// </summary>
        public string crm_service_bill_code
        {
            get
            {
                return this.crm_service_bill_codeField;
            }
            set
            {
                this.crm_service_bill_codeField = value;
            }
        }

         public string dsn_service_bill_code {
            get {
                return this.dsn_service_bill_codeField;
            }
            set {
                this.dsn_service_bill_codeField = value;
            }
        }

        /// <summary> 服务站SAP代码
        /// </summary>
        public string sap_code
        {
            get
            {
                return this.sap_codeField;
            }
            set
            {
                this.sap_codeField = value;
            }
        }

        /// <summary> 服务站名称
        /// </summary>
        public string service_station_name
        {
            get
            {
                return this.service_station_nameField;
            }
            set
            {
                this.service_station_nameField = value;
            }
        }

        /// <summary> 单据类型
        /// </summary>
        public string bill_type_yt
        {
            get
            {
                return this.bill_type_ytField;
            }
            set
            {
                this.bill_type_ytField = value;
            }
        }

        /// <summary> 是否外出
        /// </summary>
        public string whether_go_out
        {
            get
            {
                return this.whether_go_outField;
            }
            set
            {
                this.whether_go_outField = value;
            }
        }

        /// <summary> 改装情况
        /// </summary>
        public string refit_case
        {
            get
            {
                return this.refit_caseField;
            }
            set
            {
                this.refit_caseField = value;
            }
        }

        /// <summary> 报修日期时间
        /// </summary>
        public string repairs_time
        {
            get
            {
                return this.repairs_timeField;
            }
            set
            {
                this.repairs_timeField = value;
            }
        }

        /// <summary> 开单日期时间
        /// </summary>
        public string create_time
        {
            get
            {
                return this.create_timeField;
            }
            set
            {
                this.create_timeField = value;
            }
        }

        /// <summary> 车工号
        /// </summary>
        public string depot_no
        {
            get
            {
                return this.depot_noField;
            }
            set
            {
                this.depot_noField = value;
            }
        }

        /// <summary> 车牌号
        /// </summary>
        public string vehicle_no
        {
            get
            {
                return this.vehicle_noField;
            }
            set
            {
                this.vehicle_noField = value;
            }
        }

        /// <summary> 行程里程
        /// </summary>
        public string travel_mileage
        {
            get
            {
                return this.travel_mileageField;
            }
            set
            {
                this.travel_mileageField = value;
            }
        }

        /// <summary> 用户性质（车辆用途）
        /// </summary>
        public string vehicle_use
        {
            get
            {
                return this.vehicle_useField;
            }
            set
            {
                this.vehicle_useField = value;
            }
        }

        /// <summary> 车辆所在地代码
        /// </summary>
        public string vehicle_location
        {
            get
            {
                return this.vehicle_locationField;
            }
            set
            {
                this.vehicle_locationField = value;
            }
        }

        /// <summary> 车辆所在地名称
        /// </summary>
        public string vehicle_location_name
        {
            get
            {
                return this.vehicle_location_nameField;
            }
            set
            {
                this.vehicle_location_nameField = value;
            }
        }

        /// <summary> 车辆使用单位名称
        /// </summary>
        public string vehicle_use_corp
        {
            get
            {
                return this.vehicle_use_corpField;
            }
            set
            {
                this.vehicle_use_corpField = value;
            }
        }

        /// <summary> 车主姓名
        /// </summary>
        public string linkman
        {
            get
            {
                return this.linkmanField;
            }
            set
            {
                this.linkmanField = value;
            }
        }

        /// <summary> 车主电话
        /// </summary>
        public string link_man_mobile
        {
            get
            {
                return this.link_man_mobileField;
            }
            set
            {
                this.link_man_mobileField = value;
            }
        }

        /// <summary> 报修人姓名
        /// </summary>
        public string repairer_name
        {
            get
            {
                return this.repairer_nameField;
            }
            set
            {
                this.repairer_nameField = value;
            }
        }

        /// <summary> 报修电话
        /// </summary>
        public string repairer_mobile
        {
            get
            {
                return this.repairer_mobileField;
            }
            set
            {
                this.repairer_mobileField = value;
            }
        }

        /// <summary> 司机姓名
        /// </summary>
        public string driver_name
        {
            get
            {
                return this.driver_nameField;
            }
            set
            {
                this.driver_nameField = value;
            }
        }

        /// <summary> 司机电话
        /// </summary>
        public string driver_mobile
        {
            get
            {
                return this.driver_mobileField;
            }
            set
            {
                this.driver_mobileField = value;
            }
        }

        /// <summary> 维修开始时间
        /// </summary>
        public string start_work_time
        {
            get
            {
                return this.start_work_timeField;
            }
            set
            {
                this.start_work_timeField = value;
            }
        }

        /// <summary> 维修结束时间
        /// </summary>
        public string complete_work_time
        {
            get
            {
                return this.complete_work_timeField;
            }
            set
            {
                this.complete_work_timeField = value;
            }
        }

        /// <summary> 质检员
        /// </summary>
        public string appraiser_name
        {
            get
            {
                return this.appraiser_nameField;
            }
            set
            {
                this.appraiser_nameField = value;
            }
        }

        /// <summary> 外出事由
        /// </summary>
        public string travel_desc
        {
            get
            {
                return this.travel_descField;
            }
            set
            {
                this.travel_descField = value;
            }
        }

        /// <summary> 外出批准人名称
        /// </summary>
        public string approve_by_people
        {
            get
            {
                return this.approve_by_peopleField;
            }
            set
            {
                this.approve_by_peopleField = value;
            }
        }

        /// <summary> 外出地点
        /// </summary>
        public string apply_rescue_place
        {
            get
            {
                return this.apply_rescue_placeField;
            }
            set
            {
                this.apply_rescue_placeField = value;
            }
        }

        /// <summary> 交通方式
        /// </summary>
        public string travel_lookup_code
        {
            get
            {
                return this.travel_lookup_codeField;
            }
            set
            {
                this.travel_lookup_codeField = value;
            }
        }

        /// <summary> 外出时间
        /// </summary>
        public string depart_time
        {
            get
            {
                return this.depart_timeField;
            }
            set
            {
                this.depart_timeField = value;
            }
        }

        /// <summary> 返回时间
        /// </summary>
        public string back_time
        {
            get
            {
                return this.back_timeField;
            }
            set
            {
                this.back_timeField = value;
            }
        }

        /// <summary> 外出里程
        /// </summary>
        public string rescue_mileage
        {
            get
            {
                return this.rescue_mileageField;
            }
            set
            {
                this.rescue_mileageField = value;
            }
        }

        /// <summary> 外出人数
        /// </summary>
        public string travel_employees
        {
            get
            {
                return this.travel_employeesField;
            }
            set
            {
                this.travel_employeesField = value;
            }
        }

        /// <summary> 工时补助
        /// </summary>
        public string hour_amount
        {
            get
            {
                return this.hour_amountField;
            }
            set
            {
                this.hour_amountField = value;
            }
        }

        /// <summary> 路程补助
        /// </summary>
        public string mile_allowance
        {
            get
            {
                return this.mile_allowanceField;
            }
            set
            {
                this.mile_allowanceField = value;
            }
        }

        /// <summary> 住宿费
        /// </summary>
        public string hotel_fee
        {
            get
            {
                return this.hotel_feeField;
            }
            set
            {
                this.hotel_feeField = value;
            }
        }

        /// <summary> 车船费
        /// </summary>
        public string traffic_fee
        {
            get
            {
                return this.traffic_feeField;
            }
            set
            {
                this.traffic_feeField = value;
            }
        }

        /// <summary> 差旅费合计
        /// </summary>
        public string travel_amount
        {
            get
            {
                return this.travel_amountField;
            }
            set
            {
                this.travel_amountField = value;
            }
        }

        /// <summary> 其他费用
        /// </summary>
        public string other_item_sum_money
        {
            get
            {
                return this.other_item_sum_moneyField;
            }
            set
            {
                this.other_item_sum_moneyField = value;
            }
        }

        /// <summary> 其他费用说明
        /// </summary>
        public string other_remarks
        {
            get
            {
                return this.other_remarksField;
            }
            set
            {
                this.other_remarksField = value;
            }
        }

        /// <summary> 费用合计 
        /// </summary>
        public string should_sum
        {
            get
            {
                return this.should_sumField;
            }
            set
            {
                this.should_sumField = value;
            }
        }

        /// <summary> 备注(费用)
        /// </summary>
        public string remarks
        {
            get
            {
                return this.remarksField;
            }
            set
            {
                this.remarksField = value;
            }
        }

        /// <summary> 服务单状态
        /// </summary>
        public string approve_status_yt
        {
            get
            {
                return this.approve_status_ytField;
            }
            set
            {
                this.approve_status_ytField = value;
            }
        }

        /// <summary> 故障分类编码
        /// </summary>
        public string fault_class_code
        {
            get
            {
                return this.fault_class_codeField;
            }
            set
            {
                this.fault_class_codeField = value;
            }
        }

        /// <summary> 故障分类描述
        /// </summary>
        public string fault_class_name
        {
            get
            {
                return this.fault_class_nameField;
            }
            set
            {
                this.fault_class_nameField = value;
            }
        }

        /// <summary> 故障总成编码
        /// </summary>
        public string fault_assembly_code
        {
            get
            {
                return this.fault_assembly_codeField;
            }
            set
            {
                this.fault_assembly_codeField = value;
            }
        }

        /// <summary> 故障总成名称
        /// </summary>
        public string fault_assembly_name
        {
            get
            {
                return this.fault_assembly_nameField;
            }
            set
            {
                this.fault_assembly_nameField = value;
            }
        }

        /// <summary> 故障总成组件编码
        /// </summary>
        public string fault_part_code
        {
            get
            {
                return this.fault_part_codeField;
            }
            set
            {
                this.fault_part_codeField = value;
            }
        }

        /// <summary> 故障总成组件名称
        /// </summary>
        public string fault_part_name
        {
            get
            {
                return this.fault_part_nameField;
            }
            set
            {
                this.fault_part_nameField = value;
            }
        }

        /// <summary> 故障模式编码
        /// </summary>
        public string fault_mode_code
        {
            get
            {
                return this.fault_mode_codeField;
            }
            set
            {
                this.fault_mode_codeField = value;
            }
        }

        /// <summary> 故障模式名称
        /// </summary>
        public string fault_mode_name
        {
            get
            {
                return this.fault_mode_nameField;
            }
            set
            {
                this.fault_mode_nameField = value;
            }
        }

        /// <summary> 故障原因
        /// </summary>
        public string fault_cause
        {
            get
            {
                return this.fault_causeField;
            }
            set
            {
                this.fault_causeField = value;
            }
        }

        /// <summary> 维修工程师名称
        /// </summary>
        public string repair_man
        {
            get
            {
                return this.repair_manField;
            }
            set
            {
                this.repair_manField = value;
            }
        }

        /// <summary> 原因分析
        /// </summary>
        public string reason_analysis
        {
            get
            {
                return this.reason_analysisField;
            }
            set
            {
                this.reason_analysisField = value;
            }
        }

        /// <summary> 处理结果
        /// </summary>
        public string dispose_result
        {
            get
            {
                return this.dispose_resultField;
            }
            set
            {
                this.dispose_resultField = value;
            }
        }

        /// <summary> 宇通批准人(政策照顾)
        /// </summary>
        public string approver_name_yt
        {
            get
            {
                return this.approver_name_ytField;
            }
            set
            {
                this.approver_name_ytField = value;
            }
        }

        /// <summary> 政策照顾审批编码(政策照顾)
        /// </summary>
        public string policy_approval_no
        {
            get
            {
                return this.policy_approval_noField;
            }
            set
            {
                this.policy_approval_noField = value;
            }
        }

        /// <summary> 费用类型(政策照顾)
        /// </summary>
        public string policy_cost_type
        {
            get
            {
                return this.policy_cost_typeField;
            }
            set
            {
                this.policy_cost_typeField = value;
            }
        }

        /// <summary> 情况简述(政策照顾)
        /// </summary>
        public string describe
        {
            get
            {
                return this.describeField;
            }
            set
            {
                this.describeField = value;
            }
        }

        /// <summary> 产品改进通知号(产品改进)
        /// </summary>
        public string product_notice_no
        {
            get
            {
                return this.product_notice_noField;
            }
            set
            {
                this.product_notice_noField = value;
            }
        }

        /// <summary> 费用类型(服务活动)
        /// </summary>
        public string activity_cost_type
        {
            get
            {
                return this.activity_cost_typeField;
            }
            set
            {
                this.activity_cost_typeField = value;
            }
        }

        /// <summary> 费用类型（高档车）
        /// </summary>
        public string luxury_cost_type
        {
            get
            {
                return this.luxury_cost_typeField;
            }
            set
            {
                this.luxury_cost_typeField = value;
            }
        }

        /// <summary> 故障责任单位
        /// </summary>
        public string fault_duty_corp
        {
            get
            {
                return this.fault_duty_corpField;
            }
            set
            {
                this.fault_duty_corpField = value;
            }
        }

        /// <summary> 故障描述
        /// </summary>
        public string fault_describe
        {
            get
            {
                return this.fault_describeField;
            }
            set
            {
                this.fault_describeField = value;
            }
        }

        /// <summary> 工时费合计
        /// </summary>
        public string man_hour_cost
        {
            get
            {
                return this.man_hour_costField;
            }
            set
            {
                this.man_hour_costField = value;
            }
        }

        /// <summary> 零件费合计
        /// </summary>
        public string parts_cost
        {
            get
            {
                return this.parts_costField;
            }
            set
            {
                this.parts_costField = value;
            }
        }

        /// <summary> 配件购买日期
        /// </summary>
        public string parts_buy_time
        {
            get
            {
                return this.parts_buy_timeField;
            }
            set
            {
                this.parts_buy_timeField = value;
            }
        }

        /// <summary> 配件购买单位
        /// </summary>
        public string parts_buy_corp
        {
            get
            {
                return this.parts_buy_corpField;
            }
            set
            {
                this.parts_buy_corpField = value;
            }
        }

        /// <summary> 是否含工时费
        /// </summary>
        public string contain_man_hour_cost
        {
            get
            {
                return this.contain_man_hour_costField;
            }
            set
            {
                this.contain_man_hour_costField = value;
            }
        }

        /// <summary> 配件编号
        /// </summary>
        public string car_parts_code
        {
            get
            {
                return this.car_parts_codeField;
            }
            set
            {
                this.car_parts_codeField = value;
            }
        }

        /// <summary> 配件名称
        /// </summary>
        public string parts_name
        {
            get
            {
                return this.parts_nameField;
            }
            set
            {
                this.parts_nameField = value;
            }
        }

        /// <summary> 配件安装服务站
        /// </summary>
        public string first_install_station
        {
            get
            {
                return this.first_install_stationField;
            }
            set
            {
                this.first_install_stationField = value;
            }
        }

        /// <summary> 配件协议保修期
        /// </summary>
        public string part_guarantee_period
        {
            get
            {
                return this.part_guarantee_periodField;
            }
            set
            {
                this.part_guarantee_periodField = value;
            }
        }

        /// <summary> 配件批次号
        /// </summary>
        public string part_lot
        {
            get
            {
                return this.part_lotField;
            }
            set
            {
                this.part_lotField = value;
            }
        }

        /// <summary> 反馈数量
        /// </summary>
        public string feedback_num
        {
            get
            {
                return this.feedback_numField;
            }
            set
            {
                this.feedback_numField = value;
            }
        }

        /// <summary> 是否特殊约定质保
        /// </summary>
        public string is_special_agreed_warranty
        {
            get
            {
                return this.is_special_agreed_warrantyField;
            }
            set
            {
                this.is_special_agreed_warrantyField = value;
            }
        }

        /// <summary> 新件信息,不能为空，返回长度为零的空数组，如new ChangePartsDetail[0]
        /// </summary>
        public ChangePartsDetail[] ChangePartsDetails
        {
            get
            {
                return this.changePartsDetailsField;
            }
            set
            {
                this.changePartsDetailsField = value;
            }
        }

        /// <summary> 维修项目,不能为空，返回长度为零的空数组，如new RepairItemsDetails[0]
        /// </summary>
        public RepairItems[] RepairItemsDetails
        {
            get
            {
                return this.repairItemsDetailsField;
            }
            set
            {
                this.repairItemsDetailsField = value;
            }
        }

        /// <summary> 文档信息,不能为空，返回长度为零的空数组，如new FilesDetails[0]
        /// </summary>
        public Files[] FilesDetails
        {
            get
            {
                return this.filesDetailsField;
            }
            set
            {
                this.filesDetailsField = value;
            }
        }
        #endregion
    }

    /// <summary> 新件信息
    /// </summary>
    public partial class ChangePartsDetail
    {
        #region 字段
        private string parts_sourceField;

        private string parts_nameField;

        private string car_parts_codeField;

        private string unitField;

        private string remarksField;

        private string unit_priceField;

        private string quantityField;

        private string sum_moneyField;
        #endregion

        #region 属性
        /// <summary> 新件来源
        /// </summary>
        public string parts_source
        {
            get
            {
                return this.parts_sourceField;
            }
            set
            {
                this.parts_sourceField = value;
            }
        }

        /// <summary> 新件名称
        /// </summary>
        public string parts_name
        {
            get
            {
                return this.parts_nameField;
            }
            set
            {
                this.parts_nameField = value;
            }
        }

        /// <summary> 新件编号
        /// </summary>
        public string car_parts_code
        {
            get
            {
                return this.car_parts_codeField;
            }
            set
            {
                this.car_parts_codeField = value;
            }
        }

        /// <summary> 单位名称
        /// </summary>
        public string unit
        {
            get
            {
                return this.unitField;
            }
            set
            {
                this.unitField = value;
            }
        }

        /// <summary> 配件说明
        /// </summary>
        public string remarks
        {
            get
            {
                return this.remarksField;
            }
            set
            {
                this.remarksField = value;
            }
        }

        /// <summary> 单价
        /// </summary>
        public string unit_price
        {
            get
            {
                return this.unit_priceField;
            }
            set
            {
                this.unit_priceField = value;
            }
        }

        /// <summary> 数量
        /// </summary>
        public string quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }

        /// <summary> 金额
        /// </summary>
        public string sum_money
        {
            get
            {
                return this.sum_moneyField;
            }
            set
            {
                this.sum_moneyField = value;
            }
        }
        #endregion 
    }

    /// <summary> 维修项目
    /// </summary>
    public partial class RepairItems
    {
        #region 字段
        private string item_noField;

        private string item_nameField;

        private string remarksField;

        private string man_hour_typeField;

        private string man_hour_unitpriceField;

        private string sum_moneyField;
        #endregion 

        #region 属性
        /// <summary> 维修项目编码
        /// </summary>
        public string item_no
        {
            get
            {
                return this.item_noField;
            }
            set
            {
                this.item_noField = value;
            }
        }

        /// <summary> 维修项目名称
        /// </summary>
        public string item_name
        {
            get
            {
                return this.item_nameField;
            }
            set
            {
                this.item_nameField = value;
            }
        }

        /// <summary> 工时描述
        /// </summary>
        public string remarks
        {
            get
            {
                return this.remarksField;
            }
            set
            {
                this.remarksField = value;
            }
        }

        /// <summary> 工时
        /// </summary>
        public string man_hour_type
        {
            get
            {
                return this.man_hour_typeField;
            }
            set
            {
                this.man_hour_typeField = value;
            }
        }

        /// <summary> 工时单价
        /// </summary>
        public string man_hour_unitprice
        {
            get
            {
                return this.man_hour_unitpriceField;
            }
            set
            {
                this.man_hour_unitpriceField = value;
            }
        }

        /// <summary> 工时金额
        /// </summary>
        public string sum_money
        {
            get
            {
                return this.sum_moneyField;
            }
            set
            {
                this.sum_moneyField = value;
            }
        }
        #endregion
    }

    /// <summary> 文档信息
    /// </summary>
    public partial class Files
    {
        #region 字段
        private string doc_nameField;

        private string accessory_nameField;

        private string docField;
        #endregion

        #region 属性
        /// <summary> 文档名称
        /// </summary>
        public string doc_name
        {
            get
            {
                return this.doc_nameField;
            }
            set
            {
                this.doc_nameField = value;
            }
        }

        /// <summary> 文件名称
        /// </summary>
        public string accessory_name
        {
            get
            {
                return this.accessory_nameField;
            }
            set
            {
                this.accessory_nameField = value;
            }
        }

        /// <summary> 文件内容
        /// </summary>
        public string Doc
        {
            get
            {
                return this.docField;
            }
            set
            {
                this.docField = value;
            }
        }
        #endregion
    }
}
