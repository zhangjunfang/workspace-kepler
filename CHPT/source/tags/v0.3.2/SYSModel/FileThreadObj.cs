using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace SYSModel
{
   public class FileThreadObj
    {
      public byte[] fileDataArry{get;set;}
      public int CanReadLength { get; set; }
      public string fileName { get; set; }
      public string guId { get; set; }
      public string serverDir { get; set; }
      public long fileSize { get; set; }
      public bool EndFlag { get; set; }
      public Thread myThread { get; set; }
    }
   public class FileThreadPramObj
   {
       public Thread myThread { get; set; }
       public string guId { get; set; }
   }  
}
