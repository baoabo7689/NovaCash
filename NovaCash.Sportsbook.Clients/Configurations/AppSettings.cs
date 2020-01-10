using System.Diagnostics;
using System.IO;
using System.Linq;
using Fanex.Data;
using Fanex.Data.MySql;
using Microsoft.Extensions.Configuration;
using NovaCash.Sportsbook.Clients.Models;

namespace NovaCash.Sportsbook.Clients.Configurations
{
    public class AppSettings
    {
        public static AppSettingModel Settings;

        public static void Load(string[] args = null)
        {
            if (Settings != null)
            {
                return;
            }

            SetCurrentDirectory(args);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connections = configuration
              .GetSection("ConnectionStrings")
              .GetChildren()
              .ToDictionary(connection => connection.Key,
                            connection => new ConnectionConfiguration(connection.Key, connection.Value));

            Settings = new AppSettingModel
            {
                APIURL = configuration.GetValue<string>("APIURL"),
                APIVendorId = configuration.GetValue<string>("APIVendorId"),
                Currency = configuration.GetValue<int>("Currency"),
                HangfireConnection = configuration.GetValue<string>("HangfireConnection"),
                BetDetailConnection = configuration.GetValue<string>("BetDetailConnection"),
                StoreProceduresPath = configuration.GetValue<string>("StoreProceduresPath"),
                ConnectionStrings = connections,
                ExcelFolder = configuration.GetValue<string>("ExcelFolder")
            };

            DbSettingProviderManager
                .StartNewSession()
                .Use(connections)
                .WithMySql(resourcePath: Settings.StoreProceduresPath)
                .Run();
        }

        public static void SetCurrentDirectory(string[] args)
        {
            var isDebug = Debugger.IsAttached || args.Contains("--console");
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "appSettings.json"))
                || isDebug)
            {
                return;
            }

            var processModule = Process.GetCurrentProcess().MainModule;
            if (processModule == null)
            {
                return;
            }

            var pathToExe = processModule.FileName;
            var pathToContentRoot = Path.GetDirectoryName(pathToExe);
            Directory.SetCurrentDirectory(pathToContentRoot);
        }
    }
}