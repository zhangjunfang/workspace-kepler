using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //客户档案
    public class tb_customer
    {
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
        /// 客户简称
        /// </summary>		
        private string _cust_short_name;
        public string cust_short_name
        {
            get { return _cust_short_name; }
            set { _cust_short_name = value; }
        }
        /// <summary>
        /// 简称首拼速查码
        /// </summary>		
        private string _cust_quick_code;
        public string cust_quick_code
        {
            get { return _cust_quick_code; }
            set { _cust_quick_code = value; }
        }
        /// <summary>
        /// 客户类别，关联字典表关联
        /// </summary>		
        private string _cust_type;
        public string cust_type
        {
            get { return _cust_type; }
            set { _cust_type = value; }
        }
        /// <summary>
        /// 法人
        /// </summary>		
        private string _legal_person;
        public string legal_person
        {
            get { return _legal_person; }
            set { _legal_person = value; }
        }
        /// <summary>
        /// 所属省，关联字典码表
        /// </summary>		
        private string _province;
        public string province
        {
            get { return _province; }
            set { _province = value; }
        }
        /// <summary>
        /// 所属市，关联字典码表
        /// </summary>		
        private string _city;
        public string city
        {
            get { return _city; }
            set { _city = value; }
        }
        /// <summary>
        /// 所属区县，关联字典码表
        /// </summary>		
        private string _county;
        public string county
        {
            get { return _county; }
            set { _county = value; }
        }
        /// <summary>
        /// 联系地址
        /// </summary>		
        private string _cust_address;
        public string cust_address
        {
            get { return _cust_address; }
            set { _cust_address = value; }
        }
        /// <summary>
        /// 邮编
        /// </summary>		
        private string _zip_code;
        public string zip_code
        {
            get { return _zip_code; }
            set { _zip_code = value; }
        }
        /// <summary>
        /// 固话
        /// </summary>		
        private string _cust_tel;
        public string cust_tel
        {
            get { return _cust_tel; }
            set { _cust_tel = value; }
        }
        /// <summary>
        /// 手机
        /// </summary>		
        private string _cust_phone;
        public string cust_phone
        {
            get { return _cust_phone; }
            set { _cust_phone = value; }
        }
        /// <summary>
        /// 传真
        /// </summary>		
        private string _cust_fax;
        public string cust_fax
        {
            get { return _cust_fax; }
            set { _cust_fax = value; }
        }
        /// <summary>
        /// 电子邮箱
        /// </summary>		
        private string _cust_email;
        public string cust_email
        {
            get { return _cust_email; }
            set { _cust_email = value; }
        }
        /// <summary>
        /// 网址
        /// </summary>		
        private string _cust_website;
        public string cust_website
        {
            get { return _cust_website; }
            set { _cust_website = value; }
        }
        /// <summary>
        /// 企业性质，关联码表字典
        /// </summary>		
        private string _enterprise_nature;
        public string enterprise_nature
        {
            get { return _enterprise_nature; }
            set { _enterprise_nature = value; }
        }
        /// <summary>
        /// 纳税人识别号
        /// </summary>		
        private string _tax_num;
        public string tax_num
        {
            get { return _tax_num; }
            set { _tax_num = value; }
        }
        /// <summary>
        /// 信用等级，关联字码表典
        /// </summary>		
        private string _credit_rating;
        public string credit_rating
        {
            get { return _credit_rating; }
            set { _credit_rating = value; }
        }
        /// <summary>
        /// 信用额度
        /// </summary>		
        private int _credit_line;
        public int credit_line
        {
            get { return _credit_line; }
            set { _credit_line = value; }
        }
        /// <summary>
        /// 信用账期
        /// </summary>		
        private int _credit_account_period;
        public int credit_account_period
        {
            get { return _credit_account_period; }
            set { _credit_account_period = value; }
        }
        /// <summary>
        /// 价格类型，关联字典码表
        /// </summary>		
        private string _price_type;
        public string price_type
        {
            get { return _price_type; }
            set { _price_type = value; }
        }
        /// <summary>
        /// 开户银行，关联字典码表
        /// </summary>		
        private string _open_bank;
        public string open_bank
        {
            get { return _open_bank; }
            set { _open_bank = value; }
        }
        /// <summary>
        /// 银行账号
        /// </summary>		
        private string _bank_account;
        public string bank_account
        {
            get { return _bank_account; }
            set { _bank_account = value; }
        }
        /// <summary>
        /// 银行账号开户人
        /// </summary>		
        private string _bank_account_person;
        public string bank_account_person
        {
            get { return _bank_account_person; }
            set { _bank_account_person = value; }
        }
        /// <summary>
        /// 开票名称
        /// </summary>		
        private string _billing_name;
        public string billing_name
        {
            get { return _billing_name; }
            set { _billing_name = value; }
        }
        /// <summary>
        /// 开票地址
        /// </summary>		
        private string _billing_address;
        public string billing_address
        {
            get { return _billing_address; }
            set { _billing_address = value; }
        }
        /// <summary>
        /// 开票账号
        /// </summary>		
        private string _billing_account;
        public string billing_account
        {
            get { return _billing_account; }
            set { _billing_account = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>		
        private string _cust_remark;
        public string cust_remark
        {
            get { return _cust_remark; }
            set { _cust_remark = value; }
        }
        /// <summary>
        /// 是否会员
        /// </summary>		
        private string _is_member;
        public string is_member
        {
            get { return _is_member; }
            set { _is_member = value; }
        }
        /// <summary>
        /// 会员编号
        /// </summary>		
        private string _member_number;
        public string member_number
        {
            get { return _member_number; }
            set { _member_number = value; }
        }
        /// <summary>
        /// 会员等级，关联字典码表
        /// </summary>		
        private string _member_class;
        public string member_class
        {
            get { return _member_class; }
            set { _member_class = value; }
        }
        /// <summary>
        /// 会员有效期
        /// </summary>		
        private long _member_period_validity;
        public long member_period_validity
        {
            get { return _member_period_validity; }
            set { _member_period_validity = value; }
        }
        /// <summary>
        /// 配件享受折扣
        /// </summary>		
        private decimal _accessories_discount;
        public decimal accessories_discount
        {
            get { return _accessories_discount; }
            set { _accessories_discount = value; }
        }
        /// <summary>
        /// 工时费享受折扣
        /// </summary>		
        private decimal _workhours_discount;
        public decimal workhours_discount
        {
            get { return _workhours_discount; }
            set { _workhours_discount = value; }
        }
        /// <summary>
        /// 状态   0，停用   1，启用
        /// </summary>		
        private string _status;
        public string status
        {
            get { return _status; }
            set { _status = value; }
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
        /// 宇通SAP代码
        /// </summary>		
        private string _yt_sap_code;
        public string yt_sap_code
        {
            get { return _yt_sap_code; }
            set { _yt_sap_code = value; }
        }
        /// <summary>
        /// 宇通客户经理
        /// </summary>		
        private string _yt_customer_manager;
        public string yt_customer_manager
        {
            get { return _yt_customer_manager; }
            set { _yt_customer_manager = value; }
        }
        /// <summary>
        /// 数据来源，关联字典码表
        /// </summary>		
        private string _data_source;
        public string data_source
        {
            get { return _data_source; }
            set { _data_source = value; }
        }
        /// <summary>
        /// 国家---宇通
        /// </summary>		
        private string _country;
        public string country
        {
            get { return _country; }
            set { _country = value; }
        }
        /// <summary>
        /// 客户关系综述---宇通
        /// </summary>		
        private string _cust_relation;
        public string cust_relation
        {
            get { return _cust_relation; }
            set { _cust_relation = value; }
        }
        /// <summary>
        /// 独立法人---宇通
        /// </summary>		
        private string _indepen_legalperson;
        public string indepen_legalperson
        {
            get { return _indepen_legalperson; }
            set { _indepen_legalperson = value; }
        }
        /// <summary>
        /// 细分市场---宇通
        /// </summary>		
        private string _market_segment;
        public string market_segment
        {
            get { return _market_segment; }
            set { _market_segment = value; }
        }
        /// <summary>
        /// 组织机构代码（工商）---宇通
        /// </summary>		
        private string _institution_code;
        public string institution_code
        {
            get { return _institution_code; }
            set { _institution_code = value; }
        }
        /// <summary>
        /// 公司体制---宇通
        /// </summary>		
        private string _com_constitution;
        public string com_constitution
        {
            get { return _com_constitution; }
            set { _com_constitution = value; }
        }
        /// <summary>
        /// 注册资金(万)---宇通
        /// </summary>		
        private string _registered_capital;
        public string registered_capital
        {
            get { return _registered_capital; }
            set { _registered_capital = value; }
        }
        /// <summary>
        /// 车辆结构---宇通
        /// </summary>		
        private string _vehicle_structure;
        public string vehicle_structure
        {
            get { return _vehicle_structure; }
            set { _vehicle_structure = value; }
        }
        /// <summary>
        /// 经销商---宇通
        /// </summary>		
        private string _agency;
        public string agency
        {
            get { return _agency; }
            set { _agency = value; }
        }
        /// <summary>
        /// 客户sap代码---宇通
        /// </summary>		
        private string _sap_code;
        public string sap_code
        {
            get { return _sap_code; }
            set { _sap_code = value; }
        }
        /// <summary>
        /// 业务经营范围---宇通
        /// </summary>		
        private string _business_scope;
        public string business_scope
        {
            get { return _business_scope; }
            set { _business_scope = value; }
        }
        /// <summary>
        /// 企业资质---宇通
        /// </summary>		
        private string _ent_qualification;
        public string ent_qualification
        {
            get { return _ent_qualification; }
            set { _ent_qualification = value; }
        }
        /// <summary>
        /// CRM客户ID
        /// </summary>		
        private string _cust_crm_guid;
        public string cust_crm_guid
        {
            get { return _cust_crm_guid; }
            set { _cust_crm_guid = value; }
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

    }
}