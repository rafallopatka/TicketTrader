namespace TicketTrader.Api.Services.PaymentTypes
{
    public class PaymentTypeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal VatRate { get; set; }
    }
}