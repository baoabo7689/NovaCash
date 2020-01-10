using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using NovaCash.Sportsbook.Clients.Models;

namespace NovaCash.Sportsbook.Clients.Configurations
{
    public class AppSettings
    {
        public static AppSettingModel Settings;
        public static bool IsConsole = false;

        public static void Load(string env)
        {
            if (Settings != null)
            {
                return;
            }

            var evnSuffix = string.IsNullOrWhiteSpace(env) ? string.Empty : $".{env}";
            var settingFile = $"appsettings{evnSuffix}.json";
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), settingFile)))
            {
                SetCurrentDirectory();
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(settingFile, optional: false, reloadOnChange: true)
                .Build();

            Settings = new AppSettingModel
            {
                APIURL = configuration.GetValue<string>("APIURL"),
                APIVendorId = configuration.GetValue<string>("APIVendorId"),
                Currency = configuration.GetValue<int>("Currency"),
                HangfireConnection = configuration.GetValue<string>("HangfireConnection"),
                BetDetailConnection = configuration.GetValue<string>("ConnectionStrings:BetDetailConnection"),
                ExcelFolder = configuration.GetValue<string>("ExcelFolder"),
                GMT = configuration.GetValue<int>("GMT")
            };
        }

        public static void SetCurrentDirectory()
        {
            if (Debugger.IsAttached || IsConsole)
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