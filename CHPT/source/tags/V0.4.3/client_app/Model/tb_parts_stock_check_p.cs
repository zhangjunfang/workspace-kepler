using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 盘点单配件
    /// </summary>
   public class tb_parts_stock_check_p
    {

        /// <summary>
        /// id
        /// </summary>		
        private string _stock_check_parts_id;
        public string stock_check_parts_id
        {
            get { return _stock_check_parts_id; }
            set { _stock_check_parts_id = value; }
        }
        /// <summary>
        /// 盘点单ID
        /// </summary>		
        private string _stock_check_id;
        public string stock_check_id
        {
            get { return _stock_check_id; }
            set { _stock_check_id = value; }
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
        private string _parts_code;
        public string parts_code
        {
            get { return _parts_code; }
            set { _parts_code = value; }
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
        /// model
        /// </summary>		
        private string _model;
        public string model
        {
            get { return _model; }
            set { _model = value; }
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
        /// 厂家编码
        /// </summary>		
        private string _car_parts_code;
        public string car_parts_code
        {
            get { return _car_parts_code; }
            set { _car_parts_code = value; }
        }
        /// <summary>
        /// 条形码
        /// </summary>		
        private string _parts_barcode;
        public string parts_barcode
        {
            get { return _parts_barcode; }
            set { _parts_barcode = value; }
        }
        /// <summary>
        /// 账面数量
        /// </summary>		
        private int _paper_count;
        public int paper_count
        {
            get { return _paper_count; }
            set { _paper_count = value; }
        }
        /// <summary>
        /// 实盘数量
        /// </summary>		
        private int _firmoffer_count;
        public int firmoffer_count
        {
            get { return _firmoffer_count; }
            set { _firmoffer_count = value; }
        }
        /// <summary>
        /// business_price
        /// </summary>		
        private decimal _business_price;
        public decimal business_price
        {
            get { return _business_price; }
            set { _business_price = value; }
        }
        /// <summary>
        /// profitloss_count
        /// </summary>		
        private int _profitloss_count;
        public int profitloss_count
        {
            get { return _profitloss_count; }
            set { _profitloss_count = value; }
        }
        /// <summary>
        /// 金额
        /// </summary>		
        private decimal _money;
        public decimal money
        {
            get { return _money; }
            set { _money = value; }
        }
        /// <summary>
        /// make_date
        /// </summary>		
        private long _make_date;
        public long make_date
        {
            get { return _make_date; }
            set { _make_date = value; }
        }
        /// <summary>
        /// validity_date
        /// </summary>		
        private long _validity_date;
        public long validity_date
        {
            get { return _validity_date; }
            set { _validity_date = value; }
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
        /// 仓库id
        /// </summary>		
        private string _wh_id;
        public string wh_id
        {
            get { return _wh_id; }
            set { _wh_id = value; }
        }
        /// <summary>
        /// 仓库编码
        /// </summary>		
        private string _wh_code;
        public string wh_code
        {
            get { return _wh_code; }
            set { _wh_code = value; }
        }
        /// <summary>
        /// 仓库名称，历史字段
        /// </summary>		
        private string _wh_name;
        public string wh_name
        {
            get { return _wh_name; }
            set { _wh_name = value; }
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
    }
}
