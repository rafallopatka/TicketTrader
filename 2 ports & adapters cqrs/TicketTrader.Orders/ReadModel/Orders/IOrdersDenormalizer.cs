using System.Threading.Tasks;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Orders.ReadModel.Orders
{
    public interface IOrdersDenormalizer : IDenormalizer
    {
        Task UpdateOrderStatus(Order order);
        Task CreateOrder(Order order);
        Task DiscardOrder(Order order);
    }
}
