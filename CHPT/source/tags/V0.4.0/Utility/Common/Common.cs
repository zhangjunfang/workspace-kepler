using System;
using System.Net;
using System.Web;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;
using System.Data;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;

namespace Utility.Common
{
    public abstract class Common
    {
        public const string LOTTERY_TYPE_XYWL = "HNXYWL";
        public const string LOTTERY_TYPE_SSQ = "SSQ";
        public const string LOTTERY_TYPE_C522 = "HNC522";
        public const string LOTTERY_TYPE_D3 = "D3";
        public const string LOTTERY_TYPE_QLC = "QLC";

        public const string MERCHANT_NO = "102240054110004"; //商户编号
        public const string USER_ID = "12345678901234567890";

        /// <summary>
        /// 读取配置文件customConfig组下的自定义节点值
        /// </summary>
        /// <param name="sectionName">节点名称</param>
        /// <param name="nodeKey">节点Key</param>
        /// <returns>节点Value</returns>
        public static string GetConfigValue(string sectionName, string nodeKey)
        {
            System.Collections.Specialized.NameValueCollection section = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("customConfig/" + sectionName);
            return section.Get(nodeKey);
        }

        /// <summary>
        /// Http Post(utf-8编码)
        /// </summary>
        /// <param name="parms">请求参数</param>
        /// <returns>请求结果</returns>
        public static string HttpRequest(string parms)
        {
            string result = String.Empty;
            try
            {
                byte[] postData = Encoding.UTF8.GetBytes(parms);
                //old:http://124.42.83.34:8080/greatwallweb/main
                //new:http://124.42.83.34:9191/greatwallweb/main
                //official http://218.28.234.14:8080/greatwallweb/main
                //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://218.28.234.14:8080/greatwallweb/main");
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://124.42.83.34:8080/greatwallweb/main");
                webRequest.Timeout = 30000;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = postData.Length;

                Stream requestStream = webRequest.GetRequestStream();
                requestStream.Write(postData, 0, postData.Length);
                requestStream.Close();
                requestStream.Dispose();
                StreamReader sr = new StreamReader(webRequest.GetResponse().GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
            }
            catch { }
            return result;
        }

        /// <summary>
        /// 保存日志文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="content">文件内容</param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        public static void WriteLog(string path, string msg)
        {
            if (System.IO.File.Exists(path))
            {
                using (System.IO.StreamWriter sw = System.IO.File.AppendText(path))
                {
                    sw.WriteLine("Time: " + DateTime.Now.ToString());
                    sw.WriteLine("IP: " + HttpContext.Current.Request.UserHostAddress);
                    sw.WriteLine("URL: " + HttpContext.Current.Request.Url.ToString());
                    sw.WriteLine("Message: " + msg);
                    sw.Flush();
                    sw.Close();
                }
            }
            else
            {
                using (System.IO.StreamWriter sw = System.IO.File.CreateText(path))
                {
                    sw.WriteLine("Time: " + DateTime.Now.ToString());
                    sw.WriteLine("IP: " + HttpContext.Current.Request.UserHostAddress);
                    sw.WriteLine("URL: " + HttpContext.Current.Request.Url.ToString());
                    sw.WriteLine("Message: " + msg);
                    sw.Flush();
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// 发送Html格式邮件
        /// </summary>
        /// <param name="toEmail">目的邮箱地址</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        public static void SendEmail(string toEmail, string subject, string body)
        {
            using (MailMessage message = new MailMessage())
            {
                message.To.Add(new MailAddress(toEmail));
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient mailClient = new SmtpClient();
                mailClient.Send(message);
            }
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="mText">原字符串</param>
        /// <param name="byteCount">保留的长度</param>
        /// <returns>截取后的字符串</returns>
        public static string Left(string mText, int byteCount)
        {
            if (byteCount < 1) return mText;

            if (System.Text.Encoding.Default.GetByteCount(mText) <= byteCount)
            {
                return mText;
            }
            else
            {
                byte[] txtBytes = System.Text.Encoding.Default.GetBytes(mText);
                byte[] newBytes = new byte[byteCount];

                for (int i = 0; i < byteCount; i++)
                {
                    newBytes[i] = txtBytes[i];
                }
                string retStr = System.Text.Encoding.Default.GetString(newBytes);
                if (retStr.Substring(retStr.Length - 1) == "?")
                    retStr = retStr.Replace("?", "");
                return retStr;
            }
        }

        //public static bool SendEmail(string toEmail, string strName, string Uid)
        //{
        //    string Subject = "使用ASP.NET发送HTML格式邮件";
        //    jmail.Message Jmail = new jmail.Message();
        //    //读取HTML模板，即发送的页面
        //    string strPath = System.Web.HttpContext.Current.Server.MapPath("../email.htm");
        //    //读取文件，“System.Text.Encoding.Default”可以解决中文乱码问题
        //    StreamReader sr = new StreamReader(strPath, System.Text.Encoding.Default);
        //    StringBuilder body = new StringBuilder();
        //    body.Append(sr.ReadToEnd());
        //    //关闭文件流
        //    sr.Close();
        //    //替换指定内容，通常为需要变动的内容
        //    body = body.Replace("<%Message%>", strName);
        //    body = body.Replace("<%UserID%>", Uid);

        //    string FromEmail = "916567960@qq.com";
        //    //string ToEmail = "285747824@qq.com";
        //    string ToEmail = toEmail;
        //    //Silent属性：如果设置为true,JMail不会抛出例外错误. JMail. Send( () 会根据操作结果返回true或false
        //    Jmail.Silent = true;
        //    //Jmail创建的日志，前提loging属性设置为true
        //    Jmail.Logging = true;
        //    //字符集，缺省为"US-ASCII"
        //    Jmail.Charset = "GB2312";
        //    //信件的contentype. 缺省是"text/plain"） : 字符串如果你以HTML格式发送邮件, 改为"text/html"即可。
        //    Jmail.ContentType = "text/html";
        //    //添加收件人
        //    Jmail.AddRecipient(ToEmail, null, null);
        //    //Jmail.AddRecipient("916567960@qq.com", null, null);
        //    Jmail.From = FromEmail;
        //    //发件人邮件用户名
        //    Jmail.MailServerUserName = "916567960@qq.com";
        //    //发件人邮件密码
        //    Jmail.MailServerPassWord = "123456789";
        //    //设置邮件标题
        //    Jmail.Subject = Subject;
        //    // 设置优先级，范围从1到5，越大的优先级越高，3为普通
        //    Jmail.Priority = 5;
        //    Jmail.AddHeader("Originating-IP", "originating");
        //    //邮件添加附件,(多附件的话，可以再加一条Jmail.AddAttachment( "c:\\test.jpg",true,null);)就可以搞定了。［注］：加了附件，讲把上面的Jmail.ContentType="text/html";删掉。否则会在邮件里出现乱码。
        //    //Jmail.AddAttachment( "c:\\test.jpg",true,null);
        //    //邮件内容
        //    Jmail.Body = body.ToString().Trim();
        //    //Jmail.Body = "aaa";
        //    //Jmail发送的方法,可以修改,此为163邮箱服务器
        //    bool bSend = Jmail.Send("smtp.qq.com", false);
        //    Jmail.Close();
        //    Jmail.Clear();
        //    return bSend;
        //}

        /// <summary> 获取Utc时间Ticks
        /// </summary>
        /// <param name="dt">local时间DateTime</param>
        /// <returns>返回long</returns>
        public static long LocalDateTimeToUtcLong(DateTime dt)
        {
            long ticks = (dt.ToUniversalTime().Ticks - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime().Ticks) / 10000;
            return ticks;
        }

        /// <summary> 获取本地时间DateTime
        /// </summary>
        /// <param name="ticks">utc时间ticks</param>
        /// <returns>返回DateTime</returns>
        public static DateTime UtcLongToLocalDateTime(long ticks)
        {
            long tempTicks = DateTime.Now.ToUniversalTime().Ticks - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime().Ticks;
            if (ticks > tempTicks)
            {
                ticks = (ticks - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime().Ticks) / 10000;
            }

            System.DateTime startTime = new System.DateTime(1970, 1, 1).ToUniversalTime();
            DateTime dt = startTime.AddTicks(ticks * 10000).ToLocalTime();
            return dt;
        }

        /// <summary> 获取本地时间DateTime
        /// </summary>
        /// <param name="ticks">utc时间ticks</param>
        /// <returns>返回时间的String</returns>
        public static string UtcLongToLocalDateTime(object ticks)
        {
            return UtcLongToLocalDateTime(ticks, null);
        }

        /// <summary> 获取本地时间DateTime
        /// </summary>
        /// <param name="ticks">utc时间ticks</param>
        /// <param name="format">日期格式</param>
        /// <returns>返回时间的String</returns>
        public static string UtcLongToLocalDateTime(object ticks, string format)
        {
            if (ticks == null || ticks.ToString().Length == 0 || ticks == DBNull.Value)
            {
                return string.Empty;
            }
            long tTicks = Convert.ToInt64(ticks);
            if (!string.IsNullOrEmpty(format))
            {
                return UtcLongToLocalDateTime(tTicks).ToLocalTime().ToString(format);
            }
            else
            {
                return UtcLongToLocalDateTime(tTicks).ToLocalTime().ToString();
            }
        }

        /// <summary>
        /// 获取指定日期，在为一年中为第几周
        /// </summary>
        /// <param name="dt">指定时间</param>
        /// <reutrn>返回第几周</reutrn>
        public static int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekOfYear;
        }

        /// <summary> 设置容器内空间是否可用
        /// </summary>
        /// <param name="cnl">容器空间</param>
        /// <param name="isCanUse">是否可用</param>
        public static void SetControlEnable(Control pControl, bool isCanUse)
        {
            //Label lable = new Label();
            //lable.AutoSize = false;
            //lable.Size = cnl.Size;
            //lable.Location = cnl.Location;
            //pControl.Controls.Add(lable);
            //lable.Text = cnl.Text;
            //pControl.Controls.Remove(cnl);
            foreach (Control cnl in pControl.Controls)
            {
                if (cnl is TextBox)
                {
                    TextBox txt = cnl as TextBox;
                    txt.ReadOnly = !isCanUse;
                }
                else if (cnl is CheckBox || cnl is DateTimePicker || cnl is ServiceStationClient.ComponentUI.DateTimePickerEx || cnl is ComboBox || cnl is NumericUpDown || cnl is RadioButton || cnl is Button)
                {
                    cnl.Enabled = isCanUse;
                }
                else if (cnl is DataGridView)
                {
                    DataGridView dgv = cnl as DataGridView;
                    dgv.ReadOnly = !isCanUse;
                }
                else if (cnl is ServiceStationClient.ComponentUI.ButtonEx)
                {
                    cnl.Visible = false;
                }
                
                else if (cnl.HasChildren)
                {
                    SetControlEnable(cnl, isCanUse);
                }
            }
        }

        /// <summary>
        /// 正则表达式验证字符串是否匹配
        /// </summary>
        /// <param name="str">要验证的字符串</param>
        /// <param name="matchStr">要匹配正则表达式</param>
        /// <returns>匹配结果：0-匹配错误，1-匹配正确，2-正则表达式出错</returns>
        public static int ValidateStr(string str, string matchStr)
        {
            #region 常用匹配串
            #region 注释:匹配特定字符串：
            //匹配(部分号码段)手机号码:(?:1[378]\d||15[89])-?\d{5}(\d{3}|\*{3})  
            //匹配Email地址:\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*
            //匹配网址URL的正则表达式：[a-zA-z]+:\\[^\s]*
            //匹配帐号是否合法(字母开头，允许5-16字节，允许字母数字下划线)：^[a-zA-Z][a-zA-Z0-9_]{4,15}$
            //匹配国内电话号码：\d{3}-\d{8}|\d{4}-\d{7}
            //匹配腾讯QQ号：[1-9][0-9]{4,}
            //匹配中国邮政编码：[1-9]\d{5}(?!\d)
            //匹配身份证：\d{15}|\d{18}
            //匹配ip地址：\d+\.\d+\.\d+\.\d+
            #endregion

            #region 注释:匹配特定数字：
            //匹配正整数:^[1-9]\d*$　 　
            //匹配负整数:^-[1-9]\d*$ 　 
            //匹配整数:^-?[1-9]\d*$　　 
            //匹配非负整数（正整数 + 0）:^[1-9]\d*|0$　 
            //匹配非正整数（负整数 + 0）:^-[1-9]\d*|0$　　 
            //匹配正浮点数:^[1-9]\d*\.\d*|0\.\d*[1-9]\d*$　　
            //匹配负浮点数:^-([1-9]\d*\.\d*|0\.\d*[1-9]\d*)$　 
            //匹配浮点数:^-?([1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0)$　 
            //匹配非负浮点数（正浮点数 + 0）:^[1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0$　　 
            //匹配非正浮点数（负浮点数 + 0）:^(-([1-9]\d*\.\d*|0\.\d*[1-9]\d*))|0?\.0+|0$　　
            #endregion

            #region 注释: 匹配特定字符串：
            //匹配由26个英文字母组成的字符串:^[A-Za-z]+$　　
            //匹配由26个英文字母的大写组成的字符串:^[A-Z]+$　　
            //匹配由26个英文字母的小写组成的字符串:^[a-z]+$　　
            //匹配由数字和26个英文字母组成的字符串:^[A-Za-z0-9]+$　　
            //匹配由数字、26个英文字母或者下划线组成的字符串:^\w+$　　
            #endregion
            #endregion
            try
            {
                bool b = Regex.IsMatch(str, matchStr);
                return b ? 1 : 0;
            }
            catch (Exception)
            {
                return 2;
            }
        }


        /// <summary> 判断程序是否启动
        /// </summary>
        public static bool IsCurrentProcessExist()
        {
            string name = Process.GetCurrentProcess().ProcessName;
            int id = Process.GetCurrentProcess().Id;
            Process[] nProcess = Process.GetProcesses();
            foreach (Process process in nProcess)
            {
                if ((name == process.ProcessName) && (process.Id != id))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
