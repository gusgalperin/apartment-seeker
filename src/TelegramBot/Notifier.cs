using ApartmentScrapper.Domain.Notifier;
using ApartmentScrapper.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ApartmentScrapper.TelegramBot
{
    public class Notifier : Bot, INotifier
    {
        public Notifier()
        {
        }

        public async Task NotifyAsync(Entities.User user, Apartment apartment)
        {
            Message sentMessage = await _telegramBotClient.SendPhotoAsync(
                chatId: user.ChatId, 
                photo: InputFile.FromUri(apartment.Thumbnail),
                caption: $"<b>{apartment.Source} -> {apartment.Title.Trim()} | {apartment.Price}</b>. <a href=\"{apartment.Permalink}\">LINK</a>",
                parseMode: ParseMode.Html);
        }

        public async Task SendHelpAsync(Entities.User user)
        {
            Dictionary<string, string> commands = new Dictionary<string, string>
            {
                { "arranca", "Empieza la busqueda" },
                { "frena", "Frena la búsqueda" }
            };

            // Send the menu message
            await _telegramBotClient.SendTextMessageAsync(user.ChatId, "Welcome to my bot! I can help you with the following tasks:");
            foreach (var command in commands)
            {
                await _telegramBotClient.SendTextMessageAsync(user.ChatId, "* /" + command.Key + " - " + command.Value);
            }
        }
    }
}