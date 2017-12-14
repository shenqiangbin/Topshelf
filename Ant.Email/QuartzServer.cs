using Quartz;
using Quartz.Impl;
using System;
using Topshelf;
using Topshelf.Logging;

namespace Ant.Email
{
    public class QuartzServer : ServiceControl, ServiceSuspend
    {
        private ISchedulerFactory schedulerFactory;
        private IScheduler scheduler;
        private LogWriter logger;

        public QuartzServer()
        {
            logger = HostLogger.Get(GetType());
        }

        public void Initialize()
        {
            try
            {
                schedulerFactory = new StdSchedulerFactory();
                scheduler = schedulerFactory.GetScheduler();
            }
            catch (Exception ex)
            {
                logger.Error("服务初始化失败：" + ex.Message + ex.StackTrace);
            }

            logger.Info("服务初始化成功");
        }

        public bool Start(HostControl hostControl)
        {
            try
            {
                scheduler.Start();
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Scheduler start failed: {0}", ex.Message), ex);
                throw;
            }

            logger.Info("Scheduler started successfully");

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            try
            {
                scheduler.Shutdown(false);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Scheduler stop failed: {0}", ex.Message), ex);
            }

            logger.Info("Scheduler shutdown complete");

            return true;
        }

        public bool Continue(HostControl hostControl)
        {
            scheduler.ResumeAll();
            return true;
        }

        public bool Pause(HostControl hostControl)
        {
            scheduler.PauseAll();
            return true;
        }
    }
}