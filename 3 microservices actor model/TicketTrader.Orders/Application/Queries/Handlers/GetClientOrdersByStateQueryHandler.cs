using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.Domain.Queries;
using TicketTrader.Orders.Domain.Queries.ReadModels;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Queries.Handlers
{
    class GetClientOrdersByStateQueryHandler : IQueryHandler<GetClientOrdersByStateQuery, GetClientOrdersByStateQuery.Response>
    {
        private readonly DomainActorSystem _domainActorSystem;

        public GetClientOrdersByStateQueryHandler(DomainActorSystem domainActorSystem)
        {
            _domainActorSystem = domainActorSystem;
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

            var response = await _domainActorSystem.Query<RespondCustomerOrdersByState>(new GetCustomerOrdersByState(stateReadModel, query.ClientId));

            return new GetClientOrdersByStateQuery.Response
            {
                ClientOrders = response.Orders.Select(x => new GetClientOrdersByStateQuery.ClientOrder
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