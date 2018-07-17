using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Api.Services.DeliveryTypes;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Deliveries")]
    public class DeliveriesController : ApiController
    {
        private readonly DeliveryTypesProvider _provider;

        public DeliveriesController(DeliveryTypesProvider provider, ILogger<ApiController> logger) : base(logger)
        {
            _provider = provider;
        }

        // GET: api/Deliveries
        [HttpGet]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<IList<DeliveryTypeDto>>> Get()
        {
            return Respond
                .AsyncWith<IList<DeliveryTypeDto>>(async (response, fail) =>
                    {
                        response.Result = await _provider.GetDeliveryTypesAsync();
                    });
        }

        // GET: api/Deliveries/5
        [HttpGet("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Deliveries
        [HttpPost]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Deliveries/5
        [HttpPut("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
