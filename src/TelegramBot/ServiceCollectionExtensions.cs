using ApartmentScrapper.Domain.Notifier;
using Microsoft.Extensions.DependencyInjection;

namespace ApartmentScrapper.TelegramBot
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramBot(this IServiceCollection services)
        {
            services.AddTransient<INotifier, Notifier>();
            services.AddTransient<IListener, Listener>();

            return services;
        }
    }
}