using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Canonical.Commands
{
    public class RemoveTicketCommand: ICommand
    {
        public string EventId { get; set; }
        public string ClientId { get; set; }
        public string OrderId { get; set; }
        public string TicketId { get; set; }
    }
}
