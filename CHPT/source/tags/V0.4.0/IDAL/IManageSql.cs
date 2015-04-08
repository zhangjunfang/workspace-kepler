using System.Collections.Generic;
using System.Data;
using SYSModel;
namespace IDAL
{
    public interface IManageSql
    {
        bool Submit_AddOrEdit(string connStr, string tableName, string pkName, string pkVal, Dictionary<string, string> ht, UserIDOP userID);
        int DeleteData(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID);
        DataTable GetDataTable(string connStr, string tableName, UserIDOP userID);
        Dictionary<string, string> GetHashtableById(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID);
        int IsExist(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID);
        int GetMaxNum(string connStr, string tableName, string pkName, UserIDOP userID);
    }
}
