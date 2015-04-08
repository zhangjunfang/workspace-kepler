using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //联系人表
    public class tb_contacts
    {
        /// <summary>
        /// id
        /// </summary>		
        private string _cont_id;
        public string cont_id
        {
            get { return _cont_id; }
            set { _cont_id = value; }
        }
        /// <summary>
        /// 联系人名称
        /// </summary>		
        private string _cont_name;
        public string cont_name
        {
            get { return _cont_name; }
            set { _cont_name = value; }
        }
        /// <summary>
        /// 联系人职务，关联字典
        /// </summary>		
        private string _cont_post;
        public string cont_post
        {
            get { return _cont_post; }
            set { _cont_post = value; }
        }
        /// <summary>
        /// 联系人手机
        /// </summary>		
        private string _cont_phone;
        public string cont_phone
        {
            get { return _cont_phone; }
            set { _cont_phone = value; }
        }
        /// <summary>
        /// 联系人固话
        /// </summary>		
        private string _cont_tel;
        public string cont_tel
        {
            get { return _cont_tel; }
            set { _cont_tel = value; }
        }
        /// <summary>
        /// 联系人邮箱
        /// </summary>		
        private string _cont_email;
        public string cont_email
        {
            get { return _cont_email; }
            set { _cont_email = value; }
        }
        /// <summary>
        /// 联系人生日
        /// </summary>		
        private long _cont_birthday;
        public long cont_birthday
        {
            get { return _cont_birthday; }
            set { _cont_birthday = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>		
        private string _sex;
        public string sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        /// <summary>
        /// 民族
        /// </summary>		
        private string _nation;
        public string nation
        {
            get { return _nation; }
            set { _nation = value; }
        }
        /// <summary>
        /// 是否车主 0否，1是   默认0
        /// </summary>		
        private string _is_car_owner;
        public string is_car_owner
        {
            get { return _is_car_owner; }
            set { _is_car_owner = value; }
        }
        /// <summary>
        /// 是否默认  0否，1是  默认0
        /// </summary>		
        private string _is_default;
        public string is_default
        {
            get { return _is_default; }
            set { _is_default = value; }
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
        /// 上级客户---宇通
        /// </summary>		
        private string _parent_customer;
        public string parent_customer
        {
            get { return _parent_customer; }
            set { _parent_customer = value; }
        }
        /// <summary>
        /// 职务备注---宇通
        /// </summary>		
        private string _post_remark;
        public string post_remark
        {
            get { return _post_remark; }
            set { _post_remark = value; }
        }
        /// <summary>
        /// 删除标记，0为删除，1未删除  默认1
        /// </summary>		
        private string _enable_flag;
        public string enable_flag
        {
            get { return _enable_flag; }
            set { _enable_flag = value; }
        }
        /// <summary>
        /// 数据来源 1,自建   2，宇通
        /// </summary>		
        private string _data_source;
        public string data_source
        {
            get { return _data_source; }
            set { _data_source = value; }
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
        /// 最后修改时间
        /// </summary>		
        private long _update_time;
        public long update_time
        {
            get { return _update_time; }
            set { _update_time = value; }
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
        /// 最后修改人，关联人员表
        /// </summary>		
        private string _update_by;
        public string update_by
        {
            get { return _update_by; }
            set { _update_by = value; }
        }
    }

    public class tb_contacts_ex 
    {
        private string cont_idField;
        private string crmcont_guidField;

        private string sexField;

        private string cont_nameField;

        private string nationField;

        private string parent_customerField;

        private string cont_phoneField;

        private string cont_postField;

        private string cont_post_remarkField;

        private string statusField;

        public string cont_id
        {
            get { return this.cont_idField; }
            set { this.cont_idField = value; }
        }

        public string cont_crm_guid
        {
            get
            {
                return this.crmcont_guidField;
            }
            set
            {
                this.crmcont_guidField = value;
            }
        }

        public string sex
        {
            get
            {
                return this.sexField;
            }
            set
            {
                this.sexField = value;
            }
        }


        public string cont_name
        {
            get
            {
                return this.cont_nameField;
            }
            set
            {
                this.cont_nameField = value;
            }
        }

        public string nation
        {
            get
            {
                return this.nationField;
            }
            set
            {
                this.nationField = value;
            }
        }

        public string parent_customer
        {
            get
            {
                return this.parent_customerField;
            }
            set
            {
                this.parent_customerField = value;
            }
        }

        public string cont_phone
        {
            get
            {
                return this.cont_phoneField;
            }
            set
            {
                this.cont_phoneField = value;
            }
        }

        public string cont_post
        {
            get
            {
                return this.cont_postField;
            }
            set
            {
                this.cont_postField = value;
            }
        }

        public string cont_post_remark
        {
            get
            {
                return this.cont_post_remarkField;
            }
            set
            {
                this.cont_post_remarkField = value;
            }
        }

        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <summary>
        /// 联系人类别
        /// </summary>
        public string contact_type
        {
            get;
            set;
        }

    }
}
