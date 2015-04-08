using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Net;

namespace Utility.Tools
{
   public class HardwareSerialNumber
    {
        /// <summary>
        /// 获取本地IP
        /// </summary>
        /// <returns></returns>
        public static string Get_UserIP()
        {
            string ip = "";
            string strHostName = Dns.GetHostName(); //得到本机的主机名
            IPHostEntry ipEntry = Dns.GetHostByName(strHostName); //取得本机IP
            if (ipEntry.AddressList.Length > 0)
            {
                ip = ipEntry.AddressList[0].ToString();
            }
            return ip;
            //string userip = "";
            //string name = Dns.GetHostName();
            //IPAddress[] ips = Dns.GetHostAddresses(name);
            //foreach (IPAddress ip in ips)
            //    userip += ip.ToString() + "|";//所有IP
            //return userip;
        }
        /// <summary>
        /// 获取主机域名
        /// </summary>
        /// <returns></returns>
        public static string Get_HostName()
        {
            return Dns.GetHostName();
        }

        /// <summary> 生成机器码
        /// </summary>
       public static string GetMachineCode()
        {
            string regNo = "";
            regNo = "[CPUBH:" + GetCpu() + "]" + "[ZBBH:" + GetboardID() + "]" + "[YPBH:" + GetHardDiskID() + "]";
            string code = string.Empty;
            code = Security.Secret.MD5(regNo + "HXC", Encoding.UTF8);
            return code;
        }
        /// <summary> 获得CPU的序列号
        /// </summary>
       public static string GetCpu()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuConnection)
            {
                strCpu = myObject.Properties["Processorid"].Value.ToString();
                break;
            }
            return strCpu;
        }
        /// <summary> 硬盘编号
        /// </summary>
       public static string GetHardDiskID()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * FROM Win32_PhysicalMedia");
                string strHardDiskID = null;
                foreach (ManagementObject mo in searcher.Get())
                {
                    strHardDiskID = mo["SerialNumber"].ToString().Trim();
                    if (strHardDiskID != "")
                    {
                        break;
                    }
                }
                return strHardDiskID;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>主板编号
        /// </summary>
       public static string GetboardID()
        {
            string strbNumber = string.Empty;
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_baseboard");
            foreach (ManagementObject mo in mos.Get())
            {
                strbNumber = mo["SerialNumber"].ToString();
                break;
            }
            return strbNumber;
        }
    }
}
