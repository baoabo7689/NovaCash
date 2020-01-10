using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NovaCash.Sportsbook.Clients.Configurations;
using NovaCash.SportsbookServices.Workers;

namespace NovaCash.SportsbookServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppSettings.Load(args);

            // new Sportsbook.Clients.ExcelServices.SportsbookExcelService().ExportBetDetail();

            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices(ConfigureServices)
                .Build()
                .Run();
        }

        public static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddHostedService<BetDetailWorker>();
        }
    }
}