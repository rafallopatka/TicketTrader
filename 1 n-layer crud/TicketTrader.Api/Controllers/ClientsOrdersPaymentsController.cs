using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Services.Domain.Orders.OrderPayments;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Clients/{clientId}/Orders/{orderId}/Payments")]
    public class ClientsOrdersPaymentsController : ApiController
    {
        private readonly IOrderPaymentsService _paymentService;

        public ClientsOrdersPaymentsController(ILogger<ApiController> logger,
             IOrderPaymentsService paymentService) 
            : base(logger)
        {
            _paymentService = paymentService;
        }

        [HttpGet()]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<IEnumerable<OrderPaymentDto>>> Get(int clientId, int orderId)
        {
            return Respond
                .AsyncWithMany<OrderPaymentDto>(async (response, fail) =>
                {
                    response.Result = await _paymentService.GetSelectedPaymentAsync(clientId, orderId);
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
                    await _paymentService.SelectPaymentAsync(clientId, orderId, optionId);
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