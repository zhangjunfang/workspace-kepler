using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 应收应付明细
    /// </summary>
    public class tb_payment_detail
    {
        #region Model
        private string _payment_detail_id;
        private string _order_id;
        private decimal? _money;
        private string _balance_way;
        private string _payment_account;
        private string _bank_of_deposit;
        private string _bank_account;
        private string _check_number;
        private string _remark;
        private string _order_type;
        private string _create_by;
        private long? _create_time;
        private string _update_by;
        private long? _update_time;
        /// <summary>
        /// id
        /// </summary>
        public string payment_detail_id
        {
            set { _payment_detail_id = value; }
            get { return _payment_detail_id; }
        }
        /// <summary>
        /// 应收付款单id
        /// </summary>
        public string order_id
        {
            set { _order_id = value; }
            get { return _order_id; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? money
        {
            set { _money = value; }
            get { return _money; }
        }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string balance_way
        {
            set { _balance_way = value; }
            get { return _balance_way; }
        }
        /// <summary>
        /// 付款账户
        /// </summary>
        public string payment_account
        {
            set { _payment_account = value; }
            get { return _payment_account; }
        }
        /// <summary>
        /// 开户银行
        /// </summary>
        public string bank_of_deposit
        {
            set { _bank_of_deposit = value; }
            get { return _bank_of_deposit; }
        }
        /// <summary>
        /// 银行账户
        /// </summary>
        public string bank_account
        {
            set { _bank_account = value; }
            get { return _bank_account; }
        }
        /// <summary>
        /// 票号
        /// </summary>
        public string check_number
        {
            set { _check_number = value; }
            get { return _check_number; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 款单类型 1：收款 2：付款
        /// </summary>
        public string order_type
        {
            set { _order_type = value; }
            get { return _order_type; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string create_by
        {
            set { _create_by = value; }
            get { return _create_by; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long? create_time
        {
            set { _create_time = value; }
            get { return _create_time; }
        }
        /// <summary>
        /// 最后编辑人
        /// </summary>
        public string update_by
        {
            set { _update_by = value; }
            get { return _update_by; }
        }
        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public long? update_time
        {
            set { _update_time = value; }
            get { return _update_time; }
        }
        #endregion Model
    }
}
