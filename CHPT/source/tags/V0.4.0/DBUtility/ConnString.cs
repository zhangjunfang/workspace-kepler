using System;

namespace DBUtility
{
    /// <summary>
    /// 切记：引用完类组件后，一定要调用此方法来释放！
    /// </summary>
    public class DBUtilityDispose
    {
        /// <summary>
        /// 切记：引用完类组件后，一定要调用此方法来释放！
        /// </summary>
        public static void Dispose()
        {
            ConnString.Dispose();
        }
    }
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public class ConnString
    {
        private static ConnString _instance = null;
        // Creates an syn object.
        private static readonly object SynObject = new object();
        ConnString()
        {
        }
        public static ConnString Instance
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
                            _instance = new ConnString();
                        }
                    }
                }
                return _instance;
            }
        }
        private static readonly Object locker = new Object();
        private static bool _alreadyDisposed = false;
        ~ConnString()
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
                _instance = null;
                //GC.SuppressFinalize(this);
            }
            _alreadyDisposed = true;
        }

        /// <summary>
        /// 管理数据库连接字符串
        /// </summary>        
        public string connManageWrite
        {
            get
            {
                if (_connManageWrite == null)
                {
                    _connManageWrite = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionManageStringWrite"].ToString();
                }
                return _connManageWrite;
            }
            set
            {
                _connManageWrite = value;
            }
        }
        private string _connManageWrite = null;


        /// <summary>
        /// 读写数据库连接字符串
        /// </summary>        
        public string connWrite
        {
            get
            {
                if (_connWrite == null)
                {
                    _connWrite = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringWrite"].ToString();
                }
                return _connWrite;
            }
            set
            {
                _connWrite = value;
            }
        }
        private string _connWrite = null;

        /// <summary>
        /// 只读数据库连接字符串
        /// </summary>
        public string connReadonly
        {
            get
            {
                if (_connReadonly == null)
                {
                    _connReadonly = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringReadonly"].ToString();
                }
                return _connReadonly;
            }
            set
            {
                _connReadonly = value;
            }
        }
        private string _connReadonly = null;

        /// <summary>
        /// 管理数据库业务层业务SQL连接字符串
        /// </summary>
        public string ConStrManageSql
        {
            get
            {
                if (_conStrManageSql == null)
                {
                    _conStrManageSql = System.Configuration.ConfigurationManager.ConnectionStrings["ConStrManageSql"].ToString();
                }
                return _conStrManageSql;
            }
            set
            {
                _conStrManageSql = value;
            }
        }
        private string _conStrManageSql = null;
    }
}
