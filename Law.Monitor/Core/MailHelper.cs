using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Topshelf.Logging;

namespace Law.Monitor.Core
{
    public class MailHelper
    {
        private LogWriter logger = HostLogger.Get<MailHelper>();

        public bool SendEmail(List<string> toList, List<string> copyList, string subject, string body)
        {
            //如果是QQ邮箱，需要启用POP3服务，且密码使用的是 授权码，而不是密码
            string eamil = "1348907384@qq.com";
            string pwd = "xkwmjgjupmwjfhbc";

            try
            {
                MailAddress from = new MailAddress(eamil);
                System.Net.Mail.MailMessage MyMessage = new System.Net.Mail.MailMessage();
                MyMessage.From = from;

                if (toList != null)
                {
                    foreach (var item in toList)
                    {
                        MyMessage.To.Add(item);
                    }
                }
                if (copyList != null)
                {
                    foreach (var item in copyList)
                    {
                        MyMessage.CC.Add(item);
                    }
                }
                
                MyMessage.Priority = System.Net.Mail.MailPriority.Normal;
                MyMessage.IsBodyHtml = false;
                MyMessage.IsBodyHtml = true;

                MyMessage.Body = body;
                MyMessage.BodyEncoding = System.Text.Encoding.UTF8;
                MyMessage.Subject = subject;
                //邮件服务器和端口 
                //这里使用你服务器端发送的邮箱smtp协议 新浪邮箱：smtp.sina.com  网易邮箱：smtp.126.com  QQ邮箱：smtp.qq.com ，QQ企业邮箱的：smtp.exmail.qq.com
                string SmtpServer = "smtp.qq.com";
                SmtpClient client = new SmtpClient(SmtpServer, 25);
                System.Net.NetworkCredential cred = new System.Net.NetworkCredential(eamil, pwd);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = cred;
                client.Send(MyMessage);

                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ex.StackTrace);
                return false;
                //return exp.Message;
            }
        }

    }
}
