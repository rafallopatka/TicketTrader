using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Services.Domain.Orders.OrderDeliveries;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Clients/{clientId}/Orders/{orderId}/Deliveries")]
    public class ClientsOrdersDeliveriesController : ApiController
    {
        private readonly IOrderDeliveriesService _deliveriesService;

        public ClientsOrdersDeliveriesController(ILogger<ApiController> logger,
            IOrderDeliveriesService deliveriesService)
            : base(logger)
        {
            _deliveriesService = deliveriesService;
        }

        [HttpGet()]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<IEnumerable<OrderDeliveryDto>>> Get(int clientId, int orderId)
        {
            return Respond
                .AsyncWithMany<OrderDeliveryDto>(async (response, fail) =>
                {
                    response.Result = await _deliveriesService.GetSelectedDeliveryAsync(clientId, orderId);
                });
        }

        [HttpGet("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public string Get(int eventId, int clientId, int orderId, int id)
        {
            return "value";
        }

        [HttpPost()]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse> Post(int clientId, int orderId, [FromBody]int optionId)
        {
            return Respond
                .AsyncWith(async (response, fail) =>
                {
                    await _deliveriesService.SelectDeliveryAsync(clientId, orderId, optionId);
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
            throw new NotImplementedException();
        }
    }
}