using System.Collections.Generic;
using System.Threading.Tasks;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Orders.ReadModel.Reservations
{
    public interface IReservationsFinder : IFinder
    {
        Task<IEnumerable<SeatReservationReadModel>> GetEventSeatsReservations(string eventId);
        Task<IEnumerable<SeatReservationReadModel>> GetEventOrderReservations(string eventId, string clientId, string orderId);
    }
}
