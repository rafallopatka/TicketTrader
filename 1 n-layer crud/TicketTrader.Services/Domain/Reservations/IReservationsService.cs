using System.Collections.Generic;
using System.Threading.Tasks;
using TicketTrader.Services.Domain.Events.EventReservations;

namespace TicketTrader.Services.Domain.Reservations
{
    public interface IReservationsService
    {
        Task<IList<SeatReservationDto>> GetEventSeatReservationsAsync(int eventId);

        Task<IList<SeatReservationDto>> GetEventOrderReservationsAsync(int eventId, int clientId, int orderId);

        Task<SeatReservationDto> ReserveSeatAsync(int eventId, int clientId, int orderId, int sceneSeatId);

        Task DiscardReservationAsync(int eventId, int clientId, int orderId, int id);
    }
}
