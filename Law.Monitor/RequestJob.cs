using Law.Monitor.Core;
using Law.Monitor.Util;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf.Logging;

namespace Law.Monitor
{
    [DisallowConcurrentExecution]
    public class RequestJob : IJob
    {
        private LogWriter logger = HostLogger.Get<RequestJob>();

        public void Execute(IJobExecutionContext context)
        {
            logger.Info("RequestJob开始执行");

            try
            {
                string urls = JsonConfig.Get("monitorUrls");
                string[] urlList = urls.Split(new char[] { ';' });

                SendHelper sendHelper = new SendHelper();
                MailHelper mailHelper = new MailHelper();
                foreach (var url in urlList)
                {
                    SendResult sendResult = sendHelper.Send(url);
                    if (sendResult.Success == false)
                    {
                        string key = url + "-sendTime";
                        string sendTime = JsonConfig.Get(key);
                        string timeInterval = JsonConfig.Get("timeInterval");

                        if (string.IsNullOrEmpty(sendTime) || DateTime.Now > DateTime.Parse(sendTime).AddMinutes(int.Parse(timeInterval)))
                        {
                            string toListStr = JsonConfig.Get("toList");
                            string[] toList = toListStr.Split(new char[] { ';' });
                            string subject = JsonConfig.Get("subject");
                            string content = string.Format("{0} <br/> {1} 访问无效 </br> {2} {3} ", DateTime.Now, url, sendResult.Code, sendResult.Msg);
                            bool sendMailResult = mailHelper.SendEmail(toList.ToList(), null, subject, content);
                            if (sendMailResult)
                                JsonConfig.Add(key, DateTime.Now.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ex.StackTrace);
            }

            logger.Info("RequestJob执行结束");
        }
    }
}
