using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.Domain.Commands;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class CommitOrderCommandHandler: ICommandHandler<CommitOrderCommand>
    {
        private readonly DomainActorSystem _domainActorSystem;

        public CommitOrderCommandHandler(DomainActorSystem domainActorSystem)
        {
            _domainActorSystem = domainActorSystem;
        }

        public async Task HandleAsync(CommitOrderCommand command)
        {
            _domainActorSystem.Execute(new CommitOrder(command.ClientId, command.OrderId));
        }
    }
}
