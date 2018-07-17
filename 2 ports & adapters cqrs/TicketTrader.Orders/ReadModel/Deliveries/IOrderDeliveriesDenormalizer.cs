using System.Threading.Tasks;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Orders.ReadModel.Deliveries
{
    public interface IOrderDeliveriesDenormalizer : IDenormalizer
    {
        Task UpdateDeliveryMethod(Order order);
    }
}