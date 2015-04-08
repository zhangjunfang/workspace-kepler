using System.Data;
using System.Windows.Forms;
using SYSModel;
using BLL;
using CloudPlatSocket.Protocol;
using System.Collections.Generic;
using Utility.Common;
using System;

namespace CloudPlatSocket
{
    /// <summary>
    /// 数据操作帮助类
    /// 创建人：杨天帅
    /// 创建时间：2014.11.10
    /// 修改日期：2014.11.17
    /// </summary>
    public class DataHelper
    {
        #region --获取新增数据
        /// <summary> 获取添加类别的上传数据 
        /// </summary>
        /// <param name="tableName">表名</param>     
        /// <param name="dbName">帐套名</param>
        /// <param name="lastTime">最后一次上传时间</param>
        /// <param name="time">下一次开始上传时间</param>
        /// <returns></returns>
        public static DataTable GetAddData(string tableName, string dbName, string lastTime, string time)
        {
            var dataRow = GetColumnInfo(dbName, tableName, "create_time");
            if (dataRow != null)
            {
                if (dataRow["type"].ToString() == "datetime")
                {
                    return GetAddData2RelationTable(tableName, dbName, lastTime, time);
                }
            }
            var where = string.Format("create_time>='{0}' and create_time<'{1}'", lastTime, time);
            DataTable dt = null;
            try
            {
                if (!String.IsNullOrEmpty(where))
                {
                    dt = DBHelper.GetTable("获取新增上传表[" + tableName + "]数据", dbName, tableName, "*", where, "", "");
                }
            }
            catch (Exception ex)
            {
                LogAssistant.LogServiceError.WriteLog(string.Format("{0}", tableName), ex.Message);
                dt = null;
            }

            if (dt != null)
            {
                dt.TableName = tableName;
            }
            return dt;
        }
        public static DataTable GetAddData2RelationTable(string tableName, string dbName, string lastTime, string time)
        {
            var where = string.Format("create_time >= '{0}' and create_time < '{1}'", Common.UtcLongToLocalDateTime(lastTime, "yyyy-MM-dd HH:mm:ss.fff"), Common.UtcLongToLocalDateTime(time,"yyyy-MM-dd HH:mm:ss.fff"));
            DataTable dt = null;
            try
            {
                if (!String.IsNullOrEmpty(where))
                {
                    dt = DBHelper.GetTable("获取新增上传表[" + tableName + "]数据", dbName, tableName, "*", where, "", "");
                    dt.Columns.Remove("create_time");
                    dt.TableName = tableName;
                }
            }
            catch (Exception ex)
            {
                LogAssistant.LogServiceError.WriteLog(string.Format("{0}", tableName), ex.Message);
                dt = null;
            }
            return dt;
        }
        public static DataTable GetAddData(string tableName, ProtocolValue pv, string dbName, string lastTime, string time)
        {
            return null;
            //if (pv == null) return null;
            //if (!String.IsNullOrEmpty(pv.PreTableName))
            //{
            //    return GetAddData(
            //        string.Format("{0} inner join {1} on {0}.{2}={1}.{3}", tableName, pv.PreTableName, pv.Key, pv.PreKey),
            //        tableName, pv.PreTableName, dbName, lastTime, time, pv.Operate);
            //}
            //return GetAddData(tableName, dbName, lastTime, time, pv.Operate);
        }

        /// <summary> 获取添加类别的上传数据 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="realTableName"></param>
        /// <param name="preTableName">关联表</param>        
        /// <param name="dbName">帐套名</param>
        /// <param name="lastTime">最后一次上传时间</param>
        /// <param name="time">下一次开始上传时间</param>
        /// <returns></returns>
        public static DataTable GetAddDataFromHXC(string tableName, string realTableName, string preTableName, string dbName, string lastTime, string time, OperateType type)
        {
            if (preTableName.Length == 0)
            {
                preTableName = tableName;
            }

            string where = string.Empty;
            if (type == OperateType.AUD)
            {
                where = string.Format("({3}.create_time = {3}.update_time or {3}.update_time is null) and {3}.enable_flag='{0}' and {3}.create_time>='{1}' and {3}.create_time<'{2}' and {3}.data_source='{4}'",
                DataSources.EnumStatus.Start.ToString("d"), lastTime, time, preTableName, DataSources.EnumDataSources.SELFBUILD.ToString("d"));
            }
            else if (type == OperateType.AU)
            {
                where = string.Format("({2}.create_time = {2}.update_time or {2}.update_time is null) and {2}.create_time>='{0}' and {2}.create_time<'{1}' and {2}.data_source='{3}'",
                   lastTime, time, preTableName, DataSources.EnumDataSources.SELFBUILD.ToString("d"));
            }
            else if (type == OperateType.A)
            {
                where = string.Format("{2}.create_time>='{0}' and {2}.create_time<'{1}'", lastTime, time, preTableName);
            }

            DataTable dt = null;
            try
            {
                if (where.Length > 0)
                {
                    dt = DBHelper.GetTable("获取新增上传表[" + tableName + "]数据", dbName, tableName, realTableName + ".*", where, "", "");
                }
            }
            catch (Exception ex)
            {
                LogAssistant.LogServiceError.WriteLog(string.Format("{0}", tableName), ex.Message);
                dt = null;
            }

            if (dt != null)
            {
                dt.TableName = realTableName;
            }
            return dt;
        }
        public static DataTable GetAddDataFromHXC(string tableName, string dbName, string lastTime, string time, OperateType opType = OperateType.AUD)
        {
            return GetAddDataFromHXC(tableName, tableName, tableName, dbName, lastTime, time, opType);
        }
        public static DataTable GetAddDataFromHXC(string tableName, ProtocolValue pv, string dbName, string lastTime, string time)
        {
            if (pv == null) return null;
            if (pv.PreTableName.Length > 0)
            {
                return GetAddDataFromHXC(
                    string.Format("{0} inner join {1} on {0}.{2}={1}.{3}", tableName, pv.PreTableName, pv.Key, pv.PreKey),
                    tableName, pv.PreTableName, dbName, lastTime, time, pv.Operate);
            }
            return GetAddDataFromHXC(tableName, dbName, lastTime, time, pv.Operate);
        }
        #endregion

        #region --获取修改数据
        /// <summary> 获取更新类别的上传数据
        /// </summary>
        /// <param name="tableName">表名</param>  
        /// <param name="dbName">帐套名</param>
        /// <param name="lastTime">最后一次上传时间</param>
        /// <param name="time">下一次开始上传时间</param>
        /// <returns></returns>
        public static DataTable GetUpdateData(string tableName, string dbName, string lastTime, string time)
        {
            var where = string.Format("update_time>='{0}' and update_time<'{1}'", lastTime, time);
            DataTable dt = null;
            try
            {
                if (!String.IsNullOrEmpty(where))
                {
                    dt = DBHelper.GetTable("获取新增上传表[" + tableName + "]数据", dbName, tableName, "*", where, "", "");
                }
            }
            catch (Exception ex)
            {
                LogAssistant.LogServiceError.WriteLog(string.Format("{0}", tableName), ex.Message);
                dt = null;
            }

            if (dt != null)
            {
                dt.TableName = tableName;
            }
            return dt;
        }
        public static DataTable GetUpdateData(string tableName, string dbName, string lastTime, string time, OperateType type = OperateType.AUD)
        {
            return null;
            //return GetUpdateData(tableName, tableName, tableName, dbName, lastTime, time, type);
        }
        public static DataTable GetUpdateData(string tableName, ProtocolValue pv, string dbName, string lastTime, string time)
        {
            return null;
            //if (pv == null) return null;
            //if (pv.PreTableName.Length > 0)
            //{
            //    return GetUpdateData(
            //        string.Format("{0} inner join {1} on {0}.{2}={1}.{3}", tableName, pv.PreTableName, pv.Key, pv.PreKey),
            //        tableName, pv.PreTableName, dbName, lastTime, time, pv.Operate);
            //}
            //return GetUpdateData(tableName, dbName, lastTime, time, pv.Operate);
        }
        /// <summary> 获取更新类别的上传数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="preTableName">关联表</param>        
        /// <param name="dbName">帐套名</param>
        /// <param name="lastTime">最后一次上传时间</param>
        /// <param name="time">下一次开始上传时间</param>
        /// <returns></returns>
        public static DataTable GetUpdateDataFormHXC(string tableName, string realTableName, string preTableName, string dbName, string lastTime, string time, OperateType type)
        {
            if (preTableName.Length == 0)
            {
                preTableName = tableName;
            }

            string where = string.Empty;
            if (type == OperateType.AUD)
            {
                where = string.Format("{3}.update_time > {3}.create_time and {3}.enable_flag='{0}' and {3}.update_time>='{1}' and {3}.update_time<'{2}' and {3}.data_source='{4}'",
                    DataSources.EnumStatus.Start.ToString("d"), lastTime, time, preTableName, DataSources.EnumDataSources.SELFBUILD.ToString("d"));
            }

            DataTable dt = null;
            try
            {
                if (where.Length > 0)
                {
                    dt = DBHelper.GetTable("获取更新上传表[" + tableName + "]数据", dbName, tableName, tableName + ".*", where, "", "");
                }
            }
            catch (Exception ex)
            {
                LogAssistant.LogServiceError.WriteLog(string.Format("{0}", tableName), ex.Message);
                dt = null;
            }

            if (dt != null)
            {
                dt.TableName = realTableName;
            }
            return dt;
        }
        public static DataTable GetUpdateDataFormHXC(string tableName, string dbName, string lastTime, string time, OperateType type = OperateType.AUD)
        {
            return GetUpdateDataFormHXC(tableName, tableName, tableName, dbName, lastTime, time, type);
        }
        public static DataTable GetUpdateDataFormHXC(string tableName, ProtocolValue pv, string dbName, string lastTime, string time)
        {
            if (pv == null) return null;
            if (pv.PreTableName.Length > 0)
            {
                return GetUpdateDataFormHXC(
                    string.Format("{0} inner join {1} on {0}.{2}={1}.{3}", tableName, pv.PreTableName, pv.Key, pv.PreKey),
                    tableName, pv.PreTableName, dbName, lastTime, time, pv.Operate);
            }
            return GetUpdateDataFormHXC(tableName, dbName, lastTime, time, pv.Operate);
        }
        #endregion

        #region --获取删除数据
        public static void DeleteBkData(String dbName, String time)
        {
            try
            {
                var where = string.Format("drop_time < '{0}'", Common.UtcLongToLocalDateTime(time, "yyyy-MM-dd HH:mm:ss.fff"));
                var result = DBHelper.BatchDeleteDataByWhere("获取删除备份表数据", dbName, "tl_delete_bk", where);
                if (result)
                {
                    LogAssistant.LogServiceError.WriteLog(String.Format("删除账套{0}云平台上传备份表", dbName));
                }
                else
                {
                    LogAssistant.LogServiceError.WriteLog(String.Format("删除账套{0}云平台上传备份表失败", dbName));
                }
            }
            catch (Exception ex)
            {
                LogAssistant.LogServiceError.WriteLog(ex);
            }
        }
        /// <summary> 获取删除类别的上传数据 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="realTableName"></param>
        /// <param name="preTableName">关联表</param>        
        /// <param name="dbName">帐套名</param>
        /// <param name="lastTime">最后一次上传时间</param>
        /// <param name="time">下一次开始上传时间</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable GetDeleteData(string tableName, string realTableName, string preTableName, string dbName, string lastTime, string time, OperateType type)
        {
            if (preTableName.Length == 0)
            {
                preTableName = tableName;
            }

            string where = string.Empty;
            if (type == OperateType.AUD)
            {
                where = string.Format("{3}.enable_flag='{0}' and {3}.update_time>='{1}' and {3}.update_time < '{2}'",
                    DataSources.EnumStatus.Start.ToString("d"), lastTime, time, preTableName);
            }

            DataTable dt = null;
            try
            {
                if (where.Length > 0)
                {
                    dt = DBHelper.GetTable("获取删除上传表[" + tableName + "]数据", dbName, tableName, tableName + ".*", where, "", "");
                }
            }
            catch (Exception ex)
            {
                LogAssistant.LogServiceError.WriteLog(string.Format("{0}", tableName), ex.Message);
                dt = null;
            }

            if (dt != null)
            {
                dt.TableName = realTableName;
            }
            return dt;
        }
        public static DataTable GetDeleteData(string tableName, string dbName, String lastTime, string time)
        {
            DataTable dt = null;
            try
            {
                var columnName = String.Empty;
                var where = string.Format("table_name = '{0}'", tableName);
                dt = DBHelper.GetTable("获取新增上传表[" + tableName + "]数据的主键列", dbName, "tl_delete_bk", "top 1 *", where, "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    columnName = dt.Rows[0]["column_name"].ToString();
                }
                if (String.IsNullOrEmpty(columnName)) return dt;
                dt = null;
                where = string.Format("table_name = '{2}' and drop_time >= '{0}' and drop_time < '{1}'", Common.UtcLongToLocalDateTime(lastTime, "yyyy-MM-dd HH:mm:ss.fff"), Common.UtcLongToLocalDateTime(time, "yyyy-MM-dd HH:mm:ss.fff"),tableName);
                if (!String.IsNullOrEmpty(where))
                {
                    dt = DBHelper.GetTable("获取新增上传表[" + tableName + "]数据", dbName, "tl_delete_bk", "data_guid as " + columnName, where, "", "");
                    dt.TableName = tableName;
                }
            }
            catch (Exception ex)
            {
                LogAssistant.LogServiceError.WriteLog(string.Format("{0}", tableName), ex.Message);
                dt = null;
            }
            return dt;
        }
        public static DataTable GetDeleteData(string tableName, ProtocolValue pv, string dbName, string lastTime, string time)
        {
            if (pv == null) return null;
            if (pv.PreTableName.Length > 0)
            {
                return GetDeleteData(
                    string.Format("{0} inner join {1} on {0}.{2}={1}.{3}", tableName, pv.PreTableName, pv.Key, pv.PreKey),
                    tableName, pv.PreTableName, dbName, lastTime, time, pv.Operate);
            }
            return GetDeleteData(tableName, tableName,tableName, dbName, lastTime, time, pv.Operate);
        }

        /// <summary> 获取删除类别的上传数据 
        /// </summary>
        /// <param name="tableName">表名</param>  
        /// <param name="dbName">帐套名</param>
        /// <param name="lastTime">最后一次上传时间</param>
        /// <param name="time">下一次开始上传时间</param>
        /// <returns></returns>
        public static DataTable GetDeleteDataFromHXC(string tableName, string realTableName, string preTableName, string dbName, string lastTime, string time, OperateType type)
        {
            if (preTableName.Length == 0)
            {
                preTableName = tableName;
            }

            string where = string.Empty;
            if (type == OperateType.AUD)
            {
                where = string.Format("{3}.enable_flag='{0}' and {3}.update_time>={1} and {3}.update_time < {2} and {3}.data_source='{4}'",
                    DataSources.EnumStatus.Start.ToString("d"), lastTime, time, preTableName, DataSources.EnumDataSources.SELFBUILD.ToString("d"));
            }

            DataTable dt = null;
            try
            {
                if (where.Length > 0)
                {
                    dt = DBHelper.GetTable("获取删除上传表[" + tableName + "]数据", dbName, tableName, tableName + ".*", where, "", "");
                }
            }
            catch (Exception ex)
            {
                LogAssistant.LogServiceError.WriteLog(string.Format("{0}", tableName), ex.Message);
                dt = null;
            }

            if (dt != null)
            {
                dt.TableName = realTableName;
            }
            return dt;
        }
        public static DataTable GetDeleteDataFromHXC(string tableName, string dbName, string lastTime, string time, OperateType type = OperateType.AUD)
        {
            return GetDeleteDataFromHXC(tableName, tableName, tableName, dbName, lastTime, time, type);
        }
        #endregion

        public static DataTable GetDataTable(string dbName, string sqlString)
        {
            SQLObj sql = new SQLObj();
            sql.Param = new Dictionary<string, SYSModel.ParamObj>();
            sql.cmdType = CommandType.Text;
            sql.sqlString = sqlString;
            try
            {
                DataSet ds = DBHelper.GetDataSet("获取监控信息", dbName, sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                LogAssistant.LogServiceError.WriteLog(string.Format("获取数据失败:{0}", sqlString), ex.Message);

            }
            return null;
        }

        #region 校验数据库信息有效性 -- add by kord
        /// <summary>
        /// 判断数据库是否存在
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <returns></returns>
        public static bool DbIsExists(String dbName)
        {
            if (String.IsNullOrEmpty(dbName)) return false;
            var sql = String.Format("SELECT DBID,NAME FROM MASTER.DBO.SYSDATABASES WHERE NAME='{0}'", dbName);
            var sqlObj = new SQLObj
            {
                Param = new Dictionary<string, ParamObj>(),
                cmdType = CommandType.Text,
                sqlString = sql
            };
            try
            {
                var ds = DBHelper.GetDataSet(String.Format("判断数据库({0})是否存在", dbName), "MASTER", sqlObj);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogAssistant.LogService.WriteLog(ex);
                return false;
            }
            return false;
        }
        /// <summary>
        /// 判断数据表是否存在
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="tableName">数据表名称</param>
        /// <returns></returns>
        public static bool TableIsExists(String dbName, String tableName)
        {
            if (String.IsNullOrEmpty(dbName) || String.IsNullOrEmpty(tableName)) return false;
            var sql = String.Format("SELECT * FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = '{0}'", tableName);
            var sqlObj = new SQLObj
            {
                Param = new Dictionary<string, ParamObj>(),
                cmdType = CommandType.Text,
                sqlString = sql
            };
            try
            {
                var ds = DBHelper.GetDataSet(String.Format("判断数据库({0})是否存在", tableName), dbName, sqlObj);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogAssistant.LogService.WriteLog(ex);
                return false;
            }
            return false;
        }
        /// <summary>
        /// 获取数据列信息
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="tableName">数据表名称</param>
        /// <param name="columnName">数据列名称</param>
        /// <returns></returns>
        public static DataRow GetColumnInfo(String dbName, String tableName, String columnName)
        {
            if (String.IsNullOrEmpty(dbName) || String.IsNullOrEmpty(tableName) || String.IsNullOrEmpty(columnName)) return null;
            var sql = String.Format(
                    " SELECT column_name= convert(varchar(100), a.name), table_name= convert(varchar(50), d.name ), type= CONVERT(varchar(50),b.name), library_name= '{0}', description=convert(varchar(50), isnull(g.[value],'')) FROM dbo.syscolumns a left join dbo.systypes b on a.xusertype=b.xusertype inner join dbo.sysobjects d on a.id=d.id and d.xtype='U' and d.name<>'dtproperties' left join dbo.syscomments e on a.cdefault=e.id left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id left join sys.extended_properties f on d.id=f.major_id and f.minor_id=0 where d.name ='{1}' and a.name = '{2}'",dbName, tableName, columnName);
            var sqlObj = new SQLObj
            {
                Param = new Dictionary<string, ParamObj>(),
                cmdType = CommandType.Text,
                sqlString = sql
            };
            try
            {
                var ds = DBHelper.GetDataSet("获取数据库列信息", dbName, sqlObj);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0];
                }
            }
            catch (Exception ex)
            {
                LogAssistant.LogService.WriteLog(ex);
                return null;
            }
            return null;
        }
        #endregion
    }
}
