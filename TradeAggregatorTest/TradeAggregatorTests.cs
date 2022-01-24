using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using System.Collections.Generic;
using TradeReporterService.Service;

namespace TradeReporterUnitTests
{
    [TestClass]
    public class TradeAggregatorTests
    {
        /// <summary>Initialises this instance.</summary>
        [TestInitialize]
        public void Initialise()
        {
        }

        [TestMethod]
        public void When_Aggregating_two_trade_periods_expected_correct_total()
        {
            // Arrange 
            var tradePeriods = new List<PowerPeriod[]>();
            tradePeriods.Add(this.GeneratePowerPeriod(10));
            tradePeriods.Add(this.GeneratePowerPeriod(20));

            //Act 
             var PowerPeriods = TradeAggregator.AggregateVolume(tradePeriods);

            //Assert 
            var expectedResults = this.GeneratePowerPeriod(30);
            PowerPeriods.Should().BeEquivalentTo(expectedResults);
        }

        [TestMethod]
        public void When_Aggregating_two_unOrder_trade_periods_expected_correct_total()
        {
            // Arrange 
            var tradePeriods = new List<PowerPeriod[]>();
            tradePeriods.Add(this.GeneratePowerPeriodUnOrder(10));
            tradePeriods.Add(this.GeneratePowerPeriodUnOrder(20));

            //Act 
            var PowerPeriods = TradeAggregator.AggregateVolume(tradePeriods);

            //Assert 
            var expectedResults = this.GeneratePowerPeriod(30);
            PowerPeriods.Should().BeEquivalentTo(expectedResults);
        }

        [TestMethod]
        public void When_Aggregating_two_unOrder_trade_periods_expected_incorrect_total()
        {
            // Arrange 
            var tradePeriods = new List<PowerPeriod[]>();
            tradePeriods.Add(this.GeneratePowerPeriodUnOrder(10));
            tradePeriods.Add(this.GeneratePowerPeriodUnOrder(20));

            //Act 
            var PowerPeriods = TradeAggregator.AggregateVolume(tradePeriods);

            //Assert 
            var expectedResults = this.GeneratePowerPeriod(55);
            PowerPeriods.Should().NotBeEquivalentTo(expectedResults);
        }



        [TestMethod]
        public void When_Aggregating_two_trade_periods_expected_incorrect_total()
        {
            // Arrange 
            var tradePeriods = new List<PowerPeriod[]>();
            tradePeriods.Add(this.GeneratePowerPeriod(10));
            tradePeriods.Add(this.GeneratePowerPeriod(20));

            //Act 
            var PowerPeriods = TradeAggregator.AggregateVolume(tradePeriods);

            //Assert 
            var expectedResults = this.GeneratePowerPeriod(10);
            PowerPeriods.Should().NotBeEquivalentTo(expectedResults);
        }

        [TestMethod]
        public void When_Aggregating_three_trade_periods_expected_correct_total()
        {
            // Arrange 
            var tradePeriods = new List<PowerPeriod[]>();
            tradePeriods.Add(this.GeneratePowerPeriod(10));
            tradePeriods.Add(this.GeneratePowerPeriod(20));
            tradePeriods.Add(this.GeneratePowerPeriod(30));

            //Act 
            var PowerPeriods = TradeAggregator.AggregateVolume(tradePeriods);

            //Assert 
            var expectedResults = this.GeneratePowerPeriod(60);
            PowerPeriods.Should().BeEquivalentTo(expectedResults);
        }

        [TestMethod]
        public void When_Aggregating_three_trade_periods_expected_incorrect_total()
        {
            // Arrange 
            var tradePeriods = new List<PowerPeriod[]>();
            tradePeriods.Add(this.GeneratePowerPeriod(10));
            tradePeriods.Add(this.GeneratePowerPeriod(20));
            tradePeriods.Add(this.GeneratePowerPeriod(30));
            //Act 
            var PowerPeriods = TradeAggregator.AggregateVolume(tradePeriods);

            //Assert 
            var expectedResults = this.GeneratePowerPeriod(55);
            PowerPeriods.Should().NotBeEquivalentTo(expectedResults);
        }

        /// <summary>
        /// Generates the power period order array.
        /// </summary>
        /// <param name="vol">The vol.</param>
        /// <returns>Array of Power Period</returns>

        private PowerPeriod[] GeneratePowerPeriod(int vol)
        {
            PowerPeriod[] powerPeriod = new PowerPeriod[24];
            
            for (var i = 0;  i< 24; i++)
            {
                powerPeriod[i] = new PowerPeriod() { Period = i+1 , Volume = vol };
           }

            return powerPeriod;
        }

        /// <summary>
        /// Generates the power period un order.
        /// </summary>
        /// <param name="vol">The vol.</param>
        /// <returns>Array of Power Period</returns>
        private PowerPeriod[] GeneratePowerPeriodUnOrder(int vol)
        {
            PowerPeriod[] powerPeriod = new PowerPeriod[24];
            var i = 14;
            powerPeriod[i] = new PowerPeriod() { Period = 18, Volume = vol };
            i = 16;
            powerPeriod[i] = new PowerPeriod() { Period = 20, Volume = vol };
            i = 15;
            powerPeriod[i] = new PowerPeriod() { Period = 19, Volume = vol };

            for (i = 0; i < 14; i++)
            {
                powerPeriod[i] = new PowerPeriod() { Period = i + 1, Volume = vol };
            }

            i = 17;
            powerPeriod[i] = new PowerPeriod() { Period = 15, Volume = vol };
            i = 19;
            powerPeriod[i] = new PowerPeriod() { Period = 17, Volume = vol };
            i = 18;
            powerPeriod[i] = new PowerPeriod() { Period = 16, Volume = vol };
            
            for (i = 20; i < 24; i++)
            {
                powerPeriod[i] = new PowerPeriod() { Period = i+1, Volume = vol };
            }

            return powerPeriod;
        }
    }
}
