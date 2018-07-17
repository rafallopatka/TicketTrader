namespace TicketTrader.Services.Domain.Payments
{
    public class PaymentTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PriceId { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal VatRate { get; set; }
    }
}