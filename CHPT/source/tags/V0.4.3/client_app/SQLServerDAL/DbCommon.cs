using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using SYSModel;
namespace SQLServerDAL
{
    /// <summary>
    /// 切记：引用完类组件后，一定要调用此方法来释放！
    /// </summary>
    public class SQLServerDALDispose
    {
        /// <summary>
        /// 切记：引用完类组件后，一定要调用此方法来释放！
        /// </summary>
        public static void Dispose()
        {
            DbCommon.Dispose();
        }
    }

    public class DbCommon : IDAL.IDbCommon
    {

        private static DbCommon _instance = null;
        // Creates an syn object.
        private static readonly object SynObject = new object();
        static DbCommon()
        {
            _instance = new DbCommon(); ;
        }
        public static DbCommon Instance
        {
            get
            {
                // Double-Checked Locking
                if (null == _instance)
                {
                    lock (SynObject)
                    {
                        if (null == _instance)
                        {
                            _instance = new DbCommon();
                        }
                    }
                }
                return _instance;
            }
        }


        private static readonly Object locker = new Object();
        private static DbCommon comn = null;

        private static bool _alreadyDisposed = false;
        ~DbCommon()
        {
            Dispose(true);
        }
        /// <summary>
        /// 切记：引用完类组件后，一定要调用此方法来释放！
        /// </summary>
        public static void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="isDisposing">如果应释放托管资源，为 true；否则为 false</param>
        protected static void Dispose(bool isDisposing)
        {
            if (_alreadyDisposed)
                return;
            if (isDisposing)
            {
                comn = null;
                _instance = null;
                //GC.SuppressFinalize(this);
            }
            _alreadyDisposed = true;
        }

        public static DbCommon Comn
        {
            get
            {
                if (comn == null)
                {
                    lock (locker)
                    {
                        if (comn == null)
                        {
                            comn = new DbCommon();
                        }
                    }
                }
                return comn;
            }
        }
        public DataSet SelOPSingleTable(string conStr, IList<string> Fields, string tableName, string whereString, string AdditionalConditions, UserIDOP userID)
        {
            return DBUtility.SqlHelper.SelOPSingleTable(conStr, Fields, tableName, whereString, AdditionalConditions, userID);
        }
        public DataSet SelTopOPSingleTable(string conStr, int TopNum, IList<string> Fields, string tableName, string whereString, string AdditionalConditions, UserIDOP userID)
        {
            return DBUtility.SqlHelper.SelTopOPSingleTable(conStr, TopNum, Fields, tableName, whereString, AdditionalConditions, userID);
        }
        public int Exists(string sql, string connStr, UserIDOP userID)
        {
            object obj = DBUtility.SqlHelper.ExecuteScalar(connStr, CommandType.Text, sql, userID, null);
            return Convert.ToInt32(obj);
        }

        public int ExtNonQuery(string sql, string connStr, UserIDOP userID)
        {
            return DBUtility.SqlHelper.ExecuteNonQueryTran(connStr, CommandType.Text, sql, userID);
        }

        public int ExtNonQuery(SQLObj obj, string connStr, UserIDOP userID)
        {
            return DBUtility.SqlHelper.ExecuteNonQuery(connStr, obj, userID);
        }

        public bool ExecuteNonQueryReturnBoolNoTrans(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            return DBUtility.SqlHelper.ExecuteNonQueryReturnBoolNoTrans(connStr, cmdType, sql, dic, userID);
        }
        public int ExecuteNonQueryReturnIntNoTrans(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            return DBUtility.SqlHelper.ExecuteNonQueryReturnIntNoTrans(connStr, cmdType, sql, dic, userID);
        }
        public object ExecuteNonQueryReturnObjectNoTrans(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            return DBUtility.SqlHelper.ExecuteNonQueryReturnObjectNoTrans(connStr, cmdType, sql, dic, userID);
        }
        public DataSet ExecuteNonQueryReturnDataSetNoTrans(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            return DBUtility.SqlHelper.ExecuteNonQueryReturnDataSetNoTrans(connStr, cmdType, sql, dic, userID);
        }
        public int ExtNonQueryByTrans(string sql, string connStr, UserIDOP userID)
        {
            int val = 0;
            val = DBUtility.SqlHelper.ExecuteNonQueryTran(connStr, CommandType.Text, sql, userID, null);
            return val;
        }
        public DataSet getDataSet(string connStr, CommandType cmdType, string sql, UserIDOP userID)
        {
            DataSet ds = new DataSet();
            ds = DBUtility.SqlHelper.ExecuteDataSet(connStr, cmdType, sql, userID, null);
            return ds;
        }
        public bool ExecuteNonQueryReturnBoolByTrans(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            return DBUtility.SqlHelper.ExecuteNonQueryReturnBoolByTrans(connStr, cmdType, sql, dic, userID);
        }
        public bool BatchExeSQLStrMultiByTransNoLogNoBackup(string connStr, IList<SysSQLString> batSQLStringList, UserIDOP userID)
        {
            return DBUtility.SqlHelper.BatchExeSQLStrMultiByTransNoLogNoBackup(connStr, batSQLStringList, userID);
        }
        public DataSet getDataSetByDicParams(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            DataSet ds = new DataSet();
            ds = DBUtility.SqlHelper.ExecuteDataSetByDicParams(connStr, cmdType, sql, dic, userID);
            return ds;
        }

        //public DataSet getDataSetByDicParams(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID)
        //{
        //    DataSet ds = new DataSet();
        //    ds = DBUtility.SqlHelper.ExecuteDataSetByDicParams(connStr, cmdType, sql, dic, userID);
        //    return ds;
        //}

        public SqlDataReader getDataReader(string sql, string connStr, UserIDOP userID)
        {
            return DBUtility.SqlHelper.ExecuteReader(connStr, CommandType.Text, sql, userID, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex">页号，从0开始</param>
        /// <param name="pageSize">每页记录数(页尺寸)</param>
        /// <param name="Counts">查询到的记录数</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="TableSource">表名或视图</param>
        /// <param name="OrderExpression">排序表达式</param>
        /// <param name="Fields">查询字段列表</param>
        /// <returns></returns>
        public DataSet GetListTables(string conStr, int pageIndex, int pageSize, string tableSource, string strWhere, string OrderExpression, string Fields, UserIDOP userID, ref int Counts)
        {
            return ListPage.GetListPages(conStr, pageIndex, pageSize, tableSource, strWhere, OrderExpression, Fields, userID, ref Counts);
        }

        public int GetLsh(string connStr, ref Int64 Orderlsh)
        {
            return ListPage.GetLsh(connStr, ref Orderlsh);
        }
        public bool Submit_AddOrEdit(string connStr, string tableName, string pkName, string pkVal, Dictionary<string, string> ht, UserIDOP userID)
        {
            return DBUtility.SqlHelper.Submit_AddOrEdit(connStr, tableName, pkName, pkVal, ht, userID);
        }

        public bool Submit_AddOrEditLog(UserIDOP userID, string connStr, string tableName, string pkName, string pkVal, Dictionary<string, string> ht)
        {
            return DBUtility.SqlHelper.Submit_AddOrEditLog(userID, connStr, tableName, pkName, pkVal, ht);
        }

        public int DeleteData(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID)
        {
            return DBUtility.SqlHelper.DeleteData(connStr, tableName, pkName, pkVal, userID);
        }

        public int BatchDeleteDataByIn(string connStr, string tableName, string pkName, object[] pkValues, UserIDOP userID)
        {
            return DBUtility.SqlHelper.BatchDeleteData(connStr, tableName, pkName, pkValues, userID);
        }

        public int BatchUpdateDataByIn(string connStr, string tableName, Dictionary<string, string> ht, string pkName, object[] pkValues, UserIDOP userID)
        {
            return DBUtility.SqlHelper.BatchUpdateDataByIn(connStr, tableName, ht, pkName, pkValues, userID);
        }
        public int BatchUpdateDataByWhere(string connStr, string tableName, Dictionary<string, string> ht, string whereString, UserIDOP userID)
        {
            return DBUtility.SqlHelper.BatchUpdateDataByWhere(connStr, tableName, ht, whereString, userID);
        }
        public int BatchUpdateDataMulti(string connStr, IList<UpdateSQLObj> batUpdateList, UserIDOP userID)
        {
            return DBUtility.SqlHelper.BatchUpdateDataMulti(connStr, batUpdateList, userID);
        }
        public bool BatchExeSQLMultiByTrans(string connStr, IList<SQLObj> batSQLObjList, UserIDOP userID)
        {
            return DBUtility.SqlHelper.BatchExeSQLMultiByTrans(connStr, batSQLObjList, userID);
        }
        public bool BatchExeSQLMultiByTrans(string connStr, IList<SysSQLString> batSQLStringList, UserIDOP userID)
        {
            return DBUtility.SqlHelper.BatchExeSQLMultiByTrans(connStr, batSQLStringList, userID);
        }
        public int BatchUpdateDataMultiByTrans(string connStr, IList<UpdateSQLObj> batUpdateList, UserIDOP userID)
        {
            return DBUtility.SqlHelper.BatchUpdateDataMultiByTrans(connStr, batUpdateList, userID);
        }
        public int BatchDeleteDataByWhere(string connStr, string tableName, string whereString, UserIDOP userID)
        {
            return DBUtility.SqlHelper.BatchDeleteDataByWhere(connStr, tableName, whereString, userID);
        }

        public DataTable GetDataTable(string connStr, string tableName, UserIDOP userID)
        {
            return DBUtility.SqlHelper.GetDataTable(connStr, tableName, userID);
        }
        public Dictionary<string, string> GetHashtableById(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID)
        {
            return DBUtility.SqlHelper.GetHashtableById(connStr, tableName, pkName, pkVal, userID);
        }
        public int IsExist(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID)
        {
            return DBUtility.SqlHelper.IsExist(connStr, tableName, pkName, pkVal, userID);
        }
        public int GetMaxNum(string connStr, string tableName, string pkName, UserIDOP userID)
        {
            return DBUtility.SqlHelper.GetMaxNum(connStr, tableName, pkName, userID);
        }

        public bool ExtNonQueryByTrans(string connStr, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            SqlParameter[] commandParameters = DBUtility.SqlHelper.GetParameter(dic);
            return DBUtility.SqlHelper.ExecuteNonQueryReturnBool(connStr, CommandType.Text, sql, userID, commandParameters);
        }
        public DataSet GetListPageStoreProcedure(string conStr, int page, int pageSize, string tableSource, string whereValue, string OrderExpression, string Fields, UserIDOP userID, ref int Counts)
        {
            return ListPage.GetListPages(conStr, page, pageSize, tableSource, whereValue, OrderExpression, Fields, userID, ref Counts);
        }

        public bool SqlBulkByTransNoLogNoBackUp(string connStr, string tableName, List<DataRow> listRow)
        {
            return DBUtility.SqlHelper.SqlBulkByTransNoLogNoBackUp(connStr, tableName, listRow);
        }
    }
}

