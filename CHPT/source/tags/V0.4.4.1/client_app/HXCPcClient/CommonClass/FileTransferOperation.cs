using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuiXiuCheWcfFileContract;
using System.IO;

namespace HXCPcClient.CommonClass
{
    public class FileTransferOperation 
    {
        string guid = string.Empty;
        bool isUp = false;
        bool isDown = false;
        //delegate void FileDelegate(string fileName);
        //public event HXCFileTransferCache_Client.DownLoadFileHandler<HXCFileTransferCache_Client.DownLoadFileEventArgs> DownFile;
        //public event HXCFileTransferCache_Client.UpLoadFileHandler<HXCFileTransferCache_Client.UpLoadFileEventArgs> UpFile;
        //public event HXCFileTransferCache_Client.UpLoadFileReceiveHandler<HXCFileTransferCache_Client.UpLoadFileReceiveEventArgs> UpReceive;
        //public FileTransferOperation()
        //{
        //    HXCFileTransferCache_Client.FileTransferClient.OnDownLoadFile += new HXCFileTransferCache_Client.DownLoadFileHandler<HXCFileTransferCache_Client.DownLoadFileEventArgs>(FileTransferClient_OnDownLoadFile);
        //    HXCFileTransferCache_Client.FileTransferClient.OnUpLoadFile += new HXCFileTransferCache_Client.UpLoadFileHandler<HXCFileTransferCache_Client.UpLoadFileEventArgs>(FileTransferClient_OnUpLoadFile);
        //    HXCFileTransferCache_Client.FileTransferClient.OnUpLoadReceiveFile += new HXCFileTransferCache_Client.UpLoadFileReceiveHandler<HXCFileTransferCache_Client.UpLoadFileReceiveEventArgs>(FileTransferClient_OnUpLoadReceiveFile);
        //    HXCFileTransferCache_Client.FileTransferClient.UserId = GlobalStaticObj.UserID;
        //    HXCFileTransferCache_Client.FileTransferClient.PCClientCookieStr = GlobalStaticObj.CookieStr;
        //}

        //void FileTransferClient_OnUpLoadReceiveFile(object sender, HXCFileTransferCache_Client.UpLoadFileReceiveEventArgs e)
        //{
        //    if (UpReceive != null && guid==e.UpLoadReceiveFileObj.guId)
        //    {
        //        UpReceive(sender, e);
        //    }
        //}

        //void FileTransferClient_OnUpLoadFile(object sender, HXCFileTransferCache_Client.UpLoadFileEventArgs e)
        //{
        //    //if (e.SendFileObj.Status == SYSModel.SendFileStatus.SendEnd)
        //    //{
        //    if (UpFile != null && guid==e.Guid)
        //    {
        //        UpFile(sender, e);
        //    }
        //    //}
        //}

        //void FileTransferClient_OnDownLoadFile(object sender, HXCFileTransferCache_Client.DownLoadFileEventArgs e)
        //{
        //    //if (e.DownLoadFileObj.Status == SYSModel.GetFileWriteStatus.WriteEnd)
        //    //{
        //    if (DownFile != null && guid==e.Guid)
        //    {
        //        DownFile(sender, e);
        //    }
        //    //}
        //}
        ///// <summary>
        ///// 上传文件
        ///// </summary>
        ///// <returns></returns>
        //public void UploadFile(string filePath, string path)
        //{
        //    guid = Guid.NewGuid().ToString("N");
        //    //HXCFileTransferCache_Client.FileTransferClient.AddIdList(guid);
        //    //path = "\\" + path;
        //    HXCFileTransferCache_Client.FileTransferClient.ClientUpLoadFile(filePath, guid, path);
        //}

        ///// <summary>
        ///// 下载文件
        ///// </summary>
        ///// <param name="fileName">文件名称</param>
        ///// <returns>文件流</returns>
        //public bool DownLoadFile(string fileName, string path)
        //{
        //    string msg = string.Empty;
        //    guid = Guid.NewGuid().ToString("N");
        //    long fileSize = 0;
        //    path = "\\" + path;
        //    return HXCFileTransferCache_Client.FileTransferClient.ClientDownLoadFile(path, fileName, guid, out msg, out fileSize);
        //}

        ////public bool DeleteFile()
        ////{
        ////    //return HXCFileTransferCache_Client.FileTransferClient.
        ////}

        //public void Dispose()
        //{
        //    HXCFileTransferCache_Client.FileTransferClient.OnDownLoadFile -= new HXCFileTransferCache_Client.DownLoadFileHandler<HXCFileTransferCache_Client.DownLoadFileEventArgs>(FileTransferClient_OnDownLoadFile);
        //    HXCFileTransferCache_Client.FileTransferClient.OnUpLoadFile -= new HXCFileTransferCache_Client.UpLoadFileHandler<HXCFileTransferCache_Client.UpLoadFileEventArgs>(FileTransferClient_OnUpLoadFile);
        //    HXCFileTransferCache_Client.FileTransferClient.OnUpLoadReceiveFile -= new HXCFileTransferCache_Client.UpLoadFileReceiveHandler<HXCFileTransferCache_Client.UpLoadFileReceiveEventArgs>(FileTransferClient_OnUpLoadReceiveFile);
        //}
    }
}
