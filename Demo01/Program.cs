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
                x.Service<TownCrier>(s =>
                {
                    s.ConstructUsing(name => new TownCrier());
                    s.WhenStarted(m => m.Start());
                    s.WhenStopped(m => m.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription("Sample Topshelf");
                x.SetDisplayName("stuff");
                x.SetServiceName("stuff");
            });
        }
    }
}
