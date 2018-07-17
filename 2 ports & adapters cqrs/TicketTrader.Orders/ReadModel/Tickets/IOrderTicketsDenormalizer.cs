using System.Threading.Tasks;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Orders.ReadModel.Tickets
{
    public interface IOrderTicketsDenormalizer : IDenormalizer
    {
        Task CreateTicket(Order order, Ticket ticket);
        Task RemoveTicket(Ticket ticket);
    }
}