namespace TicketTrader.Model
{
    public class BusinessClient : Client
    {
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Nip { get; set; }
    }
}