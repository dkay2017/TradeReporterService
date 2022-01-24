using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Timers;
using TradeReporterService.Service;

namespace TradeReporterService
{
    /// <summary>
    /// Trade Report Generator Service
    /// </summary>
    /// <seealso cref="System.ServiceProcess.ServiceBase" />
    public partial class TradeReporterService : ServiceBase
    {
        private string filePath = string.Empty;
        const string APP_NAME = "Trade Reporter Service";
        Timer timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TradeReporterService"/> class.
        /// </summary>
        public TradeReporterService()
        {
            InitializeComponent();
            timer = new Timer();
            this.filePath = ConfigurationManager.AppSettings["FilePath"];
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            WriteToLogFile(string.Format("{0} is Started at {1} ", APP_NAME, DateTime.Now.ToLocalTime().ToString()));
            var intervalinSecs = int.Parse(ConfigurationManager.AppSettings["IntervalInSecs"]);
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Interval = intervalinSecs * 1000; //number in milisecinds  
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            WriteToLogFile(string.Format("{0} is Stopped at {1} ", APP_NAME, DateTime.Now.ToLocalTime().ToString()));
        }

        /// <summary>
        /// Called when [timer].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            WriteToLogFile("Generating Trade Report the System -" + DateTime.Now.ToLocalTime());
            GenerateTradeReport();
        }

        /// <summary>
        /// Generates the trade report.
        /// </summary>
        private void GenerateTradeReport()
        {
            var today = DateTime.Now;
            var service = new PowerService();
            var fileName = "PowerPosition_" + today.ToString("yyyyMMdd") + "_" + today.ToString("HHmm") + ".csv";

            try
            {
                var trades = service.GetTrades(today);
                var tradePeriods = new List<PowerPeriod[]>();
                foreach (var t in trades)
                {
                    tradePeriods.Add(t.Periods);
                }
                var aggrPowerPeriods = TradeAggregator.AggregateVolume(tradePeriods);
                CSVGenerator.WriteTradeToCSV(filePath, fileName, aggrPowerPeriods);
            }
            catch (PowerServiceException ex)
            {
                WriteToLogFile(string.Format("Encountered Exception when calling Power position Service GetTrade() with argument {0} , Exception {1}", DateTime.Now.ToString() , ex.Message));
            }
            catch ( Exception ex)
             {
                WriteToLogFile(string.Format("Encountered Exception when generating file  - {0} , Exeception {1}" , fileName,ex.Message ));
            }
        }

        /// <summary>
        /// Writes to log file.
        /// </summary>
        /// <param name="Message">The message.</param>
        public void WriteToLogFile(string Message)
        {
            string path = filePath+ @"Log\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = path + "ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
