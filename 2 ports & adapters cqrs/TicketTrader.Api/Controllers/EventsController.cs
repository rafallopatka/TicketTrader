using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.EventDefinitions.Gateway.Client;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Events")]
    public class EventsController : ApiController
    {
        private readonly EventDefinitionsClientProvider _eventDefinitionsClient;

        public EventsController(EventDefinitionsClientProvider eventDefinitionsClient, ILogger<ApiController> logger) : base(logger)
        {
            _eventDefinitionsClient = eventDefinitionsClient;
        }

        [HttpGet]
        [Authorize(Policy = nameof(PolicyDefinitions.JustAuthenticatedUsers))]
        public Task<ApiResponse<IEnumerable<EventListItemDto>>> Get()
        {
            return Respond
                .AsyncWithMany<EventListItemDto>(async (response, fail) =>
                {
                    response.Result = await _eventDefinitionsClient.EventDefinitionsClient.ApiEventsGetAsync();
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
                    response.Result = await _eventDefinitionsClient.EventDefinitionsClient.ApiEventsGetAsync(id);
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
