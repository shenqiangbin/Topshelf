using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Demo02
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));

            HostFactory.Run(x =>
            {
                x.UseLog4Net();

                x.Service<ServiceRunner>();

                x.RunAsLocalSystem();

                x.SetDescription("QuartzDemo");
                x.SetDisplayName("QuartzDemo");
                x.SetServiceName("QuartzDemo");

                x.EnablePauseAndContinue();

            });
        }
    }
}
