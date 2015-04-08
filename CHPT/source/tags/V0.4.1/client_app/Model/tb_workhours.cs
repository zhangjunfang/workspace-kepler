using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //工时档案
    public class tb_workhours
    {

        /// <summary>
        /// 工时档案ID
        /// </summary>		
        private string _whours_id;
        public string whours_id
        {
            get { return _whours_id; }
            set { _whours_id = value; }
        }
        /// <summary>
        /// 数据来源，关联字典码表
        /// </summary>		
        private string _data_sources;
        public string data_sources
        {
            get { return _data_sources; }
            set { _data_sources = value; }
        }
        /// <summary>
        /// 维修项目类别，关联字典码表
        /// </summary>		
        private string _repair_type;
        public string repair_type
        {
            get { return _repair_type; }
            set { _repair_type = value; }
        }
        /// <summary>
        /// 项目编号
        /// </summary>		
        private string _project_num;
        public string project_num
        {
            get { return _project_num; }
            set { _project_num = value; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>		
        private string _project_name;
        public string project_name
        {
            get { return _project_name; }
            set { _project_name = value; }
        }
        /// <summary>
        /// 项目备注
        /// </summary>		
        private string _project_remark;
        public string project_remark
        {
            get { return _project_remark; }
            set { _project_remark = value; }
        }
        /// <summary>
        /// 工时类型，1工时，2定额
        /// </summary>		
        private string _whours_type;
        public string whours_type
        {
            get { return _whours_type; }
            set { _whours_type = value; }
        }
        /// <summary>
        /// A类工时数
        /// </summary>		
        private decimal _whours_num_a;
        public decimal whours_num_a
        {
            get { return _whours_num_a; }
            set { _whours_num_a = value; }
        }
        /// <summary>
        /// B类工时数
        /// </summary>		
        private decimal _whours_num_b;
        public decimal whours_num_b
        {
            get { return _whours_num_b; }
            set { _whours_num_b = value; }
        }
        /// <summary>
        /// C类工时数
        /// </summary>		
        private decimal _whours_num_c;
        public decimal whours_num_c
        {
            get { return _whours_num_c; }
            set { _whours_num_c = value; }
        }
        /// <summary>
        /// 定额单价
        /// </summary>		
        private decimal _quota_price;
        public decimal quota_price
        {
            get { return _quota_price; }
            set { _quota_price = value; }
        }
        /// <summary>
        /// 状态
        /// </summary>		
        private int _status;
        public int status
        {
            get { return _status; }
            set { _status = value; }
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
        /// 删除标记，0为删除，1未删除  默认1
        /// </summary>		
        private string _enable_flag;
        public string enable_flag
        {
            get { return _enable_flag; }
            set { _enable_flag = value; }
        }

    }
}
