using System.Collections.Generic;

namespace TicketTrader.Web.ViewModels
{
    public class EventListViewModel
    {
        public IEnumerable<EventListItemViewModel> EventsList { get; }

        public EventListViewModel(IEnumerable<EventListItemViewModel> list)
        {
            EventsList = list;
        }
    }
}
