using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Services.Domain.Events.EventReservations;
using TicketTrader.Services.Domain.Reservations;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Events/{eventId}/Reservations")]
    public class EventsReservationsController : ApiController
    {
        private readonly IReservationsService _reservationsProvider;

        public EventsReservationsController(ILogger<ApiController> logger, 
            IReservationsService eventReservationsProvider) : base(logger)
        {
            _reservationsProvider = eventReservationsProvider;
        }

        [HttpGet]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<IEnumerable<SeatReservationDto>>> Get(int eventId)
        {
            return Respond
                .AsyncWithMany<SeatReservationDto>(async (response, fail) =>
                {
                    response.Result = await _reservationsProvider.GetEventSeatReservationsAsync(eventId);
                });
        }

        [HttpGet("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public string Get(int eventId, int id)
        {
            return "value";
        }

        [HttpPost]
        [Authorize(Policy = nameof(PolicyDefinitions.AdministratorsOnly))]
        public void Post(int eventId, [FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.AdministratorsOnly))]
        public void Put(int eventId, int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.AdministratorsOnly))]
        public void Delete(int eventId, int id)
        {
        }
    }
}
