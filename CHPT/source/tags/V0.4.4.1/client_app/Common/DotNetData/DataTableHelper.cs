using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace HXCCommon.DotNetData
{
    /// <summary>
    /// DataTable 公共帮助类
    /// </summary>
    public class DataTableHelper
    {
        #region Hashtable根据key过滤表的内容
        /// <summary>
        /// Hashtable根据key过滤表的内容
        /// </summary>
        /// <param name="dt">数据库表</param>
        /// <param name="keyField">键</param>
        /// <param name="valFiled">值</param>
        /// <returns></returns>
        public static Hashtable DataTableToHashtableByKeyValue(DataTable dt, string keyField, string valFiled)
        {
            Hashtable ht = new Hashtable();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string key = dr[keyField].ToString();
                    ht[key] = dr[valFiled];
                }
            }
            return ht;
        }
        #endregion

        #region DataTable 转 XML
        /// <summary>
        /// DataTable 转 XML
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToXML(DataTable dt)
        {
            if (dt != null)
            {
                StringBuilder sb = new StringBuilder();
                XmlWriter writer = XmlWriter.Create(sb);
                XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
                serializer.Serialize(writer, dt);
                writer.Close();
                return sb.ToString();
            }
            return String.Empty;
        }
        #endregion

        #region DataTable 转 IList 行中是用Hashtable对象存
        /// <summary>
        /// DataTable 转 IList 行中是用Hashtable对象存
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>行中是用Hashtable对象存</returns>
        public static IList<Hashtable> DataTableToArrayList(DataTable dt)
        {
            if (dt == null) return new List<Hashtable>();
            IList<Hashtable> datas = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable ht = DataRowToHashTable(dr);
                datas.Add(ht);
            }
            return datas;
        }
        #endregion

        #region DataTable 转 DataTableToHashtable
        /// <summary>
        /// DataTable 转 DataTableToHashtable
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static Dictionary<string,string> DataTableToHashtable(DataTable dt)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string key = dt.Columns[i].ColumnName;
                    ht[key.ToUpper()] = dr[key].ToString();
                }
            }
            return ht;
        }
        #endregion

        #region DataRow  转  HashTable
        /// <summary>
        /// DataRow  转  HashTable
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Hashtable DataRowToHashTable(DataRow dr)
        {
            Hashtable htReturn = new Hashtable(dr.ItemArray.Length);
            foreach (DataColumn dc in dr.Table.Columns)
                htReturn.Add(dc.ColumnName, dr[dc.ColumnName]);
            return htReturn;
        }
        #endregion

        #region DataTable 转 实体类对象LIST
        /// <summary>
        /// DataTable 转 对象要LIST
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>行中是对象的类，类的属性与数据字段一致</returns>
        public static IList DataTableToIList<T>(DataTable dt)
        {
            // 定义集合    
            IList list = new List<T>();
            // 获得此模型的类型    
            // Type type = typeof(T);
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                //T t = new T();
                T obj = Activator.CreateInstance<T>();
                // 获得此模型的公共属性    
                PropertyInfo[] propertys = obj.GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;

                    // 检查DataTable是否包含此列    
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter    
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(obj, value, null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        #endregion

        #region DataTable根据条件过滤表的内容
        /// <summary>
        /// 根据条件过滤表的内容
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static DataTable GetNewDataTable(DataTable dt, string condition)
        {
            if (DataTableHelper.IsExistRows(dt))
            {
                if (condition.Trim() == "")
                {
                    return dt;
                }
                else
                {
                    DataTable newdt = new DataTable();
                    newdt = dt.Clone();
                    DataRow[] dr = dt.Select(condition);
                    for (int i = 0; i < dr.Length; i++)
                    {
                        newdt.ImportRow((DataRow)dr[i]);
                    }
                    return newdt;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 过滤DataTable重复数据！
        //// <summary>
        /// 返回执行Select distinct后的DataTable
        /// </summary>
        /// <param name="SourceTable">源数据表</param>
        /// <param name="FieldNames">字段集</param>
        /// <returns></returns>
        public static DataTable SelectDistinct(DataTable SourceTable, string[] FieldNames)
        {
            object[] lastValues;
            DataTable newTable;
            DataRow[] orderedRows;

            if (FieldNames == null || FieldNames.Length == 0)
                throw new ArgumentNullException("FieldNames");

            lastValues = new object[FieldNames.Length];
            newTable = new DataTable();

            foreach (string fieldName in FieldNames)
                newTable.Columns.Add(fieldName, SourceTable.Columns[fieldName].DataType);

            orderedRows = SourceTable.Select("", string.Join(",", FieldNames));

            foreach (DataRow row in orderedRows)
            {
                if (!fieldValuesAreEqual(lastValues, row, FieldNames))
                {
                    newTable.Rows.Add(createRowClone(row, newTable.NewRow(), FieldNames));

                    setLastValues(lastValues, row, FieldNames);
                }
            }
            return newTable;
        }
        private static DataRow createRowClone(DataRow sourceRow, DataRow newRow, string[] fieldNames)
        {
            foreach (string field in fieldNames)
                newRow[field] = sourceRow[field];

            return newRow;
        }

        private static void setLastValues(object[] lastValues, DataRow sourceRow, string[] fieldNames)
        {
            for (int i = 0; i < fieldNames.Length; i++)
                lastValues[i] = sourceRow[fieldNames[i]];
        }

        private static bool fieldValuesAreEqual(object[] lastValues, DataRow currentRow, string[] fieldNames)
        {
            bool areEqual = true;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                if (lastValues[i] == null || !lastValues[i].Equals(currentRow[fieldNames[i]]))
                {
                    areEqual = false;
                    break;
                }
            }
            return areEqual;
        }
        #endregion

        #region 检查DataTable 是否有数据行
        /// <summary>
        /// 检查DataTable 是否有数据行
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static bool IsExistRows(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
                return true;

            return false;
        }
        #endregion

        #region 排序表的视图
        /// <summary>
        /// 排序表的视图
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sorts"></param>
        /// <returns></returns>
        public static DataTable SortedTable(DataTable dt, params string[] sorts)
        {
            if (dt.Rows.Count > 0)
            {
                string tmp = "";
                for (int i = 0; i < sorts.Length; i++)
                {
                    tmp += sorts[i] + ",";
                }
                dt.DefaultView.Sort = tmp.TrimEnd(',');
            }
            return dt;
        }
        #endregion

        #region DataTable 分页
        /// <summary>
        /// Datatable 分页
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        public static DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0)
                return dt;
            DataTable newdt = dt.Copy();
            newdt.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            return newdt;
        }
        #endregion
    }
}
