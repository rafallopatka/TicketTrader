using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicketTrader.EventDefinitions.EntityFramework;

namespace TicketTrader.Tools.SeedDb
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Any() == false && Debugger.IsAttached)
            {
                args = new[]
                {
                    "1",
                    "User ID=postgres;Password=devpwd;Host=localhost;Port=5433;Database=tickettrader-event-definitions;Pooling=true",
                    @"D:\Mannelig\Dev\Magisterka\src\TicketTrader\TicketTrader.Web\wwwroot\scenes\theatre-clean.indexed.svg"
                };
            }


            Console.WriteLine("Populating db");

            int multiplication = Convert.ToInt32(args[0]);

            string connectionString = args[1];

            var builder = new DbContextOptionsBuilder<DalContext>();
            builder.UseNpgsql(connectionString);
            var dalContext = new DalContext(builder.Options);

            dalContext.Database.EnsureDeleted();
            dalContext.Database.Migrate();

            var filePaths = args.Skip(2).ToList();

            for (int i = 0; i < multiplication; i++)
            {
                Console.WriteLine($"Iteration {i}");

                foreach (var filePath in filePaths)
                {
                    LoadEventData(dalContext, filePath);
                }
            }

            Console.WriteLine("Finished");
        }

        private static void LoadEventData(DalContext dalContext, string filePath)
        {
            try
            {
                Console.WriteLine($"Loading {filePath}");

                var reader = new ArrangementReader.ArrangementReader();
                var arrangement = reader.Read(filePath);

                var theatreSeedData = new EventSeedData();
                theatreSeedData.Populate(dalContext, arrangement);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
                Console.ResetColor();
            }
        }
    }
}