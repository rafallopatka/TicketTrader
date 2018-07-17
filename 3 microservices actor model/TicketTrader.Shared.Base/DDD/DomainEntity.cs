namespace TicketTrader.Shared.Base.DDD
{
    public class DomainEntity
    {
        public Id Id { get; set; }
        public EntityStatus EntityStatus { get; set; }

        public void MarkAsArchived()
        {
            EntityStatus = EntityStatus.Archived;
        }
    }
}