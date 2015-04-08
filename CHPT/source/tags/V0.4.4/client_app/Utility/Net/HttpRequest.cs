using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;

namespace Utility.Net
{
    /// <summary> http请求调用WebService
    /// </summary>
    public class HttpRequest
    {
        /// <summary> WebService：Post调用
        /// </summary>
        /// <param name="url">Webservice地址</param>
        /// <param name="paramsOfUrl">传入数据</param>
        /// <returns>返回结果</returns>
        public static string DoPost(string url, Hashtable paramsOfUrl)
        {
            if (url == null)
            {
                throw new Exception("WebService地址为空");
            }

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            // 编辑并Encoding提交的数据  
            byte[] data = GetJointBOfParams(paramsOfUrl);

            // 发送请求  
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            // 获得回复  
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string result = reader.ReadToEnd();
            reader.Close();

            return result;
        }

        /// <summary> http请求调用接口：Get调用
        /// </summary>
        /// <param name="url">接口地址</param>
        /// <param name="paramsOfUrl">传入数据</param>
        /// <returns>返回结果</returns>
        public static string DoGet(string url, Hashtable paramsOfUrl)
        {
            if (url == null)
            {
                throw new Exception("WebService地址为空");
            }

            // 编辑并Encoding提交的数据  
            string data = GetJointSOfParams(paramsOfUrl);

            // 拼接URL
            url += "?" + data;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            string responseData = "";
            try
            {
                // 获得回复  
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        responseData = reader.ReadToEnd();
                    }
                }

            }
            catch (Exception ex)
            {
                return "";
            }
            return responseData;
        }

        /// <summary> http请求下载文件：Get调用
        /// </summary>
        /// <param name="url">Webservice地址</param>
        /// <param name="paramsOfUrl">传入数据</param>
        /// <param name="filePath">下载文件存放位置，带文件名</param>
        /// <returns>返回结果</returns>
        public static bool DoGet(string url, Hashtable paramsOfUrl, string filePath)
        {
            if (url == null)
            {
                throw new Exception("WebService地址为空");
            }

            // 编辑并Encoding提交的数据  
            string data = GetJointSOfParams(paramsOfUrl);

            // 拼接URL
            url += "?" + data;

            //添加验证证书的回调方法
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            try
            {
                // 获得回复  
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                string dir = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                using (FileStream fs = File.Create(filePath))
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        int bufferSize = 2048;
                        byte[] bytes = new byte[bufferSize];
                        int length = stream.Read(bytes, 0, bufferSize);
                        while (length > 0)
                        {
                            fs.Write(bytes, 0, length);
                            length = stream.Read(bytes, 0, bufferSize);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region 内部方法
        /// <summary> 拼接参数串----Get
        /// </summary>
        /// <param name="paramsOfUrl">参数项</param>
        /// <returns>返回字符串</returns>
        private static String GetJointSOfParams(Hashtable paramsOfUrl)
        {
            // 编辑并Encoding提交的数据  
            StringBuilder sbuilder = new StringBuilder();
            int i = 0;
            foreach (DictionaryEntry de in paramsOfUrl)
            {
                string value = ToHttpChar(de.Value.ToString());
                if (i == 0)
                {
                    sbuilder.Append(de.Key + "=" + value);
                }
                else
                {
                    sbuilder.Append("&" + de.Key + "=" + value);
                }
                i++;
            }
            return sbuilder.ToString();
        }

        /// <summary> 拼接参数串----Post
        /// </summary>
        /// <param name="paramsOfUrl">参数项</param>
        /// <returns>返回字节数组</returns>
        private static byte[] GetJointBOfParams(Hashtable paramsOfUrl)
        {
            // 编辑并Encoding提交的数据  
            String stringJointOfParams = GetJointSOfParams(paramsOfUrl);
            byte[] data = new ASCIIEncoding().GetBytes(stringJointOfParams);
            return data;
        }

        /// <summary> 转义特殊字符
        /// </summary>
        private static string ToHttpChar(string value)
        {
            value = value.ToString().Replace("+", "%2B");
            //value = value.ToString().Replace(" ", "%20");
            //value = value.ToString().Replace("/", "%2F");
            //value = value.ToString().Replace("?", "%3F");
            //value = value.ToString().Replace("%", "%25");
            //value = value.ToString().Replace("#", "%23");
            //value = value.ToString().Replace("&", "%26");
            //value = value.ToString().Replace("=", "%3D");
            //value = value.ToString().Replace(@"\", "%5C");
            //value = value.ToString().Replace(".", "%2E");
            //value = value.ToString().Replace(":", "%3A");
            return value;
        }

        /// <summary> 回调方法
        /// </summary>
        /// <returns>成功/失败</returns> 
        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        #endregion
    }
}