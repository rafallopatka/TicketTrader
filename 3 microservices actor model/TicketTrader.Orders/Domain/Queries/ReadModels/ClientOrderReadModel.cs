using System;

namespace TicketTrader.Orders.Domain.Queries.ReadModels
{
    public class ClientOrderReadModel
    {
        public ClientOrderReadModel(string id,
            string clientId,
            DateTime createDateTime,
            DateTime updateDateTime,
            TimeSpan expirationTimeout,
            OrderStateReadModel state)
        {
            Id = id;
            ClientId = clientId;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
            ExpirationTimeout = expirationTimeout;
            State = state;
        }

        public string Id { get; }
        public string ClientId { get; }

        public DateTime CreateDateTime { get; }
        public DateTime UpdateDateTime { get; }
        public TimeSpan ExpirationTimeout { get; }

        public OrderStateReadModel State { get; }
    }
}