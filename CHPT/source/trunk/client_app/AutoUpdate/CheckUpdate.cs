using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate
{
    public class CheckUpdate
    {
        #region 2.1 、检查升级版本
        /// <summary> 检查升级版本
        /// <param name="appName">应用程序名称</param>
        /// <param name="currVerson">当前版本</param>
        /// <param name="softType">软件类型(1-服务端  2-客户端)</param>
        /// </summary>
        public static string ClientUpDataExe(string appName, string currVerson, string softType)
        {
            try
            {
                if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "this_ProgramUpdate.exe"))
                {
                    File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + "this_ProgramUpdate.exe");
                }

                string strJson = UpDateExe(softType);
                clsJsonUpExe objJson = Newtonsoft.Json.JsonConvert.DeserializeObject<clsJsonUpExe>(strJson);
                if (!objJson.flag.Equals("success"))
                {
                    return GetErrorMessage(objJson.flag);
                }
                else
                {
                    if (objJson.version.CompareTo(currVerson) > 0)//判断是否最新版本
                    {
                        //遍历该路径下的所有文件
                        if (Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "Data"))
                        {
                            DirectoryInfo di = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "Data");
                            foreach (FileInfo fi in di.GetFiles())
                            {
                                if (fi.Name.Equals("loginUser.Dat"))
                                {
                                    continue;
                                }
                                string exname = fi.Name.Substring(fi.Name.LastIndexOf(".") + 1);//得到后缀名
                                //判断当前文件后缀名是否与给定后缀名一样
                                if (exname.Equals("xml") || exname.Equals("Dat"))
                                {
                                    File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + "Data\\" + fi.Name);//删除当前文件
                                }
                            }
                        }

                        string strFileTemp = System.AppDomain.CurrentDomain.BaseDirectory + "\\upTemp";//升级文件下载临时存放文件夹
                        if (Directory.Exists(strFileTemp) == false)//如果不存在就创建文件夹
                        {
                            Directory.CreateDirectory(strFileTemp);//创建临时文件夹
                        }

                        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(objJson.address);
                        HttpWebResponse myRes = (HttpWebResponse)myReq.GetResponse();
                        objJson.size = myRes.ContentLength;
                        myRes.Close();

                        StreamWriter sw = File.CreateText(System.AppDomain.CurrentDomain.BaseDirectory + "\\upTemp\\uPversion.ini");
                        string key = objJson.version + "|" + objJson.size + "|" + objJson.address + "|" + appName;
                        //key = new MonitorClient.BLL.clsDEC().DESEnCode(key);
                        sw.Write(key);//保存要升级到的版本号及大小，升级程序来读取
                        sw.Close();

                        return "UP|" + objJson.version;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                CommonUtil.GlobalLogService.WriteLog("CheckUpdate", ex.Message);
                return "系统异常";
            }
        }


        #region 查询是否有升级版本
        /// <summary> 查询是否有升级版本
        /// </summary>
        /// <returns></returns>
        private static string UpDateExe(string softType)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("RequestType", softType);//RequestType   (1-慧修车服务端 2-慧修车客户端)

                string str = DoGet("http://119.57.151.34:1800/sspapp/version/findVersionNew.do", ht);
                return str;
            }
            catch (Exception ex)
            {
                CommonUtil.GlobalLogService.WriteLog("CheckUpdate", ex.Message);
                return "{\"error\":[{\"errorMessage\":\"" + ex.Message + "!\"},{ \"errorLevel\": \"1\" }]}";
            }
        }
        #endregion


        /// <summary> 错误提示转换汉字
        /// </summary>
        /// <param name="errorFlag">错误标记</param>
        /// <returns>对应汉字</returns>
        private static string GetErrorMessage(string errorFlag)
        {
            try
            {
                string result = string.Empty;
                switch (errorFlag)
                {
                    case "success":
                        result = "成功";
                        break;
                    case "fail":
                        result = "操作失败";
                        break;
                    case "error":
                        result = "系统异常";
                        break;
                    case "securityCodeError":
                        result = "验证码错误";
                        break;
                    case "loginnameIsNull":
                        result = "账号为空";
                        break;
                    case "corpCodeIsNull":
                        result = "企业编码为空";
                        break;
                    case "passIsNull":
                        result = "密码为空";
                        break;
                    case "exist":
                        result = "账号不存在";
                        break;
                    case "exists":
                        result = "账号存在多个";
                        break;
                    case "password":
                        result = "密码错误";
                        break;
                    case "outOfDate":
                        result = "账号已过期";
                        break;
                    case "logout":
                        result = "账号已注销";
                        break;
                    case "roleIsNull":
                        result = "账号没有可用角色";
                        break;
                    case "functionIsNull":
                        result = "账号无权限登录系统，请联系管理员。";
                        break;
                    case "nologin":
                        result = "未登录";
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                CommonUtil.GlobalLogService.WriteLog("CheckUpdate", ex.Message);
                return "系统异常";
            }
        }

        #endregion

        #region Get 发送请求
        /// <summary> http请求调用接口：Get调用
        /// </summary>
        /// <param name="url">接口地址</param>
        /// <param name="paramsOfUrl">传入数据</param>
        /// <returns>返回结果</returns>
        public static string DoGet(string url, Hashtable paramsOfUrl)
        {
            if (url == null)
            {
                throw new Exception("url地址为空");
            }
            if (paramsOfUrl != null)
            {
                // 编辑并Encoding提交的数据  
                string data = GetJointSOfParams(paramsOfUrl);

                // 拼接URL
                url += "?" + data;
            }


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
                CommonUtil.GlobalLogService.WriteLog("CheckUpdate", ex.Message);
                return "";
            }
            return responseData;
        }

        #region 内部方法
        /// <summary> 拼接参数串----Get
        /// </summary>
        /// <param name="paramsOfUrl">参数项</param>
        /// <returns>返回字符串</returns>
        private static String GetJointSOfParams(Hashtable paramsOfUrl)
        {
            if (paramsOfUrl == null)
            {
                return "";
            }
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
        #endregion
        #endregion
    }
}
