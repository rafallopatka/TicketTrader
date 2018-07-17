using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketTrader.Dal;
using TicketTrader.Services.Mappings;

namespace TicketTrader.Services.Domain.Events.EventList
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
            var queryResult = await _context
                .Events
                .Include(i => i.Description)
                    .ThenInclude(i => i.EventCategories)
                    .ThenInclude(i => i.Category)
                .AsNoTracking()
                .ToListAsync();

            return queryResult.MapTo<List<EventListItemDto>>();
        }

        public async Task<EventListItemDto> GetEventAsync(int id)
        {
            var queryResult = await _context
                .Events.Where(x => x.Id == id)
                .Include(i => i.Description)
                .ThenInclude(i => i.EventCategories)
                .ThenInclude(i => i.Category)
                .AsNoTracking()
                .SingleAsync();

            return queryResult.MapTo<EventListItemDto>();
        }
    }
}
