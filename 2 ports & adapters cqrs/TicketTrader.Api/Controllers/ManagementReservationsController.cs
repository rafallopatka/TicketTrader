using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Api.Services.Orders;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Management/Reservations")]
    [Authorize(Policy = nameof(PolicyDefinitions.Management))]
    public class ManagementReservationsController : ApiController
    {
        private readonly OrderService _orderService;

        public ManagementReservationsController(OrderService orderService, ILogger<ApiController> logger) : base(logger)
        {
            _orderService = orderService;
        }

        // GET: api/Management
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Management/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Management
        [HttpPost(Name = "Discard")]
        public async Task Discard()
        {
            var orders = await _orderService.GetAwaitingOrdersAsync();

            if (orders.Any())
            {
                var order = orders.First().OrderId;
                await _orderService.DiscardOrderAsync(order);
            }
        }

        // PUT: api/Management/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete()
        {
        }
    }
}
