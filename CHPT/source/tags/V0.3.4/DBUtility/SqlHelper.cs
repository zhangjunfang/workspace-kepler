using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using Model;
using HXCCommon.DotNetCode;
using HXCCommon.DotNetData;
using SYSModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using HXCLog;
namespace DBUtility
{
    public class SqlHelper
    {
        #region 日志相关
        /// <summary> 记录用户操作日志
        /// </summary>
        /// <param name="uOpLog">日志信息</param>
        private static void writeUserOpLog(UserOPLog uOpLog)
        {
            UserOPLogClass.Instance.Add(uOpLog);
        }

        /// <summary> 日志的更新操作，千万不要再次进行写日志操作
        /// </summary>
        public static bool Submit_AddOrEditLog(UserIDOP userID, string connString, string tableName, string pkName, string pkVal, Dictionary<string, string> ht)
        {
            if (string.IsNullOrEmpty(pkVal))
            {
                if (SqlHelper.InsertByHashtableLog(userID, connString, tableName, ht) > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                if (SqlHelper.UpdateByHashtableLog(userID, connString, tableName, pkName, pkVal, ht) > 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary> 日志的更新操作，千万不要再次进行写日志操作
        /// </summary>
        public static int InsertByHashtableLog(UserIDOP userID, string connString, string tableName, Dictionary<string, string> ht)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Insert Into ");
            sb.Append(tableName);
            sb.Append("(");
            StringBuilder sp = new StringBuilder();
            StringBuilder sb_prame = new StringBuilder();
            foreach (string key in ht.Keys)
            {
                sb_prame.Append("," + key);
                sp.Append(",@" + key);
            }
            sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")").Append(";");
            int _object = SqlHelper.ExecuteNonQueryNoBak(connString, CommandType.Text, sb.ToString(), userID, SqlHelper.GetParameter(ht));
            return _object;
        }

        /// <summary> 日志的更新操作，千万不要再次进行写日志操作
        /// </summary>
        public static int UpdateByHashtableLog(UserIDOP userID, string connString, string tableName, string pkName, string pkVal, Dictionary<string, string> ht)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Update ");
            sb.Append(tableName);
            sb.Append(" Set ");
            bool isFirstValue = true;
            foreach (string key in ht.Keys)
            {
                if (isFirstValue)
                {
                    isFirstValue = false;
                    sb.Append(key);
                    sb.Append("=");
                    sb.Append("@" + key);
                }
                else
                {
                    sb.Append("," + key);
                    sb.Append("=");
                    sb.Append("@" + key);
                }
            }
            sb.Append(" Where ").Append(pkName).Append("=").Append("@" + pkName).Append(";"); ;
            ht[pkName] = pkVal;
            SqlParameter[] _params = SqlHelper.GetParameter(ht);
            object _object = SqlHelper.ExecuteNonQueryNoBak(connString, CommandType.Text, sb.ToString(), userID, _params);
            return (_object == DBNull.Value) ? 0 : Convert.ToInt32(_object);
        }

        #endregion

        #region 辅助方法
        /// <summary> 参数处理
        /// </summary>
        /// <param name="cmdParms">参数集合</param>
        /// <returns>返回处理后的参数list</returns>
        private static List<SqlParamJSON> ConvertSQLParams(SqlParameter[] cmdParms)
        {
            if (cmdParms == null)
            {
                return null;
            }

            List<SqlParamJSON> paramsList = new List<SqlParamJSON>();
            for (int i = 0; i < cmdParms.Length; i++)
            {
                SqlParamJSON jsonParam = new SqlParamJSON();
                jsonParam.name = cmdParms[i].ParameterName;
                jsonParam.value = cmdParms[i].Value;
                paramsList.Add(jsonParam);
            }
            return paramsList;
        }

        /// <summary> 为command添加执行内容
        /// </summary>
        /// <param name="cmd">SqlCommand类型</param>
        /// <param name="conn">SqlConnection类型</param>
        /// <param name="trans">SqlTransaction要处理的事物</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="cmdParms">ommand执行的SqlParameter[]数组</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                Open(conn);
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(cmdParms);
            }
        }

        /// <summary> 为command添加执行内容--无事务
        /// </summary>
        /// <param name="cmd">SqlCommand类型</param>
        /// <param name="conn">SqlConnection类型</param>
        /// <param name="trans">SqlTransaction要处理的事物</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="cmdParms">ommand执行的SqlParameter[]数组</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                Open(conn);
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(cmdParms);
            }
        }

        #region 对象参数转换
        private static object paramEmptyConvert(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return DBNull.Value;
            }
            else if (value == null)
            {
                return DBNull.Value;
            }
            else
            {
                return (object)value;
            }
        }

        private static object objParamEmptyConvert(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            else if (string.IsNullOrEmpty(value.ToString()))
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }

        /// <summary> 对象参数转换
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public static SqlParameter[] GetParameter(Dictionary<string, string> ht)
        {
            SqlParameter[] _params;
            if (ht == null || ht.Count == 0)
            {
                _params = new SqlParameter[0];
                return _params;
            }
            _params = new SqlParameter[ht.Count];
            int i = 0;
            foreach (string key in ht.Keys)
            {
                _params[i] = new SqlParameter("@" + key, paramEmptyConvert(ht[key]));
                i++;
            }
            return _params;
        }
        public static SqlParameter[] GetParameter(Dictionary<string, ParamObj> ht)
        {
            SqlParameter[] _params = new SqlParameter[ht.Count];
            int i = 0;
            foreach (string key in ht.Keys)
            {
                _params[i] = new SqlParameter("@" + key, objParamEmptyConvert(ht[key].value));
                _params[i].SqlDbType = ConvertDbType(ht[key].type);
                if (ht[key].size != null)
                {
                    _params[i].Size = int.Parse(ht[key].size.ToString());
                }
                else
                {
                    switch (ht[key].direction)
                    {
                        case ParamDirection.OutPut:
                            _params[i].Direction = ParameterDirection.Output;
                            break;
                        case ParamDirection.InPut:
                            _params[i].Direction = ParameterDirection.Input;
                            break;
                        case ParamDirection.InputOutput:
                            _params[i].Direction = ParameterDirection.InputOutput;
                            break;
                        case ParamDirection.ReturnValue:
                            _params[i].Direction = ParameterDirection.ReturnValue;
                            break;
                        default:
                            _params[i].Direction = ParameterDirection.Input;
                            break;
                    }
                }
                i++;
            }
            return _params;
        }

        public static SqlDbType ConvertDbType(SysDbType typeStr)
        {
            switch (typeStr)
            {
                case SysDbType.BigInt:
                    return SqlDbType.BigInt;
                case SysDbType.Image:
                    return SqlDbType.Image;
                case SysDbType.Bool:
                    return SqlDbType.Bit;
                case SysDbType.Char:
                    return SqlDbType.Char;
                case SysDbType.DateTime:
                    return SqlDbType.DateTime;
                case SysDbType.Date:
                    return SqlDbType.Date;
                case SysDbType.Decimal:
                    return SqlDbType.Decimal;
                case SysDbType.Float:
                    return SqlDbType.Float;
                case SysDbType.Int:
                    return SqlDbType.Int;
                case SysDbType.NChar:
                    return SqlDbType.NChar;
                case SysDbType.NVarChar:
                    return SqlDbType.NVarChar;
                case SysDbType.NVarCharMax:
                    return SqlDbType.NText;
                case SysDbType.VarChar:
                    return SqlDbType.VarChar;
                case SysDbType.VarCharMax:
                    return SqlDbType.Text;
                case SysDbType.SmallInt:
                    return SqlDbType.SmallInt;
                case SysDbType.TinyInt:
                    return SqlDbType.TinyInt;
                case SysDbType.Binary:
                    return SqlDbType.Binary;
                default:
                    return SqlDbType.VarChar;
            }
        }
        #endregion

        /// <summary> 关闭数据库连接
        /// </summary>
        public static void Close(SqlConnection con)
        {
            ///判断连接是否已经创建
            if (con != null)
            {
                ///判断连接的状态是否打开
                //if (con.State == ConnectionState.Open)
                //{
                con.Close();
                Dispose(con);
                //}
            }
        }

        /// <summary> 打开数据库连接
        /// </summary>
        /// <param name="con"></param>
        private static void Open(SqlConnection con)
        {
            con.Open();

            if (con.State == ConnectionState.Closed)
            {
                try
                {
                    ///打开数据库连接
                    con.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ///关闭已经打开的数据库连接	
                }
            }
        }

        /// <summary> 连接测试
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public static bool IsConnected(string connString)
        {
            bool flag = false;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        flag = true;
                    }
                    conn.Close();
                }
                catch
                {
                    flag = false;
                }
            }
            return flag;
        }
        #endregion

        #region 操作数据库
        #region 增删改
        #region  基础操作
        /// <summary> 执行数据操作-无事务无备份
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdType">commond类型</param>
        /// <param name="cmdText">sql语句或存储过程</param>
        /// <param name="userID">操作用户</param>
        /// <param name="paras">参数</param>
        /// <returns>返回</returns>
        public static int ExecuteNonQueryNoBak(string connString, CommandType cmdType, string cmdText, UserIDOP userID, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    Open(conn);
                    PrepareCommand(cmd, conn, cmdType, cmdText, paras);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary> 执行数据操作-无事务无备份
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdType">commond类型</param>
        /// <param name="cmdText">sql语句或存储过程</param>
        /// <param name="userID">操作用户</param>
        /// <param name="paras">参数</param>
        /// <returns>返回</returns>
        public static int ExecuteNonQueryNoBak(SqlConnection conn, CommandType cmdType, string cmdText, UserIDOP userID, params SqlParameter[] paras)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                PrepareCommand(cmd, conn, cmdType, cmdText, paras);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary> 返回执行响应的行数-无事务有备份
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="paras">command执行的SqlParameter[]数组</param>
        /// <returns>int 返回command执行响应的行数</returns>
        public static int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, UserIDOP userID, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string modifyCmdText = string.Empty;
                    bool modifyFlag = SQLHelper_UpdateBackup.TryUpdateSQLBackup(connString, cmdText, out modifyCmdText, paras);
                    if (modifyFlag)
                    {
                        cmdText = modifyCmdText;
                    }
                    PrepareCommand(cmd, conn, cmdType, cmdText, paras);
                    int val = cmd.ExecuteNonQuery();
                    return val;
                }
            }
        }

        /// <summary> 返回执行结果-无事务有备份
        /// </summary>
        /// <param name="connection">SqlConnection类型</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="paras">command执行的SqlParameter[]数组</param>
        /// <returns>int 返回command执行响应的行数</returns>
        /// 
        public static bool ExecuteNonQuery(SqlConnection conn, CommandType cmdType, string cmdText, UserIDOP userID, params SqlParameter[] paras)
        {
            bool result = false;
            using (SqlCommand cmd = new SqlCommand())
            {
                string modifyCmdText = string.Empty;
                bool modifyFlag = SQLHelper_UpdateBackup.TryUpdateSQLBackup(conn.ConnectionString, cmdText, out modifyCmdText, paras);
                if (modifyFlag)
                {
                    cmdText = modifyCmdText;
                }
                PrepareCommand(cmd, conn, cmdType, cmdText, paras);
                try
                {
                    cmd.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                    throw ex;
                }
            }
            return result;
        }

        /// <summary> 返回执行响应的行数-有事务无备份
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="paras">command执行的SqlParameter[]数组</param>
        /// <returns>int 返回command执行响应的行数</returns>
        public static int ExecuteNonQueryTranNoBak(string connString, CommandType cmdType, string cmdText, UserIDOP userID, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                Open(conn);
                SqlTransaction trans = conn.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(cmd, conn, trans, cmdType, cmdText, paras);
                    int val = -1;
                    try
                    {
                        val = cmd.ExecuteNonQuery();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    return val;
                }
            }
        }

        /// <summary> 返回执行响应的行数-有事务无备份
        /// </summary>
        /// <param name="connection">SqlConnection类型</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="paras">command执行的SqlParameter[]数组</param>
        /// <returns>int 返回command执行响应的行数</returns>
        /// 
        public static bool ExecuteNonQueryTranNoBak(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, UserIDOP userID, params SqlParameter[] paras)
        {
            bool result = false;
            using (SqlCommand cmd = new SqlCommand())
            {
                PrepareCommand(cmd, conn, trans, cmdType, cmdText, paras);
                result = cmd.ExecuteNonQuery() > 0;
            }
            return result;
        }

        /// <summary> 返回执行响应的行数-有事务有备份
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="paras">command执行的SqlParameter[]数组</param>
        /// <returns>int 返回command执行响应的行数</returns>
        public static int ExecuteNonQueryTran(string connString, CommandType cmdType, string cmdText, UserIDOP userID, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                Open(conn);
                SqlTransaction trans = conn.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand())
                {
                    string modifyCmdText = string.Empty;
                    bool modifyFlag = SQLHelper_UpdateBackup.TryUpdateSQLBackup(connString, cmdText, out modifyCmdText, paras);
                    if (modifyFlag)
                    {
                        cmdText = modifyCmdText;
                    }
                    PrepareCommand(cmd, conn, trans, cmdType, cmdText, paras);
                    int val = -1;
                    try
                    {
                        val = cmd.ExecuteNonQuery();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    return val;
                }
            }
        }

        /// <summary> 返回执行结果-有事务有备份
        /// </summary>
        /// <param name="connection">SqlConnection类型</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="paras">command执行的SqlParameter[]数组</param>
        /// <returns>int 返回command执行响应的行数</returns>
        /// 
        public static bool ExecuteNonQueryTran(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, UserIDOP userID, params SqlParameter[] paras)
        {
            bool result = false;
            using (SqlCommand cmd = new SqlCommand())
            {
                string modifyCmdText = string.Empty;
                bool modifyFlag = SQLHelper_UpdateBackup.TryUpdateSQLBackup(conn.ConnectionString, cmdText, out modifyCmdText, paras);
                if (modifyFlag)
                {
                    cmdText = modifyCmdText;
                }
                PrepareCommand(cmd, conn, trans, cmdType, cmdText, paras);
                result = cmd.ExecuteNonQuery() > 0;
            }
            return result;
        }
        #endregion

        #region 外部操作
        /// <summary> 返回执行响应的行数
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="paras">command执行的SqlParameter[]数组</param>
        /// <returns>int 返回command执行响应的行数</returns>
        public static int ExecuteNonQuery(string connString, SQLObj obj, UserIDOP userID)
        {
            SqlParameter[] paras = GetParameter(obj.Param);
            return ExecuteNonQuery(connString, obj.cmdType, obj.sqlString, userID, paras);
        }

        /// <summary> 返回执行响应的行数
        /// </summary>
        /// <param name="trans">command中执行的事物（SqlTransaction）</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="paras">command执行的SqlParameter[]数组</param>
        /// <returns>返回执行是否成功</returns>
        public static bool ExecuteNonQueryReturnBool(string connString, CommandType cmdType, string cmdText, UserIDOP userID, params SqlParameter[] paras)
        {
            int rows = ExecuteNonQueryTran(connString, cmdType, cmdText, userID, paras);
            return rows > 0;

        }

        /// <summary> 执行数据操作
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdType">command类型</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="dic">参数</param>
        /// <param name="userID">用户id</param>
        /// <returns>返回是否成功</returns>
        public static bool ExecuteNonQueryReturnBoolNoTrans(string connString, CommandType cmdType, string cmdText, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            SqlParameter[] paras = GetParameter(dic);
            int rows = ExecuteNonQuery(connString, cmdType, cmdText, userID, paras);
            return rows > 0;
        }

        /// <summary> 数据操作-事务
        /// </summary>
        public static bool ExecuteNonQueryReturnBoolByTrans(string connString, CommandType cmdType, string cmdText, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {

            SqlParameter[] paras = GetParameter(dic);
            int rows = ExecuteNonQueryTran(connString, cmdType, cmdText, userID, paras);
            return rows > 0;
        }

        /// <summary> 执行数据操作
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdType">command类型</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="dic">参数</param>
        /// <param name="userID">用户id</param>
        /// <returns>返回影响行数</returns>
        public static int ExecuteNonQueryReturnIntNoTrans(string connString, CommandType cmdType, string cmdText, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            SqlParameter[] paras = GetParameter(dic);
            int rows = ExecuteNonQueryNoBak(connString, cmdType, cmdText, userID, paras);
            return rows;
        }


        /// <summary> 通过Dictionary插入数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">Hashtable</param>
        /// <returns>int</returns>
        public static bool InsertByHashtable(string connString, string tableName, Dictionary<string, string> ht, UserIDOP userID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Insert Into ");
            sb.Append(tableName);
            sb.Append("(");
            StringBuilder sp = new StringBuilder();
            StringBuilder sb_prame = new StringBuilder();
            foreach (string key in ht.Keys)
            {
                sb_prame.Append("," + key);
                sp.Append(",@" + key);
            }
            sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")").Append(";"); ;
            SqlParameter[] myParamArray = GetParameter(ht);
            int rows = SqlHelper.ExecuteNonQuery(connString, CommandType.Text, sb.ToString(), userID, myParamArray);
            return rows > 0;
        }
        /// <summary> 删除数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段主键</param>
        /// <param name="pkVal">字段值</param>
        /// <returns></returns>
        public static int DeleteData(string connString, string tableName, string pkName, string pkVal, UserIDOP userID)
        {
            StringBuilder sb = new StringBuilder("Delete From " + tableName + " Where " + pkName + " = @ID").Append(";");
            return SqlHelper.ExecuteNonQueryTran(connString, CommandType.Text, sb.ToString(), userID, new SqlParameter[] { new SqlParameter("@ID", pkVal) });
        }

        /// <summary> 批量删除 - where
        /// </summary>
        public static int BatchDeleteDataByWhere(string connStr, string tableName, string whereString, UserIDOP userID)
        {
            StringBuilder sb = new StringBuilder("Delete From " + tableName + " Where " + whereString).Append(";");
            return SqlHelper.ExecuteNonQueryTran(connStr, CommandType.Text, sb.ToString(), userID, null);
        }


        /// <summary> 批量删除 - object[]
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段主键</param>
        /// <param name="pkVal">字段值</param>
        /// <returns></returns>
        public static int BatchDeleteData(string connString, string tableName, string pkName, object[] pkValues, UserIDOP userID)
        {
            SqlParameter[] param = new SqlParameter[pkValues.Length];
            int index = 0;
            string str = "@ID" + index;
            StringBuilder sql = new StringBuilder("DELETE FROM " + tableName + " WHERE " + pkName + " IN (");
            for (int i = 0; i < (param.Length - 1); i++)
            {
                object obj2 = pkValues[i];
                str = "@ID" + index;
                sql.Append(str).Append(",");
                param[index] = new SqlParameter(str, obj2);
                index++;
            }
            str = "@ID" + index;
            sql.Append(str);
            param[index] = new SqlParameter(str, pkValues[index]);
            sql.Append(" );");
            return SqlHelper.ExecuteNonQueryTran(connString, CommandType.Text, sql.ToString(), userID, param);
        }

        /// <summary> 表单提交：新增，修改
        ///     参数：
        ///     tableName:表名
        ///     pkName：字段主键
        ///     pkVal：字段值
        ///     ht：参数
        /// </summary>
        /// <returns></returns>
        public static bool Submit_AddOrEdit(string connString, string tableName, string pkName, string pkVal, Dictionary<string, string> ht, UserIDOP userID)
        {
            if (string.IsNullOrEmpty(pkVal))
            {
                return SqlHelper.InsertByHashtable(connString, tableName, ht, userID);
            }
            else
            {
                return SqlHelper.UpdateByHashtable(connString, tableName, pkName, pkVal, ht, userID);
            }
        }

        /// <summary> 通过Hashtable修改数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段主键</param>
        /// <param name="pkValue"></param>
        /// <param name="ht">Hashtable</param>
        /// <returns>int</returns>
        public static bool UpdateByHashtable(string connString, string tableName, string pkName, string pkVal, Dictionary<string, string> ht, UserIDOP userID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Update ");
            sb.Append(tableName);
            sb.Append(" Set ");
            bool isFirstValue = true;
            foreach (string key in ht.Keys)
            {
                if (isFirstValue)
                {
                    isFirstValue = false;
                    sb.Append(key);
                    sb.Append("=");
                    sb.Append("@" + key);
                }
                else
                {
                    sb.Append("," + key);
                    sb.Append("=");
                    sb.Append("@" + key);
                }
            }
            sb.Append(" Where ").Append(pkName).Append("=").Append("@" + pkName).Append(";");
            ht[pkName] = pkVal;
            SqlParameter[] myParamArray = GetParameter(ht);
            return SqlHelper.ExecuteNonQueryReturnBool(connString, CommandType.Text, sb.ToString(), userID, myParamArray);
        }

        /// <summary> 批量更新- where
        /// </summary>
        public static int BatchUpdateDataByWhere(string connStr, string tableName, Dictionary<string, string> ht, string whereString, UserIDOP userID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Update ");
            sb.Append(tableName);
            sb.Append(" Set ");
            bool isFirstValue = true;
            foreach (string key in ht.Keys)
            {
                if (isFirstValue)
                {
                    isFirstValue = false;
                    sb.Append(key);
                    sb.Append("=");
                    sb.Append("@" + key);
                }
                else
                {
                    sb.Append("," + key);
                    sb.Append("=");
                    sb.Append("@" + key);
                }
            }
            sb.Append(" Where ").Append(whereString).Append(";"); ;
            SqlParameter[] _params = SqlHelper.GetParameter(ht);
            object _object = SqlHelper.ExecuteNonQueryTran(connStr, CommandType.Text, sb.ToString(), userID, _params);
            return (_object == DBNull.Value) ? 0 : Convert.ToInt32(_object);
        }
        /// <summary> 批量更新- in
        /// </summary>
        public static int BatchUpdateDataByIn(string connStr, string tableName, Dictionary<string, string> ht, string pkName, object[] pkValues, UserIDOP userID)
        {
            StringBuilder sb = new StringBuilder();
            SqlParameter[] param = new SqlParameter[ht.Count + pkValues.Length];
            int index = 0;
            string str = string.Empty;
            sb.Append(" Update ");
            sb.Append(tableName);
            sb.Append(" Set ");
            bool isFirstValue = true;
            foreach (string key in ht.Keys)
            {
                if (isFirstValue)
                {
                    isFirstValue = false;
                    sb.Append(key);
                    sb.Append("=");
                    sb.Append("@" + key);
                }
                else
                {
                    sb.Append("," + key);
                    sb.Append("=");
                    sb.Append("@" + key);
                }
                param[index] = new SqlParameter("@" + key, ht[key]);
                index++;
            }
            sb.Append(" Where ").Append(pkName).Append(" IN (");
            for (int i = 0; i < (pkValues.Length); i++)
            {
                object obj2 = pkValues[i];
                str = "@" + pkName + index;
                sb.Append(str).Append(",");
                param[index] = new SqlParameter(str, obj2);
                index++;
            }
            string sqlStr = sb.ToString().TrimEnd(',');
            sqlStr += " ); ";
            object _object = SqlHelper.ExecuteNonQueryTran(connStr, CommandType.Text, sqlStr, userID, param);
            return (_object == DBNull.Value) ? 0 : Convert.ToInt32(_object);
        }

        /// <summary> 批量更新- 多条语句
        /// </summary>
        public static int BatchUpdateDataMulti(string connStr, IList<UpdateSQLObj> batUpdateList, UserIDOP userID)
        {
            StringBuilder sb = new StringBuilder();
            List<SqlParameter> paramList = new List<SqlParameter>();
            string str = string.Empty;
            int index = 0;
            foreach (UpdateSQLObj sqlObj in batUpdateList)
            {
                sb.Append(" Update ");
                sb.Append(sqlObj.TableName);
                sb.Append(" Set ");
                bool isFirstValue = true;
                foreach (string key in sqlObj.DicParam.Keys)
                {
                    if (isFirstValue)
                    {
                        isFirstValue = false;
                        sb.Append(key);
                        sb.Append("=");
                        sb.Append("@Param" + index.ToString());
                    }
                    else
                    {
                        sb.Append("," + key);
                        sb.Append("=");
                        sb.Append("@Param" + index.ToString());
                    }
                    paramList.Add(new SqlParameter("@Param" + index.ToString(), sqlObj.DicParam[key]));
                    index++;
                }
                if (sqlObj.pkName != null)
                {
                    sb.Append(" Where ").Append(sqlObj.pkName).Append("=").Append("@Param" + index.ToString()).Append(";");
                    paramList.Add(new SqlParameter("@Param" + index.ToString(), sqlObj.pkVal));
                    index++;
                }
                else if (!string.IsNullOrEmpty(sqlObj.whereString))
                {
                    sb.Append(" Where ").Append(sqlObj.whereString).Append(";");
                }
            }
            object _object = SqlHelper.ExecuteNonQueryTran(connStr, CommandType.Text, sb.ToString(), userID, paramList.ToArray());
            return (_object == DBNull.Value) ? 0 : Convert.ToInt32(_object);
        }
        /// <summary> 批量更新- 多条语句-事务
        /// </summary
        public static int BatchUpdateDataMultiByTrans(string connStr, IList<UpdateSQLObj> batUpdateList, UserIDOP userID)
        {
            StringBuilder sb = new StringBuilder();
            List<SqlParameter> paramList = new List<SqlParameter>();
            string str = string.Empty;
            int index = 0;
            foreach (UpdateSQLObj sqlObj in batUpdateList)
            {
                sb.Append(" Update ");
                sb.Append(sqlObj.TableName);
                sb.Append(" Set ");
                bool isFirstValue = true;
                foreach (string key in sqlObj.DicParam.Keys)
                {
                    if (isFirstValue)
                    {
                        isFirstValue = false;
                        sb.Append(key);
                        sb.Append("=");
                        sb.Append("@Param" + index.ToString());
                    }
                    else
                    {
                        sb.Append("," + key);
                        sb.Append("=");
                        sb.Append("@Param" + index.ToString());
                    }
                    paramList.Add(new SqlParameter("@Param" + index.ToString(), sqlObj.DicParam[key]));
                    index++;
                }
                if (sqlObj.pkName != null)
                {
                    sb.Append(" Where ").Append(sqlObj.pkName).Append("=").Append("@Param" + index.ToString()).Append(";");
                    paramList.Add(new SqlParameter("@Param" + index.ToString(), sqlObj.pkVal));
                    index++;
                }
                else if (!string.IsNullOrEmpty(sqlObj.whereString))
                {
                    sb.Append(" Where ").Append(sqlObj.whereString).Append(";");
                }
            }
            object _object = SqlHelper.ExecuteNonQueryTran(connStr, CommandType.Text, sb.ToString(), userID, paramList.ToArray());
            return (_object == DBNull.Value) ? 0 : Convert.ToInt32(_object);
        }
        /// <summary> 批量操作- 多条语句
        /// </summary
        public static bool BatchExeSQLMultiByTrans(string connString, IList<SQLObj> batSQLObjList, UserIDOP userID)
        {
            bool result = false;
            StringBuilder sb = new StringBuilder();
            SqlParameter[] _params = null;
            foreach (SQLObj sqlObj in batSQLObjList)
            {
                _params = null;
                _params = SqlHelper.GetParameter(sqlObj.Param);

                string modifyCmdText = string.Empty;
                bool modifyFlag = SQLHelper_UpdateBackup.TryUpdateSQLBackup(connString, sqlObj.sqlString, out modifyCmdText, _params);
                if (modifyFlag)
                {
                    sqlObj.sqlString = modifyCmdText;
                }
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlTransaction trans = null;
                try
                {
                    Open(conn);
                    trans = conn.BeginTransaction();
                    foreach (SQLObj sqlObj in batSQLObjList)
                    {
                        _params = SqlHelper.GetParameter(sqlObj.Param);
                        SqlHelper.ExecuteNonQueryTranNoBak(conn, trans, sqlObj.cmdType, sqlObj.sqlString, userID, _params);
                    }
                    trans.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    result = false;
                    throw ex;
                }
            }
            return result;
        }
        /// <summary> 返回执行结果;无备份操作记录功能,无SQL日志操作记录功能带事务,一批语句全部执行成功，则返回True。否则，返回为False;
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <param name="batSQLStringList">SQ语句IList数组,每一项为一条SQL语句</param>
        /// <param name="userID">UserIDOP对象</param>
        /// <returns>一批SQL语句带事务的执行结果，成功Or失败</returns>
        public static bool BatchExeSQLStrMultiByTransNoLogNoBackup(string connStr, IList<SysSQLString> batSQLStringList, UserIDOP userID)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                foreach (SysSQLString sqlString in batSQLStringList)
                {
                    SqlParameter[] paras = GetParameter(sqlString.Param);
                    ExecuteNonQueryNoBak(connStr, sqlString.cmdType, sqlString.sqlString, userID, paras);
                }
                result = true;
            }
            return result;
        }
        /// <summary> 批量更新- 多条语句
        /// </summary
        public static bool BatchExeSQLMultiByTrans(string connString, IList<SysSQLString> batSQLStringList, UserIDOP userID)
        {
            bool result = false;
            StringBuilder sb = new StringBuilder();
            SqlParameter[] _params = null;
            foreach (SysSQLString sqlObj in batSQLStringList)
            {
                _params = null;
                _params = SqlHelper.GetParameter(sqlObj.Param);
                string modifyCmdText = string.Empty;
                bool modifyFlag = SQLHelper_UpdateBackup.TryUpdateSQLBackup(connString, sqlObj.sqlString, out modifyCmdText, _params);
                if (modifyFlag)
                {
                    sqlObj.sqlString = modifyCmdText;
                }
            }


            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlTransaction trans = null;
                try
                {
                    Open(conn);
                    trans = conn.BeginTransaction();
                    foreach (SysSQLString sqlString in batSQLStringList)
                    {
                        _params = SqlHelper.GetParameter(sqlString.Param);
                        SqlHelper.ExecuteNonQueryTranNoBak(conn, trans, sqlString.cmdType, sqlString.sqlString, userID, _params);
                    }
                    trans.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    result = false;
                    throw ex;
                }
            }
            return result;
        }

        /// <summary> SQLBulk方式操作带事务无操作备份
        /// </summary>
        /// <param name="connStr">数据库连接串</param>
        /// <param name="tableName">表名</param>
        /// <param name="listRow">DataRowList集合</param>
        /// <returns>SQLBulk批处理方式操作带事务，执行结果。全部成功，返回Bool True; 否则，返回False。</returns>
        public static bool SqlBulkByTransNoLogNoBackUp(string connStr, string tableName, List<DataRow> listRow)
        {
            if (listRow.Count == 0)
            {
                return true;
            }
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.CheckConstraints, tran))
                {
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.BulkCopyTimeout = 300;
                    DataTable dt = listRow[0].Table;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                    }
                    try
                    {
                        bulkCopy.WriteToServer(listRow.ToArray());
                        tran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        bulkCopy.Close();
                        tran.Dispose();
                    }
                }
            }
        }
        #endregion
        #endregion

        #region 查询
        #region 基类方法
        /// <summary> 返回数据内容（SqlDataReader）
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="paras">command执行的SqlParameter[]数组</param>
        /// <returns>SqlDataReader 返回执行后的数据内容</returns>
        public static SqlDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, UserIDOP userID, params SqlParameter[] paras)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connString);
            PrepareCommand(cmd, conn, cmdType, cmdText, paras);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return rdr;

        }

        /// <summary> 返回首行首列数据
        /// </summary>
        /// <param name="connection">SqlConnection类型</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="paras">command执行的SqlParameter[]数组</param>
        /// <returns>int 返回command执行响应的行数</returns>
        public static object ExecuteScalar(string connString, CommandType cmdType, string cmdText, UserIDOP userID, params SqlParameter[] paras)
        {
            object obj = null;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(cmd, conn, cmdType, cmdText, paras);
                    obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }
            }
            return obj;
        }

        /// <summary> 返回首行首列数据
        /// </summary>
        /// <param name="connection">SqlConnection类型</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="paras">command执行的SqlParameter[]数组</param>
        /// <returns>int 返回command执行响应的行数</returns>
        public static object ExecuteScalar_Backup(string connString, CommandType cmdType, string cmdText, UserIDOP userID, params SqlParameter[] paras)
        {
            object obj = null;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(cmd, conn, cmdType, cmdText, paras);
                    obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }
            }
            return obj;
        }

        /// <summary> 返回首行首列数据
        /// </summary>
        /// <param name="connection">SqlConnection类型</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="paras">command执行的SqlParameter[]数组</param>
        /// <returns>int 返回command执行响应的行数</returns>
        public static object ExecuteScalarByTrans(string connString, CommandType cmdType, string cmdText, UserIDOP userID, params SqlParameter[] paras)
        {
            object obj = null;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(cmd, conn, cmdType, cmdText, paras);
                    obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }
            }
            return obj;
        }

        /// <summary> 返回数据内容（DataSet）
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="paras">command执行的SqlParameter[]数组</param>
        /// <returns>DataSet 返回执行后的数据内容</returns>
        public static DataSet ExecuteDataSetByDicParams(string connString, CommandType cmdType, string cmdText, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            SqlParameter[] myParamArray = GetParameter(dic);
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(cmd, conn, cmdType, cmdText, myParamArray);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds, "table");

                    cmd.Parameters.Clear();
                    sda.Dispose();

                }
            }
            return ds;
        }

        /// <summary> 返回数据内容（DataSet）
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="paras">command执行的SqlParameter[]数组</param>
        /// <returns>DataSet 返回执行后的数据内容</returns>
        public static DataSet ExecuteDataSet(string connString, CommandType cmdType, string cmdText, UserIDOP userID, params SqlParameter[] paras)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(cmd, conn, cmdType, cmdText, paras);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds, "table");
                    cmd.Parameters.Clear();
                    sda.Dispose();
                }
            }
            return ds;
        }

        /// <summary> 返回数据内容（DataSet）
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="cmdType">执行类型（过程/语句）</param>
        /// <param name="cmdText">执行的过程名称/查询语句</param>
        /// <param name="paras">command执行的SqlParameter[]数组</param>
        /// <returns>DataSet 返回执行后的数据内容</returns>
        public static DataSet ExecuteDataSet_Backup(string connString, CommandType cmdType, string cmdText, UserIDOP userID, params SqlParameter[] paras)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(cmd, conn, cmdType, cmdText, paras);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds, "table");
                    cmd.Parameters.Clear();
                    sda.Dispose();
                }
            }
            return ds;
        }
        #endregion

        #region 外层方法
        /// <summary> 执行数据操作
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdType">command类型</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="dic">参数</param>
        /// <param name="userID">用户id</param>
        /// <returns>返回object对象</returns>
        public static object ExecuteNonQueryReturnObjectNoTrans(string connStr, CommandType cmdType, string cmdText, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            return ExecuteScalar(connStr, cmdType, cmdText, userID, GetParameter(dic));
        }

        /// <summary> 判断数据是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段主键</param>
        /// <param name="pkVal">字段值</param>
        /// <returns></returns>
        public static int IsExist(string connString, string tableName, string pkName, string pkVal, UserIDOP userID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select Count(1) from " + tableName + "");
            strSql.Append(" where " + pkName + " = @" + pkName + "").Append(";"); ;
            SqlParameter[] param = { new SqlParameter("@" + pkName + "", pkVal) };
            return CommonHelper.GetInt(SqlHelper.ExecuteScalar(connString, CommandType.Text, strSql.ToString(), userID, param));
        }
        /// <summary> 查询单张表
        /// </summary>
        public static DataSet SelOPSingleTable(string conStr, IList<string> Fields, string tableName, string whereString, string AdditionalConditions, UserIDOP userID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select ");
            for (int i = 0; i < Fields.Count; i++)
            {
                if (i > (Fields.Count - 2))
                {
                    strSql.Append(Fields[i]);
                }
                else
                {
                    strSql.Append(Fields[i] + ", ");
                }
            }
            strSql.Append(" from " + tableName + " ");
            if (!string.IsNullOrEmpty(whereString))
            {
                strSql.Append(" where " + whereString + " ");
            }
            if (!string.IsNullOrEmpty(AdditionalConditions))
            {
                strSql.Append(" " + AdditionalConditions + " ");
            }
            strSql.Append(";");
            DataSet ds = ExecuteDataSet(conStr, CommandType.Text, strSql.ToString(), userID, null);
            return ds;
        }

        /// <summary> 查询单张表 前n条
        /// </summary>
        public static DataSet SelTopOPSingleTable(string conStr, int TopNum, IList<string> Fields, string tableName, string whereString, string AdditionalConditions, UserIDOP userID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select top(" + TopNum.ToString() + ")");
            for (int i = 0; i < Fields.Count; i++)
            {
                if (i > (Fields.Count - 2))
                {
                    strSql.Append(Fields[i]);
                }
                else
                {
                    strSql.Append(Fields[i] + ", ");
                }
            }
            strSql.Append(" from " + tableName + " ");
            if (!string.IsNullOrEmpty(whereString))
            {
                strSql.Append(" where " + whereString + " ");
            }
            if (!string.IsNullOrEmpty(AdditionalConditions))
            {
                strSql.Append(" " + AdditionalConditions + " ");
            }
            strSql.Append(";");
            return ExecuteDataSet(conStr, CommandType.Text, strSql.ToString(), userID, null);
        }

        /// <summary> 获取数据表所有数据
        /// </summary>
        /// <param name="TargetTable">目标表名</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataTable(string connString, string TargetTable, UserIDOP userID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM " + TargetTable + "");
            return SqlHelper.ExecuteDataSet(connString, CommandType.Text, sql.ToString(), userID, null).Tables[0];
        }
        /// <summary> 根据唯一ID获取Dictionary<string,string>
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段主键</param>
        /// <param name="pkVal">字段值</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetHashtableById(string connString, string tableName, string pkName, string pkVal, UserIDOP userID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select * From ").Append(tableName).Append(" Where ").Append(pkName).Append("=@ID");
            DataTable dt = SqlHelper.ExecuteDataSet(connString, CommandType.Text, sb.ToString(), userID, new SqlParameter[] { new SqlParameter("@ID", pkVal) }).Tables[0];
            return DataTableHelper.DataTableToHashtable(dt);
        }
        /// <summary> 获取最大数字编号<string,string>
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段</param>
        /// <param name="pkVal">字段值</param>
        /// <returns></returns>
        public static int GetMaxNum(string connString, string tableName, string pkName, UserIDOP userID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select Max(").Append(pkName).Append(") From ").Append(tableName);
            object _object = SqlHelper.ExecuteScalarByTrans(connString, CommandType.Text, sb.ToString(), userID, null);
            return (_object == DBNull.Value) ? 0 : Convert.ToInt32(_object);
        }

        /// <summary> 获取数据
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdType">command类型</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="dic">参数</param>
        /// <param name="userID">用户id</param>
        /// <returns>返回数据集</returns>
        public static DataSet ExecuteNonQueryReturnDataSetNoTrans(string connStr, CommandType cmdType, string cmdText, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            return ExecuteDataSetByDicParams(connStr, cmdType, cmdText, dic, userID);
        }
        #endregion
        #endregion
        #endregion

        /// <summary> 释放资源
        /// </summary>
        public static void Dispose(SqlConnection con)
        {
            // 确认连接是否已经关闭
            if (con != null)
            {
                con.Dispose();
                con = null;
            }
        }
    }
}
