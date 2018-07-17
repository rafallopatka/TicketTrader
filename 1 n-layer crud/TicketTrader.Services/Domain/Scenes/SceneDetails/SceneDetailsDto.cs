namespace TicketTrader.Services.Domain.Scenes.SceneDetails
{
    public class SceneDetailsDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string DisplayName { get; set; }
        public string UniqueName { get; set; }
    }
}