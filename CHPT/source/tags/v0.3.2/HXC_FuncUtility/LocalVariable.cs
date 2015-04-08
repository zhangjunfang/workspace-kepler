using System.Collections;
using System.Threading;
using System.Data;
using Utility.Security;

namespace HXC_FuncUtility
{
    /// <summary>
    /// Creater:yangtianshuai
    /// CreateTime:2014.10.30
    /// Function:Local Globle Variable
    /// UpdateTime:2014.10.30
    /// </summary>
    public class LocalVariable
    {
        #region --成员变量
        /// <summary> 初始化完成 
        /// </summary>
        public delegate void InitComplate();
        public static InitComplate InitComplated;
        /// <summary>
        /// 密匙
        /// </summary>
        public const string Key = "yangtianshuai";
        #endregion

        #region --初始化
        public static void Init()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(InitParas));
        }
        #endregion

        #region --成员方法
        /// <summary>
        /// 初始化本地数据
        /// </summary>
        private static void InitParas(object state)
        {
            //初始化数据库连接配置
            Hashtable htParas = ConfigManager.GetLocalParas();

            #region --通用数据配置
            InitGeneral(htParas);
            #endregion

            //数据库连接配置
            InitConnString(htParas);

            if (InitComplated != null)
            {
                InitComplated();               
            }

            #region --WCF数据通讯
            //初始化通讯配置
            if (htParas.ContainsKey(ConfigConst.WcfData))
            {
                string[] arrays = htParas[ConfigConst.WcfData].ToString().Split(':');
                GlobalStaticObj_Server.Instance.ServerIp = arrays[0];
                if (arrays.Length > 1)
                {
                    if (!int.TryParse(arrays[1], out GlobalStaticObj_Server.Instance.ServerPort))
                    {
                        //Log
                    }
                }
            }
            #endregion

            #region --WCF文件通讯
            if (htParas.ContainsKey(ConfigConst.WcfFile))
            {
                string[] arrays = htParas[ConfigConst.WcfFile].ToString().Split(':');
                GlobalStaticObj_Server.Instance.FileIp = arrays[0];
                if (arrays.Length > 1)
                {
                    if (!int.TryParse(arrays[1], out GlobalStaticObj_Server.Instance.FilePort))
                    {
                        //Log
                    }
                }
            }
            #endregion
        }
        
        /// <summary> 初始化数据库连接
        /// </summary>
        /// <param name="htParas"></param>
        private static void InitConnString(Hashtable htParas)
        {
            if (htParas.ContainsKey(ConfigConst.ConnectionManageString))
            {
                GlobalStaticObj_Server.Instance.ManagerConnString = htParas[ConfigConst.ConnectionManageString].ToString();
                GlobalStaticObj_Server.Instance.ManagerConnString = Secret.Decrypt3DES(GlobalStaticObj_Server.Instance.ManagerConnString, Key);
            }
            if (htParas.ContainsKey(ConfigConst.ConnectionStringReadonly))
            {
                GlobalStaticObj_Server.Instance.ReadOnlyConnString = htParas[ConfigConst.ConnectionStringReadonly].ToString();
                GlobalStaticObj_Server.Instance.ReadOnlyConnString = Secret.Decrypt3DES(GlobalStaticObj_Server.Instance.ReadOnlyConnString, Key);
            }
            if (htParas.ContainsKey(ConfigConst.ConnectionStringWrite))
            {
                GlobalStaticObj_Server.Instance.CanWriteConnString = htParas[ConfigConst.ConnectionStringWrite].ToString();
                GlobalStaticObj_Server.Instance.CanWriteConnString = Secret.Decrypt3DES(GlobalStaticObj_Server.Instance.CanWriteConnString, Key);
            }
        }
        /// <summary> 初始化通用配置 
        /// </summary>
        /// <param name="htParas"></param>
        private static void InitGeneral(Hashtable htParas)
        {
            if (htParas.ContainsKey(ConfigConst.SavePath))
            {
                GlobalStaticObj_Server.Instance.FilePath = htParas[ConfigConst.SavePath].ToString();
            }

            if (htParas.ContainsKey(ConfigConst.UploadTime))
            {
                GlobalStaticObj_Server.Instance.LastUploadTime = htParas[ConfigConst.UploadTime].ToString();
            }

            if (htParas.ContainsKey(ConfigConst.FileUploadTime))
            {
                GlobalStaticObj_Server.Instance.FileLastUploadTime = htParas[ConfigConst.FileUploadTime].ToString();
            }
        }

        /// <summary>
        /// 获取数据连接类别
        /// 默认为管理员权限
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetConnStringType(string type)
        {
            if (type == ConfigConst.ConnectionManageString)
            {
                //设置数据库连接解析内容
                return GlobalStaticObj_Server.Instance.ManagerConnString;
            }
            else if (type == ConfigConst.ConnectionStringReadonly)
            {
                //设置数据库连接解析内容
                return GlobalStaticObj_Server.Instance.ReadOnlyConnString;
            }
            else if (type == ConfigConst.ConnectionStringWrite)
            {
                //设置数据库连接解析内容
                return GlobalStaticObj_Server.Instance.CanWriteConnString;
            }
            return string.Empty;
        }
        /// <summary>
        /// 更新连接语句
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void SetConnStringValue(string type, string value)
        {
            if (type == ConfigConst.ConnectionManageString)
            {
                GlobalStaticObj_Server.Instance.ManagerConnString = value;
            }
            else if (type == ConfigConst.ConnectionStringReadonly)
            {
                GlobalStaticObj_Server.Instance.ReadOnlyConnString = value;
            }
            else if (type == ConfigConst.ConnectionStringWrite)
            {
                GlobalStaticObj_Server.Instance.CanWriteConnString = value;
            }            
        }
        
        #endregion

        #region --获取数据库连接
        /// <summary> 获取数据库连接
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="dbConfig">数据连接配置节</param>
        /// <returns></returns>
        public static string GetConnString(string dbName, string dbConfig)
        {
            string conn = GetConnStringType(dbConfig);
            if (conn.Length == 0)
            {
                Hashtable ht = new Hashtable();
                //加载本地数据库连接
                ConfigManager.LoadConnString(ht);
                //初始化数据
                InitConnString(ht);
                conn = GetConnStringType(dbConfig);
            }
            //dbName = "hxc"; //测试连接hxc
            return conn.Replace("@@@", dbName);
        }
        #endregion
    }
}
