using ApartmentScrapper.Domain.Dependencies;
using ApartmentScrapper.Infrastructure.Data.Mongo;
using ApartmentScrapper.Infrastructure.Dependencies;
using ApartmentScrapper.TelegramBot;
using ApartmentScrapper.Utils.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddDomain()
    .AddInfrastructure()
    .AddTelegramBot();

builder.Services.AddLogger();

using IHost host = builder.Build();

MongoSetup.OnStartup();

var listener = host.Services.GetRequiredService<IListener>();
await listener.StartReceiving();

await host.RunAsync();