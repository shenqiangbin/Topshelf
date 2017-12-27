using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Law.Monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);

            HostFactory.Run(x =>
            {
                x.UseLog4Net("log4net.config");

                x.RunAsLocalSystem();

                x.SetDescription("ant.test.description");
                x.SetDisplayName("ant.test.testService");
                x.SetServiceName("ant.test.testService");

                x.Service(factory =>
                {
                    QuartzServer server = new QuartzServer();
                    server.Initialize();
                    return server;
                });
            });
        }
    }
}
