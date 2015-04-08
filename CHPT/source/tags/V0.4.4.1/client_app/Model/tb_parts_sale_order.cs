using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //销售订单
    public class tb_parts_sale_order
    {

        /// <summary>
        /// id
        /// </summary>		
        private string _sale_order_id;
        public string sale_order_id
        {
            get { return _sale_order_id; }
            set { _sale_order_id = value; }
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
        /// 客户ID
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
        /// 客户名称
        /// </summary>		
        private string _cust_name;
        public string cust_name
        {
            get { return _cust_name; }
            set { _cust_name = value; }
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
        /// 结算单位
        /// </summary>		
        private string _closing_unit;
        public string closing_unit
        {
            get { return _closing_unit; }
            set { _closing_unit = value; }
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
        /// 传真
        /// </summary>		
        private string _fax;
        public string fax
        {
            get { return _fax; }
            set { _fax = value; }
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
        /// 结算方式
        /// </summary>		
        private string _closing_way;
        public string closing_way
        {
            get { return _closing_way; }
            set { _closing_way = value; }
        }
        /// <summary>
        /// 合同号
        /// </summary>		
        private string _contract_no;
        public string contract_no
        {
            get { return _contract_no; }
            set { _contract_no = value; }
        }
        /// <summary>
        /// 有效期至
        /// </summary>		
        private long _valid_till;
        public long valid_till
        {
            get { return _valid_till; }
            set { _valid_till = value; }
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
        /// 发货时间
        /// </summary>		
        private long _delivery_time;
        public long delivery_time
        {
            get { return _delivery_time; }
            set { _delivery_time = value; }
        }
        /// <summary>
        /// 发货地点
        /// </summary>		
        private string _delivery_add;
        public string delivery_add
        {
            get { return _delivery_add; }
            set { _delivery_add = value; }
        }
        /// <summary>
        /// 订货人
        /// </summary>		
        private string _ordered_by;
        public string ordered_by
        {
            get { return _ordered_by; }
            set { _ordered_by = value; }
        }
        /// <summary>
        /// 预收金额
        /// </summary>		
        private decimal _advance_money;
        public decimal advance_money
        {
            get { return _advance_money; }
            set { _advance_money = value; }
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
        /// 是否中止
        /// </summary>		
        private string _is_suspend;
        public string is_suspend
        {
            get { return _is_suspend; }
            set { _is_suspend = value; }
        }
        /// <summary>
        /// 中止原因
        /// </summary>		
        private string _suspend_reason;
        public string suspend_reason
        {
            get { return _suspend_reason; }
            set { _suspend_reason = value; }
        }
        /// <summary>
        /// 订货数量
        /// </summary>		
        private decimal _order_quantity;
        public decimal order_quantity
        {
            get { return _order_quantity; }
            set { _order_quantity = value; }
        }
        /// <summary>
        /// 货款
        /// </summary>		
        private decimal _payment;
        public decimal payment
        {
            get { return _payment; }
            set { _payment = value; }
        }
        /// <summary>
        /// 税款
        /// </summary>		
        private decimal _tax;
        public decimal tax
        {
            get { return _tax; }
            set { _tax = value; }
        }
        /// <summary>
        /// 金额
        /// </summary>		
        private decimal _money;
        public decimal money
        {
            get { return _money; }
            set { _money = value; }
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
        /// 结算方式名称，历史字段
        /// </summary>		
        private string _closing_way_name;
        public string closing_way_name
        {
            get { return _closing_way_name; }
            set { _closing_way_name = value; }
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
        /// 是否锁定
        /// </summary>		
        private string _is_lock;
        public string is_lock
        {
            get { return _is_lock; }
            set { _is_lock = value; }
        }
        /// <summary>
        /// 是否占用
        /// </summary>		
        private string _is_occupy;
        public string is_occupy
        {
            get { return _is_occupy; }
            set { _is_occupy = value; }
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

    }
}
