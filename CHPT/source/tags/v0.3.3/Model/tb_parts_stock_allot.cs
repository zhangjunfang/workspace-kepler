using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 调拨单表头尾
    /// </summary>
  public   class tb_parts_stock_allot
    {
     
      	/// <summary>
		/// 调拨单主键ID
        /// </summary>		
		private string _stock_allot_id;
        public string stock_allot_id
        {
            get{ return _stock_allot_id; }
            set{ _stock_allot_id = value; }
        }        
		/// <summary>
		/// 调拨单号
        /// </summary>		
		private string _order_num;
        public string order_num
        {
            get{ return _order_num; }
            set{ _order_num = value; }
        }        
		/// <summary>
		/// 调拨单日期
        /// </summary>		
		private long _order_date;
        public long order_date
        {
            get{ return _order_date; }
            set{ _order_date = value; }
        }        
		/// <summary>
		/// 调拨单状态
        /// </summary>		
		private string _order_status;
        public string order_status
        {
            get{ return _order_status; }
            set{ _order_status = value; }
        }        
		/// <summary>
		/// 单据类型
        /// </summary>		
		private string _order_type;
        public string order_type
        {
            get{ return _order_type; }
            set{ _order_type = value; }
        }        
		/// <summary>
		/// 运输方式ID
        /// </summary>		
		private string _trans_way;
        public string trans_way
        {
            get{ return _trans_way; }
            set{ _trans_way = value; }
        }        
		/// <summary>
		/// 到货地址
        /// </summary>		
		private string _delivery_address;
        public string delivery_address
        {
            get{ return _delivery_address; }
            set{ _delivery_address = value; }
        }        
		/// <summary>
		/// 调出机构
        /// </summary>		
		private string _call_out_org;
        public string call_out_org
        {
            get{ return _call_out_org; }
            set{ _call_out_org = value; }
        }        
		/// <summary>
		/// 调出仓库
        /// </summary>		
		private string _call_out_wh;
        public string call_out_wh
        {
            get{ return _call_out_wh; }
            set{ _call_out_wh = value; }
        }        
		/// <summary>
		/// 调入机构
        /// </summary>		
		private string _call_in_org;
        public string call_in_org
        {
            get{ return _call_in_org; }
            set{ _call_in_org = value; }
        }        
		/// <summary>
		/// 调入仓库号
        /// </summary>		
		private string _call_in_wh;
        public string call_in_wh
        {
            get{ return _call_in_wh; }
            set{ _call_in_wh = value; }
        }        
		/// <summary>
		/// 部门，关联组织表
        /// </summary>		
		private string _org_id;
        public string org_id
        {
            get{ return _org_id; }
            set{ _org_id = value; }
        }        
		/// <summary>
		/// 经办人，关联人员表
        /// </summary>		
		private string _handle;
        public string handle
        {
            get{ return _handle; }
            set{ _handle = value; }
        }        
		/// <summary>
		/// 操作人，关联人员表
        /// </summary>		
		private string _operators;
        public string operators
        {
            get{ return _operators; }
            set{ _operators = value; }
        }        
		/// <summary>
		/// 创建人，关联人员表
        /// </summary>		
		private string _create_by;
        public string create_by
        {
            get{ return _create_by; }
            set{ _create_by = value; }
        }        
		/// <summary>
		/// 创建时间
        /// </summary>		
		private long _create_time;
        public long create_time
        {
            get{ return _create_time; }
            set{ _create_time = value; }
        }        
		/// <summary>
		/// 最后编辑人，关联人员表
        /// </summary>		
		private string _update_by;
        public string update_by
        {
            get{ return _update_by; }
            set{ _update_by = value; }
        }        
		/// <summary>
		/// 最后编辑时间
        /// </summary>		
		private long _update_time;
        public long update_time
        {
            get{ return _update_time; }
            set{ _update_time = value; }
        }        
		/// <summary>
		/// 单据状态名称，历史字段
        /// </summary>		
		private string _order_status_name;
        public string order_status_name
        {
            get{ return _order_status_name; }
            set{ _order_status_name = value; }
        }        
		/// <summary>
		/// 单据类型名称，历史字段
        /// </summary>		
		private string _order_type_name;
        public string order_type_name
        {
            get{ return _order_type_name; }
            set{ _order_type_name = value; }
        }        
		/// <summary>
		/// 运输方式名称，历史字段
        /// </summary>		
		private string _trans_way_name;
        public string trans_way_name
        {
            get{ return _trans_way_name; }
            set{ _trans_way_name = value; }
        }        
		/// <summary>
		/// 调出机构名称，历史字段
        /// </summary>		
		private string _call_out_org_name;
        public string call_out_org_name
        {
            get{ return _call_out_org_name; }
            set{ _call_out_org_name = value; }
        }        
		/// <summary>
		/// 调出仓库名称，历史字段
        /// </summary>		
		private string _call_out_wh_name;
        public string call_out_wh_name
        {
            get{ return _call_out_wh_name; }
            set{ _call_out_wh_name = value; }
        }        
		/// <summary>
        /// 调入仓库名称，历史字段
        /// </summary>		
		private string _call_in_wh_name;
        public string call_in_wh_name
        {
            get{ return _call_in_wh_name; }
            set{ _call_in_wh_name = value; }
        }        
		/// <summary>
		/// 调入机构名称，历史字段
        /// </summary>		
		private string _call_in_org_name;
        public string call_in_org_name
        {
            get{ return _call_in_org_name; }
            set{ _call_in_org_name = value; }
        }        
		/// <summary>
		/// 部门名称，历史字段
        /// </summary>		
		private string _org_name;
        public string org_name
        {
            get{ return _org_name; }
            set{ _org_name = value; }
        }        
		/// <summary>
		/// 经办人名称，历史字段
        /// </summary>		
		private string _handle_name;
        public string handle_name
        {
            get{ return _handle_name; }
            set{ _handle_name = value; }
        }        
		/// <summary>
		/// 操作人名称，历史字段
        /// </summary>		
		private string _operator_name;
        public string operator_name
        {
            get{ return _operator_name; }
            set{ _operator_name = value; }
        }        
		/// <summary>
		/// 创建人名称，历史字段
        /// </summary>		
		private string _create_name;
        public string create_name
        {
            get{ return _create_name; }
            set{ _create_name = value; }
        }        
		/// <summary>
		/// 修改人名称，历史字段
        /// </summary>		
		private string _update_name;
        public string update_name
        {
            get{ return _update_name; }
            set{ _update_name = value; }
        }        
		/// <summary>
		/// 删除标记，0删除，1未删除  默认1
        /// </summary>		
		private string _enable_flag;
        public string enable_flag
        {
            get{ return _enable_flag; }
            set{ _enable_flag = value; }
        }        
		/// <summary>
		/// 单据占用标志，0未占用、1占用
        /// </summary>		
		private string _is_occupy;
        public string is_occupy
        {
            get{ return _is_occupy; }
            set{ _is_occupy = value; }
        }        
		/// <summary>
		/// 单据锁定标志，0未锁定、1锁定
        /// </summary>		
		private string _is_lock;
        public string is_lock
        {
            get{ return _is_lock; }
            set{ _is_lock = value; }
        }
        /// <summary>
        ///  备注
        /// </summary>		
        private string _remark;
        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
        } 
    }
}
