using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketTrader.Api.Core;
using TicketTrader.Api.Services.Customers;

namespace TicketTrader.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Users/{identityId}/Clients")]
    public class UsersClientsController : ApiController
    {
        private readonly UserClientService _userClient;

        public UsersClientsController(ILogger<ApiController> logger, UserClientService userClient) : base(logger)
        {
            _userClient = userClient;
        }

        [HttpGet]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<IEnumerable<UserClientDto>>> Get(string identityId)
        {
            return Respond
                .AsyncWithMany<UserClientDto>(async (response, fail) =>
                {
                    var client = await _userClient.GetClientForUserAsync(identityId);
                    response.Result = client != null ? client.AsEnumeration() : new UserClientDto[0];
                });
        }

        [HttpGet("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public string Get(string identityId, int id)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public Task<ApiResponse<UserClientDto>> Post(string identityId, [FromBody]UserDto user)
        {
            return Respond
                .AsyncWith<UserClientDto>(async (response, fail) =>
                {
                    response.Result = await _userClient.CreateClientForUserAsync(user);
                });
        }
        
        [HttpPut("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public void Put(string identityId, int id, [FromBody]UserClientDto client)
        {
            throw new NotImplementedException();
        }
        
        [HttpDelete("{id}")]
        [Authorize(Policy = nameof(PolicyDefinitions.ApprovedCustomersOnly))]
        public void Delete(string identityId, int id)
        {
            throw new NotImplementedException();
        }
    }
}
