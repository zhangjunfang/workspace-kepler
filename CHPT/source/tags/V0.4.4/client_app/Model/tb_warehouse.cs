using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //仓库档案
    public class tb_warehouse
    {

        /// <summary>
        /// id
        /// </summary>		
        private string _wh_id;
        public string wh_id
        {
            get { return _wh_id; }
            set { _wh_id = value; }
        }
        /// <summary>
        /// 所属公司，关联公司表
        /// </summary>		
        private string _com_id;
        public string com_id
        {
            get { return _com_id; }
            set { _com_id = value; }
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
        /// 仓编码
        /// </summary>		
        private string _wh_code;
        public string wh_code
        {
            get { return _wh_code; }
            set { _wh_code = value; }
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
        /// 仓库名称
        /// </summary>		
        private string _wh_name;
        public string wh_name
        {
            get { return _wh_name; }
            set { _wh_name = value; }
        }
        /// <summary>
        /// 仓库地址
        /// </summary>		
        private string _wh_address;
        public string wh_address
        {
            get { return _wh_address; }
            set { _wh_address = value; }
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
        /// 电话
        /// </summary>		
        private string _wh_tel;
        public string wh_tel
        {
            get { return _wh_tel; }
            set { _wh_tel = value; }
        }
        /// <summary>
        /// 传真
        /// </summary>		
        private string _wh_fax;
        public string wh_fax
        {
            get { return _wh_fax; }
            set { _wh_fax = value; }
        }
        /// <summary>
        /// 负责人
        /// </summary>		
        private string _wh_head;
        public string wh_head
        {
            get { return _wh_head; }
            set { _wh_head = value; }
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
        /// 删除标记，0删除，1未删除
        /// </summary>		
        private string _enable_flag;
        public string enable_flag
        {
            get { return _enable_flag; }
            set { _enable_flag = value; }
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
        /// <summary>
        /// 最后修改时间
        /// </summary>		
        private long _update_time;
        public long update_time
        {
            get { return _update_time; }
            set { _update_time = value; }
        }

    }
}
