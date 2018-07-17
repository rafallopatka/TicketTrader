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
    [Route("api/Clients/{clientId}/Orders/{orderId}/Reservations/Events/{eventId}")]
    public class ClientsOrdersReservationsController : ApiController
    {
        private readonly IReservationsService _reservationService;

        public ClientsOrdersReservationsController(ILogger<ApiController> logger,
            IReservationsService reservationsService) : base(logger)
        {
            _reservationService = reservationsService;
        }

        [HttpGet]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<IEnumerable<SeatReservationDto>>> Get(int eventId, int clientId, int orderId)
        {
            return Respond
                .AsyncWithMany<SeatReservationDto>(async (response, fail) =>
                {
                    response.Result = await _reservationService.GetEventOrderReservationsAsync(eventId, clientId, orderId);
                });
        }

        [HttpGet("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public string Get(int eventId, int clientId, int orderId, int id)
        {
            return "value";
        }

        [HttpPost]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<SeatReservationDto>> Post(int eventId, int clientId, int orderId, [FromBody]int sceneSeatId)
        {
            return Respond
                .AsyncWith<SeatReservationDto>(async (response, fail) =>
                {
                    response.Result = await _reservationService.ReserveSeatAsync(eventId, clientId, orderId, sceneSeatId);
                });
        }

        [HttpPut("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public void Put(int eventId, int clientId, int orderId, int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse> Delete(int eventId, int clientId, int orderId, int id)
        {
            return Respond
                .AsyncWith(async (response, fail) =>
                {
                    await _reservationService.DiscardReservationAsync(eventId, clientId, orderId, id);
                });
        }
    }
}
