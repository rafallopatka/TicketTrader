namespace TicketTrader.EventDefinitions.Entities
{
    public class PriceOption
    {
        public int Id { get; set; }
        public Price Price { get; set; }
        public string Name { get; set; }
        public int PriceZoneId { get; set; }
        public PriceZone PriceZone { get; set; }
    }
}