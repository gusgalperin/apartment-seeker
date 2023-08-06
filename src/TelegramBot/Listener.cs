using ApartmentScrapper.Domain.Data.Repositories;
using ApartmentScrapper.Domain.Notifier;
using ApartmentScrapper.Domain.Services;
using ApartmentScrapper.Utils.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace ApartmentScrapper.TelegramBot
{
    public interface IListener
    {
        Task StartReceiving();
    }

    public class Listener : Bot, IListener
    {
        private readonly ILogger<Listener> _logger;
        private readonly IUserRepository _userRepository;
        private readonly INotifier _notifier;
        private readonly IRunner _runner;

        public Listener(
            ILogger<Listener> logger,
            IUserRepository userRepository,
            INotifier notifier,
            IRunner runner)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));
            _runner = runner ?? throw new ArgumentNullException(nameof(runner));
        }

        public async Task StartReceiving()
        {
            _telegramBotClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync);

            var me = await _telegramBotClient.GetMeAsync();

            _logger.LogInfo($"Start listening for @{me.Username}");
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
                return;
            // Only process text messages
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            _logger.LogInfo($"Received a '{messageText}' message in chat {chatId}.");

            var user = await _userRepository.GetOrDefaultAsync(chatId);

            if (user is null)
            {
                user = new Entities.User(chatId, Entities.SearchCriteria.Default());
                await _userRepository.AddAsync(user);
            }

            switch (messageText.ToLower())
            {
                case BotCommands.ARRANCA:

                    user.Run();
                    await _userRepository.UpdateAsync(user);
                    break;

                case BotCommands.FRENA:
                    user.NoRun();
                    await _userRepository.UpdateAsync(user);
                    break;

                default:
                    await _notifier.SendHelpAsync(user);
                    break;
            }

            // Echo received message text
            //Message sentMessage = await botClient.SendTextMessageAsync(
            //    chatId: chatId,
            //    text: "You said:\n" + messageText,
            //    cancellationToken: cancellationToken);
        }

        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
