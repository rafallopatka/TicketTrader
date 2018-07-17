using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.Domain.Commands;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class ReserveTicketCommandHandler: ICommandHandler<ReserveTicketCommand>
    {
        private readonly DomainActorSystem _domainActorSystem;

        public ReserveTicketCommandHandler(DomainActorSystem domainActorSystem)
        {
            _domainActorSystem = domainActorSystem;
        }

        public async Task HandleAsync(ReserveTicketCommand command)
        {
            _domainActorSystem.Execute(new ReserveTicket(command.ClientId,
                command.OrderId,
                command.EventId,
                command.TicketId,
                command.SceneSeatId,
                command.PriceOptionId,
                command.PriceZoneName,
                command.PriceOptionName,
                command.GrossAmount));
        }
    }
}