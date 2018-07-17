using System;
using TicketTrader.Payments.Domain;
using TicketTrader.Shared.Base.DDD;
using TicketTrader.Shared.Test;
using Xunit;

namespace TicketTrader.Payments.Tests
{
    public class PaymentFactoryTests: TestBase
    {
        [Fact]
        public void should_throw_when_order_is_null()
        {
            // arrange
            var paymentFactory = new PaymentFactory();

            var scheduler = UseTestSchedulers().CurrentThread;
            var eventObserver = EventBus.Current.Observable<PaymentStarted>()
                .CreateTestObserver(scheduler);

            // act / assert
            Assert.Throws<ArgumentException>(() => paymentFactory.CreatePayment(null, null));

            eventObserver.AssertNoErrors();
            eventObserver.AssertValuesCount(0);
        }

        [Fact]
        public void shold_throw_when_orderId_is_0()
        {
            // arrange
            var paymentFactory = new PaymentFactory();

            var scheduler = UseTestSchedulers().CurrentThread;
            var eventObserver = EventBus.Current.Observable<PaymentStarted>()
                .CreateTestObserver(scheduler);

            // act / assert
            Assert.Throws<ArgumentException>(() => paymentFactory.CreatePayment("", null));

            eventObserver.AssertNoErrors();
            eventObserver.AssertValuesCount(0);
        }


        [Fact]
        public void should_throw_when_price_is_null()
        {
            // arrange
            var paymentFactory = new PaymentFactory();

            var scheduler = UseTestSchedulers().CurrentThread;
            var eventObserver = EventBus.Current.Observable<PaymentStarted>()
                .CreateTestObserver(scheduler);

            // act / assert
            Assert.Throws<ArgumentException>(() => paymentFactory.CreatePayment("test", null));

            eventObserver.AssertNoErrors();
            eventObserver.AssertValuesCount(0);
        }
    }
}
