using System;
using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Orders.ReadModel.Clients;
using TicketTrader.Orders.ReadModel.Shared;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Queries.Handlers
{
    class GetClientOrdersByStateQueryHandler : IQueryHandler<GetClientOrdersByStateQuery, GetClientOrdersByStateQuery.Response>
    {
        private readonly IClientOrdersFinder _finder;

        public GetClientOrdersByStateQueryHandler(IClientOrdersFinder finder)
        {
            _finder = finder;
        }

        public async Task<GetClientOrdersByStateQuery.Response> Handle(GetClientOrdersByStateQuery query)
        {
            OrderStateReadModel stateReadModel;

            switch (query.State)
            {
                case GetClientOrdersByStateQuery.OrderState.Active:
                    stateReadModel = OrderStateReadModel.Active;
                    break;
                case GetClientOrdersByStateQuery.OrderState.Expired:
                    stateReadModel = OrderStateReadModel.Expired;
                    break;
                case GetClientOrdersByStateQuery.OrderState.Commited:
                    stateReadModel = OrderStateReadModel.Commited;
                    break;
                case GetClientOrdersByStateQuery.OrderState.Finalized:
                    stateReadModel = OrderStateReadModel.Finalized;
                    break;
                case GetClientOrdersByStateQuery.OrderState.Canceled:
                    stateReadModel = OrderStateReadModel.Canceled;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ;
            var response = await _finder.GetClientOrdersByStateAsync(query.ClientId, stateReadModel);

            return new GetClientOrdersByStateQuery.Response
            {
                ClientOrders = response.Select(x => new GetClientOrdersByStateQuery.ClientOrder
                {
                    Id = x.Id,
                    ClientId = x.ClientId,
                    CreateDateTime = x.CreateDateTime,
                    ExpirationTimeout = x.ExpirationTimeout,
                    UpdateDateTime = x.UpdateDateTime,
                }).ToList()
            };
        }
    }
}