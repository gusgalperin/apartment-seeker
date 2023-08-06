using Microsoft.Extensions.DependencyInjection;

namespace ApartmentScrapper.Utils.Logger
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLogger(this IServiceCollection services)
        {
            services.AddTransient(typeof(ILogger<>), typeof(ConsoleLogger<>));

            return services;
        }
    }
}