namespace TicketTrader.EventDefinitions.Entities
{
    public class NumberedSeat : Seat
    {
        public Sector Sector { get; set; }
        public string Number { get; set; }
    }
}