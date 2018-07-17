using System.Collections.Generic;
using System.Threading.Tasks;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Orders.ReadModel.Orders
{
    public interface IOrderFinder : IFinder
    {
        Task<IEnumerable<OrderReadModel>> GetAwaitingOrders();
    }
}
