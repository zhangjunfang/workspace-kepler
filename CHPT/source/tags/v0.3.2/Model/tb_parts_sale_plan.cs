using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //tb_parts_sale_plan
    public class tb_parts_sale_plan
    {

        /// <summary>
        /// id
        /// </summary>		
        private string _sale_plan_id;
        public string sale_plan_id
        {
            get { return _sale_plan_id; }
            set { _sale_plan_id = value; }
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
        /// 单据状态名称，历史字段
        /// </summary>		
        private string _order_status_name;
        public string order_status_name
        {
            get { return _order_status_name; }
            set { _order_status_name = value; }
        }
        /// <summary>
        /// 计划区间从
        /// </summary>		
        private long _plan_from;
        public long plan_from
        {
            get { return _plan_from; }
            set { _plan_from = value; }
        }
        /// <summary>
        /// 计划区间至
        /// </summary>		
        private long _plan_to;
        public long plan_to
        {
            get { return _plan_to; }
            set { _plan_to = value; }
        }
        /// <summary>
        /// 计划金额
        /// </summary>		
        private decimal _plan_money;
        public decimal plan_money
        {
            get { return _plan_money; }
            set { _plan_money = value; }
        }
        /// <summary>
        /// plan_counts
        /// </summary>		
        private int _plan_counts;
        public int plan_counts
        {
            get { return _plan_counts; }
            set { _plan_counts = value; }
        }
        /// <summary>
        /// 完成金额
        /// </summary>		
        private decimal _finish_money;
        public decimal finish_money
        {
            get { return _finish_money; }
            set { _finish_money = value; }
        }
        /// <summary>
        /// finish_counts
        /// </summary>		
        private int _finish_counts;
        public int finish_counts
        {
            get { return _finish_counts; }
            set { _finish_counts = value; }
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
        /// 备注
        /// </summary>		
        private string _remark;
        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
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
        /// <summary>
        /// import_status
        /// </summary>		
        private string _import_status;
        public string import_status
        {
            get { return _import_status; }
            set { _import_status = value; }
        }

    }
}
