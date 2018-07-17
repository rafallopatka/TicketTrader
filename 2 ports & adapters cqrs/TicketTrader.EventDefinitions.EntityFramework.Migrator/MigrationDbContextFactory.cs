using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TicketTrader.EventDefinitions.EntityFramework.Migrator
{
    public class MigrationDbContextFactory : IDesignTimeDbContextFactory<DalContext>
    {
        public DalContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DalContext>();
            builder.UseNpgsql("User ID=postgres;Password=devpwd;Host=localhost;Port=5433;Database=tickettrader-event-definitions;Pooling=true");
            return new DalContext(builder.Options);
        }
    }
}