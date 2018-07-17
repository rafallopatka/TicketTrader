namespace TicketTrader.Orders.Domain.Queries
{
    class GetSelectedPayment : IQueryMessage
    {
        public string ClientId { get; }
        public string OrderId { get; }

        public GetSelectedPayment(string clientId, string orderId)
        {
            ClientId = clientId;
            OrderId = orderId;
        }
    }
}