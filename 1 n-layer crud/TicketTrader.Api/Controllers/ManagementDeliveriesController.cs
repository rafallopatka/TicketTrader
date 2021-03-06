using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Services.Domain.Orders.OrdersManagement;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Management/Deliveries")]
    [Authorize(Policy = nameof(PolicyDefinitions.Management))]
    public class ManagementDeliveriesController : ApiController
    {
        private readonly IOrderManagementService _service;

        public ManagementDeliveriesController(IOrderManagementService service, ILogger<ApiController> logger) : base(logger)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public Task<ApiResponse> Deliver()
        {
            return Respond.AsyncWith(async (response, fail) =>
            {
                await _service.DeliverOrderAsync();
            });
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}