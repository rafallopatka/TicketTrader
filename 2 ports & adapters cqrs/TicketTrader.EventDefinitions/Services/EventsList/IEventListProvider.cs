using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketTrader.EventDefinitions.Services.EventsList
{
    public interface IEventListProvider
    {
        Task<IList<EventListItemDto>> GetAllEventsAsync();
        Task<EventListItemDto> GetEventAsync(int id);
    }
}
