using System.Collections.Generic;
using System.Threading.Tasks;
using TicketTrader.Orders.ReadModel.Shared;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Orders.ReadModel.Clients
{
    public interface IClientOrdersFinder: IFinder
    {
        Task<IEnumerable<ClientOrderReadModel>> GetClientOrdersByStateAsync(string clientId, OrderStateReadModel orderStateReadModel);
    }
}
