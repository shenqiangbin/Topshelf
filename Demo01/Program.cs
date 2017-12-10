using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Demo01
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.UseLog4Net("log4net.config");

                x.Service<TownCrier>(s =>
                {
                    s.ConstructUsing(name => new TownCrier());
                    s.WhenStarted(m => m.Start());
                    s.WhenStopped(m => m.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription("这里是服务的描述");
                x.SetDisplayName("这里是展示名称");
                x.SetServiceName("这是服务名称");
            });
        }
    }
}
