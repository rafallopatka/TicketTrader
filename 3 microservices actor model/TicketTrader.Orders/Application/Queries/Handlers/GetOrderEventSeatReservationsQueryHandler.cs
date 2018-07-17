using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.Domain.Queries;
using TicketTrader.Orders.Domain.Queries.ReadModels;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Queries.Handlers
{
    class GetOrderEventSeatReservationsQueryHandler : IQueryHandler<GetOrderEventSeatReservationsQuery, GetOrderEventSeatReservationsQuery.Response>
    {
        private readonly DomainActorSystem _domainActorSystem;

        public GetOrderEventSeatReservationsQueryHandler(DomainActorSystem domainActorSystem)
        {
            _domainActorSystem = domainActorSystem;
        }

        public async Task<GetOrderEventSeatReservationsQuery.Response> Handle(GetOrderEventSeatReservationsQuery query)
        {
            var response =
                await _domainActorSystem.Query<RespondOrderEventSeatReservations>(new GetOrderEventSeatReservations(
                    query.ClientId,
                    query.EventId,
                    query.OrderId));

            return new GetOrderEventSeatReservationsQuery.Response
            {
                SeatReservations = response.Reservations.Select(x => new GetOrderEventSeatReservationsQuery.SeatReservation
                {
                    Id = x.Id,
                    SceneSeatId = x.SceneSeatId
                }).ToList()
            };
        }
    }
}