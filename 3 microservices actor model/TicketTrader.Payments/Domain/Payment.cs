using TicketTrader.Payments.Canonical.Events;
using TicketTrader.Shared.Base.DDD;
using TicketTrader.Shared.Base.Infrastructure;

namespace TicketTrader.Payments.Domain
{
    public class Payment : AggregateRoot
    {
        public Order Order { get; protected set; }

        public PaymentStatus PaymentStatus { get; protected set; }

        public Id PaymentOption { get; protected set; }

        public Payment(Id paymentId, Order order, Id paymentOptionId)
        {
            Id = paymentId;
            Order = order;
            PaymentOption = paymentOptionId;
            PaymentStatus = PaymentStatus.New;
        }

        public void PaymentRegistered()
        {
            PaymentStatus = PaymentStatus.Started;
            EventBus.PublishAsync(new PaymentStarted(Id, Order.Id, CurrentDateTime.Local));
        }

        public void MakePaymentAsFailed()
        {
            PaymentStatus = PaymentStatus.Failed;
            EventBus.PublishAsync(new PaymentFailed(Id, Order.Id, CurrentDateTime.Local));
        }

        public void MakePaymentAsSucceded()
        {
            PaymentStatus = PaymentStatus.Successed;
            EventBus.PublishAsync(new PaymentSuccessed(Id, Order.Id, CurrentDateTime.Local));
        }
    }

    public enum PaymentStatus
    {
        New,
        Started,
        Successed,
        Failed
    }
}
