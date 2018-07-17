using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketTrader.Dal;
using TicketTrader.Services.Core;
using TicketTrader.Services.Mappings;

namespace TicketTrader.Services.Domain.PriceZones.PriceZonesList
{
    public class PriceZoneListProvider : IPriceZoneListProvider
    {
        private readonly DalContext _dalContext;
        private readonly ICurrentTimeProvider _currentTime;

        public PriceZoneListProvider(DalContext dalContext, ICurrentTimeProvider currentTimeProvider)
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
                .ToListAsync();


            return data.MapTo<IList<PriceZoneListItemDto>>();
        }
    }
}
