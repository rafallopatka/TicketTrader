using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace TicketTrader.Tools.SeedDb.ArrangementReader
{
    public class ArrangementReader
    {
        public Arrangement Read(string filePath)
        {
            var arrangement = new Arrangement { FileName = Path.GetFileName(filePath) };

            XDocument doc = XDocument.Load(filePath);

            string sceneName = doc.Descendants()
                .Single(x => x.Attribute("data-scene-name") != null)
                .Attribute("data-scene-name")
                ?.Value;

            arrangement.DisplayName = sceneName;

            var sectors = doc
                .Descendants()
                .Where(x => x.Attribute("data-class")?.Value == "sector")
                .ToList();

            int sectorId = 0;
            foreach (var currentSector in sectors)
            {
                var sectorName = currentSector.Attribute("data-title")?.Value;
                var dataQuality = Convert.ToInt32(currentSector.Attribute("data-quality")?.Value);
                var priceZoneId = Convert.ToInt32(currentSector.Attribute("data-price-zone-id")?.Value);

                var arrangementSector = new Sector
                {
                    Name = sectorName,
                    DataQuality = dataQuality,
                    PriceZoneId = priceZoneId,
                    SectorId = ++sectorId
                };
                arrangement.Sectors.Add(arrangementSector);

                var seats = currentSector
                    .Descendants()
                    .Where(x => x.Attribute("data-class")?.Value != null)
                    .ToList();

                foreach (var seat in seats)
                {
                    var dataClass = seat.Attribute("data-class")?.Value;
                    SeatType seatType;
                    if (dataClass == "seat")
                    {
                        seatType = SeatType.Single;
                    }
                    else if (dataClass == "unnumbered-seat")
                    {
                        seatType = SeatType.Unnumbered;
                    }
                    else
                    {
                        continue;
                    }

                    int seatId = Convert.ToInt32(seat.Attribute("data-seat-id")?.Value);
                    string seatNumber = seat.Attribute("data-seat-number")?.Value;

                    arrangementSector.Seats.Add(new Seat
                    {
                        Id = seatId,
                        SeatNumber = seatNumber,
                        SeatType = seatType
                    });
                }
            }

            return arrangement;
        }
    }
}
