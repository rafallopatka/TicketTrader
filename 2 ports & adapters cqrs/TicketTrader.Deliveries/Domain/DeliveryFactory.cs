using System;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Deliveries.Domain
{
    public class DeliveryFactory : AggregateFactory
    {
        public Delivery Create(DeliveryType type, Order order, Address address)
        {
            if (type == null)
                throw new InvalidOperationException("Type cannot be null");

            if (order == null)
                throw new InvalidOperationException("order cannot be null");

            if (address == null)
                throw new InvalidOperationException("address cannot be null");

            var delivery = new Delivery(Id.New(), type, order, address);

            return delivery;
        }
    }
}
