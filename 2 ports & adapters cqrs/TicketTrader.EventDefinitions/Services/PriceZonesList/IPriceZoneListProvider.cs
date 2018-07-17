using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketTrader.EventDefinitions.Services.PriceZonesList
{
    public interface IPriceZoneListProvider
    {
        Task<IList<PriceZoneListItemDto>> GetEventPriceZonesAsync(int eventId);
    }
}