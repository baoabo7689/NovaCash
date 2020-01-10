using System.Linq;
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
            AppSettings.IsConsole = args.Contains("--console");
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices(ConfigureServices)
                .Build()
                .Run();
        }

        public static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            AppSettings.Load(hostContext.HostingEnvironment.EnvironmentName);
            services.AddHostedService<BetDetailWorker>();
        }
    }
}