namespace TicketTrader.Payments.Canonical.Queries
{
    public class PaymentDto
    {
        public string PaymentId { get; set; }

        public string OrderId { get; set; }

        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal VatRate { get; set; }
    }
}