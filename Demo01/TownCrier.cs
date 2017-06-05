﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Demo01
{
    public class TownCrier
    {
        private readonly Timer _timer;
        public TownCrier()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(DateTime.Now);
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
