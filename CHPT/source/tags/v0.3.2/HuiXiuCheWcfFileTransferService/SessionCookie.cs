using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HuiXiuCheWcfFileTransferService
{
    public class SessionCookie
    {
        //public static string generatePCClientCookieStr(string userID)
        //{
        //    string cookieStr = string.Empty;
        //    cookieStr =  HXCCommon.DotNetEncrypt.Md5Helper.MD5(userID+System.DateTime.UtcNow.Ticks.ToString()+"HXC",32);
        //    HXCSession.PCClientSession.Instance.Add(userID, cookieStr);
        //    return cookieStr;
        //}

        //public static void removePCClientCookieStr(string userID)
        //{
        //    string cookieStr = string.Empty;
        //    HXCSession.PCClientSession.Instance.Remove(userID, out cookieStr);
        //}

        //public static bool getPCClientCookieStr(string userID, out string cookieStr)
        //{
        //    return HXCSession.PCClientSession.Instance.Get(userID, out cookieStr);
        //}
        //public static bool CheckPCClientCookieStr(string userID, string cookieStr)
        //{
        //    string str = string.Empty;
        //    HXCSession.PCClientSession.Instance.Get(userID, out str);
        //    return string.Equals(cookieStr, str);
        //}

        public static string generatePCClientCookieStr(string userID)
        {
            if (WCFClientProxy.TestPCClientProxy())
            {
                return GlobalStaticObj.sessionProxy.generatePCClientCookieStr(userID);
            }
            return "未能建立同服务器连接！";

        }
        public static void removePCClientCookieStr(string userID)
        {
            if (WCFClientProxy.TestPCClientProxy())
            {
                GlobalStaticObj.sessionProxy.removePCClientCookieStr(userID);
            }
        }
        public static bool getPCClientCookieStr(string userID, out string cookieStr)
        {
            if (WCFClientProxy.TestPCClientProxy())
            {
                return GlobalStaticObj.sessionProxy.getPCClientCookieStr(userID, out cookieStr);
            }
            cookieStr = string.Empty;
            return false;
        }
        public static bool CheckPCClientCookieStr(string userID, string cookieStr)
        {
            if (WCFClientProxy.TestPCClientProxy())
            {
                return GlobalStaticObj.sessionProxy.CheckPCClientCookieStr(userID, cookieStr);
            }
            return false;
        }
    }
}