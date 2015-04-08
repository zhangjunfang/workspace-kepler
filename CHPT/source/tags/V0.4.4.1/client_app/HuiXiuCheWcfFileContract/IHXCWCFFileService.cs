using System.IO;
using System.ServiceModel;
namespace HuiXiuCheWcfFileContract
{
    [ServiceContract(Name = "HXCWCFFileService")]
    public interface IHXCWCFFileService
    {
        [OperationContract]
        Stream TestConnect();
        [OperationContract]
        UploadFileResponse UploadFile(FileTransferMessage request);
        [OperationContract]
        Stream DownLoadFile(string fileName, string savePath, string userID, string cookieStr);
    }
    [MessageContract]
    public class FileTransferMessage
    {
        [MessageHeader(MustUnderstand = true)]
        public string SavePath { set; get; }//文件保存路径

        [MessageHeader(MustUnderstand = true)]
        public string FileName { set; get; }//文件名称

        [MessageHeader(MustUnderstand = true)]
        public string UserID { set; get; }//用户

        [MessageHeader(MustUnderstand = true)]
        public string CookieStr { set; get; }//Cookie标记

        [MessageBodyMember(Order = 1)]
        public Stream FileData { set; get; }//文件传输数据
    }

    [MessageContract]
    public class UploadFileResponse
    {
        [MessageHeader(MustUnderstand = true)]
        public bool Flag;
    }

    //[MessageContract]
    //public class DownFileResponse
    //{
    //    [MessageBodyMember(Order = 1)]
    //    public Stream FileData { get; set; }
    //}
}
