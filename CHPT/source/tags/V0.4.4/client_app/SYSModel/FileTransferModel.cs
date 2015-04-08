using System;
namespace SYSModel
{
    public class TransferFileDataObj
    {
        private  byte[] _fileData = new byte[32768];
        public byte[] FileData { get { return _fileData; } set { _fileData = value; } }
        public int CanReadLength { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int Order_Num { get; set; }
        public string guId { get; set; }
        public bool EndFlag { get; set; }
    }

    public class TransferSendFileObj
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public long ReadSize { get; set; }
        public long SendSize { get; set; }
        public string serverDir { get; set; }
        public SendFileStatus Status { get; set; }
        public int DataBlockCount { get; set; }
    }

    public class TransferSendFileObj2 : TransferSendFileObj
    {
        public int BlockNum { get; set; }
    }

    public enum SendFileStatus {StartRead,Reading,ReadEnd,StartSend,Sending,SendEnd}

    public enum ReceiveFileStatus { StartReceive, Receiving, ReceiveEnd, StartWrite, Writing, WriteEnd ,WriteWaitTimeOut}

    //客户端从服务器端拉取文件，关于此文件，服务器端的Read状态
    public enum GetFileReadStatus { waiting, StartRead, Reading, ReadEnd }
    //客户端从服务器端拉取文件，关于此文件，服务器端的Send状态
    public enum GetFileSendStatus { waiting, StartSend, Sending, SendEnd }
    //客户端从服务器端拉取文件，关于此文件，服务器端的Write状态
    public enum GetFileWriteStatus { waiting, StartWrite, Writing, WriteEnd, WriteWaitTimeOut }

    public class TransferReceiveFileObj
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string serverDir { get; set; }
        public long FileSize { get; set; }
        public long ReceiveSize { get; set; }
        public int WriteSize { get; set; }
        public ReceiveFileStatus Status { get; set; }
        public int DataBlockCount { get; set; }
        public int NextDataBlockNum { get; set; }
        public DateTime writeTime { get; set; }
        public string guId { get; set; }
    }

    public class GetFileOoj {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long ReadSize { get; set; }
        public long SendSize { get; set; }
        public string serverDir { get; set; }
        public GetFileReadStatus ReadStatus { get; set; }
        public GetFileSendStatus SendStatus { get; set; }
        public GetFileWriteStatus WriteStatus { get; set; }
        public string guId { get; set; }
    }

    //客户端从服务器端拉取文件，关于此文件，客户端的DownLoad状态
    public enum GetFileDownLoadStatus { waiting, StartDownLoad, DownLoading, DownLoadEnd,StartWrite,Writeing,WriteEnd}

    public class DownLoadFileOoj_Client
    {
        public string FileName { get; set; }
        public long DownLoadSize { get; set; }
        public int WriteSize { get; set; }
        public string path { get; set; }
        public GetFileDownLoadStatus DownLoadStatus { get; set; }
        public string guId { get; set; }
        public long FileSize { get; set; }
        public int NextDataBlockNum { get; set; }
        public DateTime writeTime { get; set; }
        public GetFileWriteStatus Status { get; set; }
        public DateTime getFileTime { get; set; }
    }

    public class DownLoadFileOoj_Server
    {
        public string FileName { get; set; }
        public long ReadSize { get; set; }
        public long SendSize { get; set; }
        public string serverDir { get; set; }
        public GetFileReadStatus ReadStatus { get; set; }
        public GetFileSendStatus SendStatus { get; set; }
        public string guId { get; set; }
        public long FileSize { get; set; }
    }

    public class DownLoadFileDataObj
    {
        private byte[] _fileData = new byte[32768];
        public byte[] FileData { get { return _fileData; } set { _fileData = value; } }
        public string FileName { get; set; }
        public string serverDir { get; set; }
        public int Order_Num { get; set; }
        public string guId { get; set; }
        public bool EndFlag { get; set; }
        public int CanReadLength { get; set; }
    }
    public class DeleteServerFileObj{
    public string ServerDir{ get; set; }
    public string fileName{ get; set; }
    }
}
