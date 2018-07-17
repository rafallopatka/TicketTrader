namespace TicketTrader.EventDefinitions.Entities
{
    public class Price
    {
        public int Id { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal VatRate { get; set; }
    }
}