using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace CloudPlatSocket
{
    public class JsonHelper
    {
        /// <summary>
        /// 服务站ID
        /// </summary>
        private const string StationId = "serStationId";
        /// <summary>
        /// 帐套ID
        /// </summary>
        private const string DB = "setBookId";

        /// <summary>
        /// 单条数据传输协议
        /// JSON转化为数据表
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string json)
        {
            return JsonToDataTable(json, string.Empty, false);
        }
        /// <summary>
        /// 单条数据传输协议
        /// JSON转化为数据表
        /// </summary>
        /// <param name="json">Json对象</param>
        /// <param name="tableName">表名</param>
        /// <param name="regFlag">是否转换</param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string json, string tableName, bool regFlag)
        {
            if (!json.Contains("{") || !json.Contains("}"))
            {
                return null;
            }
            json = json.Remove(json.Length - 1, 1).Remove(0, 1);
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(tableName))
            {
                dt.TableName = tableName;
            }
            string[] arrays = json.Split(',');
            int start = 0, end = 0;
            string[] splits;
            List<string> rowList = new List<string>();
            int i = 0;
            if (json.Contains(StationId))
            {
                i++;
            }
            if (json.Contains(DB))
            {
                i++;
            }
            string columnName = string.Empty;
            for (; i < arrays.Length; i++)
            {
                string array = arrays[i];
                splits = array.Split(':');
                if (splits.Length == 2)
                {
                    if (splits[0].Contains('"'))
                    {
                        start = splits[0].IndexOf('"') + 1;
                        end = splits[0].LastIndexOf('"') - start;
                        columnName = splits[0].Substring(start, end);
                        if (regFlag)
                        {
                            //转换 大写A转换为_a
                            columnName = Regex.Replace(columnName, "[A-Z]",
                                delegate(Match m) { return "_" + m.Value[0].ToString().ToLower(); });
                        }                     
                        dt.Columns.Add(columnName);                      
                        if (splits[1].Contains('"'))
                        {
                            rowList.Add(splits[1].Substring(1, splits[1].Length - 2));
                        }
                        else
                        {
                            rowList.Add(splits[1]);
                        }
                    }
                }
            }
            if (rowList.Count > 0)
            {
                dt.Rows.Add(rowList.ToArray());
            }
            return dt;
        }
        /// <summary>
        /// 数据表转化为JSON格式对象
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <param name="db"></param>
        /// <param name="regFlag">是否转换</param>
        /// <returns></returns>
        public static string DataTableToJson(DataRow dr, string stationId, string db, bool regFlag)
        {
            StringBuilder json = new StringBuilder();

            json.Append("{");
            //----------------------服务站ID-------------------------------
            json.Append('"');
            json.Append(StationId);
            json.Append('"');
            json.Append(":");
            json.Append('"');
            json.Append(stationId);
            json.Append('"');
            json.Append(",");
            //---------------------帐套Code--------------------------------            
            json.Append('"');
            json.Append(DB);
            json.Append('"');
            json.Append(":");
            json.Append('"');
            json.Append(db);
            json.Append('"');
            json.Append(",");

            string columnName = string.Empty;

            foreach (DataColumn dc in dr.Table.Columns)
            {
                if (!dr.IsNull(dc.ColumnName))
                {
                    
                    columnName = dc.ColumnName;
                    if (regFlag)
                    {

                        //转换 _a转换为A
                        columnName = Regex.Replace(columnName, "_[a-z]",
                            delegate(Match m) { return m.Value[1].ToString().ToUpper(); });
                    }
                    if (columnName == DB)
                    {
                        continue;
                    }
                    json.Append('"');
                    json.Append(columnName);
                    json.Append('"');
                    json.Append(":");
                    if (dc.DataType == typeof(string) || dc.DataType == typeof(DateTime))
                    {
                        json.Append('"');
                        json.Append(dr[dc.ColumnName]);
                        json.Append('"');
                    }
                    else if (dc.DataType == typeof(byte[]))
                    {
                        json.Append('"');
                        string str = string.Empty;
                        byte[] bytes = (byte[])dr[dc.ColumnName];
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            str += bytes[i].ToString("X2");
                        }                      

                        json.Append(str);
                        json.Append('"');
                    }
                    else
                    {
                        json.Append(dr[dc.ColumnName]);
                    }
                    json.Append(",");
                }
            }
            if (json.Length > 2)
            {
                json.Remove(json.Length - 1, 1);
            }
            json.Append("}");
            return json.ToString();
        }
    }
}
