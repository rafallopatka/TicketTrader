namespace TicketTrader.Orders.Domain.Queries.ReadModels
{
    public class OrderReadModel
    {
        public OrderReadModel(string id, OrderStateReadModel state)
        {
            Id = id;
            State = state;
        }

        public string Id { get; }
        public OrderStateReadModel State { get; }
    }
}