using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Canonical.Commands
{
    public class ReserveTicketCommand: ICommand
    {
        public string EventId { get; set; }
        public string ClientId { get; set; }
        public string OrderId { get; set; }
        public string PriceOptionId { get; set; }
        public string SceneSeatId { get; set; }
        public string TicketId { get; set; }

        public string PriceZoneName { get; set; }
        public string PriceOptionName { get; set; }
        public decimal GrossAmount { get; set; }
    }
}
