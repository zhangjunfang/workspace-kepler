using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary> 企业注册信息
    /// </summary>
    public class tb_signinfo
    {
        #region 企业注册信息
        /// <summary>
        /// 记录id
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
        /// 有效期
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
        private string _contact_tel;
        public string contact_tel
        {
            get { return _contact_tel; }
            set { _contact_tel = value; }
        }
        /// <summary>
        /// 联系手机
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
        /// socket用户名
        /// </summary>		
        private string _s_user;
        public string s_user
        {
            get { return _s_user; }
            set { _s_user = value; }
        }
        /// <summary>
        /// socket密码
        /// </summary>		
        private string _s_pwd;
        public string s_pwd
        {
            get { return _s_pwd; }
            set { _s_pwd = value; }
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
        /// 鉴权状态 0未授权   1授权
        /// </summary>		
        private string _authentication_status;
        public string authentication_status
        {
            get { return _authentication_status; }
            set { _authentication_status = value; }
        }
        /// <summary>
        /// 服务站id，云平台用
        /// </summary>		
        private string _ser_station_id;
        public string ser_station_id
        {
            get { return _ser_station_id; }
            set { _ser_station_id = value; }
        }
        /// <summary>
        /// 帐套id，云平台用
        /// </summary>		
        private string _set_book_id;
        public string set_book_id
        {
            get { return _set_book_id; }
            set { _set_book_id = value; }
        }
        #endregion

        #region 企业注册信息-宇通
        /// <summary>
        /// sign_yt_id
        /// </summary>		
        private string _sign_yt_id;
        public string sign_yt_id
        {
            get { return _sign_yt_id; }
            set { _sign_yt_id = value; }
        }
        /// <summary>
        /// 级别
        /// </summary>		
        private string _category;
        public string category
        {
            get { return _category; }
            set { _category = value; }
        }
        /// <summary>
        /// 站别
        /// </summary>		
        private string _com_level;
        public string com_level
        {
            get { return _com_level; }
            set { _com_level = value; }
        }
        /// <summary>
        /// 星级
        /// </summary>		
        private string _star_level;
        public string star_level
        {
            get { return _star_level; }
            set { _star_level = value; }
        }
        /// <summary>
        /// 工时单价
        /// </summary>		
        private decimal _workhours_price;
        public decimal workhours_price
        {
            get { return _workhours_price; }
            set { _workhours_price = value; }
        }
        /// <summary>
        /// 冬季补贴
        /// </summary>		
        private decimal _winter_subsidy;
        public decimal winter_subsidy
        {
            get { return _winter_subsidy; }
            set { _winter_subsidy = value; }
        }
        /// <summary>
        /// 三包外工时单价
        /// </summary>		
        private decimal _three_out_price;
        public decimal three_out_price
        {
            get { return _three_out_price; }
            set { _three_out_price = value; }
        }
        /// <summary>
        /// 中心库站代码
        /// </summary>		
        private string _center_library;
        public string center_library
        {
            get { return _center_library; }
            set { _center_library = value; }
        }
        /// <summary>
        /// 外服人员
        /// </summary>		
        private string _out_ser_person;
        public string out_ser_person
        {
            get { return _out_ser_person; }
            set { _out_ser_person = value; }
        }
        /// <summary>
        /// 否为维修NG
        /// </summary>		
        private string _is_repair_ng;
        public string is_repair_ng
        {
            get { return _is_repair_ng; }
            set { _is_repair_ng = value; }
        }
        /// <summary>
        /// 内否维修新能源
        /// </summary>		
        private string _is_repair_newenergy;
        public string is_repair_newenergy
        {
            get { return _is_repair_newenergy; }
            set { _is_repair_newenergy = value; }
        }
        /// <summary>
        /// 服务站sap代码
        /// </summary>		
        private string _service_station_sap;
        public string service_station_sap
        {
            get { return _service_station_sap; }
            set { _service_station_sap = value; }
        }
        /// <summary>
        /// 接入码---宇通接口用
        /// </summary>		
        private string _access_code;
        public string access_code
        {
            get { return _access_code; }
            set { _access_code = value; }
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
        #endregion
    }
}
