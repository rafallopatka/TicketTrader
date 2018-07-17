using System;
using TicketTrader.Orders.ReadModel.Shared;
using TicketTrader.Shared.Base.CQRS.ReadModel;

namespace TicketTrader.Orders.ReadModel.Clients
{
    public class ClientOrderReadModel: IReadModel
    {
        public string Id { get; set; }
        public string ClientId { get; set; }

        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public TimeSpan ExpirationTimeout { get; set; }

        public OrderStateReadModel State { get; set; }
    }
}