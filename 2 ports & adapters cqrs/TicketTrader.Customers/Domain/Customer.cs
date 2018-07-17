using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Customers.Domain
{
    public class Customer: AggregateRoot
    {
        public User User { get; protected set; }
        public int NumberOfCompletedOrders  { get; set; }
        public CustomerRank Rank { get; protected set; }

        public Customer(Id id, User user)
        {
            Id = id;
            Rank = CustomerRank.New;
            User = user;

        }

        public enum CustomerRank
        {
            New,
            Standard,
            Gold
        }

        public void UpgradeToStandardRank()
        {
            EventBus.Current.PublishAsync(new CustomerUpgradedToStandardRankEvent(Id));
        }

        public void UpgradeToGoldRank()
        {
            EventBus.Current.PublishAsync(new CustomerUpgradedToStandardRankEvent(Id));
        }
    }
}
