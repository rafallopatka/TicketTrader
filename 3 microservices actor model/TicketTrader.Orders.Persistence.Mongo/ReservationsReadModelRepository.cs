using System.Collections.Generic;
using System.Threading.Tasks;
using TicketTrader.Orders.ReadModel.Reservations;
using TicketTrader.Shared.Persistence.Mongo.ReadSide;
using MongoDB.Driver;
using TicketTrader.Orders.Domain;

namespace TicketTrader.Orders.Persistence.Mongo
{
    class ReservationsReadModelRepository: MongoReadSideRepository<SeatReservationReadModel>, IReservationsFinder, IOrderReservationDenormalizer
    {
        public ReservationsReadModelRepository(MongoReadSideRepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SeatReservationReadModel>> GetEventSeatsReservations(string eventId)
        {
            var result = await Collection.FindAsync(x => x.EventId == eventId);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<SeatReservationReadModel>> GetEventOrderReservations(string eventId, string clientId, string orderId)
        {
            var result = await Collection.FindAsync(x => x.EventId == eventId && x.OrderId == orderId);
            return await result.ToListAsync();
        }

        public async Task CreateReservation(Order order, Reservation reservation)
        {
            var readModel = new SeatReservationReadModel
            {
                Id = reservation.Id.ToString(),
                SceneSeatId = reservation.Seat.Id.ToString(),
                EventId = reservation.EventId.ToString(),
                OrderId = order.Id.ToString()
            };

            await Collection.InsertOneAsync(readModel);
        }

        public async Task DiscardReservation(Reservation reservation)
        {
            await Collection.DeleteOneAsync(model => model.Id == reservation.Id.ToString());
        }
    }
}
