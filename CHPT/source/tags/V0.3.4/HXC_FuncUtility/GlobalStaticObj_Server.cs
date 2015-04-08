using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
namespace HXC_FuncUtility
{
    /// <summary>
    /// 切记：引用完类组件后，一定要调用此方法来释放！
    /// </summary>
    public class HXC_FuncUtilityDispose
    {
        /// <summary>
        /// 切记：引用完类组件后，一定要调用此方法来释放！
        /// </summary>
        public static void Dispose()
        {
            GlobalStaticObj_Server.Instance.Dispose();
        }
    }
    /// <summary> 服务端全局变量
    /// Create By syn
    /// Create Time 2014-10-11 
    /// </summary>
    public class GlobalStaticObj_Server
    {
        #region 用户基础信息
        /// <summary> 用户id
        /// </summary>
        public string UserID = "111";

        /// <summary> 登录名
        /// </summary>
        public string LoginName = "";

        /// <summary> 密码
        /// </summary>
        public string PassWord = "";

        /// <summary> 用户名
        /// </summary>
        public string UserName = "111";

        /// <summary> 角色id
        /// </summary>
        public string RoleID = "";

        /// <summary> 角色名
        /// </summary>
        public string RoleName = "";

        /// <summary> 所属组织id
        /// </summary>
        public string OrgID = "";

        /// <summary> 所属组织
        /// </summary>
        public string OrgName = "";

        /// <summary> 所属公司id
        /// </summary>
        public string ComID = "";

        /// <summary> 所属公司
        /// </summary>
        public string ComName = "";
        #endregion

        #region 登录信息
        /// <summary> 登录时间
        /// </summary>
        public string LoginTime = "";

        /// <summary> 登录IP
        /// </summary>
        public string LoginIP = "";
        #endregion

        #region --通讯相关的本地变量
        /// <summary>
        /// 云平台Socket连接状况
        /// </summary>
        public bool ServerLink = false;
        /// <summary>
        /// 云平台附件Socket连接状况
        /// </summary>
        public bool FileServerLink = false;
        /// <summary>
        /// 服务器IP
        /// </summary>
        public string ServerIp = "127.0.0.1";
        /// <summary>
        /// 服务器端口
        /// </summary>
        public int ServerPort = 10000;
        /// <summary>
        /// 文件服务器IP
        /// </summary>
        public string FileIp = string.Empty;
        /// <summary>
        /// 文件传输端口
        /// </summary>
        public int FilePort = 10002;
        /// <summary>
        /// 文件传输保存路径
        /// </summary>
        public string FilePath = string.Empty;

        /// <summary>
        /// 云平台数据服务器IP
        /// </summary>
        public string CloundServerIp = "119.57.151.34";
        /// <summary>
        /// 云平台数据服务器端口
        /// </summary>
        public int CloundServerPort = 19000;
        /// <summary>
        /// 云平台文件服务器IP
        /// </summary>
        public string CloundFileIp = "119.57.151.34";
        /// <summary>
        /// 云平台文件传输端口
        /// </summary>
        public int CloundFilePort = 19999;
        #endregion

        #region --安全相关
        //鉴权码
        public string LicenseCode = string.Empty;
        public string Cloud_UserId = string.Empty;
        public string Cloud_Password = string.Empty;
        #endregion

        #region 帐套相关
        /// <summary> 备份任务
        /// code 如为空，则为所有账套
        /// int[] 0-自动备份类型，1-自动备份周期，2-自动备份开始时间，3-上次备份时间（如为-1，则未备份）
        /// </summary>
        public Dictionary<string, string[]> DicBackupPlan;

        /// <summary> 数据库实例名前缀
        /// </summary>
        public const string DbPrefix = "HXC_";//HXC_
        /// <summary> 通用库代码
        /// </summary>
        public const string CommAccCode = "000";//
        /// <summary>  主帐套代码
        /// </summary>
        public string MainAccCode = "001";//
        /// <summary> 当前使用数据库实例名
        /// </summary>
        public string CurrAccDbName = "001";
        /// <summary> 服务端数据库存放路径(带\)
        /// </summary>
        public string DbServerInstallDir = @"D:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\";

        /// <summary> 服务端模板数据库存放路径(带\)
        /// </summary>
        public string DbServerBackDir = @"D:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\";

        /// <summary> 服务端模板数据库备份文件名称(不带扩展名)
        /// </summary>
        public string DbTemplateBakFileName = "hxc";
        #endregion

        #region --数据库连接
        /// <summary> 数据库IP
        /// </summary>
        public string DbPcIP = string.Empty;

        /// <summary> 管理权限数据库连接
        /// </summary>
        public string ManagerConnString = string.Empty;
        /// <summary> 只读权限的数据库连接
        /// </summary>
        public string ReadOnlyConnString = string.Empty;
        /// <summary> 可写权限的数据库连接
        /// </summary>
        public string CanWriteConnString = string.Empty;
        #endregion

        #region windows服务名称
        /// <summary> 数据服务名
        /// </summary>
        public const string DataServiceName = "HXCDataServiceWinService";//HXCDataService
        /// <summary> 文件服务名
        /// </summary>
        public const string FileServiceName = "HXCFileServiceWinService";//HXCFileService
        #endregion

        #region --时间参数
        /// <summary> 当前时间
        /// </summary>
        public DateTime CurrentDateTime = DateTime.Now;
        /// <summary> 云平台最近一次数据上传时间
        /// Ticks字符串
        /// </summary>
        public string LastUploadTime = DateTime.Now.Ticks.ToString();
        /// <summary> 云平台最近一次附件上传时间
        /// Ticks字符串
        /// </summary>
        public string FileLastUploadTime = DateTime.Now.Ticks.ToString();
        #endregion

        #region 软件注册
        public string SoftRegUrl = "http://192.168.2.133:8080/SspApp/operation/auth/";//35.111
        #endregion

        #region 服务站信息
        /// <summary> 服务站ID
        /// </summary>
        public string StationID = string.Empty;

        /// <summary> 软件注册秘钥
        /// </summary>
        public const string RegSecret = "hxc123456!@#$%^";

        /// <summary> 接入码
        /// </summary>
        public string ClientID = "PC00000001";

        /// <summary> 服务站所在省份代码
        /// </summary>
        public string ServiceStationProvince = "450000";

        /// <summary> 字段加密秘钥
        /// </summary>
        public string KeySecurity_Server = "80EE5DAD-5EDB-4A96-A1CD-9FD8D6BC0719";
        #endregion

        private static GlobalStaticObj_Server _instance = null;
        // Creates an syn object.
        private static readonly object SynObject = new object();
        GlobalStaticObj_Server()
        {
        }
        public static GlobalStaticObj_Server Instance
        {
            get
            {
                // Double-Checked Locking
                if (null == _instance)
                {
                    lock (SynObject)
                    {
                        if (null == _instance)
                        {
                            _instance = new GlobalStaticObj_Server();
                            _instance.DbServerBackDir = ConfigurationManager.AppSettings["DbServerBackDir"];
                            _instance.DbServerInstallDir = ConfigurationManager.AppSettings["DbServerInstallDir"];
                            _instance.DbTemplateBakFileName = ConfigurationManager.AppSettings["DbTemplateBakFileName"];
                            _instance.SoftRegUrl = ConfigurationManager.AppSettings["SoftRegUrl"];
                            _instance.DicBackupPlan = new Dictionary<string, string[]>();
                        }
                    }
                }
                return _instance;
            }
        }



        private static bool _alreadyDisposed = false;
        ~GlobalStaticObj_Server()
        {
            Dispose(true);
        }
        /// <summary> 切记：引用完类组件后，一定要调用此方法来释放！
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary> 清理所有正在使用的资源。
        /// </summary>
        /// <param name="isDisposing">如果应释放托管资源，为 true；否则为 false</param>
        protected void Dispose(bool isDisposing)
        {
            if (_alreadyDisposed)
                return;
            if (isDisposing)
            {
                _instance = null;
                //GC.SuppressFinalize(this);
            }
            _alreadyDisposed = true;
        }

    }
}