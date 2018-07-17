using System;
using TicketTrader.Deliveries.Domain;
using TicketTrader.Shared.Base.DDD;
using TicketTrader.Shared.Test;
using Xunit;

namespace TicketTrader.Deliveries.Tests
{
    public class DeliveryFactoryTests: TestBase
    {
        [Fact]
        public void should_throw_when_arguments_are_null()
        {
            // arrange
            var factory = new DeliveryFactory();

            var scheduler = UseTestSchedulers().CurrentThread;
            var eventObserver = EventBus.Current.Observable<NewDeliveryRegisteredEvent>()
                .CreateTestObserver(scheduler);

            // act / assert
            Assert.Throws<ArgumentException>(() => factory.Create(null, null, null));

            eventObserver.AssertNoErrors();
            eventObserver.AssertValuesCount(0);
        }
    }
}
