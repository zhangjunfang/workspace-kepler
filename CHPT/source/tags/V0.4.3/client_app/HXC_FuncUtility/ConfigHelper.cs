using System.Collections.Generic;
namespace HXC_FuncUtility
{
    /// <summary>
    /// Creator:yangtianshuai
    /// CreateTime:2014.10.29
    /// Function:help operating configs of xml type
    /// UpdateTime:2014.10.29 
    /// </summary>
    public class ConfigHelper
    {
        #region --通用数据库连接字符串操作
        /// <summary>
        /// 对连接语句进行拆分
        /// 提取数据库IP、数据库端口号、数据库实例名称、
        /// 用户名称、口令
        /// 返回实例数组 Data Source、localhost
        /// </summary>
        /// <returns></returns>
        public static string[] SplitConnString(string connString)
        {
            string[] arrays = connString.Split(';');
            List<string> newArray = new List<string>();
            string[] subArrays;
            foreach (string array in arrays)
            {
                subArrays = array.Split('=');
                 if (subArrays.Length == 2)
                {
                    newArray.Add(subArrays[0]);
                    newArray.Add(subArrays[1]);
                }
                
            }
            return newArray.ToArray();
        }
        /// <summary>
        /// 组装连接语句
        /// </summary>
        /// <param name="splitConnStrings">拆分连接语句</param>
        /// <returns></returns>
        public static string PackageConnString(string[] splitConnStrings)
        {
            string connString = string.Empty;
            for (int i = 0; i < splitConnStrings.Length; i++)
            {
                connString += splitConnStrings[i];
                if (i % 2 == 0)
                {
                    connString += "=";
                }
                else
                {
                    connString += ";";
                }
            }            
            return connString;
        }
        #endregion
    }
}
