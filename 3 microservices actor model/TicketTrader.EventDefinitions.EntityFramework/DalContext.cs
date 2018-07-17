using Microsoft.EntityFrameworkCore;
using TicketTrader.EventDefinitions.Entities;

namespace TicketTrader.EventDefinitions.EntityFramework
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

        #endregion
    }
}
