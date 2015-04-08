using System;
using System.Data;
using System.Collections.Generic;
using Model;
using DALFactory;
using IDAL;
using SYSModel;
namespace BLL
{
    public class ManageSql 
    {
       //private static IManageSql dal = DALFactory.CommonDALAccess.ManageSql();
       private static readonly Object locker = new Object();
       private static IDAL.IManageSql dal = null;

         private static bool _alreadyDisposed = false;
         ~ManageSql()
        {
           Dispose(true);
        }
        /// <summary>
       /// 切记：引用完类组件后，一定要调用此方法来释放！
        /// </summary>
        public static void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
       /// 清理所有正在使用的资源。
       /// </summary>
       /// <param name="isDisposing">如果应释放托管资源，为 true；否则为 false</param>
        protected  static void Dispose(bool isDisposing)
      {
          if (_alreadyDisposed)
              return;
          if (isDisposing)
          {
              dal = null;
             //GC.SuppressFinalize(this);
          }
          _alreadyDisposed = true;
      }


       private static IDAL.IManageSql Dal
       {
           get
           {
               if (dal == null)
               {
                   lock (locker)
                   {
                       if (dal == null)
                       {
                           dal = DALFactory.CommonDALAccess.ManageSql();
                       }
                   }
               }
               return dal;
           }
       }
       public ManageSql()
		{}
       public static bool Submit_AddOrEdit(string connStr, string tableName, string pkName, string pkVal, Dictionary<string, string> ht, UserIDOP userID)
       {
           return Dal.Submit_AddOrEdit(connStr,tableName, pkName, pkVal, ht, userID);
       }
       public static int DeleteData(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID)
       {
           return Dal.DeleteData(connStr, tableName, pkName, pkVal,userID);
       }
       public static DataTable GetDataTable(string connStr, string tableName, UserIDOP userID)
       {
           return Dal.GetDataTable(connStr,tableName, userID);
       }
       public static Dictionary<string, string> GetHashtableById(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID)
       {
           return Dal.GetHashtableById(connStr,tableName, pkName, pkVal, userID);
       }
       public static int IsExist(string connStr, string tableName, string pkName, string pkVal, UserIDOP userID)
       {
           return Dal.IsExist(connStr,tableName, pkName, pkVal, userID);
       }
       public static int GetMaxNum(string connStr, string tableName, string pkName, UserIDOP userID)
       {
           return Dal.GetMaxNum(connStr,tableName, pkName, userID);
       }
    }
}
