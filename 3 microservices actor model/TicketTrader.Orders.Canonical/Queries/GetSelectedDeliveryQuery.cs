using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Canonical.Queries
{
    public class GetSelectedDeliveryQuery : IQuery
    {
        public string ClientId { get; set; }
        public string OrderId { get; set; }

        public class Response : IQueryResponse
        {
            public DeliveryDto Value { get; set; }
        }

        public class DeliveryDto
        {
            public string OrderId { get; set; }
            public string DeliveryId { get; set; }
        }
    }
}