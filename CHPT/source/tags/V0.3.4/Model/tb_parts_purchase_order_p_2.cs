using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    // 宇通采购订单-配件 
    public class tb_parts_purchase_order_p_2
    {

        /// <summary>
        /// id
        /// </summary>		
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 宇通采购订单ID
        /// </summary>		
        private string _purchase_order_yt_id;
        public string purchase_order_yt_id
        {
            get { return _purchase_order_yt_id; }
            set { _purchase_order_yt_id = value; }
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
        /// 配件编号
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
        /// 配件类型Id
        /// </summary>		
        private string _parts_type_id;
        public string parts_type_id
        {
            get { return _parts_type_id; }
            set { _parts_type_id = value; }
        }
        /// <summary>
        /// 配件类型名称
        /// </summary>		
        private string _parts_type_name;
        public string parts_type_name
        {
            get { return _parts_type_name; }
            set { _parts_type_name = value; }
        }
        /// <summary>
        /// 品牌ID
        /// </summary>		
        private string _parts_brand;
        public string parts_brand
        {
            get { return _parts_brand; }
            set { _parts_brand = value; }
        }
        /// <summary>
        /// 品牌名称
        /// </summary>		
        private string _parts_brand_name;
        public string parts_brand_name
        {
            get { return _parts_brand_name; }
            set { _parts_brand_name = value; }
        }
        /// <summary>
        /// 单位
        /// </summary>		
        private string _unit_id;
        public string unit_id
        {
            get { return _unit_id; }
            set { _unit_id = value; }
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
        /// 配件条码
        /// </summary>		
        private string _parts_barcode;
        public string parts_barcode
        {
            get { return _parts_barcode; }
            set { _parts_barcode = value; }
        }
        /// <summary>
        /// 车厂编号
        /// </summary>		
        private string _car_factory_code;
        public string car_factory_code
        {
            get { return _car_factory_code; }
            set { _car_factory_code = value; }
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
        /// 规格型号
        /// </summary>		
        private string _model;
        public string model
        {
            get { return _model; }
            set { _model = value; }
        }
        /// <summary>
        /// 配件属性
        /// </summary>		
        private string _parts_att;
        public string parts_att
        {
            get { return _parts_att; }
            set { _parts_att = value; }
        }
        /// <summary>
        /// 申请数量
        /// </summary>		
        private int _application_count;
        public int application_count
        {
            get { return _application_count; }
            set { _application_count = value; }
        }
        /// <summary>
        /// 单价
        /// </summary>		
        private decimal _price;
        public decimal price
        {
            get { return _price; }
            set { _price = value; }
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
        /// 预计到货时间
        /// </summary>		
        private long _predict_arrival_date;
        public long predict_arrival_date
        {
            get { return _predict_arrival_date; }
            set { _predict_arrival_date = value; }
        }
        /// <summary>
        /// 实际到货时间
        /// </summary>		
        private long _reality_arrival_date;
        public long reality_arrival_date
        {
            get { return _reality_arrival_date; }
            set { _reality_arrival_date = value; }
        }
        /// <summary>
        /// 确认数量
        /// </summary>		
        private int _conf_count;
        public int conf_count
        {
            get { return _conf_count; }
            set { _conf_count = value; }
        }
        /// <summary>
        /// 配件说明
        /// </summary>		
        private string _parts_explain;
        public string parts_explain
        {
            get { return _parts_explain; }
            set { _parts_explain = value; }
        }
        /// <summary>
        /// 替代
        /// </summary>		
        private string _replaces;
        public string replaces
        {
            get { return _replaces; }
            set { _replaces = value; }
        }
        /// <summary>
        /// 中心站/库处理说明
        /// </summary>		
        private string _center_library_explain;
        public string center_library_explain
        {
            get { return _center_library_explain; }
            set { _center_library_explain = value; }
        }
        /// <summary>
        /// 总库处理说明
        /// </summary>		
        private string _total_library_explain;
        public string total_library_explain
        {
            get { return _total_library_explain; }
            set { _total_library_explain = value; }
        }
        /// <summary>
        /// 取消原因
        /// </summary>		
        private string _cancel_reasons;
        public string cancel_reasons
        {
            get { return _cancel_reasons; }
            set { _cancel_reasons = value; }
        }
        /// <summary>
        /// 引用单号
        /// </summary>		
        private string _relation_order;
        public string relation_order
        {
            get { return _relation_order; }
            set { _relation_order = value; }
        }
        /// <summary>
        /// 导入类型
        /// </summary>		
        private string _importtype;
        public string ImportType
        {
            get { return _importtype; }
            set { _importtype = value; }
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
        /// 创建人名称，历史字段
        /// </summary>		
        private string _create_name;
        public string create_name
        {
            get { return _create_name; }
            set { _create_name = value; }
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
        /// 最后编辑人
        /// </summary>		
        private string _update_by;
        public string update_by
        {
            get { return _update_by; }
            set { _update_by = value; }
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
        /// 最后编辑时间
        /// </summary>		
        private long _update_time;
        public long update_time
        {
            get { return _update_time; }
            set { _update_time = value; }
        }
        ///// <summary>
        ///// 服务站id，云平台用
        ///// </summary>		
        //private string _ser_station_id;
        //public string ser_station_id
        //{
        //    get { return _ser_station_id; }
        //    set { _ser_station_id = value; }
        //}
        ///// <summary>
        ///// 帐套id，云平台用
        ///// </summary>		
        //private string _set_book_id;
        //public string set_book_id
        //{
        //    get { return _set_book_id; }
        //    set { _set_book_id = value; }
        //}

    }
}
