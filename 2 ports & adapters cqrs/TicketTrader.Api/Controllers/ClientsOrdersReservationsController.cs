using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Api.Services.Reservations;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Clients/{clientId}/Orders/{orderId}/Reservations/Events/{eventId}")]
    public class ClientsOrdersReservationsController : ApiController
    {
        private readonly ReservationsService _reservationsService;

        public ClientsOrdersReservationsController(
            ILogger<ApiController> logger,
            ReservationsService reservationsService) : base(logger)
        {
            _reservationsService = reservationsService;
        }

        [HttpGet]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<IEnumerable<SeatReservationDto>>> Get(string eventId, string clientId, string orderId)
        {
            return Respond
                .AsyncWithMany<SeatReservationDto>(async (response, fail) =>
                {
                    response.Result =
                        await _reservationsService.GetEventOrderReservationsAsync(eventId, clientId, orderId);
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
        public Task<ApiResponse<SeatReservationDto>> Post(string eventId, string clientId, string orderId, [FromBody]string sceneSeatId)
        {
            return Respond
                .AsyncWith<SeatReservationDto>(async (response, fail) =>
                {
                    response.Result = await _reservationsService.ReserveSeatAsync(eventId, clientId, orderId, sceneSeatId);
                });
        }

        [HttpPut("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public void Put(int eventId, int clientId, int orderId, int id, [FromBody]string value)
        {
        }

        [HttpDelete("{seatId}/reservations/{reseravationId}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse> Delete(string eventId, string clientId, string orderId, string seatId, string reseravationId)
        {
            return Respond
                .AsyncWith(async (response, fail) =>
                {
                    await _reservationsService.DiscardReservationAsync(eventId, clientId, orderId, seatId, reseravationId);
                });
        }
    }
}
