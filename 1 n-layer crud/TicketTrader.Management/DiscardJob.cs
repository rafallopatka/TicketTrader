using System.Threading.Tasks;
using TicketTrader.Api.Client;

namespace TicketTrader.Management
{
    public class DiscardJob
    {
        private readonly HttpClientFactory _factory;

        public DiscardJob(HttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task DiscardReservationsAsync()
        {
            try
            {
                using (var client = _factory.CreateAuthorizedClient())
                {
                    var apiClient = new ManagementReservationsClient(client);
                    await apiClient.DiscardAsync();
                }
            }
            catch (System.Exception e)
            {

            }
        }
    }
}