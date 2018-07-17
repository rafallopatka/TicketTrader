using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketTrader.Services.Domain.Deliveries
{
    public interface IDeliveryTypesProvider
    {
        Task<IList<DeliveryTypeDto>> GetDeliveryTypesAsync();
    }
}