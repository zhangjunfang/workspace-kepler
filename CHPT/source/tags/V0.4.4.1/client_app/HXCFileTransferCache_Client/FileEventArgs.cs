using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SYSModel;
namespace HXCFileTransferCache_Client
{
    public class UpLoadFileEventArgs : EventArgs
    {
        private readonly String _guid;
        private TransferSendFileObj2 _sendFileObj;
        public UpLoadFileEventArgs(String guid, TransferSendFileObj2 obj)
        {
            _guid = guid;
            _sendFileObj = obj;
        }
        public String Guid { get { return _guid; } }
        public TransferSendFileObj2 SendFileObj { get { return _sendFileObj; } }
    }
    public static class UpLoadFileEventArgsExtensions
    {
        public static void Raise<TEventArgs>(this TEventArgs e, Object sender, ref UpLoadFileHandler<TEventArgs> eventDelegate)
where TEventArgs : EventArgs
        {
            UpLoadFileHandler<TEventArgs> temp = Interlocked.CompareExchange(ref eventDelegate, null, null);
            if (temp != null)
            {
                temp(sender, e);
            }
        }
    }
    public class DownLoadFileEventArgs : EventArgs
    {
        private readonly String _guid;
        private DownLoadFileOoj_Client _downLoadFileObj;

        public DownLoadFileEventArgs(String guid, DownLoadFileOoj_Client obj)
        {
            _guid = guid;
            _downLoadFileObj = obj;
        }
        public String Guid { get { return _guid; } }
        public DownLoadFileOoj_Client DownLoadFileObj { get { return _downLoadFileObj; } }
    }
    public static class DownLoadFileEventArgsExtensions
    {
        public static void Raise<TEventArgs>(this TEventArgs e, Object sender, ref DownLoadFileHandler<TEventArgs> eventDelegate)
where TEventArgs : EventArgs
        {
            DownLoadFileHandler<TEventArgs> temp = Interlocked.CompareExchange(ref eventDelegate, null, null);
            if (temp != null)
            {
                temp(sender, e);
            }
        }
    }
//******************************************************************8

    public class UpLoadFile2EventArgs : EventArgs
    {
        private readonly String _guid;
        private long _upLoadSize;
        private long _fileSize;
        public UpLoadFile2EventArgs(String guid, long upLoadSize ,long fileSize)
        {
            _guid = guid;
            _upLoadSize = upLoadSize;
            _fileSize = fileSize;
        }
        public String Guid { get { return _guid; } }
        public long UpLoadSize { get { return _upLoadSize; } }
        public long FileSize { get { return _fileSize; } }
    }
    public static class UpLoadFile2EventArgsExtensions
    {
        public static void Raise<TEventArgs>(this TEventArgs e, Object sender, ref UpLoadFile2Handler<TEventArgs> eventDelegate)
where TEventArgs : EventArgs
        {
            UpLoadFile2Handler<TEventArgs> temp = Interlocked.CompareExchange(ref eventDelegate, null, null);
            if (temp != null)
            {
                temp(sender, e);
            }
        }
    }
    public class DownLoadFile2EventArgs : EventArgs
    {
        private readonly String _guid;
        private long _downLoadSize;
        private long _fileSize;

        public DownLoadFile2EventArgs(String guid, long downLoadSize, long fileSize)
        {
            _guid = guid;
            _downLoadSize = downLoadSize;
            _fileSize = fileSize;
        }
        public String Guid { get { return _guid; } }
        public long DownLoadSize { get { return _downLoadSize; } }
        public long FileSize { get { return _fileSize; } }
    }
    public static class DownLoadFile2EventArgsExtensions
    {
        public static void Raise<TEventArgs>(this TEventArgs e, Object sender, ref DownLoadFile2Handler<TEventArgs> eventDelegate)
where TEventArgs : EventArgs
        {
            DownLoadFile2Handler<TEventArgs> temp = Interlocked.CompareExchange(ref eventDelegate, null, null);
            if (temp != null)
            {
                temp(sender, e);
            }
        }
    }

    public class UpLoadFileReceiveEventArgs : EventArgs
    {
        private TransferReceiveFileObj _upLoadReceiveFileObj;
        public UpLoadFileReceiveEventArgs(TransferReceiveFileObj obj)
        {
            _upLoadReceiveFileObj = obj;
        }
        public TransferReceiveFileObj UpLoadReceiveFileObj { get { return _upLoadReceiveFileObj; } }
    }

    public static class UpLoadFileReceiveEventArgsExtensions
    {
        public static void Raise<TEventArgs>(this TEventArgs e, Object sender, ref UpLoadFileReceiveHandler<TEventArgs> eventDelegate)
where TEventArgs : EventArgs
        {
            UpLoadFileReceiveHandler<TEventArgs> temp = Interlocked.CompareExchange(ref eventDelegate, null, null);
            if (temp != null)
            {
                temp(sender, e);
            }
        }
    }

}
