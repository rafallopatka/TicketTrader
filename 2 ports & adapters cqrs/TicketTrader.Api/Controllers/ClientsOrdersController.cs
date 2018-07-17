using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Api.Services.ClientsOrders;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Clients/{clientId}/Orders")]
    public class ClientsOrdersController : ApiController
    {
        private readonly ClientOrdersProvider _ordersProvider;
        private readonly ClientsOrderService _ordersService;

        public ClientsOrdersController(ILogger<ApiController> logger, 
            ClientOrdersProvider ordersProvider,
            ClientsOrderService ordersService
            ) : base(logger)
        {
            _ordersProvider = ordersProvider;
            _ordersService = ordersService;
        }

        [HttpGet]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<IEnumerable<ClientOrderDto>>> Get(string clientId, ClientOrderState? state)
        {
            return Respond
                .AsyncWithMany<ClientOrderDto>(async (response, fail) =>
                {
                    try
                    {
                        response.Result = await _ordersProvider.GetClientOrdersByStateAsync(clientId, state);
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
        public string Get(int clientId, int id)
        {
            return "value";
        }
        
        [HttpPost]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<ClientOrderDto>> Post(string clientId)
        {
            return Respond
                .AsyncWith<ClientOrderDto>(async (response, fail) =>
                {
                    response.Result = await _ordersService.CreateOrderForClientAsync(clientId);
                });
        }

        [HttpPost("{id}/Commit")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse> Commit(string clientId, string id)
        {
            return Respond
                .AsyncWith(async (response, fail) =>
                {
                    await _ordersService.CommitOrderAsync(clientId, id);
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
