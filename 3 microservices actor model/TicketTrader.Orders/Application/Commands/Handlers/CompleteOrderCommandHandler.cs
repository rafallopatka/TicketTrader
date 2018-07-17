using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.Domain.Commands;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class CompleteOrderCommandHandler: ICommandHandler<CompleteOrderCommand>
    {
        private readonly DomainActorSystem _domainActorSystem;

        public CompleteOrderCommandHandler(DomainActorSystem domainActorSystem)
        {
            _domainActorSystem = domainActorSystem;
        }

        public async Task HandleAsync(CompleteOrderCommand command)
        {
            _domainActorSystem.Execute(new CompleteOrder(command.OrderId));
        }
    }
}