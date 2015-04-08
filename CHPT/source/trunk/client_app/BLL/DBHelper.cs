using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYSModel;
using System.Data;
using HXC_FuncUtility;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace BLL
{
    /// <summary> 服务端数据库访问类
    /// Create By syn
    /// Create Time 2014-10-11 
    /// </summary>
    public class DBHelper
    {
        /// <summary> 判断记录是否存在
        /// </summary>
        /// <param name="opName">表名</param>
        /// <param name="tableName">表名</param>
        /// <param name="where">条件</param>
        /// <returns>返回boolen</returns>
        public static bool IsExist(string opName, string currAccDbName, string tableName, string where)
        {
            DataTable dt = GetTable(opName, currAccDbName, tableName, "1", where, "", "");
            return dt.Rows.Count > 0;
        }

        /// <summary> 获取系统服务时间
        /// </summary>
        /// <returns>返回Value</returns>
        public static DateTime GetCurrentTime(string currAccDbName)
        {
            DateTime dtCurr = DateTime.Now;
            string timeStr = GetSingleValue("获取服务时间", currAccDbName, "", "SYSDATETIME()", "", "");
            if (!string.IsNullOrEmpty(timeStr))
            {
                dtCurr = Convert.ToDateTime(timeStr);
            }
            return dtCurr;
        }

        /// <summary> 查询单个字段的值(一般求最大，最小值)
        /// <param name="opName">操作名</param>
        /// <param name="tableName">表名</param>
        /// <param name="field">单个查询字段</param>
        /// <param name="orderBy">排序字段，多个以“,”分割</param>
        /// </summary>
        /// <returns>返回Value</returns>
        public static string GetSingleValue(string opName, string currAccDbName, string tableName, string field, string where, string orderBy)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("select " + field);
            if (!string.IsNullOrEmpty(tableName))
            {
                strBuilder.Append(" from  " + tableName);
                if (!string.IsNullOrEmpty(where))
                {
                    strBuilder.Append(" where  " + where);
                }
                if (!string.IsNullOrEmpty(orderBy))
                {
                    strBuilder.Append(" order by  " + orderBy);
                }
            }
            Dictionary<string, ParamObj> dic = new Dictionary<string, ParamObj>();
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj_Server.Instance.UserID, OPName = opName };
            string connString = LocalVariable.GetConnString(currAccDbName, ConfigConst.ConnectionStringReadonly);
            object obj = DbCommon.Instance.ExecuteNonQueryReturnObjectNoTrans(connString, CommandType.Text, strBuilder.ToString(), dic, userOp);
            if (obj == null)
            {
                return "";
            }
            return obj.ToString();
        }

        /// <summary> 查询单个字段的值(一般求最大，最小值)
        /// <param name="strSql">sql语句</param>
        /// </summary>
        /// <returns>返回Value</returns>
        public static string GetSingleValue(string opName, string currAccDbName, string strSql)
        {
            Dictionary<string, ParamObj> dic = new Dictionary<string, ParamObj>();
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj_Server.Instance.UserID, OPName = opName };
            string connString = LocalVariable.GetConnString(currAccDbName, ConfigConst.ConnectionStringReadonly);
            object obj = DbCommon.Instance.ExecuteNonQueryReturnObjectNoTrans(connString, CommandType.Text, strSql, dic, userOp);
            if (obj == null)
            {
                return "";
            }
            return obj.ToString();
        }

        /// <summary> 新增或更新
        /// </summary>
        /// /// <param name="opName">操作名</param>
        /// <param name="tableName">表名</param>
        /// <param name="keyName">主键名称</param>
        /// <param name="keyValue">主键值,如果有值，表示更新记录</param>
        /// <param name="dicFileds">字段集合</param>
        /// <returns>返回执行是否成功</returns>
        public static bool Submit_AddOrEdit(string opName, string currAccDbName, string tableName, string keyName, string keyValue, Dictionary<string, string> dicFileds)
        {
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj_Server.Instance.UserID, OPName = opName };
            string connString = LocalVariable.GetConnString(currAccDbName, ConfigConst.ConnectionStringWrite);
            return DbCommon.Instance.Submit_AddOrEdit(connString, tableName, keyName, keyValue, dicFileds, userOp);
        }
        /// <summary> 新增或更新(专门写日志)
        /// </summary>
        /// /// <param name="opName">操作名</param>
        /// <param name="tableName">表名</param>
        /// <param name="keyName">主键名称</param>
        /// <param name="keyValue">主键值,如果有值，表示更新记录</param>
        /// <param name="dicFileds">字段集合</param>
        /// <returns>返回执行是否成功</returns>
        public static bool Submit_AddLog(string opName, string currAccDbName, string tableName, string keyName, string keyValue, Dictionary<string, string> dicFileds)
        {
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj_Server.Instance.UserID, OPName = opName };
            string connString = LocalVariable.GetConnString(currAccDbName, ConfigConst.ConnectionStringWrite);
            return DbCommon.Instance.Submit_AddOrEditLog(userOp, connString, tableName, keyName, keyValue, dicFileds);
        }

        /// <summary>删除 BYID
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="tableName">表名</param>
        /// <param name="keyName">键名称</param>
        /// <param name="keyValue">键值</param>
        /// <returns></returns>
        public static bool DeleteDataByID(string opName, string currAccDbName, string tableName, string keyName, string keyValue)
        {
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj_Server.Instance.UserID, OPName = opName };
            string connString = LocalVariable.GetConnString(currAccDbName, ConfigConst.ConnectionStringWrite);
            return DbCommon.Instance.DeleteDataByID(connString, tableName, keyName, keyValue, userOp);
        }

        /// <summary> 根据查询条件，查单表，重点是Where条件，拼接SQL
        /// <param name="opName">操作名</param>
        /// <param name="tableName">表名</param>
        /// <param name="fileds">查询字段</param>
        /// <param name="where">查询条件，不包含where</param>
        /// <param name="groupBy">分组字段，多个以“,”分割</param>
        /// <param name="orderBy">排序字段+排序方式，多个以“,”分割</param>
        /// </summary>
        /// <returns>返回结果集</returns>
        public static DataTable GetTable(string opName, string currAccDbName, string tableName, string fileds, string where, string groupBy, string orderBy)
        {
            string[] fieldArr = Regex.Split(fileds, ",", RegexOptions.IgnorePatternWhitespace);
            List<string> listField = new List<string>();
            foreach (string str in fieldArr)
            {
                listField.Add(str);
            }
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj_Server.Instance.UserID, OPName = opName };
            string connString = LocalVariable.GetConnString(currAccDbName, ConfigConst.ConnectionStringReadonly);
            DataSet ds = DbCommon.Instance.SelOPSingleTable(connString, listField, tableName, where, orderBy, userOp);
            return ds.Tables[0];
        }

        /// <summary>查询表
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="obj">操作sql对象</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string opName, string currAccDbName, SQLObj obj)
        {
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj_Server.Instance.UserID, OPName = opName };
            string connString = LocalVariable.GetConnString(currAccDbName, ConfigConst.ConnectionStringReadonly);
            DataSet ds = DbCommon.Instance.ExecuteNonQueryReturnDataSetNoTrans(connString, obj.cmdType, obj.sqlString, obj.Param, userOp);
            return ds;
        }

        /// <summary> 根据通用的分页存储过程，分页查询
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="tableName">表名</param>
        /// <param name="fileds">查询字段集合，以“,”分割</param>
        /// <param name="where">查询条件，不包含where</param>
        /// <param name="groupBy">分组字段，多个以“,”分割</param>
        /// <param name="orderBy">排序字段+排序方式，多个以“,”分割</param>
        /// <param name="pageIndex">查询第几页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">返回总记录数</param>
        /// <returns>返回结果集</returns>
        public static DataTable GetTableByPage(string opName, string currAccDbName, string tableName, string fileds, string where, string groupBy, string orderBy, int pageIndex, int pageSize, out int recordCount)
        {
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj_Server.Instance.UserID, OPName = opName };
            int count = 0;
            string connString = LocalVariable.GetConnString(currAccDbName, ConfigConst.ConnectionStringReadonly);
            DataSet ds = DbCommon.Instance.GetListPageStoreProcedure(connString, pageIndex, pageSize, tableName, where, orderBy, fileds, userOp, ref count);
            recordCount = count;
            return ds.Tables[0];
        }

        /// <summary>根据主键值，单表删除多记录，In操作
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="sqlObjList">sql对象</param>
        /// <returns></returns>
        public static bool BatchUpdateDataByIn(string opName, string currAccDbName, string tableName, Dictionary<string, string> dicParam, string pkName, string[] pkValues)
        {
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj_Server.Instance.UserID, OPName = opName };
            string connString = LocalVariable.GetConnString(currAccDbName, ConfigConst.ConnectionStringWrite);
            int count = DbCommon.Instance.BatchUpdateDataByIn(connString, tableName, dicParam, pkName, pkValues, userOp);
            return count > 0;
        }

        /// <summary> 事务批量执行（List<string>）
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="sqlStringList">批量sql</param>
        /// <returns></returns>
        public static bool BatchExeSQLStringMultiByTrans(string opName, string currAccDbName, IList<SysSQLString> sqlStringList)
        {
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj_Server.Instance.UserID, OPName = opName };
            string connString = LocalVariable.GetConnString(currAccDbName, ConfigConst.ConnectionStringWrite);
            return DbCommon.Instance.BatchExeSQLMultiByTrans(connString, sqlStringList, userOp);
        }

        /// <summary>根据条件删除指定表中的数据
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="tableName">表名</param>
        /// <param name="strWhere">执行条件，不包含where</param>
        /// <returns></returns>
        public static bool BatchDeleteDataByWhere(string opName, string currAccDbName, string tableName, string strWhere)
        {
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj_Server.Instance.UserID, OPName = opName };
            string connString = LocalVariable.GetConnString(currAccDbName, ConfigConst.ConnectionStringWrite);
            int flag = DbCommon.Instance.BatchDeleteDataByWhere(connString, tableName, strWhere, userOp);
            return flag > 0;
        }

        /// <summary> 事务批量执行
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="sqlObjList">批量sql</param>
        /// <returns></returns>
        public static bool BatchExeSQLMultiByTrans(string opName, string currAccDbName, IList<SQLObj> sqlObjList)
        {
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj_Server.Instance.UserID, OPName = opName };
            string connString = LocalVariable.GetConnString(currAccDbName, ConfigConst.ConnectionStringWrite);
            return DbCommon.Instance.BatchExeSQLMultiByTrans(connString, sqlObjList, userOp);
        }

        /// <summary> 执行sql或存储过程
        /// </summary>
        /// <param name="opName">操作描述</param>
        /// <param name="sqlString">sql语句或存储过程</param>
        /// <param name="dicPara">参数</param>
        /// <returns>返回是否成功</returns>
        public static void ExtNonQuery(string opName, string currAccDbName, string sqlString, CommandType cmdType, Dictionary<string, string> dicPara)
        {
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj_Server.Instance.UserID, OPName = opName };
            SQLObj sqlObj = new SQLObj();
            sqlObj.cmdType = cmdType;
            sqlObj.sqlString = sqlString;
            sqlObj.Param = new Dictionary<string, ParamObj>();
            if (dicPara != null)
            {
                foreach (string str in dicPara.Keys)
                {
                    sqlObj.Param.Add(str, new ParamObj(str, dicPara[str], SysDbType.VarChar));
                }
            }
            string connString = LocalVariable.GetConnString(currAccDbName, ConfigConst.ConnectionStringWrite);
            DbCommon.Instance.ExtNonQuery(sqlObj, connString, userOp);
        }

        /// <summary>
        /// 事务批量执行，不备份
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="listSql">批量sql</param>
        /// <returns></returns>
        public static bool BatchExeSQLStrMultiByTransNoLogNoBackup(string opName, string accCode, List<SysSQLString> listSql)
        {
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj_Server.Instance.UserID, OPName = opName };
            string conn = LocalVariable.GetConnString(GlobalStaticObj_Server.DbPrefix + accCode, ConfigConst.ConnectionStringWrite);
            return DbCommon.Instance.BatchExeSQLStrMultiByTransNoLogNoBackup(conn,
                listSql, userOp);
        }
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="opName">操作名</param>
        /// <param name="tableName">表名</param>
        /// <param name="listRow">数据</param>
        /// <returns></returns>
        public static bool SqlBulkByTransNoLogNoBackUp(string opName, string accCode, string tableName, List<DataRow> listRow)
        {
            UserIDOP userOp = new UserIDOP() { UserID = GlobalStaticObj_Server.Instance.UserID, OPName = opName };
            string conn = LocalVariable.GetConnString(GlobalStaticObj_Server.DbPrefix + accCode, ConfigConst.ConnectionStringWrite);
            return DbCommon.Instance.SqlBulkByTransNoLogNoBackUp(conn,
                tableName, listRow);
        }
    }
}
