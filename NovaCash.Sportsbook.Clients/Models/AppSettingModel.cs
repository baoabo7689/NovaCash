using System.Collections.Generic;
using Fanex.Data;

namespace NovaCash.Sportsbook.Clients.Models
{
    public class AppSettingModel
    {
        public string APIURL { get; set; }

        public string APIVendorId { get; set; }

        public int Currency { get; set; }

        public string StoreProceduresPath { get; set; }

        public string HangfireConnection { get; set; }

        public string BetDetailConnection { get; set; }

        public Dictionary<string, ConnectionConfiguration> ConnectionStrings { get; set; }

        public string ExcelFolder { get; set; }
    }
}