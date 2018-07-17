using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.Domain.Queries;
using TicketTrader.Orders.Domain.Queries.ReadModels;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Queries.Handlers
{
    class GetAllEventSeatReservationsQueryHandler : IQueryHandler<GetAllEventSeatReservationsQuery, GetAllEventSeatReservationsQuery.Response>
    {
        private readonly DomainActorSystem _domainActorSystem;

        public GetAllEventSeatReservationsQueryHandler(DomainActorSystem domainActorSystem)
        {
            _domainActorSystem = domainActorSystem;
        }

        public async Task<GetAllEventSeatReservationsQuery.Response> Handle(GetAllEventSeatReservationsQuery query)
        {
            var response = await _domainActorSystem.Query<RespondAllEventSeatReservations>(new GetAllEventSeatReservations(query.EventId));

            return new GetAllEventSeatReservationsQuery.Response
            {
                Reservations = response.Reservations.Select(x => new GetAllEventSeatReservationsQuery.SeatReservation
                {
                    Id = x.Id,
                    SceneSeatId = x.SceneSeatId
                }).ToList()
            };
        }
    }
}
