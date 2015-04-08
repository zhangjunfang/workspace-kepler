using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SYSModel;
namespace BLL
{
    public class DbCommon
    {
        private static DbCommon _instance = null;
        // Creates an syn object.
        private static readonly object SynObject = new object();
        DbCommon()
        {
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
        //private static HYH.IDAL.ICommon dal = HYH.DALFactory.CommonDALAccess.Common();
        private static readonly Object locker = new Object();
        private static IDAL.IDbCommon dal = null;
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
                dal = null;
                _instance = null;
                //GC.SuppressFinalize(this);
            }
            _alreadyDisposed = true;
        }

        private static IDAL.IDbCommon Dal
        {
            get
            {
                if (dal == null)
                {
                    lock (locker)
                    {
                        if (dal == null)
                        {
                            dal = DALFactory.CommonDALAccess.DbCommon();
                        }
                    }
                }
                return dal;
            }
        }

        public DataSet SelOPSingleTable(string conStr, IList<string> Fields, string tableName, string whereString, string AdditionalConditions, UserIDOP userID)
        {
            return Dal.SelOPSingleTable(conStr, Fields, tableName, whereString, AdditionalConditions, userID);
        }
        public DataSet SelTopOPSingleTable(string conStr, int TopNum, IList<string> Fields, string tableName, string whereString, string AdditionalConditions, UserIDOP userID)
        {
            return Dal.SelTopOPSingleTable(conStr, TopNum, Fields, tableName, whereString, AdditionalConditions, userID);
        }
        public int Exists(string sql, string connStr, UserIDOP userID)
        {
            return Dal.Exists(sql, connStr, userID);
        }

        public int ExtNonQuery(SQLObj obj, string connStr, UserIDOP userID)
        {
            return Dal.ExtNonQuery(obj, connStr, userID);
        }
        public int ExtNonQuery(string sqlStr, string connStr, UserIDOP userID)
        {
            return Dal.ExtNonQuery(sqlStr, connStr, userID);
        }
        public int ExtNonQueryByTrans(string sql, string connStr, UserIDOP userID)
        {
            return Dal.ExtNonQueryByTrans(sql, connStr, userID);
        }

        public bool ExecuteNonQueryReturnBoolNoTrans(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            return Dal.ExecuteNonQueryReturnBoolNoTrans(connStr, cmdType, sql, dic, userID);
        }
        public int ExecuteNonQueryReturnIntNoTrans(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            return Dal.ExecuteNonQueryReturnIntNoTrans(connStr, cmdType, sql, dic, userID);
        }
        public object ExecuteNonQueryReturnObjectNoTrans(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            return Dal.ExecuteNonQueryReturnObjectNoTrans(connStr, cmdType, sql, dic, userID);
        }
        public DataSet ExecuteNonQueryReturnDataSetNoTrans(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            return Dal.ExecuteNonQueryReturnDataSetNoTrans(connStr, cmdType, sql, dic, userID);
        }

        public DataSet getDataSet(string connStr, CommandType cmdType, string sql, UserIDOP userID)
        {
            return Dal.getDataSet(connStr, cmdType, sql, userID);
        }

        public DataSet getDataSetByDicParams(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            return Dal.getDataSetByDicParams(connStr, cmdType, sql, dic, userID);
        }

        public SqlDataReader getDataReader(string sql, string connStr, UserIDOP userID)
        {
            return Dal.getDataReader(sql, connStr, userID);
        }

        public DataSet GetListTables(string conStr, int pageIndex, int pageSize, string tableSource, string strWhere, string OrderExpression, string Fields, UserIDOP userID, ref int Counts)
        {
            return Dal.GetListTables(conStr, pageIndex, pageSize, tableSource, strWhere, OrderExpression, Fields, userID, ref Counts);
        }

        public int GetLsh(string connStr, ref Int64 Orderlsh)
        {
            return Dal.GetLsh(connStr, ref Orderlsh);
        }

        public bool Submit_AddOrEdit(string connStr, string tableName, string pkName, string pkVal, Dictionary<string, string> ht, UserIDOP userID)
        {
            return Dal.Submit_AddOrEdit(connStr, tableName, pkName, pkVal, ht, userID);
        }

        public bool Submit_AddOrEditLog(UserIDOP userID, string connStr, string tableName, string pkName, string pkVal, Dictionary<string, string> ht)
        {
            return Dal.Submit_AddOrEditLog(userID, connStr, tableName, pkName, pkVal, ht);
        }

        public bool DeleteDataByID(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID)
        {
            return Dal.DeleteData(connStr, tableName, pkName, pkVal, userID) > 0 ? true : false;
        }
        public int BatchDeleteDataByIn(string connStr, string tableName, string pkName, object[] pkValues, UserIDOP userID)
        {
            return Dal.BatchDeleteDataByIn(connStr, tableName, pkName, pkValues, userID);
        }
        public int BatchUpdateDataByIn(string connStr, string tableName, Dictionary<string, string> ht, string pkName, object[] pkValues, UserIDOP userID)
        {
            return Dal.BatchUpdateDataByIn(connStr, tableName, ht, pkName, pkValues, userID);
        }

        public int BatchUpdateDataByWhere(string connStr, string tableName, Dictionary<string, string> ht, string whereString, UserIDOP userID)
        {
            return Dal.BatchUpdateDataByWhere(connStr, tableName, ht, whereString, userID);
        }
        public int BatchUpdateDataMulti(string connStr, IList<UpdateSQLObj> batUpdateList, UserIDOP userID)
        {
            return Dal.BatchUpdateDataMulti(connStr, batUpdateList, userID);
        }
        public int BatchUpdateDataMultiByTrans(string connStr, IList<UpdateSQLObj> batUpdateList, UserIDOP userID)
        {
            return Dal.BatchUpdateDataMultiByTrans(connStr, batUpdateList, userID);
        }
        public bool BatchExeSQLMultiByTrans(string connStr, IList<SQLObj> batSQLObjList, UserIDOP userID)
        {
            return Dal.BatchExeSQLMultiByTrans(connStr, batSQLObjList, userID);
        }
        public bool BatchExeSQLMultiByTrans(string connStr, IList<SysSQLString> batSQLStringList, UserIDOP userID)
        {
            return Dal.BatchExeSQLMultiByTrans(connStr, batSQLStringList, userID);
        }
        public bool ExecuteNonQueryReturnBoolByTrans(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            return Dal.ExecuteNonQueryReturnBoolByTrans(connStr, cmdType, sql, dic, userID);
        }
        public int BatchDeleteDataByWhere(string connStr, string tableName, string whereString, UserIDOP userID)
        {
            return Dal.BatchDeleteDataByWhere(connStr, tableName, whereString, userID);
        }
        public int DeleteData(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID)
        {
            return Dal.DeleteData(connStr, tableName, pkName, pkVal, userID);
        }
        public DataTable GetDataTable(string connStr, string tableName, UserIDOP userID)
        {
            return Dal.GetDataTable(connStr, tableName, userID);
        }
        public Dictionary<string, string> GetHashtableById(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID)
        {
            return Dal.GetHashtableById(connStr, tableName, pkName, pkVal, userID);
        }
        public int IsExist(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID)
        {
            return Dal.IsExist(connStr, tableName, pkName, pkVal, userID);
        }
        public int GetMaxNum(string connStr, string tableName, string pkName, UserIDOP userID)
        {
            return Dal.GetMaxNum(connStr, tableName, pkName, userID);
        }
        public bool ExtNonQueryByTrans(string connStr, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
            return Dal.ExtNonQueryByTrans(connStr, sql, dic, userID);
        }
        public DataSet GetListPageStoreProcedure(string conStr, int page, int pageSize, string tableSource, string whereValue, string OrderExpression, string Fields, UserIDOP userID, ref int Counts)
        {
            return Dal.GetListPageStoreProcedure(conStr, page, pageSize, tableSource, whereValue, OrderExpression, Fields, userID, ref Counts);
        }
        public bool BatchExeSQLStrMultiByTransNoLogNoBackup(string connStr, IList<SysSQLString> batSQLStringList, UserIDOP userID)
        {
            return Dal.BatchExeSQLStrMultiByTransNoLogNoBackup(connStr, batSQLStringList, userID);
        }
        public bool SqlBulkByTransNoLogNoBackUp(string connStr, string tableName, List<DataRow> listRow)
        {
            return Dal.SqlBulkByTransNoLogNoBackUp(connStr, tableName, listRow);
        }
    }
}

