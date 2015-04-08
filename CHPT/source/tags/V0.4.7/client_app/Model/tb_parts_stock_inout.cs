using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 出入库单操作表头与表尾
    /// </summary>
    public class tb_parts_stock_inout
    {
        /// <summary>
        /// id
        /// </summary>		
        private string _stock_inout_id;
        public string stock_inout_id
        {
            get { return _stock_inout_id; }
            set { _stock_inout_id = value; }
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
        /// 开单类型
        /// </summary>		
        private string _billing_type;
        public string billing_type
        {
            get { return _billing_type; }
            set { _billing_type = value; }
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
        /// 往来单位
        /// </summary>		
        private string _bussiness_units;
        public string bussiness_units
        {
            get { return _bussiness_units; }
            set { _bussiness_units = value; }
        }
        /// <summary>
        /// 公司
        /// </summary>		
        private string _com_id;
        public string com_id
        {
            get { return _com_id; }
            set { _com_id = value; }
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
        /// 单据状态名称
        /// </summary>		
        private string _order_status_name;
        public string order_status_name
        {
            get { return _order_status_name; }
            set { _order_status_name = value; }
        }
        /// <summary>
        /// 单据类型名称
        /// </summary>		
        private string _order_type_name;
        public string order_type_name
        {
            get { return _order_type_name; }
            set { _order_type_name = value; }
        }
        /// <summary>
        /// 开单类型名称
        /// </summary>		
        private string _billing_type_name;
        public string billing_type_name
        {
            get { return _billing_type_name; }
            set { _billing_type_name = value; }
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
        /// 删除标记，0删除，1未删除  默认1
        /// </summary>		
        private string _enable_flag;
        public string enable_flag
        {
            get { return _enable_flag; }
            set { _enable_flag = value; }
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
        ///// <summary>
        ///// 修改人名称，历史字段
        ///// </summary>		
        //private string _wh_id;
        //public string wh_id
        //{
        //    get { return _wh_id; }
        //    set { _wh_id = value; }
        //}
        ///// <summary>
        ///// 修改人名称，历史字段
        ///// </summary>		
        //private string _wh_name;
        //public string wh_name
        //{
        //    get { return _wh_name; }
        //    set { _wh_name = value; }
        //}
    }
}
