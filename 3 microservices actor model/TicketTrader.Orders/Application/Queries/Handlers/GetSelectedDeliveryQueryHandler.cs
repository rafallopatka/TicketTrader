using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.Domain.Queries;
using TicketTrader.Orders.Domain.Queries.ReadModels;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Queries.Handlers
{
    class GetSelectedDeliveryQueryHandler : IQueryHandler<GetSelectedDeliveryQuery, GetSelectedDeliveryQuery.Response>
    {
        private readonly DomainActorSystem _domainActorSystem;

        public GetSelectedDeliveryQueryHandler(DomainActorSystem domainActorSystem)
        {
            _domainActorSystem = domainActorSystem;
        }

        public async Task<GetSelectedDeliveryQuery.Response> Handle(GetSelectedDeliveryQuery query)
        {
            var response = await _domainActorSystem.Query<RespondSelectedDelivery>(new GetSelectedDelivery(query.ClientId, query.OrderId));

            var value = response == null
                ? null
                : new GetSelectedDeliveryQuery.DeliveryDto
                {
                    OrderId = response.Id,
                    DeliveryId = response.DeliveryId
                };

            return new GetSelectedDeliveryQuery.Response
            {
                Value = value
            };
        }
    }
}