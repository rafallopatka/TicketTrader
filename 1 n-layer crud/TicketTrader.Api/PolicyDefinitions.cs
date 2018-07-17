using Microsoft.AspNetCore.Authorization;
using TicketTrader.Infrastructure.SharedNames;

namespace TicketTrader.Api
{
    public static class PolicyDefinitions
    {
        public static void AddPolicyRules(this AuthorizationOptions options)
        {
            options.AddPolicy(AdministratorsOnly, policy => policy.RequireClaim(TicketTraderClaims.IsAdministrator, "true"));
            options.AddPolicy(ApprovedCustomersOnly, policy => policy.RequireClaim(TicketTraderClaims.IsBannedCustomer, "false"));
            options.AddPolicy(JustAuthenticatedUsers, policy => policy.RequireClaim("scope", TicketTraderScopes.ApiInternalScope, TicketTraderScopes.ApiSaleScope));
            options.AddPolicy(Management, policy => policy.RequireClaim("scope", 
                TicketTraderScopes.ApiInternalScope, 
                TicketTraderScopes.ApiSaleScope, 
                TicketTraderScopes.ApiAdministrativeScope));

        }

        public static string AdministratorsOnly = nameof(AdministratorsOnly);
        public static string ApprovedCustomersOnly = nameof(ApprovedCustomersOnly);
        public static string JustAuthenticatedUsers = nameof(JustAuthenticatedUsers);
        public static string Management = nameof(Management);

    }
}