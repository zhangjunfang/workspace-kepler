using System;
using DBUtility;
namespace SQLServerDAL
{
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public class ConnString : IDAL.IConnString
    {

        /// <summary>
        /// 管理数据库连接字符串
        /// </summary>        
        public string connManageWrite
        {
            get
            {
                return DBUtility.ConnString.Instance.connManageWrite;
            }
            set
            {
                DBUtility.ConnString.Instance.connManageWrite = value;
            }
        }
        /// <summary>
        /// 读写数据库连接字符串
        /// </summary>        
        public string connWrite
        {
            get
            {
                return DBUtility.ConnString.Instance.connWrite;
            }
            set
            {
                DBUtility.ConnString.Instance.connWrite = value;
            }
        }
        /// <summary>
        /// 只读数据库连接字符串
        /// </summary>
        public string connReadonly
        {
            get
            {
                return DBUtility.ConnString.Instance.connReadonly;
            }
            set
            {
                DBUtility.ConnString.Instance.connReadonly = value;
            }
        }
        /// <summary>
        /// 管理数据库业务层业务SQL连接字符串
        /// </summary>
        public string ConStrManageSql
        {
            get
            {
                return DBUtility.ConnString.Instance.ConStrManageSql;
            }
            set
            {
                DBUtility.ConnString.Instance.ConStrManageSql = value;
            }
        }
        private string _conStrManageSql = null;
    }
}
