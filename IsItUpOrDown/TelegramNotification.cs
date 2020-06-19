using System;
using System.Threading;
using System.Timers;
using RestSharp;
using RestSharp.Authenticators;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace IsItUpOrDown
{
    class TelegramNotification
    {
        private WebsiteError _websiteError;
        private static ITelegramBotClient _botClient;
        //
        public TelegramNotification(WebsiteError error)
        {
            _websiteError = error;
        }

        public string GetOutput()
        {
            return $"The website: '{_websiteError.Url}' has failed to respond. \nThe error message is: {_websiteError.Error}";
        }
        //
        public void OnNotificationSent()
        {
            try
            {
                foreach (var user in DataAccess.FetchUsers())
                {
                    Bot.Send(user.ChatId, GetOutput());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}