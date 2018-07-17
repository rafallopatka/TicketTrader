using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Shared.Kernel
{
    public class Price : ValueObject
    {
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal VatRate { get; set; }
    }
}
