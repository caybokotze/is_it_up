using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Timers;
using Topshelf;

namespace IsItUpOrDown
{
    class Program
    {
        //
        static void Main(string[] args)
        {
            try
            {
                DataAccess.CreateTables();
                DbSeeder.Seed();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            //
            HostFactory.Run(x =>
            {
                x.StartAutomatically();
                x.EnableServiceRecovery(rc => rc.RestartService(1));
                x.Service<Service>(s =>
                {
                    s.ConstructUsing(hostSettings => new Service(hostSettings));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("The service will periodically check whether your websites are up or not.");
                x.SetDisplayName("Is it up?");
                x.SetServiceName("Is It Up?");
            });
        }    
    }
}
