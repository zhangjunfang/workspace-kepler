using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using System.Management;

namespace HXCPcClient
{
    /// <summary>
    /// 本地变量初始化
    /// </summary>
    public class LocalVariable
    {
        /// <summary>
        /// 初始化完成事件
        /// </summary>
        public delegate void InitComplate();
        /// <summary>
        /// 初始化完成
        /// </summary>
        public InitComplate InitComplated;

        #region --初始化
        public static void Init()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(InitParas));
        }
        #endregion

        /// <summary> 获取计算机Mac地址 
        /// </summary>
        /// <returns></returns>
        public static string GetComputerMac()
        {
            string mac = string.Empty;
            ManagementClass mc;
            mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["IPEnabled"].ToString() == "True")
                    mac = mo["MacAddress"].ToString();
            }
            return mac;
        }

        /// <summary>
        /// 初始化本地数据
        /// </summary>
        private static void InitParas(object state)
        {
            //初始化数据库连接配置
            Hashtable htParas = ConfigManager.GetLocalParas();

            //初始化通讯配置
            if (htParas.ContainsKey(ConfigConst.WcfData))
            {
                string[] arrays = htParas[ConfigConst.WcfData].ToString().Split(':');
                GlobalStaticObj.DataServerIp = arrays[0];
                if (arrays.Length > 1)
                {
                    if (!int.TryParse(arrays[1], out GlobalStaticObj.DataPort))
                    {
                        //Log
                    }
                }
            }

            if (htParas.ContainsKey(ConfigConst.WcfFile))
            {
                string[] arrays = htParas[ConfigConst.WcfFile].ToString().Split(':');
                GlobalStaticObj.FileServerIp = arrays[0];
                if (arrays.Length > 1)
                {
                    if (!int.TryParse(arrays[1], out GlobalStaticObj.FilePort))
                    {
                        //Log
                    }
                }
            }         
        }       
    }
}
