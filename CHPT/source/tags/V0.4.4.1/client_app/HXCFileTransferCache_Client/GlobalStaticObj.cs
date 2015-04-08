using System.ServiceModel;
namespace HXCFileTransferCache_Client
{
   public class GlobalStaticObj
    {    
    private static GlobalStaticObj _instance = null;
    // Creates an syn object.
    private static readonly object SynObject = new object();
    GlobalStaticObj()
    {
    }
    public static GlobalStaticObj Instance
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
                        _instance = new GlobalStaticObj();
                    }
                }
            }
            return _instance;
        }
    }
    private static bool _alreadyDisposed = false;
    ~GlobalStaticObj()
        {
           Dispose(true);
        }
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

        public ChannelFactory<HuiXiuCheWcfFileTransferContract.IHXCWCFFileTransferService> channelFactoryFile { get; set; }
        public HuiXiuCheWcfFileTransferContract.IHXCWCFFileTransferService proxyFile { get; set; }
    }
}
