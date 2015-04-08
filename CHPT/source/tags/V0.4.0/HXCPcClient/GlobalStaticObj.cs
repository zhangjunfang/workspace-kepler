using System;
using System.ServiceModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Kord.LogService;
using Utility.Log;

namespace HXCPcClient
{
    public static class GlobalStaticObj
    {
        public static Form AppMainForm { get; set; }
        public static ChannelFactory<HuiXiuCheWcfContract.IHXCWCFService> channelFactory { get; set; }
        public static HuiXiuCheWcfContract.IHXCWCFService proxy { get; set; }
        public static ChannelFactory<HuiXiuCheWcfFileContract.IHXCWCFFileService> channelFactoryFile { get; set; }
        public static HuiXiuCheWcfFileContract.IHXCWCFFileService proxyFile { get; set; }
        /// <summary>
        /// ds包括用户记录UserID、关联的权限菜单列表
        /// </summary>
        public static DataSet gLoginDataSet;
        public static string CookieStr { get; set; }      
        /// <summary> 用户登录id
        /// </summary>
        public static string Login_Id { get; set; }
        /// <summary> 用户id
        /// </summary>
        public static string UserID { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public static string LandName { get; set; }        
        /// <summary> 用户名
        /// </summary>
        public static string UserName { get; set; }
        /// <summary> 密码
        /// </summary>
        public static string PassWord { get; set; }
        /// <summary>
        /// 当前登录用户的公司id
        /// </summary>
        public static string CurrUserCom_Id { get; set; }
        /// <summary>
        /// 当前登录用户的公司code
        /// </summary>
        public static string CurrUserCom_Code { get; set; }       
        /// <summary>
        /// 当前登录用户的公司名称
        /// </summary>
        public static string CurrUserCom_Name { get; set; }
        /// <summary>
        /// 当前用户组织ID
        /// </summary>
        public static string CurrUserOrg_Id { get; set; }
        /// <summary>
        /// 当前用户组织名称
        /// </summary>
        public static string CurrUserOrg_Name { get; set; }
        /// <summary>
        /// 当前登录用户公司站别
        /// </summary>
        public static string CurrUserCom_Category { get; set; }
        /// <summary> 当前服务器时间
        /// </summary>
        public static DateTime CurrentDateTime { get; set; }
        /// <summary>
        /// 服务站代码
        /// </summary>
        public static String ServerStationCode { get; set; }    //add by kord
        /// <summary>
        /// 服务站名称
        /// </summary>
        public static String ServerStationName { get; set; }    //add by kord

        #region 帐套相关
        /// <summary> 通用库代码
        /// </summary>
        public const string CommAccCode = "000";
        /// <summary> 帐套ID
        /// </summary>
        public static string CurrAccID = "HXC";
        /// <summary> 帐套名称
        /// </summary>
        public static string CurrAccName="HXC";
        /// <summary> 帐套代码
        /// </summary>
        public static string CurrAccCode="001";
        /// <summary> 是否默认帐套
        /// </summary>
        public static bool IsDefaultAcc = false;
        #endregion

        #region --通讯相关
        /// <summary> 数据服务端Ip 
        /// </summary>
        public static string DataServerIp = "127.0.0.1";
        /// <summary> 数据服务端口 
        /// </summary>
        public static int DataPort = 10000;
        /// <summary> 文件服务Ip 
        /// </summary>
        public static string FileServerIp = "127.0.0.1";
        /// <summary> 文件服务端口 
        /// </summary>
        public static int FilePort = 10001;
        #endregion

        #region datagridview相关
        /// <summary> 单元格背景色
        /// </summary>
        public static readonly Color RowBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
        /// <summary> 单元格被选定时背景色
        /// </summary>
        public static readonly Color RowSelectBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));


        /// <summary> 可编辑单元格背景色
        /// </summary>
        public static readonly Color RowEditBgColor = Color.FromArgb(20, 129, 194);
        /// <summary> 可编辑单元格被选定时背景色
        /// </summary>
        public static readonly Color RowEditSelectBgColor = Color.FromArgb(20, 129, 194);
        #endregion

        #region 应用程序全局日志服务 add by kord
        /// <summary>
        /// 应用程序全局日志
        /// </summary>
        public static LoggingService GlobalLogService = Log.CreateLogService("System", "System");
        #endregion
    }
}
