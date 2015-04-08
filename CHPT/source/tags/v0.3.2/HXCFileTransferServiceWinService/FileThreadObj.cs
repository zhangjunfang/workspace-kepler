using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HXCFileTransferServiceWinService
{
   public class FileThreadObj
    {
      public byte[] fileDataArry{get;set;}
      public int CanReadLength { get; set; }
      public string path { get; set; }
      public string fileName { get; set; }
      public int Order_Num { get; set; }
      public string guId { get; set; }
      public string serverDir { get; set; }
      public long fileSize { get; set; }
      public bool EndFlag { get; set; }
      public int DataBlockCount { get; set; }
    }
}
