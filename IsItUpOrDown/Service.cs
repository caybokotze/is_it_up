using System;
using System.Timers;
using Topshelf.Runtime;

namespace IsItUpOrDown
{
    public class Service
    {
        private static System.Timers.Timer _timer;

        public Service(HostSettings hostSettings)
        {
            
        }

        public void Start()
        {
            SetTimer();
        }

        public static void SetTimer()
        {
            _timer = new Timer(30_000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}