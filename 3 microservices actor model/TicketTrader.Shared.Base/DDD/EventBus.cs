using System;
using System.Threading.Tasks;

namespace TicketTrader.Shared.Base.DDD
{
    public abstract class EventBus
    {
        public abstract Task PublishAsync<TEvent>(TEvent evt) where TEvent : DomainEvent;

        public abstract IDisposable Subscribe<TEvent>(IEventHandler<TEvent> handler, String name = null) where TEvent : DomainEvent;

        public static EventBus Current { get; set; }
    }
}
