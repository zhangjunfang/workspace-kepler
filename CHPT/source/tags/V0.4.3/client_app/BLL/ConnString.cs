using System;
namespace BLL
{
    public class ConnString
    {
        private static ConnString _instance = null;
        private static string connString = string.Empty;
        public static ConnString Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConnString();
                    connString = DALFactory.CommonDALAccess.ConnString(HXC_FuncUtility.GlobalStaticObj_Server.Instance.CurrAccDbName).connWrite;
                }
                return _instance;
            }
        }
        /// <summary> 读写数据库连接字符串
        /// </summary>        
        public string connManageWrite(string currDbName)
        {
            return connString.Replace("@@@", currDbName);
        }

        /// <summary> 读写数据库连接字符串
        /// </summary>        
        public string connWrite(string currDbName)
        {
            return connString.Replace("@@@", currDbName);
        }
        /// <summary> 只读数据库连接字符串
        /// </summary>
        public string connReadonly(string currDbName)
        {
            return connString.Replace("@@@", currDbName);
        }
        /// <summary> 管理数据库业务层业务SQL连接字符串
        /// </summary>
        public string ConStrManageSql(string currDbName)
        {
            return connString.Replace("@@@", currDbName);
        }
    }
}
