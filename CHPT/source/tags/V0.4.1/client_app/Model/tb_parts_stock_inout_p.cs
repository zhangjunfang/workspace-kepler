using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //tb_parts_stock_inout_p
    public class tb_parts_stock_inout_p
    {

        /// <summary>
        /// id
        /// </summary>		
        private string _stock_inout_parts_id;
        public string stock_inout_parts_id
        {
            get { return _stock_inout_parts_id; }
            set { _stock_inout_parts_id = value; }
        }
        /// <summary>
        /// 出入库单ID
        /// </summary>		
        private string _stock_inout_id;
        public string stock_inout_id
        {
            get { return _stock_inout_id; }
            set { _stock_inout_id = value; }
        }
        /// <summary>
        /// 仓库
        /// </summary>		
        private string _wh_id;
        public string wh_id
        {
            get { return _wh_id; }
            set { _wh_id = value; }
        }
        /// <summary>
        /// 序号
        /// </summary>		
        private int _num;
        public int num
        {
            get { return _num; }
            set { _num = value; }
        }
        /// <summary>
        /// 配件编码
        /// </summary>		
        private string _parts_num;
        public string parts_num
        {
            get { return _parts_num; }
            set { _parts_num = value; }
        }
        /// <summary>
        /// 配件名称
        /// </summary>		
        private string _parts_name;
        public string parts_name
        {
            get { return _parts_name; }
            set { _parts_name = value; }
        }
        /// <summary>
        /// 图号
        /// </summary>		
        private string _drawing_num;
        public string drawing_num
        {
            get { return _drawing_num; }
            set { _drawing_num = value; }
        }
        /// <summary>
        /// 单位
        /// </summary>		
        private string _unit;
        public string unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        /// <summary>
        /// 品牌
        /// </summary>		
        private string _parts_brand;
        public string parts_brand
        {
            get { return _parts_brand; }
            set { _parts_brand = value; }
        }
        /// <summary>
        /// 数量
        /// </summary>		
        private int _count;
        public int count
        {
            get { return _count; }
            set { _count = value; }
        }
        /// <summary>
        /// 是否赠品，0否，1是
        /// </summary>		
        private string _is_gift;
        public string is_gift
        {
            get { return _is_gift; }
            set { _is_gift = value; }
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
        /// 单位名称，历史字段
        /// </summary>		
        private string _unit_name;
        public string unit_name
        {
            get { return _unit_name; }
            set { _unit_name = value; }
        }
        /// <summary>
        /// 创建人名称，历史字段
        /// </summary>		
        private string _create_name;
        public string create_name
        {
            get { return _create_name; }
            set { _create_name = value; }
        }
        /// <summary>
        /// 最后编辑人名称，历史字段
        /// </summary>		
        private string _update_name;
        public string update_name
        {
            get { return _update_name; }
            set { _update_name = value; }
        }

    }
}
