using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using SYSModel;
namespace HXCSession
{
    public class PCClientSession
    {
    private static PCClientSession _instance = null;
    // Creates an syn object.
    private static readonly object SynObject = new object();
    PCClientSession()
    {
 
    }
    public static PCClientSession Instance
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
                        _instance = new PCClientSession();
                    }
                }
            }
            return _instance;
        }
    }


    private static ConcurrentDictionary<string, string> _pCClientSessionDic = new ConcurrentDictionary<string, string>();           
        private static readonly Object locker = new Object();

        private bool _alreadyDisposed = false;
          ~PCClientSession()
        {
           Dispose(true);
        }
        /// <summary>
       /// 切记：引用完类组件后，一定要调用此方法来释放！
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
       /// 清理所有正在使用的资源。
       /// </summary>
       /// <param name="isDisposing">如果应释放托管资源，为 true；否则为 false</param>
        protected  void Dispose(bool isDisposing)
      {
          if (_alreadyDisposed)
              return;
          if (isDisposing)
          {
              _pCClientSessionDic = null;
              _instance = null;
             //GC.SuppressFinalize(this);
          }
          _alreadyDisposed = true;
      }

        private ConcurrentDictionary<string, string> PCClientSessionDic
        {
            get
            {
                if (_pCClientSessionDic == null)
                {
                    lock (locker)
                    {
                        if (_pCClientSessionDic == null)
                        {
                            _pCClientSessionDic = new ConcurrentDictionary<string, string>();     
                        }
                    }
                }
                return _pCClientSessionDic;
            }
        }

        public void Add(string userID,string cookieStr)
        {
            string value = string.Empty;
            if(PCClientSessionDic.TryGetValue(userID,out value))
            {
                PCClientSessionDic.TryUpdate(userID, cookieStr, value);
            }
            else{
                PCClientSessionDic.TryAdd(userID, cookieStr);
            }

        }

        public bool Get(string userID, out string cookieStr)
        {
            //string value = string.Empty;
            return PCClientSessionDic.TryGetValue(userID, out cookieStr);
        }

        public bool Remove(string userID,out string cookieStr)
        {
            return PCClientSessionDic.TryRemove(userID,out cookieStr);
        }

        public List<string> GetOnLineUserIDList()
        {
            List<string> idList = new List<string>();
            foreach (KeyValuePair<string,string> item in PCClientSessionDic)
            {
                idList.Add(item.Key);
            }
            return idList;
        }      
    }
}
