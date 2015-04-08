using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //宇通采购订单
    public class tb_parts_purchase_order_2
    {

        /// <summary>
        /// id
        /// </summary>		
        private string _purchase_order_yt_id;
        public string purchase_order_yt_id
        {
            get { return _purchase_order_yt_id; }
            set { _purchase_order_yt_id = value; }
        }
        /// <summary>
        /// 单号
        /// </summary>		
        private string _order_num;
        public string order_num
        {
            get { return _order_num; }
            set { _order_num = value; }
        }
        /// <summary>
        /// CRM单据ID--宇通
        /// </summary>		
        private string _crm_bill_id;
        public string crm_bill_id
        {
            get { return _crm_bill_id; }
            set { _crm_bill_id = value; }
        }
        /// <summary>
        /// 调件单号--宇通
        /// </summary>		
        private string _dsn_adjustable_parts;
        public string dsn_adjustable_parts
        {
            get { return _dsn_adjustable_parts; }
            set { _dsn_adjustable_parts = value; }
        }
        /// <summary>
        /// 审批记录--宇通
        /// </summary>		
        private string _approval_record;
        public string approval_record
        {
            get { return _approval_record; }
            set { _approval_record = value; }
        }
        /// <summary>
        /// 宇通单号
        /// </summary>		
        private string _order_num_yt;
        public string order_num_yt
        {
            get { return _order_num_yt; }
            set { _order_num_yt = value; }
        }
        /// <summary>
        /// 日期
        /// </summary>		
        private long _order_date;
        public long order_date
        {
            get { return _order_date; }
            set { _order_date = value; }
        }
        /// <summary>
        /// 申请日期
        /// </summary>		
        private long _apply_date_time;
        public long apply_date_time
        {
            get { return _apply_date_time; }
            set { _apply_date_time = value; }
        }
        /// <summary>
        /// 申请数量
        /// </summary>		
        private int _application_count;
        public int application_count
        {
            get { return _application_count; }
            set { _application_count = value; }
        }
        /// <summary>
        /// 确认数量
        /// </summary>		
        private int _conf_count;
        public int conf_count
        {
            get { return _conf_count; }
            set { _conf_count = value; }
        }
        /// <summary>
        /// 单据状态
        /// </summary>		
        private string _order_status;
        public string order_status
        {
            get { return _order_status; }
            set { _order_status = value; }
        }
        /// <summary>
        /// 单据状态名称，历史字段
        /// </summary>		
        private string _order_status_name;
        public string order_status_name
        {
            get { return _order_status_name; }
            set { _order_status_name = value; }
        }
        /// <summary>
        /// 宇通单据状态
        /// </summary>		
        private string _order_status_yt;
        public string order_status_yt
        {
            get { return _order_status_yt; }
            set { _order_status_yt = value; }
        }
        /// <summary>
        /// 宇通单据状态名称
        /// </summary>		
        private string _order_status_yt_name;
        public string order_status_yt_name
        {
            get { return _order_status_yt_name; }
            set { _order_status_yt_name = value; }
        }
        /// <summary>
        /// 订单类型
        /// </summary>		
        private string _order_type;
        public string order_type
        {
            get { return _order_type; }
            set { _order_type = value; }
        }
        /// <summary>
        /// 订单类型名称
        /// </summary>		
        private string _order_type_name;
        public string order_type_name
        {
            get { return _order_type_name; }
            set { _order_type_name = value; }
        }
        /// <summary>
        /// 紧急程度
        /// </summary>		
        private string _emergency_level;
        public string emergency_level
        {
            get { return _emergency_level; }
            set { _emergency_level = value; }
        }
        /// <summary>
        /// 紧急程度名称，历史字段
        /// </summary>		
        private string _emergency_level_name;
        public string emergency_level_name
        {
            get { return _emergency_level_name; }
            set { _emergency_level_name = value; }
        }
        /// <summary>
        /// 价格类型
        /// </summary>		
        private string _price_type;
        public string price_type
        {
            get { return _price_type; }
            set { _price_type = value; }
        }
        /// <summary>
        /// 价格类型名称
        /// </summary>		
        private string _price_type_name;
        public string price_type_name
        {
            get { return _price_type_name; }
            set { _price_type_name = value; }
        }
        /// <summary>
        /// 客户类型
        /// </summary>		
        private string _cust_type;
        public string cust_type
        {
            get { return _cust_type; }
            set { _cust_type = value; }
        }
        /// <summary>
        /// 客户类型名称
        /// </summary>		
        private string _cust_type_name;
        public string cust_type_name
        {
            get { return _cust_type_name; }
            set { _cust_type_name = value; }
        }
        /// <summary>
        /// 中心站/库
        /// </summary>		
        private string _center_library;
        public string center_library
        {
            get { return _center_library; }
            set { _center_library = value; }
        }
        /// <summary>
        /// 中心站/库名称
        /// </summary>		
        private string _center_library_name;
        public string center_library_name
        {
            get { return _center_library_name; }
            set { _center_library_name = value; }
        }
        /// <summary>
        /// 调拨类型
        /// </summary>		
        private string _allot_type;
        public string allot_type
        {
            get { return _allot_type; }
            set { _allot_type = value; }
        }
        /// <summary>
        /// 调拨类型名称
        /// </summary>		
        private string _allot_type_name;
        public string allot_type_name
        {
            get { return _allot_type_name; }
            set { _allot_type_name = value; }
        }
        /// <summary>
        /// 要求发货方式
        /// </summary>		
        private string _req_delivery;
        public string req_delivery
        {
            get { return _req_delivery; }
            set { _req_delivery = value; }
        }
        /// <summary>
        /// 要求发货方式名称
        /// </summary>		
        private string _req_delivery_name;
        public string req_delivery_name
        {
            get { return _req_delivery_name; }
            set { _req_delivery_name = value; }
        }
        /// <summary>
        /// 活动类型
        /// </summary>		
        private string _activity_type;
        public string activity_type
        {
            get { return _activity_type; }
            set { _activity_type = value; }
        }
        /// <summary>
        /// 活动类型名称
        /// </summary>		
        private string _activity_type_name;
        public string activity_type_name
        {
            get { return _activity_type_name; }
            set { _activity_type_name = value; }
        }
        /// <summary>
        /// 产品改进通知号
        /// </summary>		
        private string _product_notice_num;
        public string product_notice_num
        {
            get { return _product_notice_num; }
            set { _product_notice_num = value; }
        }
        /// <summary>
        /// 产品改进通知号值
        /// </summary>		
        private string _product_notice_num_name;
        public string product_notice_num_name
        {
            get { return _product_notice_num_name; }
            set { _product_notice_num_name = value; }
        }
        /// <summary>
        /// 默认客户折扣
        /// </summary>		
        private decimal _default_discount;
        public decimal default_discount
        {
            get { return _default_discount; }
            set { _default_discount = value; }
        }
        /// <summary>
        /// 要求发货时间
        /// </summary>		
        private long _req_delivery_time;
        public long req_delivery_time
        {
            get { return _req_delivery_time; }
            set { _req_delivery_time = value; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>		
        private string _cust_name;
        public string cust_name
        {
            get { return _cust_name; }
            set { _cust_name = value; }
        }
        /// <summary>
        /// 收货人
        /// </summary>		
        private string _consignee;
        public string consignee
        {
            get { return _consignee; }
            set { _consignee = value; }
        }
        /// <summary>
        /// 收货人ID
        /// </summary>		
        private string _consignee_code;
        public string consignee_code
        {
            get { return _consignee_code; }
            set { _consignee_code = value; }
        }
        /// <summary>
        /// 收货人电话
        /// </summary>		
        private string _consignee_tel;
        public string consignee_tel
        {
            get { return _consignee_tel; }
            set { _consignee_tel = value; }
        }
        /// <summary>
        /// 收货人手机
        /// </summary>		
        private string _consignee_phone;
        public string consignee_phone
        {
            get { return _consignee_phone; }
            set { _consignee_phone = value; }
        }
        /// <summary>
        /// 承运商
        /// </summary>		
        private string _carrier;
        public string carrier
        {
            get { return _carrier; }
            set { _carrier = value; }
        }
        /// <summary>
        /// 运费加成比例
        /// </summary>		
        private long _plus_proportion;
        public long plus_proportion
        {
            get { return _plus_proportion; }
            set { _plus_proportion = value; }
        }
        /// <summary>
        /// 收货地址
        /// </summary>		
        private string _delivery_address;
        public string delivery_address
        {
            get { return _delivery_address; }
            set { _delivery_address = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>		
        private string _remark;
        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// 申请单位代码
        /// </summary>		
        private string _application_code;
        public string application_code
        {
            get { return _application_code; }
            set { _application_code = value; }
        }
        /// <summary>
        /// 申请单位名称
        /// </summary>		
        private string _application_name;
        public string application_name
        {
            get { return _application_name; }
            set { _application_name = value; }
        }
        /// <summary>
        /// 公司ID
        /// </summary>		
        private string _com_id;
        public string com_id
        {
            get { return _com_id; }
            set { _com_id = value; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>		
        private string _com_code;
        public string com_code
        {
            get { return _com_code; }
            set { _com_code = value; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>		
        private string _com_name;
        public string com_name
        {
            get { return _com_name; }
            set { _com_name = value; }
        }
        /// <summary>
        /// 部门
        /// </summary>		
        private string _org_id;
        public string org_id
        {
            get { return _org_id; }
            set { _org_id = value; }
        }
        /// <summary>
        /// 部门名称，历史字段
        /// </summary>		
        private string _org_name;
        public string org_name
        {
            get { return _org_name; }
            set { _org_name = value; }
        }
        /// <summary>
        /// 经办人
        /// </summary>		
        private string _handle;
        public string handle
        {
            get { return _handle; }
            set { _handle = value; }
        }
        /// <summary>
        /// 经办人名称，历史字段
        /// </summary>		
        private string _handle_name;
        public string handle_name
        {
            get { return _handle_name; }
            set { _handle_name = value; }
        }
        /// <summary>
        /// 操作人
        /// </summary>		
        private string _operators;
        public string operators
        {
            get { return _operators; }
            set { _operators = value; }
        }
        /// <summary>
        /// 操作人名称，历史字段
        /// </summary>		
        private string _operator_name;
        public string operator_name
        {
            get { return _operator_name; }
            set { _operator_name = value; }
        }
        /// <summary>
        /// 创建人
        /// </summary>		
        private string _create_by;
        public string create_by
        {
            get { return _create_by; }
            set { _create_by = value; }
        }
        /// <summary>
        /// 创建人名称，历史字段
        /// </summary>		
        private string _create_name;
        public string create_name
        {
            get { return _create_name; }
            set { _create_name = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>		
        private long _create_time;
        public long create_time
        {
            get { return _create_time; }
            set { _create_time = value; }
        }
        /// <summary>
        /// 最后编辑人
        /// </summary>		
        private string _update_by;
        public string update_by
        {
            get { return _update_by; }
            set { _update_by = value; }
        }
        /// <summary>
        /// 最后编辑人名称，历史字段
        /// </summary>		
        private string _update_name;
        public string update_name
        {
            get { return _update_name; }
            set { _update_name = value; }
        }
        /// <summary>
        /// 最后编辑时间
        /// </summary>		
        private long _update_time;
        public long update_time
        {
            get { return _update_time; }
            set { _update_time = value; }
        }
        /// <summary>
        /// 删除标记，0删除，1未删除  默认1
        /// </summary>		
        private string _enable_flag;
        public string enable_flag
        {
            get { return _enable_flag; }
            set { _enable_flag = value; }
        }
        /// <summary>
        ///配件编码集合
        /// </summary>		
        private string _parts_codes;
        public string parts_codes
        {
            get { return _parts_codes; }
            set { _parts_codes = value; }
        }
        /// <summary>
        ///配件名称集合
        /// </summary>		
        private string _parts_names;
        public string parts_names
        {
            get { return _parts_names; }
            set { _parts_names = value; }
        }

        /// <summary>
        /// 采购配件列表
        /// </summary>
        public List<tb_parts_purchase_order_p_2> listDetails
        {
            get;
            set;
        }

    }
}
