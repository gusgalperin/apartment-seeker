using Microsoft.Extensions.DependencyInjection;

namespace ApartmentScrapper.Utils.RestClient
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRestClient(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddTransient<IRestClient, RestClient>();

            return services;
        }
    }
}