using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.Domain.Commands;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class ReserveSeatCommandHandler: ICommandHandler<ReserveSeatCommand>
    {
        private readonly DomainActorSystem _domainActorSystem;

        public ReserveSeatCommandHandler(DomainActorSystem domainActorSystem)
        {
            _domainActorSystem = domainActorSystem;
        }

        public async Task HandleAsync(ReserveSeatCommand command)
        {
            _domainActorSystem.Execute(new ReserveSeat(command.ClientId, command.OrderId, command.EventId, command.ReservationId, command.SceneSeatId));
        }
    }
}