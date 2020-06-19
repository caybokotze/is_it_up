using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;

namespace IsItUpOrDown
{
    public class Helper
    {
        public static int Interval = 60_000;
        //
        public static int IncrementValue(Dictionary<string, int> dictionary, string key)
        {
            var valuePair = dictionary.First(f => f.Key == key);
            var value = valuePair.Value;
            value++;
            return value;
        }
        //
        public static TelegramBotClient TelegramBotInstance()
        {
            return new TelegramBotClient("TELEGRAM_SECRET");
        }
        //
        public static string ConnectionString = "DataSource=app.db";
        public static int AdminChatId = 1149447052;
    }
}