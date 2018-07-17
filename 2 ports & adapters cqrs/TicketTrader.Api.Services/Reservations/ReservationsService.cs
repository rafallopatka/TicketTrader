using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Api.Services.Reservations
{
    public class ReservationsService
    {
        public async Task<IList<SeatReservationDto>> GetEventSeatReservationsAsync(string eventId)
        {
            var query = new GetAllEventSeatReservationsQuery
            {
                EventId = eventId
            };

            var data = await QueryBus.Current.Query<GetAllEventSeatReservationsQuery, GetAllEventSeatReservationsQuery.Response>(query);

            return data.Reservations.Select(x => new SeatReservationDto
            {
                Id = x.Id,
                SceneSeatId = x.SceneSeatId.ToString()
            }).ToList();
        }


        public async Task<IList<SeatReservationDto>> GetEventOrderReservationsAsync(string eventId, string clientId, string orderId)
        {
            var query = new GetOrderEventSeatReservationsQuery
            {
                EventId = eventId,
                ClientId = clientId,
                OrderId = orderId
            };

            var data = await QueryBus.Current
                .Query<GetOrderEventSeatReservationsQuery, GetOrderEventSeatReservationsQuery.Response>(query);

            return data.SeatReservations.Select(x => new SeatReservationDto
            {
                Id = x.Id,
                SceneSeatId = x.SceneSeatId
            }).ToList();
        }

        public async Task<SeatReservationDto> ReserveSeatAsync(string eventId, string clientId, string orderId, string sceneSeatId)
        {
            var command = new ReserveSeatCommand
            {
                ReservationId = Guid.NewGuid().ToString(),
                EventId = eventId,
                ClientId = clientId,
                OrderId = orderId,
                SceneSeatId = sceneSeatId
            };

            await CommandBus.Current.DispatchAsync(command);

            return new SeatReservationDto
            {
                Id = command.ReservationId,
                SceneSeatId = sceneSeatId
            };
        }

        public async Task DiscardReservationAsync(string eventId, string clientId, string orderId, string sceneSeatId, string reservationId)
        {
            var command = new DiscardSeatCommand
            {
                EventId = eventId,
                ClientId = clientId,
                OrderId = orderId,
                SceneSeatId = sceneSeatId,
                ReservationId = reservationId
            };

            await CommandBus.Current.DispatchAsync(command);
        }
    }
}