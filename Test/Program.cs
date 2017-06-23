using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Topshelf;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Log(DateTime.Now.ToString());
            SqlLogger.Log("这里是一个消息");
            SqlLogger.Log(new { userid = 123,msg="haha" });
            SqlLogger.Log(new NotImplementedException());

            //HostFactory.Run(x =>
            //{
            //    x.Service<TestService>(c =>
            //    {
            //        c.ConstructUsing(name => new TestService());
            //        c.WhenStarted(m => m.Start());
            //        c.WhenStopped(m => m.Stop());

            //    });

            //    x.SetDisplayName("");
            //    x.RunAsLocalSystem();
            //});
        }
    }

    class TestService
    {
        private Timer _timer;
        public TestService()
        {
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //System.IO.File.AppendAllText("d:/log.txt", DateTime.Now.ToString());
            //Logger.Log(DateTime.Now.ToString());
            //Console.WriteLine(DateTime.Now);
            SqlLogger.Log(DateTime.Now.ToString());
        }

        internal void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

    }
}
