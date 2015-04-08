using CloudPlatSocket.Protocol;
using HXC_FuncUtility;
using SYSModel;
using Utility.Log;

namespace CloudPlatSocket.Handler
{
    /// <summary>
    /// 登录类
    /// 创建日期：2014.11.10
    /// 修改日期：2014.11.12
    /// </summary>
    public class LoginHandler
    {
        #region --成员变量
        /// <summary>
        /// 子消息ID
        /// </summary>
        public static string SubMessageId = "L1";       
        #endregion

        #region --私有方法
        /// <summary>
        /// 获取登录协议
        /// </summary>
        /// <returns></returns>
        private static LoginProtocol GetLoginProtocol()
        {
            LoginProtocol protocol = new LoginProtocol();
            protocol.StationId = GlobalStaticObj_Server.Instance.StationID;                 
            protocol.SubMessageId = SubMessageId;
            protocol.TimeSpan = TimeHelper.GetTimeInMillis();
            protocol.UserId = GlobalStaticObj_Server.Instance.Cloud_UserId;
            protocol.Password = GlobalStaticObj_Server.Instance.Cloud_Password;
            protocol.PermissionCode = GlobalStaticObj_Server.Instance.LicenseCode;
            return protocol;
        }        
        #endregion

        #region --公用方法
        /// <summary> 登录测试
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string LoginTest(string stationId, string userId, string pwd, string code)
        {
            LoginProtocol protocol = GetLoginProtocol();
            protocol.StationId = stationId;
            protocol.UserId = userId;
            protocol.Password = pwd;
            protocol.PermissionCode = code;

            MessageProtocol mp = ServiceAgent.SendAndReceiveMessage(protocol);
            if (mp != null && mp.GetRealProtocol() is ResultProtocol)
            {
                ResultProtocol result = mp.GetRealProtocol() as ResultProtocol;
                if (result.Result == DataSources.EnumResultType.Success.ToString("d"))
                {
                    return string.Empty;
                }
                else
                {
                    string msg = "登录消息(数据端口)：" + DataSources.GetDescription(typeof(DataSources.EnumResultType), result.Result);
                    //写入日志
                    Log.writeCloudLog(msg);
                    return DataSources.GetDescription(typeof(DataSources.EnumResultType), result.Result);
                }
            }
            return "返回协议格式不正确";
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public static bool Login()
        {
            LoginProtocol protocol = GetLoginProtocol();
            MessageProtocol mp = ServiceAgent.SendAndReceiveMessage(protocol);
            if (mp != null && mp.GetRealProtocol() is ResultProtocol)
            {
                ResultProtocol result = mp.GetRealProtocol() as ResultProtocol;
                if (result.Result == DataSources.EnumResultType.Success.ToString("d"))
                {
                    //写入日志
                    Log.writeCloudLog("云平台数据通讯-登录成功！");
                    return true;
                }
                else
                {
                    string msg = "登录消息(数据端口)：" + DataSources.GetDescription(typeof(DataSources.EnumResultType), result.Result);
                    //写入日志
                    Log.writeCloudLog(msg);
                }
            }
            return false;
        }
        /// <summary>
        /// 附件登录
        /// </summary>
        /// <returns></returns>
        public static bool FileLogin()
        {
            LoginProtocol protocol = GetLoginProtocol();
            MessageProtocol mp = FileAgent.SendAndReceiveMessage(protocol);
            if (mp != null && mp.GetRealProtocol() is ResultProtocol)
            {
                ResultProtocol result = mp.GetRealProtocol() as ResultProtocol;
                if (result.Result == DataSources.EnumResultType.Success.ToString("d"))
                {
                    Log.writeCloudLog("云平台文件通讯-登录成功！");
                    return true;
                }
                else
                {
                    string msg = "登录消息(附件端口)：" + DataSources.GetDescription(typeof(DataSources.EnumResultType), result.Result);
                    //写入日志
                    Log.writeLog(msg);
                }
            }
            return false;
        }
        #endregion
    }
}
