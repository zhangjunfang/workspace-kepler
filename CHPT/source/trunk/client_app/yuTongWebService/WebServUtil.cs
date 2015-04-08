using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HXC_FuncUtility;
using BLL;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Reflection;
using Utility.Security;
using SYSModel;
using Utility.Common;

namespace yuTongWebService
{
    public class WebServUtil
    {
        #region 字典码表转换
        /// <summary> 根据本地字典ID获取宇通编码
        /// 本地存储的是字典id，获取到编码后去除字段名称_转成宇通编码
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="value">本地字典id</param>
        /// <returns>返回宇通编码</returns>
        public static string GetYTDicCode(string fieldName, string value)
        {
            string ytcode = DBHelper.GetSingleValue("获取字典编码", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "sys_dictionaries", "dic_code", "dic_id='" + value + "'", "");
            ytcode = ytcode.Replace(fieldName + "_", "");
            return ytcode;
        }

        /// <summary> 根据宇通编码获取本地字典ID
        /// 本地存储的是字典id，字段名称+"_"+宇通编码转成本地字典编码
        /// 根据本地字典编码去字典表查找字典id
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="value">宇通编码</param>
        /// <returns>返回本地字典ID</returns>
        public static string GetLocalDicID(string fieldName, string value)
        {
            string dicid = DBHelper.GetSingleValue("获取字典id", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "sys_dictionaries", "dic_id", "dic_code='" + fieldName + "_" + value + "'", "");
            return dicid;
        }
        /// <summary>根据宇通编码获取名字
        /// </summary>
        /// <param name="code">宇通编码</param>
        /// <returns></returns>
        public static string GetYTDicName(string code)
        {
            string ytName = DBHelper.GetSingleValue("获取字典编码", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "sys_dictionaries", "dic_name", "dic_code='" + code + "'", "");
            return ytName;
        }
        #endregion
        #region DB加密解密转换
        /// <summary> 获取加密后的值
        /// </summary>
        /// <param name="value">需要加密的值</param>
        /// <returns>返回加密后的值</returns>
        public static string GetEncFieldValue(string value)
        {
            string strSql = string.Format("select EncryptByPassPhrase(N'{0}',N'{1}') ", GlobalStaticObj_Server.Instance.KeySecurity_Server, value);
            return DBHelper.GetSingleValue("加密", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, strSql);
        }

        public static string GetEncField(string value)
        {
            return string.Format(" EncryptByPassPhrase(N'{0}',N'{1}') ", GlobalStaticObj_Server.Instance.KeySecurity_Server, value);
        }

        public static string GetEncFieldByField(string value)
        {
            return string.Format(" EncryptByPassPhrase(N'{0}',{1}) ", GlobalStaticObj_Server.Instance.KeySecurity_Server, value);
        }

        /// <summary> 获取解密后的值
        /// </summary>
        /// <param name="value">需要解密的值</param>
        /// <returns>返回加密后的值</returns>
        public static string GetDesFieldValue(string value)
        {
            string strSql = string.Format("select CONVERT(NVARCHAR(50),DECRYPTBYPASSPHRASE(N'{0}',{1})) ", GlobalStaticObj_Server.Instance.KeySecurity_Server, value);
            return DBHelper.GetSingleValue("解密", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, strSql);
        }

        public static string GetDesField(string value)
        {
            return string.Format(" CONVERT(NVARCHAR(50),DECRYPTBYPASSPHRASE(N'{0}',{1})) ", GlobalStaticObj_Server.Instance.KeySecurity_Server, value);
        }
        #endregion

        /// <summary> 证书验证
        /// </summary>
        /// <returns></returns>
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        #region 加密解密
        /// <summary> 实体集合解密（空值不解密）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">实体集合</param>
        /// <returns>返回解密后的实体集合</returns>
        public static T[] DesList<T>(T[] list)
        {
            if (list == null)
            {
                return null;
            }
            List<T> rtnList = new List<T>();
            foreach (T t in list)
            {
                T model = DesModel<T>(t);
                rtnList.Add(model);
            }
            return rtnList.ToArray<T>();
        }

        /// <summary> 实体解密（空值不解密）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="t">实体</param>
        /// <returns>返回解密后的实体</returns>
        public static T DesModel<T>(T t)
        {
            T model = t;
            foreach (PropertyInfo item in t.GetType().GetProperties())
            {
                if (item.PropertyType.FullName == "System.String")
                {
                    object obj = item.GetValue(t, new object[] { });
                    if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                    {
                        string value = obj.ToString();
                        value = Secret.Decrypt3DES_UTF8(value, GlobalStaticObj_YT.KeySecurity_YT);
                        item.SetValue(model, value, null);
                    }
                }
            }
            return model;
        }

        /// <summary> 实体集合加密(空值不加密)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">实体集合</param>
        /// <returns>返回加密后的实体集合</returns>
        public static T[] EncList<T>(T[] list)
        {
            if (list == null)
            {
                return null;
            }
            List<T> rtnList = new List<T>();
            foreach (T t in list)
            {
                T model = EncModel<T>(t);
                rtnList.Add(model);
            }
            return rtnList.ToArray<T>();
        }

        /// <summary> 实体加密(空值不加密)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="t">返回加密后的实体</param>
        /// <returns></returns>
        public static T EncModel<T>(T t)
        {
            T model = t;
            foreach (PropertyInfo item in t.GetType().GetProperties())
            {
                if (item.PropertyType.FullName == "System.String")
                {
                    object obj = item.GetValue(t, new object[] { });
                    if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                    {
                        string value = obj.ToString();
                        value = Secret.Encrypt3DES_UTF8(value, GlobalStaticObj_YT.KeySecurity_YT);
                        item.SetValue(model, value, null);
                    }
                }
                else if (item.PropertyType == typeof(byte[]))
                {
                    //object obj = item.GetValue(t, new object[] { });
                    //if (obj != null)
                    //{
                    //    byte[] objValue = (byte[])obj;
                    //    string value
                    //    objValue = Secret.Encrypt3DES(objValue, GlobalStaticObj_YT.KeySecurity_YT, Encoding.UTF8);
                    //    item.SetValue(model, objValue, null);
                    //}
                }
            }
            return model;
        }
        #endregion

        /// <summary> 接口调用记录
        /// </summary>
        /// <param name="enumInterfaceType">接口类型</param>
        /// <param name="enumExternalSys">外部系统</param>
        /// <param name="totalCount">同步后本地总条数</param>
        /// <param name="updateCount">更新条数</param
        /// <param name="dtLastSyncTime">同步时间</param>
        /// <returns>返回true or false</returns>
        public static bool WriteInterficeSync(DataSources.EnumInterfaceType enumInterfaceType, DataSources.EnumExternalSys enumExternalSys, int totalCount, int updateCount, DateTime dtLastSyncTime)
        {
            string enumName = DataSources.GetDescription(enumInterfaceType, true);
            string keyName = "data_sync_id";
            string keyValue = Convert.ToInt32(enumExternalSys).ToString() + "-" + Convert.ToInt32(enumInterfaceType).ToString();
            Dictionary<string, string> dicFields = new Dictionary<string, string>();
            dicFields.Add("local_total_num", totalCount.ToString());
            dicFields.Add("update_total_num", updateCount.ToString());
            dicFields.Add("last_sync_time", Common.LocalDateTimeToUtcLong(dtLastSyncTime).ToString());
            dicFields.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            bool flag = DBHelper.Submit_AddOrEdit("接口调用记录-" + enumName, GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_data_sync", keyName, keyValue, dicFields);
            return flag;
        }

        /// <summary> 接口调用日志
        /// </summary>
        /// <param name="enumInterfaceType">接口类型</param>
        /// <param name="enumExternalSys">外部系统</param>
        /// <param name="enumSyncDirection">同步方向</param>
        /// <param name="tableName">更新表名</param>
        /// <param name="dtpStart">开始时间</param>
        /// <param name="dtpEnd">结束时间</param>
        /// <param name="updateCount">更新条数</param>  
        /// <param name="errMsg">错误原因</param> 
        /// <returns>返回true or false</returns>
        public static bool WriteInterficeSyncLog(DataSources.EnumInterfaceType enumInterfaceType, DataSources.EnumExternalSys enumExternalSys, DataSources.EnumSyncDirection enumSyncDirection, string tableName, DateTime dtpStart, DateTime dtpEnd, int updateCount, string errMsg)
        {
            Dictionary<string, string> dicFields = new Dictionary<string, string>();
            dicFields.Add("data_sync_log_id", Guid.NewGuid().ToString());
            dicFields.Add("external_sys", Convert.ToInt32(enumExternalSys).ToString());
            string enumName = DataSources.GetDescription(enumInterfaceType, true);
            dicFields.Add("business_name", Convert.ToInt32(enumInterfaceType).ToString());
            dicFields.Add("table_name", tableName);
            dicFields.Add("sync_start_time", Common.LocalDateTimeToUtcLong(dtpStart).ToString());
            dicFields.Add("sync_end_time", Common.LocalDateTimeToUtcLong(dtpEnd).ToString());
            dicFields.Add("sync_direction", Convert.ToInt32(enumSyncDirection).ToString());
            dicFields.Add("changes_num", updateCount.ToString());
            dicFields.Add("sync_result", errMsg);
            dicFields.Add("create_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            bool flag = DBHelper.Submit_AddOrEdit("接口调用日志-" + enumName, GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_data_sync_log", "", "", dicFields);
            return flag;
        }
    }
}
