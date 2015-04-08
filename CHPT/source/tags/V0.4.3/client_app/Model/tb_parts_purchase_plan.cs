using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //采购计划单
    public class tb_parts_purchase_plan
    {

        /// <summary>
        /// ID
        /// </summary>		
        private string _plan_id;
        public string plan_id
        {
            get { return _plan_id; }
            set { _plan_id = value; }
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
        /// 单据状态，关联字典表
        /// </summary>		
        private string _order_status;
        public string order_status
        {
            get { return _order_status; }
            set { _order_status = value; }
        }
        /// <summary>
        /// 计划起时间
        /// </summary>		
        private long _plan_start_time;
        public long plan_start_time
        {
            get { return _plan_start_time; }
            set { _plan_start_time = value; }
        }
        /// <summary>
        /// 计划终时间
        /// </summary>		
        private long _plan_end_time;
        public long plan_end_time
        {
            get { return _plan_end_time; }
            set { _plan_end_time = value; }
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
        /// 计划数量
        /// </summary>		
        private int _plan_counts;
        public int plan_counts
        {
            get { return _plan_counts; }
            set { _plan_counts = value; }
        }
        /// <summary>
        /// 完成数量
        /// </summary>		
        private int _finish_counts;
        public int finish_counts
        {
            get { return _finish_counts; }
            set { _finish_counts = value; }
        }
        /// <summary>
        /// 完成金额
        /// </summary>		
        private decimal _plan_finish_money;
        public decimal plan_finish_money
        {
            get { return _plan_finish_money; }
            set { _plan_finish_money = value; }
        }
        /// <summary>
        /// 是否中止,0中止，默认为1
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
        /// 单据状态，历史字段
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
        /// 删除标记，0删除，1未删除  默认1
        /// </summary>		
        private string _enable_flag;
        public string enable_flag
        {
            get { return _enable_flag; }
            set { _enable_flag = value; }
        }
        /// <summary>
        /// 单据导入状态，0正常，1占用，2锁定
        /// </summary>		
        private string _import_status;
        public string import_status
        {
            get { return _import_status; }
            set { _import_status = value; }
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
    }
}

