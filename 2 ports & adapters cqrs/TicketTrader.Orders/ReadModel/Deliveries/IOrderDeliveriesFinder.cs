using System.Threading.Tasks;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Orders.ReadModel.Deliveries
{
    public interface IOrderDeliveriesFinder : IFinder
    {
        Task<OrderDeliveryReadModel> GetSelectedDeliveryAsync(string clientId, string orderId);
    }
}
