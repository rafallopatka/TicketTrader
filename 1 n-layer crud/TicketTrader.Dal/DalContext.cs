using Microsoft.EntityFrameworkCore;
using TicketTrader.Model;

namespace TicketTrader.Dal
{
    public class DalContext : DbContext
    {
        #region init
        public DalContext(DbContextOptions<DalContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelMappings.Configure(modelBuilder);
        }
        #endregion

        #region properties

        #region clients
        public DbSet<Client> Clients { get; set; }
        public DbSet<AnonymousClient> AnonymousClients { get; set; }
        public DbSet<BusinessClient> BusinessClients { get; set; }
        public DbSet<IndividualClient> IndividualClients { get; set; }
        #endregion

        #region orders
        public DbSet<Order> Orders { get; set; }
        public DbSet<AdditionalProduct> AdditionalProducts { get; set; }
        public DbSet<TicketProduct> TicketProducts { get; set; }
        #endregion

        #region payments
        public DbSet<Payment> Payments { get; set; }
        public DbSet<CardPayment> CardPayments { get; set; }
        public DbSet<CashPayment> CashPayments { get; set; }
        public DbSet<OnlinePayment> OnlinePayments { get; set; }
        #endregion

        #region deliveries
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DirectDelivery> DirectDeliveries { get; set; }
        public DbSet<DispatchDelivery> DispatchDeliveries { get; set; }
        public DbSet<OnlineDelivery> OnlineDeliveries { get; set; }
        #endregion

        #region sales documents
        public DbSet<SalesDocument> SalesDocuments { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<IndividualClientInvoice> IndividualClientInvoices { get; set; }
        public DbSet<BusinessClientInvoice> BusinessClientInvoices { get; set; }
        #endregion

        #region prices
        public DbSet<Price> Prices { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<PriceOption> PriceOptions { get; set; }
        public DbSet<PriceZone> PriceZones { get; set; }
        #endregion

        #region seats
        public DbSet<Seat> Seats { get; set; }
        public DbSet<NumberedSeat> NumberedSeats { get; set; }
        public DbSet<UnnumberedSeat> UnnumberedSeats { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Scene> Scenes { get; set; }
        #endregion

        #region events
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<EventDescription> EventDescriptions { get; set; }
        public DbSet<EventDescriptionCategories> EventDescriptionCategories { get; set; }
        #endregion

        #region reservations
        public DbSet<Reservation> Reservations { get; set; }
        #endregion

        #endregion
    }
}
