using System.Collections.Generic;

namespace TicketTrader.Tools.SeedDb.ArrangementReader
{
    public class Arrangement
    {
        public string DisplayName { get; set; }
        public string FileName { get; set; }

        public IList<Sector> Sectors { get; set; }

        public Arrangement()
        {
            Sectors = new List<Sector>();
        }
    }
}