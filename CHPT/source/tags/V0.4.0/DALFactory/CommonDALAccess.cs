using System;
using System.Reflection;
using IDAL;
namespace DALFactory
{
    public sealed class CommonDALAccess
    {
        public CommonDALAccess()
        { }

        private static readonly string sqlServerDAL = System.Configuration.ConfigurationManager.AppSettings["SQLServerDAL"];

        /// <summary>
        /// 
        /// </summary>        
        public static IDbCommon DbCommon()
        {
            string className = sqlServerDAL + ".DbCommon";
            return (IDbCommon)Assembly.Load(sqlServerDAL).CreateInstance(className);
        }
        /// <summary> 连接字符串
        /// <param name="accDbName">帐套数据库名称</param>
        /// </summary>        
        public static IConnString ConnString(string accDbName)
        {
            string className = sqlServerDAL + ".ConnString";
            IConnString iConnString = (IConnString)Assembly.Load(sqlServerDAL).CreateInstance(className);
            iConnString.connReadonly = iConnString.connReadonly.Replace("@@@", accDbName);
            iConnString.connWrite = iConnString.connWrite.Replace("@@@", accDbName);
            iConnString.connManageWrite = iConnString.connManageWrite.Replace("@@@", accDbName);
            iConnString.ConStrManageSql = iConnString.ConStrManageSql.Replace("@@@", accDbName);
            return iConnString;
        }
        ///// <summary>
        ///// 
        ///// </summary>        
        //public static IU_Admin U_Admin()
        //{
        //    string className = sqlServerDAL + ".U_Admin";
        //    return (IU_Admin)Assembly.Load(sqlServerDAL).CreateInstance(className);
        //}
        /// <summary>
        /// 
        /// </summary>        
        public static IManageSql ManageSql()
        {
            string className = sqlServerDAL + ".ManageSql";
            return (IManageSql)Assembly.Load(sqlServerDAL).CreateInstance(className);
        }
    }
}
