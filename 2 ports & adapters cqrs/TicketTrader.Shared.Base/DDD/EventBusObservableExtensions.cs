using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using TicketTrader.Shared.Base.Infrastructure;

namespace TicketTrader.Shared.Base.DDD
{
    #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public static class EventBusObservableExtensions
    {
        public static IObservable<TEvent> Observable<TEvent>(this EventBus eventBus) where TEvent : DomainEvent
        {
            return System.Reactive.Linq.Observable.Create<TEvent>(observer =>
            {
                var handler = new ObservableEventHandler<TEvent>(async evt =>
                {
                    observer.OnNext(evt);
                });

                var subscribtion = eventBus.Subscribe(handler);

                return subscribtion;
            });
        }

        internal class ObservableEventHandler<TEvent> : IEventHandler<TEvent> where TEvent : DomainEvent
        {
            private readonly Func<TEvent, Task> _eventSubscriber;

            public ObservableEventHandler(Func<TEvent, Task> eventSubscriber)
            {
                _eventSubscriber = eventSubscriber;
            }

            public async Task Handle(TEvent domainEvent)
            {
                await _eventSubscriber.Invoke(domainEvent);
            }
        }
    }
}