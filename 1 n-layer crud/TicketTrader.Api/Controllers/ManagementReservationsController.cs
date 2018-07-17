using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Services.Domain.Orders.OrderPayments;
using TicketTrader.Services.Domain.Orders.OrdersManagement;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Management/Reservations")]
    [Authorize(Policy = nameof(PolicyDefinitions.Management))]
    public class ManagementReservationsController : ApiController
    {
        private readonly IOrderManagementService _service;

        public ManagementReservationsController(IOrderManagementService service, ILogger<ApiController> logger) : base(logger)
        {
            _service = service;
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
        public void Discard()
        {
            _service.DiscardOrdersAsync();
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
