using System;
using System.Management;
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

        /// <summary> 获取mac地址
        /// </summary>
        /// <returns>返回mac地址</returns>
        public static string GetMacAddress()
        {
            try
            {
                //获取网卡硬件地址 
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "";
            }
        }

        /// <summary> 获取电脑名称
        /// </summary>
        /// <returns>返回计算机名</returns>
        public static string GetComputerName()
        {
            try
            {
                return System.Environment.GetEnvironmentVariable("ComputerName");
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }
    }
}
