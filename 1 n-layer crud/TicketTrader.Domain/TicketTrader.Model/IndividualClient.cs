namespace TicketTrader.Model
{
    public class IndividualClient : Client
    {
        public string IdentityUserId { get; set; }

        public string FistName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}