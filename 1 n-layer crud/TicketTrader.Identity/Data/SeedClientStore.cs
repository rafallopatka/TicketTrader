using System;
using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using TicketTrader.Infrastructure.SharedNames;

namespace TicketTrader.Identity.Data
{
    public class SeedClientStore
    {
        public static IEnumerable<Client> Get(IConfigurationRoot configuration)
        {
            var webClientAddress = configuration.GetValue<string>(EnvironmentVariablesNames.TicketTraderWebHostAddress);
            var apiClientSecret = configuration.GetValue<string>(EnvironmentVariablesNames.TicketTraderApiSecret);

            return new[]
            {
                // OpenID Connect hybrid flow and client credentials client (MVC)
                //new Client
                //{
                //    ClientId = TicketTraderClients.WebClientId,
                //    ClientName = TicketTraderClients.WebClientId,
                //    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                //    ClientSecrets =
                //    {
                //        new Secret(apiClientSecret.Sha256())
                //    },
                //    AllowRememberConsent = true,
                //    RequireConsent = false,
                    
                //    RedirectUris = new List<string> {$"{webClientAddress}/signin-oidc"},
                //    PostLogoutRedirectUris = new List<string> {$"{webClientAddress}/signout-callback-oidc"},
                //    AllowedCorsOrigins = new List<string>
                //    {
                //        webClientAddress
                //    },
                //    AllowedScopes =
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        IdentityServerConstants.StandardScopes.Email,
                //        TicketTraderScopes.ApiSaleScope,
                //    },
                //    AllowOfflineAccess = true,

                //},
                new Client
                {
                    ClientId = TicketTraderClients.ApiClientId,
                    ClientName = TicketTraderClients.ApiClientId,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(apiClientSecret.Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        TicketTraderScopes.ApiSaleScope,
                        TicketTraderClaims.TicketTraderStoreClaims,
                    }
                },


                new Client
                {
                    ClientId = TicketTraderClients.JsClientId,
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = new List<string>
                    {
                        $"{webClientAddress}/Spa/Store/signin-callback.html",
                        $"{webClientAddress}/Spa/Store/silent-calback.html",
                    },
                    PostLogoutRedirectUris = new List<string> {$"{webClientAddress}" },
                    AllowedCorsOrigins = new List<string>
                    {
                        webClientAddress
                    },
                    AllowRememberConsent = true,
                    RequireConsent = true,
                    
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        TicketTraderScopes.ApiSaleScope,
                        TicketTraderClaims.TicketTraderStoreClaims,
                    }
                },
                new Client
                {
                    ClientId = TicketTraderClients.InternalClient,
                    ClientSecrets = { new Secret(apiClientSecret.Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { TicketTraderScopes.ApiSaleScope, TicketTraderScopes.ApiInternalScope },
                },
                new Client
                {
                    ClientId = TicketTraderClients.ManagementClient,
                    ClientSecrets = { new Secret(apiClientSecret.Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { TicketTraderScopes.ApiAdministrativeScope,
                        TicketTraderScopes.ApiSaleScope,
                        TicketTraderScopes.ApiInternalScope },
                }
            };
        }
    }
}
