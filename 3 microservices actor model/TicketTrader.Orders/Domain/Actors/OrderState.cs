using System.Collections.Generic;

namespace TicketTrader.Orders.Domain.Actors
{
    internal class OrderState
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string DeliveryMethodId { get; set; }
        public string PaymentMethodId { get; set; }
        public OrderStatus Status { get; set; }

        public List<Ticket> Tickets { get; }

        public OrderState()
        {
            Tickets = new List<Ticket>();
        }
    }
}