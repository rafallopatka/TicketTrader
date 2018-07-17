using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Services.Domain.Orders.ClientsOrders;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Clients/{clientId}/Orders")]
    public class ClientsOrdersController : ApiController
    {
        private readonly IClientsOrdersProvider _ordersProvider;
        private readonly IClientsOrderService _orderService;

        public ClientsOrdersController(ILogger<ApiController> logger, 
            IClientsOrdersProvider ordersProvider,
            IClientsOrderService orderService) : base(logger)
        {
            _ordersProvider = ordersProvider;
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<IEnumerable<ClientOrderDto>>> Get(int clientId, ClientOrderState? state)
        {
            return Respond
                .AsyncWithMany<ClientOrderDto>(async (response, fail) =>
                {
                    response.Result = await _ordersProvider.GetClientOrdersByStateAsync(clientId, state);
                });
        }

        [HttpGet("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public string Get(int clientId, int id)
        {
            return "value";
        }
        
        [HttpPost]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<ClientOrderDto>> Post(int clientId)
        {
            return Respond
                .AsyncWith<ClientOrderDto>(async (response, fail) =>
                {
                    response.Result = await _orderService.CreateOrderForClientAsync(clientId);
                });
        }

        [HttpPost("{id}/Commit")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse> Commit(int clientId, int id)
        {
            return Respond
                .AsyncWith(async (response, fail) =>
                {
                    await _orderService.CommitOrderAsync(clientId, id);
                });
        }

        [HttpPut("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public void Put(int clientId, int id, [FromBody]string value)
        {
        }
        
        [HttpDelete("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse> Delete(int clientId, int id)
        {
            return null;
        }
    }
}
