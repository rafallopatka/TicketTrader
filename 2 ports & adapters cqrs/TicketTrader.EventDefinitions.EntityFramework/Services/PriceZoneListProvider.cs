using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketTrader.EventDefinitions.Services.PriceZonesList;
using TicketTrader.EventDefinitions.EntityFramework.Mappings;
using TicketTrader.Shared.Base.Infrastructure;
using Z.EntityFramework.Plus;

namespace TicketTrader.EventDefinitions.EntityFramework.Services
{
    public class PriceZoneListProvider : IPriceZoneListProvider
    {
        private readonly DalContext _dalContext;
        private readonly ICurrentDateTimeProvider _currentTime;

        public PriceZoneListProvider(DalContext dalContext, ICurrentDateTimeProvider currentTimeProvider)
        {
            _dalContext = dalContext;
            _currentTime = currentTimeProvider;
        }

        public async Task<IList<PriceZoneListItemDto>> GetEventPriceZonesAsync(int eventId)
        {
            var data = await _dalContext
                .PriceZones
                .Where(x => x.PriceList.EventId == eventId)
                .Where(x => x.PriceList.ValidFrom <= _currentTime.Local && _currentTime.Local <= x.PriceList.ValidTo)
                .Include(x => x.Options)
                .ThenInclude(x => x.Price)
                .Select(x => x)
                .FromCacheAsync();


            return data.MapTo<IList<PriceZoneListItemDto>>();
        }
    }
}
