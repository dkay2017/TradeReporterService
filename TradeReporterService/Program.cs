using System.ServiceProcess;

namespace TradeReporterService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            #if (!DEBUG)
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new TradeReporterService()
                };
                ServiceBase.Run(ServicesToRun);
            #else
                TradeReporterService serviceCall = new TradeReporterService();
                serviceCall.OnTimer(null,null);
            #endif
        }
    }
}
