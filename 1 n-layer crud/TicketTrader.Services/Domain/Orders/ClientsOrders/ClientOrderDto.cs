using System;

namespace TicketTrader.Services.Domain.Orders.ClientsOrders
{
    public class ClientOrderDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public ClientOrderState State { get; set; }

        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public TimeSpan ExpirationTimeout { get; set; }
    }
}