using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Ant.UserView
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Core.SendHelper().Send("http://alk.12348.gov.cn/");

            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);

            HostFactory.Run(x =>
            {
                x.UseLog4Net("log4net.config");

                x.RunAsLocalSystem();

                x.SetDescription("��ط���");
                x.SetDisplayName("law.monitor");
                x.SetServiceName("law.monitor");

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
