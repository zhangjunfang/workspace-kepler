using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 应收应付款单
    /// </summary>
    public class tb_bill_receivable
    {
        #region Model
		private string _payable_single_id;
		private string _order_num;
		private DateTime _order_date;
		private string _order_status;
		private int _order_type;
		private string _cust_code;
		private string _cust_name;
		private int _payment_type;
		private decimal? _payment_money;
		private decimal? _dealings_balance;
		private string _bank_of_deposit;
		private string _bank_account;
		private string _org_id;
		private string _handle;
		private string _operator;
		private string _create_by;
		private long? _create_time;
		private string _update_by;
		private long? _update_time;
		private string _remark;
		private string _status;
		private string _enable_flag;
		private string _verify_advice;
		private string _cust_id;
		/// <summary>
        /// id
		/// </summary>
		public string payable_single_id
		{
			set{ _payable_single_id=value;}
			get{return _payable_single_id;}
		}
		/// <summary>
        /// 单号
		/// </summary>
		public string order_num
		{
			set{ _order_num=value;}
			get{return _order_num;}
		}
		/// <summary>
        /// 日期
		/// </summary>
		public DateTime order_date
		{
			set{ _order_date=value;}
			get{return _order_date;}
		}
		/// <summary>
        /// 单据状态
		/// </summary>
		public string order_status
		{
			set{ _order_status=value;}
			get{return _order_status;}
		}
		/// <summary>
        /// 款单类型 1：收款 2：付款
		/// </summary>
		public int order_type
		{
			set{ _order_type=value;}
			get{return _order_type;}
		}
		/// <summary>
        /// 往来单位编码
		/// </summary>
		public string cust_code
		{
			set{ _cust_code=value;}
			get{return _cust_code;}
		}
		/// <summary>
        /// 往来单位名称
		/// </summary>
		public string cust_name
		{
			set{ _cust_name=value;}
			get{return _cust_name;}
		}
		/// <summary>
        /// 收付款类型 1：应收付款 2：预收付款
		/// </summary>
		public int payment_type
		{
			set{ _payment_type=value;}
			get{return _payment_type;}
		}
		/// <summary>
        /// 预付金额
		/// </summary>
		public decimal? payment_money
		{
			set{ _payment_money=value;}
			get{return _payment_money;}
		}
		/// <summary>
        /// 往来余额
		/// </summary>
		public decimal? dealings_balance
		{
			set{ _dealings_balance=value;}
			get{return _dealings_balance;}
		}
		/// <summary>
        /// 开户银行
		/// </summary>
		public string bank_of_deposit
		{
			set{ _bank_of_deposit=value;}
			get{return _bank_of_deposit;}
		}
		/// <summary>
        /// 银行账户
		/// </summary>
		public string bank_account
		{
			set{ _bank_account=value;}
			get{return _bank_account;}
		}
		/// <summary>
        /// 部门
		/// </summary>
		public string org_id
		{
			set{ _org_id=value;}
			get{return _org_id;}
		}
		/// <summary>
        /// 经办人
		/// </summary>
		public string handle
		{
			set{ _handle=value;}
			get{return _handle;}
		}
		/// <summary>
        /// 操作人
		/// </summary>
		public string operatorS
		{
			set{ _operator=value;}
			get{return _operator;}
		}
		/// <summary>
        /// 创建人
		/// </summary>
		public string create_by
		{
			set{ _create_by=value;}
			get{return _create_by;}
		}
		/// <summary>
        /// 创建时间
		/// </summary>
		public long? create_time
		{
			set{ _create_time=value;}
			get{return _create_time;}
		}
		/// <summary>
        /// 最后编辑人
		/// </summary>
		public string update_by
		{
			set{ _update_by=value;}
			get{return _update_by;}
		}
		/// <summary>
        /// 最后编辑时间
		/// </summary>
		public long? update_time
		{
			set{ _update_time=value;}
			get{return _update_time;}
		}
		/// <summary>
        /// 备注
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
        /// 状态，1启用，0停用
		/// </summary>
		public string status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
        /// 标识，0为已删除，1为使用中
		/// </summary>
		public string enable_flag
		{
			set{ _enable_flag=value;}
			get{return _enable_flag;}
		}
		/// <summary>
        /// 审核意见
		/// </summary>
		public string Verify_advice
		{
			set{ _verify_advice=value;}
			get{return _verify_advice;}
		}
		/// <summary>
        /// 往来单位ID
		/// </summary>
		public string cust_id
		{
			set{ _cust_id=value;}
			get{return _cust_id;}
		}
        /// <summary>
        /// 公司ID
        /// </summary>
        public string com_id
        {
            get;
            set;
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string com_name
        {
            get;
            set;
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string org_name
        {
            get;
            set;
        }
		#endregion Model
    }
}
