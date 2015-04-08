using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //仓库货位档案
    public class tb_cargo_space
    {

        /// <summary>
        /// id
        /// </summary>		
        private string _cs_id;
        public string cs_id
        {
            get { return _cs_id; }
            set { _cs_id = value; }
        }
        /// <summary>
        /// 所属仓库
        /// </summary>		
        private string _wh_id;
        public string wh_id
        {
            get { return _wh_id; }
            set { _wh_id = value; }
        }
        /// <summary>
        /// 货位编号
        /// </summary>		
        private string _cs_code;
        public string cs_code
        {
            get { return _cs_code; }
            set { _cs_code = value; }
        }
        /// <summary>
        /// 货位名称
        /// </summary>		
        private string _cs_name;
        public string cs_name
        {
            get { return _cs_name; }
            set { _cs_name = value; }
        }
        /// <summary>
        /// 库存上限
        /// </summary>		
        private int _stock_u_limit;
        public int stock_u_limit
        {
            get { return _stock_u_limit; }
            set { _stock_u_limit = value; }
        }
        /// <summary>
        /// 库存下限
        /// </summary>		
        private int _stock_l_limit;
        public int stock_l_limit
        {
            get { return _stock_l_limit; }
            set { _stock_l_limit = value; }
        }
        /// <summary>
        /// 货位备注
        /// </summary>		
        private string _remark;
        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
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
        /// 最后编辑时间
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
        /// 最后编辑人，关联人员表
        /// </summary>		
        private string _update_by;
        public string update_by
        {
            get { return _update_by; }
            set { _update_by = value; }
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
    }
}
