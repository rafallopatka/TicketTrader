using System;
using System.Reflection;
using TicketTrader.Customers.Domain;
using TicketTrader.Customers.Persistence.Mongo;
using TicketTrader.Shared.Base.App;
using TicketTrader.Shared.Bus.RabbitMq;
using TicketTrader.Shared.Persistence.Mongo.Domain;
using TicketTrader.Shared.Persistence.Mongo.ReadSide;

namespace TicketTrader.Customers.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var domainRepositoryConnectionString = Environment.GetEnvironmentVariable("DOMAIN_REPOSITORY_CONNECTION_STRING");
            var domainStoreName = Environment.GetEnvironmentVariable("DOMAIN_REPOSITORY_STORE_NAME");

            var readRepositoryConnectionString = Environment.GetEnvironmentVariable("READ_REPOSITORY_CONNECTION_STRING");
            var readStoreName = Environment.GetEnvironmentVariable("READ_REPOSITORY_STORE_NAME");

            var assembly = Assembly.GetAssembly(typeof(Customer));
            var readSideAssembly = Assembly.GetAssembly(typeof(CustomerReadSideHook));

            var host = App.Current
                .UseRabbitMqBus()
                .UseMongoOnWriteSide(domainRepositoryConnectionString, domainStoreName)
                .UseMongoOnReadSide(readRepositoryConnectionString, readStoreName)
                .RegisterCommandHandlers(assembly)
                .RegisterEventHandlers(assembly)
                .RegisterQueryHandlers(assembly)
                .RegisterFinders(readSideAssembly)
                .RegisterDenormalizers(readSideAssembly)
                .RegisterFactories(assembly)
                .RegisterServices(assembly)
                .Build();

            host
                .InitializeMongoDomainRepository()
                .InitializeMongoReadRepository()
                .SubscribeCommandHandlers(assembly)
                .SubscribeeEventHandlers(assembly)
                .SubscribeeQueryHandlers(assembly)
                .RunBlocking();
        }
    }
}
