using System.Threading.Tasks;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Orders.ReadModel.Clients
{
    public interface IClientOrdersDenormalizer : IDenormalizer
    {
        Task CreateOrder(Order order);
        Task UpdateOrder(Order order);
    }
}