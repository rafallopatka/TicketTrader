using System;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Deliveries.Domain
{
    public class Delivery: AggregateRoot
    {
        public DeliveryType Type { get; protected set; }
        public Order Order { get; protected set; }
        public Address Address { get; protected set; }

        public DeliveryStatus DeliveryStatus { get; protected set; }

        public Delivery(Id id, DeliveryType type, Order order, Address address)
        {
            Type = type;
            Order = order;
            Address = address;
            Id = id;
            DeliveryStatus = DeliveryStatus.New;
        }

        public void MarkAsCompleted()
        {
            if (DeliveryStatus != DeliveryStatus.New)
                throw new InvalidOperationException($"Cannot change status from {DeliveryStatus} to {DeliveryStatus.Completed}");

            DeliveryStatus = DeliveryStatus.Completed;
        }

        public void MarkAsCanceled()
        {
            if (DeliveryStatus != DeliveryStatus.New)
                throw new InvalidOperationException($"Cannot change status from {DeliveryStatus} to {DeliveryStatus.Canceled}");

            DeliveryStatus = DeliveryStatus.Canceled;
        }

        public void MarkAsFailed()
        {
            if (DeliveryStatus != DeliveryStatus.New)
                throw new InvalidOperationException($"Cannot change status from {DeliveryStatus} to {DeliveryStatus.Failed}");

            DeliveryStatus = DeliveryStatus.Failed;
        }
    }
}
