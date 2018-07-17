using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace TicketTrader.Tools.SvgIndexer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Any() == false)
            {
                PrintError("No files provided");
            }

            foreach (var arg in args)
            {
                IndexSvg(arg);
            }
        }

        private static void IndexSvg(string filePath)
        {
            try
            {
                IndexSvgInternal(filePath);
            }
            catch (Exception e)
            {
                PrintError($"Indexing error in {filePath} \n {e}");
            }
        }

        private static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void IndexSvgInternal(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var directory = filePath.Replace(fileName, string.Empty);
            var newFileName = fileName.Replace(".svg", ".indexed.svg");
            var newFilePath = directory + newFileName;

            Console.WriteLine($"Idexing {filePath}");

            XDocument doc = XDocument.Load(filePath);

            var sectors = doc
                .Descendants()
                .Where(x => x.Attribute("data-class")?.Value == "sector")
                .ToList();

            if (sectors.Any() == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"No sectors found");
                Console.ResetColor();

                return;
            }

            int globalSeatId = 1;
            int sectorId = 0;
            foreach (var currentSector in sectors)
            {
                sectorId++;
                var sectorName = currentSector.Attribute("data-title")?.Value;
                var sectorPricezone = sectorId * 10;
                currentSector.SetAttributeValue("data-price-zone-id", sectorPricezone);

                var seats = currentSector
                    .Descendants()
                    .Where(x =>
                    {
                        var attr = x.Attribute("data-class")?.Value;
                        return attr == "seat" || attr == "unnumbered-seat";
                    } )
                    .ToList();

                int seatNumber = 1;
                foreach (var seat in seats)
                {
                    seat.SetAttributeValue("data-seat-id", globalSeatId);
                    seat.SetAttributeValue("data-seat-number", seatNumber);
                    seat.SetAttributeValue("data-price-zone-id", sectorPricezone);

                    Console.WriteLine($"[{sectorId}]\t[{sectorName}]\t{globalSeatId}");

                    globalSeatId++;
                    seatNumber++;
                }
            }

            if (File.Exists(newFileName))
                File.Delete(newFileName);

            using (var sw = File.CreateText(newFilePath))
            {
                doc.Save(sw);
            }

            Console.WriteLine($"Completed {newFilePath}");
        }
    }
}
