using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SYSModel;
namespace IDAL
{
    public interface IDbCommon
    {
        DataSet SelOPSingleTable(string conStr, IList<string> Fields, string tableName, string whereString, string AdditionalConditions, UserIDOP userID);
        DataSet SelTopOPSingleTable(string conStr, int TopNum, IList<string> Fields, string tableName, string whereString, string AdditionalConditions, UserIDOP userID);
        int Exists(string sql, string connStr, UserIDOP userID);
        int ExtNonQuery(string sql, string connStr, UserIDOP userID);
        int ExtNonQuery(SQLObj obj, string connStr, UserIDOP userID);
        int ExtNonQueryByTrans(string sql, string connStr, UserIDOP userID);
        bool ExecuteNonQueryReturnBoolNoTrans(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID);
        int ExecuteNonQueryReturnIntNoTrans(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID);
        object ExecuteNonQueryReturnObjectNoTrans(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID);
        DataSet ExecuteNonQueryReturnDataSetNoTrans(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID);
        DataSet getDataSet(string connStr, CommandType cmdType, string sql, UserIDOP userID);
        DataSet getDataSetByDicParams(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID);
        SqlDataReader getDataReader(string sql, string connStr, UserIDOP userID);
        DataSet GetListTables(string conStr, int pageIndex, int pageSize, string tableSource, string strWhere, string OrderExpression, string Fields, UserIDOP userID, ref int Counts);
        int GetLsh(string connStr, ref Int64 lsh);
        bool Submit_AddOrEdit(string connStr, string tableName, string pkName, string pkVal, Dictionary<string, string> ht, UserIDOP userID);
        bool Submit_AddOrEditLog(UserIDOP userID, string connStr, string tableName, string pkName, string pkVal, Dictionary<string, string> ht);
        int DeleteData(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID);
        int BatchDeleteDataByIn(string connStr, string tableName, string pkName, object[] pkValues, UserIDOP userID);
        int BatchDeleteDataByWhere(string connStr, string tableName, string whereString, UserIDOP userID);
        int BatchUpdateDataByIn(string connStr, string tableName, Dictionary<string, string> ht, string pkName, object[] pkValues, UserIDOP userID);
        int BatchUpdateDataByWhere(string connStr, string tableName, Dictionary<string, string> ht, string whereString, UserIDOP userID);
        int BatchUpdateDataMulti(string connStr, IList<UpdateSQLObj> batUpdateList, UserIDOP userID);
        int BatchUpdateDataMultiByTrans(string connStr, IList<UpdateSQLObj> batUpdateList, UserIDOP userID);
        bool BatchExeSQLMultiByTrans(string connStr, IList<SQLObj> batSQLObjList, UserIDOP userID);
        bool BatchExeSQLMultiByTrans(string connStr, IList<SysSQLString> batSQLStringList, UserIDOP userID);
        bool ExecuteNonQueryReturnBoolByTrans(string connStr, CommandType cmdType, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID);
        DataTable GetDataTable(string connStr, string tableName, UserIDOP userID);
        Dictionary<string, string> GetHashtableById(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID);
        int IsExist(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID);
        int GetMaxNum(string connStr, string tableName, string pkName, UserIDOP userID);
        bool ExtNonQueryByTrans(string connStr, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID);
        DataSet GetListPageStoreProcedure(string conStr, int page, int pageSize, string tableSource, string whereValue, string OrderExpression, string Fields, UserIDOP userID, ref int Counts);
        bool BatchExeSQLStrMultiByTransNoLogNoBackup(string connStr, IList<SysSQLString> batSQLStringList, UserIDOP userID);
        bool SqlBulkByTransNoLogNoBackUp(string connStr, string tableName, List<DataRow> listRow);
    }
}
