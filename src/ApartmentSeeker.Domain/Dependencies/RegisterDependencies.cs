using ApartmentScrapper.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ApartmentScrapper.Domain.Dependencies
{
    public static class RegisterDependencies
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddTransient<IEvaluateApartment, EvaluateApartment>();
            services.AddTransient<IFindNewApartments, FindNewApartments>();
            services.AddTransient<INotifyNewApartment, NotifyNewApartment>();
            services.AddTransient<IRunner, Runner>();

            return services;
        }
    }
}