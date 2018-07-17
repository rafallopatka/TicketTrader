using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketTrader.Services.Domain.Events.EventList
{
    public interface IEventListProvider
    {
        Task<IList<EventListItemDto>> GetAllEventsAsync();
        Task<EventListItemDto> GetEventAsync(int id);
    }
}
