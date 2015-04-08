using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 旧件回收
    /// 有于调用宇通旧件回收接口
    /// </summary>
    public class partReturn
    {

        private string info_status_ytField = string.Empty;

        private string crm_old_bill_numField = string.Empty;

        private string sap_codeField = string.Empty;

        private string create_time_startField = string.Empty;

        private string create_time_endField = string.Empty;

        private partReturnPartDetail[] partDetailsField;
        /// <summary>
        /// 单据状态-宇通
        /// </summary>
        public string info_status_yt
        {
            get
            {
                return this.info_status_ytField;
            }
            set
            {
                this.info_status_ytField = value;
            }
        }
        /// <summary>
        /// 旧件回收单号
        /// </summary>
        public string crm_old_bill_num
        {
            get
            {
                return this.crm_old_bill_numField;
            }
            set
            {
                this.crm_old_bill_numField = value;
            }
        }
        /// <summary>
        /// 服务站编号,调用时可以不用填写
        /// </summary>
        public string sap_code
        {
            get
            {
                return this.sap_codeField;
            }
            set
            {
                this.sap_codeField = value;
            }
        }
        /// <summary>
        /// 回收周期开始
        /// </summary>
        public string create_time_start
        {
            get
            {
                return this.create_time_startField;
            }
            set
            {
                this.create_time_startField = value;
            }
        }
        /// <summary>
        /// 回收周期结束
        /// </summary>
        public string create_time_end
        {
            get
            {
                return this.create_time_endField;
            }
            set
            {
                this.create_time_endField = value;
            }
        }

        public partReturnPartDetail[] PartDetails
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

    /// <summary>
    /// 旧件回收明细
    /// </summary>
    public partial class partReturnPartDetail
    {

        private string parts_idField;

        private string service_noField;

        private string car_parts_codeField;

        private string parts_remarkField;

        private string change_numField;

        private string send_numField;

        private string process_modeField;

        private string remarkField;

        /// <summary>
        /// 明细ID
        /// </summary>
        public string parts_id
        {
            get
            {
                return this.parts_idField;
            }
            set
            {
                this.parts_idField = value;
            }
        }
        /// <summary>
        /// 服务单号
        /// </summary>
        public string service_no
        {
            get
            {
                return this.service_noField;
            }
            set
            {
                this.service_noField = value;
            }
        }
        /// <summary>
        /// 配件编号
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
        /// <summary>
        /// 接收说明
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
        /// <summary>
        /// 更换数量
        /// </summary>
        public string change_num
        {
            get
            {
                return this.change_numField;
            }
            set
            {
                this.change_numField = value;
            }
        }
        /// <summary>
        /// 发运数量
        /// </summary>
        public string send_num
        {
            get
            {
                return this.send_numField;
            }
            set
            {
                this.send_numField = value;
            }
        }
        /// <summary>
        /// 处理方式
        /// </summary>
        public string process_mode
        {
            get
            {
                return this.process_modeField;
            }
            set
            {
                this.process_modeField = value;
            }
        }
        /// <summary>
        /// 备注
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
    }
}
