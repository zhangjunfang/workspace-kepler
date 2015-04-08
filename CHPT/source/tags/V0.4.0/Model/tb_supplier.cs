using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //供应商档案
    public class tb_supplier
    {

        /// <summary>
        /// id
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
        /// 供应商简称
        /// </summary>		
        private string _sup_short_name;
        public string sup_short_name
        {
            get { return _sup_short_name; }
            set { _sup_short_name = value; }
        }
        /// <summary>
        /// 供应商首拼
        /// </summary>		
        private string _sup_first_spell;
        public string sup_first_spell
        {
            get { return _sup_first_spell; }
            set { _sup_first_spell = value; }
        }
        /// <summary>
        /// 供应商全称
        /// </summary>		
        private string _sup_full_name;
        public string sup_full_name
        {
            get { return _sup_full_name; }
            set { _sup_full_name = value; }
        }
        /// <summary>
        /// 供应商分类，关联字码表典
        /// </summary>		
        private string _sup_type;
        public string sup_type
        {
            get { return _sup_type; }
            set { _sup_type = value; }
        }
        /// <summary>
        /// 联系地址
        /// </summary>		
        private string _sup_address;
        public string sup_address
        {
            get { return _sup_address; }
            set { _sup_address = value; }
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
        /// 邮箱
        /// </summary>		
        private string _sup_email;
        public string sup_email
        {
            get { return _sup_email; }
            set { _sup_email = value; }
        }
        /// <summary>
        /// 电话
        /// </summary>		
        private string _sup_tel;
        public string sup_tel
        {
            get { return _sup_tel; }
            set { _sup_tel = value; }
        }
        /// <summary>
        /// 传真
        /// </summary>		
        private string _sup_fax;
        public string sup_fax
        {
            get { return _sup_fax; }
            set { _sup_fax = value; }
        }
        /// <summary>
        /// 网站
        /// </summary>		
        private string _sup_website;
        public string sup_website
        {
            get { return _sup_website; }
            set { _sup_website = value; }
        }
        /// <summary>
        /// 企业性质，关联字典码表
        /// </summary>		
        private string _unit_properties;
        public string unit_properties
        {
            get { return _unit_properties; }
            set { _unit_properties = value; }
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
        /// 纳税人识别号
        /// </summary>		
        private string _tax_num;
        public string tax_num
        {
            get { return _tax_num; }
            set { _tax_num = value; }
        }
        /// <summary>
        /// 信用等级，关联字典表
        /// </summary>		
        private string _credit_class;
        public string credit_class
        {
            get { return _credit_class; }
            set { _credit_class = value; }
        }
        /// <summary>
        /// 信用额度
        /// </summary>		
        private decimal _credit_line;
        public decimal credit_line
        {
            get { return _credit_line; }
            set { _credit_line = value; }
        }
        /// <summary>
        /// 信用账期
        /// </summary>		
        private decimal _credit_account_period;
        public decimal credit_account_period
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
        /// 备注
        /// </summary>		
        private string _remark;
        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// 状态，关联字典码表
        /// </summary>		
        private string _status;
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// 删除标记，0删除，1未删除    默认为1
        /// </summary>		
        private string _enable_flag;
        public string enable_flag
        {
            get { return _enable_flag; }
            set { _enable_flag = value; }
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
        /// zip_code
        /// </summary>		
        private string _zip_code;
        public string zip_code
        {
            get { return _zip_code; }
            set { _zip_code = value; }
        }

    }
}
