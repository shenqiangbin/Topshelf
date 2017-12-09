using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Topshelf.Logging;

namespace Demo01
{
    public class TownCrier
    {
        private readonly Timer _timer;
        static readonly LogWriter _log = HostLogger.Get<TownCrier>();

        public TownCrier()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _log.Debug(DateTime.Now);          
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
