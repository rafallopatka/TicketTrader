using Microsoft.EntityFrameworkCore;
using TicketTrader.EventDefinitions.Entities;

namespace TicketTrader.EventDefinitions.EntityFramework
{
    public static class ModelMappings
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventDescriptionCategories>()
                .HasKey(bc => new { bc.DescriptionId, bc.CategoryId });


            modelBuilder.Entity<EventDescriptionCategories>()
                .HasOne(bc => bc.Description)
                .WithMany(b => b.EventCategories)
                .HasForeignKey(bc => bc.DescriptionId);

            modelBuilder.Entity<EventDescriptionCategories>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.EventCategories)
                .HasForeignKey(bc => bc.CategoryId);

            modelBuilder.Entity<Event>()
                .HasOne(x => x.Scene)
                .WithOne(x => x.Event)
                .HasForeignKey<Scene>(s => s.EventId);

            modelBuilder.Entity<Event>()
                .HasOne(x => x.Description)
                .WithMany(x => x.Events)
                .HasForeignKey(x => x.DescriptionId);

            modelBuilder.Entity<Event>()
                .HasMany(x => x.PriceLists)
                .WithOne(x => x.Event)
                .HasForeignKey(x => x.EventId);

            modelBuilder.Entity<PriceList>()
                .HasMany(x => x.PriceZones)
                .WithOne(x => x.PriceList)
                .HasForeignKey(x => x.PriceListId);

            modelBuilder.Entity<PriceOption>()
                .HasOne(x => x.PriceZone)
                .WithMany(x => x.Options)
                .HasForeignKey(x => x.PriceZoneId);

            modelBuilder.Entity<Seat>()
                .HasOne(x => x.Event)
                .WithMany(x => x.Seats)
                .HasForeignKey(x => x.EventId);
        }
    }
}