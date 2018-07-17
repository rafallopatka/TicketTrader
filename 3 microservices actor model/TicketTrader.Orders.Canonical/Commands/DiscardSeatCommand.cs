using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Canonical.Commands
{
    public class DiscardSeatCommand: ICommand
    {
        public string EventId { get; set; }
        public string ClientId { get; set; }
        public string OrderId { get; set; }
        public string SceneSeatId { get; set; }
        public string ReservationId { get; set; }
    }
}
