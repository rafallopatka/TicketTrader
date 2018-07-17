using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Api.Services.ClientsOrders
{
    public class ClientOrdersProvider
    {
        public async Task<IEnumerable<ClientOrderDto>> GetClientOrdersByStateAsync(string clientId, ClientOrderState? state)
        {
            var query = new GetClientOrdersByStateQuery
            {
                ClientId = clientId,
                State = (GetClientOrdersByStateQuery.OrderState?)state
            };

            var response = await QueryBus.Current.Query<GetClientOrdersByStateQuery, GetClientOrdersByStateQuery.Response>(query);

            return response.ClientOrders.Select(x => new ClientOrderDto
            {
                Id = x.Id,
                CreateDateTime = x.CreateDateTime,
                ExpirationTimeout = x.ExpirationTimeout,
                UpdateDateTime = x.UpdateDateTime,
                ClientId = x.ClientId
            });
        }
    }
}