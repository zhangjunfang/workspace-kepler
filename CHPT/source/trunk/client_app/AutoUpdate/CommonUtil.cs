using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoUpdate
{
    public class CommonUtil
    {
        #region 日志记录
        /// <summary> 全局日志
        /// </summary>
        public static LogService.LoggingService GlobalLogService = Utility.Log.Log.CreateLogService("softupdate", "system") as LogService.LoggingService;
        #endregion
    }
}
