using System;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Shared.Test
{
#pragma warning disable 1998
    public class TestEventBus : EventBus
    {
        private Subject<DomainEvent> EventsSubject { get; }

        public TestEventBus()
        {
            EventsSubject = new Subject<DomainEvent>();
        }

        public override async Task PublishAsync<TEvent>(TEvent evt)
        {
            EventsSubject.OnNext(evt);
        }

        public override IDisposable Subscribe<TEvent>(IEventHandler<TEvent> eventHandler, string name = null)
        {
            return EventsSubject.Subscribe(e => { eventHandler.Handle(e as TEvent); });
        }
    }
}
