namespace TicketTrader.Api.Services.OrderTickets
{
    public class SeatPriceOptionDto
    {
        public string SceneSeatId { get; set; }
        public string PriceOptionId { get; set; }
        public string PriceZoneName { get; set; }
        public string PriceOptionName { get; set; }
        public decimal GrossAmount { get; set; }
    }
}