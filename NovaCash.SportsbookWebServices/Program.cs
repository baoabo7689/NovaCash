using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NovaCash.Sportsbook.Clients.Configurations;

namespace NovaCash.SportsbookWebServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppSettings.Load(args);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}