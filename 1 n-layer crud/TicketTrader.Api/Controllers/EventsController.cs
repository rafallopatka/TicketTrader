using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Services.Domain.Events.EventList;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Events")]
    public class EventsController : ApiController
    {
        private readonly IEventListProvider _listProvider;

        public EventsController(IEventListProvider listProvider, ILogger<ApiController> logger) : base(logger)
        {
            _listProvider = listProvider;
        }

        [HttpGet]
        [Authorize(Policy = nameof(PolicyDefinitions.JustAuthenticatedUsers))]
        public Task<ApiResponse<IEnumerable<EventListItemDto>>> Get()
        {
            return Respond
                .AsyncWithMany<EventListItemDto>(async (response, fail) =>
                {
                    response.Result = await _listProvider.GetAllEventsAsync();
                });
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.JustAuthenticatedUsers))]
        public Task<ApiResponse<EventListItemDto>> Get(int id)
        {
            return Respond
                .AsyncWith<EventListItemDto>(async (response, fail) =>
                {
                    response.Result = await _listProvider.GetEventAsync(id);
                });
        }

        // POST: api/Events
        [HttpPost]
        [Authorize(Policy = nameof(PolicyDefinitions.AdministratorsOnly))]
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Events/5
        [HttpPut("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.AdministratorsOnly))]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.AdministratorsOnly))]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
