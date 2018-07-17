using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Orders.ReadModel.Reservations;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Queries.Handlers
{
    class GetOrderEventSeatReservationsQueryHandler : IQueryHandler<GetOrderEventSeatReservationsQuery, GetOrderEventSeatReservationsQuery.Response>
    {
        private readonly IReservationsFinder _finder;

        public GetOrderEventSeatReservationsQueryHandler(IReservationsFinder finder)
        {
            _finder = finder;
        }

        public async Task<GetOrderEventSeatReservationsQuery.Response> Handle(GetOrderEventSeatReservationsQuery query)
        {
            var response = await _finder.GetEventOrderReservations(query.EventId, query.ClientId, query.OrderId);

            return new GetOrderEventSeatReservationsQuery.Response
            {
                SeatReservations = response.Select(x => new GetOrderEventSeatReservationsQuery.SeatReservation
                {
                    Id = x.Id,
                    SceneSeatId = x.SceneSeatId
                }).ToList()
            };
        }
    }
}