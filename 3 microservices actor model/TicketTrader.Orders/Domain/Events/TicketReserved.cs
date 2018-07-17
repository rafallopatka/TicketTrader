namespace TicketTrader.Orders.Domain.Events
{
    class TicketReserved : IEventMessage
    {
        public TicketReserved(string orderId,
            string customerId,
            string ticketId,
            string eventId,
            string priceOptionId,
            string sceneSeatId,
            string priceZoneName,
            string priceOptionName,
            decimal grossAmount)
        {
            OrderId = orderId;
            CustomerId = customerId;
            EventId = eventId;
            PriceOptionId = priceOptionId;
            SceneSeatId = sceneSeatId;
            PriceZoneName = priceZoneName;
            PriceOptionName = priceOptionName;
            GrossAmount = grossAmount;
            TicketId = ticketId;
        }

        public string OrderId { get; }
        public string CustomerId { get; }
        public string EventId { get; }
        public string PriceOptionId { get; }
        public string SceneSeatId { get; }
        public string TicketId { get; }

        public string PriceZoneName { get; }
        public string PriceOptionName { get; }
        public decimal GrossAmount { get; }
    }
}