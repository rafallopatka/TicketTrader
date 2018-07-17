using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketTrader.EventDefinitions.Entities;
using TicketTrader.EventDefinitions.EntityFramework.Mappings;
using TicketTrader.EventDefinitions.Services.EventsList;
using Z.EntityFramework.Plus;

namespace TicketTrader.EventDefinitions.EntityFramework.Services
{
    public class EventListProvider : IEventListProvider
    {
        private readonly DalContext _context;

        public EventListProvider(DalContext context)
        {
            _context = context;
        }

        public async Task<IList<EventListItemDto>> GetAllEventsAsync()
        {
            IEnumerable<Event> queryResult = await _context
                .Events
                .Where(x => true)
                .Include(i => i.Description)
                .ThenInclude(i => i.EventCategories)
                .ThenInclude(i => i.Category)
                .AsNoTracking()
                .FromCacheAsync();

            return queryResult.MapTo<List<EventListItemDto>>();
        }

        public async Task<EventListItemDto> GetEventAsync(int id)
        {
            var queryResult = await _context
                .Events
                .Where(x => x.Id == id)
                .Include(i => i.Description)
                .ThenInclude(i => i.EventCategories)
                .ThenInclude(i => i.Category)
                .AsNoTracking()
                .SingleAsync();

            return queryResult.MapTo<EventListItemDto>();
        }
    }
}
