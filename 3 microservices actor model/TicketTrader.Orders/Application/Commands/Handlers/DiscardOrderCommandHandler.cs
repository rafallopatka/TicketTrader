using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.Domain.Commands;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class DiscardOrderCommandHandler: ICommandHandler<DiscardOrderCommand>
    {
        private readonly DomainActorSystem _domainActorSystem;

        public DiscardOrderCommandHandler(DomainActorSystem domainActorSystem)
        {
            _domainActorSystem = domainActorSystem;
        }

        public async Task HandleAsync(DiscardOrderCommand command)
        {
            _domainActorSystem.Execute(new DiscardOrder(command.OrderId));
        }
    }
}