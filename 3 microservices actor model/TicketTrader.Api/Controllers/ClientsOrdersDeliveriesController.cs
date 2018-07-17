using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Api.Services.OrderDeliveries;
using TicketTrader.EventDefinitions.Gateway.Client;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Clients/{clientId}/Orders/{orderId}/Deliveries")]
    public class ClientsOrdersDeliveriesController : ApiController
    {
        private readonly EventDefinitionsClientProvider _eventDefinitionsClient;
        private readonly OrderDeliveriesService _deliveriesService;

        public ClientsOrdersDeliveriesController(ILogger<ApiController> logger, 
            EventDefinitionsClientProvider eventDefinitionsClient,
            OrderDeliveriesService deliveriesService)
            : base(logger)
        {
            _eventDefinitionsClient = eventDefinitionsClient;
            _deliveriesService = deliveriesService;
        }

        [HttpGet()]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<IEnumerable<OrderDeliveryDto>>> Get(string clientId, string orderId)
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
        public Task<ApiResponse> Post(string clientId, string orderId, [FromBody]string optionId)
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