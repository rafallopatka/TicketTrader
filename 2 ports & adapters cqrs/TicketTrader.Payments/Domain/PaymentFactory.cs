using System;
using TicketTrader.Shared.Base.DDD;
using TicketTrader.Shared.Kernel;

namespace TicketTrader.Payments.Domain
{
    public class PaymentFactory: AggregateFactory
    {
        public Payment CreatePayment(string orderId, string paymentOptionId)
        {
            if (orderId == null)
                throw new ArgumentException("Order cannot be null");

            if (orderId == default(string))
                throw new ArgumentException("Provided order id is empty");

            if (paymentOptionId == null)
                throw new ArgumentException("Price cannot be null");

            var order = new Order
            {
                Id = Id.From(orderId),
            };
            var paymentOption = Id.From(paymentOptionId);

            var payment = new Payment(Id.New(), order, paymentOption);
            return payment;
        }
    }
}