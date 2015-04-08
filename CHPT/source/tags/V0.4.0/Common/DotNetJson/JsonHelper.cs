/*----------------------------------------------------------------
// Copyright (C) 2013 郑州华骏技术有限公司
//  文件名：JsonHelper
//  文件功能描述：
//  创建标识：贾晓峰 2013-09-26 10:00:24
//  修改标识：
//  修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using HXCCommon.DotNetData;

namespace HXCCommon.DotNetJson
{
    /// <summary>
    /// 转换Json格式帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 泛型接口转Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string IListToJson<T>(IList<T> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (T t in list)
            {
                sb.Append(ObjectToJson(t) + ",");
            }
            string _temp = sb.ToString().TrimEnd(',');
            _temp += "]";
            return _temp;
        }
        /// <summary>
        /// 泛型接口转Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="ClassName"></param>
        /// <returns>{"rows":[{"c_id":"21bb6911-af52-42a4-9732-24a6e8384411","eav_id":"4a9fe8ca-112a-47c0-b074-229837cfe6e6","e_id":"cfe929e3-accd-4efb-910b-07705077b6d6","ea_id":"","ea_name":"555","eav_value":"555","eav_memo":"","sud":"0"}]}</returns>
        public static string IListToJson<T>(IList<T> list, string ClassName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"" + ClassName + "\":[");
            foreach (T t in list)
            {
                sb.Append(ObjectToJson(t) + ",");
            }
            string _temp = sb.ToString().TrimEnd(',');
            _temp += "]}";
            return _temp;
        }
        /// <summary>
        /// 对象转Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns>{"SM_ID":"71","SM_Img":"title.gif","SM_Memo":"当前位置：会所服务 ─ 宾客管理 ─ 宾客列表"}</returns>
        public static string ObjectToJson<T>(T t)
        {
            StringBuilder sb = new StringBuilder();
            string json = "";
            if (t != null)
            {
                sb.Append("{");
                PropertyInfo[] properties = t.GetType().GetProperties();
                foreach (PropertyInfo pi in properties)
                {
                    sb.Append("\"" + pi.Name.ToString() + "\"");
                    sb.Append(":");
                    sb.Append("\"" + pi.GetValue(t, null) + "\"");
                    sb.Append(",");
                }
                json = sb.ToString().TrimEnd(',');
                json += "}";
            }
            return json;
        }
        /// <summary>
        /// 对象转Json（重载）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="ClassName"></param>
        /// <returns>{"menu":[{"SM_ID":"71","SM_Img":"title.gif","SM_Memo":"当前位置：会所服务 ─ 宾客管理 ─ 宾客列表"}]}</returns>
        public static string ObjectToJson<T>(T t, string ClassName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"" + ClassName + "\":[");
            string json = "";
            if (t != null)
            {
                sb.Append("{");
                PropertyInfo[] properties = t.GetType().GetProperties();
                foreach (PropertyInfo pi in properties)
                {
                    sb.Append("\"" + pi.Name.ToString() + "\"");//.ToLower()
                    sb.Append(":");
                    sb.Append("\"" + pi.GetValue(t, null) + "\"");
                    sb.Append(",");
                }
                json = sb.ToString().TrimEnd(',');
                json += "}]}";
            }
            return json;
        }
        /// <summary>
        /// List转成json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonName"></param>
        /// <param name="IL"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(IList<T> IL, string jsonName)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{\"" + jsonName + "\":[");
            if (IL.Count > 0)
            {
                for (int i = 0; i < IL.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    Type type = obj.GetType();
                    PropertyInfo[] pis = type.GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pis.Length; j++)
                    {
                        Json.Append("\"" + pis[j].Name.ToString() + "\":\"" + pis[j].GetValue(IL[i], null) + "\"");
                        if (j < pis.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < IL.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }

        /// <summary>
        /// 将数组转换为JSON格式的字符串
        /// </summary>
        /// <typeparam name="T">数据类型，如string,int ...</typeparam>
        /// <param name="list">泛型list</param>
        /// <param name="propertyname">JSON的类名</param>
        /// <returns></returns>
        public static string ArrayToJson<T>(List<T> list, string propertyname)
        {
            StringBuilder sb = new StringBuilder();
            if (list.Count > 0)
            {
                sb.Append("[{\"");
                sb.Append(propertyname);
                sb.Append("\":[");
                foreach (T t in list)
                {
                    sb.Append("\"");
                    sb.Append(t.ToString());
                    sb.Append("\",");
                }
                string _temp = sb.ToString();
                _temp = _temp.TrimEnd(',');
                _temp += "]}]";
                return _temp;
            }
            else
                return "";
        }

        /// <summary>
        /// DataTable转Json
        /// </summary>
        /// <param name="dt">table数据集</param>
        /// <param name="dtName">json名</param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt, string dtName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"");
            sb.Append(dtName);
            sb.Append("\":[");
            if (DataTableHelper.IsExistRows(dt))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("{");
                    foreach (DataColumn dc in dr.Table.Columns)
                    {
                        sb.Append("\"");
                        sb.Append(dc.ColumnName);
                        sb.Append("\":\"");
                        if (dr[dc] != null && dr[dc] != DBNull.Value && dr[dc].ToString() != "")
                            sb.Append(dr[dc]).Replace("\\", "/");
                        else
                            sb.Append("&nbsp;");
                        sb.Append("\",");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]}");
            return JsonCharFilter(sb.ToString());
        }

        /// <summary>
        /// 数据行转Json
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <returns></returns>
        public static string DataRowToJson(DataRow dr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (DataColumn dc in dr.Table.Columns)
            {
                sb.Append("\"");
                sb.Append(dc.ColumnName);
                sb.Append("\":\"");
                if (dr[dc] != null && dr[dc] != DBNull.Value && dr[dc].ToString() != "")
                    sb.Append(dr[dc]);
                else
                    sb.Append("&nbsp;");
                sb.Append("\",");
            }
            sb = sb.Remove(0, sb.Length - 1);
            sb.Append("},");
            return sb.ToString();
        }

        /// <summary>
        /// 数组转Json
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static string ArrayToJson(string[] strs)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strs.Length; i++)
            {
                sb.AppendFormat("'{0}':'{1}',", i + 1, strs[i]);
            }
            if (sb.Length > 0)
                return "{" + sb.ToString().TrimEnd(',') + "}";
            return "";
        }

        #region ListToJson
        /// <summary>
        /// list 转换json格式
        /// </summary>
        /// <param name="jsonName">类名</param>
        /// <param name="objlist">list集合</param>
        /// <returns></returns>
        public static string ListToJson<T>(List<T> objlist, string jsonName)
        {
            string result = "{";
            //如果没有给定类的名称， 指定一个
            if (jsonName.Equals(string.Empty))
            {
                object o = objlist[0];
                jsonName = o.GetType().ToString();
            }
            result += "\"" + jsonName + "\":[";
            //处理第一行前面不加","号
            bool firstline = true;
            foreach (object oo in objlist)
            {
                if (!firstline)
                {
                    result = result + "," + ObjectToJson(oo);
                }
                else
                {
                    result = result + ObjectToJson(oo) + "";
                    firstline = false;
                }
            }
            return result + "]}";
        }
        /// <summary>
        /// 单个对象转换json
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns></returns>
        private static string ObjectToJson(object o)
        {
            string result = "{";
            List<string> ls_propertys = new List<string>();
            ls_propertys = GetObjectProperty(o);
            foreach (string str_property in ls_propertys)
            {
                if (result.Equals("{"))
                {
                    result = result + str_property;
                }
                else
                {
                    result = result + "," + str_property + "";
                }
            }
            return result + "}";
        }
        /// <summary>
        /// 获取对象属性
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns></returns>
        private static List<string> GetObjectProperty(object o)
        {
            List<string> propertyslist = new List<string>();
            PropertyInfo[] propertys = o.GetType().GetProperties();
            foreach (PropertyInfo p in propertys)
            {
                propertyslist.Add("\"" + p.Name.ToString() + "\":\"" + p.GetValue(o, null) + "\"");

            }
            return propertyslist;
        }


        #endregion

        public static string HashtableToJson(Hashtable data, string dtName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"");
            sb.Append(dtName);
            sb.Append("\":[{");
            foreach (object key in data.Keys)
            {
                object value = data[key];
                sb.Append("\"");
                sb.Append(key);
                sb.Append("\":\"");
                if (!String.IsNullOrEmpty(value.ToString()) && value != DBNull.Value)
                {
                    sb.Append(value).Replace("\\", "/");
                }
                else
                {
                    sb.Append(" ");
                }
                sb.Append("\",");
            }
            sb = sb.Remove(sb.Length - 1, 1);
            sb.Append("}]}");
            return JsonCharFilter(sb.ToString());
        }
        /// <summary>  
        /// Json特符字符过滤
        /// </summary>  
        /// <param name="sourceStr">要过滤的源字符串</param>  
        /// <returns>返回过滤的字符串</returns>  
        private static string JsonCharFilter(string sourceStr)
        {
            return sourceStr;
        }


        #region ----JSON的序列化和反序列化----
        /// <summary>
        /// 对象序列化为Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ParseToJson<T>(T obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
        /// <summary>
        /// Json序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ParseFromJson<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        #endregion

        #region ----DataTable转化为JSON----
        /// <summary>
        /// DataTable转化为JSON
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc]);
                }
                list.Add(result);
            }
            return ParseToJson(list);
        }
        #endregion
    }
}
