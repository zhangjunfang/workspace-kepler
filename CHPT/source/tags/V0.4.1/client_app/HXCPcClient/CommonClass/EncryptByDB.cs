using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HXCPcClient.CommonClass
{
    /// <summary>
    /// 从数据库加密
    /// cxz
    /// 2014-11-05
    /// </summary>
    public class EncryptByDB
    {
        /// <summary> 加密秘钥
        /// </summary>
        public static string KeySecurity_YT = "80EE5DAD-5EDB-4A96-A1CD-9FD8D6BC0719";

        /// <summary> 获取加密后的值
        /// </summary>
        /// <param name="value">需要加密的值</param>
        /// <returns>返回加密函数</returns>
        //public static string GetEncFieldValue(string value)
        //{
        //    if(string.IsNullOrEmpty(value))
        //    {
        //        return null;
        //    }
        //    string strSql= string.Format("select EncryptByPassPhrase('{0}','{1}') ", KeySecurity_YT, value);
        //    SYSModel.SQLObj sqlObj=new SYSModel.SQLObj ();
        //    sqlObj.cmdType = CommandType.Text;
        //    sqlObj.sqlString = strSql;
        //    sqlObj.Param = new Dictionary<string, SYSModel.ParamObj>();
        //    DataSet ds= DBHelper.GetDataSet("" ,sqlObj);
        //    if (ds == null || ds.Tables.Count == 0 || ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
        //    {
        //        return null;
        //    }
        //    byte[] bytes = (byte[])ds.Tables[0].Rows[0][0];
        //    bytes.ToString();
        //    return Convert.ToString(ds.Tables[0].Rows[0][0]);
        //}


        /// <summary> 获取加密后的值
        /// </summary>
        /// <param name="value">需要加密的值</param>
        /// <returns>返回加密函数</returns>
        public static string GetEncFieldValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "null";
            }
            return string.Format("EncryptByPassPhrase('{0}','{1}') ", KeySecurity_YT, value);
            
        }

        /// <summary> 获取解密后的值
        /// </summary>
        /// <param name="value">需要解密的值</param>
        /// <returns>返回加密函数</returns>
        public static string GetDesFieldValue(string value)
        {
            return string.Format("CONVERT(VARCHAR(50),DECRYPTBYPASSPHRASE('{0}',{1})) ", KeySecurity_YT, value);
        }
    }
}
