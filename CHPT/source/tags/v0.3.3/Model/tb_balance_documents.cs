using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 结算单据
    /// </summary>
    public class tb_balance_documents
    {
        #region Model
        private string _balance_documents_id;
        private string _order_id;
        private string _documents_name;
        private string _documents_id;
        private string _documents_num;
        private long _documents_date;
        private string _settled_type;
        private decimal? _billing_money;
        private decimal? _settled_money;
        private decimal? _wait_settled_money;
        private decimal? _settlement_money;
        private string _gathering;
        private decimal? _paid_money;
        private decimal? _deposit_rate;
        private decimal? _deduction;
        private string _remark;
        private string _create_by;
        private long? _create_time;
        private string _update_by;
        private long? _update_time;
        /// <summary>
        /// 主键id
        /// </summary>
        public string balance_documents_id
        {
            set { _balance_documents_id = value; }
            get { return _balance_documents_id; }
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
        /// 单据名称
        /// </summary>
        public string documents_name
        {
            set { _documents_name = value; }
            get { return _documents_name; }
        }
        /// <summary>
        /// 单据id
        /// </summary>
        public string documents_id
        {
            set { _documents_id = value; }
            get { return _documents_id; }
        }
        /// <summary>
        /// 单据号
        /// </summary>
        public string documents_num
        {
            set { _documents_num = value; }
            get { return _documents_num; }
        }
        /// <summary>
        /// 单据日期
        /// </summary>
        public long documents_date
        {
            set { _documents_date = value; }
            get { return _documents_date; }
        }
        /// <summary>
        /// 结算单据类型 1：应收款结算单据 2：应付款结算单位
        /// </summary>
        public string settled_type
        {
            set { _settled_type = value; }
            get { return _settled_type; }
        }
        /// <summary>
        /// 开单金额
        /// </summary>
        public decimal? billing_money
        {
            set { _billing_money = value; }
            get { return _billing_money; }
        }
        /// <summary>
        /// 已结算金额
        /// </summary>
        public decimal? settled_money
        {
            set { _settled_money = value; }
            get { return _settled_money; }
        }
        /// <summary>
        /// 待结算金额
        /// </summary>
        public decimal? wait_settled_money
        {
            set { _wait_settled_money = value; }
            get { return _wait_settled_money; }
        }
        /// <summary>
        /// 本次结算
        /// </summary>
        public decimal? settlement_money
        {
            set { _settlement_money = value; }
            get { return _settlement_money; }
        }
        /// <summary>
        /// 收款
        /// </summary>
        public string gathering
        {
            set { _gathering = value; }
            get { return _gathering; }
        }
        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal? paid_money
        {
            set { _paid_money = value; }
            get { return _paid_money; }
        }
        /// <summary>
        /// 折扣率%
        /// </summary>
        public decimal? deposit_rate
        {
            set { _deposit_rate = value; }
            get { return _deposit_rate; }
        }
        /// <summary>
        /// 折扣额
        /// </summary>
        public decimal? deduction
        {
            set { _deduction = value; }
            get { return _deduction; }
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
