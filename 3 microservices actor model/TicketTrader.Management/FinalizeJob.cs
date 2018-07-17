using System;
using System.Threading.Tasks;
using TicketTrader.Api.Client;

namespace TicketTrader.Management
{
    public class PayJob
    {
        private readonly HttpClientFactory _factory;

        public PayJob(HttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task FinalizeAsync()
        {
            try
            {
                using (var client = _factory.CreateAuthorizedClient())
                {
                    var api = Environment.GetEnvironmentVariable("TICKETTRADER_API_HOST");
                    var apiClient = new ManagementOrdersClient(api, client);
                    await apiClient.PayAsync();
                }
            }
            catch (System.Exception e)
            {

            }
        }
    }
}