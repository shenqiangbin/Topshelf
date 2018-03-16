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
    public class ServiceJob : IJob
    {
        private LogWriter logger = HostLogger.Get<ServiceJob>();

        public void Execute(IJobExecutionContext context)
        {
            logger.Info("ServiceJob开始执行");

            try
            {
                string names = JsonConfig.Get("monitorServerNames");
                string machineName = JsonConfig.Get("machineName");
                string[] urlList = names.Split(new char[] { ';' });

                ServiceMonitorHelper monitorHelper = new ServiceMonitorHelper();
                MailHelper mailHelper = new MailHelper();
                foreach (var url in urlList)
                {
                    MonitorResult monitorResult = monitorHelper.Send(url);
                    if (monitorResult.Success == false)
                    {
                        string key = url + "-sendTime";
                        string sendTime = JsonConfig.Get(key);
                        string timeInterval = JsonConfig.Get("timeInterval");

                        if (string.IsNullOrEmpty(sendTime) || DateTime.Now > DateTime.Parse(sendTime).AddMinutes(int.Parse(timeInterval)))
                        {
                            string toListStr = JsonConfig.Get("toList");
                            string[] toList = toListStr.Split(new char[] { ';' });
                            string subject = JsonConfig.Get("monitorSubject");
                            string content = string.Format("{0} <br/> {3} {1} 服务挂掉了 </br> {2} ", DateTime.Now, url, monitorResult.Msg, machineName);
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

            logger.Info("ServiceJob执行结束");
        }
    }
}
