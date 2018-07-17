using System.Collections.Generic;

namespace TicketTrader.Tools.SeedDb.ArrangementReader
{
    public class Sector
    {
        public string Name { get; set; }
        public IList<Seat> Seats { get; set; }
        public int SectorId { get; set; }
        public int DataQuality { get; set; }
        public int PriceZoneId { get; set; }

        public Sector()
        {
            Seats = new List<Seat>();
        }
    }
}