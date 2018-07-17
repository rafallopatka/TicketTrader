using System.Collections.Generic;
using System.Threading.Tasks;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Orders.ReadModel.Tickets
{
    public interface ITicketFinder : IFinder
    {
        Task<IEnumerable<TicketOrderReadModel>> GetClientTicketForEventAsync(string clientId, string orderId, string eventId);
        Task<IEnumerable<TicketOrderReadModel>> GetClientTicketsAsync(string clientId, string orderId);
    }
}
