using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //tb_signing_info
    public class tb_signing_info
    {
        /// <summary>
        /// id
        /// </summary>		
        private string _sign_id;
        public string sign_id
        {
            get { return _sign_id; }
            set { _sign_id = value; }
        }
        /// <summary>
        /// 编码
        /// </summary>		
        private string _sign_code;
        public string sign_code
        {
            get { return _sign_code; }
            set { _sign_code = value; }
        }
        /// <summary>
        /// 签约品牌，关联字典表
        /// </summary>		
        private string _sign_brand;
        public string sign_brand
        {
            get { return _sign_brand; }
            set { _sign_brand = value; }
        }
        /// <summary>
        /// 签约服务站编码
        /// </summary>		
        private string _com_code;
        public string com_code
        {
            get { return _com_code; }
            set { _com_code = value; }
        }
        /// <summary>
        /// 签约类型
        /// </summary>		
        private string _sign_type;
        public string sign_type
        {
            get { return _sign_type; }
            set { _sign_type = value; }
        }
        /// <summary>
        /// 服务站简称
        /// </summary>		
        private string _com_short_name;
        public string com_short_name
        {
            get { return _com_short_name; }
            set { _com_short_name = value; }
        }
        /// <summary>
        /// 服务站全称
        /// </summary>		
        private string _com_name;
        public string com_name
        {
            get { return _com_name; }
            set { _com_name = value; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>		
        private long _apply_time;
        public long apply_time
        {
            get { return _apply_time; }
            set { _apply_time = value; }
        }
        /// <summary>
        /// 建站时间
        /// </summary>		
        private long _approved_time;
        public long approved_time
        {
            get { return _approved_time; }
            set { _approved_time = value; }
        }
        /// <summary>
        /// 协议到期时间
        /// </summary>		
        private long _protocol_expires_time;
        public long protocol_expires_time
        {
            get { return _protocol_expires_time; }
            set { _protocol_expires_time = value; }
        }
        /// <summary>
        /// 系统名称
        /// </summary>		
        private string _system_name;
        public string system_name
        {
            get { return _system_name; }
            set { _system_name = value; }
        }
        /// <summary>
        /// 协议类型，关联字典表
        /// </summary>		
        private string _protocol_type;
        public string protocol_type
        {
            get { return _protocol_type; }
            set { _protocol_type = value; }
        }
        /// <summary>
        /// 服务器IP
        /// </summary>		
        private string _server_ip;
        public string server_ip
        {
            get { return _server_ip; }
            set { _server_ip = value; }
        }
        /// <summary>
        /// 服务器端口
        /// </summary>		
        private string _server_port;
        public string server_port
        {
            get { return _server_port; }
            set { _server_port = value; }
        }
        /// <summary>
        /// 用户名
        /// </summary>		
        private string _user_name;
        public string user_name
        {
            get { return _user_name; }
            set { _user_name = value; }
        }
        /// <summary>
        /// 帐号
        /// </summary>		
        private string _account;
        public string account
        {
            get { return _account; }
            set { _account = value; }
        }
        /// <summary>
        /// 密码
        /// </summary>		
        private string _password;
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }
        /// <summary>
        /// 密钥
        /// </summary>		
        private string _secret_key;
        public string secret_key
        {
            get { return _secret_key; }
            set { _secret_key = value; }
        }
        /// <summary>
        /// 接入码
        /// </summary>		
        private string _access_code;
        public string access_code
        {
            get { return _access_code; }
            set { _access_code = value; }
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
        /// 热线电话
        /// </summary>		
        private string _hotline;
        public string hotline
        {
            get { return _hotline; }
            set { _hotline = value; }
        }
        /// <summary>
        /// 省
        /// </summary>		
        private string _province;
        public string province
        {
            get { return _province; }
            set { _province = value; }
        }
        /// <summary>
        /// 市
        /// </summary>		
        private string _city;
        public string city
        {
            get { return _city; }
            set { _city = value; }
        }
        /// <summary>
        /// 区
        /// </summary>		
        private string _county;
        public string county
        {
            get { return _county; }
            set { _county = value; }
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
        /// 联系地址
        /// </summary>		
        private string _contact_address;
        public string contact_address
        {
            get { return _contact_address; }
            set { _contact_address = value; }
        }
        /// <summary>
        /// 联系人
        /// </summary>		
        private string _contact;
        public string contact
        {
            get { return _contact; }
            set { _contact = value; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>		
        private string _contact_phone;
        public string contact_phone
        {
            get { return _contact_phone; }
            set { _contact_phone = value; }
        }
        /// <summary>
        /// 维修资质
        /// </summary>		
        private string _repair_qualification;
        public string repair_qualification
        {
            get { return _repair_qualification; }
            set { _repair_qualification = value; }
        }
        /// <summary>
        /// 单位性质
        /// </summary>		
        private string _nature_unit;
        public string nature_unit
        {
            get { return _nature_unit; }
            set { _nature_unit = value; }
        }
        /// <summary>
        /// 电子邮件
        /// </summary>		
        private string _email;
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
        /// <summary>
        /// 传真
        /// </summary>		
        private string _fax;
        public string fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        /// <summary>
        /// 机器序列码
        /// </summary>		
        private string _machine_code_sequence;
        public string machine_code_sequence
        {
            get { return _machine_code_sequence; }
            set { _machine_code_sequence = value; }
        }
        /// <summary>
        /// 鉴权码
        /// </summary>		
        private string _authentication;
        public string authentication
        {
            get { return _authentication; }
            set { _authentication = value; }
        }
        /// <summary>
        /// 授权码
        /// </summary>		
        private string _grant_authorization;
        public string grant_authorization
        {
            get { return _grant_authorization; }
            set { _grant_authorization = value; }
        }
        /// <summary>
        /// 数据来源
        /// </summary>		
        private string _data_sources;
        public string data_sources
        {
            get { return _data_sources; }
            set { _data_sources = value; }
        }
        /// <summary>
        /// 状态
        /// </summary>		
        private string _status;
        public string status
        {
            get { return _status; }
            set { _status = value; }
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
