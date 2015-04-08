using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //销售开单
    public class tb_parts_sale_billing
    {

        /// <summary>
        /// id
        /// </summary>		
        private string _sale_billing_id;
        public string sale_billing_id
        {
            get { return _sale_billing_id; }
            set { _sale_billing_id = value; }
        }
        /// <summary>
        /// 单据状态，关联字典表
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
        /// 单据编号
        /// </summary>		
        private string _order_num;
        public string order_num
        {
            get { return _order_num; }
            set { _order_num = value; }
        }
        /// <summary>
        /// 单据时间
        /// </summary>		
        private long _order_date;
        public long order_date
        {
            get { return _order_date; }
            set { _order_date = value; }
        }
        /// <summary>
        /// 客户id
        /// </summary>		
        private string _cust_id;
        public string cust_id
        {
            get { return _cust_id; }
            set { _cust_id = value; }
        }
        /// <summary>
        /// 客户编码
        /// </summary>		
        private string _cust_code;
        public string cust_code
        {
            get { return _cust_code; }
            set { _cust_code = value; }
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
        /// 收款日期
        /// </summary>		
        private long _receivables_date;
        public long receivables_date
        {
            get { return _receivables_date; }
            set { _receivables_date = value; }
        }
        /// <summary>
        /// 收款期限
        /// </summary>		
        private long _receivables_limit;
        public long receivables_limit
        {
            get { return _receivables_limit; }
            set { _receivables_limit = value; }
        }
        /// <summary>
        /// 本次现收
        /// </summary>		
        private decimal _current_collect;
        public decimal current_collect
        {
            get { return _current_collect; }
            set { _current_collect = value; }
        }
        /// <summary>
        /// 客户欠款
        /// </summary>		
        private decimal _cust_arrears;
        public decimal cust_arrears
        {
            get { return _cust_arrears; }
            set { _cust_arrears = value; }
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
        /// 整单折扣
        /// </summary>		
        private decimal _whythe_discount;
        public decimal whythe_discount
        {
            get { return _whythe_discount; }
            set { _whythe_discount = value; }
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
        /// 送货地址
        /// </summary>		
        private string _delivery_address;
        public string delivery_address
        {
            get { return _delivery_address; }
            set { _delivery_address = value; }
        }
        /// <summary>
        /// 结算方式名称
        /// </summary>		
        private string _balance_way_name;
        public string balance_way_name
        {
            get { return _balance_way_name; }
            set { _balance_way_name = value; }
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
        /// 结算账户名称
        /// </summary>		
        private string _balance_account_name;
        public string balance_account_name
        {
            get { return _balance_account_name; }
            set { _balance_account_name = value; }
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
        /// 支票号
        /// </summary>		
        private string _check_number;
        public string check_number
        {
            get { return _check_number; }
            set { _check_number = value; }
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
        /// 会员卡号
        /// </summary>		
        private decimal _member_card_num;
        public decimal member_card_num
        {
            get { return _member_card_num; }
            set { _member_card_num = value; }
        }
        /// <summary>
        /// 上次累计积分
        /// </summary>		
        private int _last_cumulative_integral;
        public int last_cumulative_Integral
        {
            get { return _last_cumulative_integral; }
            set { _last_cumulative_integral = value; }
        }
        /// <summary>
        /// 本次积分
        /// </summary>		
        private int _this_cumulative_integral;
        public int this_cumulative_Integral
        {
            get { return _this_cumulative_integral; }
            set { _this_cumulative_integral = value; }
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
        /// 后置单据导入状态，0正常，1占用，2锁定
        /// </summary>		
        private string _is_occupy;
        public string is_occupy
        {
            get { return _is_occupy; }
            set { _is_occupy = value; }
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
        /// 客户名称，历史字段
        /// </summary>		
        private string _cust_name;
        public string cust_name
        {
            get { return _cust_name; }
            set { _cust_name = value; }
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
        /// 结算单位名称，历史字段
        /// </summary>		
        private string _balance_unit_name;
        public string balance_unit_name
        {
            get { return _balance_unit_name; }
            set { _balance_unit_name = value; }
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
        /// <summary>
        /// 库存导入完成状态
        /// </summary>		
        private string _is_occupy_stock;
        public string is_occupy_stock
        {
            get { return _is_occupy_stock; }
            set { _is_occupy_stock = value; }
        }
    }
}
