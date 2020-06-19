using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace IsItUpOrDown
{
    public class Bot
    {
        public static void Send(long chatId, string message)
        {
            _botClient = Helper.TelegramBotInstance();
            var me = _botClient.GetMeAsync().Result;
            Console.WriteLine();
            //_botClient.OnMessage += Bot_OnMessage;
            _botClient.StartReceiving();
            _botClient.SendTextMessageAsync(chatId, message);
        }
        //
        public static void AlertAdmin(string message)
        {
            Send(Helper.AdminChatId, message);
        }
        //
        
        private static ITelegramBotClient _botClient;
        //
        public static void RegisterLifecycle()
        {
            _botClient = Helper.TelegramBotInstance();
            var me = _botClient.GetMeAsync().Result;
            Console.WriteLine();
            _botClient.OnMessage += Bot_OnMessage;
            _botClient.StartReceiving();
            //_botClient.SendTextMessageAsync(1149447052, message);
            Thread.Sleep(Helper.Interval);
        }
        //
        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                if (e.Message.Text.Contains("/notify"))
                {
                    try
                    {
                        if (!DataAccess.FetchUserByChatId(e.Message.Chat.Id).Equals(null))
                        {
                            await _botClient.SendTextMessageAsync(e.Message.Chat,
                                "You have already subscribed to this list.");
                        }
                        else
                        {
                            DataAccess.AddUser(new User()
                            {
                                ChatId = e.Message.Chat.Id,
                                Name = e.Message.Chat.FirstName
                            });

                            await _botClient.SendTextMessageAsync(e.Message.Chat,
                                $"Thanks {e.Message.Chat.FirstName}! You have been added to the notification list.");
                        }
                    }
                    catch
                    {
                        DataAccess.AddUser(new User()
                        {
                            ChatId = e.Message.Chat.Id,
                            Name = e.Message.Chat.FirstName
                        });

                        await _botClient.SendTextMessageAsync(e.Message.Chat,
                            $"Thanks {e.Message.Chat.FirstName}! You have been added to the notification list.");
                    }
                }
                //
                if (e.Message.Text.Contains("/my-chat"))
                {
                    await _botClient.SendTextMessageAsync(e.Message.Chat, "Your chat Id is: " + e.Message.Chat.ToString());
                }

                Console.WriteLine(e.Message.Text);
            }
        }
    }
}