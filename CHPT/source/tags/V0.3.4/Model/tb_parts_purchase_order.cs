using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //采购订单
    public class tb_parts_purchase_order
    {

        /// <summary>
        /// id
        /// </summary>		
        private string _order_id;
        public string order_id
        {
            get { return _order_id; }
            set { _order_id = value; }
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
        private int _order_status;
        public int order_status
        {
            get { return _order_status; }
            set { _order_status = value; }
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
        /// 供应商编码
        /// </summary>		
        private string _sup_code;
        public string sup_code
        {
            get { return _sup_code; }
            set { _sup_code = value; }
        }
        /// <summary>
        /// 供应商名称
        /// </summary>		
        private string _sup_name;
        public string sup_name
        {
            get { return _sup_name; }
            set { _sup_name = value; }
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
        /// 订货数量
        /// </summary>		
        private int _order_quantity;
        public int order_quantity
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
        /// 中止原因
        /// </summary>		
        private string _suspend_reason;
        public string suspend_reason
        {
            get { return _suspend_reason; }
            set { _suspend_reason = value; }
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
        /// 订货人
        /// </summary>		
        private string _ordered_by;
        public string ordered_by
        {
            get { return _ordered_by; }
            set { _ordered_by = value; }
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
        /// 有效期至
        /// </summary>		
        private long _valid_till;
        public long valid_till
        {
            get { return _valid_till; }
            set { _valid_till = value; }
        }
        /// <summary>
        /// 预付金额
        /// </summary>		
        private int _prepaid_money;
        public int prepaid_money
        {
            get { return _prepaid_money; }
            set { _prepaid_money = value; }
        }
        /// <summary>
        /// 结算方式，关联字典表
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
        /// 到货日期
        /// </summary>		
        private long _arrival_date;
        public long arrival_date
        {
            get { return _arrival_date; }
            set { _arrival_date = value; }
        }
        /// <summary>
        /// 运输方式，关联字典表
        /// </summary>		
        private string _trans_mode;
        public string trans_mode
        {
            get { return _trans_mode; }
            set { _trans_mode = value; }
        }
        /// <summary>
        /// 到货地点
        /// </summary>		
        private string _arrival_place;
        public string arrival_place
        {
            get { return _arrival_place; }
            set { _arrival_place = value; }
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
        /// 部门
        /// </summary>		
        private string _org_id;
        public string org_id
        {
            get { return _org_id; }
            set { _org_id = value; }
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
        /// 操作人
        /// </summary>		
        private string _operators;
        public string operators
        {
            get { return _operators; }
            set { _operators = value; }
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
        private string _trans_mode_name;
        public string trans_mode_name
        {
            get { return _trans_mode_name; }
            set { _trans_mode_name = value; }
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
