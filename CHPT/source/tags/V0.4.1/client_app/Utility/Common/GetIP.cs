using System;
using System.Web;
namespace Utility.Common
{
    public static class GetIP
    {
        public static string GetClientIp(HttpContext context)
       {
           //可以透过代理服务器
           string userIP = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
           if (userIP == null || userIP == "")
           {
               //没有代理服务器,如果有代理服务器获取的是代理服务器的IP
               userIP = context.Request.ServerVariables["REMOTE_ADDR"];
           }
           if (null == userIP || userIP == String.Empty)
           {
               userIP = context.Request.UserHostAddress;
           }
           return userIP;
       }
    }
}
