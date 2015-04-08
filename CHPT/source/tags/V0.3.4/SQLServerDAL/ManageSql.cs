using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using  IDAL;
using SYSModel;
namespace  SQLServerDAL
{
    public class ManageSql : IManageSql
    {
        public bool Submit_AddOrEdit(string connStr, string tableName, string pkName, string pkVal, Dictionary<string, string> ht,UserIDOP userID)
        {
            return DBUtility.SqlHelper.Submit_AddOrEdit(connStr, tableName, pkName, pkVal, ht, userID);
        }
        public int DeleteData(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID)
        { 
            return  DBUtility.SqlHelper.DeleteData(connStr, tableName, pkName, pkVal, userID);
        }
        public DataTable GetDataTable(string connStr,string tableName, UserIDOP userID)
        {
            return DBUtility.SqlHelper.GetDataTable(connStr, tableName, userID);
        }
        public Dictionary<string, string> GetHashtableById(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID)
        {
            return  DBUtility.SqlHelper.GetHashtableById(connStr, tableName, pkName, pkVal, userID);
        }
        public int IsExist(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID)
        {
            return  DBUtility.SqlHelper.IsExist(connStr, tableName, pkName, pkVal, userID);
        }
        public int GetMaxNum(string connStr, string tableName, string pkName, UserIDOP userID)
        {
            return  DBUtility.SqlHelper.GetMaxNum(connStr, tableName, pkName, userID);
        }
        public bool EnableSQLObj(string connStr, string sql, Dictionary<string, ParamObj> dic, UserIDOP userID)
        {
                SqlParameter[] commandParameters =  DBUtility.SqlHelper.GetParameter(dic);
                return DBUtility.SqlHelper.ExecuteNonQueryReturnBool(connStr, CommandType.Text, sql, userID, commandParameters);
        }
    }
}
