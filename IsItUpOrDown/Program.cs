using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Topshelf;

namespace IsItUpOrDown
{

    class Program
    {
        static void Main()
        {
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
    
    class WebsiteChecker
    {
        public static async Task Initialise()
        {
            var websiteCheckList = new List<Check>
            {
                new Check()
                {
                    Name = "Merge Africa",
                    Timeout = 15,
                    Url = "https://www.merge.africa"
                },
                new Check()
                {
                    Name = "Arya Block",
                    Timeout = 20,
                    Url = "https://www.aryablock.com"
                },
                new Check()
                {
                    Name = "Roag",
                    Timeout = 15,
                    Url = "https://www.roag.org"
                }
            };

            Console.WriteLine("Checking your website.");

            await RunTasks(websiteCheckList);

            Console.WriteLine("Check list is complete.");

            Console.ReadLine();
        }

        private static async Task RunTasks(List<Check> items)
        {
            var tasks = new List<Task>();
            //
            foreach (var item in items)
            {
                tasks.Add(Ping(item));
            }

            //
            await Task.WhenAll(tasks);
        }

        private static async Task<bool> Ping(Check checkItem)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(checkItem.Url);
                request.Timeout = checkItem.Timeout * 1000;
                request.AllowAutoRedirect = true; // find out if this site is up and don't follow a redirector
                request.Method = "HEAD";
                await request.GetResponseAsync();
                //

                if (request.HaveResponse)
                {
                    Console.WriteLine($"{checkItem.Url} - True");

                    return true;
                }

                //
                Console.WriteLine("url doesn't have a reponse.");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{checkItem.Url} - False");
                Console.WriteLine(e.Message);
                //
                NotificationHandler.Send(new WebsiteError()
                {
                    WebsiteName = checkItem.Name, Error = e.Message
                });
                return false;
            }

        }
    }
}
