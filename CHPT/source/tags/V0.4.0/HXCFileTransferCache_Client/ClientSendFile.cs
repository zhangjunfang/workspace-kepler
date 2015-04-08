using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SYSModel;
using Newtonsoft.Json;

namespace HXCFileTransferCache_Client
{
    public delegate void UpLoadFile2Handler<UpLoadFile2EventArgs>(object sender, UpLoadFile2EventArgs e);
    public delegate void DownLoadFile2Handler<DownLoadFile2EventArgs>(object sender, DownLoadFile2EventArgs e);
  public class ClientSendFile
    {
      public static event UpLoadFile2Handler<UpLoadFile2EventArgs> OnUpLoadFile;
      public static event DownLoadFile2Handler<DownLoadFile2EventArgs> OnDownLoadFile;

      public static void UpLoadFileMsg(UpLoadFile2EventArgs e)
        {
            e.Raise("ClientSendFile.UpLoadFileMsg", ref OnUpLoadFile);
        }

      public static void DownLoadFileMsg(DownLoadFile2EventArgs e)
        {
            e.Raise("ClientSendFile.DownLoadFileMsg", ref OnDownLoadFile);
        }
      public static bool SendFile(string filePath, string guId, string serverDir)
      {
          string fileName = Path.GetFileName(filePath);
          string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
          string fileType = Path.GetExtension(filePath);
          FileInfo fileInfo = new FileInfo(filePath);
          long FileSize = fileInfo.Length;
          using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
          {
              return SafeReadFile(fs, fileName, filePath, serverDir, guId, FileSize);
          }
      }

      private static bool ClientSendFileDataBlock(byte[] fileDataArry,int CanReadLength, string fileName, string guId, string serverDir,bool EndFlag)
      {
          bool result = false;
          if (WCFClientProxy.TestPCClientProxy())
          {
              string resultStr = GlobalStaticObj.Instance.proxyFile.ReceiveFile2(fileDataArry,CanReadLength,fileName,guId,serverDir,EndFlag);
              bool.TryParse(resultStr, out result);
          }
          return result;
      }

      private static bool SafeReadFile(Stream stream,string FileName, string FilePath, string serverDir, string guId,long fileSize)
      {
          long sendSize = 0;
          int bufferLength = 32768;
          //初始化一个32k的缓存
          //byte[] buffer = new byte[32768];
          byte[] buffer = new byte[bufferLength];
          int byteLength = 0;
          int read = 0;
          int block;
          int _order_Num = 0;
          //每次从流中读取缓存大小的数据，知道读取完所有的流为止        
          //Utility.Log.Log.writeLineToLog("开始读取文件", "Read");
          while ((block = stream.Read(buffer, 0, buffer.Length)) > 0)
          {
              byteLength = byteLength + block;
              //重新设定读取位置
              read += block;
              _order_Num = _order_Num + 1;
              //检查是否到达了缓存的边界，检查是否还有可以读取的信息
              if (block == buffer.Length)
              {                 
                      if (ClientSendFileDataBlock(buffer,bufferLength,FileName, guId, serverDir, false))
                      {
                          sendSize +=bufferLength;
                          //Utility.Log.Log.writeLineToLog("成功发送文件块：" + _order_Num.ToString(), "Send");
                          UpLoadFileMsg(new UpLoadFile2EventArgs(guId, sendSize, fileSize));
                      }
                      else {
                          return false;
                      }                
                  Array.Clear(buffer, 0, buffer.Length);
                  byteLength = 0;
              }
          }
          _order_Num = _order_Num + 1;
          byte[] _fileData = new byte[32768];
          Array.Copy(buffer, _fileData, byteLength);
          if (ClientSendFileDataBlock(_fileData,byteLength,FileName, guId, serverDir, true))
          {
              //Utility.Log.Log.writeLineToLog("成功发送文件块：" + _order_Num.ToString(), "Send");
              UpLoadFileMsg(new UpLoadFile2EventArgs(guId, byteLength, fileSize));
          }
          else
          {
              return false;
          }               
          //清空缓存区
          Array.Clear(buffer, 0, buffer.Length);
          return true;
      }
    }
}
