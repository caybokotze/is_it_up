using System;
using System.Threading.Tasks;
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
            Bot.RegisterLifecycle();
            Console.WriteLine("Service has started.");
            SetTimer();
            //
            SetLongRunningTimer();
            //
            Bot.RegisterLifecycle();
            //
            Bot.AlertAdmin("The notification service has spun up at: " + DateTime.Now.ToString("G"));
        }

        private static void SetTimer()
        {
            _timer = new Timer(Helper.Interval);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private static void SetLongRunningTimer()
        {
            _timer = new Timer(86400_000); // Once per day.
            _timer.Elapsed += OnLongRunningTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private static async void OnLongRunningTimedEvent(Object source, ElapsedEventArgs e)
        {
            Bot.AlertAdmin("Service is still active.");
        }

        private static async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Bot.RegisterLifecycle();
            await WebsiteChecker.Initialise();
        }

        public void Stop()
        {
            Bot.AlertAdmin("The notification service has been suspended at: " + DateTime.Now.ToString("G"));
            _timer.Stop();
        }
    }
}