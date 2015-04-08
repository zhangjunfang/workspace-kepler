using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Utility.Security
{
    /// <summary>
    /// 加密类，包含 MD5 加密和 3des 加密解密。
    /// 孙亚楠 2014-10-10
    /// </summary>
    public sealed class Secret
    {
        #region md5加密
        /// <summary> 获取MD5加密后的字符串(不指定编码方式)
        /// </summary>
        /// <param name="srcString">加密前的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5(string srcString)
        {
            //缺省用 gb2312 编码
            return MD5(srcString, Encoding.GetEncoding("gb2312"));
        }

        /// <summary> 获取MD5加密后的字符串(指定编码方式)
        /// </summary>
        /// <param name="srcString">加密前的字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5(string srcString, Encoding encoding)
        {
            byte[] data = encoding.GetBytes(srcString);
            return System.BitConverter.ToString(
                new MD5CryptoServiceProvider().ComputeHash(data)).Replace("-", "").ToLower();
        }
        #endregion

        #region 3des加密
        /// <summary>3des加密字符串(默认编码)
        /// </summary>
        /// <param name="srcString">要加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>加密后并经 base64 编码的字符串</returns>
        public static string Encrypt3DES(string srcString, string key)
        {
            return Encrypt3DES(srcString, key, Encoding.Default);
        }

        /// <summary> 3des加密字符串（指定编码类型）
        /// </summary>
        /// <param name="srcString">要加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>加密后并经 base64 编码的字符串</returns>
        public static string Encrypt3DES(string srcString, string key, Encoding encoding)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

            DES.Key = hashMD5.ComputeHash(encoding.GetBytes(key));
            DES.Mode = CipherMode.ECB;

            ICryptoTransform DESEncrypt = DES.CreateEncryptor();

            byte[] Buffer = encoding.GetBytes(srcString);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        public static byte[] Encrypt3DES(byte[] bytes, string key, Encoding encoding)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

            DES.Key = hashMD5.ComputeHash(encoding.GetBytes(key));
            DES.Mode = CipherMode.ECB;
            //转成16进制
            StringBuilder sbByte = new StringBuilder();
            foreach (byte b in bytes)
            {
                sbByte.Append(b.ToString("X2"));
            }

            ICryptoTransform DESEncrypt = DES.CreateEncryptor();
            byte[] buffer = encoding.GetBytes(sbByte.ToString());
            //加密
            string value = Convert.ToBase64String(DESEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            return buffer;
        }


        /// <summary> 3des加密字符串（指定编码类型）
        /// </summary>
        /// <param name="buff">要加密的byte数组</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>加密后并经 base64 编码的字符串</returns>
        public static string Encrypt3DES(byte[] buff, string key)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

            DES.Key = hashMD5.ComputeHash(Encoding.UTF8.GetBytes(key));
            DES.Mode = CipherMode.ECB;

            ICryptoTransform DESEncrypt = DES.CreateEncryptor();

            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(buff, 0, buff.Length));
        }
        /// <summary> 3des加密字符串（UTF-8）
        /// </summary>
        /// <param name="srcString">要加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>加密后并经 base64 编码的字符串</returns>
        public static string Encrypt3DES_UTF8(string srcString, string key)
        {
            return Encrypt3DES(srcString, key, Encoding.UTF8);
        }

        /// <summary>  3des解密字符串（采用默认编码）
        /// </summary>
        /// <param name="srcString">要解密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>解密后的字符串</returns>
        /// <exception cref="">密钥错误</exception>
        public static string Decrypt3DES(string srcString, string key)
        {
            return Decrypt3DES(srcString, key, Encoding.Default);
        }

        /// <summary> 3des解密字符串UTF8
        /// </summary>
        /// <param name="srcString">要解密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt3DES_UTF8(string srcString, string key)
        {
            return Decrypt3DES(srcString, key, Encoding.UTF8);
        }

        /// <summary> 3des解密字符串
        /// </summary>
        /// <param name="srcString">要解密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt3DES(string srcString, string key, Encoding encoding)
        {
            if (string.IsNullOrEmpty(srcString))
            {
                return "";
            }
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

            DES.Key = hashMD5.ComputeHash(encoding.GetBytes(key));
            DES.Mode = CipherMode.ECB;

            ICryptoTransform DESDecrypt = DES.CreateDecryptor();

            string result = "";
            try
            {
                byte[] Buffer = Convert.FromBase64String(srcString);
                result = encoding.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }
        #endregion

        #region des加密
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary>  DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary> DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        #endregion

        #region base64
        /// <summary>
        /// 将普通文本转换成Base64编码的文本
        /// </summary>
        /// <param name="value">普通文本</param>
        /// <returns></returns>
        public static string StringToBase64String(string value)
        {
            byte[] buff = Encoding.UTF8.GetBytes(value);
            string base64Str = Convert.ToBase64String(buff, 0, buff.Length);
            return base64Str;
        }
        /// <summary>
        /// 将Base64编码的文本转换成普通文本
        /// </summary>
        /// <param name="base64Str">Base64编码的文本</param>
        /// <returns></returns>
        public static string Base64StringToString(string base64Str)
        {
            byte[] buff = Convert.FromBase64String(base64Str);
            string value = Encoding.UTF8.GetString(buff, 0, buff.Length);
            return value;
        }
        #endregion
    }
}
