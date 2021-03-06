﻿using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf.Logging;

namespace Ant.Email
{
    [DisallowConcurrentExecution]
    public class SampleJob : IJob
    {
        private LogWriter logger = HostLogger.Get<SampleJob>();

        public void Execute(IJobExecutionContext context)
        {
            logger.Error(DateTime.Now.ToString());
        }
    }
}
