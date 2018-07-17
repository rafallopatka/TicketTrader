using System;
using System.Linq;
using FluentAssertions;
using TicketTrader.Deliveries.Domain;
using TicketTrader.Shared.Base.DDD;
using TicketTrader.Shared.Test;
using Xunit;

namespace TicketTrader.Deliveries.Tests
{
    public class DeliveryTest : TestBase
    {
        [Fact]
        public void finalize_delivery_should_emit_delivery_completed_event()
        {
            // arrange
            var type = new DeliveryType("1");
            var order = new Order("1");
            var delivery = new Delivery(Id.New(), type, order, new Address("1", "2", "3", "4"));

            var scheduler = UseTestSchedulers().CurrentThread;
            var eventObserver = EventBus.Current.Observable<DeliveryCompletedEvent>()
                .CreateTestObserver(scheduler);

            // act
            delivery.MarkAsCompleted();
            scheduler.AdvanceBy(100);

            // assert
            eventObserver.AssertNoErrors();
            eventObserver.AssertValuesCount(1);

            var receivedEvent = eventObserver.ReceivedValues.First().As<DeliveryCompletedEvent>();
            receivedEvent.Should().BeOfType<DeliveryCompletedEvent>();
            receivedEvent.DeliveryId.Should().Be(delivery.Id);
        }

        [Fact]
        public void make_delivery_should_emit_delivery_failed_event()
        {
            // arrange
            var type = new DeliveryType("1");
            var order = new Order("1");
            var delivery = new Delivery(Id.New(), type, order, new Address("1", "2", "3", "4"));

            var scheduler = UseTestSchedulers().CurrentThread;
            var eventObserver = EventBus.Current.Observable<DeliveryFailedEvent>()
                .CreateTestObserver(scheduler);

            // act
            delivery.MarkAsFailed();
            scheduler.AdvanceBy(100);

            // assert
            eventObserver.AssertNoErrors();
            eventObserver.AssertValuesCount(1);

            var receivedEvent = eventObserver.ReceivedValues.First().As<DeliveryFailedEvent>();
            receivedEvent.Should().BeOfType<DeliveryFailedEvent>();
            receivedEvent.DeliveryId.Should().Be(delivery.Id);
        }
    }
}
