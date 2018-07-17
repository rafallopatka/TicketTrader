using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using TicketTrader.Infrastructure.SharedNames;

namespace TicketTrader.Identity.Data
{
    public class SeedApiResources
    {
        public static IEnumerable<ApiResource> Get()
        {
            return new List<ApiResource> {
                new ApiResource(TicketTraderScopes.FullApiScope, "TicketTrader Events Api")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope(TicketTraderScopes.ApiAdministrativeScope),
                        new Scope(TicketTraderScopes.ApiSaleScope),
                        new Scope(TicketTraderScopes.ApiInternalScope)
                    },
                    UserClaims = new List<string>
                    {
                        TicketTraderClaims.IsBannedCustomer,
                        TicketTraderClaims.TicketTraderStoreClaims,
                        TicketTraderClaims.IsAdministrator,
                        TicketTraderClaims.Role,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                    }
                }
            };
        }
    }
}
