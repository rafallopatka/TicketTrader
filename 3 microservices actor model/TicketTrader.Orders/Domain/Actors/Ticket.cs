namespace TicketTrader.Orders.Domain.Actors
{
    public class Ticket
    {
        public string Id { get; set; }
        public string EventId { get; set; }
        public string PriceOptionId { get; set; }
        public string SceneSeatId { get; set; }

        public string PriceZoneName { get; set; }
        public string PriceOptionName { get; set; }
        public decimal GrossAmount { get; set; }
    }
}