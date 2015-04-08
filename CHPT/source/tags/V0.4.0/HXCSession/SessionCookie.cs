using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HXCSession
{
    public class SessionCookie
    {
        public static string generatePCClientCookieStr(string userID)
        {
            string cookieStr = string.Empty;
            cookieStr =  HXCCommon.DotNetEncrypt.Md5Helper.MD5(userID+System.DateTime.UtcNow.Ticks.ToString()+"HXC",32);
            HXCSession.PCClientSession.Instance.Add(userID, cookieStr);
            return cookieStr;
        }

        public static void removePCClientCookieStr(string userID)
        {
            string cookieStr = string.Empty;
            HXCSession.PCClientSession.Instance.Remove(userID, out cookieStr);
        }

        public static bool getPCClientCookieStr(string userID, out string cookieStr)
        {
            return HXCSession.PCClientSession.Instance.Get(userID, out cookieStr);
        }
        public static bool CheckPCClientCookieStr(string userID, string cookieStr)
        {
            string str = string.Empty;
            HXCSession.PCClientSession.Instance.Get(userID, out str);
            return string.Equals(cookieStr, str);
        }
    }
}