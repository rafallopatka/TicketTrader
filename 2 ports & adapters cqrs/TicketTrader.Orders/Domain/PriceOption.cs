using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    public class PriceOption : DomainEntity
    {
        public string PriceOptionName { get; protected set; }
        public string PriceZoneName { get; protected set; }
        public decimal GrossAmount { get; protected set; }

        public PriceOption(Id priceOptionId, string priceOptionName, string priceZoneName, decimal grossAmount)
        {
            PriceOptionName = priceOptionName;
            PriceZoneName = priceZoneName;
            GrossAmount = grossAmount;
            Id = priceOptionId;
        }
    }
}