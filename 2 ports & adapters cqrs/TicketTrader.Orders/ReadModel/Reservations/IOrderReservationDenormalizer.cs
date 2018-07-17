using System.Threading.Tasks;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Orders.ReadModel.Reservations
{
    public interface IOrderReservationDenormalizer : IDenormalizer
    {
        Task CreateReservation(Order order, Reservation reservation);
        Task DiscardReservation(Reservation reservation);
    }
}