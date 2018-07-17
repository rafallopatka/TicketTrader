using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Api.Services.PaymentTypes;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Payments")]
    public class PaymentsController : ApiController
    {
        private readonly PaymentTypesProvider _provider;

        public PaymentsController(PaymentTypesProvider provider, ILogger<ApiController> logger) : base(logger)
        {
            _provider = provider;
        }

        [HttpGet]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<IEnumerable<PaymentTypeDto>>> Get()
        {
            return Respond
                .AsyncWith<IEnumerable<PaymentTypeDto>>(async (response, fail) =>
                    {
                        response.Result = await _provider.GetPaymentTypesAsync();
                    });
        }

        [HttpGet("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public string Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize(Policy = nameof(PolicyDefinitions.AdministratorsOnly))]
        public void Post(int eventId, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.AdministratorsOnly))]
        public void Put(int eventId, int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.AdministratorsOnly))]
        public void Delete(int eventId, int id)
        {
            throw new NotImplementedException();
        }
    }
}