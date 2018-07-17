using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Api.Services.Payment;
// ReSharper disable PossibleMultipleEnumeration

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Management/Orders")]
    [Authorize(Policy = nameof(PolicyDefinitions.Management))]
    public class ManagementOrdersController : ApiController
    {
        private readonly PaymentService _paymentService;

        public ManagementOrdersController(PaymentService paymentService, ILogger<ApiController> logger) : base(logger)
        {
            _paymentService = paymentService;
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
                var unpaidPayments = await _paymentService.GetWaitingPaymentsAsync();
                if (unpaidPayments.Any())
                {
                    var payment = unpaidPayments.First();

                    await _paymentService.RegisterPaymentSuccessAsync(payment);
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
