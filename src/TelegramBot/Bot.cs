using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ApartmentScrapper.TelegramBot
{
    public abstract class Bot
    {
        private readonly string _token = "6468069063:AAE6W7FqZ9d-sf-j-t1HDPV3hy0eEfsdnHI";
        protected readonly TelegramBotClient _telegramBotClient;

        public Bot()
        {
            _telegramBotClient = new TelegramBotClient(_token);
        }
    }
}
