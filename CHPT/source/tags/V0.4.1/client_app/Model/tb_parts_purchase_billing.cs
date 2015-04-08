using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //采购开单
    public class tb_parts_purchase_billing
    {

        /// <summary>
        /// ID
        /// </summary>		
        private string _purchase_billing_id;
        public string purchase_billing_id
        {
            get { return _purchase_billing_id; }
            set { _purchase_billing_id = value; }
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
        /// 日期
        /// </summary>		
        private long _order_date;
        public long order_date
        {
            get { return _order_date; }
            set { _order_date = value; }
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
        /// 单据类型
        /// </summary>		
        private string _order_type;
        public string order_type
        {
            get { return _order_type; }
            set { _order_type = value; }
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
        /// 供应商ID
        /// </summary>		
        private string _sup_id;
        public string sup_id
        {
            get { return _sup_id; }
            set { _sup_id = value; }
        }
        /// <summary>
        /// 供应商
        /// </summary>		
        private string _sup_name;
        public string sup_name
        {
            get { return _sup_name; }
            set { _sup_name = value; }
        }
        /// <summary>
        /// 供应商编码
        /// </summary>		
        private string _sup_code;
        public string sup_code
        {
            get { return _sup_code; }
            set { _sup_code = value; }
        }
        /// <summary>
        /// 发票类型
        /// </summary>		
        private string _receipt_type;
        public string receipt_type
        {
            get { return _receipt_type; }
            set { _receipt_type = value; }
        }
        /// <summary>
        /// 发票号
        /// </summary>		
        private string _receipt_no;
        public string receipt_no
        {
            get { return _receipt_no; }
            set { _receipt_no = value; }
        }
        /// <summary>
        /// 联系人
        /// </summary>		
        private string _contacts;
        public string contacts
        {
            get { return _contacts; }
            set { _contacts = value; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>		
        private string _contacts_tel;
        public string contacts_tel
        {
            get { return _contacts_tel; }
            set { _contacts_tel = value; }
        }
        /// <summary>
        /// 传真
        /// </summary>		
        private string _fax;
        public string fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        /// <summary>
        /// 运输方式
        /// </summary>		
        private string _trans_way;
        public string trans_way
        {
            get { return _trans_way; }
            set { _trans_way = value; }
        }
        /// <summary>
        /// 付款日期
        /// </summary>		
        private long _payment_date;
        public long payment_date
        {
            get { return _payment_date; }
            set { _payment_date = value; }
        }
        /// <summary>
        /// 付款期限
        /// </summary>		
        private long _payment_limit;
        public long payment_limit
        {
            get { return _payment_limit; }
            set { _payment_limit = value; }
        }
        /// <summary>
        /// 本次现付
        /// </summary>		
        private decimal _this_payment;
        public decimal this_payment
        {
            get { return _this_payment; }
            set { _this_payment = value; }
        }
        /// <summary>
        /// 供应商欠款
        /// </summary>		
        private decimal _sup_arrears;
        public decimal sup_arrears
        {
            get { return _sup_arrears; }
            set { _sup_arrears = value; }
        }
        /// <summary>
        /// 结算单位
        /// </summary>		
        private string _balance_unit;
        public string balance_unit
        {
            get { return _balance_unit; }
            set { _balance_unit = value; }
        }
        /// <summary>
        /// 结算方式
        /// </summary>		
        private string _balance_way;
        public string balance_way
        {
            get { return _balance_way; }
            set { _balance_way = value; }
        }
        /// <summary>
        /// 整单折扣（%）
        /// </summary>		
        private decimal _whythe_discount;
        public decimal whythe_discount
        {
            get { return _whythe_discount; }
            set { _whythe_discount = value; }
        }
        /// <summary>
        /// 结算账户
        /// </summary>		
        private string _balance_account;
        public string balance_account
        {
            get { return _balance_account; }
            set { _balance_account = value; }
        }
        /// <summary>
        /// 结算账户名称
        /// </summary>		
        private string _balance_account_name;
        public string balance_account_name
        {
            get { return _balance_account_name; }
            set { _balance_account_name = value; }
        }
        /// <summary>
        /// 送货人
        /// </summary>		
        private string _delivery_man;
        public string delivery_man
        {
            get { return _delivery_man; }
            set { _delivery_man = value; }
        }
        /// <summary>
        /// 支票号
        /// </summary>		
        private string _check_number;
        public string check_number
        {
            get { return _check_number; }
            set { _check_number = value; }
        }
        /// <summary>
        /// 送货地址
        /// </summary>		
        private string _delivery_address;
        public string delivery_address
        {
            get { return _delivery_address; }
            set { _delivery_address = value; }
        }
        /// <summary>
        /// 已结算金额
        /// </summary>		
        private decimal _balance_money;
        public decimal balance_money
        {
            get { return _balance_money; }
            set { _balance_money = value; }
        }
        /// <summary>
        /// 后置单据导入状态
        /// </summary>		
        private string _is_occupy;
        public string is_occupy
        {
            get { return _is_occupy; }
            set { _is_occupy = value; }
        }
        /// <summary>
        /// 是否锁定
        /// </summary>		
        private string _is_lock;
        public string is_lock
        {
            get { return _is_lock; }
            set { _is_lock = value; }
        }
        /// <summary>
        /// 部门，关联组织表
        /// </summary>		
        private string _org_id;
        public string org_id
        {
            get { return _org_id; }
            set { _org_id = value; }
        }
        /// <summary>
        /// 经办人，关联人员表
        /// </summary>		
        private string _handle;
        public string handle
        {
            get { return _handle; }
            set { _handle = value; }
        }
        /// <summary>
        /// 操作人，关联人员表
        /// </summary>		
        private string _operators;
        public string operators
        {
            get { return _operators; }
            set { _operators = value; }
        }
        /// <summary>
        /// 创建人，关联人员表
        /// </summary>		
        private string _create_by;
        public string create_by
        {
            get { return _create_by; }
            set { _create_by = value; }
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
        /// 最后编辑人，关联人员表
        /// </summary>		
        private string _update_by;
        public string update_by
        {
            get { return _update_by; }
            set { _update_by = value; }
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
        /// 单据状态名称，历史字段
        /// </summary>		
        private string _order_status_name;
        public string order_status_name
        {
            get { return _order_status_name; }
            set { _order_status_name = value; }
        }
        /// <summary>
        /// 单据类型名称，历史字段
        /// </summary>		
        private string _order_type_name;
        public string order_type_name
        {
            get { return _order_type_name; }
            set { _order_type_name = value; }
        }
        /// <summary>
        /// 发票类型名称，历史字段
        /// </summary>		
        private string _receipt_type_name;
        public string receipt_type_name
        {
            get { return _receipt_type_name; }
            set { _receipt_type_name = value; }
        }
        /// <summary>
        /// 运输方式名称，历史字段
        /// </summary>		
        private string _trans_way_name;
        public string trans_way_name
        {
            get { return _trans_way_name; }
            set { _trans_way_name = value; }
        }
        /// <summary>
        /// 结算方式名称，历史字段
        /// </summary>		
        private string _balance_way_name;
        public string balance_way_name
        {
            get { return _balance_way_name; }
            set { _balance_way_name = value; }
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
        /// 经办人名称，历史字段
        /// </summary>		
        private string _handle_name;
        public string handle_name
        {
            get { return _handle_name; }
            set { _handle_name = value; }
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
        /// 创建人名称，历史字段
        /// </summary>		
        private string _create_name;
        public string create_name
        {
            get { return _create_name; }
            set { _create_name = value; }
        }
        /// <summary>
        /// 修改人名称，历史字段
        /// </summary>		
        private string _update_name;
        public string update_name
        {
            get { return _update_name; }
            set { _update_name = value; }
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
        ///// <summary>
        ///// 服务站id，云平台用
        ///// </summary>		
        //private string _ser_station_id;
        //public string ser_station_id
        //{
        //    get { return _ser_station_id; }
        //    set { _ser_station_id = value; }
        //}
        ///// <summary>
        ///// 帐套id，云平台用
        ///// </summary>		
        //private string _set_book_id;
        //public string set_book_id
        //{
        //    get { return _set_book_id; }
        //    set { _set_book_id = value; }
        //}
        /// <summary>
        /// dsn配送单号
        /// </summary>		
        private string _ration_send_code;
        public string ration_send_code
        {
            get { return _ration_send_code; }
            set { _ration_send_code = value; }
        }
        /// <summary>
        /// 总金额
        /// </summary>
        private decimal _allmoney;
        public decimal allmoney
        {
            get { return _allmoney; }
            set { _allmoney = value; }
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
    }
}
