using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace TicketTrader.Api.Client
{
    public class HttpClientFactory
    {
        private readonly HttpClientConfig _config;
        private string _acessToken;

        public HttpClientFactory(HttpClientConfig config)
        {
            _config = config;
        }

        public HttpClient CreateAuthorizedClient()
        {
            lock (this)
            {
                _acessToken = GetToken().GetAwaiter().GetResult();
            }

            var client = new HttpClient();
            client.SetBearerToken(_acessToken);
            return client;
        }

        private async Task<string> GetToken()
        {
            TokenResponse tokenResponse;

            try
            {
                using (var discoveryClient = CreateDiscoveryClient(_config.IdentityServerAddress))
                {
                    var disco = await discoveryClient.GetAsync();
                    using (var tokenClient = new TokenClient(disco.TokenEndpoint, _config.ClientId, _config.ClientSecret))
                    {
                        var scopes = string.Join(" ", _config.Scopes);
                        tokenResponse = await tokenClient.RequestClientCredentialsAsync(scopes);
                    }
                }

                if (tokenResponse.IsError)
                    throw new UnauthorizedAccessException(tokenResponse.Error);
            }
            catch (Exception e)
            {
                throw new UnauthorizedAccessException("Cannot authorize api client", e);
            }
            return tokenResponse.AccessToken;
        }

        private static DiscoveryClient CreateDiscoveryClient(string identityServerAddress)
        {
            return new DiscoveryClient(identityServerAddress)
            {
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            };
        }
    }

    public class HttpClientConfig
    {
        public string IdentityServerAddress { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string[] Scopes { get; set; }
    }
}
