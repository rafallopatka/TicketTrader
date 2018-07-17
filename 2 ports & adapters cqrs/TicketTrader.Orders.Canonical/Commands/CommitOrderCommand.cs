
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Canonical.Commands
{
    public class CommitOrderCommand : ICommand
    {
        public string ClientId { get; set; }
        public string OrderId { get; set; }
    }
}