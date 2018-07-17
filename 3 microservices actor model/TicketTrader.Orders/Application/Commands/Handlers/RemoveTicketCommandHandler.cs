using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.Domain.Commands;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class RemoveTicketCommandHandler: ICommandHandler<RemoveTicketCommand>
    {
        private readonly DomainActorSystem _domainActorSystem;

        public RemoveTicketCommandHandler(DomainActorSystem domainActorSystem)
        {
            _domainActorSystem = domainActorSystem;
        }

        public async Task HandleAsync(RemoveTicketCommand command)
        {
            _domainActorSystem.Execute(new RemoveTicket(command.ClientId, command.OrderId, command.EventId, command.TicketId));
        }
    }
}