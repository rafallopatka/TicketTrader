using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Api.Services.Delivery;

namespace TicketTrader.Api.Controllers
{
    // ReSharper disable once PossibleMultipleEnumeration
    [Produces("application/json")]
    [Route("api/Management/Deliveries")]
    [Authorize(Policy = nameof(PolicyDefinitions.Management))]
    public class ManagementDeliveriesController : ApiController
    {
        private readonly DeliveryService _service;


        public ManagementDeliveriesController(DeliveryService service,
            ILogger<ApiController> logger) : base(logger)
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
                var result = await _service.GetWaitingDeliveriesAsync();
                var awaiting = result.FirstOrDefault();

                if (awaiting != null)
                {
                    await _service.RegisterDeliverySuccessAsync(awaiting);
                }
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