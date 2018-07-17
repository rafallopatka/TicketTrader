using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TicketTrader.Dal.Migrator
{
    public class MigrationDbContextFactory : IDesignTimeDbContextFactory<DalContext>
    {
        public DalContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DalContext>();
            builder.UseNpgsql("User ID=postgres;Password=devpwd;Host=localhost;Port=5433;Database=tickettrader-dal;Pooling=true");
            return new DalContext(builder.Options);
        }
    }
}