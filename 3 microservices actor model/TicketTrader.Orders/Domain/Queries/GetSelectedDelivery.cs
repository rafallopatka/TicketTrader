namespace TicketTrader.Orders.Domain.Queries
{
    class GetSelectedDelivery : IQueryMessage
    {
        public string ClientId { get; }
        public string OrderId { get; }

        public GetSelectedDelivery(string clientId, string orderId)
        {
            ClientId = clientId;
            OrderId = orderId;
        }
    }
}