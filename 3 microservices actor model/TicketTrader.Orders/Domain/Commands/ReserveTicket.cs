namespace TicketTrader.Orders.Domain.Commands
{
    class ReserveTicket : ICommandMessage, IOrderMessage
    {
        public ReserveTicket(string customerId,
            string orderId,
            string eventId,
            string ticketId,
            string sceneSeatId,
            string priceOptionId,
            string priceZoneName,
            string priceOptionName,
            decimal grossAmount)
        {
            CustomerId = customerId;
            OrderId = orderId;
            EventId = eventId;
            TicketId = ticketId;
            SceneSeatId = sceneSeatId;
            PriceOptionId = priceOptionId;
            PriceZoneName = priceZoneName;
            PriceOptionName = priceOptionName;
            GrossAmount = grossAmount;
        }

        public string EventId { get; }
        public string CustomerId { get; }
        public string OrderId { get; }
        public string PriceOptionId { get; }
        public string SceneSeatId { get; }
        public string TicketId { get; }

        public string PriceZoneName { get; }
        public string PriceOptionName { get; }
        public decimal GrossAmount { get; }
    }
}