using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.Domain.Commands;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class CreateNewOrderCommandHandler : ICommandHandler<CreateNewOrderCommand>
    {
        private readonly DomainActorSystem _domainActorSystem;

        public CreateNewOrderCommandHandler(DomainActorSystem domainActorSystem)
        {
            _domainActorSystem = domainActorSystem;
        }

        public async Task HandleAsync(CreateNewOrderCommand command)
        {
            _domainActorSystem.Execute(new CreateNewOrder(command.OrderId, command.ClientId));
        }
    }
}