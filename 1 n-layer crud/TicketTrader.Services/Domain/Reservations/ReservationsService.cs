using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketTrader.Dal;
using TicketTrader.Model;
using TicketTrader.Services.Domain.Events.EventReservations;
using TicketTrader.Services.Mappings;

namespace TicketTrader.Services.Domain.Reservations
{
    public class ReservationsService : IReservationsService
    {
        private readonly DalContext _dalContext;

        public ReservationsService(DalContext dalContext)
        {
            _dalContext = dalContext;
        }

        public async Task<IList<SeatReservationDto>> GetEventSeatReservationsAsync(int eventId)
        {
            var data = await _dalContext
                .Reservations
                .Where(x => x.EventId == eventId)
                .Where(x => x.Discarded == false)
                .Include(x => x.Seat)
                .Select(x => x.Seat)
                .Select(x => new SeatReservationDto
                {
                    SceneSeatId = x.SceneSeatId,
                })
                .ToListAsync();

            return data.MapTo<IList<SeatReservationDto>>();
        }


        public async Task<IList<SeatReservationDto>> GetEventOrderReservationsAsync(int eventId, int clientId, int orderId)
        {
            var data = await _dalContext
                .Reservations
                .Include(x => x.Seat)
                .Where(x => x.EventId == eventId)
                .Where(x => x.ClientId == clientId)
                .Where(x => x.OrderId == orderId)
                .Where(x => x.Discarded == false)
                .ToListAsync();

            return data.MapTo<IList<SeatReservationDto>>();
        }

        public async Task<SeatReservationDto> ReserveSeatAsync(int eventId, int clientId, int orderId, int sceneSeatId)
        {
            var seat = await _dalContext
                .Seats
                .Where(x => x.SceneSeatId == sceneSeatId)
                .Where(x => x.EventId == eventId)
                .SingleAsync();

            bool hasAvtiveReservations = await _dalContext
                .Reservations
                .AnyAsync(x => x.SeatId == seat.SceneSeatId);

            if (hasAvtiveReservations && seat is NumberedSeat)
                throw new InvalidOperationException("Conflict with existing reservation");
            
            var result = _dalContext
                .Reservations
                .Add(new Reservation
                {
                    ClientId = clientId,
                    EventId = eventId,
                    OrderId = orderId,
                    SeatId = seat.Id
                });


            await _dalContext.SaveChangesAsync();

            return result.Entity.MapTo<SeatReservationDto>();
        }

        public async Task DiscardReservationAsync(int eventId, int clientId, int orderId, int sceneSeatId)
        {
            var reservation = _dalContext.Reservations
                .First(x => x.Discarded == false && x.Seat.SceneSeatId == sceneSeatId && x.ClientId == clientId && x.OrderId == orderId && x.EventId == eventId);

            reservation.Discarded = true;             
            await _dalContext.SaveChangesAsync();
        }
    }
}