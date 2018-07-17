using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using TicketTrader.EventDefinitions.Entities;
using TicketTrader.EventDefinitions.EntityFramework;
using TicketTrader.Tools.SeedDb.ArrangementReader;
using Seat = TicketTrader.EventDefinitions.Entities.Seat;
using Sector = TicketTrader.EventDefinitions.Entities.Sector;


namespace TicketTrader.Tools.SeedDb
{
    public class EventSeedData
    {
        public bool StrictModeEnabled { get; set; }

        public void Populate(DalContext context, Arrangement arrangement)
        {
            List<EventCategory> eventCategories;

            if (context.EventCategories.Any() == false)
            {
                eventCategories = GenerateEventCategories();
                context.EventCategories.AddRange(eventCategories);
                context.SaveChanges();
            }
            else
            {
                eventCategories = context.EventCategories.ToList();
            }

            EventDescription description = GenerateEventDescription();
            context.EventDescriptions.AddRange(description);
            context.SaveChanges();

            List<EventDescriptionCategories> eventDescriptionCategories = GenerateEventDescriptionCategories(eventCategories, description);
            context.EventDescriptionCategories.AddRange(eventDescriptionCategories);
            context.SaveChanges();

            List<Sector> sectors = GenerateSectors(arrangement);
            context.Sectors.AddRange(sectors);
            context.SaveChanges();

            List<NumberedSeat> numberedSeats = GenerateNumberedSeats(arrangement, sectors);
            context.NumberedSeats.AddRange(numberedSeats);

            List<UnnumberedSeat> unnumberedSeats = GenerateUnnumberedSeats(arrangement);
            context.UnnumberedSeats.AddRange(unnumberedSeats);

            Scene scene = GenerateScene(arrangement, numberedSeats, unnumberedSeats);
            context.Scenes.AddRange(scene);

            var allSeats = new List<Seat>();
            allSeats.AddRange(numberedSeats);
            allSeats.AddRange(unnumberedSeats);

            Event singleEvent = GenerateEvent(description, scene);
            scene.Event = singleEvent;
            context.Events.AddRange(singleEvent);

            allSeats.ForEach(s =>
            {
                s.Event = singleEvent;
            });

            context.SaveChanges();

            var priceZones = GeneratePriceZones(arrangement, allSeats);
            context.PriceZones.AddRange(priceZones);

            PriceList priceList = GeneratePriceList(singleEvent, priceZones);
            context.PriceLists.AddRange(priceList);
            context.SaveChanges();
        }

        private List<Sector> GenerateSectors(Arrangement arrangement)
        {
            var sectors = arrangement
                .Sectors
                .Select(x => new Sector
                {
                    SceneSectorId = x.SectorId,
                    Name = x.Name
                })
                .ToList();

            return sectors;
        }

        private PriceList GeneratePriceList(Event generatedEvent, List<PriceZone> priceZones)
        {
            return new Faker<PriceList>()
                .RuleFor(o => o.Event, f => generatedEvent)
                .RuleFor(o => o.ValidFrom, f => f.Date.Recent(30))
                .RuleFor(o => o.ValidTo, f => f.Date.Future(1))
                .RuleFor(o => o.PriceZones, f => priceZones)
                .Generate();
        }

        private List<PriceZone> GeneratePriceZones(Arrangement arrangement, List<Seat> allSeats)
        {
            var priceZones = new List<PriceZone>();

            foreach (var sector in arrangement.Sectors)
            {
                Price regularPrice = GetRegularPriceLevels()[sector.DataQuality];
                var discoundedPrice = GetDiscoundedPriceLevels()[sector.DataQuality];

                var priceZone = new PriceZone
                {
                    ScenePriceZoneId = sector.PriceZoneId,
                    Name = sector.Name,
                    Seats = allSeats.Where(x => sector.Seats.Any(s => s.Id == x.SceneSeatId)).ToList(),
                    Options = new List<PriceOption>
                    {
                        new PriceOption {Name = "Regular", Price = regularPrice},
                        new PriceOption {Name = "Discounted", Price = discoundedPrice},
                    }
                };

                priceZones.Add(priceZone);
            }

            return priceZones;
        }

        private Dictionary<int, Price> GetRegularPriceLevels()
        {
            return new Dictionary<int, Price>
            {
                {1, new Price { GrossAmount = 100, NetAmount = 92.59M, VatRate = 8 } },
                {2, new Price { GrossAmount = 150, NetAmount = 138.89M, VatRate = 8 } },
                {3, new Price { GrossAmount = 200, NetAmount = 185.19M, VatRate = 8 } },
                {4, new Price { GrossAmount = 300, NetAmount = 277.78M, VatRate = 8 } },
                {5, new Price { GrossAmount = 500, NetAmount = 462.96M, VatRate = 8 } }
            };
        }

        private Dictionary<int, Price> GetDiscoundedPriceLevels()
        {
            return new Dictionary<int, Price>
            {
                {1, new Price { GrossAmount = 80, NetAmount = 74.07M, VatRate = 8 } },
                {2, new Price { GrossAmount = 130, NetAmount = 120.37M, VatRate = 8 } },
                {3, new Price { GrossAmount = 170, NetAmount = 138.79M, VatRate = 8 } },
                {4, new Price { GrossAmount = 250, NetAmount = 203.25M, VatRate = 8 } },
                {5, new Price { GrossAmount = 400, NetAmount = 325.20M, VatRate = 8 } }
            };
        }

        private List<NumberedSeat> GenerateNumberedSeats(Arrangement arrangement, IEnumerable<Sector> sectors)
        {
            return arrangement
                .Sectors
                .SelectMany(x =>
                {
                    return x.Seats
                        .Where(s => s.SeatType == SeatType.Single)
                        .Select(s => new NumberedSeat
                        {
                            Sector = sectors.Single(f => f.SceneSectorId == x.SectorId),
                            SceneSeatId = s.Id,
                            Number = s.SeatNumber
                        });
                })
                .ToList();
        }

        private List<UnnumberedSeat> GenerateUnnumberedSeats(Arrangement arrangement)
        {
            return arrangement
                .Sectors
                .SelectMany(x =>
                {
                    return x.Seats
                        .Where(s => s.SeatType == SeatType.Unnumbered)
                        .Select(s => new UnnumberedSeat
                        {
                            SceneSeatId = s.Id,
                            Name = x.Name
                        });
                })
                .ToList();
        }

        private Scene GenerateScene(Arrangement arrangement, List<NumberedSeat> numberedSeats, List<UnnumberedSeat> unnumberedSeats)
        {
            var seats = new List<Seat>();
            seats.AddRange(numberedSeats);
            seats.AddRange(unnumberedSeats);
            return new Scene
            {
                DisplayName = arrangement.DisplayName,
                UniqueName = arrangement.FileName,
                Seats = seats
            };
        }

        private Event GenerateEvent(EventDescription eventDescription, Scene scene)
        {
            return new Faker<Event>()
                .StrictMode(StrictModeEnabled)
                .RuleFor(o => o.DateTime, f => f.Date.Future())
                .RuleFor(o => o.Duration, f => f.PickRandom(SampleDurations()))
                .RuleFor(o => o.Description, f => eventDescription)
                .RuleFor(o => o.Scene, f => scene)
                .RuleFor(o => o.Root, f => null)
                .Generate();
        }

        private EventDescription GenerateEventDescription()
        {
            var names = new[] { "Concert", "Musical", "Cabaret", "Spectacle", "Performance" };

            var eventDescription = new Faker<EventDescription>()
                .StrictMode(StrictModeEnabled)
                .RuleFor(o => o.Title, f => f.PickRandom(names))
                .RuleFor(o => o.Authors, f => GeneratePersons(f.Random.Number(5), f.Person.FirstName, f.Person.LastName))
                .RuleFor(o => o.Cast, f => GeneratePersons(f.Random.Number(10), f.Person.FirstName, f.Person.LastName))
                .RuleFor(o => o.Description, f => f.Lorem.Paragraph())
                .RuleFor(o => o.EventCategories, f => new List<EventDescriptionCategories>())
                .Generate();
            return eventDescription;
        }

        private List<EventDescriptionCategories> GenerateEventDescriptionCategories(List<EventCategory> eventCategories, EventDescription description)
        {
            var result = new List<EventDescriptionCategories>
            {
                new Faker<EventDescriptionCategories>()
                    .RuleFor(o => o.Description, f => description)
                    .RuleFor(o => o.Category, f => eventCategories[0])
                    .Generate(),

                new Faker<EventDescriptionCategories>()
                    .RuleFor(o => o.Description, f=> description)
                    .RuleFor(o => o.Category, f => eventCategories[1])
                    .Generate()
            };

            return result;
        }

        private List<EventCategory> GenerateEventCategories()
        {
            return new List<EventCategory>()
            {
                new EventCategory { Id = 1, Name =  "Category 1" },
                new EventCategory { Id = 2, Name = "Category 2" },
                new EventCategory { Id = 3, Name = "Category 3" },
            };
        }

        protected TimeSpan[] SampleDurations()
        {
            return new[]
            {
                TimeSpan.FromHours(1),
                TimeSpan.FromHours(1.5),
                TimeSpan.FromHours(2),
                TimeSpan.FromHours(2.5),
                TimeSpan.FromHours(3)
            };
        }

        protected string GeneratePersons(int number, string firstName, string lastName)
        {
            return String.Join(", ", Enumerable.Range(1, number).Select(x => $"{firstName} {lastName}"));
        }
    }
}
