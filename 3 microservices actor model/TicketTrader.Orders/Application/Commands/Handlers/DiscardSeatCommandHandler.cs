using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.Domain.Commands;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class DiscardSeatCommandHandler : ICommandHandler<DiscardSeatCommand>
    {
        private readonly DomainActorSystem _domainActorSystem;

        public DiscardSeatCommandHandler(DomainActorSystem domainActorSystem)
        {
            _domainActorSystem = domainActorSystem;
        }

        public async Task HandleAsync(DiscardSeatCommand command)
        {
            _domainActorSystem.Execute(
                new DiscardSeat(
                orderId: command.OrderId,
                customerId: command.ClientId,
                eventId: command.EventId,
                reservationId: command.ReservationId,
                sceneSeatId: command.SceneSeatId));
        }
    }
}