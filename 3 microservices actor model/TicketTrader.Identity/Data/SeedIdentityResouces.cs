using System.Collections.Generic;
using IdentityServer4.Models;
using TicketTrader.Infrastructure.SharedNames;

namespace TicketTrader.Identity.Data
{
    public class SeedIdentityResouces
    {
        public static IEnumerable<IdentityResource> Get()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResource {
                    Name = TicketTraderClaims.Role,
                    UserClaims = new List<string> { TicketTraderClaims.Role },
                    Required =  true
                },
                new IdentityResource
                {
                    Name = TicketTraderClaims.TicketTraderStoreClaims,
                    DisplayName = "TicketTrader profile",
                    Required = true,
                    UserClaims = new List<string>{ TicketTraderClaims.IsAdministrator, TicketTraderClaims.IsBannedCustomer }
                },
            };
        }
    }
}
