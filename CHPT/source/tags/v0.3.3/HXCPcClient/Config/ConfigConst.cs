using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HXCPcClient
{
    /// <summary>
    /// Creator:yangtianshuai
    /// CreateTime:2014.10.31
    /// Function:save the config key
    /// </summary>
    public class ConfigConst
    {       
        /// <summary>
        /// Wcf数据传输配置
        /// </summary>
        public static string WcfData = "HuiXiuCheWcfService";
        /// <summary>
        /// Wcf文件传输配置(文件流传输模式传输)
        /// </summary>
        public static string WcfFile = "HuiXiuCheWcfFileService";  
        ///// <summary>
        ///// Wcf文件传输配置(大文件分块传输)
        ///// </summary>
        //public static string WcfFileTransfer = "HuiXiuCheWcfFileTransferService";  
    }
}
