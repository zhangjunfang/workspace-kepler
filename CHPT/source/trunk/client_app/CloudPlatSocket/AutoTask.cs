using System;
using System.Configuration;
using System.Globalization;
using System.Windows.Forms;
using BLL;
using CloudPlatSocket.Protocol;
using SYSModel;
using System.Threading;
using CloudPlatSocket.Handler;
using HXC_FuncUtility;
using System.Collections.Generic;
using System.Data;
using Utility.Log;

namespace CloudPlatSocket
{
    /// <summary>
    /// 描述：自动任务类
    /// 功能：自动同步任务
    /// 创建者：杨天帅
    /// 创建时间：2014.11.11
    /// 修改日期：2014.11.13
    /// </summary>
    public class AutoTask
    {
        #region --成员变量
        private static object mylock = new object();
        /// <summary> 线程状态
        /// </summary>
        private static DataSources.EnumTaskStatus status;
        /// <summary> 主任务线程
        /// </summary>
        private static Thread thread;
        /// <summary> 附件主任务线程
        /// </summary>
        private static Thread fileThread;
        /// <summary> 心跳包线程
        /// </summary>
        private static Thread threadHeartBeat;
        /// <summary> 心跳包间隔
        /// </summary>
        private static int waitSecond = 60;
        /// <summary> 帐套集合
        /// </summary>
        private static List<string> dbList = new List<string>();
        /// <summary> 帐套集合
        /// </summary>
        private static List<string> dbListFile = new List<string>();
        /// <summary> 帐套集合
        /// </summary>
        private static List<string> _dbList = new List<string>();
        /// <summary> 登录标志
        /// </summary>
        private static bool LoginFlag = false;
        /// <summary> 附件登录标志
        /// </summary>
        private static bool FileLoginFlag = false;
        #endregion

        #region --成员方法
        /// <summary> 开始任务
        /// </summary>
        public static void Start()
        {
            try
            {
                ////用户登录
                //LoginFlag = LoginCloud();

                //if (LoginFlag)
                //{
                //    ServiceAgent.ReceiveComplated += WriteDataLog;
                //    GetStationPeriodValidityFromCloud();
                //}
                //return;
                if (thread == null)
                {
                    status = DataSources.EnumTaskStatus.Not_Started;
                    thread = new Thread(new ThreadStart(_ExcuteTask));
                    thread.IsBackground = true;
                }
                if (fileThread == null)
                {
                    fileThread = new Thread(new ThreadStart(_ExcuteFileTask));
                    fileThread.IsBackground = true;
                }
                if (threadHeartBeat == null)
                {
                    threadHeartBeat = new Thread(new ThreadStart(_HeartBeatThread));
                    threadHeartBeat.IsBackground = true;
                }
                if (status == DataSources.EnumTaskStatus.Not_Started)
                {
                    //获取帐套信息
                    dbList = GetDbList();
                    dbListFile.Clear();
                    _dbList.Clear();
                    foreach (string db in dbList)
                    {
                        dbListFile.Add(db);
                        _dbList.Add(db);
                    }
                    status = DataSources.EnumTaskStatus.Runing;

                    thread.Start();
                    fileThread.Start();
                    threadHeartBeat.Start();
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.CloudPadLogService.WriteLog("云平台", ex);
            }
        }
        /// <summary> 停止任务
        /// </summary>
        public static void Stop()
        {
            if (status == DataSources.EnumTaskStatus.Runing)
            {
                ServiceAgent.SupendSendThread();
                FileAgent.SupendSendThread();
                if (thread.ThreadState == ThreadState.Running)
                {
                    thread.Suspend();
                }
                if (fileThread.ThreadState == ThreadState.Running)
                {
                    fileThread.Suspend();
                }
                if (threadHeartBeat.ThreadState == ThreadState.Running)
                {
                    threadHeartBeat.Suspend();
                }
                status = DataSources.EnumTaskStatus.Suspend;
            }
        }
        /// <summary>
        /// 继续任务
        /// </summary>
        public static void Continue()
        {
            if (status == DataSources.EnumTaskStatus.Suspend)
            {
                ServiceAgent.ResumeSendThread();
                if (thread.ThreadState == ThreadState.Suspended)
                {
                    thread.Resume();
                }
                if (fileThread.ThreadState == ThreadState.Suspended)
                {
                    fileThread.Resume();
                }
                if (threadHeartBeat.ThreadState == ThreadState.Suspended)
                {
                    threadHeartBeat.Resume();
                }
                status = DataSources.EnumTaskStatus.Runing;
            }
        }
        /// <summary> 初始化登录
        /// </summary>
        public static void InitLogin()
        {
            LoginFlag = false;
        }
        /// <summary> 初始化登录
        /// </summary>
        public static void InitFileLogin()
        {
            FileLoginFlag = false;
        }
        /// <summary> 执行数据自动上传任务
        /// </summary>
        private static void _ExcuteTask()
        {
            try
            {
                //用户登录
                LoginFlag = LoginCloud();

                if (LoginFlag)
                {
                    ServiceAgent.ReceiveComplated += WriteDataLog;
                }

                ServiceAgent.SetParas(GlobalStaticObj_Server.Instance.CloundServerIp, GlobalStaticObj_Server.Instance.CloundServerPort);
                //服务传输服务
                ServiceAgent.StartSendAndReceiveThread();
                var startHour = Convert.ToInt32(ConfigurationManager.AppSettings.Get("SyncStartHour"));
                var endHour = Convert.ToInt32(ConfigurationManager.AppSettings.Get("SyncEndHour"));
                while (true)
                {
                    if (LoginFlag)
                    {
                        var currentTime = DateTime.Now;
                        var time = long.Parse(GlobalStaticObj_Server.Instance.LastUploadTime);
                        if (currentTime.Hour >= startHour && currentTime.Hour < endHour && time < currentTime.Ticks)
                        {
                            foreach (var dbName in dbList)
                            {
                                //if (!dbName.Contains("001")) continue;
                                //上传本地数据
                                UploadDataHandler.UpLoadData(dbName, currentTime.Ticks.ToString(CultureInfo.InvariantCulture));
                                DataHelper.DeleteBkData(dbName, time.ToString(CultureInfo.InvariantCulture));
                            }
                            //备份上传时间
                            GlobalStaticObj_Server.Instance.LastUploadTime = time.ToString(CultureInfo.InvariantCulture);
                            //写入
                            ConfigManager.SaveConfig(ConfigConst.UploadTime,
                                GlobalStaticObj_Server.Instance.LastUploadTime, ConfigConst.ConfigPath);
                            MessageBox.Show("云平台数据上传完成！");
                        }
                        Thread.Sleep(60 * 60 * 1000);
                        dbList = GetDbList();

                    }
                    else
                    {
                        Thread.Sleep(waitSecond * 1000);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.CloudPadLogService.WriteLog("云平台", ex);
            }
        }
        /// <summary> 执行附件自动上传任务
        /// </summary>
        private static void _ExcuteFileTask()
        {
            try
            {
                //用户登录
                FileLoginFlag = LoginCloud_File();

                if (FileLoginFlag)
                {
                    FileAgent.ReceiveComplated += new FileAgent.ReceiveComplate(WriteFileLog);
                }
                FileAgent.SetParas(GlobalStaticObj_Server.Instance.CloundFileIp, GlobalStaticObj_Server.Instance.CloundFilePort);
                //附件传输服务
                FileAgent.StartSendAndReceiveThread();

                while (true)
                {
                    if (FileLoginFlag)
                    {

                        var time = long.Parse(GlobalStaticObj_Server.Instance.FileLastUploadTime);
                        var currentTime = DateTime.Now;
                        if (currentTime.Hour > 22 && currentTime.Hour < 23
                            && time < currentTime.Ticks)
                        {
                            time = currentTime.Ticks;
                            foreach (var dbName in dbListFile)
                            {
                                //上传附件信息
                                FileHandler.UpLoadFile(dbName, time.ToString());

                                //备份上传时间
                                GlobalStaticObj_Server.Instance.FileLastUploadTime = time.ToString();
                                //写入
                                ConfigManager.SaveConfig(ConfigConst.FileUploadTime,
                                    GlobalStaticObj_Server.Instance.FileLastUploadTime, ConfigConst.ConfigPath);
                            }
                        }
                        Thread.Sleep(60 * 60 * 1000);
                        dbListFile = GetDbList();

                    }
                    else
                    {
                        Thread.Sleep(waitSecond * 1000);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.CloudPadLogService.WriteLog("云平台", ex);
            }
        }
        /// <summary> 心跳包\在线客户端线程 
        /// </summary>
        public static void _HeartBeatThread()
        {
            try
            {
                var min = 0;
                while (true)
                {

                    if (LoginFlag)
                    {
                        int minute = DateTime.Now.Minute;
                        if (minute % 5 == 0 && (minute > 4 || minute == 0))
                        {

                            FactoryHandler.HandleLocalFacData();
                            //服务站在线状态
                            ContolHandler.UpLoadServerStatus();
                            DateTime currentTime = GlobalStaticObj_Server.Instance.CurrentDateTime;

                            foreach (string dbName in _dbList)
                            {
                                //在线用户
                                ContolHandler.UpLoadOnline(dbName, GlobalStaticObj_Server.Instance.UserUploadTime.ToString());
                            }

                            //备份上传时间
                            GlobalStaticObj_Server.Instance.LastUploadTime = currentTime.Ticks.ToString();
                            //写入
                            ConfigManager.SaveConfig(ConfigConst.UserUploadTime, GlobalStaticObj_Server.Instance.UserUploadTime, ConfigConst.ConfigPath);

                        }
                        //if (min != minute)
                        //{
                            HeartBeatHandler.SendHeartBeat();
                            min = minute;
                        //}
                    }
                    //心跳包间隔
                    Thread.Sleep(waitSecond * 1000);
                }

            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.CloudPadLogService.WriteLog("云平台", ex);
            }
        }
        /// <summary> 登录云平台
        /// </summary>
        private static bool LoginCloud()
        {
            if (!LoginFlag)
            {
                return LoginHandler.Login();
            }
            return true;
        }
        /// <summary> 登录云平台-附件通讯端口
        /// </summary>
        private static bool LoginCloud_File()
        {
            if (!FileLoginFlag)
            {
                return LoginHandler.FileLogin();
            }
            return true;
        }
        private static void WriteDataLog(string msg)
        {
            Log.writeCloudLog("【数据通讯-接收消息】：" + msg);
        }
        private static void WriteFileLog(string msg)
        {
            Log.writeCloudLog("【文件通讯-接收消息】：" + msg);
        }
        /// <summary> 获取帐套信息 
        /// </summary>
        /// <returns></returns>
        private static List<string> GetDbList()
        {
            lock (mylock)
            {
                List<string> lists = new List<string>();
                string db = GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode;
                //lists.Add(db); //现在账套库已经包含了000通用库了
                DataTable dt = BLL.DBHelper.GetTable("获取帐套信息", db, "sys_setbook", "setbook_code", "", "", "");
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dt.Columns.Contains("setbook_code"))
                        {
                            var dbName = dr["setbook_code"] == null ? "" : dr["setbook_code"].ToString();
                            if (!DataHelper.DbIsExists(GlobalStaticObj_Server.DbPrefix + dbName)) continue;    //判断数据库是否存在 -- add by kord
                            lists.Add(GlobalStaticObj_Server.DbPrefix + dr["setbook_code"].ToString());
                        }
                    }
                }
                if (lists.Count == 0 && db.Length > 0)
                {
                    lists.Add(db);
                }

                return lists;
            }
        }
        /// <summary> 获取帐套集合 
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDatabaseList()
        {
            return dbList;
        }
        /// <summary> 获取当前线程状态 
        /// </summary>
        /// <returns></returns>
        public static DataSources.EnumTaskStatus GetStatus()
        {
            return status;
        }
        /// <summary> 获取是否已经登录 
        /// </summary>
        /// <returns></returns>
        public static bool HasLogin()
        {
            return LoginFlag;
        }
        /// <summary> 登录验证测试 
        /// </summary>
        /// <param name="stationId">服务站ID</param>
        /// <param name="userId">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="code">鉴权码</param>
        /// <returns></returns>
        public static string LoginTest(string stationId, string userId, string pwd, string code)
        {
            if (ServiceAgent.ServiceTest())
            {
                return LoginHandler.LoginTest(stationId, userId, pwd, code);
            }
            else
            {
                return "无法连接云平台";
            }
        }
        public static string GetStationPeriodValidityFromCloud()
        {
            if (LoginFlag)
            {
                var mp = ServiceAgent.SendAndReceiveMessage(ContolHandler.GetC3Protocol());
                if (mp != null && mp.GetRealProtocol() is ResultProtocol)
                {
                    var result = mp.GetRealProtocol() as ResultProtocol;
                    if (result.Result == DataSources.EnumResultType.Success.ToString("d"))
                    {
                        var lastIndex = result.ToString().LastIndexOf("$", StringComparison.Ordinal);
                        var ticks = result.ToString().Substring(lastIndex + 1, result.ToString().Length - 1- lastIndex);
                        return ticks;
                    }
                    else
                    {
                        string msg = "服务站有效期：" +
                                     DataSources.GetDescription(typeof(DataSources.EnumResultType), result.Result);
                        //写入日志
                        Log.writeCloudLog(msg);
                        return DataSources.GetDescription(typeof(DataSources.EnumResultType), result.Result);
                    }
                }
                return "返回协议格式不正确";
            }
            return "无法连接云平台";
        }
        #endregion

        #region --测试部分
        public static void SetLogin()
        {
            LoginFlag = true;
            FileLoginFlag = true;
        }
        public static bool fileFlag = false;
        public static bool annouceFlag = false;
        public static bool uploadFlag = false;
        #endregion
    }
}