using System.Data;

namespace CloudPlatSocket.Model
{
    /// <summary>
    /// 服务站数据模型
    /// 创建人：杨天帅
    /// 创建时间：2014.11.13
    /// 修改时间：2014.11.13
    /// </summary>
    public class ServiceStationDataModel
    {
        #region --构造函数
        public ServiceStationDataModel()
        {

        }
        public ServiceStationDataModel(DataRow dr)
        {
            if (dr == null)
            {
                return;
            }
            if (dr.Table.Columns.Contains("serviceStationSap"))
            {
                this.sap = dr["serviceStationSap"].ToString();
            }
            if (dr.Table.Columns.Contains("requestTime"))
            {
                this.requestTime = dr["requestTime"].ToString();
            }
            if (dr.Table.Columns.Contains("billType"))
            {
                this.billType = dr["billType"].ToString();
            }
            if (dr.Table.Columns.Contains("billNumber"))
            {
                this.billNumber = dr["billNumber"].ToString();
            }
            if (dr.Table.Columns.Contains("opType"))
            {
                this.operType = dr["opType"].ToString();
            }
        }
        #endregion

        #region --成员属性
        private string sap;
        /// <summary> 获取或设置服务站SAP代码
        /// </summary>
        public string Sap
        {
            get
            {
                return sap;
            }
            set
            {
                this.sap = value;
            }
        }

        private string requestTime;
        /// <summary> 获取或设置请求时间
        /// </summary>
        public string RequestTime
        {
            get
            {
                return requestTime;
            }
            set
            {
                this.requestTime = value;
            }
        }

        private string billType;
        /// <summary> 获取或设置单据类型
        /// </summary>
        public string BillType
        {
            get
            {
                return billType;
            }
            set
            {
                this.billType = value;
            }
        }

        private string billNumber;
        /// <summary> 获取或设置单据号
        /// </summary>
        public string BillNumber
        {
            get
            {
                return billNumber;
            }
            set
            {
                this.billNumber = value;
            }
        }

        private string operType;
        /// <summary> 获取或设置操作类型
        /// </summary>
        public string OperType
        {
            get
            {
                return operType;
            }
            set
            {
                this.operType = value;
            }
        }
        #endregion
    }
}