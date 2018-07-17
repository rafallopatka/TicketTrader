namespace TicketTrader.Model
{
    public class AdditionalProduct
    {
        public int Id { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}