using System;
using System.Threading.Tasks;
using RawRabbit;
using RawRabbit.Enrichers.QueueSuffix;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Shared.Bus.RabbitMq
{
    public class RabbitMqEventBus : EventBus
    {
        public override async Task PublishAsync<TEvent>(TEvent evt)
        {
            await Bus.Client.PublishAsync(evt);
        }

        public override IDisposable Subscribe<TEvent>(IEventHandler<TEvent> handler, string name = null)
        {
            var subscription = Bus.Client.SubscribeAsync<TEvent>(async evt => await handler.Handle(evt), ctx => ctx.UseCustomQueueSuffix(name));
            return subscription;
        }
    }
}
