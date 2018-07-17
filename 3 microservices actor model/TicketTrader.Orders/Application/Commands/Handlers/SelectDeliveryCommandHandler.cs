using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.Domain.Commands;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class SelectDeliveryCommandHandler: ICommandHandler<SelectDeliveryCommand>
    {
        private readonly DomainActorSystem _domainActorSystem;

        public SelectDeliveryCommandHandler(DomainActorSystem domainActorSystem)
        {
            _domainActorSystem = domainActorSystem;
        }

        public async Task HandleAsync(SelectDeliveryCommand command)
        {
            _domainActorSystem.Execute(new SetDelivery(command.ClientId, command.OrderId, command.DeliveryTypeId));
        }
    }
}