using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.ServiceProcess;

namespace Law.Monitor.Core
{
    public class ServiceMonitorHelper
    {
        private Topshelf.Logging.LogWriter logger = Topshelf.Logging.HostLogger.Get<ServiceMonitorHelper>();

        public MonitorResult Send(string serviceName)
        {
            MonitorResult result = new MonitorResult();

            try
            {
                ServiceController service = GetService(serviceName);
                if (service == null)
                {
                    result.Success = false;
                    result.Msg = "没有找到此服务";
                }
                else if (!CheckService(service))
                {
                    result.Success = false;
                    result.Msg = "服务没有启动,开始自启动";
                    StartService(service);
                    result.Msg += "-服务启动成功";
                }
                else
                {
                    result.Success = true;
                }                
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Msg = ex.Message + ex.StackTrace;
                logger.Error(result.Msg);
            }

            return result;
        }

        private bool CheckService(ServiceController service)
        {
            bool result = true;
            //判断服务状态（Stopped：服务停止， StopPending：服务正在停止）     <requestedExecutionLevel level="requireAdministrator" uiAccess="false" />
            if ((service.Status == ServiceControllerStatus.Stopped) || (service.Status == ServiceControllerStatus.StopPending))
            {
                result = false;
            }
            return result;
        }

        private ServiceController GetService(string serviceName)
        {
            //获取本机所有的服务
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController service in services)
            {
                if (service.ServiceName.Trim() == serviceName.Trim())
                {
                    return service;
                }
            }

            return null;
        }

        /// <summary>
        /// 开启指定服务
        /// </summary>
        /// <param name="serviceName">检测的服务名称</param>
        private void StartService(ServiceController service)
        {
            //开启服务
            service.Start();
            service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));
        }
    }

    public class MonitorResult
    {
        public bool Success { get; set; }
        public string Msg { get; set; }
    }
}
