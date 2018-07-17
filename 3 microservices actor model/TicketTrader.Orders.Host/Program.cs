using System;
using System.Reflection;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.App;
using TicketTrader.Shared.Bus.RabbitMq;

namespace TicketTrader.Orders.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var domainRepositoryConnectionString = Environment.GetEnvironmentVariable("DOMAIN_REPOSITORY_CONNECTION_STRING");
            var readRepositoryConnectionString = Environment.GetEnvironmentVariable("READ_REPOSITORY_CONNECTION_STRING");

            var assembly = Assembly.GetAssembly(typeof(DomainActorSystem));

            var host = App.Current
                .UseDomainActorSystem("TicketTraderOrders", domainRepositoryConnectionString, readRepositoryConnectionString)
                .UseRabbitMqBus()
                .RegisterCommandHandlers(assembly)
                .RegisterEventHandlers(assembly)
                .RegisterQueryHandlers(assembly)
                .RegisterFactories(assembly)
                .RegisterServices(assembly)
                .Build();

            host
                .SubscribeCommandHandlers(assembly)
                .SubscribeeEventHandlers(assembly)
                .SubscribeeQueryHandlers(assembly)
                .RunBlocking();
        }
    }
}
