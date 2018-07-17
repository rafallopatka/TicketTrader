using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Orders.ReadModel.Reservations;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Queries.Handlers
{
    class GetAllEventSeatReservationsQueryHandler : IQueryHandler<GetAllEventSeatReservationsQuery, GetAllEventSeatReservationsQuery.Response>
    {
        private readonly IReservationsFinder _reservationsFinder;

        public GetAllEventSeatReservationsQueryHandler(IReservationsFinder reservationsFinder)
        {
            _reservationsFinder = reservationsFinder;
        }

        public async Task<GetAllEventSeatReservationsQuery.Response> Handle(GetAllEventSeatReservationsQuery query)
        {
            var response = await _reservationsFinder.GetEventSeatsReservations(query.EventId);

            return new GetAllEventSeatReservationsQuery.Response
            {
                Reservations = response.Select(x => new GetAllEventSeatReservationsQuery.SeatReservation
                {
                    Id = x.Id,
                    SceneSeatId = x.SceneSeatId
                }).ToList()
            };
        }
    }
}
