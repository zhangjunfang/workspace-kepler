using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yuTongWebService
{
    /// <summary>
    /// 查询车辆
    /// </summary>
    public class QueryBusModels
    {
        public string ReturnStatus
        {
            get;
            set;
        }
        public string ReturnValue
        {
            get;
            set;
        }

        public string ReturnTime
        {
            get;
            set;
        }
        public QueryBus.Detail[] busDetails
        {
            get;
            set;
        }

    }
}
