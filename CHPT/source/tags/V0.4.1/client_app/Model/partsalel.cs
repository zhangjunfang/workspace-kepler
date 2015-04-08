using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class partsale
    {
        private string sale_dateField;

        private string cust_nameField;

        private string cust_phoneField;

        private string turnerField;

        private string license_plateField;

        private string amountField;

        private string remarkField;

        private partDetail[] partDetailsField;

        /// <summary>出库日期
        /// </summary>
        public string sale_date
        {
            get
            {
                return this.sale_dateField;
            }
            set
            {
                this.sale_dateField = value;
            }
        }

        /// <summary> 购买者(联系人)
        /// </summary>
        public string cust_name
        {
            get
            {
                return this.cust_nameField;
            }
            set
            {
                this.cust_nameField = value;
            }
        }

        /// <summary> 购买人电话（联系人电话）
        /// </summary>
        public string cust_phone
        {
            get
            {
                return this.cust_phoneField;
            }
            set
            {
                this.cust_phoneField = value;
            }
        }

        /// <summary> 车工号
        /// </summary>
        public string turner
        {
            get
            {
                return this.turnerField;
            }
            set
            {
                this.turnerField = value;
            }
        }

        /// <summary> 车牌号
        /// </summary>
        public string license_plate
        {
            get
            {
                return this.license_plateField;
            }
            set
            {
                this.license_plateField = value;
            }
        }

        /// <summary> 总金额
        /// </summary>
        public string amount
        {
            get
            {
                return this.amountField;
            }
            set
            {
                this.amountField = value;
            }
        }

        /// <summary> 备注
        /// </summary>
        public string remark
        {
            get
            {
                return this.remarkField;
            }
            set
            {
                this.remarkField = value;
            }
        }

        /// <summary> 配件明细
        /// </summary>
        public partDetail[] partDetails
        {
            get
            {
                return this.partDetailsField;
            }
            set
            {
                this.partDetailsField = value;
            }
        }
    }

    public class partDetail
    {
        private string wh_codeField;

        private string car_parts_codeField;

        private string remarkField;

        private string business_countField;

        private string business_priceField;

        private string amountField;

        private string parts_remarkField;


        /// <summary> 仓库编号
        /// </summary>
        public string wh_code
        {
            get
            {
                return this.wh_codeField;
            }
            set
            {
                this.wh_codeField = value;
            }
        }

        /// <summary> 配件编号
        /// </summary>
        public string car_parts_code
        {
            get
            {
                return this.car_parts_codeField;
            }
            set
            {
                this.car_parts_codeField = value;
            }
        }

        /// <summary> 备注
        /// </summary>
        public string remark
        {
            get
            {
                return this.remarkField;
            }
            set
            {
                this.remarkField = value;
            }
        }

        /// <summary> 出库数量
        /// </summary>
        public string business_count
        {
            get
            {
                return this.business_countField;
            }
            set
            {
                this.business_countField = value;
            }
        }

        /// <summary> 单价
        /// </summary>
        public string business_price
        {
            get
            {
                return this.business_priceField;
            }
            set
            {
                this.business_priceField = value;
            }
        }

        /// <summary> 金额
        /// </summary>
        public string amount
        {
            get
            {
                return this.amountField;
            }
            set
            {
                this.amountField = value;
            }
        }

        /// <summary> 配件说明
        /// </summary>
        public string parts_remark
        {
            get
            {
                return this.parts_remarkField;
            }
            set
            {
                this.parts_remarkField = value;
            }
        }
    }
}
