using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HXC_FuncUtility;

namespace yuTongWebService
{
    /// <summary> 宇通类库全局变量
    /// Create By syn
    /// Create Time 2014-10-11 
    /// </summary>
    public class GlobalStaticObj_YT
    {
        static string keySecurity_yt = string.Empty;
        /// <summary> 加密秘钥
        /// </summary>
        public static string KeySecurity_YT
        {
            get
            {
                if (string.IsNullOrEmpty(keySecurity_yt))
                {
                    Load();
                }
                return keySecurity_yt;
            }
            set
            {
                keySecurity_yt = value;
            }
        }

        static string sapCode = string.Empty;
        /// <summary> 宇通SAP代码-服务站编码
        /// </summary>
        public static string SAPCode
        {
            get
            {
                if (string.IsNullOrEmpty(sapCode))
                {
                    Load();
                }
                return sapCode;
            }
            set
            {
                sapCode = value;
            }
        }

        static string clientID = string.Empty;
        /// <summary>
        /// 接入码
        /// </summary>
        public static string ClientID
        {
            get
            {
                if (string.IsNullOrEmpty(clientID))
                {
                    Load();
                }
                return clientID;
            }
            set
            {
                clientID = value;
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        static void Load()
        {
            DataTable dt = BLL.DBHelper.GetTable("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_signing_info", "service_station_sap,access_code", "", "", "");
            if (dt.Rows.Count > 0)
            {
                SAPCode = dt.Rows[0]["service_station_sap"].ToString();
                ClientID = dt.Rows[0]["access_code"].ToString();
            }
            KeySecurity_YT = BLL.DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "select key_value from sys_config where key_name='KeySecurity_YT'");
            GlobalStaticObj_Server.Instance.CurrAccDbName = GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode;//宇通接口单独调用主库
        }
    }
}
