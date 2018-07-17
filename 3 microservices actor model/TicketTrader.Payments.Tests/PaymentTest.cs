using System;
using System.Linq;
using FluentAssertions;
using TicketTrader.Payments.Canonical.Events;
using TicketTrader.Payments.Domain;
using TicketTrader.Shared.Base.DDD;
using TicketTrader.Shared.Test;
using Xunit;

namespace TicketTrader.Payments.Tests
{
    public class PaymentTest : TestBase
    {
        [Fact]
        public void finalize_payment_should_emit_payment_successed_event()
        {
            // arrange
            var order = new Order { Id = Id.New() };
            var payment = new Payment(Id.New(), order, Id.New());

            var scheduler = UseTestSchedulers().CurrentThread;
            var eventObserver = EventBus.Current.Observable<PaymentSuccessed>()
                .CreateTestObserver(scheduler);

            // act
            payment.PaymentRegistered();
            scheduler.AdvanceBy(100);

            // assert
            eventObserver.AssertNoErrors();
            eventObserver.AssertValuesCount(1);

            var receivedEvent = eventObserver.ReceivedValues.First().As<PaymentSuccessed>();
            receivedEvent.Should().BeOfType<PaymentSuccessed>();
            receivedEvent.OrderId.Should().Be(order.Id);
        }

        [Fact]
        public void make_payment_failed_should_emit_payment_failed_event()
        {
            // arrange
            var order = new Order { Id = Id.New() };
            var payment = new Payment(Id.New(), order, Id.New());

            var scheduler = UseTestSchedulers().CurrentThread;
            var eventObserver = EventBus.Current.Observable<PaymentFailed>()
                .CreateTestObserver(scheduler);

            // act
            payment.MakePaymentAsFailed();
            scheduler.AdvanceBy(100);

            // assert
            eventObserver.AssertNoErrors();
            eventObserver.AssertValuesCount(1);

            var receivedEvent = eventObserver.ReceivedValues.First().As<PaymentFailed>();
            receivedEvent.Should().BeOfType<PaymentFailed>();
            receivedEvent.OrderId.Should().Be(order.Id);
        }
    }
}
