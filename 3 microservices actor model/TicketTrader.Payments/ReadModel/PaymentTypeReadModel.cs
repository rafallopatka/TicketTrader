namespace TicketTrader.Payments.ReadModel
{
    public class PaymentTypeReadModel
    {
        public string PaymentTypeId { get; set; }
        public string Name { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal VatRate { get; set; }
    }
}
