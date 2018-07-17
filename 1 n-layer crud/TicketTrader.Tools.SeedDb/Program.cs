using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicketTrader.Dal;
using TicketTrader.Model;

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
                    "User ID=postgres;Password=devpwd;Host=localhost;Port=5433;Database=tickettrader-dal;Pooling=true",
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

            LoadPaymentTypes(dalContext);
            LoadDeliveryTypes(dalContext);

            Console.WriteLine("Finished");
        }

        private static void LoadPaymentTypes(DalContext dalContext)
        {
            var cardPaymentPrice = new Price()
            {
                GrossAmount = 0,
                NetAmount = 0,
                VatRate = 8
            };
            var cardPayment = new CardPayment
            {
                ServiceFee = cardPaymentPrice,
                Name = "Card payment"
            };

            var onlinePaymentPrice = new Price()
            {
                GrossAmount = 3,
                NetAmount = 2.78m,
                VatRate = 8
            };
            var onlinePayment = new OnlinePayment()
            {
                ServiceFee = onlinePaymentPrice,
                Name = "Online payment"
            };

            dalContext.Prices.Add(cardPaymentPrice);
            dalContext.CardPayments.Add(cardPayment);

            dalContext.Prices.Add(onlinePaymentPrice);
            dalContext.OnlinePayments.Add(onlinePayment);

            dalContext.SaveChanges();
        }

        private static void LoadDeliveryTypes(DalContext dalContext)
        {
            var directDeliveryPrice = new Price()
            {
                GrossAmount = 0,
                NetAmount = 0,
                VatRate = 8
            };
            var directDelivery = new DirectDelivery()
            {
                ServiceFee = directDeliveryPrice,
                Name = "Direct delivery"
            };

            var onlineDeliveryPrice = new Price()
            {
                GrossAmount = 3,
                NetAmount = 2.78m,
                VatRate = 8
            };
            var onlineDelivery = new OnlineDelivery()
            {
                ServiceFee = onlineDeliveryPrice,
                Name = "Online delivery"
            };

            var dispatchDeliveryPrice = new Price()
            {
                GrossAmount = 15,
                NetAmount = 13.89m,
                VatRate = 8
            };
            var dispatchDelivery = new DispatchDelivery()
            {
                ServiceFee = dispatchDeliveryPrice,
                Name = "Dispatch delivery"
            };

            dalContext.Prices.Add(directDeliveryPrice);
            dalContext.DirectDeliveries.Add(directDelivery);

            dalContext.Prices.Add(onlineDeliveryPrice);
            dalContext.OnlineDeliveries.Add(onlineDelivery);

            dalContext.Prices.Add(dispatchDeliveryPrice);
            dalContext.DispatchDeliveries.Add(dispatchDelivery);

            dalContext.SaveChanges();
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