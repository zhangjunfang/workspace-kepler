using System.ServiceModel;

namespace HuiXiuCheWcfFileTransferContract
{
    [ServiceContract(Name = "HXCWCFFileTransferService")]
    public interface IHXCWCFFileTransferService
    {
        [OperationContract]
        string TestConnect();
        [OperationContract]
        string ReceiveFile(byte[] fileDataArry, int CanReadLength, string path, string fileName, int Order_Num, string guId, string serverDir, long fileSize, bool EndFlag, int DataBlockCount,string UserID, string PCClientCookieStr);
        [OperationContract]
        bool DownLoadFile(string path, string fileName, string guId, string UserID, string PCClientCookieStr, out string msg, out long FileSize);
        [OperationContract]
        string DownLoadFileDataBlock(string guId, string UserID, string PCClientCookieStr);
        [OperationContract]
        bool GetSucDataBlock(string guId, string UserID, string PCClientCookieStr);
        [OperationContract]
        string ReceiveFile2(byte[] fileDataArry,int CanReadLength, string fileName, string guId, string serverDir,bool EndFlag);
        [OperationContract]
        string GetUploadFileStatus(string[] fileGuId,string UserID, string PCClientCookieStr);
        [OperationContract]
        string DeleteServerFiles(string FilesJsonStr, string UserID, string PCClientCookieStr);
        [OperationContract]
        string DeleteServerFileId(string DownFileId, string UserID, string PCClientCookieStr);
    }
}
