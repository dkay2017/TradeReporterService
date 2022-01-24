using CsvHelper;
using Services;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace TradeReporterService.Service
{
    /// <summary>
    /// Trade CSV Output Class 
    /// </summary>
    public class TradeOutput
    {
        public string Period { get; set; }
        public string Volume { get; set; }
    }

    /// <summary>
    /// CSV Generator Class
    /// </summary>
    public static class CSVGenerator
    {
        /// <summary>
        /// Writes the trade to CSV.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="powerPeriods">The power periods.</param>
        public static void WriteTradeToCSV(string path, string fileName, List<PowerPeriod> powerPeriods)
        {
            List<TradeOutput> tradeOutputs = new List<TradeOutput>();
            foreach (var pp in powerPeriods)
            {
                var period = pp.Period == 1 ? "23" : (pp.Period - 2).ToString().PadLeft(2, '0') + ".00";
                tradeOutputs.Add(new TradeOutput() { Period = period, Volume = pp.Volume.ToString() });
            }

            var writer = new StreamWriter(path + fileName);
            var csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture);
            csvWriter.WriteHeader<TradeOutput>();
            csvWriter.NextRecord(); // adds new line after header
            csvWriter.WriteRecords(tradeOutputs);
            csvWriter.Flush();
            return;
        }
    }
}
