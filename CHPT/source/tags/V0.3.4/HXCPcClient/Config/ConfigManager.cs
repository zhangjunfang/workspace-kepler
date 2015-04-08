using System;
using System.Collections;
using System.Xml;
using System.IO;
using System.Data;
using Utility.Xml;
using System.Windows.Forms;

namespace HXCPcClient
{
    /// <summary>
    /// Creator:yangtianshuai
    /// CreateTime:2014.10.29
    /// Function:manage config
    /// UpdateTime:2014.10.29 
    /// </summary>
    public class ConfigManager
    {
        #region --成员变量
        /// <summary>
        /// 配置路径
        /// </summary>       
        public static string ConfigPath = "HXCPcClient.exe.config";
        #endregion

        #region --共有方法
        /// <summary>
        /// 获取本地配置参数
        /// </summary>
        /// <returns>参数配置散列表</returns>
        public static Hashtable GetLocalParas()
        {
            Hashtable ht = new Hashtable();
            XmlDocument xmlDoc = GetXmlDocument(GetRealPath());
            if (xmlDoc != null)
            {
                LoadSettingParasite(ht, xmlDoc);               
                //加载WCF配置，IP和端口
                LoadWcfConfig(ht, xmlDoc);
            }
            return ht;
        }       
        /// <summary>
        /// 保存Wcf配置
        /// </summary>
        /// <param name="name">配置节</param>
        /// <param name="connString">对应值</param>
        /// <returns></returns>
        public static bool SaveWcfConfig(string name, string value)
        {
            string path = GetRealPath();
            XmlDocument xmlDoc = GetXmlDocument(path);
            if (xmlDoc != null)
            {
                return SaveWcfConfig(name, value, xmlDoc, path);
            }
            return false;
        }
        /// <summary>
        /// 保存AppConfig配置
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public static bool SaveConfig(Hashtable ht)
        {
            string path = GetRealPath();
            XmlDocument xmlDoc = GetXmlDocument(path);
            if (xmlDoc != null)
            {
                return SaveConfig(ht, xmlDoc, path);
            }
            return false;
        }
        public static bool SaveConfig(string name, string value)
        {
            Hashtable ht = new Hashtable();
            ht.Add(name, value);
            return SaveConfig(ht);
        }
        #endregion

        #region --私有方法
        /// <summary>
        /// 加载本地连接字符串
        /// </summary>
        /// <param name="ht">配置散列表</param>
        /// <param name="xmlDoc"></param>
        private static void LoadSettingParasite(Hashtable ht, XmlDocument xmlDoc)
        {
            XmlNode connNode = xmlDoc.SelectSingleNode("configuration/appSettings");
            string key = string.Empty;
            foreach (XmlNode node in connNode.ChildNodes)
            {
                if (node.Name == "add")
                {
                    key = node.Attributes["key"].Value;
                    if (!ht.ContainsKey(key))
                    {
                        ht.Add(key, node.Attributes["value"].Value);
                    }
                }
            }     
        }        
        /// <summary>
        /// 加载Wcf配置
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="xmlDoc"></param>
        private static void LoadWcfConfig(Hashtable ht, XmlDocument xmlDoc)
        {
            XmlNodeList wcfNodes = xmlDoc.SelectNodes("configuration/system.serviceModel/client/endpoint");
            foreach (XmlNode node in wcfNodes)
            {
                if (node.Name == "endpoint")
                {
                    string config = node.Attributes["address"].Value;
                    string[] arrays = config.Split('/');
                    if (arrays.Length > 2)
                    {
                        //Wcf服务名，服务IP+端口号
                        ht.Add(node.Attributes["name"].Value, arrays[2]);
                    }
                }
            }
        }
        /// <summary>
        /// 保存Wcf的IP和端口的配置
        /// </summary>
        /// <param name="name">配置节</param>
        /// <param name="value">修改后的值</param>
        /// <param name="xmlDoc">Dom</param>
        /// <param name="path">文件保存路径</param>
        /// <returns></returns>
        private static bool SaveWcfConfig(string name,string value,XmlDocument xmlDoc,string path)
        {
            XmlNode connNode = xmlDoc.SelectSingleNode("configuration/system.serviceModel/client");
            string config = string.Empty;
            bool flag = false;

            foreach (XmlNode node in connNode.ChildNodes)
            {
                if (node.Name == "endpoint")
                {
                    if (node.Attributes["name"].Value == name)
                    {
                        config = node.Attributes["address"].Value;
                        string[] arrays = config.Split('/');
                        if (arrays.Length > 2)
                        {
                            config = string.Empty;
                            for (int i = 0; i < arrays.Length; i++)
                            {
                                if (i == 2)
                                {
                                    config += value;
                                }
                                else
                                {
                                    config += arrays[i];
                                }
                                config += "/";
                            }
                            config = config.Remove(config.Length - 1);
                            node.Attributes["address"].Value = config;
                            flag = true;
                        }                      
                    }
                }
            }
            if (flag)
            {
                xmlDoc.Save(path);              
            }
            return flag;
        }        
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="ht">配置、值散列集合</param>
        /// <param name="xmlDoc">Dom</param>
        /// <param name="path">文件保存路径</param>
        /// <returns></returns>
        private static bool SaveConfig(Hashtable ht, XmlDocument xmlDoc, string path)
        {
            XmlNode connNode = xmlDoc.SelectSingleNode("configuration/appSettings");
            string key = string.Empty;
            bool flag = false;

            foreach (XmlNode node in connNode.ChildNodes)
            {
                if (node.Name == "add")
                {
                    key = node.Attributes["key"].Value;
                    if (ht.ContainsKey(key))
                    {
                        node.Attributes["value"].Value = ht[key].ToString();
                        flag = true;                        
                    }
                }
            }
            if (flag)
            {
                xmlDoc.Save(path);
            }
            return flag;
        }

        /// <summary>
        /// 获取实际路径
        /// </summary>
        /// <returns></returns>
        private static string GetRealPath()
        {
            string path = ConfigPath;          
            path = AppDomain.CurrentDomain.BaseDirectory + path;
            return path;
        }

        /// <summary>
        /// 获取Dom对象
        /// </summary>
        /// <param name="path">xml文件路径</param>
        /// <returns></returns>
        private static XmlDocument GetXmlDocument(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (File.Exists(path))
            {
                try
                {
                    xmlDoc.Load(path);
                }
                catch (Exception ex)
                {
                    return null;
                }
                return xmlDoc;
            }
            return null;
        }
        #endregion

        #region --操作cfg文件
        #region --用户记录
        private static DataSet _user;
        public static DataSet User
        {
            get
            {
                if (_user == null)
                {
                    _user = new XmlDataSetConvert(Application.StartupPath + @"\User.cfg").XMLToDataSet();
                }
                return _user;
            }
        }
        /// <summary>
        /// 写入本地用户
        /// </summary>
        /// <param name="dt"></param>       
        /// <returns></returns>
        public static bool WriteUser(DataSet ds)
        {
            if (ds.Tables.Count == 1)
            {
                ds.Tables[0].TableName = "User";
                return new XmlDataSetConvert(Application.StartupPath + @"\User.cfg").DataSetToXML(ds);
            }
            return false;
        }
        #endregion
        #endregion
    }
}
