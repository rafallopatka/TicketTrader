using System.Collections.Generic;
using System.Threading.Tasks;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Deliveries.ReadModel
{
    public interface IDeliveryFinder: IFinder
    {
        Task<IEnumerable<DeliveryType>> GetDeliveryTypesAsync();
        Task<IEnumerable<DeliveryReadModel>> GetWaitingDeliveriesAsync();
    }
}
