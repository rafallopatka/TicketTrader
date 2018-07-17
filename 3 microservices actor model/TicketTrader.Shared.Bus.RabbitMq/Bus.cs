using System;
using System.Linq;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.Enrichers.GlobalExecutionId;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Enrichers.MessageContext.Context;
using RawRabbit.Instantiation;
using BusClient = RawRabbit.Instantiation.Disposable.BusClient;


namespace TicketTrader.Shared.Bus.RabbitMq
{
    static class Bus
    {
        public static IBusClient Client => Instance.Value;

        private static readonly Lazy<IBusClient> Instance = new Lazy<IBusClient>(Init);

        private static BusClient Init()
        {
            var userName = Environment.GetEnvironmentVariable("BUS_USERNAME");
            var password = Environment.GetEnvironmentVariable("BUS_PASSWORD");
            var port = int.Parse(Environment.GetEnvironmentVariable("BUS_PORT"));
            var virtualHost = Environment.GetEnvironmentVariable("BUS_VIRTUAL_HOST");
            var hostNames = Environment.GetEnvironmentVariable("BUS_HOSTNAME")?.Split(',').ToList();

            var busConfig = new RawRabbitConfiguration
            {
                Username = userName,
                Password = password,
                Port = port,
                VirtualHost = virtualHost,
                Hostnames = hostNames,
                RequestTimeout = TimeSpan.FromSeconds(30),
                PublishConfirmTimeout = TimeSpan.FromSeconds(30),
            };

            return RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = busConfig,
                Plugins = p => p
                    .UseStateMachine()
                    .UseGlobalExecutionId()
                    .UseHttpContext()
                    .UseCustomQueueSuffix()
                    .UseMessageContext(ctx => new MessageContext
                    {
                        GlobalRequestId = Guid.Parse(ctx.GetGlobalExecutionId())
                    })
            });
        }
    }
}