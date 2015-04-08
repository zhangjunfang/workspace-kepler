using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HXC_FuncUtility
{
    /// <summary>
    /// Creator:yangtianshuai
    /// CreateTime:2014.10.31
    /// Function:save the config key
    /// </summary>
    public class ConfigConst
    {
        /// <summary>
        /// 默认配置路径
        /// </summary>
        public static string ConfigPath = "HXCServerWinForm.exe.config";      
        /// <summary>
        /// Wcf数据传输配置
        /// </summary>
        public static string WcfData = "HuiXiuCheWcfService.HXCWCFService";       
        /// <summary>
        /// Wcf文件传输配置
        /// </summary>
        public static string WcfFile = "HuiXiuCheWcfFileTransferService.HXCWCFFileTransferService";
       
        /// <summary>
        /// 管理员数据库连接字符串
        /// </summary>
        public static string ConnectionManageString = "ConnectionManageStringWrite";
        /// <summary>
        /// 数据库连接字符串，具有写入权限
        /// </summary>
        public static string ConnectionStringWrite = "ConnectionStringWrite";
        /// <summary>
        /// 数据库连接字符串，仅具有只读权限
        /// </summary>
        public static string ConnectionStringReadonly = "ConnectionStringReadonly";
        /// <summary>
        /// 
        /// </summary>
        public static string ConStrManageSql = "ConStrManageSql";
        /// <summary>
        /// 文件保存路径
        /// </summary>
        public static string SavePath = "SavePath";
        /// <summary>
        /// 上传最后时间
        /// </summary>
        public static string UploadTime = "UploadTime";
        /// <summary>
        /// 附件上传最后时间
        /// </summary>
        public static string FileUploadTime = "FileUploadTime";
    }
}
