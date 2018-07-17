using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketTrader.Services.Domain.PriceZones.PriceZonesList
{
    public interface IPriceZoneListProvider
    {
        Task<IList<PriceZoneListItemDto>> GetEventPriceZonesAsync(int eventId);
    }
}