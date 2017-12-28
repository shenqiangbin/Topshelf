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
    public class SampleJob : IJob
    {
        private LogWriter logger = HostLogger.Get<SampleJob>();

        public void Execute(IJobExecutionContext context)
        {
            logger.Error("SampleJob:" + DateTime.Now.ToString());
        }
    }
}
