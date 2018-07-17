using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Api.Services.Reservations;
using TicketTrader.EventDefinitions.Gateway.Client;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Events/{eventId}/Reservations")]
    public class EventsReservationsController : ApiController
    {
        private readonly ReservationsService _reservationsService;

        public EventsReservationsController(ILogger<ApiController> logger, 
            ReservationsService reservationsService) : base(logger)
        {
            _reservationsService = reservationsService;
        }

        [HttpGet]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<IEnumerable<SeatReservationDto>>> Get(string eventId)
        {
            return Respond
                .AsyncWithMany<SeatReservationDto>(async (response, fail) =>
                {
                    try
                    {
                        response.Result = await _reservationsService.GetEventSeatReservationsAsync(eventId);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
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
