using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketTrader.EventDefinitions.Services.EventsList;

namespace TicketTrader.EventDefinitions.Gateway.Controllers
{
    [Produces("application/json")]
    [Route("api/Events")]
    public class EventsController : Controller
    {
        private readonly IEventListProvider _listProvider;

        public EventsController(IEventListProvider listProvider)
        {
            _listProvider = listProvider;
        }

        [HttpGet]
        public async Task<IEnumerable<EventListItemDto>> Get()
        {
            return await _listProvider.GetAllEventsAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<EventListItemDto> Get(int id)
        {
            return await _listProvider.GetEventAsync(id);
        }

        // POST: api/Events
        [HttpPost]
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Events/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}