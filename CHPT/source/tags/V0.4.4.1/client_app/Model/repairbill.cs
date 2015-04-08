
namespace Model
{
    /// <summary> 维修单
    /// </summary>
    public partial class repairbill
    {
        #region 字段
        private string maintain_noField;

        private string clearing_timeField;

        private string cust_idField;

        private string cust_nameField;

        private string linkmanField;

        private string link_man_mobileField;

        private string vehicle_noField;

        private string vehicle_modelField;

        private string vehicle_brandField;

        private string vehicle_vinField;

        private string engine_noField;

        private string driver_nameField;

        private string driver_mobileField;

        private string travel_mileageField;

        private string maintain_typeField;

        private string remarkField;

        private string man_hour_sum_moneyField;

        private string man_hour_sumField;

        private string fitting_sum_moneyField;

        private string fitting_sumField;

        private string other_item_tax_costField;

        private string other_item_sumField;

        private string privilege_costField;

        private string should_sumField;

        private string received_sumField;

        private string cost_typesField;

        private string sum_moneyField;

        private string other_remarksField;

        private string dispatch_timeField;

        private string start_work_timeField;

        private string shut_down_timeField;

        private string complete_work_timeField;

        private RepairProjectDetail[] repairProjectDetailsField;

        private RepairmaterialDetail[] repairmaterialDetailsField;
        #endregion

        #region 属性
        /// <summary> 维修单号
        /// </summary>
        public string maintain_no
        {
            get
            {
                return this.maintain_noField;
            }
            set
            {
                this.maintain_noField = value;
            }
        }
        /// <summary> 结算时间
        /// </summary>
        public string clearing_time
        {
            get
            {
                return this.clearing_timeField;
            }
            set
            {
                this.clearing_timeField = value;
            }
        }
        /// <summary> 客户id
        /// </summary>
        public string cust_id
        {
            get
            {
                return this.cust_idField;
            }
            set
            {
                this.cust_idField = value;
            }
        }
        /// <summary> 客户名称
        /// </summary>
        public string cust_name
        {
            get
            {
                return this.cust_nameField;
            }
            set
            {
                this.cust_nameField = value;
            }
        }
        /// <summary> 联系人姓名
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
        /// <summary> 联系人手机
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
        /// <summary> 车型(文本)
        /// </summary>
        public string vehicle_model
        {
            get
            {
                return this.vehicle_modelField;
            }
            set
            {
                this.vehicle_modelField = value;
            }
        }
        /// <summary> 车辆品牌(文本)
        /// </summary>
        public string vehicle_brand
        {
            get
            {
                return this.vehicle_brandField;
            }
            set
            {
                this.vehicle_brandField = value;
            }
        }
        /// <summary> VIN
        /// </summary>
        public string vehicle_vin
        {
            get
            {
                return this.vehicle_vinField;
            }
            set
            {
                this.vehicle_vinField = value;
            }
        }
        /// <summary> 发动机号
        /// </summary>
        public string engine_no
        {
            get
            {
                return this.engine_noField;
            }
            set
            {
                this.engine_noField = value;
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
        /// <summary> 司机手机
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
        /// <summary> 进站里程
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
        /// <summary> 维修类别
        /// </summary>
        public string maintain_type
        {
            get
            {
                return this.maintain_typeField;
            }
            set
            {
                this.maintain_typeField = value;
            }
        }
        /// <summary> 备注
        /// </summary>
        public string remark
        {
            get
            {
                return this.remarkField;
            }
            set
            {
                this.remarkField = value;
            }
        }
        /// <summary> 工时货款
        /// </summary>
        public string man_hour_sum_money
        {
            get
            {
                return this.man_hour_sum_moneyField;
            }
            set
            {
                this.man_hour_sum_moneyField = value;
            }
        }
        /// <summary> 工时价税合计（也就是金额）
        /// </summary>
        public string man_hour_sum
        {
            get
            {
                return this.man_hour_sumField;
            }
            set
            {
                this.man_hour_sumField = value;
            }
        }
        /// <summary> 配件货款
        /// </summary>
        public string fitting_sum_money
        {
            get
            {
                return this.fitting_sum_moneyField;
            }
            set
            {
                this.fitting_sum_moneyField = value;
            }
        }
        /// <summary> 配件价税合计
        /// </summary>
        public string fitting_sum
        {
            get
            {
                return this.fitting_sumField;
            }
            set
            {
                this.fitting_sumField = value;
            }
        }
        /// <summary> 其他项目税额
        /// </summary>
        public string other_item_tax_cost
        {
            get
            {
                return this.other_item_tax_costField;
            }
            set
            {
                this.other_item_tax_costField = value;
            }
        }
        /// <summary> 其他项目价税合计
        /// </summary>
        public string other_item_sum
        {
            get
            {
                return this.other_item_sumField;
            }
            set
            {
                this.other_item_sumField = value;
            }
        }
        /// <summary> 优惠费用
        /// </summary>
        public string privilege_cost
        {
            get
            {
                return this.privilege_costField;
            }
            set
            {
                this.privilege_costField = value;
            }
        }
        /// <summary> 应收总额
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
        /// <summary> 实收总额
        /// </summary>
        public string received_sum
        {
            get
            {
                return this.received_sumField;
            }
            set
            {
                this.received_sumField = value;
            }
        }
        /// <summary> 其他费用类别
        /// </summary>
        public string cost_types
        {
            get
            {
                return this.cost_typesField;
            }
            set
            {
                this.cost_typesField = value;
            }
        }
        /// <summary> 其他费用金额
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
        /// <summary> 其他费用备注
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
        /// <summary> 派工时间
        /// </summary>
        public string dispatch_time
        {
            get
            {
                return this.dispatch_timeField;
            }
            set
            {
                this.dispatch_timeField = value;
            }
        }
        /// <summary> 开工时间
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
        /// <summary> 停工时间
        /// </summary>
        public string shut_down_time
        {
            get
            {
                return this.shut_down_timeField;
            }
            set
            {
                this.shut_down_timeField = value;
            }
        }
        /// <summary> 实际完工时间
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
        /// <summary> 维修单,不能为空，返回长度为零的空数组，如new RepairProjectDetails[0]
        /// </summary>
        public RepairProjectDetail[] RepairProjectDetails
        {
            get
            {
                return this.repairProjectDetailsField;
            }
            set
            {
                this.repairProjectDetailsField = value;
            }
        }
        /// <summary> 维修单,不能为空，返回长度为零的空数组，如new RepairmaterialDetails[0]
        /// </summary>
        public RepairmaterialDetail[] RepairmaterialDetails
        {
            get
            {
                return this.repairmaterialDetailsField;
            }
            set
            {
                this.repairmaterialDetailsField = value;
            }
        }
        #endregion
    }

    /// <summary> 项目
    /// </summary>
    public partial class RepairProjectDetail
    {
        #region 字段
        private string item_noField;

        private string item_typeField;

        private string item_nameField;

        private string man_hour_typeField;

        private string man_hour_quantityField;

        private string man_hour_norm_unitpriceField;

        private string sum_money_goodsField;

        private string three_warrantyField;

        private string item_remarksField;
        #endregion

        #region 属性
        /// <summary> 项目编码
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
        /// <summary> 维修项目类别
        /// </summary>
        public string item_type
        {
            get
            {
                return this.item_typeField;
            }
            set
            {
                this.item_typeField = value;
            }
        }
        /// <summary> 项目名称
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
        /// <summary> 工时类别
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
        /// <summary> 工时数量
        /// </summary>
        public string man_hour_quantity
        {
            get
            {
                return this.man_hour_quantityField;
            }
            set
            {
                this.man_hour_quantityField = value;
            }
        }
        /// <summary> 原工时单价
        /// </summary>
        public string man_hour_norm_unitprice
        {
            get
            {
                return this.man_hour_norm_unitpriceField;
            }
            set
            {
                this.man_hour_norm_unitpriceField = value;
            }
        }
        /// <summary> 货款
        /// </summary>
        public string sum_money_goods
        {
            get
            {
                return this.sum_money_goodsField;
            }
            set
            {
                this.sum_money_goodsField = value;
            }
        }
        /// <summary> 是否三包
        /// </summary>
        public string three_warranty
        {
            get
            {
                return this.three_warrantyField;
            }
            set
            {
                this.three_warrantyField = value;
            }
        }
        /// <summary> 备注
        /// </summary>
        public string item_remarks
        {
            get
            {
                return this.item_remarksField;
            }
            set
            {
                this.item_remarksField = value;
            }
        }
        #endregion
    }

    /// <summary> 配件
    /// </summary>
    public partial class RepairmaterialDetail
    {
        #region 字段
        private string car_parts_codeField;

        private string parts_nameField;

        private string normsField;

        private string unitField;

        private string quantityField;

        private string unit_priceField;

        private string sum_moneyField;

        private string vehicle_brandField;

        private string three_warrantyField;

        private string parts_remarksField;
        #endregion

        #region 属性
        /// <summary> 配件编码（车厂编码）
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
        /// <summary> 规格
        /// </summary>
        public string norms
        {
            get
            {
                return this.normsField;
            }
            set
            {
                this.normsField = value;
            }
        }
        /// <summary> 单位
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
        /// <summary> 原始单价
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
        /// <summary> 货款
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
        /// <summary> 品牌
        /// </summary>
        public string vehicle_brand
        {
            get
            {
                return this.vehicle_brandField;
            }
            set
            {
                this.vehicle_brandField = value;
            }
        }
        /// <summary> 是否三包
        /// </summary>
        public string three_warranty
        {
            get
            {
                return this.three_warrantyField;
            }
            set
            {
                this.three_warrantyField = value;
            }
        }
        /// <summary> 备注
        /// </summary>
        public string parts_remarks
        {
            get
            {
                return this.parts_remarksField;
            }
            set
            {
                this.parts_remarksField = value;
            }
        }
        #endregion
    }
}
