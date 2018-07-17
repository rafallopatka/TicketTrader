using System.Threading.Tasks;
using TicketTrader.Deliveries.Domain;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Deliveries.ReadModel
{
    public interface IDeliveryDenormalizer: IDenormalizer
    {
        Task AddDelivery(Delivery delivery);
        Task UpdateDelivery(Delivery delivery);
    }
}
