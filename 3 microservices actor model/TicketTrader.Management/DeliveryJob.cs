using System;
using System.Threading.Tasks;
using TicketTrader.Api.Client;

namespace TicketTrader.Management
{
    public class DeliveryJob
    {
        private readonly HttpClientFactory _factory;

        public DeliveryJob(HttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task DeliiverAsync()
        {
            try
            {
                using (var client = _factory.CreateAuthorizedClient())
                {
                    var api = Environment.GetEnvironmentVariable("TICKETTRADER_API_HOST");
                    var apiClient = new ManagementDeliveriesClient(api, client);
                    await apiClient.DeliverAsync();
                }
            }
            catch (System.Exception e)
            {

            }
        }
    }
}