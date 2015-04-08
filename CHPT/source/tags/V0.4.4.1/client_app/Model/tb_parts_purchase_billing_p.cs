using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //tb_parts_purchase_billing_p
    public class tb_parts_purchase_billing_p
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
        /// 采购开单ID
        /// </summary>		
        private string _purchase_billing_id;
        public string purchase_billing_id
        {
            get { return _purchase_billing_id; }
            set { _purchase_billing_id = value; }
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
        /// 配件类型Id
        /// </summary>		
        private string _parts_type_id;
        public string parts_type_id
        {
            get { return _parts_type_id; }
            set { _parts_type_id = value; }
        }
        /// <summary>
        /// 配件类型名称，历史字段
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
        /// 品牌，历史字段
        /// </summary>		
        private string _parts_brand_name;
        public string parts_brand_name
        {
            get { return _parts_brand_name; }
            set { _parts_brand_name = value; }
        }
        /// <summary>
        /// 单位ID
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
        /// 车厂编码
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
        /// 车型名称
        /// </summary>		
        private string _vm_name;
        public string vm_name
        {
            get { return _vm_name; }
            set { _vm_name = value; }
        }
        /// <summary>
        /// 业务单价
        /// </summary>		
        private decimal _business_price;
        public decimal business_price
        {
            get { return _business_price; }
            set { _business_price = value; }
        }
        /// <summary>
        /// 业务数量
        /// </summary>		
        private decimal _business_counts;
        public decimal business_counts
        {
            get { return _business_counts; }
            set { _business_counts = value; }
        }
        /// <summary>
        /// 原始单价
        /// </summary>		
        private decimal _original_price;
        public decimal original_price
        {
            get { return _original_price; }
            set { _original_price = value; }
        }
        /// <summary>
        /// 折扣%
        /// </summary>		
        private decimal _discount;
        public decimal discount
        {
            get { return _discount; }
            set { _discount = value; }
        }
        /// <summary>
        /// 税率
        /// </summary>		
        private decimal _tax_rate;
        public decimal tax_rate
        {
            get { return _tax_rate; }
            set { _tax_rate = value; }
        }
        /// <summary>
        /// 税款
        /// </summary>		
        private decimal _tax;
        public decimal tax
        {
            get { return _tax; }
            set { _tax = value; }
        }
        /// <summary>
        /// 货款
        /// </summary>		
        private decimal _payment;
        public decimal payment
        {
            get { return _payment; }
            set { _payment = value; }
        }
        /// <summary>
        /// 价税合计
        /// </summary>		
        private decimal _valorem_together;
        public decimal valorem_together
        {
            get { return _valorem_together; }
            set { _valorem_together = value; }
        }
        /// <summary>
        /// 赠品
        /// </summary>		
        private string _is_gift;
        public string is_gift
        {
            get { return _is_gift; }
            set { _is_gift = value; }
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
        /// 退货业务数量
        /// </summary>		
        private decimal _return_bus_count;
        public decimal return_bus_count
        {
            get { return _return_bus_count; }
            set { _return_bus_count = value; }
        }
        /// <summary>
        /// 入库数量
        /// </summary>		
        private decimal _storage_count;
        public decimal storage_count
        {
            get { return _storage_count; }
            set { _storage_count = value; }
        }
        /// <summary>
        /// 导入类型
        /// </summary>		
        private string _importordertype;
        public string ImportOrderType
        {
            get { return _importordertype; }
            set { _importordertype = value; }
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
        /// 被引用辅助数量
        /// </summary>		
        private decimal _assisted_count;
        public decimal assisted_count
        {
            get { return _assisted_count; }
            set { _assisted_count = value; }
        }
        /// <summary>
        /// 生产日期
        /// </summary>		
        private long _make_date;
        public long make_date
        {
            get { return _make_date; }
            set { _make_date = value; }
        }
        /// <summary>
        /// 到货日期
        /// </summary>		
        private long _arrival_date;
        public long arrival_date
        {
            get { return _arrival_date; }
            set { _arrival_date = value; }
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
