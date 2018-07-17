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
    [Route("api/Management/Orders")]
    [Authorize(Policy = nameof(PolicyDefinitions.Management))]
    public class ManagementOrdersController : ApiController
    {
        private readonly IOrderManagementService _service;

        public ManagementOrdersController(IOrderManagementService service, ILogger<ApiController> logger) : base(logger)
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
        public Task<ApiResponse> Pay()
        {
            return Respond.AsyncWith(async (response, fail) =>
            {
                await _service.PayOrdersAsync();
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
