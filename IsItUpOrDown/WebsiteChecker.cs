using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IsItUpOrDown
{
    class WebsiteChecker
    {
        public static async Task Initialise()
        {
            
            Console.WriteLine("Checking your website.");

            var websites = DataAccess.GetSites();

            await RunTasks(websites);

            Console.WriteLine("Check list is complete.");

            Console.ReadLine();
        }

        private static async Task RunTasks(List<Website> items)
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

        private static async Task<bool> Ping(Website checkItem)
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
                    //
                    checkItem.RetryCount = 0;
                    DataAccess.UpdateSite(checkItem);
                    //
                    return true;
                }

                //
                Console.WriteLine("url doesn't have a reponse.");
                return false;
            }
            catch (Exception e)
            {
                //
                Console.WriteLine($"{checkItem.Url} - False");
                Console.WriteLine(e.Message);
                //
                if (checkItem.RetryCount >= 3)
                {
                    NotificationHandler.Send(new WebsiteError()
                    {
                        WebsiteName = checkItem.Name, 
                        Error = e.Message,
                        Url = checkItem.Url
                    });
                    //
                    checkItem.RetryCount = 0;
                    DataAccess.UpdateSite(checkItem);
                }
                //
                checkItem.RetryCount++;
                DataAccess.UpdateSite(checkItem);
                //
                return false;
            }

        }
    }
}