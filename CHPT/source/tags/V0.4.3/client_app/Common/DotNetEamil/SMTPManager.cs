using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace HXCCommon.DotNetEamil
{
    /// <summary>
    /// 发送邮件
    /// </summary>
    public class SMTPManager
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="Dep_Email">发送人、支持发送多个人每个地址用 ; 号隔开</param>
        /// <param name="Mis_Name">任务名称</param>
        /// <param name="Mis_Describe">内容</param>
        /// <param name="File_Path">附件</param>
        /// <returns></returns>
        public static string MailSending(string Dep_Email, string Mis_Name, string Mis_Describe, string File_Path)
        {
            string MailUser = System.Configuration.ConfigurationManager.AppSettings["MailUser"].ToString();
            string MailName = System.Configuration.ConfigurationManager.AppSettings["MailName"].ToString();
            string MailHost = System.Configuration.ConfigurationManager.AppSettings["MailHost"].ToString();
            string MailPwd = System.Configuration.ConfigurationManager.AppSettings["MailPwd"].ToString();
            MailAddress from = new MailAddress(MailUser, MailName); //邮件的发件人
            MailMessage mail = new MailMessage();

            //设置邮件的标题
            mail.Subject = Mis_Name;//任务名称

            //设置邮件的发件人
            //Pass:如果不想显示自己的邮箱地址，这里可以填符合mail格式的任意名称，真正发mail的用户不在这里设定，这个仅仅只做显示用
            mail.From = from;

            //设置邮件的收件人
            string address = "";
            string displayName = "";
            /**/
            /*  这里这样写是因为可能发给多个联系人，每个地址用 ; 号隔开
一般从地址簿中直接选择联系人的时候格式都会是 ：用户名1 < mail1 >; 用户名2 < mail 2>; 
因此就有了下面一段逻辑不太好的代码
如果永远都只需要发给一个收件人那么就简单了 mail.To.Add("收件人mail");
*/
            string[] mailNames = (Dep_Email + ";").Split(';');
            foreach (string name in mailNames)
            {
                if (name != string.Empty)
                {
                    if (name.IndexOf('<') > 0)
                    {
                        displayName = name.Substring(0, name.IndexOf('<'));
                        address = name.Substring(name.IndexOf('<') + 1).Replace('>', ' ');
                    }
                    else
                    {
                        displayName = string.Empty;
                        address = name.Substring(name.IndexOf('<') + 1).Replace('>', ' ');
                    }
                    mail.To.Add(new MailAddress(address, displayName));
                }
            }

            //设置邮件的抄送收件人
            //这个就简单多了，如果不想快点下岗重要文件还是CC一份给领导比较好
            //mail.CC.Add(new MailAddress("Manage@hotmail.com", "尊敬的领导");

            //设置邮件的内容
            mail.Body = Mis_Describe;
            //设置邮件的格式
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            //设置邮件的发送级别
            mail.Priority = MailPriority.Normal;

            //设置邮件的附件，将在客户端选择的附件先上传到服务器保存一个，然后加入到mail中
            if (File_Path != "")
            {
                mail.Attachments.Add(new Attachment(File_Path));
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            } SmtpClient client = new SmtpClient();
            //设置用于 SMTP 事务的主机的名称，填IP地址也可以了
            client.Host = MailHost;
            //设置用于 SMTP 事务的端口，默认的是 25
            client.Port = 25;
            client.UseDefaultCredentials = false;
            //这里才是真正的邮箱登陆名和密码， 我的用户名为 MailUser ，我的密码是 MailPwd
            client.Credentials = new System.Net.NetworkCredential(MailUser, MailPwd);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            ////如果发送失败，SMTP 服务器将发送 失败邮件告诉我
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            //都定义完了，正式发送了，很是简单吧！
            client.Send(mail);
            return mail.ToString();
        }
    }
}
