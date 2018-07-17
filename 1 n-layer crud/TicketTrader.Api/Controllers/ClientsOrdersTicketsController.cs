using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Services.Domain.Orders.OrderTickets;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Clients/{clientId}/Orders/{orderId}/Tickets")]
    public class ClientsOrdersTicketsController : ApiController
    {
        private readonly IOrderTicketsService _ticketsService;

        public ClientsOrdersTicketsController(ILogger<ApiController> logger, IOrderTicketsService ticketsService) : base(logger)
        {
            _ticketsService = ticketsService;
        }

        [HttpGet()]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<IEnumerable<TicketOrderDto>>> Get(int clientId, int orderId)
        {
            return Respond
                .AsyncWithMany<TicketOrderDto>(async (response, fail) =>
                {
                    response.Result = await _ticketsService.GetClientTicketsAsync(clientId, orderId);
                });
        }

        [HttpGet("Events/{eventId}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<IEnumerable<TicketOrderDto>>> Get(int eventId, int clientId, int orderId)
        {
            return Respond
                .AsyncWithMany<TicketOrderDto>(async (response, fail) =>
                {
                    response.Result = await _ticketsService.GetClientTicketsForEventAsync(eventId, clientId, orderId);
                });
        }

        [HttpGet("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public string Get(int eventId, int clientId, int orderId, int id)
        {
            return "value";
        }

        [HttpPost("Events/{eventId}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<TicketOrderDto>> Post(int eventId, int clientId, int orderId, [FromBody]SeatPriceOptionDto option)
        {
            return Respond
                .AsyncWith<TicketOrderDto>(async (response, fail) =>
                {
                    response.Result = await _ticketsService.ReserveTicketAsync(eventId, clientId, orderId, option);
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
            return Respond.AsyncWith(async (response, fail) =>
            {
                await _ticketsService.RemoveTicketAsync(eventId, clientId, orderId, id);
            });
        }
    }
}
