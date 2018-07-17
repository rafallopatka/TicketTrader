using Microsoft.Reactive.Testing;
using TicketTrader.Shared.Base.DDD;
using TicketTrader.Shared.Base.Infrastructure;

namespace TicketTrader.Shared.Test
{
    public abstract class TestBase: ReactiveTest
    {
        protected TestBase()
        {
            EventBus.Current = new TestEventBus();
        }

        protected TestSchedulersProvider UseTestSchedulers()
        {
            var provider = new TestSchedulersProvider();
            Schedulers.Provider = provider;
            return provider;
        }
    }
}
