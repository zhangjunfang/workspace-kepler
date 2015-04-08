using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using HXCCommon.DotNetCode;
using HXCCommon.DotNetData;
namespace DBUtility
{
   public class MySqlHelper
    {
           /// <summary>
           /// 为command添加执行内容
           /// </summary>
           /// <param name="cmd">MySqlCommand类型</param>
           /// <param name="conn">MySqlConnection类型</param>
           /// <param name="trans">MySqlTransaction要处理的事物</param>
           /// <param name="cmdType">执行类型（过程/语句）</param>
           /// <param name="cmdText">执行的过程名称/查询语句</param>
           /// <param name="cmdParms">ommand执行的MySqlParameter[]数组</param>
           private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
           {
               if (conn.State != ConnectionState.Open)
                   Open(conn);
               cmd.Connection = conn;
               cmd.CommandText = cmdText;

               if (trans != null)
                   cmd.Transaction = trans;

               cmd.CommandType = cmdType;

               if (cmdParms != null)
               {
                   foreach (MySqlParameter parm in cmdParms)
                       cmd.Parameters.Add(parm);
               }
           }

           /// <summary>
           /// 返回执行响应的行数
           /// </summary>
           /// <param name="connectionString">数据库连接字符串</param>
           /// <param name="cmdType">执行类型（过程/语句）</param>
           /// <param name="cmdText">执行的过程名称/查询语句</param>
           /// <param name="commandParameters">command执行的MySqlParameter[]数组</param>
           /// <returns>int 返回command执行响应的行数</returns>
           public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
           {
               MySqlCommand cmd = new MySqlCommand();
               using (MySqlConnection conn = new MySqlConnection(connectionString))
               {
                   PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                   int val = cmd.ExecuteNonQuery();
                   cmd.Parameters.Clear();
                   Close(conn);
                   Dispose(conn);
                   return val;
               }
           }

           /// <summary>
           /// 返回执行响应的行数
           /// </summary>
           /// <param name="connection">MySqlConnection类型</param>
           /// <param name="cmdType">执行类型（过程/语句）</param>
           /// <param name="cmdText">执行的过程名称/查询语句</param>
           /// <param name="commandParameters">command执行的MySqlParameter[]数组</param>
           /// <returns>int 返回command执行响应的行数</returns>
           public static int ExecuteNonQuery(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
           {

               MySqlCommand cmd = new MySqlCommand();
               PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
               int val = cmd.ExecuteNonQuery();
               cmd.Parameters.Clear();
               Close(connection);
               Dispose(connection);
               return val;
           }

           /// <summary>
           /// 返回执行响应的行数
           /// </summary>
           /// <param name="trans">command中执行的事物（MySqlTransaction）</param>
           /// <param name="cmdType">执行类型（过程/语句）</param>
           /// <param name="cmdText">执行的过程名称/查询语句</param>
           /// <param name="commandParameters">command执行的MySqlParameter[]数组</param>
           /// <returns>int 返回command执行响应的行数</returns>
           public static int ExecuteNonQuery(MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
           {
               int val = 0;
               MySqlCommand cmd = new MySqlCommand();
               try
               {
                   PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
                   val = cmd.ExecuteNonQuery();
                   cmd.Parameters.Clear();
                   //Close(trans.Connection);
                   //Dispose(trans.Connection);
                   trans.Commit();
                   return val;
               }
               catch
               {
                   trans.Rollback();
                   return val;
               }
           }

           /// <summary>
           /// 返回数据内容（MySqlDataReader）
           /// </summary>
           /// <param name="connectionString">数据库连接字符串</param>
           /// <param name="cmdType">执行类型（过程/语句）</param>
           /// <param name="cmdText">执行的过程名称/查询语句</param>
           /// <param name="commandParameters">command执行的MySqlParameter[]数组</param>
           /// <returns>MySqlDataReader 返回执行后的数据内容</returns>
           public static MySqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
           {
               MySqlCommand cmd = new MySqlCommand();
               MySqlConnection conn = new MySqlConnection(connectionString);
               try
               {
                   PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                   MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                   cmd.Parameters.Clear();
                   return rdr;
               }
               catch
               {
                   //Close(conn);
                   //Dispose(conn);
                   throw;
               }
           }

           /// <summary>
           /// 返回首行首列数据
           /// </summary>
           /// <param name="connection">MySqlConnection类型</param>
           /// <param name="cmdType">执行类型（过程/语句）</param>
           /// <param name="cmdText">执行的过程名称/查询语句</param>
           /// <param name="commandParameters">command执行的MySqlParameter[]数组</param>
           /// <returns>int 返回command执行响应的行数</returns>
           public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
           {
               MySqlCommand cmd = new MySqlCommand();

               MySqlConnection conn = new MySqlConnection(connectionString);

               try
               {
                   PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                   object obj = cmd.ExecuteScalar();
                   cmd.Parameters.Clear();
                   return obj;
               }
               catch
               {
                   throw;
               }
               finally
               {
                   Close(conn);
                   Dispose(conn);
               }
           }

           /// <summary>
           /// 返回数据内容（DataSet）
           /// </summary>
           /// <param name="connectionString">数据库连接字符串</param>
           /// <param name="cmdType">执行类型（过程/语句）</param>
           /// <param name="cmdText">执行的过程名称/查询语句</param>
           /// <param name="commandParameters">command执行的MySqlParameter[]数组</param>
           /// <returns>DataSet 返回执行后的数据内容</returns>
           public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
           {
               MySqlCommand cmd = new MySqlCommand();
               MySqlConnection conn = new MySqlConnection(connectionString);
               try
               {
                   PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                   DataSet ds = new DataSet();
                   MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                   sda.Fill(ds, "table");
                   return ds;
               }
               catch
               {
                   throw;
               }
               finally
               {
                   Close(conn);
                   Dispose(conn);
               }
           }

           #region 对象参数转换
           /// <summary>
           /// 对象参数转换
           /// </summary>
           /// <param name="ht"></param>
           /// <returns></returns>
           public static MySqlParameter[] GetParameter(Dictionary<string, string> ht)
           {
               MySqlParameter[] _params = new MySqlParameter[ht.Count];
               int i = 0;
               foreach (string key in ht.Keys)
               {
                   _params[i] = new MySqlParameter("?" + key, ht[key]);
                   i++;
               }
               return _params;
           }
           #endregion

           /// <summary>
           /// 通过Dictionary插入数据
           /// </summary>
           /// <param name="tableName">表名</param>
           /// <param name="ht">Hashtable</param>
           /// <returns>int</returns>
           public static int InsertByHashtable(string connectionString, string tableName, Dictionary<string, string> ht)
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
                   sp.Append(",?" + key);
               }
               sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
               sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")");
               int _object = MySqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, sb.ToString(), MySqlHelper.GetParameter(ht));
               return _object;
           }

           /// <summary>
           /// 判断数据是否存在
           /// </summary>
           /// <param name="tableName">表名</param>
           /// <param name="pkName">字段主键</param>
           /// <param name="pkVal">字段值</param>
           /// <returns></returns>
           public static int IsExist(string connectionString, string tableName, string pkName, string pkVal)
           {
               StringBuilder strSql = new StringBuilder();
               strSql.Append("Select Count(1) from " + tableName + "");
               strSql.Append(" where " + pkName + " = ?" + pkName + "");
               MySqlParameter[] param = {
                                         new MySqlParameter("?"+pkName+"",pkVal)};
               return CommonHelper.GetInt(MySqlHelper.ExecuteScalar(connectionString, CommandType.Text, strSql.ToString(), param));
           }

           /// <summary>
           /// 删除数据
           /// </summary>
           /// <param name="tableName">表名</param>
           /// <param name="pkName">字段主键</param>
           /// <param name="pkVal">字段值</param>
           /// <returns></returns>
           public static int DeleteData(string connectionString, string tableName, string pkName, string pkVal)
           {
               StringBuilder sb = new StringBuilder("Delete From " + tableName + " Where " + pkName + " = ?ID");
               return MySqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, sb.ToString(), new MySqlParameter[] { new MySqlParameter("?ID", pkVal) });
           }

           /// <summary>
           /// 批量删除
           /// </summary>
           /// <param name="tableName">表名</param>
           /// <param name="pkName">字段主键</param>
           /// <param name="pkVal">字段值</param>
           /// <returns></returns>
           public int BatchDeleteData(string connectionString, string tableName, string pkName, object[] pkValues)
           {
               MySqlParameter[] param = new MySqlParameter[pkValues.Length];
               int index = 0;
               string str = ":ID" + index;
               StringBuilder sql = new StringBuilder("DELETE FROM " + tableName + " WHERE " + pkName + " IN (");
               for (int i = 0; i < (param.Length - 1); i++)
               {
                   object obj2 = pkValues[i];
                   str = ":ID" + index;
                   sql.Append(str).Append(",");
                   param[index] = new MySqlParameter(str, obj2);
                   index++;
               }
               str = ":ID" + index;
               sql.Append(str);
               param[index] = new MySqlParameter(str, pkValues[index]);
               sql.Append(")");
               return MySqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, sql.ToString(), param);
           }

           /// <summary>
           /// 表单提交：新增，修改
           ///     参数：
           ///     tableName:表名
           ///     pkName：字段主键
           ///     pkVal：字段值
           ///     ht：参数
           /// </summary>
           /// <returns></returns>
           public static bool Submit_AddOrEdit(string connectionString, string tableName, string pkName, string pkVal, Dictionary<string, string> ht)
           {
               if (string.IsNullOrEmpty(pkVal))
               {
                   if (MySqlHelper.InsertByHashtable(connectionString, tableName, ht) > 0)
                       return true;
                   else
                       return false;
               }
               else
               {
                   if (MySqlHelper.UpdateByHashtable(connectionString, tableName, pkName, pkVal, ht) > 0)
                       return true;
                   else
                       return false;
               }
           }

           /// <summary>
           /// 通过Hashtable修改数据
           /// </summary>
           /// <param name="tableName">表名</param>
           /// <param name="pkName">字段主键</param>
           /// <param name="pkValue"></param>
           /// <param name="ht">Hashtable</param>
           /// <returns>int</returns>
           public static int UpdateByHashtable(string connectionString, string tableName, string pkName, string pkVal, Dictionary<string, string> ht)
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
                       sb.Append("?" + key);
                   }
                   else
                   {
                       sb.Append("," + key);
                       sb.Append("=");
                       sb.Append("?" + key);
                   }
               }
               sb.Append(" Where ").Append(pkName).Append("=").Append("?" + pkName);
               ht[pkName] = pkVal;
               MySqlParameter[] _params = MySqlHelper.GetParameter(ht);
               object _object = MySqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, sb.ToString(), _params);
               return (_object == DBNull.Value) ? 0 : Convert.ToInt32(_object);
           }
           /// <summary>
           /// 获取数据表
           /// </summary>
           /// <param name="TargetTable">目标表名</param>
           /// <returns>DataTable</returns>
           public DataTable GetDataTable(string connectionString, string TargetTable)
           {
               StringBuilder sql = new StringBuilder();
               sql.Append("SELECT * FROM " + TargetTable + "");
               return MySqlHelper.ExecuteDataSet(connectionString, CommandType.Text, null).Tables[0];
           }

           /// <summary>
           /// 根据唯一ID获取Dictionary<string,string>
           /// </summary>
           /// <param name="tableName">表名</param>
           /// <param name="pkName">字段主键</param>
           /// <param name="pkVal">字段值</param>
           /// <returns></returns>
           public Dictionary<string, string> GetHashtableById(string connectionString, string tableName, string pkName, string pkVal)
           {
               StringBuilder sb = new StringBuilder();
               sb.Append("Select * From ").Append(tableName).Append(" Where ").Append(pkName).Append("=?ID");
               DataTable dt = MySqlHelper.ExecuteDataSet(connectionString, CommandType.Text, sb.ToString(), new MySqlParameter[] { new MySqlParameter("?ID", pkVal) }).Tables[0];
               return DataTableHelper.DataTableToHashtable(dt);
           }

           /// <summary>
           /// 获取最大数字编号<string,string>
           /// </summary>
           /// <param name="tableName">表名</param>
           /// <param name="pkName">字段</param>
           /// <param name="pkVal">字段值</param>
           /// <returns></returns>
           public static int GetMaxNum(string connectionString, string tableName, string pkName)
           {
               StringBuilder sb = new StringBuilder();
               sb.Append("Select Max(").Append(pkName).Append(") From ").Append(tableName);
               object _object = MySqlHelper.ExecuteScalar(connectionString, CommandType.Text, sb.ToString(), null);
               return (_object == DBNull.Value) ? 0 : Convert.ToInt32(_object);
           }

           /// <summary>
           /// 关闭数据库连接
           /// </summary>
           public static void Close(MySqlConnection con)
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

           // 打开数据库连接
           private static void Open(MySqlConnection con)
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
                       throw new Exception(ex.Message);
                   }
                   finally
                   {
                       ///关闭已经打开的数据库连接				
                   }
               }
           }

           /// <summary>
           /// 释放资源
           /// </summary>
           public static void Dispose(MySqlConnection con)
           {
               // 确认连接是否已经关闭
               if (con != null)
               {
                   con.Close();
                   con.Dispose();
                   con = null;
               }
           }
       }
    }
