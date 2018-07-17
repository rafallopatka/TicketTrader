using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Test;
using TicketTrader.Infrastructure.SharedNames;

namespace TicketTrader.Identity.Data
{
    public class SeedUsers
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser> {
                new TestUser {
                    SubjectId = "000",

                    Username = "dev",
                    Password = "P@ssw0rd",
                    Claims = new List<Claim> {
                        new Claim(JwtClaimTypes.Name, "Jan Kowalski"),
                        new Claim(JwtClaimTypes.GivenName, "Jan"),
                        new Claim(JwtClaimTypes.FamilyName, "Kowalski"),
                        new Claim(JwtClaimTypes.Email, "jk@tickettrader.identity"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Address, @"{ ""street_address"": ""One Hacker Way"", ""locality"": ""Heidelberg"", ""postal_code"": 69118, ""country"": ""Germany"" }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),

                        new Claim(JwtClaimTypes.Role, "admin"),
                        new Claim(TicketTraderClaims.IsAdministrator, "true", ClaimValueTypes.Boolean),
                        new Claim(TicketTraderClaims.IsBannedCustomer, "false", ClaimValueTypes.Boolean)
                    }
                },
                new TestUser
                {
                    SubjectId = "AAAAA", Username = "client1", Password = "P@ssw0rd", 
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Krzysztof Kowalski"),
                        new Claim(JwtClaimTypes.GivenName, "Krzysztof"),
                        new Claim(JwtClaimTypes.FamilyName, "Kowalski"),
                        new Claim(JwtClaimTypes.Email, "kk@tickettrader.identity"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Address, @"{ ""street_address"": ""One Hacker Way"", ""locality"": ""Heidelberg"", ""postal_code"": 69118, ""country"": ""Germany"" }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim(TicketTraderClaims.IsAdministrator, "false"),
                        new Claim(TicketTraderClaims.IsBannedCustomer, "false")
                    }
                },
                new TestUser
                {
                    SubjectId = "BBBB", Username = "client2", Password = "P@ssw0rd",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Andrzej Kowalski"),
                        new Claim(JwtClaimTypes.GivenName, "Krzysztof"),
                        new Claim(JwtClaimTypes.FamilyName, "Kowalski"),
                        new Claim(JwtClaimTypes.Email, "ak@tickettrader.identity"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Address, @"{ ""street_address"": ""One Hacker Way"", ""locality"": ""Heidelberg"", ""postal_code"": 69118, ""country"": ""Germany"" }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim(TicketTraderClaims.IsAdministrator, "false"),
                        new Claim(TicketTraderClaims.IsBannedCustomer, "false")
                    }
                },
                new TestUser
                {
                    SubjectId = "banned", Username = "banned", Password = "P@ssw0rd",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Banned user"),
                        new Claim(JwtClaimTypes.GivenName, "Banned"),
                        new Claim(JwtClaimTypes.FamilyName, "Banned"),
                        new Claim(JwtClaimTypes.Email, "Banned@tickettrader.identity"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Address, @"{ ""street_address"": ""One Hacker Way"", ""locality"": ""Heidelberg"", ""postal_code"": 69118, ""country"": ""Germany"" }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim(TicketTraderClaims.IsAdministrator, "false"),
                        new Claim(TicketTraderClaims.IsBannedCustomer, "true")
                    }
                },
                new TestUser{SubjectId = "818727", Username = "alice", Password = "P@ssw0rd",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, @"{ ""street_address"": ""One Hacker Way"", ""locality"": ""Heidelberg"", ""postal_code"": 69118, ""country"": ""Germany"" }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                new TestUser{SubjectId = "88421113", Username = "bob", Password = "P@ssw0rd",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, @"{ ""street_address"": ""One Hacker Way"", ""locality"": ""Heidelberg"", ""postal_code"": 69118, ""country"": ""Germany"" }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "somewhere"),
                    }
                },
            };
        }
    }
}
