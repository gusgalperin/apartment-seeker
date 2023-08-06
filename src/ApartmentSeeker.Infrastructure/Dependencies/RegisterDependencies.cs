using ApartmentScrapper.Domain.Data;
using ApartmentScrapper.Domain.Data.Repositories;
using ApartmentScrapper.Infrastructure.Data.Mongo;
using ApartmentScrapper.Infrastructure.Data.Mongo.Repositories;
using ApartmentScrapper.Utils.RestClient;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ApartmentScrapper.Infrastructure.Dependencies
{
    public static class RegisterDependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IFindApartments, Data.External.Argenprop.FindApartments>();
            services.AddTransient<IFindApartments, Data.External.Meli.FindApartments>();
            services.AddTransient<IApartmentRepository, ApartmentRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddSingleton(sp => new MongoClient("mongodb://localhost:27017"));
            services.AddTransient<DataContext>();

            services.AddRestClient();

            return services;
        }
    }
}