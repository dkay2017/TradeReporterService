using Services;
using System;
using System.Collections.Generic;


namespace TradeReporterService.Service
{
    /// <summary>
    /// Trade Aggregator Classs
    /// </summary>
    public static class TradeAggregator
    {
        /// <summary>
        /// Aggregates the volume.
        /// </summary>
        /// <param name="tradePeriods">The trade periods.</param>
        /// <returns></returns>
        public static List<PowerPeriod> AggregateVolume(IEnumerable<PowerPeriod[]> tradePeriods)
        {
            var aggrPowerPeriods = new List<PowerPeriod>();
            foreach (var tradePeriod in tradePeriods)
            {
                foreach (var p in tradePeriod)
                {
                    p.Volume = Math.Round(p.Volume);
                    var app = aggrPowerPeriods.Find(x => x.Period == p.Period);
                    if (app == null)
                    {
                        aggrPowerPeriods.Add(p);
                    }
                    else
                    {
                        app.Volume += p.Volume;
                    }
                }
            }
            return aggrPowerPeriods;
        }
    }
}
