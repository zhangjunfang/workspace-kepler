using System;
using System.Text;

namespace CloudPlatSocket
{   
    /// <summary>
    /// 编码解码类
    /// </summary>
    public class BaseCodeHelper
    {
        #region 成员方法
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="stringToCompress">需要编码的信息</param>
        /// <returns></returns>
        public static string EnCode(string stringToCompress)
        {
            if (stringToCompress.Length > 0)
            {
                //采用Base64编码，之前要用GBK
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(stringToCompress));
            }
            return string.Empty;
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="stringToDecompress"></param>
        /// <returns></returns>
        public static string DeCode(string stringToDecompress)
        {
            string str = string.Empty;
            if (stringToDecompress == null)
            {
                throw new ArgumentNullException("stringToDecompress", "You tried to use an empty string");
            }
            try
            {
                try
                {
                    byte[] bytesToDecompress = Convert.FromBase64String(stringToDecompress.Trim());
                    //转换为
                    str = Encoding.UTF8.GetString(bytesToDecompress);
                }
                catch
                {
                    return stringToDecompress;
                }
            }
            catch (NullReferenceException exception)
            {
                return exception.Message;
            }
            return str;
        }
        #endregion       
    }
}
